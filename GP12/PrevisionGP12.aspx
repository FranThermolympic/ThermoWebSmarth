<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PrevisionGP12.aspx.cs" Inherits="ThermoWeb.GP12.PrevisionGP12" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Previsión de cargas en Muro de Calidad</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Planificación de cargas con revisión             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Revisiones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../GP12/GP12.aspx">Iniciar una revisión</a></li>
                <li><a class="dropdown-item" href="../GP12/PrevisionGP12.aspx">Consultar planificación de cargas</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación de referencia</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../GP12/GP12Historico.aspx">Consultar últimas revisiones</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12HistoricoCliente.aspx">Consultar histórico de cliente</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12HistoricoReferencia.aspx">Consultar histórico de producto</a></li>
                <li><a class="dropdown-item" href="../KPI/KPIIndice.aspx">Consultar indicadores</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../KPI/KPIIndice.aspx">Ver cuadros de mando</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Gestiones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../GP12/GP12ReferenciasEstado.aspx">Gestionar estado de referencias</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12RegistroComunicaciones.aspx">Registrar comunicaciones</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../PDCA/PDCA.aspx">Abrir plan de acción</a></li>
                <li><a class="dropdown-item" href="../CALIDAD/Alertas_Calidad.aspx">Abrir no conformidad</a></li>

            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <script type="text/javascript">
        function ShowPopup1() {
            document.getElementById("AUXMODALACCION").click();
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
        $(document).on("click", "[src*=plus]", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("class", "btn btn-sm btn-outline-dark ms-md-2 bi bi-dash-circle");
            $(this).attr("src", "dash");
        });
        $(document).on("click", "[src*=dash]", function () {
            $(this).attr("class", "btn btn-sm btn-outline-dark ms-md-2 bi bi-plus-circle");
            $(this).closest("tr").next().remove();
            $(this).attr("src", "plus");
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.form-check-input').change(function () {
                //$('AuxVerFiltrados').click();
                document.getElementById("AuxVerFiltrados").click();
                //var isChecked = $(this).prop('checked');
                /*
                if (isChecked) {
                    // Llama a la función en el servidor usando AJAX
                    alert(isChecked)
                }
                else {
                    alert(isChecked)
                }
                */
            });
        });
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
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="row" hidden="hidden">

            <div class="col-lg-1 text-right">
                <button id="AuxVerFiltrados" runat="server" onserverclick="CargarSwitch" type="button" class="btn btn-lg btn-info" style="width: 100%; text-align: left">
                    <span class="glyphicon glyphicon-search"></span>Filtrar</button>
            </div>
        </div>

        <div class="container-fluid ">

            <div class="row">
                <div class="col-lg-12">
                    <div class="form-check form-switch">
                        <input class="form-check-input" runat="server" id="SwitchOcultarRevision" type="checkbox" role="switch"  />
                        <h5 class="form-check-label" for="flexSwitchCheckChecked">Ocultar lineas sin revisión</h5>
                    </div>
                </div>



            </div>
            <div class="col-lg-12">
                <ul class="nav nav-pills justify-content-end" id="pills-tab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="pills-profile-tab2" data-bs-toggle="pill" data-bs-target="#pills-profile2" type="button" role="tab" aria-controls="pills-profile2" aria-selected="false"><i class="bi bi-boxes me-2"></i>Stock almacén GP12</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-exclamation-triangle me-2"></i>Retrasadas</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true"><i class="bi bi-truck me-2"></i>Previstas por día</button>
                    </li>
                </ul>
                <div class="tab-content" id="pills-tabContent">
                    <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                        <asp:GridView ID="dgv_PedidosClientes" runat="server" AllowSorting="True" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false" DataKeyNames="FechaEntrega" OnRowDataBound="OnRowDataBound" EmptyDataText="There are no data records to display.">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <button type="button" id="btnDESP" runat="server" class="btn btn-outline-dark btn-sm ms-md-2 bi bi-plus-circle" src="plus" style="font-size: 1em"></button>
                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none; background-color: lightgray">
                                            <asp:GridView ID="dgv_PrevisionGP12" runat="server" AllowSorting="True" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false" EmptyDataText="There are no data records to display.">
                                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                <RowStyle BackColor="White" />
                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Fecha entrega" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecharev" runat="server" Font-Size="Large" Text='<%#Eval("FechaEntrega", "{0:dd/MM/yyyy}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReferencia" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("Referencia") %>' /><br />
                                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="A enviar" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCantidad" Font-Size="Large" Font-Bold="true" runat="server" Text='<%#Eval("ENTREGAR") %>' /><br />
                                                            <asp:Label ID="Label2" Font-Italic="true" Font-Size="Small" runat="server" Text='pz. pedidas.' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Disponibles" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisponible" runat="server" Font-Size="Large" Text='<%#Eval("ALMACEN")%>' /><br />
                                                            <asp:Label ID="Label2" Font-Italic="true" Font-Size="Small" runat="server" Text='pz. disponibles.' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pdte. revisar" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PdteRevisar" runat="server" Font-Size="Large" Text='<%#Eval("PDTEREVISAR")%>' /><br />
                                                            <asp:Label ID="EnGP12" Font-Italic="true" Font-Size="Small" runat="server" Text='<%#"<strong>" + Eval("GP12") + "</strong> retenidas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ubicación" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAlmacén" runat="server" Font-Bold="true">Almacén</asp:Label><br />
                                                            <asp:Label ID="lblPosicion" runat="server" Font-Italic="true">Posición</asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cliente y razón">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCliente" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("planta") %>' /><br />
                                                            <asp:Label ID="lblRazon" runat="server" Text='<%#Eval("RAZONMONT") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Recursos estimados">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNecesarias" runat="server" Text='<%#"<strong>" + Eval("HORASNECESARIAS") + "</strong> horas" %>' /><br />
                                                            <asp:Label ID="lblPZHora" runat="server" Font-Size="Smaller" Font-Italic="true" Text='<%#"a " + Eval("PZHORA") + " pz/hora" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha entrega" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaTotal" runat="server" Font-Size="Large" Text='<%#Eval("FechaEntrega", "{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lineas de entrega" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcionTotal" Font-Size="Large" runat="server" Text='<%#Eval("Productos") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Piezas solicitadas" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCantidadTotal" Font-Size="Large" Font-Bold="true" runat="server" Text='<%#Eval("TotalEntregar") + " pz. pendientes de enviar." %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recursos estimados">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHorasNecesariasTotal" runat="server" Text='<%#Eval("TotalHoras") + " horas de revisión previstas." %>' /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <asp:GridView ID="GridRetrasadas" runat="server" AllowSorting="True" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false" DataKeyNames="FechaEntrega" EmptyDataText="There are no data records to display.">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                                <asp:TemplateField HeaderText="Fecha entrega" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFecharev" runat="server" Font-Size="Large" Text='<%#Eval("FechaEntrega", "{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("Referencia") %>' /><br />
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A enviar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCantidad" Font-Size="Large" Font-Bold="true" runat="server" Text='<%#Eval("ENTREGAR") %>' /><br />
                                        <asp:Label ID="Label2" Font-Italic="true" Font-Size="Small" runat="server" Text='pz. pedidas.' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Existencias" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDisponible" runat="server" Font-Size="Large" Text='<%#Eval("ALMACEN")%>' /><br />
                                        <asp:Label ID="Label1" Font-Italic="true" Font-Size="Small" runat="server" Text='pz. disponibles.' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pdte. revisar" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PdteRevisar" runat="server" Font-Size="Large" Text='<%#Eval("PDTEREVISAR")%>' /><br />
                                                            <asp:Label ID="EnGP12" Font-Italic="true" Font-Size="Small" runat="server" Text='<%#"<strong>" + Eval("GP12") + "</strong> retenidas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ubicación" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlmacén" runat="server" Font-Bold="true">Almacén</asp:Label><br />
                                        <asp:Label ID="lblPosicion" runat="server" Font-Italic="true">Posición</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cliente y razón">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCliente" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("planta") %>' /><br />
                                        <asp:Label ID="lblRazon" runat="server" Text='<%#Eval("RAZONMONT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Recursos estimados">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHorasNecesarias" runat="server" Text='<%#"<strong>" + Eval("HORASNECESARIAS") + "</strong> horas" %>' /><br />
                                        <asp:Label ID="lblPZHora" runat="server" Font-Size="Smaller" Font-Italic="true" Text='<%#"a " + Eval("PZHORA") + " pz/hora" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>
                    <div class="tab-pane fade" id="pills-profile2" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <div class="row">
                            <div class="col-lg-5 mt-2">
                            </div>
                            <div class="col-lg-3 text-center">
                                <label class="ms-1" style="font-size: larger; font-weight: bold">Stock pendiente acumulado:</label><br />
                                <asp:Label ID="lblHoras" Font-Size="X-Large" runat="server">0</asp:Label>

                            </div>
                            <div class="col-lg-4 mt-2">
                            </div>

                        </div>
                        <asp:GridView ID="dgv_Almacenes" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 " BorderColor="black" Width="100%">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>

                                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="50%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Font-Size="X-Large" Font-Bold="true" Text='<%#Eval("Referencia") %>' />
                                        <asp:Label ID="lblDescripcion" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("Descripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuenas" runat="server" Font-Size="X-Large" Text='<%#Eval("Piezas") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recursos estimados" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHorasNecesarias" runat="server" Text='<%#"<strong>" + Eval("HorasRev", "{0:n2}") + "</strong> horas" %>' /><br />
                                        <asp:Label ID="lblPZHora" runat="server" Font-Size="Smaller" Font-Italic="true" Text='<%#"a " + Eval("PZHoras") + " pz/hora" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <%--MODALES DE EDICION --%>
</asp:Content>
