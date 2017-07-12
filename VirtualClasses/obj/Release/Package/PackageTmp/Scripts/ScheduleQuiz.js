$(document).ready(function () {
    $("#totalquestionsbtn").click(function () {
        var total = parseInt($("#idtotalquestions").val());
        var text = "";
        if (total != 0) {
            text = "<hr/>";
        }
        for (i = 1; i <= total; i++)
        {
            text += "<h3>Question # " + i + "</h3>";
            text += "<hr/>";
            text += "<div class=\"form-group tab\">";
            text += "<label for=\"question" + i + "\" style=\"margin-left:12.6%;\">Question</label>";
            text += "<textarea class=\"form-control\" style=\"width:84.5%;margin-left:12.6%;\" name=\"question" + i + "\" placeholder=\"Question\" rows=\"5\"></textarea>";
            text += "</div>";
            text += "<div class=\"form-inline tab\">";
            text += "<div class=\"form-group\">";
            text += "<label style=\"margin-left:50%;\" for=\"q" + i + "optionA\">Option A</label>";
            text += "<input type=\"text\" style=\"margin-left: 50%\" class=\"form-control\" name=\"q" + i + "optionA\" placeholder=\"Option A\"></input>";
            text += "</div>";
            text += "<div class=\"form-group\" style=\"margin-left:36%\">";
            text += "<label for=\"q" + i + "optionB\">Option B</label>";
            text += "<input type=\"text\" style=\"right:20px;margin-top:-5px;\" class=\"form-control\" name=\"q" + i + "optionB\" placeholder=\"Option B\"></input>";
            text += "</div></div>";
            text += "<div class=\"form-inline tab\" style=\"margin-top:10px\"><div class=\"form-group\">";
            text += "<label  style=\"margin-left:50%;\" for=\"q" + i + "optionC\">Option C</label>";
            text += "<input type=\"text\" style=\"margin-left: 50%\" class=\"form-control\" name=\"q" + i + "optionC\" placeholder=\"Option C\"></input>";
            text += "</div><div class=\"form-group\" style=\"margin-left:36%\">";
            text += "<label for=\"q" + i + "optionD\">Option D</label>";
            text += "<input type=\"text\" style=\"right:20px;margin-top:-5px;\" class=\"form-control\" name=\"q" + i + "optionD\" placeholder=\"Option D\"></input>";
            text += "</div></div>";
            text += "<hr/>";
        }
        if (total != 0) {
            text += "<button class=\"btn btn-default\" style=\"margin-left:11.2%;\" type=\"submit\">Add Quiz</button>";
        }
        $("#idform").html(text);
    });
});