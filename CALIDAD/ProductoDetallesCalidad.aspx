<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ProductoDetallesCalidad.aspx.cs" Inherits="ThermoWeb.CALIDAD.ProductoDetallesCalidad" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Detalles de producto</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Detalles de producto             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Revisiones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../GP12/GP12.aspx">Iniciar una revisión</a></li>
                <li><a class="dropdown-item" href="../GP12/PrevisionGP12.aspx">Consultar planificación de cargas</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación de referencia</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../GP12/GP12Historico.aspx">Consultar últimas revisiones</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12HistoricoCliente.aspx">Consultar histórico de cliente</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12HistoricoReferencia.aspx">Consultar histórico de producto</a></li>
                <li><a class="dropdown-item" href="../KPI/KPI_GP12.aspx">Consultar indicadores</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../KPI/KPIIndice.aspx">Ver cuadros de mando</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Gestiones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../GP12/GP12ReferenciasEstado.aspx">Gestionar estado de referencias</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12RegistroComunicaciones.aspx">Registrar comunicaciones</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../PDCA/PDCA.aspx">Abrir plan de acción</a></li>
                <li><a class="dropdown-item" href="../CALIDAD/Alertas_Calidad.aspx">Abrir no conformidad</a></li>

            </ul>
        </li>
    </ul>
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
        function ShowPopupFirma() {
            document.getElementById("AUXMODALACCIONFIRMA").click();
        }
        function ClosePopupFirma() {
            document.getElementById("AUXCIERRAMODALFIRMA").click();
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
    <script type="text/javascript">
        function CargarCharts(referencia, molde, fechainicio, fechafin) {
            document.getElementById('loading-overlay').hidden = false;
            document.getElementById('loading-overlay0').hidden = false;
            document.getElementById('loading-overlay2').hidden = false;
            document.getElementById('GR1').hidden = false;
            document.getElementById('GR2').hidden = false;

            $.ajax({
                type: "POST",
                url: "ProductoDetallesCalidad.aspx/GetChart",
                data: "{producto: '" + referencia + "', molde: '" + molde + "', fechainicio: '" + fechainicio + "', fechafin:'" + fechafin + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//DATOS PRODUCCION
                    var series2 = r.d[2];//DATOS GP12

                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Produccion",
                                //pointStyle: 'triangle',
                                borderColor: "rgb(11, 81, 255)",
                                backgroundColor: "rgb(1, 145, 245)",
                                //fill: '+1',
                                data: series1
                            },
                            {
                                label: "GP12",
                                //pointStyle: 'triangle',
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 193, 27 )",
                                //fill: '+1',
                                data: series2
                            }
                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartDefectos").getContext('2d');
                    var lblfin = "";
                    if (fechafin != "") { lblfin = fechafin } else { lblfin = "hoy" };
                    document.getElementById('loading-overlay2').hidden = true;
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'bar',
                        data,
                        options: {

                            scales: {
                                x: {
                                    stacked: true
                                },
                                y: {
                                    stacked: true,
                                    ticks: {
                                        callback: function (value, index, ticks) {
                                            return value + ' pz.';
                                        }
                                    }
                                }
                            },
                            plugins: {
                                title: {
                                    display: true,
                                    text: "Defectivo acumulado por tipo ( " + fechainicio + " a " + lblfin + ")",
                                },
                                legend:
                                {
                                    position: 'bottom',
                                }
                            },
                        }
                    })
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
            CargarChartsAnual(referencia, molde, fechainicio, fechafin)
        }
        function CargarChartsAnual(referencia, molde, fechainicio, fechafin) {
            const loadingOverlay2 = document.getElementById('loading-overlay');
            $.ajax({
                type: "POST",
                url: "ProductoDetallesCalidad.aspx/GetChartAnual",
                data: "{producto: '" + referencia + "', molde: '" + molde + "',fechainicio: '" + fechainicio + "', fechafin:'" + fechafin + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//DATOS PRODUCCION
                    var series2 = r.d[2];//DATOS GP12
                    var series3 = r.d[3];//EUROS PRODUCCION
                    var series4 = r.d[4];//EUROS GP12 HORAS
                    var series5 = r.d[5];//EUROS GP12 PIEZAS
                    var series6 = r.d[6];//EUROS NO CONFORMIDADES 


                    //PIEZAS MALAS
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Produccion",
                                //pointStyle: 'triangle',
                                borderColor: "rgb(11, 81, 255)",
                                backgroundColor: "rgb(1, 145, 245)",
                                //fill: '+1',
                                data: series1
                            },
                            {
                                label: "GP12",
                                //pointStyle: 'triangle',
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 193, 27 )",
                                //fill: '+1',
                                data: series2
                            }

                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartEvolucion").getContext('2d');
                    var lblfin = "";
                    if (fechafin != "") { lblfin = fechafin } else { lblfin = "hoy" };
                    document.getElementById('loading-overlay').hidden = true;
                    const footer = (tooltipItems) => {
                        let suma = 0;

                        tooltipItems.forEach(function (tooltipItem) {
                            suma += tooltipItem.parsed.y;
                        });
                        return 'Total: ' + suma + '%';
                    };
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'bar',
                        data,
                        options: {
                            interaction: {
                                intersect: false,
                                mode: 'index',
                            },
                            scales: {
                                x: {
                                    stacked: true
                                },
                                y: {
                                    stacked: true,
                                    ticks: {
                                        callback: function (value, index, ticks) {
                                            return value + '%';
                                        }
                                    }
                                }
                            },
                            responsive: true,
                            plugins: {

                                title: {
                                    display: true,
                                    text: "% Scrap respecto a piezas producidas ( " + fechainicio + " a " + lblfin + ")",
                                },
                                legend:
                                {
                                    position: 'bottom',
                                },
                                tooltip: {
                                    callbacks: {
                                        footer: footer,
                                    }
                                }
                            }
                        },
                    })

                    //EUROS
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Produccion",
                                //pointStyle: 'triangle',
                                borderColor: "rgb(11, 81, 255)",
                                backgroundColor: "rgb(1, 145, 245)",
                                //fill: '+1',
                                data: series3
                            },
                            {
                                label: "GP12 (Horas)",
                                //pointStyle: 'triangle',
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 193, 27 )",
                                //fill: '+1',
                                data: series4
                            },
                            {
                                label: "GP12 (Scrap)",
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 33, 33 )",
                                data: series5
                            },
                            {
                                label: "No conformidades",
                                borderColor: "rgb(75, 116, 255)",
                                backgroundColor: "rgb(75, 116, 255)",
                                data: series6
                            },


                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartEvolucioneEUROS").getContext('2d');
                    document.getElementById('loading-overlay0').hidden = true;
                    const footer2 = (tooltipItems) => {
                        let sum = 0;

                        tooltipItems.forEach(function (tooltipItem) {
                            sum += tooltipItem.parsed.y;
                        });
                        return 'Total: ' + sum + '€';
                    };
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'bar',
                        data,
                        options: {
                            interaction: {
                                intersect: false,
                                mode: 'index',
                            },
                            scales: {
                                x: {
                                    stacked: true
                                },
                                y: {
                                    stacked: true,
                                    ticks: {
                                        callback: function (value, index, ticks) {
                                            return value + '€';
                                        }
                                    }
                                }
                            },
                            responsive: true,
                            plugins: {
                                title: {
                                    display: true,
                                    text: "Distribución de costes de No Calidad ( " + fechainicio + " a " + lblfin + ")",
                                },
                                legend:
                                {
                                    position: 'bottom',
                                },
                                tooltip: {
                                    callbacks: {
                                        footer: footer2,
                                    }
                                }
                            }

                        },

                    })
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
    </script>
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="row shadow" style="background-color: gainsboro">
            <div class="col-lg-4">
                <div class="input-group mt-1 mb-1 shadow">
                    <span class="input-group-text border border-dark" id="basic-addon1">Referencia</span>
                    <input class="form-control border border-dark" list="DatalistReferencias" id="selectReferencia" runat="server" placeholder="Escribe una referencia...">
                </div>
                <datalist id="DatalistReferencias" runat="server"></datalist>
            </div>
            <div class="col-lg-3">
                <div class="input-group mt-1 mb-1 shadow">
                    <span class="input-group-text border border-dark" id="basic-addon1">Molde</span>
                    <input class="form-control border border-dark" list="DatalistMolde" id="selectMolde" runat="server" placeholder="Escribe un molde...">
                </div>
                <datalist id="DatalistMolde" runat="server"></datalist>
            </div>
            <div class="col-lg-2">
                <div class="input-group mt-1 mb-1 shadow">
                    <span class="input-group-text border border-dark">Desde</span>
                    <input type="text" id="InputFechaDesde" class="form-control  border border-dark Add-text" autocomplete="off" runat="server">
                </div>
            </div>
            <div class="col-lg-2">
                <div class="input-group mt-1 mb-1 shadow">
                    <span class="input-group-text border border-dark">Hasta</span>
                    <input type="text" id="InputFechaHasta" class="form-control  border border-dark Add-text"  autocomplete="off" runat="server">
                </div>
            </div>
            <div class="col-lg-1">
                <button id="Button2" runat="server" onserverclick="RellenarGrids" type="button" class="btn btn-secondary border border-dark  mt-1 shadow" style="width: 100%; text-align: left">
                    <i class="bi bi-search">&nbsp Filtrar</i></button>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6">
                <h4 id="GR1" hidden="hidden">Evolución mensual</h4>
                <div class="container-fluid">
                    <div class="d-flex align-items-start">
                        <div class="nav flex-column nav-pills me-3" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                            <button class="nav-link active" id="v-pills-home-tab" data-bs-toggle="pill" data-bs-target="#v-pills-home" type="button" role="tab" aria-controls="v-pills-home" aria-selected="true" style="font-weight: bold; font-style: italic">%</button>
                            <button class="nav-link" id="v-pills-profile-tab" data-bs-toggle="pill" data-bs-target="#v-pills-profile" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="font-weight: bold; font-style: italic">€</button>
                        </div>
                        <div class="tab-content col-11" id="v-pills-tabContent">
                            <div class="tab-pane fade show active" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab" tabindex="0" style="width: 100%">
                                <canvas id="LineChartEvolucion" width="500"></canvas>
                                <div id="loading-overlay" style="text-align: center" hidden="hidden">
                                    <img src="newloading.gif" />
                                </div>
                            </div>
                            <div class="tab-pane fade" id="v-pills-profile" role="tabpanel" aria-labelledby="v-pills-profile-tab" tabindex="0">
                                <canvas id="LineChartEvolucioneEUROS" width="500"></canvas>
                                <div id="loading-overlay0" style="text-align: center" hidden="hidden">
                                    <img src="newloading.gif" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <h4 id="GR2" hidden="hidden">Vista del periodo</h4>
                <canvas id="LineChartDefectos" width="500"></canvas>
                <div id="loading-overlay2" style="text-align: center" hidden="hidden">
                    <img src="newloading.gif" />
                </div>

            </div>

        </div>
        <div class="row">
            <div class="col-lg-12">
                <h4 id="LABELGRIDVIEW" runat="server" visible="false">Detalles del producto</h4>
                <asp:GridView ID="dgv_GP12_Historico" runat="server" AllowSorting="false" OnRowDataBound="GridView_DataBoundHist"
                    Width="98.5%" AutoGenerateColumns="true" CssClass="table table-responsive shadow p-3 rounded border-top-0 mt-2"
                    EmptyDataText="No hay revisiones para mostrar.">
                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                    <RowStyle BackColor="White" />
                    <AlternatingRowStyle BackColor="#eeeeee" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="FALSE">
                            <HeaderTemplate>
                                <button type="button" class="btn btn-outline-dark bg-white ms-md-1" onclick="CargarCharts()"><i class="bi bi-link-45deg"></i></button>
                            </HeaderTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
        <div class="row">
            <div class="col-lg-6" id="LABELNC" runat="server" visible="false">
                <h4 class="mt-2">No conformidades</h4>
                <asp:GridView ID="dgv_NoConformidades" runat="server" AllowSorting="false"
                    Width="98.5%" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0 mt-2"
                    EmptyDataText="No hay reclamaciones en el periodo seleccionado.">
                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                    <RowStyle BackColor="White" />
                    <AlternatingRowStyle BackColor="#eeeeee" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="FALSE">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="PRODUCTO" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="Gainsboro">
                            <HeaderTemplate>
                                <label>APP</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Font-Bold="true" runat="server" Text='NO CONFORMIDADES' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="Gainsboro">
                            <HeaderTemplate>
                                <label>Reclamaciones</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLote" Font-Size="Larger" Font-Bold="true" runat="server" Text='<%#Eval("RECLAMACIONES") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <label>Administrativos</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAdmon" Font-Size="Larger" Font-Italic="true" runat="server" Text='<%#Eval("ADMINISTRATIVOS") + "€" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <label>Cargos</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCargos" Font-Size="Larger" Font-Italic="true" runat="server" Text='<%#Eval("CARGOS") + "€" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <label>Rechazo</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRechazo" Font-Size="Larger" Font-Italic="true" runat="server" Text='<%#Eval("RECHAZO") + "€" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <label>Selección (externa)</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSelecOUT" Font-Size="Larger" Font-Italic="true" runat="server" Text='<%#Eval("SELECCION_EXT") + "€" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <label>Selección (interna)</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSelecINT" Font-Size="Larger" Font-Italic="true" runat="server" Text='<%#Eval("SELECCION_INT") + "€" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <label>Otros</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOtros" Font-Size="Larger" Font-Italic="true" runat="server" Text='<%#Eval("OTROS") + "€"%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>

    </div>

</asp:Content>




