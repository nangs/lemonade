﻿var app = angular.module("lemonade", ["ngAnimate", "xeditable", "ngRoute"]);

app.value("signalRServer", "");

app.run(function(editableOptions, editableThemes) {
    editableOptions.theme = 'bs3'; 
    editableThemes.bs3.inputClass = 'input-sm';
    editableThemes.bs3.buttonsClass = 'btn-xs';
});

app.config(function($routeProvider) {
    $routeProvider
        .when("/Applications", {
            templateUrl: "/Views/Applications.html",
            controller: "applicationController"
        })
        .when("/Features", {
            templateUrl: "/Views/Features.html",
            controller: "featureController"
        })
        .when("/About", {
            templateUrl: "/Views/About.html",
            controller: "aboutController"
        });
});

$("head").append($("<link rel=\"stylesheet\" type=\"text/css\" />").attr("href", "styles/Lemonade.css"));