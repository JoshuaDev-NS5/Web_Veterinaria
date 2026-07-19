<%@ Page Title="Inicio INUPETS" Language="C#" MasterPageFile="~/Veterinaria.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Veterinaria.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inicio INUPETS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Cabecera" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Contenido_De_consulta" runat="server">

    <section class="hero-section" style="background-image: url('imagenes/Img1.png'); background-size: cover; background-position: center 30%;">
        <div class="hero-content">
            <h1>Todo lo que tu mascota necesita, en un solo lugar</h1>
            <p>Atencion veterinaria de calidad disponible para el cuidado integral de tus animales. Profesionales especializados listos para ayudarte.</p>
            <a href="Consultas/consVeterinario.aspx" class="hero-btn">
                <i class="fa-solid fa-stethoscope"></i> Ver nuestros veterinarios
            </a>
        </div>
    </section>

    <section class="servicios-section">
        <h2>Nuestros Servicios</h2>
        <p class="subtitulo">Contamos con especialistas en diversas areas para el cuidado de tu mascota</p>
        <div class="cards-servicios">
            <div class="card-servicio">
                <div class="icono"><i class="fa-solid fa-stethoscope"></i></div>
                <h3>Medicina General</h3>
                <p>Consultas, diagnosticos y tratamientos para todo tipo de mascotas.</p>
            </div>
            <div class="card-servicio">
                <div class="icono"><i class="fa-solid fa-scissors"></i></div>
                <h3>Cirugia</h3>
                <p>Procedimientos quirurgicos con equipos modernos y especialistas certificados.</p>
            </div>
            <div class="card-servicio">
                <div class="icono"><i class="fa-solid fa-tooth"></i></div>
                <h3>Odontologia</h3>
                <p>Limpieza dental, extracciones y cuidado bucal para tu mascota.</p>
            </div>
            <div class="card-servicio">
                <div class="icono"><i class="fa-solid fa-eye"></i></div>
                <h3>Oftalmologia</h3>
                <p>Diagnostico y tratamiento de enfermedades oculares en animales.</p>
            </div>
            <div class="card-servicio">
                <div class="icono"><i class="fa-solid fa-syringe"></i></div>
                <h3>Vacunacion</h3>
                <p>Calendario completo de vacunas para mantener a tu mascota protegida.</p>
            </div>
            <div class="card-servicio">
                <div class="icono"><i class="fa-solid fa-bone"></i></div>
                <h3>Traumatologia</h3>
                <p>Atencion de fracturas, lesiones y problemas musculoesqueleticos.</p>
            </div>
        </div>
    </section>

    <section class="stats-section" aria-label="Indicadores de INUPETS">
        <div class="stat-item">
            <div class="numero contador-numero" data-counter-target="21" data-counter-prefix="+" data-counter-duration="850">0</div>
            <div class="etiqueta">Veterinarios especializados</div>
        </div>
        <div class="stat-item">
            <div class="numero contador-numero" data-counter-target="500" data-counter-prefix="+" data-counter-duration="1050">0</div>
            <div class="etiqueta">Mascotas atendidas</div>
        </div>
        <div class="stat-item">
            <div class="numero contador-numero" data-counter-target="10" data-counter-prefix="+" data-counter-duration="750">0</div>
            <div class="etiqueta">Especialidades</div>
        </div>
        <div class="stat-item">
            <div class="numero dato-continuo">24/7</div>
            <div class="etiqueta">Atencion disponible</div>
        </div>
    </section>

    <script type="text/javascript">
        (function () {
            var counters = document.querySelectorAll(".contador-numero");
            var steadyNumbers = document.querySelectorAll(".dato-continuo");

            function formatNumber(value) {
                return value.toLocaleString("es-PE");
            }

            function revealSteadyNumber(item) {
                if (item.getAttribute("data-counter-ready") === "true") {
                    return;
                }

                item.setAttribute("data-counter-ready", "true");
                item.className += " numero-listo";
            }

            function animateCounter(counter) {
                if (counter.getAttribute("data-counter-ready") === "true") {
                    return;
                }

                counter.setAttribute("data-counter-ready", "true");

                var target = parseInt(counter.getAttribute("data-counter-target"), 10) || 0;
                var prefix = counter.getAttribute("data-counter-prefix") || "";
                var suffix = counter.getAttribute("data-counter-suffix") || "";
                var duration = parseInt(counter.getAttribute("data-counter-duration"), 10) || 900;
                var startedAt = null;

                function step(timestamp) {
                    if (!startedAt) {
                        startedAt = timestamp;
                    }

                    var elapsed = timestamp - startedAt;
                    var progress = Math.min(elapsed / duration, 1);
                    var eased = 1 - Math.pow(1 - progress, 3);
                    var current = Math.round(target * eased);

                    counter.textContent = prefix + formatNumber(current) + suffix;

                    if (progress < 1) {
                        window.requestAnimationFrame(step);
                    } else {
                        counter.textContent = prefix + formatNumber(target) + suffix;
                    }
                }

                window.requestAnimationFrame(step);
            }

            if ("IntersectionObserver" in window) {
                var observer = new IntersectionObserver(function (entries) {
                    entries.forEach(function (entry) {
                        if (entry.isIntersecting) {
                            if (entry.target.className.indexOf("contador-numero") !== -1) {
                                animateCounter(entry.target);
                            } else {
                                revealSteadyNumber(entry.target);
                            }

                            observer.unobserve(entry.target);
                        }
                    });
                }, { threshold: 0.35 });

                counters.forEach(function (counter) {
                    observer.observe(counter);
                });

                steadyNumbers.forEach(function (item) {
                    observer.observe(item);
                });
            } else {
                counters.forEach(animateCounter);
                steadyNumbers.forEach(revealSteadyNumber);
            }
        })();
    </script>

</asp:Content>

