<%@ Page Title="Consulta Veterinarios" Language="C#" CodePage="65001" MasterPageFile="~/Veterinaria.Master" AutoEventWireup="true" CodeBehind="consVeterinario.aspx.cs" Inherits="Veterinaria.ConsVeterinario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Consulta Veterinarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Cabecera" runat="server">
    <link href="/CSS/consVeterinaria.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Contenido_De_consulta" runat="server">

    <div class="cons-wrapper">

        <div class="cons-header">
            <h1><i class="fas fa-stethoscope"></i> Consulta de Veterinarios</h1>
            <p>Busca y filtra veterinarios por especialidad y estado</p>
        </div>

        <div class="stats-cards">
            <div class="stat-card">
                <div class="stat-icon"><i class="fas fa-stethoscope"></i></div>
                <div>
                    <div class="stat-num"><asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Veterinarios registrados</div>
                </div>
            </div>
            <div class="stat-card">
                <div class="stat-icon"><i class="fas fa-check-circle"></i></div>
                <div>
                    <div class="stat-num"><asp:Label ID="lblActivos" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Veterinarios activos</div>
                </div>
            </div>
            <div class="stat-card">
                <div class="stat-icon"><i class="fas fa-award"></i></div>
                <div>
                    <div class="stat-num"><asp:Label ID="lblEspecialidades" runat="server" Text="0"></asp:Label></div>
                    <div class="stat-label">Especialidades</div>
                </div>
            </div>
        </div>

        <div class="filtros-box">
            <label for="ddlEspecialidad">Especialidad:</label>
            <asp:DropDownList ID="ddlEspecialidad" runat="server">
                <asp:ListItem Text="Todos" Value="" />
                <asp:ListItem Text="Oftalmología" Value="Oftalmología" />
                <asp:ListItem Text="Oncología" Value="Oncología" />
                <asp:ListItem Text="Ortopedia" Value="Ortopedia" />
                <asp:ListItem Text="Nutrición" Value="Nutrición" />
                <asp:ListItem Text="Endocrinología" Value="Endocrinología" />
                <asp:ListItem Text="Medicina General" Value="Medicina General" />
                <asp:ListItem Text="Cirugía" Value="Cirugía" />
                <asp:ListItem Text="Dermatología" Value="Dermatología" />
                <asp:ListItem Text="Pediatría Animal" Value="Pediatría Animal" />
                <asp:ListItem Text="Cardiología" Value="Cardiología" />
                <asp:ListItem Text="Radiología" Value="Radiología" />
                <asp:ListItem Text="Anestesiología" Value="Anestesiología" />
                <asp:ListItem Text="Neurología" Value="Neurología" />
                <asp:ListItem Text="Traumatología" Value="Traumatología" />
            </asp:DropDownList>

            <label for="ddlEstado">Estado:</label>
            <asp:DropDownList ID="ddlEstado" runat="server">
                <asp:ListItem Text="Todos" Value="" />
                <asp:ListItem Text="Activo" Value="Activo" />
                <asp:ListItem Text="Inactivo" Value="Inactivo" />
            </asp:DropDownList>

            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn-consultar" OnClick="btnConsultar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn-limpiar" OnClick="btnLimpiar_Click" />
            <asp:Button ID="btnExcel" runat="server" Text="⬇ Exportar Excel"
    CssClass="btn-excel" OnClick="btnExcel_Click" />
        </div>

        <div class="tabla-box">
            <div class="hm-tabla-card-header">
                <h3 style="font-size: 1.1rem;"><i class="fa-solid fa-table-list"></i> Resultado de la consulta</h3>
                <asp:Label ID="lblMensaje" runat="server" CssClass="labelErrores" />
                <span class="consulta-count">Registros: <asp:Label ID="lblConteo" runat="server" Text="0" /></span>
            </div>

            <asp:GridView ID="gvVeterinarios" runat="server"
                CssClass="tabla-vet"
                AutoGenerateColumns="false"
                GridLines="None">
                <Columns>
                    <asp:BoundField DataField="ID_vet" HeaderText="ID" />
                    <asp:BoundField DataField="nomVeterinario" HeaderText="Nombre" />
                    <asp:BoundField DataField="apellidoVeterinario" HeaderText="Apellido" />
                    <asp:BoundField DataField="especialidad" HeaderText="Especialidad" />
                    <asp:BoundField DataField="telefonoVeterinario" HeaderText="Tel&eacute;fono" />
                    <asp:BoundField DataField="email" HeaderText="Email" />
                    <asp:BoundField DataField="DNI_Veterinario" HeaderText="DNI" />
                    <asp:BoundField DataField="Estado_Veterinario" HeaderText="Estado" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="empty-msg">Selecciona filtros y presiona Consultar.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>

    </div>

</asp:Content>


