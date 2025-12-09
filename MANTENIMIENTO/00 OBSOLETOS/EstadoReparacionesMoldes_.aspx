<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EstadoReparacionesMoldes_.aspx.cs" Inherits="ThermoWeb.MANTENIMIENTO.EstadoReparacionesMoldes_" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Acciones pendientes de moldes</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Acciones pendientes de moldes
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex" id="wrapper">
        <div class="flex-shrink-0 p-3 bg-dark shadow-sm" style="width: 240px;">
            <a class="btn btn btn-outline-primary border-0" href="MantenimientoIndex.aspx" role="button" style="width: 100%; color: white; text-align: start"><i class="bi bi-building me-2"></i>Página principal</a><br />              
            
            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-tools me-md-2"></i>Partes de trabajo    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="../MANTENIMIENTO/ReparacionMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Moldes</a></div>
            </div>

            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-list-check me-md-2"></i>Acciones abiertas <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample2">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMaquina.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Moldes</a></div>
            </div>
            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-journals me-md-2"></i>Informes    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample3">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de moldes</a><br />
                </div>
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="InformePerifericos.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de periféricos</a></div>
            </div>
            <i class="bi bi-journals me-md-2"></i>
        </div>
        <div class="flex-fill" id="page-content-wrapper">
            <div class="container-fluid">
                <div class="row mt-3">
                    <div class="col-lg-6"></div>
                    <div class="col-lg-2">
                        <h6>Tipo de mantenimiento:</h6>
                        <asp:DropDownList ID="SelectTipoMant" runat="server" class="form-select form-select-sm" Width="100%" Height="35px" />
                    </div>
                    <div class="col-lg-4">
                        <div class="input-group  mt-4">
                            <input class="form-control" list="FiltroMolde" id="tbBuscarMolde" runat="server" placeholder="Selecciona un molde...">
                            <datalist id="FiltroMolde" runat="server">
                            </datalist>
                            <button id="VerTodas" runat="server" type="button" class="btn btn-secondary " onserverclick="VerTodo">Ver todas</button>
                            <button class="btn btn-outline-dark me-md-3" type="button" runat="server" onserverclick="CargarFiltrados">Filtrar</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="nav nav-pills me-3 " id="v-pills-tab" role="tablist">
                <br />
                <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-wrench me-2"></i>Pendientes de reparación</button>
                <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-hand-thumbs-up me-2"></i>Pendientes de validación</button>
            </div>
            <div class="tab-content col-12" id="v-pills-tabContent">
                <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                    <div class="container-fluid mt-3">
                        <div class="form-check form-switch">
                            <input class="form-check-input" type="checkbox" runat="server" id="SwitchMostraPreventivos" checked="checked" onchange="CargarFiltrados">
                            <label class="form-check-label" for="SwitchActivas">Mostrar Mant. Preventivos</label>

                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="dgv_ListaPendientesMOLDE" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%"  CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                EmptyDataText="No hay partes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-outline-dark" Font-Size="Large" runat="server" CommandName="Redirect" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parte" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="large" Text='<%#Eval("PARTE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maquina" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("MOLDE") %>' /><br />
                                            <asp:Label ID="lblPeriferico" runat="server" Text='<%#Eval("DESCRIPCION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Avería" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoMant" Font-Bold="true" runat="server" Text='<%#Eval("TIPOMANTENIMIENTO") %>' /><br />
                                            <asp:Label ID="lblAveria" runat="server" Text='<%#Eval("AVERIA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fechas" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFecha" runat="server" Text='<%#"<strong>Creado: </strong>" + Eval("FECHAVERIA") %>' /><br />
                                            <asp:Label ID="Label1" runat="server" Text='<%#"<strong>Prox.prod.: </strong>"%>' /><br />
                                            <asp:Label ID="lblReplanificada" runat="server" Text='<%#"<strong>Rep. plan.: </strong>" + Eval("FECHINICIOREP") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Horas previstas" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHorasEstimadas" runat="server" Text='<%#Eval("HorasEstimadasReparacion","{0:0.##}") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                   
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab2" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                    <div class="container-fluid mt-3">
                        <h6>Personal:</h6>
                        <asp:DropDownList ID="SelectPersonal" runat="server" class="form-select form-select-sm" Width="100%" Height="35px" />

                        <div class="table-responsive">
                            <asp:GridView ID="dgv_ListaPendientesValidarMAQ" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridView_RowDataBound" EmptyDataText="No hay partes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-outline-dark" Font-Size="Large" runat="server" CommandName="Redirect" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Parte" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="large" Text='<%#Eval("PARTE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maquina" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("MOLDE") %>' /><br />
                                            <asp:Label ID="lblPeriferico" runat="server" Text='<%#Eval("DESCRIPCION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Avería" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoMant" Font-Bold="true" runat="server" Text='<%#Eval("TIPOMANTENIMIENTO") %>' /><br />
                                            <asp:Label ID="lblAveria" runat="server" Text='<%#Eval("AVERIA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fechas" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFecha" runat="server" Text='<%#"<strong>Reparado: </strong>" + Eval("FECHAVERIA") %>' /><br />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFechaprevsalida" runat="server" CssClass="textbox Add-text" Width="80px" autocomplete="off" Text='<%#Eval("FECHINICIOREP", "{0:dd/MM/yyyy}") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A validar por:" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAsignado" runat="server" Text='<%#Eval("AREPARAR") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="dropdownAsignado" runat="server" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>                                    
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab3" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                    <div class="container-fluid mt-3">
                    </div>

                </div>
                <div class="tab-pane fade" id="v-pills-tab4" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                    <div class="container-fluid mt-3">
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab5" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab6" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

















































