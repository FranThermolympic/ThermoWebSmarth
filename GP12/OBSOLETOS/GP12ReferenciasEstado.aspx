<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GP12ReferenciasEstado.aspx.cs" Inherits="ThermoWeb.GP12.GP12ReferenciasEstado"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Referencias y estado de revisión</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="js/json2.js" type="text/javascript"></script>
    <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
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
            showAnim: "fold",
            yearRange: "c-20:c+10",
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa']
        });
    });
    </script>
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
          <a class="navbar-brand" href="../index.aspx">Thermolympic S.L.</a>
        </div>
        <div class="collapse navbar-collapse" id="myNavbar">
          <ul class="nav navbar-nav"> 
                <li><a href="GP12.aspx">Iniciar revisión</a></li>
                <li><a href="PrevisionGP12.aspx" >Planificación de cargas</a></li>
                <li><a href="GP12ReferenciasEstado.aspx" >Estado de referencias</a></li>
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">Informes de revisión
                    <span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <li><a href="GP12Historico.aspx">Últimas revisiones</a></li>
                        <li><a href="GP12HistoricoCliente.aspx">Detalles</a></li>
                        <li><a href="../KPI/KPI_GP12.aspx">Cuadro de mando</a></li>
                        <li><a href="GP12RegistroComunicaciones.aspx">Registro de comunicaciones</a></li>
                    </ul>
                </li> 
                  
          </ul>
          <ul class="nav navbar-nav navbar-right">
                    <li><a href="#" runat="server" onserverclick="ImportardeBMS"><span class="glyphicon glyphicon-circle-arrow-down"></span> Importar de BMS</a></li>
                </ul>    
        </div>
      </div>
    </nav>
    <h1>&nbsp;&nbsp;&nbsp; Gestión de Muro de calidad</h1>
    <div class="row">
            <div class="col-lg-12">
                
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                            <br />
                            <button id="VerTodas" runat="server" onserverclick="cargar_todas" type="button" class="btn btn-lg btn-primary" style="width:85%; text-align:left">
                            <span class="glyphicon glyphicon-list"></span> Ver todas</button>
                            <button id="VerRevision" runat="server" onserverclick="cargar_EnRevision" type="button" class="btn btn-lg btn-primary" visible="false" style="width:85%; text-align:left">
                            <span class="glyphicon glyphicon-indent-right"></span> Ver en revisión</button>
                            </div>
                            <div class="col-lg-1">
                                <label for="usr">Referencia:</label>
                                        <asp:TextBox ID="selectReferencia" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />
                                                            
                            </div>                                                                                            
                            <div class="col-lg-1">                         
                                <label for="usr">Molde:</label>
                                       <asp:TextBox ID="selectMolde" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />                               
                            </div>
                            <div class="col-lg-2">                                
                                    <label for="usr">Estado:</label>
                                        <asp:DropDownList ID="lista_estado" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value=""> - </asp:ListItem>
                                    </asp:DropDownList>                                                             
                            </div>
                            <div class="col-lg-2">                               
                                <label for="usr">Cliente:</label>
                                        <asp:DropDownList ID="lista_clientes" runat="server" CssClass="form-control">     
                                        
                                    </asp:DropDownList>                           
                            </div>
                            <div class="col-lg-2">                         
                                <label for="usr">Responsable:</label>
                                        <asp:DropDownList ID="lista_responsable" runat="server" CssClass="form-control">
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
    </div>
    <div class="table-responsive">
        <asp:GridView ID="dgv_AreaRechazo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnRowDataBound="GridView_RowDataBound" OnRowCommand="ContactsGridView_RowCommand"
            EmptyDataText="There are no data records to display." on>
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <%-- DataKeyNames="Id" ShowFooter="true"  "
            "OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing OnRowUpdating="GridView_RowUpdating" 
            OnRowCommand="gridView_RowCommand" OnRowDeleting="GridView_RowDeleting" OnRowCommand="gridView_RowCommand" --%>
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("REF") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtReferencia" runat="server" Text='<%#Eval("REF") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Molde" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblMolde" runat="server" Text='<%#Eval("Molde") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtMolde" runat="server" Text='<%#Eval("Molde") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="250px">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoActual" runat="server" Text='<%#Eval("EstadoActual") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="txtEstadoActual" runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Responsable" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="lblResponsable" runat="server" Text='<%#Eval("Responsable") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="txtResponsable" runat="server" Width="80px" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha de revisión">
                    <ItemTemplate>
                        <asp:Label ID="lblFecharev" runat="server" Text='<%#Eval("Fecharev", "{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%-- <asp:TextBox ID="txtFecharev" runat="server" Width="80px" Text='<%#Eval("Fecharev", "{0:dd/MM/yyyy}") %>' />--%>
                        <asp:Label ID="txtFecharev" runat="server" Width="80px" Text='<%#DateTime.Now.ToString("dd/MM/yyyy") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Previsión de salida">
                    <ItemTemplate>
                        <asp:Label ID="lblFechaprevsalida" runat="server" Text='<%#Eval("Fechaprevsalida", "{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFechaprevsalida" runat="server" CssClass="textbox Add-text" Width="80px"  autocomplete="off" Text='<%#Eval("Fechaprevsalida", "{0:dd/MM/yyyy}") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Est. Anterior" ItemStyle-Width="100px" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoAnterior" runat="server" Text='<%#Eval("EstadoAnterior") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtEstadoAnterior" runat="server" Width="80px" Text='<%#Eval("EstadoActual") %>'/>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblFechaestanterior" runat="server" Text='<%#Eval("Fechaestanterior", "{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                    
                    <EditItemTemplate>
                        <asp:Label ID="txtFechaestanterior" runat="server" Width="80px" Text='<%#Eval("Fecharev", "{0:dd/MM/yyyy}") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Observaciones">
                    <ItemTemplate>
                        <asp:Label ID="lblObservaciones" runat="server" Width="100%" Text='<%#Eval("Observaciones") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtObservaciones" runat="server" Width="100%" Text='<%#Eval("Observaciones") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...--%>
                        <asp:Button ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-info" CommandName="Edit"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--Botones de grabar y cancelar la edición de registro...--%>
                        <asp:Button ID="btnUpdate" runat="server" Text="Guardar" CssClass="btn btn-success" Width="100%"
                            CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar esta fila?');" />
                        <asp:Button ID = "btnDetail" runat = "server" CommandName="Redirect" class="btn btn-default" Width="100%" CommandArgument='<%#Eval("REF")%>' Text="Detalles" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-danger" Width="100%"
                            CommandName="Cancel" />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    
    </form>
</body>
</html>
