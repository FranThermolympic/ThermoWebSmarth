<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPI_NoConformidades.aspx.cs"
    Inherits="ThermoWeb.CALIDAD.KPI_NoConformidades" EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>KPI - Muro de calidad</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.css">
        <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.min.js"></script>
    
    
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
                <li><a href="Alertas_Calidad.aspx" target="_blank">Nueva alerta</a></li>
                <li><a href="ListaAlertasCalidad.aspx" target="_blank">Lista de alertas</a></li>
                <li><a href="DashboardAlertasCalidad.aspx" target="_blank">Dashboard</a></li>
                <li><a href="KPI_NoConformidades.aspx" target="_blank">Indicadores</a></li>
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
                                <asp:listitem text="2021" Value="2021"></asp:listitem>   
                        </asp:DropDownList> 
                </div>
                </div>
             
  
    <div class="row">
            <h2>&nbsp;&nbsp;&nbsp; No Conformidades</h2>
                <div class="col-lg-12">            
                <div class="table-responsive">
            <asp:GridView ID="dgv_KPI_Mensual" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" ShowFooter="true"
            EmptyDataText="No hay datos para mostrar.">
            <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
             --%>
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <FooterStyle Font-Bold="True" ForeColor="Black" />
            <Columns>
                <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>
                
                <asp:TemplateField HeaderText="Mes" HeaderStyle-Width="10%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#9999ff" >
                    <ItemTemplate>
                        <asp:Label ID="lblMes" runat="server" Text='<%#Eval("MES") %>'  Font-Bold="true" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="footmes1" runat="server" Text="Media acum." /><hr>
                        <asp:Label ID="footmes1suma" runat="server" Text="Total"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NC Oficiales" HeaderStyle-Width="10%"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="Oficiales" runat="server" Text='<%#Eval("NCOFICIALES") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGOficiales" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalOficiales" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Objetivo" HeaderStyle-Width="10%" FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="ObjetivoOF" runat="server" Text='< 4' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="ObjetivoOFfoot" runat="server" Text='< 4' /><hr>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mes" HeaderStyle-Width="10%"  ItemStyle-BackColor="#ccccff"  FooterStyle-BackColor="#9999ff">
                    <ItemTemplate>
                        <asp:Label ID="lblMes2" runat="server" Text='<%#Eval("MES") %>'  Font-Bold="true" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="footmes2" runat="server" Text="Media acum." /><hr>
                        <asp:Label ID="footmes2suma" runat="server" Text="Total"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Q-Info" FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="QINFO" runat="server" Text='<%#Eval("QINFO") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGQInfo" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="SumaQInfo" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A proveedor"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="Aproveedor" runat="server" Text='<%#Eval("PROVEEDOR") %>'  />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGOProveedor" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="SumaProveedor" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mes" HeaderStyle-Width="10%"  ItemStyle-BackColor="#ccccff"  FooterStyle-BackColor="#9999ff">
                    <ItemTemplate>
                        <asp:Label ID="lblMes3" runat="server" Text='<%#Eval("MES") %>'  Font-Bold="true" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="footmes3" runat="server" Text="Media Acum." /><hr>
                        <asp:Label ID="footmes3total" runat="server" Text="Total" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pz. Enviadas"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="Enviadas" runat="server" Text='<%#Eval("CantidadEnviada","{0:#,0}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGEnviada" runat="server" Text="100" Font-Bold="true"/><hr>
                        <asp:Label ID="SUMEnviada" runat="server" Text="100" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Pz. Imputadas"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="NOK" runat="server" Text='<%#Eval("PIEZASNOK","{0:#,0}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGNOK" runat="server" Text="100" Font-Bold="true"/><hr>
                        <asp:Label ID="SUMNOK" runat="server" Text="100" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="PPM"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="PPM" runat="server" Text='<%#Eval("PPM","{0:0.##}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <br />
                        <asp:Label ID="AVGPPM" runat="server" Text="100" Font-Bold="true" />
                    </FooterTemplate>
                </asp:TemplateField>                
            </Columns>
        </asp:GridView>
   
    </div>
     </div>
</div>          
    <div class="row">
            <h2>&nbsp;&nbsp;&nbsp; Costes de No Calidad y chatarras</h2>
                <div class="col-lg-12">            
                <div class="table-responsive">
            <asp:GridView ID="dgv_CostesNoCalidad" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" ShowFooter="true"
            EmptyDataText="No hay datos para mostrar.">
            <%-- OnRowUpdating="GridView_RowUpdating" "table table-striped table-bordered table-hover OnRowCommand="gridView_RowCommand""
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing"
             --%>
            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#ffffcc" />
            <FooterStyle Font-Bold="True" ForeColor="Black" />
            <Columns>
                <%-- <asp:BoundField DataField="CodMolde" HeaderText="Molde" ReadOnly="True" SortExpression="Molde" />--%>
                
                <asp:TemplateField HeaderText="Mes" HeaderStyle-Width="10%" ItemStyle-BackColor="#ccccff"  FooterStyle-BackColor="#9999ff" >
                    <ItemTemplate>
                        <asp:Label ID="lblMescostes" runat="server" Text='<%#Eval("MES") %>'  Font-Bold="true" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="footmes1costes" runat="server" Text="Media acum." /><hr>
                        <asp:Label ID="footmescos1costes" runat="server" Text="Total"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NC - Selecciones" HeaderStyle-Width="8%"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="NCSelExternas" runat="server" Text='<%#Eval("SELECCIONEXT","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGNCSelExternas" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalNCSelExternas" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NC - Chatarras" HeaderStyle-Width="8%"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="NCChatarras" runat="server" Text='<%#Eval("CHATARRAEXT","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGNCChatarras" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalNCChatarras" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NC - Cargos" HeaderStyle-Width="8%"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="NCCargos" runat="server" Text='<%#Eval("CARGOSEXT","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGNCCargos" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalNCCargos" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NC - Cost. Admón." HeaderStyle-Width="8%"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="NCCostAdmon" runat="server" Text='<%#Eval("ADMONEXT","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGNCCostAdmon" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalNCCostAdmon" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NC - Otros" HeaderStyle-Width="8%"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="NCOtros" runat="server" Text='<%#Eval("OTROSINT","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGNCOtros" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalNCOtros" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GP12" HeaderStyle-Width="8%"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="CosteGP12" runat="server" Text='<%#Eval("GP12","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGCosteGP12" runat="server" Text="0" Font-Bold="true" /><hr>
                        <asp:Label ID="TotalCosteGP12" runat="server" Text="0" DataFormatString="{0:c}" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Chatarra" HeaderStyle-Width="8%"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="CosteChatarra" runat="server" Text='<%#Eval("CHATARRA","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGCosteChatarra" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalCosteChatarra" runat="server" Text="0" Font-Bold="true"/>

                    </FooterTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Costes No Calidad" HeaderStyle-Width="13%"  ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#ccccff">
                    <ItemTemplate>
                        <asp:Label ID="CosteNoCalidad" runat="server"  Font-Bold="true" Text='<%#Eval("CosteNoCalidad","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGCosteNoCalidad" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalCosteNoCalidad" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Arranques" HeaderStyle-Width="10%"  FooterStyle-BackColor="#EFEFEF">
                    <ItemTemplate>
                        <asp:Label ID="CosteArranques" runat="server" Text='<%#Eval("ARRANQUE","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGCosteArranques" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalCosteArranques" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Coste Total" HeaderStyle-Width="15%"  ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#ccccff">
                    <ItemTemplate>
                        <asp:Label ID="CosteTotal"  Font-Bold="true" runat="server" Text='<%#Eval("CosteTotal","{0:c}") %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="AVGCosteCosteTotal" runat="server" Text="0" Font-Bold="true"/><hr>
                        <asp:Label ID="TotalCosteTotal" runat="server" Text="0" Font-Bold="true"/>
                    </FooterTemplate>
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

    </form>
                <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.css">
        <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.min.js"></script>
      
</body>
</html>
