<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tareas_cambiador.aspx.cs" Inherits="ThermoWeb.PLANIFICACION.Tareas_cambiador" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Previsión de cambios y moldes para taller</title>
    <%-- <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />--%>
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Informe de cambiador
              
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown  ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Acciones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMoldes.aspx">Crear un parte de molde</a></li>
                <li><a class="dropdown-item" href="../LIBERACIONES/EstadoLiberacion.aspx">Liberar una máquina</a></li>
                <li><a class="dropdown-item" href="Tareas_cambiador.aspx?TAB=TALLER">Trasladar molde al taller</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Informes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="Tareas_Cambiador.aspx">Previsión cambios de molde</a></li>
                <li><a class="dropdown-item" href="Tareas_Cambiador.aspx?TAB=LISTAMOLDES">Listado de moldes</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación</a></li>
                <li><a class="dropdown-item" href="Gestion_Ubicaciones_Moldes.aspx">Gestionar ubicaciones (moldes)</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.Add-text').datetimepicker({
                dateFormat: 'dd/mm/yy',
                inline: true,
                showOtherMonths: true,
                changeMonth: true,
                changeYear: true,
                constrainInput: true,
                firstDay: 1,
                navigationAsDateFormat: true,
                yearRange: "c-20:c+10",
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
                timeOnlyTitle: 'Elegir una hora',
                timeText: 'Hora',
                hourText: 'Horas',
                minuteText: 'Minutos',
                secondText: 'Segundos',
                millisecText: 'Milisegundos',
                microsecText: 'Microsegundos',
                timezoneText: 'Uso horario',
                currentText: 'AHORA',
                closeText: 'Cerrar',
                timeFormat: 'HH:mm',
                timeSuffix: '',
                amNames: ['a.m.', 'AM', 'A'],
                pmNames: ['p.m.', 'PM', 'P'],
                isRTL: false
            });
        });
    </script>
    <script type="text/javascript">
        function ShowPopup() {
            document.getElementById("BTNModalUbicacion").click();
            //$("#AUXMODALACCION").click();
        }
    </script>
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid">
            <br />
            <div class="nav nav-pills me-3" id="v-pills-tab" role="tablist">
                <br />
                <button class="nav-link  active" id="PILLPREVISION" runat="server" data-bs-toggle="pill" data-bs-target="#vpillstab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2 me-2"></i>Previsión de cambios</button>
                <button class="nav-link" id="PILLTALLER" runat="server" data-bs-toggle="pill" data-bs-target="#vpillstab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-textarea me-2"></i>Pendientes de taller</button>
                <button class="nav-link" id="PILLLISTAMOLDES" runat="server" data-bs-toggle="pill" data-bs-target="#vpillstab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-book-half me-2"></i>Listado de moldes</button>
            </div>
            <div class="tab-content col-12" id="v-pills-tabContent">
                <div class="tab-pane fade  show active" id="vpillstab1" role="tabpanel" runat="server" aria-labelledby="v-pills-profile-tab">
                    <div class="row">
                        <div class="col-lg-2">
                            <label class="ms-4" style="font-size: larger; font-weight: bold">Fecha inicial</label><br />
                            <input type="text" id="HoraInicio" class="form-control Add-text ms-4 mb-2" autocomplete="off" runat="server">
                        </div>
                        <div class="col-lg-2">
                            <label class="ms-4" style="font-size: larger; font-weight: bold">Fecha final</label><br />
                            <input type="text" id="HoraFin" class="form-control Add-text  ms-4 mb-2" autocomplete="off" runat="server">
                        </div>
                        <div class="col-lg-2"></div>
                        <div class="col-lg-1">
                            <label class="ms-1" style="font-size: larger; font-weight: bold">Total</label><br />
                            <asp:Label ID="CambiosPREV" Font-Size="XX-Large" runat="server" CssClass="ms-3"></asp:Label>
                        </div>
                        <div class="col-lg-1">
                            <label class="ms-1" style="font-size: larger; font-weight: bold">Mañana</label><br />
                            <asp:Label ID="lblCambiosMañana" Font-Size="XX-Large" runat="server" CssClass="ms-3"></asp:Label>
                        </div>
                        <div class="col-lg-1">
                            <label class="ms-1" style="font-size: larger; font-weight: bold">Tarde</label><br />
                            <asp:Label ID="lblCambiosTarde" Font-Size="XX-Large" runat="server" CssClass="ms-3"></asp:Label>
                        </div>
                        <div class="col-lg-1">
                            <label class="ms-1" style="font-size: larger; font-weight: bold">Noche</label><br />
                            <asp:Label ID="lblCambiosNoche" Font-Size="XX-Large" runat="server" CssClass="ms-3"></asp:Label>
                        </div>
                        <div class="col-lg-2">
                            <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-1 me-md-3 mb-md-1">
                                <button id="btnoffcanvas" runat="server" type="button" class="btn btn-outline-dark ms-md-0 bi bi-funnel-fill" onserverclick="Cargar_filtro" style="font-size: larger"></button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <h3>Previsión de cambios de molde</h3>
                        <div style="overflow-y: auto;">
                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%" ShowFooter="true" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBound">
                                <HeaderStyle BackColor="blue" Font-Bold="True" Font-Size="Large" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <FooterStyle BackColor="#e8e8e8" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Máq." ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%" ItemStyle-Font-Size="XX-Large" ItemStyle-ForeColor="white" ItemStyle-BackColor="#3366ff">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("MAQUINA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Orden Actual" ItemStyle-HorizontalAlign="left" ItemStyle-Width="7%" ItemStyle-BackColor="#ccccff">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrden" runat="server" Font-Size="X-Large" Text='<%#Eval("ACT_ORDEN") + " - " + Eval("ACT_PRODUCTO")%>' /><br />
                                            <asp:Label ID="lblProdDesc" runat="server" Text='<%#Eval("ACT_PRODDESCRIPT")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tiempo restante" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%" ItemStyle-BackColor="#ccccff">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfincal" runat="server" Text='<%#"<strong>Termina:</strong> " + Eval("FINCALCULADO") %>' /><br />
                                            <asp:Label ID="lblRestante" runat="server" Text='<%#Eval("TIMETOGO")%>' />
                                            <asp:Label ID="lblCANTPENDIENTE" runat="server" Visible="false" Text='<%#Eval("ACT_CANTPENDIENTE")%>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prioridad" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%" ItemStyle-BackColor="#eeeeee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrioridad" runat="server" Font-Size="XX-Large" Text='<%#Eval("PRIORIDAD")%>' /><br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Próxima orden" ItemStyle-HorizontalAlign="left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNextProduct" CssClass="ms-2" runat="server" Font-Size="X-Large" Text='<%#Eval("NEXT_Orden") + " - " +Eval("NEXT_PRODUCTO") %>' /><br />
                                            <asp:Label ID="lblNextDescript" CssClass="ms-2" runat="server" Text='<%#Eval("NEXT_PRODDESCRIPT")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Molde" ItemStyle-HorizontalAlign="left" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EDITAUBICA" runat="server" CommandName="EditUbicacion2" CommandArgument='<%#Eval("NEXT_MOLDE")%>'><i class="bi bi-geo-alt"></i></asp:LinkButton>

                                            <asp:Label ID="lblNextMolde" Font-Size="X-Large" runat="server" Text='<%#Eval("NEXT_MOLDE") %>' />

                                            <br />
                                            <asp:Label ID="lblNextUbicacion" runat="server" Text='<%#"<strong>Ubicación:</strong> " + Eval("UBICACION")%>' /><br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mano" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNextMano" Font-Size="X-Large" runat="server" Text='<%#Eval("MANOROBOT") %>' /><br />
                                            <asp:Label ID="lblNextManUbicacion" runat="server" Text='<%#Eval("AREA")+ "- Pos." + Eval("UBIMANO")%>' /><br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receta" ItemStyle-HorizontalAlign="left" ItemStyle-Width="5%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNextReceta" runat="server" Text='<%#Eval("NEXT_RECETA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notas" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTiempo" runat="server" Text="<strong>Tiempo estimado:</strong> 30 min." /><br />
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("REMARKS") %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <button type="button" class="btn btn-primary"><i class="bi bi-wrench"></i></button>
                                                <button type="button" class="btn btn-primary"><i class="bi bi-bounding-box"></i></button>
                                                <button type="button" class="btn btn-primary"><i class="bi bi-layout-text-sidebar-reverse"></i></button>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="vpillstab2" role="tabpanel" runat="server" aria-labelledby="v-pills-messages-tab">
                    <div class="container-fluid mt-3">
                        <div class="table-responsive">
                            <asp:GridView ID="dgv_ListadoMoldes" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                EmptyDataText="No hay moldes para mostrar.">
                                <HeaderStyle BackColor="blue" Font-Bold="True" Font-Size="Large" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <FooterStyle BackColor="#e8e8e8" />

                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="XX-Large" ItemStyle-BackColor="#ccccff">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnUbicarTaller" runat="server" CommandName="Reubicar" class="btn btn-success" CommandArgument='<%#Eval("PARTE")%>' Text="Marcar ubicado"><i class="bi bi-hand-thumbs-up"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Parte" ItemStyle-HorizontalAlign="left" ItemStyle-BackColor="#ccccff">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParte" Font-Size="X-Large" Font-Bold="true" runat="server" Text='<%#Eval("PARTE") %>' /><br />
                                            <asp:Label ID="lblFecha" runat="server" Text='<%#"("+ Eval("FECHA") + ")"%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Molde" ItemStyle-HorizontalAlign="left" ItemStyle-BackColor="#ccccff">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMolde" Font-Size="X-Large" runat="server" Text='<%#Eval("MOLDE") %>' /><br />
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("DESCRIPCION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ubicación" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUbicacion" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("UBICACION") %>' /><br />
                                            <asp:Label ID="lblEstado" runat="server" Text='<%#"("+Eval("ESTADO")+")" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Avería">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvería" runat="server" Text='<%#Eval("REPARACION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abierto por" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEncargado" runat="server" Text='<%#Eval("ENCARGADO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnVerParte" CssClass="btn btn-outline-dark me-1" Font-Size="Large" runat="server" CommandName="Redirect" CommandArgument='<%#Eval("PARTE")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="vpillstab3" role="tabpanel" runat="server" aria-labelledby="v-pills-settings-tab">
                    <div class="container-fluid mt-3">

                        <div class="row mt-3">
                            <div class="col-lg-8">
                                <button class="btn btn-outline-dark mt-2 ms-md-5 bi bi-funnel-fill" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasFILTRO" aria-controls="offcanvasRight"></button>


                            </div>
                            <div class="col-lg-4">
                                <div class="input-group  mb-3">
                                    <input class="form-control" list="FiltroMolde" id="tbBuscarMolde" runat="server" autocomplete="off" placeholder="Selecciona un molde...">
                                    <datalist id="FiltroMolde" runat="server">
                                    </datalist>
                                    <button class="btn btn-outline-dark me-md-3" type="button" runat="server" onserverclick="BuscarMoldeinforme">Buscar</button>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="dgv_Listado_MoldesComp" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBoundLISMOL" AutoGenerateColumns="false" EmptyDataText="No hay moldes para mostrar.">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Image ID="IMGliente" runat="server" Width="59px" src='<%#Eval("Logotipo") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Molde">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMolde" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' />
                                                <asp:Label ID="lblDescripcion" Font-Italic="true" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ubicación" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="EDITAUBICA" CssClass="btn btn-primary btn-sm me-md-1 " Font-Size="Large" runat="server" CommandName="EditUbicacion" CommandArgument='<%#Eval("ReferenciaMolde")%>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                <asp:Label ID="lblZona" runat="server" Font-Size="Small" Font-Italic="true" Text='<%#"(" + Eval("Zona") + ")" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mano asignada" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("MANO") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ubicación mano" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblManoUbi" runat="server" Font-Size="X-Large" Text='<%#Eval("MANUBICACION") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab4" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab5" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab6" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                </div>
            </div>

            <%--OFFCANVAS DE FILTROS --%>
            <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasFILTRO" aria-labelledby="offcanvasExampleLabel">
                <div class="offcanvas-header">
                    <h5 class="offcanvas-title" id="offcanvasExampleLabel">Filtros</h5>
                    <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                </div>
                <div class="offcanvas-body">
                    <div>
                        <div class="input-group">
                            <div class="form-check form-switch ms-4 mt-2">
                                <input class="form-check-input" type="checkbox" runat="server" id="SwitchObsoletos" checked="checked">
                                <label class="form-check-label" for="SwitchObsoletos">Ocultar moldes obsoletos</label>
                            </div>
                            <div class="form-check form-switch ms-4 mt-2">
                                <input class="form-check-input" type="checkbox" runat="server" id="SwitchRecientes">
                                <label class="form-check-label" for="SwitchRecientes">Ocultar moldes sin producción reciente</label>
                            </div>
                            <div class="form-check form-switch ms-4 mt-2">
                                <input class="form-check-input" type="checkbox" runat="server" id="SwitchActivas">
                                <label class="form-check-label" for="SwitchActivas">Ocultar moldes activos</label>
                            </div>
                            <div class="form-check form-switch ms-4 mt-2">
                                <input class="form-check-input" type="checkbox" runat="server" id="SwitchThermo">
                                <label class="form-check-label" for="SwitchThermo">Ocultar fuera de planta</label>
                            </div>
                        </div>
                        <button id="Button1" runat="server" type="button" class="btn btn-outline-dark ms-md-0 mt-3" onserverclick="BuscarMoldeinforme" style="font-size: large; width: 100%">Filtrar</button>

                    </div>
                </div>
            </div>
            <!-- Button trigger modal -->
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal" hidden="hidden">
                Launch demo modal
            </button>

            <!-- Modal Molde -->
            <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" runat="server" id="TituloMolde">Detalles de molde</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body ">
                            <div class="row">
                                <div class="col-lg-3">
                                    <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail shadow" alt="...">
                                </div>
                                <div class="col-lg-5">
                                    <label style="font-weight: bold">MOLDE:</label><asp:Label ID="LabelMoldescripcion" runat="server">DESCRIPCION MOLDE</asp:Label><br />
                                    <label style="font-weight: bold">CLIENTE:</label><asp:Label ID="Label1" runat="server">CLIENTE A</asp:Label><br />
                                    <label style="font-weight: bold">MOLDISTA:</label><asp:Label ID="Label2" runat="server">MOLDISTA A</asp:Label><br />
                                    <label style="font-weight: bold">FECHA RECEPCIÓN:</label><asp:Label ID="Label4" runat="server">10/10/2021</asp:Label><br />
                                </div>
                                <div class="col-lg-4">
                                    <h6>Ubicación del molde: </h6>
                                    <asp:TextBox ID="TBMolUbicacion" runat="server" class="form-control"></asp:TextBox>
                                    <h6>Mano Robot: </h6>
                                    <asp:DropDownList ID="TBManDesignado" Width="100%" class="form-select" runat="server"></asp:DropDownList>
                                    <h6>Ubicación de la mano: </h6>
                                    <asp:Label ID="Label3" runat="server">UBI</asp:Label>
                                </div>
                            </div>
                            <ul class="nav nav-tabs" id="myTab" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link active" id="home-tabMOL" data-bs-toggle="tab" data-bs-target="#homeMOL" type="button" role="tab" aria-controls="homeMOL" aria-selected="true">Características</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="profile-tabMOL" data-bs-toggle="tab" data-bs-target="#profileMOL" type="button" role="tab" aria-controls="profileMOL" aria-selected="false">Inyección</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="contact-tabMOL" data-bs-toggle="tab" data-bs-target="#contactMOL" type="button" role="tab" aria-controls="contactMOL" aria-selected="false">Refrigeración</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="contact-tabMOL2" data-bs-toggle="tab" data-bs-target="#contactMOL2" type="button" role="tab" aria-controls="contactMOL2" aria-selected="false">Movimientos</button>
                                </li>
                            </ul>
                            <div class="tab-content" id="myTabContent">
                                <div class="tab-pane fade show active" id="homeMOL" role="tabpanel" aria-labelledby="home-tabMOL">
                                    <div class="row shadow" style="background-color: #e6e6e6">
                                        <div class="col-lg-3">
                                            <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                        </div>
                                        <div class="col-lg-2">
                                            <label style="font-weight: bold">Dimensiones:</label><br />
                                            <label style="font-weight: bold">H:</label><asp:Label ID="Label6" runat="server">100mm</asp:Label><br />
                                            <label style="font-weight: bold">V:</label><asp:Label ID="Label7" runat="server">200mm</asp:Label><br />
                                            <label style="font-weight: bold">E:</label><asp:Label ID="Label8" runat="server">300mm</asp:Label><br />
                                        </div>
                                        <div class="col-lg-3">
                                            <label style="font-weight: bold">Peso (Kg):</label><asp:Label ID="Label13" runat="server">30Kg</asp:Label><br />
                                            <label style="font-weight: bold">Cavidades:</label><asp:Label ID="Label11" runat="server">5</asp:Label><br />
                                            <label style="font-weight: bold">Punt. cavidad:</label><asp:Label ID="Label12" runat="server">4</asp:Label><br />
                                        </div>
                                        <div class="col-lg-3">
                                            <label style="font-weight: bold">Ø Anillo:</label><asp:Label ID="Label5" runat="server">100mm</asp:Label><br />
                                            <label style="font-weight: bold">Bulón:</label><asp:Label ID="Label9" runat="server">200mm</asp:Label><br />
                                            <label style="font-weight: bold">Bridas:</label><asp:Label ID="Label10" runat="server">SI</asp:Label><br />
                                        </div>
                                    </div>

                                </div>
                                <div class="tab-pane fade" id="profileMOL" role="tabpanel" aria-labelledby="profile-tabMOL">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h5>Proceso</h5>
                                            <asp:DropDownList ID="DropProcesoMold" class="form-select" runat="server" Width="100%">
                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                <asp:ListItem Value="1">Bi-Inyección</asp:ListItem>
                                                <asp:ListItem Value="2">Con versiones</asp:ListItem>
                                                <asp:ListItem Value="3">Convencional</asp:ListItem>
                                                <asp:ListItem Value="4">Inyección con gas</asp:ListItem>
                                                <asp:ListItem Value="5">Mu-Cell</asp:ListItem>
                                                <asp:ListItem Value="6">Sandwich</asp:ListItem>
                                                <asp:ListItem Value="7">Sobremoldeo</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6">
                                            <h5>Entrada</h5>
                                            <asp:DropDownList ID="DropDownList1" class="form-select" runat="server" Width="100%">
                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                <asp:ListItem Value="1">Abanico</asp:ListItem>
                                                <asp:ListItem Value="2">Cuerno</asp:ListItem>
                                                <asp:ListItem Value="3">Directa</asp:ListItem>
                                                <asp:ListItem Value="4">Laminar</asp:ListItem>
                                                <asp:ListItem Value="5">Submarina</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h5>Inyección</h5>
                                            <asp:DropDownList ID="DropDownList2" class="form-select" runat="server" Width="100%">
                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                <asp:ListItem Value="1">Bebedero/Canal frío</asp:ListItem>
                                                <asp:ListItem Value="2">Boquilla caliente</asp:ListItem>
                                                <asp:ListItem Value="3">Cámara caliente</asp:ListItem>
                                                <asp:ListItem Value="4">Inyección secuencial</asp:ListItem>
                                                <asp:ListItem Value="5">Sobre canal frío</asp:ListItem>
                                                <asp:ListItem Value="6">Sobre pieza</asp:ListItem>
                                                <asp:ListItem Value="7">Sobre pieza con mazarota</asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row shadow">
                                        <div class="col-lg-12" style="background-color: #e6e6e6">
                                            <h5>Cámara caliente</h5>
                                            <label style="font-weight: bold">Nº Zonas:</label><asp:Label ID="Label14" runat="server"></asp:Label><br />
                                            <label style="font-weight: bold">Nº Boquillas:</label><asp:Label ID="Label15" runat="server"></asp:Label><br />
                                            <label style="font-weight: bold">Tipo de boquilla:</label><asp:Label ID="Label16" runat="server"></asp:Label><br />
                                            <label style="font-weight: bold">Nº de Resistencias:</label><asp:Label ID="Label17" runat="server"></asp:Label><br />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                        </div>
                                        <div class="col-lg-3">
                                            <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                        </div>
                                        <div class="col-lg-3">
                                            <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                        </div>
                                        <div class="col-lg-3">
                                            <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="contactMOL" role="tabpanel" aria-labelledby="contact-tabMOL">
                                    <div class="row mt-2">
                                        <div class="col-lg-4">
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox1" value="option1">
                                                <label class="form-check-label" for="inlineCheckbox1">Correderas</label>
                                            </div>
                                            <br />
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox2" value="option2">
                                                <label class="form-check-label" for="inlineCheckbox2">Expulsores inclinados</label>
                                            </div>
                                            <br />
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox3" value="option3">
                                                <label class="form-check-label" for="inlineCheckbox3">Placa embridaje movil</label>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox4" value="option4">
                                                <label class="form-check-label" for="inlineCheckbox4">Placa intermedia</label>
                                            </div>
                                            <br />
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox5" value="option5">
                                                <label class="form-check-label" for="inlineCheckbox5">Cavidad</label>
                                            </div>
                                            <br />
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox6" value="option6">
                                                <label class="form-check-label" for="inlineCheckbox6">Punzón</label>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox7" value="option7">
                                                <label class="form-check-label" for="inlineCheckbox7">Placa embridaje fija</label>
                                            </div>

                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox8" value="option8">
                                                <label class="form-check-label" for="inlineCheckbox8">Agua lado movil</label>
                                            </div>
                                            <br />
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox9" value="option9">
                                                <label class="form-check-label" for="inlineCheckbox9">Agua lado fijo</label>
                                            </div>

                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox10" value="option10">
                                                <label class="form-check-label" for="inlineCheckbox10">Aceite lado móvil</label>
                                            </div>
                                            <br />
                                            <div class="form-check form-check-inline">
                                                <input class="form-check-input" type="checkbox" id="inlineCheckbox11" value="option11">
                                                <label class="form-check-label" for="inlineCheckbox11">Aceite lado fijo</label>
                                            </div>

                                        </div>

                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                        </div>
                                        <div class="col-lg-3">
                                            <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                        </div>
                                        <div class="col-lg-3">
                                            <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                        </div>
                                        <div class="col-lg-3">
                                            <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="contactMOL2" role="tabpanel" aria-labelledby="contact-tabMOL2">
                                    333
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary">Save changes</button>
                        </div>
                    </div>
                </div>
            </div>


            <!-- Button trigger modal Ubicacion -->
            <button type="button" id="BTNModalUbicacion" runat="server" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#ModalUbicacion" hidden="hidden">
                Launch demo modal
            </button>

            <!-- Modal -->
            <div class="modal fade" id="ModalUbicacion" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">

                            <label class="h4" id="UbicaMolde" runat="server">3546</label>
                            <label class="h4 ms-2" id="UbicaMoldeNombre" runat="server">nombre de molde</label>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h5>Ubicación</h5>
                                    <div class="input-group">
                                        <span class="input-group-text" id="basic-addon1"><i class="bi bi-geo-alt"></i></span>
                                        <asp:DropDownList ID="UbicacionMolde" runat="server" class="form-select">
                                        </asp:DropDownList>
                                       
                                    </div>

                                </div>
                                <div class="col-lg-6">
                                    <h5>&nbsp</h5>
                                    <div class="form-check">
                                        <input class="form-check-input" runat="server" type="checkbox" value="" id="flexCheckDefault">
                                        <label class="form-check-label" for="flexCheckDefault">
                                            Molde activo
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="LblModificado" CssClass="mb-3" runat="server" Font-Size="Small"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-lg-12">
                                    <img id="ImgUbicacion" runat="server" class="img-fluid border border-1 rounded rounded-2 mt-2" src="http://facts4-srv/thermogestion/SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" />
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                            <asp:LinkButton ID="BtnGuardarAccion" runat="server" OnClick="Actualizar_Ubicacion" class="btn btn-success"><i class="bi bi-sd-card"></i></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
