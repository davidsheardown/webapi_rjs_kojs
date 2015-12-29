define(['app/core'], function (core) {

    var routes = {
        login: { html: '/login.html', app: 'views/login' },
        index: { html: '/index.html', app: 'views/index' },
        thankyou: { html: '/profile-thankyou.html', app: 'views/profile-thankyou.html' }
    }

    function getCurrentPageRef() {
        var pageContainer = null;
        var pageScript = window.location.href.match(/[^/]+$/g);
        if (pageScript === null || pageScript === undefined || pageScript == '#') {
            pageScript = 'index.html'; // When we use routing, we can re-factor this, however at the moment the blank page would indiate index in our case
        }
        pageContainer = pageScript.toString().replace(/\.[^/.]+$/, "");
        return pageContainer;
    }

    // ROUTE TO
    // Function simply routes fully to another page (not SPA mode)
    function routeTo(route) {
        if (routes.hasOwnProperty(route)) {
            console.log('router.js: route required', route, routes[route].html);
            window.location.href = routes[route].html;
            //window.location.reload(true);
            return false;
        }
        else {
            throw new Error('Cannot route to: ' + route + ', does not exist');
        }
    }

    // LOAD CONTENT
    // Loads the required page/view into a requested container (SPA mode)
    function loadContainer(container, route) {
        if (routes.hasOwnProperty(route)) {
            var html = routes[route].html;
            var app = routes[route].app;
            $(container).load(html, function () {
                $(document).ready(function () {
                    var mod = require([app]);
                });
            });
        }
        else {
            throw new Error('Cannot route to: ' + route + ', does not exist');
        }
    }

    return {
        getCurrentPageRef: getCurrentPageRef,
        loadContainer: loadContainer,
        routeTo: routeTo
    }
});