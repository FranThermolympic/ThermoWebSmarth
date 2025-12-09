<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Consulta_Pesos.aspx.cs" Inherits="ThermoWeb.PLANIFICACION.Consulta_Pesos" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Informe de pesos</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Informes de pesos
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex" id="wrapper">
        <div class="flex-fill" id="page-content-wrapper">
            <div class="row mt-3">
                <div class="col-lg-8"></div>
                <div class="col-lg-4">
                    <div class="input-group">
                        <input class="form-control" list="FiltroMolde" id="tbBuscarMolde" runat="server" placeholder="Selecciona una referencia...">
                        <datalist id="FiltroMolde" runat="server">
                        </datalist>
                        <button class="btn btn-outline-dark me-md-3" type="button" runat="server" onserverclick="BuscarMoldeinforme">Filtrar</button>
                    </div>
                </div>

            </div>
            <div class="nav nav-pills me-3 " id="v-pills-tab" role="tablist">
                <br />
                <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2 me-2"></i>Q-MASTERxNAV</button>
                <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-textarea me-2"></i>Estado preventivo</button>
                <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-book-half me-2"></i>Histórico reparaciones</button>
                <button class="nav-link" id="PILLMAQPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab4" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Val.</i></button>
            </div>
            <div class="tab-content col-12" id="v-pills-tabContent">
                <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                    <div class="container-fluid mt-3">
                        <div class="table-responsive">
                            <asp:GridView ID="GridExport" runat="server" Visible="false" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" EmptyDataText="No hay moldes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Producto">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMolde" runat="server" Font-Size="X-Large" Text='<%#Eval("PRODUCTO") + "_" +Eval("DESCRIPCION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Peso medio (g)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReparaciones" runat="server" Font-Size="X-Large" Text='<%#Eval("CANTIDAD") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Peso colada" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblColada" runat="server" Font-Size="X-Large" Text='<%#Eval("PESOCOLADA")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cavidades" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblColada" runat="server" Font-Size="X-Large" Text='<%#Eval("Cavidades")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Peso MIN" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMin" runat="server" Text='<%#Eval("CANTIDADMIN") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Peso MAX" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMax" runat="server" Text='<%#Eval("CANTIDADMAX") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NAV" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReparaciones" runat="server" Font-Size="X-Large" Text='<%#Eval("CANTIDAD_NAV") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Desviacion" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesviacion" runat="server" Font-Size="X-Large" Text='<%#Eval("PORCENTAJE") +"%"  %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="dgv_ListadoPesajes" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" EmptyDataText="No hay moldes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Producto">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMolde" runat="server" Font-Size="X-Large" Text='<%#Eval("PRODUCTO") + "_" +Eval("DESCRIPCION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Peso medio (g)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReparaciones" runat="server" Font-Size="X-Large" Text='<%#Eval("CANTIDAD") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Peso colada" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblColada" runat="server" Font-Size="X-Large" Text='<%#Eval("PESOCOLADA") %>' /><br />
                                            <asp:Label ID="lblCavi" runat="server" Text='<%#"/" + Eval("Cavidades") + " cav." %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MIN/MAX" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMin" runat="server" Text='<%#"<strong>MIN: </strong>" + Eval("CANTIDADMIN") %>' /><br />
                                            <asp:Label ID="lblMax" runat="server" Text='<%#"<strong>MAX: </strong>" + Eval("CANTIDADMAX") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="NAV" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReparaciones" runat="server" Font-Size="X-Large" Text='<%#Eval("CANTIDAD_NAV") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Desviacion" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesviacion" runat="server" Font-Size="X-Large" Text='<%#Eval("PORCENTAJE") +"%"  %>' />
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
                    <div class="table-responsive">
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
































