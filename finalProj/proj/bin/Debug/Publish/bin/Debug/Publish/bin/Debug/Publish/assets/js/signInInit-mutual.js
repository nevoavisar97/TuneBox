$(document).ready(function () {
    if (sessionStorage['logged'] != null) {
        $(".disconnected").hide();
        $(".connected").show();
        user = JSON.parse(sessionStorage['logged']);
        $(".userAvatar").attr("src", user.avatar);
    }
    else {
        $(".disconnected").show();
        $(".connected").hide();
    }

});