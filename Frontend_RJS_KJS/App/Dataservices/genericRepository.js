// genericRepository.JS
// Inherits the base dataservice.js generic Ajax method, and provides a generic set of repository functions

define(['dataservices/dataservice'], function (dataservice) {

    //  Set any class/static vars
    function routeUrl(route, id) {
        return route + (id || "")
    }

    //  Set the instance function
    function dataservice_base(setRoute) {

        var self = this;

        //  Set the route or throw error if not defined
        if (setRoute === undefined) {
            throw new Error('genericRepository does not have a valid route passed: ' + self.name);
        }
        self.route = setRoute;

        self.getAllEntities = function (callbacks) {
            return dataservice.ajaxRequest('GET', self.route)
            .done(callbacks.done)
            .fail(callbacks.fail)
        }

        self.getEntitiesById = function (id, callbacks) {
            return dataservice.ajaxRequest('GET', routeUrl(self.route, id))
            .done(callbacks.done)
            .fail(callbacks.fail)
        }

        self.putEntity = function (id, data, callbacks) {
            return dataservice.ajaxRequest('PUT', routeUrl(self.route, id), data)
            .done(callbacks.done)
            .fail(callbacks.fail)
        }

        self.postEntity = function (data, callbacks) {
            return dataservice.ajaxRequest('POST', routeUrl(self.route), data)
            .done(callbacks.done)
            .fail(callbacks.fail)
        }

        self.deleteEntity = function (id, callbacks) {
            return dataservice.ajaxRequest('DELETE', routeUrl(self.route, id))
            .done(callbacks.done)
            .fail(callbacks.fail)
        }

    } // eof instance

    return dataservice_base;
});