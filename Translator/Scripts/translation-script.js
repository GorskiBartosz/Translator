$("#main-alert").hide();

$("#translateButton").click(
    function () {
        var source = $("#source").val();
        
        $.ajax({
            url: "/Home/Translate/leetspeak/"+source,
            dataType: "json",
            success: function (result) {
                if (result.success) {
                    $("#translation").val(result.content);
                } else {
                    $("#main-alert").show();
                }
            },
        });

        
    }
);

$("#close-main-alert").click(function () {
    $("#main-alert").hide();
});

