define(['dataservices/genericRepository', 'viewmodels/index-vm', 'app/core'], function (genericRepository, index_vm, core) {

    // Get member entities and load the required viewmodel
    var dataservice_index = new genericRepository('http://localhost:8081/api/v1/oumember');
    dataservice_index.getAllEntities({
        done: function (data) {
            index_vm.loadViewModel(data);
        },
        fail: function (error) {
            if (error.status === 401 || error.status === 403 || error.status == 404) {
                core.notify.error('Currently not logged in, please login again');
                //core.login();
            }
            else {
                core.notify.error(error.statusText);
            }
            return;
        }
    });
});