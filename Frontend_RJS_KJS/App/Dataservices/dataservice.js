// DATASERVICE.JS
// Base AJAX functionality that provides a generic ajax process

define(['app/core', 'app/router', 'json'], function (core, router, json) {

    // Generic Ajax method passing back callbacks to the parent
    function _ajaxRequest(type, url, data) {

        // Ensure we have a fully formed JSON object of the data to post/update or at least a blank JSON object
        var jsonData = (data != null || data != undefined) ? json.parse(data) : {};

        // Verify the usertoken is present.. if not, route to the login page
        var userToken = null;
        if (router.getCurrentPageRef() != 'login') {
            userToken = core.currentUserToken();
            if (userToken === undefined || userToken === null) {
                router.routeTo('login');
            }
        }

        // Create the base ajax options
        var options = {
            url: url,
            type: type,
            data: jsonData,
            dataType: 'json',
            cache: false
        }
        
        // If this is the login page, we will not have a token, so don't add the header
        if (userToken !== null) {
            options.headers = { 'usertoken': userToken };
        }

        // For all GET operations, add the json content type
        if (type === 'GET') {
            options.contentType = 'application/json; charset=utf-8';
        }
        
        // Construct the ajax call, and return the call back to the generic repository calling..
        var ajaxCall = $.ajax(url, options)
        return ajaxCall;
    }

    //  Public Interface
    return {
        ajaxRequest: _ajaxRequest
    }
});