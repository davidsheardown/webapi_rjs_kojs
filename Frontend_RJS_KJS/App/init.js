requirejs.config({

    baseUrl: '/',

    paths: {
        app: 'App',
        dataservices: 'App/Dataservices',
        viewmodels: 'App/Viewmodels',
        utils: 'App/Utils',
        views: 'App/Views',

        'jquery': 'Scripts/jquery-2.1.4.min',
        //'jqueryui': 'libs/jquery-ui-1.9.2.min',
        'toastr': 'Scripts/toastr.min',
        //'jlinq': 'libs/jlinq',
        //'json2': 'libs/json2',
        'knockout': 'Scripts/knockout-3.4.0',
        'knockout-mapping': 'Scripts/knockout.mapping-latest',
        'knockout-validation': 'Scripts/knockout.validation.min',
        'amplify': 'Scripts/amplify.min',
        //'jquery.ui.widget': 'libs/jquery.ui.widget',
        //'jquery.fileupload': 'libs/jquery.fileupload'
    },

    shim: {
        //'jqueryui': { deps: ['jquery'], exports: 'jqueryui' },
        //'jlinq': { deps: [], exports: 'jlinq' },
        'amplify': { deps: ['jquery'], exports: 'amplify' },
        'knockout-validation': { deps: ['knockout'], exports: 'knockout-validation' },
        //'jquery.ui.widget': { deps: ['jquery'], exports: 'jquery.ui.widget' },
        //'jquery.fileupload': { deps: ['jquery', 'jquery.ui.widget'], exports: 'fileupload' }
    }
});

//  Get the current page (HTML) name which allows us to execute the correct load script for the current page
var pageContainer = null;
var pageScript = window.location.href.match(/[^/]+$/g);
if (pageScript === null || pageScript === undefined || pageScript == '#') {
    pageScript = 'index.html'; // When we use routing, we can re-factor this, however at the moment the blank page would indiate index in our case
}
pageContainer = pageScript.toString().replace(/\.[^/.]+$/, "");

requirejs(['views/'+pageContainer]);