'use strict';
app.factory('uploadFileService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:14597/';
    var uploadFileServiceFactory = {};

    var _fileUpload = function (empid, file, path) {
        var data = new FormData();
        data.append('EmployeeID', empid);
        data.append('UploadImage', file);
        data.append('FolderPath', path);
        return $http.post(serviceBase + 'api/manage/imageUpload', data, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (results) {
        });
    };
    uploadFileServiceFactory.fileUpload = _fileUpload;

    return uploadFileServiceFactory;
}]);