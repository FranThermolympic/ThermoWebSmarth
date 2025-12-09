<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="InformeMoldes.aspx.cs" Inherits="ThermoWeb.MANTENIMIENTO.InformesMoldes" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Informe de moldes</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Informes de moldes
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ShowPopupUbicacion() {
            document.getElementById("ModalUbicacion").click();
            //$("#AUXMODALACCION").click();
        }
        function ShowPopup() {
            document.getElementById("BTNModalUbicacion").click();
            //$("#AUXMODALACCION").click();
        }


    </script>
    <div class="d-flex" id="wrapper">
        <div class="flex-shrink-0 p-3 bg-dark shadow-sm" style="width: 240px;">
            <a class="btn btn btn-outline-primary border-0" href="MantenimientoIndex.aspx" role="button" style="width: 100%; color: white; text-align: start"><i class="bi bi-building me-2"></i>Página principal</a><br />

            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-tools me-md-2"></i>Partes de trabajo    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="../MANTENIMIENTO/ReparacionMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Moldes</a></div>
            </div>

            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-list-check me-md-2"></i>Acciones abiertas <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample2">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMaquina.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Moldes</a></div>
            </div>
            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-journals me-md-2"></i>Informes    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample3">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de moldes</a><br />
                </div>
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="InformePerifericos.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de periféricos</a></div>
            </div>


            <i class="bi bi-journals me-md-2"></i>

        </div>
        <div class="flex-fill" id="page-content-wrapper">
            <div class="row mt-3">
                <div class="col-lg-8"></div>
                <div class="col-lg-4">
                    <div class="input-group">
                        <input class="form-control" list="FiltroMolde" id="tbBuscarMolde" runat="server" placeholder="Selecciona un molde...">
                        <datalist id="FiltroMolde" runat="server">
                        </datalist>
                        <button class="btn btn-outline-dark me-md-3" type="button" runat="server" onserverclick="Rellenar_grid">Filtrar</button>
                    </div>
                </div>

            </div>
            <div class="nav nav-pills me-3 " id="v-pills-tab" role="tablist">
                <br />
                <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2 me-2"></i>General molde</button>
                <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-textarea me-2"></i>Estado preventivo</button>
                <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-book-half me-2"></i>Histórico reparaciones</button>
                <button class="nav-link" id="PILLMAQPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab4" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Val.</i></button>
            </div>
            <div class="tab-content col-12" id="v-pills-tabContent">
                <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                    <div class="container-fluid mt-3">
                        <div class="table-responsive">
                            <asp:GridView ID="dgv_ListadoMoldes" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" EmptyDataText="No hay moldes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                             <button id="BTNOrdenaMolde" runat="server" onserverclick="Rellenar_grid" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: center; font-weight: bold"><i class="bi bi-arrow-down-up me-2"></i>Molde</button>                                          
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMolde" runat="server" Font-Size="X-Large" Text='<%#Eval("ReferenciaMolde") %>' />
                                            <asp:Label ID="lblDescripcion" Font-Italic="true" runat="server" Text='<%#Eval("Descripcion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                         <HeaderTemplate>
                                             <button id="BTNOrdenaNumReparaciones" runat="server" onserverclick="Rellenar_grid" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: center; font-weight: bold"><i class="bi bi-arrow-down-up me-2"></i>Nº Reparaciones</button>                                          
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblReparaciones" runat="server" Font-Size="X-Large"  Text='<%#Eval("NumReparaciones") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                         <HeaderTemplate>
                                             <button id="BTNOrdenaPreventivo" runat="server" onserverclick="Rellenar_grid" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: center; font-weight: bold"><i class="bi bi-arrow-down-up me-2"></i>Preventivo</button>                                          
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreventivo" runat="server" Font-Size="Large" Font-Italic="true" Text='<%#Eval("TIPOMANT") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="left" >
                                        <HeaderTemplate>
                                             <button id="BTNOrdenaUbicacion" runat="server" onserverclick="Rellenar_grid" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: center; font-weight: bold"><i class="bi bi-arrow-down-up me-2"></i>Ubicación</button>                                          
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:LinkButton ID="EDITAUBICA"  runat="server" CommandName="EditUbicacion" CommandArgument='<%#Eval("ReferenciaMolde")%>'><i class="bi bi-geo-alt"></i></asp:LinkButton>
                                        
                                            <asp:Label ID="lblUbicacion" runat="server" Font-Size="X-Large" Text='<%#Eval("Ubicacion") %>' />
                                            <asp:Label ID="lblZona" runat="server" Font-Size="Small" Font-Italic="true" Text='<%#"(" + Eval("Zona") + ")" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="border-start border-bottom-0 border-top-0 border-top-0 border-dark">
                                         <HeaderTemplate>
                                             <button id="BTNOrdenaMano" runat="server" onserverclick="Rellenar_grid" type="button" class="btn btn-sm btn-primary" style="width: 100%; text-align: center; font-weight: bold"><i class="bi bi-arrow-down-up me-2"></i>Mano asignada</button>                                          
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMano" runat="server" Font-Size="X-Large" Text='<%#Eval("MANO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <HeaderTemplate>
                                            <label style="font-weight:bold">Ubicación</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblManoUbi" Font-Italic="true" runat="server" Font-Size="Large" Text='<%#Eval("MANUBICACION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab2" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                    <a href="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/PREVENTIVOMOLDES.png" class="mt-4" style="font: bold; font-size: large; color: black"><i class="bi bi-file-earmark-font ms-4"></i>Consultar tabla estándar preventivo</a>
                    <div class="table-responsive">
                        <asp:GridView ID="dgv_ListadoPreventivo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                            Width="98.5%" CssClass="table table-responsive shadow p-3 mt-3 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="GridViews_RowCommandReset"
                            EmptyDataText="No hay moldes para mostrar.">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#e6e6e6" />
                            <Columns>
                                <asp:TemplateField HeaderText="Molde">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Font-Size="X-Large" Text='<%#Eval("C_ID") %>' />
                                        <asp:Label ID="Decripción" runat="server" Text='<%#Eval("descripcion_tool") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo de mantenimiento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoMant" runat="server" Text='<%#Eval("C_LONG_DESCR") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Límite">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLimite" runat="server" Text='<%#Eval("C_THRESHOLDVALUE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contadores">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContador" runat="server" Text='<%#"<strong>Actual: </strong>" +Eval("C_RESOURCECOUNTER") %>' /><br />
                                        <asp:Label ID="lblContaTotal" runat="server" Text='<%#"<strong>Histórico: </strong>" +Eval("C_TOTALCOUNTER") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ultima actualización">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUltActu" runat="server" Text='<%#Eval("C_TIMELASTUPDATE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Porcentaje">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPorcentaje" runat="server" Text='<%#Eval("porc") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Button ID="button2" runat="server" type="button" CommandName="Reset" class="btn btn-primary btn-sm"  CommandArgument='<%#Eval("C_ID")+","+Eval("C_THRESHOLDVALUE")%>' Text="Resetear" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab3" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                    <div class="table-responsive">
                        <asp:GridView ID="dgv_ListadoHistorico" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                            Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                            EmptyDataText="No hay moldes para mostrar.">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#e6e6e6" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="button2" CssClass="btn btn-outline-dark" Font-Size="Large" runat="server" CommandName="Redirect" CommandArgument='<%#Eval("IdReparacionMolde")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parte">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParte" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("IdReparacionMolde") %>' /><br />
                                        <asp:Label ID="lblFecha" runat="server" Text='<%#"("+Eval("FechaFinalizacionReparacion")+")" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Molde">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMolde" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("IDMoldes") %>' /><br />
                                        <asp:Label ID="lblMoldeDescript" runat="server" Text='<%#Eval("Descripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Avería">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAveria" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reparación">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReparacion" runat="server" Text='<%#Eval("Reparacion ") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coste">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcoste" runat="server" Font-Size="Large" Text='<%#Eval("ImporteEmpresa1 ", "{0:0.##€}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-tab4" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab5" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab6" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                </div>
            </div>
              <!-- Button trigger modal Ubicacion -->
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
                                    <asp:label ID="UbicacionMolde" runat="server" class="input-group-text">
                                    </asp:label>
                                    <%-- 
                                        <input type="text" id="UbicacionMolde" runat="server" class="form-control" placeholder="Ubicación" aria-label="Username" aria-describedby="basic-addon1">
                                    --%>
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
                        
                    </div>
                </div>
            </div>
        </div>

        </div>
    </div>
          
</asp:Content>
































