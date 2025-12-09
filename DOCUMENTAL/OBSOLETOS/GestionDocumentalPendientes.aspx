<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GestionDocumentalPendientes.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.GestionDocumentalPendientes"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión documental</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

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
                <li><a href="GestionDocumental.aspx">Gestión documental</a></li>
                <li><a href="FichaReferencia.aspx" >Fichas de referencia</a></li>
                <li><a href="AccesoDocumentalMaquina.aspx">Tablero de máquinas</a></li>

          </ul>
          
        </div>
      </div>
    </nav>
    <h1>&nbsp;&nbsp;&nbsp; Documentación pendiente de digitalizar</h1>
    
 
    <div class="table-responsive" style="overflow:auto">
       
        <asp:GridView ID="dgv_PendientesProduciendo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCommand="ContactsGridView_RowCommand"
             EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <%-- DataKeyNames="Id" ShowFooter="true"  "
            "OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing OnRowUpdating="GridView_RowUpdating" 
            OnRowCommand="gridView_RowCommand" OnRowDeleting="GridView_RowDeleting" OnRowCommand="gridView_RowCommand" --%>
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="Maquina">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Documento pendiente">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Documento") %>' />
                    </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Jefe del proyecto">
                    <ItemTemplate>
                        <asp:Label ID="lblPlanControl" runat="server" Text='<%#Eval("Nombre") %>' /> 
                    </ItemTemplate>
                </asp:TemplateField>
                  
            </Columns>
        </asp:GridView>
    </div>   
    </form>
</body>
</html>
