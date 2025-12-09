<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PDCA.aspx.cs" Inherits="ThermoWeb.PDCA.PlandeAcciones" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Listado de planes de acción</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Listado de planes de acción
              
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Gridview desplegable nested-child--%>
    <script type="text/javascript">
        $(document).on("click", "[src*=plus]", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-dash-circle");
            $(this).attr("src", "dash");
        });
        $(document).on("click", "[src*=dash]", function () {
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-plus-circle");
            $(this).closest("tr").next().remove();
            $(this).attr("src", "plus");
        });
    </script>
    <%--Scripts de botones --%>
    <script type="text/javascript">
        function ShowPopup() {
            document.getElementById("AUXMODALACCION").click();
            //$("#AUXMODALACCION").click();
        }
        function ShowPopupPDCA() {
            document.getElementById("AUXMODALACCIONPDCA").click();
            //$("#AUXMODALACCION").click();
        }
        function linkaryvolver() {
            if (document.getElementById("DataList").value != "" && document.getElementById("DLPARTESELECT3").value != "") {
                document.getElementById("DATOSAPP").style.display = null;
                document.getElementById('ICONOAPP').src = 'ICONOS/ICONOMAQUINAS.png';
                document.getElementById('APPLABEL').innerText = "PARTE DE MÁQUINA VINCULADO";
                $("[id$=AUXNUMDLPARTESELECT]").val("2");
                $("#TABACCION").click();
            }
            //document.getElementById('AUXNUMDLPARTESELECT').value = "2";
            //alert('TEST DE FUNC');  
        }
        function linkaryvolver2() {
            if (document.getElementById("DataList2").value != "" && document.getElementById("DLPARTESELECT3").value != "") {
                document.getElementById("DATOSAPP").style.display = null;
                document.getElementById('ICONOAPP').src = 'ICONOS/ICONOMOLDES.png';
                document.getElementById('APPLABEL').innerText = "PARTE DE MOLDE VINCULADO";
                $("[id$=AUXNUMDLPARTESELECT]").val("3");
                $("#TABACCION").click();
            }
            //document.getElementById('AUXNUMDLPARTESELECT').value = "3";
            //alert('TEST DE FUNC2');

        }
        function linkaryvolver3() {
            if (document.getElementById("DataList3").value != "" && document.getElementById("DLPARTESELECT3").value != "") {
                document.getElementById("DATOSAPP").style.display = null;
                document.getElementById('ICONOAPP').src = 'ICONOS/ICONOCALIDAD.png';
                document.getElementById('APPLABEL').innerText = "ALERTA DE CALIDAD VINCULADA";
                $("[id$=AUXNUMDLPARTESELECT]").val("1");
                $("#TABACCION").click();
                //document.getElementById('AUXNUMDLPARTESELECT').value = "1";
                //alert('TEST DE FUNC3');
            }
        }
        function limpiarcajasNPA() {
            document.getElementById("NuevoDropTipo").value = "-";
            document.getElementById("NuevoDropPrioridad").value = "Baja";
            document.getElementById("NuevoDropPiloto").value = "-";
            document.getElementById("NuevoObjetivo").value = "";
            document.getElementById("DataList4").value = "";
            document.getElementById("DataList5").value = "";
            document.getElementById("NuevoGeneral").value = "";
        }
        function QuitarParte() {
            document.getElementById("DATOSAPP").style.display = "none";
            document.getElementById("DLPARTESELECTHIDDEN").value = "0";
            document.getElementById("AUXNUMDLPARTESELECT").value = "0";
            document.getElementById("DataList").value = "";
            document.getElementById("DataList2").value = "";
            document.getElementById("DataList3").value = "";

        }
    </script>
    <%--Selectores de parte --%>
    <script type="text/javascript">
        $(document).on('change', 'input', function () {
            var optionslist = $('datalist')[1].options;
            var value = $(this).val();
            for (var x = 0; x < optionslist.length; x++) {
                if (optionslist[x].value === value) {
                    const Valores = value.split("|");
                    document.getElementById("DLPARTESELECT").value = Valores[0];
                    document.getElementById("DLPARTESELECT2").value = Valores[1];
                    document.getElementById("DLPARTESELECT3").value = Valores[2];
                    document.getElementById("DataList2").value = "";
                    document.getElementById("DataList3").value = "";
                    if (document.getElementById("DLPARTESELECT3").value == "") {
                        document.getElementById("DLPARTESELECT").value = "";
                        document.getElementById("DLPARTESELECT2").value = "";
                    }
                    else {
                        $("[id$=DLPARTESELECTHIDDEN]").val(Valores[0]);
                    }

                    //$("#TABACCION").click();
                    break;
                }
            }
            var optionslist2 = $('datalist')[2].options;
            var value2 = $(this).val();
            for (var x = 0; x < optionslist2.length; x++) {
                if (optionslist2[x].value === value2) {
                    Valores = value2.split("|");
                    document.getElementById("DLPARTESELECT").value = Valores[0];
                    document.getElementById("DLPARTESELECT2").value = Valores[1];
                    document.getElementById("DLPARTESELECT3").value = Valores[2];
                    document.getElementById("DataList").value = "";
                    document.getElementById("DataList3").value = "";
                    if (document.getElementById("DLPARTESELECT3").value == "") {
                        document.getElementById("DLPARTESELECT").value = "";
                        document.getElementById("DLPARTESELECT2").value = "";
                    }
                    else {
                        $("[id$=DLPARTESELECTHIDDEN]").val(Valores[0]);
                    }
                    //$("#TABACCION").click();
                    break;
                }
            }

            var optionslist3 = $('datalist')[3].options;
            var value3 = $(this).val();
            for (var x = 0; x < optionslist3.length; x++) {
                if (optionslist3[x].value === value3) {
                    Valores = value3.split("|");
                    document.getElementById("DLPARTESELECT").value = Valores[0];
                    document.getElementById("DLPARTESELECT2").value = Valores[1];
                    document.getElementById("DLPARTESELECT3").value = Valores[2];
                    document.getElementById("DataList").value = "";
                    document.getElementById("DataList2").value = "";
                    if (document.getElementById("DLPARTESELECT3").value == "") {
                        document.getElementById("DLPARTESELECT").value = "";
                        document.getElementById("DLPARTESELECT2").value = "";
                    }
                    else {
                        $("[id$=DLPARTESELECTHIDDEN]").val(Valores[0]);
                    }
                    //$("#TABACCION").click();
                    break;
                }
            }
        });
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
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12">
                <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-1 me-md-3 mb-md-1">
                    <button id="AUXCIERRAMODAL" runat="server" type="button" class="btn-close" data-bs-target="#ModalEditaAccion" data-bs-toggle="modal" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                    <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>
                    <button id="AUXMODALACCIONPDCA" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#EditaPlanAccion" style="font-size: larger"></button>
                    <button id="btnoffcanvas" runat="server" type="button" class="btn btn-primary ms-md-0 bi bi-funnel" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger"></button>
                    <button type="button" class="btn btn-outline-dark ms-md-1  bi bi-file-earmark-plus" data-bs-toggle="modal" data-bs-target="#CreaPlanAccion" onclick="limpiarcajasNPA()" style="font-size: larger"></button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="container-fluid mt-md-1">
                    <div style="overflow-y: auto;">
                        <asp:GridView ID="dgvListadoPDCA" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded border-top-0" DataKeyNames="IdPDCA" OnRowDataBound="OnRowDataBound" OnRowEditing="GridView_RowEditing_PDCA" OnRowCancelingEdit="gridView_RowCancelingEdit_PDCA" OnRowCommand="GridViewCommandEventHandler" BorderColor="black" Width="100%">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <AlternatingRowStyle BackColor="#e6e6e6" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <button type="button" class="btn btn-outline-dark ms-md-2 bi bi-plus-circle" src="plus" style="font-size: 1em"></button>
                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" BorderColor="black" Width="100%" ShowFooter="true" OnRowDataBound="OnRowDataBoundAUX" OnRowCommand="GridViewCommandEventHandler">
                                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                                <RowStyle BackColor="White" />
                                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                                <FooterStyle BackColor="#e8e8e8" />
                                                <Columns>
                                                    <asp:BoundField DataField="Id" HeaderText="Nº" ItemStyle-BackColor="#ccccff" Visible="false" />
                                                    <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="VERAPP" CssClass="btn btn-outline-dark ms-md-1" Font-Size="Large" runat="server" CommandName="IrApp" Visible="false" CommandArgument='<%#Eval("APP")+"¬"+ Eval("APPIdVinculada")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nº" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNºRef" runat="server" Text='<%# "<strong>"+Eval("IdPDCA")+"."+ Eval("IdReferencial")+"</strong>" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Prioridad" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:Image ID="IMGAccionPrioridad" ImageUrl="~/SMARTH_fonts/INTERNOS/PRIORIDAD1.png" runat="server" Height="35" Width="35" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="left" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProdMold2" runat="server" Font-Bold="true" Text='<%#Eval("ProdDescrip") + " - " + Eval("ProdMold")%>' /><br />
                                                            <asp:Label ID="lblProdMoldDescr" runat="server" Width="100%" Text='<%#Eval("Descripcion") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <button id="EstadoRunning" runat="server" type="button" class="btn btn-success ms-md-1 " style="visibility: hidden; width: 100%; height: 100%"><i class="bi bi-play-fill"></i></button>
                                                            <button id="EstadoEnCola" runat="server" type="button" class="btn btn-warning ms-md-1 " style="visibility: hidden; width: 100%; height: 100%"><i class="bi bi-hourglass-split"></i></button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Problema" ItemStyle-HorizontalAlign="left" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesviacionlocal" runat="server" Text='<%# "<strong>"+Eval("Tipo")+" </strong><br/>"%>' />
                                                            <asp:TextBox ID="lblDesviacionlocal2" runat="server" TextMode="MultiLine" Enabled="false" BackColor="Transparent" BorderColor="Transparent" Style="overflow: hidden" Width="100%" Text='<%# Eval("DesviacionEncontrada") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Causa raíz" ItemStyle-HorizontalAlign="left" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblRazonlocal" runat="server" TextMode="MultiLine" Enabled="false" BackColor="Transparent" BorderColor="Transparent" Style="overflow: hidden" Width="100%" Text='<%#Eval("CausaRaiz") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="left" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblAccionlocal" runat="server" TextMode="MultiLine" Enabled="false" BackColor="Transparent" BorderColor="Transparent" Style="overflow: hidden" Width="100%" Text='<%#Eval("Accion") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderText="Estado" Visible="false" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Image ImageUrl='<%# "http://facts4-srv/thermogestion/imagenes/" + Eval("APPEstado") %>' runat="server" Height="35" Width="35" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Piloto" ItemStyle-HorizontalAlign="center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAPPPiloto" runat="server" Text='<%#Eval("APPPiloto") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cierre previsto" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCierrePrevisto" runat="server" Text='<%#Eval("FechaCierrePrev", "{0:dd/MM/yyyy}") %>' /><br />
                                                            <asp:Label ID="lblRevision" runat="server" Text='<%#Eval("FECHAREV", "{0:dd/MM/yyyy}") %>' />
                                                            <asp:Label ID="lblCierreReal" Visible="false" runat="server" Text='<%#Eval("FechaCierreReal", "{0:dd/MM/yyyy}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:Image ID="IMGAccionEstado" ImageUrl="~/SMARTH_fonts/INTERNOS/CirculoNA.png" runat="server" Height="35" Width="35" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Identificador" ItemStyle-HorizontalAlign="center" Visible="false" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIdAPP" runat="server" Text='<%#Eval("APPIdVinculada") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nº" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccionVencida" runat="server" Text='<%#Eval("VENCIDO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="EDITAACCION" CssClass="btn btn-primary me-md-1 " Font-Size="Large" runat="server" CommandName="EditAccion" CommandArgument='<%#Eval("Id")+"¬"+ Eval("Desviacion")%>'><i class="bi bi-pencil-fill"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BORRARACCION" CssClass="btn btn-danger me-md-1" Font-Size="Large" runat="server" OnClientClick="return confirm('Se eliminará esta acción de la lista. ¿Seguro que quieres continuar? ');" CommandName="BorrarAccion" CommandArgument='<%#Eval("Id")%>'><i class="bi bi-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="NuevaAccion" CssClass="btn btn-sm btn-outline-dark mb-md-1 mt-md-1 me-md-1 float-end" Font-Size="Large" runat="server" CommandName="NuevaAccion" CommandArgument='<%#Eval("Id")+"¬"+ Eval("Desviacion")%>'><i class="bi bi-plus"></i></asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnRedirectPDCADETALLE" CommandName="RedirectPDCADETALLE" CommandArgument='<%#Eval("IdPDCA") %>' CssClass="btn btn-secondary ms-md-0 " Style="font-size: 1rem">
                                            <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nº" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" ItemStyle-Font-Size="X-Large" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdPDCA" runat="server" Text='<%#Eval("IdPDCA") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipo" runat="server" Text='<%#Eval("TIPO") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderText="Acc. cerradas" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVencidas" Visible="false" runat="server" ForeColor="Red" Font-Size="X-Small" Text='<%#Eval("VENCIDO") %>' />
                                        <asp:Label ID="lblAcciones" runat="server" Text='<%#Eval("ACCIONES") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referencia / Proyecto" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModo" runat="server" Font-Bold="true" Text='<%#Eval("Modo") %>' /><br />
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("ReferenciaTEXT") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtReferencia" runat="server" Text='<%#Eval("Referencia") %>' /><br />
                                        <asp:Label ID="lblDescripcion2" runat="server" Text='-' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Objetivo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesviacion" runat="server" Text='<%#Eval("Desviacion") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDesviacion" TextMode="MultiLine" Width="100%" runat="server" Text='<%#Eval("Desviacion") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cliente" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderText="Prioridad" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Image ImageUrl='<%# "http://facts4-srv/thermogestion/imagenes/" + Eval("Prioridad") %>' runat="server" Height="25" Width="25" />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Piloto" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPiloto" runat="server" Text='<%#Eval("Nombre") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" HeaderText="Estado" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Image ImageUrl='<%# "http://facts4-srv/thermogestion/imagenes/" + Eval("Estado") %>' runat="server" Height="35" Width="35" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="left" HeaderText="Fechas" ItemStyle-Width="12%">
                                    <ItemTemplate>
                                        <label><strong>Apertura:</strong>&nbsp </label>
                                        <asp:Label ID="lblApertura" runat="server" Text='<%#Eval("Apertura", "{0:dd/MM/yyyy}") %>' /><br />
                                        <label><strong>Cierre</strong>:&nbsp </label>
                                        <asp:Label ID="lblCierre" runat="server" Text='<%#Eval("Cierre", "{0:dd/MM/yyyy}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Font-Bold="true" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnEdit" CommandName="EditaPDCA" CommandArgument='<%#Eval("IdPDCA") %>' CssClass="btn btn-secondary me-md-3 float-end" Style="font-size: 1rem">
                                            <i class="bi bi-pencil-fill" aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <%--MODALES DE EDICION --%>
        <div class="modal fade" id="ModalEditaAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background: #e6e6e6">
                        <asp:Label ID="HeaderTipoGuardado" Enabled="false" runat="server" Font-Size="Large" Font-Bold="true" Visible="false">---</asp:Label>
                        <asp:Label ID="HeaderIdPDCA" Enabled="false" runat="server" Font-Size="Large" Font-Bold="true" Visible="false"></asp:Label>
                        <asp:Label ID="HeaderIdPDCAXListaacciones" Enabled="false" runat="server" Font-Size="Large" Font-Bold="true" Visible="false">0</asp:Label>
                        <h5 class="modal-title" id="staticBackdropLabel" runat="server">Nueva acción / Editar acción</h5>

                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body" runat="server">
                        <div class="row">
                            <div class="d-flex align-items-start">
                                <div class="nav flex-column nav-pills me-3" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                                    <button id="TABACCION" class="nav-link active" data-bs-toggle="pill" data-bs-target="#v-pills-home" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true"><i class="bi bi-kanban"></i></button>
                                    <button id="TABAPP" class="nav-link" data-bs-toggle="pill" data-bs-target="#v-pills-profile" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false"><i class="bi bi-link-45deg"></i></button>
                                    <button class="nav-link" id="v-pills-messages-tab" data-bs-toggle="pill" data-bs-target="#v-pills-messages" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false"><i class="bi bi-image"></i></button>
                                    <button class="nav-link" id="v-pills-settings-tab" data-bs-toggle="pill" data-bs-target="#v-pills-settings" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="visibility: hidden">D</button>
                                </div>
                                <div class="tab-content" id="v-pills-tabContent">
                                    <div class="tab-pane fade show active" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <h6>Máquina:</h6>
                                                <asp:DropDownList ID="DropMaquina" runat="server" CssClass="form-select border border-dark shadow">
                                                    <asp:ListItem Text="-" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-9">
                                                <h6>Producto:</h6>
                                                <div class="input-group mb-3">
                                                    <input class="form-control  border border-dark shadow" list="DLProdMoldes" id="SelectProdMoldes" runat="server" placeholder="Escribe aquí para buscar...">
                                                    <datalist id="DLProdMoldes" runat="server">
                                                    </datalist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-lg-3">
                                                <h6>Tipo:</h6>
                                                <asp:DropDownList ID="DropTipoaccion" runat="server" CssClass="form-select  border border-dark shadow">
                                                    <asp:ListItem Text="-" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Contención" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No detección" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Ocurrencia" Value="3"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-lg-9">
                                                <h6>Desviación:</h6>
                                                <input type="text" id="DescripcionProblema" class="form-control  border border-dark shadow" placeholder="Descripción del problema" autocomplete="off" runat="server">
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <h6>Fecha de apertura:</h6>
                                                <input type="text" id="InputFechaApertura" class="form-control border border-dark shadow" runat="server" disabled="disabled">
                                            </div>
                                            <div class="col-lg-4">
                                                <h6>Fecha objetivo:</h6>
                                                <input type="text" id="InputFechaLimiteOriginal" class="form-control border border-dark shadow" autocomplete="off" disabled="disabled" runat="server">
                                                <input type="text" id="InputFechaLimite" class="form-control border border-dark shadow Add-text" autocomplete="off" runat="server">
                                            </div>
                                            <div class="col-lg-4">
                                                <h6>Prioridad:</h6>
                                                <asp:DropDownList ID="DropAccionPrioridad" runat="server" CssClass="form-select  border border-dark shadow">
                                                    <asp:ListItem Value="0">Baja</asp:ListItem>
                                                    <asp:ListItem Value="1">Media</asp:ListItem>
                                                    <asp:ListItem Value="2">Alta</asp:ListItem>
                                                    <asp:ListItem Value="3">Máxima</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row mb-2">
                                            <div class="col-lg-4">
                                                <h6>Estado contención:</h6>
                                                <asp:DropDownList ID="DropDownContencion" runat="server" CssClass="form-select border border-dark shadow">
                                                    <asp:ListItem Text="N/A" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Pendiente" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Completada" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-4">
                                                <h6>Origen:</h6>
                                                <asp:DropDownList ID="DropDownISHIKAWA" runat="server" CssClass="form-select border border-dark shadow">
                                                    <asp:ListItem Text="Sin definir" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Entorno" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Máquina / Molde / Periférico" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Mediciones" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Material" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Método / Documentación" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Persona" Value="6"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-lg-4"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h6>Causa Raiz:</h6>
                                                <textarea id="InputCausaRaiz" runat="server" class="form-control border border-dark shadow" placeholder="Descripción de la causa raiz." rows="2"></textarea>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h6>Acción propuesta:</h6>
                                                <textarea id="InputAccion" runat="server" class="form-control border border-dark shadow" placeholder="Accion a realizar." rows="2"></textarea>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h6>Revisión:</h6>
                                                <textarea id="InputRevision" runat="server" class="form-control border border-dark shadow" placeholder="Realizar revisión." rows="1"></textarea>
                                                <br />
                                                <h6>Histórico revisiones:</h6>
                                                <asp:TextBox ID="lblUltimaRev" runat="server" Width="100%" CssClass="form-control  border border-dark shadow" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                <div class="accordion" id="accordionExample">
                                                    <div class="accordion-item">
                                                        <h1 class="accordion-header" id="headingOne">
                                                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne" style="height: 10px">
                                                                Ver más
                                                            </button>
                                                        </h1>
                                                        <div id="collapseOne" class="accordion-collapse collapse" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                                            <div class="accordion-body">
                                                                <div style="overflow-y: auto;">
                                                                    <asp:GridView ID="DgvListaRevs" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow-lg p-3 mb-5 rounded" OnRowCommand="EliminarRevision" BorderColor="black" Width="100%">
                                                                        <HeaderStyle CssClass="card-header" HorizontalAlign="Center" />
                                                                        <AlternatingRowStyle BackColor="#e6e6e6" />
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIDRevision" runat="server" Text='<%#Eval("Id") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFechaREV" runat="server" Text='<%#Eval("FechaApertura", "{0:dd/M/yyyy}") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Revisión" ItemStyle-HorizontalAlign="left" ItemStyle-Width="75%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFechaREVTXT" runat="server" Text='<%#Eval("Revision") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="BORRARREVISION" CssClass="btn btn-sm btn-danger" Font-Size="Large" runat="server" CommandName="BorrarAccion" CommandArgument='<%#Eval("Id")%>'><i class="bi bi-trash"></i></asp:LinkButton>

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
                                        <br />
                                        <div class="row" id="DATOSAPP" runat="server" style="display: none">
                                            <div style="background-color: #BAD6EC; border: 1px solid gray">
                                                <div class="row">
                                                    <div class="col-lg-11">
                                                        <a id="linkICONOAPP" runat="server" href="#" class="ms-md-3">
                                                            <img id="ICONOAPP" runat="server" src="http://facts4-srv/thermogestion/imagenes/NULL.png" style="width: 40px" /></a>
                                                        <asp:Label runat="server" ID="APPLABEL" Font-Size="X-Large" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-1" style="align-content: end">

                                                        <button type="button" onclick="QuitarParte()" class="btn"><i class="bi bi-x-lg"></i></button>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-3 ms-md-4">

                                                        <h6>Identificador:</h6>
                                                        <asp:TextBox ID="DLPARTESELECT" runat="server" Width="100%" Enabled="false" Text="" />
                                                        <asp:HiddenField ID="DLPARTESELECTHIDDEN" runat="server" Value="0" />
                                                        <%-- <asp:TextBox ID="AUXNUMDLPARTESELECT" runat="server" Width="100%" Enabled="false" Text="0" />--%>
                                                        <asp:HiddenField ID="AUXNUMDLPARTESELECT" runat="server" Value="0" />
                                                    </div>
                                                    <div class="col-lg-8">

                                                        <h6>Producto:</h6>
                                                        <asp:TextBox ID="DLPARTESELECT2" runat="server" Width="100%" Enabled="false" Text="" />
                                                    </div>
                                                    <div class="col-lg-11 ms-md-4 ">
                                                        <h6>Acción:</h6>
                                                        <textarea id="DLPARTESELECT3" textmode="MultiLine" runat="server" width="100%" text="" class="form-control" rows="2" disabled="disabled"></textarea>
                                                        <br />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-lg-3">
                                                <h6>Piloto:</h6>
                                                <asp:DropDownList ID="PilotoModal" runat="server" CssClass="form-select  border border-dark shadow">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3">
                                                <h6>Estado:</h6>
                                                <asp:DropDownList ID="DropAccionEstado" runat="server" CssClass="form-select  border border-dark shadow">
                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                    <asp:ListItem Value="1">25%</asp:ListItem>
                                                    <asp:ListItem Value="2">50%</asp:ListItem>
                                                    <asp:ListItem Value="3">75%</asp:ListItem>
                                                    <asp:ListItem Value="4">100%</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3">
                                                <h6>Fecha de cierre:</h6>
                                                <input type="text" id="InputFechaCierre" class="form-control border border-dark shadow Add-text" autocomplete="off" runat="server">
                                            </div>
                                            <div class="col-lg-3">
                                                <h6>Lección aprendida:</h6>
                                                <asp:DropDownList ID="DropLeccionAprendida" runat="server" CssClass="form-select  border border-dark shadow">
                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                    <asp:ListItem Value="1">Sí</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="tab-pane fade" id="v-pills-profile" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                                        <div class="row">
                                            <h6>Formatos de proceso:</h6>
                                            <div class="col-lg-12">
                                                 <a href="../FPOCS/FPOC-09-11 v2 S Hoja de Contención.xlsx" class="mt-4" style="font: bold; font-size: large; color: black"><i class="bi bi-file-earmark-font ms-4 me-2"></i><strong><i>FPOC-09-11: Hoja de contención</i></strong></a><br />
                                                 <a href="../FPOCS/FPOC-12-01 FIT 18.01 Informe 8D-A3 V2.xlsm" class="mt-4" style="font: bold; font-size: large; color: black"><i class="bi bi-file-earmark-font me-2 ms-4"></i><strong><i>FPOC-12-01: Formato 8D/A3</i></strong></a><br />
                                                 <a href="../FPOCS/FPOC-12-04 Solicitud de Derogación v2.docx" class="mt-4" style="font: bold; font-size: large; color: black"><i class="bi bi-file-earmark-font me-2 ms-4"></i><strong><i>FPOC-12-04: Solicitud de derogación</i></strong></a><br />
                                            </div>
                                            <div class="col-lg-12">
                                                <h6>Partes de máquina:</h6>
                                                <div class="input-group mb-3">
                                                    <button id="PDCACreaParteMaquina" runat="server" type="button" onserverclick="RedireccionaAPP" class="btn btn-outline-dark"><i class="bi bi-file-earmark-plus"></i></button>
                                                    <input class="form-control" list="DLSelectMaquina" id="DataList" runat="server" placeholder="Escribe aquí para buscar...">
                                                    <datalist id="DLSelectMaquina" runat="server">
                                                    </datalist>
                                                    <button type="button" class="btn btn-primary" onclick="linkaryvolver()"><i class="bi bi-link-45deg"></i></button>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <h6>Partes de molde:</h6>
                                                <div class="input-group mb-3">
                                                    <button type="button" id="PDCACreaParteMolde" runat="server" onserverclick="RedireccionaAPP" class="btn btn-outline-dark"><i class="bi bi-file-earmark-plus"></i></button>
                                                    <input class="form-control" list="DLSelectMolde" id="DataList2" runat="server" placeholder="Escribe aquí para buscar...">
                                                    <datalist id="DLSelectMolde" runat="server">
                                                    </datalist>
                                                    <button type="button" class="btn btn-primary" onclick="linkaryvolver2()"><i class="bi bi-link-45deg"></i></button>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <h6>Alertas de calidad:</h6>
                                                <div class="input-group mb-3">
                                                    <button type="button" id="PDCACreaNoConformidad" runat="server" onserverclick="RedireccionaAPP" class="btn btn-outline-dark"><i class="bi bi-file-earmark-plus"></i></button>
                                                    <input class="form-control" list="DLSelectAlerta" id="DataList3" runat="server" placeholder="Escribe aquí para buscar...">
                                                    <datalist id="DLSelectAlerta" runat="server">
                                                    </datalist>
                                                    <button type="button" class="btn btn-primary" onclick="linkaryvolver3()"><i class="bi bi-link-45deg"></i></button>
                                                </div>
                                            </div>
                                            <div class="col-lg-12"></div>
                                            <div class="col-lg-12"></div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="v-pills-messages" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h6>Imágenes de APPs:</h6>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <img src="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" class="img-fluid rounded img-thumbnail float-start" alt="...">
                                            </div>
                                            <div class="col-lg-6">
                                                <img src="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" class="img-fluid rounded img-thumbnail float-end" alt="...">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h6>Documentos vinculados:</h6>
                                            </div>
                                        </div>
                                        <%-- <div class="row">
                                            <div class="col-lg-3">
                                                <div class="form-check">
                                                    <input class="form-check-input" runat="server" type="radio" name="flexRadioDefault" id="flexRadioDefault1" checked>
                                                    <label class="form-check-label" for="flexRadioDefault1">
                                                        Evidencia 1
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-check">
                                                    <input class="form-check-input" runat="server" type="radio" name="flexRadioDefault" id="flexRadioDefault2">
                                                    <label class="form-check-label" for="flexRadioDefault2">
                                                        Evidencia 2
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-check">
                                                    <input class="form-check-input" runat="server" type="radio" name="flexRadioDefault" id="flexRadioDefault3" >
                                                    <label class="form-check-label" for="flexRadioDefault3">
                                                        Evidencia 3
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-check">
                                                    <input class="form-check-input" runat="server" type="radio" name="flexRadioDefault" id="flexRadioDefault4" >
                                                    <label class="form-check-label" for="flexRadioDefault4">
                                                        Evidencia 4
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        --%>
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <a id="LINKevidencia1" runat="server" href="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg">
                                                    <img id="IMGevidencia1" runat="server" src="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" class="img-fluid rounded img-thumbnail" alt="...">
                                                </a>
                                                <button id="BTNBorrarEvidencia1" runat="server" onserverclick="BorrarDocumento" type="button" class="btn btn-sm btn-danger" style="float: right"><i class="bi bi-trash"></i></button>
                                            </div>
                                            <div class="col-lg-3">
                                                <a id="LINKevidencia2" runat="server" href="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg">
                                                    <img id="IMGevidencia2" runat="server" src="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" class="img-fluid rounded img-thumbnail" alt="...">
                                                </a>
                                                <button id="BTNBorrarEvidencia2" runat="server" onserverclick="BorrarDocumento" type="button" class="btn btn-sm btn-danger" style="float: right"><i class="bi bi-trash"></i></button>
                                            </div>
                                            <div class="col-lg-3">
                                                <a id="LINKevidencia3" runat="server" href="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg">
                                                    <img id="IMGevidencia3" runat="server" src="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" class="img-fluid rounded img-thumbnail" alt="...">
                                                </a>
                                                <button id="BTNBorrarEvidencia3" runat="server" onserverclick="BorrarDocumento" type="button" class="btn btn-sm btn-danger" style="float: right"><i class="bi bi-trash"></i></button>
                                            </div>
                                            <div class="col-lg-3">
                                                <a id="LINKevidencia4" runat="server" href="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg">
                                                    <img id="IMGevidencia4" runat="server" src="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" class="img-fluid rounded img-thumbnail" alt="...">
                                                </a>
                                                <button id="BTNBorrarEvidencia4" runat="server" onserverclick="BorrarDocumento" type="button" class="btn btn-sm btn-danger" style="float: right"><i class="bi bi-trash"></i></button>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="input-group">
                                                <button type="button" class="btn btn-outline-dark" runat="server" onserverclick="Insertar_documento"><i class="bi bi-upload"></i></button>
                                                <asp:FileUpload ID="FileUpload1" class="form-control" runat="server"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <%--
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h6>Subir documentos:</h6>
                                                <div class="form-group">
                                                    <div class="file-loading">
                                                        <input id="DocInput" name="DocInput" type="file" class="file" data-upload-url="http://localhost:14653/FileUploadHandler.ashx" data-browse-on-zone-click="true">
                                                    </div>
                                                </div>
                                                <hr>
                                                <script type="text/javascript">
                                                    $('#DocInput').fileinput({
                                                        language: 'es',
                                                        uploadAsync: true,
                                                        uploadUrl: 'http://localhost:14653/FileUploadHandler.ashx',
                                                    });


                                                </script>
                                            </div>

                                        </div>--%>
                                    </div>
                                    <div class="tab-pane fade" id="v-pills-settings" role="tabpanel" aria-labelledby="v-pills-settings-tab">...</div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer" style="background: #e6e6e6">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                        <asp:LinkButton ID="BtnGuardarAccion" runat="server" OnClick="GuardarAccion" class="btn btn-success"><i class="bi bi-sd-card"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="CreaPlanAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background: #e6e6e6">
                        <h5 class="modal-title" id="H1" runat="server">Crear nuevo Plan de Acción.</h5>

                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body" runat="server">
                        <div class="row">
                            <div class="col-lg-3">
                                <label style="font-weight: 700">Tipo de plan de acción:</label>
                                <asp:DropDownList ID="NuevoDropTipo" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True" Value="0"> - </asp:ListItem>
                                    <asp:ListItem Value="1"> ACTA </asp:ListItem>
                                    <asp:ListItem Value="2"> DE CLIENTE </asp:ListItem>
                                    <asp:ListItem Value="3"> INTERNO </asp:ListItem>
                                    <asp:ListItem Value="4"> PROYECTO </asp:ListItem>
                                    <asp:ListItem Value="5"> NO CONFORMIDAD </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                <label style="font-weight: 700">Prioridad:</label>
                                <asp:DropDownList ID="NuevoDropPrioridad" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True" Value="0"> Baja </asp:ListItem>
                                    <asp:ListItem Value="1"> Media </asp:ListItem>
                                    <asp:ListItem Value="3"> Alta </asp:ListItem>
                                    <asp:ListItem Value="4"> Máxima </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-6">
                                <label style="font-weight: 700">Piloto:</label>
                                <asp:DropDownList ID="NuevoDropPiloto" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12">
                                <label style="font-weight: 700">Objetivo:</label>
                                <asp:TextBox ID="NuevoObjetivo" runat="server" Rows="2" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <label style="font-weight: 700">Añadir desde referencia:</label>
                        <input class="form-control" list="NuevaReferencia" id="DataList4" runat="server" placeholder="Escribe aquí para buscar...">
                        <datalist id="NuevaReferencia" runat="server">
                        </datalist>
                        <br />
                        <label style="font-weight: 700">Añadir desde molde:</label>
                        <input class="form-control" list="NuevoMolde" id="DataList5" runat="server" placeholder="Escribe aquí para buscar...">
                        <datalist id="NuevoMolde" runat="server">
                        </datalist>
                        <br />
                        <label style="font-weight: 700">Añadir general:</label>
                        <input class="form-control" id="NuevoGeneral" runat="server">
                    </div>
                    <div class="modal-footer" style="background: #e6e6e6">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="CreaNuevoPlanAccion" class="btn btn-success"><i class="bi bi-sd-card"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="EditaPlanAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background: #e6e6e6">
                        <h5 class="modal-title" id="H2" runat="server">Editar Plan de Acción</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body" runat="server">
                        <div class="row">
                            <div class="col-lg-3">
                                <label style="font-weight: 700">Plan de acción:</label><br />
                                <asp:Label ID="PDCAEditNUMAccion" runat="server">0</asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <label style="font-weight: 700">Estado:</label><br />
                                <asp:DropDownList ID="PDCAEditEstado" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True" Value="0"> 0% </asp:ListItem>
                                    <asp:ListItem Value="1"> 25% </asp:ListItem>
                                    <asp:ListItem Value="2"> 50% </asp:ListItem>
                                    <asp:ListItem Value="3"> 75% </asp:ListItem>
                                    <asp:ListItem Value="4"> 100% </asp:ListItem>
                                    <asp:ListItem Value="5" Enabled="false"> Especial </asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12">
                                <label style="font-weight: 700">Objetivo:</label>
                                <asp:TextBox ID="PDCAEditObjetivo" CssClass="form-control" runat="server" Rows="2" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3">
                                <label style="font-weight: 700">Cliente:</label><br />
                                <asp:Label ID="PDCAEditCliente" runat="server"></asp:Label>
                                
                            </div>

                            <div class="col-lg-3">
                                <label style="font-weight: 700">Referencia:</label><br />
                                <asp:Label ID="PDCAEditNumReferencia" runat="server"></asp:Label>
                                
                            </div>
                            <div class="col-lg-3">
                                <label style="font-weight: 700">Molde:</label><br />
                                <asp:Label ID="PDCAEditNumMolde" runat="server"></asp:Label>
                                <asp:HiddenField ID="PDCAEditModo" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-9">
                                <asp:Label ID="PDCAEditProdDescripcion" runat="server"></asp:Label>                               
                            </div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-3">
                                <label style="font-weight: 700">Tipo de plan de acción:</label>
                                <asp:DropDownList ID="PDCAEditTipo" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True" Value="0"> - </asp:ListItem>
                                    <asp:ListItem Value="1"> ACTA </asp:ListItem>
                                    <asp:ListItem Value="2"> DE CLIENTE </asp:ListItem>
                                    <asp:ListItem Value="3"> INTERNO </asp:ListItem>
                                    <asp:ListItem Value="4"> PROYECTO </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                <label style="font-weight: 700">Prioridad:</label>
                                <asp:DropDownList ID="PDCAEditPrioridad" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True" Value="1"> Baja </asp:ListItem>
                                    <asp:ListItem Value="2"> Media </asp:ListItem>
                                    <asp:ListItem Value="3"> Alta </asp:ListItem>
                                    <asp:ListItem Value="4"> Máxima </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-6">
                                <label style="font-weight: 700">Piloto:</label>
                                <asp:DropDownList ID="PDCAEditPiloto" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <label style="font-weight: 700">Editar desde referencia:</label>
                        <input class="form-control" list="NuevaReferencia" id="PDCAEditReferencia" runat="server" placeholder="Escribe aquí para buscar...">
                        <datalist id="Datalist1" runat="server">
                        </datalist>
                        <br />
                        <label style="font-weight: 700">Editar desde molde:</label>
                        <input class="form-control" list="NuevoMolde" id="PDCAEditMolde" runat="server" placeholder="Escribe aquí para buscar...">
                        <datalist id="Datalist6" runat="server">
                        </datalist>
                        <br />
                        <label style="font-weight: 700">Editar desde general:</label>
                        <input class="form-control" id="PDCAEditGeneral" runat="server">
                    </div>
                    <div class="modal-footer" style="background: #e6e6e6">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="ActualizaPDCAPlanAccion" class="btn btn-success"><i class="bi bi-sd-card"></i></asp:LinkButton>
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
                    <input class="form-check-input" type="checkbox" runat="server" id="SwitchActivas" checked="checked">
                    <label class="form-check-label" for="SwitchActivas">Mostrar sólo activas</label>
                </div>
                <div class="form-check form-switch">
                    <input class="form-check-input" type="checkbox" runat="server" id="SwitchVencidas">
                    <label class="form-check-label" for="SwitchActivas">Mostrar sólo vencidas</label>
                </div>
                <br />
                <h6>Producto/Proceso:</h6>
                <div class="input-group mb-3">
                    <input class="form-control" list="FiltroMoldes" id="InputFiltroMoldes" runat="server" placeholder="Escribe un molde...">
                    <datalist id="FiltroMoldes" runat="server">
                    </datalist>
                </div>
                <div class="input-group mb-3">
                    <input class="form-control" list="FiltroProductos" id="InputFiltroProductos" runat="server" placeholder="Escribe un producto...">
                    <datalist id="FiltroProductos" runat="server">
                    </datalist>
                </div>
                <div class="input-group mb-3">
                    <input class="form-control" list="FiltroCliente" id="InputFiltroCliente" runat="server" placeholder="Escribe un cliente...">
                    <datalist id="FiltroCliente" runat="server">
                    </datalist>
                </div>
                <div class="input-group mb-3">
                    <input class="form-control" list="FiltroMaquina" id="InputFiltroMaquina" runat="server" placeholder="Escribe una máquina...">
                    <datalist id="FiltroMaquina" runat="server">
                    </datalist>
                </div>
                <br />
                <h6>Piloto:</h6>
                <select class="form-select" runat="server" id="FiltroPiloto">
                </select>  
                <br />
                <h6>Asignado:</h6>
                <select class="form-select" runat="server" id="FiltroAsignado">
                </select>  
                <br />
                <h6>Ordenar por:</h6>
                <div class="input-group">
                    <select class="form-select" runat="server" id="selecorderby">
                        <option selected value="0">Por defecto</option>
                        <option value="1">Por número de ID</option>
                        <option value="2">Fecha de creación</option>
                        <option value="3">Fecha de vencimiento</option>
                        <option value="4">Prioridad</option>
                        <option value="5">Próxima producción</option>
                    </select>
                    <button class="btn btn-outline-dark me-md-3" type="button" runat="server" onserverclick="RellenarGrids">Filtrar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
