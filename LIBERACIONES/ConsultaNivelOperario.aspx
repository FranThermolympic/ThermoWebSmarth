<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ConsultaNivelOperario.aspx.cs" Inherits="ThermoWeb.LIBERACIONES.ConsultaNivelOperario"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Operarios actuales con nivel I</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
      <script src="js/json2.js" type="text/javascript"></script>
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
         <div class="col-lg-12">
                <div class="col-lg-9"></div>
                <div class="col-lg-3" style="text-align:right">
               <label>Última actualización:&nbsp</label><asp:TextBox ID="FECHAACT" Font-Size="Large" runat="server" BorderColor="Transparent" BackColor="Transparent" Enabled="false"></asp:TextBox>
            </div>
            </div>
        <div class="col-lg-12">
    <div class="table-responsive">
    <asp:GridView ID="dgv_AreaRechazo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
            EmptyDataText="No hay revisiones para mostrar.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>     
                <asp:TemplateField HeaderText="Máquina" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblMáquina" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("C_MACHINE_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Font-Size="Large" Text='<%#Eval("C_PRODUCT_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="30%">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Font-Size="Large" Text='<%#Eval("C_PRODLONGDESCR") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nº" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblOperario" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("C_CLOCKNO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="30%">
                    <ItemTemplate>
                        <asp:Label ID="lblNombre" runat="server" Font-Size="Large" Text='<%#Eval("C_NAME") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Horas" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblHoras" runat="server" Font-Size="X-Large" Text='<%#Eval("TIEMPOHORAS") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nivel" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" Visible="false" >
                    <ItemTemplate>
                        <asp:Label ID="lblNivel" runat="server" Font-Bold="true" Font-Size="X-Large"  Text='<%#Eval("NIVEL") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
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
