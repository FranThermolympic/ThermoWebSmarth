<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPIConsultasDimensiones.aspx.cs" Inherits="ThermoWeb.KPI.KPIConsultasDimensiones" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores de consumos</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores de consumos
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="d-flex align-items-start ">
        <div class="nav flex-column nav-pills me-3 " id="v-pills-tab" role="tablist" aria-orientation="vertical">
            <br />
            <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2">&nbsp Operario</i></button>
            <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2">&nbsp General</i></button>
            <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Rep.</i></button>
            <button class="nav-link" id="PILLMAQPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab4" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Val.</i></button>
        </div>
        <div class="tab-content col-10" id="v-pills-tabContent">
            <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                <div class="container">

                    <div class="row mt-2">
                        <div class="tab-content shadow" id="pills-tabContent">
                            <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                                <div class="row" hidden="hidden">
                                    <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasTotalMAQ" style="font-size: 30px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de máquina</h6>
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
                                                        <i class="bi bi-box text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIKgstransformados" style="font-size: 30px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Kgs. transformados</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesHoras">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-7">
                                         <h4>Personal por cliente</h4>
                                    </div>                                    
                                    <div class="col-lg-5 text-right mt-3">
                                        <div class="input-group mb-3">
                                            <div class="form-floating">
                                                <input type="text" id="InputFechaDesde" class="form-control  Add-text" style="line-height" autocomplete="off" runat="server">
                                                <label for="InputFechaDesde">Desde:</label>
                                            </div>
                                            <div class="form-floating">
                                                <input type="text" id="InputFechaHasta" class="form-control Add-text" autocomplete="off" runat="server">
                                                <label for="InputFechaHasta">Hasta:</label>
                                            </div>
                                            <button type="button" class="btn btn-secondary" runat="server" onserverclick="Cargar_tablas">Filtrar</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        
                                        <div class="table-responsive">
                                            <asp:GridView ID="dgv_KPI_ClienteXPersona" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false"
                                                EmptyDataText="No hay fichas para mostrar.">

                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <HeaderTemplate>
                                                            <button type="button" class="btn btn-light border-dark border-1" runat="server" onserverclick="ExportToExcel"><i class="bi bi-cloud-download"></i></button>
                                                            <label class="ms-2">Cliente</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("CLIENTE") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NUM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumOperario" runat="server" Text='<%#Eval("NUMOPERARIO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Operario">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOperario" runat="server" Text='<%#Eval("OPERARIO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Puesto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPuesto" runat="server" Text='<%#Eval("PUESTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Empresa">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpresa" runat="server" Text='<%#Eval("EMPRESA") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Disponible">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisponible" runat="server" Text='<%#Eval("TIEMPODISP") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Trabajando">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFuncionando" runat="server" Text='<%#Eval("TIEMPOFUNC") %>' />
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
                <div class="container">
                    <div class="row">
                        <div class="col-lg-5">
                        </div>
                        <div class="col-lg-2 text-right  mt-3">
                            <h6>Cliente:</h6>
                            <asp:DropDownList ID="Lista_Clientes" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="False">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 text-right mt-3">
                            <h6>Mes:</h6>
                            <asp:DropDownList ID="SeleMes" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="False">
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
                        <div class="col-lg-2 text-right  mt-3">
                            <h6>Periodo de revisión:</h6>
                            <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="False">
                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-1 text-right  mt-3">
                            <h6 style="color: transparent">.</h6>
                            <button type="button" class="btn btn-secondary" style="width: 100%" runat="server" onserverclick="Cargar_tablas">Filtrar</button>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="tab-content shadow" id="pills-tabContent">
                            <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                                <div class="row">
                                    <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasOperario" style="font-size: 30px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de operario</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesMalas">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasCambiador" style="font-size: 30px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas cambiador</h6>
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
                                        <h4>Uso de mano de obra (Operario)</h4>
                                        <div class="table-responsive">
                                            <asp:GridView ID="dgv_KPI_Uso_Operario" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false"
                                                EmptyDataText="No hay fichas para mostrar.">
                                                <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
                    OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
                                                --%>
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>
                                                    <asp:TemplateField HeaderText="Año" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAño" runat="server" Text='<%#Eval("YEAR") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mes" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOperario" runat="server" Text='<%#Eval("MES") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cliente">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("C_CUSTOMER") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Horas" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("TIEMPOOP") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <h4>Uso de mano de obra (Cambiador)</h4>
                                        <div class="table-responsive">
                                            <asp:GridView ID="dgv_KPI_Uso_Cambiador" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false"
                                                EmptyDataText="No hay fichas para mostrar.">
                                                <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
                OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
                                                --%>
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Año" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAño" runat="server" Text='<%#Eval("YEAR") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mes" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOperario" runat="server" Text='<%#Eval("MES") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cliente">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("C_CUSTOMER") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Horas" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("TIEMPOOP") %>' />
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




