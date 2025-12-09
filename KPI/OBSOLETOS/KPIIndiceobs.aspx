<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPIIndiceobs.aspx.cs" Inherits="ThermoWeb.KPI.KPIIndice"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cuadros de mando</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../SMARTH_css/bootstrap.css" rel="stylesheet">
    <link href="../SMARTH_fonts/bootstrap-icons.css" rel="stylesheet">
    <script src="../SMARTH_js/bootstrap.bundle.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">

            <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
                <div class="container-fluid">
                    <a class="navbar-brand" href="../index.aspx">
                        <img src="http://facts4-srv/thermogestion/imagenes/SMARTHLOGOSM.png" alt="" width="30" height="30" class="d-inline-block align-text-top">
                        SmarTH
                    </a>
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul class="navbar-nav me-auto  col-lg-9 mb-lg-0">
                            <li class="nav-item">
                                <a class="nav-link active" aria-current="page" href="#">Cuadros de mando y accesos rápidos</a>
                            </li>
                        </ul>
                        <form class="d-flex">
                            <a class="navbar-brand" href="https://www.thermolympic.es/">
                                <img src="http://facts4-srv/thermogestion/imagenes/THERMOLOGONEGRO.jpg" alt="" width="30" height="30" class="d-inline-block align-text-top">
                                Thermolympic S.L.
                            </a>
                        </form>
                    </div>
                </div>
            </nav>
        </div>
        <div class="row">
            <div class="col-lg-4">
                <div class="card mt-md-3 shadow p-3 mb-5 bg-white rounded" style="width: 100%;">
                    <div class="card-body bg-primary text-white border border-primary rounded-top">
                        <h5 class="card-title"><i class="bi bi-building">  Producción</i></h5>
                        <p class="card-text small">Informes de prioridades, liberaciones de serie y parámetros</p>
                    </div>
                    <ul class="list-group list-group-flush border border-primary">
                        <a href="http://facts4-srv/thermogestion/PLANIFICACION/ListadoPrioridades.aspx" class="list-group-item list-group-item-action"><i class="bi bi-display me-md-2"></i><strong>En producción:</strong> Informe de prioridades y acciones abiertas</a>
                        <a href="http://facts4-srv/thermogestion/LIBERACIONES/ConsultaNivelOperario.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-display me-md-2"></i><strong>En producción:</strong> Operarios trabajando con nivel I</a>
                        <a href="http://facts4-srv/thermogestion/LIBERACIONES/EstadoLiberacion.aspx" class="list-group-item list-group-item-action "><i class="bi bi-display me-md-2"></i><strong>Liberaciones:</strong> Referencias en producción</a>  
                        <a href="http://facts4-srv/thermogestion/LIBERACIONES/HistoricoLiberacion.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-journals me-md-2"></i><strong>Liberaciones:</strong> Histórico</a>
                        <a href="http://facts4-srv/thermogestion/ListaFichasParametros.aspx" class="list-group-item list-group-item-action"><i class="bi bi-journals me-md-2"></i><strong>Parámetros:</strong> Listado de fichas</a>
                        <a href="http://facts4-srv/thermogestion/KPI/KPILiberaciones.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-graph-up me-md-2"></i><strong>Liberaciones:</strong> Registro de liberaciones</a>
                        <a href="http://facts4-srv/thermogestion/KPIFichasParametros.aspx" class="list-group-item list-group-item-action"><i class="bi bi-graph-up me-md-2"></i><strong>Parámetros:</strong> Referencias produciendo sin digitalizar</a>
                        <a href="http://facts4-srv/thermogestion/KPI/KPIAprovechados.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-graph-up me-md-2"></i><strong>Operarios:</strong> Aprovechamiento</a>
                    </ul>
                    <div class="card-body-sm text-white border border-primary rounded-bottom border-top-0" style="background-color:dodgerblue">
                        <a href="http://facts4-srv/thermogestion/FichasParametros_nuevo.aspx" class="card-link text-white ms-md-3" style="font-weight:500"><i class="bi bi-file-plus me-md-2"></i>Crear una ficha de parámetros</a>
                        <a href="http://facts4-srv/thermogestion/LIBERACIONES/EstadoLiberacion.aspx" class="card-link text-white float-end me-md-3" style="font-weight:500"><i class="bi bi-file-plus me-md-2"></i>Liberar una máquina</a>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card ms-md-1 mt-md-3 shadow p-3 mb-5 bg-white rounded" style="width: 100%;">
                    <div class="card-body bg-success text-white border border-success rounded-top">
                        <h5 class="card-title"><i class="bi bi-wrench">  Mantenimiento</i></h5>
                        <p class="card-text small">Acciones abiertas e histórico de reparaciones</p>
                    </div>
                    <ul class="list-group list-group-flush border border-success">
                            <a href="http://facts4-srv/oftecnica/EstadoReparacionesMaquina.aspx" class="list-group-item list-group-item-action"><i class="bi bi-display me-md-2"></i><strong>Máquinas:</strong> Acciones abiertas</a>
                            <a href="http://facts4-srv/oftecnica/EstadoReparacionesMoldes.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-display me-md-2"></i><strong>Moldes:</strong> Acciones abiertas</a>
                            <a href="http://facts4-srv/oftecnica/InformeMaquinas.aspx" class="list-group-item list-group-item-action"><i class="bi bi-journals me-md-2"></i><strong>Máquinas:</strong> Detalles e histórico</a> 
                            <a href="http://facts4-srv/oftecnica/InformeMoldes.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-journals me-md-2"></i><strong>Moldes:</strong> Detalles e histórico</a>
                            <a href="http://facts4-srv/oftecnica/GestionPerifericos.aspx" class="list-group-item list-group-item-action"><i class="bi bi-journals me-md-2"></i><strong>Periféricos:</strong> Detalles e histórico</a>
                        
                    </ul>
                    <div class="card-body-sm text-white border border-success rounded-bottom border-top-0" style="background-color:mediumseagreen">
                        <a href="http://facts4-srv/oftecnica/ReparacionMoldes.aspx" class="card-link text-white ms-md-3" style="font-weight:500"><i class="bi bi-file-plus me-md-2"></i>Crear un parte de molde</a>
                        <a href="http://facts4-srv/oftecnica/ReparacionMaquinas.aspx" class="card-link text-white float-end me-md-3" style="font-weight:500"><i class="bi bi-file-plus me-md-2"></i>Crear un parte de máquina</a>
                    </div>
                </div>
                
            </div>
            <div class="col-lg-4">
                <div class="card ms-md-1 mt-md-3 me-mt-1 shadow p-3 mb-5 bg-white rounded" style="width: 100%;">
                    <div class="card-body bg-danger text-white border border-danger rounded-top">
                        <h5 class="card-title"><i class="bi bi-rulers">  Calidad</i></h5>
                        <p class="card-text small">Muro de calidad y no conformidades</p>
                    </div>
                    <ul class="list-group list-group-flush border border-danger">
                            <a href="http://facts4-srv/thermogestion/GP12/GP12Historico.aspx" class="list-group-item list-group-item-action "><i class="bi bi-display me-md-2"></i><strong>GP12:</strong> Últimas revisiones</a>
                            <a href="http://facts4-srv/thermogestion/CALIDAD/ListaAlertasCalidad.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-display me-md-2"></i><strong>No conformidades:</strong> Listado de reclamaciones</a>
                            <a href="http://facts4-srv/thermogestion/GP12/GP12ReferenciasEstado.aspx" class="list-group-item list-group-item-action"><i class="bi bi-journals me-md-2"></i><strong>GP12:</strong> Estado de referencias</a>
                            <a href="http://facts4-srv/thermogestion/GP12/GP12RegistroComunicaciones.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-journals me-md-2"></i><strong>GP12:</strong> Registro de comunicaciones</a>
                    </ul>
                    <div class="card-body-sm text-white border border-danger rounded-bottom border-top-0" style="background-color:palevioletred">
                        <a href="http://facts4-srv/thermogestion/CALIDAD/Alertas_Calidad.aspx" class="card-link text-white ms-md-3" style="font-weight:500"><i class="bi bi-file-plus me-md-2"></i>Abrir una no conformidad</a>
                        <a href="http://facts4-srv/thermogestion/GP12/GP12.aspx" class="card-link text-white float-end me-md-3" style="font-weight:500"><i class="bi bi-file-plus me-md-2"></i>Iniciar una revisión</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4">
                 <div class="card mt-md-3 me-mt-1 shadow p-3 mb-5 bg-white rounded" style="width: 100%;">
                    <div class="card-body bg-dark text-white border border-dark rounded-top">
                        <h5 class="card-title"><i class="bi bi-graph-up me-md-2">  Indicadores</i></h5>
                        <p class="card-text small">Resultados generales de las distintas aplicaciones</p>
                    </div>
                    <ul class="list-group list-group-flush border border-dark">
                            <a href="http://facts4-srv/oftecnica/InformeHorasMantenimiento.aspx" class="list-group-item list-group-item-action"><i class="bi bi-graph-up me-md-2"></i>Costes de mantenimiento (correctivos/preventivos)</a>
                            <a href="http://facts4-srv/thermogestion/CALIDAD/KPI_NoConformidades.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-graph-up me-md-2"></i>No conformidades, costes de no calidad y chatarras</a>
                            <a href="http://facts4-srv/thermogestion/GP12/GP12KPI.aspx" class="list-group-item list-group-item-action"><i class="bi bi-graph-up me-md-2"></i>Resultados de muro de calidad</a>
                    </ul>
                    <div class="card-body-sm text-white border border-secondary rounded-bottom border-top-0" style="background-color:darkgrey">
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                 <div class="card mt-md-3 me-mt-1 shadow p-3 mb-5 bg-white rounded align-content-center" style="width: 100%;">
                    <div class="card-body bg-dark text-white border border-dark rounded-top">
                        <button type="button" class="btn btn-dark" runat="server" onserverclick="ImportardeBMS" style="width:100%"><i class="bi bi-cloud-arrow-down" style="font-size:100px"></i><br />Importar nuevos productos y operarios de BMS/NAV</button>
                    </div>
                </div>
            </div>

        </div>
    </form>
    <link href="../SMARTH_css/bootstrap.css" rel="stylesheet">
    <link href="../SMARTH_fonts/bootstrap-icons.css" rel="stylesheet">
    <script src="../SMARTH_js/bootstrap.bundle.js"></script>
</body>
</html>
