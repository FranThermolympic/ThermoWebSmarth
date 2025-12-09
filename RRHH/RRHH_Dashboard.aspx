<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RRHH_Dashboard.aspx.cs" Inherits="ThermoWeb.RRHH.RRHH_Dashboard" MasterPageFile="~/SMARTHLite.Master"
    EnableEventValidation="false" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Artículos entregados pendientes de firma</title>

</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Artículos entregados pendientes de firma
              
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
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
            <style>
                #lblSinAccidentes {
                    vertical-align: middle;
                }
            </style>
            <div class="mt-5 border border-secondary shadow shadow-lg" style="text-align: center; vertical-align:middle">
                <label id="lblSinAccidentes" class="mt-1 " style="font-size: 100px; font-style: italic">Días sin baja por accidente:</label><br />
                <button type="button" id="lblSinNumAccidentes" runat="server"  style="height: 175px; font-size: 125px; border-color: transparent; background-color: transparent" data-bs-toggle="modal" data-bs-target="#exampleModal">
                    5
                </button>
            </div>
            <div class="mt-5" style="text-align: center">
                <label style="font-size: 50px; color: red; font-weight:bold">Protecciones Individuales</label><br />
                <img src="EPISLOGO.png" height="250px" /><br />
                <label style="font-size: 65px; color: darkblue; font-weight:bold">YA QUE LAS TIENES ¡PÓNTELAS!</label>
             
            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalLabel">Fecha del último accidente:</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">

                        <input type="text" id="InputFechaAlta" class="form-control border-dark shadow Add-text" autocomplete="off" runat="server">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" runat="server" id="GuardaFecha" onserverclick="GuardarFecha">Guardar</button>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
