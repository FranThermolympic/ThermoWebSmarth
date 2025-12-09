<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="MontajesExternos.aspx.cs" Inherits="ThermoWeb.PRODUCCION.MontajesExternos" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Montajes externos</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp-
    <label id="LabelNumMaquina" runat="server">Montajes externos</label>
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="MontajesHistorico.aspx">Histórico de montajes</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById("BtnEnviar").onclick = function fun() {
                //alert("ENTRA");
                TerminaRevisionAJAX();
                //validation code to see State field is mandatory.  
            }
        }
        function ClosePopupDocumentacion() {
            document.getElementById("btnDismissModal").click();
        }
        function ShowPopupDOCAUX() {
            document.getElementById("AUXMODALDOCAUX").click();
        }
        function TerminaRevisionAJAX() {
            $.ajax({
                type: "POST",
                url: "MontajesExternos.aspx/TerminarMontajeAJAX",
                data: "{LOTE: '" + document.getElementById("tbLote").value + "', PIEZASFABRICADAS: '" + document.getElementById("tbPiezasFabricadas").value + "', HORAINICIO: '" + document.getElementById("tbHoraInicio").value + "', COSTEOPERARIO: '" + document.getElementById("CosteOperario").value + "', AUXOPERARIOEMPRESA: '" + document.getElementById("AUXOperarioEmpresa").value + "', AUXOPERARIOINT: '" + document.getElementById("AUXOperarioINT").value + "',AUXOPERARIO: '" + document.getElementById("AUXOperario").value + "',NOTAS: '" + document.getElementById("tbNotas").value + "',CANTIDADMONTADA: '" + document.getElementById("tbPiezasFabricadas").value + "',REFERENCIA: '" + document.getElementById("AUXReferencia").value + "',REFERENCIATEXT: '" + document.getElementById("AuxReferenciaNombre").value + "', CANTIDADMALA: '" + document.getElementById("tbPiezasMalas").value+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r)
                {
                    const DataUse = eval(r.d);
                    console.log(DataUse);
                    if (DataUse == true) {
                        //alert("Vamos");
                        alert("Montaje guardado correctamente.");
                        //let url = location.href;
                        location.href = location.href;

                    }
                    else
                    {
                        alert("Se ha producido un error al guardar el montaje. Revise los datos introducidos. Recuerde que los campos 'Lote' y 'Piezas fabricadas' son numéricos y obligatorios");
                    }
                },
                failure: function (response) {
                    //alert("NoVamos");
                    alert("Se ha producido un error al guardar el montaje. Revise los datos introducidos. Recuerde que los campos 'Lote' y 'Piezas fabricadas' son numéricos y obligatorios");
                }
            });
        }
    </script>

    <%--Calendario--%>
    
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container">
            <div class="row">
                <%--AUXILIARES--%>
                <asp:HiddenField ID="AUXReferencia" runat="server" />
                <asp:HiddenField ID="AuxReferenciaNombre" runat="server" />
                <asp:HiddenField ID="AUXOperarioINT" runat="server" />
                <asp:HiddenField ID="AUXOperario" runat="server" />
                <asp:HiddenField ID="AUXOperarioEmpresa" runat="server" />
                <asp:HiddenField ID="CosteOperario" runat="server" />
                 

            </div>
            <div class="row ">
                <%--cabecera de selección--%>
                <div class="col-lg-5">
                    <div class="card mt-2 shadow shadow-lg  border border-secondary">
                        <h5 class="card-header text-bg-primary"><i class="bi bi-journals me-2"></i>Parte de ensamblado</h5>
                        <div class="card-body" style="background-color: whitesmoke">
                            <div class="input-group shadow">
                                <span class="input-group-text border border-secondary" style="width: 40%">Hora de inicio:</span>
                                <asp:TextBox ID="tbHoraInicio" runat="server" Enabled="false" Font-Italic="true" class="form-control border border-secondary"></asp:TextBox>
                            </div>

                            <div class="input-group shadow">
                                <span class="input-group-text border border-secondary" style="width: 40%">Lote:</span>
                                <asp:TextBox ID="tbLote" runat="server" TextMode="Number" Enabled="false" class="form-control border border-secondary"></asp:TextBox>
                            </div>
                            <div class="input-group shadow">
                                <span class="input-group-text border border-secondary" style="width: 40%">Piezas buenas:</span>
                                <asp:TextBox ID="tbPiezasFabricadas" runat="server" Enabled="false" TextMode="Number" class="form-control border border-secondary"></asp:TextBox>
                            </div>
                            <div class="input-group shadow">
                                <span class="input-group-text border border-secondary" style="width: 40%">Piezas a chatarra:</span>
                                <asp:TextBox ID="tbPiezasMalas" runat="server" Enabled="false" TextMode="Number" class="form-control border border-secondary"></asp:TextBox>
                            </div>
                            <div class="border border-secondary rounded rounded-2" style="background-color: orange; text-align:center">
                                <label style="font-weight:bold">Notas</label>
                            </div>
                            <asp:TextBox ID="tbNotas" runat="server" Enabled="false" Width="100%" TextMode="MultiLine" Rows="2" class="form-control border border-secondary"></asp:TextBox>
                    
                        </div>
                        <div class="card-footer bg-success p-0">
                            <button id="BtnEnviar" type="button" class="btn btn-success btn-sm" style="width: 100%; font-size: large" runat="server" disabled="disabled" ><i class="bi bi-send">&nbsp Enviar</i></button>
                        </div>
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="card mt-2 shadow shadow-lg  border border-secondary">
                        <h5 class="card-header text-bg-primary"><i class="bi bi-journals me-2"></i>Detalles</h5>
                        <div class="card-body" style="background-color: whitesmoke">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:HyperLink ID="IMGPieza" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid border border-dark  shadow" />
                                </div>
                                <div class="col-lg-8">
                                    <div class="input-group shadow">
                                        <span class="input-group-text border border-secondary" style="width: 20%">Producto</span>
                                        <asp:TextBox ID="tbReferencia" Enabled="false" runat="server" Font-Italic="true" class="form-control border border-secondary"></asp:TextBox>

                                    </div>
                                    <div class="input-group shadow">
                                        <span class="input-group-text border border-secondary" style="width: 20%">Operario</span>
                                        <asp:TextBox ID="tbOperario" Enabled="false" runat="server" Font-Italic="true" class="form-control border border-secondary"></asp:TextBox>
                                    </div>
                                    <div style="display: flex; align-items: center; justify-content: center">
                                        <asp:HyperLink ID="IMGCliente" NavigateUrl="" Width="105px" ImageWidth="99px" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid border border-dark mt-1 shadow" />
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

            </div>
            <div class="row  mt-2">
                <div class="col-lg-12">
                    <ul class="nav nav-pills nav-fill mt-1 bg-white" role="tablist">
                        <li class="nav-item" role="presentation" id="BTNREF1PTC">
                            <button class="nav-link shadow  border border-secondary active" data-bs-toggle="pill" data-bs-target="#PAUTACONTROL" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">PAUTA DE CONTROL</button>
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
                            <button class="nav-link shadow border border-secondary" data-bs-toggle="pill" data-bs-target="#EMBALAJE" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold">EMBALAJE</button>
                        </li>

                    </ul>
                    <div class="tab-content rounded rounded-2 border border-secondary shadow">
                        <div id="PAUTACONTROL" class="tab-pane fade show active ">
                            <div class="row">
                                <div class="col-lg-12">
                                    <iframe id="PautaControl_1" runat="server"  frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
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

                    </div>
                </div>
            </div>
          

        </div>
    </div>
    

    <!-- Button trigger modal -->
    <div class="modal fade" id="ModalDocumentosAuxiliares" runat="server" data-bs-keyboard="false" data-bs-backdrop="static" tabindex="-1" aria-labelledby="ModalDocumentosAuxiliares" aria-hidden="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="H1DOCAUX" runat="server">Datos del montaje</h5>
                    <button type="button" class="btn-close invisible" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" runat="server">
                    <div class="row">
                        <div class="col-lg-8">
                            <div class="input-group shadow">
                                <span class="input-group-text border border-secondary" style="width: 30%">Producto</span>
                                <datalist id="DatalistReferencias" runat="server">
                                </datalist>
                                <input list="DatalistReferencias" style="width: 70%" id="tbReferenciaCarga" autocomplete="off" class="form-control border border-secondary" runat="server" placeholder="Escribe una referencia...">
                            </div>
                            <div class="input-group mb-3">
                                <span class="input-group-text border border-secondary" style="width: 30%">Operario</span>
                                <input id="tbOperarioCarga" style="width: 70%" class="form-control border border-secondary" runat="server" placeholder="Escribe tu número de operario">
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <button type="button" class="btn btn-primary shadow mt-3" runat="server" onserverclick="IniciarMontaje" style="width: 100%">Iniciar montaje</button>
                            
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <button id="AUXMODALDOCAUX" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalDocumentosAuxiliares" style="font-size: larger">MSA</button>
    

    <!-- Modal -->

</asp:Content>




