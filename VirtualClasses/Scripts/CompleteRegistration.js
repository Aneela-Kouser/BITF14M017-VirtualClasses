$(document).ready(function () {

    function checkPin(){
        if ($("#pin").val() == "") {
            $("#pindiv").removeClass("has-error").addClass("has-error");
            return false;
        } else {
            $("#pindiv").removeClass("has-error");
            return true;
        }
    }

    $("#pin").blur(function () {
        checkPin();
    });

    $("#comform").submit(function () {
        return checkPin();
    });
});