<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KPIAprovechados.aspx.cs" Inherits="ThermoWeb.KPI.KPIAprovechados"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Histórico de revisiones</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script src="js/json2.js" type="text/javascript"></script>
    <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.Add-text').datepicker({
                dateFormat: 'yy-mm-dd',
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
    <script type="text/javascript">
        function valorNOK() {
            alert("Debes seleccionar una fecha y un turno para poder ver los datos.");
        }
    </script>
    <script type="text/javascript">
        function valorVACIO() {
            alert("No hay datos en ese turno.");
        }
    </script>
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
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="GP12FAQ.aspx"><span class="glyphicon glyphicon-question-sign">AYUDA</span></a></li>
                    </ul>
                </div>
            </div>
        </nav>

        <ul class="nav nav-pills nav-justified">
            <li class="active" id="tab0button" runat="server"><a data-toggle="pill" href="#TURNO">VER POR TURNO</a></li>
            <li id="tab1button" runat="server"><a data-toggle="pill" href="#ANUAL">ACUMULADOS ANUALES</a></li>

        </ul>
        <div class="tab-content">
            <%-- Abro panel de MENSUAL--%>
            <div id="TURNO" class="tab-pane fade in active" runat="server">
                <div class="row">
                    <br />
                    <div class="col-lg-12" style="background-color: lemonchiffon">
                        <div class="col-lg-4">
                            <asp:TextBox ID="tbEncargado1" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center; background: none; border: none" />
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="tbEncargado2" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center; background: none; border: none" />
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="tbCalidad" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center; background: none; border: none" />
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="panel-body">
                            <div class="col-lg-3" style="text-align: right">
                                <label for="usr" style="text-align: right">Media logueados:</label><br />
                                <asp:TextBox ID="tbLogueados" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center" />
                            </div>
                            <div class="col-lg-3" style="text-align: right">
                                <label for="usr" style="text-align: right">Media asignados:</label><br />
                                <asp:TextBox ID="tbAsignados" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center" />
                            </div>
                            <div class="col-lg-3" style="text-align: right">
                                <label for="usr">Media aprovechados:</label><br />
                                <asp:TextBox ID="tbAprovechados" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center" />
                            </div>
                            <div class="col-lg-3" style="text-align: right">
                                <label for="usr">Media libres:</label><br />
                                <asp:TextBox ID="tbLibres" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-lg-1">
                                        <label for="usr">Fecha:</label>
                                        <asp:TextBox ID="selectFecha" runat="server" CssClass="textbox Add-text" Width="100%" Height="33px" autocomplete="off" />
                                    </div>
                                    <div class="col-lg-3">
                                        <label for="usr">Turno:</label><br />
                                        <div class="o-switch btn-group" data-toggle="buttons" role="group" style="width: 100%">
                                            <label id="TMañana" class="btn btn-md btn-primary" runat="server" style="width: 33%">
                                                <input type="radio" name="options15" id="INTMañana" runat="server" autocomplete="off">MAÑANA
                                            </label>
                                            <label id="Ttarde" class="btn btn-md btn-primary" runat="server" style="width: 33%">
                                                <input type="radio" name="options15" id="INTTarde" runat="server" autocomplete="off">TARDE
                                            </label>
                                            <label id="TNoche" class="btn btn-md btn-primary" runat="server" style="width: 33%">
                                                <input type="radio" name="options15" id="INTNoche" runat="server" autocomplete="off">NOCHE
                                            </label>
                                        </div>
                                    </div>
                                    <%-- 
                            <div class="col-lg-1">
                                <label for="usr">Fecha Final:</label>
                                        <asp:TextBox ID="selectFecha2" runat="server" CssClass="textbox Add-text" Width="100%" Height="35px" autocomplete="off" />                                                            
                            </div>
                                    --%>
                                    <div class="col-lg-2">
                                    </div>
                                    <div class="col-lg-4">
                                    </div>
                                    <div class="col-lg-2 text-right">
                                        <br />
                                        <button id="Button2" runat="server" onserverclick="Cargar_Filtrados" type="button" class="btn btn-md btn-info" style="width: 75%; text-align: left">
                                            <span class="glyphicon glyphicon-search"></span>Filtrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <asp:GridView ID="dgv_Aprovechamiento" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                            EmptyDataText="No hay revisiones para mostrar.">
                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#ffffcc" />
                            <Columns>
                                <asp:TemplateField HeaderText="Fecha">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFecharevision" runat="server" Text='<%#Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Logueados">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLogueados" runat="server" Text='<%#Eval("OPLOGUEADOS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asignados">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAsignados" runat="server" Text='<%#Eval("OPASIGNADOS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aprovechados">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAprovechados" runat="server" Text='<%#Eval("OPAPROVECHADOS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Libres">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLibres" runat="server" Text='<%#Eval("OPLIBRES") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%-- --%>
            <div id="ANUAL" class="tab-pane fade" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-3">
                                    <br />
                                    <label for="usr">Periodo:</label>
                                    <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="Cargar_anuales">
                                        <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                        <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                        <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                        <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                        <asp:ListItem Text="2019" Value="2019"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-5">
                                </div>
                                <div class="col-lg-4 text-right">
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel-body">
                                    <div class="col-lg-3" style="text-align: right">
                                        <label for="usr" style="text-align: right">Media logueados:</label><br />
                                        <asp:TextBox ID="tbLogueadosAÑO" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center" />
                                    </div>
                                    <div class="col-lg-3" style="text-align: right">
                                        <label for="usr">Media asignados:</label><br />
                                        <asp:TextBox ID="tbAsignadosAÑO" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center" />
                                    </div>
                                    <div class="col-lg-3" style="text-align: right">
                                        <label for="usr" style="text-align: right">Media aprovechados:</label><br />
                                        <asp:TextBox ID="tbAprovechadosAÑO" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center" />
                                    </div>

                                    <div class="col-lg-3" style="text-align: right">
                                        <label for="usr">Media Libres:</label><br />
                                        <asp:TextBox ID="tbLibresAÑO" runat="server" Width="100%" Height="45px" autocomplete="off" Enabled="false" Font-Size="XX-Large" Style="text-align: center" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-8">
                            <h3>&nbsp;&nbsp;&nbsp; Informe de aprovechamiento por semana </h3>
                            <div class="table-responsive">
                                <asp:GridView ID="dgv_AprovechamientoSEMANAMTM" OnRowDataBound="GridView1_DataBound" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                    EmptyDataText="No hay revisiones para mostrar.">
                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#ffffcc" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Semana" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFecharevision" runat="server" Font-Size="Large" Text='<%#Eval("SEMANA") %>' />
                                                <asp:Label ID="lblInicio" runat="server" Font-Size="Small" Text='<%#"("+Eval("INICIO", "{0:dd/MM/yyyy}")+")" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Log. M">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLogueadosM" runat="server" Text='<%#Eval("MANOPLOGUEADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Asig. M">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAsignadosM" runat="server" Text='<%#Eval("MANOPASIGNADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aprov. M">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAprovechadosM" runat="server" Text='<%#Eval("MANOPAPROVECHADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Libres M" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblLibresM" runat="server" Text='<%#Eval("MANOPLIBRES", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Log. T">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLogueadosT" runat="server" Text='<%#Eval("TAROPLOGUEADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Asig. T">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAsignadosT" runat="server" Text='<%#Eval("TAROPASIGNADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aprov. T">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAprovechadosT" runat="server" Text='<%#Eval("TAROPAPROVECHADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Libres T" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLibresT" runat="server" Text='<%#Eval("TAROPLIBRES", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Log. N">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLogueadosN" runat="server" Text='<%#Eval("NOCOPLOGUEADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Asig. N">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAsignadosN" runat="server" Text='<%#Eval("NOCOPASIGNADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aprov. N">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAprovechadosN" runat="server" Text='<%#Eval("NOCOPAPROVECHADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Libres N" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLibresN" runat="server" Text='<%#Eval("NOCOPLIBRES", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="dgv_AprovechamientoSEMANA" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                    EmptyDataText="No hay revisiones para mostrar.">
                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#ffffcc" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Semana" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFecharevision" runat="server" Text='<%#Eval("SEMANA") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inicio">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInicio" runat="server" Text='<%#Eval("INICIO", "{0:dd/MM/yyyy}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Logueados">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLogueados" runat="server" Text='<%#Eval("OPLOGUEADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Asignados">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAsignados" runat="server" Text='<%#Eval("OPASIGNADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aprovechados">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAprovechados" runat="server" Text='<%#Eval("OPAPROVECHADOS", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Libres" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLibres" runat="server" Text='<%#Eval("OPLIBRES", "{0:0.00}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <h3>&nbsp;&nbsp;&nbsp; Informe de aprovechamiento por mes </h3>
                            <asp:GridView ID="dgv_AprovechamientoMES" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                EmptyDataText="No hay revisiones para mostrar.">
                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Mes" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFecharevision" runat="server" Text='<%#Eval("INICIO", "{0:MMMM}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Logueados">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLogueados" runat="server" Text='<%#Eval("OPLOGUEADOS", "{0:0.00}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asignados">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAsignados" runat="server" Text='<%#Eval("OPASIGNADOS", "{0:0.00}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aprovechados">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAprovechados" runat="server" Text='<%#Eval("OPAPROVECHADOS", "{0:0.00}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Libres" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLibres" runat="server" Text='<%#Eval("OPLIBRES", "{0:0.00}") %>' />
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script src="js/json2.js" type="text/javascript"></script>
    <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</body>
</html>
