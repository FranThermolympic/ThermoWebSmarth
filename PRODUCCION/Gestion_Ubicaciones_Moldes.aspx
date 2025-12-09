<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Gestion_Ubicaciones_Moldes.aspx.cs" Inherits="ThermoWeb.PRODUCCION.Gestion_Ubicaciones_Moldes" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Gestión de ubicaciones de moldes</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Gestor de ubicaciones (moldes)
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown  ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Acciones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMoldes.aspx">Crear un parte de molde</a></li>
                <li><a class="dropdown-item" href="../LIBERACIONES/EstadoLiberacion.aspx">Liberar una máquina</a></li>
                <li><a class="dropdown-item" href="Tareas_cambiador.aspx?TAB=TALLER">Trasladar molde al taller</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Informes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="Tareas_Cambiador.aspx">Previsión cambios de molde</a></li>
                <li><a class="dropdown-item" href="Tareas_Cambiador.aspx?TAB=LISTAMOLDES">Listado de moldes</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación</a></li>
                <li><a class="dropdown-item" href="Gestion_Ubicaciones_Moldes.aspx">Gestionar ubicaciones (moldes)</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ShowPopup() {
            document.getElementById("AUXMODALACCION").click();
            //$("#AUXMODALACCION").click();
        }
    </script>
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-1 me-md-3 mb-1">
                        <button id="btnoffcanvas" runat="server" type="button" class="btn btn-outline-dark ms-md-0 bi bi-funnel-fill bg-white" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger" visible="false"></button>
                        <button id="AUXMODAL" runat="server" type="button" class="btn-close" data-bs-target="#ModalEditaUbicacion" data-bs-toggle="modal" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                        <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaUbicacion" style="font-size: larger"></button>

                    </div>
                </div>
            </div>
            <div class="row">
                <ul class="nav nav-pills mb-2  nav-fill bg-white shadow " id="pills-tab" role="tablist">
                    <li class="nav-item " role="presentation">
                        <button class="nav-link border border-1 border-secondary active " id="TAB_NAVE3" runat="server" data-bs-toggle="pill" data-bs-target="#pills_TAB_NAVE3" type="button" role="tab" aria-controls="pills-home" aria-selected="true">NAVE 3</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link border border-1 border-secondary shadow shadow-sm  " id="TAB_NAVE4" runat="server" data-bs-toggle="pill" data-bs-target="#pills_TAB_NAVE4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">NAVE 4</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link border border-1 border-secondary shadow shadow-sm  " id="TAB_NAVE5" runat="server" data-bs-toggle="pill" data-bs-target="#pills_TAB_NAVE5" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">NAVE 5</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link border border-1 border-secondary shadow shadow-sm  " id="TAB_EXTERNO" runat="server" data-bs-toggle="pill" data-bs-target="#pills_TAB_EXTERNO" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">EXTERNOS</button>
                    </li>
                </ul>
            </div>
            <div class="row">
                <div class="tab-content shadow" id="pills-tabContent">
                    <div class="tab-pane fade show active" id="pills_TAB_NAVE3" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <div class="row">
                            <div class="col-lg-2 rounded rounded-2 border border-1 border-secondary shadow" style="background-color: #E1E1E1">
                                <div class="nav flex-column nav-pills " id="v-pills-tab" role="tablist" aria-orientation="vertical">
                                    <h5 class="MS-2 mt-2">ALTILLO</h5>
                                    <button class="nav-link active text-md-start" id="BTN_ALTILLO_ABC" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_ABC" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">ALTILLO A-B-C</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_DEF" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_DEF" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false">ALTILLO D-E-F</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_G" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_G" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false">ALTILLO G</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_HIJ" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_HIJ" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">ALTILLO H-I-J</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MLK" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MLK" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false">ALTILLO M-L-K</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_XX" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_XX" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false">ALTILLO X1-X2-X3</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_NOP" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_NOP" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false">ALTILLO P-O-N</button>
                                    <h5 class="MS-2 mt-2">ALTILLO SUELO</h5>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_YZ" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_YZ" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false">ALTILLO Y-Z</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_AAAC" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_AAAC" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false">ALTILLO AA-AC</button>
                                    <h5 class="MS-2 mt-2">BAJO ALTILLO</h5>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_ABAK" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_ABAK" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false">ALTILLO AB-AK</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_AE" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_AE" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false">ALTILLO AE-AF</button>
                                    <h5 class="MS-2 mt-2">JUNTO A MÁQUINAS</h5>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ46" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ46" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false">MÁQUINA 46</button>
                                </div>
                            </div>
                            <div class="col-lg-10">
                                <div class="tab-content" id="v-pills-tabContent">
                                    <div class="tab-pane fade show active" id="ALTILLO_ABC" runat="server" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="row">
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 A-C.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado1" runat="server" AllowSorting="True"
                                                    CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0 overflow-auto" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado2" runat="server" AllowSorting="True"
                                                    CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado3" runat="server" AllowSorting="True"
                                                    CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_DEF" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 D-F.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado4" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado5" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado6" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_G" runat="server" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 G.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado7" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_HIJ" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 H-J.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado8" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado9" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado10" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MLK" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 K-M.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado11" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado12" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado13" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_XX" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 X1-X3.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado14" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado15" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado16" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_NOP" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 P-N.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado17" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado18" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado19" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_YZ" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 Z-Y.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado20" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado21" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_AAAC" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 AA-AC.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado22" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado23" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_ABAK" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 AB-AK.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado24" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado25" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_AE" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 AE.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado26" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado44" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ46" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE3 MAQ46.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-5 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado27" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
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
                    </div>
                    <div class="tab-pane fade show" id="pills_TAB_NAVE4" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <div class="row">
                            <div class="col-lg-2 rounded rounded-2 border border-1 border-secondary shadow" style="background-color: #E1E1E1">
                                <div class="nav flex-column nav-pills me-3 border border-end" id="v-pills-tabNAVE4" role="tablist" aria-orientation="vertical">
                                    <h5 class="MS-2 mt-2">JUNTO A MÁQUINAS</h5>
                                    <button class="nav-link active text-md-start" id="BTN_ALTILLO_MAQ25" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ25" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">MÁQUINA 25</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ29" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ29" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false">MÁQUINA 29</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ31" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ31" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">MÁQUINA 31</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ32" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ32" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false">MÁQUINA 32 - AD</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ38" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ38" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false">MÁQUINA 38</button>
                                </div>
                            </div>
                            <div class="col-lg-10">
                                <div class="tab-content" id="v-pills-tabContentNAVE4">
                                    <div class="tab-pane fade show active" id="ALTILLO_MAQ25" runat="server" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE4 MAQ25.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado28" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ29" runat="server" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE4 MAQ29.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado29" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ31" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE4 MAQ31.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado30" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ32" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE4 MAQ32.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado31" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado45" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ38" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE4 MAQ38.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado32" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
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
                    </div>
                    <div class="tab-pane fade show " id="pills_TAB_NAVE5" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <div class="row">
                            <div class="col-lg-2 rounded rounded-2 border border-1 border-secondary shadow" style="background-color: #E1E1E1">
                                <div class="nav flex-column nav-pills me-3 border border-end" id="v-pills-tabNAVE5" role="tablist" aria-orientation="vertical">
                                    <h5 class="MS-2 mt-2">JUNTO A MÁQUINAS</h5>
                                    <button class="nav-link active text-md-start" id="BTN_ALTILLO_AG" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_AG" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">UBICACIÓN AG</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ33" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ33" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false">MÁQUINA 33</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ39" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ39" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">MÁQUINA 39</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ42" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ42" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false">MÁQUINA 42</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ43" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ43" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false">MÁQUINA 43</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_MAQ48" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_MAQ48" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false">MÁQUINA 48</button>
                                </div>
                            </div>
                            <div class="col-lg-10">
                                <div class="tab-content" id="v-pills-tabContentNAVE5">
                                    <div class="tab-pane fade show active" id="ALTILLO_AG" runat="server" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 AG.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado33" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ33" runat="server" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 MAQ33.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado34" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ39" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 MAQ39.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado35" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ42" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 MAQ42.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado36" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ43" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 MAQ43.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado37" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_MAQ48" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 MAQ48.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado38" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
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
                    </div>
                    <div class="tab-pane fade show " id="pills_TAB_EXTERNO" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <div class="row">
                            <div class="col-lg-2 rounded rounded-2 border border-1 border-secondary shadow" style="background-color: #E1E1E1">
                                <div class="nav flex-column nav-pills me-3 border border-end" id="v-pills-tabEXTERNO" role="tablist" aria-orientation="vertical">
                                    <h5 class="MS-2 mt-2">JUNTO A MÁQUINAS</h5>
                                    <button class="nav-link active text-md-start" id="BTN_ALTILLO_NAVE68" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_NAVE68" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">NAVE 68-69</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_OGX" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_OGX" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false">OGX</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_CASTRO" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_CASTRO" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true">CASTRO</button>
                                    <button class="nav-link text-md-start" id="BTN_ALTILLO_SINUBICAR" runat="server" data-bs-toggle="pill" data-bs-target="#ALTILLO_SINUBICAR" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false">Externo / Sin ubicación</button>
                                </div>
                            </div>
                            <div class="col-lg-10">
                                <div class="tab-content" id="v-pills-tabContentEXTERNO">
                                    <div class="tab-pane fade show active" id="ALTILLO_NAVE68" runat="server" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="row" hidden="hidden">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 AG.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado39" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-6 justify-content-center mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado40" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_OGX" runat="server" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                                        <div class="row" hidden="hidden">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 MAQ33.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado41" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_CASTRO" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row" hidden="hidden">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 MAQ39.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado42" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="ALTILLO_SINUBICAR" runat="server" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row" hidden="hidden">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <img id="" class="img-fluid" src="IMAGENES\NAVE5 MAQ42.png" />
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="mt-5 bg-white border border-1 rounded rounded-1">
                                                    <button type="button" class="btn btn-sm btn-warning disabled ms-1 me-1"><i class="bi bi-calendar-x"></i></button>
                                                    Más de dos años sin producir<br />
                                                    <button type="button" class="btn btn-sm btn-danger disabled ms-1 me-1 mb-1" style="font-size: xx-small">OBS</button>Molde obsoleto                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                                <asp:GridView ID="dgv_Listado43" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                    OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("FechaUltimaProduccion") %>' Visible="false" />
                                                                <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' Visible="false" />
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary me-md-1 " CommandName="ActUbicacion" CommandArgument='<%#Eval("Ubicacion") %>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ubic.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molde">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("MOLDE") %>' />
                                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalEditaUbicacion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background: #e6e6e6">
                    <h5 class="modal-title" id="staticBackdropLabel" runat="server">Gestionar Ubicación:
                        <asp:Label ID="lblNombreUbicacion" CssClass="ms2" runat="server">A01</asp:Label>
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" runat="server">

                    <div class="row">
                        <div class="col-lg-12">
                            <h6>Ubicar molde:</h6>

                            <div class="input-group mb-3">
                                <input class="form-control" list="selectmolde" id="InputFiltroMoldes" runat="server" placeholder="Escribe un molde...">
                                <button class="btn btn-outline-secondary" runat="server" id="AgregaMolde" onserverclick="Agregar_Molde" type="button">Agregar</button>
                                <datalist id="selectmolde" runat="server">
                                </datalist>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div style="overflow-y: auto;">
                            <asp:GridView ID="DgvListaMoldesUbicados" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded rounded-2" OnRowUpdating="GridView_RowUpdating" BorderColor="black" Width="100%">
                                <HeaderStyle CssClass="card-header" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BORRARREVISION" CssClass="btn btn-sm btn-danger" CommandName="update" runat="server"><strong>X</strong></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Moldes en la ubicación" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMolde" runat="server" Text='<%#Eval("Molde") %>' />
                                            <asp:Label ID="lblMolnum" runat="server" Text='<%#Eval("Molnum") %>' Visible="false" />
                                            <asp:Label ID="lblFecha" runat="server" Font-Size="Small" Text='<%#" - Actualizado: (" + Eval("FechaModificaUbicacion") + ")" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
                <div class="modal-footer" style="background: #e6e6e6" hidden="hidden">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                    <asp:LinkButton ID="BtnGuardarAccion" runat="server" class="btn btn-success"><i class="bi bi-sd-card"></i></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>




