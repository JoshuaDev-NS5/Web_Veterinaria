<%@ Page Language="C#" CodePage="65001" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Veterinaria.WebForm1" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>INUPETS | Login</title>
    <link rel="stylesheet" href="CSS/Login.css">

        <script>
        window.addEventListener('pageshow', function (event) {
            if (event.persisted) {
                // La página vino del bfcache: forzamos recarga real al servidor para evitar la pagina de inicio guardada en cache
                window.location.reload();
            }
        });
</script>

</head>
<body>
    <video autoplay muted loop id="bgVideo">
        <source src="files/fondo_vet.mp4" type="video/mp4">
    </video>

    <main class="login-shell">
        <section class="formulario">
            <div class="login-brand">
                <img src="imagenes/modern-animal-pet-logo-template-free-vector-Photoroom.png" alt="logo INUPETS">
                <div>
                    <span class="brand-name">INUPETS</span>
                    <span class="brand-subtitle">Sistema Veterinario</span>
                </div>
            </div>

            <div class="login-heading">
                <h1>Inicio de Sesion</h1>
                <p>Accede al panel de consultas y atencion veterinaria</p>
            </div>

            <form id="form1" runat="server" autocomplete="off">
                <div class="username">
                    <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                    <%--<input id="txtUsuario" runat="server" type="text" placeholder=" " required>--%>
                    <label>Usuario</label>
                </div>
                <div class="username">
                    <asp:TextBox ID="txtClave" runat="server"  TextMode="Password"></asp:TextBox>
                   <%-- <input id="txtClave" runat="server" type="password" placeholder=" " required>--%>
                    <label>Contrase&ntilde;a</label>
                </div>
                <asp:Label ID="lblMensaje" runat="server" CssClass="login-error" EnableViewState="false"></asp:Label>
                <asp:Button ID="btnIngresar" runat="server" Text="Iniciar" CssClass="login-submit" OnClick="btnIngresar_Click" />
            </form>
        </section>
    </main>
</body>
</html>
