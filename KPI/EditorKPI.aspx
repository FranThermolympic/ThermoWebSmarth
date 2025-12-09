<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EditorKPI.aspx.cs" Inherits="ThermoWeb.KPI.EditorKPI" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Editor KPI</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Editor KPI             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
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

        function ShowPopupEdicion() {
            document.getElementById("AUXMODALEDITARECURSO").click();
        }
        function ShowPopupNuevo() {
            document.getElementById("AUXMODALNUEVORECURSO").click();
        }
        function AlertaFormato(TextoAlerta) {
            alert(TextoAlerta);
        }
    </script>
    <button id="AUXMODALEDITARECURSO" runat="server" type="button" class="btn btn-primary" data-bs-toggle="modal" hidden="hidden" data-bs-target="#ModalEditaRecurso" style="font-size: larger">MSA</button>
    <button id="AUXMODALNUEVORECURSO" runat="server" type="button" class="btn btn-primary" data-bs-toggle="modal" hidden="hidden" data-bs-target="#ModalNuevoRecurso" style="font-size: larger">MSA</button>

    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid mt-2">
            <div class="col-lg-12">

                <div class="tab-content" id="pills-tabContent">
                    <div class="tab-pane fade show active" id="pills_historico" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <div class="row">
                            <div class="col-lg-4">
                                <h5>Indicador</h5>
                                <asp:DropDownList ID="Drop_KPISelect" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="RellenarGrid">
                                    <asp:ListItem Value="22_1">22_1 - OEE Calidad</asp:ListItem>
                                    <asp:ListItem Value="23_1">23_1 - PPM Cliente acumulado</asp:ListItem>
                                    <asp:ListItem Value="24_1">24_1 - Reclamaciones a proveedores</asp:ListItem>
                                    <asp:ListItem Value="25_1">25_1 - Reclamaciones de cliente</asp:ListItem>
                                    <asp:ListItem Value="26_1">26_1 - Coste de reclamaciones</asp:ListItem>
                                    <asp:ListItem Value="31_1">31_1 - OEE General</asp:ListItem>
                                    <asp:ListItem Value="91_1">91_1 - Absentismo</asp:ListItem>
                                    <asp:ListItem Value="101_1">101_1 - %Mant preventivo</asp:ListItem>
                                    <asp:ListItem Value="101_2">101_2 - %Mant preventivo moldes</asp:ListItem>
                                    <asp:ListItem Value="101_3">101_3 - %Mant preventivo máquinas</asp:ListItem>
                                    <asp:ListItem Value="102_1">102_1 - %Mant preventivo</asp:ListItem>
                                    <asp:ListItem Value="102_2">102_2 - %Mant preventivo máquinas</asp:ListItem>
                                    <asp:ListItem Value="102_3">102_3 - %Mant preventivo moldes</asp:ListItem>
                                    <asp:ListItem Value="103_1">103_1 - Costes de mantenimiento</asp:ListItem>
                                    <asp:ListItem Value="XXPS1_1">XXPS1_1 - Aprovechamiento operario</asp:ListItem>
                                </asp:DropDownList>
                                <asp:GridView ID="dgv_AudiovisualKPI" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="GridViewCommandEventHandler"
                                    OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnRowCancelingEdit="gridView_RowCancelingEdit"
                                    EmptyDataText="There are no data records to display.">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#eeeeee" />

                                    <Columns>
                                        <asp:TemplateField ItemStyle-VerticalAlign="Middle">

                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnEdit" CommandName="Edit" CssClass="btn btn-lg btn-primary mt-1 shadow-lg border border-1 border-dark" Style="font-size: 1rem"><i class="bi bi-pencil"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                 <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-danger" CommandName="Cancel"><i class="bi-caret-left-fill"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-success"
                                                CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar esta fila?');"><i class="bi-sd-card"></i></asp:LinkButton>
                                       
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Año">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAño" runat="server" Text='<%#Eval("Año") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="txtAño" Enabled="false" runat="server" Text='<%#Eval("Año") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Simbolo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSimbolo" runat="server" Text='<%#Eval("SIMBOLO") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Textbox ID="txtSimbolo" runat="server" Text='<%#Eval("SIMBOLO") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Objetivo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKPI" Font-Bold="true" runat="server" Text='<%#Eval("KPI") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Textbox ID="txtKPIValor" runat="server" Text='<%#Eval("KPI") %>' />
                                            </EditItemTemplate>
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
            <div class="modal fade" id="ModalEditaRecurso" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalEditaRecurso" aria-hidden="false">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header bg-primary shadow">
                            <h5 class="modal-title text-white" id="H1MSA" runat="server">Editar recurso</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body" runat="server">
                            <div>
                                <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="row mt-2 mb-1 ">
                                                <div class="col-sm-12 ms-2 mb-2 shadow rounded-2 border border-dark bg-white">
                                                    <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>
                                                        Datos del documento
                                                    </h5>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <asp:HiddenField ID="AUXRecursoID" runat="server" />
                                                <asp:HiddenField ID="AUXRecursoTipo" runat="server" />
                                                <asp:HiddenField ID="AUXRecursoURL" runat="server" />
                                                <div class="col-lg-2">
                                                    <h6>Aplicación:</h6>
                                                    <input type="text" id="InputAPP" class="form-control border border-dark shadow" disabled="disabled" autocomplete="off" runat="server">
                                                </div>
                                                <div class="col-lg-7">
                                                    <h6>Descripción:</h6>
                                                    <input type="text" id="InputDescripcion" class="form-control border border-dark shadow" maxlength="75" autocomplete="off" runat="server">
                                                </div>
                                                <div class="col-lg-3">
                                                    <h6>Disponible:</h6>
                                                    <asp:DropDownList ID="DropEstadoDocumento" runat="server" class="form-select border border-dark shadow">
                                                        <asp:ListItem Value="0">Oculto</asp:ListItem>
                                                        <asp:ListItem Value="1">Disponible</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="btn-group border border-dark mt-3" style="width: 100%">
                            <button type="button" class="btn btn-success" id="BTNActualizarRecurso" runat="server" onserverclick="ActualizaDocumento"><i class="bi bi-sd-card">&nbsp Actualizar recurso</i></button>
                            <button type="button" class="btn btn-primary" id="BTNVerRecurso" runat="server" onserverclick="VerDocumento"><i class="bi bi-display">&nbsp Ver recurso</i></button>
                            <button type="button" class="btn btn-danger" id="BTNEliminarRecurso" runat="server" onserverclick="EliminaDocumento"><i class="bi bi-trash">&nbsp Eliminar recurso</i></button>
                        </div>

                    </div>
                </div>
            </div>
            <div class="modal fade" id="ModalNuevoRecurso" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalNuevoRecurso" aria-hidden="false">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header bg-primary shadow">
                            <h5 class="modal-title text-white" id="H1" runat="server">Nuevo recurso</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body" runat="server">
                            <div>
                                <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="row mt-2 mb-1 ">
                                                <div class="col-sm-12 ms-2 mb-2 shadow rounded-2 border border-dark bg-white">
                                                    <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>
                                                        Datos del documento
                                                    </h5>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-lg-2">
                                                    <h6>Aplicación:</h6>
                                                    <input type="text" id="InputNuevaAPP" class="form-control border border-dark shadow" disabled="disabled" autocomplete="off" runat="server">
                                                </div>
                                                <div class="col-lg-7">
                                                    <h6>Descripción:</h6>
                                                    <input type="text" id="InputNuevaDescripcion" class="form-control border border-dark shadow" maxlength="75" autocomplete="off" runat="server">
                                                </div>
                                                <div class="col-lg-3">
                                                    <h6>Disponible:</h6>
                                                    <asp:DropDownList ID="DropDownNuevoEstado" runat="server" class="form-select border border-dark shadow">
                                                        <asp:ListItem Value="0">Oculto</asp:ListItem>
                                                        <asp:ListItem Value="1">Disponible</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-12 mt-2">
                                                    <h6>Adjunto:</h6>
                                                    <div class="input-group bg-white ">
                                                        <asp:FileUpload ID="UploadRecurso" runat="server" class="form-control border border-dark shadow"></asp:FileUpload>
                                                        <button class="btn btn-outline-secondary border border-dark shadow" type="button" runat="server" id="BTNUploadMSA" onserverclick="Insertar_documento">Guardar</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
