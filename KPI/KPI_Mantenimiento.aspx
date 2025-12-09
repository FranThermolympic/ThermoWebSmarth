<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_Mantenimiento.aspx.cs" Inherits="ThermoWeb.KPI.KPI_Mantenimiento" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores de mantenimiento</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores de mantenimiento
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Partes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMaquinas.aspx">Crear un parte de máquina</a></li>
                <li><a class="dropdown-item" href="http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx">Crear un parte de molde</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación de referencia</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Pendientes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMaquina.aspx">Estado reparaciones máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMoldes.aspx">Estado reparaciones moldes</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="#">Gestionar preventivos máquina</a></li>

            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Informes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMaquinas.aspx">Informe de máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMoldes.aspx">Informe de moldes</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformePerifericos.aspx.aspx">Informe de periféricos</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CargarCharts(año, mes) {
            $.ajax({
                type: "POST",
                url: "KPI_Mantenimiento.aspx/Resultados_Completos_Mantenimiento_MAQ",
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
                    var ctx_linechart = document.getElementById("LineChartMantenimientoCorrectivo").getContext('2d');
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
                    var ctx_linechart = document.getElementById("LineChartMantenimientoPreventivo").getContext('2d');
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

                    document.getElementById("LblPOPMantCor").innerText = "Mantenimiento correctivo de máquina (" + año + ")";
                    document.getElementById("LblPOPMantPrev").innerText = "Mantenimiento preventivo de máquina (" + año + ")";
                    document.getElementById("LblPOPMantCorOBJ").innerText = "Objetivo: ≤" + series2[0] + "%";
                    document.getElementById("LblPOPMantPrevOBJ").innerText = "Objetivo: ≥" + series4[0] + "%";

                    PIETOPHorasMesMaquina(año, mes);
                    PIETOPHorasMesMaquinaMTBF(año, mes);
                    PIETOPHorasAñoMaquina(año);
                    PIETOPHorasAñoMaquinaMTBF(año);
                    CargarChartsMoldes(año, mes);

                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
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
        
    </script>
    <div class="d-flex align-items-start ">
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
                    <div class="row mt-2">
                        <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab" role="tablist">
                            <li class="nav-item " role="presentation">
                                <button class="nav-link shadow active " id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Resultados de máquinas</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link shadow " id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Resultados de moldes</button>
                            </li>
                        </ul>
                        <div class="tab-content shadow" id="pills-tabContent">
                            <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                                <div class="row">
                                    <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-currency-exchange text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICosteTotalMAQ" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">En reparaciones</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesCosteTotal">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasMAQCORR" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de correctivo</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesHoras">Ir a planes de acción</h6>
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
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasMAQPREV" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de preventivo</h6>
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
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIPartesMAQ" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Partes creados</h6>
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
                                    <div class="col-lg-6 border border-secondary rounded rounded-2 shadow">
                                        <h5 id="LblPOPMantCor">&nbsp Mantenimiento correctivo</h5>
                                        <h6 id="LblPOPMantCorOBJ" class="float-end">&nbspObjetivo: <0.7%</h6>
                                        <div>
                                            <canvas id="LineChartMantenimientoCorrectivo" width="500" height="200"></canvas>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 border border-secondary rounded rounded-2 shadow">
                                        <h5 id="LblPOPMantPrev">&nbsp Mantenimiento preventivo</h5>
                                        <h6 id="LblPOPMantPrevOBJ" class="float-end">&nbspObjetivo: <0.7%</h6>
                                        <div>
                                            <canvas id="LineChartMantenimientoPreventivo" width="500" height="200"></canvas>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h4>Detalles mantenimiento máquinas</h4>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridResultadosMaq" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridView_DataBoundHist"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Corr. previsto" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("ESTIMADASMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Producción" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasProductivas" runat="server" Text='<%#Eval("HorasProduciendo") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Correctivas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCorrectivasMaq" runat="server" Text='<%#Eval("REALESMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="%" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPorCorrectivo" runat="server" Text='<%#Eval("PORCCORRECTIVO") + "%" %>' />
                                                            <asp:HiddenField ID="lblKPICorrectivo" runat="server" Value='<%#Eval("KPICOR")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Prev. previsto" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPreventivosMaqPrev" runat="server" Text='<%#Eval("ESTIMADASPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Preventivas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPreventivasMaq" runat="server" Text='<%#Eval("REALESPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="%" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPorPreventivo" runat="server" Text='<%#Eval("PORCPREVENTIVO") + "%" %>' />
                                                            <asp:HiddenField ID="lblKPIPreventivo" runat="server" Value='<%#Eval("KPIPREV")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOS","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOS","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALES","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <a class="btn btn-primary btn-sm ms-3" data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">Más detalles
                                        </a>
                                        <div class="collapse" id="collapseExample">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <h5>Correctivos (Máquinas)</h5>
                                                    <div class="table-responsive" style="width: 100%">
                                                        <asp:GridView ID="GridDetallesMaqCORR" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                            Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                            EmptyDataText="No hay scrap declarado para mostrar.">
                                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#ffffcc" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Partes" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartes" runat="server" Text='<%#Eval("PARTESCORR")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Horas correctivo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("HorasProduciendo") + " horas" %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Repuestos">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOSCORR","{0:c}") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Coste Op.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOSCORR","{0:c}") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALESCORR","{0:c}") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <h5>Preventivos (Máquinas)</h5>
                                                    <div class="table-responsive" style="width: 100%">
                                                        <asp:GridView ID="GridDetallesMaqPREV" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                            Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 " AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                            EmptyDataText="No hay scrap declarado para mostrar.">
                                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#ffffcc" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Partes" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartes" runat="server" Text='<%#Eval("PARTESPREV")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Horas preventivo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("REALESPREV") + " horas" %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Repuestos">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOSPREV","{0:c}") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Coste Op.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOSPREV","{0:c}") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALESPREV","{0:c}") %>' />
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
                                <div class="row">
                                </div>
                                <div class="row mt-5" style="background-color: orange">
                                    <label id="lblAñoMaquina" runat="server" style="width: 100%; text-align: center; font-weight: bold; font-size: x-large"></label>
                                </div>
                                <ul class="nav nav-pills nav-fill mb-2" id="pills-tab_maquina" role="tablist">
                                    <li class="nav-item " role="presentation">
                                        <button class="nav-link shadow active " id="pills-home-tab_maquina" data-bs-toggle="pill" data-bs-target="#pills-home_maquina" type="button" role="tab" aria-controls="pills-home_maquina" aria-selected="true">Resultados del mes</button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link shadow " id="pills-profile-tab_maquina" data-bs-toggle="pill" data-bs-target="#pills-profile_maquina" type="button" role="tab" aria-controls="pills-profile_maquina" aria-selected="false">Resultados del año</button>
                                    </li>
                                </ul>
                                <div class="tab-content shadow" id="pills-tabContent_maquina">
                                    <div class="tab-pane fade show active rounded" id="pills-home_maquina" role="tabpanel" aria-labelledby="pills-home-tab" style="background-color: #eeeeee">

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
                                            <div class="col-lg-6">
                                                <strong style="font-size:x-large">TOP10 - MTTR Máquina</strong>
                                                <h6>Tiempo medio de reparación (horas)</h6>

                                                <div>
                                                    <canvas id="PieChartMantenimientoHORASMES" style="max-height: 250px"></canvas>
                                                </div>

                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMAQxParte" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMAQ" CommandArgument='<%#Eval("IdMaquinaCHAR")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" Visible="false" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxP" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MES" Visible="false" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMES_MxP" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Máquina" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxP" runat="server" Text='<%#Eval("Maquina")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reparación">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxH" runat="server" Font-Bold="true" Text='<%#Eval("HORAS") %>' /><label class="ms-2">horas</label><br />
                                                                    <asp:Label ID="lblPARTES_MxP" runat="server" Font-Size="small" Font-Italic="true" Text='<%#"para <strong>" + Eval("PARTES") + "</strong> partes." %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MTTR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_Media" runat="server" Font-Bold="true" Text='<%#Eval("MTTR") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>


                                            </div>
                                            <div class="col-lg-6">
                                                <strong style="font-size:x-large">TOP10 - MTBF Máquina</strong>
                                                <h6>Tiempo medio entre fallos (horas)</h6>
                                                <div>
                                                    <canvas id="PieChartMantenimientoHORASMESMTBF" style="max-height: 250px"></canvas>
                                                </div>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMAQxParteMTBF" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="white" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMAQ" CommandArgument='<%#Eval("IdMaquinaCHAR")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" Visible="false" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxPAÑO" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Máquina" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxP" runat="server" Text='<%#Eval("Maquina")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Produciendo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxH" runat="server" Font-Bold="true" Text='<%#Eval("HorasProduciendo") %>' /><label class="ms-2">horas prod.</label><br />
                                                                    <asp:Label ID="lblPARTES_MxP" runat="server" Font-Size="small" Font-Italic="true" Text='<%#"con <strong>" + Eval("PARTES") + "</strong> partes." %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MTBF">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_Media" runat="server" Font-Bold="true" Text='<%#Eval("MTBF") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <h4>Apertura de partes de máquina</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMAQ" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Persona">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("Nombre")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("PARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade rounded" id="pills-profile_maquina" role="tabpanel" aria-labelledby="pills-profile-tab" style="background-color: #eeeeee">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <strong style="font-size:x-large">TOP10 - MTTR Máquina</strong>
                                                <h6>Tiempo medio de reparación (horas)</h6>
                                                <div>
                                                    <canvas id="PieChartMantenimientoHORASAÑO" style="max-height: 250px"></canvas>
                                                </div>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMAQxParteAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="white" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMAQ" CommandArgument='<%#Eval("IdMaquinaCHAR")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" Visible="false" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxPAÑO" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Máquina" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxP" runat="server" Text='<%#Eval("Maquina")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reparación">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxH" runat="server" Font-Bold="true" Text='<%#Eval("HORAS") %>' /><label class="ms-2">horas</label><br />
                                                                    <asp:Label ID="lblPARTES_MxP" runat="server" Font-Size="small" Font-Italic="true" Text='<%#"para <strong>" + Eval("PARTES") + "</strong> partes." %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MTTR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_Media" runat="server" Font-Bold="true" Text='<%#Eval("MTTR") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <strong style="font-size:x-large">TOP10 - MTFB Máquina</strong>
                                                <h6>Tiempo medio entre fallos (horas)</h6>
                                                <div>
                                                    <canvas id="PieChartMantenimientoHORASAÑOMTBF" style="max-height: 250px"></canvas>
                                                </div>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMAQxParteAÑOMTBF" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="white" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMAQ" CommandArgument='<%#Eval("IdMaquinaCHAR")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" Visible="false" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxPAÑO" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Máquina" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxP" runat="server" Text='<%#Eval("Maquina")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Produciendo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxH" runat="server" Font-Bold="true" Text='<%#Eval("HorasProduciendo") %>' /><label class="ms-2">horas</label><br />
                                                                    <asp:Label ID="lblPARTES_MxP" runat="server" Font-Size="small" Font-Italic="true" Text='<%#"para <strong>" + Eval("PARTES") + "</strong> partes." %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MTBF">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_Media" runat="server" Font-Bold="true" Text='<%#Eval("MTBF") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <h4>Apertura de partes de máquina</h4>
                                                <div class="table-responsive" style="width: 98.5%">
                                                    <asp:GridView ID="GridRankingMAQAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <RowStyle BackColor="white" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Persona">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("Nombre")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("PARTES") %>' />
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
                            <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                                <div class="row">
                                    <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-currency-exchange text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICosteTotalMOL" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">En reparaciones</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H1">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasMOLCORR" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de correctivo</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H2">Ir a planes de acción</h6>
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
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasMOLPREV" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de preventivo</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H3">Ir a planes de acción</h6>
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
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIPartesMOL" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Partes creados</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H4">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-6 border border-secondary rounded rounded-2 shadow">
                                        <h5 id="LblPOPMantCorMolde">&nbsp Mantenimiento correctivo</h5>
                                        <h6 id="LblPOPMantCorOBJMolde" class="float-end">&nbspObjetivo: <0.7%</h6>
                                        <div>
                                            <canvas id="LineChartMantenimientoCorrectivoMolde" width="500" height="200"></canvas>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 border border-secondary rounded rounded-2 shadow">
                                        <h5 id="LblPOPMantPrevMolde">&nbsp Mantenimiento preventivo</h5>
                                        <h6 id="LblPOPMantPrevOBJMolde" class="float-end">&nbspObjetivo: <0.7%</h6>
                                        <div>
                                            <canvas id="LineChartMantenimientoPreventivoMolde" width="500" height="200"></canvas>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h4>Detalles mantenimiento (Moldes)</h4>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridResultadosMolde" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridView_DataBoundHist"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Corr. previsto" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("ESTIMADASMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Producción" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasProductivas" runat="server" Text='<%#Eval("HorasProduciendo") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Correctivas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCorrectivasMaq" runat="server" Text='<%#Eval("REALESMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="%" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPorCorrectivo" runat="server" Text='<%#Eval("PORCCORRECTIVO") + "%" %>' />
                                                            <asp:HiddenField ID="lblKPICorrectivo" runat="server" Value='<%#Eval("KPICOR")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Prev. previsto" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPreventivosMaqPrev" runat="server" Text='<%#Eval("ESTIMADASPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Preventivas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPreventivasMaq" runat="server" Text='<%#Eval("REALESPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="%" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPorPreventivo" runat="server" Text='<%#Eval("PORCPREVENTIVO") + "%" %>' />
                                                            <asp:HiddenField ID="lblKPIPreventivo" runat="server" Value='<%#Eval("KPIPREV")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOS","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOS","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALES","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <a class="btn btn-primary btn-sm ms-3" data-bs-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample2">Más detalles
                                </a>
                                <div class="collapse" id="collapseExample2">
                                     <div class="row">
                                    <div class="col-lg-6">
                                        <h5>Correctivos (Moldes)</h5>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridDetallesMolCORR" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Partes" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartes" runat="server" Text='<%#Eval("PARTESCORR")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Horas correctivo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("REALESMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOSCORR","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOSCORR","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALESCORR","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <h5>Preventivos (Moldes)</h5>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridDetallesMolPREV" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Partes" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartes" runat="server" Text='<%#Eval("PARTESPREV")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Horas preventivo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("REALESPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOSPREV","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOSPREV","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALESPREV","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                </div>
                                <div class="row mt-5" style="background-color: orange">
                                    <label id="lblAñoMolde" runat="server" style="width: 100%; text-align: center; font-weight: bold; font-size: x-large"></label>
                                </div>
                                <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab_molde" role="tablist">
                                    <li class="nav-item " role="presentation">
                                        <button class="nav-link shadow active " id="pills-home-tab_molde" data-bs-toggle="pill" data-bs-target="#pills-home_molde" type="button" role="tab" aria-controls="pills-home_molde" aria-selected="true">Resultados del mes</button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link shadow " id="pills-profile-tab_molde" data-bs-toggle="pill" data-bs-target="#pills-profile_molde" type="button" role="tab" aria-controls="pills-profile_molde" aria-selected="false">Resultados del año</button>
                                    </li>
                                </ul>
                                <div class="tab-content shadow" id="pills-tabContent_molde">
                                    <div class="tab-pane fade show active rounded" id="pills-home_molde" role="tabpanel" aria-labelledby="pills-home-tab" style="background-color: #eeeeee">
                                        <div class="row">
                                            <div class="col-lg-2 mt-2 mb-2">
                                                <h6>Mes:</h6>
                                                <asp:DropDownList ID="SELECMES2" runat="server" CssClass="form-select shadow-sm mb-2" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="cargar_tablas">
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
                                            <div class="col-lg-6">
                                                <strong style="font-size:x-large">TOP10 - MTTR Molde</strong>
                                                <h6>Tiempo medio de reparación (horas)</h6>
                                                <div>
                                                    <canvas id="PieChartMantenimientoHORASMESMolde" style="max-height: 250px"></canvas>
                                                </div>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOLxParte" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMOL" CommandArgument='<%#Eval("Molde")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" Visible="false" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxP" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MES" Visible="false" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMES_MxP" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Máquina" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxP" runat="server" Text='<%#Eval("Molde")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reparación">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxH" runat="server" Font-Bold="true" Text='<%#Eval("HORAS") %>' /><label class="ms-2">horas</label><br />
                                                                    <asp:Label ID="lblPARTES_MxP" runat="server" Font-Size="small" Font-Italic="true" Text='<%#"para <strong>" + Eval("PARTES") + "</strong> partes." %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MTTR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_Media" runat="server" Font-Bold="true" Text='<%#Eval("MTTR") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>


                                            </div>
                                            <div class="col-lg-6">
                                                <strong style="font-size:x-large">TOP10 - MTBF Molde</strong>
                                                <h6>Tiempo medio entre fallos (horas)</h6>
                                                <div>
                                                    <canvas id="PieChartMantenimientoHORASMESMoldeMTBF" style="max-height: 250px"></canvas>
                                                </div>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOLxParteMTBF" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="white" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMOL" CommandArgument='<%#Eval("Molde")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" Visible="false" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxPAÑO" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Molde" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxP" runat="server" Text='<%#Eval("Molde")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Produciendo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxH" runat="server" Font-Bold="true" Text='<%#Eval("HorasProduciendo") %>' /><label class="ms-2">horas prod.</label><br />
                                                                    <asp:Label ID="lblPARTES_MxP" runat="server" Font-Size="small" Font-Italic="true" Text='<%#"con <strong>" + Eval("PARTES") + "</strong> partes." %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MTBF">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_Media" runat="server" Font-Bold="true" Text='<%#Eval("MTBF") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <h4>Apertura de partes de molde</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOL" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="white" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Persona">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("Nombre")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("PARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade rounded" id="pills-profile_molde" role="tabpanel" aria-labelledby="pills-profile-tab" style="background-color: #eeeeee">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <strong style="font-size:x-large">TOP10 - MTTR Molde</strong>
                                                <h6>Tiempo medio de reparación (horas)</h6>
                                                <div>
                                                    <canvas id="PieChartMantenimientoHORASAÑOMolde" style="max-height: 250px"></canvas>
                                                </div>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOLxParteAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMOL" CommandArgument='<%#Eval("Molde")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" Visible="false" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxP" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Molde" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxP" runat="server" Text='<%#Eval("Molde")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reparación">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxH" runat="server" Font-Bold="true" Text='<%#Eval("HORAS") %>' /><label class="ms-2">horas</label><br />
                                                                    <asp:Label ID="lblPARTES_MxP" runat="server" Font-Size="small" Font-Italic="true" Text='<%#"para <strong>" + Eval("PARTES") + "</strong> partes." %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MTTR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_Media" runat="server" Font-Bold="true" Text='<%#Eval("MTTR") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>


                                            </div>
                                            <div class="col-lg-6">
                                                <strong style="font-size:x-large">TOP10 - MTBF Molde</strong>
                                                <h6>Tiempo medio entre fallos (horas)</h6>
                                                <div>
                                                    <canvas id="PieChartMantenimientoHORASAÑOMoldeMTBF" style="max-height: 250px"></canvas>
                                                </div>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOLxParteAÑOMTBF" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="white" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMOL" CommandArgument='<%#Eval("Molde")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" Visible="false" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxPAÑO" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Molde" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxP" runat="server" Text='<%#Eval("Molde")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Produciendo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxH" runat="server" Font-Bold="true" Text='<%#Eval("HorasProduciendo") %>' /><label class="ms-2">horas prod.</label><br />
                                                                    <asp:Label ID="lblPARTES_MxP" runat="server" Font-Size="small" Font-Italic="true" Text='<%#"con <strong>" + Eval("PARTES") + "</strong> partes." %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MTBF">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_Media" runat="server" Font-Bold="true" Text='<%#Eval("MTBF") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>



                                            <div class="col-lg-6">
                                                
                                                <h4>Apertura de partes de molde</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOLAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescNOKAÑO" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Persona">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOKAÑO" runat="server" Text='<%#Eval("Nombre")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblScrapNOKAÑO" runat="server" Text='<%#Eval("PARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <h4>Tipos de mantenimiento</h4>
                                                <div class="input-group mb-3 bg-white shadow-sm me-2">
                                                    <asp:DropDownList ID="lista_trabajos" runat="server" CssClass="form-select">
                                                    </asp:DropDownList>
                                                    <button class="btn btn-outline-secondary" type="button" id="CargarIdTrabajo" runat="server" onserverclick="cargar_tablas">Filtrar</button>
                                                </div>


                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridTipoMantenmientoAño" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMES_MOLxTEXTO" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mantenimiento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMOL_TipoMant" runat="server" Text='<%#Eval("TIPOMANT")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMOL_Partes" runat="server" Text='<%#Eval("NUMPARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Horas">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMOL_Horas" runat="server" Text='<%#Eval("HorasREP") %>' />
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

</asp:Content>




