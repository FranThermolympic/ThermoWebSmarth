<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Metrologia.aspx.cs" Inherits="ThermoWeb.CALIDAD.MetrologiaCalibraciones" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Listado de equipos</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Metrología             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <script type="text/javascript">
        function ShowPopup1() {
            document.getElementById("AUXMODALACCION").click();
        }
        function ClosePopup1() {

        }

        function ShowPopupMSA() {
            document.getElementById("AUXMODALMSA").click();
        }
        function ShowPopupCalibracion() {
            document.getElementById("AUXMODALCALIBRACION").click();
        }
        function ShowPopupDOCAUX() {
            document.getElementById("AUXMODALDOCAUX").click();
        }
        function ShowPopupDOCProductos() {
            document.getElementById("AUXMODALPRODUCTOS").click();
        }
        function ShowPopupMODALNuevoEquipo() {
            document.getElementById("AUXMODALNuevoEquipo").click();
        }
        function ClosePopupFirma() {
            document.getElementById("AUXCIERRAMODALFIRMA").click();
        }
        function ShowPopupEquiposXProductos() {
            document.getElementById("AUXMODALEquiposXProductos").click();
        }
        function Equipo_Fuera_RangoNUM() {
            alert("Error al crear el equipo. El equipo está fuera del rango asignado a metrología (55500000 a 55599999).");
        }
        function EquipoExistente() {
            alert("Error al crear el equipo. El número de equipo asignado ya existe.");
        }
        function Borrado_MSA_OK() {
            alert("El documento se ha eliminado correctamente.");
        }
        function Borrado_MSA_NoExiste() {
            alert("El documento seleccionado ya se ha borrado con anterioridad. Se eliminará del listado.");
        }
        function Borrado_MSA_NOK() {
            alert("Se ha producido un error al eliminar el documento. Por favor, consulte con el administrador.");
        }
    </script>
    <style>
        .tab-content {
            width: 100%;
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
                    <button id="AUXCIERRAMODAL" runat="server" type="button" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                    <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>
                    <button id="AUXMODALMSA" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalMSAGestion" style="font-size: larger">MSA</button>
                    <button id="AUXMODALCALIBRACION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalCalibración" style="font-size: larger">MSA</button>
                    <button id="AUXMODALDOCAUX" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalDocumentosAuxiliares" style="font-size: larger">MSA</button>
                    <button id="AUXMODALPRODUCTOS" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalAgregarProductos" style="font-size: larger">MSA</button>
                    <button id="AUXMODALEquiposXProductos" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEquiposXProductos" style="font-size: larger">MSA</button>
                    <button id="AUXMODALNuevoEquipo" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalNuevoEquipo" style="font-size: larger">MSA</button>

                    <button id="btnoffcanvas" runat="server" type="button" class="btn btn-primary ms-md-0 bi bi-funnel shadow-sm" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger"></button>


                </div>
            </div>
        </div>
        <div class="container-fluid mt-2">
            <div class="col-lg-12">
                <ul class="nav nav-pills justify-content-end" id="pills-tab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="BTN_TAREAS_METROLOG" runat="server" data-bs-toggle="pill" data-bs-target="#pills_tareas_metrolog" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-hammer me-2"></i>Tareas</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="BTN_ESTADO_PRODUCTOS" runat="server" data-bs-toggle="pill" data-bs-target="#pills_plancalib" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-calendar-month me-2"></i>Planificación de calibraciones</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="BTN_ULTIMAS_REVISIONES" runat="server" data-bs-toggle="pill" data-bs-target="#pills_historico" type="button" role="tab" aria-controls="pills-home" aria-selected="true"><i class="bi bi-book me-2"></i>Listado de equipos</button>
                    </li>
                </ul>
                <div class="tab-content" id="pills-tabContent">
                    <div class="tab-pane fade show active" id="pills_historico" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <asp:GridView ID="dgv_Listado_Equipos" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="GridView_DataBoundHist">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Visible="true" ItemStyle-BackColor="#e6e6e6" HeaderStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="button2" CommandName="NuevoEquipo" UseSubmitBehavior="true" CssClass="btn btn-light border border-1 border-dark shadow shadow-sm" Style="font-size: 1rem">
                                          <i class="bi bi-plus-circle"></i></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="button2" CommandName="FichaCalibracion" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <button type="button" id="BTNOrdenaEquipo" runat="server" onserverclick="Ordenar_lineas" class="btn btn-primary"><i class="bi bi-arrow-down-up me-2"></i>Equipo</button>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumEquipo" runat="server" Font-Bold="true" Text='<%#Eval("NumEquipo") %>' /><br />
                                        <asp:Label ID="lblNumEquipoORI" runat="server" Font-Size="Small" Text='<%#"(" + Eval("NequipoORI") + ")" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <button type="button" class="btn btn-primary" runat="server" id="BTNOrdenaDescripcion" onserverclick="Ordenar_lineas"><i class="bi bi-arrow-down-up me-2"></i>Descripción</button>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Font-Bold="true" Text='<%#Eval("Nombre") %>' /><br />
                                        <asp:Label ID="lblNSerie" Font-Size="Small" Font-Italic="true" runat="server" Text='<%#" (" + Eval("NSerie") + ")" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="center">
                                    <HeaderTemplate>
                                        <button type="button" class="btn btn-primary" runat="server" id="BTNOrdenaTipoEquipo" onserverclick="Ordenar_lineas"><i class="bi bi-arrow-down-up me-2"></i>Tipo de equipo</button>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClase" runat="server" Text='<%#Eval("Clase") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <button type="button" class="btn btn-primary">Rango / división esc.</button>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRango" runat="server" Text='<%#Eval("Rango") + "(" + Eval("DivisionEscala") + ")" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <button type="button" class="btn btn-primary">Periodo</button>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoEquipo" runat="server" Font-Bold="true" Font-Size="Smaller" ForeColor="Red" Visible="false" Text='<%#Eval("EstadoEquipo") %>' />
                                        <asp:Label ID="lblAlertaInactivo" runat="server" Font-Bold="true" Font-Size="Smaller" Visible="false"><i class="bi bi-exclamation-circle-fill">&nbsp Inactivo</i><br /></asp:Label>
                                        <asp:Label ID="lblAlertaObsoleto" runat="server" Font-Bold="true" Font-Size="Smaller" Visible="false"><i class="bi bi-exclamation-circle-fill">&nbsp Obsoleto</i><br /></asp:Label>
                                        <asp:Label ID="lblFechaVencida" runat="server" Font-Bold="true" Font-Size="Smaller" ForeColor="Red" Visible="false"><i class="bi bi-exclamation-circle-fill"> ¡Plazo vencido!</i><br /></asp:Label>
                                        <asp:Label ID="lblFechaProxima" runat="server" Font-Bold="true" Font-Size="Smaller" ForeColor="orange" Visible="false"><i class="bi bi-exclamation-triangle-fill"></i> ¡Vence pronto!</i><br /></asp:Label>
                                        <asp:Label ID="lblFrecuencia" runat="server" Text='<%#Eval("FrecuenciaCalibracion") + " años." %>' />
                                        <asp:Label ID="lblNACAlibracion" runat="server" Font-Bold="true" Font-Size="Smaller" Visible="false"><i class="bi bi-slash-circle">&nbsp No requerido</i><br /></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <button type="button" class="btn btn-primary" runat="server" id="BTNOrdenaCalibracion" onserverclick="Ordenar_lineas"><i class="bi bi-arrow-down-up me-2"></i>Calibrado</button>
                                        <button type="button" class="btn btn-primary" runat="server" id="BTNOrdenaVence" onserverclick="Ordenar_lineas"><i class="bi bi-arrow-down-up me-2"></i>Vence</button>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBoundCalibracion" Visible="false" Font-Size="small" runat="server" Text='<%#Eval("VenceCalibracion", "{0:dd/MM/yyyy}") %>' />
                                        <asp:Label ID="lblTipoCalibracion" Visible="false" Font-Size="small" runat="server" Text='<%#Eval("TipoCalibracion") %>' />

                                        <asp:Label ID="lblUltimaCalibracion" Font-Size="small" runat="server" Text='<%#"<strong>Vence en: </strong>" + Eval("VenceCalibracion", "{0:dd/MM/yyyy}") %>' /><br />
                                        <asp:Label ID="lblProximaCalibracion" Font-Size="small" runat="server" Text='<%#"<strong>Calibrado en: </strong>" + Eval("FechaDoc", "{0:dd/MM/yyyy}") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="center" Visible="false">
                                    <HeaderTemplate>
                                        <button type="button" class="btn btn-primary" runat="server" id="BTNOrdenaMSA" onserverclick="Ordenar_lineas"><i class="bi bi-arrow-down-up me-2"></i>MSA</button>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstadoMSA" Font-Size="small" Font-Bold="true" runat="server" Text='<%#Eval("EstadoMSA") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle" ItemStyle-VerticalAlign="Middle">
                                    <HeaderTemplate>
                                        <button type="button" class="btn btn-primary btn-lg" runat="server" id="BTNImprimirETIQ" onserverclick="Imprimir_etiquetas"><i class="bi bi-printer"></i></button>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" CssClass="ChkBoxClass" type="checkbox" ID="CheckPrinting" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                     <style>.ChkBoxClass input {width:25px;
                                   height:25px;
                                   box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.075) !important;
                                    }
                    </style>
                    <div class="tab-pane fade" id="pills_plancalib" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <div class="row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-10">
                                <div class="input-group ">
                                    <asp:DropDownList CssClass="form-select border border-dark" ID="FiltroTipoEquipoAgrupado" runat="server">
                                        <asp:ListItem Value="0">-</asp:ListItem>
                                        <asp:ListItem Value="1">Equipos de uso genérico</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList CssClass="form-select border border-dark" ID="FiltroTipoPendCalibracion" runat="server">
                                    </asp:DropDownList>
                                    <asp:DropDownList CssClass="form-select border border-dark" ID="FiltroEstadoPendCalibracion" runat="server">
                                        <asp:ListItem Value="0">-</asp:ListItem>
                                        <asp:ListItem Value="1">Vencen pronto</asp:ListItem>
                                        <asp:ListItem Value="2">Calibración vencida</asp:ListItem>
                                        <asp:ListItem Value="3">Sin calibrar</asp:ListItem>
                                    </asp:DropDownList>
                                    <button class="btn btn-outline-secondary border border-dark" type="button" id="BTNFiltroCalib" runat="server" onserverclick="Rellenar_grid">Filtrar</button>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2">
                                <h5 style="font-weight: bold">Equipos activos:</h5>
                                <asp:Label runat="server" ID="lblKPIEquipoActivo" CssClass="ms-5" Font-Size="XX-Large">0</asp:Label>
                                <h5 style="font-weight: bold">Vencen pronto:</h5>
                                <asp:Label runat="server" ID="lblKPIVencenPronto" CssClass="ms-5" Font-Size="XX-Large">0</asp:Label>
                                <h5 style="font-weight: bold">Vencidos:</h5>
                                <asp:Label runat="server" ID="lblKPIVencidos" CssClass="ms-5" Font-Size="XX-Large">0</asp:Label>
                                <h5 style="font-weight: bold">Sin Calibrar:</h5>
                                <asp:Label runat="server" ID="lblKPISinCalibrar" CssClass="ms-5" Font-Size="XX-Large">0</asp:Label>
                            </div>
                            <div class="col-lg-10">

                                <asp:GridView ID="GridPendientesCalibracion" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="GridView_DataBoundCalib"
                                    OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" Visible="true" ItemStyle-BackColor="#e6e6e6" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="DetalleCalib" CommandName="FichaCalibracion" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>
                                                <button type="button" class="btn btn-primary">Equipo</button>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumEquipo" runat="server" Font-Bold="true" Text='<%#Eval("NumEquipo") %>' /><br />
                                                <asp:Label ID="lblNumEquipoORI" runat="server" Font-Size="Small" Text='<%#"(" + Eval("NequipoORI") + ")" %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEDITNumEquipo" runat="server" Font-Bold="true" Text='<%#Eval("NumEquipo") %>' /><br />
                                                <asp:Label ID="lblEDITlblNumEquipoORI" runat="server" Font-Size="Small" Text='<%#"(" + Eval("NequipoORI") + ")" %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>
                                                <button type="button" class="btn btn-primary">Descripción</button>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNombre" runat="server" Font-Bold="true" Text='<%#Eval("Nombre") %>' /><br />
                                                <asp:Label ID="lblClase" runat="server" Text='<%#Eval("Clase") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>
                                                <button type="button" class="btn btn-primary">Vence calibración</button>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBoundCalibracion" Visible="false" Font-Size="small" runat="server" Text='<%#Eval("VenceCalibracion", "{0:dd/MM/yyyy}") %>' />
                                                <asp:Label ID="lblTipoCalibracion" Visible="false" Font-Size="small" runat="server" Text='<%#Eval("TipoCalibracion") %>' />
                                                <asp:Label ID="lblUltimaCalibracion" Font-Bold="true" runat="server" Text='<%#Eval("VenceCalibracion", "{0:dd/MM/yyyy}") %>' /><br />
                                                <asp:Label ID="lblEstadoEquipo" runat="server" Font-Bold="true" Font-Size="Smaller" ForeColor="Red" Visible="false" Text='<%#Eval("EstadoEquipo") %>' />
                                                <asp:Label ID="lblAlertaInactivo" runat="server" Font-Bold="true" Font-Size="Smaller" Visible="false"><i class="bi bi-exclamation-circle-fill">&nbsp Inactivo</i><br /></asp:Label>
                                                <asp:Label ID="lblAlertaObsoleto" runat="server" Font-Bold="true" Font-Size="Smaller" Visible="false"><i class="bi bi-exclamation-circle-fill">&nbsp Obsoleto</i><br /></asp:Label>
                                                <asp:Label ID="lblFechaVencida" runat="server" Font-Bold="true" Font-Size="Smaller" ForeColor="Red" Visible="false"><i class="bi bi-exclamation-circle-fill"> ¡Plazo vencido!</i><br /></asp:Label>
                                                <asp:Label ID="lblFechaProxima" runat="server" Font-Bold="true" Font-Size="Smaller" ForeColor="orange" Visible="false"><i class="bi bi-exclamation-triangle-fill"></i> ¡Vence pronto!</i><br /></asp:Label>
                                                <asp:Label ID="lblNACAlibracion" runat="server" Font-Bold="true" Font-Size="Smaller" Visible="false"><i class="bi bi-slash-circle">&nbsp No requerido</i><br /></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>
                                                <button type="button" class="btn btn-primary"></i>Calibración planificada</button>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProximaCalibracion" Font-Size="X-Large" Font-Bold="true" runat="server" Text='<%#Eval("CalibracionPlanificada", "{0:dd/MM/yyyy}") %>' />
                                                <asp:LinkButton runat="server" ID="EditaCalib" Font-Size="Large" CommandName="Edit" UseSubmitBehavior="true" class="btn btn-outline-dark border border-0"><i class="bi bi-pencil"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <div class="input-group mb-3">
                                                    <asp:TextBox runat="server" ID="TXTEditaFecha" CssClass="form-control Add-text" Text='<%#Eval("CalibracionPlanificada") %>'></asp:TextBox>
                                                    <asp:LinkButton runat="server" ID="UpdateCalib" Font-Size="Large" Width="25%" CommandName="Update" UseSubmitBehavior="true" class="btn btn-outline-dark"><i class="bi bi-calendar-check"></i></asp:LinkButton>
                                                </div>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="pills_tareas_metrolog" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <div class="row">
                            <div class="col-lg-4">
                                <h5>Pendientes de MSA
                                    <button type="button" class="btn btn-primary position-relative bg-transparent border-0">
                                        <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" runat="server" id="BadgePendMSA">0
                                        </span>
                                    </button>
                                </h5>
                                <div style="overflow-y: auto; height: 400px">
                                    <asp:GridView ID="GridPendientesMSA" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="button2" CommandName="FichaCalibracion" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="85%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumEquipo" runat="server" Font-Bold="true" Text='<%#Eval("NumEquipo") %>' /><br />
                                                    <asp:Label ID="lblNombre" runat="server" Font-Size="Small" Text='<%#Eval("Nombre") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <h5>Pendientes de imagen
                                    <button type="button" class="btn btn-primary position-relative bg-transparent border-0">
                                        <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" runat="server" id="BadgePendImagen">0
                                        </span>
                                    </button>
                                </h5>
                                <div style="overflow-y: auto; height: 400px">
                                    <asp:GridView ID="GridEquiposSinImagen" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="button2" CommandName="FichaCalibracion" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="85%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumEquipo" runat="server" Font-Bold="true" Text='<%#Eval("NumEquipo") %>' /><br />
                                                    <asp:Label ID="lblNombre" runat="server" Font-Size="Small" Text='<%#Eval("Nombre") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <h5>Pendientes de ubicar
                                    <button type="button" class="btn btn-primary position-relative bg-transparent border-0">
                                        <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" runat="server" id="BadgePendUbicar">0
                                        </span>
                                    </button>
                                </h5>
                                <div style="overflow-y: auto; height: 400px">
                                    <asp:GridView ID="GridSinUbicar" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="button2" CommandName="FichaCalibracion" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="85%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumEquipo" runat="server" Font-Bold="true" Text='<%#Eval("NumEquipo") %>' /><br />
                                                    <asp:Label ID="lblNombre" runat="server" Font-Size="Small" Text='<%#Eval("Nombre") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                             <div class="col-lg-4 mt-2">
                                <h5>Pendientes de equipo alternativo
                                    <button type="button" class="btn btn-primary position-relative bg-transparent border-0">
                                        <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" runat="server" id="BadgePendAlternativos">0
                                        </span>
                                    </button>
                                </h5>
                                <div style="overflow-y: auto; height: 400px">
                                    <asp:GridView ID="GridSinEquiposAlternativos" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="button2" CommandName="FichaCalibracion" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="85%" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumEquipo" runat="server" Font-Bold="true" Text='<%#Eval("NumEquipo") %>' /><br />
                                                    <asp:Label ID="lblNombre" runat="server" Font-Size="Small" Text='<%#Eval("Nombre") %>' />
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
    <%--MODALES DE EDICION --%>
    <div class="modal fade" id="ModalEditaAccion" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="staticBackdropLabel" runat="server">Ficha de calibración</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    <asp:TextBox ID="IDINSPECCION" runat="server" Style="text-align: center" Width="100%" Enabled="false" Visible="false"></asp:TextBox>
                </div>
                <div class="modal-body" runat="server">
                    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                        <div class="row">
                            <div class="d-flex align-items-start">
                                <div class="nav flex-column nav-pills me-2" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                                    <button id="TABACCION" class="nav-link active" data-bs-toggle="pill" data-bs-target="#v-pills-home" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true"><i class="bi bi-kanban"></i></button>
                                    <button id="TABDOCUS" class="nav-link" data-bs-toggle="pill" data-bs-target="#v-pills-docus" type="button" role="tab" aria-controls="v-pills-docus" aria-selected="true"><i class="bi bi-folder2-open"></i></button>
                                </div>
                                <div class="tab-content" id="v-pills-tabContent">
                                    <div class="tab-pane fade show active" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark text-white fw-bolder" style="background-color: darkorange">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>
                                                            <label runat="server" id="lblNumEquipo"></label>
                                                            <label runat="server" id="lblNombreEquipo" class="ms-2"></label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <a id="AImagenEquipo" runat="server" href="../SMARTH_docs/METROLOGIA/sin_imagen.jpg">
                                                        <img id="ImagenEquipo" runat="server" src="../SMARTH_docs/METROLOGIA/sin_imagen.jpg" class="rounded img-thumbnail shadow" alt="...">
                                                    </a>
                                                    <div class="input-group input-group-sm mb-1 shadow">
                                                        <asp:FileUpload ID="UploadImagen" runat="server" accept=".png,.jpg,.jpeg" class="form-control"></asp:FileUpload>
                                                        <button class="btn btn-outline-secondary" type="button" runat="server" id="BTNUploadImagen" onserverclick="Insertar_documento">Subir</button>

                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <div class="row">
                                                        <div class="col-lg-7">
                                                            <h6>Nombre:</h6>
                                                            <input type="text" id="InputNombreEquipo" class="form-control border-dark shadow" autocomplete="off" runat="server">
                                                        </div>
                                                        <div class="col-lg-5">
                                                            <h6>Número de serie:</h6>
                                                            <input type="text" id="InputNumSerie" class="form-control border-dark shadow" autocomplete="off" runat="server">
                                                        </div>
                                                    </div>
                                                    <div class="row mt-3">
                                                        <div class="col-lg-4">
                                                            <h6>Fecha de alta:</h6>
                                                            <input type="text" id="InputFechaAlta" class="form-control border-dark shadow Add-text" autocomplete="off" runat="server">
                                                            <div runat="server" id="DIVFechaBaja" visible="false">
                                                                <h6>Fecha de baja:</h6>
                                                                <input type="text" id="InputFechaBaja" class="form-control border-dark shadow Add-text" autocomplete="off" runat="server">
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <h6>Estado del equipo:</h6>
                                                            <asp:DropDownList ID="DropEstado" runat="server" class="form-select border-dark shadow">
                                                                <asp:ListItem Value="0">Pendiente de entrada</asp:ListItem>
                                                                <asp:ListItem Value="1">Funcional</asp:ListItem>
                                                                <asp:ListItem Value="2">Retirado</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <h6>Ubicacion:</h6>
                                                            <div class="input-group shadow">
                                                                <span class="input-group-text border-dark" id="basic-addon1"><i class="bi bi-geo-alt"></i></span>
                                                                <input class="form-select border-dark shadow" list="DatalistUbicacion" id="UbicacionEquipo" runat="server" autocomplete="off">
                                                                <datalist id="DatalistUbicacion" runat="server">
                                                                </datalist>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row mt-3">
                                                        <div class="col-lg-6">
                                                            <h6>Notas:</h6>
                                                            <textarea id="InputNotasEquipo" runat="server" class="form-control border-dark shadow" placeholder="Notas del equipo." rows="2"></textarea>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <h6>Alertas:</h6>
                                                            <label id="AlertaActivo" runat="server" visible="false">
                                                                - El equipo está activo/está inactivo/fuera de uso.<br />
                                                            </label>
                                                            <label id="AlertaCalibrado" runat="server" visible="false">
                                                                - La calibración del equipo ha vencido.<br />
                                                            </label>
                                                            <label id="AlertaProxCalibracion" runat="server" visible="false">
                                                                - La próxima calibración está prevista para.<br />
                                                            </label>
                                                            <label id="AlertaMSA" runat="server" visible="false">
                                                                - El equipo no tiene MSA vinculado.<br />
                                                            </label>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark text-white fw-bolder" style="background-color: darkorange">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-eye me-2"></i>Datos del equipo</h5>
                                                    </div>
                                                </div>
                                                <div class="row mt-2">
                                                    <div class="col-lg-4">
                                                        <h6>Tipo:</h6>
                                                        <asp:DropDownList ID="DropTipoEquipo" runat="server" class="form-select border-dark shadow">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <h6>Rango:</h6>
                                                        <input type="text" id="InputRango" class="form-control border-dark shadow" autocomplete="off" runat="server">
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <h6>División de escala:</h6>
                                                        <input type="text" id="InputEscala" class="form-control border-dark shadow" autocomplete="off" runat="server">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4 mt-2">
                                                        <h6 class="mt-2">Usuario:</h6>
                                                        <asp:DropDownList ID="DropDepartamento" runat="server" class="form-select border-dark shadow">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-4 mt-2">
                                                        <h6 class="mt-2">Fabricante:</h6>
                                                        <input class="form-control border-dark shadow" list="DatalistFabricante" id="InputFabricante" runat="server" autocomplete="off">
                                                        <datalist id="DatalistFabricante" runat="server">
                                                        </datalist>
                                                    </div>
                                                    <div class="col-lg-4 mt-2">
                                                        <h6 class="mt-2">Propietario:</h6>
                                                        <input class="form-control border-dark shadow" list="DatalistPropietario" id="InputPropietario" runat="server" autocomplete="off">
                                                        <datalist id="DatalistPropietario" runat="server">
                                                        </datalist>
                                                    </div>
                                                </div>
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark text-white fw-bolder" style="background-color: darkorange">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-alt me-2"></i>Equipo y procesos alternativo</h5>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-5 mt-2">
                                                        <h6 class="mt-2">Equipo/Proceso:</h6>
                                                        <input class="form-select border-dark shadow" list="DatalistAlternativos" id="tbEquipoAlternativo" runat="server" autocomplete="off" maxlength="50">
                                                        <datalist id="DatalistAlternativos" runat="server">
                                                        </datalist>
                                                    </div>
                                                    <div class="col-lg-7 mt-2">
                                                        <h6 class="mt-2">Detalles:</h6>
                                                        <textarea id="tbNotasAlternativo" runat="server" class="form-control border-dark shadow" placeholder="Detalles del equipo/proceso." rows="2"></textarea>

                                                    </div>
                                                </div>

                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark text-white fw-bolder" style="background-color: darkorange">
                                                    <div class="col-sm-6">
                                                        <h5 class="mt-1"><i class="bi bi-tv me-2"></i>Productos vinculados</h5>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h5 class="mt-1"><i class="bi bi-sliders me-2"></i>Análisis del sistema de medición</h5>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <button id="BotonAuxiliarAgregarProducto" runat="server" type="button" class="btn btn-primary btn-sm" onserverclick="AbreAgregarProductos" style="width: 100%">Añadir Producto Vinculado</button>

                                                    <asp:GridView ID="GridProductos" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded rounded-2 border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                        <RowStyle BackColor="White" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                                                <HeaderTemplate>
                                                                    <button id="IdAbreAñadirProductos" runat="server" type="button" class="btn btn-outline-dark btn-sm bg-white" onserverclick="AbreAgregarProductos"><i class="bi bi-plus-circle"></i></button>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="BorrarEquipoXProducto" CssClass="btn btn-danger btn-sm me-md-1 shadow" runat="server" OnClientClick="return confirm('Se eliminará esta linea. ¿Seguro que quieres continuar? ');" CommandName="EliminarProductoXEquipo" CommandArgument='<%#Eval("Id")%>'><i class="bi bi-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="left" Visible="true" ItemStyle-BackColor="#e6e6e6" HeaderText="Producto">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProducto" runat="server" Font-Bold="true" Text='<%#Eval("REFERENCIA") %>' />
                                                                    <asp:Label ID="lblDescripcion" runat="server" CssClass="ms-1" Text='<%#Eval("DESCRIPCION") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Image ID="IMGliente" runat="server" Width="50px" src='<%#Eval("ClienteLogo") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:DropDownList ID="DropMSA" runat="server" class="form-select border-dark shadow">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">Por equipo</asp:ListItem>
                                                        <asp:ListItem Value="2">Por familia</asp:ListItem>
                                                        <asp:ListItem Value="3">N/A</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <button id="BotonAuxiliarMSA" runat="server" type="button" class="btn btn-primary btn-sm" onserverclick="AbreMSA" style="width: 100%">Añadir MSA</button>
                                                    <div style="overflow-y: auto; height: 250px">
                                                        <asp:GridView ID="GridMSA" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                            <RowStyle BackColor="White" />
                                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                                            <Columns>

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                                                    <HeaderTemplate>
                                                                        <button id="IdAbreMSA" runat="server" type="button" class="btn btn-outline-dark btn-sm bg-white" onserverclick="AbreMSA"><i class="bi bi-plus-circle"></i></button>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href='<%#Eval("URLDocumento")%>' class="btn btn-primary btn-sm"><i class="bi bi-file-post"></i></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Tipo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNumEquipo" runat="server" Font-Bold="true" Text='<%#Eval("TipoMSA") %>' /><br />
                                                                        <asp:Label ID="lblDescripcion" runat="server" Font-Size="Small" Font-Italic="true" Text='<%#Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle" HeaderText="Fecha">
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblFecha" runat="server" Font-Bold="true" Text='<%#Eval("FechaDoc", "{0:dd/MM/yyyy}") %>' /><br />
                                                                        <asp:Label ID="lblNombre" runat="server" Font-Size="Small" Text='<%#Eval("Nombre") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="CENTER" ItemStyle-VerticalAlign="Middle" HeaderText="Result.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblResultado" runat="server" Text='<%#Eval("Resultado") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="CENTER" ItemStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="EditaMSA" CssClass="btn btn-primary btn-sm me-md-1" runat="server" CommandName="EditarMSA" CommandArgument='<%#Eval("URLDocumento")%>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>

                                                                        <asp:LinkButton ID="BorrarMSA" CssClass="btn btn-danger btn-sm me-md-1" runat="server" OnClientClick="return confirm('Se eliminará este documento. ¿Seguro que quieres continuar? ');" CommandName="EliminarMSA" CommandArgument='<%#Eval("URLDocumento")%>'><i class="bi bi-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark text-white fw-bolder" style="background-color: darkorange">
                                                    <div class="col-lg-12">
                                                        <h5 class="mt-1"><i class="bi bi-camera me-2"></i>Calibraciones</h5>
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <button id="BotonAuxiliarCalibracion" runat="server" type="button" class="btn btn-primary btn-sm" onserverclick="AbreCalibracion" visible="false" style="width: 100%">Añadir Calibracion</button>
                                                    <asp:GridView ID="GridCalibracion" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                        <RowStyle BackColor="White" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                                                <HeaderTemplate>
                                                                    <button id="IdAbreCalibracion" runat="server" type="button" class="btn btn-outline-dark btn-sm bg-white" onserverclick="AbreCalibracion"><i class="bi bi-plus-circle"></i></button>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href='<%#Eval("URLDocumento")%>' class="btn btn-primary btn-sm"><i class="bi bi-file-post"></i></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Tipo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipoCal" runat="server" Font-Bold="true" Text='<%#Eval("TipoCalibracionCHAR") %>' /><br />
                                                                    <asp:Label ID="lblDescripcion" runat="server" Font-Size="Small" Font-Italic="true" Text='<%#Eval("Descripcion") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle" HeaderText="Fecha">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFecha" runat="server" Font-Bold="true" Text='<%#Eval("FechaDoc", "{0:dd/MM/yyyy}") %>' /><br />
                                                                    <asp:Label ID="lblResultado" runat="server" Font-Size="Small" Text='<%#Eval("INT_EXT") + " (" +Eval("EntidadCertificadora")+")" %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle" HeaderText="Resultado">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblResultadoObtenido" runat="server" Font-Bold="true" Text='<%#Eval("ResultadoObtenido") %>' /><br />
                                                                    <asp:Label ID="lblCriterio" runat="server" Font-Size="Small" Text='<%#Eval("CriterioChar") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="CENTER" ItemStyle-VerticalAlign="Middle" HeaderText="Valoración">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblResultadoCalibracion" runat="server" Font-Bold="true" Text='<%#Eval("ResultadoCalibracion") %>' /><br />
                                                                    <asp:Label ID="lblNombre" runat="server" Font-Size="Small" Text='<%#Eval("Nombre") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="CENTER" ItemStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="EditaMSA" CssClass="btn btn-primary btn-sm me-md-1" runat="server" CommandName="EditarCalibracion" CommandArgument='<%#Eval("URLDocumento")%>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="BorrarMSA" CssClass="btn btn-danger btn-sm me-md-1" runat="server" OnClientClick="return confirm('Se eliminará este documento. ¿Seguro que quieres continuar? ');" CommandName="EliminarCalibracion" CommandArgument='<%#Eval("URLDocumento")%>'><i class="bi bi-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-lg-4 mt-2">
                                                    <h6>Proxima calibración:</h6>
                                                    <input type="text" id="InputProximaCalibracion" class="form-control border-dark shadow mb-2" disabled="disabled" autocomplete="off" runat="server">
                                                    <h6>Frecuencia (años):</h6>
                                                    <asp:DropDownList ID="DropFrecuenciaCalibracion" runat="server" class="form-select border-dark shadow mb-2">
                                                        <asp:ListItem Value="0">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <input type="text" id="InputUltimaCalibracion" class="form-control Add-text" hidden="hidden" autocomplete="off" runat="server">
                                                    <h6>Tipo de calibracion:</h6>
                                                    <asp:DropDownList ID="DropTipoCalibracion" runat="server" class="form-select border-dark shadow mb-2">
                                                        <asp:ListItem Value="-"></asp:ListItem>
                                                        <asp:ListItem Value="INTERNA">INTERNA</asp:ListItem>
                                                        <asp:ListItem Value="EXTERNA">EXTERNA</asp:ListItem>
                                                        <asp:ListItem Value="N/A">N/A</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <h6>Medio de calibración:</h6>
                                                    <input type="text" id="InputMedioCalibracion" class="form-control border-dark shadow mb-2" autocomplete="off" runat="server">

                                                    <h6>Criterio aceptación:</h6>
                                                    <asp:DropDownList ID="DropCriterioAceptacion" runat="server" class="form-select border-dark shadow mb-2">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">1/10 < 2I/T < 1/3</asp:ListItem>
                                                        <asp:ListItem Value="2">10 >T/2U > 3</asp:ListItem>
                                                        <asp:ListItem Value="3">Dim. aceptado</asp:ListItem>
                                                        <asp:ListItem Value="4">Validación con patrón</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="v-pills-docus" role="tabpanel" aria-labelledby="v-pills-home-docus">
                                        <div class="container-fluid">

                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="row mt-2 mb-1 shadow rounded-2 border border-dark text-white fw-bolder" style="background-color: darkorange">
                                                        <div class="col-sm-12">
                                                            <h5 class="mt-1"><i class="bi bi-file-earmark-zip me-2"></i>Documentos del equipo</h5>
                                                        </div>
                                                    </div>
                                                    <button id="AUXAÑADIRDOCAUX" runat="server" type="button" class="btn btn-primary btn-sm shadow" onserverclick="AbreDOCAUX" style="width: 100%" visible="false">Añadir documento</button>
                                                    <div style="overflow-y: auto; height: 250px">
                                                        <asp:GridView ID="GridDOCAUX" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                            <RowStyle BackColor="White" />
                                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                                            <Columns>

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                                                    <HeaderTemplate>
                                                                        <button id="IdAbreDOCAUX" runat="server" type="button" class="btn btn-outline-dark btn-sm bg-white" onserverclick="AbreDOCAUX"><i class="bi bi-plus-circle"></i></button>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href='<%#Eval("URLDocumento")%>' class="btn btn-primary btn-sm"><i class="bi bi-file-post"></i></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Tipo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNumEquipo" runat="server" Font-Bold="true" Text='<%#Eval("TipoDOC") %>' /><br />
                                                                        <asp:Label ID="lblDescripcion" runat="server" Font-Size="Small" Font-Italic="true" Text='<%#Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle" HeaderText="Fecha">
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblFecha" runat="server" Font-Bold="true" Text='<%#Eval("FechaDoc", "{0:dd/MM/yyyy}") %>' /><br />
                                                                        <asp:Label ID="lblNombre" runat="server" Font-Size="Small" Text='<%#Eval("Nombre") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="CENTER" ItemStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="EditaDOCAUX" CssClass="btn btn-primary btn-sm me-md-1" runat="server" CommandName="EditarDOCAUX" CommandArgument='<%#Eval("URLDocumento")%>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="BorrarDOCAUX" CssClass="btn btn-danger btn-sm me-md-1" runat="server" OnClientClick="return confirm('Se eliminará este documento. ¿Seguro que quieres continuar? ');" CommandName="EliminarDOCAUX" CommandArgument='<%#Eval("URLDocumento")%>'><i class="bi bi-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark text-white fw-bolder" style="background-color: darkorange">
                                                        <div class="col-sm-12">
                                                            <h5 class="mt-1"><i class="bi bi-file-earmark me-2"></i>Procedimientos e instrucciones</h5>
                                                        </div>
                                                    </div>
                                                    <a href="../FPOCS/Poc.10 Control equipos  Ed 13.pdf" class="mt-4" style="font: bold; font-size: large; color: black; text-decoration: none"><i class="bi bi-file-spreadsheet ms-4 me-2"></i><strong><i>Poc.10:</i></strong> Control equipos - Ed.13</a><br />
                                                    <a href="../FPOCS/ITGs-95 Calibración reloj comparador.pdf" class="mt-4" style="font: bold; font-size: large; color: black; text-decoration: none"><i class="bi bi-file-pdf ms-4 me-2"></i><strong><i>ITGs-95:</i></strong> Calibración reloj comparador - Ed.00</a><br />
                                                    <a href="../FPOCS/ITGs-98 Calibración pie de rey ED01.pdf" class="mt-4" style="font: bold; font-size: large; color: black; text-decoration: none"><i class="bi bi-file-pdf ms-4 me-2"></i><strong><i>ITGs-98:</i></strong> Calibración pie de rey - Ed.01</a><br />

                                                    <div class="row mt-2 mb-1 ms-2 mt-2 shadow rounded-2 border border-dark text-white fw-bolder" style="background-color: darkorange">
                                                        <div class="col-sm-12">
                                                            <h5 class="mt-1"><i class="bi bi-file-earmark me-2"></i>Formatos de proceso</h5>
                                                        </div>
                                                    </div>
                                                    <a href="../FPOCS/Listado Equipos alternativos de control.xlsx" class="mt-4" style="font: bold; font-size: large; color: black; text-decoration: none" hidden="hidden" ><i class="bi bi-file-earmark-font ms-4 me-2"></i><strong><i>FPOC-10-07:</i></strong> Listado Equipos alternativos de control</a><br />
                                                    <a href="../FPOCS/FPOC-10-05 - Plantilla R&R Kappa Atributos.xls" class="mt-4" style="font: bold; font-size: large; color: black; text-decoration: none"><i class="bi bi-file-earmark-font ms-4 me-2"></i><strong><i>FPOC-10-05:</i></strong> Plantilla R&R Kappa Atributos</a><br />
                                                    <a href="../FPOCS/FPOC-10-06 - Plantilla R&R Cota Variables.xls" class="mt-4" style="font: bold; font-size: large; color: black; text-decoration: none"><i class="bi bi-file-earmark-font ms-4 me-2"></i><strong><i>FPOC-10-06:</i></strong> Plantilla R&R Cota Variables</a><br />
                                                    <a href="../FPOCS/FPOC-10-10 - Calibración PIE DE REY.xls" class="mt-4" style="font: bold; font-size: large; color: black; text-decoration: none"><i class="bi bi-file-earmark-font ms-4 me-2"></i><strong><i>FPOC-10-10:</i></strong> Calibración PIE DE REY</a><br />
                                                    <a href="../FPOCS/FPOC-10-11 - Calibración COMPARADOR.xls" class="mt-4" style="font: bold; font-size: large; color: black; text-decoration: none"><i class="bi bi-file-earmark-font ms-4 me-2"></i><strong><i>FPOC-10-11:</i></strong> Calibración COMPARADOR</a><br />
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
                    <button class="btn btn-success" type="button" runat="server" id="btnActualizarEquipo" onserverclick="Actualizar_equipo"><i class="bi bi-sd-card"></i></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalMSAGestion" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalMSAGestion" aria-hidden="false">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="H1MSA" runat="server">Análisis del sistema de medición</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

                </div>
                <div class="modal-body" runat="server">
                    <div>
                        <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                            <div class="d-flex align-items-start">
                                <div class="nav flex-column nav-pills me-2" id="v-pills-tabMSA" role="tablist" aria-orientation="vertical">
                                    <button id="TABACCIONMSA" class="nav-link active" data-bs-toggle="pill" data-bs-target="#v-pills-home" type="button" role="tab" aria-controls="v-pills-homeMSA" aria-selected="true"><i class="bi bi-kanban"></i></button>
                                </div>
                                <div class="tab-content" id="v-pills-tabContentMSA">
                                    <div class="tab-pane fade show active" id="v-pills-homeMSA" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>
                                                            <label>Datos del documento</label>
                                                            <label></label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <h6>Tipo de documento:</h6>
                                                        <asp:DropDownList ID="DropTipoMSA" runat="server" class="form-select">
                                                            <asp:ListItem Value="0">-</asp:ListItem>
                                                            <asp:ListItem Value="1">R&R por atributos</asp:ListItem>
                                                            <asp:ListItem Value="2">R&R por variables</asp:ListItem>
                                                            <asp:ListItem Value="3">Otros</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <h6>Descripción:</h6>
                                                        <input type="text" id="InputDescripcionMSA" class="form-control" maxlength="49" autocomplete="off" runat="server">
                                                    </div>

                                                    <div class="col-lg-4">
                                                        <h6>Fecha del documento:</h6>
                                                        <input type="text" id="InputFechaMSA" class="form-control Add-text" autocomplete="off" runat="server">
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <h6>Resultado:</h6>
                                                        <asp:DropDownList ID="DropResultadoMSA" runat="server" class="form-select">
                                                            <asp:ListItem>APTO</asp:ListItem>
                                                            <asp:ListItem>APTO CON RESERVAS</asp:ListItem>
                                                            <asp:ListItem>NO APTO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <h6>Aprobado por:</h6>
                                                        <asp:DropDownList ID="DropAprobadoMSA" runat="server" class="form-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="AUXMSAURL" runat="server" />
                                                <div class="row" runat="server" id="RowAdjuntoMSA">
                                                    <div class="col-lg-12">
                                                        <h6>Adjunto:</h6>
                                                        <div class="input-group">

                                                            <asp:FileUpload ID="UploadMSA" runat="server" class="form-control"></asp:FileUpload>
                                                            <button class="btn btn-outline-secondary" type="button" runat="server" id="BTNUploadMSA" onserverclick="Insertar_documento">Subir</button>
                                                        </div>
                                                    </div>
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
                    <button type="button" class="btn btn-success" id="BTNActualizarMSA" runat="server" onserverclick="Actualizar_MSA"><i class="bi bi-sd-card"></i></button>

                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalCalibración" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalMSAGestion" aria-hidden="false">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="H1Calibracion" runat="server">Calibración</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" runat="server">
                    <div>
                        <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                            <div class="d-flex align-items-start">
                                <div class="nav flex-column nav-pills me-2" id="v-pills-tabCALIB" role="tablist" aria-orientation="vertical">
                                    <button id="TABACCIONCALIB" class="nav-link active" data-bs-toggle="pill" data-bs-target="#v-pills-CALIB" type="button" role="tab" aria-controls="v-pills-homeCALIB" aria-selected="true"><i class="bi bi-kanban"></i></button>
                                </div>
                                <div class="tab-content" id="v-pills-tabContentCALIB">
                                    <div class="tab-pane fade show active" id="v-pills-homeCALIB" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>
                                                            <label>Datos del documento</label>
                                                            <label></label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4 mt-2">
                                                        <h6>Tipo de documento:</h6>
                                                        <asp:DropDownList ID="DropCalibracionDocTIPO" runat="server" class="form-select shadow">
                                                            <asp:ListItem Value="0">-</asp:ListItem>
                                                            <asp:ListItem Value="1">Certificado de calibración</asp:ListItem>
                                                            <asp:ListItem Value="2">Dimensional</asp:ListItem>
                                                            <asp:ListItem Value="3">Otros</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-8 mt-2">
                                                        <h6>Descripción:</h6>
                                                        <input type="text" id="inputCalibracionDescripcion" class="form-control shadow" maxlength="49" autocomplete="off" runat="server">
                                                    </div>
                                                    <div class="col-lg-3 mt-2">
                                                        <h6>Tipo de calibración:</h6>
                                                        <asp:DropDownList ID="DropInputINTEXT" runat="server" class="form-select shadow">
                                                            <asp:ListItem>INTERNO</asp:ListItem>
                                                            <asp:ListItem>EXTERNO</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 mt-2">
                                                        <h6>Entidad certificadora:</h6>
                                                        <input type="text" id="inputEntidadCalibracion" class="form-control shadow" maxlength="19" autocomplete="off" runat="server">
                                                    </div>
                                                    <div class="col-lg-3 mt-2">
                                                        <h6>Fecha del documento:</h6>
                                                        <input type="text" id="inputFechaCalibracion" class="form-control shadow Add-text" autocomplete="off" runat="server">
                                                    </div>

                                                    <div class="col-lg-3 mt-2">
                                                        <h6>Criterio:</h6>
                                                        <asp:DropDownList ID="DropCalibracionCriterio" runat="server" class="form-select shadow">
                                                            <asp:ListItem Value="0">-</asp:ListItem>
                                                            <asp:ListItem Value="1">1/10 < 2I/T < 1/3</asp:ListItem>
                                                            <asp:ListItem Value="2">10 >T/2U > 3</asp:ListItem>
                                                            <asp:ListItem Value="3">Dim. aceptado</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 mt-2">
                                                        <h6>Valor obtenido:</h6>
                                                        <input type="number" id="inputCalibracionValor" class="form-control  shadow" autocomplete="off" runat="server">
                                                    </div>
                                                    <div class="col-lg-3 mt-2">
                                                        <h6>Aprobado por:</h6>
                                                        <asp:DropDownList ID="DropAprobadoCalibracion" runat="server" class="form-select shadow">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="AUXCalibracionURL" runat="server" />
                                                <div class="row" runat="server" id="RowCalibracionAdjunto">
                                                    <div class="col-lg-12 mt-2">
                                                        <h6>Adjunto:</h6>
                                                        <div class="input-group shadow">
                                                            <asp:FileUpload ID="UploadCalibracion" runat="server" class="form-control "></asp:FileUpload>
                                                            <button class="btn btn-outline-secondary" type="button" runat="server" id="BTNUploadCalibracion" onserverclick="Insertar_documento">Subir</button>
                                                        </div>
                                                    </div>
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
                    <button type="button" class="btn btn-success" id="BTNActualizarCalibracion" runat="server" onserverclick="Actualizar_Calibracion"><i class="bi bi-sd-card"></i></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalDocumentosAuxiliares" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalDocumentosAuxiliares" aria-hidden="false">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="H1DOCAUX" runat="server">Documentos auxiliares</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" runat="server">
                    <div>
                        <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                            <div class="d-flex align-items-start">
                                <div class="nav flex-column nav-pills me-2" id="v-pills-tabDOCS" role="tablist" aria-orientation="vertical">
                                    <button id="TABDOCUMENTOSAUXILIARES" class="nav-link active" data-bs-toggle="pill" data-bs-target="#v-pills-CALIB" type="button" role="tab" aria-controls="v-pills-homeCALIB" aria-selected="true"><i class="bi bi-kanban"></i></button>
                                </div>
                                <div class="tab-content" id="v-pills-tabContentDOCS">
                                    <div class="tab-pane fade show active" id="v-pills-homeDOCS" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>
                                                            <label>Datos del documento</label>
                                                            <label></label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <h6>Tipo de documento:</h6>
                                                        <asp:DropDownList ID="DropTipoDOCAUX" runat="server" class="form-select">
                                                            <asp:ListItem Value="0">-</asp:ListItem>
                                                            <asp:ListItem Value="5">Instrucción equipo alternativo</asp:ListItem>
                                                            <asp:ListItem Value="1">Manual de uso</asp:ListItem>
                                                            <asp:ListItem Value="2">Manual de mantenimiento</asp:ListItem>
                                                            <asp:ListItem Value="3">Planos/CAD de equipo</asp:ListItem>
                                                            <asp:ListItem Value="4">Otros</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <h6>Descripción:</h6>
                                                        <input type="text" id="tbDescripcionDOCAUX" class="form-control" maxlength="49" autocomplete="off" runat="server">
                                                    </div>

                                                    <div class="col-lg-3">
                                                        <h6>Fecha del documento:</h6>
                                                        <input type="text" id="tbFechaDOCAUX" class="form-control Add-text" autocomplete="off" runat="server">
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <h6>Aprobado por:</h6>
                                                        <asp:DropDownList ID="dropAprobadoDOCAUX" runat="server" class="form-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <asp:HiddenField ID="AUXDOCAUXURL" runat="server" />
                                                        <div class="row" id="RowAdjuntoDOCAUX" runat="server">
                                                            <h6>Adjunto:</h6>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="UploadDOCAUX" runat="server" class="form-control"></asp:FileUpload>
                                                                <button class="btn btn-outline-secondary" type="button" runat="server" id="BTNUploadDOCAUX" onserverclick="Insertar_documento">Subir</button>
                                                            </div>
                                                        </div>
                                                    </div>
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
                    <button type="button" class="btn btn-success" id="BTNActualizarDOCAUX" runat="server" onserverclick="Actualizar_DOCAUX"><i class="bi bi-sd-card"></i></button>

                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalAgregarProductos" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalAgregarProductos" aria-hidden="false">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="H1AgregarDOCS" runat="server">Agregar producto</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" runat="server">
                    <div>
                        <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                            <div class="d-flex align-items-start">
                                <div class="nav flex-column nav-pills me-2" id="v-pills-tabPROD" role="tablist" aria-orientation="vertical">
                                    <button id="TABACCIONPROD" class="nav-link active" data-bs-toggle="pill" data-bs-target="#v-pills-PROD" type="button" role="tab" aria-controls="v-pills-homeCALIB" aria-selected="true"><i class="bi bi-kanban"></i></button>
                                </div>
                                <div class="tab-content" id="v-pills-tabContentPROD">
                                    <div class="tab-pane fade show active" id="v-pills-homePROD" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>
                                                            <label>Agregar Producto</label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 mt-2">
                                                        <h6>Producto:</h6>
                                                        <div class="input-group mb-3">
                                                            <input class="form-control" list="DatalistProductos" id="InputAgregarProducto" runat="server" autocomplete="off" placeholder="Escribe un equipo...">
                                                            <datalist id="DatalistProductos" runat="server">
                                                            </datalist>
                                                            <button class="btn btn-outline-secondary" type="button" id="BTNAgregarProducto" runat="server" onserverclick="AgregarProductos">Agregar</button>
                                                        </div>
                                                    </div>

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
    <div class="modal fade" id="ModalEquiposXProductos" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalEquiposXProductos" aria-hidden="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="HEquiposXProductos" runat="server"></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" runat="server">
                    <div>
                        <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                            <asp:GridView ID="GridEquiposXProductos" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler" EmptyDataText="Sin equipos vinculados." EmptyDataRowStyle-BackColor="White">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#eeeeee" />

                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" Visible="true" ItemStyle-BackColor="#e6e6e6" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="DetalleCalib" CommandName="FichaCalibracion" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" CssClass="btn btn-sm btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" Visible="true">
                                        <ItemTemplate>
                                            <asp:Image ID="IMGliente" runat="server" Width="59px" src='<%#Eval("LogotipoSM") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Width="80%" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProducto" runat="server" Font-Bold="true" Text='<%#Eval("NUMEQUIPO") %>' /><br />
                                            <asp:Label ID="lblNombre" runat="server" Font-Size="Small" Text='<%#Eval("Nombre") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
                <div class="modal-footer" style="background: #e6e6e6">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalNuevoEquipo" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalMSAGestion" aria-hidden="false">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="H1" runat="server">Agregar nuevo equipo</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" runat="server">
                    <div>
                        <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                            <div class="container-fluid ">
                                <div class="row mt-2 mb-1 ms-2 me-2 shadow rounded-2 border border-dark" style="background-color: orange">
                                    <div class="col-sm-12">
                                        <h5 class="mt-1" style="color: white"><i class="bi bi-info-square me-2"></i>
                                            <label style="color: white">Datos del equipo</label>
                                        </h5>
                                    </div>
                                </div>
                                <div class="row ms-2 me-2">
                                    <div class="col-lg-4">
                                        <h6>Número de equipo</h6>
                                        <div class="input-group mb-2 shadow">
                                            <span class="input-group-text fw-bold border-dark">Propuesto:</span>
                                            <input type="number" id="txtNuevoEquipo" class="form-control bg-white border-dark" autocomplete="off" runat="server">
                                        </div>
                                        <div class="input-group mb-2" hidden="hidden">
                                            <span class="input-group-text">Personalizado:</span>
                                            <input type="number" id="txtNuevoEquipoAlternativo" class="form-control shadow" autocomplete="off" runat="server">
                                        </div>
                                    </div>
                                    <div class="col-lg-8">
                                        <h6>Descripción:</h6>
                                        <input type="text" id="txtNuevaDescripcion" class="form-control bg-white border-dark shadow" maxlength="49" autocomplete="off" runat="server">
                                    </div>
                                </div>
                                <div class="row ms-2 me-2 mb-4">
                                    <div class="col-lg-4">
                                        <h6>Tipo de equipo:</h6>
                                        <asp:DropDownList ID="DropNuevoTipoEquipo" runat="server" class="form-select border-dark shadow">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <h6>Estado del equipo:</h6>
                                        <asp:DropDownList ID="DropNuevoEquipoEstado" runat="server" class="form-select border-dark shadow">
                                            <asp:ListItem Value="0">Pendiente de entrada</asp:ListItem>
                                            <asp:ListItem Value="1">Funcional</asp:ListItem>
                                            <asp:ListItem Value="2">Retirado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <h6>Ubicacion:</h6>
                                        <div class="input-group shadow">
                                            <span class="input-group-text border-dark" id="basic-addon1"><i class="bi bi-geo-alt"></i></span>
                                            <input class="form-select border-dark shadow" list="DatalistUbicacion" id="txtNuevoUbicacion" runat="server" autocomplete="off">
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-2 mb-1 ms-2 me-2 shadow rounded-2 border border-dark" style="background-color: orange">
                                    <div class="col-sm-12">
                                        <h5 class="mt-1" style="color: white"><i class="bi bi-info-square me-2"></i>
                                            <label style="color: white">Calibración y MSA</label>
                                        </h5>
                                    </div>
                                </div>
                                <div class="row ms-2 me-2">
                                    <div class="col-lg-3">
                                        <h6>Frecuencia de calibración:</h6>
                                        <div class="input-group mb-2 shadow">
                                            <input type="number" id="txtNuevoFrecuenciaCal" class="form-control border-dark" autocomplete="off" runat="server" value="1" min="1">
                                            <span class="input-group-text border-dark">años</span>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <h6>Tipo de calibración:</h6>
                                        <asp:DropDownList ID="DropNuevoTipoCalibracion" runat="server" class="form-select border-dark shadow">
                                            <asp:ListItem>EXTERNA</asp:ListItem>
                                            <asp:ListItem>INTERNA</asp:ListItem>
                                            <asp:ListItem>N/A</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <h6>Medio de calibración:</h6>
                                        <input type="text" id="txtNuevoMedioCalibracion" class="form-control border-dark shadow mb-2" autocomplete="off" runat="server">
                                    </div>
                                    <div class="col-lg-3">
                                        <h6>Criterio de aceptación:</h6>
                                        <asp:DropDownList ID="DropNuevoCriterioCalibracion" runat="server" class="form-select border-dark shadow">
                                            <asp:ListItem Value="0">-</asp:ListItem>
                                            <asp:ListItem Value="1">1/10 < 2I/T < 1/3</asp:ListItem>
                                            <asp:ListItem Value="2">10 >T/2U > 3</asp:ListItem>
                                            <asp:ListItem Value="3">Dim. aceptado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row ms-2 me-2 mb-4">
                                    <div class="col-lg-3">
                                        <h6>Tipo de MSA:</h6>
                                        <asp:DropDownList ID="DropNuevoTipoMSA" runat="server" class="form-select border-dark shadow">
                                            <asp:ListItem Value="0">-</asp:ListItem>
                                            <asp:ListItem Value="1">Por equipo</asp:ListItem>
                                            <asp:ListItem Value="2">Por familia</asp:ListItem>
                                            <asp:ListItem Value="3">N/A</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row mt-2 mb-1 ms-2 me-2 shadow rounded-2 border border-dark" style="background-color: orange">
                                    <div class="col-sm-12">
                                        <h5 class="mt-1" style="color: white"><i class="bi bi-info-square me-2"></i>
                                            <label style="color: white">Otros</label>
                                        </h5>
                                    </div>
                                </div>
                                <div class="row ms-2 me-2">
                                    <div class="col-lg-12 ">
                                        <h6>Notas:</h6>
                                        <textarea id="txtNuevoNotas" runat="server" class="form-control border-dark shadow" placeholder="Notas del equipo." rows="2"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer" style="background: #e6e6e6">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                    <button type="button" class="btn btn-success" id="Button3" runat="server" onserverclick="InsertarEquipo"><i class="bi bi-sd-card"></i></button>
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
            <div class="d-flex flex-column">
                <h6>Estado:</h6>
                <div class="form-check form-switch ms-1 mb-3">
                    <input class="form-check-input" type="checkbox" runat="server" id="SwitchActivas" checked="checked">
                    <label class="form-check-label" for="SwitchActivas">Ocultar equipos obsoletos</label>
                </div>

                <h6>Equipo:</h6>
                <div class="input-group mb-3">
                    <input class="form-control" list="DatalistEquipos" id="selectEquipo" runat="server" autocomplete="off" placeholder="Escribe un equipo...">
                    <datalist id="DatalistEquipos" runat="server">
                    </datalist>
                </div>

                <h6>Tipo:</h6>
                <div class="input-group mb-3">
                    <asp:DropDownList ID="Drop_tipos_equipos_filtro" runat="server" class="form-select">
                    </asp:DropDownList>
                </div>


                <div class="input-group mb-3">
                    <button id="Button2" runat="server" onserverclick="Rellenar_grid" type="button" class="btn btn-secondary" style="width: 100%; text-align: left">
                        <i class="bi bi-search me-2"></i>Filtrar</button>
                </div>
                <h5>Ver más:</h5>
                <h6>Equipos por referencia:</h6>
                <div class="input-group mb-3">
                    <input class="form-control" list="DatalistReferencias" id="selectReferencia" runat="server" autocomplete="off" placeholder="Escribe una referencia...">
                    <datalist id="DatalistReferencias" runat="server">
                    </datalist>
                    <button id="ConsultaReferencia" runat="server" onserverclick="Devuelve_EquiposXProducto" type="button" class="btn btn-outline-secondary" style="text-align: left">
                        <i class="bi bi-search"></i>
                    </button>
                </div>

            </div>
        </div>
        <div class="offcanvas-end bg-dark ">
            <div class="mt-2 mb-2">
                <a href="../SMARTH_docs/AYUDA/METROLOGIA - Instrucciones App.pdf" class="align-middle" style="font: bold; font-size: large; color: white; text-decoration: none"><i class="bi bi-question-circle ms-4 me-2"></i><strong>Documentos de ayuda</strong></a>
            </div>

        </div>
</asp:Content>
