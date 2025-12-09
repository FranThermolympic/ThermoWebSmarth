<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lista_Operario_Recursos_Pendientes_Firmar.aspx.cs" Inherits="ThermoWeb.RRHH.Lista_Operario_Recursos_Pendientes_Firmar" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Artículos entregados pendientes de firma</title>
    <%-- <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />--%>
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Artículos entregados pendientes de firma
              
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function filtro_NOK() {
            alert("Se ha producido un error al filtrar. Debes seleccionar el operario tal y como aparece en el desplegable del filtro. Revisa que los datos introducidos estén bien.");
        }
    </script>
    <br />
    <div class="container-fluid">
        <div class="tab-content" id="nav-tabContent">
            <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                <div class="row">
                    <div class="col-lg-4">
                        <div class="input-group ms-3">
                            <button id="filtrar" runat="server" class="btn btn-outline-secondary" type="button" onserverclick="Rellenar_grid">Filtrar</button>
                            <input class="form-control" list="FiltroOperario" id="InputFiltroOperario" runat="server" placeholder="Escribe un operario...">
                            <datalist id="FiltroOperario" runat="server">
                            </datalist>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-10">
                        </div>
                        <div class="col-lg-2">
                            <div class="form-check form-switch me-2" style="float: right">
                                <label class="form-check-label" for="flexSwitchCheckChecked">Ocultar entregados</label>
                                <input class="form-check-input" type="checkbox" id="CheckEntregados" runat="server" checked>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="container">
                    <asp:Label ID="numOperarioP2" Font-Bold="true" Font-Size="large" runat="server"></asp:Label>
                    <asp:GridView ID="GridView2" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                        Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                        OnRowUpdating="GridView_RowUpdating" OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
                        OnRowCommand="GridView_RowCommand" OnRowDeleting="GridView_RowDeleting" OnRowDataBound="GridView_RowDataBound"
                        EmptyDataText="There are no data records to display.">
                        <HeaderStyle CssClass="card-header" BackColor="OrangeRed" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="#e6e6e6" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnRedirect" runat="server" CommandName="Redirect" class="btn btn-outline-dark" CommandArgument='<%#Eval("LINEA") %>'><i class="bi bi-pencil-square"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operario" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblOperario" runat="server" Font-Size="X-Large" Text='<%#Eval("OPERARIO") %>' />
                                    <asp:Label ID="lblNombre" runat="server" Font-Bold="true" Text='<%#Eval("NOMBRE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLinea" HeaderStyle-Width="10%" runat="server" Text='<%#Eval("LINEA") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Articulo" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblArticulo" runat="server" Text='<%#Eval("ARTICULO") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Identificador" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTalla" runat="server" Text='XL' Visible="false" /><br />
                                    <asp:Label ID="lblNumSerie" runat="server" Text='<%#"(" + Eval("NUMSERIE") + ")" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cantidad" ItemStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de entrega" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%#Eval("FECHAENTREGA") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Firma" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Image ID="lblFirmaIMG" runat="server" Height="50px" ImageUrl='<%#Eval("Firma")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
