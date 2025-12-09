<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="InformePerifericos.aspx.cs" Inherits="ThermoWeb.MANTENIMIENTO.InformePerifericos" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Informe de periféricos</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Informes de periféricos
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
            <div class="row mt-3">
                <div class="col-lg-8"></div>
                <div class="col-lg-4">
                    <div class="input-group">
                        <input class="form-control" list="FiltroPeriferico" id="tbBuscarMaquina" runat="server" placeholder="Selecciona un periférico...">
                        <datalist id="FiltroPeriferico" runat="server">
                        </datalist>
                        <button class="btn btn-outline-dark me-md-3" type="button" runat="server" onserverclick="BuscarMaquinainforme">Filtrar</button>
                    </div>
                </div>

            </div>
            <div class="nav nav-pills me-3 " id="v-pills-tab" role="tablist">
                <br />
                <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2 me-2"></i>General periférico</button>
                <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-textarea me-2"></i>Estado preventivo</button>
                <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-book-half me-2"></i>Histórico reparaciones</button>
                <button class="nav-link" id="PILLMAQPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab4" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-book-half me-2"></i>Histórico preventivos</button>
            </div>
            <div class="tab-content col-12" id="v-pills-tabContent">
                <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                    <div class="table-responsive">
                        <div class="container-fluid mt-3">
                            <asp:GridView ID="dgv_Listadoperifericos" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                EmptyDataText="No hay moldes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Periferico">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriferico" Font-Size="Large" runat="server" Text='<%#Eval("Máquina") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Familia" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFamilia" runat="server" Text='<%#Eval("Descripcion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nº Reparaciones" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReparaciones" Font-Size="Large" runat="server" Text='<%#Eval("REPARACIONES") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>

                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab2" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab3" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                    <div class="container-fluid mt-3">
                        <div class="table-responsive">
                            <asp:GridView ID="dgv_ListadoHistoricoperifericos" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                EmptyDataText="No hay moldes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-outline-dark" Font-Size="Large" runat="server" CommandName="Redirect" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parte">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParte" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("PARTE") %>' /><br />
                                            <asp:Label ID="lblFecha" runat="server" Text='<%#"("+Eval("FECHA")+")" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maquina">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Máquina") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Avería">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAveria" runat="server" Text='<%#Eval("AVERIA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reparación">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReparacion" runat="server" Text='<%#Eval("REPARACION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Coste" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcoste" runat="server" Font-Size="Large" Text='<%#Eval("ImporteEmpresa2", "{0:0.##€}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>


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





























