<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_Comunicaciones.aspx.cs" Inherits="ThermoWeb.KPI.KPI_Comunicaciones" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Comunicación a trabajador</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores de comunicaciones
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
                <li><a class="dropdown-item" href="../GP12/GP12HistoricoReferencia.aspx">Consultar histórico de producto</a></li>
                <li><a class="dropdown-item" href="../KPI/KPI_GP12.aspx">Consultar indicadores</a></li>
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
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server" >
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat"">
    <div class="d-flex align-items-start ">
        <div class="nav flex-column nav-pills me-3 " id="v-pills-tab" role="tablist" aria-orientation="vertical">
            <br />
            <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-bricks"></i>&nbsp Muro de calidad</i></button>
            <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600" visible="false" ><i class="bi bi-grid-1x2">&nbsp No conformidades</i></button>
            <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Rep.</i></button>
            <button class="nav-link" id="PILLMAQPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab4" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Val.</i></button>
        </div>
        <div class="tab-content col-10" id="v-pills-tabContent" >
            <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-10">
                        </div>
                        <div class="col-lg-2 text-right">
                            <br />
                            <h6>Periodo de revisión:</h6>
                            <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm mb-2" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="Cargar_tablas">
                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">                        
                        <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab" role="tablist">
                            <li class="nav-item " role="presentation">
                                <button class="nav-link shadow active " id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Resultados del mes</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link shadow " id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Resultados del año</button>
                            </li>
                        </ul>
                        <div class="tab-content shadow" id="pills-tabContent" style="background-color:white">
                            <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab" >
                                <div class="row">
                                    <div class="col-lg-2 mt-2 mb-2">
                                        <h6>Mes:</h6>
                                        <asp:DropDownList ID="SelecMes" runat="server" CssClass="form-select shadow-sm mb-2" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="Cargar_tablas">
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
                                    <div class="col-lg-10">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-layers-half text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIDeteccionesMES" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Detecciones</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesDetecciones">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-hand-thumbs-up text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIInformadosMES" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Operarios informados</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesInformados">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-exclamation-diamond text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPINoInformadosMES" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Operarios sin informar</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesNoInformados">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                               <div class="row">
                                    <div class="col-lg-6">
                                        <h2>&nbsp;&nbsp;&nbsp; Histórico de informadores</h2>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridInformadores" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>                                                    
                                                     <asp:TemplateField HeaderText="Mes"  ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMES" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Informador"  ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNombreInformador" runat="server" Text='<%#Eval("INFORMADOR") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comunicaciones" ItemStyle-Font-Size="large">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumComunicaciones" runat="server" Text='<%#Eval("INFORMADO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                     
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <h2>&nbsp;&nbsp;&nbsp; Estado de operarios</h2>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridOperarios" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay piezas retrabajadas para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnDetail2" CommandName="Redirect" CommandArgument='<%#Eval("NOp")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                 
                                                    <asp:TemplateField HeaderText="Mes"  ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMES" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Operario" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOKAño" runat="server" Text='<%#Eval("NOp") + " " + Eval("Operario") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Informadas" ItemStyle-Font-Size="large">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOKAño" runat="server" Text='<%#Eval("INFORMADO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pendientes" ItemStyle-Font-Size="large">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblScrapNOKAño" runat="server" Text='<%#Eval("PENDIENTE") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                                 <div class="row">
                                    <div class="col-sm-4 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-layers-half text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIDeteccionesAÑO" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Detecciones</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootAÑODetecciones">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-hand-thumbs-up text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIInformadosAÑO" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Operarios informados</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootAÑOInformados">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-exclamation-diamond text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPINoInformadosAÑO" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Operarios sin informar</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootAÑONoInformados">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                </div>                               
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h2>&nbsp;&nbsp;&nbsp; Histórico de informadores</h2>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridInformadoresAño" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Año"  ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAño" runat="server" Text='<%#Eval("AÑO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Informador"  ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNombreInformador" runat="server" Text='<%#Eval("INFORMADOR") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comunicaciones" ItemStyle-Font-Size="large">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumComunicaciones" runat="server" Text='<%#Eval("INFORMADO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                        
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <h2>&nbsp;&nbsp;&nbsp; Estado de operarios</h2>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridOperariosAño" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay piezas retrabajadas para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnDetail2" CommandName="Redirect" CommandArgument='<%#Eval("NOp")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Año" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRefNOKAño" runat="server" Text='<%#Eval("AÑO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Operario" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOKAño" runat="server" Text='<%#Eval("NOp") + " " + Eval("Operario") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Informadas" ItemStyle-Font-Size="large">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOKAño" runat="server" Text='<%#Eval("INFORMADO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pendientes" ItemStyle-Font-Size="large">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblScrapNOKAño" runat="server" Text='<%#Eval("PENDIENTE") %>' />
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




