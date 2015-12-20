define(['dataservices/dataservice-instance', 'viewmodels/index-vm', 'app/core'], function (dataservice_instance, index_vm, core) {

    // Create a new instance of the dataservice object, passing the base API URL
    var dataservice_index = new dataservice_instance('http://localhost:8081/api/v1/oumember');
    
    // Get member entities and load the required viewmodel
    dataservice_index.getAllEntities({
        done: function (data) {
            index_vm.loadViewModel(data);
            return;
        },
        fail: function (error) {
            if (error.status === 401 || error.status === 403 || error.status == 404) {
                console.log('index.js: cannot load all members, authentication failed');
            }
            else {
                core.notify.error(error.statusText);
            }
            return;
        }
    });
});