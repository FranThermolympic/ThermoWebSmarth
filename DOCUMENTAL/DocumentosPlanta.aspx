<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="DocumentosPlanta.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.DocumentosPlanta" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Documentos de producción</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp-
    <label id="LabelNumMaquina" runat="server">Máquina XX</label>
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">

    <ul class="navbar-nav me-auto mb-2 mb-lg-0">

        <li><a href="#" class="nav-link active" id="navbarrecarga" runat="server" onserverclick="lanzaPostback">Recargar con orden actual</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript"><%--Scripts--%>
        function ShowPopupIncrustados() {
            document.getElementById("btnPopIncrustados").click();
        }
        function ShowPopupEstructura() {
            document.getElementById("btnPopEstructura").click();
        }
        function ShowPopupExperiencia() {
            document.getElementById("btnPopExperiencia").click();
        }
        function ShowPopupDocumentacion() {
            document.getElementById("btnPopDocumentacion").click();
        }
        function ShowPopDocVinculados() {
            document.getElementById("btnPopDocVinculados").click();
        }
        function ClosePopupDocumentacion() {
            document.getElementById("btnDismissModal").click();
        }
        function ClosePopupDocumentacion2() {
            document.getElementById("btnDismissModal2").click();
        }
        function CierraAviso() {
            $.ajax({
                type: "POST",
                url: "DocumentosPlanta.aspx/CerrarAvisoDocumentacionWeb",
                data: "{Avisolog: 'Test'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    ClosePopupDocumentacion2();
                },
                failure: function (response) {
                    ClosePopupDocumentacion2();
                }
            });
        }

        function ModoAraven()
        {
            document.getElementById("BTNREF1EST").click();
            document.getElementById("BTNREF1PTC").style.display = "none";
            document.getElementById("BTNREF1EMB").style.display = "none";

            document.getElementById("BTNREF2EST").click();
            document.getElementById("BTNREF2PTC").style.display = "none";
            document.getElementById("BTNREF2EMB").style.display = "none";

            document.getElementById("BTNREF3EST").click();
            document.getElementById("BTNREF3PTC").style.display = "none";
            document.getElementById("BTNREF3EMB").style.display = "none";

            document.getElementById("BTNREF4EST").click();
            document.getElementById("BTNREF4PTC").style.display = "none";
            document.getElementById("BTNREF4EMB").style.display = "none";

           
        }
        
        function EnviarFeedback() {
            $.ajax({
                type: "POST",
                url: "DocumentosPlanta.aspx/InsertarFeedbackDocumentalWeb",
                data: "{operario1: '" + document.getElementById("Operario1Numero").value + "', operario2: '" + document.getElementById("Operario2Numero").value + "', Referencia: '" + document.getElementById("ref1labtext").innerText + "', Molde: '" + document.getElementById("NumeroMolde").value + "', InputFeedback: '" + document.getElementById("tbDenunciaError").value +"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert("Vamos");
                    document.getElementById("tbDenunciaError").value = '';
                    alert("Feedback enviado correctamente. Gracias por informar.")
                },
                failure: function (response) {
                    //alert("NoVamos");
                    document.getElementById("tbDenunciaError").value = '';
                }
            });
        }
        $(document).on("click", "[id*=POL]", function () {
            document.getElementById("btnPopExperiencia").click();
        });
    </script>
    <script type="text/javascript"><%--Configurar onloads--%>
        window.onload = function () {
            document.getElementById("BTNCerrarAviso").onclick = function fun() {
                //alert("ENTRA");
                CierraAviso();
                //validation code to see State field is mandatory.  
            }
            document.getElementById("BtnReportar").onclick = function fun2() {
                EnviarFeedback();
            }
        }
    </script>
    <asp:HiddenField ID="Operario1Numero" runat="server" Value="---" />
    <asp:HiddenField ID="Operario2Numero" runat="server" Value="---" />
    <asp:HiddenField ID="NumeroMolde" runat="server" Value="0" />
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid">
            <div class="row bg-warning">
                <asp:Label ID="tbAlertaCalidad1" CssClass="mb-2" runat="server" Style="text-align: center; width: 100%; height: 30px" Visible="false" Font-Bold="true" Font-Size="X-Large"><i id="INAlertaCalidad1" runat="server" class="bi bi-exclamation-triangle-fill">&nbsp PRODUCTO PENDIENTE DE MURO DE CALIDAD</i></asp:Label>
            </div>
            <div class="d-flex align-items-start">
                <ul class="nav flex-column nav-pills me-3 mt-1 " id="pills-tab" role="tablist">
                    <li class="nav-item " role="presentation">
                        <button class="nav-link shadow  border border-secondary active" id="Button2" runat="server" data-bs-toggle="pill" data-bs-target="#ORDEN" type="button" role="tab" aria-controls="pills-home" aria-selected="true" style="font-weight: bold; width: 100%">MÁQUINA</button>
                    </li>
                    <li class="nav-item " role="presentation">
                        <label enabled="false" class="mt-2" type="button" style="font-weight: bold; width: 100%"><i class="bi bi-bookmarks">DOCUMENTOS</i></label>
                    </li>
                    <li class="nav-item" role="presentation" id="ref1lab" runat="server">
                        <button class="nav-link  shadow  border border-dark ms-1 me-1" style="font-weight: bold; width: 95%" id="ref1labtext" runat="server" data-bs-toggle="pill" data-bs-target="#REF1" type="button" role="tab" aria-controls="pills-home" aria-selected="true">---</button>
                    </li>
                    <li class="nav-item" role="presentation" id="ref2lab" runat="server" visible="false">
                        <button class="nav-link shadow  border border-dark ms-1 me-1" style="font-weight: bold; width: 95%" id="ref2labtext" runat="server" data-bs-toggle="pill" data-bs-target="#REF2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">---</button>
                    </li>
                    <li class="nav-item" role="presentation" id="ref3lab" runat="server" visible="false">
                        <button class="nav-link shadow  border border-dark ms-1 me-1" style="font-weight: bold; width: 95%" id="ref3labtext" runat="server" data-bs-toggle="pill" data-bs-target="#REF3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">---</button>
                    </li>
                    <li class="nav-item" role="presentation" id="ref4lab" runat="server" visible="false">
                        <button class="nav-link shadow  border border-dark ms-1 me-1" style="font-weight: bold; width: 95%" id="ref4labtext" runat="server" data-bs-toggle="pill" data-bs-target="#REF4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">---</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <asp:Image ID="CLIENTE" runat="server" CssClass="mt-2" ImageUrl="" Width="130px" />
                    </li>
                </ul>
                <div class="tab-content">
                    <div id="ORDEN" class="tab-pane fade show active" runat="server">
                        <div class="row">
                            <div class="col-lg-6">
                                <ul class="nav nav-pills mt-1" id="pills-tab2" role="tablist">
                                    <li class="nav-item" role="presentation" id="Li2" runat="server">
                                        <button class="nav-link shadow  border border-dark" id="btn_DU11_3" runat="server" data-bs-toggle="pill" data-bs-target="#DU11_3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold" visible="false">DU11_3</button>
                                    </li>
                                    <li class="nav-item " role="presentation" id="Li1" runat="server">
                                        <button class="nav-link shadow  border border-dark " id="btn_DU11_2" runat="server" data-bs-toggle="pill" data-bs-target="#DU11_2" type="button" role="tab" aria-controls="pills-home" aria-selected="true" style="font-weight: bold" visible="false">DU11_2</button>
                                    </li>
                                    <li class="nav-item " role="presentation">
                                        <button class="nav-link shadow  border border-secondary active " id="btn_DU11_1" runat="server" data-bs-toggle="pill" data-bs-target="#DU11_1" type="button" role="tab" aria-controls="pills-home" aria-selected="true" style="font-weight: bold">DU11_1</button>
                                    </li>
                                </ul>

                                <div class="tab-content">
                                    <div id="DU11_1" class="tab-pane fade show active" runat="server">

                                        <div class="embed-responsive embed-responsive-16by9 ">
                                            <iframe id="IframeDU11_1" class="shadow shadow-lg" runat="server" src="..\SMARTH_fonts\INTERNOS\NODU11s.PNG" frameborder="0" style="height: 90%; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" lang="es"></iframe>
                                        </div>
                                    </div>
                                    <div id="DU11_2" class="tab-pane fade" runat="server">
                                        <div class="embed-responsive embed-responsive-16by9">
                                            <iframe id="IframeDU11_2" class="shadow shadow-lg" runat="server" src="..\SMARTH_fonts\INTERNOS\NODU11s.PNG" frameborder="0" style="height: 90%; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" lang="es"></iframe>
                                        </div>
                                    </div>
                                    <div id="DU11_3" class="tab-pane fade" runat="server">
                                        <div class="embed-responsive embed-responsive-16by9">
                                            <iframe id="IframeDU11_3" class="shadow shadow-lg" runat="server" src="..\SMARTH_fonts\INTERNOS\NODU11s.PNG" frameborder="0" style="height: 90%; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" lang="es"></iframe>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6" style="height: 600px">
                                <div class="col-lg-12" runat="server" visible=" false">
                                    <h4>Datos generales</h4>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed" runat="server">
                                            <tr>
                                                <th runat="server" visible="false">
                                                    <asp:TextBox ID="tbfechaMod" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff"></asp:TextBox>
                                                </th>
                                                <th runat="server" visible="false">
                                                    <asp:TextBox ID="RazonMod" runat="server" Style="text-align: center; width: 100%; height: 30px" Enabled="false" BackColor="#ff6600" ForeColor="#ffffff"></asp:TextBox>
                                                </th>
                                               
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-lg-12 mt-4">
                                    <h4>Referencias produciendo</h4>
                                    <asp:GridView ID="dgv_Ordenes" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                                        EmptyDataText="No hay datos para mostrar.">
                                        <HeaderStyle BackColor="orange" Font-Bold="True" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="6%" ItemStyle-BackColor="#ccccff" ItemStyle-HorizontalAlign="Center" Visible="FALSE">
                                                <ItemTemplate>
                                                    <div class="btn-group" role="group" aria-label="Basic example">
                                                        <asp:LinkButton ID="btnDetallesOrden" CssClass="btn btn-outline-dark " Font-Size="Large" runat="server"> <i class="bi bi-file-post"></i></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ORDEN" HeaderStyle-Width="19%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#ccccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrden" runat="server" Text='<%#Eval("C_ID") %>' Font-Size="Large" Font-Bold="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REFERENCIA" HeaderStyle-Width="65%" FooterStyle-BackColor="#EFEFEF">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("C_PRODUCT_ID") %>' />
                                                    <asp:Label ID="lblDescripción" runat="server" Text='<%#" " + Eval("C_PRODLONGDESCR") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-lg-12">
                                    <h4>Personal trabajando</h4>
                                    
                                    <asp:GridView ID="dgv_Personal" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                                        EmptyDataText="No hay datos para mostrar.">
                                        <HeaderStyle BackColor="orange" Font-Bold="True" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="20%" ItemStyle-BackColor="#ccccff" FooterStyle-BackColor="#ccccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipo" runat="server" Text='<%#Eval("TIPOOPE")%>' Font-Size="Large" Font-Bold="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="55%" HeaderText="NOMBRE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumOP" runat="server" Text='<%#Eval("NUMOP") %>' Font-Size="Large" Font-Bold="true" />
                                                    <asp:Label ID="lblNomOP" runat="server" Text='<%#Eval("NOMBREOP") %>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="15%" HeaderText="EXPERIENCIA" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipoOP" runat="server" Text='<%#Eval("TIEMPOHORAS") + " h."%>' Font-Bold="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="NIVEL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNivelOP" runat="server" Text='<%#Eval("NIVELOPE")%>' Font-Size="Large" Font-Bold="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <h4>Reportar errores en documentación</h4>
                                    <div class="input-group shadow shadow-lg mt-2">
                                        <textarea ID="tbDenunciaError" runat="server" class="form-control" Style="text-align: center; width: 85%; height: 60px" rows="2"></textarea>
                                        <button id="BtnReportar" runat="server" type="button" class="btn btn-lg btn-primary" style="height: 60px; width: 15%; text-align: center">
                                            <i class="bi bi-send-fill"></i>
                                        </button>
                                    </div>
                                    <div class="table-responsive invisible">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 100%">
                                                    <asp:TextBox ID="TituloPosicion" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Posición</asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                    <button type="button" id="btnPopIncrustados" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopIncrustados"></button>
                                    <button type="button" id="btnPopEstructura" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopEstructura"></button>
                                    <button type="button" id="btnPopExperiencia" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopPolivalencia"></button>
                                    <button type="button" id="btnPopDocumentacion" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopNuevaDocumentacion"></button>
                                    <button type="button" id="btnDismissModal" class="btn btn-primary  invisible" data-bs-dismiss="modal">Cerrar</button>
                                    <button type="button" id="btnDismissModal2" class="btn btn-primary  invisible" data-bs-dismiss="modal" data-bs-target="#PopNuevaDocumentacion">Cerrar</button>
                                    <button type="button" id="btnPopDocVinculados" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopDocVinculados"></button>

                                </div>
                                <div class="col-lg-12">
                                </div>
                                <div class="row bg-warning border border-1 invisible">
                                    <asp:Label ID="LblLiberacion" CssClass="mb-2" runat="server" Style="text-align: center; width: 100%; height: 30px" Font-Bold="true" Font-Size="X-Large"><i id="I1" runat="server" class="bi bi-exclamation-triangle-fill">&nbsp PROCESO PENDIENTE DE LIBERAR</i></asp:Label>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                    <div id="REF1" class="tab-pane fade">
                        <div class="row">
                            <ul class="nav nav-pills nav-fill mt-1 bg-white" role="tablist">
                                <li class="nav-item" role="presentation" id="BTNREF1PTC">
                                    <button class="nav-link shadow  border border-secondary active"  data-bs-toggle="pill" data-bs-target="#PAUTACONTROL" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">PAUTA DE CONTROL</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" id="BTNREF1EST" data-bs-toggle="pill" data-bs-target="#ESTANDAR" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ESTÁNDAR</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#DEFECTOLOGIA" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOLOGIA</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#IMAGENES" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOS GP12</button>
                                </li>
                                <li class="nav-item" role="presentation" id="BTNREF1EMB">
                                    <button class="nav-link shadow border border-secondary"  data-bs-toggle="pill" data-bs-target="#EMBALAJE" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">EMBALAJE</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#DOCOTROS" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">OTROS</button>
                                </li>
                            </ul>
                            <div class="nav nav-pills nav-fill" runat="server" id="NUM1" role="tablist" style="background-color: #EFEFEF">
                                <button type="button" runat="server" id="MUR1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Datos del muro</button>
                                <button type="button" runat="server" id="DOC1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Documentación de la referencia</button>
                                <button type="button" runat="server" id="EST1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Estructura de materiales</button>
                                <button type="button" runat="server" id="FAB1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Ficha de fabricación</button>
                                <button type="button" runat="server" id="LIB1" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Liberación de serie</button>
                                <button type="button" runat="server" id="POL1" class="btn btn-outline-dark shadow shadow-sm" style="text-align: center; width: 16%; font-weight: bold">Polivalencia</button>
                            </div>
                            <div class="tab-content">
                                <div id="PAUTACONTROL" class="tab-pane fade show active ">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <iframe id="PautaControl_1" runat="server" src="tbd" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                        </div>
                                    </div>
                                </div>
                                <div id="ESTANDAR" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class='embed-container'>
                                                <iframe id="GP12_1" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DEFECTOLOGIA" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class='embed-container'>
                                                <iframe id="DEFECTOS_1" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div id="IMAGENES" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="container">
                                                <div id="carouselExampleControls" class="carousel slide shadow" data-bs-ride="carousel">
                                                    <div class="carousel-inner" id="ACTIVOS" runat="server">
                                                        <div class="carousel-item active">
                                                            <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" class="d-block w-100" style="width: 100%;">
                                                        </div>
                                                    </div>
                                                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Previous</span>
                                                    </button>
                                                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Next</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="EMBALAJE" class="tab-pane fade">
                                    <ul class="nav nav-pills nav-fill bg-white" role="tablist">
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#EMBALAJE_HOMOLOGADO1" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">HOMOLOGADO</button>
                                        </li>
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE_ALTERNATIVO1" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ALTERNATIVO</button>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="EMBALAJE_HOMOLOGADO1" class="tab-pane fade show active">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='embed-container'>
                                                        <iframe id="PAUTAEMBALAJE_1" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="EMBALAJE_ALTERNATIVO1" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='embed-container'>
                                                        <iframe id="PAUTAEMBALAJEALTERNATIVO_1" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DOCOTROS" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <div class="card mt-2 border border-dark shadow" runat="server" id="Div1">
                                                <h5 class="card-header text-bg-primary ">Otros documentos</h5>

                                                <asp:GridView ID="GridVinculados" runat="server" AllowSorting="True" OnRowCommand="OnRowCommand"
                                                    Width="100%" CssClass="table table-striped table-bordered table-hover shadow p-3 rounded border-top-0" AutoGenerateColumns="false" ShowHeader="false"
                                                    EmptyDataText="Ningún documento adicional vinculado.">
                                                    <HeaderStyle BackColor="#0d6efd" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" ItemStyle-BackColor="#ccccff">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnVer" CommandName="Redirect" CommandArgument='<%#Eval("URL")%>' CssClass="btn btn-outline-dark " Style="font-size: 1rem">
                                                                            <i class="bi bi-file-post"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTipoDocumento" Font-Bold="true" runat="server" Text='<%#Eval("TipoDocumento") %>' /><br />
                                                                <asp:Label ID="lblMultiREF" Font-Size="Small" runat="server" Text='<%#Eval("MULTIREFTEXT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescripcionDOC" runat="server" Text='<%#Eval("DescripcionDOC") %>' /><br />
                                                                <asp:Label ID="lblFechaDOC" Font-Size="Small" Font-Bold="true" runat="server" Text='<%#Eval("Fecha","{0:dd/MM/yyyy}") + " - Ed:" + Eval("Edicion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div id="REF2" class="tab-pane fade">
                        <div class="row">
                            <ul class="nav nav-pills nav-fill mt-1 bg-white" role="tablist">
                                <li class="nav-item" role="presentation" id="BTNREF2PTC">
                                    <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#PAUTACONTROL2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">PAUTA DE CONTROL</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" id="BTNREF2EST" data-bs-toggle="pill" data-bs-target="#ESTANDAR2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ESTÁNDAR</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#DEFECTOLOGIA2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOLOGIA</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#IMAGENES2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOS GP12</button>
                                </li>
                                <li class="nav-item" role="presentation" id="BTNREF2EMB">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">EMBALAJE</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#DOCOTROS2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">OTROS</button>
                                </li>
                            </ul>
                            <div class="nav nav-pills nav-fill" runat="server" role="tablist" style="background-color: #EFEFEF">
                                <button type="button" runat="server" id="MUR2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Datos del muro</button>
                                <button type="button" runat="server" id="DOC2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Documentación de la referencia</button>
                                <button type="button" runat="server" id="EST2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Estructura de materiales</button>
                                <button type="button" runat="server" id="FAB2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Ficha de fabricación</button>
                                <button type="button" runat="server" id="LIB2" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Liberación de serie</button>
                                <button type="button" runat="server" id="POL2" class="btn btn-outline-dark shadow shadow-sm" style="text-align: center; width: 16%; font-weight: bold">Polivalencia</button>
                            </div>
                            <div class="tab-content">
                                <div id="PAUTACONTROL2" class="tab-pane fade show active">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="PautaControl_2" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="ESTANDAR2" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="GP12_2" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DEFECTOLOGIA2" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="DEFECTOS_2" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="IMAGENES2" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="container">
                                                <div id="myCarousel_2" class="carousel slide shadow" data-bs-ride="carousel">
                                                    <div class="carousel-inner" id="ACTIVOS2" runat="server">
                                                        <div class="carousel-item active">
                                                            <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" class="d-block w-100" style="width: 100%;">
                                                        </div>
                                                    </div>
                                                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Previous</span>
                                                    </button>
                                                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Next</span>
                                                    </button>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="EMBALAJE2" class="tab-pane fade">

                                    <ul class="nav nav-pills nav-fill bg-white" role="tablist">
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#EMBALAJE_HOMOLOGADO2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">HOMOLOGADO</button>
                                        </li>
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE_ALTERNATIVO2" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ALTERNATIVO</button>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="EMBALAJE_HOMOLOGADO2" class="tab-pane fade show active">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='embed-container'>
                                                        <iframe id="PAUTAEMBALAJE_2" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="EMBALAJE_ALTERNATIVO2" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='embed-container'>
                                                        <iframe id="PAUTAEMBALAJEALTERNATIVO_2" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DOCOTROS2" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <div class="card mt-2 shadow" runat="server" id="Div2">
                                                <h5 class="card-header">Otros documentos</h5>
                                                <div class="card-body">
                                                    <asp:GridView ID="GridVinculados2" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;" OnRowCommand="OnRowCommand"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                        EmptyDataText="Ningún documento adicional vinculado.">
                                                        <HeaderStyle BackColor="#0d6efd" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnVer" CommandName="Redirect" CommandArgument='<%#Eval("URL")%>' CssClass="btn btn-outline-dark " Style="font-size: 1rem">
                                                                            <i class="bi bi-file-post"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipoDocumento" Font-Bold="true" runat="server" Text='<%#Eval("TipoDocumento") %>' />
                                                                    <br />
                                                                    <asp:Label ID="lblMultiREF" Font-Size="Small" runat="server" Text='<%#Eval("MULTIREFTEXT") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Documento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescripcionDOC" runat="server" Text='<%#Eval("DescripcionDOC") %>' /><br />
                                                                    <asp:Label ID="lblFechaDOC" Font-Size="Small" Font-Bold="true" runat="server" Text='<%#Eval("Fecha","{0:dd/MM/yyyy}") + " - Ed:" + Eval("Edicion") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div id="REF3" class="tab-pane fade">
                        <div class="row">
                            <ul class="nav nav-pills nav-fill mt-1 bg-white" role="tablist">
                                <li class="nav-item" role="presentation" id="BTNREF3PTC">
                                    <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#PAUTACONTROL3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">PAUTA DE CONTROL</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" id="BTNREF3EST" data-bs-toggle="pill" data-bs-target="#ESTANDAR3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ESTÁNDAR</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#DEFECTOLOGIA3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOLOGIA</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#IMAGENES3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOS GP12</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">EMBALAJE</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#DOCOTROS3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">OTROS</button>
                                </li>
                            </ul>
                            <div class="nav nav-pills nav-fill" runat="server" role="tablist" style="background-color: #EFEFEF">
                                <button type="button" runat="server" id="MUR3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Datos del muro</button>
                                <button type="button" runat="server" id="DOC3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Documentación de la referencia</button>
                                <button type="button" runat="server" id="EST3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Estructura de materiales</button>
                                <button type="button" runat="server" id="FAB3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Ficha de fabricación</button>
                                <button type="button" runat="server" id="LIB3" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Liberación de serie</button>
                                <button type="button" runat="server" id="POL3" class="btn btn-outline-dark shadow shadow-sm" style="text-align: center; width: 16%; font-weight: bold">Polivalencia</button>
                            </div>
                            <div class="tab-content">
                                <div id="PAUTACONTROL3" class="tab-pane fade show active">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="PautaControl_3" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="ESTANDAR3" class="tab-pane fade">
                                    <div class="embed-responsive embed-responsive-16by9">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <iframe id="GP12_3" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DEFECTOLOGIA3" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="DEFECTOS_3" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="IMAGENES3" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="container">
                                                <div id="myCarousel_3" class="carousel slide shadow" data-bs-ride="carousel">
                                                    <div class="carousel-inner" id="ACTIVOS3" runat="server">
                                                        <div class="carousel-item active">
                                                            <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" class="d-block w-100" style="width: 100%;">
                                                        </div>
                                                    </div>
                                                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Previous</span>
                                                    </button>
                                                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Next</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="EMBALAJE3" class="tab-pane fade">
                                    <ul class="nav nav-pills nav-fill bg-white" role="tablist">
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#EMBALAJE_HOMOLOGADO3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">HOMOLOGADO</button>
                                        </li>
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE_ALTERNATIVO3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ALTERNATIVO</button>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="EMBALAJE_HOMOLOGADO3" class="tab-pane fade show active">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='embed-container'>
                                                        <iframe id="PAUTAEMBALAJE_3" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="EMBALAJE_ALTERNATIVO3" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='embed-container'>
                                                        <iframe id="PAUTAEMBALAJEALTERNATIVO_3" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DOCOTROS3" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <div class="card mt-2 shadow" runat="server" id="Div3">
                                                <h5 class="card-header">Otros documentos</h5>
                                                <div class="card-body">
                                                    <asp:GridView ID="GridVinculados3" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;" OnRowCommand="OnRowCommand"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                        EmptyDataText="Ningún documento adicional vinculado.">
                                                        <HeaderStyle BackColor="#0d6efd" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnVer" CommandName="Redirect" CommandArgument='<%#Eval("URL")%>' CssClass="btn btn-outline-dark " Style="font-size: 1rem">
                                                                            <i class="bi bi-file-post"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipoDocumento" Font-Bold="true" runat="server" Text='<%#Eval("TipoDocumento") %>' />
                                                                    <br />
                                                                    <asp:Label ID="lblMultiREF" Font-Size="Small" runat="server" Text='<%#Eval("MULTIREFTEXT") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Documento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescripcionDOC" runat="server" Text='<%#Eval("DescripcionDOC") %>' /><br />
                                                                    <asp:Label ID="lblFechaDOC" Font-Size="Small" Font-Bold="true" runat="server" Text='<%#Eval("Fecha","{0:dd/MM/yyyy}") + " - Ed:" + Eval("Edicion") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="REF4" class="tab-pane fade">
                        <div class="row">
                            <ul class="nav nav-pills nav-fill mt-1 bg-white" role="tablist">
                                <li class="nav-item" role="presentation" id="BTNREF4PTC">
                                    <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#PAUTACONTROL4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">PAUTA DE CONTROL</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" id="BTNREF4EST" data-bs-toggle="pill" data-bs-target="#ESTANDAR4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ESTÁNDAR</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#DEFECTOLOGIA4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOLOGIA</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#IMAGENES4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">DEFECTOS GP12</button>
                                </li>
                                <li class="nav-item" role="presentation" id="BTNREF4EMB">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">EMBALAJE</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#DOCOTROS4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">OTROS</button>
                                </li>
                            </ul>
                            <div class="nav nav-pills nav-fill" runat="server" role="tablist" style="background-color: #EFEFEF">
                                <button type="button" runat="server" id="MUR4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Datos del muro</button>
                                <button type="button" runat="server" id="DOC4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Documentación de la referencia</button>
                                <button type="button" runat="server" id="EST4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 18%; font-weight: bold">Estructura de materiales</button>
                                <button type="button" runat="server" id="FAB4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Ficha de fabricación</button>
                                <button type="button" runat="server" id="LIB4" class="btn btn-outline-dark shadow shadow-sm" onserverclick="RedireccionaDocumento" style="text-align: center; width: 16%; font-weight: bold">Liberación de serie</button>
                                <button type="button" runat="server" id="POL4" class="btn btn-outline-dark shadow shadow-sm" style="text-align: center; width: 16%; font-weight: bold">Polivalencia</button>
                            </div>
                            <div class="tab-content">
                                <div id="PAUTACONTROL4" class="tab-pane fade show active">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="PautaControl_4" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div id="ESTANDAR4" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="GP12_4" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DEFECTOLOGIA4" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="embed-responsive embed-responsive-16by9">
                                                <iframe id="DEFECTOS_4" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div id="IMAGENES4" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="container">
                                                <div id="myCarousel_4" class="carousel slide shadow" data-bs-ride="carousel">
                                                    <div class="carousel-inner" id="ACTIVOS4" runat="server">
                                                        <div class="carousel-item active">
                                                            <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" class="d-block w-100" style="width: 100%;">
                                                        </div>
                                                    </div>
                                                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Previous</span>
                                                    </button>
                                                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                        <span class="visually-hidden">Next</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="EMBALAJE4" class="tab-pane fade">
                                    <ul class="nav nav-pills nav-fill bg-white" role="tablist">
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#EMBALAJE_HOMOLOGADO4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">HOMOLOGADO</button>
                                        </li>
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link shadow  border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE_ALTERNATIVO4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">ALTERNATIVO</button>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="EMBALAJE_HOMOLOGADO4" class="tab-pane fade show active">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='embed-container'>
                                                        <iframe id="PAUTAEMBALAJE_4" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="EMBALAJE_ALTERNATIVO4" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class='embed-container'>
                                                        <iframe id="PAUTAEMBALAJEALTERNATIVO_4" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="DOCOTROS4" class="tab-pane fade">
                                    <div class="row">
                                        <div class="col-lg-5">
                                            <div class="card mt-2 shadow" runat="server" id="Div4">
                                                <h5 class="card-header">Otros documentos</h5>
                                                <div class="card-body">
                                                    <asp:GridView ID="GridVinculados4" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;" OnRowCommand="OnRowCommand"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                                        EmptyDataText="Ningún documento adicional vinculado.">
                                                        <HeaderStyle BackColor="#0d6efd" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnVer" CommandName="Redirect" CommandArgument='<%#Eval("URL")%>' CssClass="btn btn-outline-dark " Style="font-size: 1rem">
                                                                            <i class="bi bi-file-post"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipoDocumento" Font-Bold="true" runat="server" Text='<%#Eval("TipoDocumento") %>' /><br />
                                                                    <asp:Label ID="lblMultiREF" Font-Size="Small" runat="server" Text='<%#Eval("MULTIREFTEXT") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Documento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescripcionDOC" runat="server" Text='<%#Eval("DescripcionDOC") %>' /><br />
                                                                    <asp:Label ID="lblFechaDOC" Font-Size="Small" Font-Bold="true" runat="server" Text='<%#Eval("Fecha","{0:dd/MM/yyyy}") + " - Ed:" + Eval("Edicion") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
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
        </div>

    </div>
    <!-- Button trigger modal -->
    <!-- Modal -->
    <div class="modal fade" id="PopIncrustados" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-warning">

                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" style="height: 900px">
                    <iframe id="IframeIncrustados" class="shadow shadow-lg" runat="server" src="http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=74986" frameborder="0" style="height: 90%; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%"></iframe>

                </div>
                <div class="modal-footer bg-warning">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="PopEstructura" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <label id="Label1" style="font-size: x-large; font-weight: bold">Estructura de materiales</label>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" style="height: 900px">
                    <label id="lblEstructuraProducto" style="font-size: large; font-weight: bold" runat="server"></label>
                    <asp:GridView ID="GridEstructura" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                        Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                        EmptyDataText="No hay datos para mostrar.">
                        <HeaderStyle BackColor="orange" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Orden" ItemStyle-BackColor="#ccccff">
                                <ItemTemplate>
                                    <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("ORDEN") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material">
                                <ItemTemplate>
                                    <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("MATERIAL") %>' />
                                    <asp:Label ID="lblDescripción" runat="server" Text='<%#" " + Eval("DESCRIPCION") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ubicación">
                                <ItemTemplate>
                                    <asp:Label ID="lblUbicacion" runat="server" Text='<%#Eval("UBICACION") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Necesario">
                                <ItemTemplate>
                                    <asp:Label ID="lblConsumo" runat="server" Text='<%#Eval("CONSUMOORDEN") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("NOTAS") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <label id="lblEstructuraOrden" style="font-size: large; font-weight: bold" runat="server" class="mt-1">Materiales necesarios para la producción (todas las órdenes):</label>
                    <asp:GridView ID="GridEstructuraOrden" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                        Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                        EmptyDataText="No hay datos para mostrar.">
                        <HeaderStyle BackColor="orange" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="Material">
                                <ItemTemplate>
                                    <asp:Label ID="lblReferencia" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("MATERIAL") %>' />
                                    <asp:Label ID="lblDescripción" runat="server" Text='<%#" " + Eval("DESCRIPCION") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ubicación">
                                <ItemTemplate>
                                    <asp:Label ID="lblUbicacion" runat="server" Text='<%#Eval("UBICACION") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total necesario">
                                <ItemTemplate>
                                    <asp:Label ID="lblConsumo" runat="server" Text='<%#Eval("CONSUMOORDEN") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("NOTAS") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>

                <div class="modal-footer bg-warning">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="PopPolivalencia" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <label style="font-size: x-large; font-weight: bold">Niveles de polivalencia</label>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="GridPolivalencia" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                        Width="98.5%" CssClass="table table-striped table-bordered table-hover bg-white shadow shadow-lg" AutoGenerateColumns="false"
                        EmptyDataText="No hay datos para mostrar.">
                        <HeaderStyle BackColor="orange" Font-Bold="True" />
                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-BackColor="#ccccff">
                                <ItemTemplate>
                                    <asp:Label ID="lblTIPO" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("TIPOOPERARIO") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operario">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumero" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("C_CLOCKNUMBER") %>' />

                                    <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("C_OPERATORNAME") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Horas">
                                <ItemTemplate>
                                    <asp:Label ID="lblUbicacion" runat="server" Text='<%#Eval("TIEMPOHORAS") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nivel">
                                <ItemTemplate>
                                    <asp:Label ID="lblConsumo" runat="server" Text='<%#Eval("NIVEL") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Última revisión">
                                <ItemTemplate>
                                    <asp:Label ID="lblNotas" runat="server" Text='<%#Eval("REVISION") %>' Font-Size="Large" Font-Bold="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                </div>

                <div class="modal-footer bg-warning">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="PopNuevaDocumentacion" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content" style="background-color: lightgoldenrodyellow">
                <div class="modal-body">
                    <div runat="server" id="AlertaDOC12">

                        <asp:Label ID="AlertaDOCTEXT" runat="server" Font-Size="Large" Font-Italic="true" BackColor="Transparent" ForeColor="Black" Width="100%" BorderColor="Transparent"></asp:Label><br />
                        <asp:Label ID="AlertaDOCTEXTCAMBIOS" runat="server" Font-Size="Large" Font-Italic="true" BackColor="Transparent" ForeColor="Black" Width="100%" BorderColor="Transparent"></asp:Label>
                        <div class="rounded rounded-2 mt-2" style="background-color: orange">
                            <button type="button" id="BTNCerrarAviso" class="btn btn-sm btn-outline-dark  shadow" style="width: 100%; font-weight: bold">Cierra este diálogo</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="PopDocVinculados" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <label style="font-size: x-large; font-weight: bold">Otros documentos</label>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="embed-responsive embed-responsive-16by9">
                                <iframe id="DocVinculado" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer bg-warning">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>




