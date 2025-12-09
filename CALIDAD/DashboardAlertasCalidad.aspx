<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="DashboardAlertasCalidad.aspx.cs" Inherits="ThermoWeb.CALIDAD.DashboardAlertasCalidad"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tablero No Conformidades</title>
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

    <%-- <nav class="navbar navbar-inverse">
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
          </ul>
          <ul class="nav navbar-nav navbar-right">
                <li><a href="GP12FAQ.aspx"  target="_blank"><span class="glyphicon glyphicon-question-sign"> AYUDA</span></a></li>
          </ul>
        </div>
      </div>
    </nav>  --%>
       <div class="col-lg-12">
           <div class="row">
               <div class="col-lg-12">
               <div id="NC1" class="col-lg-4" style="border:solid; border-color:transparent; border-width:10px">
                   <div class="row">
                       <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC1_ID" runat="server" Style="text-align: center; background-color:red" Font-Size="Large" ForeColor="White" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="NC1_Cliente" runat="server" Style="text-align: center; border-left-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                   
                                                <th>
                                                    <asp:TextBox ID="NC1_TIPO" runat="server" Style="text-align: center; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>

                                        </table>
                                    </div>
                   </div>
                   <div class="row">
                   <div class="col-lg-6" style="background-color:forestgreen">
                      <div class="thumbnail">                        
                         <img src="" id="NC1_IMG1" style="min-height:170px;height:170px; margin-top:auto" runat="server"/>
                      </div>
                   </div>
                   <div class="col-lg-6" style="background-color:red">
                      <div id="myCarousel" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                            <li data-target="#myCarousel" data-slide-to="1" runat="server" id="CAR11"></li>
                                            <li data-target="#myCarousel" data-slide-to="2" runat="server" id="CAR12"></li>
                                            <li data-target="#myCarousel" data-slide-to="3" runat="server" id="CAR13"></li>
                                        </ol>
                                        <!-- Wrapper for slides -->
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <img id="NC1_IMG2" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto" >
                                            </div>
                                            <div class="item" runat="server" id="CAR11A">
                                                <img id="NC1_IMG3" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR12A">
                                                <img id="NC1_IMG4" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR13A">
                                                <img id="NC1_IMG5" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <%-- 
                                        <a class="left carousel-control" href="#myCarousel" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>--%>
                                    </div>
                   </div>
                   </div>
                   <div class="row">
                   <div class="table-responsive">
                       <table width="100%">
                           <tr>
                              <th colspan="2">
                                 <asp:TextBox ID="NC1Referencia" runat="server" Style="text-align: center; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                              </th>
                           </tr>
                           <tr>
                              <th colspan="2">
                                 <asp:TextBox ID="NC1DefectoReclamado" runat="server" Style="text-align: center; border-bottom-color:red; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                              </th>
                           </tr>
                       </table>
                      </div>
                   </div>
               </div>     
               <div id="NC2" class="col-lg-4" runat="server" visible="false" style="border:solid; border-color:transparent; border-width:10px">
                   <div class="row">
                       <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC2_ID" runat="server" Style="text-align: center; background-color:red" Font-Size="Large" ForeColor="White" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="NC2_Cliente" runat="server" Style="text-align: center; border-left-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="NC2_TIPO" runat="server" Style="text-align: center; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                   </div>
                   <div class="row">
                   <div class="col-lg-6" style="background-color:forestgreen">
                      <div class="thumbnail">                        
                            <img src="" id="NC2_IMG1" style="min-height:170px;height:170px; margin-top:auto" runat="server"/>
                      </div>
                   </div>
                   <div class="col-lg-6" style="background-color:red">
                       <div id="myCarousel2" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#myCarousel2" data-slide-to="0" class="active"></li>
                                            <li data-target="#myCarousel2" data-slide-to="1" runat="server" id="CAR21"></li>
                                            <li data-target="#myCarousel2" data-slide-to="2" runat="server" id="CAR22"></li>
                                            <li data-target="#myCarousel2" data-slide-to="3" runat="server" id="CAR23"></li>
                                        </ol>
                                        <!-- Wrapper for slides -->
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <img id="NC2_IMG2" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto" >
                                            </div>
                                            <div class="item" runat="server" id="CAR21A">
                                                <img id="NC2_IMG3" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR22A">
                                                <img id="NC2_IMG4" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR23A">
                                                <img id="NC2_IMG5" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <%-- 
                                        <a class="left carousel-control" href="#myCarousel2" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel2" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>--%>
                                    </div>
                   </div>
                   </div>
                   <div class="row">
                   <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC2Referencia" runat="server" Style="text-align: center; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC2DefectoReclamado" runat="server" Style="text-align: center; border-bottom-color:red; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>

                                        </table>
                                    </div>
                   </div>
               </div>
               <div id="NC3" class="col-lg-4" runat="server" visible="false" style="border:solid; border-color:transparent; border-width:10px">
                   <div class="row">
                       <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC3_ID" runat="server" Style="text-align: center; background-color:red" Font-Size="Large" ForeColor="White" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="NC3_Cliente" runat="server" Style="text-align: center; border-left-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="NC3_TIPO" runat="server" Style="text-align: center; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                   </div>
                   <div class="row">
                   <div class="col-lg-6" style="background-color:forestgreen">
                      <div class="thumbnail">                        
                            <img src="" id="NC3_IMG1" style="min-height:170px;height:170px; margin-top:auto" runat="server"/>
                      </div>
                   </div>
                   <div class="col-lg-6" style="background-color:red">
                       <div id="myCarousel3" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#myCarousel3" data-slide-to="0" class="active"></li>
                                            <li data-target="#myCarousel3" data-slide-to="1" runat="server" id="CAR31"></li>
                                            <li data-target="#myCarousel3" data-slide-to="2" runat="server" id="CAR32"></li>
                                            <li data-target="#myCarousel3" data-slide-to="3" runat="server" id="CAR33"></li>
                                        </ol>
                                        <!-- Wrapper for slides -->
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <img id="NC3_IMG2" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto" >
                                            </div>
                                            <div class="item" runat="server" id="CAR31A">
                                                <img id="NC3_IMG3" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR32A">
                                                <img id="NC3_IMG4" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR33A">
                                                <img id="NC3_IMG5" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <%-- 
                                        <a class="left carousel-control" href="#myCarousel3" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel3" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>--%>
                                    </div>
                   </div>
                   </div>
                   <div class="row">
                   <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC3Referencia" runat="server" Style="text-align: center; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC3DefectoReclamado" runat="server" Style="text-align: center; border-bottom-color:red; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                   </div>
               </div>
               </div>
           </div>
           <br />
           <div class="row">
               <div class="col-lg-12">
               <div id="NC4" class="col-lg-4" runat="server" visible="false" style="border:solid; border-color:transparent; border-width:10px">
                   <div class="row">
                       <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC4_ID" runat="server" Style="text-align: center; background-color:red" Font-Size="Large" ForeColor="White" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="NC4_Cliente" runat="server" Style="text-align: center; border-left-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                   
                                                <th>
                                                    <asp:TextBox ID="NC4_TIPO" runat="server" Style="text-align: center; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>

                                        </table>
                                    </div>
                   </div>
                   <div class="row">
                   <div class="col-lg-6" style="background-color:forestgreen">
                      <div class="thumbnail">                        
                         <img src="" id="NC4_IMG1" style="min-height:170px;height:170px; margin-top:auto" runat="server"/>
                      </div>
                   </div>
                   <div class="col-lg-6" style="background-color:red">
                      <div id="myCarousel4" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#myCarousel4" data-slide-to="0" class="active"></li>
                                            <li data-target="#myCarousel4" data-slide-to="1" runat="server" id="CAR41"></li>
                                            <li data-target="#myCarousel4" data-slide-to="2" runat="server" id="CAR42"></li>
                                            <li data-target="#myCarousel4" data-slide-to="3" runat="server" id="CAR43"></li>
                                        </ol>
                                        <!-- Wrapper for slides -->
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <img id="NC4_IMG2" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto" >
                                            </div>
                                            <div class="item" runat="server" id="CAR41A">
                                                <img id="NC4_IMG3" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR42A">
                                                <img id="NC4_IMG4" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR43A">
                                                <img id="NC4_IMG5" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <%-- 
                                        <a class="left carousel-control" href="#myCarousel4" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel4" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>--%>
                                    </div>
                   </div>
                   </div>
                   <div class="row">
                   <div class="table-responsive">
                       <table width="100%">
                           <tr>
                              <th colspan="2">
                                 <asp:TextBox ID="NC4Referencia" runat="server" Style="text-align: center; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                              </th>
                           </tr>
                           <tr>
                              <th colspan="2">
                                 <asp:TextBox ID="NC4DefectoReclamado" runat="server" Style="text-align: center; border-bottom-color:red; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                              </th>
                           </tr>
                       </table>
                      </div>
                   </div>
               </div>     
               <div id="NC5" class="col-lg-4" runat="server" visible="false" style="border:solid; border-color:transparent; border-width:10px">
                   <div class="row">
                       <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC5_ID" runat="server" Style="text-align: center; background-color:red" Font-Size="Large" ForeColor="White" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="NC5_Cliente" runat="server" Style="text-align: center; border-left-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="NC5_TIPO" runat="server" Style="text-align: center; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                   </div>
                   <div class="row">
                   <div class="col-lg-6" style="background-color:forestgreen">
                      <div class="thumbnail">                        
                            <img src="" id="NC5_IMG1" style="min-height:170px;height:170px; margin-top:auto" runat="server"/>
                      </div>
                   </div>
                   <div class="col-lg-6" style="background-color:red">
                      <div id="myCarousel5" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#myCarousel5" data-slide-to="0" class="active"></li>
                                            <li data-target="#myCarousel5" data-slide-to="1" runat="server" id="CAR51"></li>
                                            <li data-target="#myCarousel5" data-slide-to="2" runat="server" id="CAR52"></li>
                                            <li data-target="#myCarousel5" data-slide-to="3" runat="server" id="CAR53"></li>
                                        </ol>
                                        <!-- Wrapper for slides -->
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <img id="NC5_IMG2" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto" >
                                            </div>
                                            <div class="item" runat="server" id="CAR51A">
                                                <img id="NC5_IMG3" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR52A">
                                                <img id="NC5_IMG4" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR53A">
                                                <img id="NC5_IMG5" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <%-- 
                                        <a class="left carousel-control" href="#myCarousel5" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel5" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>--%>
                                    </div>
                   </div>
                   </div>
                   <div class="row">
                   <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC5Referencia" runat="server" Style="text-align: center; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC5DefectoReclamado" runat="server" Style="text-align: center; border-bottom-color:red; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>

                                        </table>
                                    </div>
                   </div>
               </div>
               <div id="NC6" class="col-lg-4" runat="server" visible="false" style="border:solid; border-color:transparent; border-width:10px">
                   <div class="row">
                       <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC6_ID" runat="server" Style="text-align: center; background-color:red" Font-Size="Large" ForeColor="White" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="NC6_Cliente" runat="server" Style="text-align: center; border-left-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="NC6_TIPO" runat="server" Style="text-align: center; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                   </div>
                   <div class="row">
                   <div class="col-lg-6" style="background-color:forestgreen">
                      <div class="thumbnail">                        
                            <img src="" id="NC6_IMG1" style="min-height:170px;height:170px; margin-top:auto" runat="server"/>
                      </div>
                   </div>
                   <div class="col-lg-6" style="background-color:red">
                       <div id="myCarousel6" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#myCarousel6" data-slide-to="0" class="active"></li>
                                            <li data-target="#myCarousel6" data-slide-to="1" runat="server" id="CAR61"></li>
                                            <li data-target="#myCarousel6" data-slide-to="2" runat="server" id="CAR62"></li>
                                            <li data-target="#myCarousel6" data-slide-to="3" runat="server" id="CAR63"></li>
                                        </ol>
                                        <!-- Wrapper for slides -->
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <img id="NC6_IMG2" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto" >
                                            </div>
                                            <div class="item" runat="server" id="CAR61A">
                                                <img id="NC6_IMG3" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR62A">
                                                <img id="NC6_IMG4" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR63A">
                                                <img id="NC6_IMG5" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <%-- 
                                        <a class="left carousel-control" href="#myCarousel6" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel6" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>--%>
                                    </div>
                   </div>
                   </div>
                   <div class="row">
                   <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC6Referencia" runat="server" Style="text-align: center; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC6DefectoReclamado" runat="server" Style="text-align: center; border-bottom-color:red; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>

                                        </table>
                                    </div>
                   </div>
               </div>
               </div>
           </div>
           <br />   
           <div class="row">
               <div class="col-lg-12">
                    <div id="NC7" class="col-lg-4" runat="server" visible="false" style="border:solid; border-color:transparent; border-width:10px">
                   <div class="row">
                       <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC7_ID" runat="server" Style="text-align: center; background-color:red" Font-Size="Large" ForeColor="White" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="NC7_Cliente" runat="server" Style="text-align: center; border-left-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="NC7_TIPO" runat="server" Style="text-align: center; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                   </div>
                   <div class="row">
                   <div class="col-lg-6" style="background-color:forestgreen">
                      <div class="thumbnail">                        
                            <img src="" id="NC7_IMG1" style="min-height:170px;height:170px; margin-top:auto" runat="server"/>
                      </div>
                   </div>
                   <div class="col-lg-6" style="background-color:red">
                       <div id="myCarousel7" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#myCarousel7" data-slide-to="0" class="active"></li>
                                            <li data-target="#myCarousel7" data-slide-to="1" runat="server" id="CAR71"></li>
                                            <li data-target="#myCarousel7" data-slide-to="2" runat="server" id="CAR72"></li>
                                            <li data-target="#myCarousel7" data-slide-to="3" runat="server" id="CAR73"></li>
                                        </ol>
                                        <!-- Wrapper for slides -->
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <img id="NC7_IMG2" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto" >
                                            </div>
                                            <div class="item" runat="server" id="CAR71A">
                                                <img id="NC7_IMG3" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR72A">
                                                <img id="NC7_IMG4" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR73A">
                                                <img id="NC7_IMG5" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <%-- 
                                        <a class="left carousel-control" href="#myCarousel7" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel7" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>--%>
                                    </div>
                   </div>
                   </div>
                   <div class="row">
                   <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC7Referencia" runat="server" Style="text-align: center; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC7DefectoReclamado" runat="server" Style="text-align: center; border-bottom-color:red; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>

                                        </table>
                                    </div>
                   </div>
               </div>
                    <div id="NC8" class="col-lg-4" runat="server" visible="false" style="border:solid; border-color:transparent; border-width:10px">
                   <div class="row">
                       <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC8_ID" runat="server" Style="text-align: center; background-color:red" Font-Size="Large" ForeColor="White" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="NC8_Cliente" runat="server" Style="text-align: center; border-left-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="NC8_TIPO" runat="server" Style="text-align: center; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                   </div>
                   <div class="row">
                   <div class="col-lg-6" style="background-color:forestgreen">
                      <div class="thumbnail">                        
                            <img src="" id="NC8_IMG1" style="min-height:170px;height:170px; margin-top:auto" runat="server"/>
                      </div>
                   </div>
                   <div class="col-lg-6" style="background-color:red">
                      <div id="myCarousel8" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#myCarousel8" data-slide-to="0" class="active"></li>
                                            <li data-target="#myCarousel8" data-slide-to="1" runat="server" id="CAR81"></li>
                                            <li data-target="#myCarousel8" data-slide-to="2" runat="server" id="CAR82"></li>
                                            <li data-target="#myCarousel8" data-slide-to="3" runat="server" id="CAR83"></li>
                                        </ol>
                                        <!-- Wrapper for slides -->
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <img id="NC8_IMG2" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto" >
                                            </div>
                                            <div class="item" runat="server" id="CAR81A">
                                                <img id="NC8_IMG3" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR82A">
                                                <img id="NC8_IMG4" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR83A">
                                                <img id="NC8_IMG5" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <%-- 
                                        <a class="left carousel-control" href="#myCarousel8" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel8" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>--%>
                                    </div>
                   </div>
                   </div>
                   <div class="row">
                   <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC8Referencia" runat="server" Style="text-align: center; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC8DefectoReclamado" runat="server" Style="text-align: center; border-bottom-color:red; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>

                                        </table>
                                    </div>
                   </div>
               </div>
                    <div id="NC9" class="col-lg-4" runat="server" visible="false" style="border:solid; border-color:transparent; border-width:10px">
                   <div class="row">
                       <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC9_ID" runat="server" Style="text-align: center; background-color:red" Font-Size="Large" ForeColor="White" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <asp:TextBox ID="NC9_Cliente" runat="server" Style="text-align: center; border-left-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="NC9_TIPO" runat="server" Style="text-align: center; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                   </div>
                   <div class="row">
                   <div class="col-lg-6" style="background-color:forestgreen">
                      <div class="thumbnail">                        
                            <img src="" id="NC9_IMG1" style="min-height:170px;height:170px; margin-top:auto" runat="server"/>
                      </div>
                   </div>
                   <div class="col-lg-6" style="background-color:red">
                    <div id="myCarousel9" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <ol class="carousel-indicators">
                                            <li data-target="#myCarousel9" data-slide-to="0" class="active"></li>
                                            <li data-target="#myCarousel9" data-slide-to="1" runat="server" id="CAR91"></li>
                                            <li data-target="#myCarousel9" data-slide-to="2" runat="server" id="CAR92"></li>
                                            <li data-target="#myCarousel9" data-slide-to="3" runat="server" id="CAR93"></li>
                                        </ol>
                                        <!-- Wrapper for slides -->
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <img id="NC9_IMG2" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto" >
                                            </div>
                                            <div class="item" runat="server" id="CAR91A">
                                                <img id="NC9_IMG3" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR92A">
                                                <img id="NC9_IMG4" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                            <div class="item" runat="server" id="CAR93A">
                                                <img id="NC9_IMG5" runat="server" src="" alt="" style=" max-height: 200px;
                                                    height: 100%; border-color:red; margin:auto">
                                            </div>
                                        </div>
                                        <!-- Left and right controls -->
                                        <%-- 
                                        <a class="left carousel-control" href="#myCarousel9" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">
                                        </span><span class="sr-only">Previous</span> </a><a class="right carousel-control"
                                            href="#myCarousel9" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                                            </span><span class="sr-only">Next</span> </a>--%>
                                    </div>
                   </div>
                   </div>
                   <div class="row">
                   <div class="table-responsive">
                                        <table width="100%">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC9Referencia" runat="server" Style="text-align: center; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                    <asp:TextBox ID="NC9DefectoReclamado" runat="server" Style="text-align: center; border-bottom-color:red; border-left-color:red; border-right-color:red" Enabled="false" Width="100%"></asp:TextBox>
                                                </th>
                                            </tr>

                                        </table>
                                    </div>
                   </div>
               </div>
               </div>
           </div>

       </div>   
    </form>
</body>
</html>
