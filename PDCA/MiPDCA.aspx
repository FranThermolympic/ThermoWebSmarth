<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="MiPDCA.aspx.cs" Inherits="ThermoWeb.PDCA.MiPDCA" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Mis acciones pendientes</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- MiPDCA
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).on("click", "[src*=plus]", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-dash-circle");
            $(this).attr("src", "dash");
        });
        $(document).on("click", "[src*=dash]", function () {
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-plus-circle");
            $(this).closest("tr").next().remove();
            $(this).attr("src", "plus");
        });
    </script>
    <script type="text/javascript">
        $(document).on('change', 'input', function () {
            var optionslist = $('datalist')[0].options;
            var value = $(this).val();
            for (var x = 0; x < optionslist.length; x++) {
                if (optionslist[x].value === value) {
                    const Valores = value.split("|");
                    document.getElementById("DLPARTESELECT").value = Valores[0];
                    document.getElementById("DLPARTESELECT2").value = Valores[1];
                    document.getElementById("DLPARTESELECT3").value = Valores[2];
                    document.getElementById("DataList2").value = "";
                    document.getElementById("DataList3").value = "";
                    $("[id$=DLPARTESELECTHIDDEN]").val(Valores[0]);
                    //$("#TABACCION").click();
                    break;
                }
            }
            var optionslist2 = $('datalist')[1].options;
            var value2 = $(this).val();
            for (var x = 0; x < optionslist2.length; x++) {
                if (optionslist2[x].value === value2) {
                    Valores = value2.split("|");
                    document.getElementById("DLPARTESELECT").value = Valores[0];
                    document.getElementById("DLPARTESELECT2").value = Valores[1];
                    document.getElementById("DLPARTESELECT3").value = Valores[2];
                    document.getElementById("DataList").value = "";
                    document.getElementById("DataList3").value = "";
                    $("[id$=DLPARTESELECTHIDDEN]").val(Valores[0]);
                    //$("#TABACCION").click();
                    break;
                }
            }

            var optionslist3 = $('datalist')[2].options;
            var value3 = $(this).val();
            for (var x = 0; x < optionslist3.length; x++) {
                if (optionslist3[x].value === value3) {
                    Valores = value3.split("|");
                    document.getElementById("DLPARTESELECT").value = Valores[0];
                    document.getElementById("DLPARTESELECT2").value = Valores[1];
                    document.getElementById("DLPARTESELECT3").value = Valores[2];
                    document.getElementById("DataList").value = "";
                    document.getElementById("DataList2").value = "";
                    $("[id$=DLPARTESELECTHIDDEN]").val(Valores[0]);
                    //$("#TABACCION").click();
                    break;
                }
            }
        });
    </script>
    <script>
        //scripts de botones
        function VerAcciones() {
            $("#PILLACCIONES").click();
        }
        function VerNOCONFORMIDADES() {
            $("#PILLNOCONFORMIDADES").click();
        }
        function VerMOLREPARAR() {
            $("#PILLMOLREPARAR").click();
        }
        function VerMOLVALIDAR() {
            $("#PILLMOLREPARAR").click();
        }
        function VerMAQREPARAR() {
            $("#PILLMAQREPARAR").click();
        }
        function VerMAQVALIDAR() {
            $("#PILLMAQREPARAR").click();
        }
        function VerGP12JAULA() {
            $("#PILLMURODECALIDAD").click();
        }
    </script>
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
                showAnim: "fold",
                yearRange: "c-20:c+10",
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa']
            });
        });
    </script>
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="row" id="selectpersonalrow" runat="server" visible="false">
            <div class="col-lg-12">
                <div class="input-group ms-1 mb-3 mt-3">
                    <button class="btn btn-outline-secondary" type="button" runat="server" onserverclick="CargaOperario">Cargar</button>
                    <asp:DropDownList ID="SelectPersonal" runat="server" class="form-select" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="d-flex align-items-start ">

                <div class="nav flex-column nav-pills me-3 " id="v-pills-tab" role="tablist" aria-orientation="vertical">
                    <br />
                    <button class="nav-link active" id="v-pills-home-tab" data-bs-toggle="pill" data-bs-target="#v-pills-home" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true" style="text-align: start; font-weight: 600"><i class="bi bi-house">&nbsp Resumen</i></button>
                    <button class="nav-link" id="PILLMURODECALIDAD" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab8" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-bricks">&nbsp GP12 / Jaula Rechazo</i></button>
                    <button class="nav-link" id="PILLACCIONES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab6" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-x-diamond">&nbsp Planes de acción</i></button>
                    <button class="nav-link" id="PILLNOCONFORMIDADES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab5" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-cone-striped">&nbsp No conformidades</i></button>
                    <button class="nav-link" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2">&nbsp Mant. Moldes</i></button>
                    <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-building">&nbsp Mant. Máquinas</i></button>
                     
                </div>
                <div class="tab-content col-10" id="v-pills-tabContent">
                    <div class="tab-pane fade show active" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">
                        <div class="row">
                            <div class="col-lg-3 mt-3" id="COLPlanAccion" runat="server" visible="false">
                                <button type="button" class="btn btn-primary position-relative border border-dark shadow" id="BtnPlanAccion" onclick="VerAcciones()" style="width: 100%; height: 50px; text-align: center">
                                    Planes de acción
                                  <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger border border-dark shadow-sm" id="BADGEACCION" runat="server">0</span>
                                </button>
                            </div>
                            <div class="col-lg-3 mt-3" id="COLNoConformidades" runat="server" visible="false">
                                <button type="button" class="btn btn-primary position-relative border border-dark shadow" id="BtnNoConformidades" onclick="VerNOCONFORMIDADES()" style="width: 100%; height: 50px; text-align: center">
                                    No conformidades (Pendientes)
                                  <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger border border-dark shadow-sm" id="BADGENC" runat="server">0
                                    
                                  </span>
                                </button>
                            </div>
                            <div class="col-lg-3 mt-3" id="COLNoConformidadesVENC" runat="server" visible="false">
                                <button type="button" class="btn btn-primary position-relative border border-dark shadow" id="BtnNoConformidadesVENC" onclick="VerNOCONFORMIDADES()" style="width: 100%; height: 50px; text-align: center">
                                    No conformidades (Vencidas)
                                  <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger border border-dark shadow-sm" id="BADGENCVEN" runat="server">0
                                    
                                  </span>
                                </button>
                            </div>
                            <div class="col-lg-3 mt-3" id="COLMolReparar" runat="server" visible="false">
                                <button type="button" class="btn btn-primary position-relative border border-dark shadow" onclick="VerMOLREPARAR()" style="width: 100%; height: 50px; text-align: center">
                                    Moldes por reparar
                                  <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger border border-dark shadow-sm" id="BADGEMOLDREP" runat="server">0
                                    
                                  </span>
                                </button>
                            </div>
                            <div class="col-lg-3 mt-3" id="COLMolValidar" runat="server" visible="false">
                                <button type="button" class="btn btn-primary position-relative border border-dark shadow" onclick="VerMOLVALIDAR()" style="width: 100%; height: 50px; text-align: center">
                                    Moldes por validar
                                  <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger border border-dark shadow-sm" id="BADGEMOLDVAL" runat="server">0
                                
                                  </span>
                                </button>
                            </div>
                            <div class="col-lg-3 mt-3" id="COLMaqReparar" runat="server" visible="false">
                                <button type="button" class="btn btn-primary position-relative border border-dark shadow" onclick="VerMAQREPARAR()" style="width: 100%; height: 50px; text-align: center">
                                    Máquinas por reparar
                                  <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger border border-dark shadow-sm" id="BADGEMAQREP" runat="server">0
                                    
                                  </span>
                                </button>
                            </div>
                            <div class="col-lg-3 mt-3" id="COLMaqValidar" runat="server" visible="false">
                                <button type="button" class="btn btn-primary position-relative border border-dark shadow" onclick="VerMAQVALIDAR()" style="width: 100%; height: 50px; text-align: center">
                                    Máquinas por validar
                                  <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger border border-dark shadow-sm" id="BADGEMAQVAL" runat="server">0
                                    
                                  </span>
                                </button>
                            </div>
                            <div class="col-lg-3 mt-3" id="COLJaulaRechazo" runat="server" visible="false">
                                <button type="button" class="btn btn-primary position-relative border border-dark shadow" onclick="VerGP12JAULA()" style="width: 100%; height: 50px; text-align: center">
                                   Jaula de rechazo
                                  <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger border border-dark shadow-sm" id="BADGEJAULA" runat="server">0
                                  </span>
                                </button>
                            </div>
                            <div class="col-lg-3 mt-3" id="COLGP12" runat="server" visible="false">
                                <button type="button" class="btn btn-primary position-relative border border-dark shadow" onclick="VerGP12JAULA()" style="width: 100%; height: 50px; text-align: center">
                                   Vencidos GP12
                                  <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger border border-dark shadow-sm" id="BADGEGP12" runat="server">0
                                    
                                  </span>
                                </button>
                            </div>
                        </div>

                    </div>
                    <div class="tab-pane fade" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="container-fluid mt-md-1">
                                    <h4>Pendientes de reparar</h4>
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="dgvListaPendientesReparacionMolde" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded  " BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP" EmptyDataText="Ningún molde pendientede reparar.">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                            <RowStyle BackColor="White" />
                                            
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in shadow" runat="server" CommandName="RepMoldes" CommandArgument='<%#Eval("IdReparacionMolde")%>' Style="font-size: 1rem"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Parte y molde" ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdReparacionMolde" runat="server" Font-Bold="true" Text='<%#Eval("IdReparacionMolde") %>' /><br />
                                                        <asp:Label ID="lblIDMoldes" runat="server" Text='<%#Eval("IDMoldes") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Avería" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                 <div class="container-fluid mt-md-1">
                                    <h4>Pendientes de validar</h4>
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="dgvListaPendientesValidacionMolde" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded" BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP"  EmptyDataText="Ningún molde pendientede de validar.">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                             <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in shadow" Style="font-size: 1rem" runat="server" CommandName="ValMoldes" CommandArgument='<%#Eval("IdReparacionMolde")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Parte" ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdReparacionMolde" runat="server" Font-Bold="true" Text='<%#Eval("IdReparacionMolde") %>' />
                                                        <asp:Label ID="lblIDMoldes" runat="server" Text='<%#Eval("IDMoldes") %>' />
                                                        </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Avería" ItemStyle-Width="60%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="tab-pane fade" id="v-pills-tab3" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="container-fluid mt-md-1">
                                    <h4>Pendientes de reparar</h4>
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="dgvListaPendientesReparacionMaquina" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded" BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP" EmptyDataText="Ninguna máquina pendiente de reparar.">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                             <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in shadow" Style="font-size: 1rem" runat="server" CommandName="RepMaquinas" CommandArgument='<%#Eval("PARTE")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Parte y máquina" ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdPARTE" runat="server" Font-Bold="true" Text='<%#Eval("PARTE") %>' /><br />
                                                        <asp:Label ID="lblIDDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Avería" ItemStyle-Width="60%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="container-fluid mt-md-1">
                                    <h4>Pendientes de validación</h4>
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="dgvListaPendientesValidacionMaquina" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded border-top-0 " BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP" EmptyDataText="Ninguna máquina pendiente de validar.">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                             <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in shadow" Style="font-size: 1rem" runat="server" CommandName="ValMaquinas" CommandArgument='<%#Eval("PARTE")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Parte y máquina" ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdPARTE" runat="server" Font-Bold="true" Text='<%#Eval("PARTE") %>' /><br />
                                                         <asp:Label ID="lblIDDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Avería y reparación" ItemStyle-Width="60%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("MotivoAveria") %>' /><br />
                                                        <asp:Label ID="lblReparación" runat="server" Text='<%#"<strong>Rep: </strong>" + Eval("Reparacion") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="tab-pane fade" id="v-pills-tab5" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                        <div class="row">
                            <div class="col-lg-6">
                                <h4 class="mt-2">No conformidades vencidas</h4>
                                <div class="container-fluid mt-md-1">
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="dgvListaVencidasNoConformidades" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded border-top-0 " BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                             <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in shadow" Style="font-size: 1rem" runat="server" CommandName="NoConformidades" CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nº" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdPARTE" runat="server" Text='<%#Eval("ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Producto" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("PRODDESCRIPCION") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Problema" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDDescripcion" runat="server" Text='<%#Eval("DESCRIPCION") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contramedida" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotivoAveria2" runat="server" Text='<%#Eval("CONTRAMEDIDA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <h4 class="mt-2">No conformidades en curso</h4>
                                <div class="container-fluid mt-md-1">
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="dgvListaPendientesNoConformidades" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded border-top-0 " BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                             <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in shadow" Style="font-size: 1rem" runat="server" CommandName="NoConformidades" CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nº" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdPARTE" runat="server" Text='<%#Eval("ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Producto" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("PRODDESCRIPCION") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Problema" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDDescripcion" runat="server" Text='<%#Eval("DESCRIPCION") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contramedida" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotivoAveria2" runat="server" Text='<%#Eval("CONTRAMEDIDA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="v-pills-tab6" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                        <div class="container-fluid mt-md-1">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h4 class="mt-2">Acciones que dirijo:</h4>
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="dgvListaPlanesPendientesPDCA" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow p-3 rounded" BorderColor="black" OnRowCommand="RedireccionaAPP">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                            <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in shadow" Style="font-size: 1rem" runat="server" CommandName="PlanAccion" CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nº" Visible="false" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdPDCA"  runat="server" Text='<%#Eval("ID") %>' />
                                                          <asp:Label ID="lblIdPARTE" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("NUMACCION") %>' />
                                            
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                   
                                                <asp:TemplateField HeaderText="Plan de acción" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFMOLD" Font-Bold="true" runat="server" Text='<%#Eval("Molde") + " " + Eval("Producto") %>' /><br />
                                                       
                                                        <asp:Label ID="lblDesviación" Font-Italic="true" runat="server" Text='<%#Eval("DesviacionEncontrada") %>' />
                                                 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acción a realizar" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" Font-Bold="true" runat="server" CssClass="me-2" Text="Ejecuta:" /><asp:Label ID="lblNombre" runat="server" Text='<%#Eval("EJECUTA") %>' /><br />
                                                        <asp:Label ID="Label2" Font-Bold="true" runat="server" CssClass="me-2" Text="Prevista:" /><asp:Label ID="Label3" Font-Italic="true" runat="server" Text='<%#Eval("FECHA", "{0:dd/MM/yyyy}") %>' /><br />
                                                        <asp:Label ID="lblMotivoAveria" Font-Italic="true" runat="server" Text='<%#Eval("REQUISITO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <h4 class="mt-2">Acciones asignadas a mí:</h4>
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="GridAccionesPendientes" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow p-3 rounded" BorderColor="black" OnRowCommand="RedireccionaAPP">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                             <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in shadow" Style="font-size: 1rem" runat="server" CommandName="PlanAccion" CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nº" Visible="false" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdPDCA"  runat="server" Text='<%#Eval("ID") %>' />
                                                        <asp:Label ID="lblIdPARTE" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("NUMACCION") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acción a realizar" ItemStyle-Width="30%">
                                                    <ItemTemplate>                                                       
                                                        <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("REQUISITO") %>' /><br />
                                                        <asp:Label ID="Label2" Font-Bold="true" runat="server" CssClass="me-2" Text="Prevista:" /><asp:Label ID="Label3" Font-Italic="true" runat="server" Text='<%#Eval("FECHA", "{0:dd/MM/yyyy}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Plan de acción" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblREFMOLD" Font-Bold="true" runat="server" Text='<%#Eval("Molde") + " " + Eval("Producto") %>' /><br />
                                                
                                                        <asp:Label ID="Label1" Font-Bold="true" runat="server" CssClass="me-2" Text="Dirige:" /><asp:Label ID="lblNombre" runat="server" Text='<%#Eval("JEFE") %>' /><br />
                                                        <asp:Label ID="lblDesviación" Font-Italic="true" runat="server" Text='<%#Eval("DesviacionEncontrada") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="tab-pane fade" id="v-pills-tab8" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="container-fluid mt-md-1">
                                    <h4>Vencidos GP12</h4>
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="dgvMuroCalidad" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded" BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP" EmptyDataText="Ningún producto fuera de fecha.">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                             <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in shadow" Style="font-size: 1rem" runat="server" CommandName="RedMuroCalidad" CommandArgument='<%#Eval("REF")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Producto" ItemStyle-Width="60%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdPARTE" runat="server" Font-Bold="true" Text='<%#Eval("REF") %>' /><br />
                                                        <asp:Label ID="lblIDDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Motivo y fecha" ItemStyle-Width="35%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("EstadoActual") %>' /><br />
                                                        <asp:Label ID="lblReparación" runat="server" Text='<%#"<strong>Debe salir: </strong>" + Eval("Fechaprevsalida", "{0:dd/MM/yyyy}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="container-fluid mt-md-1">
                                    <h4>Jaula de rechazo</h4>
                                    <div style="overflow-y: auto;">
                                        <asp:GridView ID="dgvJaulaRechazo" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive  shadow-lg p-3 mb-5 rounded" BorderColor="black" Width="100%" OnRowCommand="RedireccionaAPP" EmptyDataText="Sin materiales para gestionar.">
                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <AlternatingRowStyle BackColor="#e6e6e6" />
                                             <RowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton type="button" class="btn btn-secondary ms-md-0 bi bi-zoom-in" Style="font-size: 1rem" runat="server" CommandName="RedirectJaula"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Text='<%#Eval("Referencia") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Motivo y entrada" ItemStyle-Width="60%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReparación" runat="server" Text='<%#"<strong>Entrada: </strong>" + Eval("FechaEntrada", "{0:dd/MM/yyyy}") %>' /><br />
                                                        <asp:Label ID="lblMotivoAveria" runat="server" Text='<%#Eval("Motivo") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Debe salir" ItemStyle-Width="60%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDebeSalir" runat="server" Text='<%#"<strong>Debe salir: </strong>" + Eval("DebeSalir", "{0:dd/MM/yyyy}") %>' /><br />
                                                        <asp:Label ID="lblDecision" runat="server" Text='<%#Eval("Decision") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


