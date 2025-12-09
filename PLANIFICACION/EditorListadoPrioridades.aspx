<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EditorListadoPrioridades.aspx.cs" Inherits="ThermoWeb.PLANIFICACION.EditorPrioridades_" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Prioridades (MODO EDICIÓN)</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />

</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Editor de prioridades           
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown me-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Producción
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../ListaFichasParametros.aspx">Fichas de parámetros</a></li>
                <li><a class="dropdown-item" href="../LIBERACIONES/EstadoLiberacion.aspx">Liberaciones de serie</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown  me-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Calidad
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../GP12/GP12Historico.aspx">Histórico GP12</a></li>
                <li><a class="dropdown-item" href="../CALIDAD/ListaAlertasCalidad.aspx">Listado No conformidades</a></li>

            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Mantenimiento
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMaquina.aspx">Partes de máquina</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMoldes.aspx">Partes de molde</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <script type="text/javascript">
        function ShowPopup() {
            document.getElementById("AUXMODALACCION").click();
        }
        function myFunction() {
            document.getElementById("RecargarTodo").click();
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
        <div class="row" runat="server">
            <div class="col-lg-4">
                <h1 class="ms-4 mt-3">Editor de prioridades</h1>
            </div>
            <div class="col-lg-2">
                <h6 class="mt-2">&nbsp</h6>
                <button id="Button2" runat="server" onserverclick="ActualizarOrdenesSecuencial" type="button" class="btn btn-success shadow" style="width: 100%; text-align: left">
                    <i class="bi bi-sd-card"></i>&nbsp Actualizar marcadas</button>
            </div>
            <div class="col-lg-2">
                <h6 class="mt-2">&nbsp</h6>
                <button id="Button3" runat="server" onserverclick="MandarMail" type="button" class="btn btn-primary shadow" style="width: 100%; text-align: left">
                    <i class="bi bi-send-check"></i>&nbsp Enviar prioridades</button>
            </div>
            <div class="col-lg-2">
                <h6 class="mt-2">&nbsp</h6>
                <button id="Button1" runat="server" onserverclick="BorrarPrioridades" type="button" class="btn btn-danger shadow" style="width: 100%; text-align: left">
                    <i class="bi bi-trash3"></i>&nbsp Borrar prioridades</button>
            </div>
            <div class="col-lg-2">
                <h6 class="mt-2">Órdenes a mostrar:</h6>
                <asp:DropDownList ID="TipoAlerta" runat="server" CssClass="form-select border border-dark shadow" Font-Size="Large" AutoPostBack="True">
                    <asp:ListItem Text="Por defecto" runat="server" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Todas" runat="server" Value="100"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <style>
            .ChkBoxClass input {
                width: 25px;
                height: 25px;
                box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.075) !important;
            }
        </style>     
        <div class="row mt-3">
            <asp:GridView ID="dgv_AccionesAbiertas" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                Width="98.5%" CssClass="table table-responsive shadow p-3 border border-secondary rounded" AutoGenerateColumns="false"
                OnRowDataBound="GridView_RowDataBound" EmptyDataText="No hay datos para mostrar">
                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                <RowStyle BackColor="White" />
                <AlternatingRowStyle BackColor="#eeeeee" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <HeaderTemplate>
                            <button id="OrdenarProd" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-primary" style="width: 100%; text-align: left">
                                ¿Actualizar?</button>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Style="height: 25px; width: 25px" class="ChkBoxClass" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <HeaderTemplate>
                            <button id="OrdenarMaq" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-primary" style="width: 100%; text-align: left">
                                <i class="bi bi-arrow-down-up me-2"></i>Máquina</button>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMaquina" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("Maquina") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="8%" ItemStyle-VerticalAlign="Middle">
                        <HeaderTemplate>
                            <button id="OrdenarPrior" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-primary" style="width: 100%; text-align: left">
                                <i class="bi bi-arrow-down-up me-2"></i>Prioridad</button>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="Selecprioridad" runat="server" CssClass="form-select border border-secondary shadow-sm" Font-Size="Medium">
                                <asp:ListItem Text=" " Value="100"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="8%">
                        <HeaderTemplate>
                            <button id="OrdenarOrden" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-primary" style="width: 100%; text-align: left">
                                <i class="bi bi-arrow-down-up me-2"></i>Orden</button>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOrden" CssClass="ms-2" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Orden") %>' /><br />
                            <asp:Label ID="lblTiempo" CssClass="ms-2" runat="server" Text='<%#"(" + Eval("Tiempo") + ")" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="32%">
                        <HeaderTemplate>
                            <button id="OrdenarReferencia" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-primary" style="width: 100%; text-align: left">
                                <i class="bi bi-arrow-down-up me-2"></i>Referencia</button>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("REFERENCIA") %>' Visible="false" />
                            <asp:Label ID="lblReferencia2" Font-Bold="true" Font-Size="Large" runat="server" Text='<%#Eval("REF") %>' /><br />
                            <asp:Label ID="lblReferenciaDescripcion" runat="server" Text='<%#Eval("DESCRIPCION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acción orden" ItemStyle-Width="35%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAccionOrden" runat="server" TextMode="MultiLine" CssClass="border border-secondary shadow-sm" Width="100%" Height="30px" Text='<%#Eval("RemarkOrden") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div runat="server" visible="false">
            <div class="table-responsive">
                <asp:GridView ID="GridView2" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                    Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                    EmptyDataText="There are no data records to display.">
                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                    <%-- DataKeyNames="Id" ShowFooter="true"  "
            "OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing OnRowUpdating="GridView_RowUpdating" "
            OnRowCommand="gridView_RowCommand" OnRowDeleting="GridView_RowDeleting" OnRowCommand="gridView_RowCommand" --%>
                    <EditRowStyle BackColor="#ffffcc" />
                    <Columns>
                        <asp:TemplateField HeaderText="Máq." ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblMaquina2" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("Maquina") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prioridad" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblPrioridad2" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("Prioridaddec") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Orden" ItemStyle-Width="6%">
                            <ItemTemplate>
                                <asp:Label ID="lblOrden2" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Orden") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblREFNUM" runat="server" Text='<%#Eval("REF") %>' />
                                <br />
                                <asp:Label ID="lblReferencia2" runat="server" Text='<%#Eval("Descripcion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tiempo Restante" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblTiempo2" runat="server" Text='<%#Eval("Tiempo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acción orden" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="lblAccionOrden2" runat="server" Text='<%#Eval("RemarkOrden") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Otras acciones" ItemStyle-Width="35%">
                            <ItemTemplate>
                                <strong>Producto:</strong><asp:Label ID="lblAccionProducto2" runat="server" Text='<%#Eval("RemarkProducto") %>' />
                                <br />
                                <strong>Mantenimiento:</strong><asp:Label ID="lblAccionMolde2" runat="server" Text='<%#Eval("RemarkMolde") %>' />
                                <asp:Label ID="lblAccionMaquina2" runat="server" Text='<%#Eval("RemarkMaquina") %>' />
                                <br />
                                <strong>Notas:</strong><asp:Label ID="lblAccionReceta2" runat="server" Text='<%#Eval("RemarksReceta") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>


    </div>
</asp:Content>
