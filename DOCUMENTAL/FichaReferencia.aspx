<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FichaReferencia.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.FichaReferencia" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Documentos de producto</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp-
    <label id="LabelNumMaquina" runat="server">Lista de documentos</label>
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
    <script type="text/javascript">
        function ShowPopDocVinculados() {
            document.getElementById("btnPopDocVinculados").click();
        }
        function ClosePopupDocumentacion() {
            document.getElementById("btnDismissModal").click();
        }
        function ShowPopupDOCAUX() {
            document.getElementById("AUXMODALDOCAUX").click();
        }
        function ShowPopupEstructura() {
            document.getElementById("btnPopEstructura").click();
        }
    </script>
    <script type="text/javascript">
        $(document).on("click", "[id*=BTNCerrarAviso]", function () {
            document.getElementById("CheckAvisoNoperario").checked = true;
        });
        $(document).on("click", "[id*=POL]", function () {
            document.getElementById("btnPopExperiencia").click();
        });
    </script>
    <script type="text/javascript">
        function BorraMOL() {
            var reply = confirm("Vas a borrar los documentos seleccionados de todas las referencias que comparten este molde, ¿estás seguro?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
        function BorraREF() {
            var reply = confirm("Vas a borrar los documentos seleccionados de esta referencia, ¿estás seguro?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
        
        function guardado_OK() {
            return confirm('Vas a guardar los documentos de la referencia, ¿estás seguro?');
        }
        function guardado_NOK() {
            alert("Se ha producido un error al guardar la orden de revisión. Revise que los datos introducidos estén bien, el número de lote seleccionado y los operarios en formato numérico).");
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
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-4">
                    <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-2 px-0 mt-2">
                            <asp:Image ID="IMGliente" runat="server" Width="100%" ImageUrl='http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' />
                        </div>
                        <div class="col-lg-8 px-0 ms-2 mt-2">
                            <asp:Label ID="tbReferenciaCarga" runat="server" Font-Size="X-Large" Font-Bold="true" Style="text-align: center" Enabled="false"></asp:Label>
                            <label id="tbMolde" class="ms-2" runat="server" style="text-align: left; font-size: large; font-weight: bold; font: italic" enabled="false"></label>
                            <br />
                            <asp:Label ID="tbDescripcionCarga" class="ms-1" runat="server" Font-Italic="true" Style="text-align: left" /><br />

                        </div>
                    </div>
                </div>
                <div class="col-lg-4"></div>
                <div class="col-lg-4">
                    <h6>Referencia:</h6>
                    <div class="input-group mb-3 shadow bg-white">
                        <input list="DatalistReferencias" id="tbReferencia" class="form-control border border-secondary" runat="server" placeholder="Escribe una referencia...">
                        <datalist id="DatalistReferencias" runat="server">
                        </datalist>
                        <button id="btnCargar" runat="server" class="btn btn-outline-secondary border border-secondary" type="button" onserverclick="CargarDatos">Cargar datos</button>
                    </div>
                    <button type="button" id="btnPopDocVinculados" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopDocVinculados"></button>

                </div>
            </div>
            <div class="row">
                <%--cabecera de selección--%>
                <div class="col-lg-2">
                    <div class="card mt-2 shadow shadow-lg">
                        <div class="card-header text-bg-primary border border-dark">
                            <div class="form-check">
                                <input class="form-check-input border border-dark" type="checkbox" value="" id="CHECKIMG" runat="server">
                                <button id="btnsubir0" type="button" runat="server" onserverclick="Insertar_documento" class="btn btn-secondary border border-dark" style="float: right"><i class="bi bi-cloud-upload"></i></button>
                            </div>
                        </div>
                        <div class="card-body" style="background-color: #eeeeee">
                            <asp:HyperLink ID="hyperlink9" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid shadow" />
                            <asp:FileUpload ID="FileUpload0" runat="server" class="form-control shadow" accept=".png,.jpg,.jpeg"></asp:FileUpload>
                        </div>
                    </div>

                </div>
                <div class="col-lg-4">
                    <div class="card mt-2 shadow shadow-lg  border border-secondary">
                        <h5 class="card-header text-bg-primary shadow">Referencias que comparten molde</h5>
                        <div style="overflow-y: scroll; height: 275px">
                            <asp:GridView ID="dgv_RefXMolde" runat="server" AllowSorting="True"
                                Width="100%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                EmptyDataText="No hay referencias compartidas para este molde." ShowHeader="false" OnRowCommand="OnRowCommandRedi">
                                <HeaderStyle BackColor="#0d6efd" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btnDetail3" CommandName="Redirect" CommandArgument='<%#Eval("Referencia")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Productos">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Referencia") +" " +Eval("Descripcion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="card mt-2 shadow shadow-lg  border border-secondary">
                        <h5 class="card-header text-bg-primary"><i class="bi bi-journals me-2"></i>Comentarios</h5>
                        <label style="background-color: orange; font-weight: bold; text-align: center" class="border-bottom border-top border-secondary shadow">Notas de ficha</label>
                        <asp:TextBox ID="tbObservacionesCarga" Width="100%" runat="server" Font-Italic="true" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false" Height="50px" TextMode="MultiLine"></asp:TextBox>
                        <label style="background-color: orange; font-weight: bold; text-align: center" class="border-bottom border-top border-secondary shadow">Notas producto BMS</label>
                        <asp:TextBox ID="tbObservacionesCargaBMS" Width="100%" Font-Italic="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false" Height="50px" TextMode="MultiLine"></asp:TextBox>
                        <button id="btnreferencia" type="button" runat="server" onserverclick="InsertarDocumentosReferenciaMoldeBD" class="btn btn-sm btn-primary border border-dark" style="width: 100%; font-weight: bold">Actualizar notas</button>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="card mt-2 shadow shadow-lg border-secondary">
                        <h5 class="card-header text-bg-primary "><i class="bi bi-hand-index me-2"></i>Acciones</h5>
                        <div class="btn-group border border-dark" style="width: 100%">
                            <button id="btnMaquinaVista" width="100%" type="button" runat="server" onserverclick="Redirecciones" class="btn btn-secondary" disabled="true">Vista previa (Máquina)</button>
                            <button id="btnGP12Vista" width="100%" type="button" runat="server" onserverclick="Redirecciones" class="btn btn-secondary" disabled="true">Vista previa (GP12)</button>
                        </div>
                        <button id="btnmolde" type="button" style="width: 100%" runat="server" onserverclick="InsertarDocumentosMolde" class="btn btn-info border border-dark" disabled="true">Guardar seleccionados en molde</button>

                        <div class="btn-group shadow" style="width: 100%" hidden="hidden">
                            <button id="btnderogacion" type="button" runat="server" class="btn btn-basic" style="background-color: cornflowerblue; color: white">Derogación</button>
                            <button id="btnregistro" type="button" runat="server" class="btn btn-primary">Cambio de proceso</button>
                            <button id="btnpartedeprueba" type="button" runat="server" class="btn btn-basic" style="background-color: cornflowerblue; color: white">Parte de prueba</button>
                        </div>
                        <div class="btn-group border border-dark" style="width: 100%">
                            <asp:Button ID="borrarref" Width="100%" type="button" runat="server" OnClientClick="return confirm('Vas a borrar los documentos seleccionados para la referencia, ¿estás seguro?');" OnClick="BorrarDocumento" class="btn btn-danger btn-md" Visible="false" Text="Borrar sel. de referencia" />
                            <asp:Button ID="borrarmolde" Width="100%" type="button" runat="server" OnClientClick="return confirm('Vas a borrar los documentos seleccionados para todas las refrencias del molde, ¿estás seguro?');" OnClick="BorrarDocumento" class="btn btn-danger btn-md" Visible="false" Text="Borrar sel. de molde" />
                        </div>
                        <button id="Button1" type="button" runat="server" onserverclick="Imprimiretiqueta" class="btn btn-outline-dark btn-sm shadow" visible="false" style="width: 100%">Ver Etiqueta</button>
                        <button id="btnVerEstructura" type="button" runat="server" onserverclick="VerEstructura" class="btn btn-outline-dark btn-sm shadow" visible="false" style="width: 100%">Ver estructura</button>

                        <div runat="server" visible="false">
                            <asp:TextBox ID="txtPlanControl" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtPautaControl" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtOperacionEstandar" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtDefoteca" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtEmbalaje" runat="server"></asp:TextBox>
                        </div>


                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card mt-2 shadow shadow-lg">
                        <h5 class="card-header text-bg-primary  border border-dark"><i class="bi bi-display me-2"></i>Documentos</h5>
                        <div class="card-body" style="background-color: #eeeeee">
                            <div class="row">
                                <div class="col-lg-8">
                                    <div class="row">
                                        <%--cuatro columnas 1 --%>
                                        <div class="col-lg-4">
                                            <div class="card mt-2 shadow border border-dark" runat="server" id="PreviewPautaControl">
                                                <div class="card-header text-bg-primary  border-bottom border-dark">
                                                    <div class="form-check">
                                                        <input id="CHECKPREVPAC" runat="server" class="form-check-input border border-dark" type="checkbox" value="">
                                                        <label class="form-check-label" style="font-weight: bold" for="CHECKPREVPAC">Pauta de control</label>
                                                        <div class="btn-group shadow" style="float: right">
                                                            <button id="btnver5" type="button" runat="server" onserverclick="Redirecciones" class="btn btn-success border border-dark"><i class="bi bi-eye"></i></button>
                                                            <button id="btnsubir5" type="button" runat="server" onserverclick="Insertar_documento" class="btn btn-secondary border border-dark"><i class="bi bi-cloud-upload"></i></button>
                                                            <button id="btnborrar5" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger border border-dark"><i class="bi bi-trash"></i></button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <iframe id="framepautadecontrol" class="embed-responsive-item" src="" width="100%" runat="server" style="background: url(SinDocumentos.png) no-repeat; background-position: center"></iframe>
                                                <asp:FileUpload ID="FileUpload4" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="card mt-2 border border-dark shadow" runat="server" id="PreviewOperacionEstandar">
                                                <div class="card-header text-bg-primary  border-bottom border-dark">
                                                    <div class="form-check">
                                                        <input id="CHECKPREVHOS" runat="server" class="form-check-input  border border-dark" type="checkbox" value="">
                                                        <label class="form-check-label" style="font-weight: bold" for="CHECKPREVHOS">Operación estándar</label>
                                                        <div class="btn-group" style="float: right">
                                                            <button id="btnver9" type="button" runat="server" onserverclick="Redirecciones" class="btn btn-success  border border-dark"><i class="bi bi-eye"></i></button>
                                                            <button id="btnsubir9" type="button" runat="server" onserverclick="Insertar_documento" class="btn btn-secondary  border border-dark"><i class="bi bi-cloud-upload"></i></button>
                                                            <button id="btnborrar9" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger  border border-dark"><i class="bi bi-trash"></i></button>
                                                        </div>
                                                    </div>
                                                </div>

                                                <iframe id="frameoperacionestandar" class="embed-responsive-item" src="" width="100%" runat="server" style="background: url(SinDocumentos.png) no-repeat; background-position: center"></iframe>
                                                <asp:FileUpload ID="FileUpload8" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>

                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="card mt-2 border border-dark shadow" runat="server" id="PreviewDefoteca">
                                                <div class="card-header  text-bg-primary border-bottom border-dark">
                                                    <div class="form-check">
                                                        <input id="CHECKPREVDEF" runat="server" class="form-check-input border border-dark" type="checkbox" value="">
                                                        <label class="form-check-label" style="font-weight: bold" for="CHECKPREVDEF">Defoteca</label>
                                                        <div class="btn-group" style="float: right">
                                                            <button id="btnver15" type="button" runat="server" onserverclick="Redirecciones" class="btn btn-success border border-dark"><i class="bi bi-eye"></i></button>
                                                            <button id="btnsubir15" type="button" runat="server" onserverclick="Insertar_documento" class="btn btn-secondary border border-dark"><i class="bi bi-cloud-upload"></i></button>
                                                            <button id="btnborrar15" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger border border-dark"><i class="bi bi-trash"></i></button>
                                                        </div>
                                                    </div>
                                                </div>

                                                <iframe id="framedefoteca" class="embed-responsive-item" src="" width="100%" runat="server" style="background: url(SinDocumentos.png) no-repeat; background-position: center"></iframe>
                                                <asp:FileUpload ID="FileUpload14" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="card mt-2 border border-dark shadow" runat="server" id="PreviewEmbalaje">
                                                <div class="card-header text-bg-primary border-bottom border-dark">
                                                    <div class="form-check">
                                                        <input id="CHECKPREVEMB" runat="server" class="form-check-input  border border-dark" type="checkbox" value="">
                                                        <label class="form-check-label" style="font-weight: bold" for="CHECKPREVEMB">Embalaje</label>
                                                        <div class="btn-group" style="float: right">
                                                            <button id="btnver17" type="button" runat="server" onserverclick="Redirecciones" class="btn btn-success   border border-dark"><i class="bi bi-eye"></i></button>
                                                            <button id="btnsubir17" type="button" runat="server" onserverclick="Insertar_documento" class="btn btn-secondary   border border-dark"><i class="bi bi-cloud-upload"></i></button>
                                                            <button id="btnborrar17" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger   border border-dark"><i class="bi bi-trash"></i></button>
                                                        </div>
                                                    </div>
                                                </div>

                                                <iframe id="frameembalaje" class="embed-responsive-item" src="" width="100%" runat="server" style="background: url(SinDocumentos.png) no-repeat; background-position: center"></iframe>
                                                <asp:FileUpload ID="FileUpload16" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>

                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="card mt-2 border border-dark shadow" runat="server" id="PreviewEmbalajeAlternativo">
                                                <div class="card-header text-bg-primary border-bottom border-dark">
                                                    <div class="form-check">
                                                        <input id="CHECKPREVEMBALT" runat="server" class="form-check-input border border-dark" type="checkbox" value="">
                                                        <label class="form-check-label" style="font-weight: bold" for="CHECKPREVEMBALT">Embalaje alternativo</label>
                                                        <div class="btn-group" style="float: right">
                                                            <button id="btnver7" type="button" runat="server" onserverclick="Redirecciones" class="btn btn-success  border border-dark"><i class="bi bi-eye"></i></button>
                                                            <button id="btnsubir7" type="button" runat="server" onserverclick="Insertar_documento" class="btn btn-secondary border border-dark"><i class="bi bi-cloud-upload"></i></button>
                                                            <button id="btnborrar7" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger  border border-dark"><i class="bi bi-trash"></i></button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <iframe id="frameEmbalajeAlternativo" class="embed-responsive-item" src="" width="100%" runat="server" style="background: url(SinDocumentos.png) no-repeat; background-position: center"></iframe>
                                                <asp:FileUpload ID="FileUpload6" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="card mt-2 border border-dark shadow" runat="server" id="PreviewGP12">
                                                <div class="card-header  text-bg-primary border-bottom border-dark">
                                                    <div class="form-check">
                                                        <input id="CHECKPREVGP12" runat="server" class="form-check-input border border-dark" type="checkbox" value="">
                                                        <label class="form-check-label" style="font-weight: bold" for="CHECKPREVGP12">GP12</label>
                                                        <div class="btn-group" style="float: right">
                                                            <button id="btnver11" type="button" runat="server" onserverclick="Redirecciones" class="btn btn-success  border border-dark"><i class="bi bi-eye"></i></button>
                                                            <button id="btnsubir11" type="button" runat="server" onserverclick="Insertar_documento" class="btn btn-secondary  border border-dark"><i class="bi bi-cloud-upload"></i></button>
                                                            <button id="btnborrar11" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger border border-dark"><i class="bi bi-trash"></i></button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <iframe id="framemurodecalidad" class="embed-responsive-item" src="" width="100%" runat="server" style="background: url(SinDocumentos.png) no-repeat; background-position: center"></iframe>
                                                <asp:FileUpload ID="FileUpload10" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="card mt-2 shadow" runat="server" id="Div1">
                                        <div class="card-header text-bg-primary border border-dark">
                                            <label class="h5" style="font-weight: bold" for="CHECKPREVGP12"><i class="bi bi-folder me-2"></i>Otros Documentos</label>
                                            <button id="btnOtrosDocs" type="button" runat="server" visible="false" class="btn btn-secondary border border-dark" onserverclick="AbrirModalSubirDocs" style="float: right"><i class="bi bi-file-earmark-plus"></i></button>
                                        </div>
                                        <asp:GridView ID="GridVinculados" runat="server" AllowSorting="True" OnRowCommand="OnRowCommand"
                                            CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded rounded-2 border-top-0" AutoGenerateColumns="false"
                                            EmptyDataText="No hay documentos adicionales vinculados." ShowHeader="false">
                                            <HeaderStyle BackColor="#0d6efd" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" ItemStyle-BackColor="#ccccff">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnVer" CommandName="Redirect" CommandArgument='<%#Eval("URL")%>' class="btn btn-outline-dark" Style="font-size: 1rem">
                                                                <i class="bi bi-file-post"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipoDocumento" Font-Bold="true" runat="server" Text='<%#Eval("TipoDocumento") %>' /><br />
                                                        <asp:Label ID="lblMultiREF" Font-Size="Small" runat="server" Text='<%#Eval("MULTIREFTEXT") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documento">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcionDOC" runat="server" Text='<%#Eval("DescripcionDOC") %>' /><br />
                                                        <asp:Label ID="lblFechaDOC" Font-Size="Small" Font-Bold="true" runat="server" Text='<%#Eval("Fecha","{0:dd/MM/yyyy}") + " - Ed:" + Eval("Edicion") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnEliminar" CommandName="Eliminar" CommandArgument='<%#Eval("URL")%>' class="btn btn-danger shadow" OnClientClick="return confirm('¿Seguro que quieres eliminar este documento?');" Style="font-size: 1rem">
                                                                <i class="bi bi-trash"></i>
                                                        </asp:LinkButton>
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
        <div class="modal fade" id="PopDocVinculados" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header bg-warning">
                        <label style="font-size: x-large; font-weight: bold">Otros documentos</label>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="embed-responsive embed-responsive-16by9">
                                    <iframe id="DocVinculado" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer bg-warning">
                        <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Button trigger modal -->
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
                                                <div class="row mt-2 mb-2 ms-2 shadow rounded-2 border border-dark bg-white">
                                                    <div class="col-sm-12">
                                                        <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>
                                                            <label>Datos del documento</label>
                                                            <label></label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-2">
                                                        <h6>Vincular a:</h6>
                                                        <asp:DropDownList ID="DropSelectMOLREF" runat="server" class="form-select border border-secondary shadow">
                                                            <asp:ListItem Value="0">Referencia</asp:ListItem>
                                                            <asp:ListItem Value="1">Molde</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <h6>Tipo de documento:</h6>
                                                        <asp:DropDownList ID="DropTipoDOCAUX" runat="server" class="form-select border border-secondary shadow">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-7">
                                                        <h6>Descripción:</h6>
                                                        <input type="text" id="tbDescripcionDOCAUX" class="form-control border border-secondary shadow" maxlength="49" autocomplete="off" runat="server">
                                                    </div>
                                                    <%-- --%>
                                                </div>
                                                <div class="row mt-2">
                                                    <div class="col-lg-2">
                                                        <h6>Fecha:</h6>
                                                        <input type="text" id="tbFechaDOCAUX" class="form-control border border-secondary shadow Add-text" autocomplete="off" runat="server">
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <h6>Edición:</h6>
                                                        <input type="number" id="TbNumEdicion" min="0" class="form-control border border-secondary shadow" autocomplete="off" runat="server" value="0">
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <h6>Aprobado por:</h6>
                                                        <asp:DropDownList ID="dropAprobadoDOCAUX" runat="server" class="form-select border border-secondary shadow">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-5">
                                                        <asp:HiddenField ID="AUXDOCAUXURL" runat="server" />
                                                        <div class="row" id="RowAdjuntoDOCAUX" runat="server">
                                                            <h6>Adjunto:</h6>
                                                            <div class="input-group bg-white">
                                                                <asp:FileUpload ID="UploadDOCAUX" runat="server" class="form-control  border border-secondary shadow"></asp:FileUpload>
                                                                <button class="btn btn-outline-secondary" type="button" runat="server" onserverclick="NewSaveFile" id="BTNUploadDOCAUX">Subir</button>
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

            </div>
        </div>
    </div>
    <div class="modal fade" id="PopEstructura" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <label id="Label1" style="font-size: x-large; font-weight: bold">Estructura de materiales</label>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" style="height: 900px">
                    <label id="lblEstructuraProducto" style="font-size: large; font-weight: bold" runat="server"></label>
                    <asp:GridView ID="GridEstructura" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                        Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                        EmptyDataText="No hay datos para mostrar.">
                        <HeaderStyle BackColor="orange" Font-Bold="True" />
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Material">
                                <ItemTemplate>
                                    <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("MATERIAL") %>' />
                                    <asp:Label ID="lblDescripción" runat="server" Text='<%#" " + Eval("DESCRIPCION") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ubicación">
                                <ItemTemplate>
                                    <asp:Label ID="lblUbicacion" runat="server" Text='<%#Eval("UBICACION") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Necesario">
                                <ItemTemplate>
                                    <asp:Label ID="lblConsumo" runat="server" Text='<%#Eval("CONSUMOUNIDAD") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("NOTAS") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                  

                </div>

                <div class="modal-footer bg-warning">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <button id="AUXMODALDOCAUX" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalDocumentosAuxiliares" style="font-size: larger">MSA</button>
    <button type="button" id="btnPopEstructura" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopEstructura"></button>
                                    

    <!-- Modal -->

</asp:Content>




