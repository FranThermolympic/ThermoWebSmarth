<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EtiquetasCalidad.aspx.cs" Inherits="ThermoWeb.CALIDAD.EtiquetasCalidad" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Imprimir estado de control</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Etiquetas de estado de control             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../LIBERACIONES/EstadoLiberacion.aspx">Liberaciones de serie</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Instrucciones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../SMARTH_docs/POCS/POC.11 Estado Control Ed. 11.pdf">POC.11 Estado Control Ed. 3</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript"><%--Configurar onloads--%>
    window.onload = function () {
        document.getElementById("BTNIMPRIMIR").onclick = function fun() {
            //alert("ENTRA");
            if (document.getElementById("SelecImpresora").value == 0)
            {
                alert("Debes seleccionar una impresora.")
            }
            else
            {
                ImprimeEtiquetasNEW();
            }
            
            //validation code to see State field is mandatory.  
        }
    }
    </script>
    <%--Scripts de botones --%>
    <style>
        th {
            background: #0000ff !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
        }

        th, td {
            padding: 0.25rem;
        }
    </style>
    <script type="text/javascript">

        function ShowPopupImprimirEtiquetas() {
            document.getElementById("AUXMODALIMPRIMIRETIQUETA").click();
        }
        function ActualizarGRID() {
            document.getElementById("BTN_RELLENARGRID").click();
        }

        function ImprimeEtiquetasNEW() {
            document.getElementById("ICONPRINTER").setAttribute("class", "spinner-border");
            $.ajax({
                type: "POST",
                url: "EtiquetasCalidad.aspx/ImprimirEtiquetasV2",
                data: "{ETIQUETA: '" + document.getElementById("IdTipoEtiqueta").value + "', TAREA: '" + document.getElementById("LblTipoRevision").innerText + "', REFERENCIA: '" + document.getElementById("NUMProducto").value + "', LOTE: '" + document.getElementById("InputAlbaran").value + "', OBSERVACIONES: '" + document.getElementById("InputObservaciones").value + "', CANTIDADORI: '" + document.getElementById("InputCantidad").value + "', CANTIDADREAL: '" + document.getElementById("InputCantidadCaja").value + "', OPERARIO: '" + document.getElementById("InputOperario").value + "', NUMETIQUETAS: " + document.getElementById("NumEtiquetas").value + ", IMPRESORA: " + document.getElementById("SelecImpresora").value+"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert("Vamos");
                    document.getElementById("CIERRAPOP").click();
                    document.getElementById("ICONPRINTER").setAttribute("class", "bi bi-printer-fill");
                    myFunctionPB();
                },
                failure: function (response) {
                    //alert("NoVamos");
                    document.getElementById("CIERRAPOP").click();
                    document.getElementById("ICONPRINTER").setAttribute("class", "bi bi-printer-fill");
                    myFunctionPB();
                }
            });
        }

        function ClosePopup() {
            document.getElementById("CIERRAPOP").click();
        }

        function LogErrores() {
            alert("Debes seleccionar la tarea que aplica a la etiqueta.");
        }
        
        function myFunctionPB() {
                __doPostBack();
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
        <div class="row">

            <div class="col-lg-9">
                <button id="AUXMODALIMPRIMIRETIQUETA" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalImprimirEtiqueta" style="font-size: larger"></button>

            </div>
            <div class="col-lg-3" style="text-align: right">
            </div>

        </div>
        <div class="container-fluid mt-2">
            <div class="col-lg-12">

                <div class="tab-content" id="pills-tabContent">
                    <div class="tab-pane fade show active" id="pill_secados" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <div class="row">
                            <div class="col-lg-3">
                                <h5>Imprimir etiquetas</h5>
                                <div class="input-group">
                                    <span class="input-group-text border border-dark">Tarea</span>
                                    <asp:DropDownList CssClass="form-select border border-dark" ID="DropDownPENDINSP" runat="server">
                                        <asp:ListItem>---</asp:ListItem>
                                        <asp:ListItem>CONTROL RECEPCION</asp:ListItem>
                                        <asp:ListItem>MUESTREO</asp:ListItem>
                                        <asp:ListItem>OPERARIO NUEVO</asp:ListItem>
                                        <asp:ListItem>PDTE. RETRABAJO</asp:ListItem>
                                        <asp:ListItem>PDTE. LIBERACION</asp:ListItem>
                                        <asp:ListItem>REVISION 100%</asp:ListItem>
                                    </asp:DropDownList>
                                    <button runat="server" id="BTNImprPENDINSP" onserverclick="SelectEtiqueta" type="button" class="btn btn-outline-dark" style="font-size: larger"><i class="bi bi-printer-fill"></i></button>
                                </div>
                                <asp:Image ID="IMGEtibloqueo" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="..\SMARTH_fonts\INTERNOS\FPOC11_01.png" runat="server" class="rounded img-thumbnail img-fluid border border-dark shadow" />
                            </div>
                            <div class="col-lg-3">
                                <h5>&nbsp</h5>
                                <div class="input-group">
                                    <span class="input-group-text border border-dark" id="inputGroup-sizing-sm">Tipo</span>
                                    <asp:DropDownList CssClass="form-select border border-dark" ID="DropDownPENDPROC" runat="server">
                                        <asp:ListItem>---</asp:ListItem>
                                        <asp:ListItem>CAJA DE PICO</asp:ListItem>
                                        <asp:ListItem>FALTA ETIQUETA</asp:ListItem>
                                        <asp:ListItem>PEND. TRASVASAR</asp:ListItem>
                                    </asp:DropDownList>
                                    <button runat="server" id="BTNImprPENDPROC" onserverclick="SelectEtiqueta" type="button" class="btn btn-outline-dark"  style="font-size: larger"><i class="bi bi-printer-fill"></i></button>
                                </div>
                                <asp:Image ID="Image1" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="..\SMARTH_fonts\INTERNOS\FPOC11_10.png" runat="server" class="rounded img-thumbnail img-fluid  border border-dark shadow" />
                            </div>
                            <div class="col-lg-1"></div>
                            <div class="col-lg-5">
                                <h5>Histórico de etiquetas</h5>
                                <div class="tab-pane fade show active" id="pills_historico" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                                    <asp:GridView ID="dgv_Historico_Etiquetas" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler" EmptyDataText="No hay etiquetas para mostrar.">
                                        <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                        <RowStyle BackColor="White" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="button2" CommandName="CargaDetalle" CommandArgument='<%#Eval("Id")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Etiqueta" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLote" Font-Size="Large" Font-Bold="true" runat="server" Text='<%#Eval("Tarea") %>' /><br />
                                                    <asp:Label ID="lblFecharevision" runat="server" Font-Italic="true" Text='<%#"(" + Eval("FechaImpresion", "{0:dd/MM/yyyy}") + ")" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="35%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReferencia" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("Referencia") %>' /><br />
                                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Motivo" ItemStyle-Width="55%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBuenas" runat="server"  Text='<%#Eval("Observaciones") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                        </div>
                    </div>
                    <div class="tab-pane fade" id="pills_listamateriales" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                    </div>
                    <div class="tab-pane fade" id="pills_liberaciones" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                    </div>
                </div>
                <div class="modal fade" id="ModalImprimirEtiqueta" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header bg-primary shadow">
                                <h5 class="modal-title text-white" id="staticBackdropLabel" runat="server"><i class="bi bi-printer">&nbsp Imprimir etiquetas</i></h5>
                                <button id="CIERRAPOP" type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body" runat="server">
                                <div class="row">
                                    <asp:Label runat="server" ID="LblTipoRevision" Font-Size="X-Large" Font-Italic="true" Font-Bold="true" CssClass="mb-2">---</asp:Label>
                                    <asp:HiddenField ID="IdTipoEtiqueta" runat="server" />
                                    <h6 class="mt-2">Producto:</h6>
                                    <div class="me-4 ms-2">
                                        <input class="form-control border border-dark shadow" list="DatalistProducto" id="NUMProducto" runat="server" placeholder="Escribe un producto...">
                                        <datalist id="DatalistProducto" runat="server">
                                        </datalist>
                                    </div>
                                    <h6 class="mt-2">Albarán / Lote de fabricación:</h6>
                                    <div class="me-4 ms-2">
                                        <asp:TextBox ID="InputAlbaran" runat="server" Font-Size="Large" Width="100%" CssClass="form-control border border-dark shadow "></asp:TextBox>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h6 class="mt-2" id="lblCantRet" runat="server">Cantidad retenida:</h6>
                                            <div class="ms-2">
                                                <asp:TextBox ID="InputCantidad" TextMode="Number" runat="server" Font-Size="Large" CssClass="form-control border border-dark shadow "></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 invisible" runat="server" id="AUXColCantCaja">
                                            <h6 class="mt-2">Cantidad en caja:</h6>
                                            <div class="ms-2">
                                                <asp:TextBox ID="InputCantidadCaja" TextMode="Number" runat="server" Font-Size="Large" CssClass="form-control border border-dark shadow"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <h6 class="mt-2">Observaciones:</h6>
                                    <div class="me-4 ms-2">
                                        <asp:TextBox ID="InputObservaciones" TextMode="MultiLine" Rows="3" runat="server" Font-Size="Large" CssClass="form-control border border-dark shadow "></asp:TextBox>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h6 class="mt-2">Operario:</h6>
                                            <div class="ms-2">
                                                <asp:TextBox ID="InputOperario" TextMode="Number" runat="server" Font-Size="Large" Width="65%" CssClass="form-control border border-dark shadow "></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-1"></div>
                                        <div class="col-lg-5">
                                            <h6 class="mt-2">Etiquetas (Max.10):</h6>
                                            <div class="ms-2">
                                                <input type="number" min="1" max="10" style="width: 60%; font-size: large" onkeydown="return false" step="1" id="NumEtiquetas" runat="server" class="form-control border border-dark shadow" value="1" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:DropDownList ID="SelecImpresora" runat="server" Font-Italic="true" CssClass="form-select border border-dark mt-3" >
                                        <asp:ListItem Value="0">Seleccionar impresora</asp:ListItem>
                                        <asp:ListItem Value="1">Thermolympic - Nave 3</asp:ListItem>
                                        <asp:ListItem Value="2">Thermolympic - Nave 4</asp:ListItem>
                                          <asp:ListItem Value="3">Thermolympic - GP12</asp:ListItem>
                                        <asp:ListItem Value="4">Thermolympic 3 - Picking</asp:ListItem>
                                            
                                    </asp:DropDownList>
                                    <button type="button" id="BTNIMPRIMIR" class="btn btn-primary btn-lg shadow " style="width: 100%; font-weight: bold">
                                        <i id="ICONPRINTER" class="bi bi-printer-fill"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
















