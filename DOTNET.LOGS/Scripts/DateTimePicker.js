$(document).ready(function () {
    var currentDateTime = moment().format("YYYY-MM-DD HH:mm");
    $("#startdatetimepicker").val("");
    $("#enddatetimepicker").val("");

    $("#startdatetimepicker").datetimepicker({
        startDate: "-7d",
        endDate: currentDateTime
    }).on("changeDate", function (e) {
        $("#enddatetimepicker").val("");
        $("#enddatetimepicker").datetimepicker("setStartDate", e.date);
    });
    
    $("#enddatetimepicker").datetimepicker({
        startDate: "-7d",
        endDate: currentDateTime
    });

    $("#UserSelectedEnvironment").on("change", function () {
        if ($(this).find("option:selected").text() === "LIVE") {
            $("#startdatetimepicker").datetimepicker("setStartDate",
                moment().subtract(moment.duration(2, "hours")).format("YYYY-MM-DD HH:mm"));
        } else {
            $("#startdatetimepicker").datetimepicker("setStartDate",
                moment().subtract(moment.duration(7, "d")).format("YYYY-MM-DD HH:mm"));
        }
    });
});
