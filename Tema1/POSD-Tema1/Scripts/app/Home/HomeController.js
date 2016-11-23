(function () {
    "use strict";

    angular.module("app")
    .controller("HomeController", ["$scope", "$http", homeController]);

    function homeController($scope, $http, constantsService) {
        $scope.submitForm = function (methodType, form) {
            switch(methodType){
                case "1":
                    sendRequest("create-resource", form);
                    break;
                case "2":
                    sendRequest("read-resource", form);
                    break;
                case "3":
                    sendRequest("write-resource", form);
                    break;
                case "4":
                    sendRequest("add-rights", form);
                    break;
            }
        }

        function sendRequest(link, form) {
            delete $scope.response.message;
            delete $scope.response.fullMessage;
            delete $scope.response.data;

            $http({
                method: 'POST',
                url: '/api/' + link,
                data: form,
                headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' }
            }).then(
                function (res) {
                    console.log('succes !', res.data);
                    setTimeout(function () {
                        $scope.response = res.data;
                        $scope.allResources = res.data.allData.allResources;
                        $scope.allRoles = res.data.allData.allRoles;
                        $scope.$apply();
                    }, 500);
                },
                function (err) {
                    console.log('error');
                }
            );
        }

        function getAllResources() {
            $http({
                method: 'GET',
                url: '/api/get-all-resources',
                headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' }
            }).then(
                function (res) {
                    console.log('succes !', res.data);
                    $scope.response = res.data;
                    $scope.allResources = res.data.allData.allResources;
                    $scope.allRoles = res.data.allData.allRoles;
                },
                function (err) {
                    console.log('error');
                }
            );
        }
        getAllResources();
    }
}());