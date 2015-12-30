define(['knockout', 'knockout-mapping', 'dataservices/genericRepository', 'app/core', 'app/router'], function (ko, koMapping, genericRepository, core, router) {

    //  Set the service route(s)
    var dataservice_index = new genericRepository('http://localhost:8081/api/v1/oumember/');

    // Define member model
    function member() {
        this.Id = 0;
        this.Firstname = null;
        this.Lastname = null;
        this.Age = null;
    }

    var viewModelList = {
        selectedItemIndexval: ko.observable(1),
    }

    var viewModel = {

        //  Even though our data is coming via the mapping, we need to add certain validation rules
        //memberDOBDay: ko.observable().extend({ minLength: 2, maxLength: 2 }),

        members: ko.observableArray([]),

        add: function () {
            viewModel.members.push(new member());
        },

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

        cancel: function () {
            router.routeTo('login');
        }
    };

    var viewModelFunctions = {
        saveModel: function () {
            viewModel.save();
        }
    }

    function koMapData(datasource) {
        viewModel.members = ko.observableArray(ko.toJS(datasource));
        ko.applyBindings(viewModel, $('#koMembersTable')[0]);
    };

    return {
        viewModelFunctions: viewModelFunctions,
        loadViewModel: koMapData
    }

});