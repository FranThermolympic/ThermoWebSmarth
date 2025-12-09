<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GP12HistoricoOperario_.aspx.cs" Inherits="ThermoWeb.GP12.GP12HistoricoOperario_" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Histórico del muro de calidad</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Histórico de referencia             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Revisiones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../GP12/GP12.aspx">Iniciar una revisión</a></li>
                <li><a class="dropdown-item" href="../GP12/PrevisionGP12.aspx">Consultar planificación de cargas</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación de referencia</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../GP12/GP12Historico.aspx">Consultar últimas revisiones</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12HistoricoCliente.aspx">Consultar histórico de cliente</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12HistoricoReferencia.aspx">Consultar histórico de producto</a></li>
                <li><a class="dropdown-item" href="../KPI/KPI_GP12.aspx">Consultar indicadores</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../KPI/KPIIndice.aspx">Ver cuadros de mando</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Gestiones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../GP12/GP12ReferenciasEstado.aspx">Gestionar estado de referencias</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12RegistroComunicaciones.aspx">Registrar comunicaciones</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../PDCA/PDCA.aspx">Abrir plan de acción</a></li>
                <li><a class="dropdown-item" href="../CALIDAD/Alertas_Calidad.aspx">Abrir no conformidad</a></li>

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
    <script type="text/javascript">
        function ShowPopup1() {
            document.getElementById("AUXMODALACCION").click();
        }
        function ClosePopup1() {

        }
        function ShowPopupFirma() {
            document.getElementById("AUXMODALACCIONFIRMA").click();
        }
        function ClosePopupFirma() {
            document.getElementById("AUXCIERRAMODALFIRMA").click();
        }

    </script>

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
            <div class="col-lg-4 mt-4">
                <div class="row">
                    <div class="col-lg-1"></div>
                     <div class="col-lg-2 px-0"><asp:Image ID="IMGliente" runat="server" Width="100%" ImageUrl='http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' /></div>
                    <div class="col-lg-8 px-0 ms-2">
                        <asp:Label ID="Label1" class="ms-2" Font-Size="XX-Large" Font-Bold="true" Font-Italic="true" runat="server"  Style="text-align: left" Text="Operario:" />
                        <asp:Label ID="tbNumoperario" class="ms-2" Font-Size="XX-Large" Font-Bold="true" Font-Italic="true" runat="server"  Style="text-align: left" Text="456" /><br />
                        <asp:label id="tbNombreOperario" class="ms-2" runat="server" Font-Size="Large" Font-Italic="true" Font-Bold="true" style="text-align: left" Text="Eduardo Cabezabuque" />
                       
                    </div>
                </div>
            </div>
            <div class="col-lg-8 mt-4 ">
                <div class="row ">
                    <div class="col-lg-3"></div>
                    <div class="col-lg-1 px-0">
                        <asp:TextBox ID="CabeceraRevisadas" runat="server" Width="100%" ForeColor="White" Font-Bold="true" Style="text-align: center; background-color: #337ab7; border-color: orange; border: thin" Enabled="false">Revisadas</asp:TextBox><br />
                        <asp:TextBox ID="tbPiezasRevisadas" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>                      
                    </div>
                    <div class="col-lg-1 px-0">
                        <asp:TextBox ID="CabeceraBuenas" runat="server" Width="100%" ForeColor="White" Font-Bold="true" Style="text-align: center; background-color: #337ab7; border-color: orange; border: thin" Enabled="false">Buenas</asp:TextBox><br />
                        <asp:TextBox ID="tbRevisadasOK" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                       
                    </div>
                    <div class="col-lg-1 px-0">
                        <asp:TextBox ID="CabeceraMalas" runat="server" Width="100%" ForeColor="White" Font-Bold="true" Style="text-align: center; background-color: #337ab7; border-color: orange; border: thin" Enabled="false">Malas</asp:TextBox><br />
                        <asp:TextBox ID="tbRevisadasNOK" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                      
                    </div>
                    <div class="col-lg-1 px-0">
                        <asp:TextBox ID="CabeceraHoras" runat="server" Width="100%" ForeColor="White" Font-Bold="true" Style="text-align: center; background-color: #337ab7; border-color: orange; border: thin" Enabled="false">Recuperadas</asp:TextBox><br />
                        <asp:TextBox ID="tbRecuperadas" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                      
                    </div>
                    <div class="col-lg-2 px-0">
                        <asp:TextBox ID="CabeceraHorasRevisión" runat="server" Width="100%" ForeColor="White" Font-Bold="true" Style="text-align: center; background-color: #337ab7; border-color: orange; border: thin" Enabled="false">Horas en revisión</asp:TextBox><br />
                        <asp:TextBox ID="tbHorasRevision" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                        
                    </div>
                    <div class="col-lg-2 px-0">
                        <asp:TextBox ID="CabeceracostesInspeccion" runat="server" Width="100%" ForeColor="White" Font-Bold="true" Style="text-align: center; background-color: #337ab7; border-color: orange; border: thin" Enabled="false">Costes de inspección (€)</asp:TextBox><br />
                        <asp:TextBox ID="tbCostesInspeccion" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                        
                    </div>
                   
                    <div class="col-lg-1">
                        <div class="d-grid gap-2 d-md-flex justify-content-md-end me-md-3 mb-md-1">
                            <button id="AUXCIERRAMODALFIRMA" runat="server" type="button" class="btn-close" data-bs-target="#ModalFirmaOperario" data-bs-toggle="modal" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                            <button id="AUXMODALACCIONFIRMA" runat="server" type="button" class="btn btn-primary invisible " data-bs-toggle="modal" data-bs-target="#ModalFirmaOperario" style="font-size: larger"></button>
                            <button id="AUXCIERRAMODAL" runat="server" type="button" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                            <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>

                            <button id="btnoffcanvas" runat="server" type="button" class="btn btn-primary ms-md-0 bi bi-funnel shadow-sm" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger"></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <asp:GridView ID="dgv_GP12_Historico" runat="server" AllowSorting="True"
                    Width="98.5%" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0 mt-4"
                    OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="GridView1_DataBound"
                    EmptyDataText="No hay revisiones para mostrar.">
                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                    <RowStyle BackColor="White" />
                    <AlternatingRowStyle BackColor="#eeeeee" />
                    <Columns>
                        <asp:TemplateField ItemStyle-BackColor="#e6e6e6" Visible="false">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#e6e6e6">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="LinkButton1" CommandName="CargaDetalle" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                            </ItemTemplate>
                              <FooterTemplate>
                                <asp:Label ID="placeholdertexto2" runat="server" Text='Tiempo (Horas):' Font-Bold />

                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID" Visible="false" ItemStyle-BackColor="#e6e6e6">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha" ItemStyle-BackColor="#e6e6e6">
                            <ItemTemplate>
                                <asp:Label ID="lblFecharevision" runat="server" Font-Bold="true" Text='<%#Eval("FechaInicio", "{0:dd/MM/yyyy}") %>' />
                                <asp:Label ID="lblHoras" runat="server" Font-Italic="true" Text='<%#"(" + Eval("HORAS") + ")" %>' />
                            </ItemTemplate>
                          
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Lote" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblLote" runat="server" Text='<%#Eval("Nlote") %>' Font-Size="large" Font-Bold="true" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="placeholdertexto" runat="server" Text='Resultados:' Font-Bold />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Buenas" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblBuenas" runat="server" Font-Size="Large" Text='<%#Eval("PiezasOK") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Retrab." ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRetrabajadas" runat="server" Text='<%#Eval("Retrabajadas") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Malas" ItemStyle-Width="80px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblMalas" runat="server" Text='<%#Eval("PiezasNOK") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Coste Chatarra" ItemStyle-Width="90px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                            <ItemTemplate>
                                <asp:Label ID="lblCosteChatarra" runat="server" Text='<%#Eval("CosteScrapRevision","{0:c}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Coste Operario" ItemStyle-Width="90px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                            <ItemTemplate>
                                <asp:Label ID="lblCosteTiempo" runat="server" Text='<%#Eval("CostePiezaRevision","{0:c}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total" ItemStyle-Width="90px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                            <ItemTemplate>
                                <asp:Label ID="lblCosteTotal" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("CosteRevision","{0:c}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Razón" ItemStyle-Width="95px">
                            <ItemTemplate>
                                <asp:Label ID="lblRazonRevision" runat="server" Text='<%#Eval("Razon") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Observa" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblInci" runat="server" Text='<%#Eval("Incidencias") %>' /><br />
                                <asp:Label ID="lblObserva" runat="server" Text='<%#Eval("Notas") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Falta material" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto1" runat="server" Text='<%#Eval("Def1") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ráfagas" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto2" runat="server" Text='<%#Eval("Def2") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Roturas" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto3" runat="server" Text='<%#Eval("Def3") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Montaje NOK" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto4" runat="server" Text='<%#Eval("Def4") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deform." ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto5" runat="server" Text='<%#Eval("Def5") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Etiqueta NOK" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto6" runat="server" Text='<%#Eval("Def6") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Burbujas" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto7" runat="server" Text='<%#Eval("Def7") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Arrastre mat." ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto8" runat="server" Text='<%#Eval("Def8") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Raya / Expulsor" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto9" runat="server" Text='<%#Eval("Def9") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quema." ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto10" runat="server" Text='<%#Eval("Def10") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brillos" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto11" runat="server" Text='<%#Eval("Def11") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mat. contam." ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto12" runat="server" Text='<%#Eval("Def12") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rechupes" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto13" runat="server" Text='<%#Eval("Def13") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Color NOK" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto14" runat="server" Text='<%#Eval("Def14") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manchas" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto15" runat="server" Text='<%#Eval("Def15") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rebabas" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto16" runat="server" Text='<%#Eval("Def16") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sólo plástico" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto17" runat="server" Text='<%#Eval("Def17") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sólo goma" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto18" runat="server" Text='<%#Eval("Def18") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Otros" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto19" runat="server" Text='<%#Eval("Def19") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Electro-válvula" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto20" runat="server" Text='<%#Eval("Def20") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grapa: Posición" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto21" runat="server" Text='<%#Eval("Def21") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grapa: Altura" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto22" runat="server" Text='<%#Eval("Def22") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tubo: Deformado" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto23" runat="server" Text='<%#Eval("Def23") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tubo: Mal puesto" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto24" runat="server" Text='<%#Eval("Def24") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mal clipado" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto25" runat="server" Text='<%#Eval("Def25") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suciedad" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto26" runat="server" Text='<%#Eval("Def26") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Punzon NOK" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto27" runat="server" Text='<%#Eval("Def27") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Láser ilegible" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto28" runat="server" Text='<%#Eval("Def28") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Defecto29" ItemStyle-Width="120px" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto29" runat="server" Text='<%#Eval("Def29") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Defecto30" ItemStyle-Width="120px" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDefecto30" runat="server" Text='<%#Eval("Def30") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFAKE" runat="server" Text='<%#Eval("FakeMode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>

    </div>
    <!-- Modal bootstrap-->
    <div class="modal fade" id="ModalEditaAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="staticBackdropLabel" runat="server">Detalles de revisión</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    <asp:TextBox ID="IDINSPECCION" runat="server" Style="text-align: center" Width="100%" Enabled="false" Visible="false"></asp:TextBox>
                </div>
                <div class="modal-body" runat="server">
                    <div>
                        <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                            <div class="d-flex align-items-start">
                                <div class="nav flex-column nav-pills me-2" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                                    <button id="TABACCION" class="nav-link active" data-bs-toggle="pill" data-bs-target="#v-pills-home" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true"><i class="bi bi-kanban"></i></button>
                                </div>
                                <div class="tab-content" id="v-pills-tabContent">
                                    <div class="tab-pane fade show active" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>Referencia</h5>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <label id="detalleReferencia" runat="server" class="form-label ms-4" style="text-align: left" enabled="false"></label>
                                                    <button id="btnHistorico" type="button" class="btn btn-outline-primary btn-sm" style="border: none" runat="server" onserverclick="CargaHistorico"><i class="bi bi-box-arrow-up-right"></i></button>
                                                </div>
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-eye me-2"></i>Revisado por</h5>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 mt-2">

                                                    <label id="TbOperarioRevision" runat="server" class="form-label ms-4" style="text-align: left" enabled="false"></label>

                                                    <label id="CosteHoras" runat="server" class="form-label ms-1" style="text-align: center"></label>
                                                    <br />
                                                </div>
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-coin me-2"></i>Costes</h5>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">

                                                    <b>
                                                        <label class="form-label ms-4">Coste Chatarra:</label></b>
                                                    <label id="CosteChatarra" runat="server" class="form-label ms-1" style="text-align: center"></label>
                                                    <br />
                                                    <b>
                                                        <label class="form-label ms-4">Coste Revisión:</label></b>
                                                    <label id="CosteOperario" runat="server" class="form-label ms-1" style="text-align: center"></label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <b>
                                                        <label class="form-label ms-4">Coste Total:</label></b>
                                                    <label id="CosteTotal" runat="server" class="form-label ms-1" style="text-align: center"></label>
                                                </div>
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-lg-12">
                                                        <h5 class="mt-1"><i class="bi bi-person me-2"></i>Operarios vinculados</h5>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 mt-2">
                                                    <div id="BOXOP1" runat="server">
                                                        <asp:HiddenField ID="NOperario1" runat="server" />
                                                        <asp:HiddenField ID="NFirmaOperario1" runat="server" />
                                                        <asp:HiddenField ID="NInformadoPor1" runat="server" />
                                                        <button id="InformarOp1" type="button" class="btn btn-primary btn-sm ms-4" style="width: 75px" runat="server" onserverclick="CargarInformador"><i class="bi bi-pencil-square"></i></button>
                                                        <label id="Operario1" runat="server" class="form-label ms-1" style="text-align: center" enabled="false"></label>
                                                        <b>
                                                            <label id="FechaINFO1" runat="server" class="form-label ms-1"></label>
                                                        </b>
                                                        <button id="BtnDetalleOP1" type="button" class="btn btn-outline-primary btn-sm" style="border: none" runat="server" onserverclick="CargaHistoricoOperario"><i class="bi bi-box-arrow-up-right"></i></button>
                                                        <br />
                                                        <label id="lblFeedbackOp1" runat="server" class="form-label ms-5" style="text-align: center"></label>

                                                    </div>
                                                    <div id="BOXOP2" runat="server">
                                                        <asp:HiddenField ID="NOperario2" runat="server" />
                                                        <asp:HiddenField ID="NFirmaOperario2" runat="server" />
                                                        <asp:HiddenField ID="NInformadoPor2" runat="server" />
                                                        <button id="InformarOp2" type="button" class="btn btn-primary btn-sm ms-4" style="width: 75px" runat="server" onserverclick="CargarInformador"><i class="bi bi-pencil-square"></i></button>
                                                        <label id="Operario2" runat="server" class="form-label ms-1" style="text-align: center" enabled="false"></label>
                                                        <b>
                                                            <label id="FechaINFO2" runat="server" class="form-label ms-1"></label>
                                                        </b>
                                                        <button id="BtnDetalleOP2" type="button" class="btn btn-outline-primary btn-sm" style="border: none" runat="server" onserverclick="CargaHistoricoOperario"><i class="bi bi-box-arrow-up-right"></i></button>
                                                        <br />
                                                        <label id="lblFeedbackOp2" runat="server" class="form-label ms-5" style="text-align: center" enabled="false"></label>

                                                    </div>
                                                    <div id="BOXOP3" runat="server">
                                                        <asp:HiddenField ID="NOperario3" runat="server" />
                                                        <asp:HiddenField ID="NFirmaOperario3" runat="server" />
                                                        <asp:HiddenField ID="NInformadoPor3" runat="server" />
                                                        <button id="InformarOp3" type="button" class="btn btn-primary btn-sm ms-4" style="width: 75px" runat="server" onserverclick="CargarInformador"><i class="bi bi-pencil-square"></i></button>
                                                        <label id="Operario3" runat="server" class="form-label ms-1" style="text-align: center" enabled="false"></label>
                                                        <b>
                                                            <label id="FechaINFO3" runat="server" class="form-label ms-1"></label>
                                                        </b>
                                                        <button id="BtnDetalleOP3" type="button" class="btn btn-outline-primary btn-sm" style="border: none" runat="server" onserverclick="CargaHistoricoOperario"><i class="bi bi-box-arrow-up-right"></i></button>
                                                        <br />
                                                        <label id="lblFeedbackOp3" runat="server" class="form-label ms-5" style="text-align: center" enabled="false"></label>
                                                    </div>
                                                    <div id="BOXOP4" runat="server">
                                                        <asp:HiddenField ID="NOperario4" runat="server" />
                                                        <asp:HiddenField ID="NFirmaOperario4" runat="server" />
                                                        <asp:HiddenField ID="NInformadoPor4" runat="server" />
                                                        <button id="InformarOp4" type="button" class="btn btn-primary btn-sm ms-4" style="width: 75px" runat="server" onserverclick="CargarInformador"><i class="bi bi-pencil-square"></i></button>
                                                        <label id="Operario4" runat="server" class="form-label ms-1" style="text-align: center"></label>
                                                        <b>
                                                            <label id="FechaINFO4" runat="server" class="form-label ms-1"></label>
                                                        </b>
                                                        <button id="BtnDetalleOP4" type="button" class="btn btn-outline-primary btn-sm" style="border: none" runat="server" onserverclick="CargaHistoricoOperario"><i class="bi bi-box-arrow-up-right"></i></button>
                                                        <br />
                                                        <label id="lblFeedbackOp4" runat="server" class="form-label ms-5" style="text-align: center" enabled="false"></label>
                                                    </div>

                                                </div>
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-lg-6">
                                                        <h5 class="mt-1"><i class="bi bi-file-bar-graph me-2"></i>Resultados</h5>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <h5 class="mt-1"><i class="bi bi-trash me-2"></i>Defectuosas</h5>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">

                                                    <b>
                                                        <label class="form-label ms-4">Revisadas:</label></b><label id="TbRevisadas" runat="server" class="form-label ms-1" style="text-align: center"></label>
                                                    <br />
                                                    <b>
                                                        <label class="form-label ms-4">Buenas:</label></b><label id="TbBuenas" runat="server" class="form-label ms-1" style="text-align: center"></label>
                                                    <br />
                                                    <b>
                                                        <label class="form-label ms-4">Retrabajadas:</label></b><label id="TbRetrabajadas" runat="server" class="form-label ms-1" style="text-align: center"></label>
                                                    <br />
                                                    <b>
                                                        <label class="form-label ms-4">Malas:</label></b><label id="TbMalas" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label>

                                                </div>
                                                <div class="col-lg-6">

                                                    <div id="THDefecto1" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Falta llenado:</label></b><label id="TDDefecto1" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto2" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Ráfagas:</label></b><label id="TDDefecto2" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto3" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Roturas:</label></b><label id="TDDefecto3" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto4" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Montaje NOK:</label></b><label id="TDDefecto4" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto5" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Deformaciones:</label></b><label id="TDDefecto5" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto6" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Etiqueta NOK:</label></b><label id="TDDefecto6" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto7" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Burbujas:</label></b><label id="TDDefecto7" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto8" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Arrastre material:</label></b><label id="TDDefecto8" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto9" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Rayas / Marcas expulsor:</label></b><label id="TDDefecto9" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto10" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Quemazos:</label></b><label id="TDDefecto10" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto11" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Brillos:</label></b><label id="TDDefecto11" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto12" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Mat. contaminado:</label></b><label id="TDDefecto12" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto13" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Rechupes:</label></b><label id="TDDefecto13" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto14" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Color NOK:</label></b><label id="TDDefecto14" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto15" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Manchas:</label></b><label id="TDDefecto15" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto16" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Rebabas:</label></b><label id="TDDefecto16" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto17" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Sólo plástico:</label></b><label id="TDDefecto17" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto18" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Sólo goma:</label></b><label id="TDDefecto18" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto19" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Otros:</label></b><label id="TDDefecto19" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto20" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Electroválvula:</label></b><label id="TDDefecto20" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto21" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Grapa: Posición:</label></b><label id="TDDefecto21" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto22" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Grapa: Altura:</label></b><label id="TDDefecto22" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto23" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Tubo: Deformado:</label></b><label id="TDDefecto23" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto24" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Tubo: Mal puesto:</label></b><label id="TDDefecto24" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto25" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Mal clipado:</label></b><label id="TDDefecto25" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto26" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Suciedad:</label></b><label id="TDDefecto26" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto27" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Punzonado NOK:</label></b><label id="TDDefecto27" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                    <div id="THDefecto28" runat="server" visible="false">
                                                        <b>
                                                            <label class="form-label ms-4">Láser ilegible:</label></b><label id="TDDefecto28" runat="server" runat="server" class="form-label ms-1" style="text-align: center"></label><br />
                                                    </div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <h6><i class="bi bi-cone-striped ms-3"></i>Incidencias:</h6>
                                                    <input id="TDNotas" runat="server" class="form-control ms-4" type="text" disabled="disabled">
                                                </div>
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-lg-12">
                                                        <h5 class="mt-1"><i class="bi bi-camera me-2"></i>Imágenes</h5>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <a id="linkimagen1" runat="server" href="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg">
                                                        <img id="IMGlinkimagen1" runat="server" src="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" class="rounded img-thumbnail" alt="...">
                                                    </a>
                                                </div>
                                                <div class="col-lg-4">
                                                    <a id="linkimagen2" runat="server" href="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg">
                                                        <img id="IMGlinkimagen2" runat="server" src="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" class="rounded img-thumbnail" alt="...">
                                                    </a>
                                                </div>
                                                <div class="col-lg-4">
                                                    <a id="linkimagen3" runat="server" href="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg">
                                                        <img id="IMGlinkimagen3" runat="server" src="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" class="rounded img-thumbnail" alt="...">
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer" style="background: #e6e6e6">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalFirmaOperario" data-bs-backdrop="static" aria-hidden="true" aria-labelledby="lblModalFirmaOperario" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="exampleModalToggleLabel2"><i class="bi bi-person-video3 me-2"></i>Formación a operario</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                            <div class="mt-2">
                                <asp:TextBox ID="IDREVISION" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="NUMOP" runat="server" Visible="false"></asp:TextBox>
                                <input id="signatureJSON" type="hidden" name="signature" class="signature" value="" runat="server">
                                <p class="ms-2">
                                    <b>
                                        <label id="LblInformadoOP" runat="server">---</label></b> confirma que ha recibido formación acerca de los defectos localizados en el muro de calidad para los modos de fallo detectados.
                                </p>
                                <h6><i class="bi bi-chat-dots ms-3 me-1"></i>Feedback de operario:</h6>
                                <input class="form-control ms-3" type="text" id="FeedbackOPERARIOS" runat="server" placeholder="Comentarios operarios" aria-label="default input example">
                            </div>
                        </div>
                        <div class="row mt-2" runat="server" id="SinFirmarOp">
                            <div class="col-lg-12" style="text-align: end">
                                <label class="mt-2" style="font-weight: bold">Informa: &nbsp</label><asp:Label ID="LabelEntrega" runat="server"></asp:Label><br />
                                <asp:DropDownList ID="Informador" runat="server" Style="text-align: center" CssClass="form-select ms-2"></asp:DropDownList><br />

                                <div id="captureSignature" class="shadow"></div>
                                <br />
                                <button id="btnFirmarOp" runat="server" type="button" class="btn btn-sm btn-secondary shadow" onserverclick="ActualizaInformador">Firmar</button>

                            </div>
                        </div>
                        <div class="row mt-2" runat="server" id="FirmadoOp">
                            <div class="col-lg-12" style="text-align: end">
                                <asp:Image ID="IMGFirma" runat="server" />
                                <br />
                                <asp:Label ID="lblInformadoPor" Font-Bold="true" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

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
            <div class="row">
                <br />
                <h6>Estado:</h6>
                <div class="form-check form-switch ms-3 mb-3">
                    <input class="form-check-input" type="checkbox" runat="server" id="SwitchActivas">
                    <label class="form-check-label" for="SwitchActivas">Mostrar sólo defetuosas</label>
                </div>
                <br />
                <h6>Periodo de revisión:</h6>
                <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm ms-2 mb-3" Style="width: 95%" Font-Size="Large" OnSelectedIndexChanged="Rellenar_grid">
                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                    <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                </asp:DropDownList>
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
                <h6>Molde:</h6>
                <div class="input-group mb-3">
                    <input class="form-control" list="DatalistMoldes" id="selectMolde" runat="server" placeholder="Escribe un molde...">
                    <datalist id="DatalistMoldes" runat="server">
                    </datalist>
                </div>
                <br />
                <h6>Lote:</h6>
                <div class="input-group mb-3">
                    <input class="form-control" list="DatalistLotes" id="selectLote" runat="server" placeholder="Escribe un lote...">
                    <datalist id="DatalistLotes" runat="server">
                    </datalist>
                </div>
                <br />

                <h6>Estado de revisión:</h6>
                <div class="input-group mb-3">
                    <asp:DropDownList ID="lista_estado_revision" runat="server" class="form-select">
                    </asp:DropDownList>
                </div>
                <br />

                <div class="input-group mb-3">
                    <button id="Button1" runat="server" onserverclick="Rellenar_grid" type="button" class="btn btn-secondary" style="width: 100%; text-align: left">
                        <i class="bi bi-search me-2"></i>Filtrar</button>
                </div>
            </div>
        </div>
    </div>
    <%-- 
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color: Blue; color: White; text-align: center; font-size: x-large">
                    Detalles
                </div>
                <div class="modal-body" style="padding: 40px 50px;">

                    <div class="form-group">
                        <label for="revisor"><span class="glyphicon glyphicon-info-sign"></span>Referencia</label><br />
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="detalleReferencia" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="detalleReferenciaNombre" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="form-group">
                        <label for="revisor"><span class="glyphicon glyphicon-eye-open"></span>Revisión</label><br />
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="TbOperarioRevision" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbEmpresaRevision" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="form-group">
                        <label for="operario"><span class="glyphicon glyphicon-user"></span>Operarios</label><br />
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="Operario1" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="Operario2" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="Operario3" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="Operario4" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="form-group">
                        <label for="costes"><span class="glyphicon glyphicon-euro"></span>Costes</label><br />
                        <table width="100%">
                            <tr>
                                <th>
                                    <asp:TextBox ID="ThCosteHoras" runat="server" Style="text-align: center" Enabled="false" Width="100%">Horas de revisión</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="ThCosteChatarra" runat="server" Style="text-align: center" Enabled="false" Width="100%">Coste Chatarra</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="ThCosteOperario" runat="server" Style="text-align: center" Enabled="false" Width="100%">Coste Revisión</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="ThCosteTotal" runat="server" Style="text-align: center" Enabled="false" Width="100%">Coste Total</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="CosteHoras" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="CosteChatarra" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="CosteOperario" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="CosteTotal" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="form-group">
                        <label for="defectos"><span class="glyphicon glyphicon-trash"></span>Defectos</label><br />
                        <table width="100%">
                            <tr>
                                <th>
                                    <asp:TextBox ID="ThRevisadas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Revisadas</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="ThBuenas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Buenas</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="ThRetrabajadas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Retrabajadas</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="ThMalas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Malas</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TbRevisadas" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbBuenas" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbRetrabajadas" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbMalas" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:TextBox ID="THDefecto1" runat="server" Style="text-align: center" Enabled="false" Width="100%">Falta llenado</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto2" runat="server" Style="text-align: center" Enabled="false" Width="100%">Ráfagas</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto3" runat="server" Style="text-align: center" Enabled="false" Width="100%">Roturas</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto4" runat="server" Style="text-align: center" Enabled="false" Width="100%">Montaje NOK</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TDDefecto1" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto2" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto3" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto4" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:TextBox ID="THDefecto5" runat="server" Style="text-align: center" Enabled="false" Width="100%">Deformaciones</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto6" runat="server" Style="text-align: center" Enabled="false" Width="100%">Etiqueta NOK</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto7" runat="server" Style="text-align: center" Enabled="false" Width="100%">Burbujas</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto8" runat="server" Style="text-align: center" Enabled="false" Width="100%">Chapa visible</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TDDefecto5" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto6" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto7" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto8" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:TextBox ID="THDefecto9" runat="server" Style="text-align: center" Enabled="false" Width="100%">Rayas/Marcas expulsor</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto10" runat="server" Style="text-align: center" Enabled="false" Width="100%">Quemazos</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto11" runat="server" Style="text-align: center" Enabled="false" Width="100%">Brillos</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto12" runat="server" Style="text-align: center" Enabled="false" Width="100%">M. contaminado</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TDDefecto9" runat="server" Style="text-align: center" Enabled="false" Width="100%" BackColor=""></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto10" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto11" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto12" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:TextBox ID="THDefecto13" runat="server" Style="text-align: center" Enabled="false" Width="100%">Rechupes</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto14" runat="server" Style="text-align: center" Enabled="false" Width="100%">Color NOK</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto15" runat="server" Style="text-align: center" Enabled="false" Width="100%">Manchas</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto16" runat="server" Style="text-align: center" Enabled="false" Width="100%">Rebabas</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TDDefecto13" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto14" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto15" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto16" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:TextBox ID="THDefecto17" runat="server" Style="text-align: center" Enabled="false" Width="100%">Sólo plástico</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto18" runat="server" Style="text-align: center" Enabled="false" Width="100%">Sólo goma</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto19" runat="server" Style="text-align: center" Enabled="false" Width="100%">Otros</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto20" runat="server" Style="text-align: center" Enabled="false" Width="100%">Electroválvula</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TDDefecto17" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto18" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto19" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto20" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:TextBox ID="THDefecto21" runat="server" Style="text-align: center" Enabled="false" Width="100%">Grapa: Posición</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto22" runat="server" Style="text-align: center" Enabled="false" Width="100%">Grapa: Altura</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto23" runat="server" Style="text-align: center" Enabled="false" Width="100%">Tubo: Deformado</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto24" runat="server" Style="text-align: center" Enabled="false" Width="100%">Tubo: Mal puesto</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TDDefecto21" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto22" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto23" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto24" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:TextBox ID="THDefecto25" runat="server" Style="text-align: center" Enabled="false" Width="100%">Mal clipado</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto26" runat="server" Style="text-align: center" Enabled="false" Width="100%">Suciedad</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto27" runat="server" Style="text-align: center" Enabled="false" Width="100%">Punzonado NOK</asp:TextBox>
                                </th>
                                <th>
                                    <asp:TextBox ID="THDefecto28" runat="server" Style="text-align: center" Enabled="false" Width="100%">Láser ilegible</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TDDefecto25" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto26" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto27" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TDDefecto28" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="4">
                                    <asp:TextBox ID="THNotas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Incidencias</asp:TextBox>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:TextBox ID="TDNotas" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:TextBox ID="TDObservaciones" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>



                    </div>
                    <div class="form-group">
                        <label for="imagenes"><span class="glyphicon glyphicon-picture"></span>Imágenes</label><br />
                        <div class="thumbnail" style="vertical-align: middle">
                            <asp:HyperLink ID="linkimagen1" NavigateUrl="" ImageUrl="" Text="Defecto 1" Target="_new" runat="server" Width="32%" />
                            <asp:HyperLink ID="linkimagen2" NavigateUrl="" ImageUrl="" Text="Defecto 2" Target="_new" runat="server" Width="32%" />
                            <asp:HyperLink ID="linkimagen3" NavigateUrl="" ImageUrl="" Text="Defecto 3" Target="_new" runat="server" Width="32%" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-danger btn-default pull-right" data-dismiss="modal">Volver</button>
                </div>
            </div>

        </div>
    </div>
    --%>
</asp:Content>




