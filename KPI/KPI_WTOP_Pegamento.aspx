<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_WTOP_Pegamento.aspx.cs" Inherits="ThermoWeb.KPI.KPI_WTOP_Pegamento" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Seguimiento cambio de bidón</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Seguimiento cambio de bidón
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <%--Desplegables --%>

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
                            <h2>&nbsp;&nbsp;&nbsp; Últimos cambios de bidón de pegamento </h2>
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgv_KPI_Mensual" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;" OnRowCreated="OnRowDataBound"
                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                        EmptyDataText="No hay datos para mostrar.">
                                        <HeaderStyle BackColor="#0d6efd" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Inicio Cambio" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIniCam" runat="server" Text='<%#Eval("INICIO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fin Cambio" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinCam" runat="server" Text='<%#Eval("HORAFIN") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Tiempo Cambio (horas)"  ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTiempoCam" runat="server" Text='<%#Eval("TIEMPO_CAMBIO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Horas produciendo"  ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHoras" runat="server" Text='<%#Eval("HORASBIDON") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Piezas fabricadas"  ItemStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPiezas" runat="server" Text='<%#Eval("PIEZASBIDON") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            
                        </div>
                        <div class="row mb-2 invisible">
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
                    </div>
                </div>
               
            </div>
        </div>
    </div>

</asp:Content>




