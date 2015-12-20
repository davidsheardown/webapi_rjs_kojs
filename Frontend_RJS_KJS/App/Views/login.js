
define(['app/router', 'viewmodels/login-vm', 'app/core'], function (router, login_vm, core) {

    amplify.subscribe('loginSuccess', function () {
        console.log('Logged in successfully');
        core.notify.info('User logged in successfully');
        router.routeTo('index');
    });

    amplify.subscribe('loginFail', function (errorDesc) {
        console.log('* Login failed: ', errorDesc);
        core.notify.error('Login failed for the username/password combination');
    });

});