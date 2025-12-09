<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FichasParametrosListado.aspx.cs" Inherits="ThermoWeb.PRODUCCION.FichasParametrosListado" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Listado de fichas de parámetros</title>
    <%-- <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />--%>
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Listado de fichas de parámetros
              
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
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="row">
            <div class="col-lg-12">
                <div class="container">
                    <div class="row"></div>
                    <div class="col-lg-12" style="align-items: center">
                        <div class="input-group shadow mt-2 bg-white">
                            <div class="form-floating" style="width: 45%">
                                <input type="text" list="DatalistReferencias" class="form-control border border-dark" id="tbReferencia" runat="server" autocomplete="off">
                                <label for="tbReferencia" style="font-style: italic">Busca un producto</label>
                                <datalist id="DatalistReferencias" runat="server">
                                </datalist>
                            </div>
                            <div class="form-floating" style="width: 45%">
                                <input type="text" list="DatalistMoldes" class="form-control border border-dark" id="tbMolde" runat="server" autocomplete="off">
                                <label for="tbMolde" style="font-style: italic">Busca un molde</label>
                                <datalist id="DatalistMoldes" runat="server">
                                </datalist>
                            </div>
                            <button class="btn btn-primary border border-dark" type="button" id="btnBuscarFicha" style="width: 10%; font-size: x-large" runat="server" onserverclick="BuscarFicha"><i class="bi bi-search"></i></button>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="dgv_ListadoFichas" runat="server" AllowSorting="True" OnRowCommand="ContactsGridView_RowCommand" OnRowDeleting="GridView_RowDeleting"
                                CssClass="table table-responsive shadow p-3 mb-5 rounded border border-dark" AutoGenerateColumns="false"
                                EmptyDataText="There are no data records to display.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <RowStyle BackColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="BtnRedirect" CommandName="CargaDetalle" CommandArgument='<%#Eval("Referencia") + "¬" + Eval("Maquina") + "¬" + Eval("Version") %>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                                    <i class="bi bi-file-post"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="50%">
                                        <HeaderTemplate>
                                            <button type="button" class="btn btn-primary" runat="server" id="BTNOrdenaReferencia" onserverclick="Ordenar_lineas" style="font-weight:bold"><i class="bi bi-arrow-down-up me-2"></i>Referencia</button>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferencia" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("Referencia") %>' />
                                            <asp:Label ID="lblMolde" Font-Bold="true" Font-Italic="true" runat="server" Text='<%#" (" + Eval("CodMolde") + ")" %>' /><br />
                                            <asp:Label ID="lblDescripcion" runat="server" Font-Italic="true" Text='<%#Eval("Descripcion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <HeaderTemplate>
                                            <label class="btn btn-primary" style="font-weight:bold">Máquina</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaquina" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("Maquina") %>' /><br />
                                            <asp:Label ID="lblVersion" runat="server" Font-Italic="true" Text='<%# "Ed." + Eval("Version") %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30%">
                                        <HeaderTemplate>
                                            <button type="button" class="btn btn-primary" runat="server" id="BTNOrdenaFecha" onserverclick="Ordenar_lineas" style="font-weight:bold"><i class="bi bi-arrow-down-up me-2"></i>Fecha y autor</button>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAutor" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("AUTOR") %>' /><br />
                                            <asp:Label ID="lblFecha" Font-Italic="true" runat="server" Text='<%#" (" + Eval("Fecha") + ")" %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" Visible="true">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="button2" CommandName="Delete" CommandArgument='<%#Eval("Referencia") + "¬" + Eval("Maquina") + "¬" + Eval("Version") %>' OnClientClick="return confirm('Vas a eliminar esta ficha, ¿estás seguro?');" CssClass="btn btn-lg btn-danger mt-1 shadow" Style="font-size: 1rem">
                                                    <i class="bi bi-trash"></i></asp:LinkButton>
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
</asp:Content>

