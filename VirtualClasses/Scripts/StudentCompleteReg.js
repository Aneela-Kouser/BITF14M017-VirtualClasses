$(document).ready(function () {

    var perrorshown = false, uerrorshown = false, eerrorshown = false;

    function checkFname() {
        if ($("#fname").val() == "") {
            $("#fnamediv").removeClass("has-error has-success").addClass("has-error");
            $("#fnamealert").html("You must enter First Name").removeClass("hidden");
            return false;
        }
        else {
            $("#fnamediv").removeClass("has-error has-success").addClass("has-success");
            $("#fnamealert").removeClass("hidden").addClass("hidden");
            return true;
        }
    }

    function checkLname() {
        if ($("#lname").val() == "") {
            $("#lnamediv").removeClass("has-error has-success").addClass("has-error");
            $("#lnamealert").html("You must enter First Name").removeClass("hidden");
            return false;
        }
        else {
            $("#lnamediv").removeClass("has-error has-success").addClass("has-success");
            $("#lnamealert").removeClass("hidden").addClass("hidden");
            return true;
        }
    }

    function usernameBasicChecks() {
        var u = $("#usernameid").val();
        var error = false;
        if (u == "") {
            error = true;
            $("#useralert").html("You must choose a Username").removeClass("hidden");
        }
        if (error) {
            $("#usernamediv").removeClass("has-error has-success").addClass("has-error");
        }
        return !error;
    }

    function checkUsername() {
        var u = $("#usernameid").val();
        var error = false;
        if (u == "") {
            error = true;
            $("#useralert").html("You must choose a Username").removeClass("hidden");
        }
        else {
            //$.getJSON("/Home/DoesUsernameExist?un="+u, function (un) {
            //    if (un) {
            //        error = true;
            //        $("#usernamediv").removeClass("has-error has-success").addClass("has-error");
            //        $("#useralert").html("Username \"" + $("#usernameid").val() + "\" already exists").removeClass("hidden");
            //    } else {
            //        $("#useralert").removeClass("hidden").addClass("hidden");
            //        $("#usernamediv").removeClass("has-error has-success").addClass("has-success");
            //    }
            //    return !error;
            //});

            $.ajax({
                url: "/Home/DoesUsernameExist",
                datatype: "json",
                type: 'get',
                data: {
                    un: u
                },
                asynch: false,
                success: function (un) {
                    if (un) {
                        error = true;
                        $("#usernamediv").removeClass("has-error has-success").addClass("has-error");
                        $("#useralert").html("Username \"" + $("#usernameid").val() + "\" already exists").removeClass("hidden");
                    } else {
                        $("#useralert").removeClass("hidden").addClass("hidden");
                        $("#usernamediv").removeClass("has-error has-success").addClass("has-success");
                    }
                    return !error;
                },
                error: function (error) {
                    $("#useralert").html("Could not proceed Request").removeClass("hidden");
                    $("#usernamediv").removeClass("has-error has-success");
                }
            });

        }
        if (error) {
            $("#usernamediv").removeClass("has-error has-success").addClass("has-error");
        } else {
            $("#useralert").removeClass("hidden").addClass("hidden");
            $("#usernamediv").removeClass("has-error has-success").addClass("has-success");
        }
        return !error;
    }

    function checkPassword() {
        var error = false;
        var e = $("#passid").val();
        if (e == "") {
            error = true;
            $("#passalert").html("You must choose a password").removeClass("hidden");
        } else if (e.length < 8) {
            error = true;
            $("#passalert").html("Password should be atleast 8 characters long").removeClass("hidden");
        }
        if (error) {
            $("#passdiv").removeClass("has-error has-success").addClass("has-error");
        } else {
            $("#passalert").removeClass("hidden").addClass("hidden");
            $("#passdiv").removeClass("has-error has-success").addClass("has-success");
        }
        if ($("#repassdiv").hasClass("has-success") || $("#repassdiv").hasClass("has-error")) {
            error = !checkRepass();
        }
        return !error;
    }

    function checkRepass() {
        if ($("#repassid").val().length < 8 || $("#repassid").val() != $("#passid").val()) {
            $("#repassalert").html("Passwords do not match").removeClass("hidden");
            $("#repassdiv").removeClass("has-error has-success").addClass("has-error");
            return false;
        }
        else {
            $("#repassalert").removeClass("hidden").addClass("hidden");
            $("#repassdiv").removeClass("has-error has-success").addClass("has-success");
            return true;
        }
    }


    function checkPin() {
        if ($("#emailconfirm").val() == "") {
            $("#emailconfirmalert").html("You must Enter Your Email Confirmation PIN").removeClass("hidden");
            $("#emailconfirmdiv").removeClass("has-error has-success").addClass("has-error");
            return false;
        }
        else {
            $("#emailconfirmalert").removeClass("hidden").addClass("hidden");
            $("#emailconfirmdiv").removeClass("has-error has-success").addClass("has-success");
            return true;
        }
    }

    $("#emailconfirm").blur(function () {
        checkPin();
    });

    $("#fname").blur(function () {
        checkFname();
    });

    $("#lname").blur(function () {
        checkLname();
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

    $("#completeRegForm").submit(function () {
        var err = true;
        if (!checkFname())
            err = false;
        if (!checkLname())
            err = false;
        if (!usernameBasicChecks())
            err = false;
        if (!checkPassword())
            err = false;
        if (!checkPin())
            err = false;
        if (!checkRepass())
            err = false;
        return err;
    });

});