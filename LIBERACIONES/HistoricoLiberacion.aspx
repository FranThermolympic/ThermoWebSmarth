<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="HistoricoLiberacion.aspx.cs" Inherits="ThermoWeb.LIBERACIONES.HistoricoLiberacion"
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
      
      <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
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
                <li><a href="EstadoLiberacion.aspx">Ver máquinas y liberaciones</a></li>
          
          </ul>
        </div>
      </div>
    </nav>
    <div class="row">
            <div class="col-lg-12">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                            <br />
                            <button id="VerTodas" runat="server" onserverclick="Cargar_todas" type="button" class="btn btn-lg btn-primary" style="width:65%; text-align:left">
                            <span class="glyphicon glyphicon-list"></span> Ver todas</button>
                            </div>
                            <div class="col-lg-1">
                                <label for="usr">Orden:</label>
                                        <asp:TextBox ID="selectOrden" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />          
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
                                <label for="usr">Máquina:</label>
                                        <asp:DropDownList ID="lista_maquinas" runat="server" CssClass="form-control">
                                            <asp:ListItem Selected="True" Value=""> - </asp:ListItem>
                                        </asp:DropDownList>                           
                            </div>
                            <div class="col-lg-1">                               
                                <label for="usr">Cliente:</label>
                                        <asp:DropDownList ID="lista_clientes" runat="server" CssClass="form-control">
                                        </asp:DropDownList>                           
                            </div>
                            <div class="col-lg-1"> 
                            <label for="usr">Cambiador:</label>
                            <asp:DropDownList ID="CamblibNom" runat="server" CssClass="form-control" > 
                            </asp:DropDownList> 
                             <asp:DropDownList ID="Camblib" runat="server" CssClass="form-control" > 
                                    <asp:listitem text="-" Value=""></asp:listitem>
                                    <asp:listitem text="Sin liberar" Value="0"></asp:listitem>
                                    <asp:listitem text="Ok condicionada" Value="1"></asp:listitem>
                                    <asp:listitem text="Liberada OK" Value="2"></asp:listitem>
                                    <asp:listitem text="Liberada NOK" Value="3"></asp:listitem>
                             </asp:DropDownList>

                             </div>
                            <div class="col-lg-1"> 
                            <label for="usr">Producción:</label>
                            <asp:DropDownList ID="ProdlibNom" runat="server" CssClass="form-control" > 
                                                                    
                            </asp:DropDownList> 
                            <asp:DropDownList ID="Prodlib" runat="server" CssClass="form-control" > 
                                    <asp:listitem text="-" Value=""></asp:listitem>
                                    <asp:listitem text="Sin liberar" Value="0"></asp:listitem>
                                    <asp:listitem text="Ok condicionada" Value="1"></asp:listitem>
                                    <asp:listitem text="Liberada OK" Value="2"></asp:listitem>
                                    <asp:listitem text="Liberada NOK" Value="3"></asp:listitem>                                   
                                </asp:DropDownList> 

                            </div>
                            <div class="col-lg-1"> 
                            <label for="usr">Calidad:</label>
                            <asp:DropDownList ID="CallibNom" runat="server" CssClass="form-control" > 
                                                                    
                            </asp:DropDownList>
                             <asp:DropDownList ID="Callib" runat="server" CssClass="form-control" > 
                                    <asp:listitem text="-" Value=""></asp:listitem>
                                    <asp:listitem text="Sin liberar" Value="0"></asp:listitem>
                                    <asp:listitem text="Ok condicionada" Value="1"></asp:listitem>
                                    <asp:listitem text="Liberada OK" Value="2"></asp:listitem>
                                    <asp:listitem text="Liberada NOK" Value="3"></asp:listitem>
                                </asp:DropDownList> 
 
                             </div>
                       
                            <div class="col-lg-2 text-right">
                            <br />
                                 <button id="Button2" runat="server" onserverclick="Cargar_Filtrados" type="button" class="btn btn-lg btn-info" style="width:75%; text-align:center">
                                 <span class="glyphicon glyphicon-search"></span>  Filtrar</button>
                            </div>
                         </div>
                        </div>
                </div>
            </div>
    <div class="table-responsive">
    <asp:GridView ID="dgv_AreaRechazo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCommand="GridViewCommandEventHandler" OnRowDataBound="GridView1_DataBound"
            EmptyDataText="No hay revisiones para mostrar.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" Visible="true">
                    <ItemTemplate>
                     <asp:Button ID = "button2" runat ="server" class="btn btn-info btn-xs" CommandName="Redirect" CommandArgument='<%#Eval("Orden")%>' UseSubmitBehavior="true" Text="Ver" />
                    </ItemTemplate>
                </asp:TemplateField>     
                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("Orden") %>' />
                        <asp:Label ID="lblAccionLiberado" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("AccionLiberado") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="250px">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Molde" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblMolde" runat="server" Text='<%#Eval("CodMolde") %>' />
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Máquina" ItemStyle-Width="100px" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Lib. Cambio" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="center" >
                    <ItemTemplate>
                        <asp:Label ID="lblCambio" runat="server" Text='<%#Eval("LIBERARCAMBIO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lib. Producción" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblProduccion" runat="server" Text='<%#Eval("LIBERARPRODUCCION") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lib. Calidad" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblCalidad" runat="server" Text='<%#Eval("LIBERARCALIDAD") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notas" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("NotasLiberado") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="lblAccion" runat="server" Text='<%#Eval("ACCIONES") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>            
 </form>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
      
      <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
      <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</body>
</html>
