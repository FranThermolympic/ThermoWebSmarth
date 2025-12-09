<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GP12Historicoold.aspx.cs" Inherits="ThermoWeb.GP12.GP12Historicoold"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Histórico de revisiones</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
      <script src="js/json2.js" type="text/javascript"></script>
      <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
      <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
    $(document).ready(function () {
        $('.Add-text').datepicker({
            dateFormat: 'yy-mm-dd',
            inline: true,
            showOtherMonths: true,
            changeMonth: true,
            changeYear: true,
            constrainInput: true,
            firstDay: 1,
            navigationAsDateFormat: true,    
            showAnim: "fold",
            yearRange: "c-20:c+10",
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa']
        });
    });
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>    
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
                <li><a href="GP12.aspx">Iniciar revisión</a></li>
                <li><a href="PrevisionGP12.aspx">Planificación de cargas</a></li>
                <li><a href="GP12ReferenciasEstado.aspx">Estado de referencias</a></li> 
                <li><a href="GP12Historico.aspx">Informes de revisión</a></li>
                <li><a href="GP12HistoricoCliente.aspx">Histórico de pieza</a></li>
                <li><a href="../KPI/KPI_GP12.aspx">Cuadro de mando</a></li>
                <li><a href="GP12RegistroComunicaciones.aspx">Registro de comunicaciones</a></li>
          </ul>
          <ul class="nav navbar-nav navbar-right">
                <li><a href="GP12FAQ.aspx"><span class="glyphicon glyphicon-question-sign"> AYUDA</span></a></li>
          </ul>
        </div>
      </div>
    </nav>
    <div class="row">
        <div class="col-lg-12">
                <div class="col-lg-10">
                    <h1>&nbsp;&nbsp;&nbsp; Informe de revisiones </h1>
                </div>
                <div class="col-lg-2">
                    <label for="usr">Periodo de revisión:</label>
                        <asp:DropDownList ID="TablaSeleccion" runat="server" CssClass="form-control" Font-Size="Large">
                                <%--<asp:ListItem Text="2022" Value="2022"></asp:ListItem>--%>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:listitem text="2019" Value="2019"></asp:listitem>
                                <asp:listitem text="2018" Value="2018"></asp:listitem>
                                <%-- <asp:listitem text="2018"></asp:listitem>
                                <asp:listitem text="2017"></asp:listitem>--%>    
                        </asp:DropDownList> 
                </div>
        </div>
     </div>
    
    <div class="row">
            <div class="col-lg-12">
                
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-lg-2">
                            <br />
                            <button id="VerTodas" runat="server" onserverclick="cargar_todas" type="button" class="btn btn-lg btn-primary" style="width:65%; text-align:left">
                            <span class="glyphicon glyphicon-list"></span> Ver todas</button>
                            </div>
                            <div class="col-lg-1">

                                <label for="usr">Fecha:</label>
                                        <asp:TextBox ID="selectFecha" runat="server" CssClass="textbox Add-text" Width="100%" Height="35px" autocomplete="off" />
                                                            
                            </div> 
                            <div class="col-lg-1">
                                <label for="usr">Referencia:</label>
                                        <asp:TextBox ID="selectReferencia" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />
                                                            
                            </div>                                                                                            
                            <div class="col-lg-1">                         
                                <label for="usr">Molde:</label>
                                       <asp:TextBox ID="selectMolde" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />                               
                            </div>
                            <div class="col-lg-1">                         
                                <label for="usr">Lote:</label>
                                        <asp:TextBox ID="selectLote" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />                                
                            </div>
                            <div class="col-lg-1">                               
                                <label for="usr">Cliente:</label>
                                        <asp:DropDownList ID="lista_clientes" runat="server" CssClass="form-control">     
                                    </asp:DropDownList>                           
                            </div>
                            <div class="col-lg-1">                               
                                <label for="usr">Responsable:</label>
                                        <asp:DropDownList ID="lista_responsable" runat="server" CssClass="form-control">     
                                    </asp:DropDownList>                           
                            </div>
                            <div class="col-lg-2">                                
                                    <label for="usr">Razón:</label>
                                        <asp:DropDownList ID="lista_estado" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value=""> - </asp:ListItem>
                                    </asp:DropDownList>                                                             
                            </div>
                            <div class="col-lg-2 text-right">
                            <br />
                                 <button id="Button2" runat="server" onserverclick="cargar_Filtrados" type="button" class="btn btn-lg btn-info" style="width:75%; text-align:left">
                                 <span class="glyphicon glyphicon-search"></span> Filtrar</button>
                            </div>
                         </div>

                        </div>
                    
                </div>
            </div>
 
 

    <div class="table-responsive">
    <asp:GridView ID="dgv_AreaRechazo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" ShowFooter="true" AutoGenerateColumns="false" OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="GridView1_DataBound"
            EmptyDataText="No hay revisiones para mostrar.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="ID" ItemStyle-Width="100px" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblFecharevision" runat="server" Text='<%#Eval("FechaInicio", "{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Horas inspección" ItemStyle-Width="40px">
                    <ItemTemplate>
                        <asp:Label ID="lblHoras" runat="server" Text='<%#Eval("HORAS") %>'/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="totalHoras" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Molde" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblMolde" runat="server" Text='<%#Eval("Molde") %>' />
                    </ItemTemplate>
                </asp:TemplateField>             
                <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="250px">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Nombre") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lote" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblLote" runat="server" Text='<%#Eval("Nlote") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="placeholdertexto" runat="server" Text='Resultados:' Font-Bold />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Buenas" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblBuenas" runat="server" Text='<%#Eval("PiezasOK") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Retrabajadas" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblRetrabajadas" runat="server" Text='<%#Eval("Retrabajadas") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Malas" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblMalas" runat="server" Text='<%#Eval("PiezasNOK") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Observaciones" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="lblObservaciones" runat="server" Text='<%#Eval("Incidencias") %>' /><br />
                        <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("Notas") %>' />
                    </ItemTemplate>
                    
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="placeholdertexto2" runat="server" Text='Tiempo (Horas):' Font-Bold />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Razón" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="lblRazonRevision" runat="server" Text='<%#Eval("Razon") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false" >
                    <ItemTemplate>
                        <asp:Label ID="lblFAKE" runat="server" Text='<%#Eval("FakeMode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" Visible="true">
                    <ItemTemplate>
                     <asp:Button ID = "button2" runat ="server" class="btn btn-info" CommandName="CargaDetalle" CommandArgument='<%#Eval("ID")%>' UseSubmitBehavior="true" Text="Detalles" />
                     <%--<asp:Button ID = "button3" runat = "server" class="btn btn-info" OnClientClick="return false;" data-toggle="modal" href="#myModal" UseSubmitBehavior="true" Text="Ver detalle" />
                        data-toggle="modal" href="#myModal"--%>
                    </ItemTemplate>
                </asp:TemplateField>      
            </Columns>
        </asp:GridView>
    </div>
    
    <!-- Modal bootstrap-->
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog modal-lg">
                  <!-- Modal content-->
                  <div class="modal-content">
                    <div class="modal-header" style="background-color:Blue; color:White; text-align:center; font-size:x-large">
                      Detalles
                    </div>
                    <div class="modal-body" style="padding:40px 50px;">
                      
                      <div class="form-group">
                      <label for="revisor"><span class="glyphicon glyphicon-info-sign"></span> Referencia</label><br />
                      <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="IDINSPECCION" runat="server" Style="text-align: center" width="100%" Enabled="false" Visible="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="detalleReferencia" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="detalleReferenciaNombre" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                </table>
                            </div>
                      <div class="form-group">
                      <label for="revisor"><span class="glyphicon glyphicon-eye-open"></span> Revisión</label><br />
                      <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TbOperarioRevision" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbEmpresaRevision" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                </table>
                            </div>
                      <div class="form-group">
                      <label for="operario"><span class="glyphicon glyphicon-user"></span> Operarios</label><br />
                      <table width="100%">
                                
                                <tr>
                                    <td>
                                        <asp:TextBox ID="Operario1" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Operario2" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <asp:TextBox ID="FechaINFO1" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="FechaINFO2" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="Informador1" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="Informador2" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <th> 
                                        <button id="InformadoOp1" runat="server" onserverclick="ActualizaInformador" type="button" class="btn btn-primary btn-xs" style="width:100%">MARCAR INFORMADO</button>
                                    </th>
                                    <th>
                                        <button id="InformadoOp2" runat="server" onserverclick="ActualizaInformador" type="button" class="btn btn-primary btn-xs" style="width:100%">MARCAR INFORMADO</button>
                               
                                    </th>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:TextBox ID="Operario3" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Operario4" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="FechaINFO3" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="FechaINFO4" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="Informador3" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="Informador4" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <button id="InformadoOp3" runat="server" onserverclick="ActualizaInformador" type="button" class="btn btn-primary btn-xs" style="width:100%">MARCAR INFORMADO</button>
                                    </th>
                                    <th>
                                        <button id="InformadoOp4" runat="server" onserverclick="ActualizaInformador" type="button" class="btn btn-primary btn-xs" style="width:100%">MARCAR INFORMADO</button>
                                    </th>
                                </tr>
                                </table>
                                <br />
                                <label for="FEEDBACKOP"><span class="glyphicon glyphicon-share"></span> Feedback de operario</label><br />
                                <tr>
                                    <td>
                                        <asp:TextBox ID="FeedbackOPERARIOS" runat="server" Style="text-align: center" width="100%"></asp:TextBox>
                                    </td>
                                </tr>

                            </div>
                      <div class="form-group">
                      <label for="costes"><span class="glyphicon glyphicon-euro"></span> Costes</label><br />
                      <table width="100%">
                                <tr>
                                    <th>
                                        <asp:TextBox ID="ThCosteHoras" runat="server" Style="text-align: center" Enabled="false" width="100%">Horas de revisión</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThCosteChatarra" runat="server" Style="text-align: center" Enabled="false" width="100%">Coste Chatarra</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThCosteOperario" runat="server" Style="text-align: center" Enabled="false" width="100%">Coste Revisión</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThCosteTotal" runat="server" Style="text-align: center" Enabled="false" width="100%">Coste Total</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="CosteHoras" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CosteChatarra" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CosteOperario" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CosteTotal" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                </table>
                            </div>
                            <div class="form-group">
                            <label for="defectos"><span class="glyphicon glyphicon-trash"></span> Defectos</label><br />
                            <table width="100%">
                                  <tr>
                                    <th>
                                        <asp:TextBox ID="ThRevisadas" runat="server" Style="text-align: center" Enabled="false" width="100%">Revisadas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThBuenas" runat="server" Style="text-align: center" Enabled="false" width="100%">Buenas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThRetrabajadas" runat="server" Style="text-align: center" Enabled="false" width="100%">Retrabajadas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThMalas" runat="server" Style="text-align: center" Enabled="false" width="100%">Malas</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TbRevisadas" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbBuenas" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbRetrabajadas" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbMalas" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto1" runat="server" Style="text-align: center" Enabled="false" width="100%">Falta llenado</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto2" runat="server" Style="text-align: center" Enabled="false" width="100%">Ráfagas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto3" runat="server" Style="text-align: center" Enabled="false" width="100%">Roturas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto4" runat="server" Style="text-align: center" Enabled="false" width="100%">Montaje NOK</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto1" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto2" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto3" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto4" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto5" runat="server" Style="text-align: center" Enabled="false" width="100%">Deformaciones</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto6" runat="server" Style="text-align: center" Enabled="false" width="100%">Etiqueta NOK</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto7" runat="server" Style="text-align: center" Enabled="false" width="100%">Burbujas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto8" runat="server" Style="text-align: center" Enabled="false" width="100%">Arrastre mat.</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto5" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto6" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto7" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto8" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto9" runat="server" Style="text-align: center" Enabled="false" width="100%">Rayas / Marcas expulsor</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto10" runat="server" Style="text-align: center" Enabled="false" width="100%">Quemazos</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto11" runat="server" Style="text-align: center" Enabled="false" width="100%">Brillos</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto12" runat="server" Style="text-align: center" Enabled="false" width="100%">M. contaminado</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto9" runat="server" Style="text-align: center" Enabled="false" width="100%" BackColor="">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto10" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto11" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto12" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto13" runat="server" Style="text-align: center" Enabled="false" width="100%">Rechupes</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto14" runat="server" Style="text-align: center" Enabled="false" width="100%">Color NOK</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto15" runat="server" Style="text-align: center" Enabled="false" width="100%">Manchas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto16" runat="server" Style="text-align: center" Enabled="false" width="100%">Rebabas</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto13" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto14" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto15" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto16" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto17" runat="server" Style="text-align: center" Enabled="false" width="100%">Sólo plástico</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto18" runat="server" Style="text-align: center" Enabled="false" width="100%">Sólo goma</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto19" runat="server" Style="text-align: center" Enabled="false" width="100%">Otros</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto20" runat="server" Style="text-align: center" Enabled="false" width="100%">Electroválvula</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto17" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto18" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto19" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto20" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto21" runat="server" Style="text-align: center" Enabled="false" width="100%">Grapa: Posición</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto22" runat="server" Style="text-align: center" Enabled="false" width="100%">Grapa: Altura</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto23" runat="server" Style="text-align: center" Enabled="false" width="100%">Tubo: Deformado</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto24" runat="server" Style="text-align: center" Enabled="false" width="100%">Tubo: Mal puesto</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto21" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto22" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto23" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto24" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto25" runat="server" Style="text-align: center" Enabled="false" width="100%">Mal clipado</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto26" runat="server" Style="text-align: center" Enabled="false" width="100%">Suciedad</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto27" runat="server" Style="text-align: center" Enabled="false" width="100%">Punzonado NOK</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto28" runat="server" Style="text-align: center" Enabled="false" width="100%">Láser ilegible</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto25" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto26" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto27" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto28" runat="server" Style="text-align: center" Enabled="false" width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th colspan="4">
                                        <asp:TextBox ID="THNotas" runat="server" Style="text-align: center" Enabled="false" width="100%">Incidencias</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">       
                                        <asp:TextBox ID="TDNotas" runat="server" Style="text-align: center" Enabled="false" width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>



                            </div>
                            <div class="form-group">
                      <label for="imagenes"><span class="glyphicon glyphicon-picture"></span> Imágenes</label><br />
                            <div class="thumbnail" style="vertical-align:middle">                        
                                <asp:HyperLink id="linkimagen1" NavigateUrl="" ImageUrl= "" Text="Defecto 1" Target="_new" runat="server" Width="32%"/>      
                                <asp:HyperLink id="linkimagen2" NavigateUrl="" ImageUrl= "" Text="Defecto 2" Target="_new" runat="server" Width="32%"/>      
                                <asp:HyperLink id="linkimagen3" NavigateUrl="" ImageUrl= "" Text="Defecto 3" Target="_new" runat="server" Width="32%"/>      
                            </div>
                      </div>
                    </div>
                    <div class="modal-footer">
                      <button  class="btn btn-info btn-default pull-right" runat ="server" onserverclick="CargaHistorico" > Ver histórico</button>
                       
                      <button type="submit" class="btn btn-danger btn-default pull-right" data-dismiss="modal"> Volver</button>
                    </div>
                  </div>
      
                </div>
              </div>    
 </form>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
      <script src="js/json2.js" type="text/javascript"></script>
      <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
      <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</body>
</html>
