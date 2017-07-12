$(document).ready(function () {

    function checkFname(){
        if ($("#fname").val() == "") {
            $("#fnamediv").removeClass("has-error has-success").addClass("has-error");
            return false;
        }
        else {
            $("#fnamediv").removeClass("has-error has-success").addClass("has-success");
            return true;
        }
    }

    function checkLname() {
        if ($("#lname").val() == "") {
            $("#lnamediv").removeClass("has-error has-success").addClass("has-error");
            return false;
        }
        else {
            $("#lnamediv").removeClass("has-error has-success").addClass("has-success");
            return true;
        }
    }

    function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    function checkEmail() {
        var e = $("#emailid").val();
        if (!validateEmail(e)) {
            $("#emaildiv").removeClass("has-error has-success").addClass("has-error");
            return false;
        }
        else {
            $("#emaildiv").removeClass("has-error has-success").addClass("has-success");
            return true;
        }
    }

    function checkUsername() {
        if ($("#usernameid").val() == "") {
            $("#usernamediv").removeClass("has-error has-success").addClass("has-error");
            return false;
        }
        else {
            $("#usernamediv").removeClass("has-error has-success").addClass("has-success");
            return true;
        }
    }

    function checkPassword() {
        var e = $("#passid").val();
        if (e.length < 8) {
            $("#passdiv").removeClass("has-error has-success").addClass("has-error");
            $("#repassdiv").removeClass("has-error has-success").addClass("has-error");
            $("#passalert").html("Password should be atleast 8 characters long");
            $("#passalert").removeClass("sr-only");
            return false;
        }
        else {
            $("#passdiv").removeClass("has-error has-success").addClass("has-success");
            $("#passalert").removeClass("sr-only").addClass("sr-only");
            if ($("#repassid").val() != $("#passid").val()) {
                $("#repassdiv").removeClass("has-error has-success").addClass("has-error");
                return false;
            }
            else {
                $("#repassdiv").removeClass("has-error has-success").addClass("has-success");
            }
            return true;
        }
    }

    function checkRepass() {
        if ($("#repassid").val().length < 8 || $("#repassid").val() != $("#passid").val()) {
            $("#repassdiv").removeClass("has-error has-success").addClass("has-error");
            return false;
        }
        else {
            $("#repassdiv").removeClass("has-error has-success").addClass("has-success");
            return true;
        }
    }

    $("#fname").blur(function () {
        checkFname();
    });

    $("#lname").blur(function () {
        checkLname();
    });

    $("#emailid").blur(function () {
        checkEmail();
    });

    $("#usernameid").blur(function () {
        checkUsername();
    });

    $("#passid").blur(function () {
        checkPassword();
    });

    $("#repassid").blur(function () {
        checkRepass();
    });

    $("#signupform").submit(function () {
        var err = true;
        if (!checkFname())
            err = false;
        if (!checkLname())
            err = false;
        if (!checkEmail())
            err = false;
        if (!checkUsername())
            err = false;
        if (!checkPassword())
            err = false;
        if (!checkRepass())
            err = false;
        return err;
    });

});