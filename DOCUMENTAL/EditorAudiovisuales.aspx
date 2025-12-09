<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EditorAudiovisuales.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.EditorAudiovisuales" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Editor audiovisuales</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Editor audiovisuales             
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
        function AlertaFormato(TextoAlerta)
            {
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
                                <h5>Audiovisual Dashboard KPI</h5>
                                <asp:GridView ID="dgv_AudiovisualKPI" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="OnRowDataBoud"
                                    EmptyDataText="There are no data records to display.">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>
                                                <asp:LinkButton runat="server" ID="button2" CommandName="NuevoRecurso" CommandArgument="KPIPLANTA" UseSubmitBehavior="true" CssClass="btn btn-light border border-1 border-dark shadow shadow-sm" Style="font-size: 1rem">
                                                <i class="bi bi-plus-circle"></i></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="button2" CommandName="EditarRecurso" CommandArgument='<%#Eval("Id")%>' UseSubmitBehavior="true" CssClass="btn btn-primary border border-1 border-dark shadow shadow-sm" Style="font-size: 1rem">
                                                <i class="bi bi-file-post"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripcion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" Font-Bold="true" runat="server" Text='<%#Eval("Disponible") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-lg-4">
                                <h5>Audiovisual Pantallas Visita</h5>
                                <asp:GridView ID="dgv_AudiovisualVISITAS" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                                    OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="OnRowDataBoud"
                                    EmptyDataText="There are no data records to display.">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <HeaderTemplate>
                                                <asp:LinkButton runat="server" ID="button2" CommandName="NuevoRecurso" CommandArgument="VISITAS" UseSubmitBehavior="true" CssClass="btn btn-light border border-1 border-dark shadow shadow-sm" Style="font-size: 1rem">
                                                <i class="bi bi-plus-circle"></i></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="button2" CommandName="EditarRecurso" CommandArgument='<%#Eval("Id")%>' UseSubmitBehavior="true" CssClass="btn btn-primary border border-1 border-dark shadow shadow-sm" Style="font-size: 1rem">
                                                <i class="bi bi-file-post"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripcion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" Font-Bold="true" runat="server" Text='<%#Eval("Disponible") %>' />
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
                                                    <input type="text" id="InputAPP" class="form-control border border-dark shadow" disabled="disabled"  autocomplete="off" runat="server">
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
                        
                            <div class="btn-group border border-dark mt-3" style="width:100%">
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
                                                    <input type="text" id="InputNuevaAPP" class="form-control border border-dark shadow" disabled="disabled"  autocomplete="off" runat="server">
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
                                                            <button class="btn btn-outline-secondary border border-dark shadow" type="button" runat="server" id="BTNUploadMSA" onserverclick="Insertar_documento" >Guardar</button>
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
