<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Area_Rechazo.aspx.cs" Inherits="ThermoWeb.AREA_RECHAZO.Area_Rechazo"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de jaula de rechazo</title>
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
          <a class="navbar-brand" href="../index.aspx">Thermolympic S.L.</a>
        </div>
        <div class="collapse navbar-collapse" id="myNavbar">
          <ul class="nav navbar-nav"> 
                
          </ul>
        </div>
      </div>
    </nav>
    <h1>
        &nbsp;&nbsp;&nbsp; Area de rechazo</h1>
    <div class="table-responsive">
        <asp:GridView ID="dgv_AreaRechazo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
            DataKeyNames="Id" ShowFooter="true" OnRowUpdating="GridView_RowUpdating"
            OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
            OnRowCommand="GridView_RowCommand" OnRowDeleting="GridView_RowDeleting"
            EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtReferencia" runat="server" Width="80px" Text='<%#Eval("Referencia") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footReferencia" Width="80px" runat="server" />
                        <asp:RequiredFieldValidator ID="vstorReferencia" runat="server" ControlToValidate="footReferencia"
                            Text="*" ValidationGroup="validaiton" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Motivo">
                    <ItemTemplate>
                        <asp:Label ID="lblMotivo" runat="server" Text='<%#Eval("Motivo") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMotivo" runat="server" Width="380px" Text='<%#Eval("Motivo") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footMotivo" Width="380px" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Responsable entrada">
                    <ItemTemplate>
                        <asp:Label ID="lblResponsableEntrada" runat="server" Text='<%#Eval("ResponsableEntrada") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtResponsableEntrada" runat="server" Width="120px" Text='<%#Eval("ResponsableEntrada") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footResponsableEntrada" Width="120px" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("Cantidad") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCantidad" runat="server" Width="80px" Text='<%#Eval("Cantidad") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footCantidad" Width="80px" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha entrada">
                    <ItemTemplate>
                        <asp:Label ID="lblFechaEntrada" runat="server" Text='<%#Eval("FechaEntrada", "{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFechaEntrada" runat="server" Width="100px" Text='<%#Eval("FechaEntrada", "{0:dd/MM/yyyy}") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footFechaEntrada" Width="100px" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha salida">
                    <ItemTemplate>
                        <asp:Label ID="lblFechaSalida" runat="server" Text='<%#Eval("FechaSalida", "{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFechaSalida" runat="server" Width="100px" Text='<%#Eval("FechaSalida", "{0:dd/MM/yyyy}") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footFechaSalida" Width="100px" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Debe salir">
                    <ItemTemplate>
                        <asp:Label ID="lblDebeSalir" runat="server" Text='<%#Eval("DebeSalir", "{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDebeSalir" runat="server" Width="100px" Text='<%#Eval("DebeSalir", "{0:dd/MM/yyyy}") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footDebeSalir" Width="100px" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Decision">
                    <ItemTemplate>
                        <asp:Label ID="lblDecision" runat="server" Text='<%#Eval("Decision") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDecision" runat="server" Width="100px" Text='<%#Eval("Decision") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footDecision" Width="100px" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Responsable salida">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenciaResponsableSalida" runat="server" Text='<%#Eval("ResponsableSalida") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtResponsableSalida" runat="server" Width="130px" Text='<%#Eval("ResponsableSalida") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footResponsableSalida" Width="130px" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Observaciones">
                    <ItemTemplate>
                        <asp:Label ID="lblObservaciones" runat="server" Text='<%#Eval("Observaciones") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtObservaciones" runat="server" Width="310px" Text='<%#Eval("Observaciones") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footObservaciones" Width="310px" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="220px">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...--%>
                        <asp:Button ID="btnDelete" runat="server" Text="Quitar" CssClass="btn btn-danger"
                            CommandName="Delete" OnClientClick="return confirm('¿Seguro que quieres eliminar esta fila?');" />
                        <asp:Button ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-info" CommandName="Edit" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--Botones de grabar y cancelar la edición de registro...--%>
                        <asp:Button ID="btnUpdate" runat="server" Text="Grabar" CssClass="btn btn-success"
                            CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar esta fila?');" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                            CommandName="Cancel" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="ButtonAdd" runat="server" CommandName="AddNew" Text="Añadir nueva fila"
                            CssClass="btn btn-success" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="col-lg-2">
        <div class="form-group">
            <label for="sel1">
                Selecciona un filtro</label>
            <select class="form-control" runat="server" id="cbFiltro">
                <option>Activas</option>
                <option>Todas</option>
            </select>
        </div>
        <div class="form-group">
            <button id="btnCargarFiltro" runat="server" onserverclick="Cargar_filtro" type="button" class="btn">
                Cargar
            </button>
        </div>
    </div>
    </form>
</body>
</html>
