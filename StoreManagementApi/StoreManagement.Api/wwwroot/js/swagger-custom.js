$(document).ready(function () {

    //Definition of the function (non-global, because of the previous line)
    function Update() {
        $("section.models").remove();
        jQuery.each($(".response-col_status"), function (idx, element) {
            if ($(element).text().length > 3 && !isNaN($(element).text())) {
                $(element).attr("original-status", $(element).text())
                $(element).text($(element).text().substring(0, 3));
            }
        });
    }
    //set an interval
    setInterval(Update, 100);
    //Call the function
    Update();
});