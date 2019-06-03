// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $(".dropdown-placeholder + .dropdown-menu .dropdown-item input").on("change", function () {
        let valores = [];
        $(this).parents(".dropdown-placeholder + .dropdown-menu").find("input").each(function() {
            if ($(this).prop("checked")) {
                valores.push($(this).siblings("label").text().trim());
            }
        });
        let placeholder = $(this).parents(".dropdown-placeholder + .dropdown-menu").siblings(".dropdown-placeholder");
        if (!placeholder.data("default")) {
            placeholder.data("default", placeholder.text());
        }
        placeholder.text(valores.join(",") || placeholder.data("default"));
    });
});