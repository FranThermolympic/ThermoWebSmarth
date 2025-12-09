<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_Molidos.aspx.cs" Inherits="ThermoWeb.KPI.KPI_Molidos" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores de reciclado</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores de molido
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Acciones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../MATERIALES/GestionMolinos.aspx">Moler un material</a></li>
            </ul>
        </li>

    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="d-flex align-items-start ">
            <div class="nav flex-column nav-pills me-3 " id="v-pills-tab" role="tablist" aria-orientation="vertical">
                <br />
                <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2">&nbsp General</i></button>
                <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-grid-1x2">Listado</i></button>
                <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Rep.</i></button>
                <button class="nav-link" id="PILLMAQPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab4" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Val.</i></button>
            </div>
            <div class="tab-content col-10" id="v-pills-tabContent">
                <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                    <div class="container">
                        <div class="row mt-2">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-4">
                                <br />
                                <h6>Material:</h6>
                                <input class="form-control" list="DatalistFiltroMat" id="InputFiltroMaterial" runat="server" autocomplete="off" placeholder="Escribe un material...">
                                <datalist id="DatalistFiltroMat" runat="server">
                                </datalist>
                            </div>
                            <div class="col-lg-3">
                                <br />
                                <h6>Mes:</h6>
                                <asp:DropDownList ID="SeleMes" runat="server" CssClass="form-select shadow-sm ms-2" Font-Size="Large" AutoPostBack="False">
                                    <asp:ListItem Text="-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-2 text-right">
                                <br />
                                <h6>Periodo de revisión:</h6>
                                <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="rellenar_grid">
                                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1 text-right">
                                <br />
                                <h6>&nbsp</h6>
                                <button type="button" class="btn btn-outline-dark bg-white" style="width: 100%" id="BTNFiltrar" runat="server" onserverclick="rellenar_grid">Filtrar</button>
                            </div>
                        </div>
                        <div class="row mt-2">

                            <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                    <div class="card-body">
                                        <div class="row align-items-center m-b-0">
                                            <div class="col-auto">

                                                <i class="bi bi-window-split text-white ms-3" style="font-size: 60px"></i>
                                            </div>
                                            <div class="col ms-2 text-md-end">
                                                <i class="text-white me-3 mb-0" runat="server" id="KPILineasTotalMOL" style="font-size: 40px">0</i>
                                                <h6 class="text-white me-2 mb-1">Líneas de reciclado generadas</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer bg-light text-md-end">
                                        <h6 class="mb-1" runat="server" id="H1">En la aplicación</h6>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                    <div class="card-body">
                                        <div class="row align-items-center m-b-0">
                                            <div class="col-auto">
                                                <i class="bi bi-recycle text-white ms-3" style="font-size: 60px"></i>
                                            </div>
                                            <div class="col ms-2 text-md-end">
                                                <i class="text-white me-3 mb-0" runat="server" id="KPIKGSMOL" style="font-size: 40px">0</i>
                                                <h6 class="text-white me-2 mb-1">Kgs. de material reciclados</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer bg-light text-md-end">
                                        <h6 class="mb-1" runat="server" id="FootMesReciclados">-</h6>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-box text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIKgstransformados" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Kgs. transformados</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesHoras">En las máquinas de inyección</h6>
                                            </div>
                                        </div>
                                    </div>

                        </div>
                        <div class="row mt-2">
                            <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow " id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Líneas de reciclado</button>
                                </li>
                                <li class="nav-item " role="presentation">
                                    <button class="nav-link shadow active " id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Resultados de reciclado</button>
                                </li>

                            </ul>
                            <div class="tab-content shadow" id="pills-tabContent">
                                <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">

                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h4>Resultados por mes</h4>
                                            <asp:GridView ID="GridKPIporMES" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                                                EmptyDataText="There are no data records to display.">
                                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                <RowStyle BackColor="White" />
                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Mes" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecha" CssClass="ms-2" runat="server" Text='<%#Eval("MES") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Referencia" ItemStyle-HorizontalAlign="left" ItemStyle-BackColor="#e6e6e6">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReferencia" Font-Bold="true" runat="server" Text='<%#Eval("Referencia") %>' /><br />
                                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCantidad" Font-Bold="true" CssClass="ms-2" runat="server" Text='<%#Eval("CANTIDAD") + " kgs." %>' /><br />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-lg-6">
                                            <h4>Resultados por molino</h4>
                                            <asp:GridView ID="GridKPIporMOLINOMES" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                                                EmptyDataText="There are no data records to display.">
                                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                <RowStyle BackColor="White" />
                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Mes" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecha" CssClass="ms-2" runat="server" Text='<%#Eval("MES") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Molino" ItemStyle-HorizontalAlign="left" ItemStyle-BackColor="#e6e6e6">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMolino" CssClass="ms-2" runat="server" Text='<%#Eval("Molino") %>' /><br />
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cantidad">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCantidad" Font-Bold="true" CssClass="ms-2" runat="server" Text='<%#Eval("CANTIDAD") + " kgs." %>' /><br />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h4>Histórico de molidos</h4>
                                            <div class="table-responsive" style="width: 100%">
                                                <asp:GridView ID="dgv_HistoricoMolidos" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                                                    EmptyDataText="There are no data records to display.">
                                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Fecha" ItemStyle-Width="10%" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFecha" Font-Size="Large" CssClass="ms-2" runat="server" Text='<%#Eval("Fecha", "{0:dd/MM/yyyy}") %>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Molino" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left" ItemStyle-BackColor="#e6e6e6">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMolino" Font-Bold="true" Font-Size="Large" CssClass="ms-2" runat="server" Text='<%#Eval("Molino") %>' /><br />
                                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="Medium" CssClass="ms-2" Text='<%#Eval("UBICACION") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cantidad" ItemStyle-Font-Size="X-Large" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMolino" runat="server" Text='<%#Eval("Cantidad") + " Kgs." %>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material" ItemStyle-Width="60%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterial" Font-Bold="true" Font-Size="Large" CssClass="ms-2" runat="server" Text='<%#Eval("Referencia") %>' /><br />
                                                                <asp:Label ID="lblDescripcion" runat="server" CssClass="ms-2" Text='<%#Eval("Descripcion") %>' />
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
                <div class="tab-pane fade" id="v-pills-tab2" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab3" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab4" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab5" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab6" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                </div>
            </div>
        </div>
    </div>

</asp:Content>




