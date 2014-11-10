define(['utils/appFunc', 'utils/xhr', 'view/module', 'GS', 'i18n!nls/lang'], function (appFunc, xhr, VM, GS, i18n) {
    var loginCtrl = {
        init: function () {
            var bindings = [
                       {
                           element: '#submit',
                           event: 'click',
                           handler: loginCtrl.submitFb
                       },
                       {
                           element: '.loginFb',
                           event: 'click',
                           handler: loginCtrl.loginFbSubmit
                       }
            ];
            VM.module('loginView').init({
                bindings: bindings
            });
        },
        loginFbSubmit: function () {
            facebookConnectPlugin.login(['email', 'public_profile', 'user_birthday'], function (event) {
                storage('fbData', { id: event.authResponse.userID });
                logger.log(event);
                logger.log('facebook logged in');
                require('GS').facebookUpdate();
            }, function (event) {
                alert(JSON.stringify(event));
            });
        },
        loginSubmit: function () {
            var loginName = $$('input.login-name').val();
            var password = $$('input.password').val();
            if (loginName === '' || password === '') {
                hiApp.alert(i18n.login.err_empty_input);
            } else if (!appFunc.isEmail(loginName)) {
                hiApp.alert(i18n.login.err_illegal_email);
            } else {
                hiApp.showPreloader(i18n.login.login);
                xhr.simpleCall({
                    func: 'user_login',
                    data: {
                        loginname: loginName,
                        password: password
                    }
                }, function (response) {
                    setTimeout(function () {
                        if (response.err_code === 0) {

                            var login = response.data;
                            GS.setCurrentUser(login.sid, login.user);
                            mainView.loadPage('index.html');
                            hiApp.hidePreloader();
                        } else {
                            hiApp.hidePreloader();
                            hiApp.alert(response.err_msg);
                        }
                    }, 500);

                });
            }
        }
    };
    return loginCtrl;
});