<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="MontajesHistorico.aspx.cs" Inherits="ThermoWeb.PRODUCCION.MontajesHistorico" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Histórico del muro de calidad</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Histórico GP12             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Acciones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="MontajesExternos.aspx">Iniciar un montaje</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
     <style>
     th {
        background: #0d6efd!important;
        color:white!important;
        position: sticky!important;
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
        <div class="row">
            <div class="col-lg-11"></div>

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
        
        <div class="container-fluid mt-2">
            <div class="col-lg-12">
                 <asp:GridView ID="dgv_Montaje_Historico" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                          
                                
                                <asp:TemplateField ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6" HeaderText="Operario" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblOpINT" runat="server" Font-Size="Larger" Text='<%#Eval("OperarioMontaje") %>' /><br />
                                        <asp:Label ID="lblPiezasrev" Font-Size="small" runat="server" Text='<%#Eval("Proveedor" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6" HeaderText="Horas">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHoras" runat="server" Font-Size="Larger" Text='<%#Eval("HorasMontaje", "{0:0.00 h}") %>' /><br />
                                        <asp:Label ID="lblPiezasrev" Font-Size="small" runat="server" Text='<%#"A " + Eval("PZHORA" ) + " piezas/hora" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lote" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLote" Font-Size="Larger" Font-Bold="true" runat="server" Text='<%#Eval("Nlote") %>' /><br />
                                        <asp:Label ID="lblFecharevision" Font-Size="small" runat="server" Font-Italic="true" Text='<%#"(" + Eval("FechaInicio", "{0:dd/MM/yyyy}") + ")" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("Referencia") %>' /><br />
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Nombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fabricadas" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuenas" runat="server" Font-Size="X-Large" Text='<%#Eval("PiezasMontadas") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Observaciones" ItemStyle-Width="41%">
                                    <ItemTemplate>                     
                                        <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("Notas") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
            </div>
        </div>
    
    

        <%--OFFCANVAS DE FILTROS --%>
        <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasExample" aria-labelledby="offcanvasExampleLabel">
            <div class="offcanvas-header">
                <h5 class="offcanvas-title" id="offcanvasExampleLabel">Filtros</h5>
                <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <div class="row">              
                    <br />
                    <h6>Desde:</h6>
                    <input type="text" id="InputFechaDesde" class="form-control ms-2 mb-3 Add-text" style="width: 95%" autocomplete="off" runat="server">
                    <br />
                    <h6>Hasta:</h6>
                    <input type="text" id="InputFechaHasta" class="form-control ms-2 mb-3 Add-text" style="width: 95%" autocomplete="off" runat="server">
                    <br />
                    <h6>Referencia:</h6>
                    <div class="input-group mb-3">
                        <input class="form-control" list="DatalistReferencias" id="selectReferencia" runat="server" placeholder="Escribe un referencia...">
                        <datalist id="DatalistReferencias" runat="server">
                        </datalist>
                    </div>
                    <br />
                    <div class="input-group mb-3">
                        <button id="Button2" runat="server" onserverclick="Rellenar_grid" type="button" class="btn btn-secondary" style="width: 100%; text-align: left">
                            <i class="bi bi-search me-2"></i>Filtrar</button>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
