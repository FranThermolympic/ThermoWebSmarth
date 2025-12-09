<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GestionPreventivos.aspx.cs" Inherits="ThermoWeb.MANTENIMIENTO.GestionPreventivos" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Gestión de preventivos (MÁQUINA)</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Gestión de preventivos (MÁQUINA)
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex" id="wrapper">
        <div class="flex-shrink-0 p-3 bg-dark shadow-sm" style="width: 240px;">
            <a class="btn btn btn-outline-primary border-0" href="MantenimientoIndex.aspx" role="button" style="width: 100%; color: white; text-align: start"><i class="bi bi-building me-2"></i>Página principal</a><br />              
            
            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-tools me-md-2"></i>Partes de trabajo    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="../MANTENIMIENTO/ReparacionMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Mant. Moldes</a></div>
                
                </div>
              

            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-list-check me-md-2"></i>Acciones abiertas <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample2">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMaquina.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="EstadoReparacionesMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Pdtes. Moldes</a></div>
            </div>
            <a class="btn btn btn-outline-primary border-0" data-bs-toggle="collapse" href="#collapseExample3" role="button" aria-expanded="false" aria-controls="collapseExample" style="width: 100%; color: white; text-align: start">
                <i class="bi bi-journals me-md-2"></i>Informes    <i class="bi bi-caret-down-fill"></i>
            </a>
            <div class="collapse" id="collapseExample3">
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMoldes.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de moldes</a><br />
                </div>
                <div class="mt-2">
                    <a class="btn-sm btn-outline-light border-0 ms-4" href="InformeMaquinas.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de máquinas</a><br />
                </div>
                <div class="mt-2"><a class="btn-sm btn-outline-light border-0 ms-4" href="InformePerifericos.aspx" role="button" style="width: 90%; text-align: start; text-decoration: none"><i class="bi bi-arrow-right-circle me-2"></i>Informe de periféricos</a></div>
            </div>
            <i class="bi bi-journals me-md-2"></i>
        </div>
        <div class="flex-fill" id="page-content-wrapper">
            <div class="container-fluid" runat="server" id="SinAcceso">
                <div class="alert alert-danger d-flex align-items-center mt-3" role="alert">
                    <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Danger:">
                        <i class="bi bi-exclamation-triangle-fill"></i>
                    </svg>
                    <div>
                        &nbsp No tienes acceso al contenido de esta página.
                    </div>
                </div>
            </div>
            <div class="container-fluid" runat="server" id="AccesoOK" visible="false">
                <div class="row">
                    <div class="col-lg-1">
                        <button id="btnGuardar" runat="server" onserverclick="Guardar_PlanMantenimiento" class="btn btn-secondary mt-2" type="button">Guardar</button>
                    </div>
                    <div class="col-lg-7"></div>
                    <div class="col-lg-4">
                        <div class="input-group mt-2">
                            <asp:DropDownList ID="TipoMantCarga" runat="server" CssClass="form-select"></asp:DropDownList>
                            <button id="btnBuscarMaquina" runat="server" onserverclick="CargaDatos" class="btn btn-outline-secondary" type="button">Cargar</button>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:TextBox ID="tbTipoRecurso" runat="server" Enabled="false" Height="30px" Visible="false"></asp:TextBox><br />
                        <asp:TextBox ID="tbIdMantenimiento" runat="server" Enabled="false" Height="30px" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="tbTipoFrecuencia" runat="server" BackColor="Transparent" BorderColor="Transparent" Font-Size="X-Large" Width="100%" Font-Bold="TRUE" Enabled="false" Height="30px" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3">
                        <div class="table-responsive mt-2">
                            <asp:GridView ID="dgv_ListadoMaquinas" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                Width="98.5%" CssClass="table table-responsive shadow p-3 mb-5 rounded border-top-0" AutoGenerateColumns="false"
                                EmptyDataText="No hay moldes para mostrar.">
                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="#e6e6e6" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Máquinas" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaquinas" runat="server" Text='<%#Eval("C_LONG_DESCR") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-lg-9">
                        <div id="TablaAcciones" runat="server" class="table-responsive mt-2" visible="false">
                            <table class="table">
                                <colgroup>
                                    <col span="1" style="width: 5%; align-items: center">
                                    <col span="1" style="width: 90%;">
                                </colgroup>
                                <thead style="background-color: #0d6efd; color: white">
                                    <tr>
                                        <th>#</th>
                                        <th>Acción a realizar</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="font-size: large"><strong>1</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q1" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>2</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q2" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>3</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q3" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>4</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q4" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>5</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q5" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>6</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q6" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>7</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q7" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>8</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q8" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>9</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q9" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>10</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q10" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>11</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q11" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>12</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q12" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>13</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q13" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>14</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q14" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>15</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q15" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>16</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q16" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>17</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q17" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>18</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q18" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>19</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q19" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: large"><strong>20</strong></td>
                                        <td>
                                            <asp:DropDownList ID="Q20" runat="server" CssClass="form-control" Font-Bold="true"></asp:DropDownList></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>





























