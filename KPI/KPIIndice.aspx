<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/SMARTH.Master" CodeBehind="KPIIndice.aspx.cs" Inherits="ThermoWeb.KPI.KPIIndice" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Cuadros de mando</title>
    <link rel="shortcut icon" type="image/x-icon" href="../FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Cuadros de mando y accesos rápidos
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-4">
                <div class="card mt-md-3 shadow p-2 mb-5 bg-white rounded" style="width: 100%;">
                    <div class="card-body bg-primary text-white border border-primary rounded-top">
                        <h5 class="card-title"><i class="bi bi-building">Producción</i></h5>
                        <p class="card-text small">Informes de prioridades, liberaciones de serie y parámetros</p>
                    </div>
                    <ul class="list-group list-group-flush border border-primary">
                        <a href="../PLANIFICACION/ListadoPrioridades.aspx" class="list-group-item list-group-item-action"><i class="bi bi-display me-md-2"></i><strong>En producción:</strong> Informe de prioridades y acciones abiertas</a>
                        <a href="../PRODUCCION/Tareas_cambiador.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-display me-md-2"></i><strong>En producción:</strong> Informe de cambios de orden</a>
                        <a href="../LIBERACIONES/ConsultaNivelOperario.aspx" class="list-group-item list-group-item-action "><i class="bi bi-display me-md-2"></i><strong>En producción:</strong> Operarios trabajando con nivel I</a>
                        <a href="../LIBERACIONES/EstadoLiberacion.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-display me-md-2"></i><strong>Liberaciones:</strong> Referencias en producción</a>
                        <a href="../LIBERACIONES/HistoricoLiberacion.aspx" class="list-group-item list-group-item-action "><i class="bi bi-journals me-md-2"></i><strong>Liberaciones:</strong> Histórico</a>
                        
                        <a href="../PRODUCCION/MontajesHistorico.aspx" class="list-group-item list-group-item-action list-group-item-secondary "><i class="bi bi-journals me-md-2"></i><strong>Montajes:</strong> Histórico</a>
                       
                        <a href="../ListaFichasParametros.aspx" class="list-group-item list-group-item-action "><i class="bi bi-journals me-md-2"></i><strong>Parámetros:</strong> Listado de fichas</a>
                        <a href="../KPIFichasParametros.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-graph-up me-md-2"></i><strong>Parámetros:</strong> Referencias produciendo sin digitalizar</a>
                        <a href="KPIAprovechados.aspx" class="list-group-item list-group-item-action "><i class="bi bi-graph-up me-md-2"></i><strong>Operarios:</strong> Aprovechamiento</a>
                         <a href="KPI_WTOP_Pegamento.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-graph-up me-md-2"></i><strong>WTOP:</strong> Últimos cambios de bidón</a>
                     
                    </ul>
                    <div class="card-body-sm text-white border border-primary rounded-bottom border-top-0" style="background-color: dodgerblue">
                        <a href="../FichasParametros_nuevo.aspx" class="card-link text-white ms-md-3" style="font-weight: 500"><i class="bi bi-file-plus me-md-2"></i>Crear una ficha de parámetros</a>
                        <a href="../LIBERACIONES/EstadoLiberacion.aspx" class="card-link text-white float-end me-md-3" style="font-weight: 500"><i class="bi bi-file-plus me-md-2"></i>Liberar una máquina</a>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card ms-md-1 mt-md-3 shadow p-2 mb-5 bg-white rounded" style="width: 100%;">
                    <div class="card-body bg-success text-white border border-success rounded-top">
                        <h5 class="card-title"><i class="bi bi-wrench">Mantenimiento</i></h5>
                        <p class="card-text small">Acciones abiertas e histórico de reparaciones</p>
                    </div>
                    <ul class="list-group list-group-flush border border-success">
                        <a href="../MANTENIMIENTO/EstadoReparacionesMaquina.aspx" class="list-group-item list-group-item-action"><i class="bi bi-display me-md-2"></i><strong>Máquinas:</strong> Acciones abiertas</a>
                        <a href="../MANTENIMIENTO/EstadoReparacionesMoldes.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-display me-md-2"></i><strong>Moldes:</strong> Acciones abiertas</a>
                        <a href="../MANTENIMIENTO/InformeMaquinas.aspx" class="list-group-item list-group-item-action"><i class="bi bi-journals me-md-2"></i><strong>Máquinas:</strong> Detalles e histórico</a>
                        <a href="../MANTENIMIENTO/InformeMoldes.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-journals me-md-2"></i><strong>Moldes:</strong> Detalles e histórico</a>
                        <a href="../MANTENIMIENTO/InformePerifericos.aspx" class="list-group-item list-group-item-action"><i class="bi bi-journals me-md-2"></i><strong>Periféricos:</strong> Detalles e histórico</a>

                    </ul>
                    <div class="card-body-sm text-white border border-success rounded-bottom border-top-0" style="background-color: mediumseagreen">
                        <a href="http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx" class="card-link text-white ms-md-3" style="font-weight: 500"><i class="bi bi-file-plus me-md-2"></i>Crear un parte de molde</a>
                        <a href="../MANTENIMIENTO/ReparacionMaquinas.aspx" class="card-link text-white float-end me-md-3" style="font-weight: 500"><i class="bi bi-file-plus me-md-2"></i>Crear un parte de máquina</a>
                    </div>
                </div>

            </div>
            <div class="col-lg-4">
                <div class="card ms-md-1 mt-md-3 me-mt-1 shadow p-2 mb-5 bg-white rounded" style="width: 100%;">
                    <div class="card-body bg-danger text-white border border-danger rounded-top">
                        <h5 class="card-title"><i class="bi bi-rulers">Calidad</i></h5>
                        <p class="card-text small">Muro de calidad y no conformidades</p>
                    </div>
                    <ul class="list-group list-group-flush border border-danger">
                        <a href="../GP12/GP12Historico.aspx" class="list-group-item list-group-item-action "><i class="bi bi-display me-md-2"></i><strong>GP12:</strong> Últimas revisiones</a>
                        <a href="../CALIDAD/ListaAlertasCalidad.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-display me-md-2"></i><strong>No conformidades:</strong> Listado de reclamaciones</a>
                        <a href="../GP12/GP12ReferenciasEstado.aspx" class="list-group-item list-group-item-action"><i class="bi bi-journals me-md-2"></i><strong>GP12:</strong> Estado de referencias</a>
                        <a href="../GP12/GP12RegistroComunicaciones.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-journals me-md-2"></i><strong>GP12:</strong> Registro de comunicaciones</a>
                         <a href="../CALIDAD/ProductoDetallesCalidad.aspx" class="list-group-item list-group-item-action"><i class="bi bi-journals me-md-2"></i><strong>CALIDAD:</strong> Informe de producto</a>
                        
                    </ul>
                    <div class="card-body-sm text-white border border-danger rounded-bottom border-top-0" style="background-color: palevioletred">
                        <a href="../CALIDAD/Alertas_Calidad.aspx" class="card-link text-white ms-md-3" style="font-weight: 500"><i class="bi bi-file-plus me-md-2"></i>Abrir una no conformidad</a>
                        <a href="../GP12/GP12.aspx" class="card-link text-white float-end me-md-3" style="font-weight: 500"><i class="bi bi-file-plus me-md-2"></i>Iniciar una revisión</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4">
                <div class="card mt-md-3 me-mt-1 shadow p-2 mb-5 bg-white rounded" style="width: 100%;">
                    <div class="card-body bg-dark text-white border border-dark rounded-top">
                        <h5 class="card-title"><i class="bi bi-graph-up me-md-2">Indicadores</i></h5>
                        <p class="card-text small">Resultados generales de las distintas aplicaciones</p>
                    </div>
                    <ul class="list-group list-group-flush border border-dark">
                        <a href="KPI_Comunicaciones.aspx" class="list-group-item list-group-item-action"><i class="bi bi-graph-up me-md-2"></i>Resultados de comunicaciones</a>
                        <a href="KPI_Mantenimiento.aspx" class="list-group-item list-group-item-action  list-group-item-secondary"><i class="bi bi-graph-up me-md-2"></i>Resultados de mantenimiento</a>
                        <a href="KPI_NoConformidades.aspx" class="list-group-item list-group-item-action"><i class="bi bi-graph-up me-md-2"></i>No conformidades, costes de no calidad y chatarras</a>
                        <a href="KPILiberaciones.aspx" class="list-group-item list-group-item-action  list-group-item-secondary"><i class="bi bi-graph-up me-md-2"></i>Resultados de liberaciones</a>
                        <a href="KPI_GP12.aspx" class="list-group-item list-group-item-action"><i class="bi bi-graph-up me-md-2"></i>Resultados de muro de calidad</a>
                        <a href="KPI_PDCA.aspx" class="list-group-item list-group-item-action list-group-item-secondary"><i class="bi bi-graph-up me-md-2"></i>Resultados de planes de acción</a>
                        <a href="KPI_Dashboard_General_Tendencias.aspx" class="list-group-item list-group-item-action"><i class="bi bi-display me-md-2"></i><strong>Dashboard:</strong> Tendencias de planta</a>
                    </ul>
                    <div class="card-body-sm bg-dark text-white border border-dark rounded-bottom border-top-0" style="background-color: darkgrey">
                    <br />
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="card mt-md-3 me-mt-1 shadow p-2 mb-5 bg-white rounded" style="width: 100%;">
                    <div class="card-body bg-secondary text-white border border-secondary rounded-top">
                        <h5 class="card-title"><i class="bi bi bi-pass me-md-2">Procedimientos</i></h5>
                        <p class="card-text small">Acceso rápido a procedimientos de planta</p>
                    </div>
                    <div class="accordion accordion-flush  border border-secondary" id="accordionFlushExample">
                        <div class="accordion-item">
                            <h2 class="accordion-header">
                                <button class="accordion-button btn-sm collapsed" type="button" style="font-weight: bold" data-bs-toggle="collapse" data-bs-target="#flush-collapseOne" aria-expanded="false" aria-controls="flush-collapseOne">
                                    <i class="bi bi-journals me-md-2"></i>Plan de contingencia
                                </button>
                            </h2>
                            <div id="flush-collapseOne" class="accordion-collapse collapse" data-bs-parent="#accordionFlushExample">
                                <div class="accordion-body">
                                    <a href="../SMARTH_docs/POCS/POC 31.02 Sistema de alarma y escalación Ed. 8.pdf" class="list-group-item list-group-item-action"><i class="bi bi-journals ms-4 me-md-2"></i>Procedimiento de escalado</a>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="card-body-sm text-white border border-secondary rounded-bottom border-top-0" style="background-color: darkgrey">
                    <br />
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card mt-md-3 me-mt-1 shadow p-2 mb-5 bg-white rounded align-content-center" style="width: 100%;">
                    <div class="card-body bg-dark text-white border border-dark rounded-top">
                        <button type="button" class="btn btn-dark" runat="server" onserverclick="ImportardeBMS" style="width: 100%">
                            <i class="bi bi-cloud-arrow-down" style="font-size: 100px"></i>
                            <br />
                            Importar nuevos productos y operarios de BMS/NAV</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
