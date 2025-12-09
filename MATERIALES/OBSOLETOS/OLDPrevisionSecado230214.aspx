<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="OLDPrevisionSecado230214.aspx.cs" Inherits="ThermoWeb.MATERIALES.PrevisionSecadoV3" MasterPageFile="~/SMARTH.Master"
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
    </style>
    <script type="text/javascript">

        function ShowPopupImprimirEtiquetas() {
            document.getElementById("AUXMODALIMPRIMIRETIQUETA").click();
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
                data: "{MATERIAL: '" + document.getElementById("AUX_MATERIAL").value + "', DESCRIPCION: '" + document.getElementById("AUX_DESCRIPCION").value + "', INPUTOPERARIO: '" + document.getElementById("InputOperario").value + "', LOTE: '" + document.getElementById("InputLote").value + "', TIPO: '" + document.getElementById("DropTipoPrint").value + "'}",
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

    </script>

    <%--Calendario--%>
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
        function myFunction() {
            __doPostBack();
        }
    </script>
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="row">

            <div class="col-lg-9">
                <asp:Label ID="label" runat="server" Font-Size="Large" CssClass="ms-3 mt-2">Última actualización:&nbsp</asp:Label><asp:Label ID="FECHAACT" runat="server" Font-Bold="true" Font-Size="Large" Font-Italic="true"></asp:Label>
                <button id="AUXMODALIMPRIMIRETIQUETA" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalImprimirEtiqueta" style="font-size: larger"></button>
                <button type="button" id="btnPopEstructura" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopEstructura"></button>

            </div>
            <div class="col-lg-3" style="text-align: right">
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
                                <asp:TemplateField HeaderText="Iniciar secado" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoActual" Font-Size="X-Large" runat="server" Text='<%#Eval("INICIARSECADO") %>' />
                                        <asp:Label ID="lblFechacambio" Font-Italic="true" runat="server" Text='<%# "&nbsp("+Eval("FECHA")+")" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="5%" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("ORDEN") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Material" ItemStyle-Width="20%" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>

                                        <asp:Label ID="lblReferencia" runat="server" Font-Size="X-Large" Text='<%#Eval("MATERIAL") + " - "%>' />
                                        <asp:Label ID="Label2" runat="server" Font-Size="Large" Text='<%#Eval("DESCRIPCION")%>' />
                                        <asp:Label ID="lblDescripcionLONG" runat="server" Visible="false" Font-Size="Large" Text='<%#Eval("DESCRIPCIONLONG")%>' />
                                        <asp:LinkButton runat="server" ID="btnEstructura" CommandName="EstructuraMSEQ" CommandArgument='<%#Eval("MAQ") + "¬" + Eval("SEQNR") + "¬" + Eval("ORDEN")%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-outline-dark shadow shadow-sm ms-2" Style="font-weight: bold">
                                         <i class="bi bi-three-dots-vertical"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="A preparar" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoCambio" Font-Bold="true" runat="server" Text='<%#Eval("PREPARAR") %>' /><br />
                                        <asp:Label ID="lblUbicacionNew" runat="server" Text='<%#Eval("UBICACION") %>' />
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
                                        <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("UBICACION") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notas" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoProduccion" Font-Bold="true" runat="server" Text='<%#Eval("NOTAS") %>' />
                                        <asp:Label ID="lblConsumo" Font-Size="Smaller" runat="server" Visible="false" Text='<%#"<br />(Preparar " + Eval("SUMAMATS", "{0:0.00 Kg}") +" para los próximos 3 días)" %>' />
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
                                <asp:TemplateField HeaderText="Material actual" ItemStyle-Width="25%" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>

                                        <asp:Label ID="lblReferencia" runat="server" Font-Size="X-Large" Text='<%#Eval("MATERIAL") + " - "%>' />
                                        <asp:Label ID="Label2" runat="server" Font-Size="Large" Text='<%#Eval("DESCRIPCION")%>' />
                                        <asp:Label ID="lblDescripcionLONG" runat="server" Visible="false" Font-Size="Large" Text='<%#Eval("DESCRIPCIONLONG")%>' />
                                        <asp:LinkButton runat="server" ID="btnEstructura" CommandName="Estructura" CommandArgument='<%#Eval("MAQ") %>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-outline-dark shadow shadow-sm ms-2" Style="font-weight: bold">
                                         <i class="bi bi-three-dots-vertical"></i>
                                        </asp:LinkButton>
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
                                <asp:TemplateField HeaderText="Notas" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoProduccion" Font-Bold="true" runat="server" Text='<%#Eval("NOTAS") %>' />
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
                            <div class="col-lg-9"></div>
                            <div class="col-lg-3" style="text-align: right">
                                <div class="input-group mb-3">
                                    <input class="form-control border border-dark shadow" list="DatalistNUMMaterial" id="NUMMaterial" runat="server" placeholder="Escribe un producto...">
                                    <datalist id="DatalistNUMMaterial" runat="server">
                                    </datalist>
                                    <button id="Button2" runat="server" onserverclick="FiltraMaterial" type="button" class="btn btn-lg btn-primary border border-dark shadow" style="text-align: left">
                                        <i class="bi bi-search"></i>
                                    </button>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="dgv_Materiales" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="100%" CssClass="table table-responsive shadow border border-secondary rounded" AutoGenerateColumns="false"
                                    OnRowCommand="GridViewCommandEventHandler" EmptyDataText="There are no data records to display.">
                                    <HeaderStyle BackColor="#0000ff" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#ccccff" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Material" ItemStyle-Width="10%" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="X-Large">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstadoActual" runat="server" Text='<%#Eval("MATERIAL") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción" ItemStyle-Font-Size="Large" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("LONG_DESCRIPTION") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ubicación" ItemStyle-Font-Size="X-Large" ItemStyle-Width="15%" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("SHORT_DESCRIPTION") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock" ItemStyle-Font-Size="Large" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCantalm" runat="server" Font-Bold="true" Font-Italic="true" Text='<%#Eval("CANTALM", "{0:0 Kg./Uds.}") %>' /><br />
                                                <asp:Label ID="lblPrevision" runat="server" Font-Size="Medium" Font-Italic="true" Text='<%#"Previsión: " + Eval("FECHA", "{0:dd/MM/yyyy}") + " - " + Eval("QUANTITY", "{0:#}") + " uds." %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Notas" ItemStyle-Font-Size="X-Large" ItemStyle-Width="35%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNotasMAT" runat="server" Text='<%#Eval("REMARKS") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="button2" CommandName="ImprimirEtiqueta" CommandArgument='<%#Eval("MATERIAL") + "¬" + Eval("LONG_DESCRIPTION") + "¬TABLAMATERIALES"%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow" Style="font-size: 1rem">
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
                                        <asp:TemplateField ItemStyle-Font-Size="Large" ItemStyle-Width="43%">
                                            </asp:TemplateField>
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
            </div>
        </div>


    </div>
</asp:Content>
















