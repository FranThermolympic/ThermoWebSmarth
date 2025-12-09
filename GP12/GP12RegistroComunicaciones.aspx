<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GP12RegistroComunicaciones.aspx.cs" Inherits="ThermoWeb.GP12.GP12RegistroComunicaciones" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Registro de comunicaciones</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Registro de comunicaciones             
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
                <li><a class="dropdown-item" href="../KPI/KPIIndice.aspx">Consultar indicadores</a></li>
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
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat"">
        <div class="row">
            <div class="col-lg-2"></div>
            <div class="col-lg-7"></div>
            <div class="col-lg-2 mt-3">
                <h6>Periodo de revisión:</h6>
                <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="Rellenar_grid">
                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                    <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-1">
                <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-5 me-md-4 mb-md-1">


                    <button id="AUXCIERRAMODALFIRMA" runat="server" type="button" class="btn-close" data-bs-target="#ModalFirmaOperario" data-bs-toggle="modal" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                    <button id="AUXMODALACCIONFIRMA" runat="server" type="button" class="btn btn-primary invisible " data-bs-toggle="modal" data-bs-target="#ModalFirmaOperario" style="font-size: larger"></button>

                    <button id="AUXCIERRAMODAL" runat="server" type="button" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                    <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>
                    <button id="btnoffcanvas" runat="server" type="button" class="btn btn-outline-dark ms-md-0 bi bi-funnel-fill" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger"></button>
                </div>
            </div>
        </div>
        <div class="container-fluid mt-2">
            <div class="col-lg-12">
                <ul class="nav nav-pills justify-content-end" id="pills-tab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-file-earmark-binary me-2"></i>Por orden</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true"><i class="bi bi-person me-2"></i>Por trabajador</button>
                    </li>
                </ul>
                <div class="tab-content" id="pills-tabContent">
                    <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                        <asp:GridView ID="dgv_RegistroOperarios" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="GridView1_DataBound">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("NOp") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="button2" CommandName="CargaDetalle" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-pencil" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operarios">
                                    <ItemTemplate>
                                        <asp:Label ID="INFOOP1" Font-Bold="true" runat="server" Text='<%#Eval("INFO") %>' /><br />
                                        <asp:Label ID="NOMOP1" runat="server" Text='<%#Eval("NOMOP") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="lblFirmaIMG" runat="server" Height="50px" ImageUrl='<%#Eval("FIRMANOP")  %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lote" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLote" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("Nlote") %>' /><br />
                                        <asp:Label ID="lblFecharevision" runat="server" Text='<%#"("+ Eval("FechaInicio", "{0:dd/MM/yyyy}") + ")" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" Font-Bold="true" runat="server" Font-Size="Large" Text='<%#Eval("Referencia") %>' /><br />
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Nombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Malas" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMalas" Font-Size="Large" runat="server" Text='<%#Eval("PiezasNOK") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retrabajadas" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetrabajadas" Font-Size="Large" runat="server" Text='<%#Eval("Retrabajadas") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notas revisión" ItemStyle-BackColor="#e6e6e6" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblRevision" Width="100%" runat="server" TextMode="MultiLine" BackColor="Transparent" Rows="1" BorderColor="Transparent" Enabled="false" Text='<%#Eval("Notas") + " || " + Eval("Incidencias") %>' /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <asp:GridView ID="dgv_RegistroOrdenes" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="GridView2_DataBound">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="button2" CommandName="CargaDetalle" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-pencil" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lote" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLote" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("Nlote") %>' /><br />
                                        <asp:Label ID="lblFecharevision" runat="server" Text='<%#"("+ Eval("FechaInicio", "{0:dd/MM/yyyy}") + ")" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" Font-Bold="true" runat="server" Font-Size="Large" Text='<%#Eval("Referencia") %>' /><br />
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Nombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Malas" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMalas" Font-Size="Large" runat="server" Text='<%#Eval("PiezasNOK") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retrabajadas" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetrabajadas" Font-Size="Large" runat="server" Text='<%#Eval("Retrabajadas") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notas revisión" ItemStyle-BackColor="#e6e6e6" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblRevision" Width="100%" runat="server" TextMode="MultiLine" BackColor="Transparent" Rows="1" BorderColor="Transparent" Enabled="false" Text='<%#Eval("Notas") + " || " + Eval("Incidencias") %>' /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operarios">
                                    <ItemTemplate>
                                        <asp:Label ID="INFOOP1" Font-Bold="true" runat="server" Text='<%#Eval("INFO1") %>' /><br />
                                        <asp:Label ID="NOMOP1" runat="server" Text='<%#Eval("NOMOP1") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="INFOOP2" Font-Bold="true" runat="server" Text='<%#Eval("INFO2") %>' /><br />
                                        <asp:Label ID="NOMOP2" runat="server" Text='<%#Eval("NOMOP2") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="INFOOP3" Font-Bold="true" runat="server" Text='<%#Eval("INFO3") %>' /><br />
                                        <asp:Label ID="NOMOP3" runat="server" Text='<%#Eval("NOMOP3") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="INFOOP4" Font-Bold="true" runat="server" Text='<%#Eval("INFO4") %>' /><br />
                                        <asp:Label ID="NOMOP4" runat="server" Text='<%#Eval("NOMOP4") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <%--MODALES DE EDICION --%>
        <div class="modal fade" id="ModalEditaAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header bg-primary shadow">
                        <h5 class="modal-title text-white" id="staticBackdropLabel" runat="server">Detalles de revisión</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        <asp:TextBox ID="IDINSPECCION" runat="server" Style="text-align: center" Width="100%" Enabled="false" Visible="false"></asp:TextBox>

                    </div>
                    <div class="modal-body"  runat="server">
                        <div>                            
                            <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat"">
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
                            <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat"">
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
                    <div class="form-check form-switch ms-3">
                        <input class="form-check-input" type="checkbox" runat="server" id="SwitchActivas" checked="checked">
                        <label class="form-check-label" for="SwitchActivas">Mostrar sólo pendientes</label>
                    </div>
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
                    <h6>Cliente:</h6>
                    <div class="input-group mb-3">
                        <asp:DropDownList ID="lista_clientes" runat="server" class="form-select">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <h6>Responsable:</h6>
                    <div class="input-group mb-3">
                        <asp:DropDownList ID="lista_responsable" runat="server" class="form-select">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <h6>Operario:</h6>
                    <div class="input-group mb-3">
                        <input class="form-control" list="DatalistOperario" id="lista_operario" runat="server" placeholder="Escribe un operario...">
                        <datalist id="DatalistOperario" runat="server">
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
