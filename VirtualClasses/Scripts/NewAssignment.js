$(document).ready(function() {
    $("#totalquestionsbtn").click(function () {
        var total = parseInt($("#idtotalquestions").val());
        var text = "";
        if (total != 0) {
            text = "<hr/>";
        }
        for (i = 1; i <= total; i++) {
            text += "<h3 class=\"myriad-pro-font\">Question # " + i + "</h3>";
            text += "<hr/>";
            text += "<div class=\"form-group tab\">";
            text += "<textarea class=\"form-control\" style=\"width:89.5%;margin-left:5.6%;\" name=\"Question" + i + "\" placeholder=\"Question #" + i + "\" rows=\"5\"></textarea>";
            text += "</div>";
        }
        if (total != 0) {
            text += "<button class=\"btn btn-default\" style=\"margin-left:11.2%;\" type=\"submit\">Add Assignment</button>";
        }
        $("#idform").html(text);
    });
});