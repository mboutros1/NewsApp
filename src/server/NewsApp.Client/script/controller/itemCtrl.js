define(['utils/appFunc', 'view/module', 'utils/xhr'], function (appFunc, VM, xhr) {

    var itemCtrl = {
        init: function (query) {
            var bindings = [
                {
                    element: '.back2home',
                    event: 'click',
                    handler: VM.module('appView').showToolbar
                }
            ];

            VM.module('itemView').init({
                bindings: bindings,
                query: query, item: query.item
            });

        }
    };

    return itemCtrl;
});