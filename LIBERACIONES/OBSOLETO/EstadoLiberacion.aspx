<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EstadoLiberacion.aspx.cs" Inherits="ThermoWeb.LIBERACIONES.EstadoLiberacion"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Estado de liberación</title>
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
             <li><a href="HistoricoLiberacion.aspx">Consultar histórico</a></li> 
          </ul>
              
        </div>
      </div>
    </nav>
    <h1>&nbsp;&nbsp;&nbsp; Listado de liberaciones</h1>
             <ul class="nav nav-pills nav-justified">
                <li class="active"><a data-toggle="pill" href="#GENERAL">Vista general</a></li>
                <li><a data-toggle="pill" href="#NAVE3">Nave 3</a></li>
                <li><a data-toggle="pill" href="#NAVE4">Nave 4</a></li>
                <li><a data-toggle="pill" href="#NAVE5">Nave 5</a></li>
                <li><a data-toggle="pill" href="#DESV">Desviaciones</a></li>
         </ul>

         <div class="tab-content"> 
            <div id="GENERAL" class="tab-pane fade in active">
            
                <br>           
               <div class="table-responsive">
        <asp:GridView ID="dgv_Liberaciones" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridView0_DataBound"
             EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoActual" runat="server" Text='<%#Eval("EstadoMaquina") %>' /><br />
                         <asp:Label ID="lblAccionLiberado" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("AccionLiberado") %>' />             
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("Orden") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server"  Text='<%#Eval("Cliente") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Cambiador" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoCambio" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCAMBIO") %>' /><br />
                        <asp:Label ID="lblfechaCambiador" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHACAMBIADORLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Producción" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoProduccion" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARPRODUCCION") %>' /><br />
                        <asp:Label ID="lblfechaproduccion" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHAPRODUCCIONLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Calidad" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoCalidad" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCALIDAD") %>' /><br />
                        <asp:Label ID="lblfechaCalidad"  Font-Size="Smaller" runat="server" Text='<%#Eval("FECHACALIDADLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notas" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoNotas" runat="server" Text='<%#Eval("NotasLiberado") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...--%>
                        <asp:Button ID="btnVer" runat="server" Text="Ver liberación" CssClass="btn btn-info"  CommandArgument='<%#Eval("Orden")%>' CommandName="Redirect"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                    </div>
             </div>
             
            <%-- /NAVE 3--%>
            <div id="NAVE3" class="tab-pane fade">
                <br>           
                 <div class="table-responsive">
        <asp:GridView ID="dgv_Liberaciones3" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridView1_DataBound"
             EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Estado" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoActual" runat="server" Text='<%#Eval("EstadoMaquina") %>' /><br />
                          <asp:Label ID="lblAccionLiberado" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("AccionLiberado") %>' />   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("Orden") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server"  Text='<%#Eval("Cliente") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Cambiador" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoCambio" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCAMBIO") %>' /><br />
                        <asp:Label ID="lblfechaCambiador" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHACAMBIADORLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Producción" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoProduccion" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARPRODUCCION") %>' /><br />
                        <asp:Label ID="lblfechaproduccion" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHAPRODUCCIONLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Calidad" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoCalidad" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCALIDAD") %>' /><br />
                        <asp:Label ID="lblfechaCalidad"  Font-Size="Smaller" runat="server" Text='<%#Eval("FECHACALIDADLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notas" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoNotas" runat="server" Text='<%#Eval("NotasLiberado") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...--%>
                        <asp:Button ID="btnVer" runat="server" Text="Ver liberación" CssClass="btn btn-info"  CommandArgument='<%#Eval("Orden")%>' CommandName="Redirect"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                    </div>
            </div>
            <%-- /NAVE 4--%>
            <div id="NAVE4" class="tab-pane fade">
            <br>           
                 <div class="table-responsive">
        <asp:GridView ID="dgv_Liberaciones4" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridView2_DataBound"
             EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoActual" runat="server" Text='<%#Eval("EstadoMaquina") %>' /><br />
                          <asp:Label ID="lblAccionLiberado" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("AccionLiberado") %>' />    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("Orden") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server"  Text='<%#Eval("Cliente") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cambiador" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoCambio" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCAMBIO") %>' /><br />
                        <asp:Label ID="lblfechaCambiador" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHACAMBIADORLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Producción" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoProduccion" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARPRODUCCION") %>' /><br />
                        <asp:Label ID="lblfechaproduccion" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHAPRODUCCIONLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Calidad" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoCalidad" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCALIDAD") %>' /><br />
                        <asp:Label ID="lblfechaCalidad"  Font-Size="Smaller" runat="server" Text='<%#Eval("FECHACALIDADLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notas" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoNotas" runat="server" Text='<%#Eval("NotasLiberado") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...--%>
                        <asp:Button ID="btnVer" runat="server" Text="Ver liberación" CssClass="btn btn-info"  CommandArgument='<%#Eval("Orden")%>' CommandName="Redirect"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
            </div>
            <%-- /NAVE 5--%>
            <div id="NAVE5" class="tab-pane fade">
            <br>           
                <div class="table-responsive">
        <asp:GridView ID="dgv_Liberaciones5" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridView3_DataBound"
             EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoActual" runat="server" Text='<%#Eval("EstadoMaquina") %>' /><br />
                          <asp:Label ID="lblAccionLiberado" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("AccionLiberado") %>' />  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("Orden") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblCliente" runat="server"  Text='<%#Eval("Cliente") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Cambiador" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoCambio" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCAMBIO") %>' /><br />
                        <asp:Label ID="lblfechaCambiador" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHACAMBIADORLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Producción" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoProduccion" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARPRODUCCION") %>' /><br />
                        <asp:Label ID="lblfechaproduccion" runat="server" Font-Size="Smaller" Text='<%#Eval("FECHAPRODUCCIONLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Calidad" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoCalidad" Font-Bold="true" runat="server" Text='<%#Eval("LIBERARCALIDAD") %>' /><br />
                        <asp:Label ID="lblfechaCalidad"  Font-Size="Smaller" runat="server" Text='<%#Eval("FECHACALIDADLIBERADO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Notas" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblEstadoNotas" runat="server" Text='<%#Eval("NotasLiberado") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...--%>
                        <asp:Button ID="btnVer" runat="server" Text="Ver liberación" CssClass="btn btn-info"  CommandArgument='<%#Eval("Orden")%>' CommandName="Redirect"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
            </div>
            <%-- /DESVIACIONES--%>
            <div id="DESV" class="tab-pane fade" runat="server" >
            <br>           
                <div class="table-responsive">
        <asp:GridView ID="GridDesviaciones" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="GridViewDESV_DataBound"
             EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
              
                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("Maquina") %>' />
                        <asp:Label ID="lblEstadoActual" Visible="false" runat="server" Text='<%#Eval("EstadoMaquina") %>' />

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Responsables" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPersonalEncargado" runat="server" Font-Size="Smaller" Text='<%#Eval("Encargado") %>' /><br />
                        <asp:Label ID="lblPersonalCalidad" runat="server" Font-Size="Smaller" Text='<%#Eval("Calidad") %>' /><br />
                        <asp:Label ID="lblPersonalCambiador" runat="server" Font-Size="Smaller" Text='<%#Eval("Cambiador") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nv. Op." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPersonalOperario" Font-Bold="true" runat="server" Text='<%#Eval("Operario1Nivel") %>' />
                        <asp:Label ID="lblOperarioNivel" runat="server" Font-Size="Smaller" Text='<%#Eval("Operario1Notas") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Materiales y cambio de molde" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        
                        <asp:Label ID="Desv1Prod" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q1E") %>' />
                        <asp:Label ID="Desv1Cal" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q1C") %>' />
                        <asp:Label ID="Q1PROD" runat="server" Text='<%#"<strong>1.Camb: </strong>" + Eval("Q1ENC") %>' />
                        <asp:Label ID="Desv2Prod" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q2E") %>' />
                        <asp:Label ID="Desv2Cal" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q2C") %>' />
                        <asp:Label ID="Q2PROD" runat="server" Text='<%#"<br /><strong>2.Camb: </strong>" + Eval("Q2ENC") %>' />
                        <asp:Label ID="Desv3Prod" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q3E") %>' />
                        <asp:Label ID="Desv3Cal" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q3C") %>' />
                        <asp:Label ID="Q3PROD" runat="server" Text='<%#"<br /><strong>3.Camb: </strong>" + Eval("Q3ENC") %>' />
                        <asp:Label ID="ResultadoLOT" visible="false" runat="server" Text='<%#Eval("ResultadoLOTES") %>' />
                        <asp:Label ID="ResultadoLOTTEXT" visible="false" runat="server" Text='<%#"<br /><strong>Mat: </strong>" + Eval("ResultadosLOTESTEXT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lotes" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" Visible="false">
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Máquina y parámetros" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="lblAccionLiberado" runat="server" Font-Bold="true" Visible="false" Text='<%#Eval("AccionLiberado") %>' />  
                        <asp:Label ID="Desv4Prod" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q4E") %>' />
                        <asp:Label ID="Q4PROD" runat="server" Text='<%#"<strong>4.Prod: </strong>" + Eval("Q4ENC") %>' />
                        <asp:Label ID="Desv5Prod" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q5E") %>' />
                        <asp:Label ID="Q5PROD" runat="server" Text='<%#"<br /><strong>5.Prod: </strong>" + Eval("Q5ENC") %>' />
                        <asp:Label ID="Desv6Prod" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q6E") %>' />
                        <asp:Label ID="Desv6Cal" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q6C") %>' />
                        <asp:Label ID="Q6PROD" runat="server" Text='<%#"<br /><strong>6.Prod: </strong>" + Eval("Q6ENC") %>' />
                        <asp:Label ID="Q6CAL" runat="server" Text='<%#"<br /><strong>6.Cal: </strong>" + Eval("Q6CAL") %>' />
                        <asp:Label ID="ResultadoPARAM" visible="false" runat="server" Text='<%#Eval("ResultadoPARAM") %>' />
                        <asp:Label ID="ResultadoPARAMTEXT" visible="false" runat="server" Text='<%#"<br /><strong>Prod: </strong>" + Eval("ResultadoPARAMTEXT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>      
                <asp:TemplateField HeaderText="Proceso y atributos" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Label ID="Desv7Prod" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q7E") %>' />
                        <asp:Label ID="Desv7Cal" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q7C") %>' />
                        <asp:Label ID="Q7PROD" runat="server" Text='<%#"<strong>7.Prod: </strong>" + Eval("Q7ENC") %>' />
                        <asp:Label ID="Q7CAL" runat="server" Text='<%#"<br /><strong>7.Cal: </strong>" + Eval("Q7CAL") %>' />
                        <asp:Label ID="Desv8Prod" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q8E") %>' />
                        <asp:Label ID="Desv8Cal" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q8C") %>' />
                        <asp:Label ID="Q8PROD" runat="server" Text='<%#"<br /><strong>8.Prod: </strong>" + Eval("Q8ENC") %>' />
                        <asp:Label ID="Q8CAL" runat="server" Text='<%#"<br /><strong>8.Cal: </strong>" + Eval("Q8CAL") %>' />
                        <asp:Label ID="Desv9Prod" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q9E") %>' />
                        <asp:Label ID="Desv9Cal" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q9C") %>' />
                        <asp:Label ID="Q9PROD" runat="server" Text='<%#"<br /><strong>9.Prod: </strong>" + Eval("Q9ENC") %>' />
                        <asp:Label ID="Q9CAL" runat="server" Text='<%#"<br /><strong>9.Cal: </strong>" + Eval("Q9CAL") %>' />                       
                        <asp:Label ID="Desv10Cal" Font-Bold="true" runat="server" visible="false" Text='<%#Eval("Q10C") %>' />
                        <asp:Label ID="Q10CAL" runat="server" Text='<%#"<br /><strong>10.Cal: </strong>" + Eval("Q10CAL") %>' />
                        <asp:Label ID="LiberadoPROD" visible="false"  Font-Bold="true" runat="server" Text='<%#Eval("ProduccionLiberado") %>' />
                        <asp:Label ID="DesvNCPROD" visible="false" Font-Bold="true" runat="server" Text='<%#Eval("ENCNoconformidad") %>' />
                        <asp:Label ID="DesvNCPRODText" visible="false" runat="server" Text='<%#"<br /><strong>Prod.: </strong>" + Eval("ENCNoconformidadTEXT") %>' />
                        <asp:Label ID="DesvGP12PROD" visible="false" Font-Bold="true" runat="server" Text='<%#Eval("ENCDefectos") %>' />
                        <asp:Label ID="DesvGP12PRODText" Visible="false" runat="server" Text='<%#"<br /><strong>Prod: </strong>" + Eval("ENCDefectosTEXT") %>' />
                        <asp:Label ID="LiberadoCal" visible="false" Font-Bold="true" runat="server" Text='<%#Eval("CalidadLiberado") %>' />
                        <asp:Label ID="DesvNCCAL" visible="false" Font-Bold="true" runat="server" Text='<%#Eval("CALNoconformidad") %>' />
                        <asp:Label ID="DesvNCCALText" visible="false" runat="server" Text='<%#"<br /><strong>Cal.: </strong>" + Eval("CALNoconformidadTEXT") %>' />
                        <asp:Label ID="DesvGP12CAL" visible="false" Font-Bold="true" runat="server" Text='<%#Eval("CALDefectos") %>' />
                        <asp:Label ID="DesvGP12CALText" visible="false" runat="server" Text='<%#"<br /><strong>Cal.: </strong>" + Eval("CALDefectosTEXT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Parámetros" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" Visible="false">
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notas" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                       <asp:Label ID="QXCAMB" runat="server" Text='<%#"<strong>Camb: </strong>" + Eval("QXFeedbackCambiador") %>' /><br />
                       <asp:Label ID="QXPROD" runat="server" Text='<%#"<strong>Prod: </strong>" + Eval("QXFeedbackProduccion") %>' /><br />
                       <asp:Label ID="QXCAL" runat="server" Text='<%#"<strong>Cal: </strong>" + Eval("QXFeedbackCalidad") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...--%>
                        <asp:Button ID="btnVer" runat="server" Text="+" CssClass="btn btn-sm btn-info"  CommandArgument='<%#Eval("Orden")%>' CommandName="Redirect"/>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
    </div>
            </div>
          </div>
    </form>
</body>
</html>
