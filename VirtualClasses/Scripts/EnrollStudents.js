$(document).ready(function () {

    function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    $("#emailid").blur(function () {
        var e = $("#emailid").val();
        if (!validateEmail(e)) {
            $("#emailalert").html("Invalid Email address").removeClass("hidden");
            return false;
        } else {
            $("#emailalert").removeClass("hidden").addClass("hidden");
        }
        return true;
    });

    $("#idform1").submit(function () {
        var e = $("#emailid").val();
        if (!validateEmail(e)) {
            $("#emailalert").html("Invalid Email address").removeClass("hidden");
            return false;
        }
        return true;
    });

    $("#uploadform").submit(function () {
        var val = $("#uploadfile").val();
        if (val == "") {
            $("#uploadalert").html("You must choose a file first.").removeClass("hidden");
            return false;
        }
        else {
            var l = val.length;
            if (l < 5 || val[l - 1] != 't' || val[l - 2] != 'x' || val[l - 3] != 't' || val[l - 4] != '.') {
                $("#uploadalert").html("You must choose a text file.").removeClass("hidden");
                return false;
            }
        }
        return true;
    });

    $(document).on('change', '.btn-file :file', function () {
        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [numFiles, label]);
    });

    $(document).ready(function () {
        $('.btn-file :file').on('fileselect', function (event, numFiles, label) {

            var input = $(this).parents('.input-group').find(':text'),
                log = numFiles > 1 ? numFiles + ' files selected' : label;

            if (input.length) {
                input.val(log);
            } else {
                if (log) alert(log);
            }

        });
    });
});