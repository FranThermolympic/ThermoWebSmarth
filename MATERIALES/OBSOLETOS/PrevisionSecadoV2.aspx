<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PrevisionSecado.aspx.cs" Inherits="ThermoWeb.MATERIALES.PrevisionSecado"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Previsión de secado</title>
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
        <script>
            function myFunction() {
                __doPostBack();
            }
        </script>
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
                        <li><a href="http://facts4-srv/thermogestion/LIBERACIONES/EstadoLiberacion.aspx" target="_blank">Liberaciones de serie</a></li>
                    </ul>
                </div>
            </div>
        </nav>

        <ul class="nav nav-pills nav-justified">
            <li class="active" id="tab0button" runat="server"><a data-toggle="pill" href="#SECADO">Previsión de secado</a></li>
            <li id="tab1button" runat="server"><a data-toggle="pill" href="#TABLAMATERIALES">Tabla de materiales</a></li>
        </ul>
        </ul>
        <div class="col-lg-12">
            <div class="tab-content">
                <div id="SECADO" class="tab-pane fade in active" runat="server">
                    <div class="col-lg-12">
                        <div class="col-lg-9"></div>
                        <div class="col-lg-3" style="text-align: right">
                            <label>Última actualización:&nbsp</label><asp:TextBox ID="FECHAACT" Font-Size="Large" runat="server" BorderColor="Transparent" BackColor="Transparent" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <br>
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <asp:GridView ID="dgv_secado" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                OnRowCommand="ContactsGridView_RowCommand" OnRowDataBound="OnRowDataBound"
                                EmptyDataText="There are no data records to display.">
                                <HeaderStyle BackColor="blue" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#ccccff" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Máq." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="X-Large" ItemStyle-ForeColor="white" ItemStyle-BackColor="#3366ff">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("MAQ") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Iniciar secado" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstadoActual" Font-Size="X-Large" runat="server" Text='<%#Eval("INICIARSECADO") %>' />
                                            <asp:Label ID="Label1" runat="server" Text='<%# "&nbsp("+Eval("FECHA")+")" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Orden" ItemStyle-Width="5%" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="X-Large">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("ORDEN") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material" ItemStyle-Width="22%">
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblReferencia" runat="server" Font-Size="X-Large" Text='<%#Eval("MATERIAL") + " - "%>' />
                                            <asp:Label ID="Label2" runat="server" Font-Size="Large" Text='<%#Eval("DESCRIPCION")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A preparar" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="X-Large">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstadoCambio" Font-Bold="true" runat="server" Text='<%#Eval("PREPARAR") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Disponible" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="X-Large">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRepiticiones" runat="server" Text='<%#Eval("REPETICIONES") %>' Visible="false" />
                                            <asp:Label ID="lblDisponible" runat="server" Text='<%#Eval("DISPONIBLE", "{0:0.00 Kg}") %>' />
                                             
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ubicación" ItemStyle-Width="10%" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("UBICACION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notas" ItemStyle-Width="22%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Large">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstadoProduccion" Font-Bold="true" runat="server" Text='<%#Eval("NOTAS") %>' />
                                            <asp:Label ID="lblConsumo" Font-Size="Smaller" runat="server" Visible="false" Text='<%#"<br />(Preparar " + Eval("SUMAMATS", "{0:0.00 Kg}") +" para los próximos 3 días)" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" Visible="false">
                                        <ItemTemplate>
                                            <%--Botones de eliminar y editar cliente...--%>
                                            <asp:Button ID="btnVer" runat="server" Text="Detalles" CssClass="btn btn-sm btn-info" CommandArgument='<%#Eval("Orden")%>' CommandName="Redirect" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div id="TABLAMATERIALES" class="tab-pane fade" runat="server">
                    <br>
                    <div class="col-lg-12">
                        <div class="col-lg-2" style="text-align: left">
                            <button id="VerTodas" runat="server" onserverclick="cargar_todas" type="button" class="btn btn-lg btn-primary" style="width: 85%; text-align: left">
                                <span class="glyphicon glyphicon-list"></span>Ver todas</button>

                        </div>
                        <div class="col-lg-7"></div>
                        <div class="col-lg-3" style="text-align: right">
                            <asp:TextBox ID="NUMMaterial" Font-Size="Large" runat="server"></asp:TextBox>
                            <button id="Button2" runat="server" onserverclick="FiltraMaterial" type="button" class="btn btn-lg btn-primary" style="text-align: left">
                                <span class="glyphicon glyphicon-search"></span>Buscar</button>
                        </div>
                    </div>

                    <div class="col-lg-12">
                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="dgv_Materiales" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                OnRowCommand="ContactsGridView_RowCommand" EmptyDataText="There are no data records to display.">
                                <HeaderStyle BackColor="blue" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#cccccc" />
                                <EditRowStyle BackColor="#cccccc" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Material" ItemStyle-Width="10%" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="X-Large">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstadoActual" runat="server" Text='<%#Eval("MATERIAL") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripción" ItemStyle-Font-Size="Large" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("LONG_DESCRIPTION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ubicación" Visible="false" ItemStyle-Font-Size="X-Large" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("SHORT_DESCRIPTION") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notas" ItemStyle-Font-Size="X-Large" ItemStyle-Width="45%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotasMAT" runat="server" Text='<%#Eval("REMARKS") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
