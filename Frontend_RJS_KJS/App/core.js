// CORE.JS
// Core functions that apply to the app in general, and included within all modules
// Always includes JQUERY, TOASTR (Notifications) and AMPLIFY for Pub/Sub and Local Storage

define(['jquery', 'toastr', 'amplify'], function ($, toastr, amplify) {

    // Loosely couple TOASTR lib in case we need to change.  Set defaults for TOASTR
    toastr.options.timeOut = 3000;
    toastr.options.positionClass = 'toast-bottom-right';
    notify = toastr;

    // Setup default local cache expiry default
    amplifyExpiry = { expires: 20000 }

    // Amplify cache types
    var cacheType = {
        none: 0,
        expires: 1,
        session: 2
    }

    // Save to local cache:
    // id = cache item id
    // cacheItem = actual item to cache locally
    // cacheType = none (generic), expires (cache for limited time), session (current session only)
    function saveCache(params) {
        if (typeof (params) != 'object') {
            return false;
        }
        var settings = {
            id: null,
            cacheItem: null,
            cacheType: cacheType.None
        }
        $.extend(settings, params);
        if (settings.id === null || settings.cacheItem === null) {
            return false;
        }
        switch (settings.cacheType) {
            case cacheType.session:
                amplify.store.sessionStorage(settings.id, params.cacheItem);
                return true;
            case cacheType.expires:
                amplify.store(params.id, settings.cacheItem, amplifyExpiry);
                return true;
            default:
                amplify.store(params.id, settings.cacheItem);
                return true;
        }
    }

    // Retrieve item from cache
    function loadCache(params) {
        if (typeof (params) != 'object') {
            return false;
        }
        var settings = {
            id: null,
            cacheType: cacheType.session,
            selectedItem: null
        }
        $.extend(settings, params);
        if (settings.id === null) {
            return false;
        }
        var storedItem = null;
        if (settings.cacheType === cacheType.session) {
            storedItem = amplify.store.sessionStorage(params.id);
        }
        else {
            storedItem = amplify.store(settings.id);
        }
        if (typeof (storedItem) != 'object') {
            return storedItem;
        }
        else {
            if (settings.selectedItem === null) {
                return storedItem;
            }
            else {
                return storedItem[settings.selectedItem];
            }
        }
    }

    // HELPER FUNCTIONS

    // Get current user ID from cache
    function currentUser() {
        return loadCache({
            id: 'login'
        });
    }

    // Retrieve cached user token
    function currentUserToken() {
        return loadCache({
            id: 'login',
            selectedItem: 'UserGuid'
        });
    }

    // Save login info object
    function saveLoginObject(loginObject) {
        var bok = saveCache({
            id: 'login',
            cacheItem: loginObject,
            cacheType: cacheType.session
        });
        return bok;
    }

    // Clear login cache object, redirect to login page
    function restartLogin(sessionExpired) {
        amplify.store.sessionStorage('login', null); // Clear the session local storage
        if (sessionExpired === true) { // If we want to show the session expired notice and wait..
            setTimeout(restartLoginRedirect, 4000); // Wait a bit before redirecting
        }
        else {
            login()
        }
    }
    function login() {
        window.location = '/login.html';
    }

    // AJAX error helper
    var genericAjaxErrorCheck = function (error) {
        if (error.status === 401 || error.status === 403) {
            return; // Do nothing as this is handled in the dataservice.js function
        }
        else {
            console.log(error);
            return;
        }
    }

    //  PUBLIC INTERFACE
    return {
        login: login,
        saveLoginObject: saveLoginObject,
        currentUser: currentUser,
        currentUserToken: currentUserToken,
        cacheType: cacheType,
        saveCache: saveCache,
        loadCache: loadCache,
        notify: notify,
        genericAjaxErrorCheck: genericAjaxErrorCheck
    }
});