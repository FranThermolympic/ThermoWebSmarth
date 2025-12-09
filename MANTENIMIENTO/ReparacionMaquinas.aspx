<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReparacionMaquinas.aspx.cs" Inherits="ThermoWeb.MANTENIMIENTO.ReparacionMaquinas" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Mantenimiento de máquinas</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Mantenimiento de máquinas
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown  ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Partes de trabajo
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMaquinas.aspx">Partes de máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMoldes.aspx">Partes de moldes</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación de referencia</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/MantenimientoIndex.aspx">Índice de mantenimiento</a></li>
            </ul>

        </li>
        <li class="nav-item dropdown  ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Acciones abiertas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMaquina.aspx">Pendientes en máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMoldes.aspx">Pendientes en moldes</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown ms-2">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Informes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMaquinas.aspx">Informe de máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMoldes.aspx">Informe de moldes</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformePerifericos.aspx">Informe de periféricos</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../KPI/KPI_Mantenimiento.aspx">Resultados de mantenimiento</a></li>

            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid" runat="server">
            <script type="text/javascript">
                function ValidadoDoble() {
                    alert("Se ha producido un error. La reparación está validada como OK y como NOK al mismo tiempo.");
                }
                function ValidadoNREP() {
                    alert("Se ha producido un error. No se puede validar un parte que aun no ha sido reparado.");
                }
            </script>
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
            <div class="row">
                <div class="col-lg-2 mt-2">
                    <div class="input-group shadow ms-2">
                        <span class="input-group-text border border-1 border-dark bg-white" style="font: bold" id="inputGroup-sizing-default">Parte</span>
                        <input class="form-control border border-1 border-dark" id="lista_partes" type="text" runat="server" placeholder="" disabled>
                        <asp:HiddenField ID="HiddenNuevo" runat="server" />
                        
                        <button id="btnAnterior" runat="server" class="btn btn-outline-dark" type="button" onserverclick="IrAnteriorSiguiente"><i class="bi bi-chevron-double-left"></i></button>
                        <button id="btnSiguiente" runat="server" class="btn btn-outline-dark" type="button" onserverclick="IrAnteriorSiguiente"><i class="bi bi-chevron-double-right"></i></button>
                    </div>
                </div>
                <div class="col-lg-4 mt-2">
                    <button id="Button1" runat="server" class="btn btn-outline-dark" type="button" onserverclick="MandarMailTESTING" visible="false">Mail</button>
                </div>
                <div class="col-lg-6 mt-2">
                    <div class="input-group bg-white shadow ms-2">
                        <button class="btn btn-outline-dark" runat="server" type="button" id="buttonaddon3" onserverclick="CrearNuevo"><i class="bi bi-file-plus">&nbspNuevo</i></button>
                        <button class="btn btn-outline-dark" runat="server" type="button" id="buttonaddon2" onserverclick="GuardarParte"><i class="bi bi-sd-card">&nbspGuardar</i></button>
                        <input class="form-control border border-1 border-dark" list="FiltroParte" id="tbBuscar" runat="server" placeholder="Selecciona un parte..." autocomplete="off">
                        <datalist id="FiltroParte" runat="server">
                        </datalist>
                        <button type="button" class="btn btn-outline-secondary dropdown-toggle dropdown-toggle-split border border-1 border-dark " data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="bi bi-bell-fill"></i>
                        </button>
                        <ul class="dropdown-menu">
                            <div class="list-group" id="listado_partes" runat="server">
                                <%-- ESPACIO PARA ALERTAS--%>
                            </div>
                        </ul>
                        <button class="btn btn-outline-dark" type="button" runat="server" onserverclick="BuscarParte">Filtrar</button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <nav>
                        <div class="nav nav-tabs mt-3 justify-content-start border border-0 " id="nav-tab" role="tablist">
                            <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true"><i class="bi bi-pencil-square"></i>&nbsp Parte</button>
                            <button class="nav-link" id="nav-profile-tab2" data-bs-toggle="tab" data-bs-target="#nav-profile2" type="button" role="tab" aria-controls="nav-profile2" aria-selected="false" hidden="hidden"><i class="bi bi-bullseye"></i>&nbsp Molde</button>
                            <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile" type="button" role="tab" aria-controls="nav-profile" aria-selected="false"><i class="bi bi-folder2-open"></i>&nbsp Histórico de máquina</button>
                        </div>
                    </nav>
                    <div class="tab-content" id="nav-tabContent">
                        <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card shadow p-2 mb-3 bg-white rounded" style="width: 100%">
                                        <div class="progress">
                                            <div id="progressERROR" class="progress-bar progress-bar-striped progress-bar-animated bg-danger" runat="server" role="progressbar" visible="false" style="width: 100%">
                                                ¡Parte con errores de validación!
                                            </div>
                                            <div id="progressABIERTO" class="progress-bar progress-bar-striped progress-bar-animated bg-warning" runat="server" role="progressbar" visible="false" style="width: 25%; color: black; font-weight: bold">
                                                ABIERTO (Sin acciones registradas)
                                            </div>
                                            <div id="progressABIERTOINI" class="progress-bar progress-bar-striped  progress-bar-animated bg-info" runat="server" role="progressbar" visible="false" style="width: 50%; color: black; font-weight: bold">
                                                ABIERTO (Iniciadas acciones)
                                            </div>
                                            <div id="progressPENDIENTE" class="progress-bar progress-bar-striped  progress-bar-animated bg-info" runat="server" role="progressbar" visible="false" style="width: 75%; color: black; font-weight: bold">
                                                REPARADO (Pendiente de validar)
                                            </div>
                                            <div id="progressENCURSO" class="progress-bar progress-bar-striped progress-bar-animated  bg-info" runat="server" role="progressbar" visible="false" style="width: 35%; color: black; font-weight: bold">
                                                Parte abierto, en curso
                                            </div>
                                            <div id="progressNOCONFORME" class="progress-bar progress-bar-striped progress-bar-animated bg-danger" runat="server" role="progressbar" visible="false" style="width: 50%; color: black; font-weight: bold">
                                                Reparación no conforme
                                            </div>
                                            <div id="progressREPARADO" class="progress-bar progress-bar-striped progress-bar-animated bg-success" runat="server" role="progressbar" visible="false" style="width: 100%; color: black; font-weight: bold">
                                                PARTE CERRADO
                                            </div>
                                        </div>
                                        <div class="card-body bg-primary text-white border border-success rounded-top">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <h3 class="card-title"><i class="bi bi-chat-left-text me-2"></i>Registro</h3>
                                                </div>
                                                <div class="col-lg-3">
                                                    <span class="input-group-text bg-transparent border-0 text-white float-end" style="font-size: larger">Prioridad: </span>

                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:DropDownList ID="prioridad" runat="server" CssClass="form-select  border border-secondary"></asp:DropDownList>
                                                </div>

                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-lg-7">

                                                <h6 class="ms-1 mt-1">Datos de la avería</h6>
                                                <div class="rounded shadow border border border-secondary" style="background-color: lavender">
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <h6 class="ms-2 mt-2">Inyectoras y asociados:</h6>
                                                            <div class="mb-3 ms-2" style="width: 98%">
                                                                <asp:DropDownList ID="lista_maquinas" runat="server" CssClass="form-select border border-secondary" Width="98%">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <h6 class="ms-2 mt-2">Periféricos:</h6>
                                                            <asp:DropDownList ID="lista_perifericos" runat="server" CssClass="form-select border border-secondary" Width="98%">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <h6 class="ms-2 mt-2">Instalaciones:</h6>
                                                            <asp:DropDownList ID="instalacion" runat="server" CssClass="form-select border border-secondary" Width="98%">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <h6 class="ms-2">Tipo de mantenimiento:</h6>
                                                            <asp:DropDownList ID="lista_trabajos" runat="server" CssClass="form-select ms-2 me-2 border border-secondary" Width="98%">
                                                            </asp:DropDownList>
                                                            <div id="LabelTipoPreventivo" runat="server" visible="false">
                                                                <h6 class="ms-2 mt-2">Tipo de preventivo:</h6>
                                                                <asp:DropDownList ID="DropTipoPreventivo" runat="server" CssClass="form-select border border-secondary ms-2 me-2" Width="98%" AutoPostBack="True" OnSelectedIndexChanged="SeleccionPreventivo"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-8">
                                                            <h6 class="ms-2">Avería / Descripción:</h6>
                                                            <textarea id="averia" class="form-control ms-2 me-2 border border-secondary" rows="4" runat="server" style="width: 98%"></textarea>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <h6 class="ms-2 mt-2">Insertar imágenes:</h6>
                                                        <div class="col-lg-5">
                                                            <asp:FileUpload ID="FileUpload1" class="form-control ms-2 border border-secondary" Width="98%" runat="server"></asp:FileUpload>
                                                        </div>
                                                        <div class="col-lg-5">
                                                            <asp:FileUpload ID="FileUpload2" class="form-control  ms-2 border border-secondary" Width="98%" runat="server"></asp:FileUpload>
                                                        </div>

                                                        <div class="col-lg-2">
                                                            <button id="Button2" runat="server" onserverclick="Insertar_foto" type="button" class="btn btn-primary btn-lg float-end mb-2 me-3"><i class="bi bi-upload"></i></button>
                                                        </div>
                                                        <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <h6 class="ms-1 mt-1">Datos del parte</h6>
                                                <div class="rounded shadow border border-secondary" style="background-color: lavender">
                                                    <div class="row">
                                                        <div class="col-lg-4 mb-2">
                                                            <h6 class="ms-2 mt-2">Fecha apertura:</h6>
                                                            <input type="text" class="form-control Add-text ms-2  border border-secondary" id='date_apertura' runat="server" style="width: 98%" autocomplete="off">
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <h6 class="ms-2 mt-2">Próxima producción:</h6>
                                                            <input type="text" class="form-control Add-text ms-2  border border-secondary" id='date_prox' runat="server" style="width: 98%" autocomplete="off">
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <div class="form-group">
                                                                <h6 class="ms-2 mt-2">Piloto:</h6>
                                                                <asp:DropDownList ID="encargado" runat="server" CssClass="form-select ms-2 border border-secondary" Width="93%">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-5">
                                                <div class="form-group">
                                                    <h6 class="ms-1 mt-1">Imágenes</h6>
                                                    <div id="myCarousel" class="carousel slide" data-bs-ride="carousel">
                                                        <div class="carousel-inner border border-secondary shadow rounded rounded-1" style="height: 500px">
                                                            <div class="carousel-item active">
                                                                <%--<img id="img1" class="d-block w-100" runat="server" src="../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" alt="">--%>
                                                                <asp:HyperLink ID="img1" NavigateUrl="../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" Width="100%" ImageWidth="100%" ImageUrl="../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" Target="_new" runat="server" />

                                                            </div>
                                                            <div class="carousel-item">
                                                                <%--<img id="img2" class="d-block w-100" runat="server" src="../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" alt="">--%>
                                                                <asp:HyperLink ID="img2" NavigateUrl="../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" Width="100%" ImageWidth="100%" ImageUrl="../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" Target="_new" runat="server" />

                                                                <%-- style="width: 100%; max-height: 100%; height: 100%;"--%>
                                                            </div>

                                                        </div>
                                                        <button class="carousel-control-prev" type="button" data-bs-target="#myCarousel" data-bs-slide="prev">
                                                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                            <span class="visually-hidden">Previous</span>
                                                        </button>
                                                        <button class="carousel-control-next" type="button" data-bs-target="#myCarousel" data-bs-slide="next">
                                                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                            <span class="visually-hidden">Next</span>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card shadow p-2 mb-3 bg-white rounded" style="width: 100%;">
                                        <div class="card-body bg-success text-white border border-success rounded-top">
                                            <div class="col-lg-4">
                                                <h3 class="card-title"><i class="bi bi-wrench me-2"></i>Reparación</h3>
                                            </div>
                                            <div class="col-lg-8"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <h6>General</h6>
                                                <div class="rounded shadow border border-secondary" style="background-color: lavender">
                                                    <div class="input-group-sm ms-1 mt-2">
                                                        <h6>Seleccionar personal</h6>
                                                        <asp:DropDownList ID="lista_realizadoPor" runat="server" CssClass="form-control shadow-sm " Visible="false">
                                                        </asp:DropDownList>
                                                        <div class="btn-group " style="width: 98%">
                                                            <button id="AgregarAsignado" type="button" class="btn btn-sm btn-primary border border-secondary " style="width: 50%" runat="server" onserverclick="Gestionar_trabajadores">Asignación</button>
                                                            <button id="AgregarReparado" type="button" class="btn btn-sm btn-success border border-secondary" style="width: 50%" runat="server" onserverclick="Gestionar_trabajadores">Reparación</button>
                                                        </div>
                                                        <asp:DropDownList ID="lista_realizadoPorNEW" runat="server" CssClass="form-select border border-secondary" Style="width: 98%"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group ms-1 mt-2">
                                                        <h6>Fecha asignada</h6>
                                                        <input type='text' class="form-control  border border-secondary Add-text" id='datetime_ini2' style="width: 98%" runat="server" autocomplete="off" />
                                                    </div>
                                                    <div class="form-group ms-1 mt-2">
                                                        <h6>Fecha de reparación</h6>
                                                        <input type='text' class="form-control  border border-secondary Add-text" id='datetime_rep2' style="width: 98%" runat="server" autocomplete="off" />
                                                    </div>
                                                    <div class="form-group ms-1 mb-2 mt-2">
                                                        <h6>Reparado</h6>
                                                        <input id="reparado" runat="server" class="form-check-input ms-2 bg-success shadow-sm" type="checkbox" style="width: 30px; height: 30px" value="" aria-label="..." checked>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="col-lg-10">
                                                <h6>Asignación</h6>
                                                <div class="rounded shadow  border border-secondary" style="background-color: lavender">
                                                    <div class="row">
                                                        <div class="col-lg-2 mt-2">
                                                            <div class="form-group ms-2">
                                                                <h6>Estado</h6>
                                                                <asp:DropDownList ID="DropEstado" runat="server" Class="form-select  border border-secondary" Width="98%">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">Iniciado</asp:ListItem>
                                                                    <asp:ListItem Value="2">En curso</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 mt-2">
                                                            <div class="form-group ms-2" runat="server" visible="false">
                                                                <h6>A reparar por</h6>
                                                                <asp:DropDownList ID="reparar_por" runat="server" Class="form-select border border-secondary" Width="98%">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group ms-2">
                                                                <h6>A reparar por</h6>
                                                                <div class="input-group mb-3 shadow-sm " style="width: 98%">
                                                                    <button class="btn btn-outline-secondary" id="BorrarAsignado1" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                    <input id="Asignado1" runat="server" disabled="disabled" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 mt-2" id="ColAsignado2" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <h6>&nbsp</h6>
                                                                <div class="input-group mb-3 ms-2" style="width: 98%">
                                                                    <button class="btn btn-outline-secondary" id="BorrarAsignado2" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                    <input id="Asignado2" runat="server" disabled="disabled" height="30px" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 mt-2" id="ColAsignado3" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <h6>&nbsp</h6>
                                                                <div class="input-group mb-3 ms-2" style="width: 98%">
                                                                    <button class="btn btn-outline-secondary" id="BorrarAsignado3" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                    <input id="Asignado3" runat="server" disabled="disabled" height="30px" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 mt-2">
                                                            <div class="form-group">
                                                                <h6 class="ms-2">Horas previstas</h6>
                                                                <input id="Horasprevistas" runat="server" class="form-control  border border-secondary ms-2 mb-2" style="width: 98%">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <h6>Reparación</h6>
                                                <div class="rounded shadow  border border-secondary" style="background-color: lavender">
                                                    <div class="row">
                                                        <div class="col-lg-7 mt-2">
                                                            <div class="form-group ms-2">
                                                                <h6>Tareas realizadas</h6>
                                                                <textarea id="reparacion" runat="server" class="form-control border border-secondary " rows="2"></textarea>
                                                            </div>
                                                            <div class="form-group ms-2 mb-2 mt-2">
                                                                <h6>Observaciones</h6>
                                                                <textarea id="observaciones" runat="server" class="form-control border border-secondary " rows="2"></textarea>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3  mt-2">
                                                            <div class="row">
                                                                <div class="col-lg-8" runat="server">
                                                                    <div class="form-group">
                                                                        <h6 class="ms-2">Realizado por</h6>
                                                                        <div class="input-group mb-1 shadow-sm ms-2" style="width: 98%">
                                                                            <button class="btn btn-outline-secondary " id="BorrarReparadoPor1" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                            <input id="ReparadoPor1" runat="server" disabled="disabled" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="input-group mb-1 shadow-sm ms-2" style="width: 98%">
                                                                            <button class="btn btn-outline-secondary" id="BorrarReparadoPor2" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                            <input id="ReparadoPor2" runat="server" disabled="disabled" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="input-group mb-1 shadow-sm ms-2" style="width: 98%">
                                                                            <button class="btn btn-outline-secondary" id="BorrarReparadoPor3" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                            <input id="ReparadoPor3" runat="server" disabled="disabled" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                        </div>
                                                                    </div>


                                                                </div>


                                                                <div class="col-lg-4">
                                                                    <div class="form-group">
                                                                        <h6 class="ms-2">Horas</h6>
                                                                        <input id="horas1" runat="server" placeholder="0" style="width: 98%" class="form-control border border-secondary shadow-sm mb-1 ms-2" />
                                                                        <input id="horas2" runat="server" placeholder="0" style="width: 98%" visible="false" class="form-control border border-secondary shadow-sm mb-1 ms-2" />
                                                                        <input id="horas3" runat="server" placeholder="0" style="width: 98%" visible="false" class="form-control border border-secondary shadow-sm mb-1 ms-2" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2  mt-2">
                                                            <div class="form-group me-2">
                                                                <h6 class="ms-2">Costes de repuestos (€)</h6>
                                                                <input id="TbCostes" runat="server" class="form-control border border-secondary mb-2 ms-2" style="width: 98%">

                                                                <h6 class="ms-2">Costes totales (€)</h6>
                                                                <input id="TbCostesTotales" runat="server" class="form-control border border-secondary  ms-2 mb-2" style="width: 98%" disabled>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="BloquePreventivo" runat="server" visible="false" style="background-color: lightsteelblue; border-bottom-left-radius:5px; border-bottom-right-radius:5px">
                                                        <div id="Q1LINEA" class="row" runat="server">
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="TextTipoPreventivo" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" Font-Size="X-Large" BackColor="transparent" BorderColor="Transparent"></asp:TextBox>
                                                                <h6 class="ms-2">Acciones a realizar</h6>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q1DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q1INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>

                                                                <asp:TextBox ID="Q1Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div id="Q2LINEA" class="row" runat="server" visible="false">
                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q2DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q2INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q2Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div id="Q3LINEA" class="row" runat="server" visible="false">
                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q3DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q3INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q3Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div id="Q4LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q4DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q4INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q4Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q5LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q5DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q5INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q5Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q6LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q6DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q6INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q6Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q7LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q7DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q7INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q7Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q8LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q8DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q8INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q8Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q9LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q9DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q9INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q9Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q10LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q10DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q10INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q10Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q11LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q11DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q11INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q11Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q12LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q12DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q12INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q12Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q13LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q13DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q13INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q13Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q14LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q14DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q14INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q14Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q15LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q15DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q15INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q15Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q16LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q16DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q16INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q16Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q17LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q17DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q17INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q17Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q18LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q18DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q18INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q18Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q19LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q19DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q19INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q19Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                        <div id="Q20LINEA" class="row" runat="server" visible="false">

                                                            <div class="col-sm-1">
                                                                <asp:DropDownList ID="Q20DROP" Width="95%" runat="server" CssClass="form-control ms-2" Height="35px">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="1">OK</asp:ListItem>
                                                                    <asp:ListItem Value="2">NOK</asp:ListItem>
                                                                    <asp:ListItem Value="3">N/A</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-11">
                                                                <asp:TextBox ID="Q20INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="Q20Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card shadow p-2 mb-3 bg-white rounded" style="width: 100%;">
                                        <div class="card-body bg-danger text-white border border-danger rounded-top">
                                            <div class="col-lg-4">
                                                <h3 class="card-title"><i class="bi bi-hand-thumbs-up"></i>&nbsp Revisión</h3>
                                            </div>
                                            <div class="col-lg-8"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <div class="row">

                                                    <div class="form-group ms-2 mt-2">
                                                        <h6>Revisado OK</h6>
                                                        <div class="btn-group2 btn-group" data-toggle="buttons">
                                                            <input id="revisado" runat="server" class="form-check-input ms-2 bg-success shadow-sm" type="checkbox" style="width: 30px; height: 30px" value="" aria-label="...">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">

                                                    <div class="form-group ms-2 mt-2">
                                                        <h6>Revisado NOK</h6>
                                                        <div class="btn-group2 btn-group" data-toggle="buttons">
                                                            <input id="revisadoNOK" runat="server" class="form-check-input ms-2 bg-success shadow-sm" type="checkbox" style="width: 30px; height: 30px" value="" aria-label="...">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="row">
                                                    <div class="form-group mt-2">
                                                        <h6>Revisado por </h6>
                                                        <asp:DropDownList ID="revisado_por" runat="server" CssClass="form-select  border border-secondary" Style="width: 97%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group mt-2">
                                                        <h6>Fecha revisión</h6>
                                                        <input id="date_revision2" disabled="disabled" runat="server" type='text' class="form-control  border border-secondary" style="width: 97%" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="form-group mt-2">
                                                            <h6>Observaciones</h6>
                                                            <textarea id="observaciones_revision" runat="server" class="form-control  border border-secondary" rows="4"></textarea>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                            <div class="table-responsive">
                                <asp:GridView ID="dgv_ListadoHistorico" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border border-secondary border-top-0" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                    EmptyDataText="No hay moldes para mostrar.">
                                    <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#e6e6e6" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="button2" CssClass="btn btn-outline-dark" Font-Size="Large" runat="server" CommandName="Redirect" CommandArgument='<%#Eval("IdMantenimiento")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Parte">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParte" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("IdMantenimiento") %>' /><br />
                                                <asp:Label ID="lblFecha" runat="server" Text='<%#"("+Eval("FechaFinalizacionReparacion")+")" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Avería">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAveria" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reparación">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReparacion" runat="server" Text='<%#Eval("Reparacion") %>' />
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

</asp:Content>


<%-- 
<html>
<body>
    <form id="idform" runat="server">
        <div id="wrapper">            
            <div id="page-wrapper">
                <div id="Div1" class="container-fluid" runat="server">                        
                    <div class="row">
                        
                        <div class="col-lg-12">
                            <ul class="pagination">
                                <li>
                                    <asp:LinkButton ID="btnant" runat="server" OnClick="anterior">&laquo;</asp:LinkButton></li>
                                <li id="b1" runat="server" class="active">
                                    <asp:LinkButton ID="btn1" runat="server" OnClick="irPagina">1</asp:LinkButton></li>
                                <li id="b2" runat="server">
                                    <asp:LinkButton ID="btn2" runat="server" OnClick="irPagina">2</asp:LinkButton></li>
                                <li id="b3" runat="server">
                                    <asp:LinkButton ID="btn3" runat="server" OnClick="irPagina">3</asp:LinkButton></li>
                                <li id="b4" runat="server">
                                    <asp:LinkButton ID="btn4" runat="server" OnClick="irPagina">4</asp:LinkButton></li>
                                <li id="b5" runat="server">
                                    <asp:LinkButton ID="btn5" runat="server" OnClick="irPagina">5</asp:LinkButton></li>
                                <li id="b6" runat="server">
                                    <asp:LinkButton ID="btn6" runat="server" OnClick="irPagina">6</asp:LinkButton></li>
                                <li id="b7" runat="server">
                                    <asp:LinkButton ID="btn7" runat="server" OnClick="irPagina">7</asp:LinkButton></li>
                                <li id="b8" runat="server">
                                    <asp:LinkButton ID="btn8" runat="server" OnClick="irPagina">8</asp:LinkButton></li>
                                <li id="b9" runat="server">
                                    <asp:LinkButton ID="btn9" runat="server" OnClick="irPagina">9</asp:LinkButton></li>
                                <li id="b10" runat="server">
                                    <asp:LinkButton ID="btn10" runat="server" OnClick="irPagina">10</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="btnsig" runat="server" OnClick="siguiente">&raquo;</asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                    <div class="row">
                        
                        <div class="col-lg-10">
                            <div class="form-group">
                                <button id="nuevo" runat="server" onserverclick="crearNuevo" type="button" class="btn btn-default">
                                    <span style="margin-right: 8px" class="glyphicon glyphicon-file"></span>Nuevo
                                </button>
                                <button id="guardar" runat="server" onserverclick="guardarParte" type="button" class="btn btn-info">
                                    <span style="margin-right: 8px" class="glyphicon glyphicon-floppy-disk"></span>Guardar
                                </button>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <div class="input-group">
                                    <input id="tbBuscar" runat="server" type="text" class="form-control" placeholder="Escribe nº de parte">
                                    <div class="input-group-btn">
                                        <button id="btnBuscar" runat="server" onserverclick="buscarParte" class="btn" type="submit">
                                            <span style="margin-right: 8px" class="glyphicon glyphicon-search"></span>Buscar
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="progress">
                                <div id="progressERROR" class="progress-bar progress-bar-danger" runat="server" role="progressbar" visible="false" style="width: 100%">
                                    ¡Parte con errores de validación!
                                </div>
                                <div id="progressABIERTO" class="progress-bar progress-bar-warning" runat="server" role="progressbar" style="width: 25%">
                                    Parte abierto, sin acciones registradas
                                </div>
                                <div id="progressABIERTOINI" class="progress-bar progress-bar-info" runat="server" role="progressbar" style="width: 35%">
                                    Parte abierto, iniciadas acciones
                                </div>
                                <div id="progressENCURSO" class="progress-bar progress-bar-info" runat="server" role="progressbar" style="width: 35%">
                                    Parte abierto, en curso
                                </div>
                                <div id="progressPENDIENTE" class="progress-bar progress-bar-info" runat="server" role="progressbar" visible="false" style="width: 50%">
                                    Reparado, pendiente de validar
                                </div>
                                <div id="progressNOCONFORME" class="progress-bar progress-bar-danger" runat="server" role="progressbar" visible="false" style="width: 50%">
                                    Reparación no conforme
                                </div>
                                <div id="progressREPARADO" class="progress-bar progress-bar-success" runat="server" role="progressbar" visible="false" style="width: 100%">
                                    Reparado
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h1 class="panel-title">
                                <h2>Registro
                                </h2>
                            </h1>
                        </div>
                        <div id="Div2" class="panel-body" runat="server">
                            <div class="row">
                                <div class="col-lg-7">
                                    <div class="row">
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label for="disabledSelect">
                                                    Parte</label>
                                                <input class="form-control" id="lista_partes" type="text" runat="server" placeholder=""
                                                    disabled>
                                            </div>
                                        </div>
                                        <div class="col-lg-6"></div>
                                        <div class="col-lg-4">
                                            <div class="form-group" style="text-align: right">
                                                <label>Prioridad</label>
                                                <asp:DropDownList ID="prioridad" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    <label>Datos de la avería</label>
                                    <div class="well well-sm" style="background-color: lavender">
                                        <div class="row">
                                            <div class="col-lg-5">
                                                <div class="form-group">
                                                    <label>
                                                        Inyectoras y asociados</label>
                                                    <asp:DropDownList ID="lista_maquinas" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <div class="form-group">
                                                    <label>
                                                        Tipo de mantenimiento</label>
                                                    <asp:DropDownList ID="lista_trabajos" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <div id="LabelTipoPreventivo" runat="server" visible="false">
                                                        <br />
                                                        <label>Tipo de preventivo</label>
                                                        <asp:DropDownList ID="DropTipoPreventivo" Width="100%" runat="server" CssClass="form-control" Height="30px" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="SeleccionPreventivo"></asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="form-group">
                                                    <label>Avería / Descripción</label>
                                                    <textarea id="averia" class="form-control" rows="3" runat="server"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-5">
                                                <label>Insertar imágenes</label><asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload></div>
                                            <div class="col-lg-5">
                                                <label>&nbsp</label><asp:FileUpload ID="FileUpload2" runat="server"></asp:FileUpload></div>
                                            <div class="col-lg-2">
                                                <label>&nbsp</label>
                                                <button id="Button1" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-primary"><span class="glyphicon glyphicon-upload"></span>Cargar fotos</button></div>
                                            <asp:FileUpload ID="FileUpload3" runat="server" Visible="false"></asp:FileUpload>
                                            
                                            <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
                                        </div>

                                    </div>
                                    <label>Datos del parte</label>
                                    <div class="well well-sm" style="background-color: lavender">
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <div class="form-group">
                                                    <label>
                                                        Fecha apertura</label>
                                                    <div class='input-group date' id='datetimepicker2' runat="server">
                                                        <input type='text' class="form-control" id='date_apertura' runat="server" />
                                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                                                    <script type="text/javascript">
                                                        $(function () {
                                                            $('#datetimepicker2').datetimepicker({
                                                                locale: 'es'
                                                            });
                                                        });
                                                    </script>
                                                    <script type="text/javascript">
                                                        $(document).ready(function (e) {
                                                            //Attach change eventhandler
                                                            $('#datetimepicker2').on('change.bfhdatepicker', function (e) {
                                                                //Assign the value to Hidden Variable
                                                                $('#date1').val($('#datetimepicker2').val());
                                                            });
                                                        });
                                                    </script>
                                                    <script type="text/javascript">
                                                        $(document).ready(function () {
                                                            //Assign the value to Hidden Variable on page load
                                                            $('#date1').val($('#datetimepicker2').val());

                                                            //Attach change eventhandler
                                                            $('#datetimepicker2').on('change.bfhdatepicker', function (e) {
                                                                //Assign the value to Hidden Variable
                                                                $('#date1').val($('#datetimepicker2').val());
                                                            });
                                                        });
                                                    </script>
                                                    <input id="date1" type="hidden" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-lg-4">
                                                <div class="form-group">
                                                    <label>
                                                        Próxima producción</label>
                                                    <div class='input-group date' id='datetimepicker_prox' runat="server">
                                                        <input type='text' class="form-control" id='date_prox' runat="server" />
                                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                                                    <script type="text/javascript">
                                                        $(function () {
                                                            $('#datetimepicker_prox').datetimepicker({
                                                                locale: 'es'
                                                            });
                                                        });
                                                    </script>
                                                    <script type="text/javascript">
                                                        $(document).ready(function (e) {
                                                            //Attach change eventhandler
                                                            $('#datetimepicker_prox').on('change.bfhdatepicker', function (e) {
                                                                //Assign the value to Hidden Variable
                                                                $('#date2').val($('#datetimepicker_prox').val());
                                                            });
                                                        });
                                                    </script>
                                                    <script type="text/javascript">
                                                        $(document).ready(function () {
                                                            //Assign the value to Hidden Variable on page load
                                                            $('#date2').val($('#datetimepicker_prox').val());

                                                            //Attach change eventhandler
                                                            $('#datetimepicker_prox').on('change.bfhdatepicker', function (e) {
                                                                //Assign the value to Hidden Variable
                                                                $('#date2').val($('#datetimepicker_prox').val());
                                                            });
                                                        });
                                                    </script>
                                                    <input id="date2" type="hidden" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-lg-4">
                                                <div class="form-group">
                                                    <label>
                                                        Abierto por</label>
                                                    <asp:DropDownList ID="encargado" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-4" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>
                                                        Turno</label>
                                                    <asp:DropDownList ID="turno" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-5">
                                    <div class="form-group">
                                        <label>
                                            Imágenes</label>
                                        <div id="myCarousel" class="carousel">
                                            <!-- Indicators -->
                                            <ol class="carousel-indicators">
                                                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                                <li data-target="#myCarousel" data-slide-to="1"></li>
                                                <li data-target="#myCarousel" data-slide-to="2"></li>
                                            </ol>
                                            <!-- Wrapper for slides -->
                                            <div class="carousel-inner">
                                                <div class="item active">
                                                    <img id="img1" runat="server" src="" alt="" style="width: 100%; max-height: 100%; height: 100%;">
                                                </div>
                                                <div class="item">
                                                    <img id="img2" runat="server" src="" alt="" style="width: 100%; max-height: 100%; height: 100%;">
                                                </div>
                                                <div class="item">
                                                    <img id="img3" runat="server" src="" alt="" style="width: 100%; max-height: 100%; height: 100%;">
                                                </div>
                                            </div>
                                            <!-- Left and right controls -->
                                            <a class="left carousel-control" href="#myCarousel" data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                                href="#myCarousel" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span><span class="sr-only">Next</span> </a>
                                        </div>
                                    </div>
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-green">
                        <div class="panel-heading">
                            <h1 class="panel-title">
                                <h2>Reparación</h2>
                            </h1>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    <label>General</label>
                                    <div class="well well-sm" style="background-color: lavender">
                                        <div class="form-group">
                                            <label>Seleccionar personal</label><br />
                                            <asp:DropDownList ID="lista_realizadoPor" runat="server" CssClass="form-control" Visible="false">
                                            </asp:DropDownList>
                                            <div class="btn-group" style="width: 100%">
                                                <button id="AgregarAsignado" type="button" class="btn btn-sm btn-primary" style="width: 50%" runat="server" onserverclick="Asignar_trabajador">Asignación</button>
                                                <button id="AgregarReparado" type="button" class="btn btn-sm btn-success" style="width: 50%" runat="server" onserverclick="Agregar_trabajador">Reparación</button>
                                            </div>
                                            <asp:DropDownList ID="lista_realizadoPorNEW" runat="server" CssClass="form-control" Height="30px"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>
                                                Fecha asignada</label>
                                            <div class='input-group date' id='datetime_ini' runat="server">
                                                <input type='text' class="form-control" id='datetime_ini2' runat="server" />
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                            <script type="text/javascript">
                                                $(function () {
                                                    $('#datetime_ini').datetimepicker({
                                                        locale: 'es'
                                                    });
                                                });
                                            </script>
                                            <script type="text/javascript">
                                                $(document).ready(function (e) {
                                                    //Attach change eventhandler
                                                    $('#datetime_ini').on('change.bfhdatepicker', function (e) {
                                                        //Assign the value to Hidden Variable
                                                        $('#date3').val($('#datetime_ini').val());
                                                    });
                                                });
                                            </script>
                                            <script type="text/javascript">
                                                $(document).ready(function () {
                                                    //Assign the value to Hidden Variable on page load
                                                    $('#date3').val($('#datetime_ini').val());

                                                    //Attach change eventhandler
                                                    $('#datetime_ini').on('change.bfhdatepicker', function (e) {
                                                        //Assign the value to Hidden Variable
                                                        $('#date3').val($('#datetime_ini').val());
                                                    });
                                                });
                                            </script>
                                            <input id="date3" type="hidden" runat="server" />
                                        </div>

                                        <div class="form-group">
                                            <label>
                                                Fecha de reparación</label>
                                            <div class='input-group date' id='datetime_rep' runat="server">
                                                <input type='text' class="form-control" id='datetime_rep2' runat="server" />
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                            <script type="text/javascript">
                                                $(function () {
                                                    $('#datetime_rep').datetimepicker({
                                                        locale: 'es'
                                                    });
                                                });
                                            </script>
                                            <script type="text/javascript">
                                                $(document).ready(function (e) {
                                                    //Attach change eventhandler
                                                    $('#datetime_rep').on('change.bfhdatepicker', function (e) {
                                                        //Assign the value to Hidden Variable
                                                        $('#date4').val($('#datetime_rep').val());
                                                    });
                                                });
                                            </script>
                                            <script type="text/javascript">
                                                $(document).ready(function () {
                                                    //Assign the value to Hidden Variable on page load
                                                    $('#date4').val($('#datetime_rep').val());

                                                    //Attach change eventhandler
                                                    $('#datetime_rep').on('change.bfhdatepicker', function (e) {
                                                        //Assign the value to Hidden Variable
                                                        $('#date4').val($('#datetime_rep').val());
                                                    });
                                                });
                                            </script>
                                            <input id="date4" type="hidden" runat="server" />
                                        </div>

                                        <label>Reparado</label>
                                        <div class="form-group">
                                            <div class="btn-group2 btn-group" data-toggle="buttons">
                                                <label id="terminado" runat="server" class="btn btn2 btn-success2 active">
                                                    <input id="reparado" runat="server" type="checkbox" autocomplete="off" checked>
                                                    <span class="glyphicon glyphicon-ok"></span>
                                                </label>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                                <div class="col-lg-10">
                                    <label>Asignación</label>
                                    <div class="well well-sm" style="background-color: lavender">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <label>Estado</label>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="DropEstado" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">Iniciado</asp:ListItem>
                                                        <asp:ListItem Value="2">En curso</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="form-group" runat="server" visible="false">
                                                    <label>A reparar por</label>
                                                    <asp:DropDownList ID="reparar_por" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>A reparar por</label>
                                                    <div class="input-group br">
                                                        <span class="input-group-btn">
                                                            <a id="BorrarAsignado1" runat="server" onserverclick="Eliminar_trabajador" class="btn btn-default" style="height: 30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="Asignado1" runat="server" Enabled="false" Width="100%" Height="30px"></asp:TextBox>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="col-lg-2" id="ColAsignado2" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>&nbsp</label>
                                                    <div class="input-group br">
                                                        <span class="input-group-btn">
                                                            <a id="BorrarAsignado2" runat="server" onserverclick="Eliminar_trabajador" class="btn btn-default" style="height: 30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="Asignado2" runat="server" Enabled="false" Width="100%" Height="30px"></asp:TextBox>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="col-lg-2" id="ColAsignado3" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>&nbsp</label>
                                                    <div class="input-group br">
                                                        <span class="input-group-btn">
                                                            <a id="BorrarAsignado3" runat="server" onserverclick="Eliminar_trabajador" class="btn btn-default" style="height: 30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="Asignado3" runat="server" Enabled="false" Width="100%" Height="30px"></asp:TextBox>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <label>Horas previstas</label>
                                                    <input id="Horasprevistas" runat="server" class="form-control">
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                        </div>
                                    </div>
                                    <label>Reparación</label>
                                    <div class="well well-sm" style="background-color: lavender">
                                        <div class="row">
                                            <div class="col-lg-7">
                                                <div class="form-group">
                                                    <label>Tareas realizadas</label>
                                                    <textarea id="reparacion" runat="server" class="form-control" rows="2"></textarea>
                                                </div>
                                                <div class="form-group">
                                                    <label>Observaciones</label>
                                                    <textarea id="observaciones" runat="server" class="form-control" rows="2"></textarea>
                                                </div>

                                            </div>
                                            <div class="col-lg-3">
                                                <div class="row">
                                                    <div class="col-lg-8" runat="server">
                                                        <div class="form-group">
                                                            <label>Realizado por</label>
                                                            <div class="input-group br">
                                                                <span class="input-group-btn">
                                                                    <a id="BorrarReparadoPor1" runat="server" onserverclick="Eliminar_trabajador" class="btn btn-default" style="height: 30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                                </span>
                                                                <asp:TextBox ID="ReparadoPor1" runat="server" Enabled="false" Width="100%" Height="30px"></asp:TextBox>
                                                            </div>


                                                            <div class="input-group br">
                                                                <span class="input-group-btn">
                                                                    <a id="BorrarReparadoPor2" runat="server" visible="false" onserverclick="Eliminar_trabajador" class="btn btn-default" style="height: 30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                                </span>
                                                                <asp:TextBox ID="ReparadoPor2" runat="server" Visible="false" Enabled="false" Width="100%" Height="30px"></asp:TextBox>
                                                            </div>


                                                            <div class="input-group br">
                                                                <span class="input-group-btn">
                                                                    <a id="BorrarReparadoPor3" runat="server" visible="false" onserverclick="Eliminar_trabajador" class="btn btn-default" style="height: 30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                                </span>
                                                                <asp:TextBox ID="ReparadoPor3" runat="server" Visible="false" Enabled="false" Width="100%" Height="30px"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-group">
                                                            <label>
                                                                Horas</label>
                                                            <input id="horas1" runat="server" placeholder="0" class="form-control" style="height: 30px" />
                                                            <input id="horas2" runat="server" placeholder="0" visible="false" class="form-control" style="height: 30px" />
                                                            <input id="horas3" runat="server" placeholder="0" visible="false" class="form-control" style="height: 30px" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6"></div>

                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <label>Costes de repuestos (€)</label>
                                                    <input id="TbCostes" runat="server" class="form-control">
                                                    <br />
                                                    <label>Costes totales (€)</label>
                                                    <input id="TbCostesTotales" runat="server" class="form-control" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="BloquePreventivo" runat="server" visible="false" class="well well-sm" style="background-color: lightsteelblue">
                                            <div id="Q1LINEA" class="row" runat="server">
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="TextTipoPreventivo" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" Font-Size="X-Large" BackColor="transparent" BorderColor="Transparent"></asp:TextBox>


                                                    <label>Acciones a realizar</label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q1DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q1INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>

                                                    <asp:TextBox ID="Q1Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="Q2LINEA" class="row" runat="server" visible="false">
                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q2DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q2INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q2Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="Q3LINEA" class="row" runat="server" visible="false">
                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q3DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q3INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q3Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="Q4LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q4DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q4INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q4Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q5LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q5DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q5INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q5Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q6LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q6DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q6INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q6Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q7LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q7DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q7INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q7Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q8LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q8DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q8INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q8Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q9LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q9DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q9INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q9Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q10LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q10DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q10INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q10Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q11LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q11DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q11INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q11Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q12LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q12DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q12INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q12Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q13LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q13DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q13INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q13Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q14LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q14DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q14INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q14Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q15LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q15DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q15INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q15Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q16LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q16DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q16INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q16Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q17LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q17DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q17INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q17Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q18LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q18DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q18INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q18Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q19LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q19DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q19INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q19Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div id="Q20LINEA" class="row" runat="server" visible="false">

                                                <div class="col-sm-1">
                                                    <asp:DropDownList ID="Q20DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">OK</asp:ListItem>
                                                        <asp:ListItem Value="2">NOK</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="Q20INT" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="Q20Text" runat="server" Enabled="false" Width="100%" Height="30px" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-red">
                        <div class="panel-heading">
                            <h1 class="panel-title">
                                <h2>Revisión</h2>
                            </h1>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    <label>Revisado OK</label>
                                    <div class="form-group">

                                        <div class="btn-group2 btn-group" data-toggle="buttons">
                                            <label id="revisado_label" runat="server" class="btn btn2 btn-success2 active">
                                                <input id="revisado" runat="server" type="checkbox" autocomplete="off" checked>
                                                <span class="glyphicon glyphicon-ok"></span>
                                            </label>
                                        </div>
                                    </div>
                                    <label>Revisado NOK</label>
                                    <div class="form-group">
                                        <div class="btn-group2 btn-group" data-toggle="buttons">
                                            <label id="revisadoNOK_label" runat="server" class="btn btn2 btn-success2 active">
                                                <input id="revisadoNOK" runat="server" type="checkbox" autocomplete="off" checked>
                                                <span class="glyphicon glyphicon-ok"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">

                                    <div class="form-group">
                                        <label>Revisado por</label>
                                        <asp:DropDownList ID="revisado_por" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>


                                    <div class="form-group">
                                        <label>
                                            Fecha revisión</label>
                                        <div class='input-group date' id='date_revision' runat="server">
                                            <input type='text' class="form-control" id='date_revision2' runat="server" />
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                        <script type="text/javascript">
                                            $(function () {
                                                $('#date_revision').datetimepicker({
                                                    locale: 'es'
                                                });
                                            });
                                        </script>
                                        <script type="text/javascript">
                                            $(document).ready(function (e) {
                                                //Attach change eventhandler
                                                $('#date_revision').on('change.bfhdatepicker', function (e) {
                                                    //Assign the value to Hidden Variable
                                                    $('#date5').val($('#date_revision').val());
                                                });
                                            });
                                        </script>
                                        <script type="text/javascript">
                                            $(document).ready(function () {
                                                //Assign the value to Hidden Variable on page load
                                                $('#date5').val($('#date_revision').val());

                                                //Attach change eventhandler
                                                $('#date_revision').on('change.bfhdatepicker', function (e) {
                                                    //Assign the value to Hidden Variable
                                                    $('#date5').val($('#date_revision').val());
                                                });
                                            });
                                        </script>
                                        <input id="date5" type="hidden" runat="server" />
                                    </div>

                                </div>
                                <div class="col-lg-8">

                                    <div class="form-group">
                                        <label>Observaciones</label>
                                        <asp:TextBox ID="observaciones_revision" runat="server" TextMode="MultiLine" Width="100%" Height="60px"></asp:TextBox>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>


    </form>
    

</body>

</html>
--%>