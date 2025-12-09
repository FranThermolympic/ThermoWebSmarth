<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="LIBPanelEstado.aspx.cs" Inherits="ThermoWeb.LIBERACIONES.PANELLIBERACIONES"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>
<meta http-equiv="Page-Exit" content="blendTrans(Duration=0.5)" />
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estado de liberaciones</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
         <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
         <script src="js/json2.js" type="text/javascript"></script>
         <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
         <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
         <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <nav class="navbar navbar-inverse">
      <div class="container-fluid">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>                        
          </button>
          <a class="navbar-brand" href="../index.aspx">Thermolympic S.L.</a>
        </div>
        <div class="collapse navbar-collapse" id="myNavbar">
          <ul class="nav navbar-nav"> 
                <li><a href="GP12.aspx" target="_blank">Iniciar revisión</a></li>
                <li><a href="PrevisionGP12.aspx" target="_blank">Planificación de cargas</a></li>
                <li><a href="GP12ReferenciasEstado.aspx" target="_blank">Estado de referencias</a></li>
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">Informes de revisión
                    <span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <li><a href="GP12Historico.aspx" target="_blank">Últimas revisiones</a></li>
                        <li><a href="GP12HistoricoReferencia.aspx" target="_blank">Detalles</a></li>
                        <li><a href="GP12KPI.aspx" target="_blank">Cuadro de mando</a></li>
                    </ul>
                </li>      
          </ul>
        </div>
      </div>
    </nav>
    <h1>&nbsp;&nbsp;&nbsp; Parte de revisión</h1>

    <div class="container-fluid">
    <div class="row"><%--panel por nave--%>
    <div class="col-lg-4">
         <div id="Nave5" class="panel panel-primary" runat="server">
                  <div class="panel-heading">
                       <h3 class="panel-title">
                       <i class="fa fa-list-ul fa-fw"></i>Nave 5</h3>
                   </div>
                   <div class="panel-body">
                   <div class="col-lg-6">
                        <div class="table-responsive" style="width:100%">
                                    <table>
                                        <tr>
                                            <th colspan="2">
                                                <asp:TextBox ID="Maquina34" runat="server" Style="text-align: center" Enabled="false" Width="100%" >Máquina 34</asp:TextBox>
                                            </th>    
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="LIBPROD34" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="CALPROD34" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="BTNPROD34" type="button" runat="server" onserverclick = "CargaDefecto" class="btn btn-SM">PRODUCCION</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="BTNCAL34" type="button" runat="server"  onserverclick = "CargaDefecto" class="btn btn-SM">CALIDAD</button>
                                            </div>
                                        </div>
                            </div>
                            <br />
                            <div class="table-responsive" style="width:100%">
                                    <table>
                                        <tr>
                                            <th colspan="2">
                                                <asp:TextBox ID="Maquina35" runat="server" Style="text-align: center" Enabled="false" Width="100%" >Máquina 35</asp:TextBox>
                                            </th>    
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="LIBPROD35" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="CALPROD35" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="BTNPROD35" type="button" runat="server" onserverclick = "CargaDefecto" class="btn btn-SM">PRODUCCION</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="BTNCAL35" type="button" runat="server"  onserverclick = "CargaDefecto" class="btn btn-SM">CALIDAD</button>
                                            </div>
                                        </div>
                            </div>
                            <br />
                            <div class="table-responsive" style="width:100%">
                                    <table>
                                        <tr>
                                            <th colspan="2">
                                                <asp:TextBox ID="Maquina43" runat="server" Style="text-align: center" Enabled="false" Width="100%" >Máquina 43</asp:TextBox>
                                            </th>    
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="LIBPROD43" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="CALPROD43" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="BTNPROD43" type="button" runat="server" onserverclick = "CargaDefecto" class="btn btn-SM">PRODUCCION</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="BTNCAL43" type="button" runat="server"  onserverclick = "CargaDefecto" class="btn btn-SM">CALIDAD</button>
                                            </div>
                                        </div>
                            </div>


                          </div>
                      
        <div class="col-lg-6">
                     
                        <div class="table-responsive" style="width:100%">
                                    <table>
                                        <tr>
                                            <th colspan="2">
                                                <asp:TextBox ID="Maquina42" runat="server" Style="text-align: center" Enabled="false" Width="100%" >Máquina 42</asp:TextBox>
                                            </th>    
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="LIBPROD42" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="CALPROD42" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="BTNPROD42" type="button" runat="server" onserverclick = "CargaDefecto" class="btn btn-SM">PRODUCCION</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="BTNCAL42" type="button" runat="server"  onserverclick = "CargaDefecto" class="btn btn-SM">CALIDAD</button>
                                            </div>
                                        </div>
                            </div>
                            <br />
                            <div class="table-responsive" style="width:100%">
                                    <table>
                                        <tr>
                                            <th colspan="2">
                                                <asp:TextBox ID="Maquina39" runat="server" Style="text-align: center" Enabled="false" Width="100%" >Máquina 39</asp:TextBox>
                                            </th>    
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="LIBPROD39" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="CALPROD39" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="BTNPROD39" type="button" runat="server" onserverclick = "CargaDefecto" class="btn btn-SM">PRODUCCION</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="BTNCAL39" type="button" runat="server"  onserverclick = "CargaDefecto" class="btn btn-SM">CALIDAD</button>
                                            </div>
                                        </div>
                            </div>
                            <br />
                            <div class="table-responsive" style="width:100%">
                                    <table>
                                        <tr>
                                            <th colspan="2">
                                                <asp:TextBox ID="Maquina33" runat="server" Style="text-align: center" Enabled="false" Width="100%" >Máquina 33</asp:TextBox>
                                            </th>    
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="LIBPROD33" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="CALPROD33" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="BTNPROD33" type="button" runat="server" onserverclick = "CargaDefecto" class="btn btn-SM">PRODUCCION</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="BTNCAL33" type="button" runat="server"  onserverclick = "CargaDefecto" class="btn btn-SM">CALIDAD</button>
                                            </div>
                                        </div>
                                </div>
                          </div>
                      </div>
        </div>
    </div>
    <div class="col-lg-4">
        <div id="Nave4" class="panel panel-primary" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Nave 4</h3>
                        </div>
        </div>
    </div>
    <div class="col-lg-4">
        <div id="Nave3" class="panel panel-primary" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Nave 3</h3>
                        </div>
        </div>
    </div>
    </div>
    <div class="row"><%--panel por footer--%>
    </div>
        <div class="row"><%--cabecera de selección--%>
            <div class="col-lg-3">
                
                </div>
                <div class="col-lg-9">
                    <div id="Div4" class="panel panel-primary" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Detalles</h3>
                        </div>
                        <div class="panel-body">
                       <div class="row">
                          <div class="col-lg-7">
                            <div id="Div5">
                                <div class="table-responsive">
                                    <table>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tituloReferenciaCarga" runat="server" Style="text-align: center" Enabled="false" w>Referencia</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tituloDescripcionCarga" runat="server" Style="text-align: center" Enabled="false">Descripción</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tituloClienteCarga" runat="server" Style="text-align: center" Enabled="false">Cliente</asp:TextBox>
                                            </th> 
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="tbReferenciaCarga" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbDescripcionCarga" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbClienteCarga" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <th>
                                                <asp:TextBox ID="tituloOperarioCarga" runat="server" Style="text-align: center" Enabled="false">Nº Operario</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tituloNombreCarga" runat="server" Style="text-align: center" Enabled="false">Nombre</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tituloEmpresaCarga" runat="server" Style="text-align: center" Enabled="false">Empresa</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tituloMolde" runat="server" Style="text-align: center" Enabled="false" Visible="false">Molde</asp:TextBox>
                                            </th> 
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="tbOperarioCarga" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbOpeNombreCarga" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbEmpresaCarga" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbMolde" runat="server" Style="text-align: center" Enabled="false" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbCosteOPE" runat="server" Style="text-align: center" Enabled="false" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbCostePIEZA" runat="server" Style="text-align: center" Enabled="false" Visible="false"></asp:TextBox>
                                            </td>
                                             <td>
                                                <asp:TextBox ID="tbResponsable" runat="server" Style="text-align: center" Enabled="false" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                            </div>

                            </div><%--final de panel de descripción--%>
                             <div class="col-lg-2">
                                 <div id="Div2">
                                   <div id="alertasinfecha" runat="server" class="alert alert-warning" visible="false" style="text-align:center"><strong>¡Atención!</strong><br /> Estado de control GP12 sin fecha.</div>   
                                   <div id="alertafuera" runat="server" class="alert alert-danger" visible="false" style="text-align:center"><strong>¡Atención!</strong><br /> Estado de control GP12 fuera de plazo.</div>
                                   <div id="alertasinrevision" runat="server" class="alert alert-info" visible="false" style="text-align:center"><strong>¡Atención!</strong><br /> Pieza sin revisión. Selecciona la causa de esta revisión en el desplegable.</div>
                                    <%--<asp:TextBox ID="AlertaRevision" runat="server" Enabled="false" Style="text-align: center" width="100%" Height="100px" Font-Size="Large" BackColor="Red" TextMode="MultiLine"><span class="glyphicon glyphicon-warning-sign"></span>Fuera de plazo de revision</asp:TextBox>--%>
                                 </div>
                             </div>
     
                                <div class="col-lg-3">
                                            <div id="Div6">
                                                <button id="BtnInicioRevision" type="button" runat="server" onserverclick="IniciarRevision" class="btn btn-primary btn-lg" style="width:100%" disabled>Iniciar revisión</button>
                                                <br />
                                                <button id="BtnTerminarRevision" type="button" runat="server" onserverclick="TerminarRevision" class="btn btn-primary btn-lg" style="width:100%" disabled>Terminar revisión</button>
                                             </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      
        <div class="row"><%--panel de revisión--%>
                <div class="col-lg-12">
                    <div id="Div3" class="panel panel-primary" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Revisión</h3>
                        </div>
                        <div class="panel-body">
                        <%--fila común --%>
                        <%--<div class="row">
                      <div class="col-lg-12">
                        <div class="well well-sm">

                            <div id="Div2">
                                <div class="table-responsive">
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="TituloHoraInicio" runat="server" Style="text-align: center" Enabled="false" width="100%">Hora Inicio</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloTotalRevisado" runat="server" Style="text-align: center" Enabled="false" width="100%">Revisadas</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloTotalOK" runat="server" Style="text-align: center" Enabled="false" width="100%">Buenas</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloTotalNOK" runat="server" Style="text-align: center" Enabled="false" width="100%">Defectuosas</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloTotalRetrabajado" runat="server" Style="text-align: center" Enabled="false" width="100%">Retrabajadas</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloHoraFinal" runat="server" Style="text-align: center" Enabled="false" width="100%">Hora final</asp:TextBox>
                                            </th>  
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="HoraInicio" runat="server" Style="text-align: center" Enabled="false"  width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TotalRevisado" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TotalOK" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                             <td>
                                                <asp:TextBox ID="TotalNOK" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TotalRetrabajado" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="HoraFinal" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            </div>
                             </div>
                        </div>--%>
                       <div class="row"><%--tres columnas --%>
                       <div class="col-lg-5">

                       <div id="Declaradefecto" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <i class="fa fa-list-ul fa-fw"></i>Declaraciones</h3>
                                </div>
                            <div class="panel-body">
                               <div class="row">
                                  <div class="col-lg-12">
                                    <asp:TextBox ID="DefectoDeclarado" runat="server" width="100%" Height = "50px" font-size="X-Large" Style="text-align: center" Enabled="false"></asp:TextBox>
                                    <asp:TextBox ID="DefectoCarga" runat="server" width="100%" Height = "50px" font-size="X-Large" Style="text-align: center" Enabled="false" Visible="false"></asp:TextBox>
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-6">
                                    <br />
                                          <div class="input-group">
                                                <span class="input-group-btn">
                                                        <button id="BtnSuma" class="btn btn-lg" runat="server" onserverclick = "RestarPieza" type="button" disabled>-</button>
                                                </span>
                                                <input id="CantidadNOK" runat="server" type="text" class="form-control input-lg" font-size="X-Large" style="text-align: center" placeholder="Cantidad" disabled>
                                                <span class="input-group-btn">
                                                        <button id="BtnResta" class="btn btn-lg" runat="server" onserverclick = "SumarPieza" type="button" disabled>+</button>
                                                </span>
                                          </div>
                                          <br />
                                         
                                    </div>
                                    <div class="well well-sm">
                                   <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="Buenas" type="button" runat="server" onserverclick = "CargaDefecto" class="btn btn-success btn-lg">Buenas</button>
                                            </div>
                                    </div>
                                    <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="Retrabajadas" type="button" runat="server" onserverclick = "CargaDefecto" class="btn btn-info btn-lg">Retrabajadas</button>
                                            </div>
                                    </div>
                                     <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="Defecto01" type="button" runat="server" onserverclick = "CargaDefecto" class="btn btn-danger btn-lg">Falta llenado</button>
                                            </div>
                                           <div class="btn-group" role="group">
                                                    <button id="Defecto02" type="button" runat="server"  onserverclick = "CargaDefecto" class="btn btn-danger btn-lg">Ráfagas</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto03" type="button" runat="server"  onserverclick = "CargaDefecto" class="btn btn-danger btn-lg">Roturas</button>
                                            </div>
                                     </div>
                                     <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto04" type="button"  runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Montaje NOK</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto05" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Deformaciones</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto06" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Etiqueta NOK</button>
                                            </div>
                                     </div>
                                     <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto07" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Burbujas</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto08" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Arrastre material</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto09" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Marca expulsor</button>
                                            </div>
                                     </div>
                                     <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto10" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Quemazos</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto11" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Brillos</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto12" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">M. contaminado</button>
                                            </div>
                                     </div>
                                     <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto13" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Rechupes</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto14" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Color NOK</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto15" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Manchas</button>
                                            </div>
                                     </div>                                    
                                     <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto16" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Rebabas</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto17" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Sólo plástico</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto18" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Sólo goma</button>
                                            </div>
                                     </div>
                                     <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto20" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Electroválvula</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto21" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Grapa: Posición</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto22" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Grapa: Altura</button>
                                            </div>
                                     </div>
                                     <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto23" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Tubo: Deformado</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto24" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Tubo: Mal puesto</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto25" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Mal clipado</button>
                                            </div>
                                     </div>
                                     <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto26" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Suciedad</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto27" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Punzonado NOK</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto19" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-warning btn-lg">Otros</button>
                                            </div>
                                     </div>
                                     <%-- <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button id="Defecto19" type="button" runat="server"  onserverclick = "CargaDefecto"  class="btn btn-danger btn-lg">Otros</button>
                                            </div>
                                     </div>
                                     --%>
                                     </div>
                                     </div>
                                     </div>
                                         </div> 
                            </div>

                            </div>
                       <div class="col-lg-4">
                            <div id="DetallesGP12" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <i class="fa fa-list-ul fa-fw"></i>Detalles de la revisión</h3>
                                </div>
                            <div class="panel-body">
                               <div class="row">
                                  <div class="col-lg-12">
                                    <div id="Div10">
                                        <asp:TextBox ID="TituloRazonRevision" runat="server" Enabled="false" Style="text-align: center" width="100%" Height="35px" Font-Size="Large">Causa de la revisión</asp:TextBox>
                                        <asp:DropDownList ID="RazonRevision" runat="server" Style="text-align: center" Enabled="false" CssClass="form-control" width="100%"></asp:DropDownList>
                                        <asp:TextBox ID="ObservacionesRevision" runat="server" Enabled="false" Style="text-align: center" width="100%" Height="30px"></asp:TextBox>
                                        <%-- <div class="table-responsive">
                                            <table width="100%">
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="DebeSalir" runat="server" Style="text-align: left" Enabled="false" width="100%">  Fecha máx.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="FechaMax" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            </table>
                                        </div>--%>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="DetallesRev" runat="server" Enabled="false" Style="text-align: center" width="100%" Height="35px" Font-Size="Large">Detalles</asp:TextBox>        
                                        <div class="table-responsive">
                                            <table width="100%">
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="TituloLOTE" runat="server" Style="text-align: left" Enabled="false" width="100%" Height="35px">  NºLote</asp:TextBox>
                                                </th>
                                                <td>
                                                    <%-- <asp:TextBox ID="TbLote" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="TbLote" runat="server" Style="text-align: center" Enabled="false" CssClass="form-control" width="100%"></asp:DropDownList>
                                                    <asp:TextBox ID="TbLoteManual" runat="server" visible="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="TituloHoraInicio" runat="server" Enabled="false" width="100%">  Hora de inicio</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="HoraInicio" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="TituloTotalRevisado" runat="server" Enabled="false" width="100%">  Revisadas</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="TotalRevisado" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="TituloTotalOK" runat="server" Enabled="false" width="100%">  Buenas</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="TotalOK" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="TituloTotalNOK" runat="server" Enabled="false" width="100%">  Defectuosas</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="TotalNOK" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="TituloTotalRetrabajado" runat="server" Enabled="false" width="100%">  Retrabajadas</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="TotalRetrabajado" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                    <th>
                                                        <asp:TextBox ID="TituloLOP1" runat="server" Style="text-align: left" Enabled="false" width="100%">  Nº Operario 1</asp:TextBox>
                                                    </th>
                                                    <td>
                                                        <asp:TextBox ID="TbOP1" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <th>
                                                        <asp:TextBox ID="TituloLOP2" runat="server" Style="text-align: left" Enabled="false" width="100%">  Nº Operario 2</asp:TextBox>
                                                    </th>
                                                    <td>
                                                        <asp:TextBox ID="TbOP2" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <th>
                                                        <asp:TextBox ID="TituloLOP3" runat="server" Style="text-align: left" Enabled="false" width="100%">  Nº Operario 3</asp:TextBox>
                                                    </th>
                                                    <td>
                                                        <asp:TextBox ID="TbOP3" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                    </td>
                                                    
                                            </tr>
                                            <tr>
                                                    <th>
                                                        <asp:TextBox ID="TituloLOP4" runat="server" Style="text-align: left" Enabled="false" width="100%">  Nº Operario 4</asp:TextBox>
                                                    </th>
                                                    <td>
                                                        <asp:TextBox ID="TbOP4" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                    </td>
                                                    
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="TituloHoraFinal" runat="server" Enabled="false" width="100%">  Hora de cierre</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="HoraFinal" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                        <asp:TextBox ID="TituloIncidencias" runat="server" Style="text-align: center" Enabled="false" width="100%">Incidencias</asp:TextBox>
                                                </th>
                                                    </tr>
                                                    <tr>
                                                <td colspan="2">
                                                        <asp:TextBox ID="TbIncidencias" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                        <asp:TextBox ID="TituloLObservaciones" runat="server" Style="text-align: center" Enabled="false" width="100%">Observaciones</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                        <asp:TextBox ID="TbObservaciones" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                        <asp:TextBox ID="TituloTiempoRevision" runat="server" Style="text-align: center" Enabled="false" width="100%">Tiempo de revisión (Horas)</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                        <asp:TextBox ID="tbTiempoRevision" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                        <asp:TextBox ID="TituloCosteRevision" runat="server" Style="text-align: center" Enabled="false" width="100%" Visible="false">Coste de la revisión (Tiempo+Scrap)</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                        <asp:TextBox ID="CosteRevision" runat="server" Style="text-align: center" Enabled="false" width="100%" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                          </table>
                                   <br />
                                        </div>
                                        <div class="form-group">
                                        
                                <label>Imagen 1</label>
                                <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                                <br />
                                <label>Imagen 2</label>
                                <asp:FileUpload ID="FileUpload2" runat="server"></asp:FileUpload>
                                <br />
                                <label>Imagen 3</label>
                                <asp:FileUpload ID="FileUpload3" runat="server"></asp:FileUpload>
                                <br />
                                <button id="UploadButton" runat="server" type="button" onserverclick="insertar_foto" class="btn btn-basic" disabled>
                                    Subir imágenes
                                </button>
                                <%--<asp:Button ID="UploadButton" Text="Cargar" OnClick="insertar_foto" runat="server">
                                    </asp:Button>--%>
                                <asp:Label ID="UploadStatusLabel" runat="server">
                                </asp:Label>
                            </div>
                                    </div>

                                    </div><%--final de panel de entradas--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div id="Div7" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <i class="fa fa-list-ul fa-fw"></i>Imágenes</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-12">
                                      <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink1" NavigateUrl="" ImageUrl= "" Target="_new" runat="server"/>
                                          </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="thumbnail">
                                                <asp:HyperLink id="hyperlink2" NavigateUrl="" ImageUrl= "" Target="_new" runat="server"/>
                                        </div>                                         
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="thumbnail">
                                                <asp:HyperLink id="hyperlink3" NavigateUrl="" ImageUrl= "" Target="_new" runat="server"/>
                                        </div>                                         
                                    </div>
                                  </div>

                          <%--final de panel de descripción--%>
                                
                            </div>
                        </div>
                    </div>
                    
                 </div>
         </div><%--final de panel de revisión --%>
         <div class="row"><%--panel de revisión--%>
                <div class="col-lg-12">
                    <div id="Div8" class="panel panel-primary" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Contadores</h3>
                        </div>
                        <div class="panel-body">

                            <div id="Div9">
                                <div class="table-responsive">
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto1" runat="server" Style="text-align: center" Enabled="false" width="100%">Falta llenado</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto2" runat="server" Style="text-align: center" Enabled="false" width="100%">Ráfagas</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto3" runat="server" Style="text-align: center" Enabled="false" width="100%">Roturas</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto4" runat="server" Style="text-align: center" Enabled="false" width="100%">Montaje NOK</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto5" runat="server" Style="text-align: center" Enabled="false" width="100%">Deformaciones</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto6" runat="server" Style="text-align: center" Enabled="false" width="100%">Etiqueta NOK</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto7" runat="server" Style="text-align: center" Enabled="false" width="100%">Burbujas</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto8" runat="server" Style="text-align: center" Enabled="false" width="100%">Arrastre material</asp:TextBox>
                                            </th>    
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto1" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto2" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto3" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                             <td>
                                                <asp:TextBox ID="ContadorDefecto4" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto5" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto6" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto7" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto8" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto9" runat="server" Style="text-align: center" Enabled="false" width="100%">Expulsor marcado</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto10" runat="server" Style="text-align: center" Enabled="false" width="100%">Quemazos</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto11" runat="server" Style="text-align: center" Enabled="false" width="100%">Brillos</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto12" runat="server" Style="text-align: center" Enabled="false" width="100%">Material contaminado</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto13" runat="server" Style="text-align: center" Enabled="false" width="100%">Rechupes</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto14" runat="server" Style="text-align: center" Enabled="false" width="100%">Color NOK</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto15" runat="server" Style="text-align: center" Enabled="false" width="100%">Manchas</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto16" runat="server" Style="text-align: center" Enabled="false" width="100%">Rebabas</asp:TextBox>
                                            </th>    
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto9" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto10" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto11" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                             <td>
                                                <asp:TextBox ID="ContadorDefecto12" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto13" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto14" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto15" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto16" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto17" runat="server" Style="text-align: center" Enabled="false" width="100%">Sólo plástico</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto18" runat="server" Style="text-align: center" Enabled="false" width="100%">Sólo goma</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto20" runat="server" Style="text-align: center" Enabled="false" width="100%">Electroválvula</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto21" runat="server" Style="text-align: center" Enabled="false" width="100%">Grapa: Posición</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto22" runat="server" Style="text-align: center" Enabled="false" width="100%">Grapa: Altura</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto23" runat="server" Style="text-align: center" Enabled="false" width="100%">Tubo: Deformado</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto24" runat="server" Style="text-align: center" Enabled="false" width="100%">Tubo: Mal puesto</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TituloDefecto25" runat="server" Style="text-align: center" Enabled="false" width="100%">Mal clipado</asp:TextBox>
                                            </th> 
              
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto17" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto18" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto20" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto21" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto22" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto23" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto24" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto25" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                             <th>
                                                <asp:TextBox ID="TituloDefecto26" runat="server" Style="text-align: center" Enabled="false" width="100%">Suciedad</asp:TextBox>
                                            </th>
                                             <th>
                                                    <asp:TextBox ID="TituloDefecto27" runat="server" Style="text-align: center" Enabled="false" width="100%">Punzonado NOK</asp:TextBox>
                                             </th> 
                                             <th>
                                                    <asp:TextBox ID="TituloDefecto28" runat="server" Style="text-align: center" Enabled="false" width="100%">Láser ilegible</asp:TextBox>
                                             </th> 
                                             <th>
                                                    <asp:TextBox ID="TituloDefecto19" runat="server" Style="text-align: center" Enabled="false" width="100%">Otros</asp:TextBox>
                                             </th>
                                             <th>
                                                    <asp:TextBox ID="TituloDefecto29" runat="server" Style="text-align: center" Enabled="false" width="100%" Visible ="false">Defecto29</asp:TextBox>
                                             </th>
                                             <th>
                                                    <asp:TextBox ID="TituloDefecto30" runat="server" Style="text-align: center" Enabled="false" width="100%" Visible ="false">Defecto30</asp:TextBox>
                                             </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto26" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto27" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto28" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto19" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto29" runat="server" Style="text-align: center" Enabled="false" width="100%" Visible ="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContadorDefecto30" runat="server" Style="text-align: center" Enabled="false" width="100%" Visible ="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                           
                          <%--final de panel de descripción--%>
                                
                            </div>
                        </div>
                    </div>
                 </div>
         </div>

      </div><%--container--%>
    </form>
</body>
</html>
