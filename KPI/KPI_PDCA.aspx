<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_PDCA.aspx.cs" Inherits="ThermoWeb.KPI.KPIPDCA" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores de planes de acción</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores de planes de acción
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        //scripts de botones
        function VerResultados() {
            $("#PILLRESULTADOS").click();
        }
        function VerPendientes() {
            $("#PILLPENDIENTES").click();
        }
        function VerMOLREPARAR() {
            $("#PILLMOLREPARAR").click();
        }
        function VerMOLVALIDAR() {
            $("#PILLMOLPENDIENTES").click();
        }
        function VerMAQREPARAR() {
            $("#PILLMAQREPARAR").click();
        }
        function VerMAQVALIDAR() {
            $("#PILLMAQPENDIENTES").click();
        }
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
                showAnim: "fold",
                yearRange: "c-20:c+10",
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa']
            });
        });
    </script>
    <div class="row" id="selectpersonalrow" runat="server" visible="false">
        <div class="col-lg-12">
            <div class="input-group ms-1 mb-3 mt-3">
                <button class="btn btn-outline-secondary" type="button" runat="server">Cargar</button>
                <asp:DropDownList ID="SelectPersonal" runat="server" class="form-select" />
            </div>
        </div>
    </div>
    <div class="row">
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
                    <div class="row mt-3">
                        <div class="col-lg-12">
                            <div class="row">
                                <h3>Estado actual</h3>
                                <div class="col-sm-4 justify-content-center">
                                    <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                        <div class="card-body">
                                            <div class="row align-items-center m-b-0">
                                                <div class="col-auto">
                                                    <i class="bi bi-hand-thumbs-up text-white ms-3" style="font-size: 60px"></i>
                                                </div>
                                                <div class="col ms-2 text-md-end">
                                                    <i class="text-white me-3 mb-0" runat="server" id="KPIAccionesCerradas" style="font-size: 45px">0</i>
                                                    <h6 class="text-white me-2 mb-1">Acciones cerradas</h6>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer bg-light text-md-end">
                                            <a href="../PDCA/PDCA.aspx">
                                                <h6 class="mb-1">Ir a planes de acción</h6>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 justify-content-center">
                                    <div class="card prod-p-card bg-warning background-pattern-white shadow h-100">
                                        <div class="card-body">
                                            <div class="row align-items-center m-b-0">
                                                <div class="col-auto">
                                                    <i class="bi bi-stack text-white ms-3" style="font-size: 60px"></i>
                                                </div>
                                                <div class="col ms-2 text-md-end">
                                                    <i class="text-white me-3 mb-0" runat="server" id="KPIAccionesPendientes" style="font-size: 45px">0</i>
                                                    <h6 class="text-white me-2 mb-1">Acciones pendientes</h6>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer bg-light text-md-end">
                                            <a href="../PDCA/PDCA.aspx">
                                                <h6 class="mb-1">Ir a planes de acción</h6>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 justify-content-center">
                                    <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                        <div class="card-body">
                                            <div class="row align-items-center m-b-0">
                                                <div class="col-auto">
                                                    <i class="bi bi-exclamation-triangle text-white ms-3" style="font-size: 60px"></i>
                                                </div>
                                                <div class="col ms-2 text-md-end">
                                                    <i class="text-white me-3 mb-0" runat="server" id="KPIAccionesVencidas" style="font-size: 45px">0</i>
                                                    <h6 class="text-white me-2 mb-1">Acciones vencidas</h6>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer bg-light text-md-end">
                                            <a href="../PDCA/PDCA.aspx">
                                                <h6 class="mb-1">Ir a planes de acción</h6>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-lg-10">
                        </div>
                        <div class="col-lg-2 text-right">             
                            <h6>Periodo de revisión:</h6>
                            <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="rellenargrids">
                               <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>        
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="row">
                                <h3>Resultados por mes</h3>
                                <div style="overflow-y: auto;">
                                    <asp:GridView ID="dgvResultadosMES" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Mes" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%" ItemStyle-BackColor="#ccccff" ItemStyle-CssClass="shadow-sm">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMes" runat="server" Font-Bold="true" Width="100%" Text='<%#Eval("MESAPERTURA") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ac. abiertas" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAbiertas" runat="server" Width="100%" Text='<%#Eval("ABIERTOS") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ac. cerradas" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCerrados" runat="server" Width="100%" Text='<%#Eval("CERRADOS") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actualizaciones" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActualizaciones" runat="server" Width="100%" Text='<%#Eval("ACTUALIZACIONES") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>       
                    <div class="row mt-2">
                        <div class="col-lg-4">
                            <h3>PDCA por personal (cerrados)</h3>
                            <div style="overflow-y: auto;">
                                <asp:GridView ID="dgv_Acciones_CerradosXOP" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="PERSONAL" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%" ItemStyle-BackColor="#ccccff" ItemStyle-CssClass="shadow-sm ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrabajador" runat="server" Font-Bold="true" Width="100%" Text='<%#Eval("NOMBRE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DIRIGIDO" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAbiertas" runat="server" Width="100%" Text='<%#Eval("PLANES") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EJECUTADO" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAbiertas" runat="server" Width="100%" Text='<%#Eval("ACCIONES") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <h3>PDCA por personal (pendientes)</h3>
                            <div style="overflow-y: auto;">
                                <asp:GridView ID="dgv_Acciones_PendientesXOP" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#e6e6e6" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="PERSONAL" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%" ItemStyle-BackColor="#ccccff" ItemStyle-CssClass="shadow-sm">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrabajador" runat="server" Font-Bold="true" Width="100%" Text='<%#Eval("NOMBRE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DIRIGE" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAbiertas" runat="server" Width="100%" Text='<%#Eval("PLANES") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EJECUTA" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAbiertas" runat="server" Width="100%" Text='<%#Eval("ACCIONES") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <h3>PDCA por personal (vencidos)</h3>
                            <div style="overflow-y: auto;">
                                <asp:GridView ID="dgv_Acciones_VencidosXOP" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#e6e6e6" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="PERSONAL" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%" ItemStyle-BackColor="#ccccff" ItemStyle-CssClass="shadow-sm">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrabajador" runat="server" Font-Bold="true" Width="100%" Text='<%#Eval("NOMBRE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DIRIGE" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAbiertas" runat="server" Width="100%" Text='<%#Eval("PLANES") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EJECUTA" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAbiertas" runat="server" Width="100%" Text='<%#Eval("ACCIONES") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12">
                            <h3>Detalle de acciones abiertas</h3>
                            <div style="overflow-y: auto;">
                                <asp:GridView ID="dgv_Acciones_Pendientes" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%" OnRowDataBound="OnRowDataBound">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="#e6e6e6" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="EJECUTA" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEjecutor" Font-Bold="true" runat="server" Width="100%" Text='<%#Eval("EJECUTA") %>' />
                                                <asp:Label ID="AUXlblEjecutor" Font-Bold="true" runat="server" Width="100%" Visible="FALSE" Text='<%#Eval("EJECUTA") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumaccion" runat="server" Width="100%" Text='<%#Eval("NUMACCION") %>' />
                                                <asp:Label ID="lblFechaCierrePrev" runat="server" Width="100%" Visible="false" Text='<%#Eval("FechaCierrePrev") %>' />                                                
                                                <asp:Label ID="lblVencimiento" runat="server" ForeColor="red" Font-Bold="true" Width="100%" Text='' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan acción" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPDCANOM" runat="server" Width="100%" Text='<%#Eval("PLANACCION") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="LEFT" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblproducto" runat="server" Width="100%" Text='<%#Eval("PRODUCTO") %>' />
                                                <asp:Label ID="lblproddescript" runat="server" Width="100%" Text='<%#Eval("Descripcion") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desviación" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblDesviacion" runat="server" TextMode="MultiLine" Enabled="false" BackColor="Transparent" BorderColor="Transparent" Style="overflow: hidden" Width="100%" Text='<%#Eval("DesviacionEncontrada") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acción propuesta" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblREQUISITO" runat="server" TextMode="MultiLine" Enabled="false" BackColor="Transparent" BorderColor="Transparent" Style="overflow: hidden" Width="100%" Text='<%#Eval("REQUISITO") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab2" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="container-fluid mt-md-1">
                                <div style="overflow-y: auto;">
                                    <asp:GridView ID="dgvListaPendientesValidacionMolde" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded border-top-0 " BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in" Style="font-size: 1rem" runat="server" CommandName="ValMoldes" CommandArgument='<%#Eval("IdReparacionMolde")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parte" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReparacionMolde" runat="server" Text='<%#Eval("IdReparacionMolde") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Molde" ItemStyle-Width="35%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDMoldes" runat="server" Text='<%#Eval("IDMoldes") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Avería" ItemStyle-Width="60%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab3" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="container-fluid mt-md-1">
                                <div style="overflow-y: auto;">
                                    <asp:GridView ID="dgvListaPendientesReparacionMaquina" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded border-top-0 " BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in" Style="font-size: 1rem" runat="server" CommandName="RepMaquinas" CommandArgument='<%#Eval("PARTE")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parte" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdPARTE" runat="server" Text='<%#Eval("PARTE") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Máquina" ItemStyle-Width="35%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reparación" ItemStyle-Width="60%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab4" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="container-fluid mt-md-1">
                                <div style="overflow-y: auto;">
                                    <asp:GridView ID="dgvListaPendientesValidacionMaquina" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded border-top-0 " BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in" Style="font-size: 1rem" runat="server" CommandName="ValMaquinas" CommandArgument='<%#Eval("PARTE")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PARTE" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdPARTE" runat="server" Text='<%#Eval("PARTE") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Máquina" ItemStyle-Width="35%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reparación" ItemStyle-Width="60%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab5" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="container-fluid mt-md-1">
                                <div style="overflow-y: auto;">
                                    <asp:GridView ID="dgvListaPendientesNoConformidades" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded border-top-0 " BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in" Style="font-size: 1rem" runat="server" CommandName="NoConformidades" CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nº" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdPARTE" runat="server" Text='<%#Eval("ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Producto" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("PRODDESCRIPCION") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Problema" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDDescripcion" runat="server" Text='<%#Eval("DESCRIPCION") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contramedida" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMotivoAveria2" runat="server" Text='<%#Eval("CONTRAMEDIDA") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab6" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                    <div class="container-fluid mt-md-1">
                        <div style="overflow-y: auto;">
                            <asp:GridView ID="dgvListaPendientesPDCA" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded border-top-0 " BorderColor="black" OnRowCommand="RedireccionaAPP">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in" Style="font-size: 1rem" runat="server" CommandName="PlanAccion" CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nº" ItemStyle-Width="8%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdPDCA" runat="server" Text='<%#Eval("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nº" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdPARTE" runat="server" Text='<%#Eval("NUMACCION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plan de acción" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIDDescripcion" runat="server" Text='<%#Eval("PLANACCION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Desviación" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesviación" runat="server" Text='<%#Eval("DesviacionEncontrada") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acción a realizar" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("REQUISITO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dirección" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDirector" runat="server" Text='<%#Eval("JEFE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ejecución" ItemStyle-Width="14%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("EJECUTA") %>' />
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


