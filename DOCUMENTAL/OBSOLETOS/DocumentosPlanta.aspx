<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="DocumentosPlanta.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.DocumentosPlanta"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>
    <meta http-equiv="refresh" content="3600">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Documentación de fabricación</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <%-- <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous"> --%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <%-- <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script> --%>
    <link href="https://cdn.jsdelivr.net/css-toggle-switch/latest/toggle-switch.css" rel="stylesheet" />
    <script src="js/json2.js" type="text/javascript"></script>

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
            <ul class="nav navbar-nav navbar-right"> 
                <li><a href="#" id="navbarrecarga" runat="server" onserverclick="lanzaPostback"><span class="glyphicon glyphicon-refresh"></span>   Recargar con orden actual</a></li>
            </ul>
        </div>
      </div>
    </nav>
        <div class="container-fluid" >
    <div class="row">
        <div class="col-lg-12">
            <div class="alert alert-warning" runat="server" id="AlertaDOC1" Visible="false">
                <asp:TextBox ID="AlertaDOCTEXT1"  runat="server" Font-Size="Large"  Enabled="false" Font-Italic="true" BackColor="Transparent" ForeColor="Black" Width="100%" BorderColor="Transparent"></asp:TextBox><button type="button" runat="server" id="Button1" class="btn btn-default" onserverclick="CerrarAvisoDocumentacion" Style="text-align:left; background-color:transparent; border-color:transparent; font-weight:bold">>Pincha aquí para cerrar este cuadro de diálogo.<</button>                              
            </div>
       </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
          <ul class="nav nav-pills nav-justified">
                    <li id="ref1lab" class="active" runat="server" visible="true"><a id="ref1labtext" data-toggle="pill" runat="server" href="#REF1">---</a></li>
                    <li id="ref2lab" runat="server" visible="false"><a id="ref2labtext" data-toggle="pill" runat="server" href="#REF2">---</a></li>
                    <li id="ref3lab" runat="server" visible="false"><a id="ref3labtext" data-toggle="pill" runat="server" href="#REF3">---</a></li>
                    <li id="ref4lab" runat="server" visible="false"><a id="ref4labtext" data-toggle="pill" runat="server" href="#REF4">---</a></li>
          </ul>
        </div>
    </div>
    <div class="row">
              <div class="col-lg-12">
                  <div class="tab-content"> 
                  <div id="REF1" class="tab-pane fade in active">
                  <ul class="nav nav-pills nav-justified">
                        <li class="active"><a data-toggle="pill" href="#ORDEN">MÁQUINA</a></li>
                        <li><a data-toggle="pill" href="#PAUTACONTROL">PAUTA DE CONTROL</a></li>
                        <li><a data-toggle="pill" href="#ESTANDAR">ESTÁNDAR</a></li>
                        <li><a data-toggle="pill" href="#DEFECTOLOGIA">DEFECTOLOGIA</a></li>
                        <li><a data-toggle="pill" href="#IMAGENES">DEFECTOS GP12</a></li>
                        <li><a data-toggle="pill" href="#EMBALAJE">EMBALAJE</a></li>
                   </ul>
                  <div class="row" ID="NUM1" runat="server">
            <div class="col-lg-12">
                <div class="table-responsive">
                    <table width="100%">  
                        <tr>
                            <th>
                                <button type="button" runat="server" id="DOC1" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Documentación de la referencia</button>                       
                            </th>
                            <th>
                                <button type="button" runat="server" id="LIB1"  class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Liberación de serie</button>
                            </th>
                            <th>
                                <button type="button" runat="server" id="MUR1" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Datos del muro</button>
                                 </th>
                            <th>
                                <button type="button" runat="server" id="FAB1" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Ficha de fabricación</button>
                            <th>
                        </tr>
                    </table>    
                </div>
            </div>
            </div>
                  <div class="row">
                    <div class="col-lg-12">
                    <asp:TextBox ID="tbAlertaCalidad1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ffff00" Font-Bold="true" Font-Size="X-Large" Visible="false" >PRODUCTO PENDIENTE DE MURO DE CALIDAD</asp:TextBox>
                    </div>
                  </div>
                  <div class="tab-content">          
                    <div id="ORDEN" class="tab-pane fade in active" runat="server">
                      <div class="row">
                           <div class="col-lg-12">
                            <div class="col-lg-1"></div>
                             <div class="col-lg-3">
                           <br />
                           <br />  
                       <asp:Image ID="CLIENTE" runat="server" ImageUrl="" Width="100%" />
                       </div>
                             <div class="col-lg-1"></div>
                             <div class="col-lg-6" runat="SERVER">

                          <div class="col-lg-12" runat="server" visible =" false">

                            <h2>Datos generales</h2>
                            <div class="table-responsive">
                                <table style="table-layout:fixed" runat="server">
                                <tr>
                                    <th runat="server" visible="false">
                                        <asp:TextBox ID="tbfechaMod" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff"></asp:TextBox>
                                    </th>
                                    <th runat="server" visible="false">
                                        <asp:TextBox ID="RazonMod" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff"></asp:TextBox>
                                    </th>


                                    <th style="width:10%">
                                        <asp:TextBox ID="tbMaquinaTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"  BackColor="Orange">MÁQUINA</asp:TextBox>
                                    </th>
                                    <td style="width:10%">
                                        <asp:TextBox ID="tbMaquina1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <th style="width:10%">
                                        <asp:TextBox ID="tbMolde1desc" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"  BackColor="Orange" >MOLDE</asp:TextBox>
                                    </th>
                                    <td style="width:25%">
                                        <asp:TextBox ID="tbMolde1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <th style="width:15%">
                                        <asp:TextBox ID="tbCalidadTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="Orange">ESTADO</asp:TextBox>
                                    </th>
                                    <td style="width:30%">
                                        <asp:TextBox ID="tbCalidad1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                </table>
                             </div>
                          </div>
                      
                          <div class="col-lg-12"><h2>Referencias produciendo</h2></div>
                          <asp:GridView ID="dgv_Ordenes" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
                                EmptyDataText="No hay datos para mostrar.">
                                <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
                                OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
                                --%>
                                <HeaderStyle BackColor="orange" Font-Bold="True" />
                                <Columns>
                                    <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>
                
                                    <asp:TemplateField HeaderText="ORDEN" HeaderStyle-Width="20%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#ccccff" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("C_ID") %>'  Font-Bold="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REFERENCIA" HeaderStyle-Width="20%"  FooterStyle-BackColor="#EFEFEF">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("C_PRODUCT_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DESCRIPCION" HeaderStyle-Width="60%" FooterStyle-BackColor="#EFEFEF">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripción" runat="server" Text='<%#Eval("C_PRODLONGDESCR") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>            
                                </Columns>
                            </asp:GridView>
                          <div class="col-lg-12">
                            <h2>Personal trabajando</h2>
                            <div class="table-responsive">
                                <table style="table-layout:fixed;width:100%">
                                    <tr>
                                    <th style="width:25%">
                                        <asp:TextBox ID="TituloPosicion" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="Orange">Posición</asp:TextBox>
                                    </th>
                                    <th style="width:10%">
                                        <asp:TextBox ID="TituloNumOpe" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="Orange">Nº</asp:TextBox>
                                    </th>
                                    <th style="width:35%">
                                        <asp:TextBox ID="TituloNomOpe" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="Orange">Nombre</asp:TextBox>
                                    </th>
                                    <th style="width:20%">
                                        <asp:TextBox ID="TituloHorasAcumuladas" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="Orange">Horas acum.</asp:TextBox>
                                    </th>
                                    <th style="width:20%">
                                        <asp:TextBox ID="TituloNivel" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="Orange">Nivel</asp:TextBox>
                                    </th>
                                </tr>
                                <tr>
                                    <th style="width:25%">
                                        <asp:TextBox ID="Operario1Posicion" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false" BackColor="#ccccff">OPERARIO</asp:TextBox>
                                    </th>
                                    <td style="width:10%">
                                        <asp:TextBox ID="Operario1Numero" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false">---</asp:TextBox>
                                    </td>
                                    <td style="width:35%">
                                        <asp:TextBox ID="Operario1Nombre" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false">SIN LOGUEAR</asp:TextBox>
                                    </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="Operario1Horas" runat="server" Style="text-align:center; width:100%; Height:34px"  Enabled="false">---</asp:TextBox>
                                    </td>
                        
                                    <td style="width:20%">
                                        <asp:TextBox ID="Operario1Nivel" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false">---</asp:TextBox>
                                    </td>
                        
                                </tr>
                                <tr>
                                    <th style="width:25%">
                                        <asp:TextBox ID="Operario2Posicion" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false" Visible="false" BackColor="#ccccff">OPERARIO 2</asp:TextBox>
                                    </th>
                                    <td style="width:10%">
                                        <asp:TextBox ID="Operario2Numero" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false" Visible="false">---</asp:TextBox>
                                    </td>
                                    <td style="width:35%">
                                        <asp:TextBox ID="Operario2Nombre" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false" Visible="false">SIN LOGUEAR</asp:TextBox>
                                    </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="Operario2Horas" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false" Visible="false">---</asp:TextBox>
                                    </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="Operario2Nivel" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false">---</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width:25%">
                                        <asp:TextBox ID="EncargadoPosicion" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false" BackColor="#ccccff">ENCARGADO</asp:TextBox>
                                    </th>
                                    <td style="width:10%">
                                        <asp:TextBox ID="EncargadoNumero" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false">---</asp:TextBox>
                                    </td>
                                    <td style="width:35%">
                                        <asp:TextBox ID="EncargadoNombre" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false">SIN LOGUEAR</asp:TextBox>
                                    </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="EncargadoHoras" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false">---</asp:TextBox>
                                    </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="EncargadoNivel" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false" Visible="false">---</asp:TextBox>
                                    </td>
                                </tr>                
                                <tr>
                                    <th style="width:25%">
                                        <asp:TextBox ID="CalidadPosicion" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false" BackColor="#ccccff">CALIDAD</asp:TextBox>
                                    </th>
                                    <td style="width:10%">
                                        <asp:TextBox ID="CalidadNumero" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false">---</asp:TextBox>
                                    </td>
                                    <td style="width:35%">
                                        <asp:TextBox ID="CalidadNombre" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false">SIN LOGUEAR</asp:TextBox>
                                    </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="CalidadHoras" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false">---</asp:TextBox>
                                    </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="CalidadNivel" runat="server" Style="text-align:center; width:100%; Height:34px" Enabled="false" Visible="false">---</asp:TextBox>
                                    </td>
                                </tr>
                    
                            </table>  
                        </div>
              </div>
                      
                          <div class="col-lg-12" runat="server">
                              <br />
                          <h2>Reportar errores en documentación</h2>
                          
                              <div class="col-lg-1">
                                <button id="BtnReportar" runat="server" onserverclick="InsertarFeedbackDocumental" type="button" class="btn btn-lg btn-primary" style="width:100%; height:60px; text-align:center">
                                <span class="glyphicon glyphicon-send"></span></button>
                              </div>
                              <div class="col-lg-11">
                                <asp:TextBox ID="tbDenunciaError" runat="server" Style="text-align:center; width:100%; height:60px" TextMode="MultiLine"></asp:TextBox>
                              </div>

                          </div>

                          <div class="table-responsive" runat="server" visible="false">
                                <table style="table-layout:fixed">
                                <tr>
                                    <th style="width:10%">
                                        <asp:TextBox ID="tbOrdenTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Orden</asp:TextBox>
                                    </th>


                                    <td style="width:15%">
                                        <asp:TextBox ID="tbOrden1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <th style="width:20%">
                                        <asp:TextBox ID="tbReferenciaTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Referencia</asp:TextBox>
                                    </th>
                                    <td style="width:20%">
                                        <asp:TextBox ID="tbReferencia1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width:60%">
                                        <asp:TextBox ID="tbNombre1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                </table>  
                        </div>
                          <div class="table-responsive"  runat="server" visible="false">
                                <table style="table-layout:fixed" runat="server">
                                <tr>
                                    <th style="width:34%">
                                        <asp:TextBox ID="tbClienteTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Cliente</asp:TextBox>
                                    </th>
 
                                    <th style="width:33%">
                                        <asp:TextBox ID="tbCicloTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Ciclo</asp:TextBox>
                                    </th>
                                    <th style="width:33%">
                                        <asp:TextBox ID="tbOperariosTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Operarios</asp:TextBox>
                                    </th>

                                
                                </tr>
                                <tr>
                                    <td style="width:34%">
                                        <asp:TextBox ID="tbCliente1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td> 
                                    <td style="width:33%">
                                        <asp:TextBox ID="tbCiclo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width:33%">
                                        <asp:TextBox ID="tbOperarios1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width:34%">
                                        <asp:TextBox ID="tbAProducirTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">A producir</asp:TextBox>
                                    </th>
                                    <th style="width:33%">
                                        <asp:TextBox ID="tbRestantesTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Restantes</asp:TextBox>
                                    </th>

                                </tr>
                                <tr>
                                    <td style="width:34%">
                                        <asp:TextBox ID="tbAProducir1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width:33%">
                                        <asp:TextBox ID="tbRestantes1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>

                                </tr>
                                </table>  
                        </div>
                          <div class="table-responsive">
                                <table style="table-layout:fixed" runat="server">
                                <tr>
                                    <th style="width:34%">
                                        <asp:TextBox ID="tbFechaIniTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Fecha de incio</asp:TextBox>
                                    </th>
 
                                    <th style="width:33%">
                                        <asp:TextBox ID="tbFechaFinTitulo1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff">Fin previsto</asp:TextBox>
                                    </th>
                                
                                </tr>
                                <tr>
                                    <td style="width:34%">
                                        <asp:TextBox ID="tbFechaIni1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td> 
                                    <td style="width:33%">
                                        <asp:TextBox ID="tbFechaFin1" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                </table>  
                        </div>

                       </div>
                             <div class="col-lg-1"></div>
                           </div>
                        </div>
                    </div>            
                    <div id="PAUTACONTROL" class="tab-pane fade ">
                      <div class="row">
                        <div class="col-lg-12">
                            <%-- <div class='embed-container'><iframe id="PautaControl_1" runat="server" style="overflow:hidden; height: 80%; width: 100%"></iframe></div> --%> 
                                <embed id="PautaControl_1" runat="server" src="tbd" style="overflow:hidden; height: 80%; width: 100%" />
                         </div>
                       </div>
                    </div>
                    <div id="ESTANDAR" class="tab-pane fade">
                      <div class="row">
                        <div class="col-lg-12">
                            <div class='embed-container'><iframe id="GP12_1" runat="server" style="overflow:hidden; height: 80%; width: 100%"></iframe></div>
                         </div>  
                       </div>
                    </div>
                    <div id="DEFECTOLOGIA" class="tab-pane fade">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class='embed-container'><iframe id="DEFECTOS_1" runat="server" style="overflow:hidden; height: 80%; width: 100%"></iframe></div>

                       </div> 
                    </div>
                    </div>
                    <div id="IMAGENES" class="tab-pane fade">
                      <div class="row">
                        <div class="col-lg-12">
                         <div class="container">
                          <div id="myCarousel" class="carousel slide" data-ride="carousel">
                            <!-- Indicators -->
                            <ol class="carousel-indicators">
                              <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                              <li data-target="#myCarousel" data-slide-to="1"></li>
                              <li data-target="#myCarousel" data-slide-to="2"></li>
                              <li data-target="#myCarousel" data-slide-to="3"></li>
                              <li data-target="#myCarousel" data-slide-to="4"></li>
                              <li data-target="#myCarousel" data-slide-to="5"></li>
                              <li data-target="#myCarousel" data-slide-to="6"></li>
                              <li data-target="#myCarousel" data-slide-to="7"></li>
                              <li data-target="#myCarousel" data-slide-to="8"></li>
                              <li data-target="#myCarousel" data-slide-to="9"></li>
                            </ol>

                            <!-- Wrapper for slides -->
                            <div class="carousel-inner" ID="ACTIVOS" runat="server">
                              <div class="item active">
                                <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" style="width:100%;">
                              </div>
                            </div>

                            <!-- Left and right controls -->
                            <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                              <span class="glyphicon glyphicon-chevron-left"></span>
                              <span class="sr-only">Previous</span>
                            </a>
                            <a class="right carousel-control" href="#myCarousel" data-slide="next">
                              <span class="glyphicon glyphicon-chevron-right"></span>
                              <span class="sr-only">Next</span>
                            </a>
                                </div>
                            </div>  
                         </div>
                    </div>
                    </div>
                    <div id="EMBALAJE" class="tab-pane fade">
                       <div class="row">
                        <div class="col-lg-12">
                            <div class='embed-container'><iframe id="PAUTAEMBALAJE_1" runat="server" style="overflow:hidden; height: 80%; width: 100%"></iframe></div>
                        </div>
                       </div>
                    </div>
            </div>
                </div>
                  <div id="REF2" class="tab-pane fade">
                      <div class="row">
                        <ul class="nav nav-pills nav-justified">
                            <li class="active"><a data-toggle="pill" href="#PAUTACONTROL2">PAUTA DE CONTROL</a></li>
                            <li><a data-toggle="pill" href="#ESTANDAR2">ESTÁNDAR</a></li>
                            <li><a data-toggle="pill" href="#DEFECTOLOGIA2">DEFECTOLOGIA</a></li>
                            <li><a data-toggle="pill" href="#IMAGENES2">DEFECTOS GP12</a></li>
                            <li><a data-toggle="pill" href="#EMBALAJE2">EMBALAJE</a></li>
                        </ul>
                       </div>   
                      <div class="row" runat="server">
                         <div class="col-lg-12">
                <div class="table-responsive">
                    <table width="100%">  
                        <tr>
                            <th>
                                <button type="button" runat="server" id="DOC2" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Documentación de la referencia</button>
                            </th>
                            <th>
                                <button type="button" runat="server" id="LIB2"  class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Liberación de serie</button>
                            </th>
                            <th>
                                <button type="button" runat="server" id="MUR2" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Datos del muro</button>
                                 </th>
                            <th>
                                <button type="button" runat="server" id="FAB2" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Ficha de fabricación</button>
                            <th>
                        </tr>
                    </table>  
                    <asp:TextBox ID="tbAlertaCalidad2" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ffff00" Font-Bold="true" Font-Size="X-Large" Visible="false">PRODUCTO PENDIENTE DE MURO DE CALIDAD</asp:TextBox>
  
                    <table style="table-layout:fixed" runat="server" visible="false">
                        <tr>
                        <th style="width:10%">
                            <asp:TextBox ID="tbOrdenTitulo2" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="#333333" ForeColor="#cccccc">Orden:</asp:TextBox>
                        </th>
                        <td style="width:10%">
                            <asp:TextBox ID="tbOrden2" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                        </td>
                        <th style="width:10%">
                            <asp:TextBox ID="tbReferenciaTitulo2" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="#333333" ForeColor="#cccccc">Referencia:</asp:TextBox>
                        </th>
                        <td style="width:15%">
                            <asp:TextBox ID="tbReferencia2" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width:40%">
                            <asp:TextBox ID="tbNombre2" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                        </td>
                        <th style="width:10%">
                            <asp:TextBox ID="tbMaquinaTitulo2" runat="server" Style="text-align:center; width:100%" Enabled="false"  BackColor="#333333" ForeColor="#cccccc">Máquina:</asp:TextBox>
                        </th>
                        <td style="width:5%">
                            <asp:TextBox ID="tbMaquina2" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width:5%">
                            <asp:TextBox ID="tbMolde2" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    </table>  
                    
            </div>
        
          </div>
                       </div>   
            <div class="tab-content">
                <div id="PAUTACONTROL2" class="tab-pane fade in active">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="embed-responsive embed-responsive-16by9">
                                    <iframe id="PautaControl_2" runat="server" class="embed-responsive-item" src="" allowfullscreen></iframe>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="ESTANDAR2" class="tab-pane fade">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="embed-responsive embed-responsive-16by9">
                                    <iframe id="GP12_2" runat="server" class="embed-responsive-item" src="" allowfullscreen></iframe>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="DEFECTOLOGIA2" class="tab-pane fade">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="embed-responsive embed-responsive-16by9">
                                <iframe id= "DEFECTOS_2" runat="server" class="embed-responsive-item" src="" ></iframe> 
                            </div>
                        </div>
                    </div>
                </div>
                <div id="IMAGENES2" class="tab-pane fade">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="container">
                      <div id="myCarousel_2" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                          <li data-target="#myCarousel_2" data-slide-to="0" class="active"></li>
                          <li data-target="#myCarousel_2" data-slide-to="1"></li>
                          <li data-target="#myCarousel_2" data-slide-to="2"></li>
                          <li data-target="#myCarousel_2" data-slide-to="3"></li>
                          <li data-target="#myCarousel_2" data-slide-to="4"></li>
                          <li data-target="#myCarousel_2" data-slide-to="5"></li>
                          <li data-target="#myCarousel_2" data-slide-to="6"></li>
                          <li data-target="#myCarousel_2" data-slide-to="7"></li>
                          <li data-target="#myCarousel_2" data-slide-to="8"></li>
                          <li data-target="#myCarousel_2" data-slide-to="9"></li>
                        </ol>

                        <!-- Wrapper for slides -->
                        <div class="carousel-inner" ID="ACTIVOS2" runat="server">
                          <div class="item active">
                            <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" style="width:100%;">
                          </div>
                        </div>

                        <!-- Left and right controls -->
                        <a class="left carousel-control" href="#myCarousel_2" data-slide="prev">
                          <span class="glyphicon glyphicon-chevron-left"></span>
                          <span class="sr-only">Previous</span>
                        </a>
                        <a class="right carousel-control" href="#myCarousel_2" data-slide="next">
                          <span class="glyphicon glyphicon-chevron-right"></span>
                          <span class="sr-only">Next</span>
                        </a>
                      </div>
                    </div>
                        </div>
                    </div>
                </div>
                <div id="EMBALAJE2" class="tab-pane fade">
                <div class="embed-responsive embed-responsive-16by9">
                        <iframe id="PAUTAEMBALAJE_2" runat="server" class="embed-responsive-item" src="" allowfullscreen></iframe>
                </div>
                </div>
            </div>
          </div> 
                  <div id="REF3" class="tab-pane fade">
                    <ul class="nav nav-pills nav-justified">
                        <li class="active"><a data-toggle="pill" href="#PAUTACONTROL3">PAUTA DE CONTROL</a></li>
                        <li><a data-toggle="pill" href="#ESTANDAR3">ESTÁNDAR</a></li>
                        <li><a data-toggle="pill" href="#DEFECTOLOGIA3">DEFECTOLOGIA</a></li>
                        <li><a data-toggle="pill" href="#IMAGENES3">DEFECTOS GP12</a></li>
                        <li><a data-toggle="pill" href="#EMBALAJE3">EMBALAJE</a></li>
                    </ul>
                    <div class="row" runat="server">
                            <div class="col-lg-12">
                        <div class="table-responsive">
                            <table width="100%">  
                                <tr>
                                    <th>
                                        <button type="button" runat="server" id="DOC3" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Documentación de la referencia</button>
                                    </th>
                                    <th>
                                        <button type="button" runat="server" id="LIB3"  class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Liberación de serie</button>
                                    </th>
                                    <th>
                                        <button type="button" runat="server" id="MUR3" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Datos del muro</button>
                                         </th>
                                    <th>
                                        <button type="button" runat="server" id="FAB3" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Ficha de fabricación</button>
                                    <th>
                                </tr>
                            </table>  
                            <asp:TextBox ID="tbAlertaCalidad3" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ffff00" Font-Bold="true" Font-Size="X-Large" Visible="false">PRODUCTO PENDIENTE DE MURO DE CALIDAD</asp:TextBox>
  
                            <table style="table-layout:fixed" runat="server" visible="false">
                                <tr>
                                <th style="width:10%">
                                    <asp:TextBox ID="tbOrdenTitulo3" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="#333333" ForeColor="#cccccc">Orden:</asp:TextBox>
                                </th>
                                <td style="width:10%">
                                    <asp:TextBox ID="tbOrden3" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                                </td>
                                <th style="width:10%">
                                    <asp:TextBox ID="tbReferenciaTitulo3" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="#333333" ForeColor="#cccccc">Referencia:</asp:TextBox>
                                </th>
                                <td style="width:15%">
                                    <asp:TextBox ID="tbReferencia3" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width:40%">
                                    <asp:TextBox ID="tbNombre3" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                                </td>
                                <th style="width:10%">
                                    <asp:TextBox ID="tbMaquinaTitulo3" runat="server" Style="text-align:center; width:100%" Enabled="false"  BackColor="#333333" ForeColor="#cccccc">Máquina:</asp:TextBox>
                                </th>
                                <td style="width:5%">
                                    <asp:TextBox ID="tbMaquina3" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width:5%">
                                    <asp:TextBox ID="tbMolde3" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            </table>  
                    
                    </div>
        
                  </div>
                          </div>
                    <div class="tab-content">
                
                        <div id="PAUTACONTROL3" class="tab-pane fade in active">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="embed-responsive embed-responsive-16by9">
                                            <iframe id="PautaControl_3" runat="server" class="embed-responsive-item" src="" allowfullscreen></iframe>
                                    </div>
                                </div>
                            </div>
                        </div>
                
                        <div id="ESTANDAR3" class="tab-pane fade">
                            <div class="embed-responsive embed-responsive-16by9">
                              <div class="row">
                                <div class="col-lg-12">
                                    <iframe id="GP12_3" runat="server" class="embed-responsive-item" src="" allowfullscreen></iframe>
                                </div>
                              </div>
                            </div>
                        </div>
                        <div id="DEFECTOLOGIA3" class="tab-pane fade">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="embed-responsive embed-responsive-16by9">
                                        <iframe id= "DEFECTOS_3" runat="server" class="embed-responsive-item" src="" ></iframe> 
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="IMAGENES3" class="tab-pane fade">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                              <div id="myCarousel_3" class="carousel slide" data-ride="carousel">
                                <!-- Indicators -->
                                <ol class="carousel-indicators">
                                  <li data-target="#myCarousel_3" data-slide-to="0" class="active"></li>
                                  <li data-target="#myCarousel_3" data-slide-to="1"></li>
                                  <li data-target="#myCarousel_3" data-slide-to="2"></li>
                                  <li data-target="#myCarousel_3" data-slide-to="3"></li>
                                  <li data-target="#myCarousel_3" data-slide-to="4"></li>
                                  <li data-target="#myCarousel_3" data-slide-to="5"></li>
                                  <li data-target="#myCarousel_3" data-slide-to="6"></li>
                                  <li data-target="#myCarousel_3" data-slide-to="7"></li>
                                  <li data-target="#myCarousel_3" data-slide-to="8"></li>
                                  <li data-target="#myCarousel_3" data-slide-to="9"></li>
                                </ol>

                                <!-- Wrapper for slides -->
                                <div class="carousel-inner" ID="ACTIVOS3" runat="server">
                                  <div class="item active">
                                    <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" style="width:100%;">
                                  </div>
                                </div>

                                <!-- Left and right controls -->
                                <a class="left carousel-control" href="#myCarousel_3" data-slide="prev">
                                  <span class="glyphicon glyphicon-chevron-left"></span>
                                  <span class="sr-only">Previous</span>
                                </a>
                                <a class="right carousel-control" href="#myCarousel_3" data-slide="next">
                                  <span class="glyphicon glyphicon-chevron-right"></span>
                                  <span class="sr-only">Next</span>
                                </a>
                              </div>
                            </div>   
                                </div>
                            </div>
                        </div> 
                        <div id="EMBALAJE3" class="tab-pane fade">
                            <div class="embed-responsive embed-responsive-16by9">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <iframe id="PAUTAEMBALAJE_3" runat="server" class="embed-responsive-item" src="" allowfullscreen></iframe>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                  </div>
                  <div id="REF4" class="tab-pane fade">
                    <ul class="nav nav-pills nav-justified">
                        <li class="active"><a data-toggle="pill" href="#PAUTACONTROL4">PAUTA DE CONTROL</a></li>
                        <li><a data-toggle="pill" href="#ESTANDAR4">ESTÁNDAR</a></li>
                        <li><a data-toggle="pill" href="#DEFECTOLOGIA4">DEFECTOLOGIA</a></li>
                        <li><a data-toggle="pill" href="#IMAGENES4">DEFECTOS GP12</a></li>
                        <li><a data-toggle="pill" href="#EMBALAJE4">EMBALAJE</a></li>
                    </ul>
                  <div class="row" runat="server">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <table width="100%">  
                                <tr>
                                    <th>
                                        <button type="button" runat="server" id="DOC4" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Documentación de la referencia</button>
                                    </th>
                                    <th>
                                        <button type="button" runat="server" id="LIB4"  class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Liberación de serie</button>
                                    </th>
                                    <th>
                                        <button type="button" runat="server" id="MUR4" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Datos del muro</button>
                                    </th>
                                    <th>
                                        <button type="button" runat="server" id="FAB4" class="btn btn-default" onserverclick="RedireccionaDocumento" Style="text-align:center; width:100%">Ficha de fabricación</button>
                                    <th>
                                </tr>
                            </table>  
                            <asp:TextBox ID="tbAlertaCalidad4" runat="server" Style="text-align:center; width:100%; height:30px" Enabled="false" BackColor="#ffff00" Font-Bold="true" Font-Size="X-Large" Visible="false">PRODUCTO PENDIENTE DE MURO DE CALIDAD</asp:TextBox>
                            <table style="table-layout:fixed" runat="server" visible="false">
                                <tr>
                                <th style="width:10%">
                                    <asp:TextBox ID="tbOrdenTitulo4" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="#333333" ForeColor="#cccccc">Orden:</asp:TextBox>
                                </th>
                                <td style="width:10%">
                                    <asp:TextBox ID="tbOrden4" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                                </td>
                                <th style="width:10%">
                                    <asp:TextBox ID="tbReferenciaTitulo4" runat="server" Style="text-align:center; width:100%" Enabled="false" BackColor="#333333" ForeColor="#cccccc">Referencia:</asp:TextBox>
                                </th>
                                <td style="width:15%">
                                    <asp:TextBox ID="tbReferencia4" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width:40%">
                                    <asp:TextBox ID="tbNombre4" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                                </td>
                                <th style="width:10%">
                                    <asp:TextBox ID="tbMaquinaTitulo4" runat="server" Style="text-align:center; width:100%" Enabled="false"  BackColor="#333333" ForeColor="#cccccc">Máquina:</asp:TextBox>
                                </th>
                                <td style="width:5%">
                                    <asp:TextBox ID="tbMaquina4" runat="server" Style="text-align:center; width:100%" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width:5%">
                                    <asp:TextBox ID="tbMolde4" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            </table>                      
                    </div>
                  </div>
                  </div>
                  <div class="tab-content">
                    <div id="PAUTACONTROL4" class="tab-pane fade in active">
                        <div class="row">
                            <div class="col-lg-12">
                                 <div class="embed-responsive embed-responsive-16by9">
                                    <iframe id="PautaControl_4" runat="server" class="embed-responsive-item" src="" allowfullscreen></iframe>
                                 </div>
                            </div>
                        </div>
                   
                    </div>
                    <div id="ESTANDAR4" class="tab-pane fade">
                       <div class="row">
                            <div class="col-lg-12">
                                <div class="embed-responsive embed-responsive-16by9">
                                        <iframe id="GP12_4" runat="server" class="embed-responsive-item" src="" allowfullscreen></iframe>
                                </div>
                            </div>
                       </div>
                    </div>
                    <div id="DEFECTOLOGIA4" class="tab-pane fade">
                        <div class="row">
                            <div class="col-lg-12">
                              <div class="embed-responsive embed-responsive-16by9">
                                       <iframe id= "DEFECTOS_4" runat="server" class="embed-responsive-item" src="" ></iframe> 
                              </div>
                            </div>
                        </div>
  
                    </div>
                    <div id="IMAGENES4" class="tab-pane fade">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="container">
                          <div id="myCarousel_4" class="carousel slide" data-ride="carousel">
                            <!-- Indicators -->
                            <ol class="carousel-indicators">
                              <li data-target="#myCarousel_4" data-slide-to="0" class="active"></li>
                              <li data-target="#myCarousel_4" data-slide-to="1"></li>
                              <li data-target="#myCarousel_4" data-slide-to="2"></li>
                              <li data-target="#myCarousel_4" data-slide-to="3"></li>
                              <li data-target="#myCarousel_4" data-slide-to="4"></li>
                              <li data-target="#myCarousel_4" data-slide-to="5"></li>
                              <li data-target="#myCarousel_4" data-slide-to="6"></li>
                              <li data-target="#myCarousel_4" data-slide-to="7"></li>
                              <li data-target="#myCarousel_4" data-slide-to="8"></li>
                              <li data-target="#myCarousel_4" data-slide-to="9"></li>
                            </ol>

                            <!-- Wrapper for slides -->
                            <div class="carousel-inner" ID="ACTIVOS4" runat="server">
                              <div class="item active">
                                <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" style="width:100%;">
                              </div>
                            </div>

                            <!-- Left and right controls -->
                            <a class="left carousel-control" href="#myCarousel_4" data-slide="prev">
                              <span class="glyphicon glyphicon-chevron-left"></span>
                              <span class="sr-only">Previous</span>
                            </a>
                            <a class="right carousel-control" href="#myCarousel_4" data-slide="next">
                              <span class="glyphicon glyphicon-chevron-right"></span>
                              <span class="sr-only">Next</span>
                            </a>
                          </div>
                        </div> 
                            </div>
                        </div>
                    </div>
                    <div id="EMBALAJE4" class="tab-pane fade">
                        <div class="row"><div class="col-lg-12">
                            <div class="embed-responsive embed-responsive-16by9">
                                <iframe id="PAUTAEMBALAJE_4" runat="server" class="embed-responsive-item" src="" allowfullscreen></iframe>
                            </div>
                        </div>
                    </div>
                    </div>
                 </div>
          </div>
              </div>
          </div>
      </div>
            </div>
    </form>

</body>
</html>
