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
                    sendRequest("change-rights", form);
                    break;
            }
        }

        function sendRequest(link, form) {
            $http({
                method: 'POST',
                url: '/api/' + link,
                data: form,
                headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' }
            }).then(
                function (res) {
                    console.log('succes !', res.data);
                },
                function (err) {
                    console.log('error');
                }
            );
        }
    }
}());