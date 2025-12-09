<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GestionMolinos.aspx.cs" Inherits="ThermoWeb.MATERIALES.GestionMolinos" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Gestión de molidos</title>
   <link rel="shortcut icon" type="image/x-icon" href="../FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Molidos             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Indicadores
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                 <li><a class="dropdown-item" href="UbicacionMateriasPrimas.aspx">Ubicaciones materiales</a></li>
                <li><a class="dropdown-item" href="../KPI/KPI_Molidos.aspx">Resultados de reciclado</a></li>
            </ul>
        </li>

    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <script type="text/javascript">
        function ShowPopup1() {
            document.getElementById("AUXMODALACCION").click();
        }
        function ShowPopupMolido() {
            document.getElementById("AUXMODALMOLIDO").click();
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
        <div class="row">
            <ul class="nav nav-pills mb-2 nav-fill mt-2 " id="pills-tab" role="tablist" style="background-color: white">
                <li class="nav-item" role="presentation">
                    <button class="nav-link shadow " id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Líneas generadas</button>
                </li>
                <li class="nav-item " role="presentation">
                    <button class="nav-link shadow active " id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Estado molinos</button>
                </li>

            </ul>
        </div>
        <div class="tab-content shadow" id="pills-tabContent">
            <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                <div class="row">
                    <div class="col-lg-5">
                        <asp:GridView ID="dgv_Materiales" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                            Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display.">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                                <asp:TemplateField HeaderText="Máquina" ItemStyle-Width="10%" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="X-Large" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaquina" runat="server" Text='<%#Eval("MAQ") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material" ItemStyle-Width="60%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterial" Font-Bold="true" Font-Size="X-Large" CssClass="ms-2" runat="server" Text='<%#Eval("MATERIAL") %>' />
                                        <asp:Label ID="lblDescripcion" runat="server" Font-Size="Large" CssClass="ms-2" Text='<%#Eval("DESCRIPCION") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Molino" ItemStyle-Font-Size="X-Large" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMolino" runat="server" Text='<%#Eval("MOLINO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-lg-7">
                        <div class="row border border-1 border-dark rounded rounded-2 shadow" style="background-color: #ebeced">
                            <div class="card-header bg-primary text-white">
                                <label style="font-weight: 700; font-size: large">NAVE 5</label>
                            </div>
                            <div class="col-lg-4 mt-2">
                            </div>
                            <div class="col-lg-4 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº8</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 5 - MÁQUINA 48)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM8" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM8" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM8MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº14</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 5 - MÁQUINA 34)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM14" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM14" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM14MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mb-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº33</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 5 - MÁQUINA 43)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM33" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM33" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM33MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="row border border-1 border-dark rounded rounded-2 shadow mt-2" style="background-color: #ebeced">
                            <div class="card-header bg-primary text-white">
                                <label style="font-weight: 700; font-size: large">NAVE 4</label>
                            </div>
                            <div class="col-lg-4 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº17</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 4 - MÁQUINA 23)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM17" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM17" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM17MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mt-2">
                            </div>
                            <div class="col-lg-4 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº10</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 4 - MÁQUINA 31)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM10" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM10" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM10MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-8 mt-2">
                            </div>
                            <div class="col-lg-4 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº50</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 4 - FONDO)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM50" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM50" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM50MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-4 mb-2 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino N7</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 4 - MÁQUINA 32)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end  bg-white" id="MueleM7" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM7" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM7MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mb-2 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº6</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 4 - MÁQUINA 29)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM6" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM6" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM6MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mb-2 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº4</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 4 - MÁQUINA 38)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM4" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM4" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM4MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="row border border-1 border-dark rounded rounded-2 shadow mt-2" style="background-color: #ebeced">
                            <div class="card-header bg-primary text-white">
                                <label style="font-weight: 700; font-size: large">NAVE 3</label>
                            </div>
                            <div class="col-lg-4 mt-2 mb-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº 3</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 3 - MÁQUINA 46)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM3" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM3" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM3MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº 5</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 3 - MÁQUINA 47)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM5" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM5" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM5MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mt-2">
                            </div>


                        </div>
                        <div class="row border border-1 border-dark rounded rounded-2 shadow mt-2" style="background-color: #ebeced">
                            <div class="card-header bg-primary text-white">
                                <label style="font-weight: 700; font-size: large">MATERIALES</label>
                            </div>
                            <div class="col-lg-4 mt-2 mb-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº 1</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 2 - MATERIALES)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM1" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body ">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM1" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">

                                                <label style="font-weight: 700" runat="server" id="lblM1MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mt-2">
                                <div class="card border border-1 border-dark shadow shadow-sm">
                                    <div class="card-header bg-secondary text-white">
                                        <label style="font-weight: 700; font-size: large">Molino Nº 2</label><label class="ms-2" style="font-size: small; font-style: italic">(NAVE 2 - MATERIALES)</label>
                                        <button class="btn btn-sm btn-outline-dark float-end bg-white" id="MueleM2" runat="server" onserverclick="AbrirMolidoMaterial"><i class="bi bi-minecart "></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <button class="btn btn-sm btn-primary" runat="server" id="EditaM2" onserverclick="Editar_Molino"><i class="bi bi-pencil-fill"></i></button>
                                            </div>
                                            <div class="col-lg-10">
                                                <label class="ms-2" style="font-weight: 700" runat="server" id="lblM2MAT"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 mt-2">
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade show" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
              <div class="container">
                    <div class="col-lg-12">
                        <asp:GridView ID="dgv_HistoricoMolidos" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                            Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display.">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                                <asp:TemplateField HeaderText="Fecha" ItemStyle-Width="10%" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFecha" Font-Size="Large" CssClass="ms-2" runat="server" Text='<%#Eval("Fecha", "{0:dd/MM/yyyy}") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Molino" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMolino" Font-Bold="true" Font-Size="Large" CssClass="ms-2" runat="server" Text='<%#Eval("Molino") %>' /><br />
                                        <asp:Label ID="lblUbicacion" runat="server" Font-Size="Medium" CssClass="ms-2" Text='<%#Eval("UBICACION") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad" ItemStyle-Font-Size="X-Large" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMolino" runat="server" Text='<%#Eval("Cantidad") + " Kgs." %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material" ItemStyle-Width="60%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterial" Font-Bold="true" Font-Size="Large" CssClass="ms-2" runat="server" Text='<%#Eval("Referencia") %>' /><br />
                                        <asp:Label ID="lblDescripcion" runat="server" CssClass="ms-2" Text='<%#Eval("Descripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
             </div>
               
            </div>
            <div class="tab-pane fade show" id="pills-histo" role="tabpanel" aria-labelledby="pills-profile-tab">
                
                <div class="row">
                    <div class="col-lg-3">
                        <h6>Mes:</h6>
                        <asp:DropDownList ID="SeleMes" runat="server" CssClass="form-select shadow-sm ms-2" Font-Size="Large" AutoPostBack="False">
                            <asp:ListItem Text="-" Value="0"></asp:ListItem>
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
                    <div class="col-lg-3">
                        <h6>Material:</h6>
                        <input class="form-control" list="DatalistFiltroMat" id="InputFiltroMaterial" runat="server" autocomplete="off" placeholder="Escribe un material...">
                                        <datalist id="DatalistFiltroMat" runat="server">
                                        </datalist>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <h6>Molidos por mes</h6>
                        <asp:GridView ID="GridKPIporMES" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                            Width="98.5%" CssClass="table table-responsive shadow p-3 rounded border-top-0" AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display.">
                            <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                            <RowStyle BackColor="White" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                            <Columns>
                                <asp:TemplateField HeaderText="Mes" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFecha" CssClass="ms-2" runat="server" Text='<%#Eval("MES") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referencia" ItemStyle-HorizontalAlign="left" ItemStyle-BackColor="#e6e6e6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferencia" CssClass="ms-2" runat="server" Text='<%#Eval("Referencia") %>' /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("Descripcion") + " Kgs." %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCantidad" Font-Bold="true" CssClass="ms-2" runat="server" Text='<%#Eval("CANTIDAD") + " kgs." %>' /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>


            </div>
        </div>

        <div class="row invisible">
            <div class="col-lg-1"></div>
            <div class="col-lg-1 mt-1">
                <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-md-3 me-md-4 mb-md-1">
                    <button id="AUXCIERRAMODAL" runat="server" type="button" data-bs-dismiss="modal" aria-label="Close" visible="false"></button>
                    <button id="AUXMODALACCION" runat="server" type="button" class="btn btn-primary " hidden="hidden" data-bs-toggle="modal" data-bs-target="#ModalEditaAccion" style="font-size: larger"></button>
                    <button id="AUXMODALMOLIDO" runat="server" type="button" class="btn btn-primary " hidden="hidden" data-bs-toggle="modal" data-bs-target="#ModalMolerMaterial" style="font-size: larger"></button>
                    <button id="btnoffcanvas" runat="server" type="button" class="btn btn-primary ms-md-0 bi bi-funnel shadow-sm" data-bs-toggle="offcanvas" href="#offcanvasExample" style="font-size: larger"></button>
                </div>
            </div>
        </div>

        <%--MODALES DE EDICION --%>
        <div class="modal fade" id="ModalEditaAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header bg-primary shadow">
                        <h4 class="modal-title text-white" id="labelMolino" runat="server">Sin molino</h4>
                        <label runat="server" id="IDlabelMolino" visible="false">0</label>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

                    </div>
                    <div class="modal-body" runat="server" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                        <div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <label style="font-size: x-large; font-weight: bold">Material actual:</label>
                                    <label class="ms-4" id="lblMaterialActual" runat="server" style="font-style: italic; font-size: large"></label>
                                    <br />
                                </div>
                                <div class="col-lg-6">
                                    <div id="DIVMaterialAlternativo" runat="server" visible="false">
                                        <label style="font-size: x-large; font-weight: bold">Recicla en:</label><br />
                                        <label class="ms-4" id="lblMaterialAlternativo" runat="server" style="font-style: italic; font-size: large"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-lg-12">

                                    <label style="font-size: x-large; font-weight: bold">Nuevo material a moler:</label>
                                    <div class="input-group">
                                        <input class="form-control" list="DatalistNuevoMat" id="inputNuevoMaterial" runat="server" autocomplete="off" placeholder="Escribe un material...">
                                        <datalist id="DatalistNuevoMat" runat="server">
                                        </datalist>
                                        <button class="btn btn-danger" type="button" id="BtnBorrarMaterial" runat="server" onserverclick="Guardar_Molino" style="width: 80px"><i class="bi bi-trash"></i></button>
                                        <button class="btn btn-success" type="button" id="BtnCambiaMaterial" runat="server" onserverclick="Guardar_Molino" style="width: 80px"><i class="bi bi-check-lg"></i></button>



                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                    <div class="modal-footer" style="background: #e6e6e6">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ModalMolerMaterial" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="MolerMaterialLabel" aria-hidden="false">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header bg-primary shadow">
                        <h3 class="modal-title text-white" id="lblMaterialMolido" runat="server">Material</h3>
                        <h4 class="modal-title text-white ms-3" id="lblDescripcionMolido" runat="server">Descripcion</h4>
                        <h4 class="modal-title text-white ms-3" id="lblMolinoAsignado" hidden="hidden" runat="server">0</h4>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

                    </div>
                    <div class="modal-body" runat="server" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                        <div>
                            <div class="row mt-3">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="input-group input-group-lg">
                                        <label style="font-size: xx-large">Cantidad a reciclar:</label>
                                        <input type="number" id="inputMolidoKgs" runat="server" class="form-control ms-3" aria-label="Amount (to the nearest dollar)">
                                        <span class="input-group-text">Kgs.</span>
                                        <button class="btn btn-success" type="button" id="Button1" runat="server" onserverclick="Moler_Material"><i class="bi bi-check-lg">Registrar Kgs.</i></button>

                                    </div>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                            <div class="row" id="rowMolidoAlternativo" runat="server">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-6">
                                    <label id="lblMaterialAlternativoAviso" runat="server" style="font-style: italic" class="float-end"></label>
                                    <br />
                                    <label id="lblMaterialAlternativoAvisoReferencia" runat="server" style="font-style: italic" class="float-end"></label>
                                </div>
                                <div class="col-lg-2"></div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="background: #e6e6e6">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
