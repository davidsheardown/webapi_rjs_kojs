define(['knockout', 'knockout-mapping', 'dataservices/dataservice-instance', 'app/core', 'app/router'], function (ko, koMapping, dataservice_instance, core, router) {

    //  Set the service route(s)
    var dataservice_index = new dataservice_instance('http://localhost:8081/api/v1/oumember');

    var viewModel = {

        //  Even though our data is coming via the mapping, we need to add certain validation rules
        //memberDOBDay: ko.observable().extend({ minLength: 2, maxLength: 2 }),

        save: function () {
            var saveData = koMapping.toJS(viewModel);
            dataservice_index.putEntity(saveData.Id, koMapping.toJSON(saveData), {
                success: function (data) {
                    //router.routeTo('form2'); // Move to the next profile section
                },
                error: function (err) {
                    core.genericAjaxErrorCheck(error);
                }
            });
        },

        cancel: function () {
             core.restartLogin();
        }
    };

    var viewModelFunctions = {
        saveModel: function () {
            viewModel.save();
        }
    }

    function koMapData(datasource) {
        koMapping.fromJS(datasource, null, viewModel);
        ko.applyBindings(viewModel);
        console.log('index-vm', datasource, viewModel);
    };

    return {
        viewModelFunctions: viewModelFunctions,
        loadViewModel: koMapData
    }

});