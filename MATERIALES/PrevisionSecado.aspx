<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PrevisionSecado.aspx.cs" Inherits="ThermoWeb.MATERIALES.PrevisionSecado" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Previsión de secado</title>
    <link rel="shortcut icon" type="image/x-icon" href="../FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Previsión de secado             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../LIBERACIONES/EstadoLiberacion.aspx">Liberaciones de serie</a></li>
                <li><a class="dropdown-item" href="UbicacionMateriasPrimas.aspx">Ubicaciones materiales</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Instrucciones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../SMARTH_docs/POCS/ITGs-125 -PROCEDIMIENTO MATERIALES Ed.3.pdf">ITGs-125 Procedimiento preparación materiales Ed.3</a></li>
                <li><a class="dropdown-item" href="../SMARTH_docs/POCS/ITGs-141 Instrucción manipulación secado (alimentación manual) Ed.2.pdf">ITGs-141 Manipulación secado alimentación manual Ed.2</a></li>

            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript"><%--Configurar onloads--%>
    window.onload = function () {
        document.getElementById("BTNIMPRIMIR").onclick = function fun() {
            //alert("ENTRA");
            ImprimeEtiquetasNEW();
            //validation code to see State field is mandatory.  
        }
    }
    </script>
    <%--Scripts de botones --%>
    <style>
        th {
            background: #0000ff !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
        }

        th, td {
            padding: 0.25rem;
        }

        .ui-front {
            z-index: 9999999 !important;
        }
    </style>
    <script type="text/javascript">
        function ShowPopupEditaDetalles() {
            if (document.getElementById("lblMatReciclado").innerText == "") {
                document.getElementById("RecicladoInput").hidden = false;
                document.getElementById("RecicladoPill").hidden = true;
            }
            else {
                document.getElementById("RecicladoInput").hidden = true;
                document.getElementById("RecicladoPill").hidden = false;
            }
            document.getElementById("AUXMODALEDITARMATERIAL").click();
        }
        function ShowPopupImprimirEtiquetas() {
            document.getElementById("AUXMODALIMPRIMIRETIQUETA").click();
        }
        function AlertaSinLoguear() {
            alert("Debes estar logueado para poder ejecutar esta acción.");
        }
        function ActualizarGRID() {
            document.getElementById("BTN_RELLENARGRID").click();
        }
        function ShowPopupEstructura() {
            document.getElementById("btnPopEstructura").click();
        }
        function ImprimeEtiquetasNEW() {
            document.getElementById("ICONPRINTER").setAttribute("class", "spinner-border");
            $.ajax({
                type: "POST",
                url: "PrevisionSecado.aspx/ImprimirEtiquetasV2",
                data: "{MATERIAL: '" + document.getElementById("AUX_MATERIAL").value + "', DESCRIPCION: '" + document.getElementById("AUX_DESCRIPCION").value + "', INPUTOPERARIO: '" + document.getElementById("InputOperario").value + "', LOTE: '" + document.getElementById("InputLote").value + "', TIPO: '" + document.getElementById("DropTipoPrint").value + "', CANTIDAD: '" + document.getElementById("InputCantidad").value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert("Vamos");
                    document.getElementById("CIERRAPOP").click();
                    document.getElementById("ICONPRINTER").setAttribute("class", "bi bi-printer-fill");
                },
                failure: function (response) {
                    //alert("NoVamos");
                    document.getElementById("CIERRAPOP").click();
                    document.getElementById("ICONPRINTER").setAttribute("class", "bi bi-printer-fill");
                }
            });
        }
        function ClosePopup() {
            document.getElementById("CIERRAPOP").click();
        }
        function myFunction() {
            __doPostBack();
        }
        function GRecAñadeMat() {
            document.getElementById("lblMatReciclado").innerText = document.getElementById("inputMatReciclado").innerText;
            $.ajax({
                type: "POST",
                url: "GestionMateriales.aspx/Inserta_Material_Reciclado",
                data: "{MATERIAL: '" + document.getElementById("HiddenMaterial").value + "', RECICLADO: '" + document.getElementById("inputMatReciclado").value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    if (r.d != "0") {
                        //alert("Vamos");
                        //document.getElementById("CIERRAPOP").click();
                        //document.getElementById("ICONPRINTER").setAttribute("class", "bi bi-printer-fill");

                        document.getElementById("RecicladoInput").hidden = true;//oculta
                        document.getElementById("RecicladoPill").hidden = false;//muestra
                        document.getElementById("lblMatReciclado").innerText = r.d;
                    }
                    else {
                        alert("El material no es válido.");
                    }
                },
                failure: function (response) {
                    alert("Se ha producido un error.");
                    //document.getElementById("CIERRAPOP").click();
                    //document.getElementById("ICONPRINTER").setAttribute("class", "bi bi-printer-fill");

                }
            })
        };
        function GRecEliminaMat() {
            //document.getElementById("lblMatReciclado").innerText = document.getElementById("inputMatReciclado").innerText;
            $.ajax({
                type: "POST",
                url: "GestionMateriales.aspx/Elimina_Material_Reciclado",
                data: "{MATERIAL: '" + document.getElementById("HiddenMaterial").value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {

                    document.getElementById("RecicladoInput").hidden = false;
                    document.getElementById("RecicladoPill").hidden = true;
                    document.getElementById("lblMatReciclado").innerText = "";
                },
                failure: function (response) {
                    alert("Se ha producido un error.");
                    //document.getElementById("CIERRAPOP").click();
                    //document.getElementById("ICONPRINTER").setAttribute("class", "bi bi-printer-fill");

                }
            })
        };
        $(function () {
            $("#NUMMaterial").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../AutoCompleteServicio.asmx/GetAutoCompleteListaMateriales", // Ruta al método web de servidor
                        data: JSON.stringify({ term: request.term }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (xhr, status, error) {
                            console.log(xhr.responseText);
                        }
                    });
                },
                minLength: 2 // Longitud mínima para activar el autocompletado
            });
        });
        $(function () {
            $("#inputMatReciclado").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../AutoCompleteServicio.asmx/GetAutoCompleteListaMateriales", // Ruta al método web de servidor
                        data: JSON.stringify({ term: request.term }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (xhr, status, error) {
                            console.log(xhr.responseText);
                        }
                    });
                },
                minLength: 2 // Longitud mínima para activar el autocompletado
            });
        });
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
        $(document).on('change', '.CheckMostrarPreparados', function (e) {
            document.getElementById("GridAuxiliar").click();
        });
    </script>
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="row">
            <div class="col-lg-3">
                <div class="form-check form-switch ms-4 mt-2">
                    <input class="form-check-input CheckMostrarPreparados" runat="server" type="checkbox" role="switch" id="CheckMostrarPreparados" checked="">
                    <label class="form-check-label fw-bold" for="flexSwitchCheckChecked">Mostrar materiales preparados</label>
                </div>
            </div>
            <div class="col-lg-9">
                <div class="float-end mt-2">
                    <asp:Label ID="label" runat="server" Font-Size="Large" CssClass="ms-3 mt-2">Última actualización:&nbsp</asp:Label><asp:Label ID="FECHAACT" runat="server" Font-Bold="true" Font-Size="Large" Font-Italic="true"></asp:Label>
                </div>
                 <button id="GridAuxiliar" runat="server" onserverclick="Rellenar_Grid" type="button" hidden="hidden" class="btn btn-lg btn-primary border border-dark shadow" style="text-align: left"></button>
                <button id="AUXMODALIMPRIMIRETIQUETA" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalImprimirEtiqueta" style="font-size: larger"></button>
                <button id="AUXMODALEDITARMATERIAL" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditarMaterial" style="font-size: larger"></button>
                <button type="button" id="btnPopEstructura" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopEstructura"></button>
            </div>

        </div>
        <div class="container-fluid mt-2">
            <div class="col-lg-12">
                <ul class="nav nav-pills nav-fill" id="pills-tab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="BTN_ESTADO_SECADO" runat="server" data-bs-toggle="pill" data-bs-target="#pill_secados" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><strong>PREVISIÓN DE SECADO</strong></button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link " id="BTN_CONSUMO_MAQUINAS" runat="server" data-bs-toggle="pill" data-bs-target="#pills_ConsumoRestante" type="button" role="tab" aria-controls="pills-home" aria-selected="true"><strong>MATERIALES EN MÁQUINA</strong></button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="BTN_LISTADO_PRODUCTOS" runat="server" data-bs-toggle="pill" data-bs-target="#pills_listamateriales" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><strong>MATERIALES Y COMPONENTES</strong></button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="BTN_ENTRADAMATS" runat="server" data-bs-toggle="pill" data-bs-target="#pills_entradamateriales" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><strong>ENTRADAS MATERIALES</strong></button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link " id="BTN_LIBERACIONES" runat="server" data-bs-toggle="pill" data-bs-target="#pills_liberaciones" type="button" role="tab" aria-controls="pills-home" aria-selected="true"><strong>LIBERACIONES DE SERIE</strong></button>
                    </li>
                </ul>
                <div class="tab-content" id="pills-tabContent">
                    <div class="tab-pane fade show active" id="pill_secados" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <asp:GridView ID="dgv_secado" runat="server" AllowSorting="True" Style="margin-left: 1%;" Width="100%" CssClass="table table-responsive shadow border border-secondary rounded" AutoGenerateColumns="false"
                            OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="OnRowDataBound" EmptyDataText="There are no data records to display.">
                            <HeaderStyle BackColor="#0000ff" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#ccccff" />
                            <Columns>

                                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="X-Large" ItemStyle-ForeColor="white" ItemStyle-BackColor="#3366ff">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("MAQ") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Iniciar secado" ItemStyle-Width="9%" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoActual" Font-Size="X-Large" runat="server" Text='<%#Eval("INICIARSECADO") %>' />
                                        <asp:Label ID="lblFechacambio" Font-Italic="true" runat="server" Text='<%# "&nbsp("+Eval("FECHA")+")" %>' /></br>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="6%" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("ORDEN") %>' />
                                        <asp:LinkButton runat="server" ID="BTNMaterialPrep" CommandName="MaterialPreparado" CommandArgument='<%#Eval("ORDEN") + "¬" + Eval("ORDENVER") + "¬" + Eval("MATERIAL")+"¬SECADO"%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-danger border border-dark shadow" Style="font-size: 1rem">
                                          <i class="bi bi-arrow-bar-right"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Material" ItemStyle-Width="20%" ItemStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <label>
                                        </label>
                                        &nbsp Material</label>
                                        <%-- <input type="checkbox" class="btn-check" id="btn-check-outlined" autocomplete="off" checked>
                                            <label class="btn btn-sm btn-outline-light" for="btn-check-outlined"><i class="bi bi-eye"></i>
                                        --%>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:Label ID="lblReferencia" runat="server" Font-Size="X-Large" Text='<%#Eval("MATERIAL") + " - "%>' />
                                        <asp:Label ID="Label2" runat="server" Font-Size="Large" Text='<%#Eval("DESCRIPCION")%>' />
                                        <asp:Label ID="lblDescripcionLONG" runat="server" Visible="false" Font-Size="Large" Text='<%#Eval("DESCRIPCIONLONG")%>' />
                                        <asp:LinkButton runat="server" ID="btnEstructura" CommandName="EstructuraMSEQ" CommandArgument='<%#Eval("MAQ") + "¬" + Eval("SEQNR") + "¬" + Eval("ORDEN")%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-outline-dark shadow shadow-sm ms-2" Style="font-weight: bold">
                                         <i class="bi bi-three-dots-vertical"></i>
                                        </asp:LinkButton><br />
                                        <asp:Label ID="lblFechasecado" Visible="false" Font-Italic="true" Font-Bold="true" runat="server" Text='<%# "Preparado por "+Eval("PREPARADOR")+" ("+Eval("INICIOSECADO","{0:dd/MM HH:mm}") +")" %>' /></br>
                                         <asp:HiddenField ID="HiddenInicioSecado" runat="server" Value='<%#Eval("INICIOSECADO")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="A preparar" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoCambio" Font-Bold="true" runat="server" Text='<%#Eval("PREPARAR") %>' /><br />
                                        <asp:Label ID="lblUbicacionNew" runat="server" Text='<%#"<i><strong>" + Eval("LOTE") + "</strong></i>" + " - " + Eval("UBICACION") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disponible" ItemStyle-Width="9%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRepiticiones" runat="server" Text='<%#Eval("REPETICIONES") %>' Visible="false" />
                                        <asp:Label ID="lblDisponible" runat="server" Font-Bold="true" Text='<%#Eval("DISPONIBLE", "{0:0.00 Kg}") %>' /><br />
                                        <asp:Label ID="lblPrevision" runat="server" Font-Size="Medium" Font-Italic="true" Text='<%#"Próx. ent: " + Eval("PREVISION","{0:dd/MM/yyyy}")%>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ubicación" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("UBICACION")  %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notas" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <%-- <asp:Label ID="lblEstadoProduccion" Font-Bold="true" runat="server" Text='<%#Eval("NOTAS") %>' />--%>
                                        <asp:Label ID="lblSecaDetalle" Font-Bold="true" runat="server" Text='<%#Eval("TEMPSECADO") + " | " + Eval("TIEMPOSECADO") %>' />
                                        <asp:Label ID="lblConsumo" Font-Size="Smaller" runat="server" Visible="false" Text='<%#"<br />(Preparar " + Eval("SUMAMATS", "{0:0.00 Kg}") +" para los próximos 3 días)" %>' />
                                        <asp:HiddenField ID="HiddenTempSecado" runat="server" Value='<%#Eval("TEMPSECADO") %>' />
                                        <asp:HiddenField ID="HiddenSecadoSN" runat="server" Value='<%#Eval("SECARSN") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="button2" CommandName="ImprimirEtiqueta" CommandArgument='<%#Eval("MATERIAL") + "¬" + Eval("DESCRIPCIONLONG")+"¬SECADO"%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow" Style="font-size: 1rem">
                                          <i class="bi bi-receipt"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="tab-pane fade" id="pills_ConsumoRestante" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <asp:GridView ID="dgv_estado_maquina" runat="server" AllowSorting="True" Style="margin-left: 1%;" Width="100%" CssClass="table table-responsive shadow border border-secondary rounded" AutoGenerateColumns="false"
                            OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="OnRowDataBound2" EmptyDataText="There are no data records to display.">
                            <HeaderStyle BackColor="#0000ff" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#ccccff" />
                            <Columns>
                                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="X-Large" ItemStyle-ForeColor="white" ItemStyle-BackColor="#3366ff">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("MAQ") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material actual" ItemStyle-Width="30%" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="BTNMaterialPrep" CommandName="MaterialPreparado" CommandArgument='<%#Eval("ORDEN") + "¬" + Eval("ORDENVER") + "¬" + Eval("MATERIAL") + "¬TABLAESTADO"%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-danger border border-dark shadow" Style="font-size: 1rem">
                                          <i class="bi bi-arrow-bar-right"></i>
                                        </asp:LinkButton>
                                        <asp:Label ID="lblReferencia" runat="server" Font-Size="X-Large" Text='<%#Eval("MATERIAL") + " - "%>' />
                                        <asp:Label ID="Label2" runat="server" Font-Size="Large" Text='<%#Eval("DESCRIPCION")%>' />
                                        <asp:Label ID="lblDescripcionLONG" runat="server" Visible="false" Font-Size="Large" Text='<%#Eval("DESCRIPCIONLONG")%>' />
                                        <asp:LinkButton runat="server" ID="btnEstructura" CommandName="Estructura" CommandArgument='<%#Eval("MAQ") %>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-outline-dark shadow shadow-sm ms-2" Style="font-weight: bold">
                                         <i class="bi bi-three-dots-vertical"></i>
                                        </asp:LinkButton><br />
                                        <asp:Label ID="lblFechasecado" Visible="false" Font-Italic="true" Font-Bold="true" runat="server" Text='<%# "Preparado por "+Eval("PREPARADOR")+" ("+Eval("INICIOSECADO","{0:dd/MM HH:mm}") +")" %>' /></br>
                                         <asp:HiddenField ID="HiddenInicioSecado" runat="server" Value='<%#Eval("INICIOSECADO")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                    <HeaderTemplate>
                                        <label>Necesario para terminar</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRestantes" Font-Bold="true" runat="server" Text='<%#Eval("KGS_RESTANTE") + " kgs." %>' /><br />
                                        <asp:Label ID="lblNecesidad" runat="server" Font-Size="Medium" Font-Italic="true" Text='<%#"para completar " + Eval("ORDENES" ) + " órdenes"%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                    <HeaderTemplate>
                                        <label>Ubicación</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUbicacionNew" runat="server" Font-Bold="true" Text='<%#Eval("UBICACION") %>' /><br />
                                        <asp:Label ID="lblDisponible" runat="server" Font-Size="Medium" Font-Italic="true" Text='<%#"disponibles " + Eval("DISPONIBLE", "{0:0.00 Kg}") + " en almacén" %>' /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notas" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecadoHabilitado" Font-Size="X-Large" Font-Bold="true" runat="server" Text='<%#Eval("SECADOHABILITADO") %>' />
                                        <asp:Label ID="lblSecTemp" Font-Bold="true" Font-Italic="true" runat="server" Text='<%#Eval("SecadoTemp")%>' /><br />
                                        <asp:Label ID="lblSecTiemp" Font-Size="Medium" Font-Italic="true" runat="server" Text='<%#Eval("SECADOTIEMP")%>' />
                                        <asp:HiddenField ID="HiddenTempSecado" runat="server" Value='<%#Eval("SecadoTemp") %>' />
                                        <asp:HiddenField ID="HiddenSecadoSN" runat="server" Value='<%#Eval("SECADOHABILITADO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="button2" CommandName="ImprimirEtiquetaEST" CommandArgument='<%#Eval("MATERIAL") + "¬" + Eval("DESCRIPCIONLONG") + "¬TABLAESTADO"%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow" Style="font-size: 1rem">
                                          <i class="bi bi-receipt"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="tab-pane fade" id="pills_listamateriales" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <div class="row">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-1">
                                <h6>Familia</h6>
                                <asp:DropDownList ID="DropFamiliaFiltro" CssClass="form-select border border-dark" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1">
                                <h6>Tipo</h6>
                                <asp:DropDownList ID="DropTipoFiltro" CssClass="form-select border border-dark" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1">
                                <h6>Reciclado</h6>
                                <asp:DropDownList ID="DropRecicladoFiltro" CssClass="form-select border border-dark" runat="server">
                                    <asp:ListItem Value="-">-</asp:ListItem>
                                    <asp:ListItem Value="1">Mat. alternativo</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1">
                                <h6>Secar</h6>
                                <asp:DropDownList ID="DropSecadoFiltro" CssClass="form-select border border-dark" runat="server">
                                    <asp:ListItem Value="-">-</asp:ListItem>
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1">
                                <h6>Documentos</h6>
                                <asp:DropDownList ID="DropDocumentosFiltro" CssClass="form-select border border-dark" runat="server">
                                    <asp:ListItem Value="-">-</asp:ListItem>
                                    <asp:ListItem Value="0">Sin ficha téc.</asp:ListItem>
                                    <asp:ListItem Value="1">Sin ficha seg.</asp:ListItem>
                                    <asp:ListItem Value="2">Sin adjuntos</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-4" style="text-align: right">
                                <h6>Referencia</h6>
                                <div class="input-group mb-3">
                                    <input class="form-control border border-dark shadow" list="DatalistNUMMaterial" id="NUMMaterialFiltro" runat="server" placeholder="Escribe un producto...">
                                    <datalist id="DatalistNUMMaterial" runat="server">
                                    </datalist>
                                    <button id="BTNFiltraMateriales" runat="server" onserverclick="Rellenar_Grid" type="button" class="btn btn-lg btn-primary border border-dark shadow" style="text-align: left">
                                        <i class="bi bi-search"></i>
                                    </button>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="dgv_Materiales" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="100%" CssClass="table table-responsive shadow border border-secondary rounded" AutoGenerateColumns="false"
                                    OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="OnRowDataBound3" EmptyDataText="There are no data records to display.">
                                    <HeaderStyle BackColor="#0000ff" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#ccccff" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Size="X-Large" ItemStyle-Width="3%" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="BTNEditMat" CommandName="EditarMaterial" CommandArgument='<%#Eval("MATERIAL")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow" Style="font-size: 1rem">
                                          <i class="bi bi-pencil"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="center" ItemStyle-Width="10%">
                                            <HeaderTemplate>
                                                <asp:Label ID="hdMaterial" Font-Size="X-Large" runat="server" Text='Material' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterial" Font-Size="XX-Large" runat="server" Text='<%#Eval("MATERIAL") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Font-Size="Large" ItemStyle-Width="35%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrden" Font-Bold="true" runat="server" Text='<%#Eval("DESCRIPCION") %>' /><br />
                                                <asp:Label ID="lblTipoMat" Font-Size="Medium" Font-Italic="true" runat="server" Text='<%#Eval("TipoMaterial") %>' /><asp:Label ID="lblRecicla" Font-Size="Medium" Font-Italic="true" runat="server" Text='<%#" - Recicla en: " + Eval("REFERENCIARECICLADO") %>' /><br />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock y ubicación" ItemStyle-Font-Size="Large" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCantalm" runat="server" Font-Bold="true" Font-Italic="true" Text='<%#Eval("DISPONIBLE", "{0:0 Kg./Uds.}") %>' /><br />
                                                <asp:Label ID="lblPrevision" runat="server" Font-Size="Medium" Font-Italic="true" Text='<%#"Previsión: " + Eval("PREVISION", "{0:dd/MM/yyyy}") + " - " + Eval("QUANTITY", "{0:0.#}") + " uds." %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Font-Size="Large" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUbicacion" runat="server" Font-Bold="true" Font-Italic="true" Text='<%#Eval("Ubicacion") %>' /><br />
                                                <asp:Label ID="lblUbicacionAUX" runat="server" Font-Size="Medium" Font-Italic="true" Visible="false" Text='Almacén de materiales' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large" ItemStyle-Width="12%">
                                            <HeaderTemplate>
                                                <asp:Label ID="hdSECADO" Font-Size="X-Large" runat="server" Text='¿Secado?' />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSecadoHabilitado" Font-Size="X-Large" Font-Bold="true" runat="server" Text='<%#Eval("SECADOHABILITADO") %>' />
                                                <asp:Label ID="lblSecTemp" Font-Bold="true" Font-Italic="true" runat="server" Text='<%#Eval("SecadoTemp")%>' /><br />
                                                <asp:Label ID="lblSecTiemp" Font-Size="Medium" Font-Italic="true" runat="server" Text='<%#Eval("SECADOTIEMP")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Size="X-Large" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Right" HeaderText="Documentos">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="IMGICONOTEC" runat="server" CssClass="btn btn-lg btn-outline-dark border-0" Height="45px" Visible="false" CommandName="RedirectFicha" CommandArgument='<%#Eval("FichaMaterial") %>'><i class="bi bi-filetype-doc"><h6  style="font-size:small">Téc.</h6></i></asp:LinkButton>
                                                <asp:HiddenField ID="LBLICONOTEC" runat="server" Value='<%#Eval("FichaMaterial") %>' />
                                                <asp:LinkButton ID="IMGICONOSEC" runat="server" CssClass="btn btn-lg btn-outline-dark border-0 me-2" Height="45px" Visible="false" CommandName="RedirectFicha" CommandArgument='<%#Eval("FichaSeguridad") %>'><i class="bi bi-filetype-doc"><h6 style="font-size:small">Seg.</h6></i></asp:LinkButton>
                                                <asp:HiddenField ID="LBLICONOSEC" runat="server" Value='<%#Eval("FichaSeguridad") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="button2" CommandName="ImprimirEtiqueta" CommandArgument='<%#Eval("MATERIAL") + "¬" + Eval("DESCRIPCION") + "¬TABLAMATERIALES"%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow" Style="font-size: 1rem">
                                          <i class="bi bi-receipt"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                    <div class="tab-pane fade" id="pills_entradamateriales" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="dgv_EntradasPrevistas" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="100%" CssClass="table table-responsive shadow border border-secondary rounded" AutoGenerateColumns="false"
                                    EmptyDataText="There are no data records to display.">
                                    <HeaderStyle BackColor="#0000ff" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#ccccff" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Fecha prevista" ItemStyle-Width="12%" ItemStyle-Font-Size="X-Large">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFecha" CssClass="ms-2" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("FECHA", "{0:dd/MM/yyyy}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material" ItemStyle-Width="40%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstadoActual" Font-Size="X-Large" Font-Bold="true" runat="server" Text='<%#Eval("No_") %>' />
                                                <asp:Label ID="lblOrden" Font-Size="Large" Font-Italic="true" runat="server" Text='<%#Eval("Description") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad" ItemStyle-Font-Size="Large" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCantalm" runat="server" Font-Bold="true" Font-Italic="true" Text='<%#Eval("Quantity", "{0:0 Kg./Uds.}") %>' /><br />
                                                <%--  <asp:Label ID="lblCajas" runat="server" Font-Size="Small" Font-Italic="true" Text="en .. cajas" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Font-Size="Large" ItemStyle-Width="43%"></asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="pills_liberaciones" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <asp:GridView ID="dgv_Liberaciones" runat="server" AllowSorting="True"
                            Width="100%" CssClass="table table-responsive shadow p-3 border border-secondary rounded" AutoGenerateColumns="false"
                            OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridView_DataBound_Liberaciones"
                            EmptyDataText="There are no data records to display.">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="#e6e6e6" />
                            <RowStyle BackColor="white" CssClass="shadow" />
                            <EditRowStyle BackColor="#ffcc66" />
                            <FooterStyle BackColor="Silver" BorderColor="#808080" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-CssClass="shadow border border-secondary">
                                    <HeaderTemplate>
                                        <asp:DropDownList ID="DropSelecMaq" runat="server" CssClass="bg-transparent border-0 text-white fw-bold">
                                            <asp:ListItem Value="Máq." class="text-black">Máq.</asp:ListItem>
                                            <asp:ListItem Value="NAVE 3" class="text-black">Nave 3</asp:ListItem>
                                            <asp:ListItem Value="NAVE 4" class="text-black">Nave 4</asp:ListItem>
                                            <asp:ListItem Value="NAVE 5" class="text-black">Nave 5</asp:ListItem>
                                            <asp:ListItem Value="Máq." class="text-black">Todas</asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaquina" runat="server" Font-Size="X-Large" Text='<%#Eval("Maquina") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="10%">
                                    <HeaderTemplate>
                                        <asp:DropDownList ID="DropSelecEstado" runat="server" CssClass="bg-transparent border-0 text-white fw-bold">
                                            <asp:ListItem Value="0" class="text-black">Estado</asp:ListItem>
                                            <asp:ListItem Value="1" class="text-black">En marcha</asp:ListItem>
                                            <asp:ListItem Value="2" class="text-black">Parada</asp:ListItem>
                                            <asp:ListItem Value="0" class="text-black">Todas</asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoActual" runat="server" Font-Italic="true" Text='<%#Eval("EstadoMaquina") %>' />
                                        <br />
                                        <asp:Label ID="lblAccionLiberado" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("AccionLiberado") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                    <HeaderTemplate>
                                        <asp:DropDownList ID="DropPrioridad" runat="server" CssClass="bg-transparent border-0 text-white fw-bold">
                                            <asp:ListItem Value="0" class="text-black">Prior.</asp:ListItem>
                                            <asp:ListItem Value="1" class="text-black">Ascendente</asp:ListItem>
                                            <asp:ListItem Value="2" class="text-black">Descendente</asp:ListItem>
                                            <asp:ListItem Value="3" class="text-black">No ordenar</asp:ListItem>
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrioridad" runat="server" Font-Italic="true" Font-Size="X-LARGE" Text='<%#Eval("PRIORIDADTEXT") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Image ID="IMGliente" runat="server" Width="59px" src='<%#Eval("LogotipoSM") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orden y Producto" ItemStyle-Width="35%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrden" runat="server" Font-Bold="true" Text='<%#Eval("Orden") + " - " + Eval("Referencia")%>' />
                                        <br />
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cambiador" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="border-start border-secondary">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoCambio" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCAMBIO") %>' />
                                        <br />
                                        <asp:Label ID="lblfechaCambiador" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHACAMBIADORLIBERADO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Producción" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoProduccion" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARPRODUCCION") %>' />
                                        <br />
                                        <asp:Label ID="lblfechaproduccion" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHAPRODUCCIONLIBERADO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calidad" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="border-end border-secondary">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoCalidad" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCALIDAD") %>' />
                                        <br />
                                        <asp:Label ID="lblfechaCalidad" Font-Size="Smaller" runat="server" Text='<%#Eval("FECHACALIDADLIBERADO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notas" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoNotas" runat="server" Text='<%#Eval("NotasLiberado") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--Botones de eliminar y editar cliente...--%>
                                        <asp:LinkButton runat="server" ID="btnVer" CommandName="Redirect" CommandArgument='<%#Eval("Orden")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>


                </div>
                <div class="modal fade" id="ModalImprimirEtiqueta" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header bg-primary shadow">

                                <h5 class="modal-title text-white" id="staticBackdropLabel" runat="server"><i class="bi bi-printer">&nbsp Imprimir etiquetas</i></h5>
                                <button id="CIERRAPOP" type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>


                            </div>

                            <div class="modal-body" runat="server">
                                <div class="row">
                                    <asp:Label runat="server" ID="LblImpDESCMAT" Font-Size="X-Large" Font-Italic="true" Font-Bold="true" CssClass="mb-2">---</asp:Label>
                                </div>

                                <div class="row ms-2 me-2">

                                    <asp:HiddenField runat="server" ID="AUX_MATERIAL" />
                                    <asp:HiddenField runat="server" ID="AUX_DESCRIPCION" />
                                    <h5>Escribe el lote y el operario que lo prepara:</h5>
                                    <div>
                                        <div class="input-group input-group">
                                            <asp:Label ID="Label1" TextMode="Number" runat="server" Font-Size="Large" Width="35%" CssClass="form-control border border-dark shadow"><i class="bi bi-receipt-cutoff">&nbsp <strong>Etiquetas:</strong></i></asp:Label>
                                            <asp:DropDownList ID="DropTipoPrint" runat="server" Font-Size="Large" Width="65%" CssClass="form-select border border-dark shadow">
                                                <asp:ListItem Value="0" Text="Estufa y material"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Sólo estufa"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Sólo material"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Cubetas y auxiliares"></asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                        <div class="input-group input-group">
                                            <asp:Label ID="Operario" TextMode="Number" runat="server" Font-Size="Large" Width="35%" CssClass="form-control border border-dark shadow"><i class="bi bi-person-circle">&nbsp <strong>Operario:</strong></i></asp:Label>
                                            <asp:DropDownList ID="InputOperario" runat="server" Font-Size="Large" Width="65%" CssClass="form-select border border-dark shadow">
                                                <asp:ListItem Value="0" Text="-"></asp:ListItem>
                                                <asp:ListItem Value="517" Text="CARLOS NARANJO"></asp:ListItem>
                                                <asp:ListItem Value="694" Text="ESTIUAR DE JESUS"></asp:ListItem>
                                                <asp:ListItem Value="305" Text="JULIAN GHIORLAN"></asp:ListItem>
                                                <%-- <asp:ListItem Value="449" Text="YOUSSEF ECH CHINE"></asp:ListItem>--%>
                                                <asp:ListItem Value="425" Text="JAOUAD HASSAOUI"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="input-group input-group">
                                            <asp:Label TextMode="Number" runat="server" Font-Size="Large" Width="35%" CssClass="form-control border border-dark shadow"><i class="bi bi-box">&nbsp <strong>Cantidad:</strong></i></asp:Label>
                                            <asp:TextBox ID="InputCantidad" TextMode="Number" runat="server" Font-Size="Large" Width="65%" CssClass="form-control border border-dark shadow"></asp:TextBox>
                                        </div>
                                        <div class="input-group input-group-lg ">
                                            <asp:Label TextMode="Number" runat="server" Font-Size="X-Large" Width="35%" CssClass="form-control border border-dark shadow"><i class="bi bi-dropbox">&nbsp <strong>Lote:</strong></i></asp:Label>
                                            <asp:TextBox ID="InputLote" TextMode="Number" runat="server" Font-Size="X-Large" Width="65%" CssClass="form-control border border-dark shadow"></asp:TextBox>
                                        </div>
                                         
                                        <%-- <button class="btn btn-primary btn-lg shadow" runat="server" style="width: 100%" type="button" onserverclick="ImprimirLabel"><i class="bi bi-printer-fill"></i></button>--%>
                                        <button type="button" id="BTNIMPRIMIR" class="btn btn-primary btn-lg shadow" style="width: 100%; font-weight: bold">
                                            <i id="ICONPRINTER" class="bi bi-printer-fill"></i>
                                        </button>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="PopEstructura" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header bg-warning">
                                <label id="TituloEstructura" style="font-size: x-large; font-weight: bold">Estructura de materiales</label>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body" style="height: 900px">
                                <label id="lblEstructuraProducto" style="font-size: large; font-weight: bold" runat="server"></label>
                                <asp:GridView ID="GridEstructura" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                                    EmptyDataText="No hay datos para mostrar.">
                                    <HeaderStyle BackColor="orange" Font-Bold="True" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Orden" ItemStyle-BackColor="#ccccff">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("ORDEN") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("MATERIAL") %>' />
                                                <asp:Label ID="lblDescripción" runat="server" Text='<%#" " + Eval("DESCRIPCION") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ubicación">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUbicacion" runat="server" Text='<%#Eval("UBICACION") %>' Font-Size="Large" Font-Bold="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Necesario">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConsumo" runat="server" Text='<%#Eval("CONSUMOORDEN") %>' Font-Size="Large" Font-Bold="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("NOTAS") %>' Font-Size="Large" Font-Bold="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <label id="lblEstructuraOrden" style="font-size: large; font-weight: bold" runat="server" class="mt-1">Materiales necesarios para la producción (todas las órdenes):</label>
                                <asp:GridView ID="GridEstructuraOrden" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                                    EmptyDataText="No hay datos para mostrar.">
                                    <HeaderStyle BackColor="orange" Font-Bold="True" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Material">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("MATERIAL") %>' />
                                                <asp:Label ID="lblDescripción" runat="server" Text='<%#" " + Eval("DESCRIPCION") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ubicación">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUbicacion" runat="server" Text='<%#Eval("UBICACION") %>' Font-Size="Large" Font-Bold="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total necesario">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConsumo" runat="server" Text='<%#Eval("CONSUMOORDEN") %>' Font-Size="Large" Font-Bold="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("NOTAS") %>' Font-Size="Large" Font-Bold="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>

                            <div class="modal-footer bg-warning">
                                <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="ModalEditarMaterial" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header bg-primary shadow">
                                <h5 class="modal-title text-white" id="ModalNombreMaterial" runat="server"><i class="bi bi-pencil-square">&nbsp Editar material</i></h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body" runat="server">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h4>Tipo de material:</h4>
                                        <asp:DropDownList CssClass="form-select border border-dark mb-2 shadow" Width="100%" ID="DropTipoMaterial" runat="server"></asp:DropDownList>
                                        <h4>Parámetros de secado:</h4>
                                        <div class="input-group border border-dark rounded">
                                            <span class="input-group-text fw-bold" style="width: 30%">Tiempo</span>
                                            <span class="input-group-text fw-bold" style="width: 15%">MIN.</span>
                                            <input type="number" style="width: 15%" runat="server" id="TxtTiempMIN" class="form-control">
                                            <span class="input-group-text fw-bold" style="width: 15%">MAX.</span>
                                            <input type="number" class="form-control" runat="server" id="TxtTiempMAX" style="width: 15%">
                                            <span class="input-group-text fw-bold" style="width: 10%">H</span>
                                        </div>
                                        <div class="input-group border  border-dark rounded">
                                            <span class="input-group-text  fw-bold" style="width: 30%">Temperatura</span>
                                            <span class="input-group-text fw-bold" style="width: 15%">MIN.</span>
                                            <input type="number" runat="server" id="TxtTempMIN" class="form-control" style="width: 15%">
                                            <span class="input-group-text fw-bold" style="width: 15%">MAX.</span>
                                            <input type="number" runat="server" id="TxtTempMAX" class="form-control" style="width: 15%">
                                            <span class="input-group-text fw-bold" style="width: 10%">°C</span>
                                        </div>
                                        <div class="form-check form-switch float-end mt-1 mb-3">
                                            <label class="form-check-label fw-bold" for="flexSwitchCheckChecked">¿Es necesario secarlo?</label>
                                            <div class="btn-group" role="group" aria-label="Basic example">
                                                <input type="radio" class="btn-check" runat="server" name="options-outlined2" id="successoutlined2" autocomplete="off" checked>
                                                <label class="btn btn-sm btn-outline-success border border-dark fw-bold" for="successoutlined2" style="width: 40px">SI</label>
                                                <input type="radio" class="btn-check" runat="server" name="options-outlined2" id="dangeroutlined2" autocomplete="off">
                                                <label class="btn btn-sm btn-outline-danger  border border-dark fw-bold" for="dangeroutlined2" style="width: 40px">NO</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <h4>Ubicación:</h4>
                                        <asp:HiddenField ID="HiddenMaterial" runat="server" />
                                        <asp:GridView ID="GridUbicacion" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark shadow" OnRowCommand="GridViewCommandEventHandler" AutoGenerateColumns="false" ShowHeader="false">
                                            <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                            <RowStyle BackColor="White" />
                                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <div class="input-group">
                                                            <label>&nbsp</label>
                                                            <asp:Button CssClass="btn btn-sm btn-dark" ID="Label1" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Estanteria") %>' CommandName="Redirect" CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                                            <label style="padding-top: 7px; font-weight: bold" cssclass="btn btn-sm btn-dark me-1" backcolor="Black" forecolor="White" font-bold="true">&nbsp.&nbsp</label>
                                                            <asp:Button CssClass="btn btn-sm btn-dark" ID="Label2" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Modulo") %>' CommandName="Redirect" CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                                            <label style="padding-top: 7px; font-weight: bold" cssclass="btn btn-sm btn-dark me-1" backcolor="Black" forecolor="White" font-bold="true">&nbsp.&nbsp</label>
                                                            <asp:Button CssClass="btn btn-sm btn-dark" ID="Label3" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Balda") %>' CommandName="Redirect" CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                                            <label>&nbsp</label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <h4>Reciclado:</h4>
                                    <div class="input-group input-group-sm" runat="server" id="RecicladoPill">
                                        <button class="btn btn-outline-secondary border border-dark border-end-0" type="button" id="BtnEliminaMat" onclick="GRecEliminaMat()" style="width: 10%; background-color: ghostwhite" runat="server">X</button>
                                        <asp:Label ID="lblMatReciclado" runat="server" CssClass="border border-dark border-start-0 rounded-end" Style="width: 90%; background-color: ghostwhite; vertical-align: middle; font-weight: 500"></asp:Label>
                                    </div>
                                    <div class="input-group input-group shadow mb-3" style="width: 100%" runat="server" id="RecicladoInput">
                                        <input class="form-control border border-dark " list="DatalistNUMMaterialBusc" id="inputMatReciclado" runat="server" placeholder="Escribe la referencia">
                                        <button class="btn btn-outline-secondary border border-dark" type="button" id="BtnAñadeMat" onclick="GRecAñadeMat()">Añadir</button>
                                    </div>
                                </div>
                                <div class="row">
                                    <h4>Documentos:</h4>
                                    <div class="col-lg-6">
                                        <div class="card border-dark mb-3" style="max-width: 18rem; background-color: whitesmoke">
                                            <div>
                                                <label class="ms-2 mt-1" style="font-weight: bold">Ficha técnica</label>
                                                <button runat="server" id="BtnEliminaFTEC" onserverclick="EliminarDocumento" class="btn btn-danger btn-sm mt-1 me-1 border border-dark float-end"><i class="bi bi-trash"></i></button>
                                            </div>
                                            <asp:HyperLink ID="hyperlink1" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="../GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded border border-0 img-thumbnail img-fluid shadow" />
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="card border-dark mb-3" style="max-width: 18rem; background-color: whitesmoke">
                                            <div>
                                                <label class="ms-2 mt-1" style="font-weight: bold">Ficha seguridad</label>
                                                <button runat="server" id="BtnEliminaFSEC" onserverclick="EliminarDocumento" class="btn btn-danger btn-sm mt-1 me-1 border border-dark float-end"><i class="bi bi-trash"></i></button>
                                            </div>
                                            <asp:HyperLink ID="hyperlink2" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="../GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded border border-0 img-thumbnail img-fluid shadow" />
                                        </div>
                                    </div>


                                </div>
                                <div class="row ms-1 me-1">
                                    <h6 class="mt-2">Actualizar documento</h6>
                                    <div class="col-lg-12">
                                        <asp:DropDownList CssClass="form-select fw-bold border border-dark" runat="server" ID="DropUploadSelect">
                                            <asp:ListItem Value="0">-</asp:ListItem>
                                            <asp:ListItem Value="1">Ficha técnica</asp:ListItem>
                                            <asp:ListItem Value="2">Ficha de seguridad</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="input-group border border-dark rounded mb-3">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"></asp:FileUpload>
                                            <button class="btn btn-outline-secondary  fw-bold border border-0 " type="button" id="SubirDocumentos" runat="server" onserverclick="Insertar_documento"><i class="bi bi-box-arrow-up"></i></button>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <button type="button" runat="server" id="BtnGuardarCambios" onserverclick="ActualizarMaterial" class="btn btn-success border border-dark" style="border-top-left-radius: 0px; border-top-right-radius: 0px">Guardar cambios</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </div>
</asp:Content>
















