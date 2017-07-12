$(document).ready(function () {
    $("#totalquestionsbtn").click(function () {
        var total = parseInt($("#idtotalquestions").val());
        var text = "";
        if (total != 0) {
            text = "<hr/>";
        }
        for (i = 1; i <= total; i++)
        {
            text += "<h3 class=\"myriad-pro-font\">Question # " + i + "</h3>";
            text += "<hr/>";
            text += "<div class=\"form-group tab\">";
            text += "<textarea class=\"form-control\" style=\"width:89.5%;margin-left:5.6%;\" name=\"Question" + i + "\" placeholder=\"Question\" rows=\"5\"></textarea>";
            text += "</div>";
            text += "<div class=\"form-inline tab\">";
            text += "<div class=\"form-group\">";
            text += "<input type=\"text\" style=\"margin-left: 50%; width: 150%\" class=\"form-control\" name=\"Q" + i + "OptionA\" placeholder=\"Option A\"></input>";
            text += "</div>";
            text += "<div class=\"form-group\" style=\"margin-left:36%\">";
            text += "<input type=\"text\" style=\"right:20px;width: 150%\" class=\"form-control\" name=\"Q" + i + "OptionB\" placeholder=\"Option B\"></input>";
            text += "</div></div>";
            text += "<div class=\"form-inline tab\" style=\"margin-top:10px\"><div class=\"form-group\">";
            text += "<input type=\"text\" style=\"margin-left: 50%;width:150%\" class=\"form-control\" name=\"Q" + i + "OptionC\" placeholder=\"Option C\"></input>";
            text += "</div><div class=\"form-group\" style=\"margin-left:36%\">";
            text += "<input type=\"text\" style=\"right:20px;width:150%\" class=\"form-control\" name=\"Q" + i + "OptionD\" placeholder=\"Option D\"></input>";
            text += "</div>";
            text += "<div style=\"margin-top:10px;width:45%;margin-left:25%;\">";
            text += "<select name=\"Correct" + i + "\" class=\"form-control\">";
            text += "<option value=\"1\">Option A</option>";
            text += "<option value=\"2\">Option B</option>";
            text += "<option value=\"3\">Option C</option>";
            text += "<option value=\"4\">Option D</option>";
            text += "</select>";
            text += "</div>";
            text += "</div>";
            text += "<hr/>";
        }
        if (total != 0) {
            text += "<button class=\"btn btn-default\" style=\"margin-left:11.2%;\" type=\"submit\">Add Quiz</button>";
        }
        $("#idform").html(text);
    });
});