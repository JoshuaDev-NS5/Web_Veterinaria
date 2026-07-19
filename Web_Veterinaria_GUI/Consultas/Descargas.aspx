<%@ Page Title="" Language="C#" MasterPageFile="~/Veterinaria.Master" AutoEventWireup="true" CodeBehind="Descargas.aspx.cs" Inherits="Veterinaria.Consultas.Desacargas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Descargas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cabecera" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido_De_consulta" runat="server">
     <div class="hm-page consulta-page">
     <div class="hm-page-header">
         <div class="hm-icon-header">
             <i class="fa-solid fa-download"></i>
         </div>
         <div>
             <h2>Descargas</h2>
             <p>Almacenamiento de todos tus archivos descargados desde la web</p>
         </div>
     </div>

     <div class="hm-tabla-section">
         <div class="hm-tabla-card">
             <div class="hm-tabla-card-header">
                 <h3><i class="fa-solid fa-table-list"></i> Archivos Descargados</h3>
                 <asp:Label ID="lblMensaje" runat="server" CssClass="labelErrores" />
                 <span class="consulta-count">Registros: <asp:Label ID="lblConteo" runat="server" Text="0" /></span>
                 <asp:Button ID="btnLimpiarDescargas" runat="server"
                     Text="Limpiar descargas" CssClass="hm-btn-limpiar"
                     CausesValidation="false"
                     OnClientClick="return confirm('¿Deseas eliminar todos los archivos descargados?');"
                     OnClick="btnLimpiarDescargas_Click" />
             </div>

             <asp:GridView ID="GridDescargas" runat="server" CssClass="hm-gridview" GridLines="None" EmptyDataText="" Width="100%"
                 AutoGenerateColumns="False" DataKeyNames="Archivo" OnRowCommand="GridDescargas_RowCommand">
                  <Columns>
                      <asp:BoundField DataField="Archivo" HeaderText="Archivo" />
                      <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                      <asp:BoundField DataField="Tamano" HeaderText="Tamaño" />
                      <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                      <asp:ButtonField ButtonType="Image" CommandName="Descargar" HeaderText="Descargar" ImageUrl="~/imagenes/download-solid.png" >
                      <ItemStyle HorizontalAlign="Center" />
                      <ControlStyle Width="40px"  />
                      </asp:ButtonField>
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
                         <p>Los reportes que exportes aparecerán aquí.</p>
                     </div>
                 </EmptyDataTemplate>
             </asp:GridView>
         </div>
     </div>
 </div>
</asp:Content>
