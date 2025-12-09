<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FichasParametros_nuevo.aspx.cs"
    Inherits="ThermoWeb.FichasParametros_nuevo" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fichas de parametros</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="js/json2.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <%--<nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
      <div class="container">
        <a class="navbar-brand" href="index.aspx">Thermolympic S.L.</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarResponsive">
          <ul class="navbar-nav ml-auto">            
            <li class="nav-item">
              <a class="nav-link" href="#">Contacto</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>--%>
    <nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>                        
      </button>
      <a class="navbar-brand" href="index.aspx">Thermolympic S.L.</a>
    </div>
    <div class="collapse navbar-collapse" id="myNavbar">
      <ul class="nav navbar-nav"> 
        <li><a href="FichasParametros.aspx">Consultar ficha de fabricación</a></li>
         <li><a href="ListaFichasParametros.aspx">Ver listado de fichas</a></li>           
      </ul>
      <ul class="nav navbar-nav navbar-right">
        <li><a href="#"><span class="glyphicon glyphicon-question-sign"> AYUDA</span></a></li>
      </ul>
    </div>
  </div>
</nav>
    <div class="container">
        <h1>
            Nueva ficha de parámetros</h1>
        <div class="row">
        </div>
        <div class="row">
        </div>
        <div class="row">
            <label for="usr">
            </label>
        </div>
        <div class="row">
            <div class="col-lg-6">
                <div class="form-group">

                    <div class="btn-group">
                        <button id="btnImportarMaquina" runat="server" type="button" onserverclick="cargardatosbms"
                            class="btn btn-primary">
                            Importar datos de BMS</button>
                        <button id="brnGuardar" runat="server" onserverclick="guardarFicha" type="button"
                            class="btn btn-primary">
                            Guardar ficha</button>
                         </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function guardado_OK() {
                alert("Ficha de parámetros creada correctamente.");
            }
        </script>
        <script type="text/javascript">
            function guardado_NOK() {
                alert("Se ha producido un error al crear la ficha de parámetros. Consulte con el administrador del sistema.");
            }
        </script>
        <script type="text/javascript">
             function guardado_NOKEXISTE() {
                alert("Se ha producido un error. La ficha de parámetros ya existe. ");
             }
        </script>                                
        <div class="row">
            <label for="usr">
            </label>
        </div>
            <div class="row">
                <div class="col-lg-12">
                    <div id="Div7" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Información principal</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-6">
                                <div class="table-responsive">
                                    <table>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbReferenciaTitulo" runat="server" Style="text-align: center" Enabled="false">Referencia</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbReferencia" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                         <th>
                                                <asp:TextBox ID="tbNombreTitulo" runat="server" Style="text-align: center" Enabled="false">Nombre</asp:TextBox>
                                          </th>
                                            <td colspan="2">
                                                <asp:TextBox ID="tbNombre" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbClienteTitulo" runat="server" Style="text-align: center" Enabled="false">Cliente</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbCliente" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbCodigoMoldeTitulo" runat="server" Style="text-align: center" Enabled="false">Código molde</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbCodigoMolde" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <th>
                                                <asp:TextBox ID="tbCavidadesTitulo" runat="server" Style="text-align: center" Enabled="false">Cavidades</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbCavidades" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbPersonalTitulo" runat="server" Style="text-align: center" Enabled="false">Personal</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbPersonal" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbAutomaticoTitulo" runat="server" Style="text-align: center" Enabled="false">Automático</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbAutomatico" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbManualTitulo" runat="server" Style="text-align: center" Enabled="false">Manual</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbManual" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="table-responsive">
                                    <table>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbMaquinaTitulo" runat="server" Style="text-align: center" Enabled="false">Máquina</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbMaquina" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <th>
                                                <asp:TextBox ID="tbProgramaMoldeTitulo" runat="server" Style="text-align: center"
                                                    Enabled="false">Programa de inyección</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbProgramaMolde" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <th>
                                                <asp:TextBox ID="tbAperturaMaquinaTitulo" runat="server" Style="text-align: center"
                                                    Enabled="false">Apertura máquina (MM)</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbAperturaMaquina" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbFuerzaCierreTitulo" runat="server" Style="text-align: center"
                                                    Enabled="false">Fuerza cierre (KN)</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbFuerzaCierre" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                         </tr>
                                       

                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbProgramaRobotTitulo" runat="server" Style="text-align: center"
                                                    Enabled="false">Nº programa robot</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbProgramaRobot" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbManoRobotTitulo" runat="server" Style="text-align: center" Enabled="false">Nº mano robot</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TextBox1" runat="server" Style="text-align: center" Enabled="false">Asignar en APP</asp:TextBox>
                                                <asp:TextBox ID="tbManoRobot" runat="server" Style="text-align: center" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbPesoPiezaTitulo" runat="server" Style="text-align: center" Enabled="false">Peso piezas(gr)</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbPesoPieza" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbPesoColadaTitulo" runat="server" Style="text-align: center" Enabled="false">Peso colada(gr)</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbPesoColada" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbPesoTotalTitulo" runat="server" Style="text-align: center" Enabled="false">Peso total(gr)</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbPesoTotal" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                                                   
                                </div>
                          </div>
                        </div>
                    </div>
                </div>
            </div>

             <%--Defino tabs --%>
            <ul class="nav nav-pills nav-justified">
                <li class="active"><a data-toggle="pill" href="#PARAMETROS">Parámetros</a></li>
                <li><a data-toggle="pill" href="#AGUAS">Atemperado</a></li>
             </ul>
            <div class="tab-content">
            <%-- Abro panel de parámetros--%>
                <div id="PARAMETROS" class="tab-pane fade in active">
                <h3>Parámetros de máquina</h3>
                    <p>En esta sección podrás ver y editar los parámetros definidos de máquina.</p>
                    <div class="row">
                <%--<div class="col-lg-12">
                    <div id="Div6" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Material</h3>
                        </div>
                        <div class="panel-body">
                            <div id="Div5">
                                <div class="table-responsive">
                                    <table id="tablaMaterial" runat="server">
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbCodMaterial" runat="server" Style="text-align: center" Enabled="false">Cod. Material</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbMaterial" runat="server" Style="text-align: center" Enabled="false">Material</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbCodColorante" runat="server" Style="text-align: center" Enabled="false">Cod. Colorante</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbColorante" runat="server" Style="text-align: center" Enabled="false">Colorante</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbColor" runat="server" Style="text-align: center" Enabled="false">% Color</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbTempSecado" runat="server" Style="text-align: center" Enabled="false">Tª secado</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbTiempoSecado" runat="server" Style="text-align: center" Enabled="false">T. secado</asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr id="trMaterial" runat="server">
                                            <td>
                                                <asp:TextBox ID="thCodMaterial" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thMaterial" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thCodColorante" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thColorante" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thColor" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thTempSecado" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thTiempoSecado" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="thCodMaterial2" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thMaterial2" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thCodColorante2" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thColorante2" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thColor2" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thTempSecado2" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thTiempoSecado2" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="col-lg-6">
                    <div id="Div17" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Temperaturas cilindro (±10%)</h3>
                        </div>
                        <div class="panel-body">
                            <div id="Div18">
                                <div class="table-responsive">
                                    <table>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbBoq" runat="server" Style="text-align: center" Enabled="false">BOQ</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT1" runat="server" Style="text-align: center" Enabled="false">T1</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT2" runat="server" Style="text-align: center" Enabled="false">T2</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT3" runat="server" Style="text-align: center" Enabled="false">T3</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT4" runat="server" Style="text-align: center" Enabled="false">T4</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT5" runat="server" Style="text-align: center" Enabled="false">T5</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT6" runat="server" Style="text-align: center" Enabled="false">T6</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT7" runat="server" Style="text-align: center" Enabled="false">T7</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT8" runat="server" Style="text-align: center" Enabled="false">T8</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT9" runat="server" Style="text-align: center" Enabled="false">T9</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbT10" runat="server" Style="text-align: center" Enabled="false">T10</asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="thBoq" runat="server" Style="text-align: center" data-toggle="popover"  title="thBoq--" content ="1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT1" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT2" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT3" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT4" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT5" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT6" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT7" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT8" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT9" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thT10" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div id="Div2" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Temperaturas cámara caliente  (±10%)</h3>
                        </div>
                        <div class="panel-body">
                            <div id="Div20" class="table-editable">
                                <div class="table-responsive">
                                    <table>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbZ1" runat="server" Style="text-align: center" Enabled="false">Z1</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ2" runat="server" Style="text-align: center" Enabled="false">Z2</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ3" runat="server" Style="text-align: center" Enabled="false">Z3</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ4" runat="server" Style="text-align: center" Enabled="false">Z4</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ5" runat="server" Style="text-align: center" Enabled="false">Z5</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ6" runat="server" Style="text-align: center" Enabled="false">Z6</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ7" runat="server" Style="text-align: center" Enabled="false">Z7</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ8" runat="server" Style="text-align: center" Enabled="false">Z8</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ9" runat="server" Style="text-align: center" Enabled="false">Z9</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ10" runat="server" Style="text-align: center" Enabled="false">Z10</asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="thZ1" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ2" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ3" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ4" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ5" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ6" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ7" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ8" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ9" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ10" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tbZ11" runat="server" Style="text-align: center" Enabled="false">Z11</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ12" runat="server" Style="text-align: center" Enabled="false">Z12</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ13" runat="server" Style="text-align: center" Enabled="false">Z13</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ14" runat="server" Style="text-align: center" Enabled="false">Z14</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ15" runat="server" Style="text-align: center" Enabled="false">Z15</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ16" runat="server" Style="text-align: center" Enabled="false">Z16</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ17" runat="server" Style="text-align: center" Enabled="false">Z17</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ18" runat="server" Style="text-align: center" Enabled="false">Z18</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ19" runat="server" Style="text-align: center" Enabled="false">Z19</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tbZ20" runat="server" Style="text-align: center" Enabled="false">Z20</asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="thZ11" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ12" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ13" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ14" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ15" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ16" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ17" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ18" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ19" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="thZ20" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Tabs de inyección --%>
             <ul class="nav nav-tabs nav-justified">
                <li class="active"><a data-toggle="tab" href="#INYECCION">Inyección</a></li>
                <li><a data-toggle="tab" href="#SECUENCIAL">Secuencial</a></li>
             </ul>

            <div class="tab-content">
            <%-- Abro panel de INYECCION--%>
                <div id="INYECCION" class="tab-pane fade in active">

            <div class="row">
                <div class="col-lg-12">
                    <div id="Div9" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Inyección (±10%)</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-8">
                                    <div class="table-responsive">
                                        <table>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="tbPaso" runat="server" Style="text-align: center" Enabled="false">Paso</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb1" runat="server" Style="text-align: center" Enabled="false">1</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb2" runat="server" Style="text-align: center" Enabled="false">2</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb3" runat="server" Style="text-align: center" Enabled="false">3</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb4" runat="server" Style="text-align: center" Enabled="false">4</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb5" runat="server" Style="text-align: center" Enabled="false">5</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb6" runat="server" Style="text-align: center" Enabled="false">6</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb7" runat="server" Style="text-align: center" Enabled="false">7</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb8" runat="server" Style="text-align: center" Enabled="false">8</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb9" runat="server" Style="text-align: center" Enabled="false">9</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb10" runat="server" Style="text-align: center" Enabled="false">10</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb11" runat="server" Style="text-align: center" Enabled="false">11</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tb12" runat="server" Style="text-align: center" Enabled="false">12</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbVelocidad" runat="server" Style="text-align: center" Enabled="false">Velocidad (mm/s)</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV11" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thV12" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbCarrera" runat="server" Style="text-align: center" Enabled="false">Carrera (mm)</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC11" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thC12" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="table-responsive">
                                        <table>
                                            <tr>
                                                <th colspan="4">
                                                    <asp:TextBox ID="tbTiempoInyeccionTitulo" runat="server" Style="text-align: center"
                                                        Enabled="false">Tiempo (s)</asp:TextBox>
                                                </th>
                                                <th colspan="4">
                                                    <asp:TextBox ID="tbLimitePresionTitulo" runat="server" Style="text-align: center"
                                                        Enabled="false">Límite presión (bar)</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:TextBox ID="tbTiempoInyeccion" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="tbLimitePresion" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>

                                             <tr>
                                                <th>
                                                    <asp:TextBox ID="tbTiempoInyeccionN" runat="server" Style="text-align: center">Mín.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbTiempoInyeccionNVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>
                                                <th>
                                                    <asp:TextBox ID="tbTiempoInyeccionM" runat="server" Style="text-align: center">Máx.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbTiempoInyeccionMVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>

                                                <th>
                                                    <asp:TextBox ID="tbLimitePresionN" runat="server" Style="text-align: center">Mín.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbLimitePresionNVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>
                                                <th>
                                                    <asp:TextBox ID="tbLimitePresionM" runat="server" Style="text-align: center">Máx.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbLimitePresionMval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>                                               
                                            </tr>                                            
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
                        <%--empieza inyección secuencial --%>
    <div id="SECUENCIAL" class="tab-pane fade">
            <div class="row">
                <div class="col-lg-12">
                    <div id="Div23" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Inyección secuencial</h3>
                        </div>
                        <div class="panel-body">
                           <div class="row">
                                <div class="col-lg-8">
                                    <div class="table-responsive">
                                         <table id="tablesec" runat="server">
                                         <tr>
                                                 <th colspan="11">
                                                    <asp:TextBox ID="tituloporcota" runat="server" Style="text-align: center" Enabled="false">Por cota (mm)</asp:TextBox>
                                                </th>
                                         </tr>
                                          <tr>
                                                <th>
                                                    <asp:TextBox ID="tituloBoquilla" runat="server" Style="text-align: center" Enabled="false">Boquilla</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial1" runat="server" Style="text-align: center" Enabled="false">1</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial2" runat="server" Style="text-align: center" Enabled="false">2</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial3" runat="server" Style="text-align: center" Enabled="false">3</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial4" runat="server" Style="text-align: center" Enabled="false">4</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial5" runat="server" Style="text-align: center" Enabled="false">5</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial6" runat="server" Style="text-align: center" Enabled="false">6</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial7" runat="server" Style="text-align: center" Enabled="false">7</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial8" runat="server" Style="text-align: center" Enabled="false">8</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial9" runat="server" Style="text-align: center" Enabled="false">9</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencial10" runat="server" Style="text-align: center" Enabled="false">10</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="lineabrir1secu" runat="server" Style="text-align: center" Enabled="false">Abrir 1</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir1_10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>                                    
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="linecerrar1secu" runat="server" Style="text-align: center" Enabled="false">Cerrar 1</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar1_10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <asp:TextBox ID="lineabrir2secu" runat="server" Style="text-align: center" Enabled="false">Abrir 2</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqAbrir2_10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>                                    
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="linecerrar2secu" runat="server" Style="text-align: center" Enabled="false">Cerrar 2</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqCerrar2_10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="lineTPresPost" runat="server" Style="text-align: center" Enabled="false">T.Pres.Post.</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTPresPost10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            
                               <div class="col-lg-8">
                                    <div class="table-responsive">
                                         <table id="tablesectiempo" runat="server">
                                         <tr>
                                                 <th colspan="11">
                                                    <asp:TextBox ID="tituloporTiempo" runat="server" Style="text-align: center" Enabled="false">Por tiempo (s)</asp:TextBox>
                                                </th>
                                         </tr>
                                          <tr>
                                                <th>
                                                    <asp:TextBox ID="tituloBoquillaTiempo" runat="server" Style="text-align: center" Enabled="false">Boquilla</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo1" runat="server" Style="text-align: center" Enabled="false">1</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo2" runat="server" Style="text-align: center" Enabled="false">2</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo3" runat="server" Style="text-align: center" Enabled="false">3</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo4" runat="server" Style="text-align: center" Enabled="false">4</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo5" runat="server" Style="text-align: center" Enabled="false">5</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo6" runat="server" Style="text-align: center" Enabled="false">6</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo7" runat="server" Style="text-align: center" Enabled="false">7</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo8" runat="server" Style="text-align: center" Enabled="false">8</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo9" runat="server" Style="text-align: center" Enabled="false">9</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="titulosecuencialTiempo10" runat="server" Style="text-align: center" Enabled="false">10</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="lineTiempoRetardo" runat="server" Style="text-align: center" Enabled="false">T.Retardo</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="seqTiempoRetardo_10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>                                    
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="seqAnotacionesTitulo" runat="server" Style="text-align: center" Enabled="false">Anotaciones</asp:TextBox>
                                                </td>
                                                <td colspan="10">
                                                    <asp:TextBox ID="seqAnotaciones" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>                                                    
                                            </tr>                                     
                                        </table>
                                    </div>
                                </div> 
                              </div> 
                             
                               <%-- 
                                <div class="col-lg-4">
                                    <div class="table-responsive">
                                        <table>
                                            <tr>
                                                <th colspan="4">
                                                    <asp:TextBox ID="tbTiempoSecuencialTitulo" runat="server" Style="text-align: center"
                                                        Enabled="false">Tiempo</asp:TextBox>
                                                </th>
                                                <th colspan="4">
                                                    <asp:TextBox ID="tbPresionSecuencialTitulo" runat="server" Style="text-align: center"
                                                        Enabled="false">Límite presión</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:TextBox ID="tbTiempoSecuencial" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="tbPresionSecuencial" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>

                                             <tr>
                                                <th>
                                                    <asp:TextBox ID="tbTiempoSecuencialN" runat="server" Style="text-align: center">Mín.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbTiempoSecuencialNVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>
                                                <th>
                                                    <asp:TextBox ID="tbTiempoSecuencialM" runat="server" Style="text-align: center">Máx.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbTiempoSecuencialMVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>
                                                <th>
                                                    <asp:TextBox ID="tbPresionSecuencialN" runat="server" Style="text-align: center">Mín.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbPresionSecuencialNVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>
                                                <th>
                                                    <asp:TextBox ID="tbPresionSecuencialM" runat="server" Style="text-align: center">Máx.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbPresionSecuencialMVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>                                               
                                            </tr>                                            
                                        </table>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           </div>
    <%--termina inyección secuencial --%>
                <div class="row">
                <div class="col-lg-12">
                    <div id="Div10" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Postpresión (±10%)</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-8">
                                    <div class="table-responsive">
                                        <table>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="tbPasoPresion" runat="server" Style="text-align: center" Enabled="false">Paso</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP1" runat="server" Style="text-align: center" Enabled="false">1</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP2" runat="server" Style="text-align: center" Enabled="false">2</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP3" runat="server" Style="text-align: center" Enabled="false">3</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP4" runat="server" Style="text-align: center" Enabled="false">4</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP5" runat="server" Style="text-align: center" Enabled="false">5</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP6" runat="server" Style="text-align: center" Enabled="false">6</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP7" runat="server" Style="text-align: center" Enabled="false">7</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP8" runat="server" Style="text-align: center" Enabled="false">8</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP9" runat="server" Style="text-align: center" Enabled="false">9</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="tbP10" runat="server" Style="text-align: center" Enabled="false">10</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="thPresion" runat="server" Style="text-align: center" Enabled="false">Presión</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thP10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="thTPtiempo" runat="server" Style="text-align: center" Enabled="false">Tiempo</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP6" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP7" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP8" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP9" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thTP10" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="table-responsive">
                                        <table>
                                            <tr>
                                                <th colspan="4">
                                                    <asp:TextBox ID="tbConmutacionTitulo" runat="server" Style="text-align: center" Enabled="false">Conmutación (mm)</asp:TextBox>
                                                </th>
                                                <th colspan="4">
                                                    <asp:TextBox ID="tbTiempoPresionTitulo" runat="server" Style="text-align: center"
                                                        Enabled="false">Tiempo presión (s)</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:TextBox ID="tbConmutacion" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="tbTiempoPresion" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <th>
                                                    <asp:TextBox ID="thConmuntaciontolN" runat="server" Style="text-align: center">Mín.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="thConmuntaciontolNVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>
                                                <th>
                                                    <asp:TextBox ID="thConmuntaciontolM" runat="server" Style="text-align: center">Máx.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="thConmuntaciontolMVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>

                                                <th>
                                                    <asp:TextBox ID="tbTiempoPresiontolN" runat="server" Style="text-align: center">Mín.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbTiempoPresiontolNVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>
                                                <th>
                                                    <asp:TextBox ID="tbTiempoPresiontolM" runat="server" Style="text-align: center">Máx.</asp:TextBox>
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbTiempoPresiontolMVal" runat="server" Style="text-align: center">10%</asp:TextBox>
                                                </td>
                                               
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            </div>
                            </div>
                            </div>
                            </div>
                            <div class="row">
                <div class="col-lg-12">
                    <div id="Div11" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Dosificación / Ciclo</h3>
                        </div>
                        <div class="panel-body">
                            <div id="Div12" class="table-editable">
                                <div class="table-responsive">
                                    <table id="Table1" runat="server">
                                        <tr>
                                            <th colspan="4">
                                                <asp:TextBox ID="tbVCarga" runat="server" Style="text-align: center" Enabled="false">V. Carga (%)</asp:TextBox>
                                            </th>
                                            <th colspan="4">
                                                <asp:TextBox ID="tbCarga" runat="server" Style="text-align: center" Enabled="false">Carga (mm)</asp:TextBox>
                                            </th>
                                            <th colspan="4">
                                                <asp:TextBox ID="tbDescom" runat="server" Style="text-align: center" Enabled="false">Descom. (mm)</asp:TextBox>
                                            </th>
                                            <th colspan="4">
                                                <asp:TextBox ID="tbContra" runat="server" Style="text-align: center" Enabled="false">Contrapr. (bar)</asp:TextBox>
                                            </th>
                                            <th colspan="4">
                                                <asp:TextBox ID="tbTiempoDos" runat="server" Style="text-align: center" Enabled="false">Tiempo (s)</asp:TextBox>
                                            </th>
                                            <th colspan="4">
                                                <asp:TextBox ID="tbEnfriamiento" runat="server" Style="text-align: center" Enabled="false">Enfriamiento (s)</asp:TextBox>
                                            </th>
                                            <th colspan="4">
                                                <asp:TextBox ID="tbCiclo" runat="server" Style="text-align: center" Enabled="false">Ciclo (s)</asp:TextBox>
                                            </th>
                                             <th colspan="4">
                                                <asp:TextBox ID="tbCojin" runat="server" Style="text-align: center" Enabled="false">Cojín (mm)</asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr id="tr1" runat="server">
                                            <td colspan="4">                        
                                                <asp:TextBox ID="thVCarga" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="thCarga" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="thDescomp" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="thContrapr" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="thTiempo" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="thEnfriamiento" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="thCiclo" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="thCojin" runat="server" Style="text-align: center"></asp:TextBox>
                                            </td>
                                        </tr>
                      

                                        <tr id="tr2" runat="server">
                                            <th>
                                                <asp:TextBox ID="TNvcarga" runat="server" Style="text-align: center" Enabled="false">Mín.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TNvcargaval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>                                 
                                            <th>
                                                <asp:TextBox ID="TMvcarga" runat="server" Style="text-align: center" Enabled="false">Máx.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TMvcargaval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TNcarga" runat="server" Style="text-align: center" Enabled="false">Mín.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TNcargaval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TMcarga" runat="server" Style="text-align: center" Enabled="false">Máx.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TMcargaval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TNdescom" runat="server" Style="text-align: center" Enabled="false">Mín.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TNdescomval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TMdescom" runat="server" Style="text-align: center" Enabled="false">Máx.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TMdescomval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TNcontrap" runat="server" Style="text-align: center" Enabled="false">Mín.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TNcontrapval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TMcontrap" runat="server" Style="text-align: center" Enabled="false">Máx.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TMcontrapval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TNTiempdos" runat="server" Style="text-align: center" Enabled="false">Mín.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TNTiempdosval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TMTiempdos" runat="server" Style="text-align: center" Enabled="false">Máx.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TMTiempdosval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TNEnfriam" runat="server" Style="text-align: center" Enabled="false">Mín.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TNEnfriamval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TMEnfriam" runat="server" Style="text-align: center" Enabled="false">Máx.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TMEnfriamval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                             <th>
                                                <asp:TextBox ID="TNCiclo" runat="server" Style="text-align: center" Enabled="false">Mín.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TNCicloval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>   
                                            <th>
                                                <asp:TextBox ID="TMCiclo" runat="server" Style="text-align: center" Enabled="false">Máx.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TMCicloval" runat="server" Style="text-align: center">10%</asp:TextBox>
                                            </td>
                                            <th>
                                                <asp:TextBox ID="TNCojin" runat="server" Style="text-align: center" Enabled="false">Mín.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TNCojinval" runat="server" Style="text-align: center" Enabled="false">10%</asp:TextBox>
                                            </td>   
                                            <th>
                                                <asp:TextBox ID="TMCojin" runat="server" Style="text-align: center" Enabled="false">Máx.</asp:TextBox>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="TMCojinval" runat="server" Style="text-align: center" Enabled="false">10%</asp:TextBox>
                                            </td>
                                         </tr>   
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div id="Div15" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Marcad lo que corresponda</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbNoyos" runat="server" type="checkbox" value="" readonly="readonly">Noyos</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbHembra" runat="server" type="checkbox" value="">Hembra dos</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbMacho" runat="server" type="checkbox" value="">Macho</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbAntesExpul" runat="server" type="checkbox" value="">Antes expuls.</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbAntesApert" runat="server" type="checkbox" value="">Antes apert.</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbAntesCierre" runat="server" type="checkbox" value="">Recoger antes
                                            cierre</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbDespuesCierre" runat="server" type="checkbox" value="">Recoger después
                                            cierre</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbOtros1" runat="server" type="checkbox" value="">Otros</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbBoquilla" runat="server" type="checkbox" value="">Boquilla</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbCono" runat="server" type="checkbox" value="">Cono</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbRadioLarga" runat="server" type="checkbox" value="">Radio larga</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbLibre" runat="server" type="checkbox" value="">Libre</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbValvula" runat="server" type="checkbox" value="">Con válvula</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbResistencia" runat="server" type="checkbox" value="">Con resistencia</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbOtros2" runat="server" type="checkbox" value="">Otros</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbExpulsion" runat="server" type="checkbox" value="">Expulsión</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbHidraulica" runat="server" type="checkbox" value="">Hidraulica</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbNeumatica" runat="server" type="checkbox" value="">Neumática</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbNormal" runat="server" type="checkbox" value="">Normal(choque)</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbArandela125" runat="server" type="checkbox" value="">Arandela centr.
                                            125</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbArandela160" runat="server" type="checkbox" value="">Arandela centr.
                                            160</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbArandela200" runat="server" type="checkbox" value="">Arandela centr.
                                            200</label>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <input id="cbArandela250" runat="server" type="checkbox" value="">Arandela centr.
                                            250</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table>
                                            <tr>                                                
                                                <label class="control-label col-sm-1" for="MarcasOtrosText">Notas:</label> 
                                                <th>
                                                    <asp:TextBox ID="MarcasOtrosText" runat="server" Style="text-align: center" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                            </div>

                        </div>      
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div id="Div6" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Instrucciones de arranque</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="ThOperacionNum" runat="server" Style="text-align: center" Enabled="false">Nº</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="ThOperacionTitulo" runat="server" Style="text-align: center" Enabled="false">OPERACIÓN</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionN1" runat="server" Style="text-align: center" Enabled="false">1</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionText1" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionN2" runat="server" Style="text-align: center" Enabled="false">2</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionText2" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionN3" runat="server" Style="text-align: center" Enabled="false">3</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionText3" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionN4" runat="server" Style="text-align: center" Enabled="false">4</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionText4" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionN5" runat="server" Style="text-align: center" Enabled="false">5</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TbOperacionText5" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>                               
                           </div>
                        </div>
                    </div>
                </div>
            </div>
     </div>
<%-- Cierro panel de parámetros--%>
<%-- Abro panel de atemperado--%>
           <div id="AGUAS" class="tab-pane fade">
                    <h3>Aguas y atemperado</h3>
                    <p>En esta sección podrás ver y editar las conexiones de refrigeración del molde.</p>
                    
           <%--Tabs de FIJA Y MOVIL --%>
           <ul class="nav nav-tabs nav-justified">
                <li class="active"><a data-toggle="tab" href="#FIJA">PARTE FIJA</a></li>
                <li><a data-toggle="tab" href="#MOVIL">PARTE MOVIL</a></li>
             </ul>
            <div class="tab-content">
            <%-- Abro panel de FIJA--%>
                <div id="FIJA" class="tab-pane fade in active">
                <div class="row">
                    <div class="col-lg-12">
                    <div id="AtempConexiones" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Circuitos de atemperado y conexiones</h3>
                        </div>
                  <div class="panel-body">
                        <div class="container">
                                  <p></p>
                                  <div class="row">
                                        <div class="col-md-4 col-md-offset-4">
                                          <div class="thumbnail">                        
                                            <asp:HyperLink id="hyperlink1" NavigateUrl="" ImageUrl= "" Text="Lado opuesto" Target="_new" runat="server"/>
                                                <div class="caption">
                                                    <p>Vista general</p>                                                
                                                    <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                                                    <button id="Botonsubida1" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-basic">Subir imagen</button>
                                                </div> 
                                          </div>
                                        </div>                       
                                      <p></p>
                                  </div>
                        </div>
                        <div class="container">
                           <div class="table-responsive">
                                <table id="AtempFija" runat="server">
                                    <tr>
                                    <th colspan="12">
                                            <asp:DropDownList ID="AtempTipoF" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="ThCircuitoF" runat="server" Style="text-align: center" Enabled="false">Nº Circuito:</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoF1" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoF2" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoF3" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoF4" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoF5" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList> 
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoF6" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="ThCaudalF" runat="server" Style="text-align: center" Enabled="false">Caudal:</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="TbCaudalF1" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalF2" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalF3" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalF4" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalF5" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalF6" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="ThTemperaturaF" runat="server" Style="text-align: center" Enabled="false">Temperatura:</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaF1" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaF2" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaF3" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaF4" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaF5" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaF6" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="ThEntradaF" runat="server" Style="text-align: center" Enabled="false">Entrada:</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaF1" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaF2" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaF3" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaF4" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaF5" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaF6" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="container">
                                  <div class="row">
                                  <p></p>
                                    <div class="col-md-3">
                                      <div class="thumbnail">                        
                                        <asp:HyperLink id="hyperlink2" NavigateUrl="" ImageUrl= "" Text="Lado opuesto" Target="_new" runat="server"/>
                                            <div class="caption">
                                                <p>Vista general</p>
                                                <asp:FileUpload ID="FileUpload2" runat="server"></asp:FileUpload>
                                                <button id="Botonsubida2" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-basic">Subir imagen</button>
                                           </div> 
                                      </div>
                                    </div>
                                    <div class="col-md-3 col-md-offset-1">
                                      <div class="thumbnail">                                         
                                       <asp:HyperLink id="hyperlink3" NavigateUrl="" ImageUrl= "" Text="Lado opuesto" Target="_new" runat="server"/>
                                            <div class="caption">
                                                <p>Lado operario</p>                                                
                                                <asp:FileUpload ID="FileUpload3" runat="server"></asp:FileUpload>
                                                <button id="Botonsubida3" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-basic">Subir imagen</button>
                                            </div> 
                                      </div>
                                    </div>
                                    <div class="col-md-3 col-md-offset-1">
                                      <div class="thumbnail">
                                        <asp:HyperLink id="hyperlink4" NavigateUrl="" ImageUrl= "" Text="Lado opuesto" Target="_new" runat="server"/>
                                        <div class="caption">
                                                <p>Lado opuesto</p>
                                                <asp:FileUpload ID="FileUpload4" runat="server"></asp:FileUpload>
                                                <button id="Botonsubida4" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-basic">Subir imagen</button>
                                        </div> 
                                      </div>
                                    </div>
                                  </div>
                                  <div class="row">
                                  <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
                                  </div>
                                </div>
                            </div>
                        </div>
                     </div>    
                </div>
                </div>
            
            <div id="MOVIL" class="tab-pane fade">
                <div class="row">
                    <div class="col-lg-12">
                    <div id="Div5" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Circuitos de atemperado y conexiones</h3>
                        </div>
                        <div class="panel-body">
                        <div class="container">
                            <div class="row">
                                  <p></p>
                                    <div class="col-md-4 col-md-offset-4">
                                      <div class="thumbnail">                        
                                        <asp:HyperLink id="hyperlink5" NavigateUrl="" ImageUrl= "" Text="Lado opuesto" Target="_new" runat="server"/>
                                            <div class="caption">
                                                <p>Vista general</p>
                                                <asp:FileUpload ID="FileUpload5" runat="server"></asp:FileUpload>
                                                <button id="Botonsubida5" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-basic">Subir imagen</button>
                                            </div> 
                                      </div>
                                    </div>
                            </div>
                            <div class="table-responsive">
                                <table id="Table5" runat="server">
                                    <tr>
                                    <th colspan="7">
                                            <asp:DropDownList ID="AtempTipoM" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="ThCircuitoM" runat="server" Style="text-align: center" Enabled="false">Nº Circuito:</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoM1" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoM2" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoM3" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoM4" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoM5" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList> 
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbCircuitoM6" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="ThCaudalM" runat="server" Style="text-align: center" Enabled="false">Caudal:</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="TbCaudalM1" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalM2" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalM3" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalM4" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalM5" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbCaudalM6" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="ThTemperaturaM" runat="server" Style="text-align: center" Enabled="false">Temperatura:</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaM1" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaM2" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaM3" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaM4" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaM5" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TbTemperaturaM6" runat="server" Style="text-align: center"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="ThEntradaM" runat="server" Style="text-align: center" Enabled="false">Entrada:</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaM1" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaM2" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaM3" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaM4" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaM5" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TbEntradaM6" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="container">
                                  <div class="row">
                                  <p></p>
                                    <div class="col-md-3">
                                      <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink6" NavigateUrl="" ImageUrl= "" Text="Lado opuesto" Target="_new" runat="server"/>
                                            <div class="caption">
                                                <p>Vista general</p>
                                                <asp:FileUpload ID="FileUpload6" runat="server"></asp:FileUpload>
                                                <button id="Botonsubida6" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-basic">Subir imagen</button>
                                            </div>
                                          </div>
                                    </div>
                                    <div class="col-md-3 col-md-offset-1">
                                        <div class="thumbnail">
                                                <asp:HyperLink id="hyperlink7" NavigateUrl="" ImageUrl= "" Text="Lado opuesto" Target="_new" runat="server"/>
                                            <div class="caption">
                                                <p>Lado operario</p>
                                                <asp:FileUpload ID="FileUpload7" runat="server"></asp:FileUpload>
                                                <button id="Botonsubida7" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-basic">Subir imagen</button>
                                            </div>
                                        </div>                                         
                                    </div>
                                    <div class="col-md-3 col-md-offset-1">
                                      <div class="thumbnail">                                     
                                                <asp:HyperLink id="hyperlink8" NavigateUrl="" ImageUrl= "" Text="Lado opuesto" Target="_new" runat="server"/>
                                          <div class="caption">
                                                <p>Lado opuesto</p>
                                                <asp:FileUpload ID="FileUpload8" runat="server"></asp:FileUpload>  
                                                <button id="Botonsubida8" runat="server" onserverclick="insertar_foto" type="button" class="btn btn-basic">Subir imagen</button>                                                                   
                                          </div>                            
                                      </div>
                                     </div>
                                  </div>
                                  <div class="row">
                                         <asp:Label ID="UploadStatusLabel2" runat="server"></asp:Label>
                                  </div>
                                </div>
                        </div>
                    </div>
                </div> 
            </div>
           </div>
          </div>  
            <%-- Cierro panel de aguas fijas y moviles--%>
        </div>
            <%-- Cierro panel de atemperado--%>
        </div>
<%--Cierro definición de tabs --%>   
<%--Pie de página común --%>
            <div class="row">
                <div class="col-lg-12">
                    <div id="Div16" class="panel panel-default" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Datos de la ficha</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table id="Table4" runat="server">
                                        <tr>
                                                <th colspan="3">
                                                    <asp:TextBox ID="tbObservacionesTitulo" runat="server" Style="text-align: center"
                                                        Enabled="false">Cambios respecto a la ficha anterior</asp:TextBox>
                                                </th>
                                                <th colspan="2">
                                                    <asp:TextBox ID="tbRazonesTitulo" runat="server" Style="text-align: center"
                                                        Enabled="false">Motivo del cambio</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:TextBox ID="tbObservaciones" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="tbRazones" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                            </tr>


                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="cbEdicionTitulo" runat="server" Style="text-align: center" Enabled="false">Edición</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="cbFechaTitulo" runat="server" Style="text-align: center" Enabled="false">Fecha</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="cbElaboradoPorTitulo" runat="server" Style="text-align: center"
                                                        Enabled="false">Elaborado por</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="cbRevisadoPorTitulo" runat="server" Style="text-align: center" Enabled="false">Revisado por</asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="cbAprobadoPorTitulo" runat="server" Style="text-align: center" Enabled="false">Aprobado por</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="cbEdicion" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="cbFecha" runat="server" Style="text-align: center"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <%--<asp:TextBox ID="cbElaboradoPor" runat="server" Style="text-align: center"></asp:TextBox> --%>
                                                    <asp:DropDownList ID="cbElaboradoPor" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <%--<asp:TextBox ID="cbRevisadoPor" runat="server" Style="text-align: center"></asp:TextBox> --%>
                                                    <asp:DropDownList ID="cbRevisadoPor" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
             
                                                </td>
                                                <td>
                                                    <%--<asp:TextBox ID="cbAprobadoPor" runat="server" Style="text-align: center"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="cbAprobadoPor" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
             
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>
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