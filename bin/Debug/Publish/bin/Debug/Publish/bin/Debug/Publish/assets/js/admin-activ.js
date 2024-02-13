$(document).ready(function () {
    if (sessionStorage["logged"] != null)
        if (JSON.parse(sessionStorage["logged"]).userName == "admin") {
            let a = $('<a>').attr('href', 'admin-panel.html');
            a.text('Admin Panel');
            $(".dropdown-content").append(a);
        }

});