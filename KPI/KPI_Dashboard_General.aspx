<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_Dashboard_General.aspx.cs" Inherits="ThermoWeb.KPI.KPI_Dashboard_General" MasterPageFile="~/SMARTHLite.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores de mantenimiento</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores de mantenimiento
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CargarChartsOEE() {
            $.ajax({
                type: "POST",
                url: "KPI_Dashboard_General.aspx/BARCHART_OEECAL_OEE",
                data: null,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0]; //Meses
                    var series1 = r.d[1].slice(0, 12);//Objetivo OEE
                    var series2 = r.d[2].slice(0, 12);//OEE Año Anterior
                    var series3 = r.d[3].slice(0, 12);//OEE Año Actual
                    var series4 = r.d[4];//Objetivo OEE Calidad
                    var series5 = r.d[5];//OEE Calidad Año Anterior
                    var series6 = r.d[6];//OEE Calidad Año Actual
                    var series7 = r.d[7].slice(0, 12);//Resultados OEE General
                    var series8 = r.d[8];//Resultados OEE Calidad

                    //Gráfica OEE Gral
                    var data = {
                        labels: r.d[0].slice(0, 12),
                        datasets: [
                            {
                                label: new Date().getFullYear(),
                                pointStyle: 'triangle',
                                pointRadius: 5,
                                pointHoverRadius: 15,
                                borderColor: "rgb(2, 35, 245 )",
                                backgroundColor: "rgb(83, 106, 252)",
                                type: 'line',
                                cubicInterpolationMode: 'monotone',
                                tension: 0.4,
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                data: series3

                            },
                            {
                                label: new Date().getFullYear() - 1,
                                pointStyle: 'triangle',
                                pointRadius: 5,
                                pointHoverRadius: 15,
                                borderColor: "rgb(252, 119, 3)",
                                backgroundColor: "rgb(252, 155, 71)",
                                cubicInterpolationMode: 'monotone',
                                tension: 0.4,
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                type: 'line',
                                data: series2
                            },
                            {
                                label: "Objetivo",
                                pointStyle: false,
                                borderColor: "rgb(252, 3, 3)",
                                backgroundColor: "rgba(252, 149, 149,0.2)",
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
                    var serieAUX = r.d[3];
                    document.getElementById("LblAnualOEEGral").innerText = serieAUX[12];
                    document.getElementById("lblOBJOEEGral").innerText = " Objetivo: >" + series1[0] + "% aprovechado";

                    if (series7[0] == "INCUMPLIDO") {
                        document.getElementById("OEEGralEmojiVerde").hidden = true;
                        document.getElementById("OEEGralEmojiRojo").hidden = false;
                    }
                    else {
                        document.getElementById("OEEGralEmojiVerde").hidden = false;
                        document.getElementById("OEEGralEmojiRojo").hidden = true;
                    };
                    if (series7[1] == "INCUMPLIDO") {
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
                                borderColor: "rgb(3, 1, 0)",
                                backgroundColor: "rgb(255, 160, 77)",
                                borderWidth: 2,

                                borderRadius: 2,
                                type: 'bar',
                                data: series5
                            },
                            {
                                label: new Date().getFullYear(),
                                //pointStyle: 'triangle',
                                borderColor: "rgb(3, 1, 0)",
                                backgroundColor: "rgb(80, 101, 235)",
                                borderWidth: 2,
                                borderRadius: 2,
                                type: 'bar',
                                data: series6
                            },

                            {
                                pointStyle: false,
                                borderColor: "rgb(252, 3, 3)",
                                backgroundColor: "rgba(252, 149, 149,0.3)",
                                borderWidth: 2,
                                type: 'line',
                                fill: { value: 0 },

                                data: series4
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

                                    min: 95,
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

                            }
                        }
                    })

                    //CABECERAS Y DATOS ADICIONALES OEECalidad
                    var serieAUX2 = r.d[6];
                    document.getElementById("LblAnualOEECalidad").innerText = serieAUX2[12];
                    document.getElementById("lblOBJOEECalidad").innerText = " Objetivo: >" + series4[0] + "% piezas conformes";
                    if (series8[0] == "INCUMPLIDO") {
                        document.getElementById("OEECalidadEmojiVerde").hidden = true;
                        document.getElementById("OEECalidadEmojiRojo").hidden = false;
                    }
                    else {
                        document.getElementById("OEECalidadEmojiVerde").hidden = false;
                        document.getElementById("OEECalidadEmojiRojo").hidden = true;
                    };
                    if (series8[1] == "INCUMPLIDO") {
                        document.getElementById("COLOEECalidad").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-danger bg-opacity-25";
                    }
                    else {
                        document.getElementById("COLOEECalidad").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-success bg-opacity-25";
                    };

                    document.getElementById("LabelAñoAnterior").innerText = "Año " + (new Date().getFullYear() - 1);
                    document.getElementById("LabelAñoActual").innerText = "Año " + new Date().getFullYear();
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
                url: "KPI_Dashboard_General.aspx/BARCHART_NOCONFORMIDADES",
                data: null,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0]; //Meses
                    var series1 = r.d[1];//Objetivo NoConformidades
                    var series2 = r.d[2];//OEE NoConformidades Año Anterior
                    var series3 = r.d[3];//OEE NoConformidades Año Actual
                    var series4 = r.d[4];//Resultados NoConformidades


                    //Gráfica OEE
                    var data = {
                        labels: r.d[0],
                        datasets: [

                            {
                                label: new Date().getFullYear() - 1,
                                //pointStyle: 'triangle',
                                borderColor: "rgb(3, 1, 0)",
                                backgroundColor: "rgb(255, 160, 77)",
                                borderWidth: 2,
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                borderRadius: 2,
                                type: 'bar',
                                data: series2
                            },
                            {
                                label: new Date().getFullYear(),
                                //pointStyle: 'triangle',
                                borderColor: "rgb(3, 1, 0)",
                                backgroundColor: "rgb(80, 101, 235)",
                                borderWidth: 2,
                                borderRadius: 2,
                                type: 'bar',
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                data: series3
                            },
                            {
                                label: "Objetivo",
                                pointStyle: false,

                                borderColor: "rgb(252, 3, 3)",
                                backgroundColor: "rgba(252, 149, 149,0.3)",
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
                                    max: 11,

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
                    var serieAUX = r.d[3];
                    document.getElementById("LblAnualNoConformidades").innerText = serieAUX[12];
                    document.getElementById("lblOBJOEENoConformidades").innerText = " Objetivo: <" + series1[0] + " no conformidades";
                    if (series4[0] == "INCUMPLIDO") {
                        document.getElementById("OEENoConformidadesEmojiVerde").hidden = true;
                        document.getElementById("OEENoConformidadesEmojiRojo").hidden = false;
                             }
                    else {
                        document.getElementById("OEENoConformidadesEmojiVerde").hidden = false;
                        document.getElementById("OEENoConformidadesEmojiRojo").hidden = true;
                    };
                    if (series4[1] == "INCUMPLIDO") {
                        document.getElementById("COLOEECalidad").className = "col-lg-4 border border-dark rounded rounded-2  shadow bg-danger bg-opacity-25";

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
                url: "KPI_Dashboard_General.aspx/BARCHART_APROVECHAMIENTO",
                data: null,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0].slice(0, 12); //Meses
                    var series1 = r.d[1].slice(0, 12);//Objetivo Aprovechamiento
                    var series2 = r.d[2].slice(0, 12);//Aprovechamiento Año Anterior
                    var series3 = r.d[3].slice(0, 12);//Aprovechamiento Año Actual
                    var series4 = r.d[4];//Resultados Aprovechamiento


                    //Gráfica OEE
                    var data = {
                        labels: etiquetas,
                        datasets: [

                            {
                                label: new Date().getFullYear() - 1,
                                //pointStyle: 'triangle',
                                borderColor: "rgb(3, 1, 0)",
                                backgroundColor: "rgb(255, 160, 77)",
                                borderWidth: 2,
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                borderRadius: 2,
                                type: 'bar',
                                data: series2
                            },
                            {
                                label: new Date().getFullYear(),
                                //pointStyle: 'triangle',
                                borderColor: "rgb(3, 1, 0)",
                                backgroundColor: "rgb(80, 101, 235)",
                                borderWidth: 2,
                                borderRadius: 2,
                                type: 'bar',
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                data: series3
                            },
                            {
                                label: "Objetivo",
                                pointStyle: false,

                                borderColor: "rgb(252, 3, 3)",
                                backgroundColor: "rgba(252, 149, 149,0.3)",
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
                    var serieAUX = r.d[3];
                    document.getElementById("LblAnualAprovechamiento").innerText = serieAUX[12];
                    document.getElementById("lblOBJAprov").innerText = " Objetivo: <" + series1[0] + " operario libre";

                    if (series4[0] == "INCUMPLIDO") {
                        document.getElementById("OEEAprovechamientoEmojiVerde").hidden = true;
                        document.getElementById("OEEAprovechamientoEmojiRojo").hidden = false;
                    }
                    else {
                        document.getElementById("OEEAprovechamientoEmojiVerde").hidden = false;
                        document.getElementById("OEEAprovechamientoEmojiRojo").hidden = true;
                    };
                    if (series4[1] == "INCUMPLIDO") {
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
                url: "KPI_Dashboard_General.aspx/BARCHART_ABSENTISMO",
                data: null,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0].slice(0, 12); //Meses
                    var series1 = r.d[1].slice(0, 12);//Objetivo Absentismo
                    var series2 = r.d[2].slice(0, 12);//Absentismo Año Anterior
                    var series3 = r.d[3].slice(0, 12);//Absentismo Año Actual
                    var series4 = r.d[4];//Resultados Absentismo


                    //Gráfica OEE
                    var data = {
                        labels: etiquetas,
                        datasets: [

                            {
                                label: new Date().getFullYear() - 1,
                                //pointStyle: 'triangle',
                                borderColor: "rgb(3, 1, 0)",
                                backgroundColor: "rgb(255, 160, 77)",
                                borderWidth: 2,
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                borderRadius: 2,
                                type: 'bar',
                                data: series2
                            },
                            {
                                label: new Date().getFullYear(),
                                //pointStyle: 'triangle',
                                borderColor: "rgb(3, 1, 0)",
                                backgroundColor: "rgb(80, 101, 235)",
                                borderWidth: 2,
                                borderRadius: 2,
                                type: 'bar',
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                data: series3
                            },
                            {
                                label: "Objetivo",
                                pointStyle: false,

                                borderColor: "rgb(252, 3, 3)",
                                backgroundColor: "rgba(252, 149, 149,0.3)",
                                borderWidth: 2,
                                type: 'line',
                                fill: { value: 9 },
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
                                    max: 9,
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
                    var serieAUX = r.d[3];
                    document.getElementById("LblAnualAbsentismo").innerText = serieAUX[12];
                    document.getElementById("lblOBJAbsentismo").innerText = " Objetivo: <" + series1[0] + " operario libre";

                    if (series4[0] == "INCUMPLIDO") {
                        document.getElementById("AbsentismoEmojiVerde").hidden = true;
                        document.getElementById("AbsentismoEmojiRojo").hidden = false;
                    }
                    else {
                        document.getElementById("AbsentismoEmojiVerde").hidden = false;
                        document.getElementById("AbsentismoEmojiRojo").hidden = true;
                    };
                    if (series4[1] == "INCUMPLIDO") {
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
        /*
        function PIETOPHorasAñoMaquina(año) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/PIE_Ranking_Maquinas_HORASAÑO",
                data: "{año: '" + año + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//VALOR

                    //Correctivo
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",

                                //pointStyle: 'triangle',
                                //borderColor: "rgb(11, 81, 255)",
                                //backgroundColor: "rgb(169, 204, 227 )",
                                data: series1
                            },

                        ]
                    };
                    var ctx_linechart = document.getElementById("PieChartMantenimientoHORASAÑO").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'pie',
                        data,
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'bottom'
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
        function PIETOPHorasAñoMaquinaMTBF(año) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/PIE_Ranking_Maquinas_HORASAÑOMTBF",
                data: "{año: '" + año + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//VALOR

                    //Correctivo
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",

                                //pointStyle: 'triangle',
                                //borderColor: "rgb(11, 81, 255)",
                                //backgroundColor: "rgb(169, 204, 227 )",
                                data: series1
                            },

                        ]
                    };
                    var ctx_linechart = document.getElementById("PieChartMantenimientoHORASAÑOMTBF").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'pie',
                        data,
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'bottom'
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
        function PIETOPHorasMesMaquina(año, mes) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/PIE_Ranking_Maquinas_HORASMES",
                data: "{año: '" + año + "', mes: '" + mes + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//VALOR


                    //Correctivo
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",

                                //pointStyle: 'triangle',
                                //borderColor: "rgb(11, 81, 255)",
                                //backgroundColor: "rgb(169, 204, 227 )",
                                data: series1
                            },

                        ]
                    };
                    var ctx_linechart = document.getElementById("PieChartMantenimientoHORASMES").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'pie',
                        data,
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'bottom'
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
        function PIETOPHorasMesMaquinaMTBF(año, mes) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/PIE_Ranking_Maquinas_HORASMESMTBF",
                data: "{año: '" + año + "', mes: '" + mes + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//VALOR


                    //Correctivo
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",

                                //pointStyle: 'triangle',
                                //borderColor: "rgb(11, 81, 255)",
                                //backgroundColor: "rgb(169, 204, 227 )",
                                data: series1
                            },

                        ]
                    };
                    var ctx_linechart = document.getElementById("PieChartMantenimientoHORASMESMTBF").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'pie',
                        data,
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'bottom'
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
        function CargarChartsMoldes(año, mes) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/Resultados_Completos_Mantenimiento_MOL",
                data: "{año: '" + año + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//VALOR CORRECTIVO
                    var series2 = r.d[2];//LIMITE PREVENTIVO
                    var series3 = r.d[3];//VALOR CORRECTIVO
                    var series4 = r.d[4];//LIMITE PREVENTIVO

                    //Correctivo
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",
                                pointStyle: 'triangle',
                                borderColor: "rgb(11, 81, 255)",
                                backgroundColor: "rgb(169, 204, 227 )",
                                data: series1
                            },
                            {
                                label: "Objetivo",
                                pointStyle: 'triangle',
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 201, 205 )",
                                //backgroundColor: 'rgba(0, 0, 255, 0.2)',
                                data: series2
                            }

                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartMantenimientoCorrectivoMolde").getContext('2d');
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
                                        stepSize: 0.5,
                                        callback: function (value, index, ticks) {
                                            return value + '%';
                                        }
                                    }

                                }
                            },
                            plugins: {
                                fillAboveLine: {
                                    fillColor: 'rgba(255, 201, 205)'  // Color de fondo del área por encima del valor deseado
                                }
                            }
                        }
                    })
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",
                                pointStyle: 'triangle',
                                borderColor: "rgb(11, 81, 255)",
                                backgroundColor: "rgb(169, 204, 227 )",
                                //fill: '+1',
                                data: series3
                            },
                            {
                                label: "Objetivo",
                                pointStyle: 'triangle',
                                borderColor: "rgb(255, 33, 33 )",
                                backgroundColor: "rgb(255, 201, 205 )",
                                //fill: '-1',
                                //fill:true,
                                data: series4
                            }

                        ]
                    };
                    var ctx_linechart = document.getElementById("LineChartMantenimientoPreventivoMolde").getContext('2d');
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
                                        stepSize: 0.5,
                                        callback: function (value, index, ticks) {
                                            return value + '%';
                                        }
                                    }

                                }
                            }

                        }
                    })

                    document.getElementById("LblPOPMantCorMolde").innerText = "Mantenimiento correctivo de molde (" + año + ")";
                    document.getElementById("LblPOPMantPrevMolde").innerText = "Mantenimiento preventivo de molde (" + año + ")";
                    document.getElementById("LblPOPMantCorOBJMolde").innerText = "Objetivo: ≤" + series2[0] + "%";
                    document.getElementById("LblPOPMantPrevOBJMolde").innerText = "Objetivo: ≥" + series4[0] + "%";

                    PIETOPHorasMesMolde(año, mes);
                    PIETOPHorasAñoMolde(año);
                    PIETOPHorasMesMoldeMTBF(año, mes);
                    PIETOPHorasAñoMoldeMTBF(año);

                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
        function PIETOPHorasMesMolde(año, mes) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/PIE_Ranking_Molde_HORASMES",
                data: "{año: '" + año + "', mes: '" + mes + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//VALOR


                    //Correctivo
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",

                                //pointStyle: 'triangle',
                                //borderColor: "rgb(11, 81, 255)",
                                //backgroundColor: "rgb(169, 204, 227 )",
                                data: series1
                            },

                        ]
                    };
                    var ctx_linechart = document.getElementById("PieChartMantenimientoHORASMESMolde").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'pie',
                        data,
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'bottom'
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
        function PIETOPHorasAñoMolde(año) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/PIE_Ranking_Molde_HORASAÑO",
                data: "{año: '" + año + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//VALOR

                    //Correctivo
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",

                                //pointStyle: 'triangle',
                                //borderColor: "rgb(11, 81, 255)",
                                //backgroundColor: "rgb(169, 204, 227 )",
                                data: series1
                            },

                        ]
                    };
                    var ctx_linechart = document.getElementById("PieChartMantenimientoHORASAÑOMolde").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'pie',
                        data,
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'bottom'
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
        function PIETOPHorasMesMoldeMTBF(año, mes) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/PIE_Ranking_Molde_HORASMESMTBF",
                data: "{año: '" + año + "', mes: '" + mes + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//VALOR


                    //Correctivo
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",

                                //pointStyle: 'triangle',
                                //borderColor: "rgb(11, 81, 255)",
                                //backgroundColor: "rgb(169, 204, 227 )",
                                data: series1
                            },

                        ]
                    };
                    var ctx_linechart = document.getElementById("PieChartMantenimientoHORASMESMoldeMTBF").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'pie',
                        data,
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'bottom'
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
        function PIETOPHorasAñoMoldeMTBF(año) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/PIE_Ranking_Molde_HORASAÑOMTBF",
                data: "{año: '" + año + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var etiquetas = r.d[0];
                    var series1 = r.d[1];//VALOR

                    //Correctivo
                    var data = {
                        labels: r.d[0],
                        datasets: [
                            {
                                label: "Resultado",

                                //pointStyle: 'triangle',
                                //borderColor: "rgb(11, 81, 255)",
                                //backgroundColor: "rgb(169, 204, 227 )",
                                data: series1
                            },

                        ]
                    };
                    var ctx_linechart = document.getElementById("PieChartMantenimientoHORASAÑOMoldeMTBF").getContext('2d');
                    var myLineChart = new Chart(ctx_linechart, {
                        type: 'pie',
                        data,
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'bottom'
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
        */
    </script>
    <div class="container-fluid">
        <div class="row m-2 rounded rounded-2 border border-dark shadow" style="background-color:whitesmoke; text-align:center; background-color:orange">
            <h2 class="mt-1" style="color: whitesmoke; text-shadow: 1px 2px 1px black">TABLERO DE INDICADORES DE PLANTA</h2>
        </div>
        <div class="row m-2 mt-3">
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
                    <canvas id="BARChartOEEPlanta" style="max-height: 225px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
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
                    <canvas id="BARChartOEECalidad" style="max-height: 225px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
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
                    <canvas id="BarChartNoConformidades" style="max-height: 225px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
                </div>
            </div>
        </div>
        <div class="row m-2  mt-3">
           <div class="col-lg-4 border border-dark rounded rounded-2 shadow" style="border-width: 5px; border-color: green" id="COLPlanificacion">
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
                    <canvas id="BarChartPlanificacion" style="max-height: 225px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
                </div>
            </div>
            <div class="col-lg-4" style="border-width: 5px; border-color: green" id="COLAprovechamiento">
                <div class="row border border border-top-0 border-start-0 border-end-0 border-dark rounded rounded-2 shadow">
                    <div class="col-lg-8">
                        <div class="ms-2 mt-2">
                            <strong style="font-size: x-large">Desaprovechamiento personal</strong>
                            <h6><i class="bi bi-graph-down-arrow ms-1" id="lblOBJAprov" style="font-size: large"></i></h6>
                        </div>
                    </div>
                    <div class="col-lg-4 border border-dark rounded rounded-2 bg-white" style="text-align: center">
                        <i class="bi bi-emoji-smile float-end  mt-2 ms-3" id="OEEAprovechamientoEmojiVerde" style="color: green; font-size: 45px"></i>
                        <i class="bi bi-emoji-angry float-end  mt-2 ms-3" id="OEEAprovechamientoEmojiRojo" hidden="true" style="color: red; font-size: 45px"></i>

                        <strong style="font-size: xx-large" id="LblAnualAprovechamiento">0.54</strong><br />
                        <h6>MEDIA ANUAL</h6>

                    </div>
                </div>
                <div class="row p-0 bg-white rounded rounded-2">
                    <canvas id="BarChartAprovechamiento" style="max-height: 225px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
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
                    <canvas id="BarChartAbsentismo" style="max-height: 225px; background: url(LOGOFONDOTHERMOV2.png) right top no-repeat"></canvas>
                </div>
            </div>
        </div>
        <div class="row m-2 rounded rounded-2 border border-dark shadow" style="background-color:whitesmoke">
           <div class="col-lg-3 mt-2">
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
            <div class="col-lg-1"></div>
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
            <div class="col-lg-3 mt-2">
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

</asp:Content>




