<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="IoTRefrigeracion.aspx.cs" Inherits="ThermoWeb.PRODUCCION.IoTRefrigeracion" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Caudales de máquina</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Caudales             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <script type="text/javascript">
        function ShowPopup1(machvar) {
            document.getElementById("lblMaqPOP").innerText = "Máquina " + machvar + "";
            document.getElementById("LblPOPHidTemp").innerText = "Hidráulica - Temperatura refrigeración (ºC)";
            document.getElementById("LblPOPHidCaudal").innerText = "Hidráulica - Caudal de agua (m3/h)";
            document.getElementById("LblPOPMolTemp").innerText = "Moldes - Temperatura refrigeración (ºC)";
            document.getElementById("LblPOPMolCaudal").innerText = "Moldes - Caudal de agua (m3/h)";
            document.getElementById("AUXMODALACCION").click();
            CargarCharts(machvar);

        }
        function ShowPopup2(machvar) {
            document.getElementById("lblMaqPOP").innerText = "Enfriadora y Torre";
            document.getElementById("LblPOPHidTemp").innerText = "Torre - Temperatura refrigeración (ºC)";
            document.getElementById("LblPOPHidCaudal").innerText = "Torre - Caudal de agua (m3/h)";
            document.getElementById("LblPOPMolTemp").innerText = "Enfriadora - Temperatura refrigeración (ºC)";
            document.getElementById("LblPOPMolCaudal").innerText = "Enfriadora - Caudal de agua (m3/h)";
            document.getElementById("AUXMODALACCION").click();
            CargarChartsENFTOR(machvar);

        }
        function ClosePopup1() {

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
        function CargarCharts(machvar) {
            $.ajax({
                type: "POST",
                url: "IoTRefrigeracion.aspx/GetChart",
                data: "{maquina: '" + machvar + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//Entrada Hidra
                    var series2 = r.d[2];//Salida Hidra
                    var series3 = r.d[3];//Caudal Hidra
                    var series4 = r.d[4];//Entrada Molde
                    var series5 = r.d[5];//Salida Molde
                    var series6 = r.d[6];//Caudal Molde

                    //Hidraulica Temps
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Entrada",
                                pointStyle: 'triangle',
                                borderColor: "rgb(11, 81, 255)",
                                backgroundColor: "rgb(169, 204, 227 )",
                                fill: '+1',
                                data: series1
                            },
                            {
                                label: "Salida",
                                pointStyle: 'triangle',
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 201, 205 )",
                                fill: '+1',
                                data: series2
                            }

                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartHidraulicaTemps").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 12
                                    }
                                },
                                y: {

                                    ticks: {
                                        // Include a dollar sign in the ticks

                                        callback: function (value, index, ticks) {
                                            return value + 'ºC';
                                        }
                                    }

                                }
                            }
                        }
                    })

                    //Hidraulica Caudal
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Caudal de agua",
                                pointStyle: 'recRot',
                                borderColor: "rgb(162, 80, 255 )",
                                backgroundColor: "rgb(212, 174, 255 )",
                                data: series3
                            },


                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartHidraulicaCaudal").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 12
                                    }
                                },
                                y: {

                                    ticks: {
                                        // Include a dollar sign in the ticks

                                        callback: function (value, index, ticks) {
                                            return value + ' m3/h';
                                        }
                                    }

                                }
                            }
                        }
                    })

                    //Moldes Temps
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Entrada",
                                pointStyle: 'triangle',
                                borderColor: "rgb(11, 81, 255)",
                                backgroundColor: "rgb(169, 204, 227 )",
                                fill: '+1',
                                data: series4
                            },
                            {
                                label: "Salida",
                                pointStyle: 'triangle',
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 201, 205 )",
                                fill: '+1',
                                data: series5
                            }

                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartMoldeTemps").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 12
                                    }
                                },
                                y: {

                                    ticks: {
                                        // Include a dollar sign in the ticks

                                        callback: function (value, index, ticks) {
                                            return value + 'ºC';
                                        }
                                    }

                                }
                            }
                        }
                    })

                    //Molde Caudal
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Caudal de agua",
                                pointStyle: 'recRot',
                                borderColor: "rgb(162, 80, 255 )",
                                backgroundColor: "rgb(212, 174, 255 )",
                                data: series6
                            },
                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartMoldeCaudal").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 12
                                    }
                                },
                                y: {

                                    ticks: {
                                        // Include a dollar sign in the ticks

                                        callback: function (value, index, ticks) {
                                            return value + ' m3/h';
                                        }
                                    }
                                }
                            }
                        }
                    })
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
        function CargarChartsENFTOR(machvar) {
            $.ajax({
                type: "POST",
                url: "IoTRefrigeracion.aspx/GetChart",
                data: "{maquina: '" + machvar + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//Salida Torre
                    var series2 = r.d[2];//Retorno Torre
                    var series3 = r.d[3];//Caudal Hidra
                    var series4 = r.d[4];//Salida Enfriadora
                    var series5 = r.d[5];//Retorno Enfriadora
                    var series6 = r.d[6];//Caudal Enfriadora

                    //Torre Temps
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Salida",
                                pointStyle: 'triangle',
                                borderColor: "rgb(11, 81, 255)",
                                backgroundColor: "rgb(169, 204, 227 )",
                                fill: '+1',
                                data: series1
                            },
                            {
                                label: "Retorno",
                                pointStyle: 'triangle',
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 201, 205 )",
                                fill: '+1',
                                data: series2
                            }

                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartHidraulicaTemps").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 12
                                    }
                                },
                                y: {

                                    ticks: {
                                        // Include a dollar sign in the ticks

                                        callback: function (value, index, ticks) {
                                            return value + 'ºC';
                                        }
                                    }

                                }
                            }
                        }
                    })

                    //Hidraulica Caudal
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Caudal de agua",
                                pointStyle: 'recRot',
                                borderColor: "rgb(162, 80, 255 )",
                                backgroundColor: "rgb(212, 174, 255 )",
                                data: series3
                            },


                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartHidraulicaCaudal").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 12
                                    }
                                },
                                y: {

                                    ticks: {
                                        // Include a dollar sign in the ticks

                                        callback: function (value, index, ticks) {
                                            return value + ' m3/h';
                                        }
                                    }

                                }
                            }
                        }
                    })

                    //Enfriadora Temps
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Salida",
                                pointStyle: 'triangle',
                                borderColor: "rgb(11, 81, 255)",
                                backgroundColor: "rgb(169, 204, 227 )",
                                fill: '+1',
                                data: series4
                            },
                            {
                                label: "Retorno",
                                pointStyle: 'triangle',
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 201, 205 )",
                                fill: '+1',
                                data: series5
                            }

                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartMoldeTemps").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 12
                                    }
                                },
                                y: {

                                    ticks: {
                                        // Include a dollar sign in the ticks

                                        callback: function (value, index, ticks) {
                                            return value + 'ºC';
                                        }
                                    }

                                }
                            }
                        }
                    })

                    //Molde Caudal
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Caudal de agua",
                                pointStyle: 'recRot',
                                borderColor: "rgb(162, 80, 255 )",
                                backgroundColor: "rgb(212, 174, 255 )",
                                data: series6
                            },
                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartMoldeCaudal").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 12
                                    }
                                },
                                y: {

                                    ticks: {
                                        // Include a dollar sign in the ticks

                                        callback: function (value, index, ticks) {
                                            return value + ' m3/h';
                                        }
                                    }
                                }
                            }
                        }
                    })
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
    </script>
    <style>
        .vertical-text {
            transform: rotate(270deg);
            transform-origin: center;
            position: relative;
            right: 20px;
            top: 20px;
        }
    </style>
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="tab-content shadow" id="pills-tabContent">
            <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-6 p-0">
                            <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-1 ">
                                <div class="card-header bg-primary text-white">
                                    <label style="font-weight: 700; font-size: large">Torre de agua</label>
                                    <button class="btn btn-sm btn-outline-dark float-end bg-white" id="GRAPHTOR" runat="server" onserverclick="AbrirGraficas"><i class="bi bi-graph-up"></i></button>
                                </div>
                                <div class="card-body">
                                    <div class="row ">
                                        <div class="col-lg-4">
                                            <label style="font-weight: 700">Temp. salida:</label><br /><label class="ms-2" runat="server" id="TORRETEMPENT" style="font-size:x-large">-</label><br />
                                        </div>
                                        <div class="col-lg-4">
                                            <label style="font-weight: 700">Temp. retorno:</label><br /><label class="ms-2" runat="server" id="TORRETEMPSAL" style="font-size:x-large">-</label><br />
                                        </div>
                                        <div class="col-lg-4">
                                            <label style="font-weight: 700">Caudal:</label><br /><label class="ms-2" runat="server" id="TORRECAUDAL" style="font-size:x-large">-</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <label id="TORRETIMESTAMP" runat="server" style="font-size: small; font-style: italic" class="float-end bottom">-</label>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-6 p-0">
                            <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-1 ">
                                <div class="card-header bg-primary text-white">
                                    <label style="font-weight: 700; font-size: large">Enfriadora</label>
                                    <button class="btn btn-sm btn-outline-dark float-end bg-white" id="GRAPHENF" runat="server" onserverclick="AbrirGraficas"><i class="bi bi-graph-up"></i></button>
                                </div>
                                <div class="card-body">
                                    <div class="row ">
                                        <div class="col-lg-4">
                                            <label style="font-weight: 700">Temp. salida:</label><br /><label class="ms-2" runat="server" id="ENFRIADORATEMPENT" style="font-size:x-large">-</label><br />
                                        </div>
                                        <div class="col-lg-4">
                                            <label style="font-weight: 700">Temp. retorno:</label><br /><label class="ms-2" runat="server" id="ENFRIADORATEMPSAL" style="font-size:x-large">-</label><br />
                                        </div>
                                        <div class="col-lg-4">
                                            <label style="font-weight: 700">Caudal:</label><br /><label class="ms-2" runat="server" id="ENFRIADORACAUDAL" style="font-size:x-large">-</label>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="card-footer">
                                            <label id="ENFRIADORATIMESTAMP" runat="server" style="font-size: small; font-style: italic" class="float-end bottom">-</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-lg-4">
                            <div class="row border border-1 border-dark rounded rounded-2 shadow" style="background-color: #ebeced">
                                <div class="card-header bg-primary text-white rounded rounded-2 shadow">
                                    <label style="font-weight: 700; font-size: large" class="ms-3"><i class="bi bi-building me-2"></i>NAVE 5</label>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">39</label><label id="M39ESTADOMAQ" runat="server" class="ms-2" style="font-style: italic">-</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="GRAPH39" runat="server" onserverclick="AbrirGraficas"><i class="bi bi-graph-up"></i></button>
                                            </div>
                                            <div class="card-body">
                                                <div class="row ">
                                                    <div class="col-lg-2 align-content-center">
                                                        <label class="vertical-text align-middle" style="font-weight: bold">MOLDE</label>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <label style="font-weight: 700">Temp. entrada:</label><label class="ms-2" runat="server" id="M39MOLTEMPENT">-</label><br />
                                                        <label style="font-weight: 700">Temp. salida:</label><label class="ms-2" runat="server" id="M39MOLTEMSAL">-</label><br />
                                                        <label style="font-weight: 700">Caudal:</label><label class="ms-2" runat="server" id="M39MOLCAUDAL">-</label>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row ">
                                                    <div class="col-lg-2">
                                                        <label class="vertical-text align-middle" style="font-weight: bold">HIDRA.</label>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <label style="font-weight: 700">Temp. entrada:</label><label class="ms-2" runat="server" id="M39HIDTEMPENT">-</label><br />
                                                        <label style="font-weight: 700">Temp. salida:</label><label class="ms-2" runat="server" id="M39HIDTEMSAL">-</label><br />
                                                        <label style="font-weight: 700">Caudal:</label><label class="ms-2" runat="server" id="M39HIDCAUDAL">-</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer">
                                                <label id="M39AMBIENTAL" runat="server" class="ms-2" style="font-size: small">-</label>
                                                <label id="M39TIMESTAMP" runat="server" style="font-size: small; font-style: italic" class="float-end bottom">-</label>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white" id="M33CARDHEADER" runat="SERVER">
                                                <label style="font-weight: 700; font-size: large">33</label><label id="M33ESTADOMAQ" runat="server" class="ms-2" style="font-style: italic">-</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="GRAPH33" runat="server" onserverclick="AbrirGraficas"><i class="bi bi-graph-up"></i></button>
                                            </div>
                                            <div class="card-body">
                                                <div class="row ">
                                                    <div class="col-lg-2">
                                                        <label class="vertical-text align-middle" style="font-weight: bold">MOLDE</label>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <label style="font-weight: 700">Temp. entrada:</label><label class="ms-2" runat="server" id="M33MOLTEMPENT">-</label><br />
                                                        <label style="font-weight: 700">Temp. salida:</label><label class="ms-2" runat="server" id="M33MOLTEMSAL">-</label><br />
                                                        <label style="font-weight: 700">Caudal:</label><label class="ms-2" runat="server" id="M33MOLCAUDAL">-</label>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row ">
                                                    <div class="col-lg-2">
                                                        <label class="vertical-text align-middle" style="font-weight: bold">HIDRA.</label>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <label style="font-weight: 700">Temp. entrada:</label><label class="ms-2" runat="server" id="M33HIDTEMPENT">-</label><br />
                                                        <label style="font-weight: 700">Temp. salida:</label><label class="ms-2" runat="server" id="M33HIDTEMSAL">-</label><br />
                                                        <label style="font-weight: 700">Caudal:</label><label class="ms-2" runat="server" id="M33HIDCAUDAL">-</label>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="card-footer">
                                                <label id="M33AMBIENTAL" runat="server" class="ms-2" style="font-size: small">-</label>
                                                <label id="M33TIMESTAMP" runat="server" style="font-size: small; font-style: italic" class="float-end bottom">-</label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="row border border-1 border-dark rounded rounded-2 shadow" style="background-color: #ebeced">
                                <div class="card-header bg-primary text-white rounded rounded-2 shadow">
                                    <label style="font-weight: 700; font-size: large" class="ms-3"><i class="bi bi-building me-2"></i>NAVE 4</label>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">30</label><label id="M30ESTADOMAQ" runat="server" class="ms-2" style="font-style: italic">-</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="GRAPH30" runat="server" onserverclick="AbrirGraficas"><i class="bi bi-graph-up"></i></button>
                                            </div>
                                            <div class="card-body">
                                                <div class="row ">
                                                    <div class="col-lg-2">
                                                        <label class="vertical-text align-middle" style="font-weight: bold">MOLDE</label>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <label style="font-weight: 700">Temp. entrada:</label><label class="ms-2" runat="server" id="M30MOLTEMPENT">-</label><br />
                                                        <label style="font-weight: 700">Temp. salida:</label><label class="ms-2" runat="server" id="M30MOLTEMSAL">-</label><br />
                                                        <label style="font-weight: 700">Caudal:</label><label class="ms-2" runat="server" id="M30MOLCAUDAL">-</label>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row ">
                                                    <div class="col-lg-2">
                                                        <label class="vertical-text align-middle" style="font-weight: bold">HIDRA.</label>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <label style="font-weight: 700">Temp. entrada:</label><label class="ms-2" runat="server" id="M30HIDTEMPENT">-</label><br />
                                                        <label style="font-weight: 700">Temp. salida:</label><label class="ms-2" runat="server" id="M30HIDTEMSAL">-</label><br />
                                                        <label style="font-weight: 700">Caudal:</label><label class="ms-2" runat="server" id="M30HIDCAUDAL">-</label>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="card-footer">
                                                <label id="M30AMBIENTAL" runat="server" class="ms-2" style="font-size: small">-</label>
                                                <label id="M30TIMESTAMP" runat="server" style="font-size: small; font-style: italic" class="float-end bottom">-</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-lg-4">
                            <div class="row border border-1 border-dark rounded rounded-2 shadow" style="background-color: #ebeced">
                                <div class="card-header bg-primary text-white rounded rounded-2 shadow">
                                    <label style="font-weight: 700; font-size: large" class="ms-3"><i class="bi bi-building me-2"></i>NAVE 3</label>
                                </div>

                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">16</label><label id="M16ESTADOMAQ" runat="server" class="ms-2" style="font-style: italic">-</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="GRAPH16" runat="server" onserverclick="AbrirGraficas"><i class="bi bi-graph-up"></i></button>
                                            </div>
                                            <div class="card-body">
                                                <div class="row ">
                                                    <div class="col-lg-2">
                                                        <label class="vertical-text align-middle" style="font-weight: bold">MOLDE</label>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <label style="font-weight: 700">Temp. entrada:</label><label class="ms-2" runat="server" id="M16MOLTEMPENT">-</label><br />
                                                        <label style="font-weight: 700">Temp. salida:</label><label class="ms-2" runat="server" id="M16MOLTEMSAL">-</label><br />
                                                        <label style="font-weight: 700">Caudal:</label><label class="ms-2" runat="server" id="M16MOLCAUDAL">-</label>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row ">
                                                    <div class="col-lg-2">
                                                        <label class="vertical-text align-middle" style="font-weight: bold">HIDRA.</label>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <label style="font-weight: 700">Temp. entrada:</label><label class="ms-2" runat="server" id="M16HIDTEMPENT">-</label><br />
                                                        <label style="font-weight: 700">Temp. salida:</label><label class="ms-2" runat="server" id="M16HIDTEMSAL">-</label><br />
                                                        <label style="font-weight: 700">Caudal:</label><label class="ms-2" runat="server" id="M16HIDCAUDAL">-</label>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="card-footer">
                                                <label id="M16AMBIENTAL" runat="server" class="ms-2" style="font-size: small">-</label>
                                                <label id="M16TIMESTAMP" runat="server" style="font-size: small; font-style: italic" class="float-end bottom">-</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>

                </div>
            </div>

            <div class="tab-pane fade show" id="pills-histo" role="tabpanel" aria-labelledby="pills-profile-tab">
                <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>
            </div>
        </div>
        <div class="modal fade" id="ModalEditaAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
            <div class="modal-dialog modal-fullscreen">
                <div class="modal-content">
                    <div class="modal-header bg-primary shadow">
                        <h5 class="modal-title text-white" id="staticBackdropLabel" runat="server"><i class="bi bi-thermometer-snow me-2"></i>Sensores de temperatura y caudal -
                            <label id="lblMaqPOP">Máquina 33</label></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        <asp:TextBox ID="IDINSPECCION" runat="server" Style="text-align: center" Width="100%" Enabled="false" Visible="false"></asp:TextBox>
                    </div>
                    <div class="modal-body" runat="server">
                        <div>
                            <div class="row " style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                                <div class="row mb-2 bg-white bg-opacity-75 ms-1 ">
                                    <div class="col-lg-6 border border-secondary rounded rounded-2 shadow">
                                        <h5 id="LblPOPMolTemp">Molde - Temperatura refrigeración (ºC)</h5>
                                        <div>
                                            <canvas id="LineChartMoldeTemps" width="500" height="200"></canvas>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 border border-secondary rounded rounded-2 shadow">
                                        <h5 id="LblPOPMolCaudal">Molde - Caudal de agua (m3/h)</h5>
                                        <div>
                                            <canvas id="LineChartMoldeCaudal" width="500" height="200"></canvas>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mb-2 bg-white bg-opacity-75 ms-1 ">
                                    <div class="col-lg-6 border border-secondary rounded rounded-2 shadow">
                                        <h5 id="LblPOPHidTemp">Hidráulica - Temperatura refrigeración (ºC)</h5>
                                        <div>
                                            <canvas id="LineChartHidraulicaTemps" width="500" height="200"></canvas>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 border border-secondary rounded rounded-2 shadow">
                                        <h5 id="LblPOPHidCaudal">Hidráulica - Caudal de agua (m3/h)</h5>
                                        <div>
                                            <canvas id="LineChartHidraulicaCaudal" width="500" height="200"></canvas>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </div>

        <%--MODALES DE EDICION --%>
    </div>
</asp:Content>
