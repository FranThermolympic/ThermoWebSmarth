<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="MantenimientoIndex.aspx.cs" Inherits="ThermoWeb.MANTENIMIENTO.MantenimientoIndex" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Área de mantenimiento</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Área de mantenimiento
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown  ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Partes de trabajo
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">               
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMaquinas.aspx">Partes de máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMoldes.aspx">Partes de moldes</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación de referencia</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/GestionPreventivos.aspx">Gestión de preventivos</a></li>
            </ul>

        </li>
        <li class="nav-item dropdown  ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Acciones abiertas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMaquina.aspx">Pendientes en máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMoldes.aspx">Pendientes en moldes</a></li> 
            </ul>

        </li>
        <li class="nav-item dropdown ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Informes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMaquinas.aspx">Informe de máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMoldes.aspx">Informe de moldes</a></li>               
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformePerifericos.aspx">Informe de periféricos</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../KPI/KPI_Mantenimiento.aspx">Resultados de mantenimiento</a></li>
                
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat"">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-4 mt-2 ms-3">
                        <div class="col-sm-12 justify-content-center">
                            <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                <div class="card-body">
                                    <div class="row align-items-center m-b-0">
                                        <div class="col-auto">
                                            <i class="bi bi-chat-left-dots text-white ms-3" style="font-size: 60px"></i>
                                        </div>
                                        <div class="col ms-2 text-md-end">
                                            <i class="text-white me-3 mb-0" runat="server" id="avisosMoldes" style="font-size: 45px">5</i>
                                            <h6 class="text-white me-2 mb-1">Avisos de reparación de moldes</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer bg-light text-md-end">
                                    <a href="../MANTENIMIENTO/ReparacionMoldes.aspx">
                                        <h6 class="mb-1">Ir a reparación de moldes</h6>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-2 ms-3">
                        <div class="col-sm-12 justify-content-center">
                            <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                <div class="card-body">
                                    <div class="row align-items-center m-b-0">
                                        <div class="col-auto">
                                            <i class="bi bi-chat-left-dots text-white ms-3" style="font-size: 60px"></i>
                                        </div>
                                        <div class="col ms-2 text-md-end">
                                            <i class="text-white me-3 mb-0" runat="server" id="avisosMaquinas" style="font-size: 45px">0</i>
                                            <h6 class="text-white me-2 mb-1">Avisos de reparación de máquinas</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer bg-light text-md-end">
                                    <a href="../MANTENIMIENTO/ReparacionMaquinas.aspx">
                                        <h6 class="mb-1">Ir a reparación de máquinas</h6>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-4 mt-3 ms-3">
                        <h3 class="panel-title"><i class="fa fa-list-ul fa-fw"></i>Acciones en moldes</h3>
                        <ul class="nav nav-pills justify-content-end">
                            <li class="nav-item">
                                <button id="TABPREVMOLD" class="nav-link" data-bs-toggle="pill" data-bs-target="#PREVMOLD" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false">Preventivos</button>
                            </li>
                            <li class="nav-item">
                                <button id="TABREPAMOLD" class="nav-link active" data-bs-toggle="pill" data-bs-target="#REPAMOLD" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">Correctivos</button>
                            </li>
                        </ul>
                        <div class="tab-content" id="v-pills-tabContent">
                            <div class="tab-pane fade show active" id="REPAMOLD" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                <div style="overflow-y: auto;">
                                    <asp:GridView ID="dgvMoldPend" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%" ShowFooter="true" OnRowCommand="ContactsGridView_RowCommand">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <FooterStyle BackColor="#e8e8e8" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="VERAPP" CssClass="btn btn-outline-dark ms-md-1" runat="server" CommandName="IrAppMOL" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parte" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParte" runat="server" Text='<%#"<strong>"+Eval("PARTE")+"</strong>"%>' /><br />
                                                    <asp:Label ID="lblFecha" runat="server" Text='<%#"(" + Eval("FECHAVERIA", "{0:dd/MM/yyyy}" +")") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Molde" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMolde" runat="server" Text='<%# "<strong>"+Eval("MOLDE")+"</strong>" %>' /><br />
                                                    <asp:Label ID="lblDescrip" runat="server" Text='<%#Eval("DESCRIPCION") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Averia" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblAveria" runat="server" TextMode="MultiLine" Enabled="false" BackColor="Transparent" BorderColor="Transparent" Style="overflow: hidden" Width="100%" Text='<%#Eval("AVERIA") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="NuevaAccion" CssClass="btn btn-sm btn-outline-dark mb-md-1 mt-md-1 me-md-1 float-end" Font-Size="Large" runat="server" CommandName="RedirectMOL"><i class="bi bi-caret-down-fill"></i></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="PREVMOLD" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                <div style="overflow-y: auto;">
                                    <asp:GridView ID="dgvMoldPendPrev" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%" ShowFooter="true" OnRowCommand="ContactsGridView_RowCommand">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <FooterStyle BackColor="#e8e8e8" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="VERAPP" CssClass="btn btn-outline-dark ms-md-1" runat="server" CommandName="IrAppMOL" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parte" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParte" runat="server" Text='<%#"<strong>"+Eval("PARTE")+"</strong>"%>' /><br />
                                                    <asp:Label ID="lblFecha" runat="server" Text='<%#"(" + Eval("FECHAVERIA", "{0:dd/MM/yyyy}" +")") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Molde" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMolde" runat="server" Text='<%# "<strong>"+Eval("MOLDE")+"</strong>" %>' /><br />
                                                    <asp:Label ID="lblDescrip" runat="server" Text='<%#Eval("DESCRIPCION") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Averia" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblAveria" runat="server" TextMode="MultiLine" Enabled="false" BackColor="Transparent" BorderColor="Transparent" Style="overflow: hidden" Width="100%" Text='<%#Eval("AVERIA") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="NuevaAccion" CssClass="btn btn-sm btn-outline-dark mb-md-1 mt-md-1 me-md-1 float-end" Font-Size="Large" runat="server" CommandName="RedirectMOL" ><i class="bi bi-caret-down-fill"></i></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-3 ms-3">
                        <h3 class="panel-title"><i class="fa fa-list-ul fa-fw"></i>Acciones en máquinas</h3>
                        <ul class="nav nav-pills justify-content-end">
                            <li class="nav-item">
                                <button id="TABPREVMAQ" class="nav-link" data-bs-toggle="pill" data-bs-target="#PREVMAQ" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false">Preventivos</button>
                            </li>
                            <li class="nav-item">
                                <button id="TABREPAMAQ" class="nav-link active" data-bs-toggle="pill" data-bs-target="#REPAMAQ" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">Correctivos</button>
                            </li>
                        </ul>
                        <div class="tab-content" id="v-pills-tabContent2">
                            <div class="tab-pane fade show active" id="REPAMAQ" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                <div style="overflow-y: auto;">
                                    <asp:GridView ID="dgvMaqPend" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%" ShowFooter="true" OnRowCommand="ContactsGridView_RowCommand">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <FooterStyle BackColor="#e8e8e8" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="VERAPP" CssClass="btn btn-outline-dark ms-md-1" runat="server" CommandName="IrAppMAQ" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parte" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParte" runat="server" Text='<%#"<strong>"+Eval("PARTE")+"</strong>"%>' /><br />
                                                    <asp:Label ID="lblFecha" runat="server" Text='<%#"(" + Eval("FECHAVERIA", "{0:dd/MM/yyyy}" +")") %>' /><br />
                                                    <asp:Label ID="lblRepInic" runat="server" Font-Bold="true" Text='<%#Eval("IdEstadoReparacion") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Máquina" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescrip" runat="server" Text='<%#Eval("MAQUINA") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Averia" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblAveria" runat="server" TextMode="MultiLine" Enabled="false" BackColor="Transparent" BorderColor="Transparent" Style="overflow: hidden" Width="100%" Text='<%#Eval("AVERIA") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="RedirectMAQ" CssClass="btn btn-sm btn-outline-dark mb-md-1 mt-md-1 me-md-1 float-end" Font-Size="Large" runat="server" CommandName="RedirectMAQ"><i class="bi bi-caret-down-fill"></i></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="PREVMAQ" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                <div style="overflow-y: auto;">
                                    <asp:GridView ID="dgvMaqPendPrev" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%" ShowFooter="true" OnRowCommand="ContactsGridView_RowCommand">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <FooterStyle BackColor="#e8e8e8" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="VERAPP" CssClass="btn btn-outline-dark ms-md-1" runat="server" CommandName="IrAppMAQ" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parte" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParte" runat="server" Text='<%#"<strong>"+Eval("PARTE")+"</strong>"%>' /><br />
                                                    <asp:Label ID="lblFecha" runat="server" Text='<%#Eval("FECHAVERIA", "{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Máquina" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescrip" runat="server" Text='<%#Eval("MAQUINA") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Averia" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblAveria" runat="server" TextMode="MultiLine" Enabled="false" BackColor="Transparent" BorderColor="Transparent" Style="overflow: hidden" Width="100%" Text='<%#Eval("AVERIA") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="RedirectMAQ" CssClass="btn btn-sm btn-outline-dark mb-md-1 mt-md-1 me-md-1 float-end" Font-Size="Large" runat="server" CommandName="RedirectMAQ" ><i class="bi bi-caret-down-fill"></i></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
            </div>
     
    </div>


</asp:Content>
