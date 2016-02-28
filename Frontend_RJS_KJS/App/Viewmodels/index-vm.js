define(['knockout', 'knockout-mapping', 'dataservices/genericRepository', 'app/core', 'app/router'], function (ko, koMapping, genericRepository, core, router) {

    //  Create a reference/instance of the generic JQ AJAX repository (handles most of the common get/put/post etc.)
    var dataservice_index = new genericRepository('http://localhost:8081/api/v1/oumember/');

    // Define member model.
    // We get the actual model/data (see koMapData function below), however to generate a new row, we need a JS based model representation
    // for KOJS.
    function member() {
        this.Id = 0;
        this.Firstname = null;
        this.Lastname = null;
        this.Age = null;
    }


    // Main ViewModel for KOJS.
    // "members" is an observable array which (if records exist) is populated by the "koMapData" function below.  This array can be initially blank/initialised,
    // and new records can be added because we have defined a "member model" above.
    var viewModel = {

        //  Even though our data is coming via the mapping, we need to add certain validation rules
        //memberDOBDay: ko.observable().extend({ minLength: 2, maxLength: 2 }),

        // Members collection
        members: ko.observableArray([]),

        // Add new member row
        add: function () {
            viewModel.members.push(new member());
        },

        // Remove member - which also issues a "delete" function on the actual data.  Technically you could allow the delete from
        // the viewmodel and a confirm to delete the actual data (which allows for an undo).
        remove: function (item) {
            if (item.Id > 0) {
                dataservice_index.deleteEntity(item.Id, {
                    done: function (data) {
                        core.notify.info('Removed item successfully');
                    }
                });
            }
            viewModel.members.remove(item);
        },

        // Save a member row - this is triggered from the HTML.  If this is a new member (i.e. data.Id = 0) then it will be created otherwise
        // an update will be issued
        save: function (data, event) {

            if (data.Id == 0) {
                dataservice_index.postEntity(koMapping.toJSON(data), {
                    done: function (boolResponse) {
                        if (boolResponse) {
                            core.notify.info(data.Firstname + ' ' + data.Lastname + ' created successfully');
                            return;
                        }
                        else {
                            core.notify.error('Cannot add/save item at this time');
                        }
                    }
                });
            }
            else {
                dataservice_index.putEntity(data.Id, koMapping.toJSON(data), {
                    done: function (boolResponse) {
                        if (boolResponse) {
                            core.notify.info(data.Firstname + ' ' + data.Lastname + ' updated successfully');
                            return;
                        }
                        else {
                            core.notify.error('Could not update/save at this time');
                            return;
                        }
                    },
                    fail: function (err) {
                        core.notify.error('Could not update/save at this time, please retry');
                        console.log('index-vm', err);
                        return;
                    }
                });
            }
        },

        // Simply return to the login screen
        cancel: function () {
            router.routeTo('login');
        }
    };

    // This provides a set of addition functions that can be grouped together (as an example) and called from another module.
    var viewModelFunctions = {
        saveModel: function () {
            viewModel.save();
        }
    }

    // Maps the incoming data (datasource) into the viewmodel "members" observable array and binds the viewmodel to the HTML
    // section required - in this case there is a DIV called "koMembersTable" (0=first instance of this which is should always be)
    function koMapData(datasource) {
        viewModel.members = ko.observableArray(ko.toJS(datasource));
        ko.applyBindings(viewModel, $('#koMembersTable')[0]);
    };

    // Because RequireJS uses the revealing module pattern, we return the actual functions externally that map to what we want internally within 
    // this module.  Technically you could call the functions the same!
    return {
        viewModelFunctions: viewModelFunctions,
        loadViewModel: koMapData
    }

});