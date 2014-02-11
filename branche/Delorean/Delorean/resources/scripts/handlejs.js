$(function () {
    var $txt = $(".cond");

    $txt.keydown(function (e) {
        if (e.which == 13) {
            var cond = $(this).val();
            var di = $(this).attr('tabindex');
            var uid = $(this).parent().parent().children(':first').text();
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../controls/factors_set.aspx/upcond",
                data: "{'cnd':'" + cond + "','di':'" + di + "','uid':'" + uid + "'}",
                dataType: "json"
            });
        }
        else {
            return $txt.val();
        }
    })
});