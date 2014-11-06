define([],function(){
    var that = {
        log: function (log) {
            var result = $$("#result");
            if (typeof log == 'object') log = JSON.stringify(log);
            result.html(result.html() + "<br/>" + log);
            console.log(log);
            this.list.push(log);
            localStorage.setItem("logs", JSON.stringify(that.list));

        },
        clear: function () {
            localStorage.removeItem("logs");
            this.list = [];
        }
    }
    that.list = [];
    var savedValue = JSON.parse(localStorage.getItem("logs"));
    if (Array.isArray(savedValue))
        that.list = savedValue;
    console.log('log created');
    return that;
})

