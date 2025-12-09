<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EditorListadoPrioridades.aspx.cs" Inherits="ThermoWeb.PLANIFICACION.EditorPrioridades"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Editor de prioridades</title>
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
                <li><a href="http://facts4-srv/thermogestion/DOCUMENTAL/FichaReferencia.aspx" >Fichas de referencia</a></li> 
          </ul>
             
        </div>
      </div>
    </nav>
    <div class="row" runat="server">
        <div class="col-lg-12">
            <div class="col-lg-6">

            </div>
            <div class="col-lg-2">
              <button id="Button2" runat="server" onserverclick="ActualizarOrdenesSecuencial"  type="button" class="btn btn-md btn-success" style="width:100%; text-align:left">
              <span class="glyphicon glyphicon-floppy-disk"></span>&nbsp Actualizar marcadas</button>
            </div>
            <div class="col-lg-2">
              <button id="Button3" runat="server" onserverclick="MandarMail"  type="button" class="btn btn-md btn-primary" style="width:100%; text-align:left">
              <span class="glyphicon glyphicon-envelope"></span>&nbsp Enviar prioridades</button>
            </div>
            <div class="col-lg-2">
              <button id="Button1" runat="server" onserverclick="BorrarPrioridades"  type="button" class="btn btn-md btn-danger" style="width:100%; text-align:left">
              <span class="glyphicon glyphicon-remove"></span>&nbsp Borrar prioridades</button>
            </div>
         </div>
        <h1>&nbsp;&nbsp;&nbsp; Editor de prioridades</h1>
        <div class="col-lg-12">
                    <div class="panel-body">
                        <div class="row">
                           
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarProdASC" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Producción</button>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarPriorASC" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Prioridad</button>
                                <button id="OrdenarPriorDESC" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-lg btn-basic" visible="false" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Prioridad</button>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarMaqASC" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Máquina</button>
                                <button id="OrdenarMaqDESC" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-lg btn-basic" visible="false" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Máquina</button>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarOrdenASC" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Orden</button>
                                <button id="OrdenarOrdenDESC" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-lg btn-basic" visible="false" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Orden</button>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarReferenciaASC" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Referencia</button>
                                <button id="OrdenarReferenciaDESC" runat="server" onserverclick="cargar_Ordenados" type="button" class="btn btn-lg btn-basic" visible="false" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Referencia</button>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label runat="server" Font-Bold="true" Text="Órdenes a mostrar:"></asp:Label>
                                 <asp:DropDownList ID="TipoAlerta" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True"> 
                                                    <asp:listitem text="Por defecto" runat="server" Value="3" ></asp:listitem>
                                                    <asp:listitem text="Todas" runat="server" Value="100"></asp:listitem>
                            </asp:DropDownList> 
                            </div>
                         </div>

                        </div>
                    
                </div>
        </div>
   
    <div class="table-responsive">
        <asp:GridView ID="dgv_AccionesAbiertas" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowDataBound="GridView_RowDataBound"  EmptyDataText="No hay datos para mostrar">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <%-- DataKeyNames="Id" ShowFooter="true"  "
            "OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing OnRowUpdating="GridView_RowUpdating" "
            OnRowCommand="gridView_RowCommand" OnRowDeleting="GridView_RowDeleting" OnRowCommand="gridView_RowCommand" --%>
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="¿Actualizar?" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true"/>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Máquina" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                   
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Prioridad"  ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:DropDownList ID="Selecprioridad" runat="server" CssClass="form-control" Font-Size="Medium"> 
                                <asp:listitem text=" " Value="100"></asp:listitem>
                                <asp:listitem text="1" Value="1"></asp:listitem>
                                <asp:listitem text="2" Value="2"></asp:listitem>
                                <asp:listitem text="3" Value="3"></asp:listitem>
                                <asp:listitem text="4" Value="4"></asp:listitem>
                                <asp:listitem text="5" Value="5"></asp:listitem> 
                                <asp:listitem text="6" Value="6"></asp:listitem> 
                                <asp:listitem text="7" Value="7"></asp:listitem> 
                                <asp:listitem text="8" Value="8"></asp:listitem> 
                                <asp:listitem text="9" Value="9"></asp:listitem> 
                                <asp:listitem text="10" Value="10"></asp:listitem> 
                                <asp:listitem text="11" Value="11"></asp:listitem> 
                                <asp:listitem text="12" Value="12"></asp:listitem> 
                                <asp:listitem text="13" Value="13"></asp:listitem> 
                                <asp:listitem text="14" Value="14"></asp:listitem> 
                                <asp:listitem text="15" Value="15"></asp:listitem> 
                                <asp:listitem text="16" Value="16"></asp:listitem> 
                                <asp:listitem text="17" Value="17"></asp:listitem> 
                                <asp:listitem text="18" Value="18"></asp:listitem> 
                                <asp:listitem text="19" Value="19"></asp:listitem> 
                                <asp:listitem text="20" Value="20"></asp:listitem> 
                                <asp:listitem text="21" Value="21"></asp:listitem> 
                                <asp:listitem text="22" Value="22"></asp:listitem> 
                                <asp:listitem text="23" Value="23"></asp:listitem> 
                                <asp:listitem text="24" Value="24"></asp:listitem> 
                                <asp:listitem text="25" Value="25"></asp:listitem> 
                                <asp:listitem text="26" Value="26"></asp:listitem> 
                                <asp:listitem text="27" Value="27"></asp:listitem> 
                                <asp:listitem text="28" Value="28"></asp:listitem> 
                                <asp:listitem text="29" Value="29"></asp:listitem> 
                                <asp:listitem text="30" Value="30"></asp:listitem> 
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrden" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Orden") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia y descripción" ItemStyle-Width="30%">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("REFERENCIA") %>' />
                     
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tiempo Restante" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblTiempo" runat="server" Text='<%#Eval("Tiempo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acción orden" ItemStyle-Width="30%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAccionOrden" runat="server" TextMode="MultiLine" Width="100%" Height="30px" Text='<%#Eval("RemarkOrden") %>'  />      
                    </ItemTemplate>
                </asp:TemplateField> 
            </Columns>
        </asp:GridView>
    </div>
    <div runat="server" visible="false" >
        <div class="table-responsive">
        <asp:GridView ID="GridView2" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <%-- DataKeyNames="Id" ShowFooter="true"  "
            "OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing OnRowUpdating="GridView_RowUpdating" "
            OnRowCommand="gridView_RowCommand" OnRowDeleting="GridView_RowDeleting" OnRowCommand="gridView_RowCommand" --%>
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina2" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Prioridad"  ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPrioridad2" runat="server" Font-Bold="true" Font-Size="X-Large" Text='<%#Eval("Prioridaddec") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrden2" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Orden") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblREFNUM" runat="server" Text='<%#Eval("REF") %>' />
                        <br />
                        <asp:Label ID="lblReferencia2" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tiempo Restante" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblTiempo2" runat="server" Text='<%#Eval("Tiempo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="Acción orden" ItemStyle-Width="30%">
                    <ItemTemplate>
                        <asp:Label ID="lblAccionOrden2" runat="server" Text='<%#Eval("RemarkOrden") %>' />                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Otras acciones" ItemStyle-Width="35%">
                    <ItemTemplate>
                        <strong>Producto:</strong><asp:Label ID="lblAccionProducto2" runat="server" Text='<%#Eval("RemarkProducto") %>' />
                        <br /><strong>Mantenimiento:</strong><asp:Label ID="lblAccionMolde2" runat="server" Text='<%#Eval("RemarkMolde") %>' />
                        <asp:Label ID="lblAccionMaquina2" runat="server" Text='<%#Eval("RemarkMaquina") %>' />
                        <br /><strong>Notas:</strong><asp:Label ID="lblAccionReceta2" runat="server" Text='<%#Eval("RemarksReceta") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </div>
    </form>
</body>
</html>
