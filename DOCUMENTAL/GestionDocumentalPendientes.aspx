<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GestionDocumentalPendientes.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.GestionDocumentalPendientes" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Documentos pendientes</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Documentos pendientes             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Gestión Documental
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="FichaReferencia.aspx">Documentación de referencia</a></li>
                <li><a class="dropdown-item" href="GestionDocumentalPendientes.aspx">Produciendo sin digitalizar</a></li>
                <li><a class="dropdown-item" href="AccesoDocumentalMaquina.aspx">Tablero de máquinas</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <style>
        th {
            background: #0d6efd !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
        }

        th, td {
            padding: 0.25rem;
        }
    </style>

    <%--Calendario--%>
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
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">


        <div class="container-fluid mt-2">
            <div class="col-lg-12">
                <ul class="nav nav-pills justify-content-end invisible" id="pills-tab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="BTN_ESTADO_PRODUCTOS" runat="server" data-bs-toggle="pill" data-bs-target="#pills_estados" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-file-earmark-binary me-2"></i>Estado de productos</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="BTN_ULTIMAS_REVISIONES" runat="server" data-bs-toggle="pill" data-bs-target="#pills_historico" type="button" role="tab" aria-controls="pills-home" aria-selected="true"><i class="bi bi-book me-2"></i>Últimas revisiones GP12</button>
                    </li>
                </ul>
                <div class="tab-content" id="pills-tabContent">
                    <div class="tab-pane fade show active" id="pills_historico" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <div class="row">
                            <div class="col-lg-4">
                                <h5>Produciendo con documentación pendiente</h5>
                                <asp:GridView ID="dgv_PendientesProduciendo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                                    
                                    EmptyDataText="There are no data records to display.">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Maquina">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Maquina") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Referencia">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Documento pendiente">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Documento") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jefe del proyecto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlanControl" runat="server" Text='<%#Eval("Nombre") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-lg-8">
                                <h5>Notas de planta</h5>
                                <asp:GridView ID="dgv_Notas_Operarios" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                                    OnRowCommand="GridViewCommandEventHandler"
                                    EmptyDataText="There are no data records to display.">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="button2" CommandName="ActualizaDOC" CommandArgument='<%#Eval("Id") + "¬" + Eval("Estado")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-repeat"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Operario">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" runat="server" Font-Bold="true" Font-Italic="true" Text='<%#Eval("EstadoDOC") %>' /><br />
                                                <asp:Label ID="lblFecha" runat="server" Text='<%#Eval("Fecha", "{0:dd/MM/yyyy}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Operario">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOP" runat="server" Text='<%#Eval("NUMOperario") %>' /><br />
                                                <asp:Label ID="lblOPNOM" Font-Italic="true" runat="server" Text='<%#Eval("Operario") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Referencia">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReferencia" Font-Bold="true" runat="server" Text='<%#Eval("Referencia") %>' />
                                                <asp:Label ID="lblMolde" runat="server" Font-Italic="true" Text='<%#"(" + Eval("Molde") + ")" %>' /><br />
                                                <asp:Label ID="lblReferenciaDESC" Font-Italic="true" runat="server" Text='<%#Eval("Descripcion") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feedback">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("FeedbackDocumento") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                    <div class="tab-pane fade" id="pills_estados" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                    </div>
                </div>
            </div>
        </div>
        <div class="row invisible">
            <div class="col-lg-6"></div>
            <div class="col-lg-1 text-center">
                <label class="ms-1" style="font-size: larger; font-weight: bold">Revisadas</label><br />
                <asp:Label ID="lblRevisadas" Font-Size="X-Large" runat="server">0</asp:Label>
            </div>
            <div class="col-lg-1 text-center">
                <label style="font-size: larger; font-weight: bold">Buenas</label><br />
                <asp:Label ID="lblBuenas" Font-Size="X-Large" runat="server">0</asp:Label><br />
                <asp:Label ID="lblBuenasPORC" Font-Size="Smaller" runat="server" Font-Italic="true">0% bueno.</asp:Label>
            </div>
            <div class="col-lg-1 text-center">
                <label class="ms-1" style="font-size: larger; font-weight: bold">Retrabajadas</label><br />
                <asp:Label ID="lblRetrabajadas" Font-Size="X-Large" runat="server">0</asp:Label><br />
                <asp:Label ID="lblRetrabajadasPORC" Font-Size="Smaller" runat="server" Font-Italic="true" CssClass="ms-3">0% retrabajado.</asp:Label>
            </div>
            <div class="col-lg-1 text-center">
                <label class="ms-1" style="font-size: larger; font-weight: bold">Defectuosas</label><br />
                <asp:Label ID="lblDefectuosas" Font-Size="X-Large" runat="server">0</asp:Label><br />
                <asp:Label ID="lblDefectuosasPORC" Font-Size="Smaller" runat="server" Font-Italic="true" CssClass="ms-3">0% defectuoso.</asp:Label>

            </div>
            <div class="col-lg-1"></div>
            <div class="col-lg-1 mt-1">
                <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-3 me-md-4 mb-md-1">
                    <button id="AUXCIERRAMODALFIRMA" runat="server" type="button" class="btn-close" data-bs-target="#ModalFirmaOperario" data-bs-toggle="modal" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                    <button id="AUXMODALACCIONFIRMA" runat="server" type="button" class="btn btn-primary invisible " data-bs-toggle="modal" data-bs-target="#ModalFirmaOperario" style="font-size: larger"></button>
                    <button id="AUXCIERRAMODAL" runat="server" type="button" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                    <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>
                    <button id="btnoffcanvas" runat="server" type="button" class="btn btn-primary ms-md-0 bi bi-funnel shadow-sm" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger"></button>
                </div>
            </div>
        </div>
</asp:Content>
