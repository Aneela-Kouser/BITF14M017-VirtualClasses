$(document).ready(function () {

    $("#commentid").bind("enterKey", function (e) {
        var comment = $("#commentid").val();
        if (comment != "") {
            $.ajax({
                url: "/Teacher/CommentOnDiscussion",
                datatype: "json",
                type: "post",
                data: {
                    comm: comment
                },
                success: function (d) {
                    $("#commentid").val("");
                    var t = "";
                    if (d.IsCommentOK) {
                        t += "<li class=\"media\">";
                        t += "<div class=\"media-left\">";
                        t += "<span class=\"media-object glyphicon glyphicon-comment\" style=\"font-size:22px;opacity:0.4;margin-top:11px;\"></span>";
                        t += "</div>";
                        t += "<div class=\"media-body\">";
                        t += "<h4 class=\"myriad-pro-font\"><small>" + d.FullName + " (&commat;" + d.Username + ") says:</small></h4>";
                        t += "<p class=\"myriad-pro-font\" style=\"margin-top:-12px\"><small>" + d.CommentTime + "</small></p>";
                        t += "<p class=\"myriad-pro-font\">" + d.CommentBody + "</p>";
                        t += "</div>";
                        t += "</li>";
                        if (d.IsFirstComment) {
                            $("#comments").html(t);
                        } else {
                            $("#comments").append(t);
                        }
                    } else {
                        t += "<div class=\"media\">";
                        t += "<div class=\"media-body\">";
                        t += "<div class=\"form-inline\">";
                        t += "<input id=\"commentid\" type=\"text\" placeholder=\"Say Something\" style=\"width:65%;height:30px;font-size:12px\" class=\"form-control\" />";
                        t += "<p class=\"myriad-pro-font\" style=\"color:red\">*Something went wrong. Can't proceed your request.</p>";
                        t += "</div>";
                        t += "</div>";
                        t += "</div>";
                        $("#abc").html(t);
                    }
                },
                error: function (d) {
                    $("#commentid").val("");
                    var t = "";
                    t += "<div class=\"media\">";
                    t += "<div class=\"media-body\">";
                    t += "<div class=\"form-inline\">";
                    t += "<input id=\"commentid\" type=\"text\" placeholder=\"Say Something\" style=\"width:65%;height:30px;font-size:12px\" class=\"form-control\" />";
                    t += "<p class=\"myriad-pro-font\" style=\"color:red\">*Something went wrong. Can't proceed your request.</p>";
                    t += "</div>";
                    t += "</div>";
                    t += "</div>";
                    $("#abc").html(t);
                }
            });
        }
    });
    $("#commentid").keyup(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger("enterKey");
        }
    });
});