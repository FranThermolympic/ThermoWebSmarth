<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listado_Manos_Robot.aspx.cs" Inherits="ThermoWeb.Listado_Manos_Robot" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Gestión de Manos de Robot</title>
    <%-- <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />--%>
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Listado de Manos de Robot
              
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ShowPopDocVinculados() {
            document.getElementById("btnPopDocVinculados").click();
        }
        function ShowPopListaMoldes() {
            document.getElementById("btnPopListaMoldes").click();
        }
        function ExisteMano() {
            alert("El número de Mano de Robot ya existe, no se creará ningún registro nuevo.");
        }
        function GuardadoNok() {
            alert("¡El estándar de número de mano no se cumple!</br>Por favor, asígnale un valor numérico a la mano.");
        }
        function ShowPopup() {
            document.getElementById("BTNModalUbicacion").click();
        }
        function ShowPopupAsignaMano() {
            document.getElementById("BTNModalAsignaMano").click();
        }

    </script>

    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid">
            <div class="row">
            <div class="nav nav-pills me-3 mt-1" id="v-pills-tab" role="tablist">
                <br />
                <button class="nav-link  active shadow" id="PILLPREVISION" runat="server" data-bs-toggle="pill" data-bs-target="#vpillstab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-layers-half me-2"></i>Manos de robot</button>
                <button class="nav-link shadow" id="PILLLISTAMOLDES" runat="server" data-bs-toggle="pill" data-bs-target="#vpillstab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2 me-2"></i>Asignación de moldes</button>
            </div>
                </div>
            <div class="tab-content col-12" id="v-pills-tabContent">
                <div class="tab-pane fade  show active" id="vpillstab1" role="tabpanel" runat="server" aria-labelledby="v-pills-profile-tab">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-1 me-md-3 mb-md-1">
                                <button type="button" id="btnPopDocVinculados" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopDocVinculados"></button>
                                <button type="button" id="btnPopListaMoldes" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopListaMoldes"></button>
                                <div class="bg-white">
                                    <button id="btnoffcanvas" runat="server" type="button" class="btn btn-outline-dark ms-md-0 mb-2 bi bi-funnel-fill" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger"></button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="container-fluid">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgv_ListadoManos" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating"
                                        OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound" OnRowCancelingEdit="GridView_RowCancelingEdit"
                                        EmptyDataText="There are no data records to display.">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <RowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <FooterStyle BackColor="Silver" BorderColor="#808080" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Mano" HeaderStyle-Width="40%" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="shadow shadow-sm">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" CssClass="me-md-1 " Font-Size="X-Large" runat="server" CommandName="Edit"><i class="bi bi-pencil"></i></asp:LinkButton>

                                                    <asp:Image ID="IMGliente" CssClass="rounded shadow shadow-sm me-2" runat="server" Width="49px" src='<%#Eval("Logotipo") %>' />
                                                    <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("MANO") %>' />
                                                    <asp:Label ID="lblManoDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="txtMano" runat="server" Width="20%" Font-Size="X-Large" Text='<%#Eval("MANO") %>' />
                                                    <asp:TextBox ID="txtManoDescripcion" Width="75%" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ubicación" HeaderStyle-Width="20%" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="border-start border-1 ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#"<strong>Ub.: </strong>"+ Eval("UBICACION") %>' />
                                                    <asp:Label ID="lblAlmacenUB" Font-Italic="true" runat="server" Text='<%#Eval("AREA") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label runat="server" Font-Bold="true" Text="Área: "></asp:Label>
                                                    <asp:DropDownList ID="listAREA" runat="server">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">Obsoleto</asp:ListItem>
                                                        <asp:ListItem Value="4">Cubetas estantería</asp:ListItem>
                                                        <asp:ListItem Value="2">Cuarto de manos</asp:ListItem>
                                                        <asp:ListItem Value="8">Junto a molde</asp:ListItem>
                                                        <asp:ListItem Value="5">Máquina 32</asp:ListItem>
                                                        <asp:ListItem Value="3">Máquina 34</asp:ListItem>
                                                        <asp:ListItem Value="7">Máquina 43</asp:ListItem>
                                                        <asp:ListItem Value="6">Máquina 48</asp:ListItem>
                                                    </asp:DropDownList><br />
                                                    <asp:Label runat="server" Font-Bold="true" Text="Ubicación: "></asp:Label><asp:TextBox ID="txtUbicacion" runat="server" Text='<%#Eval("UBICACION") %>' />

                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Molde / Notas" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMUMOL" runat="server" Visible="false" Text='<%#Eval("NUMOL") %>' />
                                                    <asp:Label ID="lblMolde" Font-Size="Large" Font-Italic="true" runat="server" Text='<%#Eval("Molde") %>' />
                                                    <asp:LinkButton ID="BTNVerMasMold" runat="server" Visible="false" class="btn btn-outline-dark border-0 btn-lg" CommandName="ListaMoldes" CommandArgument='<%#Eval("MANO") %>'><i class="bi bi-bar-chart-steps"></i></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="25%" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("NOTA") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNotas" runat="server" Width="100%" Text='<%#Eval("NOTA") %>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="right" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="15%">
                                                <HeaderTemplate>

                                                    <asp:LinkButton ID="ButtonAdd" runat="server" CommandName="AddNew" Text="Añadir nueva fila" Font-Bold="true" CssClass="btn btn-outline-dark bg-white float-end"><i class="bi bi-plus-lg"></i></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%--Botones de eliminar y editar cliente...--%>
                                                    <div class="btn-group" role="group" aria-label="Basic example">
                                                    </div>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-success me-md-1" Width="100%" CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar esta fila?');"><i class="bi bi-sd-card"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-primary me-md-1" Width="100%" CommandName="Cancel"><i class="bi bi-caret-left-fill"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger me-md-1" Width="100%" CommandName="Borrar" CommandArgument='<%#Eval("MANO") %>' OnClientClick="return confirm('¿Seguro que quieres eliminar esta fila?');"><i class="bi bi-trash"></i></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="vpillstab3" role="tabpanel" runat="server" aria-labelledby="v-pills-settings-tab">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-1 me-md-3 mb-md-1">
                                <div class="bg-white">
                                    <button class="btn btn-outline-dark ms-md-0 mb-2 bi bi-funnel-fill" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasFILTRO" aria-controls="offcanvasRight" style="font-size: larger"></button>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="container-fluid">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgv_Listado_MoldesComp" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBoundLISMOL" AutoGenerateColumns="false" EmptyDataText="No hay moldes para mostrar.">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <RowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Molde y ubicación" ItemStyle-Width="45%" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>

                                                    <asp:Image ID="IMGliente" runat="server" CssClass="rounded shadow shadow-sm me-2" runat="server" Width="49px" src='<%#Eval("Logotipo") %>' />
                                                    <asp:Label ID="lblMolde" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' />
                                                    <asp:Label ID="lblDescripcion" Font-Italic="true" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                    <button type="button" id="btnObsoleto" runat="server" class="btn btn-sm btn-warning disabled" visible="false"><i class="bi bi-calendar-x"></i></button>
                                                    <button type="button" id="btnRetirado" runat="server" class="btn btn-sm btn-danger disabled" style="font-size: xx-small" visible="false">OBS</button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="EDITAUBICA" CssClass="me-md-1 " Font-Size="X-Large" runat="server" CommandName="EditUbicacion" CommandArgument='<%#Eval("ReferenciaMolde")%>'><i class="bi bi-pencil"></i></asp:LinkButton>

                                                    <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                                    <asp:Label ID="lblZona" runat="server" Font-Size="Small" Font-Italic="true" Text='<%#"(" + Eval("Zona") + ")" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mano asignada" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="border-start border-1" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="EDITAMANO" CssClass="me-md-2 " Font-Size="X-Large" Font-Bold="true" runat="server" CommandName="EditMano" CommandArgument='<%#Eval("ReferenciaMolde")%>'><i class="bi bi-link"></i></asp:LinkButton>
                                                    <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("MANO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ubicación mano" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblManoArea" runat="server" Font-Italic="true" Font-Size="X-Large" Text='<%#Eval("AREA") %>' />
                                                    <asp:Label ID="lblManoUbi" runat="server" Font-Italic="true" Font-Size="Large" Text='<%#Eval("MANUBICACION") %>' />
                                                    
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

            <%-- MODALES DE EDICION/CREACION--%>
            <div class="modal fade" id="PopDocVinculados" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header bg-primary shadow">
                            <h5 class="modal-title text-white">Añadir mano</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <h5>Número</h5>
                                    <div class="form-floating mb-3">
                                        <input type="text" class="form-control" id="NuevoNumMano" runat="server" placeholder="4444" autocomplete="off">
                                        <label for="NuevoNumMano">Número propuesto</label>
                                    </div>
                                </div>
                                <div class="col-lg-8">
                                    <h5>Descripción</h5>
                                    <div class="form-floating mb-3">
                                        <input type="text" class="form-control" id="NuevoNomMano" runat="server" placeholder="Mano" autocomplete="off">
                                        <label for="NuevoNomMano">Nombre de la mano</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <h5>Área</h5>
                                    <div class="form-floating">
                                        <asp:DropDownList ID="NuevoAreaSelect" class="form-select" runat="server">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">Obsoleto</asp:ListItem>
                                                        <asp:ListItem Value="4">Cubetas estantería</asp:ListItem>
                                                        <asp:ListItem Value="2">Cuarto de manos</asp:ListItem>
                                                        <asp:ListItem Value="8">Junto a molde</asp:ListItem>
                                                        <asp:ListItem Value="5">Máquina 32</asp:ListItem>
                                                        <asp:ListItem Value="3">Máquina 34</asp:ListItem>
                                                        <asp:ListItem Value="7">Máquina 43</asp:ListItem>
                                                        <asp:ListItem Value="6">Máquina 48</asp:ListItem>
                                                    </asp:DropDownList><br />
                                        <label for="NuevoAreaSelect">Área de la ubicación</label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <h5>Ubicación</h5>
                                    <div class="form-floating mb-3">
                                        <input type="text" class="form-control" id="NuevoUbicacion" runat="server" placeholder="Ubicacion" autocomplete="off">
                                        <label for="NuevoUbicacion">Ubicación</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5>Notas</h5>
                                    <div class="form-floating mb-3">
                                        <input type="text" class="form-control" id="NuevoNotas" runat="server" placeholder="Notas" autocomplete="off">
                                        <label for="NuevoNotas">Notas</label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer bg-primary shadow">
                            <button type="button" class="btn btn-success shadow shadow-sm" runat="server" onserverclick="Añadir_Mano">Añadir</button>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="PopListaMoldes" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-primary shadow">
                            <h5 class="modal-title text-white"><i class="bi bi-info-circle" id="H5TituloListaMolde" runat="server"></i></h5>

                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:GridView ID="DgvListaMoldes" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating"
                                        OnRowCommand="GridView_RowCommand" OnRowDataBound="OnRowDataBound" OnRowCancelingEdit="GridView_RowCancelingEdit"
                                        EmptyDataText="There are no data records to display.">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                        <RowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Moldes vinculados" HeaderStyle-Width="40%" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="shadow shadow-sm">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMolde" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' />
                                                    <asp:Label ID="lblMoldeDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer bg-primary shadow">
                            <button type="button" class="btn btn-danger btn-sm" data-bs-dismiss="modal">Cerrar</button>
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

            <!-- Button trigger modal AsignaMano -->
            <button type="button" id="BTNModalAsignaMano" runat="server" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#ModalAsignaMano" hidden="hidden">
                Launch demo modal
            </button>
            <!-- Modal -->
            <div class="modal fade" id="ModalAsignaMano" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">

                            <label class="h4" id="AsignaMano" runat="server">3546</label>
                            <label class="h4 ms-2" id="AsignaManoNombre" runat="server">nombre de molde</label>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5>Mano de robot actual:</h5>
                                    <div class="input-group mb-3">
                                        <span class="input-group-text"><i class="bi bi-link-45deg"></i></span>
                                        <input type="text" disabled="disabled" id="InputManoActual" runat="server" class="form-control" placeholder="Username" aria-label="Username" aria-describedby="basic-addon1">
                                    </div>
                                    <h5>Nueva mano a asignar:</h5>
                                    <div class="input-group mb-3">
                                        <span class="input-group-text"><i class="bi bi-save"></i></span>
                                        <input class="form-control" list="ListaManosAsignacion" id="InputAsignaNuevaMano" runat="server" placeholder="Escribe una mano...">
                                        <datalist id="ListaManosAsignacion" runat="server">
                                        </datalist>
                                    </div>
                                </div>

                            </div>
                            

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Actualizar_Mano" class="btn btn-success"><i class="bi bi-sd-card"></i></asp:LinkButton>

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
                    <h6>Estado:</h6>
                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" runat="server" id="SwitchActivas">
                        <label class="form-check-label" for="SwitchActivas">Mostrar obsoletas</label>
                    </div>

                    <h6>Mano:</h6>
                    <div class="input-group mb-3">
                        <input class="form-control" list="FiltroManos" id="InputFiltroManos" runat="server" placeholder="Escribe una mano...">
                        <datalist id="FiltroManos" runat="server">
                        </datalist>
                    </div>
                    <div class="input-group">
                        <select class="form-select" runat="server" id="InputFiltroArea">
                            <option selected value="0">-</option>
                            <option value="1">Obsoleto</option>
                            <option value="4">Cubetas estantería</option>
                            <option value="2">Cuarto de manos</option>
                            <option value="5">Máquina 32</option>
                            <option value="3">Máquina 34</option>
                            <option value="6">Máquina 48</option>
                        </select>

                    </div>
                    <div class="input-group mb-3">
                        <input class="form-control" list="FiltroUbicacion" id="InputFiltroUbicacion" runat="server" placeholder="Escribe una ubicación...">
                        <datalist id="FiltroUbicacion" runat="server">
                        </datalist>
                    </div>
                    <br />
                    <h6>Ordenar por:</h6>
                    <div class="input-group">
                        <select class="form-select" runat="server" id="selecorderby">
                            <option selected value="0">Por defecto</option>
                            <option value="1">Por área</option>
                            <option value="2">Por ubicación</option>
                        </select>
                        <button class="btn btn-outline-dark me-md-3" type="button" runat="server" onserverclick="Cargar_filtro">Filtrar</button>
                    </div>
                </div>
            </div>
            <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasFILTRO" aria-labelledby="offcanvasExampleLabel">
                <div class="offcanvas-header">
                    <h5 class="offcanvas-title" id="offcanvasExampleLabel2">Filtros</h5>
                    <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                </div>
                <div class="offcanvas-body">
                    <div>
                        <input class="form-control" list="FiltroMolde" id="tbBuscarMolde" runat="server" autocomplete="off" placeholder="Selecciona un molde...">
                        <datalist id="FiltroMolde" runat="server">
                        </datalist>
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
                                <input class="form-check-input" type="checkbox" runat="server" id="Checkbox1">
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
        </div>
    </div>
</asp:Content>

