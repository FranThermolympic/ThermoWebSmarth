<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FichaReferencia.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.FichaReferencia"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>
<meta http-equiv="Page-Exit" content="blendTrans(Duration=0.5)" />
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión documental de referencia</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
        <script src="js/json2.js" type="text/javascript"></script>
        <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
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
    <h1>&nbsp;&nbsp;&nbsp; Ficha de Referencia</h1>

    <div class="container-fluid">
        <div class="row"><%--cabecera de selección--%>
            <div class="col-lg-4">
                <div id="Div1" class="panel panel-default" runat="server">
                    <div class="panel-body"> 
                        <div class="row">
                            <div class="col-lg-7">
                                <div class="form-group">
                                    <label for="usr">Referencia:</label>
                                    <input id="tbReferencia" runat="server" type="text" class="form-control" placeholder="Escribir referencia">
                                    </div>
                             
                            </div>
                             <div class="col-lg-5">
                                <div class="form-group">
                                <br />
                                <button id="btnCargar" runat="server" type="button" style="width:100%" onserverclick="CargarDatos" class="btn btn-primary">Cargar datos</button>
                                </div>
                            </div>
                            <script type="text/javascript">
                                function BorraREF() {
                                    var reply = confirm("Vas a borrar los documentos seleccionados de esta referencia, ¿estás seguro?");
                                    if (reply) {
                                        return true;
                                    }
                                    else {
                                        return false;
                                    }
                                }
                            </script>
                            <script type="text/javascript">
                                  function BorraMOL() {
                                   var reply = confirm("Vas a borrar los documentos seleccionados de todas las referencias que comparten este molde, ¿estás seguro?");
                                   if (reply) {
                                         return true;
                                   }
                                   else {
                                       return false;
                                   }
                                }
                            </script>
                            <script type="text/javascript">
                                function guardado_OK() {
                                    return confirm('Vas a guardar los documentos de la referencia, ¿estás seguro?');
                             
                                }
                            </script>
                        <script type="text/javascript">
                                function guardado_NOK() {
                                    alert("Se ha producido un error al guardar la orden de revisión. Revise que los datos introducidos estén bien, el número de lote seleccionado y los operarios en formato numérico).");
                                }
                        </script>
                            </div>
                         <div class="row">
                         <div class="col-lg-12">
                             <div runat="server" visible="false">
                                 <asp:TextBox ID="txtPlanControl" runat="server"></asp:TextBox>
                                 <asp:TextBox ID="txtPautaControl" runat="server"></asp:TextBox>
                                 <asp:TextBox ID="txtOperacionEstandar" runat="server"></asp:TextBox>
                                 <asp:TextBox ID="txtDefoteca" runat="server"></asp:TextBox>
                                 <asp:TextBox ID="txtEmbalaje" runat="server"></asp:TextBox>


                             </div>
                         <div class="table-responsive">
                                    <table width="100%">
                                        <tr>
                                            <th>
                                                <asp:TextBox ID="tituloReferenciaCarga" width="100%" runat="server"  Style="text-align: center" Enabled="false">Referencia</asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="tituloMolde" runat="server"  width="100%" Style="text-align: center" Enabled="false">Molde</asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr>
                                        <td >
                                                <asp:TextBox ID="tbReferenciaCarga" width="100%" runat="server"  Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbMolde" width="100%" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>   
                                             
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="tbDescripcionCarga" width="100%" runat="server" Style="text-align: center" Enabled="false"></asp:TextBox>
                                            </td>   
                                        </tr>
                                        <tr>
                                            <th colspan="2">
                                                <asp:TextBox ID="tbTituloObservaciones" width="100%" runat="server" Style="text-align: center" Enabled="false">Notas de ficha</asp:TextBox>
                                            </th>   
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="tbObservacionesCarga" width="100%" runat="server" Style="text-align: center" Enabled="false" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </td>   
                                        </tr>
                                        <tr>
                                            <th colspan="2">
                                                <asp:TextBox ID="tbTituloObservacionesBMS" width="100%" runat="server" Style="text-align: center" Enabled="false">Notas producto BMS</asp:TextBox>
                                            </th>   
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="tbObservacionesCargaBMS" width="100%" runat="server" Style="text-align: center" Enabled="false" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </td>   
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                    <button id="btnreferencia" type="button" runat="server"  onserverclick = "InsertarDocumentosReferenciaMolde" class="btn btn-primary btn-sm" style="width: 100%">Actualizar nota</button><br />
                                                    <br />
                                                    <button id="btnderogacion" type="button" runat="server"  class="btn btn-basic" style="width: 100%; background-color:cornflowerblue; color:white">Crear derogación</button> 
                                                    <br />
                                                    <button id="btnregistro" type="button" runat="server"  class="btn btn-primary" style="width: 100%">Crear registro de cambio de proceso</button> 
                                                    <br />
                                                    <button id="btnpartedeprueba" type="button" runat="server"  class="btn btn-basic" style="width: 100%; background-color:cornflowerblue; color:white">Crear parte de prueba</button> 
                                                    <br />
                                                    <button id="Button1" type="button" runat="server"  onserverclick = "Imprimiretiqueta" class="btn btn-basic btn-sm" style="width: 100%">Ver Etiqueta</button> 
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <div id="Div5">
                            <br />
                                    <div class="well well-sm">

                                       <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <button ID="btnVistaMaquina" type="button" runat="server" onserverclick = "RedireccionaOTROS" class="btn btn-default btn-lg" disabled="true">Vista previa (Máquina)</button>
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <button id="btnVistaGP12" type="button" runat="server"  onserverclick = "RedireccionaGP12" class="btn btn-default btn-lg" disabled="true">Vista previa (GP12)</button>
                                            </div>
                                       </div>                                        
                                       <br/>
                                       <div class="btn-group btn-group-justified" role="group"">
                                           <%--  <div class="btn-group" role="group">
                                                    <button ID="btnreferencia" type="button" runat="server" onserverclick = "InsertarDocumentosReferenciaMolde" class="btn btn-primary btn-lg">Guardar en referencia</button>
                                            </div>--%>
                                            
                                            <div class="btn-group" role="group">
                                                    <button id="btnmolde" type="button" runat="server"  onserverclick = "InsertarDocumentosMolde" class="btn btn-info btn-lg"  disabled="true">Guardar seleccionados en molde</button>
                                            </div>
                                       </div>
                                       <div class="btn-group btn-group-justified" role="group"">
                                            <div class="btn-group" role="group">
                                                    <asp:Button ID="borrarref" type="button" runat="server" OnClientClick="return BorraREF();" onclick = "BorrarDocumentoSeleccionados" class="btn btn-danger btn-md" Visible="false" Text="Borrar sel. de referencia" />
                                            </div>
                                            <div class="btn-group" role="group">
                                                    <asp:Button ID="borrarmolde" type="button" runat="server" OnClientClick="return BorraMOL();" onclick = "BorrarDocumentoSeleccionados" class="btn btn-danger btn-md" Visible="false" Text="Borrar sel. de molde" />
                                            </div>
                                       </div>     

                                    </div>
                                </div>
                         </div>
                         </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-8">
                    <div id="Div4" class="panel panel-primary" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Detalles</h3>
                        </div>
                <div class="panel-body">
                <div class="row">
                <div class="col-lg-7">
                    <div id="Div7" class="panel panel-primary" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Referencias que comparten molde</h3>
                        </div>
                    <div style="overflow-y: scroll;height: 300px">
                            <asp:GridView ID="dgv_RefXMolde" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display.">
                           
                            <EditRowStyle BackColor="#ffffcc" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("Referencia") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="250px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>                   
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                 </div>
                 <div class="col-lg-5">
                      <div class="thumbnail"> 
                      <label class="checkbox-inline"><input id="CHECKIMG" runat="server" type="checkbox" value=""></label>
                      <button ID="btnsubir0" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>         
                         <asp:HyperLink id="hyperlink9" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                         <asp:FileUpload ID="FileUpload0" runat="server" accept=".png,.jpg,.jpeg"></asp:FileUpload>          
                      </div>
                 </div><%--final de panel de descripción--%>  
                
              </div>
            </div>
           </div>
         </div>
         
        </div>
      
        <div class="row"><%--panel de revisión--%>
                <div class="col-lg-12">
                    <div id="Div3" class="panel panel-primary" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <i class="fa fa-list-ul fa-fw"></i>Documentos</h3>
                        </div>
                        <div class="panel-body">

                       <div class="row"><%--cuatro columnas 1 --%>
                       <div class="col-lg-3">
                            <div id="DefaultPlanControl" class="panel panel-default" runat="server">
                                <div class="panel-heading">                                   
                                    <h3 class="panel-title">
                                        <label class="checkbox-inline"><input id="CHECKDEFPC" runat="server" type="checkbox" value="">Plan de control</label>
                           
                                          <button ID="btnsubir1" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>                                      
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink8" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload1" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                          
                                           </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>


                            <div id="PreviewPlanControl" class="panel panel-default" runat="server" visible=false>
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <label class="checkbox-inline"><input id="CHECKPREVPC" runat="server" type="checkbox" value="">Plan de control</label>
                                    
                                        <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver2" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir2" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar2" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 
                                       </div>
                                       <br />
                                       <br />
                                    </h3>    
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="frameplandecontrol" class="embed-responsive-item" src=""  width="100%" runat="server"></iframe>
                                        <asp:FileUpload ID="FileUpload2" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div id="DefaultPautaControl" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <label class="checkbox-inline"><input id="CHECKDEFPAC" runat="server" type="checkbox" value="">Pauta de control</label>
                                   
                                    <button ID="btnsubir4" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink7" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload3" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>

                            <div id="PreviewPautaControl" class="panel panel-default" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                       <label class="checkbox-inline"><input id="CHECKPREVPAC" runat="server" type="checkbox" value="">Pauta de control</label>

                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver5" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir5" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar5" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 
                                       </div>
                                       <br />
                                       <br />   
                                      </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="framepautadecontrol" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload4" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div id="DefaultPautaRecepcion" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <label class="checkbox-inline"><input id="CHECKDEFPREC" runat="server" type="checkbox" value="">Pauta de recepción</label>
                                    <button ID="btnsubir6" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink6" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload5" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>


                            <div id="PreviewPautaRecepcion" class="panel panel-default" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                       <label class="checkbox-inline"><input id="CHECKPREVREC" runat="server" type="checkbox" value="">Pauta de recepción</label>
                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver7" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir7" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar7" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 
                                       </div>
                                       <br />
                                       <br />
                                       </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="framepautarecepcion" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload6" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div id="DefaultOperacionEstandar" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKDEFHOS" runat="server" type="checkbox" value="">Operación estándar</label>
                                    <button ID="btnsubir8" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink5" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload7" runat="server" accept=".png,.jpg,.jpeg,.pdf,.mp4"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>


                            <div id="PreviewOperacionEstandar" class="panel panel-default" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKPREVHOS" runat="server" type="checkbox" value="">Operación estándar</label>
                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver9" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir9" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar9" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 
                                       </div>
                                       <br />
                                       <br />
                                        </h3>  
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="frameoperacionestandar" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload8" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>
                      </div>

                      <div class="row"><%--cuatro columnas 2 --%>
                       <div class="col-lg-3">
                            <div id="DefaultGP12" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKDEFGP12" runat="server" type="checkbox" value="">GP12</label>
                                    <button ID="btnsubir10" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink4" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload9" runat="server"  accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>

                            <div id="PreviewGP12" class="panel panel-default" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKPREVGP12" runat="server" type="checkbox" value="">GP12</label>
                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver11" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir11" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar11" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 
                                       </div>
                                       <br />
                                       <br />
                                       </h3>
                                  
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="framemurodecalidad" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload10" runat="server"  accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="col-lg-3">
                        <div id="DefaultOperacionEstandar2" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                   <label class="checkbox-inline"><input id="CHECKDEFHOS2" runat="server" type="checkbox" value="">Operación estándar 2 / Pokayoke</label>
                                    <button ID="btnsubir12" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink3" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload11" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>



                            <div id="PreviewOperacionEstandar2" class="panel panel-default" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKPREVHOS2" runat="server" type="checkbox" value="">Operación estándar 2 / Pokayoke</label>
                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver13" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir13" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar13" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 

                                       </div>
                                       <br />
                                       <br />
                                        </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="frameoperacionestandar2" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload12" runat="server" accept=".png,.jpg,.jpeg,.pdf,.mp4"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="col-lg-3">
                        <div id="DefaultDefoteca" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKDEFDEF" runat="server" type="checkbox" value="">Defoteca</label>
                                    <button ID="btnsubir14" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br />
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink2" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload13" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>

                            <div id="PreviewDefoteca" class="panel panel-default" runat="server"  visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKPREVDEF" runat="server" type="checkbox" value="">Defoteca</label>
                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver15" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir15" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar15" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 
                                       </div>
                                       <br />
                                       <br />
                                        </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="framedefoteca" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload14" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="col-lg-3">
                        <div id="DefaultEmbalaje" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKDEFEMB" runat="server" type="checkbox" value="">Embalaje</label>
                                    <button ID="btnsubir16" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink1" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload15" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>

                            <div id="PreviewEmbalaje" class="panel panel-default" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKPREVEMB" runat="server" type="checkbox" value="">Embalaje</label>
                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver17" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir17" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</span></button>
                                                    <button ID="btnborrar17" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</span></button> 
                                       </div>
                                       <br />
                                       <br />
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="frameembalaje" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload16" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div><%--final de columna 2-3 --%> 
                      </div><%--final de fila 2 --%>
                      <div class="row"><%--cuatro columnas 3 --%>
                       <div class="col-lg-3">
                            <div id="defaultRetrabajo" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKDEFRTR" runat="server" type="checkbox" value="">Pauta de retrabajo</label>
                                    <button ID="btnsubir18" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink10" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload17" runat="server"  accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>

                            <div id="previewRetrabajo" class="panel panel-default" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKPREVRTR" runat="server" type="checkbox" value="">Pauta de retrabajo</label>
                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver19" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir19" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar19" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 
                                       </div>
                                       <br />
                                       <br />
                                       </h3>
                                  
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="frameretrabajo" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload18" runat="server"  accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="col-lg-3">
                        <div id="defaultVideo" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKDEFVID" runat="server" type="checkbox" value="">Video auxiliar</label>
                                    <button ID="btnsubir20" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink11" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload19" runat="server" accept=".mp4"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>

                            <div id="previewVideo" class="panel panel-default" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKPREVVID" runat="server" type="checkbox" value="">Video auxiliar</label>
                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver21" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir21" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar21" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 

                                       </div>
                                       <br />
                                       <br />
                                        </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="framevideoaux" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload20" runat="server" accept=".mp4"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>
                    <div class="col-lg-3">
                        <div id="defaultNC" class="panel panel-default" runat="server">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKDEFNC" runat="server" type="checkbox" value="">No conformidades</label>
                                    <button ID="btnsubir22" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm" style="float:right">Subir</button>
                                    </h3>
                                    <br /> 
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <div class="thumbnail">            
                                          <asp:HyperLink id="hyperlink12" NavigateUrl="" ImageUrl= "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server"/>
                                          <asp:FileUpload ID="FileUpload21" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                          </div>
                                        </div>                               
                                    </div>
                                </div> 
                            </div>

                            <div id="previewNC" class="panel panel-default" runat="server" visible="false">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                    <label class="checkbox-inline"><input id="CHECKPREVNC" runat="server" type="checkbox" value="">No conformidades</label>
                                       <div class="btn-group " role="group" style="float:right">     
                                                    <button ID="btnver23" type="button" runat="server" onserverclick = "RedireccionaDocumento" class="btn btn-success btn-sm">Ver</button>
                                                    <button ID="btnsubir23" type="button" runat="server" onserverclick = "insertar_documento" class="btn btn-primary btn-sm">Subir</button>
                                                    <button ID="btnborrar23" type="button" runat="server" onserverclick = "BorrarDocumento" class="btn btn-danger btn-sm">Borrar</button> 

                                       </div>
                                       <br />
                                       <br />
                                        </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">     
                                        <div class="col-lg-12">
                                        <iframe id="frameNoConformidad" class="embed-responsive-item" src="" width="100%" runat="server"></iframe> 
                                        <asp:FileUpload ID="FileUpload22" runat="server" accept=".png,.jpg,.jpeg,.pdf"></asp:FileUpload> 
                                        </div>                               
                                    </div>
                                </div> 
                            </div>
                        </div>
                        <%--final de columna 2-3 --%> 
                      </div> <%--final de columna 3-3 --%>
                    </div>
                    </div>
                 </div>
         </div><%--final de panel de revisión --%>         
      </div><%--container--%>
    </form>
</body>
</html>
