// DATASERVICE.JS
// Base AJAX functionality that provides a generic ajax process

define(['app/core', 'app/router'], function (core, router) {

    // We need the current usertoken to add to the header each time a REST service request is called.
    // If no token is currently stored, we auto-direct to the login page
    // *NOTE: We don't need to get a token for the actual login process (as no token would be present as yet)
    if (router.getCurrentPageRef() != 'login') {
        var userToken = core.currentUserToken();
        if (userToken === undefined || userToken === null) {
            router.routeTo('login');
        }
    }

    // Generic Ajax method passing back callbacks to the parent
    function _ajaxRequest(type, url, data) {
        var options = {
            dataType: 'json'
            , contentType: 'application/json; charset=utf-8'
            , cache: false
            , type: type
            , data: data 
            , headers: { 'usertoken': userToken }
        }
        var ajaxCall = $.ajax(url, options)
        return ajaxCall;
    }

    //  Public Interface
    return {
        ajaxRequest: _ajaxRequest
    }
});