<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ListadoPrioridades.aspx.cs" Inherits="ThermoWeb.PLANIFICACION.ListadoPrioridades"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prioridades y acciones abiertas</title>
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
    <h1>&nbsp;&nbsp;&nbsp; Órdenes produciendo con acciones abiertas</h1>
    <div class="row" runat="server" visible="false">
            <div class="col-lg-12">
                
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                            <br />
                            <button id="VerTodas" runat="server" onserverclick="Cargar_todas" type="button" class="btn btn-lg btn-primary" style="width:85%; text-align:left">
                            <span class="glyphicon glyphicon-list"></span> Ver todas</button>
                            <button id="VerRevision" runat="server" onserverclick="Cargar_EnRevision" type="button" class="btn btn-lg btn-primary" visible="false" style="width:85%; text-align:left">
                            <span class="glyphicon glyphicon-indent-right"></span> Ver en revisión</button>
                            </div>
                            <div class="col-lg-1">
                                <label for="usr">Referencia:</label>
                                        <asp:TextBox ID="selectReferencia" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />
                                                            
                            </div>                                                                                            
                            <div class="col-lg-1">                         
                                <label for="usr">Molde:</label>
                                       <asp:TextBox ID="selectMolde" runat="server" CssClass="textbox" Width="100%" Height="35px" autocomplete="off" />                               
                            </div>
                            <div class="col-lg-2">                                
                                    <label for="usr">Estado:</label>
                                        <asp:DropDownList ID="lista_estado" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value=""> - </asp:ListItem>
                                    </asp:DropDownList>                                                             
                            </div>
                            <div class="col-lg-2">                               
                                <label for="usr">Cliente:</label>
                                        <asp:DropDownList ID="lista_clientes" runat="server" CssClass="form-control">     
                                        
                                    </asp:DropDownList>                           
                            </div>
                            <div class="col-lg-2">                         
                                <label for="usr">Responsable:</label>
                                        <asp:DropDownList ID="lista_responsable" runat="server" CssClass="form-control">
                                    </asp:DropDownList>                                
                                </div>
                            <div class="col-lg-2 text-right">
                            <br />
                                 <button id="Button2" runat="server" onserverclick="Cargar_Filtrados" type="button" class="btn btn-lg btn-info" style="width:75%; text-align:left">
                                 <span class="glyphicon glyphicon-search"></span> Filtrar</button>
                            </div>
                         </div>

                        </div>
                    
                </div>
            </div>
    <div class="row" runat="server">
            <div class="col-lg-12">
                
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarProdASC" runat="server" onserverclick="Cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Producción</button>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarPriorASC" runat="server" onserverclick="Cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Prioridad</button>
                                <button id="OrdenarPriorDESC" runat="server" onserverclick="Cargar_Ordenados" type="button" class="btn btn-lg btn-basic" visible="false" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Prioridad</button>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarMaqASC" runat="server" onserverclick="Cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Máquina</button>
                                <button id="OrdenarMaqDESC" runat="server" onserverclick="Cargar_Ordenados" type="button" class="btn btn-lg btn-basic" visible="false" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Máquina</button>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarOrdenASC" runat="server" onserverclick="Cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Orden</button>
                                <button id="OrdenarOrdenDESC" runat="server" onserverclick="Cargar_Ordenados" type="button" class="btn btn-lg btn-basic" visible="false" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Orden</button>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <button id="OrdenarReferenciaASC" runat="server" onserverclick="Cargar_Ordenados" type="button" class="btn btn-lg btn-basic" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Referencia</button>
                                <button id="OrdenarReferenciaDESC" runat="server" onserverclick="Cargar_Ordenados" type="button" class="btn btn-lg btn-basic" visible="false" style="width:100%; text-align:left">
                                <span class="glyphicon glyphicon-sort"></span> Referencia</button>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label runat="server" Font-Bold="true" Text="Órdenes a mostrar:"></asp:Label>
                                 <asp:DropDownList ID="TipoAlerta" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True"> 
                                                    <asp:listitem text="Por defecto" Value="3"></asp:listitem>
                                                    <asp:listitem text="Todas" Value="100"></asp:listitem>
                            </asp:DropDownList> 
                            </div>

                         </div>

                        </div>
                    
                </div>
            </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="dgv_AccionesAbiertas" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnRowCommand="ContactsGridView_RowCommand" 
            OnRowDataBound="GridView_RowDataBound"  EmptyDataText="There are no data records to display.">
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <%-- DataKeyNames="Id" ShowFooter="true"  "
            "OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing OnRowUpdating="GridView_RowUpdating" "
            OnRowCommand="gridView_RowCommand" OnRowDeleting="GridView_RowDeleting" OnRowCommand="gridView_RowCommand" --%>
            <EditRowStyle BackColor="#ffffcc" />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10px">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...--%>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-info" CommandName="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--Botones de grabar y cancelar la edición de registro...--%>
                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Guardar" CssClass="btn btn-success" Width="100%"
                            CommandName="Update"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-danger" Width="100%"
                            CommandName="Cancel"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Máq." ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="lblMaquina" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Maquina") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtMaquina" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Maquina") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Prioridad"  ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="lblPrioridad" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Prioridaddec") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
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
                         
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orden" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="lblOrden" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Orden") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtOrden" runat="server" Font-Bold="true" Font-Size="Larger" Text='<%#Eval("Orden") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="125px">
                    <ItemTemplate>
                        <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("REFERENCIA") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtReferencia" runat="server" Text='<%#Eval("REFERENCIA") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="REF" ItemStyle-Width="125px" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenciaREF" runat="server" Text='<%#Eval("REF") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtReferenciaREF" runat="server" Text='<%#Eval("REF") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tiempo Restante" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblTiempo" runat="server" Text='<%#Eval("Tiempo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="Acción orden" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblAccionOrden" runat="server" Text='<%#Eval("RemarkOrden") %>' />
                                                
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAccionOrden" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Text='<%#Eval("RemarkOrden") %>'  />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acción producto" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblAccionProducto" runat="server" Text='<%#Eval("RemarkProducto") %>' />
                        <br /><strong>Notas:</strong>
                        <asp:Label ID="lblAccionReceta" runat="server" Text='<%#Eval("RemarksReceta") %>' />

                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAccionProducto" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Text='<%#Eval("RemarkProducto") %>'  />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ParteMolde" ItemStyle-Width="100px"  Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblParteMolde" runat="server" Text='<%#Eval("ParteMolde") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtParteMolde" runat="server" Text='<%#Eval("ParteMolde") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acción molde" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblAccionMolde" runat="server" Text='<%#Eval("RemarkMolde") %>' />
                    </ItemTemplate>
                    <EditItemTemplate> 
                        
                        <asp:Button ID = "btnParteMoldeVALIDAR" runat = "server" CommandName="OKMOL" class="btn btn-success btn-sm" Width="32%" CommandArgument='<%#Eval("ParteMolde")%>' Text="OK" />
                        <asp:Button ID = "btnParteMoldeVALIDARNOK" runat = "server" CommandName="NOKMOL" class="btn btn-danger btn-sm" Width="32%" CommandArgument='<%#Eval("ParteMolde")%>' Text="NOK" />
                        <asp:Button ID = "btnParteMolde" runat = "server" CommandName="RedirectMOL" class="btn btn-default btn-sm" Width="32%" CommandArgument='<%#Eval("ParteMolde")%>' Text="Ver" />                    
                        <asp:Textbox ID="ObservacioneParteMolde" TextMode="MultiLine" runat="server" visible="false" Width="100%" />
                        <br />
                        <asp:Label runat="server" Text="Comentarios validación:" Font-Bold="true" Visible="false" />
                        <asp:TextBox ID="ComentParteMolde" runat="server" TextMode="MultiLine" Width="100%"  Visible="false"/>
                        <asp:DropDownList ID="SelecResponsable" runat="server" CssClass="form-control" Font-Size="Medium"  Visible="false"></asp:DropDownList>
                        <asp:Label runat="server" Text="Parte:" Font-Bold="true" />
                        <asp:Label ID="lblAccionMoldeEDIT" runat="server" Text='<%#Eval("RemarkMolde") %>' />
                       
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ParteMaquina" ItemStyle-Width="100px"  Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblParteMaquina" runat="server" Text='<%#Eval("ParteMaquina") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtParteMaquina" runat="server" Text='<%#Eval("ParteMaquina") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acción máquina" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblAccionMaquina" runat="server" Text='<%#Eval("RemarkMaquina") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID = "btnParteeMaquinaVALIDAR" runat = "server" CommandName="OKMAQ" class="btn btn-success btn-sm" Width="32%" CommandArgument='<%#Eval("ParteMaquina")%>' Text="OK" />
                        <asp:Button ID = "btnParteeMaquinaVALIDARNOK" runat = "server" CommandName="NOKMAQ" class="btn btn-danger btn-sm" Width="32%" CommandArgument='<%#Eval("ParteMaquina")%>' Text="NOK" />
                        <asp:Button ID = "btnParteMaquina" runat = "server" CommandName="RedirectMAQ" class="btn btn-default btn-sm" Width="32%"  CommandArgument='<%#Eval("ParteMaquina")%>' Text="Ver" /><br />
                         <asp:Textbox ID="ObservacioneParteMaquina" runat="server" TextMode="MultiLine" Width="100%" visible="false"/>
                        
                        <br /><br />
                        <asp:Label ID="lblAccionMaquinaEDIT" runat="server" Text='<%#Eval("RemarkMaquina") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="row">
        <div class="col-lg-12" runat="server" visible="false">
            <div class="col-lg-2">
              <button id="Button3" runat="server" onserverclick="MandarMail"  type="button" class="btn btn-md btn-primary" style="width:100%; text-align:left">
              <span class="glyphicon glyphicon-envelope"></span> Enviar prioridades</button>
            </div>
            <div class="col-lg-2">
              <button id="Button1" runat="server" onserverclick="BorrarPrioridades"  type="button" class="btn btn-md btn-danger" style="width:100%; text-align:left">
              <span class="glyphicon glyphicon-remove"></span> Borrar prioridades</button>
            </div>
         </div>
    </div>
    </form>
</body>
</html>
