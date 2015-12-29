
define(['knockout', 'knockout-validation', 'dataservices/genericRepository', 'app/core'], function (ko, kovalid, genericRepository, core) {

    var viewModel = ko.validatedObservable({

        loginUsername: ko.observable().extend({ required: { message: 'Please supply your username/email.' }, email: true }),
        loginPassword: ko.observable().extend({ required: { message: 'Please supply a password.' } }),

        login: function () {

            //  Validate before attempting login
            if (!viewModel.isValid()) {
                for (var i = 0; i <= viewModel.errors.length + 1 ; i++) {
                    if (viewModel.errors()[i] !== undefined) {
                        core.notify.error("You must provide a username and password");
                    }
                }
                return null;
            }

            //  Ok, proceed to check login credentials
            var vmDTO = ko.toJSON(viewModel);

            dataservice_login = new genericRepository('http://localhost:8081/api/v1/User');
            dataservice_login.postEntity(vmDTO, {
                done: function (loginObject) {

                    //  Login successful, cache returned login object
                    var bChk = core.saveLoginObject(loginObject);

                    //  Trigger the success publish - see login.js for continuation
                    amplify.publish('loginSuccess');
                },
                fail: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status === 401 || xhr.status === 403 || xhr.status === 404) { // Not authorized/cannot find login credentials
                        amplify.publish('loginFail', 'Login Failed for provided credentials');
                    }
                    else {
                        throw new Error('viewmodel_login: ' + thrownError, xhr, ajaxOptions);
                    }
                }
            });
        }
    });

    //viewModel.errors = ko.validation.group(viewModel);
    ko.applyBindings(new viewModel());
});