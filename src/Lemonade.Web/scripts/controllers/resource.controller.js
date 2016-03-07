﻿function resourceController($scope, $http, eventService, toastService) {
    $http.get("/api/applications").then(function (res) {
        $scope.applications = res.data;
    });

    $http.get("/api/locales").then(function (res) {
        $scope.locales = res.data;
    });

    $scope.selectApplication = function (application) {
        $http.get("/api/resources?applicationId=" + application.applicationId).then(function (res) {
            $scope.application = application;
            $scope.newResource = { applicationId: application.applicationId }
            $scope.resources = res.data;
        });
    }

    $scope.addResource = function (resource) {
        $http.post("/api/resources", resource);
    }

    $scope.updateResource = function (resource) {
        $http.put("/api/resources", resource);
    }

    $scope.deleteResource = function (resourceId) {
        $.ajax({ url: "/api/resources?id=" + resourceId, type: "DELETE" });
    }

    $scope.resourceFilter = function (criteria) {
        return function (resource) {
            return criteria === undefined ||
                   criteria.resourceSet === "" || resource.resourceSet === criteria.resourceSet ||
                   criteria.resourceKey === "" || resource.resourceKey === criteria.resourceKey ||
                   criteria.locale === {} || criteria.locale.description === "Show all..." || resource.localeId === criteria.locale.localeId ||
                   criteria.value === "" || resource.value === criteria.value;
        };
    }

    $scope.onResourceAdded = function (resource) {
        $scope.$apply(function () {
            $scope.newResource = { applicationId: $scope.application.applicationId }
            $scope.resources.push(resource);
        });
    }

    $scope.onResourceRemoved = function (resource) {
        $scope.$apply(function () {
            for (var i = 0; i < $scope.resources.length; i++) {
                if ($scope.resources[i].resourceId === resource.resourceId) {
                    $scope.resources.splice(i, 1);
                }
            }

            $scope.resource = null;
        });
    }

    $scope.onResourceErrorEncountered = function (error) {
        toastService.toast(error.message, "OK", "bottom right");
    }

    eventService.onResourceAdded($scope, $scope.onResourceAdded);
    eventService.onResourceRemoved($scope, $scope.onResourceRemoved);
    eventService.onResourceErrorEncountered($scope, $scope.onResourceErrorEncountered);
}