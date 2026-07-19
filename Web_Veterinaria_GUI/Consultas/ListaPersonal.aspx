<%@ Page Title="Lista del Personal" Language="C#" CodePage="65001" MasterPageFile="~/Veterinaria.Master" AutoEventWireup="true" CodeBehind="ListaPersonal.aspx.cs" Inherits="Veterinaria.Consultas.ListaPersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lista del Personal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Cabecera" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Contenido_De_consulta" runat="server">
    <div class="hm-page consulta-page">
        <div class="hm-page-header">
            <div class="hm-icon-header">
                <i class="fa-solid fa-user-tie"></i>
            </div>
            <div>
                <h2>Lista del Personal</h2>
                <p>Revisa el personal registrado y sus horarios asignados</p>
            </div>
        </div>

        <div class="consulta-toolbar">
            <div class="consulta-toolbar-main">
                <div class="consulta-toolbar-icon">
                    <i class="fa-solid fa-users-gear"></i>
                </div>
                <div>
                    <h3>Personal administrativo y operativo</h3>
                </div>
            </div>
            <div class="hm-btn-actions consulta-actions">
                <asp:Button ID="btnListarPersonal" runat="server" Text="Listar Personal" CssClass="hm-btn-consultar" OnClick="btnListarPersonal_Click" />
                <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="hm-btn-filtrar" OnClick="btnExportar_Click" />
            </div>
        </div>

        <div class="consulta-summary-grid personal-summary">
            <div class="consulta-kpi">
                <span class="consulta-kpi-label">Registros</span>
                <asp:Label ID="lblTotal" runat="server" Text="0" />
            </div>
            <div class="consulta-kpi">
                <span class="consulta-kpi-label">Turno ma&ntilde;ana</span>
                <asp:Label ID="lblTurnoManana" runat="server" Text="0" />
            </div>
            <div class="consulta-kpi">
                <span class="consulta-kpi-label">Turno tarde</span>
                <asp:Label ID="lblTurnoTarde" runat="server" Text="0" />
            </div>
        </div>

        <div class="hm-tabla-section">
            <div class="hm-tabla-card">
                <div class="hm-tabla-card-header">
                    <h3><i class="fa-solid fa-table-list"></i> Personal registrado</h3>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="labelErrores" />
                    <span class="consulta-count">Registros: <asp:Label ID="lblConteo" runat="server" Text="0" /></span>
                </div>

                <asp:GridView ID="GridListarPersonal" runat="server" CssClass="hm-gridview" AutoGenerateColumns="False" GridLines="None" EmptyDataText="" Width="100%">
                     <Columns>
                         <asp:BoundField DataField="ID_Empleado" HeaderText="ID" />
                         <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre completo" />
                         <asp:BoundField DataField="DNI" HeaderText="DNI" />
                         <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                         <asp:BoundField DataField="Email" HeaderText="Email" />
                         <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                         <asp:BoundField DataField="HoraEntrada" HeaderText="Hora de entrada" />
                         <asp:BoundField DataField="HoraSalida" HeaderText="Hora de salida" />
                     </Columns>
                    <HeaderStyle CssClass="hm-gridview-header" />
                    <RowStyle CssClass="hm-gridview-row" />
                    <AlternatingRowStyle CssClass="hm-gridview-alt" />
                    <EmptyDataTemplate>
                        <div class="hm-tabla-empty">
                            <div class="hm-tabla-empty-icon">
                                <i class="fa-solid fa-folder-open"></i>
                            </div>
                            <strong>Sin resultados</strong>
                            <p>Presiona <b>Listar Personal</b> para consultar los registros.</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>



