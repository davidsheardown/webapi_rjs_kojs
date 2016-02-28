define(['dataservices/genericRepository', 'viewmodels/index-vm', 'app/core'], function (genericRepository, index_vm, core) {

    // Define the endpoint to get our members (in this case)
    var dataservice_index = new genericRepository('http://localhost:8081/api/v1/oumember');

    // From the generic AJAX repository, we want to get all members.  If the data loads correctly, we issue the 
    // "index_vm.loadViewModel" function passing the loaded data which will populate the viewmodel and allow us to proceed from there.
    dataservice_index.getAllEntities({
        done: function (data) {
            index_vm.loadViewModel(data);
        },
        fail: function (error) {
            if (error.status === 401 || error.status === 403 || error.status == 404) {
                core.notify.error('Currently not logged in, please login again');
                core.login();
            }
            else {
                core.notify.error(error.statusText);
            }
            return;
        }
    });
});