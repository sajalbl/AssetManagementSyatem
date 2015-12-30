'use strict';
app.factory('uploadFileService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:58474/';
   

    var uploadFileServiceFactory = {};

    var _fileUpload = function (empid, file, path) {
        var data = new FormData();
        data.append('EmployeeID', empid);
        data.append('UploadImage', file);
        data.append('FolderPath', path);
        
        return $http.post(serviceBase + "api/manage/imageUpload", data, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (results) {

        });
    };
    uploadFileServiceFactory.fileUpload = _fileUpload;


    var _CSVUpload = function (file , path) {
        var data = new FormData();
        
        data.append('UploadCSV', file);
        data.append('FolderPath', path);

        return $http.post(serviceBase + "api/manage/UploadCSV", data, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (results) {

            //var text = {"csvFileName": file.FileName}

            //$http.post(serviceBase + 'api/manage/csvController', JSON.stringify(text)).then(function (results) {


            //});

        });
    };
    uploadFileServiceFactory.CSVUpload = _CSVUpload;


      return uploadFileServiceFactory;
  
}]);