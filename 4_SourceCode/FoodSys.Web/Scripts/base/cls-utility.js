/*
区域日期
fromDate，toDate：只支持文本框ID

Ex：dateRange("#dateFrom", "#dateTo");
*/
function dateRange(fromDate, toDate) {
    var dates = $("#" + fromDate + ",#" + toDate).datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        dateFormat: 'yy-mm-dd',
        changeMonth: true,
        changeYear: true,
        defaultDate: "+1w",
        numberOfMonths: 1,
        showAnim: 'blind',
        onSelect: function (selectedDate) {
            var option = this.id == fromDate ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
            dates.not(this).datepicker("option", option, date);
        }
    });
    $("#" + fromDate + ",#" + toDate).bind("paste", function () { return false; }).forceReadOnly();
}

//--身份证号码验证-支持新的带x身份证
function isIdCardNo(pId) {
    pId = pId.toLowerCase();
    var arrVerifyCode = [1, 0, "x", 9, 8, 7, 6, 5, 4, 3, 2];
    var wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
    if (pId.length != 15 && pId.length != 18)
        return false;
    var ai = pId.length == 18 ? pId.substring(0, 17) : pId.slice(0, 6) + "19" + pId.slice(6, 16);
    if (!/^\d+$/.test(ai))
        return false;
    var yyyy = ai.slice(6, 10);
    var mm = ai.slice(10, 12) - 1;
    var dd = ai.slice(12, 14);
    var d = new Date(yyyy, mm, dd);
    var now = new Date();
    var year = d.getFullYear();
    var mon = d.getMonth();
    var day = d.getDate();
    if (year != yyyy || mon != mm || day != dd || d > now || year < 1940)
        return false;
    for (var i = 0, ret = 0; i < 17; i++)
        ret += ai.charAt(i) * wi[i];
    ai += arrVerifyCode[ret %= 11];
    return pId.length == 18 && pId != ai ? false : true;
};