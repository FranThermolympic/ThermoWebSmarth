<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_NoConformidades.aspx.cs" Inherits="ThermoWeb.KPI.KPI_NoConformidades" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores del muro de calidad</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores del muro de calidad
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Acciones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../CALIDAD/Alertas_Calidad.aspx">Abrir una no conformidad</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../CALIDAD/Alertas_Calidad.aspx">Consultar alertas de calidad</a></li>
                <li><a class="dropdown-item" href="../CALIDAD/DashboardAlertasCalidad.aspx">Consultar dashboard</a></li>
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
                        <div class="row mt-1">
                            <div class="col-lg-6">
                            </div>
                            <div class="col-lg-2 text-right">

                                <h6>Tipo:</h6>
                                <asp:DropDownList ID="TipoAlerta" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="cargar_tablas">
                                    <asp:ListItem Text="-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="A proveedor" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="De cliente" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Interna" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Logística" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2 text-right">
                                <h6>Sector:</h6>
                                <asp:DropDownList ID="SelecSector" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="cargar_tablas">
                                    <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                    <asp:ListItem Text="Automoción" Value="Automoción"></asp:ListItem>
                                    <asp:ListItem Text="Línea Blanca" Value="Línea Blanca"></asp:ListItem>
                                    <asp:ListItem Text="Menaje" Value="Menaje"></asp:ListItem>
                                    <asp:ListItem Text="Otros" Value="Otros"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2 text-right">
                                <h6>Periodo de revisión:</h6>
                                <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="cargar_tablas">
                                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                    <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                    <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="row">
                            <h2>&nbsp;&nbsp;&nbsp; No Conformidades</h2>
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgv_KPI_Mensual" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" ShowFooter="true"
                                        EmptyDataText="No hay datos para mostrar.">
                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                                        <Columns>
                                            <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>

                                            <asp:TemplateField HeaderText="Mes" HeaderStyle-Width="10%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#9999ff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMes" runat="server" Text='<%#Eval("MES") %>' Font-Bold="true" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="footmes1" runat="server" Text="Media acum." /><hr>
                                                    <asp:Label ID="footmes1suma" runat="server" Text="Total" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NC Oficiales" HeaderStyle-Width="10%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="Oficiales" runat="server" Text='<%#Eval("NCOFICIALES") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGOficiales" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalOficiales" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Objetivo" HeaderStyle-Width="10%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="ObjetivoOF" runat="server" Text='< 4' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="ObjetivoOFfoot" runat="server" Text='< 4' /><hr>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="QInfo" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="QINFO" runat="server" Text='<%#Eval("QINFO") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGQInfo" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="SumaQInfo" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Recurrencia (Defectos)" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="LBLRepiteDefecto" runat="server" Text='<%#Eval("REPITEDEFECTO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Recurrencia (Productos)" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="LBLRepiteReferencia" runat="server" Text='<%#Eval("REPITEREFERENCIA") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="A proveedor" FooterStyle-BackColor="#EFEFEF" Visible="FALSE">
                                                <ItemTemplate>
                                                    <asp:Label ID="Aproveedor" runat="server" Text='<%#Eval("PROVEEDOR") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGOProveedor" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="SumaProveedor" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mes" HeaderStyle-Width="10%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#9999ff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMes3" runat="server" Text='<%#Eval("MES") %>' Font-Bold="true" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="footmes3" runat="server" Text="Media Acum." /><hr>
                                                    <asp:Label ID="footmes3total" runat="server" Text="Total" />
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Pz. Enviadas" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="Enviadas" runat="server" Text='<%#Eval("CantidadEnviada","{0:#,0}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGEnviada" runat="server" Text="100" Font-Bold="true" /><hr>
                                                    <asp:Label ID="SUMEnviada" runat="server" Text="100" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pz. Imputadas" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="NOK" runat="server" Text='<%#Eval("PIEZASNOK","{0:#,0}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGNOK" runat="server" Text="100" Font-Bold="true" /><hr>
                                                    <asp:Label ID="SUMNOK" runat="server" Text="100" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PPM" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="PPM" runat="server" Text='<%#Eval("PPM","{0:0.##}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <br />
                                                    <asp:Label ID="AVGPPM" runat="server" Text="100" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PPM Cliente" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="PPMCliente" runat="server" Text='<%#Eval("CantidadPPM","{0:0.##}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <h2>&nbsp;&nbsp;&nbsp; Costes de No Calidad y chatarras (TOTALES)</h2>
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgv_CostesNoCalidad" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" ShowFooter="true"
                                        EmptyDataText="No hay datos para mostrar.">
                                        <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
                                        --%>
                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                                        <Columns>
                                            <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>

                                            <asp:TemplateField HeaderText="Mes" HeaderStyle-Width="10%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#9999ff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMescostes" runat="server" Text='<%#Eval("MES") %>' Font-Bold="true" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="footmes1costes" runat="server" Text="Media acum." /><hr>
                                                    <asp:Label ID="footmescos1costes" runat="server" Text="Total" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NC - Selecciones" HeaderStyle-Width="8%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="NCSelExternas" runat="server" Text='<%#Eval("SELECCIONEXT","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGNCSelExternas" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalNCSelExternas" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NC - Chatarras" HeaderStyle-Width="8%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="NCChatarras" runat="server" Text='<%#Eval("CHATARRAEXT","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGNCChatarras" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalNCChatarras" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NC - Cargos" HeaderStyle-Width="8%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="NCCargos" runat="server" Text='<%#Eval("CARGOSEXT","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGNCCargos" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalNCCargos" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NC - Cost. Admón." HeaderStyle-Width="8%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="NCCostAdmon" runat="server" Text='<%#Eval("ADMONEXT","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGNCCostAdmon" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalNCCostAdmon" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NC - Otros" HeaderStyle-Width="8%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="NCOtros" runat="server" Text='<%#Eval("OTROSINT","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGNCOtros" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalNCOtros" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GP12" HeaderStyle-Width="8%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="CosteGP12" runat="server" Text='<%#Eval("GP12","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGCosteGP12" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalCosteGP12" runat="server" Text="0" DataFormatString="{0:c}" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Chatarra" HeaderStyle-Width="8%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="CosteChatarra" runat="server" Text='<%#Eval("CHATARRA","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGCosteChatarra" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalCosteChatarra" runat="server" Text="0" Font-Bold="true" />

                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Costes No Calidad" HeaderStyle-Width="13%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#ccccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="CosteNoCalidad" runat="server" Font-Bold="true" Text='<%#Eval("CosteNoCalidad","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGCosteNoCalidad" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalCosteNoCalidad" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arranques" HeaderStyle-Width="10%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="CosteArranques" runat="server" Text='<%#Eval("ARRANQUE","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGCosteArranques" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalCosteArranques" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Coste Total" HeaderStyle-Width="15%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#ccccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="CosteTotal" Font-Bold="true" runat="server" Text='<%#Eval("CosteTotal","{0:c}") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="AVGCosteCosteTotal" runat="server" Text="0" Font-Bold="true" /><hr>
                                                    <asp:Label ID="TotalCosteTotal" runat="server" Text="0" Font-Bold="true" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

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




