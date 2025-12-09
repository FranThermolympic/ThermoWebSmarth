<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ImpresoraEtiquetas.aspx.cs" Inherits="ThermoWeb.PRODUCCION.ImpresoraEtiquetas" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Indicadores de mantenimiento</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Indicadores de mantenimiento
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">

    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Partes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/ReparacionMaquinas.aspx">Crear un parte de máquina</a></li>
                <li><a class="dropdown-item" href="http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx">Crear un parte de molde</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="../DOCUMENTAL/FichaReferencia.aspx">Consultar documentación de referencia</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Pendientes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMaquina.aspx">Estado reparaciones máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/EstadoReparacionesMoldes.aspx">Estado reparaciones moldes</a></li>
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="#">Gestionar preventivos máquina</a></li>

            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Informes
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMaquinas.aspx">Informe de máquinas</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformeMoldes.aspx">Informe de moldes</a></li>
                <li><a class="dropdown-item" href="../MANTENIMIENTO/InformePerifericos.aspx.aspx">Informe de periféricos</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="d-flex align-items-start ">
        <div class="nav flex-column nav-pills me-3 " id="v-pills-tab" role="tablist" aria-orientation="vertical">
            <br />
            <button class="nav-link  active" id="PILLMOLREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab1" type="button" role="tab" aria-controls="v-pills-profile" aria-selected="false" style="text-align: start; font-weight: 600"><i class="bi bi-grid-1x2">&nbsp General</i></button>
            <button class="nav-link" id="PILLMOLPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab2" type="button" role="tab" aria-controls="v-pills-messages" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-grid-1x2">Listado</i></button>
            <button class="nav-link" id="PILLMAQREPARAR" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab3" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Rep.</i></button>
            <button class="nav-link" id="PILLMAQPENDIENTES" runat="server" data-bs-toggle="pill" data-bs-target="#v-pills-tab4" type="button" role="tab" aria-controls="v-pills-settings" aria-selected="false" style="text-align: start; font-weight: 600" visible="false"><i class="bi bi-building">Máq. - Pend.Val.</i></button>
        </div>
        <div class="tab-content col-10" id="v-pills-tabContent">
            <div class="tab-pane fade  show active" id="v-pills-tab1" role="tabpanel" aria-labelledby="v-pills-profile-tab">
                <div class="container">
                    <div class="card mt-1">
                        <div class="card-body">
                            <h3 class="mt-1">33333319 Central Defroster SE270 - New Ibiza</h3>
                            <asp:Button ID="TestImprimir" CssClass="btn btn-primary" Text="Imprimir" runat="server" OnClick="ImprimirLabel" />
                            <div class="row">
                                <ul class="nav nav-tabs " id="myTab" role="tablist">
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link active" id="home-tabMOL" data-bs-toggle="tab" data-bs-target="#homeMOL" type="button" role="tab" aria-controls="homeMOL" aria-selected="true">Características</button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link" id="profile-tabMOL" data-bs-toggle="tab" data-bs-target="#profileMOL" type="button" role="tab" aria-controls="profileMOL" aria-selected="false">Inyección</button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link" id="contact-tabMOL" data-bs-toggle="tab" data-bs-target="#contactMOL" type="button" role="tab" aria-controls="contactMOL" aria-selected="false">Refrigeración</button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link" id="contact-tabMOL2" data-bs-toggle="tab" data-bs-target="#contactMOL2" type="button" role="tab" aria-controls="contactMOL2" aria-selected="false">Movimientos</button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link" id="contact-tabMOL4" data-bs-toggle="tab" data-bs-target="#contactMOL5" type="button" role="tab" aria-controls="contactMOL5" aria-selected="false">Postizos disponibles</button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link" id="contact-tabMOL3" data-bs-toggle="tab" data-bs-target="#contactMOL3" type="button" role="tab" aria-controls="contactMOL3" aria-selected="false">Reparaciones y pruebas</button>
                                    </li>
                                </ul>
                                <div class="tab-content" id="myTabContent">
                                    <div class="tab-pane fade show active" id="homeMOL" role="tabpanel" aria-labelledby="home-tabMOL">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail shadow mt-1 ms-2" alt="...">
                                                <labe>Camara</labe>
                                                <div id="qr-read"  style="width: 600px"></div>
                                            </div>
                                            <div class="col-lg-4">
                                                <label style="font-weight: bold">CLIENTE:</label><asp:Label ID="Label1" class="ms-2" runat="server">CLIENTE A</asp:Label><br />
                                                <label style="font-weight: bold">MOLDISTA:</label><asp:Label ID="Label2" class="ms-2" runat="server">MOLDISTA A</asp:Label><br />
                                                <label style="font-weight: bold">RESPONSABLE PROYECTO:</label><asp:Label ID="Label18" class="ms-2" runat="server">R.Borraz</asp:Label><br />
                                                <label style="font-weight: bold">FECHA RECEPCIÓN:</label><asp:Label ID="Label4" class="ms-2" runat="server">10/10/2021</asp:Label><br />
                                                <label style="font-weight: bold">RECEPCIONADO POR:</label><asp:Label ID="Label19" class="ms-2" runat="server">D.Ferrer</asp:Label><br />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-8 rounded-2 bg-secondary mt-1">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <img src="../SMARTH_fonts/INTERNOS/LOGO_MOLDE_DIMENSIONES.png" class="img-thumbnail shadow-sm mt-1 ms-2" alt="...">
                                                    </div>
                                                    <div class="col-sm-2"></div>
                                                    <div class="col-sm-6">
                                                        <label style="font-weight: bold">Dimensiones:</label><br />
                                                        <label style="font-weight: bold">H:</label><asp:Label ID="Label20" runat="server">100mm</asp:Label><br />
                                                        <label style="font-weight: bold">V:</label><asp:Label ID="Label21" runat="server">200mm</asp:Label><br />
                                                        <label style="font-weight: bold">E:</label><asp:Label ID="Label22" runat="server">300mm</asp:Label><br />
                                                        <label style="font-weight: bold">Peso (Kg):</label><asp:Label ID="Label13" runat="server">30Kg</asp:Label><br />

                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label class="ms-3" style="font-weight: bold">Ø Anillo:</label><asp:Label ID="Label5" runat="server">100mm</asp:Label><br />
                                                        <label class="ms-3" style="font-weight: bold">Bulón:</label><asp:Label ID="Label9" runat="server">200mm</asp:Label><br />

                                                        <label class="ms-3" style="font-weight: bold">Bridas:</label><asp:Label ID="Label10" runat="server">SI</asp:Label><br />

                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label style="font-weight: bold">Cavidades:</label><asp:Label ID="Label11" runat="server">5</asp:Label><br />
                                                        <label style="font-weight: bold">Puntos por cavidad:</label><asp:Label ID="Label12" runat="server">4</asp:Label><br />
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-lg-4">
                                                <h6>Ubicación del molde: </h6>
                                                <asp:TextBox ID="TBMolUbicacion" runat="server" class="form-control me-2"></asp:TextBox>
                                                <h6 class="mt-2">Mano Robot: </h6>
                                                <div class="input-group mb-3 me-3">
                                                    <asp:DropDownList ID="TBManDesignado" class="form-select " runat="server"></asp:DropDownList>
                                                    <button class="btn btn-outline-secondary" type="button" id="button-addon2"><i class="bi bi-eye"></i></button>
                                                </div>
                                                <h6>Ubicación de la mano: </h6>
                                                <asp:Label ID="Label3" runat="server">UBI</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="profileMOL" role="tabpanel" aria-labelledby="profile-tabMOL">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <h5>Proceso</h5>
                                                <asp:DropDownList ID="DropProcesoMold" class="form-select" runat="server" Width="100%">
                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                    <asp:ListItem Value="1">Bi-Inyección</asp:ListItem>
                                                    <asp:ListItem Value="2">Con versiones</asp:ListItem>
                                                    <asp:ListItem Value="3">Convencional</asp:ListItem>
                                                    <asp:ListItem Value="4">Inyección con gas</asp:ListItem>
                                                    <asp:ListItem Value="5">Mu-Cell</asp:ListItem>
                                                    <asp:ListItem Value="6">Sandwich</asp:ListItem>
                                                    <asp:ListItem Value="7">Sobremoldeo</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-6">
                                                <h5>Entrada</h5>
                                                <asp:DropDownList ID="DropDownList1" class="form-select" runat="server" Width="100%">
                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                    <asp:ListItem Value="1">Abanico</asp:ListItem>
                                                    <asp:ListItem Value="2">Cuerno</asp:ListItem>
                                                    <asp:ListItem Value="3">Directa</asp:ListItem>
                                                    <asp:ListItem Value="4">Laminar</asp:ListItem>
                                                    <asp:ListItem Value="5">Submarina</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h5>Inyección</h5>
                                                <asp:DropDownList ID="DropDownList2" class="form-select" runat="server" Width="100%">
                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                    <asp:ListItem Value="1">Bebedero/Canal frío</asp:ListItem>
                                                    <asp:ListItem Value="2">Boquilla caliente</asp:ListItem>
                                                    <asp:ListItem Value="3">Cámara caliente</asp:ListItem>
                                                    <asp:ListItem Value="4">Inyección secuencial</asp:ListItem>
                                                    <asp:ListItem Value="5">Sobre canal frío</asp:ListItem>
                                                    <asp:ListItem Value="6">Sobre pieza</asp:ListItem>
                                                    <asp:ListItem Value="7">Sobre pieza con mazarota</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <h5>Cámara caliente</h5>
                                            <div class="col-lg-12 rounded-2 shadow-sm" style="background-color: #e6e6e6">
                                                <label style="font-weight: bold">Nº Zonas:</label><asp:Label ID="Label14" runat="server"></asp:Label><br />
                                                <label style="font-weight: bold">Nº Boquillas:</label><asp:Label ID="Label15" runat="server"></asp:Label><br />
                                                <label style="font-weight: bold">Tipo de boquilla:</label><asp:Label ID="Label16" runat="server"></asp:Label><br />
                                                <label style="font-weight: bold">Nº de Resistencias:</label><asp:Label ID="Label17" runat="server"></asp:Label><br />
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <h5>Planos y esquemas</h5>
                                            <div class="col-lg-3">
                                                <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                            </div>
                                            <div class="col-lg-3">
                                                <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                            </div>
                                            <div class="col-lg-3">
                                                <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                            </div>
                                            <div class="col-lg-3">
                                                <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="contactMOL" role="tabpanel" aria-labelledby="contact-tabMOL">
                                        <div class="row mt-2">
                                            <div class="col-lg-4">
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox1" value="option1">
                                                    <label class="form-check-label" for="inlineCheckbox1">Correderas</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox2" value="option2">
                                                    <label class="form-check-label" for="inlineCheckbox2">Expulsores inclinados</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox3" value="option3">
                                                    <label class="form-check-label" for="inlineCheckbox3">Placa embridaje movil</label>
                                                </div>
                                            </div>
                                            <div class="col-lg-4">
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox4" value="option4">
                                                    <label class="form-check-label" for="inlineCheckbox4">Placa intermedia</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox5" value="option5">
                                                    <label class="form-check-label" for="inlineCheckbox5">Cavidad</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox6" value="option6">
                                                    <label class="form-check-label" for="inlineCheckbox6">Punzón</label>
                                                </div>
                                            </div>
                                            <div class="col-lg-4">
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox7" value="option7">
                                                    <label class="form-check-label" for="inlineCheckbox7">Placa embridaje fija</label>
                                                </div>

                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox8" value="option8">
                                                    <label class="form-check-label" for="inlineCheckbox8">Agua lado movil</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox9" value="option9">
                                                    <label class="form-check-label" for="inlineCheckbox9">Agua lado fijo</label>
                                                </div>

                                            </div>
                                            <div class="col-lg-4">
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox10" value="option10">
                                                    <label class="form-check-label" for="inlineCheckbox10">Aceite lado móvil</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="checkbox" id="inlineCheckbox11" value="option11">
                                                    <label class="form-check-label" for="inlineCheckbox11">Aceite lado fijo</label>
                                                </div>

                                            </div>

                                        </div>
                                        <hr />
                                        <div class="row">
                                            <h5>Planos y esquemas</h5>
                                            <div class="col-lg-3">
                                                <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                            </div>
                                            <div class="col-lg-3">
                                                <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                            </div>
                                            <div class="col-lg-3">
                                                <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                            </div>
                                            <div class="col-lg-3">
                                                <img src="../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg" class="img-thumbnail" alt="...">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="contactMOL2" role="tabpanel" aria-labelledby="contact-tabMOL2">
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="row">
                            </div>
                        </div>
                    </div>
                    <div class="row" hidden="hidden">
                        <div class="col-lg-10">
                        </div>
                        <div class="col-lg-2 text-right">
                            <br />
                            <h6>Periodo de revisión:</h6>
                            <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="cargar_tablas">
                               <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem> 
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row mt-2" hidden="hidden">
                        <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab" role="tablist">
                            <li class="nav-item " role="presentation">
                                <button class="nav-link shadow active " id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Resultados de máquinas</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link shadow " id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Resultados de moldes</button>
                            </li>
                        </ul>
                        <div class="tab-content shadow" id="pills-tabContent">
                            <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                                <div class="row">
                                    <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-currency-exchange text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICosteTotalMAQ" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">En reparaciones</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesCosteTotal">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasMAQCORR" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de correctivo</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesHoras">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasMAQPREV" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de preventivo</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesMalas">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-warning background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-exclamation-triangle text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIPartesMAQ" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Partes creados</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="FootMesRetrabajadas">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h4>Detalles mantenimiento máquinas</h4>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridResultadosMaq" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Corr. previsto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("ESTIMADASMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Corr. real" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("REALESMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prev. previsto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("ESTIMADASPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prev. real" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("REALESPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOS","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOS","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALES","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h5>Correctivos (Máquinas)</h5>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridDetallesMaqCORR" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Partes" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartes" runat="server" Text='<%#Eval("PARTESCORR")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Horas correctivo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("REALESMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOSCORR","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOSCORR","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALESCORR","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <h5>Preventivos (Máquinas)</h5>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridDetallesMaqPREV" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Partes" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartes" runat="server" Text='<%#Eval("PARTESPREV")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Horas preventivo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("REALESPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOSPREV","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOSPREV","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALESPREV","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab_maquina" role="tablist">
                                    <li class="nav-item " role="presentation">
                                        <button class="nav-link shadow active " id="pills-home-tab_maquina" data-bs-toggle="pill" data-bs-target="#pills-home_maquina" type="button" role="tab" aria-controls="pills-home_maquina" aria-selected="true">Resultados del mes</button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link shadow " id="pills-profile-tab_maquina" data-bs-toggle="pill" data-bs-target="#pills-profile_maquina" type="button" role="tab" aria-controls="pills-profile_maquina" aria-selected="false">Resultados del año</button>
                                    </li>
                                </ul>
                                <div class="tab-content shadow" id="pills-tabContent_maquina">
                                    <div class="tab-pane fade show active rounded" id="pills-home_maquina" role="tabpanel" aria-labelledby="pills-home-tab" style="background-color: #eeeeee">
                                        <div class="row">
                                            <div class="col-lg-2 mt-2 mb-2">
                                                <h6>Mes:</h6>
                                                <asp:DropDownList ID="SelecMes" runat="server" CssClass="form-select shadow-sm mb-2" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="cargar_tablas">
                                                    <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-10">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <h4>TOP10 - Partes por máquina</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMAQxParte" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMAQ" CommandArgument='<%#Eval("IdMaquinaCHAR")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxP" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMES_MxP" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Máquina">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxP" runat="server" Text='<%#Eval("Maquina")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxP" runat="server" Text='<%#Eval("PARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>


                                            </div>
                                            <div class="col-lg-6">
                                                <h4>Apertura de partes de máquina</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMAQ" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Persona">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("Nombre")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("PARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade rounded" id="pills-profile_maquina" role="tabpanel" aria-labelledby="pills-profile-tab" style="background-color: #eeeeee">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <h4>TOP10 - Partes por máquina</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMAQxParteAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="white" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMAQ" CommandArgument='<%#Eval("IdMaquinaCHAR")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MxPAÑO" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Máquina">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MxPAÑO" runat="server" Text='<%#Eval("Maquina")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MxPAÑO" runat="server" Text='<%#Eval("PARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>


                                            </div>
                                            <div class="col-lg-6">
                                                <h4>Apertura de partes de máquina</h4>
                                                <div class="table-responsive" style="width: 98.5%">
                                                    <asp:GridView ID="GridRankingMAQAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <RowStyle BackColor="white" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Persona">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("Nombre")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("PARTES") %>' />
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
                            <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                                <div class="row">
                                    <div class="col-sm-3 justify-content-center mt-1 mb-4">
                                        <div class="card prod-p-card bg-primary background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-currency-exchange text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPICosteTotalMOL" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">En reparaciones</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H1">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-danger background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasMOLCORR" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de correctivo</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H2">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-success background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-clock-history text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIHorasMOLPREV" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Horas de preventivo</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H3">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 justify-content-center  mt-1 mb-4">
                                        <div class="card prod-p-card bg-warning background-pattern-white shadow h-100">
                                            <div class="card-body">
                                                <div class="row align-items-center m-b-0">
                                                    <div class="col-auto">
                                                        <i class="bi bi-exclamation-triangle text-white ms-3" style="font-size: 60px"></i>
                                                    </div>
                                                    <div class="col ms-2 text-md-end">
                                                        <i class="text-white me-3 mb-0" runat="server" id="KPIPartesMOL" style="font-size: 40px">0</i>
                                                        <h6 class="text-white me-2 mb-1">Partes creados</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-footer bg-light text-md-end">
                                                <h6 class="mb-1" runat="server" id="H4">Ir a planes de acción</h6>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h4>Detalles mantenimiento (Moldes)</h4>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridResultadosMolde" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Corr. previsto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("ESTIMADASMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Corr. real" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("REALESMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prev. previsto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("ESTIMADASPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prev. real" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("REALESPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOS","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOS","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALES","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h5>Correctivos (Moldes)</h5>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridDetallesMolCORR" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Partes" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartes" runat="server" Text='<%#Eval("PARTESCORR")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Horas correctivo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("REALESMAQ") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOSCORR","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOSCORR","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALESCORR","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <h5>Preventivos (Moldes)</h5>
                                        <div class="table-responsive" style="width: 100%">
                                            <asp:GridView ID="GridDetallesMolPREV" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                EmptyDataText="No hay scrap declarado para mostrar.">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Partes" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartes" runat="server" Text='<%#Eval("PARTESPREV")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Horas preventivo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("REALESPREV") + " horas" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Repuestos">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRepuestos" runat="server" Text='<%#Eval("COSTESREPUESTOSPREV","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Coste Op.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteOp" runat="server" Text='<%#Eval("COSTESOPERARIOSPREV","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosteTotal" runat="server" Text='<%#Eval("COSTESTOTALESPREV","{0:c}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab_molde" role="tablist">
                                    <li class="nav-item " role="presentation">
                                        <button class="nav-link shadow active " id="pills-home-tab_molde" data-bs-toggle="pill" data-bs-target="#pills-home_molde" type="button" role="tab" aria-controls="pills-home_molde" aria-selected="true">Resultados del mes</button>
                                    </li>
                                    <li class="nav-item" role="presentation">
                                        <button class="nav-link shadow " id="pills-profile-tab_molde" data-bs-toggle="pill" data-bs-target="#pills-profile_molde" type="button" role="tab" aria-controls="pills-profile_molde" aria-selected="false">Resultados del año</button>
                                    </li>
                                </ul>
                                <div class="tab-content shadow" id="pills-tabContent_molde">
                                    <div class="tab-pane fade show active rounded" id="pills-home_molde" role="tabpanel" aria-labelledby="pills-home-tab" style="background-color: #eeeeee">
                                        <div class="row">
                                            <div class="col-lg-2 mt-2 mb-2">
                                                <h6>Mes:</h6>
                                                <asp:DropDownList ID="SELECMES2" runat="server" CssClass="form-select shadow-sm mb-2" Font-Size="Large" AutoPostBack="True" OnSelectedIndexChanged="cargar_tablas">
                                                    <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-10">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <h4>TOP10 - Partes por molde</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOLxParte" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="white" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMOL" CommandArgument='<%#Eval("Molde")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MOLxP" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMES_MOLxP" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Máquina">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MOLxP" runat="server" Text='<%#Eval("Molde")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MOLxP" runat="server" Text='<%#Eval("PARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                            <div class="col-lg-6">
                                                <h4>Apertura de partes de molde</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOL" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="white" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescNOK" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MES" ItemStyle-BackColor="#EFEFEF" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("MESTEXTO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Persona">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOK" runat="server" Text='<%#Eval("Nombre")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblScrapNOK" runat="server" Text='<%#Eval("PARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade rounded" id="pills-profile_molde" role="tabpanel" aria-labelledby="pills-profile-tab" style="background-color: #eeeeee">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <h4>TOP10 - Partes por molde</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOLxParteAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" ItemStyle-BackColor="#ccccff">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetail3" CommandName="RedirectMOL" CommandArgument='<%#Eval("Molde")%>' CssClass="btn btn-light " Style="font-size: 1rem"><i class="bi bi-zoom-in" aria-hidden="true"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAÑO_MOLxPAÑO" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Molde">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAQ_MOLxPAÑO" runat="server" Text='<%#Eval("Molde")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPARTES_MOLxPAÑO" runat="server" Text='<%#Eval("PARTES") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                            <div class="col-lg-6">
                                                <h4>Apertura de partes de molde</h4>
                                                <div class="table-responsive" style="width: 100%">
                                                    <asp:GridView ID="GridRankingMOLAÑO" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                        Width="98.5%" CssClass="table table-striped table-bordered table-hover shadow p-3 mb-5" AutoGenerateColumns="false" OnRowCommand="ContactsGridView_RowCommand"
                                                        EmptyDataText="No hay scrap declarado para mostrar.">
                                                        <RowStyle BackColor="white" />
                                                        <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#ffffcc" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="AÑO" ItemStyle-BackColor="#ccccff" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescNOKAÑO" runat="server" Text='<%#Eval("AÑO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Persona">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHorasNOKAÑO" runat="server" Text='<%#Eval("Nombre")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Partes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblScrapNOKAÑO" runat="server" Text='<%#Eval("PARTES") %>' />
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
            <div class="tab-pane fade" id="v-pills-tab2" role="tabpanel" aria-labelledby="v-pills-messages-tab">
            </div>
            <div class="tab-pane fade" id="v-pills-tab3" role="tabpanel" aria-labelledby="v-pills-settings-tab">
            </div>
            <div class="tab-pane fade" id="v-pills-tab4" role="tabpanel" aria-labelledby="v-pills-profile-tab">
            </div>
            <div class="tab-pane fade" id="v-pills-tab5" role="tabpanel" aria-labelledby="v-pills-messages-tab">
            </div>
            <div class="tab-pane fade" id="v-pills-tab6" role="tabpanel" aria-labelledby="v-pills-settings-tab">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function onScanSuccess(decodedText, decodedResult) {
            console.log(`Code scanned = ${decodedText}`, decodedResult);
        }
        var html5QrcodeScanner = new Html5QrcodeScanner(
            "qr-read", { fps: 10, qrbox: 250 });
        html5QrcodeScanner.render(onScanSuccess);
    </script>
</asp:Content>




