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
    register:function(){
        window.plugins.pushNotification.register(function (status) {
            app.last = status;
            app.storeToken(status);
        }, function (st) {
            console.log(st);
        }, {alert:true , sound:true ,badge:true});

    },
    storeToken:function (deviceId) {
        $.ajax({url:app.baseUrl + 'Home/Register',
            dataType:'json', success:function (e) {
                app.user.userId = parseInt(e);
                app.user.save();
            }, contentType:'application/json',
            type:'POST', error:function (st) {
                console.log(st)
            }, async:true,
            data:JSON.stringify({userId:app.user.userId, deviceId:deviceId, deviceType:cordova.platformId})
        })
    },
    onResume: function() {
         // Clear the badge number - if a new notification is received it will have a number set on it for the badge
        app.setBadge(0);
        app.getPending(); // Get pending since we were reopened and may have been launched from a push notification
    },
    // deviceready Event Handler
    //
    // The scope of 'this' is the event. In order to call the 'receivedEvent'
    // function, we must explicitly call 'app.receivedEvent(...);'
    onDeviceReady:function () {
        app.receivedEvent('deviceready');
        app.register();
        document.addEventListener('push-notification', function(event) {
            console.log('RECEIVED NOTIFICATION! Push-notification! ' + event);
            app.addLog(JSON.stringify(['\nPush notification received!', event]));
            // Could pop an alert here if app is open and you still wanted to see your alert
            //navigator.notification.alert("Received notification - fired Push Event " + JSON.stringify(['push-//notification!', event]));
        });
        document.removeEventListener(this.deviceready,'deviceready',  false);
    },
    receiveStatus: function() {
        var pushNotification = window.plugins.pushNotification;
        pushNotification.getRemoteNotificationStatus(function(status) {
            app.addLog(JSON.stringify(['Registration check - getRemoteNotificationStatus', status])+"\n");
        });
    },
    getPending: function() {
        var pushNotification = window.plugins.pushNotification;
        pushNotification.getPendingNotifications(function(notifications) {
            app.addLog(JSON.stringify(['getPendingNotifications', notifications])+"\n");
            console.log(JSON.stringify(['getPendingNotifications', notifications]));
        });
    },
    // Update DOM on a Received Event
    receivedEvent:function (id) {
        var parentElement = document.getElementById(id);
        var listeningElement = parentElement.querySelector('.listening');
        var receivedElement = parentElement.querySelector('.received');

        listeningElement.setAttribute('style', 'display:none;');
        receivedElement.setAttribute('style', 'display:block;');

        console.log('Received Event: ' + id);
    },
    onNotificationGCM: function(e) {
        switch( e.event )
        {
            case 'registered':
                if ( e.regid.length > 0 )
                {
                    console.log("Regid " + e.regid);
                    alert('registration id = '+e.regid);
                }
                break;

            case 'message':
                // this is the actual push notification. its format depends on the data model from the push server
                alert('message = '+e.message+' msgcnt = '+e.msgcnt);
                break;

            case 'error':
                alert('GCM error = '+e.msg);
                break;

            default:
                alert('An unknown GCM event has occurred');
                break;
        }
    }
};
