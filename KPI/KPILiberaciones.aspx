<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPILiberaciones.aspx.cs" Inherits="ThermoWeb.KPI.KPILiberaciones" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores de liberaciones</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Seguimiento de liberaciones
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Informes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../LIBERACIONES/EstadoLiberacion.aspx">Estado actual</a></li>
                <li><a class="dropdown-item" href="../LIBERACIONES/HistoricoLiberacion.aspx">Histórico de liberaciones</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex align-items-start ">
        <div class="nav flex-column nav-pills me-3 " id="v-pills-tab" role="tablist" aria-orientation="vertical">
            <br />
            <button class="nav-link  active" id="PILLPRODUCCION" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2">&nbsp General</i></button>
        </div>
        <div class="tab-content col-10" id="v-pills-tabContent">
            <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-10">
                        </div>
                        <div class="col-lg-2 text-right">
                            <br />
                            <h6>Periodo de revisión:</h6>
                            <asp:DropDownList ID="DropSelectAño" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="Cargar_todas">
                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link shadow " id="pills-profile-tab2" data-bs-toggle="pill" data-bs-target="#pills-profile2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Resultados de cambiador</button>
                            </li>
                            <li class="nav-item " role="presentation">
                                <button class="nav-link shadow active " id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Resultados de producción</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link shadow " id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Resultados de calidad</button>
                            </li>
                        </ul>
                        <div class="tab-content shadow" id="pills-tabContent">
                            <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                                <div class="row">
                                    <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-file-earmark-medical text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIProduccionAbiertas" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Órdenes abiertas</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesCosteTotal">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-hand-thumbs-up text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIProduccionConformes" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1" runat="server" id="PORCKPIProduccionConformes">Conformes</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesHoras">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-warning background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-bell text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIProduccionCondicionadas" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1" runat="server" id="PORCKPIProduccionCondicionadas">Condicionadas</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesMalas">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-exclamation-triangle text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIProduccionSinliberar" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1" runat="server" id="PORCKPIProduccionSinliberar">Sin liberar</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesRetrabajadas">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-lg-6">
                                        <h5>Resultados por mes</h5>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridResulDeptProduccion" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false"
                                                EmptyDataText="No hay revisiones para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMES" runat="server" Text='<%#Eval("mes") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Órdenes abiertas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblABIERTAS" runat="server" Text='<%#Eval("ABIERTAS") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Correctas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOK" runat="server" Text='<%#Eval("OK") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Condicionadas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCONDICIONADAS" runat="server" Text='<%#Eval("CONDICIONADAS") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sin liberar">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSINLIBERAR" runat="server" Text='<%#Eval("SINLIBERAR") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <h5>Resultados por trabajador</h5>
                                        <div class="table-responsive">
                                            <asp:GridView ID="dgv_EncargadoTOTAL" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No hay revisiones para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nº" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNEncargado" runat="server" Text='<%#Eval("Encargado") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nombre" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNOMEncargado" runat="server" Text='<%#Eval("Operario") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Órdenes registradas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblORDEncargado" runat="server" Text='<%#Eval("NUMORDENES") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Liberaciones">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLIBEncargado" runat="server" Text='<%#Eval("ProduccionLIBERADO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ratio">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPORENCARGADO" runat="server" Text='<%#Eval("PORCENTAJE", "{0:0.## %}") %>' />
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
                                    <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-file-earmark-medical text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICalidadAbiertas" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Órdenes abiertas</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H1">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-hand-thumbs-up text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICalidadConformes" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1" runat="server" id="PORCKPICalidadConformes">Conformes</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H2">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-warning background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-bell text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICalidadCondicionadas" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1" runat="server" id="PORCKPICalidadCondicionadas">Condicionadas</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H3">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-exclamation-triangle text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICalidadSinliberar" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1" runat="server" id="PORCKPICalidadSinliberar">Sin liberar</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H4">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h5>Resultados por mes</h5>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridResulDeptCalidad" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No hay revisiones para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>

                                                    <asp:TemplateField ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMES" runat="server" Font-Bold="true" Text='<%#Eval("mes") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Órdenes abiertas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblABIERTAS" runat="server" Text='<%#Eval("ABIERTAS") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Correctas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOK" runat="server" Text='<%#Eval("OK") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Condicionadas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCONDICIONADAS" runat="server" Text='<%#Eval("CONDICIONADAS") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sin liberar">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSINLIBERAR" runat="server" Text='<%#Eval("SINLIBERAR") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <h5>Resultados por trabajador</h5>
                                        <asp:GridView ID="dgv_CalidadTOTAL" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                            EmptyDataText="No hay revisiones para mostrar.">
                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nº" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNCalidad" runat="server" Text='<%#Eval("Calidad") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNOMCalidad" runat="server" Text='<%#Eval("Operario") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Órdenes registradas">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORDCalidad" runat="server" Text='<%#Eval("NUMORDENES") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Liberaciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLIBCalidad" runat="server" Text='<%#Eval("CalidadLIBERADO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ratio">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPORCCALIDAD" runat="server" Text='<%#Eval("PORCENTAJE", "{0:0.## %}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="pills-profile2" role="tabpanel" aria-labelledby="pills-profile-tab">
                                <div class="row">
                                    <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-file-earmark-medical text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICambiadorAbiertas" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Órdenes abiertas</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H5">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-hand-thumbs-up text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICambiadorConformes" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1" runat="server" id="PORCKPICambiadorConformes">Conformes</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H6">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-warning background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-bell text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICambiadorCondicionadas" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1" runat="server" id="PORCKPICambiadorCondicionadas">Condicionadas</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H7">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-exclamation-triangle text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICambiadorSinliberar" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1" runat="server" id="PORCKPICambiadorSinliberar">Sin liberar</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H8">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-lg-6">
                                        <h5>Resultados por mes</h5>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridResulDeptCambio" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No hay revisiones para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>

                                                    <asp:TemplateField ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMES" runat="server" Font-Bold="true" Text='<%#Eval("mes") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Órdenes abiertas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblABIERTAS" runat="server" Text='<%#Eval("ABIERTAS") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Correctas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOK" runat="server" Text='<%#Eval("OK") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Condicionadas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCONDICIONADAS" runat="server" Text='<%#Eval("CONDICIONADAS") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sin liberar">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSINLIBERAR" runat="server" Text='<%#Eval("SINLIBERAR") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <h5>Resultados por trabajador</h5>
                                        <asp:GridView ID="dgv_CambiadorTOTAL" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                            EmptyDataText="No hay revisiones para mostrar.">
                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nº" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNCalidad" runat="server" Text='<%#Eval("Cambiador") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNOMCalidad" runat="server" Text='<%#Eval("Operario") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Órdenes registradas">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORDCalidad" runat="server" Text='<%#Eval("NUMORDENES") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Liberaciones">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLIBCalidad" runat="server" Text='<%#Eval("CambiadorLIBERADO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ratio">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPORCCALIDAD" runat="server" Text='<%#Eval("PORCENTAJE", "{0:0.## %}") %>' />
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

</asp:Content>



