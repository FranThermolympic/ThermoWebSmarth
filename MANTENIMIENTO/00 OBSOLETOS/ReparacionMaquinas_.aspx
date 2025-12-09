<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReparacionMaquinas_.aspx.cs" Inherits="ThermoWeb.MANTENIMIENTO.ReparacionMaquinas" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Reparación de máquinas y periféricos</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Reparación de máquinas
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex" id="wrapper">
        <div class="flex-shrink-0 p-3 bg-dark shadow-sm" style="width: 240px;">
            <a class="btn btn btn-outline-primary border-0" href="MantenimientoIndex.aspx" role="button" style="width: 100%; color: white; text-align: start"><i class="bi bi-building me-2"></i>Página principal</a><br />              
            
            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-tools me-md-2"></i>Partes de trabajo    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample">
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="http://facts4-srv/oftecnica/ReparacionMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration:none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Máquinas</a><br /></div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="http://facts4-srv/oftecnica/ReparacionMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration:none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Moldes</a></div>
            </div>

            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
               <i class="bi bi-list-check me-md-2"></i>Acciones abiertas <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample2">
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMaquina.aspx" role="button" style="width: 90%; text-align: start; text-decoration:none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Máquinas</a><br /></div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration:none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Moldes</a></div>
            </div>
            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-journals me-md-2"></i>Informes    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample3">
               <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration:none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de moldes</a><br /></div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration:none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de máquinas</a><br /></div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="InformePerifericos.aspx" role="button" style="width: 90%; text-align: start; text-decoration:none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de periféricos</a></div>
            </div>
            <i class="bi bi-journals me-md-2"></i>
        </div>
        <div class="flex-fill" id="page-content-wrapper">
            <div class="row mt-3">
                <div class="col-lg-8"></div>
                <div class="col-lg-4">
                    <div class="input-group">
                        <input class="form-control" list="FiltroMaquina" id="tbBuscarMaquina" runat="server" placeholder="Selecciona una máquina...">
                        <datalist id="FiltroMaquina" runat="server">
                        </datalist>
                        <button class="btn btn-outline-dark me-md-3" type="button" runat="server" onserverclick="BuscarMaquinainforme">Filtrar</button>
                    </div>
                </div>
            </div>
            <div class="nav nav-pills me-3 " id="v-pills-tab" role="tablist">
                <br />
                <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2 me-2"></i>Parte de reparación</button>
                <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-textarea me-2"></i>Histórico</button>
                <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-book-half me-2"></i>Vínculos</button>
                <button class="nav-link" id="PILLMAQPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab4" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-book-half me-2"></i>Histórico preventivos</button>
            </div>
            <div class="tab-content col-12" id="v-pills-tabContent">
                <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                     <form id="idform" runat="server">
    <div id="wrapper">
        <!-- Navigation -->
      
        <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <img id="imgLogo" src="imagenes/thermo.png" alt="Thermolympic" runat="server" align="left" vspace="2"/>
                <a class="navbar-brand" href="index.aspx">Oficina técnica</a>
            </div>

            <!-- Top Menu Items -->
            <ul class="nav navbar-right top-nav">
             

                
                <li class="dropdown">
               
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-bell"></i> <b class="caret"></b></a>
                    <ul id="pendientes" runat="server" class="dropdown-menu message-dropdown">                        
                      
                    </ul>                    
                </li>
                
                <li class="dropdown">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-user"></i> Thermolympic <b class="caret"></b></a>
                    <ul id="info" runat="server" class="dropdown-menu">
                       
                    </ul>
                </li>
            </ul>
            <!-- Sidebar Menu Items - These collapse to the responsive navigation menu on small screens -->
            <div class="collapse navbar-collapse navbar-ex1-collapse">
                <ul class="nav navbar-nav side-nav">
                    <li>
                        <a href="javascript:;" data-toggle="collapse" data-target="#Ul1"><i class="fa fa-fw fa-wrench"></i> Mantenimiento <i class="fa fa-fw fa-caret-down"></i></a>
                        <ul id="Ul1" class="collapse">
                            <li>
                                <a href="ReparacionMoldes.aspx">Reparación moldes</a>
                            </li>
                            <li>
                                <a href="ReparacionMaquinas.aspx">Reparación máquinas</a>
                            </li>
                            <%--<li>
                                <a href="HorasTaller.aspx">Horas taller</a>
                            </li>
                            <%--<li>
                                <a href="index.aspx">Ficha molde</a>
                            </li>--%>
                        </ul>
                    </li>
                      <li>
                        <a href="javascript:;" data-toggle="collapse" data-target="#Ul3"><i class="fa fa-fw fa-list"></i> Acciones abiertas <i class="fa fa-fw fa-caret-down"></i></a>
                        <ul id="Ul3" class="collapse">
                            <li>
                                <a href="EstadoReparacionesMoldes.aspx">Acciones en moldes</a>
                            </li>
                            <li>
                                <a href="EstadoReparacionesMaquina.aspx">Acciones en máquinas</a>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href="javascript:;" data-toggle="collapse" data-target="#Ul2"><i class="fa fa-fw fa-edit"></i> Informes <i class="fa fa-fw fa-caret-down"></i></a>
                        <ul id="Ul2" class="collapse">
                            <li>
                                <a href="InformeMoldes.aspx">Informes de moldes</a>
                            </li>
                            <li>
                                <a href="InformeMaquinas.aspx">Informes de máquinas</a>
                            </li>
                            <li>
                                <a href="GestionPerifericos.aspx">Informes de periféricos</a>
                            </li>
                            <li>
                                <a href="InformeHorasMantenimiento.aspx">Horas y costes de mantenimiento por mes</a>
                            </li>     
                  <%--          <li>
                                <a href="#">Proveedores</a>
                            </li>--%>
                        </ul>
                    </li>
                    <!--<li class="active">
                        <a href="index.html"><i class="fa fa-fw fa-dashboard"></i> Dashboard</a>
                    </li>
                    <li>
                        <a href="charts.html"><i class="fa fa-fw fa-bar-chart-o"></i> Charts</a>
                    </li>
                    <li>
                        <a href="tables.html"><i class="fa fa-fw fa-table"></i> Tables</a>
                    </li>
                    <li>
                        <a href="forms.html"><i class="fa fa-fw fa-edit"></i> Forms</a>
                    </li>
                    <li>
                        <a href="bootstrap-elements.html"><i class="fa fa-fw fa-desktop"></i> Bootstrap Elements</a>
                    </li>
                    <li>
                        <a href="bootstrap-grid.html"><i class="fa fa-fw fa-wrench"></i> Bootstrap Grid</a>
                    </li>
                    <li>
                        <a href="javascript:;" ><i class="fa fa-fw fa-arrows-v"></i> Dropdown <i class="fa fa-fw fa-caret-down"></i></a>
                        <ul id="demo" class="collapse">
                            <li>
                                <a href="#">Dropdown Item</a>
                            </li>
                            <li>
                                <a href="#">Dropdown Item</a>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href="blank-page.html"><i class="fa fa-fw fa-file"></i> Blank Page</a>
                    </li>
                    <li>
                        <a href="index-rtl.html"><i class="fa fa-fw fa-dashboard"></i> RTL Dashboard</a>
                    </li>-->
                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </nav>
        <div id="page-wrapper">
            <div id="Div1" class="container-fluid" runat="server">
                 <script type="text/javascript">
                     function ValidadoDoble() {
                         alert("Se ha producido un error. La reparación está validada como OK y como NOK al mismo tiempo.");
                     }
                 </script>
                 <script type="text/javascript">
                      function ValidadoNREP() {
                         alert("Se ha producido un error. No se puede validar un parte que aun no ha sido reparado.");
                      }
                 </script>
                <!-- Page Heading -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="alert panel_title_grey">
                            <h1 class="page-header">
                                Revisión y mantenimiento de máquinas
                            </h1>
                        </div>
                        <ol class="breadcrumb">
                            <li><i class="fa fa-dashboard"></i><a href="index.aspx">Mantenimiento</a> </li>
                            <li class="active"><i class="fa fa-edit"></i>Reparación de máquinas </li>
                        </ol>
                    </div>
                </div>
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
                    <%-- <button id="nuevo" runat="server" onserverclick="crearNuevo" type="button" class="btn btn-default">
                                <span style="margin-right: 8px" class="glyphicon glyphicon-file"></span>Nuevo
                            </button>
                            <button id="guardar" runat="server" onserverclick="guardarParte" type="button" class="btn btn-info">
                                <span style="margin-right: 8px" class="glyphicon glyphicon-floppy-disk"></span>Guardar
                            </button>
                            <button id="btnBuscar" runat="server" onserverclick="buscarParte" type="button" class="btn btn-default">
                                <span style="margin-right: 8px" class="glyphicon glyphicon-search"></span>Buscar
                            </button>--%>
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
                    <%-- <button id="guardar" runat="server" onserverclick="guardarParte" type="button" class="btn btn-info">
                                        <span class="glyphicon glyphicon-floppy-disk"></span> Guardar
                                    </button>--%>
                </div>
                 
                <div class="row">
                    <div class="col-lg-12">
                        <div class="progress">
                            <div id="progressERROR" class="progress-bar progress-bar-danger" runat="server" role="progressbar" visible="false" style="width:100%">
                                ¡Parte con errores de validación!
                            </div>
                            <div id="progressABIERTO" class="progress-bar progress-bar-warning" runat="server" role="progressbar" style="width:25%">
                                Parte abierto, sin acciones registradas
                            </div>
                            <div id="progressABIERTOINI" class="progress-bar progress-bar-info" runat="server" role="progressbar" style="width:35%">
                                Parte abierto, iniciadas acciones
                            </div>
                            <div id="progressPENDIENTE" class="progress-bar progress-bar-info" runat="server" role="progressbar" visible="false" style="width:50%">
                                Reparado, pendiente de validar
                            </div>
                            <div id="progressNOCONFORME" class="progress-bar progress-bar-danger" runat="server" role="progressbar" visible="false" style="width:50%">
                                Reparación no conforme
                            </div>
                            <div id="progressREPARADO" class="progress-bar progress-bar-success" runat="server" role="progressbar" visible="false" style="width:100%">
                                Reparado
                            </div>
                        </div>
          </div>
                </div>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h1 class="panel-title">
                            <h2>
                                Registro
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
                                        <div class="form-group" style="text-align:right">
                                            <label>Prioridad</label>
                                            <asp:DropDownList ID="prioridad" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div> 
                                <label>Datos de la avería</label>
                                <div class="well well-sm"  style="background-color:lavender">
                                <div class="row">
                                    <div class="col-lg-5">
                                        <div class="form-group" >
                                            <label>
                                                Inyectoras y asociados</label>
                                            <asp:DropDownList ID="lista_maquinas" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>
                                                Periféricos</label>
                                            <asp:DropDownList ID="lista_perifericos" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>
                                                Instalaciones</label>
                                            <asp:DropDownList ID="instalacion" runat="server" CssClass="form-control">
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
                                        <div class="col-lg-5"><label>Insertar imágenes</label><asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload></div>
                                        <div class="col-lg-5"><label>&nbsp</label><asp:FileUpload ID="FileUpload2" runat="server"></asp:FileUpload></div>
                                        <div class="col-lg-2"><label>&nbsp</label><button id="Button1" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-primary"><span class="glyphicon glyphicon-upload"></span>  Cargar fotos</button></div> 
                                        <asp:FileUpload ID="FileUpload3" runat="server" Visible="false"></asp:FileUpload>
                                                <%--<asp:Button ID="UploadButton" Text="Cargar" OnClick="insertar_foto" runat="server">
                                                 </asp:Button>--%>
                                         <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
                                    </div>
                                   
                                </div>
                                <label>Datos del parte</label>
                                <div class="well well-sm" style="background-color:lavender">
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
                                                <img id="img1" runat="server" src="" alt="" style="width: 100%; max-height: 100%;
                                                    height: 100%;">
                                            </div>
                                            <div class="item">
                                                <img id="img2" runat="server" src="" alt="" style="width: 100%; max-height: 100%;
                                                    height: 100%;">
                                            </div>
                                            <div class="item">
                                                <img id="img3" runat="server" src="" alt="" style="width: 100%; max-height: 100%;
                                                    height: 100%;">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <a class="left carousel-control" href="#myCarousel" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>
                                    </div>
                                </div>
                                <%--<div class="row-fluid">
                                <div class="fileupload fileupload-new" data-provides="fileupload"><input type="hidden">
                                    <div class="input-append">
                                        <div class="uneditable-input span2" runat="server" id="statment1">
                                            <i class="icon-file fileupload-exists"></i> 
                                            <span class="fileupload-preview" style=""></span>
                                        </div>
                                        <span class="btn btn-file"><span class="fileupload-new">Select file</span
                                        <span class="fileupload-exists">Change</span><input id="myFile" type="file" runat="server">
                                        </span>
                                        <a href="#" class="btn fileupload-exists" data-dismiss="fileupload" >Remove</a>
                                    </div>
                                  </div>
                               </div>--%>
                                <%--<input id="input-1" type="file" class="file">--%>
                                <%--<button id="boton_foto" runat="server" type="button" onserverclick="insertar_foto" class="btn">
                                    <span style="margin-right: 8px" class="glyphicon glyphicon-camera"></span>Insertar foto
                                </button>--%>
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
                                        <div class="well well-sm" style="background-color:lavender">
                                            <div class="form-group">
                                                            <label>Seleccionar personal</label><br />
                                                            <asp:DropDownList ID="lista_realizadoPor" runat="server" CssClass="form-control" Visible="false">
                                                            </asp:DropDownList>
                                                    <div class="btn-group" style="width:100%">
                                                                <button Id="AgregarAsignado" type="button" class="btn btn-sm btn-primary" style="width:50%"  runat="server" onserverclick="Asignar_trabajador">Asignación</button>
                                                                <button Id="AgregarReparado" type="button" class="btn btn-sm btn-success" style="width:50%"  runat="server" onserverclick="Agregar_trabajador">Reparación</button>
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
                                   <div class="well well-sm" style="background-color:lavender">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <label>Estado</label>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="DropEstado" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="0">-</asp:ListItem>
                                                        <asp:ListItem Value="1">Iniciado</asp:ListItem>
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
                                                                <a Id="BorrarAsignado1" runat="server" onserverclick="Eliminar_trabajador"  class="btn btn-default" style="height:30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                            </span>
                                                            <asp:TextBox ID="Asignado1" runat="server" Enabled="false"  Width="100%" Height="30px"></asp:TextBox> 
                                                        </div>
                                                
                                               
                                                 </div>                                    
                                                </div>
                                            <div class="col-lg-2" id="ColAsignado2" runat="server" visible="false">
                                              <div class="form-group">
                                                        <label>&nbsp</label>
                                                        <div class="input-group br">
                                                            <span class="input-group-btn">
                                                                <a Id="BorrarAsignado2" runat="server" onserverclick="Eliminar_trabajador"  class="btn btn-default" style="height:30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                            </span>
                                                            <asp:TextBox ID="Asignado2" runat="server" Enabled="false"  Width="100%" Height="30px"></asp:TextBox> 
                                                        </div>
                                                
                                               
                                                 </div>                                    
                                                </div>
                                            <div class="col-lg-2" id="ColAsignado3" runat="server" visible="false">
                                              <div class="form-group">
                                                        <label>&nbsp</label>
                                                        <div class="input-group br">
                                                            <span class="input-group-btn">
                                                                <a Id="BorrarAsignado3" runat="server" onserverclick="Eliminar_trabajador"  class="btn btn-default" style="height:30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                            </span>
                                                            <asp:TextBox ID="Asignado3" runat="server" Enabled="false"  Width="100%" Height="30px"></asp:TextBox> 
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
                                   <div class="well well-sm" style="background-color:lavender">                      
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
                                                            <a Id="BorrarReparadoPor1" runat="server" onserverclick="Eliminar_trabajador"  class="btn btn-default" style="height:30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="ReparadoPor1" runat="server" Enabled="false"  Width="100%" Height="30px"></asp:TextBox> 
                                                    </div>
                                                
                                               
                                                    <div class="input-group br">
                                                        <span class="input-group-btn">
                                                            <a Id="BorrarReparadoPor2" runat="server" visible="false" onserverclick="Eliminar_trabajador" class="btn btn-default" style="height:30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="ReparadoPor2" runat="server" visible="false"  Enabled="false"  Width="100%" Height="30px"></asp:TextBox> 
                                                    </div>
                                             
                                                
                                                    <div class="input-group br">
                                                        <span class="input-group-btn">
                                                            <a Id="BorrarReparadoPor3" runat="server" visible="false" onserverclick="Eliminar_trabajador" class="btn btn-default" style="height:30px"><span class="glyphicon glyphicon-remove"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="ReparadoPor3" runat="server" visible="false"  Enabled="false"  Width="100%" Height="30px"></asp:TextBox> 
                                                    </div>
                                                </div>
                                            
                                                    </div>
                                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label>
                                                    Horas</label>
                                                <input id="horas1" runat="server" placeholder="0" class="form-control" style="height:30px"/>
                                                <input id="horas2" runat="server" placeholder="0"  visible="false"  class="form-control" style="height:30px" />
                                                <input id="horas3" runat="server" placeholder="0" visible="false"   class="form-control" style="height:30px" />
                                            </div>
                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6"></div>
                                                        
                                                    </div>
                                                </div>
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                        <label> Costes de repuestos (€)</label>
                                                          <input id="TbCostes" runat="server" class="form-control">
                                                     <br />
                                                        <label> Costes totales (€)</label>
                                                          <input id="TbCostesTotales" runat="server" class="form-control" disabled>
                                                 </div>
                                            </div>
                                       </div>
                                       <div id="BloquePreventivo" runat="server" visible="false" class="well well-sm" style="background-color:lightsteelblue">
                                        <div ID="Q1LINEA" class="row" runat="server">
                                            <div class="col-sm-12">
                                                        <asp:TextBox ID="TextTipoPreventivo" runat="server" enabled="false"  Width="100%" Height="30px" Font-Bold="true" Font-Size="X-Large" BackColor="transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        
                                       
                                            <label>Acciones a realizar</label></div>
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q1DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q1INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>    
                                                        
                                                            <asp:TextBox ID="Q1Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                                  </div>
					                    <div ID="Q2LINEA" class="row" runat="server" visible="false">                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q2DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q2INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox> 
                                                            <asp:TextBox ID="Q2Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"   BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>                                                
                                                  </div>
                                        <div ID="Q3LINEA" class="row" runat="server" visible="false">                                           
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q3DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q3INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q3Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"   BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>                                     
                                                  </div>
                                        <div ID="Q4LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q4DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q4INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q4Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"   BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                                  

                                                  </div>
                                        <div ID="Q5LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q5DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q5INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q5Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"   BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                               

                                                  </div>
                                        <div ID="Q6LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q6DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q6INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q6Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"   BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                                

                                                  </div>
                                        <div ID="Q7LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q7DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q7INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q7Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"   BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                                   

                                                  </div>
                                        <div ID="Q8LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q8DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q8INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q8Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                                

                                                  </div>
                                        <div ID="Q9LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q9DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q9INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q9Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"   BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                                        

                                                  </div>
                                        <div ID="Q10LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q10DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q10INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q10Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" ></asp:TextBox>    
                                                        </div>
                                             

                                                  </div>
                                        <div ID="Q11LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q11DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q11INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q11Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                          

                                                  </div>
                                        <div ID="Q12LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q12DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q12INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q12Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                                   

                                                  </div>
                                        <div ID="Q13LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q13DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q13INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q13Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                           

                                                  </div>
                                        <div ID="Q14LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q14DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q14INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q14Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                              

                                                  </div>
                                        <div ID="Q15LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q15DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q15INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q15Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                               

                                                  </div>
                                        <div ID="Q16LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q16DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q16INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q16Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                               

                                                  </div>
                                        <div ID="Q17LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q17DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q17INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q17Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                             

                                                  </div>
                                        <div ID="Q18LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q18DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q18INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q18Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                             

                                                  </div>
                                        <div ID="Q19LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q19DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q19INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q19Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
                                                        </div>
                                                     

                                                  </div>
                                        <div ID="Q20LINEA" class="row" runat="server" visible="false">
                                            
                                                        <div class="col-sm-1">
                                                            <asp:DropDownList ID="Q20DROP" Width="100%" runat="server" CssClass="form-control" Height="30px">
                                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                                <asp:ListItem Value="1">OK</asp:ListItem>
                                                                <asp:ListItem Value="2">NOK</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-sm-11">
                                                            <asp:TextBox ID="Q20INT" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="Q20Text" runat="server" Enabled="false"  Width="100%" Height="30px" Font-Bold="true"  BackColor="Transparent" BorderColor="Transparent"></asp:TextBox>    
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
                                    <asp:TextBox ID="observaciones_revision" runat="server" TextMode="MultiLine"  Width="100%" Height="60px"></asp:TextBox> 
                                    </div>
                                  
                                </div>
                          
                        </div>
                        </div>
                </div>

            </div>
        </div>
               
            
            <!-- /.row -->
       
        <!-- /.container-fluid -->
  
    </form>
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