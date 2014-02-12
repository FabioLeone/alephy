$(function () {
    var $txt = $(".cond");
    var $txt2 = $(".marg");

    $txt.keydown(function (e) {
        if (e.which == 13) {
            var cond = $(this).val();
            var di = $(this).attr('tabindex');
            var uid = $(this).parent().parent().children(':first').text();
            var ipt = $(this).parent().parent().children(':nth-child(' + ($(this).parent().index() + 2) + ')').find('input');

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../controls/factors_set.aspx/upcond",
                data: "{'cnd':'" + cond + "','di':'" + di + "','uid':'" + uid + "'}",
                dataType: "json",
                success: function (result) {
                    ipt.focus();
                }
            });
        }
        else {
            return $txt.val();
        }
    })
    
    $txt2.keydown(function (e) {
        if (e.which == 13) {
            var marg = $(this).val();
            var di = $(this).attr('tabindex');
            var uid = $(this).parent().parent().children(':first').text();
            var ipt = $(this).parent().parent().children(':nth-child(' + ($(this).parent().index() + 2) + ')').find('input');

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../controls/factors_set.aspx/upmarg",
                data: "{'mrg':'" + marg + "','di':'" + di + "','uid':'" + uid + "'}",
                dataType: "json",
                success: function (result) {
                    ipt.focus();
                }
            });
        }
        else {
            return $txt.val();
        }
    })
});