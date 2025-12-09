<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ListaFichasParametros.aspx.cs"
    Inherits="ThermoWeb.Lista_Ficha" EnableEventValidation="false" %>
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
      </ul>
    </div>
  </div>
</nav>
   <div class="container">
    <div class="row">
                <div class="col-lg-4">
                    <div class="form-group">
                        <div class="input-group">
                            <input id="tbBuscarFicha" runat="server" type="text" class="form-control" placeholder="Escribe la referencia">
                            <div class="input-group-btn">
                                <button id="btnBuscarFicha" runat="server" onserverclick="buscarFicha" class="btn btn-info" type="submit">
                                    <span style="margin-right: 8px" class="glyphicon glyphicon-search"></span>Buscar
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
                <div class="row">
                <div class="col-lg-4">
                    <div class="form-group">
                        <div class="input-group">
                            <input id="tbBuscarFichaMolde" runat="server" type="text" class="form-control" placeholder="Escribe el molde">
                            <div class="input-group-btn">
                                <button id="btnBuscarFichaMolde" runat="server" onserverclick="buscarFichaMolde" class="btn btn-info" type="submit">
                                    <span style="margin-right: 8px" class="glyphicon glyphicon-search"></span>Buscar
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                </div>
                <div class="col-lg-4 text-right">
                <button id="OrdenarRef" runat="server" onserverclick="cargarlistaordenadareferencia" class="btn btn-info" type="submit" visible="false"><span style="margin-right: 8px" class="glyphicon glyphicon-th-list"></span>Ordenar por referencia </button>
                <button id="OrdenaFecha" runat="server" onserverclick="cargarlistaordenadafecha" class="btn btn-info" type="submit"><span style="margin-right: 8px" class="glyphicon glyphicon-th-list"></span>Ordenar por fecha </button>
                </div>
                </div>
             
            
<div class="table-responsive">
        <asp:GridView ID="dgv_ListaFichasParam" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowDeleting="GridView_RowDeleting" OnRowCommand="ContactsGridView_RowCommand"
            EmptyDataText="No hay fichas para mostrar.">
            <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
             --%>
            <HeaderStyle BackColor="#DFDFDF" Font-Bold="True" ForeColor="Black" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>
                
                <asp:TemplateField HeaderText="Referencia">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Molde">
                    <ItemTemplate>
                        <asp:Label ID="lblMolde" runat="server" Text='<%#Eval("CodMolde") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripción">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Maquina">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Version">
                    <ItemTemplate>
                        <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("Version") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha">
                    <ItemTemplate>
                        <asp:Label ID="lblFecha" runat="server" Text='<%#Eval("Fechaord") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("Cliente") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Autor">
                    <ItemTemplate>
                        <asp:Label ID="lblAutor" runat="server" Text='<%#Eval("Autor") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                             
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="220px">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente..--%>
                        <%--<asp:Button ID="btnEdit" runat="server" Text="Ver ficha" CssClass="btn btn-info" CommandName="Edit" /> --%>
                        <asp:Button ID = "button2" runat = "server" CommandName="Redirect" class="btn btn-info" CommandArgument='<%#Eval("Referencia")+"&MAQUINA="+ Eval("Maquina")+"&VERSION="+Eval("Version")%>' Text="Ver ficha" />
                        <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn btn-danger"
                            CommandName="Delete" OnClientClick="return confirm('Se eliminarán todas las versiones de esta ficha. ¿Seguro que quieres continuar? ');" />     
                        
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        Botones de grabar y cancelar la edición de registro...
                        <asp:Button ID="btnUpdate" runat="server" Text="Grabar" CssClass="btn btn-success"
                            CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar esta fila? ');" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                            CommandName="Cancel" />
                    </EditItemTemplate>--%>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
   
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
