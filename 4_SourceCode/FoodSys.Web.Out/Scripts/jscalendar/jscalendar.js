/*
日历使用:
1.第一个参数为要放入的容器ID
2.第二个参数为回调函数名称
3.要初始化的日期,可以为空,默认为当日
var obj = new HotnetCalendarNameSpace.Calender("positionElementsId", "callbackFunName",date);
obj.showCalendar();
	
回调函数签名:
function callbackFunName(year, month, day) {
		
}

*/

var HotnetCalendarNameSpace = new Object();
var globalCalendarSender;
HotnetCalendarNameSpace.Calender = function (positionElementsId, callbackFunName, date, selectDate) {
    if (date == null)
        this.currentDate = new Date();
    else
        this.currentDate = date;
    this.callback = callbackFunName;
    this.selectDay = selectDate;
    this.positionElementsId = positionElementsId;
}
HotnetCalendarNameSpace.Calender.prototype = {

    /*初始化日历插件
    -----------------------------------*/
    initCalendar: function () {
        var dContent = "";
        var tContent = "";
        var calendarHead = "";
        this.year = this.currentDate.getFullYear();
        $("#" + this.positionElementsId).html("");
        var calendarHead = "<div class='divCalendarHead'>"
        /*最上面选择的年
        ----------------------------------------------*/
                          + "<a href='javascript:void(0)' id='calendarPreYear'><img src='/Content/Images/pre.gif' /></a>"
                          + "<span class='spanCalendarDateInfo'></span>"
                          + "<a href='javascript:void(0)'id='calendarNextYear'><img src='/Content/Images/nxt.gif' /></a>"
                          + "</div>"
        for (var d = 1; d <= 12; d++) {
            /*显示月和每天
            ----------------------------------------------*/

            staticContent = "<table cellspacing='5'>"
            //							    + "<thead>"
                                    + "<tr>"
            /*绑定每个月
            -------------------------------------------------*/
                                    + "<span class='calendarMonth'>" + d + " 月</span>"
                                    + "</tr>"
								    + "<tr>"
									    + "<td style='color:Red;'>日</td>"
										+ "<td>一</td>"
										+ "<td>二</td>"
										+ "<td>三</td>"
										+ "<td>四</td>"
										+ "<td>五</td>"
										+ "<td style='color:Red;'>六</td>"
								    + "</tr>"
            //							    + "</thead>"
                                + "<tr>"
                                + "<td colspan='7'>"
							    + "<table cellspacing='5' class='calendarTbody" + d + "'>"
							    + "</table>"
                                + "</td>"
                                + "</tr>"
							+ "</table>";

            dContent += "<td><div>" + staticContent + "</div></td>";
            if (d % 4 == 0) {
                tContent = "<tr>" + dContent + "</tr>";
                $("#" + this.positionElementsId).append(tContent);
                dContent = "";
                tContent = "";
            }


        }
        /* 在页面上渲染年份calendarHead为页面上DIV的ID
        -------------------------------------------------*/
        $("#calendarHead").html(calendarHead);
        /*初始化年并绑定事件
        -------------------------------------------------*/
        $("#calendarPreYear").click(this.showPreYear);
        $("#calendarNextYear").click(this.showNextYear);
        /*上面放年的span
        ----------------------------------------*/
        $(".spanCalendarDateInfo").html(this.year + "年");
      
    },
    /*显示日历详细的每一天参数:月份,循环次数(绑定第几个spanCalendarDateInfo详细天),
    ----------------------------------------*/
    showCalendar: function () {
        globalCalendarSender = this;
        this.initCalendar();
        this.today = new Date();
        /*一个月的第一天是星期几
        -------------------------------------------------*/
        for (var i = 1; i <= 12; i++) {
            /*this.firstDayMonth = new Date(this.year, this.month - 1, 1);*/
            this.firstDayMonth = new Date(this.year, i - 1, 1);
            this.firstDayMonthOfWeek = this.firstDayMonth.getDay();
            /*清空body calendarTbody为放1-31的table
            -------------------------------------------------*/
            $(".calendarTbody" + i).html();
            //渲染第一行的空行
            var firstRowBankTd = "";
            var firstRowDataTd = "";
            var firstTr = "";
            for (var k = 0; k < this.firstDayMonthOfWeek; k++)
                firstRowBankTd += "<td class='padding'></td>";
            //记录构造哪一天
            var serailDay = 0;
            //第一行剩余格渲染
            var funDateStr = "";
            for (var j = this.firstDayMonthOfWeek; j < 7; j++) {
                serailDay = j - this.firstDayMonthOfWeek + 1;
                funDateStr = "(this," + this.year + "," + i + "," + serailDay + ")";
                if (this.selectDay[i - 1].Value != null) {
                    if ($.inArray(serailDay, this.selectDay[i - 1].Value) >= 0) {
                        firstRowDataTd += "<td class='red' onclick='" + this.callback + funDateStr + "'><span>" + (this.render == null ? serailDay : this.render(this.year, i - 1, serailDay)) + "</span></td>";

                    } else {
                        firstRowDataTd += "<td onclick='" + this.callback + funDateStr + "'><span>" + (this.render == null ? serailDay : this.render(this.year, i - 1, serailDay)) + "</span></td>";
                    }
                }
                else
                    firstRowDataTd += "<td onclick='" + this.callback + funDateStr + "'><span>" + (this.render == null ? serailDay : this.render(this.year, i - 1, serailDay)) + "</span></td>";

            }

            /*星期几第一行前面就空几个td
            --------------------------------------------------------*/
            firstTr = "<tr>" + firstRowBankTd + firstRowDataTd + "</tr>";
            /* calendarTbody为放1-31的table 这里处理并显示前7天(并非前一个礼拜)
            -------------------------------------------------*/
            $(".calendarTbody" + i).html(firstTr);
            //记录行已占用的格子数量
            var fillCount = 0;
            //除了第一行剩余天数
            var remainDay = this.getNumberOfMonth(this.year, i) - (7 - this.firstDayMonthOfWeek);
            var tdContent = "";
            var trContent = "";
            for (var b = 0; b < remainDay; b++) {
                serailDay++;
                fillCount++;
                funDateStr = "(this," + this.year + "," + i + "," + serailDay + ")";

                //            //如果是今天,设置样式
                //            if (this.year == this.today.getFullYear() && this.month == (this.today.getMonth() + 1) && serailDay == this.today.getDate())
                //                tdContent = tdContent + "<td class='today' onclick='" + this.callback + funDateStr + "'><span>" + (this.render == null ? serailDay : this.render(this.year, this.month, serailDay)) + "</span></td>";
                //            else 
                if (this.selectDay[i - 1].Value != null) {
                    if ($.inArray(serailDay, this.selectDay[i - 1].Value) >= 0) {
                        tdContent = tdContent + "<td class='red' onclick='" + this.callback + funDateStr + "'><span>" + (this.render == null ? serailDay : this.render(this.year, i - 1, serailDay)) + "</span></td>";
                    } else {
                        tdContent = tdContent + "<td onclick='" + this.callback + funDateStr + "'><span>" + (this.render == null ? serailDay : this.render(this.year, i - 1, serailDay)) + "</span></td>";
                    }

                }
                else {
                    tdContent = tdContent + "<td onclick='" + this.callback + funDateStr + "'><span>" + (this.render == null ? serailDay : this.render(this.year, i - 1, serailDay)) + "</span></td>";
                }
                if ((b + 1) % 7 == 0) {
                    trContent = "<tr>" + tdContent + "</tr>";
                    /* calendarTbody为放1-31的table
                    ------------------------------*/
                    $(".calendarTbody" + i).append(trContent);
                    tdContent = "";
                    trContent = "";
                    fillCount = 0;
                }
            }
            if (fillCount != 0)
                for (var f = fillCount; f < 7; f++)
                    tdContent += "<td class='padding'></td>";
            trContent = "<tr>" + tdContent + "</tr>";
            /* calendarTbody为放1-31的table
            ------------------------------*/
            $(".calendarTbody" + i).append(trContent);
        }

    },
    /*获得月份的天数
    -----------------------------------------*/
    getNumberOfMonth: function (Year, Month) {
        var monthDay = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
        if (Year % 400 == 0 || (Year % 4 == 0 && Year % 100 != 0)) {
            monthDay[1] = 29;
        }
        var monthDayNum = monthDay[Month - 1];
        return monthDayNum;
    },
    /*上一年 根据年搜索休息日，后台C#代码有对年根据操作+或-1
    这里只要记录加1年后的currentDate 下一年就减1 注意:globalCalendarSender包含上下文部分属性便于操作
    -----------------------------------------------------------------------------------------------*/
    showPreYear: function () {
        var v = globalCalendarSender.currentDate.getFullYear() + "-" + (globalCalendarSender.currentDate.getMonth() + 1) + "-" + globalCalendarSender.currentDate.getDate();
        $.ajax({
            type: "POST",
            url: "/ModuleSys/WorkDayDateDiff/GetSelectDay",
            data: "changeDate=" + v + "&type=prev",
            success: function (msg) {
                msg = eval("(" + msg + ")");

                globalCalendarSender.selectDay = msg.result;
                /*记录加1年后的currentDate
                -----------------------------*/
                globalCalendarSender.currentDate.setFullYear(globalCalendarSender.currentDate.getFullYear() - 1);
                if (globalCalendarSender.ajaxOnMonthOrYear != null) {
                    globalCalendarSender.ajaxOnMonthOrYear(globalCalendarSender.currentDate.getFullYear(), globalCalendarSender.currentDate.getMonth() + 1);
                    return;
                }
                globalCalendarSender.showCalendar();
            }
        });
    },
    //下个年
    showNextYear: function () {
        var v = globalCalendarSender.currentDate.getFullYear() + "-" + (globalCalendarSender.currentDate.getMonth() + 1) + "-" + globalCalendarSender.currentDate.getDate();
        $.ajax({
            type: "POST",
            url: "/ModuleSys/WorkDayDateDiff/GetSelectDay",
            data: "changeDate=" + v + "&type=next",
            success: function (msg) {
                msg = eval("(" + msg + ")");

                globalCalendarSender.selectDay = msg.result;
                /*记录加1年后的currentDate
                -----------------------------*/
                globalCalendarSender.currentDate.setFullYear(globalCalendarSender.currentDate.getFullYear() + 1);
                if (globalCalendarSender.ajaxOnMonthOrYear != null) {
                    globalCalendarSender.ajaxOnMonthOrYear(globalCalendarSender.currentDate.getFullYear(), globalCalendarSender.currentDate.getMonth() + 1);
                    return;
                }
                globalCalendarSender.showCalendar();
            }
        });
    },
    setRender: function (render) {
        this.render = render;
    },
    setAjaxOnMonthOrYear: function (callback) {
        this.ajaxOnMonthOrYear = callback;
    }
}