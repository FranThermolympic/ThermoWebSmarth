<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReparacionMoldes.aspx.cs" Inherits="ThermoWeb.MANTENIMIENTO.ReparacionMoldes" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Mantenimiento de moldes</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Mantenimiento de moldes
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
<div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat"">
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
                            <button class="btn btn-outline-dark"  runat="server" type="button" id="buttonaddon3" onserverclick="CrearNuevo"><i class="bi bi-file-plus">&nbspNuevo</i></button>
                            <button class="btn btn-outline-dark"  runat="server" type="button" id="buttonaddon2" onserverclick="GuardarParte"><i class="bi bi-sd-card">&nbspGuardar</i></button>
                            <input class="form-control border border-1 border-dark" list="FiltroParte" id="tbBuscar" runat="server" placeholder="Selecciona un parte..." autocomplete="off">
                            <datalist id="FiltroParte" runat="server"  >
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
                                <button class="nav-link" id="nav-profile-tab2" data-bs-toggle="tab" data-bs-target="#nav-profile2" type="button" role="tab" aria-controls="nav-profile2" aria-selected="false" hidden="hidden"><i class="bi bi-bullseye" ></i>&nbsp Molde</button>                   
                                <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile" type="button" role="tab" aria-controls="nav-profile" aria-selected="false"><i class="bi bi-folder2-open"></i>&nbsp Histórico de molde</button>
                            </div>
                        </nav>
                        <div class="tab-content" id="nav-tabContent">
                            <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                                <div class="row" >
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
                                                <div id="progressNOCONFORME" class="progress-bar progress-bar-striped progress-bar-animated bg-danger" runat="server" role="progressbar" visible="false" style="width: 50%; color: black; font-weight: bold">
                                                    Reparación no conforme
                                                </div>
                                                <div id="progressREPARADO" class="progress-bar progress-bar-striped progress-bar-animated bg-success" runat="server" role="progressbar" visible="false" style="width: 100%; color: black; font-weight: bold">
                                                    PARTE CERRADO
                                                </div>
                                            </div>
                                            <div class="card-body bg-primary text-white border border-success rounded-top">
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <h3 class="card-title"><i class="bi bi-chat-left-text me-2"></i>Registro</h3>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <span class="input-group-text bg-transparent border-0 text-white float-end" style="font-size: larger">Próx. producción / Prioridad: </span>
                                                           
                                                    </div>
                                                    <div class="col-lg-5">
                                                        <div class="input-group">
                                                             <input id="TbProxProduccion" runat="server" type="text" class="form-control  border border-secondary" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-sm" disabled="disabled">
                                                            <asp:DropDownList ID="prioridad" runat="server" CssClass="form-select  border border-secondary">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-lg-7">
                                                    
                                                    <h6 class="ms-1 mt-1">Datos de la avería</h6>
                                                    <div class="rounded shadow border border border-secondary" style="background-color: lavender">
                                                        <div class="row">                                                            
                                                            <div class="col-lg-12">
                                                                <h6 class="ms-2 mt-2">Molde:</h6>
                                                                <div class="mb-3 ms-2" style="width: 98%">
                                                                    <input class="form-control border border-secondary" list="tbMoldeListaMoldes" id="tbMoldeNew" runat="server" placeholder="Escribe un molde...">
                                                                    <datalist id="tbMoldeListaMoldes" runat="server">
                                                                    </datalist>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-4">
                                                                <h6 class="ms-2">Tipo de mantenimiento:</h6>
                                                                <asp:DropDownList ID="lista_trabajos" runat="server" CssClass="form-select ms-2 me-2 border border-secondary" Width="98%">
                                                                </asp:DropDownList>
                                                                <div id="LabelTipoPreventivo" runat="server" visible="false">
                                                                    <h6 class="ms-2 mt-2">Tipo de preventivo:</h6>
                                                                    <asp:DropDownList ID="DropTipoPreventivo" runat="server" CssClass="form-select ms-2 me-2 shadow-sm" Width="98%" AutoPostBack="True" OnSelectedIndexChanged="SeleccionPreventivo"></asp:DropDownList>
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
                                                                <button id="Button2" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-primary btn-lg float-end mb-2 me-3"><i class="bi bi-upload"></i></button>
                                                            </div>
                                                            <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <h6 class="ms-1 mt-1">Datos del parte</h6>
                                                    <div class="rounded shadow border border-secondary" style="background-color: lavender">
                                                        <div class="row">
                                                            <div class="col-lg-4">
                                                                <h6 class="ms-2 mt-2">Fecha apertura:</h6>
                                                                <input type="text" class="form-control Add-text ms-2  border border-secondary" id='datetimepicker' runat="server" style="width:98%" autocomplete="off">
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <div class="form-group">
                                                                    <h6 class="ms-2 mt-2">Piloto:</h6>
                                                                    <asp:DropDownList ID="encargado" runat="server" CssClass="form-select ms-2 border border-secondary" Width="98%">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <div class="form-group">
                                                                    <h6 class="mt-2">Ubicación:</h6>
                                                                    <asp:DropDownList ID="ubicacion" runat="server" Width="98%" CssClass="form-select mb-2 me-2  border border-secondary">
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
                                                                    <asp:HyperLink ID="img1" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" Target="_new" runat="server" />
                                                                    
                                                                </div>
                                                                <div class="carousel-item">
                                                                    <%--<img id="img2" class="d-block w-100" runat="server" src="../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" alt="">--%>
                                                                    <asp:HyperLink ID="img2" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" Target="_new" runat="server"  />
                                                                    
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
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-2 mt-2">
                                                                <h6 class="ms-2">Tipo de reparación</h6>
                                                                <asp:DropDownList ID="lista_reparacion" runat="server" Class="form-select border border-secondary ms-2" Width="98%">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-2 mt-2">
                                                                <div class="form-group ms-2"  runat="server" visible="false">
                                                                    <h6>A reparar por</h6>
                                                                    <asp:DropDownList ID="reparar_por" runat="server" Class="form-select border border-secondary" Width="98%">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="form-group ms-2">
                                                                    <h6>A reparar por</h6>
                                                                    <div class="input-group mb-3 shadow-sm " style="width:98%">
                                                                        <button class="btn btn-outline-secondary" id="BorrarAsignado1" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                        <input id="Asignado1" runat="server" disabled="disabled" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-2 mt-2" id="ColAsignado2" runat="server" visible="false">
                                                                <div class="form-group">
                                                                    <h6>&nbsp</h6>
                                                                    <div class="input-group mb-3  border border-secondary ms-2" style="width:98%">
                                                                        <button class="btn btn-outline-secondary" id="BorrarAsignado2" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                        <input id="Asignado2" runat="server" disabled="disabled" height="30px" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-2 mt-2" id="ColAsignado3" runat="server" visible="false">
                                                                <div class="form-group">
                                                                    <h6>&nbsp</h6>
                                                                    <div class="input-group mb-3  border border-secondary ms-2" style="width:98%">
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
                                                                            <div class="input-group mb-1 shadow-sm ms-2" style="width:98%">
                                                                                <button class="btn btn-outline-secondary " id="BorrarReparadoPor1" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                                <input id="ReparadoPor1" runat="server" disabled="disabled" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="input-group mb-1 shadow-sm ms-2"  style="width:98%">
                                                                                <button class="btn btn-outline-secondary" id="BorrarReparadoPor2" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                                <input id="ReparadoPor2" runat="server" disabled="disabled" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="input-group mb-1 shadow-sm ms-2" style="width:98%">
                                                                                <button class="btn btn-outline-secondary" id="BorrarReparadoPor3" runat="server" onserverclick="Gestionar_trabajadores" type="button"><i class="bi bi-x"></i></button>
                                                                                <input id="ReparadoPor3" runat="server" disabled="disabled" type="text" class="form-control border border-secondary" value="-" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                                                            </div>
                                                                        </div>


                                                                    </div>


                                                                    <div class="col-lg-4">
                                                                        <div class="form-group">
                                                                            <h6 class="ms-2">Horas</h6>
                                                                            <input id="horas1" runat="server" placeholder="0" style="width:98%" class="form-control border border-secondary shadow-sm mb-1 ms-2" />
                                                                            <input id="horas2" runat="server" placeholder="0" style="width:98%" visible="false" class="form-control border border-secondary shadow-sm mb-1 ms-2" />
                                                                            <input id="horas3" runat="server" placeholder="0" style="width:98%" visible="false" class="form-control border border-secondary shadow-sm mb-1 ms-2" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-2  mt-2">
                                                                <div class="form-group me-2">
                                                                    <h6 class="ms-2">Costes de repuestos (€)</h6>
                                                                    <input id="TbCostes" runat="server" class="form-control border border-secondary mb-2 ms-2" style="width:98%">

                                                                    <h6 class="ms-2">Costes totales (€)</h6>
                                                                    <input id="TbCostesTotales" runat="server" class="form-control border border-secondary  ms-2 mb-2" style="width:98%" disabled>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="BloquePreventivo" runat="server" visible="false" class="well well-sm rounded" style="background-color: lightsteelblue">
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
                                                            <asp:DropDownList ID="revisado_por" runat="server" CssClass="form-select  border border-secondary" style="width: 97%">
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
                                                    <asp:LinkButton ID="button2" CssClass="btn btn-outline-dark" Font-Size="Large" runat="server" CommandName="Redirect" CommandArgument='<%#Eval("IdReparacionMolde")%>'><i class="bi bi-file-post"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parte">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParte" runat="server" Font-Size="Large" Font-Bold="true" Text='<%#Eval("IdReparacionMolde") %>' /><br />
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
                                                    <asp:Label ID="lblReparacion" runat="server" Text='<%#Eval("Reparacion ") %>' />
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
    
    <%-- -
    <div class="d-flex" id="wrapper">
        <div class="flex-shrink-0 p-3 bg-dark shadow-sm" style="width: 240px;">
            <a class="btn btn btn-outline-primary border-0" href="MantenimientoIndex.aspx" role="button" style="width: 100%; color: white; text-align: start"><i class="bi bi-building me-2"></i>Página principal</a><br />
            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-tools me-md-2"></i>Partes de trabajo    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="../MANTENIMIENTO/ReparacionMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Moldes</a></div>
            </div>

            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-list-check me-md-2"></i>Acciones abiertas <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample2">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMaquina.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Moldes</a></div>
            </div>
            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-journals me-md-2"></i>Informes    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample3">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de moldes</a><br />
                </div>
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="InformePerifericos.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de periféricos</a></div>
            </div>
            <i class="bi bi-journals me-md-2"></i>
        </div>
        <div class="flex-fill" id="page-content-wrapper">
            posición original
        </div>

    </div>
    --%>
</div>

</asp:Content>



