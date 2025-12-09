<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GestionMolinosLITE.aspx.cs" Inherits="ThermoWeb.MATERIALES.GestionMolinosLITE" MasterPageFile="~/SMARTHLITE.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Gestión de molidos</title>
    <link rel="shortcut icon" type="image/x-icon" href="../FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Molidos             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Indicadores
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="UbicacionMateriasPrimas.aspx">Ubicaciones materiales</a></li>
                <li><a class="dropdown-item" href="../KPI/KPI_Molidos.aspx">Resultados de reciclado</a></li>
            </ul>
        </li>

    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <script type="text/javascript">
        function ShowPopup1() {
            document.getElementById("AUXMODALACCION").click();
        }
        function ShowPopupMolido() {
            document.getElementById("AUXMODALMOLIDO").click();
        }
        function ClosePopup1() {

        }
        function ImprimeEtiquetasNEW(tipo) {
            //document.getElementById("ICONPRINTER").setAttribute("class", "spinner-border");
            $.ajax({
                type: "POST",
                url: "GestionMolinosLITE.aspx/ImprimirEtiquetasAuxiliares",
                //data: "{MATERIAL: '" + document.getElementById("AUX_MATERIAL").value + "', DESCRIPCION: '" + document.getElementById("AUX_DESCRIPCION").value + "', INPUTOPERARIO: '" + document.getElementById("InputOperario").value + "', LOTE: '" + document.getElementById("InputLote").value + "', TIPO: '" + document.getElementById("DropTipoPrint").value + "'}",
                data: "{MATERIAL: '" + document.getElementById("lblMaterialMolido").innerText + "', INPUTOPERARIO: '" + document.getElementById("inputOperario").value + "', MOLINO: '" + document.getElementById("lblMolinoAsignado").innerText + "', INPUTCANTIDAD: '" + document.getElementById("inputMolidoKgs").value + "', TIPO: '"+tipo+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert("Vamos");
                    document.getElementById("CIERRAMODALPRINT").click();
                    //document.getElementById("ICONPRINTER").setAttribute("class", "bi bi-printer-fill");
                },
                failure: function (response) {
                    //alert("NoVamos");
                    document.getElementById("CIERRAMODALPRINT").click();
                    //document.getElementById("ICONPRINTER").setAttribute("class", "bi bi-printer-fill");
                }
            });
        }
        $(function () {
            $("#inputNuevoMaterial").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../AutoCompleteServicio.asmx/GetAutoCompleteListaMateriales", // Ruta al método web de servidor
                        data: JSON.stringify({ term: request.term }),
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
        $(function () {
            $("#inputOperario").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../AutoCompleteServicio.asmx/GetAutoCompleteListaOperariosAnoniNUMS", // Ruta al método web de servidor
                        data: JSON.stringify({ term: request.term }),
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
                minLength: 1 // Longitud mínima para activar el autocompletado
            });
        });

    </script>
    <script type="text/javascript">
        window.onload = function () {
            const image = document.getElementById("IMGESTANTE");

            document.getElementById("PILL_EST0").onclick = function CargaEST0() {
                image.src = '..\\SMARTH_fonts\\INTERNOS\\MOLINOS GENERAL.PNG';
            }
            document.getElementById("PILL_EST1").onclick = function CargaEST1() {
                image.src = '..\\SMARTH_fonts\\INTERNOS\\MOLINOS NAVE5.PNG';
            }
            document.getElementById("PILL_EST2").onclick = function CargaEST2() {
                image.src = '..\\SMARTH_fonts\\INTERNOS\\MOLINOS NAVE4.PNG';
            }
            document.getElementById("PILL_EST3").onclick = function CargaEST3() {
                image.src = '..\\SMARTH_fonts\\INTERNOS\\MOLINOS NAVE3.PNG';
            }
            document.getElementById("PILL_EST4").onclick = function CargaEST4() {
                image.src = '..\\SMARTH_fonts\\INTERNOS\\MOLINOS NAVE2.PNG';
            }
            document.getElementById("BTNIMPRIMIR").onclick = function fun() {
                //alert("ENTRA");
                ImprimeEtiquetasNEW("PROVISIONAL");
                //validation code to see State field is mandatory.  
            }
        }
    </script>
    
    <style>
        .ps-25 {
            padding-left: 0.75rem !important;
            padding-right: 0.75rem !important;
        }

        .ui-front {
            z-index: 9999999 !important;
        }
    </style>
    
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="row">
            <div class="col-lg-2 mt-2">
                <div style="background-color: cornflowerblue" class="rounded rounded-2">
                    <asp:HyperLink ID="hyperlink1" NavigateUrl="IndiceMaterialesLITE.aspx" class="btn btn-sm btn-outline-dark" Style="font-weight: bold; width: 100%; color: white; text-shadow: 1px 1px 1px dimgrey" runat="server"><i class="bi bi-arrow-left-square">&nbsp VOLVER</i></asp:HyperLink>
                </div>
                <ul class="nav flex-column nav-pills rounded rounded-2 border border-secondary shadow bg-white mt-0" style="width: 100%" id="pills-tab" role="tablist">
                    <h6 class="ms-2 mt-1 " style="font-style: italic"><i class="bi bi-layout-sidebar-inset"></i>&nbsp MÁQUINAS</h6>
                    <li class="nav-item ms-1 me-1" role="presentation" id="Li6" runat="server">
                        <button class="nav-link shadow  border border-secondary active" runat="server" id="PILL_EST0" data-bs-toggle="pill" data-bs-target="#TAB_EST_0" type="button" role="tab" aria-controls="TAB_EST_1" aria-selected="true" style="font-weight: bold; width: 100%"><i class="bi bi-card-list"></i>&nbsp PRODUCIENDO</button>
                    </li>
                    <h6 class="ms-2 mt-3" style="font-style: italic"><i class="bi bi-layout-sidebar-inset"></i>&nbsp MOLINOS</h6>
                    <li class="nav-item ms-1 me-1" role="presentation" id="ref0lab" runat="server">
                        <button class="nav-link shadow  border border-secondary" runat="server" id="PILL_EST1" data-bs-toggle="pill" data-bs-target="#TAB_EST_1" type="button" role="tab" aria-controls="TAB_EST_1" aria-selected="true" style="font-weight: bold; width: 100%"><i class="bi bi-columns"></i>&nbsp NAVE 5</button>
                    </li>
                    <li class="nav-item ms-1 me-1" role="presentation" id="ref1lab" runat="server">
                        <button class="nav-link  shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST2" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_2" type="button" role="tab" aria-controls="pills-home" aria-selected="true"><i class="bi bi-columns"></i>&nbsp NAVE 4</button>
                    </li>
                    <li class="nav-item ms-1 me-1" role="presentation" id="ref2lab" runat="server">
                        <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST3" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp NAVE 3</button>
                    </li>
                    <li class="nav-item ms-1 me-1" role="presentation" id="ref3lab" runat="server">
                        <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST4" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp MAT.</button>
                    </li>
                </ul>
            </div>
            <div class="col-lg-10 mt-2">
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">
                        <asp:Image ID="IMGESTANTE" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\MOLINOS GENERAL.png" CssClass="border border-dark border-2 rounded rounded-1 shadow" Width="100%" />
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="tab-content">
                    <div id="TAB_EST_0" class="tab-pane fade show active" runat="server">
                        <asp:GridView ID="dgv_Materiales" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                            Width="98.5%" CssClass="table table-responsive shadow p-3 mt-2 rounded border border-dark" AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display.">
                            <HeaderStyle CssClass="border border-dark" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" BorderColor="#c0c0c0" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>

                                <asp:TemplateField HeaderText="Máquina" ItemStyle-Width="10%" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="X-Large" ItemStyle-BackColor="#6699ff" ItemStyle-ForeColor="white" ItemStyle-CssClass="border border-dark shadow">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("MAQ")   %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <button>a</button>
                                        <button>b</button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Molino asignado" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderColor="black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMolino" runat="server" Font-Size="Large" Font-Bold="true" Font-Italic="true" Text='<%#Eval("MOLINO") %>' /><br />
                                        <asp:Label ID="Label1" runat="server" Font-Size="Small" Text='<%#Eval("UBICACION") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Material" ItemStyle-Width="72%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterial" Font-Bold="true" Font-Size="X-Large" CssClass="ms-2" runat="server" Text='<%#Eval("MATERIAL") %>' />
                                        <asp:Label ID="lblDescripcion" runat="server" Font-Size="Large" CssClass="ms-2" Text='<%#Eval("DESCRIPCION") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="TAB_EST_1" class="tab-pane fade " runat="server">
                        <div class="row border  rounded rounded-2 shadow" style="background-color: #ebeced">
                            <div class="card p-0">
                                <div class="card-header border border-dark text-white shadow" style="background-color: cornflowerblue">
                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px dimgrey">NAVE 5</label>
                                </div>
                                <div class="card-body ps-25 border border-top-0 border-dark p-0">
                                    <div class="row">
                                        <div class="col-lg-4 mt-2">
                                        </div>
                                        <div class="col-lg-4 mt-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº8</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 5 - MÁQUINA 48)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM8" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM8" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM8" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM8MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="col-lg-4 mt-2"></div>
                                        
                                    </div>
                                    <div class="row border border-end-0 border-start-0 border-secondary p-0 mt-2 mb-2 shadow" style="background-color: lightgray; text-align: center">
                                        <h2 class="mt-2 mb-2" style="color: white; text-shadow: 1px 1px 2px black">PASILLO</h2>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 mb-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº33</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 5 - MÁQUINA 43)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM33" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM33" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM33" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM33MAT"></label>
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
                    <div id="TAB_EST_2" class="tab-pane fade" runat="server">
                        <div class="row border  rounded rounded-2 shadow" style="background-color: #ebeced">
                            <div class="card p-0">
                                <div class="card-header border border-dark text-white shadow" style="background-color: cornflowerblue">
                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px dimgrey">NAVE 4</label>
                                </div>
                                <div class="card-body ps-25 border border-top-0 border-dark p-0">
                                    <div class="row">
                                        <div class="col-lg-4 mt-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº17</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 4 - MÁQUINA 23)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM17" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM17" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM17" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM17MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 mt-2">
                                        </div>
                                        <div class="col-lg-4 mt-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº10</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 4 - MÁQUINA 31)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM10" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM10" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM10" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM10MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-8 mt-2">
                                        </div>
                                        <div class="col-lg-4 mt-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº50</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 4 - FONDO)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM50" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM50" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM50" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM50MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row border border-end-0 border-start-0 border-secondary p-0 mt-2 mb-2 shadow" style="background-color: lightgray; text-align: center">
                                        <h2 class="mt-2 mb-2" style="color: white; text-shadow: 1px 1px 2px black">PASILLO</h2>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 mb-2 mt-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº7</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 4 - MÁQUINA 32)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM7" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM7" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM7" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM7MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 mb-2 mt-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº6</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 4 - MÁQUINA 29)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM6" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM6" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM6" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM6MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 mb-2 mt-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº4</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 4 - MÁQUINA 38)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM4" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM4" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM4" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM4MAT"></label>
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
                    <div id="TAB_EST_3" class="tab-pane fade" runat="server">
                        <div class="row border  rounded rounded-2 shadow" style="background-color: #ebeced">
                            <div class="card p-0">
                                <div class="card-header border border-dark text-white shadow" style="background-color: cornflowerblue">
                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px dimgrey">NAVE 3</label>
                                </div>
                                <div class="card-body ps-25 border border-top-0 border-dark p-0">
                                    
                                    <div class="row" style="height: 125px">
                                        <div class="col-lg-4 mt-2"></div>
                                        <div class="col-lg-4 mt-2"></div>
                                        <div class="col-lg-4 mt-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº14</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 5 - MÁQUINA 34)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM14" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM14" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM14" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM14MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row border border-end-0 border-start-0 border-secondary p-0 mt-2 mb-2 shadow" style="background-color: lightgray; text-align: center">
                                        <h2 class="mt-2 mb-2" style="color: white; text-shadow: 1px 1px 2px black">PASILLO</h2>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 mt-2 mb-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº3</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 3 - MÁQUINA 46)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM3" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM3" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM3" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM3MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 mt-2">

                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº5</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 3 - MÁQUINA 47)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM5" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM5" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM5" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM5MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 mt-2">
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="TAB_EST_4" class="tab-pane fade" runat="server">
                        <div class="row border  rounded rounded-2 shadow" style="background-color: #ebeced">
                            <div class="card p-0">
                                <div class="card-header border border-dark text-white shadow" style="background-color: cornflowerblue">
                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px dimgrey">MATERIALES</label>
                                </div>
                                <div class="card-body ps-25 border border-top-0 border-dark p-0">
                                    <div class="row">
                                        <div class="col-lg-4 mt-2 mb-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº1</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 2 - MATERIALES)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM1" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM1" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM1" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM1MAT"></label>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 mt-2">
                                            <div class="card shadow">
                                                <div class="card-header border border-dark text-white shadow" style="background-color: orange">
                                                    <label style="font-weight: 700; font-size: large; text-shadow: 1px 1px 2px black">Molino Nº2</label><label class="ms-2" style="font-size: small; font-style: italic; text-shadow: 1px 1px 2px black">(NAVE 2 - MATERIALES)</label>
                                                    <button class="btn btn-sm btn-outline-dark float-end bg-white shadow" id="MueleM2" visible="false" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                                </div>
                                                <div class="card-body border border-dark border-top-0" style="border-bottom-right-radius: 5px; border-bottom-left-radius: 5px">
                                                    <div class="row">
                                                        <div class="col-lg-3">
                                                            <button class="btn btn-sm btn-primary border border-dark shadow me-1" runat="server" id="EditaM2" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                                            <button class="btn btn-sm btn-danger border border-dark shadow" runat="server" id="BorraM2" onserverclick="Editar_Molino"><i class="bi bi-x"></i></button>
                                                        </div>
                                                        <div class="col-lg-9">
                                                            <label class="ms-2" style="font-weight: 700" runat="server" id="lblM2MAT"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 mt-2">
                                        </div>
                                    </div>
                                    <div class="row border border-end-0 border-start-0 border-secondary p-0 mt-2 mb-2 shadow" style="background-color: lightgray; text-align: center">
                                        <h2 class="mt-2 mb-2" style="color: white; text-shadow: 1px 1px 2px black">ESTANTERÍAS</h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <div class="row invisible">
        <div class="col-lg-1"></div>
        <div class="col-lg-1 mt-1">
            <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-3 me-md-4 mb-md-1">
                <button id="AUXCIERRAMODAL" runat="server" type="button" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary " hidden="hidden" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>
                <button id="AUXMODALMOLIDO" runat="server" type="button" class="btn btn-primary " hidden="hidden" data-bs-toggle="modal" data-bs-target="#ModalMolerMaterial" style="font-size: larger"></button>
                <button id="btnoffcanvas" runat="server" type="button" class="btn btn-primary ms-md-0 bi bi-funnel shadow-sm" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger"></button>
            </div>
        </div>
    </div>
    <%--Modales --%>
    <div class="modal fade" id="ModalEditaAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h4 class="modal-title text-white" id="labelMolino" runat="server">Sin molino</h4>
                    <label runat="server" id="IDlabelMolino" visible="false">0</label>
                    <button type="button" ID="CIERRAMODAL" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

                </div>
                <div class="modal-body" runat="server" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                    <div>
                        <div class="row">
                            <div class="col-lg-6">
                                <label style="font-size: x-large; font-weight: bold">Material actual:</label>
                                <label class="ms-4" id="lblMaterialActual" runat="server" style="font-style: italic; font-size: large"></label>
                                <br />
                            </div>
                            <div class="col-lg-6">
                                <div id="DIVMaterialAlternativo" runat="server" visible="false">
                                    <label style="font-size: x-large; font-weight: bold">Recicla en:</label><br />
                                    <label class="ms-4" id="lblMaterialAlternativo" runat="server" style="font-style: italic; font-size: large"></label>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-12">

                                <label style="font-size: x-large; font-weight: bold">Nuevo material a moler:</label>
                                <div class="input-group">
                                    <input class="form-control" id="inputNuevoMaterial" runat="server" autocomplete="off" placeholder="Escribe un material...">
                                    <button class="btn btn-success" type="button" id="BtnCambiaMaterial" runat="server" onserverclick="Guardar_Molino" style="width: 80px"><i class="bi bi-check-lg"></i></button>



                                </div>
                            </div>

                        </div>
                    </div>

                </div>
                <div class="modal-footer" style="background: #e6e6e6">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalMolerMaterial" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="MolerMaterialLabel" aria-hidden="false">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h3 class="modal-title text-white" id="lblMaterialMolido" runat="server">Material</h3>
                    <h4 class="modal-title text-white ms-3" id="lblDescripcionMolido" runat="server">Descripcion</h4>
                    <h4 class="modal-title text-white ms-3" id="lblMolinoAsignado" hidden="hidden" runat="server">0</h4>
                    <button type="button" id="CIERRAMODALPRINT" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

                </div>
                <div class="modal-body" runat="server" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                    <div>
                        
                        <div class="row mt-3">
                              <div class="col-lg-4">
                                <div class="input-group input-group-lg">
                                    <label style="font-size: xx-large">Operario:</label>
                                    <input type="number" id="inputOperario" runat="server" class="form-control ms-3" aria-label="Amount (to the nearest dollar)">
      

                                </div>
                            </div>
                            <div class="col-lg-8">
                                <div class="input-group input-group-lg">
                                    <label style="font-size: xx-large">Cantidad a reciclar:</label>
                                    <input type="number" id="inputMolidoKgs" runat="server" class="form-control ms-3" aria-label="Amount (to the nearest dollar)">
                                    <span class="input-group-text">Kgs.</span>
                                    <button class="btn btn-success" type="button" id="Button1" runat="server" onserverclick="Moler_Material"><i class="bi bi-check-lg">Registrar Kgs.</i></button>

                                </div>
                            </div>
                            
                        </div>
                        <div class="row" id="rowMolidoAlternativo" runat="server">
                         <div class="col-lg-6"></div>
                            <div class="col-lg-6">
                                <label id="lblMaterialAlternativoAviso" runat="server" style="font-style: italic" class="float-end"></label>
                                <br />
                                <label id="lblMaterialAlternativoAvisoReferencia" runat="server" style="font-style: italic" class="float-end"></label>
                            </div>
                           

                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-2"></div>
                            
                            <div class="col-lg-6"></div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="background: #e6e6e6">
                    <div class="bg-white rounded rounded-2 shadow">
                        <button type="button" id="BTNIMPRIMIR" class="btn btn-outline-dark float-start" style="font-weight:bold"><i class="bi bi-printer">&nbsp Etiqueta provisional</i></button>
                        <%--  <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
