// DATASERVICE.JS
// Base AJAX functionality that provides a generic ajax process

define(['app/core', 'app/router', 'json'], function (core, router, json) {

    // Generic Ajax method passing back callbacks to the parent
    function _ajaxRequest(type, url, data) {

        var jsonData = (data != null || data != undefined) ? json.parse(data) : {};

        var userToken = null;
        if (router.getCurrentPageRef() != 'login') {
            userToken = core.currentUserToken();
            if (userToken === undefined || userToken === null) {
                router.routeTo('login');
            }
        }

        console.log('dataservice', jsonData, type, url, userToken);

        var options = {
            url: url,
            type: type,
            data: jsonData,
            dataType: 'json',
            cache: false
        }
        
        if (userToken !== null) {
            options.headers = { 'usertoken': userToken };
        }
        if (type === 'GET') {
            options.contentType = 'application/json; charset=utf-8';
        }
        
        var ajaxCall = $.ajax(url, options)
        return ajaxCall;
    }

    //  Public Interface
    return {
        ajaxRequest: _ajaxRequest
    }
});