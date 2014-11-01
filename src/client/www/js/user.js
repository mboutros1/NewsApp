function User() {
    var savedValue = JSON.parse(localStorage.getItem('user'));
    $.extend(this, savedValue);
    if (typeof(this.userId) == 'undefined' || typeof(this.userId) != 'number')
        this.userId = 0;
    this.save = function()
    {
        localStorage.setItem('user', JSON.stringify(this));
    }
}