<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrevisionGP12.aspx.cs" Inherits="ThermoWeb.GP12.GP12PrevisionGP12"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Planificación de cargas</title>
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
                        <li><a href="GP12HistoricoReferencia.aspx">Detalles</a></li>
                        <li><a href="GP12KPI.aspx">Cuadro de mando</a></li>
                    </ul>
                </li>                   
          </ul>    
        </div>
      </div>
    </nav>
    <h1>&nbsp;&nbsp;&nbsp; Entregas con Muro de calidad</h1>
    <div class="row">
            <div class="col-lg-12">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2 text-left">
                                 <label for="usr">Día de entrega:</label>
                                 <asp:TextBox ID="txtFechaprevsalida" runat="server" CssClass="textbox Add-text" Width="100%" Height="35px" autocomplete="off" />
                            </div>
                            <div class="col-lg-1 text-right">
                            <br />
                                 <button id="Button2" runat="server" onserverclick="filtrodia" type="button" class="btn btn-lg btn-info" style="width:100%; text-align:left">
                                 <span class="glyphicon glyphicon-search"></span> Filtrar</button>
                            </div>
                         </div>
                        </div>   
                </div>
            </div>
        <div class="row">
        <div class="col-lg-12">
        <div class="col-lg-2">
            <button type="button" class="btn" data-toggle="collapse" data-target="#demo">Ver atrasadas</button>
        </div>
        <div class="col-lg-10">
        </div>
        <div class="col-lg-12">
        <div id="demo" class="collapse">
            <div class="table-responsive">
            <asp:GridView ID="dgv_atrasados" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                EmptyDataText="There are no data records to display.">
                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#ffffcc" />
                <Columns>
                    <asp:TemplateField HeaderText="Fecha de entrega" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblFecharev" runat="server" Text='<%#Eval("FechaEntrega", "{0:dd/MM/yyyy}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="350px">
                        <ItemTemplate>
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("cantidad") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Razón de revisión" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Razon") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        </div>
                </div>
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="dgv_PrevisionGP12" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
            EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <%-- DataKeyNames="Id" ShowFooter="true"  OnRowUpdating="GridView_RowUpdating"
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
            OnRowCommand="gridView_RowCommand" OnRowDeleting="GridView_RowDeleting" --%>
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="Fecha de entrega" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblFecharev" runat="server" Text='<%#Eval("FechaEntrega", "{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="350px">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("cantidad") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Razón de revisión" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Razon") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>




    </form>
</body>
</html>
