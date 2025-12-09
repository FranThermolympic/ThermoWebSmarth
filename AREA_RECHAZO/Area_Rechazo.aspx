<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Area_Rechazo.aspx.cs" Inherits="ThermoWeb.AREA_RECHAZO.Area_Rechazo" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Gestión de jaula de rechazo</title>
    <%-- <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />--%>
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Gestión de jaula de rechazo
              
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function LogErrores() {
            alert("Se ha producido un error. Verifica que los campos fecha de entrada, debe salir, responsable de entrada y responsable salida están rellenos.");
        }
        function LogErrorAsignacion() {
            alert("Debes asignar un Responsable de entrada y un Responsable de salida para continuar");
            document.getElementById("AUXMODALNuevoBloqueo").click();
        }
        function ShowPopupMODALNuevoEquipo() {
            document.getElementById("AUXMODALNuevoBloqueo").click();
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

                yearRange: "c-20:c+10",
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa']
            });
        });
    </script>

    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-1 me-md-3 mb-md-1">
                        <button id="AUXMODALNuevoBloqueo" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalNuevoBloqueo" style="font-size: larger">MSA</button>
                        <button id="BTNImprimir" type="button" class="btn btn-outline-dark btn-primary text-light ms-md-0 bi bi-printer shadow" style="font-size: larger"  onclick="window.print()" ></button>
               
                        <button id="btnoffcanvas" runat="server" type="button" class="btn btn-outline-dark btn-light ms-md-0 bi bi-funnel-fill shadow" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger"></button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="container-fluid">
                        <div class="table-responsive">
                            <asp:GridView ID="dgv_AreaRechazo" runat="server" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border border-secondary" AutoGenerateColumns="false"
                                OnRowUpdating="GridView_RowUpdating" OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridView_RowCommand"
                                EmptyDataText="No hay datos para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <RowStyle BackColor="white" CssClass="shadow" />
                                <EditRowStyle BackColor="#ffcc66" />
                                <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="right" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="5%">
                                        <HeaderTemplate>
                                            <asp:LinkButton runat="server" ID="button2" CommandName="NuevaLinea" UseSubmitBehavior="true" Width="100%" CssClass="btn btn-lg btn-light border border-1 border-dark shadow shadow-sm" Style="font-size: 1rem">
                                          <i class="bi bi-plus-circle"></i></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--Botones de eliminar y editar cliente...--%>

                                            <asp:LinkButton ID="btnEdit" Width="100%" runat="server" CssClass="btn btn-lg btn-primary border border-dark shadow " CommandName="Edit"><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <%--Botones de grabar y cancelar la edición de registro...--%>
                                            <asp:LinkButton ID="btnUpdate" Width="100%" runat="server" Text="Grabar" CssClass="btn  btn-success border border-dark shadow"
                                                CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar esta fila?');"><i class="bi bi-sd-card"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" Width="100%" runat="server" Text="Cancel" CssClass="btn  btn-secondary border border-dark shadow"
                                                CommandName="Cancel"><i class="bi bi-caret-left"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Referencia" HeaderStyle-Width="15%">
                                        <ItemTemplate>

                                            <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Text='<%#Eval("Referencia") %>' /><br />
                                            <asp:Label ID="lblDescripcion" Font-Italic="true" runat="server" Text='<%#Eval("Descripcion") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("Id") %>' />
                                            <asp:TextBox ID="txtReferencia" list="DatalistReferencias" runat="server" CssClass="form-control border border-secondary" Text='<%#Eval("Referencia") %>' />
                                            <asp:DataList ID="DatalistReferencias" runat="server"></asp:DataList>

                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cant." HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidad" Type="number" Width="100%" Font-Size="Large" runat="server" Text='<%#Eval("Cantidad") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCantidad" Type="number" Width="100%" pattern="[0-9]" CssClass="form-control border border-secondary" runat="server" Text='<%#Eval("Cantidad") %>'  />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entrada" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponsableEntrada" Font-Bold="true" runat="server" Text='<%#Eval("ResponsableEntrada") %>' /><br />
                                            <asp:Label ID="lblFechaEntrada" runat="server" Font-Italic="true" Text='<%#"(" + Eval("FechaEntrada", "{0:dd/MM/yyyy}") +")"%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="txtResponsableEntrada" Width="100%" CssClass="form-select-sm" runat="server"></asp:DropDownList>
                                            <asp:TextBox ID="txtFechaEntrada" runat="server" CssClass="form-control-sm border border-secondary Add-text" Text='<%#Eval("FechaEntrada", "{0:dd/MM/yyyy}") %>' />
                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Motivo" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMotivo" runat="server" Text='<%#Eval("Motivo") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMotivo" runat="server" Width="100%" Rows="2" CssClass="form-control border border-secondary" TextMode="MultiLine" Text='<%#Eval("Motivo") %>' />
                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resp. salida" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferenciaResponsableSalida" Font-Bold="true" runat="server" Text='<%#Eval("ResponsableSalida") %>' /><br />
                                            <asp:Label ID="lblDebeSalir" Font-Italic="true" runat="server" Text='<%#"(" + Eval("DebeSalir", "{0:dd/MM/yyyy}") + ")" %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="txtResponsableSalida" Width="100%" CssClass="form-select-sm" runat="server"></asp:DropDownList>
                                            <asp:TextBox ID="txtDebeSalir" runat="server" CssClass="form-control-sm border border-secondary Add-text" Text='<%#Eval("DebeSalir", "{0:dd/MM/yyyy}") %>' />
                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Decisión" HeaderStyle-Width="14%">

                                        <ItemTemplate>
                                            <asp:Label ID="lblDecision" runat="server" Text='<%#Eval("Decision") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDecision" runat="server" CssClass="form-control border border-secondary" Width="100%" Rows="2" TextMode="MultiLine" Text='<%#Eval("Decision") %>' />
                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observaciones" HeaderStyle-Width="14%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObservaciones" runat="server" Text='<%#Eval("Observaciones") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control border border-secondary" Width="100%" Rows="2" TextMode="MultiLine" Text='<%#Eval("Observaciones") %>' />
                                        </EditItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salida" HeaderStyle-Width="7%" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                             <asp:Label ID="lblFechaSalida" runat="server" Font-Bold="true" Text='<%#Eval("FechaSalida", "{0:dd/MM/yyyy}") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnSalida" Width="100%" runat="server" CssClass="btn btn-primary border border-dark shadow" CommandName="Retirar" CommandArgument='<%#Eval("Id") %>'><i class="bi bi-box-arrow-right"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnRecuperar" Width="100%" runat="server" CssClass="btn btn-primary border border-dark shadow" CommandName="Recuperar" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm('¿Seguro que quieres volver a introducir este producto en la jaula?');"><i class="bi bi-box-arrow-left"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" Width="100%" runat="server" CssClass="btn btn-danger border border-dark shadow" CommandName="Borrar" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm('¿Seguro que quieres eliminar esta fila?');"><i class="bi bi-trash"></i></asp:LinkButton>
                                            <asp:Label ID="AUXFechaSalida" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("FechaSalida", "{0:dd/MM/yyyy}") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ModalNuevoBloqueo" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalMSAGestion" aria-hidden="false" data-bs-backdrop="static">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header bg-primary shadow">
                            <h5 class="modal-title text-white" id="H1" runat="server">Bloquear material</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body" runat="server">
                            <div>
                                <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                                    <div class="container-fluid ">
                                        <div class="row mt-2 mb-1 ms-2 me-2 shadow rounded-2 border border-dark" style="background-color: orange">
                                            <div class="col-sm-12">
                                                <h5 class="mt-1" style="color: white"><i class="bi bi-info-square me-2"></i>
                                                    <label style="color: white">Datos del material a bloquear</label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="row ms-2 me-2">
                                            <div class="col-lg-12">
                                                <h6>Producto</h6>
                                                <div class="input-group mb-3">
                                                    <input list="DatalistProductos" id="InputNuevoProducto" runat="server" autocomplete="off" class="form-control border-dark shadow"  placeholder="Escribe un producto...">
                                                    <datalist id="DatalistProductos" runat="server">
                                                    </datalist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row ms-2 me-2 mb-4">
                                            <div class="col-lg-3">
                                                <h6>Responsable de la entrada:</h6>
                                                <asp:DropDownList ID="DropNuevoResponsableEntrada" runat="server" class="form-select border-dark shadow">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3">
                                                <h6>Cantidad bloqueada:</h6>
                                                <input type="number" id="InputNuevoCantidad" min="0" runat="server" pattern="[0-9]" class="form-control border-dark shadow" autocomplete="off">
                                            </div>
                                            <div class="col-lg-6">
                                                <h6>Motivo:</h6>
                                                <div class="input-group shadow">
                                                  <textarea id="InputNuevoMotivo" runat="server" class="form-control border-dark shadow" placeholder="Motivo del bloqueo." rows="2"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row ms-2 me-2 mb-4">
                                            <div class="col-lg-3">
                                                <h6>Responsable de la salida:</h6>
                                                <asp:DropDownList ID="DropNuevoResponsableSalida" runat="server" class="form-select border-dark shadow">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3">
                                                <h6>Fecha prevista de salida:</h6>
                                                <input id="InputNuevaFechaSalida" runat="server" class="form-control border-dark shadow Add-text" autocomplete="off" >
                                            </div>
                                            <div class="col-lg-6">
                                                <h6>Observaciones:</h6>
                                                <div class="input-group shadow">
                                                  <textarea id="InputNuevoObservaciones" runat="server" class="form-control border-dark shadow" placeholder="Observaciones." rows="2"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer" style="background: #e6e6e6">
                            <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                            <button type="button" class="btn btn-success" id="Button3" onserverclick="Agregar_Producto" runat="server" ><i class="bi bi-sd-card"></i></button>
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
                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" runat="server" id="CheckHistorico" checked>
                        <label class="form-check-label" for="flexSwitchCheckChecked">Ocultar histórico</label>
                    </div>
                    <div class="input-group mb-3">
                        <button id="Button2" runat="server" onserverclick="Cargar_filtro" type="button" class="btn btn-secondary" style="width: 100%; text-align: left">
                            <i class="bi bi-search me-2"></i>Filtrar</button>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

