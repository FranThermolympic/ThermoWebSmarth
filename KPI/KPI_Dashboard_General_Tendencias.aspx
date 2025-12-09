<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_Dashboard_General_Tendencias.aspx.cs" Inherits="ThermoWeb.KPI.KPI_Dashboard_General_Tendencias" MasterPageFile="~/SMARTHLite.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores de planta</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores de planta
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CargarChartsOEE() {
            $.ajax({
                type: "POST",
                url: "KPI_Dashboard_General_Tendencias.aspx/BARCHART_OEECAL_OEE",
                data: null,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0]; //Etiquetas
                    var series1 = r.d[1].slice(0, 12);//Objetivo OEE
                    var series2 = r.d[2].slice(0, 12);//OEE General

                    var series3 = r.d[3];//Objetivo OEE Calidad
                    var series4 = r.d[4];//OEE Calidad

                    var series5 = r.d[5].slice(0, 12);//Resultados OEE General
                    var series6 = r.d[6];//Resultados OEE Calidad

                    //Gráfica OEE Gral
                    var data = {
                        labels: r.d[0].slice(0, 12),
                        datasets: [
                            {
                                label: new Date().getFullYear(),
                                pointStyle: 'triangle',
                                pointRadius: 6,
                                pointHoverRadius: 15,
                                borderColor: "rgb(2, 35, 245 )",
                                backgroundColor: "rgb(83, 106, 252)",
                                type: 'line',
                                cubicInterpolationMode: 'monotone',
                                tension: 0.4,
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                data: series2

                            },
                            {
                                label: "Objetivo",
                                pointStyle: false,
                                borderColor: "rgb(252, 3, 3)",
                                backgroundColor: "rgba(231, 76, 60,0.2)",
                                borderWidth: 2,
                                type: 'line',
                                fill: { value: 0 },
                                data: series1
                            }
                        ]
                    };
                    var ctx_linechart = document.getElementById("BARChartOEEPlanta").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        //type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 7

                                    }
                                },
                                y: {
                                    min: 65,
                                    max: 80,

                                    ticks: {
                                        // Include a dollar sign in the ticks
                                        beginAtZero: false,
                                        stepSize: 5,
                                        callback: function (value, index, ticks) {
                                            return value + '%';
                                        }
                                    }

                                }
                            },

                            plugins: {
                                legend: {
                                    display: false
                                },
                                fillAboveLine: {
                                    fillColor: 'rgba(255, 201, 205)'  // Color de fondo del área por encima del valor deseado
                                }
                            }
                        }
                    })
                    //CABECERAS Y DATOS ADICIONALES OEEGral
                    var serieAUX = r.d[2];
                    document.getElementById("LblAnualOEEGral").innerText = series5[2];
                    document.getElementById("lblOBJOEEGral").innerText = " Objetivo: >" + series1[0] + "% aprovechado";

                    if (series5[0] == "INCUMPLIDO") {
                        document.getElementById("OEEGralEmojiVerde").hidden = true;
                        document.getElementById("OEEGralEmojiRojo").hidden = false;
                    }
                    else {
                        document.getElementById("OEEGralEmojiVerde").hidden = false;
                        document.getElementById("OEEGralEmojiRojo").hidden = true;
                    };
                    if (series5[1] == "INCUMPLIDO") {
                        document.getElementById("COLOEEGral").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-danger bg-opacity-25";
                    }
                    else {
                        document.getElementById("COLOEEGral").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-success bg-opacity-25";
                    };

                    //Gráfica OEECalidad
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: new Date().getFullYear() - 1,
                                pointStyle: 'triangle',
                                pointRadius: 6,
                                pointHoverRadius: 15,
                                borderColor: "rgb(2, 35, 245 )",
                                backgroundColor: "rgb(83, 106, 252)",
                                type: 'line',
                                cubicInterpolationMode: 'monotone',
                                tension: 0.4,
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                data: series4
                            },
                            {
                                pointStyle: false,
                                borderColor: "rgb(252, 3, 3)",
                                backgroundColor: "rgba(231, 76, 60,0.2)",
                                borderWidth: 2,
                                type: 'line',
                                fill: { value: 0 },
                                data: series3
                            }
                        ]
                    };
                    var ctx_linechart = document.getElementById("BARChartOEECalidad").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        //type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 7
                                    }
                                },
                                y: {
                                    min: 96,
                                    max: 100,
                                    ticks: {
                                        // Include a dollar sign in the ticks
                                        beginAtZero: false,
                                        stepSize: 1,
                                        callback: function (value, index, ticks) {
                                            return value + '%';
                                        }
                                    }
                                }
                            },

                            plugins: {
                                legend: {
                                    display: false
                                },
                                fillAboveLine: {
                                    fillColor: 'rgba(255, 201, 205)'  // Color de fondo del área por encima del valor deseado
                                }

                            }
                        }
                    })

                    //CABECERAS Y DATOS ADICIONALES OEECalidad
                    var serieAUX2 = r.d[4];
                    document.getElementById("LblAnualOEECalidad").innerText = series6[2];
                    document.getElementById("lblOBJOEECalidad").innerText = " Objetivo: >" + series3[0] + "% piezas conformes";
                    if (series6[0] == "INCUMPLIDO") {
                        document.getElementById("OEECalidadEmojiVerde").hidden = true;
                        document.getElementById("OEECalidadEmojiRojo").hidden = false;
                    }
                    else {
                        document.getElementById("OEECalidadEmojiVerde").hidden = false;
                        document.getElementById("OEECalidadEmojiRojo").hidden = true;
                    };
                    if (series6[1] == "INCUMPLIDO") {
                        document.getElementById("COLOEECalidad").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-danger bg-opacity-25";
                    }
                    else {
                        document.getElementById("COLOEECalidad").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-success bg-opacity-25";
                    };

                    //document.getElementById("LabelAñoAnterior").innerText = "Año " + (new Date().getFullYear() - 1);
                    //document.getElementById("LabelAñoActual").innerText = "Año " + new Date().getFullYear();
                    CargarChartsNoConformidades();
                    CargarChartsAprovechados();
                    CargarChartsAbsentismo();
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
        function CargarChartsNoConformidades() {
            $.ajax({
                type: "POST",
                url: "KPI_Dashboard_General_Tendencias.aspx/BARCHART_NOCONFORMIDADES",
                data: null,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0]; //Meses
                    var series1 = r.d[1];//Objetivo NoConformidades
                    var series2 = r.d[2];//OEE NoConformidades 
                    var series3 = r.d[3];//Resultados NoConformidades


                    //Gráfica OEE
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: new Date().getFullYear(),
                                pointStyle: 'triangle',
                                pointRadius: 6,
                                pointHoverRadius: 15,
                                borderColor: "rgb(2, 35, 245 )",
                                backgroundColor: "rgb(83, 106, 252)",
                                type: 'line',
                                cubicInterpolationMode: 'monotone',
                                tension: 0.4,
                                data: series2
                            },
                            {
                                label: "Objetivo",
                                pointStyle: false,

                                borderColor: "rgb(252, 3, 3)",
                                backgroundColor: "rgba(231, 76, 60,0.2)",
                                borderWidth: 2,
                                type: 'line',
                                fill: { value: 11 },
                                data: series1
                            }
                        ]
                    };
                    var ctx_linechart = document.getElementById("BarChartNoConformidades").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        //type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 7
                                    }
                                },
                                y: {
                                    min: 0,
                                    max: 8,

                                    ticks: {
                                        // Include a dollar sign in the ticks
                                        beginAtZero: false,
                                        stepSize: 3,
                                        // callback: function (value, index, ticks) {
                                        //     return value + '%';
                                        //}
                                    }

                                }
                            },

                            plugins: {
                                legend: {
                                    display: false
                                },

                            }
                        }
                    })

                    //CABECERAS Y DATOS ADICIONALES OEECalidad
                    //var serieAUX = r.d[3];
                    document.getElementById("LblAnualNoConformidades").innerText = series3[2];
                    document.getElementById("lblOBJOEENoConformidades").innerText = " Objetivo: <" + series1[0] + " no conformidades";
                    if (series3[0] == "INCUMPLIDO") {
                        document.getElementById("OEENoConformidadesEmojiVerde").hidden = true;
                        document.getElementById("OEENoConformidadesEmojiRojo").hidden = false;
                    }
                    else {
                        document.getElementById("OEENoConformidadesEmojiVerde").hidden = false;
                        document.getElementById("OEENoConformidadesEmojiRojo").hidden = true;
                    };
                    if (series3[1] == "INCUMPLIDO") {
                        document.getElementById("COLNoConformidades").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-danger bg-opacity-25";

                        document.getElementById("COLNoConformidades").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-danger bg-opacity-25";
                    }
                    else {
                        document.getElementById("COLNoConformidades").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-success bg-opacity-25";
                    };
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
        function CargarChartsAprovechados() {
            $.ajax({
                type: "POST",
                url: "KPI_Dashboard_General_Tendencias.aspx/BARCHART_APROVECHAMIENTO",
                data: null,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0].slice(0, 12); //Meses
                    var series1 = r.d[1].slice(0, 12);//Objetivo Aprovechamiento
                    var series2 = r.d[2].slice(0, 12);//Aprovechamiento Año Anterior

                    var series3 = r.d[3];//Resultados Aprovechamiento


                    //Gráfica OEE
                    var data = {
                        labels: etiquetas,
                        datasets: [

                            {
                                label: new Date().getFullYear() - 1,
                                pointStyle: 'triangle',
                                pointRadius: 6,
                                pointHoverRadius: 15,
                                borderColor: "rgb(2, 35, 245 )",
                                backgroundColor: "rgb(83, 106, 252)",
                                type: 'line',
                                cubicInterpolationMode: 'monotone',
                                tension: 0.4,
                                data: series2
                            },
                            {
                                label: "Objetivo",
                                pointStyle: false,

                                borderColor: "rgb(252, 3, 3)",
                                backgroundColor: "rgba(231, 76, 60,0.2)",
                                borderWidth: 2,
                                type: 'line',
                                fill: { value: 3 },
                                data: series1
                            }
                        ]
                    };
                    var ctx_linechart = document.getElementById("BarChartAprovechamiento").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        //type: 'line',
                        data,
                        options: {

                            scales: {

                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 6

                                    }
                                },
                                y: {
                                    min: 0,
                                    max: 3,

                                    ticks: {
                                        // Include a dollar sign in the ticks
                                        beginAtZero: false,
                                        stepSize: 1,
                                        // callback: function (value, index, ticks) {
                                        //     return value + '%';
                                        //}
                                    }

                                }
                            },

                            plugins: {
                                legend: {
                                    display: false
                                },

                            }
                        }
                    })

                    //CABECERAS Y DATOS ADICIONALES
                    //var serieAUX = r.d[3];
                    document.getElementById("LblAnualAprovechamiento").innerText = series3[2];
                    document.getElementById("lblOBJAprov").innerText = " Objetivo: <" + series1[0] + " operario libre";

                    if (series3[0] == "INCUMPLIDO") {
                        document.getElementById("OEEAprovechamientoEmojiVerde").hidden = true;
                        document.getElementById("OEEAprovechamientoEmojiRojo").hidden = false;
                    }
                    else {
                        document.getElementById("OEEAprovechamientoEmojiVerde").hidden = false;
                        document.getElementById("OEEAprovechamientoEmojiRojo").hidden = true;
                    };
                    if (series3[1] == "INCUMPLIDO") {
                        document.getElementById("COLAprovechamiento").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-danger bg-opacity-25";
                    }
                    else {
                        document.getElementById("COLAprovechamiento").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-success bg-opacity-25";
                    };
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
        function CargarChartsAbsentismo() {
            $.ajax({
                type: "POST",
                url: "KPI_Dashboard_General_Tendencias.aspx/BARCHART_ABSENTISMO",
                data: null,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0].slice(0, 12); //Meses
                    var series1 = r.d[1].slice(0, 12);//Objetivo Absentismo
                    var series2 = r.d[2].slice(0, 12);//Absentismo Año Actual

                    var series3 = r.d[3];//Resultados Absentismo


                    //Gráfica OEE
                    var data = {
                        labels: etiquetas,
                        datasets: [
                            {
                                label: new Date().getFullYear() - 1,
                                pointStyle: 'triangle',
                                pointRadius: 6,
                                pointHoverRadius: 15,
                                borderColor: "rgb(2, 35, 245 )",
                                backgroundColor: "rgb(83, 106, 252)",
                                type: 'line',
                                cubicInterpolationMode: 'monotone',
                                tension: 0.4,
                                data: series2
                            },

                            {
                                label: "Objetivo",
                                pointStyle: false,

                                borderColor: "rgb(252, 3, 3)",
                                //backgroundColor: "rgba(252, 149, 149,0.2)",
                                backgroundColor: "rgba(231, 76, 60,0.2)",
                                borderWidth: 2,
                                type: 'line',
                                fill: { value: series3[4] },
                                data: series1
                            }
                        ]
                    };
                    var ctx_linechart = document.getElementById("BarChartAbsentismo").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        //type: 'line',
                        data,
                        options: {
                            scales: {
                                x: {
                                    ticks: {
                                        autoSkip: true,
                                        maxTicksLimit: 6
                                    }
                                },
                                y: {
                                    min: 0,
                                    max: series3[4],
                                    ticks: {
                                        // Include a dollar sign in the ticks
                                        beginAtZero: false,
                                        stepSize: 1,
                                        // callback: function (value, index, ticks) {
                                        //     return value + '%';
                                        //}
                                    }

                                }
                            },
                            plugins: {
                                legend: {
                                    display: false
                                },

                            }
                        }
                    })

                    //CABECERAS Y DATOS ADICIONALES
                    //var serieAUX = r.d[3];
                    document.getElementById("LblAnualAbsentismo").innerText = series3[2];
                    document.getElementById("lblOBJAbsentismo").innerText = " Objetivo: <" + series1[0] + "% de absentismo";

                    if (series3[0] == "INCUMPLIDO") {
                        document.getElementById("AbsentismoEmojiVerde").hidden = true;
                        document.getElementById("AbsentismoEmojiRojo").hidden = false;
                    }
                    else {
                        document.getElementById("AbsentismoEmojiVerde").hidden = false;
                        document.getElementById("AbsentismoEmojiRojo").hidden = true;
                    };
                    if (series3[1] == "INCUMPLIDO") {
                        document.getElementById("COLAbsentismo").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-danger bg-opacity-25";
                    }
                    else {
                        document.getElementById("COLAbsentismo").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-success bg-opacity-25";
                    };
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
        function playVideo() {
            var video = document.getElementById('ThermoVideo');
            video.play();
            video.onended = function () {
                document.getElementById('<%= btnEndVideo.ClientID %>').click();
            };
        }
        function TestSalida() {
            document.getElementById("VIDEO").hidden = false;
            document.getElementById("INDICADORES").hidden = true;
            playVideo();
           
        }
        function Cierre() {
            alert("go");
            document.getElementById("BTNRELD").click();
        }
    </script>
    <div class="container-fluid" id="VIDEO" hidden="true">
        
        <video id="ThermoVideo" width="100%" height="100%" muted>
         <%--<source src="http://facts4-srv/oftecnica/imagenes2/directorio/happydonia.mp4" type="video/mp4">--%>
            <source id="SRCVideo" runat="server" src="../SMARTH_docs/AUDIOVISUAL/happydonia.mp4" type="video/mp4">
            Tu navegador no soporta HTML5 video.
        </video>
    </div>
    <div class="container-fluid" id="INDICADORES">
        <div class="row m-2 mt-4 rounded rounded-2 border border-dark shadow" style="background-color: whitesmoke; text-align: center; background-color: orange">
            <h2 class="mt-1" style="color: whitesmoke; text-shadow: 1px 2px 1px black">INDICADORES Y TENDENCIAS DE PLANTA</h2>
        </div>
        <div class="row m-2 mt-4">
            <div class="col-lg-4" style="border-width: 5px; border-color: green" id="COLOEEGral">
                <div class="row border border border-top-0 border-start-0 border-end-0 border-dark rounded rounded-2 shadow">
                    <div class="col-lg-8">
                        <div class="ms-2 mt-2">
                            <strong style="font-size: x-large">OEE Gral. (% Rendimiento Planta)</strong>
                            <h6><i class="bi bi-graph-up-arrow ms-1" id="lblOBJOEEGral" style="font-size: large"></i></h6>
                        </div>
                    </div>
                    <div class="col-lg-4 border border-dark rounded rounded-2 bg-white" style="text-align: center">
                        <i class="bi bi-emoji-smile float-end  mt-2 ms-3" id="OEEGralEmojiVerde" style="color: green; font-size: 45px"></i>
                        <i class="bi bi-emoji-angry float-end  mt-2 ms-3" id="OEEGralEmojiRojo" hidden="true" style="color: red; font-size: 45px"></i>

                        <strong style="font-size: xx-large" id="LblAnualOEEGral">0.00</strong><br />
                        <h6>MEDIA ANUAL</h6>

                    </div>
                </div>
                <div class="row p-0 bg-white rounded rounded-2">
                    <canvas id="BARChartOEEPlanta" style="max-height: 275px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
                </div>
            </div>
            <div class="col-lg-4" style="border-width: 5px; border-color: green" id="COLOEECalidad">
                <div class="row border border border-top-0 border-start-0 border-end-0 border-dark rounded rounded-2 shadow">
                    <div class="col-lg-8">
                        <div class="ms-2 mt-2">
                            <strong style="font-size: x-large">OEE Calidad (% Piezas buenas)</strong>
                            <h6><i class="bi bi-graph-up-arrow ms-1" id="lblOBJOEECalidad" style="font-size: large"></i></h6>
                        </div>
                    </div>
                    <div class="col-lg-4 border border-dark rounded rounded-2 bg-white" style="text-align: center">
                        <i class="bi bi-emoji-smile float-end  mt-2 ms-3" id="OEECalidadEmojiVerde" style="color: green; font-size: 45px"></i>
                        <i class="bi bi-emoji-angry float-end  mt-2 ms-3" id="OEECalidadEmojiRojo" hidden="true" style="color: red; font-size: 45px"></i>

                        <strong style="font-size: xx-large" id="LblAnualOEECalidad">0.00</strong><br />
                        <h6>MEDIA ANUAL</h6>

                    </div>
                </div>
                <div class="row p-0 bg-white rounded rounded-2">
                    <canvas id="BARChartOEECalidad" style="max-height: 275px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
                </div>
            </div>
            <div class="col-lg-4" style="border-width: 5px; border-color: green" id="COLNoConformidades">
                <div class="row border border border-top-0 border-start-0 border-end-0 border-dark rounded rounded-2 shadow">
                    <div class="col-lg-8">
                        <div class="ms-2 mt-2">
                            <strong style="font-size: x-large">Nº Reclamaciones</strong>
                            <h6><i class="bi bi-graph-up-arrow ms-1" id="lblOBJOEENoConformidades" style="font-size: large"></i></h6>
                        </div>
                    </div>
                    <div class="col-lg-4 border border-dark rounded rounded-2 bg-white" style="text-align: center">
                        <i class="bi bi-emoji-smile float-end  mt-2 ms-3" id="OEENoConformidadesEmojiVerde" style="color: green; font-size: 45px"></i>
                        <i class="bi bi-emoji-angry float-end  mt-2 ms-3" id="OEENoConformidadesEmojiRojo" hidden="true" style="color: red; font-size: 45px"></i>

                        <strong style="font-size: xx-large" id="LblAnualNoConformidades">0.00</strong><br />
                        <h6>MEDIA ANUAL</h6>

                    </div>
                </div>
                <div class="row p-0 bg-white rounded rounded-2">
                    <canvas id="BarChartNoConformidades" style="max-height: 275px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
                </div>
            </div>
        </div>
        <div class="row m-2  mt-4">
            <div class="col-lg-4 border border-dark rounded rounded-2 shadow" style="border-width: 5px; border-color: green" id="COLPlanificacion" hidden="hidden">
                <div class="row border border border-top-0 border-start-0 border-end-0 border-dark rounded rounded-2 shadow">
                    <div class="col-lg-8">
                        <div class="ms-2 mt-2">
                            <strong style="font-size: x-large">% Cumplimiento de entregas</strong>
                            <h6><i class="bi bi-graph-down-arrow ms-1" id="lblOBJPlanificacion" style="font-size: large"></i></h6>
                        </div>
                    </div>
                    <div class="col-lg-4 border border-dark rounded rounded-2 bg-white" style="text-align: center">
                        <i class="bi bi-emoji-smile float-end  mt-2 ms-3" id="PlanificacionEmojiVerde" style="color: green; font-size: 45px"></i>
                        <i class="bi bi-emoji-angry float-end  mt-2 ms-3" id="PlanificacionEmojiRojo" hidden="true" style="color: red; font-size: 45px"></i>

                        <strong style="font-size: xx-large" id="LblAnualPlanificacion">0.00</strong><br />
                        <h6>MEDIA ANUAL</h6>

                    </div>
                </div>
                <div class="row p-0 bg-white rounded rounded-2">
                    <canvas id="BarChartPlanificacion" style="max-height: 275px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
                </div>
            </div>
            <div class="col-lg-2"></div>
            <div class="col-lg-4" style="border-width: 5px; border-color: green" id="COLAprovechamiento">
                <div class="row border border border-top-0 border-start-0 border-end-0 border-dark rounded rounded-2 shadow">
                    <div class="col-lg-8">
                        <div class="ms-2 mt-2">
                            <strong style="font-size: x-large">Aprovechamiento personal</strong>
                            <h6><i class="bi bi-graph-down-arrow ms-1" id="lblOBJAprov" style="font-size: large"></i></h6>
                        </div>
                    </div>
                    <div class="col-lg-4 border border-dark rounded rounded-2 bg-white" style="text-align: center">
                        <i class="bi bi-emoji-smile float-end  mt-2 ms-3" id="OEEAprovechamientoEmojiVerde" style="color: green; font-size: 45px"></i>
                        <i class="bi bi-emoji-angry float-end  mt-2 ms-3" id="OEEAprovechamientoEmojiRojo" hidden="true" style="color: red; font-size: 45px"></i>

                        <strong style="font-size: xx-large" id="LblAnualAprovechamiento">0.00</strong><br />
                        <h6>MEDIA ANUAL</h6>

                    </div>
                </div>
                <div class="row p-0 bg-white rounded rounded-2">
                    <canvas id="BarChartAprovechamiento" style="max-height: 275px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
                </div>
            </div>
            <div class="col-lg-4 border border-dark rounded rounded-2 shadow" style="border-width: 5px; border-color: green" id="COLAbsentismo">
                <div class="row border border border-top-0 border-start-0 border-end-0 border-dark rounded rounded-2 shadow">
                    <div class="col-lg-8">
                        <div class="ms-2 mt-2">
                            <strong style="font-size: x-large">% Absentismo</strong>
                            <h6><i class="bi bi-graph-down-arrow ms-1" id="lblOBJAbsentismo" style="font-size: large"></i></h6>
                        </div>
                    </div>
                    <div class="col-lg-4 border border-dark rounded rounded-2 bg-white" style="text-align: center">
                        <i class="bi bi-emoji-smile float-end  mt-2 ms-3" id="AbsentismoEmojiVerde" style="color: green; font-size: 45px"></i>
                        <i class="bi bi-emoji-angry float-end  mt-2 ms-3" id="AbsentismoEmojiRojo" hidden="true" style="color: red; font-size: 45px"></i>

                        <strong style="font-size: xx-large" id="LblAnualAbsentismo">0.00</strong><br />
                        <h6>MEDIA ANUAL</h6>

                    </div>
                </div>
                <div class="row p-0 bg-white rounded rounded-2">
                    <canvas id="BarChartAbsentismo" style="max-height: 275px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
                </div>
            </div>
        </div>
        <div class="row m-2 mt-4 rounded rounded-2 border border-dark shadow" style="background-color: whitesmoke">
            <div class="col-lg-1"></div>
            <div class="col-lg-4 mt-2">
                <table class="table border border-dark shadow shadow-sm bg-white" style="width: 100%">
                    <thead>
                        <tr style="background-color: orange">
                            <th scope="col" colspan="2" style="color: whitesmoke; text-align: center; text-shadow: 2px 1px 1px black">FONDO DE LA CABECERA</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row" class="bg-success opacity-25"></th>
                            <td style="font-size: large; font-weight: bold; vertical-align: middle">Objetivo mensual alcanzado</td>
                        </tr>
                        <tr>
                            <th scope="row" class="bg-danger opacity-25" style="width: 75px"></th>
                            <td style="font-size: large; font-weight: bold; vertical-align: middle">Objetivo mensual no alcanzado</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-lg-2"></div>
            <div class="col-lg-4 mt-2">
                <table class="table border border-dark shadow shadow-sm  bg-white">
                    <thead>
                        <tr style="background-color: orange">
                            <th scope="col" colspan="2" style="color: whitesmoke; text-align: center; text-shadow: 2px 1px 1px black">SIMBOLOGIA</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row">
                                <i class="bi bi-emoji-smile float-start ms-3" style="color: green; font-size: 30px"></i>
                            </th>
                            <td style="font-size: large; font-weight: bold; vertical-align: middle">Cumplimiento objetivo de media anual</td>
                        </tr>
                        <tr>
                            <th scope="row">
                                <i class="bi bi-emoji-angry float-start ms-3" style="color: red; font-size: 30px"></i>
                            </th>
                            <td style="font-size: large; font-weight: bold; vertical-align: middle">Incumplimiento objetivo de media anual</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-lg-1"></div>
            <div class="col-lg-3 mt-2" hidden="hidden">
                <table class="table border border-dark shadow shadow-sm bg-white" style="width: 100%">
                    <thead>
                        <tr style="background-color: orange">
                            <th scope="col" colspan="2" style="color: whitesmoke; text-align: center; text-shadow: 2px 1px 1px black">SERIES DE DATOS</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row" style="background-color: darkorange"></th>
                            <td style="font-size: large; font-weight: bold; vertical-align: middle" id="LabelAñoAnterior">---</td>
                        </tr>
                        <tr>
                            <th scope="row" style="width: 75px; background-color: blue"></th>
                            <td style="font-size: large; font-weight: bold; vertical-align: middle" id="LabelAñoActual">---</td>
                        </tr>

                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="row" hidden="hidden">
        <asp:Button ID="btnStartVideo" runat="server" Text="Iniciar Video" OnClick="btnStartVideo_Click" />
        <asp:Button ID="btnEndVideo" runat="server" Text="Terminar Acción" OnClick="btnEndVideo_Click" />
        <asp:Button ID="BTNRELD" runat="server" Text="BTNRELD" />
    </div>
</asp:Content>




