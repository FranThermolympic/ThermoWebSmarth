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
                <li><a class="dropdown-item" href="GestionDocumental.aspx">Listado de productos</a></li>
                <li><a class="dropdown-item" href="FichaReferencia.aspx">Documentación de referencia</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ShowPopupIncrustados() {
            document.getElementById("btnPopIncrustados").click();
        }
        function ShowPopupEstructura() {
            document.getElementById("btnPopEstructura").click();
        }
        function ShowPopupExperiencia() {
            document.getElementById("btnPopExperiencia").click();
        }
        function ShowPopupDocumentacion() {
            document.getElementById("btnPopDocumentacion").click();
        }
        function ClosePopupDocumentacion() {
            document.getElementById("btnDismissModal").click();
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
        function BorraREF() {
            var reply = confirm("Vas a borrar los documentos seleccionados de esta referencia, ¿estás seguro?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
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
    </script>
    <script type="text/javascript">
        function guardado_OK() {
            return confirm('Vas a guardar los documentos de la referencia, ¿estás seguro?');

        }
    </script>
    <script type="text/javascript">
        function guardado_NOK() {
            alert("Se ha producido un error al guardar la orden de revisión. Revise que los datos introducidos estén bien, el número de lote seleccionado y los operarios en formato numérico).");
        }
    </script>
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid">
            <div class="row">
                <%--cabecera de selección--%>
                <div class="col-lg-4">
                    <div class="card mt-2 shadow shadow-lg">
                        <div class="card-body">
                            <h6>Referencia:</h6>
                            <div class="input-group mb-3 shadow">
                                <input id="tbReferencia" runat="server" type="text" class="form-control border border-secondary" placeholder="Escribir referencia" autocomplete="off" aria-describedby="button-addon2">
                                <button id="btnCargar" runat="server" class="btn btn-outline-secondary border border-secondary" type="button" onserverclick="CargarDatos">Cargar datos</button>
                            </div>
                            <div class="table table-responsive rounded rounded-2 shadow">
                                <table width="100%">
                                    <tr style="background-color: orange; border-color: orange" class="border border-secondary">
                                        <th>
                                            <asp:Label ID="tituloReferenciaCarga" Width="100%" Font-Bold="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false" Text="Referencia" />
                                        </th>
                                        <th>
                                            <asp:Label ID="tituloMolde" Width="100%" Font-Bold="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false" Text="Molde" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <td class="border border-secondary">
                                            <asp:TextBox ID="tbReferenciaCarga" Width="100%" Font-Italic="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="border border-secondary">
                                            <asp:TextBox ID="tbMolde" Width="100%" runat="server" Font-Italic="true" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="border border-secondary">
                                            <asp:TextBox ID="tbDescripcionCarga" Width="100%" Font-Italic="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: orange; border-color: orange">
                                        <th colspan="2" class="border border-secondary">
                                            <asp:Label ID="tbTituloObservaciones" Width="100%" Font-Bold="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false" Text="Notas de ficha" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="border border-secondary">
                                            <asp:TextBox ID="tbObservacionesCarga" Width="100%" runat="server" Font-Italic="true" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: orange; border-color: orange">
                                        <th colspan="2" class="border border-secondary">
                                            <asp:Label ID="tbTituloObservacionesBMS" Width="100%" Font-Bold="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false" Text="Notas producto BMS" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="border border-secondary">
                                            <asp:TextBox ID="tbObservacionesCargaBMS" Width="100%" Font-Italic="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            <button id="btnreferencia" type="button" runat="server" onserverclick="InsertarDocumentosReferenciaMolde" class="btn btn-sm btn-primary" style="width: 100%; font-weight: bold">Actualizar nota</button><br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="btn-group shadow" style="width: 100%" hidden="hidden">
                                <button id="btnderogacion" type="button" runat="server" class="btn btn-basic" style="background-color: cornflowerblue; color: white">Derogación</button>
                                <button id="btnregistro" type="button" runat="server" class="btn btn-primary">Cambio de proceso</button>
                                <button id="btnpartedeprueba" type="button" runat="server" class="btn btn-basic" style="background-color: cornflowerblue; color: white">Parte de prueba</button>
                            </div>
                            <div runat="server" visible="false">
                                <asp:TextBox ID="txtPlanControl" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtPautaControl" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtOperacionEstandar" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtDefoteca" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtEmbalaje" runat="server"></asp:TextBox>
                            </div>
                            <div class="table table-responsive rounded rounded-2 border border-1 shadow">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <div class="btn-group" style="width: 100%">
                                                <button id="btnVistaMaquina" width="100%" type="button" runat="server" onserverclick="RedireccionaOTROS" class="btn btn-secondary" disabled="true">Vista previa (Máquina)</button>
                                                <button id="btnVistaGP12" width="100%" type="button" runat="server" onserverclick="RedireccionaGP12" class="btn btn-secondary" disabled="true">Vista previa (GP12)</button>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <button id="btnmolde" type="button" style="width: 100%" runat="server" onserverclick="InsertarDocumentosMolde" class="btn btn-info" disabled="true">Guardar seleccionados en molde</button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="btn-group" style="width: 100%">
                                                <asp:Button ID="borrarref" Width="100%" type="button" runat="server" OnClientClick="return BorraREF();" OnClick="BorrarDocumentoSeleccionados" class="btn btn-danger btn-md" Visible="false" Text="Borrar sel. de referencia" />
                                                <asp:Button ID="borrarmolde" Width="100%" type="button" runat="server" OnClientClick="return BorraMOL();" OnClick="BorrarDocumentoSeleccionados" class="btn btn-danger btn-md" Visible="false" Text="Borrar sel. de molde" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <button id="Button1" type="button" runat="server" onserverclick="Imprimiretiqueta" class="btn btn-outline-dark btn-sm shadow" style="width: 100%">Ver Etiqueta</button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="card mt-2 shadow shadow-lg">
                        <h5 class="card-header">Referencias que comparten molde</h5>
                        <div class="card-body">
                            <div style="overflow-y: scroll;height: 500px">
                                <asp:GridView ID="dgv_RefXMolde" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                    EmptyDataText="There are no data records to display.">
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
                </div>
                <div class="col-lg-3">
                    <div class="card mt-2 shadow shadow-lg">
                        <div class="card-header">
                            <div class="form-check">
                                <input class="form-check-input" type="checkbox" value="" id="CHECKIMG" runat="server">
                                <button id="btnsubir0" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:HyperLink ID="hyperlink9" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid shadow" />
                            <asp:FileUpload ID="FileUpload0" runat="server" class="form-control shadow" accept=".png,.jpg,.jpeg"></asp:FileUpload>
                        </div>
                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="card mt-2 shadow shadow-lg">
                        <h5 class="card-header">Documentos</h5>
                        <div class="card-body">
                            <div class="row">
                                <%--cuatro columnas 1 --%>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="DefaultPlanControl">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFPC" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFPC">Plan de control</label>
                                                <button id="btnsubir1" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink8" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload1" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="PreviewPlanControl" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVPC" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVPC">Plan de control</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver2" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir2" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar2" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="frameplandecontrol" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload2" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="DefaultPautaControl">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFPAC" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFPAC">Pauta de control</label>
                                                <button id="btnsubir4" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink7" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload3" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="PreviewPautaControl" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVPAC" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVPAC">Pauta de control</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver5" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir5" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar5" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="framepautadecontrol" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload4" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="DefaultPautaRecepcion">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFPREC" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFPREC">Pauta de recepción</label>
                                                <button id="btnsubir6" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink6" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload5" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="PreviewPautaRecepcion" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVREC" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVREC">Pauta de recepción</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver7" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir7" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar7" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="framepautarecepcion" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload6" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="DefaultOperacionEstandar">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFHOS" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFHOS">Operación estándar</label>
                                                <button id="btnsubir8" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink5" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload7" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="PreviewOperacionEstandar" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVHOS" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVHOS">Operación estándar</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver9" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir9" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar9" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="frameoperacionestandar" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload8" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2"><%--cuatro columnas 2 --%>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="DefaultGP12">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFGP12" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFGP12">GP12</label>
                                                <button id="btnsubir10" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink4" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload9" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="PreviewGP12" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVGP12" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVGP12">GP12</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver11" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir11" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar11" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="framemurodecalidad" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload10" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="DefaultOperacionEstandar2">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFHOS2" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFHOS2">HOS2 / Pokayoke</label>
                                                <button id="btnsubir12" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink3" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload11" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="PreviewOperacionEstandar2" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVHOS2" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVHOS2">HOS2 / Pokayoke</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver13" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir13" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar13" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="frameoperacionestandar2" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload12" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="DefaultDefoteca">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFDEF" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFDEF">Defoteca</label>
                                                <button id="btnsubir14" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink2" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload13" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="PreviewDefoteca" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVDEF" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVDEF">Defoteca</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver15" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir15" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar15" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="framedefoteca" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload14" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="DefaultEmbalaje">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFEMB" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFEMB">Embalaje</label>
                                                <button id="btnsubir16" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink1" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload15" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="PreviewEmbalaje" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVEMB" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVEMB">Embalaje</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver17" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir17" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar17" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="frameembalaje" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload16" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2"><%--cuatro columnas 3 --%>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="defaultRetrabajo">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFRTR" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFRTR">Pauta de retrabajo</label>
                                                <button id="btnsubir18" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink10" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload17" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="previewRetrabajo" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVRTR" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVRTR">Pauta de retrabajo</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver19" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir19" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar19" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="frameretrabajo" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload18" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="defaultVideo">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFVID" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFVID">Video auxiliar</label>
                                                <button id="btnsubir20" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink11" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload19" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="previewVideo" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVVID" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVVID">Video auxiliar</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver21" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir21" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar21" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="framevideoaux" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload20" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow" runat="server" id="defaultNC">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKDEFNC" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKDEFNC">No conformidades</label>
                                                <button id="btnsubir22" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm" style="float: right">Subir</button>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <asp:HyperLink ID="hyperlink12" NavigateUrl="" ImageWidth="100%" Width="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" />
                                            <asp:FileUpload ID="FileUpload21" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="card mt-2 shadow" runat="server" id="previewNC" visible="false">
                                        <div class="card-header">
                                            <div class="form-check">
                                                <input id="CHECKPREVNC" runat="server" class="form-check-input" type="checkbox" value="">
                                                <label class="form-check-label" style="font-weight: bold" for="CHECKPREVNC">No conformidades</label>
                                                <div class="btn-group" style="float: right">
                                                    <button id="btnver23" type="button" runat="server" onserverclick="RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button id="btnsubir23" type="button" runat="server" onserverclick="insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button id="btnborrar23" type="button" runat="server" onserverclick="BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <iframe id="frameNoConformidad" class="embed-responsive-item" src="" width="100%" runat="server"></iframe>
                                            <asp:FileUpload ID="FileUpload22" class="form-control shadow" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload>
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
    <!-- Button trigger modal -->


    <!-- Modal -->
    <%--
    <div class="d-flex align-items-start">
                <ul class="nav flex-column nav-pills me-3 mt-1 " id="pills-tab" role="tablist">
                    <li class="nav-item " role="presentation">
                        <button class="nav-link shadow  border border-secondary active" id="Button2" runat="server" data-bs-toggle="pill" data-bs-target="#ORDEN" type="button" role="tab" aria-controls="pills-home" aria-selected="true" style="font-weight: bold; width: 100%">MÁQUINA</button>
                    </li>
                    <li class="nav-item " role="presentation">
                        <label enabled="false" class="mt-2" type="button" style="font-weight: bold; width: 100%"><i class="bi bi-bookmarks">DOCUMENTOS</i></label>
                    </li>
                    <li class="nav-item" role="presentation" id="ref1lab" runat="server">
                        <button class="nav-link  shadow  border border-dark ms-1 me-1" style="font-weight: bold; width: 95%" id="ref1labtext" runat="server" data-bs-toggle="pill" data-bs-target="#REF1" type="button" role="tab" aria-controls="pills-home" aria-selected="true">---</button>
                    </li>
                    <li class="nav-item" role="presentation" id="ref2lab" runat="server" visible="false">
                        <button class="nav-link shadow  border border-dark ms-1 me-1" style="font-weight: bold; width: 95%" id="ref2labtext" runat="server" data-bs-toggle="pill" data-bs-target="#REF2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">---</button>
                    </li>
                    <li class="nav-item" role="presentation" id="ref3lab" runat="server" visible="false">
                        <button class="nav-link shadow  border border-dark ms-1 me-1" style="font-weight: bold; width: 95%" id="ref3labtext" runat="server" data-bs-toggle="pill" data-bs-target="#REF3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">---</button>
                    </li>
                    <li class="nav-item" role="presentation" id="ref4lab" runat="server" visible="false">
                        <button class="nav-link shadow  border border-dark ms-1 me-1" style="font-weight: bold; width: 95%" id="ref4labtext" runat="server" data-bs-toggle="pill" data-bs-target="#REF4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">---</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <asp:Image ID="CLIENTE" runat="server" CssClass="mt-2" ImageUrl="" Width="130px" />
                    </li>
                </ul>
                <div class="tab-content">
                    <div id="ORDEN" class="tab-pane fade show active" runat="server">
                        <div class="row">
                            <div class="col-lg-6">
                                <ul class="nav nav-pills mt-1" id="pills-tab2" role="tablist">
                                    <li class="nav-item" role="presentation" id="Li2" runat="server">
                                        <button class="nav-link shadow  border border-dark" id="btn_DU11_3" runat="server" data-bs-toggle="pill" data-bs-target="#DU11_3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold" visible="false">DU11_3</button>
                                    </li>
                                    <li class="nav-item " role="presentation" id="Li1" runat="server">
                                        <button class="nav-link shadow  border border-dark " id="btn_DU11_2" runat="server" data-bs-toggle="pill" data-bs-target="#DU11_2" type="button" role="tab" aria-controls="pills-home" aria-selected="true" style="font-weight: bold" visible="false">DU11_2</button>
                                    </li>
                                    <li class="nav-item " role="presentation">
                                        <button class="nav-link shadow  border border-secondary active " id="btn_DU11_1" runat="server" data-bs-toggle="pill" data-bs-target="#DU11_1" type="button" role="tab" aria-controls="pills-home" aria-selected="true" style="font-weight: bold">DU11_1</button>
                                    </li>
                                </ul>

                                <div class="tab-content">
                                    <div id="DU11_1" class="tab-pane fade show active" runat="server">

                                        <div class="embed-responsive embed-responsive-16by9 ">
                                            <iframe id="IframeDU11_1" class="shadow shadow-lg" runat="server" src="..\SMARTH_fonts\INTERNOS\NODU11s.PNG" frameborder="0" style="height: 90%; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%"></iframe>
                                        </div>
                                    </div>
                                    <div id="DU11_2" class="tab-pane fade" runat="server">
                                        <div class="embed-responsive embed-responsive-16by9">
                                            <iframe id="IframeDU11_2" class="shadow shadow-lg" runat="server" src="..\SMARTH_fonts\INTERNOS\NODU11s.PNG" frameborder="0" style="height: 90%; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%"></iframe>
                                        </div>
                                    </div>
                                    <div id="DU11_3" class="tab-pane fade" runat="server">
                                        <div class="embed-responsive embed-responsive-16by9">
                                            <iframe id="IframeDU11_3" class="shadow shadow-lg" runat="server" src="..\SMARTH_fonts\INTERNOS\NODU11s.PNG" frameborder="0" style="height: 90%; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%"></iframe>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6" style="height: 600px">
                                <div class="col-lg-12" runat="server" visible=" false">
                                    <h4>Datos generales</h4>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed" runat="server">
                                            <tr>
                                                <th runat="server" visible="false">
                                                    <asp:TextBox ID="tbfechaMod" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff"></asp:TextBox>
                                                </th>
                                                <th runat="server" visible="false">
                                                    <asp:TextBox ID="RazonMod" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff"></asp:TextBox>
                                                </th>
                                                <th style="width: 10%">
                                                    <asp:TextBox ID="tbMaquinaTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="Orange">MÁQUINA</asp:TextBox>
                                                </th>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="tbMaquina1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 10%">
                                                    <asp:TextBox ID="tbMolde1desc" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="Orange">MOLDE</asp:TextBox>
                                                </th>
                                                <td style="width: 25%">
                                                    <asp:TextBox ID="tbMolde1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="tbCalidadTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="Orange">ESTADO</asp:TextBox>
                                                </th>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="tbCalidad1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-lg-12 mt-4">
                                    <h4>Referencias produciendo</h4>
                                    <asp:GridView ID="dgv_Ordenes" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                                        EmptyDataText="No hay datos para mostrar.">
                                        <HeaderStyle BackColor="orange" Font-Bold="True" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="6%" ItemStyle-BackColor="#ccccff" ItemStyle-HorizontalAlign="Center" Visible="FALSE">
                                                <ItemTemplate>
                                                    <div class="btn-group" role="group" aria-label="Basic example">
                                                        <asp:LinkButton ID="btnDetallesOrden" CssClass="btn btn-outline-dark " Font-Size="Large" runat="server"> <i class="bi bi-file-post"></i></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ORDEN" HeaderStyle-Width="19%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#ccccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("C_ID") %>' Font-Size="Large" Font-Bold="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REFERENCIA" HeaderStyle-Width="65%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("C_PRODUCT_ID") %>' />
                                                    <asp:Label ID="lblDescripción" runat="server" Text='<%#" " + Eval("C_PRODLONGDESCR") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-lg-12">
                                    <h4>Personal trabajando</h4>
                                    <asp:GridView ID="dgv_Personal" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                                        EmptyDataText="No hay datos para mostrar.">
                                        <HeaderStyle BackColor="orange" Font-Bold="True" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="20%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#ccccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipo" runat="server" Text='<%#Eval("TIPOOPE")%>' Font-Size="Large" Font-Bold="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="55%" HeaderText="NOMBRE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumOP" runat="server" Text='<%#Eval("NUMOP") %>' Font-Size="Large" Font-Bold="true" />
                                                    <asp:Label ID="lblNomOP" runat="server" Text='<%#Eval("NOMBREOP") %>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="15%" HeaderText="EXPERIENCIA" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoOP" runat="server" Text='<%#Eval("TIEMPOHORAS") + " h."%>' Font-Bold="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="NIVEL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNivelOP" runat="server" Text='<%#Eval("NIVELOPE")%>' Font-Size="Large" Font-Bold="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <div class="table-responsive invisible">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 100%">
                                                    <asp:TextBox ID="TituloPosicion" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Posición</asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                    <button type="button" id="btnPopIncrustados" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopIncrustados"></button>
                                    <button type="button" id="btnPopEstructura" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopEstructura"></button>
                                    <button type="button" id="btnPopExperiencia" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopPolivalencia"></button>
                                    <button type="button" id="btnPopDocumentacion" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopNuevaDocumentacion"></button>
                                    <button type="button" id="btnDismissModal" class="btn btn-primary  invisible" data-bs-dismiss="modal">Cerrar</button>
                                </div>
                                <div class="row bg-warning border border-1 invisible">
                                    <asp:Label ID="LblLiberacion" CssClass="mb-2" runat="server" Style="text-align: center; width: 100%; height: 30px" Font-Bold="true" Font-Size="X-Large"><i id="I1" runat="server" class="bi bi-exclamation-triangle-fill">&nbsp PROCESO PENDIENTE DE LIBERAR</i></asp:Label>
                                </div>
                                <div class="table-responsive" runat="server" visible="false">
                                    <table style="table-layout: fixed">
                                        <tr>
                                            <th style="width: 10%">
                                                <asp:TextBox ID="tbOrdenTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Orden</asp:TextBox>
                                            </th>
                                            <td style="width: 15%">
                                                <asp:TextBox ID="tbOrden1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <th style="width: 20%">
                                                <asp:TextBox ID="tbReferenciaTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Referencia</asp:TextBox>
                                            </th>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="tbReferencia1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 60%">
                                                <asp:TextBox ID="tbNombre1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="table-responsive" runat="server" visible="false">
                                    <table style="table-layout: fixed" runat="server">
                                        <tr>
                                            <th style="width: 34%">
                                                <asp:TextBox ID="tbClienteTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Cliente</asp:TextBox>
                                            </th>

                                            <th style="width: 33%">
                                                <asp:TextBox ID="tbCicloTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Ciclo</asp:TextBox>
                                            </th>
                                            <th style="width: 33%">
                                                <asp:TextBox ID="tbOperariosTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Operarios</asp:TextBox>
                                            </th>


                                        </tr>
                                        <tr>
                                            <td style="width: 34%">
                                                <asp:TextBox ID="tbCliente1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="tbCiclo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="tbOperarios1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th style="width: 34%">
                                                <asp:TextBox ID="tbAProducirTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">A producir</asp:TextBox>
                                            </th>
                                            <th style="width: 33%">
                                                <asp:TextBox ID="tbRestantesTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Restantes</asp:TextBox>
                                            </th>

                                        </tr>
                                        <tr>
                                            <td style="width: 34%">
                                                <asp:TextBox ID="tbAProducir1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="tbRestantes1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="REF1" class="tab-pane fade">
                        <div class="row">
                            <ul class="nav nav-pills nav-fill mt-1 bg-white" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#PAUTACONTROL" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">PAUTA DE CONTROL</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#ESTANDAR" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ESTÁNDAR</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#DEFECTOLOGIA" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOLOGIA</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#IMAGENES" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOS GP12</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">EMBALAJE</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#DOCOTROS" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">OTROS</button>
                                </li>
                            </ul>
                            <div class="nav nav-pills nav-fill" runat="server" id="NUM1" role="tablist" style="background-color: #EFEFEF">
                                <button type="button" runat="server" id="MUR1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Datos del muro</button>
                                <button type="button" runat="server" id="DOC1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Documentación de la referencia</button>
                                <button type="button" runat="server" id="EST1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Estructura de materiales</button>
                                <button type="button" runat="server" id="FAB1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Ficha de fabricación</button>
                                <button type="button" runat="server" id="LIB1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Liberación de serie</button>
                                <button type="button" runat="server" id="POL1" class="btn btn-outline-dark shadow shadow-sm" style="text-align: center; width: 16%; font-weight: bold">Polivalencia</button>
                            </div>
                            <div class="tab-content">
                                <div id="PAUTACONTROL" class="tab-pane fade show active ">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <iframe id="PautaControl_1" runat="server" src="tbd" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                        </div>
                                    </div>
                                </div>
                                <div id="ESTANDAR" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class='embed-container'>
                                                <iframe id="GP12_1" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DEFECTOLOGIA" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class='embed-container'>
                                                <iframe id="DEFECTOS_1" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div id="IMAGENES" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="container">
                                                <div id="carouselExampleControls" class="carousel slide shadow" data-bs-ride="carousel">
                                                    <div class="carousel-inner" id="ACTIVOS" runat="server">
                                                        <div class="carousel-item active">
                                                            <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" class="d-block w-100" style="width: 100%;">
                                                        </div>
                                                    </div>
                                                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Previous</span>
                                                    </button>
                                                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Next</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="EMBALAJE" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class='embed-container'>
                                                <iframe id="PAUTAEMBALAJE_1" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DOCOTROS" class="tab-pane fade">
                                    <div class="row invisible">
                                        <div class="col-lg-12" runat="server">
                                            <br />
                                            <h2>Reportar errores en documentación</h2>

                                            <div class="col-lg-1">
                                                <button id="BtnReportar" runat="server" onserverclick="InsertarFeedbackDocumental" type="button" class="btn btn-lg btn-primary" style="width: 100%; height: 60px; text-align: center">
                                                    <span class="glyphicon glyphicon-send"></span>
                                                </button>
                                            </div>
                                            <div class="col-lg-11">
                                                <asp:TextBox ID="tbDenunciaError" runat="server" Style="text-align: center; width: 100%; height: 60px" TextMode="MultiLine"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="table-responsive">
                                            <table style="table-layout: fixed" runat="server">
                                                <tr>
                                                    <th style="width: 34%">
                                                        <asp:TextBox ID="tbFechaIniTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Fecha de incio</asp:TextBox>
                                                    </th>

                                                    <th style="width: 33%">
                                                        <asp:TextBox ID="tbFechaFinTitulo1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Fin previsto</asp:TextBox>
                                                    </th>

                                                </tr>
                                                <tr>
                                                    <td style="width: 34%">
                                                        <asp:TextBox ID="tbFechaIni1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:TextBox ID="tbFechaFin1" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="REF2" class="tab-pane fade">
                        <div class="row">
                            <ul class="nav nav-pills nav-fill mt-1 bg-white" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#PAUTACONTROL2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">PAUTA DE CONTROL</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#ESTANDAR2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ESTÁNDAR</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#DEFECTOLOGIA2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOLOGIA</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#IMAGENES2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOS GP12</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">EMBALAJE</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#DOCOTROS2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">OTROS</button>
                                </li>
                            </ul>
                            <div class="nav nav-pills nav-fill" runat="server" role="tablist" style="background-color: #EFEFEF">
                                <button type="button" runat="server" id="MUR2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Datos del muro</button>
                                <button type="button" runat="server" id="DOC2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Documentación de la referencia</button>
                                <button type="button" runat="server" id="EST2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Estructura de materiales</button>
                                <button type="button" runat="server" id="FAB2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Ficha de fabricación</button>
                                <button type="button" runat="server" id="LIB2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Liberación de serie</button>
                                <button type="button" runat="server" id="POL2" class="btn btn-outline-dark shadow shadow-sm" style="text-align: center; width: 16%; font-weight: bold">Polivalencia</button>
                            </div>
                            <div class="tab-content">
                                <div id="PAUTACONTROL2" class="tab-pane fade show active">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="PautaControl_2" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="ESTANDAR2" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="GP12_2" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DEFECTOLOGIA2" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="DEFECTOS_2" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="IMAGENES2" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="container">
                                                <div id="myCarousel_2" class="carousel slide shadow" data-bs-ride="carousel">
                                                    <div class="carousel-inner" id="ACTIVOS2" runat="server">
                                                        <div class="carousel-item active">
                                                            <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" class="d-block w-100" style="width: 100%;">
                                                        </div>
                                                    </div>
                                                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Previous</span>
                                                    </button>
                                                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Next</span>
                                                    </button>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="EMBALAJE2" class="tab-pane fade">
                                    <div class="embed-responsive embed-responsive-16by9">
                                        <iframe id="PAUTAEMBALAJE_2" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div id="REF3" class="tab-pane fade">
                        <div class="row">
                            <ul class="nav nav-pills nav-fill mt-1 bg-white" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#PAUTACONTROL3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">PAUTA DE CONTROL</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#ESTANDAR3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ESTÁNDAR</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#DEFECTOLOGIA3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOLOGIA</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#IMAGENES3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOS GP12</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">EMBALAJE</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#DOCOTROS3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">OTROS</button>
                                </li>
                            </ul>
                            <div class="nav nav-pills nav-fill" runat="server" role="tablist" style="background-color: #EFEFEF">
                                <button type="button" runat="server" id="MUR3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Datos del muro</button>
                                <button type="button" runat="server" id="DOC3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Documentación de la referencia</button>
                                <button type="button" runat="server" id="EST3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Estructura de materiales</button>
                                <button type="button" runat="server" id="FAB3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Ficha de fabricación</button>
                                <button type="button" runat="server" id="LIB3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Liberación de serie</button>
                                <button type="button" runat="server" id="POL3" class="btn btn-outline-dark shadow shadow-sm" style="text-align: center; width: 16%; font-weight: bold">Polivalencia</button>
                            </div>
                            <div class="tab-content">
                                <div id="PAUTACONTROL3" class="tab-pane fade show active">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="PautaControl_3" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="ESTANDAR3" class="tab-pane fade">
                                    <div class="embed-responsive embed-responsive-16by9">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <iframe id="GP12_3" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DEFECTOLOGIA3" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="DEFECTOS_3" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="IMAGENES3" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="container">
                                                <div id="myCarousel_3" class="carousel slide shadow" data-bs-ride="carousel">
                                                    <div class="carousel-inner" id="ACTIVOS3" runat="server">
                                                        <div class="carousel-item active">
                                                            <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" class="d-block w-100" style="width: 100%;">
                                                        </div>
                                                    </div>
                                                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Previous</span>
                                                    </button>
                                                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Next</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="EMBALAJE3" class="tab-pane fade">
                                    <div class="embed-responsive embed-responsive-16by9">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <iframe id="PAUTAEMBALAJE_3" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="REF4" class="tab-pane fade">
                        <div class="row">
                            <ul class="nav nav-pills nav-fill mt-1 bg-white" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#PAUTACONTROL4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">PAUTA DE CONTROL</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#ESTANDAR4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ESTÁNDAR</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#DEFECTOLOGIA4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOLOGIA</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#IMAGENES4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOS GP12</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">EMBALAJE</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#DOCOTROS4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">OTROS</button>
                                </li>
                            </ul>
                            <div class="nav nav-pills nav-fill" runat="server" role="tablist" style="background-color: #EFEFEF">
                                <button type="button" runat="server" id="MUR4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Datos del muro</button>
                                <button type="button" runat="server" id="DOC4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Documentación de la referencia</button>
                                <button type="button" runat="server" id="EST4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Estructura de materiales</button>
                                <button type="button" runat="server" id="FAB4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Ficha de fabricación</button>
                                <button type="button" runat="server" id="LIB4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Liberación de serie</button>
                                <button type="button" runat="server" id="POL4" class="btn btn-outline-dark shadow shadow-sm" style="text-align: center; width: 16%; font-weight: bold">Polivalencia</button>
                            </div>
                            <div class="tab-content">
                                <div id="PAUTACONTROL4" class="tab-pane fade show active">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="PautaControl_4" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div id="ESTANDAR4" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="GP12_4" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DEFECTOLOGIA4" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="DEFECTOS_4" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div id="IMAGENES4" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="container">
                                                <div id="myCarousel_4" class="carousel slide shadow" data-bs-ride="carousel">
                                                    <div class="carousel-inner" id="ACTIVOS4" runat="server">
                                                        <div class="carousel-item active">
                                                            <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" class="d-block w-100" style="width: 100%;">
                                                        </div>
                                                    </div>
                                                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Previous</span>
                                                    </button>
                                                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Next</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="EMBALAJE4" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="PAUTAEMBALAJE_4" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
   
    <div class="modal fade" id="PopIncrustados" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-warning">

                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" style="height: 900px">
                    <iframe id="IframeIncrustados" class="shadow shadow-lg" runat="server" src="http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=74986" frameborder="0" style="height: 90%; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%"></iframe>

                </div>
                <div class="modal-footer bg-warning">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
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
                            <asp:TemplateField HeaderText="Orden" ItemStyle-BackColor="#ccccff">
                                <ItemTemplate>
                                    <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("ORDEN") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                                    <asp:Label ID="lblConsumo" runat="server" Text='<%#Eval("CONSUMOORDEN") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("NOTAS") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <label id="lblEstructuraOrden" style="font-size: large; font-weight: bold" runat="server" class="mt-1">Materiales necesarios para la producción (todas las órdenes):</label>
                    <asp:GridView ID="GridEstructuraOrden" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
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
                            <asp:TemplateField HeaderText="Total necesario">
                                <ItemTemplate>
                                    <asp:Label ID="lblConsumo" runat="server" Text='<%#Eval("CONSUMOORDEN") %>' Font-Size="Large" Font-Bold="true" />
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
    <div class="modal fade" id="PopPolivalencia" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <label style="font-size: x-large; font-weight: bold">Niveles de polivalencia</label>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="GridPolivalencia" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                        Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                        EmptyDataText="No hay datos para mostrar.">
                        <HeaderStyle BackColor="orange" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-BackColor="#ccccff">
                                <ItemTemplate>
                                    <asp:Label ID="lblTIPO" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("TIPOOPERARIO") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operario">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumero" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("C_CLOCKNUMBER") %>' />

                                    <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("C_OPERATORNAME") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Horas">
                                <ItemTemplate>
                                    <asp:Label ID="lblUbicacion" runat="server" Text='<%#Eval("TIEMPOHORAS") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nivel">
                                <ItemTemplate>
                                    <asp:Label ID="lblConsumo" runat="server" Text='<%#Eval("NIVEL") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Última revisión">
                                <ItemTemplate>
                                    <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("REVISION") %>' Font-Size="Large" Font-Bold="true" />
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

    <div class="modal fade" id="PopNuevaDocumentacion" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content" style="background-color: lightgoldenrodyellow">
                <div class="modal-body">
                    <div runat="server" id="AlertaDOC12">

                        <asp:Label ID="AlertaDOCTEXT" runat="server" Font-Size="Large" Font-Italic="true" BackColor="Transparent" ForeColor="Black" Width="100%" BorderColor="Transparent"></asp:Label><br />
                        <asp:Label ID="AlertaDOCTEXTCAMBIOS" runat="server" Font-Size="Large" Font-Italic="true" BackColor="Transparent" ForeColor="Black" Width="100%" BorderColor="Transparent"></asp:Label>
                        <button type="button" runat="server" id="BTNCerrarAviso" class="btn btn-default" onserverclick="CerrarAvisoDocumentacion" style="text-align: left; background-color: transparent; border-color: transparent; font-weight: bold">>Pincha aquí para cerrar este cuadro de diálogo.<</button>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>




