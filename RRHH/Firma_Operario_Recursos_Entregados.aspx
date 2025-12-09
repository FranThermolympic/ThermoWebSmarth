<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Firma_Operario_Recursos_Entregados.aspx.cs" Inherits="ThermoWeb.RRHH.Firma_Operario_Recursos_Entregados" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Entrega de información, EPI y artículos</title>
    <%-- <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />--%>
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Entrega de información, EPI y artículos
              
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
   <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Listados
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="Lista_Operario_Recursos_Pendientes_Firmar.aspx">Pendientes de firma</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <nav>
            <div class="nav nav-tabs mt-1 justify-content-end" id="nav-tab" role="tablist">
                <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile" type="button" role="tab" aria-controls="nav-profile" aria-selected="false"><i class="bi bi-folder2-open"></i>&nbsp Histórico</button>
                <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true"><i class="bi bi-pencil-square"></i>&nbsp Entrega</button>
            </div>
        </nav>
        <div class="tab-content" id="nav-tabContent">
            <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card shadow-lg">
                            <div class="card-header" style="background-color: orangered">
                                <h6 style="color: white">ENTREGA DE DOCUMENTACIÓN / EPI / ARTÍCULO </h6>
                            </div>
                            <div class="card-body" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                                <div class="col-lg-11 ms-5 me-5">
                                    <p>
                                        <asp:HiddenField ID="valIdNAV" runat="server" value="0"/>
                                        <asp:HiddenField ID="valCodigo" runat="server" value=""/>
                                        <asp:Label ID="lblNumOperario" runat="server" Font-Bold="true" Visible="FALSE">0</asp:Label><asp:Label ID="lblNom" runat="server" Font-Size="X-Large">---</asp:Label>&nbsp
                                        <label style="font-size: large">ha recibido el siguiente material necesario para el desarrollo de las labores relativas a su puesto de trabajo:</label>
                                    </p>
                                    <div class="alert alert-secondary shadow-sm ms-2 me-2" role="alert">
                                        <asp:Label ID="lblCantidad" Font-Size="Large" runat="server" Visible="false">---</asp:Label>
                                        <input ID="txtCantidad" runat="server" type="number" value="1" style="width:7%" />
                                        
                                        <asp:Label ID="lblMaterial" Font-Size="Large" CssClass="ms-4" runat="server">---</asp:Label>
                                        <asp:Label ID="lblNumserie" Font-Size="Large" CssClass="ms-4" runat="server">---</asp:Label>
                                    </div>
                                    <p style="font-size: large; text-align: justify">
                                        Por otro lado, ha recibido también la información relativa a las condiciones de manejo, mantenimiento y revisión del material entregado.<br />
                                        <br />
                                        Se recuerda que el trabajador deberá usar adecuadamente, atendiendo a las instrucciones facilitadas, los medios y equipos facilitados por la empresa, y que en todo caso, de observar situaciones que entrañen riesgo para su seguridad y salud derivadas por el uso de los mismos, informará de inmediato a su superior jerárquico.
                                    </p>
                                    <p class="mt-5 me-5" style="text-align: end">
                                        <i>En <strong>THERMOLYMPIC S.L.</strong><br />
                                            C. Alemania S/N<br />
                                            50180, Utebo (Zaragoza)<br />
                                            <asp:Label runat="server" ID="lblFecha">25 de Enero de 2021</asp:Label></i>
                                    </p>
                                    <div class="row mt-2" runat="server" id="SinFirmar">
                                        <div class="col-lg-12" style="text-align: end">
                                            <div id="captureSignature" class="shadow"></div>
                                            <br />
                                            <button id="btnFirmar" runat="server" type="button" class="btn btn-secondary shadow" visible="false" onserverclick="InsertarLinea">Firmar</button>
                                            <input id="signatureJSON" type="hidden" name="signature" class="signature" value="" runat="server"><br />
                                            <label class="mt-2">Entregado por: &nbsp</label><asp:Label ID="LabelEntrega" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-2" runat="server" id="Firmado" visible="false">
                                        <div class="col-lg-12" style="text-align: end">
                                            <asp:Image ID="IMGFirma" runat="server" />
                                            <br />
                                            <label class="mt-2">Entregado por: &nbsp</label><asp:Label ID="lblEntregadoPor" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                <div class="container">
                    <asp:Label ID="numOperarioP2" Font-Bold="true" Font-Size="large" runat="server"></asp:Label>
                    <asp:GridView ID="GridView2" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                        Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                        OnRowUpdating="GridView_RowUpdating" OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
                        OnRowCommand="GridView_RowCommand" OnRowDeleting="GridView_RowDeleting"
                        EmptyDataText="There are no data records to display.">
                        <HeaderStyle CssClass="card-header" BackColor="OrangeRed" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="#e6e6e6" />
                        <EditRowStyle BackColor="#ffffcc" />
                        
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnRedirect" runat="server" CommandName="Redirect" class="btn btn-outline-dark" CommandArgument='<%#Eval("LINEA") %>'><i class="bi bi-eye-fill"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operario" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%" Visible="false">
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

        <%--OFFCANVAS DE FILTROS --%>
        <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasExample" aria-labelledby="offcanvasExampleLabel">
            <div class="offcanvas-header">
                <h5 class="offcanvas-title" id="offcanvasExampleLabel">Filtros</h5>
                <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <div>
                    <div class="input-group">
                        <div class="form-group">
                            <label for="sel1">
                                Selecciona un filtro</label>
                            <select class="form-control" runat="server" id="cbFiltro">
                                <option>Activas</option>
                                <option>Todas</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <button id="btnCargarFiltro" runat="server" onserverclick="Cargar_filtro" type="button" class="btn btn-outline-dark me-md-3">
                                Cargar
                            </button>
                        </div>

                    </div>
                </div>
                <br />
                <div class="form-check form-switch">
                    <input class="form-check-input" type="checkbox" id="flexSwitchCheckChecked" checked>
                    <label class="form-check-label" for="flexSwitchCheckChecked">Referencias activas</label>
                </div>

            </div>
        </div>
        <!-- Button trigger modal -->


        <!-- Modal -->
        <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="staticBackdropLabel" runat="server">SIN OPERARIO</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <h6>Material entregado:</h6>
                                <asp:DropDownList ID="DropEPI" class="form-select" runat="server"></asp:DropDownList>
                            </div>

                        </div>
                        <div class="row mt-2">
                            <div class="col-lg-5">
                                <h6>Marca:</h6>
                                <input type="text" class="form-control" placeholder="Marca/Norma">
                            </div>
                            <div class="col-lg-4">
                                <h6>Talla:</h6>
                                <input type="text" class="form-control" placeholder="S/M/L/XL">
                            </div>
                            <div class="col-lg-3">
                                <h6>Cantidad:</h6>
                                <input type="text" class="form-control" placeholder="0">
                            </div>

                        </div>
                        <div class="row mt-2">
                            <div class="col-lg-12">
                                <h6>Entregado por:</h6>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary">Validar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
