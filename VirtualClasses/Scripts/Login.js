$(document).ready(function () {
    function checkUsername() {
        if ($("#user").val() == "") {
            $("#usernamediv").removeClass("has-error has-success").addClass("has-error");
            $("#useralert").html("You must enter your Username").removeClass("hidden");
            return false;
        }
        else {
            $("#usernamediv").removeClass("has-error has-success").addClass("has-success");
            $("#useralert").removeClass("hidden").addClass("hidden");
            return true;
        }
    }
    function checkPassword() {
        if ($("#pass").val() == "") {
            $("#passdiv").removeClass("has-error has-success").addClass("has-error");
            $("#passalert").html("You must enter your Password").removeClass("hidden");
            return false;
        }
        else {
            $("#passdiv").removeClass("has-error has-success").addClass("has-success");
            $("#passalert").removeClass("hidden").addClass("hidden");
            return true;
        }
    }
    $("#user").blur(function () {
        checkUsername();
    });
    $("#pass").blur(function () {
        checkPassword();
    });
    $("#loginform").submit(function () {
        var err = true;
        if (!checkUsername())
            err = false;
        if (!checkPassword())
            err = false;
        return err;
    });
});