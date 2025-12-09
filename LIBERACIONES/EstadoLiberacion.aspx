<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EstadoLiberacion.aspx.cs" Inherits="ThermoWeb.LIBERACIONES.EstadoLiberacion" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Panel de liberaciones de serie</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Liberaciones de serie             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../LIBERACIONES/HistoricoLiberacion.aspx">Histórico de liberaciones</a></li>
                <li><a class="dropdown-item" href="../KPI/KPILiberaciones.aspx">Resultados de liberaciones</a></li>
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
    </style>
    <script type="text/javascript">
        function ActualizarGRID() {
            document.getElementById("BTN_RELLENARGRID").click();
        }
        function ClosePopup1() {

        }
        function ShowPopupFirma() {
            document.getElementById("AUXMODALACCIONFIRMA").click();
        }
        function ClosePopupFirma() {
            document.getElementById("AUXCIERRAMODALFIRMA").click();
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
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="row"></div>
        <div class="container-fluid mt-2">
            <div class="col-lg-12">
                <ul class="nav nav-pills justify-content-end" id="pills-tab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="BTN_ESTADO_PRODUCTOS" runat="server" data-bs-toggle="pill" data-bs-target="#pills_estados" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-file-earmark-binary me-2"></i>Desviaciones</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="BTN_ULTIMAS_REVISIONES" runat="server" data-bs-toggle="pill" data-bs-target="#pills_historico" type="button" role="tab" aria-controls="pills-home" aria-selected="true"><i class="bi bi-book me-2"></i>Estado de liberaciones</button>
                    </li>
                </ul>
                <div class="tab-content" id="pills-tabContent">
                    <div class="tab-pane fade show active" id="pills_historico" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <asp:GridView ID="dgv_Liberaciones" runat="server" AllowSorting="True"
                            Width="100%" CssClass="table table-responsive shadow p-3 border border-secondary rounded" AutoGenerateColumns="false"
                            OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridView0_DataBound"
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
                                        <asp:LinkButton runat="server" ID="btnVer" CommandName="Redirect" CommandArgument='<%#Eval("Orden")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="tab-pane fade" id="pills_estados" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <asp:GridView ID="GridDesviaciones" runat="server" AllowSorting="True"
                            Width="100%" CssClass="table table-responsive shadow p-3 border border-secondary rounded" AutoGenerateColumns="false"
                            OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridViewDESV_DataBound"
                            EmptyDataText="There are no data records to display.">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="#e6e6e6" />
                            <RowStyle BackColor="white" CssClass="shadow" />
                            <EditRowStyle BackColor="#ffcc66" />
                            <FooterStyle BackColor="Silver" BorderColor="#808080" />
                            <Columns>
                                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-CssClass="shadow border border-secondary">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaquina" runat="server" Font-Size="X-Large" Text='<%#Eval("Maquina") %>' />
                                        <asp:Label ID="lblEstadoActual" Visible="false" runat="server" Text='<%#Eval("EstadoMaquina") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Responsables" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPersonalEncargado" runat="server" Font-Size="Smaller" Text='<%#Eval("Encargado") %>' />
                                        <br />
                                        <asp:Label ID="lblPersonalCalidad" runat="server" Font-Size="Smaller" Text='<%#Eval("Calidad") %>' />
                                        <br />
                                        <asp:Label ID="lblPersonalCambiador" runat="server" Font-Size="Smaller" Text='<%#Eval("Cambiador") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nv. Op." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPersonalOperario" Font-Bold="true" runat="server" Text='<%#Eval("Operario1Nivel") %>' />
                                        <asp:Label ID="lblOperarioNivel" runat="server" Font-Size="Smaller" Text='<%#Eval("Operario1Notas") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Materiales y cambio de molde" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>

                                        <asp:Label ID="Desv1Prod" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q1E") %>' />
                                        <asp:Label ID="Desv1Cal" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q1C") %>' />
                                        <asp:Label ID="Q1PROD" runat="server" Text='<%#"<strong>1.Camb: </strong>" + Eval("Q1ENC") %>' />
                                        <asp:Label ID="Desv2Prod" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q2E") %>' />
                                        <asp:Label ID="Desv2Cal" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q2C") %>' />
                                        <asp:Label ID="Q2PROD" runat="server" Text='<%#"<br /><strong>2.Camb: </strong>" + Eval("Q2ENC") %>' />
                                        <asp:Label ID="Desv3Prod" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q3E") %>' />
                                        <asp:Label ID="Desv3Cal" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q3C") %>' />
                                        <asp:Label ID="Q3PROD" runat="server" Text='<%#"<br /><strong>3.Camb: </strong>" + Eval("Q3ENC") %>' />
                                        <asp:Label ID="ResultadoLOT" Visible="false" runat="server" Text='<%#Eval("ResultadoLOTES") %>' />
                                        <asp:Label ID="ResultadoLOTTEXT" Visible="false" runat="server" Text='<%#"<br /><strong>Mat: </strong>" + Eval("ResultadosLOTESTEXT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lotes" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" Visible="false"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Máquina y parámetros" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("AccionLiberado") %>' />
                                        <asp:Label ID="Desv4Prod" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q4E") %>' />
                                        <asp:Label ID="Q4PROD" runat="server" Text='<%#"<strong>4.Prod: </strong>" + Eval("Q4ENC") %>' />
                                        <asp:Label ID="Desv5Prod" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q5E") %>' />
                                        <asp:Label ID="Q5PROD" runat="server" Text='<%#"<br /><strong>5.Prod: </strong>" + Eval("Q5ENC") %>' />
                                        <asp:Label ID="Desv6Prod" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q6E") %>' />
                                        <asp:Label ID="Desv6Cal" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q6C") %>' />
                                        <asp:Label ID="Q6PROD" runat="server" Text='<%#"<br /><strong>6.Prod: </strong>" + Eval("Q6ENC") %>' />
                                        <asp:Label ID="Q6CAL" runat="server" Text='<%#"<br /><strong>6.Cal: </strong>" + Eval("Q6CAL") %>' />
                                        <asp:Label ID="ResultadoPARAM" Visible="false" runat="server" Text='<%#Eval("ResultadoPARAM") %>' />
                                        <asp:Label ID="ResultadoPARAMTEXT" Visible="false" runat="server" Text='<%#"<br /><strong>Prod: </strong>" + Eval("ResultadoPARAMTEXT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proceso y atributos" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="Desv7Prod" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q7E") %>' />
                                        <asp:Label ID="Desv7Cal" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q7C") %>' />
                                        <asp:Label ID="Q7PROD" runat="server" Text='<%#"<strong>7.Prod: </strong>" + Eval("Q7ENC") %>' />
                                        <asp:Label ID="Q7CAL" runat="server" Text='<%#"<br /><strong>7.Cal: </strong>" + Eval("Q7CAL") %>' />
                                        <asp:Label ID="Desv8Prod" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q8E") %>' />
                                        <asp:Label ID="Desv8Cal" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q8C") %>' />
                                        <asp:Label ID="Q8PROD" runat="server" Text='<%#"<br /><strong>8.Prod: </strong>" + Eval("Q8ENC") %>' />
                                        <asp:Label ID="Q8CAL" runat="server" Text='<%#"<br /><strong>8.Cal: </strong>" + Eval("Q8CAL") %>' />
                                        <asp:Label ID="Desv9Prod" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q9E") %>' />
                                        <asp:Label ID="Desv9Cal" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q9C") %>' />
                                        <asp:Label ID="Q9PROD" runat="server" Text='<%#"<br /><strong>9.Prod: </strong>" + Eval("Q9ENC") %>' />
                                        <asp:Label ID="Q9CAL" runat="server" Text='<%#"<br /><strong>9.Cal: </strong>" + Eval("Q9CAL") %>' />
                                        <asp:Label ID="Desv10Cal" Font-Bold="true" runat="server" Visible="false" Text='<%#Eval("Q10C") %>' />
                                        <asp:Label ID="Q10CAL" runat="server" Text='<%#"<br /><strong>10.Cal: </strong>" + Eval("Q10CAL") %>' />
                                        <asp:Label ID="LiberadoPROD" Visible="false" Font-Bold="true" runat="server" Text='<%#Eval("ProduccionLiberado") %>' />
                                        <asp:Label ID="DesvNCPROD" Visible="false" Font-Bold="true" runat="server" Text='<%#Eval("ENCNoconformidad") %>' />
                                        <asp:Label ID="DesvNCPRODText" Visible="false" runat="server" Text='<%#"<br /><strong>Prod.: </strong>" + Eval("ENCNoconformidadTEXT") %>' />
                                        <asp:Label ID="DesvGP12PROD" Visible="false" Font-Bold="true" runat="server" Text='<%#Eval("ENCDefectos") %>' />
                                        <asp:Label ID="DesvGP12PRODText" Visible="false" runat="server" Text='<%#"<br /><strong>Prod: </strong>" + Eval("ENCDefectosTEXT") %>' />
                                        <asp:Label ID="LiberadoCal" Visible="false" Font-Bold="true" runat="server" Text='<%#Eval("CalidadLiberado") %>' />
                                        <asp:Label ID="DesvNCCAL" Visible="false" Font-Bold="true" runat="server" Text='<%#Eval("CALNoconformidad") %>' />
                                        <asp:Label ID="DesvNCCALText" Visible="false" runat="server" Text='<%#"<br /><strong>Cal.: </strong>" + Eval("CALNoconformidadTEXT") %>' />
                                        <asp:Label ID="DesvGP12CAL" Visible="false" Font-Bold="true" runat="server" Text='<%#Eval("CALDefectos") %>' />
                                        <asp:Label ID="DesvGP12CALText" Visible="false" runat="server" Text='<%#"<br /><strong>Cal.: </strong>" + Eval("CALDefectosTEXT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parámetros" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" Visible="false"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Notas" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="QXCAMB" runat="server" Text='<%#"<strong>Camb: </strong>" + Eval("QXFeedbackCambiador") %>' />
                                        <br />
                                        <asp:Label ID="QXPROD" runat="server" Text='<%#"<strong>Prod: </strong>" + Eval("QXFeedbackProduccion") %>' />
                                        <br />
                                        <asp:Label ID="QXCAL" runat="server" Text='<%#"<strong>Cal: </strong>" + Eval("QXFeedbackCalidad") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnVer" CommandName="Redirect" CommandArgument='<%#Eval("Orden")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>


    </div>
</asp:Content>
















