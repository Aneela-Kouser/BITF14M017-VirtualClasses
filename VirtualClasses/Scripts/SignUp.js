$(document).ready(function () {

    var perrorshown = false, uerrorshown = false, eerrorshown = false;

    function checkFname(){
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

    function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    function checkEmail() {
        var e = $("#emailid").val();
        var error = false;
        if (e == "") {
            error = true;
            $("#emailalert").html("You must enter your Email").removeClass("hidden");
        } else if (!validateEmail(e)) {
            error = true;
            $("#emailalert").html("Invalid Email address").removeClass("hidden");
        } else {
            //$.getJSON("/Home/DoesEmailExist?em=" + e, function (un) {
            //    if (un) {
            //        error = true;
            //        $("#emaildiv").removeClass("has-error has-success").addClass("has-error");
            //        $("#emailalert").html("Email \"" + $("#emailid").val() + "\" is already registered").removeClass("hidden");
            //    } else {
            //        $("#emailalert").removeClass("hidden").addClass("hidden");
            //        $("#emaildiv").removeClass("has-error has-success").addClass("has-success");
            //    }
            //    return !error;
            //});
            $.ajax({
                url: "/Home/DoesEmailExist",
                datatype: "json",
                type: 'get',
                data: {
                    em : e
                },
                asynch: false,
                success: function (un) {
                    if (un) {
                        error = true;
                        $("#emaildiv").removeClass("has-error has-success").addClass("has-error");
                        $("#emailalert").html("Email \"" + $("#emailid").val() + "\" is already registered").removeClass("hidden");
                    } else {
                        $("#emailalert").removeClass("hidden").addClass("hidden");
                        $("#emaildiv").removeClass("has-error has-success").addClass("has-success");
                    }
                    return !error;
                },
                error: function (error) {
                    $("#emailalert").html("Could not proceed Request").removeClass("hidden");
                    $("#emaildiv").removeClass("has-error has-success");
                }
            });
        }
        if (error) {
            $("#emaildiv").removeClass("has-error has-success").addClass("has-error");
        } else {
            $("#emailalert").removeClass("hidden").addClass("hidden");
            $("#emaildiv").removeClass("has-error has-success").addClass("has-success");
        }
        return !error;
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
        if (!usernameBasicChecks())
            err = false;
        if (!checkPassword())
            err = false;
        if (!checkRepass())
            err = false;
        return err;
    });

});