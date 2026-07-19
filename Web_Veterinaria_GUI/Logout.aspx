<%@ Page Language="C#" CodePage="65001" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Web_Veterinaria_GUI.Logout" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>INUPETS | Login</title>
    <link rel="stylesheet" href="CSS/Logout.css">
    <meta http-equiv="refresh" content="7;url=Login.aspx" /><!--Retornoa a Login despues dde 7 segundos-->
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
    <video autoplay muted loop id="bgVideo" style="position: absolute; filter: contrast(0.5);">
        <source src="files/fondo_vet.mp4" type="video/mp4" >
    </video>

    <main class="login-shell">
        <div class="login-container">
        <div class="login-card">

            <div class="logo-section">
                <img src="imagenes/modern-animal-pet-logo-template-free-vector-Photoroom.png" alt="INUPETS" class="logo">
                <div class="brand-info">
                    <span class="brand">INUPETS</span>
                    <span class="tagline">Sistema Veterinario</span>
                </div>
            </div>

            <h1>Sesión Finalizada</h1>
            <p class="subtitle">Ha finalizado su sesión correctamente.</p>

            <a href="Login.aspx" class="btn-iniciar">Volver al inicio de Sesión</a>

            

        </div>
    </div>
    </main>
</body>
</html>
