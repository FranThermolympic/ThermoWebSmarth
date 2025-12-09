<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ILUO_Formularios_I.aspx.cs" Inherits="ThermoWeb.CALIDAD.ILUO_Formularios" MasterPageFile="~/SMARTHLite.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Histórico del muro de calidad</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <style>
        th {
            background: #d3d3d3 !important;
            color: black !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
        }

        th, td {
            padding: 0.25rem;
        }
    </style>
    <script type="text/javascript">
        function ShowPopup1() {
            document.getElementById("AUXMODALACCION").click();
        }
        function ClosePopup1() {

        }
        function ShowPopupFirma() {
            document.getElementById("AUXMODALACCIONFIRMA").click();
        }
        function ClosePopupFirma() {
            document.getElementById("AUXCIERRAMODALFIRMA").click();
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
    <style>
    </style>

    <div class="container-fluid">
        <div class="card text-center shadow mt-1 border border-secondary">
            <div class="card-header  shadow-sm">
                <h4>NIVEL I <i>(Conoce y aplica de manera supervisada)</i></h4>
                <label>(Explicar al operario y enseñar a usar la documentación de la pieza)</label>
            </div>
            <div class="card-body border border-secondary">
                <div class="row mt-2">
                    <div class="col-lg-3">
                        <h5>- DOCUMENTACION -</h5>
                        <div class="row">
                            <label style="width: 65%">Planos</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined1" id="I01OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I01OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined1" id="I01NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I01NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined1" id="I01NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I01NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Ficha de calidad</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined2" id="I02OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I02OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined2" id="I02NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I02NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined2" id="I02NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I02NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Método operativo</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined3" id="I03OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I03OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined3" id="I03NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I03NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined3" id="I03NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I03NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Layout</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined4" id="I04OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I04OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined4" id="I04NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I04NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined4" id="I04NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I04NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Pauta de inspección</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined5" id="I05OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I05OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined5" id="I05NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I05NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined5" id="I05NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I05NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Pauta de embalaje</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined6" id="I06OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I06OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined6" id="I06NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I06NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined6" id="I06NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I06NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Ayudas visuales / Defectos</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined7" id="I07OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I07OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined7" id="I07NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I07NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined7" id="I07NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I07NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <h5>- PIEZA -</h5>
                        <div class="row">
                            <label style="width: 65%">Zonas falta de llenado</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined8" id="I08OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I08OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined8" id="I08NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I08NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined8" id="I08NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I08NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Zonas funcionales de cliente</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined9" id="I09OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I09OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined9" id="I09NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I09NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined9" id="I09NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I09NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Marcajes de la pieza</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined10" id="I10OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I10OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined10" id="I10NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I10NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined10" id="I10NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I10NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Posibles reclamaciones</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined11" id="I11OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I11OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined11" id="I11NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I11NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined11" id="I11NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I11NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Pieza con montaje</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined12" id="I12OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I12OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined12" id="I12NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I12NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined12" id="I12NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I12NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Componentes</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined13" id="I13OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I13OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined13" id="I13NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I13NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined13" id="I13NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I13NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Utillajes / Poka Yoke</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined14" id="I14OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I14OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined14" id="I14NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I14NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined14" id="I14NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I14NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <h5>- ESPECIALES -</h5>
                        <div class="row">
                            <label style="width: 65%">Especiales <i style="font-size:small">(Seguridad, reglamentación...)</i></label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined15" id="I15OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I15OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined15" id="I15NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I15NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined15" id="I15NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I15NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Requisitos de limpieza</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined16" id="I16OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I16OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined16" id="I16NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I16NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined16" id="I16NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I16NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Marcajes en etiqueta</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined17" id="I17OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I17OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined17" id="I17NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I17NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined17" id="I17NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I17NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Embalajes especiales</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined18" id="I18OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I18OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined18" id="I18NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I18NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined18" id="I18NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I18NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Tiene algún retrabajo</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined19" id="I19OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I19OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined19" id="I19NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I19NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined19" id="I19NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I19NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Componentes</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined20" id="I20OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I20OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined20" id="I20NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I20NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined20" id="I20NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I20NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                        <div class="row">
                            <label style="width: 65%">Otros</label>
                            <div class="input-group" style="width: 35%">
                                <input type="radio" class="btn-check" name="options-outlined21" id="I21OK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-success" for="I21OK"><i class="bi bi-hand-thumbs-up"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined21" id="I21NA" autocomplete="off" checked>
                                <label class="btn btn-sm btn-outline-secondary" for="I21NA"><i class="bi bi-slash-circle"></i></label>
                                <input type="radio" class="btn-check" name="options-outlined21" id="I21NOK" autocomplete="off">
                                <label class="btn btn-sm btn-outline-danger" for="I21NOK"><i class="bi bi-hand-thumbs-down"></i></label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <h5>Acciones vinculadas al nivel I:</h5>
                        <div class="form-floating">
                            <select class="form-select border border-secondary shadow-sm" id="IXX_ACCIONES" runat="server" aria-label="Floating label select example">
                                <option value="0" selected>No se requieren acciones</option>
                                <option value="1">Trabaja en binomio con un operario Nivel L</option>
                                <option value="2">Trabaja bajo supervisión de calidad planta</option>
                                <option value="3">Se bloquea el producto para revisión GP12</option>
                                
                            </select>
                            <label for="floatingSelect" style="text-align: start">Selecciona una acción</label>
                        </div>

                        <div class="form-floating ">
                            <textarea class="form-control  border border-secondary shadow-sm" placeholder="Introduce un comentario aquí" id="IXX_Comentarios" style="height: 100px; text-align: start"></textarea>
                            <label for="IXX_Comentarios" style="text-align: start">Comentarios</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer shadow shadow-sm text-muted ">
                <div class="row">
                    <div class="col-lg-4">
                        <h6>Fecha</h6>
                        
                    </div>
                    <div class="col-lg-4">
                        <h6>Formador</h6>
                    </div>
                    <div class="col-lg-4">
                        <h6>Firma operario</h6>
                    </div>
                </div>
            </div>
            <div class="card-body border border-secondary">
                <div class="row">
                    <div class="col-lg-4">
                        <asp:TextBox ID="tbFechaOriginal" Width="100%" CssClass="textbox Add-text" runat="server" autocomplete="off" Style="text-align: center; border-color: transparent; background-color: transparent"></asp:TextBox>
                        <label style="font-size:small"><i>(Fecha en la que se realiza la formación)</i></label>
                    </div>
                    <div class="col-lg-4">
                        <label style="font-size:small"><i>(Nombre y firma del formador)</i></label>
                    </div>
                    <div class="col-lg-4">
                        <label style="font-size:small"><i>(Reconozco haber recibido formación)</i></label>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>




