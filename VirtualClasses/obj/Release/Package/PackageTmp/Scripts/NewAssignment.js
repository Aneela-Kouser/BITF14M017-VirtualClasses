$(document).ready(function func() {
    $("#idform").on("click", function setTotalQuestions() {
        var total = 0;
        total = parseInt(document.getElementById("idtotalquestions").value);
        var text = "";
        if (total != 0) {
            text = "<hr/>";
        }
        for (i = 1; i <= total; i++) {
            text += "<h3>Question # " + i + "</h3>";
            text += "<hr/>";
            text += "<div class=\"form-group tab\">";
            text += "<label for=\"question" + i + "\" style=\"margin-left:12.6%;\">Question</label>";
            text += "<textarea class=\"form-control\" style=\"width:84.5%;margin-left:12.6%;\" name=\"question" + i + "\" placeholder=\"Question\" rows=\"5\"></textarea>";
            text += "</div>";
            text += "<hr/>";
        }
        if (total != 0) {
            text += "<button class=\"btn btn-default\" style=\"margin-left:11.2%;\" type=\"submit\">Add Assignment</button>";
        }
        document.getElementById("idform").innerHTML = text;
    })
});