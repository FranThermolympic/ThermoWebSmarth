<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ListaAlertasCalidadOLD.aspx.cs" Inherits="ThermoWeb.CALIDAD.ListaAlertasCalidadOLD"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Listado de No Conformidades</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
      <script src="js/json2.js" type="text/javascript"></script>
      <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
      <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <script type="text/javascript">
            function openModal() {
                $('#myModal').modal('show');
            }
      </script> 
    <script type="text/javascript">
    $(document).ready(function () {
        $('.Add-text').datepicker({
            dateFormat: 'dd/mm/yy',
            inline: true,
            showOtherMonths: true,
            changeMonth: true,
            changeYear: true,
            constrainInput: true,
            firstDay: 1,
            navigationAsDateFormat: true,    
            showAnim: "fold",
            yearRange: "c-20:c+10",
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa']
        });
    });
    </script>

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
                <li><a href="Alertas_Calidad.aspx" target="_blank">Nueva alerta</a></li>
                <li><a href="ListaAlertasCalidad.aspx" target="_blank">Lista de alertas</a></li>
                <li><a href="DashboardAlertasCalidad.aspx" target="_blank">Dashboard</a></li>
                <li><a href="../KPI/KPI_NoConformidades.aspx" target="_blank">Indicadores</a></li>
          </ul>
          
        </div>
      </div>
    </nav>
        <div class="col-lg-10">
            <h1>&nbsp;&nbsp;&nbsp; Gestión de No conformidades</h1>
        </div>
        <div class="col-lg-2">
            <label for="usr">Periodo de revisión:</label>
                <asp:DropDownList ID="selecaño" runat="server" CssClass="form-control" Font-Size="Large"> 
                      <asp:listitem text="2022" Value="2022"></asp:listitem>
                      <asp:listitem text="2021" Value="2021"></asp:listitem>

                 </asp:DropDownList>
        </div>
         <div class="row">
            <div class="col-lg-12">
                
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-2">
                            <br />
                            <button id="VerTodas" runat="server" type="button" onserverclick="CargarTodas" class="btn btn-lg btn-primary" style="width:85%; text-align:left">
                            <span class="glyphicon glyphicon-list"></span> Ver todas</button>
                            
                            </div>
                       
                            <div class="col-lg-1">                                
                                    <label for="usr">Estado:</label>
                                        <asp:DropDownList ID="lista_estado" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True">
                                        <asp:listitem text="-" Value="0"></asp:listitem>
                                         <asp:listitem text="Cerrada" Value="1"></asp:listitem>
                                         <asp:listitem text="En curso" Value="2"></asp:listitem>
                                         <asp:listitem text="Vencida" Value="3"></asp:listitem>
                                    </asp:DropDownList>                                                             
                            </div>
                            <div class="col-lg-1">
                                <label for="usr">Tipo:</label>
                                    <asp:DropDownList ID="TipoAlerta" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True"> 
                                       <asp:listitem text="-" Value="0"></asp:listitem>
                                       <asp:listitem text="A proveedor" Value="1"></asp:listitem>
                                       <asp:listitem text="De cliente" Value="2"></asp:listitem>
                                       <asp:listitem text="Interna" Value="3"></asp:listitem>
                                    </asp:DropDownList>                                                           
                            </div>                                                                                            
                            <div class="col-lg-2">                         
                                <label for="usr">Escalado:</label>
                                      <asp:DropDownList ID="NivelAlerta" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True"> 
                                         <asp:listitem text="-" Value="0"></asp:listitem>
                                         <asp:listitem text="Q-Info" Value="1"></asp:listitem>
                                         <asp:listitem text="Reclamación oficial" Value="2"></asp:listitem>
                                         <asp:listitem text="No aceptada" Value="3"></asp:listitem>
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
                                 <button id="Button2" runat="server"  type="button" class="btn btn-lg btn-info" onserverclick="CargarFiltrados" style="width:75%; text-align:left">
                                 <span class="glyphicon glyphicon-search"></span> Filtrar</button>
                            </div>
                         </div>

                        </div>
                    
                </div>
            </div>
    <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog modal-lg">
                  <!-- Modal content-->
                  <div class="modal-content">
                    <div class="modal-header" style="background-color:Blue; color:White; text-align:center; font-size:x-large">
                     <asp:TextBox ID="IDNoConformidad" runat="server" Font-Bold="true" Font-Size="XX-Large" ForeColor="White" Style="text-align: center; background-color: transparent; border-color:transparent" Width="100%" Enabled="false">NCX</asp:TextBox>
                     <asp:TextBox ID="IDNoConformidadSM" runat="server" Visible="false" Font-Bold="true" Font-Size="XX-Large" ForeColor="White" Style="text-align: center; background-color: transparent; border-color:transparent" Width="100%" Enabled="false"></asp:TextBox>
                    
                    </div>
                    <div class="modal-body" style="padding:40px 50px;">
                      
                      <div class="form-group"><asp:CheckBox ID="CorteCheckboxEvidencias" runat="server" Checked="true" Enabled="false" />&nbsp
                      <label for="puntodecorte"><span class="glyphicon glyphicon-exclamation-sign"></span>   Punto de corte:</label><br />
                          <div class="col-lg-12">
                            <div class="col-lg-2">
                              <div class="btn-group">
                                  <button id="BTNVERInfoCorte" runat="server" onserverclick="redireccionadocumento" type="button" class="btn btn-info"><span class="glyphicon glyphicon-eye-open"></span></button>
                                  <button id="BTNCARGAInfoCorte" runat="server" type="button" class="btn btn-primary" onserverclick="insertar_documento"><span class="glyphicon glyphicon-upload"></span></button>
                              </div>
                            </div>
                            <div class="col-lg-4"><asp:TextBox ID="EvidenciaCorte" Width="100%" runat="server" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="D3FECHASUBIDA" Width="100%" runat="server" Enabled="false" Font-Bold="true" BorderColor="Transparent" BackColor="Transparent" Style="text-align: right" ></asp:TextBox>
                            </div>
                            <div class="col-lg-5"><asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload></div>
                          </div>
                     </div>
                     <br /> <br />
                     <div class="form-group"><asp:CheckBox ID="D3CheckboxEvidencias" runat="server" Checked="true" Enabled="false" />&nbsp
                      <label for="d3"><span class="glyphicon glyphicon-folder-open"></span>   D3:</label><br />
                          <div class="col-lg-12">
                            <div class="col-lg-2">
                              <div class="btn-group">
                                  <button id="BTNVERD3" runat="server" onserverclick="redireccionadocumento" type="button" class="btn btn-info"><span class="glyphicon glyphicon-eye-open"></span></button>
                                  <button id="BTNCARGAD3" runat="server" type="button" class="btn btn-primary"  onserverclick="insertar_documento"><span class="glyphicon glyphicon-upload"></span></button>
                              </div>
                            </div>
                            <div class="col-lg-4"><asp:TextBox ID="tbD3Text" Width="100%" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-lg-5"><asp:FileUpload ID="FileUpload2" runat="server"></asp:FileUpload></div>
                          </div>
                     </div>
                     <br /><br />
                     <div class="form-group"><asp:CheckBox ID="D6CheckboxEvidencias" runat="server" />&nbsp
                      <label for="d6"><span class="glyphicon glyphicon-folder-open"></span>   D6:</label><br />
                          <div class="col-lg-12">
                            <div class="col-lg-2">
                              <div class="btn-group">
                                  <button id="BTNVERD6" runat="server"  onserverclick="redireccionadocumento" type="button" class="btn btn-info"><span class="glyphicon glyphicon-eye-open"></span></button>
                                  <button id="BTNCARGAD6" runat="server" type="button" class="btn btn-primary"  onserverclick="insertar_documento"><span class="glyphicon glyphicon-upload"></span></button>
                              </div>
                            </div>
                            <div class="col-lg-4"><asp:TextBox ID="tbD6Text" Width="100%" runat="server" Enabled="false"></asp:TextBox>
                            <asp:TextBox ID="D6FECHASUBIDA" Width="100%" runat="server" Enabled="false" Font-Bold="true" BorderColor="Transparent" BackColor="Transparent" Style="text-align: right" ></asp:TextBox>
                            </div>
                            <div class="col-lg-5"><asp:FileUpload ID="FileUpload3" runat="server"></asp:FileUpload></div>
                          </div>
                     </div>
                     <br /><br />
                     <div class="form-group"><asp:CheckBox ID="D8CheckboxEvidencias" runat="server" />&nbsp
                      <label for="d8"><span class="glyphicon glyphicon-folder-open"></span>   D8:</label><br />
                          <div class="col-lg-12">
                            <div class="col-lg-2">
                              <div class="btn-group">
                                  <button id="BTNVERD8" runat="server"  onserverclick="redireccionadocumento" type="button" class="btn btn-info"><span class="glyphicon glyphicon-eye-open"></span></button>
                                  <button id="BTNCARGAD8" runat="server" type="button" class="btn btn-primary" onserverclick="insertar_documento"><span class="glyphicon glyphicon-upload"></span></button>
                              </div>
                            </div>
                            <div class="col-lg-4"><asp:TextBox ID="tbD8Text" Width="100%" runat="server" Enabled="false"></asp:TextBox><br />
                            <asp:TextBox ID="D8FECHASUBIDA" Width="100%" runat="server" Enabled="false" Font-Bold="true" BorderColor="Transparent" BackColor="Transparent" Style="text-align: right" ></asp:TextBox>
                            </div>
                            <div class="col-lg-5"><asp:FileUpload ID="FileUpload4" runat="server"></asp:FileUpload></div>
                          </div>
                     </div>
                        <br />
                        <br />
                        <div class="form-group"><asp:CheckBox ID="CargosCheckboxEvidencias" runat="server" Checked="true" Enabled="false" />&nbsp
                      <label for="d8"><span class="glyphicon glyphicon-folder-open"></span>   Cargos y evidencias:</label><br />
                          <div class="col-lg-12">
                            <div class="col-lg-2">
                              <div class="btn-group">
                                  <button id="BTNVERCARGOS" runat="server"  onserverclick="redireccionadocumento" type="button" class="btn btn-info"><span class="glyphicon glyphicon-eye-open"></span></button>
                                  <button id="BTNCARGACARGOS" runat="server" type="button" class="btn btn-primary" onserverclick="insertar_documento"><span class="glyphicon glyphicon-upload"></span></button>
                              </div>
                            </div>
                            <div class="col-lg-4"><asp:TextBox ID="tbEvidenciaCargosText" Width="100%" runat="server" Enabled="false"></asp:TextBox></div>
                            <div class="col-lg-5"><asp:FileUpload ID="FileUpload5" runat="server"></asp:FileUpload></div>
                          </div>
                     </div>
                     <br /><br />
                     <div class="form-group">
                      <label for="CostesExternos"><span class="glyphicon glyphicon-euro"></span> Costes externos:</label><br />
                          <div class="col-lg-3"><label>Selección:</label><br /><asp:TextBox ID="tbEXTSeleccion" Width="100%" runat="server"></asp:TextBox></div>
                          <div class="col-lg-3"><label>Piezas NOK:</label><br /><asp:TextBox ID="tbEXTScrap" Width="100%" runat="server"></asp:TextBox></div>
                          <div class="col-lg-3"><label>Cargos:</label><br /><asp:TextBox ID="tbEXTCargos" Width="100%" runat="server"></asp:TextBox></div>
                          <div class="col-lg-3"><label>Administrativos:</label><br /><asp:TextBox ID="tbEXTAdmon" Width="100%" runat="server"></asp:TextBox></div>
                     </div>
                     <br /><br /><br />
                     <div class="form-group">
                      <label for="CostesSeleccion"><span class="glyphicon glyphicon-euro"></span> Costes internos:</label><br />
                          <div class="col-lg-3"><label>Selección:</label><br /><asp:TextBox ID="tbINTSeleccion" Width="100%" runat="server"></asp:TextBox></div>
                          <div class="col-lg-3"><label>Otros:</label><br /><asp:TextBox ID="tbINTOtros" Width="100%" runat="server"></asp:TextBox></div>
                          <div class="col-lg-3"><label></label><br /><asp:TextBox ID="TBAUX" Width="100%" runat="server" Visible="false"></asp:TextBox></div>     
                     </div>
                     <br /><br /><br />
                    <div class="form-group">
                        <label for="FPOCS"><span class="glyphicon glyphicon-folder-open"></span> Formatos de proceso:</label><br />
                         <a href="../FPOCS/FPOC-09-11 v2 S Hoja de Contención.xlsx" class="mt-4" style="font: bold; font-size: large; color: black"> &nbsp<i class="glyphicon glyphicon-open-file"></i><i>&nbsp FPOC-09-11: Hoja de contención</i></a><br />
                         <a href="../FPOCS/FPOC-12-01 FIT 18.01 Informe 8D-A3 V2.xlsm" class="mt-4" style="font: bold; font-size: large; color: black"> &nbsp<i class="glyphicon glyphicon-open-file"></i><i>&nbsp FPOC-12-01: Formato 8D/A3</i></a><br />
                         <a href="../FPOCS/FPOC-12-04 Solicitud de Derogación v2.docx" class="mt-4" style="font: bold; font-size: large; color: black"> &nbsp<i class="glyphicon glyphicon-open-file"></i><i>&nbsp FPOC-12-04: Solicitud de derogación</i></a><br />
                                      
                     </div>
                                            
                                                    
                    </div>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-danger btn-lg " data-dismiss="modal"><span class="glyphicon glyphicon-arrow-left"></span></button>
                      <button id="btnGuardaEvidencias" runat="server" type="button" class="btn btn-success btn-lg pull-right" onserverclick="guarda_evidencias"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                      
                    </div>
                  </div>
      
                </div>
              </div>
    <div class="table-responsive">
        <asp:GridView ID="dgv_AreaRechazo" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridViewCommandEventHandler" OnRowDeleting="GridView_RowDeleting"
             EmptyDataText="There are no data records to display." on>
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <%-- DataKeyNames="Id" ShowFooter="true"  "
            "OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing OnRowUpdating="GridView_RowUpdating" 
            OnRowCommand="gridView_RowCommand" OnRowDeleting="GridView_RowDeleting" OnRowCommand="gridView_RowCommand" --%>
            <EditRowStyle BackColor="#ffffcc" />
            <AlternatingRowStyle BackColor="#e8e8e8" />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%">
                    <ItemTemplate>
                        <%--Botones de eliminar y editar cliente...--%>
                        <asp:Button ID = "Button2" runat = "server" CommandName="Edit" class="btn btn-primary" Width="100%" Text="Detalles" />
                        <asp:Button ID = "btnAbrirModal" runat ="server" class="btn btn-info" CommandName="CargaDetalle" Width="100%" CommandArgument='<%#Eval("IdNoConformidad")%>' UseSubmitBehavior="true" Text="Evidencias" />
                        
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID = "Button2" runat = "server" CommandName="Update" class="btn btn-success" Width="100%" Text="Guardar" />
                        <asp:Button ID = "Button3" runat = "server" Visible="false" CommandName="Delete" class="btn btn-danger" Width="100%" Text="Eliminar" />
                        <asp:Button ID = "Button4" runat = "server" CommandName="Cancel" class="btn btn-default" Width="100%" Text="Cancelar" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No Conformidad" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <asp:Label ID="lblNC" runat="server" Font-Size="Large" Text='<%#"NC-"+Eval("IdNoConformidad") %>' /><br />
                        <asp:Label ID="lblFecha" runat="server" Text='<%#"(" + Eval("FechaOriginal", "{0:dd/MM/yyyy}" + ")") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID = "btnAbrirAlerta" runat = "server" CommandName="Redirect" class="btn btn-xs btn-primary" Width="100%" CommandArgument='<%#Eval("IdNoConformidad")%>' Text="Ver alerta" /><br />
                        <asp:Label ID="txtNC" runat="server" Font-Size="Large" Text='<%#Eval("IdNoConformidad") %>' />
                        <asp:Label ID="txtFecha" Visible="false" runat="server" Text='<%#Eval("FechaOriginal", "{0:dd/MM/yyyy}") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo" ItemStyle-Width="13%">
                    <ItemTemplate>         
                        <asp:Label ID="lblCliente" runat="server" Font-Size="Large" Text='<%#Eval("Cliente") %>' /><br />
                        <asp:Label ID="lblTipo" runat="server" Font-Bold="true" Text='<%#Eval("TipoTEXT") %>' />
                        <asp:Label ID="lblNivel" runat="server" Text='<%#"(" + Eval("NCTEXT") + " "+ Eval("IdNoConformidadCliente") +")" %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label runat="server" Font-Bold="true" Text="Nivel de escalado:" />
                        <asp:DropDownList ID="NivelAlerta" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True"> 
                            <asp:listitem text="---" Value="0"></asp:listitem>
                            <asp:listitem text="Q-Info" Value="1"></asp:listitem>
                            <asp:listitem text="Reclamación oficial" Value="2"></asp:listitem>
                            <asp:listitem text="No aceptada" Value="3"></asp:listitem>
                        </asp:DropDownList> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="21%">
                    <ItemTemplate> 
                        <asp:Label ID="lblReferencia" Font-Size="Large" runat="server" Text='<%#Eval("Referencia") %>' />
                        <asp:Label ID="lblReiteraDefecto" Font-Size="SMALL" runat="server" Text='<%#Eval("RepiteDefecto10") %>' BackColor="Red" BorderStyle="Groove" BorderColor="RED" ForeColor="White" Font-Bold="true" Visible="false" />
                        <asp:Label ID="lblReiteraProducto" Font-Size="SMALL" runat="server" Text='<%#Eval("RepiteReferencia10") %>' BackColor="OrangeRed" BorderStyle="Groove" BorderColor="OrangeRed" ForeColor="White" Font-Bold="true"  Visible="false" /> 
                        <br />
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Equipo de trabajo:" /><br />
                        <asp:Label ID="txtPilotoProd" runat="server" Text='<%#"<strong>PRODUCCIÓN: </strong>" + Eval("ENCARGADOTEXT") %>' /><br />
                        <asp:Label ID="txtPilotoCal" runat="server" Text='<%#"<strong>CALIDAD: </strong>" +Eval("CALIDADTEXT") %>' /><br />
                        <asp:Label ID="txtPilotoING" runat="server" Text='<%#"<strong>INGENIERÍA: </strong>" +Eval("INGENIERIATEXT") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hitos Previstos" ItemStyle-Width="11%">
                    <ItemTemplate>
                        <asp:Label ID="D3TRANS" runat="server" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Text='D3:' BorderStyle="Groove" /><asp:Label ID="lblFechaD3" runat="server" Text='<%#Eval("D3") %>' /><br />
                        <asp:Label ID="D6TRANS" runat="server" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Text='D6:' BorderStyle="Groove" /><asp:Label ID="lblFechaD6" runat="server" Text='<%#Eval("CD6") %>' /><br />   
                        <asp:Label ID="D8TRANS" runat="server" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Text='D8:' BorderStyle="Groove" /><asp:Label ID="lblFechaD8" runat="server" Text='<%#Eval("CD8") %>' />
                        <asp:Label ID="OCD6TRANS" runat="server" Visible="false" Text='<%#Eval("D6") %>' />
                        <asp:Label ID="OCD8TRANS" runat="server" Visible="false" Text='<%#Eval("D8") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                         <asp:Label ID="TXTD3TRANS" runat="server" Font-Bold="true" BackColor="Transparent" BorderColor="Transparent" Text='D3:' BorderStyle="Groove" /><asp:TextBox ID="TXTFechaD3" runat="server" Text='<%#Eval("D3") %>' /><br />
                         <label><asp:CheckBox ID="TXTD6TRANS" type="checkbox" runat="server" value="" Checked="true" />&nbspD6:</label><asp:TextBox ID="TXTFechaD6" CssClass="textbox Add-text" autocomplete="off" AutoCompleteType="Disabled" runat="server" Text='<%#Eval("CD6") %>' /><br />  
                         <label><asp:CheckBox ID="TXTD8TRANS" type="checkbox" runat="server" value="" Checked="true" />&nbspD8:</label><asp:TextBox ID="TXTFechaD8" CssClass="textbox Add-text" autocomplete="off" AutoCompleteType="Disabled" runat="server" Text='<%#Eval("CD8") %>' />
                    </EditItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Hitos Reales" ItemStyle-Width="11%">
                    <ItemTemplate>
                         <asp:Label ID="D3COLOR" runat="server" ForeColor="White" Font-Bold="true" BackColor="#33cc33" BorderColor="#33cc33" Text='&nbspD3&nbsp ' BorderStyle="Groove" />&nbsp<asp:Label ID="lblFechaD3ent" runat="server" Text='<%#Eval("D3CIERRE") %>' /><br />
                         <asp:Label ID="D6COLOR" runat="server" ForeColor="White" Font-Bold="true" BackColor="#999999" BorderColor="#999999" Text='&nbspD6&nbsp ' BorderStyle="Groove" />&nbsp<asp:Label ID="lblFechaD6ent" runat="server" Text='<%#Eval("CD6CIERRE") %>' /><br />  
                         <asp:Label ID="D8COLOR" runat="server" ForeColor="White" Font-Bold="true" BackColor="red" BorderColor="red" Text='&nbspD8&nbsp ' BorderStyle="Groove" />&nbsp<asp:Label ID="lblFechaD8ent" runat="server" Text='<%#Eval("CD8CIERRE") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                         <asp:Label ID="TXTD3COLOR" runat="server" Font-Bold="true" Text='&nbspD3:&nbsp ' />&nbsp<asp:TextBox ID="txtFechaD3ent" CssClass="textbox Add-text" autocomplete="off" AutoCompleteType="Disabled" runat="server" Enabled="false" Text='<%#Eval("D3CIERRE") %>' /><br />
                         <asp:Label ID="TXTD6COLOR" runat="server" Font-Bold="true" Text='&nbspD6:&nbsp ' />&nbsp<asp:TextBox ID="txtFechaD6ent" CssClass="textbox Add-text" autocomplete="off" AutoCompleteType="Disabled" runat="server" Enabled="false" Text='<%#Eval("CD6CIERRE") %>' /><br />  
                         <asp:Label ID="TXTD8COLOR" runat="server" Font-Bold="true" Text='&nbspD8:&nbsp ' />&nbsp<asp:TextBox ID="txtFechaD8ent" CssClass="textbox Add-text" autocomplete="off" AutoCompleteType="Disabled" runat="server" Enabled="false" Text='<%#Eval("CD8CIERRE") %>' />
                    </EditItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Defecto denunciado" ItemStyle-Width="29%">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripciónActual" runat="server" Text='<%#Eval("DescripcionProblema") %>' /><br />
                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text='Comentarios:' /><br />
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("NCObservaciones") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtDescripcionProblema" runat="server" Text='<%#Eval("DescripcionProblema") %>' /><br />
                        <asp:Label runat="server" Font-Bold="true" Text="Comentarios:" /><br />
                        <asp:Textbox ID="lblNCObservaciones" runat="server" TextMode="MultiLine" Width="100%" Enabled="true" Text='<%#Eval("NCObservaciones") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                          

            </Columns>
        </asp:GridView>
    </div>
       
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script> 
    </form>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
      <script src="js/json2.js" type="text/javascript"></script>
      <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
      <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</body>
</html>
