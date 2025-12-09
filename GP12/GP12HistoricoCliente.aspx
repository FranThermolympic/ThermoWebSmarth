<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GP12HistoricoCliente.aspx.cs" Inherits="ThermoWeb.GP12.GP12HistoricoCliente"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Histórico de producto</title>
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
                        <li><a href="GP12HistoricoReferencia.aspx">Histórico de pieza</a></li>
                        <li><a href="../KPI/KPI_GP12.aspx">Cuadro de mando</a></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="GP12FAQ.aspx"><span class="glyphicon glyphicon-question-sign">AYUDA</span></a></li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="row" runat="server" visible="false">
            <div class="col-lg-12">
                <div class="col-lg-4">
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table width="100%">
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox ID="tbCliente" Width="100%" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="tituloReferenciaCarga" Width="100%" runat="server" Style="text-align: center" Enabled="false">Referencia</asp:TextBox>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="tbReferenciaCarga" Width="100%" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                    </td>
                                    <th>
                                        <asp:TextBox ID="tituloMolde" runat="server" Width="100%" Style="text-align: center" Enabled="false">Molde</asp:TextBox>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="tbMolde" Width="100%" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox ID="tbDescripcionCarga" Width="100%" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-lg-8">
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table width="100%">
                                <tr>
                                    <th>
                                        <asp:TextBox ID="CabeceraBlanco" Width="100%" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="CabeceraProdRev" runat="server" Width="100%" Style="text-align: center" Enabled="false">Buenas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="CabeceraRechazas" runat="server" Width="100%" Style="text-align: center" Enabled="false">Rechazadas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="CabeceraRecuperadas" runat="server" Width="100%" Style="text-align: center" Enabled="false">Recuperadas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="CabeceraHoras" runat="server" Width="100%" Style="text-align: center" Enabled="false">Tiempo (H)</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="CabeceraScrap" runat="server" Width="100%" Style="text-align: center" Enabled="false">Scrap (€)</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="CabeceraInspeccion" runat="server" Width="100%" Style="text-align: center" Enabled="false">Inspección(€)</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="CabeceraMuro" runat="server" Width="100%" Style="text-align: center" Enabled="false">Total GP12(€)</asp:TextBox>
                                    </th>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="LineaMaquina" Width="100%" runat="server" Style="text-align: center" Enabled="false">Máquina</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="MaquinaFabricadas" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MaquinaRechazadas" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MaquinaRecuperadas" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MaquinaHoras" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MaquinaScrap" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MaquinaInspeccion" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MaquinaTotal" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="LineaMuro" Width="100%" runat="server" Style="text-align: center" Enabled="false">GP12</asp:TextBox>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="MuroRevisadas" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MuroRechazadas" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MuroRecuperadas" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MuroHoras" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MuroScrap" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MuroInspeccion" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MuroTotal" runat="server" Width="100%" Style="text-align: center" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel-body">
                    <div class="col-lg-2">
                        <br />
                        <button id="VerTodas" runat="server" onserverclick="cargar_todas" visible="false" type="button" class="btn btn-lg btn-primary" style="width: 65%; text-align: left">
                            <span class="glyphicon glyphicon-list"></span>Ver todas</button>
                    </div>
                    
                    <div class="col-lg-2">
                        <label for="usr">Cliente:</label>
                        <asp:DropDownList ID="lista_clientes" runat="server" CssClass="form-control">
                        </asp:DropDownList>
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

                        <label for="usr">Fecha:</label>
                        <asp:TextBox ID="selectFecha" runat="server" CssClass="textbox Add-text" Width="100%" Height="35px" autocomplete="off" />

                    </div>

                    <div class="col-lg-1">
                        <label for="usr">Lote:</label>
                        <asp:TextBox ID="selectLote" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />
                    </div>
                    <div class="col-lg-2">
                        <label for="usr">Razón:</label>
                        <asp:DropDownList ID="lista_estado" runat="server" CssClass="form-control">
                            <asp:ListItem Selected="True" Value=""> - </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2 text-right">
                        <br />
                        <button id="Button2" runat="server" onserverclick="cargar_Filtrados" type="button" class="btn btn-lg btn-info" style="width: 75%; text-align: left">
                            <span class="glyphicon glyphicon-search"></span>Filtrar</button>
                    </div>
                </div>



            </div>
        </div>


        <div class="row">
            <div class="col-lg-12">

                <div class="panel-body">

                    <div class="table-responsive">

                        <asp:GridView ID="dgv_AreaRechazo" runat="server" AllowSorting="True"
                            Width="98.5%" CssClass="table table-striped table-bordered table-hover" ShowFooter="true" AutoGenerateColumns="false" OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="GridView1_DataBound"
                            EmptyDataText="No hay revisiones para mostrar.">
                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                            <EditRowStyle BackColor="#ffffcc" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemTemplate>
                                        <asp:Button ID="button2" runat="server" class="btn btn-info btn-xs" CommandName="redirect" CommandArgument='<%#Eval("Referencia")%>' UseSubmitBehavior="true" Text="+" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Horas" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                                        <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("Nombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Buenas">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuenas" runat="server" Text='<%#Eval("PiezasOK") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retrab." ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetrabajadas" runat="server" Text='<%#Eval("Retrabajadas") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Malas" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMalas" runat="server" Text='<%#Eval("PiezasNOK") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHoras" runat="server" Text='<%#Eval("PORCSCRAP","{0:P1}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coste Chatarra" ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCosteChatarra" runat="server" Text='<%#Eval("CosteScrapRevision","{0:c}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coste Operario" ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCosteTiempo" runat="server" Text='<%#Eval("CostePiezaRevision","{0:c}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coste Revisión" ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("CosteRevision","{0:c}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Falta material" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto1" runat="server" Text='<%#Eval("Def1") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ráfagas" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto2" runat="server" Text='<%#Eval("Def2") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Roturas" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto3" runat="server" Text='<%#Eval("Def3") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Montaje NOK" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto4" runat="server" Text='<%#Eval("Def4") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deform." ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto5" runat="server" Text='<%#Eval("Def5") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Etiqueta NOK" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto6" runat="server" Text='<%#Eval("Def6") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Burbujas" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto7" runat="server" Text='<%#Eval("Def7") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Arrastre mat." ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto8" runat="server" Text='<%#Eval("Def8") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Raya / Expulsor" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto9" runat="server" Text='<%#Eval("Def9") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quema." ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto10" runat="server" Text='<%#Eval("Def10") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brillos" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto11" runat="server" Text='<%#Eval("Def11") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mat. contam." ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto12" runat="server" Text='<%#Eval("Def12") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rechupes" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto13" runat="server" Text='<%#Eval("Def13") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Color NOK" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto14" runat="server" Text='<%#Eval("Def14") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manchas" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto15" runat="server" Text='<%#Eval("Def15") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rebabas" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto16" runat="server" Text='<%#Eval("Def16") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sólo plástico" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto17" runat="server" Text='<%#Eval("Def17") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sólo goma" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto18" runat="server" Text='<%#Eval("Def18") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Otros" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto19" runat="server" Text='<%#Eval("Def19") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Electro-válvula" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto20" runat="server" Text='<%#Eval("Def20") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grapa: Posición" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto21" runat="server" Text='<%#Eval("Def21") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grapa: Altura" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto22" runat="server" Text='<%#Eval("Def22") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tubo: Deformado" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto23" runat="server" Text='<%#Eval("Def23") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tubo: Mal puesto" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto24" runat="server" Text='<%#Eval("Def24") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mal clipado" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto25" runat="server" Text='<%#Eval("Def25") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Suciedad" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto26" runat="server" Text='<%#Eval("Def26") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Punzon NOK" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto27" runat="server" Text='<%#Eval("Def27") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Láser ilegible" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto28" runat="server" Text='<%#Eval("Def28") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Defecto29" ItemStyle-Width="120px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto29" runat="server" Text='<%#Eval("Def29") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Defecto30" ItemStyle-Width="120px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefecto30" runat="server" Text='<%#Eval("Def30") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal bootstrap-->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="background-color: Blue; color: White; text-align: center; font-size: x-large">
                        Detalles
                    </div>
                    <div class="modal-body" style="padding: 40px 50px;">

                        <div class="form-group">
                            <label for="revisor"><span class="glyphicon glyphicon-info-sign"></span>Referencia</label><br />
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="detalleReferencia" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="detalleReferenciaNombre" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="form-group">
                            <label for="revisor"><span class="glyphicon glyphicon-eye-open"></span>Revisión</label><br />
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TbOperarioRevision" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbEmpresaRevision" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="form-group">
                            <label for="operario"><span class="glyphicon glyphicon-user"></span>Operarios</label><br />
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="Operario1" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Operario2" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="Operario3" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Operario4" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="form-group">
                            <label for="costes"><span class="glyphicon glyphicon-euro"></span>Costes</label><br />
                            <table width="100%">
                                <tr>
                                    <th>
                                        <asp:TextBox ID="ThCosteHoras" runat="server" Style="text-align: center" Enabled="false" Width="100%">Horas de revisión</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThCosteChatarra" runat="server" Style="text-align: center" Enabled="false" Width="100%">Coste Chatarra</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThCosteOperario" runat="server" Style="text-align: center" Enabled="false" Width="100%">Coste Revisión</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThCosteTotal" runat="server" Style="text-align: center" Enabled="false" Width="100%">Coste Total</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="CosteHoras" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CosteChatarra" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CosteOperario" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CosteTotal" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="form-group">
                            <label for="defectos"><span class="glyphicon glyphicon-trash"></span>Defectos</label><br />
                            <table width="100%">
                                <tr>
                                    <th>
                                        <asp:TextBox ID="ThRevisadas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Revisadas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThBuenas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Buenas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThRetrabajadas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Retrabajadas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="ThMalas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Malas</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TbRevisadas" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbBuenas" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbRetrabajadas" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TbMalas" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto1" runat="server" Style="text-align: center" Enabled="false" Width="100%">Falta llenado</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto2" runat="server" Style="text-align: center" Enabled="false" Width="100%">Ráfagas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto3" runat="server" Style="text-align: center" Enabled="false" Width="100%">Roturas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto4" runat="server" Style="text-align: center" Enabled="false" Width="100%">Montaje NOK</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto1" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto2" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto3" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto4" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto5" runat="server" Style="text-align: center" Enabled="false" Width="100%">Deformaciones</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto6" runat="server" Style="text-align: center" Enabled="false" Width="100%">Etiqueta NOK</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto7" runat="server" Style="text-align: center" Enabled="false" Width="100%">Burbujas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto8" runat="server" Style="text-align: center" Enabled="false" Width="100%">Chapa visible</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto5" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto6" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto7" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto8" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto9" runat="server" Style="text-align: center" Enabled="false" Width="100%">Rayas/Marcas expulsor</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto10" runat="server" Style="text-align: center" Enabled="false" Width="100%">Quemazos</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto11" runat="server" Style="text-align: center" Enabled="false" Width="100%">Brillos</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto12" runat="server" Style="text-align: center" Enabled="false" Width="100%">M. contaminado</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto9" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto10" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto11" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto12" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto13" runat="server" Style="text-align: center" Enabled="false" Width="100%">Rechupes</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto14" runat="server" Style="text-align: center" Enabled="false" Width="100%">Color NOK</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto15" runat="server" Style="text-align: center" Enabled="false" Width="100%">Manchas</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto16" runat="server" Style="text-align: center" Enabled="false" Width="100%">Rebabas</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto13" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto14" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto15" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto16" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto17" runat="server" Style="text-align: center" Enabled="false" Width="100%">Sólo plástico</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto18" runat="server" Style="text-align: center" Enabled="false" Width="100%">Sólo goma</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto19" runat="server" Style="text-align: center" Enabled="false" Width="100%">Otros</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto20" runat="server" Style="text-align: center" Enabled="false" Width="100%">Electroválvula</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto17" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto18" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto19" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto20" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto21" runat="server" Style="text-align: center" Enabled="false" Width="100%">Grapa: Posición</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto22" runat="server" Style="text-align: center" Enabled="false" Width="100%">Grapa: Altura</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto23" runat="server" Style="text-align: center" Enabled="false" Width="100%">Tubo: Deformado</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto24" runat="server" Style="text-align: center" Enabled="false" Width="100%">Tubo: Mal puesto</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto21" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto22" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto23" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto24" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:TextBox ID="THDefecto25" runat="server" Style="text-align: center" Enabled="false" Width="100%">Mal clipado</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto26" runat="server" Style="text-align: center" Enabled="false" Width="100%">Suciedad</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto27" runat="server" Style="text-align: center" Enabled="false" Width="100%">Punzonado NOK</asp:TextBox>
                                    </th>
                                    <th>
                                        <asp:TextBox ID="THDefecto28" runat="server" Style="text-align: center" Enabled="false" Width="100%">Láser ilegible</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TDDefecto25" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto26" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto27" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TDDefecto28" runat="server" Style="text-align: center" Enabled="false" Width="100%">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th colspan="4">
                                        <asp:TextBox ID="THNotas" runat="server" Style="text-align: center" Enabled="false" Width="100%">Incidencias</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox ID="TDNotas" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox ID="TDObservaciones" runat="server" Style="text-align: center" Enabled="false" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>



                        </div>
                        <div class="form-group">
                            <label for="imagenes"><span class="glyphicon glyphicon-picture"></span>Imágenes</label><br />
                            <div class="thumbnail" style="vertical-align: middle">
                                <asp:HyperLink ID="linkimagen1" NavigateUrl="" ImageUrl="" Text="Defecto 1" Target="_new" runat="server" Width="32%" />
                                <asp:HyperLink ID="linkimagen2" NavigateUrl="" ImageUrl="" Text="Defecto 2" Target="_new" runat="server" Width="32%" />
                                <asp:HyperLink ID="linkimagen3" NavigateUrl="" ImageUrl="" Text="Defecto 3" Target="_new" runat="server" Width="32%" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-danger btn-default pull-right" data-dismiss="modal">Volver</button>
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
    <script src="Scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
</body>
</html>
