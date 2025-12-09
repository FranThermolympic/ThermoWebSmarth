<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="AccesoDocumentalMaquina.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.AccesoDocumentalMaquina" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Acceso a documentación de máquinas</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Documentación por máquina             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Gestión Documental
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="FichaReferencia.aspx">Documentación de referencia</a></li>
                <li><a class="dropdown-item" href="GestionDocumentalPendientes.aspx">Produciendo sin digitalizar</a></li>
                <li><a class="dropdown-item" href="AccesoDocumentalMaquina.aspx">Tablero de máquinas</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <script type="text/javascript">
        function ShowPopup1() {
            //document.getElementById("lblMaqPOP").innerText = "Máquina " + machvar + "";
            document.getElementById("AUXMODALACCION").click();
            //CargarCharts(machvar);

        }
        function ClosePopup1() {

        }
    </script>
    <%--Calendario--%>
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

                yearRange: "c-20:c+10",
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa']
            });
        });
    </script>

    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="tab-content shadow" id="pills-tabContent">
            <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                <div class="container-fluid">
                    <div class="row mt-2">
                        <div class="col-lg-3">
                            <div class="row border border-1 border-dark rounded rounded-2 shadow" style="background-color: #ebeced">
                                <div class="card-header bg-primary text-white rounded rounded-2 shadow">
                                    <label style="font-weight: 700; font-size: large" class="ms-3"><i class="bi bi-building me-2"></i>NAVE 6</label>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">FOAM</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="FOAM" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">HOUSING</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="HOUSING" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">WORKTOP</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="WTOP" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>

                        </div>
                        <div class="col-lg-3">
                            <div class="row border border-1 border-dark rounded rounded-2 shadow" style="background-color: #ebeced">
                                <div class="card-header bg-primary text-white rounded rounded-2 shadow">
                                    <label style="font-weight: 700; font-size: large" class="ms-3"><i class="bi bi-building me-2"></i>NAVE 5</label>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 34</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ34" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 48</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ48" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 50</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ50" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 43</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ43" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="col-lg-6">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 35</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ35" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 42</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ42" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 39</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ39" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 33</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ33" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="row border border-1 border-dark rounded rounded-2 shadow" style="background-color: #ebeced">
                                <div class="card-header bg-primary text-white rounded rounded-2 shadow">
                                    <label style="font-weight: 700; font-size: large" class="ms-3"><i class="bi bi-building me-2"></i>NAVE 4</label>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 31</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ31" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 30</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ30" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Montaje BSH</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="BSH" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 25</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ25" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 23</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ23" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 38</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ38" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 29</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ29" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 32</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ32" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 26</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ26" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 22</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ22" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 45</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ45" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>



                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-lg-3">
                            <div class="row border border-1 border-dark rounded rounded-2 shadow" style="background-color: #ebeced">
                                <div class="card-header bg-primary text-white rounded rounded-2 shadow">
                                    <label style="font-weight: 700; font-size: large" class="ms-3"><i class="bi bi-building me-2"></i>NAVE 3</label>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 44</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ44" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 16</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ16" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 15</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ15" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 24</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ24" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 12</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ12" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 37</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ37" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 28</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ28" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 49</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ49" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 40</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ40" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 41</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ41" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>
                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 36</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ36" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 46</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ46" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>
                                        <div class="card border border-1 border-dark shadow shadow-sm mt-2 mb-2">
                                            <div class="card-header bg-secondary text-white">
                                                <label style="font-weight: 700; font-size: large">Máquina 47</label>
                                                <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MAQ47" runat="server" onserverclick="AbrirMaquinas"><i class="bi bi-folder"></i></button>
                                            </div>

                                        </div>

                                    </div>
                                </div>


                            </div>
                        </div>

                    </div>

                </div>



                <div class="tab-pane fade show" id="pills-histo" role="tabpanel" aria-labelledby="pills-profile-tab">
                    <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>
                </div>
            </div>
            <div class="modal fade" id="ModalEditaAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
                <div class="modal-dialog modal-fullscreen">
                    <div class="modal-content">
                        <div class="modal-header bg-primary shadow">
                            <h5 class="modal-title text-white" id="staticBackdropLabel" runat="server"><i class="bi bi-book-half me-2"></i>Tablero de documentación -
                            <label id="lblMaqPOP" runat="server">Máquina X</label></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            <asp:TextBox ID="IDINSPECCION" runat="server" Style="text-align: center" Width="100%" Enabled="false" Visible="false"></asp:TextBox>
                        </div>
                        <div class="modal-body" runat="server">
                            <div>
                                <div class="row " style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                                    <div class="holds-the-iframe">
                                        <iframe id="DocVinculado" runat="server" class="embed-responsive-item" src="" frameborder="0" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                    </div>
                                    <style>
                                        .holds-the-iframe {
                                            background: url(newloading.gif) center center no-repeat;
                                            
                                        }
                                    </style>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <%--MODALES DE EDICION --%>
        </div>
    </div>
</asp:Content>
