
var app = {
    // Application Constructor
    initialize:function () {
        this.bindEvents();
        this.user = new User();
        this.baseUrl = $("#txtUrl").val();
    },
    // Bind Event Listeners
    //
    // Bind any events that are required on startup. Common events are:
    // 'load', 'deviceready', 'offline', and 'online'.
    bindEvents:function () {
        document.addEventListener('deviceready', this.onDeviceReady, false);
    },
    addlog:function (log) {
        $("#result").html($("#result").html() + "<br/>" + log);
    },
    storeToken:function (deviceId) {
        $.ajax({url:app.baseUrl + 'Home/Register',
            dataType:'json', success:function (e) {
                app.user.userId = parseInt(e);
                app.user.save();
            }, contentType:'application/json',
            type:'POST', error:oops, async:true,
            data:JSON.stringify({userId:app.user.userId, deviceId:deviceId, deviceType:cordova.platformId})
        })
    },

    // deviceready Event Handler
    //
    // The scope of 'this' is the event. In order to call the 'receivedEvent'
    // function, we must explicitly call 'app.receivedEvent(...);'
    onDeviceReady:function () {
        app.receivedEvent('deviceready');
        window.plugins.pushNotification.register(function (status) {
            app.last = status;
            app.storeToken(status);
        }, function (st) {
            console.log(st);
        }, {alert:true});
    },
    // Update DOM on a Received Event
    receivedEvent:function (id) {
        var parentElement = document.getElementById(id);
        var listeningElement = parentElement.querySelector('.listening');
        var receivedElement = parentElement.querySelector('.received');

        listeningElement.setAttribute('style', 'display:none;');
        receivedElement.setAttribute('style', 'display:block;');

        console.log('Received Event: ' + id);
    }
};
