<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GP12KPICAL.aspx.cs"
    Inherits="ThermoWeb.GP12.GP12KPICAL" EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>KPI - Muro de calidad</title>
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
                <li><a href="GP12FAQ.aspx"><span class="glyphicon glyphicon-question-sign"> AYUDA</span></a></li>
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
                <div class="col-lg-5">
                </div>
                <div class="col-lg-3 text-right">      
                    <label for="usr">Periodo de revisión:</label>
                        <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged ="cargar_tablas"> 
                            <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                            <asp:listitem text="2022" Value="2022"></asp:listitem>  
                            <asp:listitem text="2021" Value="2021"></asp:listitem>
                                <asp:listitem text="2020" Value="2020"></asp:listitem>
                                <asp:listitem text="2019" Value="2019"></asp:listitem>
                                <asp:listitem text="2018" Value="2018"></asp:listitem>   
                        </asp:DropDownList> 
                </div>
                </div>
             
  
    <div class="row">
            <h2>&nbsp;&nbsp;&nbsp; Indicadores por mes </h2>
                

     <ul class="nav nav-pills nav-justified">
                <li class="active"><a data-toggle="pill" href="#MENSUAL">MENSUAL</a></li>
                <li><a data-toggle="pill" href="#ANUAL">ANUAL</a></li>
             </ul>
            <div class="tab-content">
            <%-- Abro panel de MENSUAL--%>
     <div id="MENSUAL" class="tab-pane fade in active">
     <div class="row">
                <div class="col-lg-4">
                    
                </div>
                <div class="col-lg-5">
                </div>
                <div class="col-lg-3 text-right">      
                    <label for="usr">Mes:</label>
                        <asp:DropDownList ID="SelecMes" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged ="cargar_tablas"> 
                                <asp:listitem text="Enero" Value="1"></asp:listitem>
                                <asp:listitem text="Febrero" Value="2"></asp:listitem>
                                <asp:listitem text="Marzo" Value="3"></asp:listitem>
                                <asp:listitem text="Abril" Value="4"></asp:listitem>  
                                <asp:listitem text="Mayo" Value="5"></asp:listitem>
                                <asp:listitem text="Junio" Value="6"></asp:listitem>
                                <asp:listitem text="Julio" Value="7"></asp:listitem>
                                <asp:listitem text="Agosto" Value="8"></asp:listitem>
                                <asp:listitem text="Septiembre" Value="9"></asp:listitem>
                                <asp:listitem text="Octubre" Value="10"></asp:listitem> 
                                <asp:listitem text="Noviembre" Value="11"></asp:listitem>
                                <asp:listitem text="Diciembre" Value="12"></asp:listitem>     
                        </asp:DropDownList> 
                </div>
                </div>
    <div class="row">
                <div class="col-lg-3">     
                <h2>&nbsp;&nbsp;&nbsp;Detecciones (Op.) </h2>        
                <div class="table-responsive">
        <asp:GridView ID="dgv_KPI_Operarios" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
            EmptyDataText="No hay fichas para mostrar.">
            <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
             --%>
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>
                
                <asp:TemplateField HeaderText="Operario">
                    <ItemTemplate>
                        <asp:Label ID="lblOperario" runat="server" Text='<%#Eval("IDE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("Operario") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Detecc.">
                    <ItemTemplate>
                        <asp:Label ID="lblFicha" runat="server" Text='<%#Eval("SUMA") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
   
    </div>
     </div>
        <div class="col-lg-9">
            <h2>&nbsp;&nbsp;&nbsp;Nuevas detecciones (Op.) </h2> 
            <div class="table-responsive">
        <asp:GridView ID="NuevoGridOperarios" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
            EmptyDataText="Sin datos para mostrar.">
            
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
            
                <asp:TemplateField HeaderText="Operario" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                    <ItemTemplate>
                        <asp:Label ID="lblOperario" runat="server" Text='<%#Eval("IDE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                    <ItemTemplate>
                        <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("OPERARIO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="# Producciones">
                    <ItemTemplate>
                        <asp:Label ID="lblGp12Detec" runat="server" Text='<%#Eval("PRODUCCIONES") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Defectos Detectados">
                    <ItemTemplate>
                        <asp:Label ID="lblGp12NOK" runat="server" Text='<%#Eval("DETECCIONES") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GP12 - # Apariciones">
                    <ItemTemplate>
                        <asp:Label ID="lblGp12Detec" runat="server" Text='<%#Eval("GP12REVISIONES") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GP12 - Piezas Nok">
                    <ItemTemplate>
                        <asp:Label ID="lblGp12NOK" runat="server" Text='<%#Eval("GP12DETECCIONES") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
        </asp:GridView>
   
    </div>
        </div>
     </div>
  </div>
<%-- --%>
<div id="ANUAL" class="tab-pane fade">
    <div class="row">
                <div class="col-lg-3">     
                <h2>&nbsp;&nbsp;&nbsp;Detecciones (Op.) </h2>        
                <div class="table-responsive">
        <asp:GridView ID="TopDeteccionesAño" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
            EmptyDataText="No hay fichas para mostrar.">
            <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
             --%>
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>
                
                <asp:TemplateField HeaderText="Operario">
                    <ItemTemplate>
                        <asp:Label ID="lblOperarioAño" runat="server" Text='<%#Eval("IDE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:Label ID="lblNombreAño" runat="server" Text='<%#Eval("Operario") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Detecc.">
                    <ItemTemplate>
                        <asp:Label ID="lblFichaAño" runat="server" Text='<%#Eval("SUMA") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
   
    </div>
     </div>
     </div>

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

    </form>
</body>
</html>
