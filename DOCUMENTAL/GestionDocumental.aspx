<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GestionDocumental.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.GestionDocumental"
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
          </ul>
          
        </div>
      </div>
    </nav>
    <h1>&nbsp;&nbsp;&nbsp; Gestión de documentación</h1>
    <div class="row">
            <div class="col-lg-12">
                
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                            <br />
                            <button id="VerTodas" runat="server" onserverclick="cargar_todas" type="button" class="btn btn-lg btn-primary" style="width:85%; text-align:left">
                                <span class="glyphicon glyphicon-list"></span> Ver todas</button>
                            </div>
                            <div class="col-lg-6"></div>
                            <div class="col-lg-1">
                                <label for="usr">Referencia:</label>
                                        <asp:TextBox ID="selectReferencia" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />
                            </div>                                                                                            
                            <div class="col-lg-1">                         
                                <label for="usr">Molde:</label>
                                       <asp:TextBox ID="selectMolde" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />                               
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
    <div class="table-responsive" style="overflow:auto">
        <asp:GridView ID="dgv_AreaRechazo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCommand="ContactsGridView_RowCommand"
             EmptyDataText="There are no data records to display.">
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
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Molde" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblMolde" runat="server" Text='<%#Eval("Molde") %>' />
                    </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="250px">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Plan de Control" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblPlanControl" runat="server" Text='<%#Eval("PlanControl") %>' /> 
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Pauta de Control" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblPautaControl" runat="server" Text='<%#Eval("PautaControl") %>' />
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="Operacion Estándar" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblOperacionEstandar" runat="server" Text='<%#Eval("OperacionEstandar") %>' />
                    </ItemTemplate>
                    
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="Operacion Estándar 2" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblOperacionEstandar2" runat="server" Text='<%#Eval("OperacionEstandar2") %>' />
                    </ItemTemplate>
                    
                </asp:TemplateField>                 
                <asp:TemplateField HeaderText="Defoteca" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblDefoteca" runat="server" Text='<%#Eval("Defoteca") %>' />
                    </ItemTemplate>  
                </asp:TemplateField>                 
                <asp:TemplateField HeaderText="Embalaje" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblEmbalaje" runat="server" Text='<%#Eval("Embalaje") %>' />
                    </ItemTemplate>     
                </asp:TemplateField>                    
                 <asp:TemplateField HeaderText="Gp12" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblGp12" runat="server" Text='<%#Eval("Gp12") %>' />
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Retrabajo" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblRetrabajo" runat="server" Text='<%#Eval("PautaRetrabajo") %>' />
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Video" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblVideoAuxiliar" runat="server" Text='<%#Eval("VideoAuxiliar") %>' />
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...
                        <asp:Button ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-info" CommandName="Edit"/>--%>
                        <asp:Button ID = "button2" runat = "server" CommandName="Redirect" class="btn btn-info" CommandArgument='<%#Eval("REF")%>' Text="Ver ficha" />
                        
                    </ItemTemplate> 
                </asp:TemplateField>                   
            </Columns>
        </asp:GridView>
    </div>   
    </form>
</body>
</html>
