<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_GP12.aspx.cs" Inherits="ThermoWeb.KPI.KPI_GP12" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores del muro de calidad</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores del muro de calidad
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <%--Desplegables --%>
    <script type="text/javascript">
        $(document).on("click", "[src*=plus1]", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-dash-circle");
            $(this).attr("src", "dash1");
        });
        $(document).on("click", "[src*=dash1]", function () {
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-plus-circle");
            $(this).closest("tr").next().remove();
            $(this).attr("src", "plus1");
        });
        $(document).on("click", "[src*=plus2]", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-dash-circle");
            $(this).attr("src", "dash2");
        });
        $(document).on("click", "[src*=dash2]", function () {
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-plus-circle");
            $(this).closest("tr").next().remove();
            $(this).attr("src", "plus2");
        });
        $(document).on("click", "[src*=plus3]", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-dash-circle");
            $(this).attr("src", "dash3");
        });
        $(document).on("click", "[src*=dash3]", function () {
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-plus-circle");
            $(this).closest("tr").next().remove();
            $(this).attr("src", "plus3");
        });
        $(document).on("click", "[src*=plus4]", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-dash-circle");
            $(this).attr("src", "dash4");
        });
        $(document).on("click", "[src*=dash4]", function () {
            $(this).attr("class", "btn btn-outline-dark ms-md-2 bi bi-plus-circle");
            $(this).closest("tr").next().remove();
            $(this).attr("src", "plus4");
        });
    </script>
    <script type="text/javascript">
        $(function () {
            CargarCharts();
            document.getElementById('LabelChartMesRevisadas').innerHTML = 'Piezas revisadas periodo ' + $("[id *= Selecaño]").val();
            document.getElementById('LabelChartMesCostes').innerHTML = 'Costes de revisión periodo ' + $("[id *= Selecaño]").val();


            $("[id*=Selecaño]").bind("change", function () {
                CargarCharts();
            });
        });
        function CargarCharts() {
            /*
            var option = {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
            
            var ctx = document.getElementById("PieChart").getContext('2d');
            var myPieChart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ["green", "blue", "Gray", "Purple"],
                    datasets: [{
                        backgroundColor: ["#2ecc71", "#3498db", "#95a5a6", "#9b59b6"],
                        data: [12, 20, 18, 50]
                    }]
                }

            });
        
            var ctx_bchart = document.getElementById("BarChart").getContext('2d');

            var myBarChart = new Chart(ctx_bchart, {
                type: 'bar',
                data: {
                    labels: ["jan", "feb", "mar"],
                    datasets: [{
                        label: "Earning",

                        backgroundColor: "rgba(255,99,132,0.2)",
                        borderColor: "rgba(255,99,132,1)",
                        borderWidth: 2,
                        hoverBackgroundColor: "rgba(255,99,132,0.4)",
                        hoverBordercolor: "rgba(255,99,132,1)",
                        data: [65, 56, 45]
                    }]
                },
                options: option

            });
            */
            $.ajax({
                type: "POST",
                url: "KPI_GP12.aspx/GetChartGeneral",
                data: "{periodo: '" + $("[id*=Selecaño]").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    const DataUse = eval(r.d);
                    console.log(DataUse)


                    var ctx_linechart = document.getElementById("LineChartRevisadas").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'line',
                        data: {
                            labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                            datasets: DataUse,

                        },


                        options: {
                            scales: {
                                y: {
                                    ticks: {
                                        // Include a dollar sign in the ticks
                                        callback: function (value, index, ticks) {
                                            return value + '%';
                                        }
                                    }
                                }
                            }
                        }
                    });
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
            $.ajax({
                type: "POST",
                url: "KPI_GP12.aspx/GetChartGeneralCostes",
                data: "{periodo: '" + $("[id*=Selecaño]").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r2) {
                    const DataUseCostes = eval(r2.d);

                    var ctx_linechartCostes = document.getElementById("LineChartCostes").getContext('2d');
                    var myLineChartCostes = new Chart(ctx_linechartCostes, {
                        type: 'bar',
                        data: {
                            labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                            datasets: DataUseCostes,

                        },
                        options: {
                            locale: 'es-ES',
                            scales: {
                                x: {
                                    stacked: true
                                },
                                y: {
                                    stacked: true,
                                    ticks: {
                                        // Include a dollar sign in the ticks
                                        callback: function (value, index, ticks) {
                                            return value + ' €';
                                        }
                                    }

                                }
                            }
                        }
                    });
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        };
    </script>
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
                <li><a class="dropdown-item" href="../GP12/GP12HistoricoReferencia.aspx">Consultar histórico de producto</a></li>
                <li><a class="dropdown-item" href="../KPI/KPIIndice.aspx">Consultar indicadores</a></li>
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
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="d-flex align-items-start">
            <div class="nav flex-column nav-pills me-3 " id="v-pills-tab" role="tablist" aria-orientation="vertical">
                <br />
                <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2">&nbsp General</i></button>
                <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-grid-1x2">Listado</i></button>
                <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Rep.</i></button>
                <button class="nav-link" id="PILLMAQPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab4" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Val.</i></button>
            </div>

            <div class="tab-content col-10" id="v-pills-tabContent">
                <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                    <div class="container">

                        <div class="row">
                            <div class="col-lg-10">
                            </div>
                            <div class="col-lg-2 text-right">
                                <br />
                                <h6>Periodo de revisión:</h6>
                                <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="cargar_tablas">
                                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                    <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                            <h2>&nbsp;&nbsp;&nbsp; Indicadores por mes </h2>
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgv_KPI_Mensual" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                        EmptyDataText="No hay datos para mostrar.">
                                        <HeaderStyle BackColor="#0d6efd" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Mes" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMes" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Horas" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHoras" runat="server" Text='<%#Eval("HORAS","{0:0.00}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Producidas">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProducidas" runat="server" Text='<%#Eval("Cantidad","{0:#,0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Revisadas">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRevisadas" runat="server" Text='<%#Eval("REVISADAS","{0:#,0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% Rev." ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPORCRevisadas" runat="server" Text='<%#Eval("PORCREVI","{0:0.## %}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Buenas">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBuenas" runat="server" Text='<%#Eval("PIEZASOK","{0:#,0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Retrabajadas">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRetrabajadas" runat="server" Text='<%#Eval("RETRABAJADAS","{0:#,0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% Retr." ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPORCREV" runat="server" Text='<%#Eval("PORCRETRABAJADAS","{0:0.## %}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Malas">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMalas" runat="server" Text='<%#Eval("PIEZASNOK","{0:#,0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% Malas" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPORCMalas" runat="server" Text='<%#Eval("PORCNOK","{0:0.## %}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Coste (Inspector)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCosteInspector" runat="server" Text='<%#Eval("COSTEHORASREVISION","{0:c}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Coste (Scrap)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCosteScrap" runat="server" Text='<%#Eval("COSTESCRAP","{0:c}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Coste (Total)" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTETOTAL","{0:c}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-lg-6">
                                    <h5 id="LabelChartMesRevisadas" class="ms-2">Seguimiento por mes</h5>
                                    <div>
                                        <canvas id="LineChartRevisadas" width="100%"></canvas>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <h5 id="LabelChartMesCostes" class="ms-2">Seguimiento por mes</h5>
                                    <div>
                                        <canvas id="LineChartCostes" width="100%"></canvas>
                                    </div>
                                </div>
                            </div>
                            <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab" role="tablist">
                                <li class="nav-item " role="presentation">
                                    <button class="nav-link shadow active " id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Resultados del mes</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow " id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Resultados del año</button>
                                </li>
                            </ul>
                            <div class="tab-content shadow" id="pills-tabContent">
                                <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                                    <div class="row">
                                        <div class="col-lg-2 mt-2 mb-2">
                                            <h6>Mes:</h6>
                                            <asp:DropDownList ID="SelecMes" runat="server" CssClass="form-select shadow-sm mb-2" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="cargar_tablas">
                                                <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-10">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                            <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                                <div class="card-body">
                                                    <div class="row align-items-center m-b-0">
                                                        <div class="col-auto">
                                                            <i class="bi bi-currency-exchange text-white ms-3" style="font-size: 60px"></i>
                                                        </div>
                                                        <div class="col ms-2 text-md-end">
                                                            <i class="text-white me-3 mb-0" runat="server" id="KPICosteTotalMES" style="font-size: 40px">0</i>
                                                            <h6 class="text-white me-2 mb-1">En revisiones</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-footer bg-light text-md-end">
                                                    <h6 class="mb-1" runat="server" id="FootMesCosteTotal">Ir a planes de acción</h6>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                            <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                                <div class="card-body">
                                                    <div class="row align-items-center m-b-0">
                                                        <div class="col-auto">
                                                            <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                        </div>
                                                        <div class="col ms-2 text-md-end">
                                                            <i class="text-white me-3 mb-0" runat="server" id="KPIHorasRevMES" style="font-size: 40px">0</i>
                                                            <h6 class="text-white me-2 mb-1">Horas de revisión</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-footer bg-light text-md-end">
                                                    <h6 class="mb-1" runat="server" id="FootMesHoras">Ir a planes de acción</h6>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                            <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                                <div class="card-body">
                                                    <div class="row align-items-center m-b-0">
                                                        <div class="col-auto">
                                                            <i class="bi bi-trash text-white ms-3" style="font-size: 60px"></i>
                                                        </div>
                                                        <div class="col ms-2 text-md-end">
                                                            <i class="text-white me-3 mb-0" runat="server" id="KPIScrapMES" style="font-size: 40px">0</i>
                                                            <h6 class="text-white me-2 mb-1">Piezas malas</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-footer bg-light text-md-end">
                                                    <h6 class="mb-1" runat="server" id="FootMesMalas">Ir a planes de acción</h6>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                            <div class="card prod-p-card bg-warning background-pattern-white shadow h-100">
                                                <div class="card-body">
                                                    <div class="row align-items-center m-b-0">
                                                        <div class="col-auto">
                                                            <i class="bi bi-exclamation-triangle text-white ms-3" style="font-size: 60px"></i>
                                                        </div>
                                                        <div class="col ms-2 text-md-end">
                                                            <i class="text-white me-3 mb-0" runat="server" id="KPIRetrabajadasMES" style="font-size: 40px">0</i>
                                                            <h6 class="text-white me-2 mb-1">Piezas retrabajadas</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-footer bg-light text-md-end">
                                                    <h6 class="mb-1" runat="server" id="FootMesRetrabajadas">Ir a planes de acción</h6>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp; Top 7 - Chatarra (%)</h2>
                                            <div class="table-responsive" style="width: 100%">
                                                <asp:GridView ID="GridNOK" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay scrap declarado para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail3" CommandName="Redirect" CommandArgument='<%#Eval("Referencia")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNOK" runat="server" Text='<%#Eval("Referencia") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("Nombre") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("HORAS","{0:0.00}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Scrap">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("PIEZASNOK") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPORCNOK" runat="server" Text='<%#Eval("PORCNOK","{0:0.#%}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Total" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteNOK" runat="server" Text='<%#Eval("COSTETOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp; Top 7 - Retrabajo  (%)</h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridRetrabajos" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay piezas retrabajadas para mostrar.">

                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail3" CommandName="Redirect" CommandArgument='<%#Eval("Referencia")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNOK" runat="server" Text='<%#Eval("Referencia") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("Nombre") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("HORAS","{0:0.00}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Retr.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRTR" runat="server" Text='<%#Eval("RETRABAJADAS") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPORCRTR" runat="server" Text='<%#Eval("PORCRetrabajo","{0:0.#%}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Total" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteNOK" runat="server" Text='<%#Eval("COSTETOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp; Top 7 - Costes asociados (€)</h2>
                                            <div class="table-responsive" style="width: 100%">
                                                <asp:GridView ID="GridCoste" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail3" CommandName="Redirect" CommandArgument='<%#Eval("Referencia")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefCoste" runat="server" Text='<%#Eval("Referencia") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescCoste" runat="server" Text='<%#Eval("Nombre") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHorasCoste" runat="server" Text='<%#Eval("HORAS","{0:0.00}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inspección" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteInspc" runat="server" Text='<%#Eval("COSTEHORASREVISION","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Scrap" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblScrapCoste" runat="server" Text='<%#Eval("COSTESCRAP","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Total" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalCoste" runat="server" Text='<%#Eval("COSTETOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp; Top 7 - Horas de revisión (Horas)</h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridHoras" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>

                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail3" CommandName="Redirect" CommandArgument='<%#Eval("Referencia")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefHoras" runat="server" Text='<%#Eval("Referencia") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescHoras" runat="server" Text='<%#Eval("Nombre") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHorasHoras" runat="server" Text='<%#Eval("HORAS","{0:0.00}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Total" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteHoras" runat="server" Text='<%#Eval("COSTETOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp;Razones de revisión (€)</h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridRazonesMES" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;" DataKeyNames="RazonNUM" ShowFooter="true"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBoundTHIRD"
                                                    EmptyDataText="No hay datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <FooterStyle ForeColor="Black" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <button type="button" class="btn btn-outline-dark ms-md-2 bi bi-plus-circle" src="plus3" style="font-size: 1em"></button>
                                                                <asp:Panel ID="pnlOrders3" runat="server" Style="display: none">
                                                                    <asp:GridView ID="GridRazonesMESDetalle" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false"
                                                                        EmptyDataText="No hay datos para mostrar.">
                                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                        <EditRowStyle BackColor="#ffffcc" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesPeriodoAÑODetalle1" runat="server" Text='<%#Eval("MES") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cliente" ItemStyle-BackColor="#EFEFEF">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNombreRazonesAÑODetalle1" runat="server" Text='<%#Eval("Cliente") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Horas">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesConteoMESHORASDetalle" runat="server" Text='<%#Eval("HORAS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Revisiones">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesConteoAÑODetalle1" runat="server" Text='<%#Eval("CONTEO") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Coste total" ItemStyle-Font-Bold="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesCostetotalAÑODetalle1" runat="server" Text='<%#Eval("TOTAL","{0:c}") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mes" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesPeriodoMES" runat="server" Text='<%#Eval("MES") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de revisión" ItemStyle-BackColor="#EFEFEF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNombreRazonesMES" runat="server" Text='<%#Eval("Razon") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <Label style="font-weight:bold">Totales:</Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de revisión" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNombreRazonesMESNUM" runat="server" Text='<%#Eval("RazonNUM") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesConteoMESHoras" runat="server" Text='<%#Eval("HORAS") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblRazonesConteoMESSUMAHoras" runat="server"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Revisiones">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRevisionesConteoMES" runat="server" Text='<%#Eval("CONTEO") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblRevisionesConteoMESSUMAHoras" runat="server"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste scrap">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteScrapMES" runat="server" Text='<%#Eval("SCRAP","{0:c}") %>' />
                                                            </ItemTemplate>
                                                           <FooterTemplate>
                                                                <asp:Label ID="lblCosteScrapMESSUMAHoras" runat="server"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste total" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteTotalMES" runat="server" Text='<%#Eval("TOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                           <FooterTemplate>
                                                                <asp:Label ID="lblCosteTotalMESSUMA" runat="server"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp;Resultados por cliente (€) </h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="dgv_Resultados_Cliente_Mes" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay datos para mostrar.">

                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-BackColor="#ccccff" Visible="false">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblMES" runat="server" Text='<%#Eval("MES")%>' />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cliente" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Revisiones" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("CONTEO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("HorasInspeccion") + "h" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Scrap">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("SCRAP") + "€" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFicha" runat="server" Font-Bold="true" Text='<%#Eval("TOTAL") + "€" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp;Detecciones (Op.) </h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="dgv_KPI_Operarios" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay datos para mostrar.">

                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail3" CommandName="Redirect2" CommandArgument='<%#Eval("IDE")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Operario" ItemStyle-BackColor="#EFEFEF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOperario" runat="server" Text='<%#Eval("IDE") + " " + Eval("Operario") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Detecc.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("SUMA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp;Incidencias de gestión (€)</h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridIncidenciasMES" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;" DataKeyNames="AlertaGP12NUM"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                                    EmptyDataText="No hay datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <button type="button" class="btn btn-outline-dark ms-md-2 bi bi-plus-circle" src="plus2" style="font-size: 1em"></button>
                                                                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                    <asp:GridView ID="GridIncidenciasMESDetalle" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                                        EmptyDataText="No hay datos para mostrar.">
                                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                        <EditRowStyle BackColor="#ffffcc" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Mes" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesPeriodoMESDetalle" runat="server" Text='<%#Eval("MES") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cliente" ItemStyle-BackColor="#EFEFEF">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNombreRazonesMESDetalle" runat="server" Text='<%#Eval("Cliente") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Revisiones">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesConteoMESDetalle" runat="server" Text='<%#Eval("INCIDENCIAS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Coste total" ItemStyle-Font-Bold="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesCosteTotalMESDetalle" runat="server" Text='<%#Eval("COSTE","{0:c}") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mes" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesPeriodoMES" runat="server" Text='<%#Eval("MES") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de incidencia" ItemStyle-BackColor="#EFEFEF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNombreRazonesMES" runat="server" Text='<%#Eval("Razon") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de incidencia" ItemStyle-BackColor="#EFEFEF" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAlertaGP12NUMMES" runat="server" Text='<%#Eval("AlertaGP12NUM") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Revisiones">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesConteoMES" runat="server" Text='<%#Eval("CONTEO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste total" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesCosteTotalMES" runat="server" Text='<%#Eval("TOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                                    <div class="row mt-2">
                                        <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                            <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                                <div class="card-body">
                                                    <div class="row align-items-center m-b-0">
                                                        <div class="col-auto">
                                                            <i class="bi bi-currency-exchange text-white ms-3" style="font-size: 60px"></i>
                                                        </div>
                                                        <div class="col ms-2 text-md-end">
                                                            <i class="text-white me-3 mb-0" runat="server" id="KPICosteTotalAÑO" style="font-size: 40px">0</i>
                                                            <h6 class="text-white me-2 mb-1">En revisiones</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-footer bg-light text-md-end">
                                                    <h6 class="mb-1" runat="server" id="FootAÑOCosteTotal" visible="false">Ir a planes de acción</h6>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                            <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                                <div class="card-body">
                                                    <div class="row align-items-center m-b-0">
                                                        <div class="col-auto">
                                                            <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                        </div>
                                                        <div class="col ms-2 text-md-end">
                                                            <i class="text-white me-3 mb-0" runat="server" id="KPIHorasRevAÑO" style="font-size: 40px">0</i>
                                                            <h6 class="text-white me-2 mb-1">Horas de revisión</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-footer bg-light text-md-end">
                                                    <h6 class="mb-1" runat="server" id="FootAÑOHoras" visible="false">Ir a planes de acción</h6>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                            <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                                <div class="card-body">
                                                    <div class="row align-items-center m-b-0">
                                                        <div class="col-auto">
                                                            <i class="bi bi-trash text-white ms-3" style="font-size: 60px"></i>
                                                        </div>
                                                        <div class="col ms-2 text-md-end">
                                                            <i class="text-white me-3 mb-0" runat="server" id="KPIScrapAÑO" style="font-size: 40px">0</i>
                                                            <h6 class="text-white me-2 mb-1">Piezas malas</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-footer bg-light text-md-end">
                                                    <h6 class="mb-1" runat="server" id="FootAÑOMalas" visible="false">Ir a planes de acción</h6>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                            <div class="card prod-p-card bg-warning background-pattern-white shadow h-100">
                                                <div class="card-body">
                                                    <div class="row align-items-center m-b-0">
                                                        <div class="col-auto">
                                                            <i class="bi bi-exclamation-triangle text-white ms-3" style="font-size: 60px"></i>
                                                        </div>
                                                        <div class="col ms-2 text-md-end">
                                                            <i class="text-white me-3 mb-0" runat="server" id="KPIRetrabajadasAÑO" style="font-size: 40px">0</i>
                                                            <h6 class="text-white me-2 mb-1">Piezas retrabajadas</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-footer bg-light text-md-end">
                                                    <h6 class="mb-1" runat="server" id="FootAÑORetrabajadas" visible="false">Ir a planes de acción</h6>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp; Top 7 - Chatarra (Total NOK)</h2>
                                            <div class="table-responsive" style="width: 100%">
                                                <asp:GridView ID="TopChatarraAño" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay scrap declarado para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail1" CommandName="Redirect" CommandArgument='<%#Eval("Referencia")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNOKAño" runat="server" Text='<%#Eval("Referencia") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescNOKAño" runat="server" Text='<%#Eval("Nombre") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHorasNOKAño" runat="server" Text='<%#Eval("HORAS","{0:0.00}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Scrap">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblScrapNOKAño" runat="server" Text='<%#Eval("PIEZASNOK") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPPMNOKAño" runat="server" Text='<%#Eval("PORCNOK","{0:0.#%}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Total" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteNOKAño" runat="server" Text='<%#Eval("COSTETOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp; Top 7 - Retrabajo  (Total Retr.)</h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="TopRetrabajoAño" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay piezas retrabajadas para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail2" CommandName="Redirect" CommandArgument='<%#Eval("Referencia")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNOKAño" runat="server" Text='<%#Eval("Referencia") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescNOKAño" runat="server" Text='<%#Eval("Nombre") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHorasNOKAño" runat="server" Text='<%#Eval("HORAS","{0:0.00}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Retr.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblScrapNOKAño" runat="server" Text='<%#Eval("RETRABAJADAS") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPPMNOKAño" runat="server" Text='<%#Eval("PORCRetrabajo","{0:0.#%}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Total" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteNOKAño" runat="server" Text='<%#Eval("COSTETOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp; Top 7 - Costes asociados (€)</h2>
                                            <div class="table-responsive" style="width: 100%">
                                                <asp:GridView ID="TopCostesAño" runat="server" AllowSorting="True" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail3" CommandName="Redirect" CommandArgument='<%#Eval("Referencia")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefCosteAño" runat="server" Text='<%#Eval("Referencia") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescCosteAño" runat="server" Text='<%#Eval("Nombre") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHorasCosteAño" runat="server" Text='<%#Eval("HORAS","{0:0.00}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inspección" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteInspcAño" runat="server" Text='<%#Eval("COSTEHORASREVISION","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Scrap" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblScrapCosteAño" runat="server" Text='<%#Eval("COSTESCRAP","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Total" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalCosteAño" runat="server" Text='<%#Eval("COSTETOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp; Top 7 - Horas de revisión (Horas)</h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="TopHorasAño" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay datos para mostrar.">

                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail4" CommandName="Redirect" CommandArgument='<%#Eval("Referencia")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefHorasAño" runat="server" Text='<%#Eval("Referencia") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescHorasAño" runat="server" Text='<%#Eval("Nombre") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHorasHorasAño" runat="server" Text='<%#Eval("HORAS","{0:0.00}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Total" ItemStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteHorasAño" runat="server" Text='<%#Eval("COSTETOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp;Razones de revisión (€) </h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridRazonesAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;" DataKeyNames="RazonNUM" ShowFooter="true"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBoundFOURTH"
                                                    EmptyDataText="No hay datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <button type="button" class="btn btn-outline-dark ms-md-2 bi bi-plus-circle" src="plus4" style="font-size: 1em"></button>
                                                                <asp:Panel ID="pnlOrders4" runat="server" Style="display: none">
                                                                    <asp:GridView ID="GridRazonesAÑODetalle" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false"
                                                                        EmptyDataText="No hay datos para mostrar.">
                                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                        <EditRowStyle BackColor="#ffffcc" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesPeriodoAÑODetalle1" runat="server" Text='<%#Eval("MES") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cliente" ItemStyle-BackColor="#EFEFEF">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNombreRazonesAÑODetalle1" runat="server" Text='<%#Eval("Cliente") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Horas">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesConteoAÑOHoras" runat="server" Text='<%#Eval("HORAS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Revisiones">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesConteoAÑODetalle1" runat="server" Text='<%#Eval("CONTEO") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Coste total" ItemStyle-Font-Bold="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesCostetotalAÑODetalle1" runat="server" Text='<%#Eval("TOTAL","{0:c}") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Año" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesPeriodoAño" runat="server" Text='<%#Eval("AÑO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de revisión" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNombreRazonesMESNUM" runat="server" Text='<%#Eval("RazonNUM") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de revisión" ItemStyle-BackColor="#EFEFEF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNombreRazonesAño" runat="server" Text='<%#Eval("Razon") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesConteoAÑOHoras" runat="server" Text='<%#Eval("HORAS") %>' />
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                                 <asp:Label ID="lblRazonesConteoAÑOSUMAHoras" runat="server"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Revisiones">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesConteoAño" runat="server" Text='<%#Eval("CONTEO") %>' />
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                                 <asp:Label ID="lblRevisionesConteoAÑOSUMAHoras" runat="server"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste scrap">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteScrapAÑO" runat="server" Text='<%#Eval("SCRAP","{0:c}") %>' />
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                                 <asp:Label ID="lblCosteScrapAÑOSUMAHoras" runat="server"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste total" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCosteTotalAÑO" runat="server" Text='<%#Eval("TOTAL","{0:c}") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblCosteTotalAÑOSUMA" runat="server"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp;Resultados por cliente (€) </h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="dgv_Resultados_Cliente_Año" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay datos para mostrar.">

                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Cliente" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Revisiones" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("CONTEO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Horas">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHorasInspec" runat="server" Text='<%#Eval("HorasInspeccion") + "h" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Coste Scrap">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("SCRAP") + "€" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFicha" runat="server" Font-Bold="true" Text='<%#Eval("TOTAL") + "€" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp;Detecciones (Op.) </h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="TopDeteccionesAño" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                    EmptyDataText="No hay datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDetail3" CommandName="Redirect2" CommandArgument='<%#Eval("IDE")%>' CssClass="btn btn-light " Style="font-size: 1rem">
                                                    <i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Operario" ItemStyle-BackColor="#EFEFEF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOperario" runat="server" Text='<%#Eval("IDE") + " " + Eval("Operario") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Detecc.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFichaAño" runat="server" Text='<%#Eval("SUMA") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <h2>&nbsp;&nbsp;&nbsp;Incidencias de gestión (€) </h2>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridIncidenciasAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;" DataKeyNames="AlertaGP12NUM"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBoundSECOND"
                                                    EmptyDataText="No hay datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <button type="button" class="btn btn-outline-dark ms-md-2 bi bi-plus-circle" src="plus1" style="font-size: 1em"></button>

                                                                <asp:Panel ID="pnlOrders2" runat="server" Style="display: none">
                                                                    <asp:GridView ID="GridIncidenciasAÑODetalle" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false"
                                                                        EmptyDataText="No hay datos para mostrar.">
                                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                                        <EditRowStyle BackColor="#ffffcc" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesPeriodoAÑODetalle" runat="server" Text='<%#Eval("MES") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cliente" ItemStyle-BackColor="#EFEFEF">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNombreRazonesAÑODetalle" runat="server" Text='<%#Eval("Cliente") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Revisiones">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesConteoAÑODetalle" runat="server" Text='<%#Eval("INCIDENCIAS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Coste total" ItemStyle-Font-Bold="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRazonesCostetotalAÑODetalle" runat="server" Text='<%#Eval("COSTE","{0:c}") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de incidencia" ItemStyle-BackColor="#EFEFEF" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAlertaGP12NUMAÑO" runat="server" Text='<%#Eval("AlertaGP12NUM") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Año" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesPeriodoAño" runat="server" Text='<%#Eval("AÑO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo de incidencia" ItemStyle-BackColor="#EFEFEF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNombreRazonesAño" runat="server" Text='<%#Eval("Razon") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Revisiones">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesConteoAño" runat="server" Text='<%#Eval("CONTEO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Coste total" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRazonesConteoMES" runat="server" Text='<%#Eval("TOTAL","{0:c}") %>' />
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
                </div>
                <div class="tab-pane fade" id="v-pills-tab2" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab3" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab4" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab5" role="tabpanel" aria-labelledby="v-pills-messages-tab">
                </div>
                <div class="tab-pane fade" id="v-pills-tab6" role="tabpanel" aria-labelledby="v-pills-settings-tab">
                </div>
            </div>
        </div>
    </div>

</asp:Content>




