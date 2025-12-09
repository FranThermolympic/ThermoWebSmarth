<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ConsultaAlmacen.aspx.cs" Inherits="ThermoWeb.LOGISTICA.ConsultaAlmacen" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Movimiento entre almacenes</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Movimiento entre almacenes            
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
    <script type="text/javascript">
        function ShowPopup1() {
            document.getElementById("AUXMODALACCION").click();
        }
        function ClosePopup1() {

        }
        function AlertaResultadoMovimiento(TextoAlerta) {
            alert(TextoAlerta);
        }
        function ShowPopupFirma() {
            document.getElementById("AUXMODALACCIONFIRMA").click();
        }
        function ClosePopupFirma() {
            document.getElementById("AUXCIERRAMODALFIRMA").click();
        }
        function recalcularValorFinal() {

            //Obtengo el valor del número de cajas
            var valorInicial = document.getElementById("InputPZCajas").value;
            valorInicial = parseFloat(valorInicial);

            // Obtener el valor de piezas x cajas
            var multiplicador = document.getElementById("HiddenPZCajas").value;
            multiplicador = parseFloat(multiplicador);

            // Calcular el valor y asignarlo al campo final
            var valorFinal = valorInicial * multiplicador;
            valorFinal = Math.round(valorFinal);
            document.getElementById("CantidadMovimiento").value = valorFinal;
        }

    </script>
    <script type="text/javascript">
        $(function () {
            $("#InputReferencias").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../AutoCompleteServicio.asmx/GetAutoCompleteListaMaterialesXAlmacen", // Ruta al método web de servidor
                        data: JSON.stringify({ term: request.term, almacen: document.getElementById("DropSelect").value }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (xhr, status, error) {
                            console.log(xhr.responseText);
                        }
                    });
                },
                minLength: 2 // Longitud mínima para activar el autocompletado 
            });
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
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">


        <div class="container">
            <div class="col-lg-12">

                <div class="tab-content" id="pills-tabContent">
                    <div class="col-lg-1">
                        <div class="d-grid gap-2 d-md-flex justify-content-md-end  me-md-4 mb-md-1">
                            <button id="AUXCIERRAMODALFIRMA" runat="server" type="button" class="btn-close" data-bs-target="#ModalFirmaOperario" data-bs-toggle="modal" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                            <button id="AUXMODALACCIONFIRMA" runat="server" type="button" class="btn btn-primary invisible " data-bs-toggle="modal" data-bs-target="#ModalFirmaOperario" style="font-size: larger"></button>
                            <button id="AUXCIERRAMODAL" runat="server" type="button" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                            <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>
                        </div>
                    </div>
                    <div class="tab-pane fade show active" id="pills_historico" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                        <div class="row ms-1 me-1">
                            <div class="col-lg-6 p-0">
                                <asp:DropDownList CssClass="form-select border border-dark shadow" runat="server" ID="DropSelect" AutoPostBack="True" OnSelectedIndexChanged="LeerStockAlmacen">
                                    <asp:ListItem Value="10">Thermolympic (Materiales)</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">Thermolympic (Prod. Terminado)</asp:ListItem>
                                    <asp:ListItem Value="TH3">Thermolympic 3 (Sphere)</asp:ListItem>
                                    <%--  <asp:ListItem Value="CH">Jaula de rechazo / Devoluciones</asp:ListItem>--%>
                                    <asp:ListItem Value="CA">Muro de calidad / GP12</asp:ListItem>
                                    <asp:ListItem Value="68">Nave 68</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-6 p-0">
                                <div class="input-group bg-white">
                                    <input class="form-control border border-dark shadow" list="DatalistReferencias" id="InputReferencias" runat="server" placeholder="Escribe una referencia..." autocomplete="off">
                                    <datalist id="DatalistReferencias" runat="server">
                                    </datalist>
                                    <button type="button" class="btn btn-outline-dark" runat="server" id="FiltraProducto" onserverclick="LeerStockAlmacen">Filtrar</button>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="dgv_Almacenes" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 " BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                                <asp:TemplateField HeaderText="Almacen" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetrabajadas" runat="server" Font-Size="X-Large" Text='<%#Eval("almacen") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Font-Size="X-Large" Font-Bold="true" Text='<%#Eval("Producto") %>' />
                                        <asp:Label ID="lblDescripcion" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("Descripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuenas" runat="server" Font-Size="X-Large" Text='<%#Eval("stock","{0:0,0}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" Visible="true" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="button2" CommandName="CambioAlmacen" CommandArgument='<%#Eval("Producto") + "¬" + Eval("Descripcion")+ "¬" + Eval("stock")+ "¬" + Eval("almacen")%>' UseSubmitBehavior="true" CssClass="btn btn-lg btn-primary mt-1 shadow-lg" Style="font-size: 1rem">
                                          <i class="bi bi-file-post"></i></asp:LinkButton>
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
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header bg-primary shadow">
                        <h5 class="modal-title text-white" id="staticBackdropLabel" runat="server">Mover producto entre almacenes</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        <asp:TextBox ID="IDINSPECCION" runat="server" Style="text-align: center" Width="100%" Enabled="false" Visible="false"></asp:TextBox>
                    </div>
                    <div class="modal-body" runat="server">
                        <div>
                            <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                                <div class="d-flex align-items-start">
                                    <div class="nav flex-column nav-pills me-2" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                                        <button id="TABACCION" class="nav-link active" data-bs-toggle="pill" data-bs-target="#v-pills-home" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true"><i class="bi bi-kanban"></i></button>
                                    </div>
                                    <div class="tab-content" id="v-pills-tabContent">
                                        <div class="tab-pane fade show active" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                            <div class="container-fluid">
                                                <div class="row">
                                                    <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                        <div class="col-sm-12">
                                                            <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>Producto</h5>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <label id="detalleReferencia" runat="server" class="form-label ms-4 mt-1" style="text-align: left; font-size: x-large; font-weight: 600" enabled="false"></label>
                                                        <asp:HiddenField ID="AUXReferenciaMovimiento" runat="server" />
                                                    </div>
                                                    <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                        <div class="col-sm-12">
                                                            <h5 class="mt-1"><i class="bi bi-boxes me-2"></i>Existencias por almacén</h5>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-10 mt-2">

                                                        <asp:GridView ID="dgv_StockProducto" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered border-dark table-responsive shadow p-3 ms-2" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                                            <HeaderStyle CssClass="card-header" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="White" />
                                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Almacen" ItemStyle-Width="60%" ItemStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRetrabajadas" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("almacen") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBuenas" runat="server" Font-Size="Large" Text='<%#Eval("stock","{0:0,0}") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="col-lg-2 mt-2">
                                                        <asp:GridView ID="dgv_Lotes" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered border-dark table-responsive shadow p-3 ms-2" BorderColor="black" Width="100%" OnRowCommand="GridViewCommandEventHandler">
                                                            <HeaderStyle CssClass="card-header" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="White" />
                                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Lotes" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLotes" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("almacen") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                        <div class="col-sm-12">
                                                            <h5 class="mt-1"><i class="bi bi-arrow-left-right me-2"></i>Mover material</h5>
                                                        </div>

                                                    </div>
                                                    <div class="col-lg-12">
                                                        <asp:HiddenField ID="HiddenPZCajas" runat="server" />
                                                        <div class="input-group border border-dark rounded rounded-2 shadow mt-2 mb-1">
                                                            <span class="input-group-text" style="width: 30%">Número de cajas</span>
                                                            <input type="number" id="InputPZCajas" onkeyup="recalcularValorFinal()" class="form-control" style="width: 15%">
                                                            <style>
                                                                input::-webkit-outer-spin-button,
                                                                input::-webkit-inner-spin-button {
                                                                    -webkit-appearance: none;
                                                                    margin: 0;
                                                                }

                                                                input[type="number"] {
                                                                    -moz-appearance: textfield;
                                                                }
                                                            </style>
                                                            <span class="input-group-text" runat="server" style="width: 55%" id="AuxPZcajas">x .00</span>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-5">
                                                        <h6>Almacén de origen</h6>
                                                        <asp:DropDownList CssClass="form-select border border-dark shadow" runat="server" ID="DropAlmOrigen" Enabled="false">
                                                            <asp:ListItem Value="0">-</asp:ListItem>
                                                            <asp:ListItem Value="10">Thermolympic (Materiales)</asp:ListItem>
                                                            <asp:ListItem Value="1" Selected="True">Thermolympic (Prod. Terminado)</asp:ListItem>
                                                            <asp:ListItem Value="TH3">Thermolympic 3 (Sphere)</asp:ListItem>
                                                            <%--<asp:ListItem Value="CH">Jaula de rechazo / Devoluciones</asp:ListItem>--%>
                                                            <asp:ListItem Value="CA">Muro de calidad / GP12</asp:ListItem>
                                                            <asp:ListItem Value="68">Nave 68</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <h6>Cantidad</h6>
                                                        <input class="form-control border border-dark shadow" runat="server" id="CantidadMovimiento" type="number" max="100" min="-100" value="0" />
                                                    </div>
                                                    <div class="col-lg-5">
                                                        <h6>Almacén destino</h6>
                                                        <asp:DropDownList CssClass="form-select border border-dark shadow" runat="server" ID="DropAlmFinal">
                                                            <asp:ListItem Value="0">-</asp:ListItem>
                                                            <asp:ListItem Value="10">Thermolympic (Materiales)</asp:ListItem>
                                                            <asp:ListItem Value="1" Selected="True">Thermolympic (Prod. Terminado)</asp:ListItem>
                                                            <asp:ListItem Value="TH3">Thermolympic 3 (Sphere)</asp:ListItem>
                                                            <%-- <asp:ListItem Value="CH">Jaula de rechazo / Devoluciones</asp:ListItem>--%>
                                                            <asp:ListItem Value="CA">Muro de calidad / GP12</asp:ListItem>
                                                            <asp:ListItem Value="68">Nave 68</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <button type="button" class="btn btn-success" style="width: 100%" runat="server" onserverclick="MoverEntreAlmacenes">Ejecutar movimiento</button>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
