define(['utils/appFunc','utils/tplManager'],function(appFunc,TM){

    var contactView = {

        init: function(params){
            appFunc.bindEvents(params.bindings);
        },

        beforeLoadContacts: function(){
           
        },

        render: function(params){
  

        },

        filterResult: function(e){
            $$('.contacts-list .list-group-title').each(function(){
                if($$(this).next('.contact-item').css('display') === 'none'){
                    $$(this).hide();
                }
            });
        }

    };

    return contactView;
});