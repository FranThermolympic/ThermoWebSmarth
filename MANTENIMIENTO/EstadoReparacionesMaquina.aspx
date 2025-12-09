<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EstadoReparacionesMaquina.aspx.cs" Inherits="ThermoWeb.MANTENIMIENTO.EstadoReparacionesMaquina" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Acciones pendientes de máquina</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Acciones pendientes de máquina
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown  ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Partes de trabajo
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMaquinas.aspx">Partes de máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMoldes.aspx">Partes de moldes</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación de referencia</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/MantenimientoIndex.aspx">Índice de mantenimiento</a></li>
            </ul>

        </li>
        <li class="nav-item dropdown  ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Acciones abiertas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMaquina.aspx">Pendientes en máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMoldes.aspx">Pendientes en moldes</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Informes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMaquinas.aspx">Informe de máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMoldes.aspx">Informe de moldes</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformePerifericos.aspx">Informe de periféricos</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../KPI/KPI_Mantenimiento.aspx">Resultados de mantenimiento</a></li>

            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid" id="wrapper">
            <div class="row">
                <div class="col-lg-4"></div>
                <div class="col-lg-2 mt-2">
                    <h6>Máquina:</h6>
                    <asp:DropDownList ID="SelectMaquina" runat="server" class="form-select form-select-sm" Width="100%" Height="35px" />
                </div>
                <div class="col-lg-2 mt-2">
                    <h6>Periférico:</h6>
                    <asp:DropDownList ID="SelectPeriferico" runat="server" class="form-select form-select-sm" Width="100%" Height="35px" />
                </div>
                <div class="col-lg-2 mt-2">
                    <h6>Tipo de mantenimiento:</h6>
                    <asp:DropDownList ID="SelectTipoMant" runat="server" class="form-select form-select-sm" Width="100%" Height="35px" />
                </div>
                <div class="col-lg-2 mt-2">
                    <div class="btn-group mt-4 mb-2" style="width: 100%" role="group" aria-label="Basic example">
                        <button id="VerTodas" runat="server" type="button" class="btn btn-secondary " onserverclick="VerTodo">Ver todas</button>
                        <button id="CargarFiltros" runat="server" type="button" class="btn btn-outline-dark bg-white" onserverclick="CargarFiltrados">Filtrar</button>
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
                        <div class="table-responsive mt-3">
                            <asp:GridView ID="dgv_ListaPendientesMAQ" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border border-secondary" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" OnRowUpdating="GridView_RowUpdating"
                                OnRowEditing="GridView_RowEditing" OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowDataBound="GridView_RowDataBound" EmptyDataText="No hay partes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" Visible="false" Width="100%" Text="Asignar" CssClass="btn btn-sm btn-default" CommandName="Edit" />
                                            <%-- <asp:Button ID = "button3" runat = "server" Width="100%" CommandName="Redirect" class="btn btn-sm btn-default" CommandArgument='<%#Eval("PARTE")%>' Text="Asignar" />--%>
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-outline-dark" Font-Size="Large" runat="server" CommandName="Redirect" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <%--Botones de grabar y cancelar la edición de registro...--%>
                                            <asp:Button ID="btnUpdate" runat="server" Text="Guardar" CssClass="btn btn-success" Width="100%"
                                                CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar este parte?');" />
                                            <asp:Button ID="btnRedirect" runat="server" Width="100%" CommandName="Redirect" class="btn btn-sm btn-info" CommandArgument='<%#Eval("PARTE")%>' Text="Ver parte" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-danger" Width="100%" CommandName="Cancel" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parte" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" FooterStyle-BackColor="#9999ff">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="large" Text='<%#Eval("PARTE") %>' /><br />
                                            <asp:Label ID="lblIdEstadoReparacion" Font-Bold="true" ForeColor="green" Visible="false" runat="server" Text='<%#Eval("IdEstadoReparacion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maquina" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("MAQUINA") %>' />
                                            <br />
                                            <asp:Label ID="lblPeriferico" runat="server" Text='<%#Eval("PERIFERICO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Avería" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoMant" Font-Bold="true" runat="server" Text='<%#Eval("TIPOMANTENIMIENTO") %>' />
                                            <br />
                                            <asp:Label ID="lblAveria" runat="server" Text='<%#Eval("AVERIA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fechas" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFecha" runat="server" Text='<%#"<strong>Creado: </strong>" + Eval("FECHAVERIA") %>' />
                                            <br />
                                            <asp:Label ID="Label1" runat="server" Text='<%#"<strong>Prox.prod.: </strong>"%>' />
                                            <br />
                                            <asp:Label ID="lblReplanificada" runat="server" Text='<%#"<strong>Rep. plan.: </strong>" + Eval("FECHINICIOREP") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFechaprevsalida" runat="server" CssClass="textbox Add-text" Width="80px" autocomplete="off" Text='<%#Eval("FECHINICIOREP", "{0:dd/MM/yyyy}") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asignado a" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAsignado1" runat="server" Text='<%#Eval("AREPARAR1") %>' /><br />
                                            <asp:Label ID="lblAsignado2" runat="server" Text='<%#Eval("AREPARAR2") %>' /><br />
                                            <asp:Label ID="lblAsignado3" runat="server" Text='<%#Eval("AREPARAR3") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="dropdownAsignado" runat="server" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Horas previstas" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHorasEstimadas" runat="server" Text='<%#Eval("HorasEstimadasReparacion","{0:0.##}") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtHorasEstimadas" runat="server" Width="80px" autocomplete="off" Text='<%#Eval("HorasEstimadasReparacion","{0:0.##}") %>' />
                                        </EditItemTemplate>
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

                        <div class="table-responsive mt-3">
                            <asp:GridView ID="dgv_ListaPendientesValidarMAQ" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" OnRowUpdating="GridView_RowUpdating"
                                OnRowEditing="GridView_RowEditing" OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowDataBound="GridView_RowDataBound" EmptyDataText="No hay partes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" Visible="false" Width="100%" Text="Asignar" CssClass="btn btn-sm btn-default" CommandName="Edit" />
                                            <%-- <asp:Button ID = "button3" runat = "server" Width="100%" CommandName="Redirect" class="btn btn-sm btn-default" CommandArgument='<%#Eval("PARTE")%>' Text="Asignar" />--%>
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-outline-dark" Font-Size="Large" runat="server" CommandName="Redirect" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <%--Botones de grabar y cancelar la edición de registro...--%>
                                            <asp:Button ID="btnUpdate" runat="server" Text="Guardar" CssClass="btn btn-success" Width="100%"
                                                CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar este parte?');" />
                                            <asp:Button ID="btnRedirect" runat="server" Width="100%" CommandName="Redirect" class="btn btn-sm btn-info" CommandArgument='<%#Eval("PARTE")%>' Text="Ver parte" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-danger" Width="100%" CommandName="Cancel" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parte" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" FooterStyle-BackColor="#9999ff">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="large" Text='<%#Eval("PARTE") %>' />
                                            <asp:Label ID="lblIdEstadoReparacion" Font-Bold="true" Visible="false" ForeColor="green" runat="server" Text='' />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maquina" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("MAQUINA") %>' />
                                            <br />
                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("PERIFERICO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Avería" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" Font-Bold="true" runat="server" Text='<%#Eval("TIPOMANTENIMIENTO") %>' />
                                            <br />
                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("AVERIA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fechas" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%#"<strong>Reparado: </strong>" + Eval("FECHAVERIA") %>' />
                                            <br />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox Add-text" Width="80px" autocomplete="off" Text='<%#Eval("FECHINICIOREP", "{0:dd/MM/yyyy}") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="A validar por:" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%#Eval("AREPARAR") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server" />
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

















































