// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    const accordionHeaders = document.querySelectorAll('.accordion-header');

    accordionHeaders.forEach(header => {
        header.addEventListener('click', function () {
            const accordionItem = this.parentElement;

            // Kontrollera om den klickade redan är aktiv
            const isActive = accordionItem.classList.contains('active');

            // Stäng alla andra rutor först (valfritt, men ger en snygg effekt)
            document.querySelectorAll('.accordion-item').forEach(item => {
                item.classList.remove('active');
            });

            // Om den man klickade på INTE var aktiv, öppna den nu
            if (!isActive) {
                accordionItem.classList.add('active');
            }
        });
    });
});
