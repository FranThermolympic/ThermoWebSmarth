<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ListadoPrioridades.aspx.cs" Inherits="ThermoWeb.PLANIFICACION.ListadoPrioridades" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Listado de prioridades de producción</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />

</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Prioridades             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown me-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Producción
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../ListaFichasParametros.aspx">Fichas de parámetros</a></li>
                <li><a class="dropdown-item" href="../LIBERACIONES/EstadoLiberacion.aspx">Liberaciones de serie</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown  me-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Calidad
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../GP12/GP12Historico.aspx">Histórico GP12</a></li>
                <li><a class="dropdown-item" href="../CALIDAD/ListaAlertasCalidad.aspx">Listado No conformidades</a></li>

            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Mantenimiento
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMaquina.aspx">Partes de máquina</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMoldes.aspx">Partes de molde</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <script type="text/javascript">
        function ShowPopup() {
            document.getElementById("AUXMODALACCION").click();
        }
        function myFunction() {
            document.getElementById("RecargarTodo").click();
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
        <div class="row" runat="server">
            <div class="col-lg-12">

                <div class="row mb-2">
                    <div class="col-lg-10">
                        <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaOrden" style="font-size: larger"></button>

                    </div>
                    <div class="col-lg-2 ">
                        <h6 class="mt-2">Órdenes a mostrar:</h6>
                        <div class="input-group input-group-sm bg-white ">
                            <asp:DropDownList ID="DropNumSEQNR" runat="server" CssClass="form-select" Font-Size="Large">
                                <asp:ListItem Text="En producción" Value="0"></asp:ListItem>
                                <asp:ListItem Text="En producción +1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="En producción +2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="En producción +3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="En producción +4" Value="4" Selected></asp:ListItem>
                                <asp:ListItem Text="Todas" Value="100"></asp:ListItem>
                            </asp:DropDownList>
                            <button id="RecargarTodo" runat="server" onserverclick="Rellenar_grid" type="button" class="btn btn-outline-secondary me-3 ">
                                <i class="bi bi-arrow-clockwise"></i>
                            </button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <ul class="nav nav-tabs" id="myTab" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="home-tab" data-bs-toggle="tab" data-bs-target="#home" type="button" role="tab" aria-controls="home" style="font-weight:bold" aria-selected="true">Prioridades</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="profile-tab" data-bs-toggle="tab" data-bs-target="#profile" type="button" role="tab" aria-controls="profile"  style="font-weight:bold"  aria-selected="false">No conformidades</button>
            </li>

        </ul>
        <div class="tab-content" id="myTabContent">
            <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                <div class="table-responsive">
                    <asp:GridView ID="GridPrioridades" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                        Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                        OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBound" EmptyDataText="There are no data records to display.">
                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                        <RowStyle BackColor="White" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                        <Columns>

                            <asp:TemplateField ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-BackColor="#eeeeee">
                                <HeaderTemplate>
                                    <button id="OrdenarMaq" runat="server" onserverclick="Acciones_Cabecera" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: center; font-weight: bold">
                                        <i class="bi bi-arrow-down-up me-2"></i>Máquina</button>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <asp:LinkButton ID="btnEditarLinea" runat="server" type="button" class="btn btn-primary float-start mt-1 " Style="text-align: center; font-weight: bold" CommandName="EditarOrden" CommandArgument=' <%#Eval("Orden") %>'>
                                <i class="bi bi-pencil"></i>
                                    </asp:LinkButton>

                                    <asp:Label ID="lblMaquina" runat="server" Font-Bold="true" Font-Size="X-Large" CssClass="mt-1" Text='<%#Eval("Maquina") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-BackColor="#eeeeee">
                                <HeaderTemplate>
                                    <button id="OrdenarOrden" runat="server" onserverclick="Acciones_Cabecera" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold">
                                        <i class="bi bi-arrow-down-up me-2"></i>Orden</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrden" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Orden") %>' /><br />
                                    </i><asp:Label ID="lblRestante" Font-Size="Smaller" runat="server" Text='<%#"("+Eval("Tiempo")+")"%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-BackColor="#eeeeee">
                                <HeaderTemplate>
                                    <button id="OrdenarPrior" runat="server" onserverclick="Acciones_Cabecera" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold">
                                        <i class="bi bi-arrow-down-up me-2"></i>Prior.</button>

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPrioridad" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("PRIORIDADDEC") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="16%" ItemStyle-VerticalAlign="Middle">
                                <HeaderTemplate>
                                    <button id="OrdenarProducto" runat="server" onserverclick="Acciones_Cabecera" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold">
                                        <i class="bi bi-arrow-down-up me-2"></i>Producto</button>

                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblplay" runat="server" class="bi bi-file-play-fill me-1" visible="false" ForeColor="forestgreen" /> 
                                    <asp:Label ID="lblpause" runat="server" class="bi bi-clock-fill me-1" visible="false" ForeColor="darkorange" /> 
                                    <asp:Label ID="LBLSEQNR" runat="server" visible="false" Text='<%#Eval("SEQNR") %>' />                                
                                    <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Producto") %>' /><asp:Button ID="btnGP12" runat="server" Visible="false" type="button" class="btn btn-sm btn-danger ms-1" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("GP12") %>' CommandName="RedirectGP12" CommandArgument=' <%#Eval("Producto") %>' />
                                    <br />
                                    <asp:Label ID="lblRefDesc" runat="server" Font-Size="Smaller" Text='<%#Eval("ProdDescript") %>' Font-Italic="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acción BMS" ItemStyle-Width="17%">
                                <HeaderTemplate>
                                    <button type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold"><i class="bi bi-info-circle me-2"></i>Avisos</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAccionOrden" Visible="false" runat="server" Font-Italic="true" Font-Bold="true" Font-Size="Smaller" Text='<%#Eval("REMARKORDEN")  + "<br />"%>' />
                                    <asp:TextBox ID="txtAccionProducto" Visible="false" runat="server" Font-Size="Smaller" TextMode="MultiLine" Rows="2" Enabled="false" Width="100%" BackColor="Transparent" ForeColor="Black" BorderColor="Transparent" Text='<%#Eval("REMARKPRODUCTO")  %>' />
                                    <asp:TextBox ID="txtAccionReceta" Visible="false" runat="server" Font-Size="Smaller" TextMode="MultiLine" Rows="1" Enabled="false" Width="100%" BackColor="Transparent" ForeColor="Black" BorderColor="Transparent" Text='<%#"Notas: " + Eval("REMARKRECETA")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="16%">
                                <HeaderTemplate>
                                    <button type="button" class="btn btn-sm btn-primary" id="btnParteMaqHeader" runat="server" onserverclick="Acciones_Cabecera" style="width: 100%; text-align: left; font-weight: bold"><i class="bi bi-wrench me-2"></i>Partes abiertos (máquina)</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnParteMaq" Visible="false" runat="server" type="button" class="btn btn-sm btn-outline-primary" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("PARTEMAQ") %>' CommandName="RedirectMAQ" CommandArgument=' <%#Eval("PARTEMAQ") %>' />
                                    <asp:Label ID="lblMAQAveria" Visible="false" Font-Size="Smaller" runat="server" Text='<%#Eval("MAQAVERIA") + "<br />" %> ' />
                                    <asp:Label ID="lblMAQRepara" Visible="false" runat="server" Font-Italic="true" CssClass="ms-2" Font-Size="Smaller" Text='<%#"<strong>Rep.: </strong>" +Eval("MAQREPARACION") %>' />
                                    <asp:Button ID="btnParteROB" Visible="false" runat="server" type="button" class="btn btn-sm btn-outline-primary" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("PARTEROB") %>' CommandName="RedirectMAQ" CommandArgument=' <%#Eval("PARTEROB") %>' />
                                    <asp:Label ID="lblROBAveria" Visible="false" Font-Size="Smaller" runat="server" Text='<%#Eval("ROBAVERIA") + "<br />" %>' />
                                    <asp:Label ID="lblROBRepara" Visible="false" runat="server" Font-Italic="true" CssClass="ms-2 ShortDesc" Font-Size="Smaller" Text='<%#"<strong>Rep.: </strong>" + Eval("ROBREPARACION") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="16%">
                                <HeaderTemplate>
                                    <button type="button" id="btnParteMolHeader" runat="server" onserverclick="Acciones_Cabecera" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold"><i class="bi bi-wrench me-2"></i>Partes abiertos (molde)</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnParteMOL" Visible="false" runat="server" type="button" class="btn btn-sm btn-outline-primary" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("PARTEMOL") %>' CommandName="RedirectMOL" CommandArgument=' <%#Eval("PARTEMOL") %>' />
                                    <asp:Label ID="lblMOLAveria" Visible="false" Font-Size="Smaller" runat="server" Text='<%#Eval("MOLAVERIA") + "<br />" %>' />
                                    <asp:Label ID="lblMOLRepara" Visible="false" runat="server" Font-Italic="true" CssClass="ms-2" Font-Size="Smaller" Text='<%#"<strong>Rep.: </strong>" + Eval("MOLREPARACION") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="16%">
                                <HeaderTemplate>
                                    <button type="button" class="btn btn-sm btn-primary" id="btnNoConformidadesHeader" runat="server" onserverclick="Acciones_Cabecera" style="width: 100%; text-align: left; font-weight: bold"><i class="bi bi-exclamation-square me-2"></i>No conformidades</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblD3Estado" Visible="false" runat="server" Font-Italic="true" Font-Bold="true" Font-Size="Smaller" Text='<%#Eval("D3ESTADO") + " <br />" %>' />
                                    <asp:Label ID="lblD6Estado" Visible="false" runat="server" Font-Italic="true" Font-Bold="true" Font-Size="Smaller" Text='<%#Eval("D6ESTADO") + " <br />"  %>' />
                                    <asp:Label ID="lblD8Estado" Visible="false" runat="server" Font-Italic="true" Font-Bold="true" Font-Size="Smaller" Text='<%#Eval("D8ESTADO") + " <br />" %>' />
                                    <asp:Button ID="btnIDNOConformidad" Visible="false" runat="server" type="button" class="btn btn-sm btn-outline-danger" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("IdNoConformidad") %>' CommandName="RedirectNC" CommandArgument=' <%#Eval("IdNoConformidad") %>' />
                                    <asp:Label ID="lblNoConformidad" runat="server" Text='<%#Eval("DescripcionProblema") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                <div class="table-responsive">
                    <asp:GridView ID="GridNoConformidades" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                        Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                        OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBound" EmptyDataText="There are no data records to display.">
                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                        <RowStyle BackColor="White" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                        <Columns>

                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-BackColor="#eeeeee">
                                <HeaderTemplate>
                                    <button id="OrdenarMaq" runat="server" onserverclick="Acciones_Cabecera" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: center; font-weight: bold">
                                        <i class="bi bi-arrow-down-up me-2"></i>Máquina</button>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <asp:LinkButton ID="btnEditarLinea" runat="server" type="button" class="btn btn-primary float-start mt-1 " Style="text-align: center; font-weight: bold" CommandName="EditarOrden" CommandArgument=' <%#Eval("Orden") %>'>
                                <i class="bi bi-pencil"></i>
                                    </asp:LinkButton>

                                    <asp:Label ID="lblMaquina" runat="server" Font-Bold="true" Font-Size="X-Large" CssClass="mt-1" Text='<%#Eval("Maquina") %>' />
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-BackColor="#eeeeee">
                                <HeaderTemplate>
                                    <button id="OrdenarOrden" runat="server" onserverclick="Acciones_Cabecera" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold">
                                        <i class="bi bi-arrow-down-up me-2"></i>Orden</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrden" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Orden") %>' /><br />
                                    <asp:Label ID="lblRestante" Font-Size="Smaller" runat="server" Text='<%#"("+Eval("Tiempo")+")"%>' />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-BackColor="#eeeeee">
                                <HeaderTemplate>
                                    <button id="OrdenarPrior" runat="server" onserverclick="Acciones_Cabecera" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold">
                                        <i class="bi bi-arrow-down-up me-2"></i>Prior.</button>

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPrioridad" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("PRIORIDADDEC") %>' />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="25%" ItemStyle-VerticalAlign="Middle">
                                <HeaderTemplate>
                                    <button id="OrdenarProducto" runat="server" onserverclick="Acciones_Cabecera" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold">
                                        <i class="bi bi-arrow-down-up me-2"></i>Producto</button>

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblplay" runat="server" class="bi bi-file-play-fill me-1" visible="false" ForeColor="forestgreen" /> 
                                    <asp:Label ID="lblpause" runat="server" class="bi bi-clock-fill me-1" visible="false" ForeColor="darkorange" /> 
                                    <asp:Label ID="LBLSEQNR" runat="server" visible="false" Text='<%#Eval("SEQNR") %>' /> 
                                    <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Producto") %>' /><asp:Button ID="btnGP12" runat="server" Visible="false" type="button" class="btn btn-sm btn-danger ms-1" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("GP12") %>' CommandName="RedirectGP12" CommandArgument=' <%#Eval("Producto") %>' />
                                    <br />
                                    <asp:Label ID="lblRefDesc" runat="server" Font-Size="Smaller" Text='<%#Eval("ProdDescript") %>' Font-Italic="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acción BMS" ItemStyle-Width="45%" Visible="false">
                                <HeaderTemplate>
                                    <button type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold"><i class="bi bi-info-circle me-2"></i>Avisos</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAccionOrden" Visible="false" runat="server" Font-Italic="true" Font-Bold="true" Font-Size="Smaller" Text='<%#Eval("REMARKORDEN")  + "<br />"%>' />
                                    <asp:TextBox ID="txtAccionProducto" Visible="false" runat="server" Font-Size="Smaller" TextMode="MultiLine" Rows="2" Enabled="false" Width="100%" BackColor="Transparent" ForeColor="Black" BorderColor="Transparent" Text='<%#Eval("REMARKPRODUCTO")  %>' />
                                    <asp:TextBox ID="txtAccionReceta" Visible="false" runat="server" Font-Size="Smaller" TextMode="MultiLine" Rows="1" Enabled="false" Width="100%" BackColor="Transparent" ForeColor="Black" BorderColor="Transparent" Text='<%#"Notas: " + Eval("REMARKRECETA")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <button type="button" class="btn btn-sm btn-primary" id="btnNoConformidadesHeader" runat="server" onserverclick="Acciones_Cabecera" style="width: 100%; text-align: left; font-weight: bold"><i class="bi bi-exclamation-square me-2"></i>No conformidades</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblD3Estado" Visible="false" runat="server" Font-Italic="true" Font-Bold="true" Font-Size="Smaller" Text='<%#Eval("D3ESTADO") + " <br />" %>' />
                                    <asp:Label ID="lblD6Estado" Visible="false" runat="server" Font-Italic="true" Font-Bold="true" Font-Size="Smaller" Text='<%#Eval("D6ESTADO") + " <br />"  %>' />
                                    <asp:Label ID="lblD8Estado" Visible="false" runat="server" Font-Italic="true" Font-Bold="true" Font-Size="Smaller" Text='<%#Eval("D8ESTADO") + " <br />" %>' />
                                    <asp:Button ID="btnIDNOConformidad" Visible="false" runat="server" type="button" class="btn btn-sm btn-outline-danger" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("IdNoConformidad") %>' CommandName="RedirectNC" CommandArgument=' <%#Eval("IdNoConformidad") %>' />
                                    <asp:Label ID="lblNoConformidad" runat="server" Text='<%#Eval("DescripcionProblema") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="200px" Visible="false">
                                <HeaderTemplate>
                                    <button type="button" class="btn btn-sm btn-primary" id="btnParteMaqHeader" runat="server" onserverclick="Acciones_Cabecera" style="width: 100%; text-align: left; font-weight: bold"><i class="bi bi-wrench me-2"></i>Partes abiertos (máquina)</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnParteMaq" Visible="false" runat="server" type="button" class="btn btn-sm btn-outline-primary" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("PARTEMAQ") %>' CommandName="RedirectMAQ" CommandArgument=' <%#Eval("PARTEMAQ") %>' />
                                    <asp:Label ID="lblMAQAveria" Visible="false" Font-Size="Smaller" runat="server" Text='<%#Eval("MAQAVERIA") + "<br />" %> ' />
                                    <asp:Label ID="lblMAQRepara" Visible="false" runat="server" Font-Italic="true" CssClass="ms-2" Font-Size="Smaller" Text='<%#"<strong>Rep.: </strong>" +Eval("MAQREPARACION") %>' />
                                    <asp:Button ID="btnParteROB" Visible="false" runat="server" type="button" class="btn btn-sm btn-outline-primary" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("PARTEROB") %>' CommandName="RedirectMAQ" CommandArgument=' <%#Eval("PARTEROB") %>' />
                                    <asp:Label ID="lblROBAveria" Visible="false" Font-Size="Smaller" runat="server" Text='<%#Eval("ROBAVERIA") + "<br />" %>' />
                                    <asp:Label ID="lblROBRepara" Visible="false" runat="server" Font-Italic="true" CssClass="ms-2 ShortDesc" Font-Size="Smaller" Text='<%#"<strong>Rep.: </strong>" + Eval("ROBREPARACION") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="200px" Visible="false">
                                <HeaderTemplate>
                                    <button type="button" id="btnParteMolHeader" runat="server" onserverclick="Acciones_Cabecera" class="btn btn-sm btn-primary" style="width: 100%; text-align: left; font-weight: bold"><i class="bi bi-wrench me-2"></i>Partes abiertos (molde)</button>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnParteMOL" Visible="false" runat="server" type="button" class="btn btn-sm btn-outline-primary" Style="text-align: left; font-weight: bold; font-size: x-small" Text=' <%#Eval("PARTEMOL") %>' CommandName="RedirectMOL" CommandArgument=' <%#Eval("PARTEMOL") %>' />
                                    <asp:Label ID="lblMOLAveria" Visible="false" Font-Size="Smaller" runat="server" Text='<%#Eval("MOLAVERIA") + "<br />" %>' />
                                    <asp:Label ID="lblMOLRepara" Visible="false" runat="server" Font-Italic="true" CssClass="ms-2" Font-Size="Smaller" Text='<%#"<strong>Rep.: </strong>" + Eval("MOLREPARACION") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>





        <!-- Modal -->
        <div class="modal fade" id="ModalEditaOrden" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header bg-primary">

                        <label id="ModalOrden" runat="server" class="me-4" style="font-size: x-large; color: white">Orden</label>
                        <label id="ModalProducto" runat="server" class="me-2" style="font-size: x-large; color: white">Producto</label>
                        <label id="ModalDescripcion" runat="server" class="me-2" style="font-size: large; color: white">Referencia</label>
                        <label id="ModalMolde" runat="server" class="me-2 invisible" style="font-size: large; color: white">Referencia</label>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                            <div class="container-fluid">
                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                    <div class="col-sm-12">
                                        <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>Resumen</h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4">
                                        <h6 class="form-label ms-4">Máquina:</h6>
                                        <label id="ModalMaquina" runat="server" class="form-label ms-5" style="text-align: center"></label>
                                    </div>
                                    <div class="col-lg-4">
                                        <h6 class="form-label ms-4">Prioridad:</h6>
                                        <label id="ModalPrioridad" runat="server" class="form-label ms-5" style="text-align: center"></label>
                                    </div>
                                    <div class="col-lg-4">
                                        <h6 class="form-label ms-4">Tiempo restante:</h6>
                                        <label id="ModalTiempRestante" runat="server" class="form-label ms-5" style="text-align: center"></label>
                                    </div>
                                </div>
                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                    <div class="col-sm-12">
                                        <h5 class="mt-1"><i class="bi bi-card-list me-2"></i>Notas</h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4">
                                        <h6 class="form-label ms-4">Notas de la orden:</h6>
                                        <asp:TextBox ID="ModalNotasOrden" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <h6 class="form-label ms-4">Notas del producto:</h6>
                                        <asp:TextBox ID="ModalNotasProductos" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <h6 class="form-label ms-4">Notas del material:</h6>
                                        <asp:TextBox ID="ModalNotasMaterial" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="2" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                    <div class="col-sm-12">
                                        <h5 class="mt-1"><i class="bi bi-wrench me-2"></i>Partes abiertos</h5>
                                    </div>
                                </div>
                                <div class="container">
                                    <ul class="nav nav-pills mb-2 nav-fill" id="pills-tab" role="tablist">
                                        <li class="nav-item " role="presentation">
                                            <button class="nav-link shadow active " id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Partes de máquina</button>
                                        </li>
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link shadow " id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Partes de molde</button>
                                        </li>
                                    </ul>
                                    <div class="tab-content shadow" id="pills-tabContent">
                                        <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                                            <asp:GridView ID="GridPartesMaquina" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                                                OnRowCommand="ContactsGridView_RowCommand_Partes" EmptyDataText="No hay partes abiertos.">
                                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                <RowStyle BackColor="White" />
                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Parte" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDParte" runat="server" Font-Bold="true" CssClass="mt-1" Text='<%#Eval("IdMantenimiento") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Avería">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDAvería" runat="server" CssClass="mt-1" Text='<%#Eval("MotivoAveria") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reparación">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEstado" runat="server" Font-Bold="true" Font-Size="Small" CssClass="mt-1" Text='<%#Eval("Texto") %>' /><br />
                                                            <asp:Label ID="lblIDReparacion" runat="server" CssClass="mt-1" Text='<%#Eval("Reparacion") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Observaciones" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblObservaciones" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control mt-1" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-BackColor="#eeeeee" HeaderText="Validar">
                                                        <ItemTemplate>
                                                            <div class="btn-group" role="group" aria-label="Basic example">
                                                                <asp:LinkButton ID="btnValidarParteNOK" runat="server" type="button" class="btn btn-danger" Style="text-align: center; font-weight: bold" CommandName="MaqParteNOK" CommandArgument=' <%#Eval("IdMantenimiento") %>'>
                                                                <i class="bi bi-hand-thumbs-down"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnVerParte" runat="server" type="button" class="btn btn-outline-secondary" Style="text-align: center; font-weight: bold" CommandName="MaqVerParte" CommandArgument=' <%#Eval("IdMantenimiento") %>'>
                                                                <i class="bi bi-eye"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnValidarParte" runat="server" type="button" class="btn btn-success" Style="text-align: center; font-weight: bold" CommandName="MaqParteOK" CommandArgument=' <%#Eval("IdMantenimiento") %>'>
                                                                <i class="bi bi-hand-thumbs-up"></i>
                                                                </asp:LinkButton>

                                                            </div>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane fade show" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                                            <asp:GridView ID="GridPartesMolde" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                                                OnRowCommand="ContactsGridView_RowCommand_Partes" EmptyDataText="No hay partes abiertos.">
                                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                <RowStyle BackColor="White" />
                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Parte" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDParte" runat="server" Font-Bold="true" CssClass="mt-1" Text='<%#Eval("IdReparacionMolde") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Avería">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDAvería" runat="server" CssClass="mt-1" Text='<%#Eval("MotivoAveria") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reparación">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEstado" runat="server" Font-Bold="true" Font-Size="Small" CssClass="mt-1" Text='<%#Eval("Texto") %>' /><br />
                                                            <asp:Label ID="lblIDReparacion" runat="server" CssClass="mt-1" Text='<%#Eval("Reparacion") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Observaciones" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblObservaciones" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control mt-1" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-BackColor="#eeeeee" HeaderText="Validar">
                                                        <ItemTemplate>
                                                            <div class="btn-group" role="group" aria-label="Basic example">
                                                                <asp:LinkButton ID="btnValidarParteNOK" runat="server" type="button" class="btn btn-danger" Style="text-align: center; font-weight: bold" CommandName="MOLParteNOK" CommandArgument=' <%#Eval("IdReparacionMolde") %>'>
                                                                <i class="bi bi-hand-thumbs-down"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnVerParte" runat="server" type="button" class="btn btn-outline-secondary" Style="text-align: center; font-weight: bold" CommandName="MOLVerParte" CommandArgument=' <%#Eval("IdReparacionMolde") %>'>
                                                                <i class="bi bi-eye"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnValidarParte" runat="server" type="button" class="btn btn-success" Style="text-align: center; font-weight: bold" CommandName="MOLParteOK" CommandArgument=' <%#Eval("IdReparacionMolde") %>'>
                                                                <i class="bi bi-hand-thumbs-up"></i>
                                                                </asp:LinkButton>

                                                            </div>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                        <button type="button" id="BtnGuardarAccionesModal" class="btn btn-success" runat="server" onserverclick="ActualizarOrden"><i class="bi bi-sd-card-fill"></i></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
