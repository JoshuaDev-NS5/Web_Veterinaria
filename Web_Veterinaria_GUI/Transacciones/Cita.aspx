<%@ Page Title="" Language="C#" MasterPageFile="~/Veterinaria.Master" AutoEventWireup="true" CodeBehind="Cita.aspx.cs" Inherits="Web_Veterinaria_GUI.Cita" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Registrar Cita
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cabecera" runat="server">
    <link href="/CSS/CitaCSS.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido_De_consulta" runat="server">

    <!-- ToolkitScriptManager: necesario para los ModalPopupExtender del AjaxControlToolkit -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="cita-page">

        <!-- Encabezado -->
        <div class="cita-page-header">
            <div class="cita-icon-header">
                <i class="fa-solid fa-calendar-check"></i>
            </div>
            <div>
                <h2>Registrar Cita</h2>
                <p>Completa los datos para programar una nueva cita veterinaria</p>
            </div>
        </div>

        <!-- Tarjeta: Datos de la cita -->
        <div class="cita-form-card">

            <div class="cita-section-title">
                <i class="fa-solid fa-clipboard-list"></i> Datos de la cita
            </div>

            <asp:Label ID="lblMensajeCita" runat="server" CssClass="labelErrores" />

            <div class="cita-form-grid">

                <div class="cita-field">
                    <label for="DL_Empleado"><i class="fa-solid fa-user-tie"></i> Empleado</label>
                    <asp:DropDownList ID="DL_Empleado" runat="server" CssClass="cita-select">
                        <asp:ListItem Text="Seleccione un empleado" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="cita-field">
                    <label for="DL_Veterinario"><i class="fa-solid fa-user-doctor"></i> Veterinario</label>
                    <asp:DropDownList ID="DL_Veterinario" runat="server" CssClass="cita-select">
                        <asp:ListItem Text="Seleccione un veterinario" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="cita-field">
                    <label for="DL_Cliente"><i class="fa-solid fa-user"></i> Cliente</label>
                    <asp:DropDownList ID="DL_Cliente" runat="server" CssClass="cita-select" AutoPostBack="true" OnSelectedIndexChanged="DL_Cliente_SelectedIndexChanged">
                        <asp:ListItem Text="Seleccione un cliente" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="cita-field">
                    <label for="DL_TipoCita"><i class="fa-solid fa-tags"></i> Tipo de cita</label>
                    <asp:DropDownList ID="DL_TipoCita" runat="server" CssClass="cita-select">
                        <asp:ListItem Text="Seleccione un tipo de cita" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="cita-field">
                    <label for="DL_Mascota"><i class="fa-solid fa-paw"></i> Mascota</label>
                    <asp:DropDownList ID="DL_Mascota" runat="server" CssClass="cita-select">
                        <asp:ListItem Text="Seleccione una mascota" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="cita-field">
                    <label for="txtFecha"><i class="fa-solid fa-calendar-day"></i> Fecha</label>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="cita-input" TextMode="Date" />
                </div>

                <div class="cita-field">
                    <label for="txtHora"><i class="fa-solid fa-clock"></i> Hora</label>
                    <asp:TextBox ID="txtHora" runat="server" CssClass="cita-input" TextMode="Time" />
                </div>

                <div class="cita-field">
                    <label for="txtPrecio"><i class="fa-solid fa-sack-dollar"></i> Precio</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="cita-input" TextMode="Number" step="0.01" placeholder="S/ 0.00" />
                </div>

                <div class="cita-field cita-field-full">
                    <label for="txtMotivo"><i class="fa-solid fa-notes-medical"></i> Motivo</label>
                    <asp:TextBox ID="txtMotivo" runat="server" CssClass="cita-input" TextMode="MultiLine" Rows="3" placeholder="Describe el motivo de la cita..." />
                </div>

                <div class="cita-field cita-field-full">
                    <label for="txtObservaciones"><i class="fa-solid fa-comment-medical"></i> Observaciones</label>
                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="cita-input" TextMode="MultiLine" Rows="2" placeholder="Observaciones adicionales (opcional)" />
                </div>

            </div>

            <!-- Acciones -->
            <div class="cita-actions">
                <asp:Button ID="btnRegistrarTratamiento" runat="server" Text="Preparar Tratamiento" CssClass="cita-btn-secondary" CausesValidation="false" />
                <asp:Button ID="btnGuardarCita" runat="server" Text="Guardar Cita" CssClass="cita-btn-primary" OnClick="btnGuardarCita_Click" />
            </div>

        </div>

    </div>

    <!-- ============================================================ -->
    <!-- MODAL 1: Registrar Tratamiento                                -->
    <!-- ============================================================ -->
    <asp:Panel ID="pnlTratamiento" runat="server" CssClass="cita-modal-panel" Style="display:none;">
        <div class="cita-modal-header">
            <i class="fa-solid fa-pills cita-modal-icon"></i>
            <div>
                <h3>Registrar Tratamiento</h3>
                <p>Asocia un medicamento o vacuna a esta cita</p>
            </div>
            <asp:Button ID="btnCerrarTratamiento" runat="server" Text="✕" CssClass="cita-modal-close" CausesValidation="false" />
        </div>

        <div class="cita-modal-body">

            <asp:Label ID="lblMensajeTratamiento" runat="server" CssClass="labelErrores cita-field-full" />

            <div class="cita-field">
                <label for="DL_Medicamento"><i class="fa-solid fa-capsules"></i> Medicamento</label>
                <asp:DropDownList ID="DL_Medicamento" runat="server" CssClass="cita-select">
                    <asp:ListItem Text="Seleccione un medicamento" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="cita-field">
                <label for="DL_Vacuna"><i class="fa-solid fa-syringe"></i> Vacuna</label>
                <asp:DropDownList ID="DL_Vacuna" runat="server" CssClass="cita-select">
                    <asp:ListItem Text="Seleccione una vacuna" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="cita-field">
                <label for="txtDosis"><i class="fa-solid fa-prescription-bottle"></i> Dosis</label>
                <asp:TextBox ID="txtDosis" runat="server" CssClass="cita-input" placeholder="Ej. 1 tableta cada 8h" />
            </div>

            <div class="cita-field">
                <label for="txtFrecuencia"><i class="fa-solid fa-rotate"></i> Frecuencia</label>
                <asp:TextBox ID="txtFrecuencia" runat="server" CssClass="cita-input" placeholder="Ej. Cada 12 horas" />
            </div>

            <div class="cita-field cita-field-full">
                <label for="txtMiligramosVacuna"><i class="fa-solid fa-vial"></i> Miligramos de vacuna</label>
                <asp:TextBox ID="txtMiligramosVacuna" runat="server" CssClass="cita-input" placeholder="Ej. 5 ml" />
            </div>

        </div>

        <div class="cita-modal-footer">
            <asp:Button ID="btnCancelarTratamiento" runat="server" Text="Cancelar" CssClass="cita-btn-cancel" CausesValidation="false" />
            <asp:Button ID="btnGuardarTratamiento" runat="server" Text="Confirmar Tratamiento" CssClass="cita-btn-primary" CausesValidation="false" OnClick="btnGuardarTratamiento_Click" />
        </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="mpeTratamiento" runat="server"
        TargetControlID="btnRegistrarTratamiento"
        PopupControlID="pnlTratamiento"
        BackgroundCssClass="cita-modal-fondo"
        CancelControlID="btnCancelarTratamiento"
        DropShadow="false" />

    <!-- ============================================================ -->
    <!-- MODAL 2: Cita registrada con exito                            -->
    <!-- ============================================================ -->
    <asp:Panel ID="pnlExito" runat="server" CssClass="cita-exito-panel" Style="display:none;">
        <div class="cita-exito-icono">
            <i class="fa-solid fa-circle-check"></i>
        </div>
        <h3>¡Cita registrada con éxito!</h3>
        <p>La cita se guardó correctamente en el sistema.</p>
        <asp:Button ID="btnAceptarExito" runat="server" Text="Aceptar" CssClass="cita-btn-primary" CausesValidation="false" />
    </asp:Panel>

    <!-- Botón oculto que sirve como TargetControlID: se invocará desde el
         code-behind (ej. mpeExito.Show();) cuando la cita se guarde con éxito -->
    <asp:Button ID="btnMostrarExito" runat="server" Style="display:none;" CausesValidation="false" />

    <ajaxToolkit:ModalPopupExtender ID="mpeExito" runat="server"
        TargetControlID="btnMostrarExito"
        PopupControlID="pnlExito"
        BackgroundCssClass="cita-modal-fondo"
        CancelControlID="btnAceptarExito"
        DropShadow="false" />
</asp:Content>
