<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPIFichasParametros.aspx.cs"
    Inherits="ThermoWeb.KPIFichasParametros" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fichas de parametros</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <%-- <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous"> --%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <%-- <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script> --%>

    <script src="js/json2.js" type="text/javascript"></script>
</head>
<body>
    <form id="cabecera" runat="server">
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
        <li><a href="FichasParametros_nuevo.aspx">Crear ficha</a></li>
        <li><a href="FichasParametros.aspx">Consultar ficha de fabricación</a></li> 
        <li><a href="ListaFichasParametros.aspx">Ver listado de fichas</a></li>       
      </ul>
    </div>
  </div>
</nav>
   <div class="container">
    <div class="row">
                <div class="col-lg-4">
                    
                </div>
                </div>
                <div class="row">
                <div class="col-lg-4">
                    
                </div>
                <div class="col-lg-4">
                </div>
                <div class="col-lg-4 text-right">
                </div>
                </div>
             
   <div class="container">
    <div class="row">
                <div class="col-lg-4">    
                 <h2>&nbsp;&nbsp;&nbsp; Top autores de fichas </h2>        
                <div class="table-responsive">
        <asp:GridView ID="dgv_ListaEncargados" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
            EmptyDataText="No hay fichas para mostrar.">
            <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
             --%>
            <HeaderStyle BackColor="#DFDFDF" Font-Bold="True" ForeColor="Black" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>
                
                <asp:TemplateField HeaderText="Encargado">
                    <ItemTemplate>
                        <asp:Label ID="lblEncargado" runat="server" Text='<%#Eval("PElaborado") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fichas creadas">
                    <ItemTemplate>
                        <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("FICHAS") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Última fecha">
                    <ItemTemplate>
                        <asp:Label ID="lblFicha" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
   
    </div>
     </div>
     <div class="col-lg-8">
                 <h2>&nbsp;&nbsp;&nbsp; Referencias produciendo sin digitalizar </h2>            
                <div class="table-responsive">
        <asp:GridView ID="KPIListaMissing" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
            EmptyDataText="No hay fichas para mostrar.">
            <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
             --%>
            <HeaderStyle BackColor="#DFDFDF" Font-Bold="True" ForeColor="Black" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>
                
                <asp:TemplateField HeaderText="Maquina">
                    <ItemTemplate>
                        <asp:Label ID="lblKPEncargado" runat="server" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orden">
                    <ItemTemplate>
                        <asp:Label ID="lblKPOrden" runat="server" Text='<%#Eval("Orden") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia">
                    <ItemTemplate>
                        <asp:Label ID="lblKPRef" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>         
                <asp:TemplateField HeaderText="Descripcion">
                    <ItemTemplate>
                        <asp:Label ID="lblKPDescr" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>         
                
            </Columns>
        </asp:GridView>
   
    </div>
     </div>


                    </div>
                </div>
  
    <%-- <div class="col-lg-2">
        <div class="form-group">
            <label for="sel1">
                Selecciona un filtro</label>
            <select class="form-control" runat="server" id="cbFiltro">
                <option>Activas</option>
                <option>Todas</option>
            </select>
        </div>
        <div class="form-group">
            <button id="btnCargarFiltro" runat="server" onserverclick="cargar_filtro" type="button" class="btn">
                Cargar
            </button>
        </div>
    </div>
    --%>
</div> 

    </form>
</body>
</html>
