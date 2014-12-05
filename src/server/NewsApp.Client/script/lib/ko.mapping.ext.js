ko.virtualElements.allowedBindings.formatNumber = true;
ko.virtualElements.allowedBindings.formatAvatar = true;
ko.virtualElements.allowedBindings.formatDate = true;
ko.bindingHandlers.formatNumber = {
    init: function (element, valueAccessor) {
        var vAccess = valueAccessor();
        ko.utils.unwrapObservable(vAccess); //grab dependency
        var num = parseInt(vAccess());
        var result = "";
        if (num <= 0) result = "";
        else if (num > 0 && num < 1000) result = num;
        else if (num > 999 && num < 1000000) result = parseInt(num / 1000) + 'K';
        else if (num >= 1000000 && num < 1000000000) result = parseInt(num / 1000000) + 'M';
        else if (num >= 1000000000 && num < 1000000000000) result = parseInt(num / 1000000000) + 'T';
        else result = parseInt(num / 1000000000) + 'T';
        element.textContent = result;
    }
};
ko.bindingHandlers.formatAvatar = {
    init: function (element, valueAccessor) {
        //style/img/avatar/avatar' +Avatar() +  '.jpg'
        var vAccess = valueAccessor();
        ko.utils.unwrapObservable(vAccess);  //grab dependency
        var path = vAccess();
        var nullVar = '/null.png';
        var bUrl = 'style/img/avatar/';
        if (path == null || path == '' || path == nullVar)
            path = bUrl + nullVar;
        else if (path.indexOf('FB:') == 0)
            path = 'https://graph.facebook.com/' + path.substr(3) + '/picture?type=small';
        else if (!isUrl(path)) {
            if (path.indexOf('style/') == -1) {
                if (path.indexOf('.') == -1) path += '.jpg';
                path = bUrl + 'avatar' + path;
            }
        }
        element.setAttribute('src', path);
    }
};

ko.bindingHandlers.formatDate = {
    init: function (element, valueAccessor) {
        //style/img/avatar/avatar' +Avatar() +  '.jpg'
        var vAccess = valueAccessor();
        ko.utils.unwrapObservable(vAccess);  //grab dependency
        var path = vAccess();
        var d = new moment(path);

        element.textContent = d.calendar();
    }
};
ko.updateThis = function (newData, element, mapping) {
    var current = ko.dataFor(element);
    var init = current == null;
    current = current || {};
    if (init) {
        if (mapping)
            current = ko.mapping.fromJS(newData, mapping);
        else
            current = ko.mapping.fromJS(newData);
        ko.applyBindings(current, element);
    } else if (newData)
        if (mapping)
            ko.mapping.fromJS(newData, mapping, current);
        else
            ko.mapping.fromJS(newData, current);
    return this._currentComments;
}
function isUrl(str) {
    var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
    '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
    '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
    '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
    '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
    '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator
    if (!pattern.test(str)) {
        return false;
    } else {
        return true;
    }
}