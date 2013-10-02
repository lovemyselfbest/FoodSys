/* select 插入 选项。 index - 表示插入的位置，从0 开始
----------------------------------------------------------*/
$.fn.insertItem = function (index, text, value) {
	return this.each(function () {
		var list = this;
		var options = [];
		var optionsReplace = [];
		var insertOption = new Option(text, value);
		$(this).find("option").each(
			function () {
				var option = new Option($(this).html(), $(this).attr("value"));
				options[options.length] = option;
			}
		);

		if (options.length == 0 || (options.length - 1) < index) {
			list.add(insertOption);
			return;
		}

		$(this).empty();
		for (var i = 0; i < options.length; i++) {
			if (i == index) {
				optionsReplace[optionsReplace.length] = insertOption;
			}
			optionsReplace[optionsReplace.length] = options[i];
		}

		for (var i = 0; i < optionsReplace.length; i++) {
			list.add(optionsReplace[i]);
		}

	});
};

/* json 对象加入到select
----------------------------------------------------------*/
/*
	itemData			: 数据源
	textField		: textField名称
	valueField		: valueField名称
	defaultText		: 默认text
	defaultValue	: 默认Value
*/
$.fn.addItems = function (options) {
    options.textField = options.textField == undefined || options.textField == null || options.textField == "" ? "Text" : options.textField;
    options.valueField = options.valueField == undefined || options.valueField == null || options.valueField == "" ? "Value" : options.valueField;
    return this.each(function () {
        var list = this;
        if (options.defaultText != undefined && options.defaultText != null) {
            list.add(new Option(options.defaultText, options.defaultValue));
        } else {
            list.add(new Option("-- 请选择 --", ""));
        }
        $.each(options.itemData, function (index, itemData) {
            var option = new Option(itemData[options.textField], itemData[options.valueField]);
            list.add(option);
        });
    });
};

/* 文本框只允许输入浮点数
----------------------------------------------------------*/
$(document).ready(function () {
	jQuery.fn.forceDecimal = function () {
		return this.each(function () {
			$(this).keydown(function (e) {
				var key = e.which || e.keyCode;
				var arrVal = $(this).val().split(".");
				if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
				// numbers   
                         key >= 48 && key <= 57 ||
				// Numeric keypad
                         key >= 96 && key <= 105 ||
				// comma, period and minus, . on keypad
                        key == 188 || key == 109 ||
				// Backspace and Tab and Enter
                        key == 8 || key == 9 || key == 13 ||
				// Home and End
                        key == 35 || key == 36 ||
				// left and right arrows
                        key == 37 || key == 39 ||
				// Del and Ins
                        key == 46 || key == 45 || ((key == 190 || key == 110) && arrVal.length <= 1)) {
					return true;
				}
				return false;
			});
		});
	}
	$(".decimal-input").bind("paste", function () { return false; });
	$(".decimal-input").forceDecimal();

	/* 让文本框只允许输入数字
	----------------------------------------------------------*/
	jQuery.fn.forceNumeric = function () {

		return this.each(function () {
			$(this).keydown(function (e) {
				var key = e.which || e.keyCode;
				if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
				// numbers   
                         key >= 48 && key <= 57 ||
				// Home and End
						  key == 35 || key == 36 ||
				// Numeric keypad
                         key >= 96 && key <= 105 ||
				// left and right arrows
                        key == 37 || key == 39 ||
				// Backspace and Tab and Enter
                        key == 8 || key == 46 || key == 13 || key == 9)
					return true;

				return false;
			});
		});
	}

	$(".number-input").bind("paste", function () { return false; });
	$(".number-input").forceNumeric();


	/* 文本框不允许输入字符
	----------------------------------------------------------*/
	jQuery.fn.forceReadOnly = function () {
		return this.each(function () {
			$(this).keydown(function (e) {
				var key = e.which || e.keyCode;
				var txtValues = $(this).val();
				if (key == 8 || key == 46) {
					$(this).val("");
				}
				else
					return false;

			});
		});
	}

	/* 日期format
	today = new Date();
	var dateString = today.format('yyyy-MM-dd hh:mm:ss');
	alert(dateString);
	----------------------------------------------------------*/
	Date.prototype.format = function (format) {
		var o =
    {
    	"M+": this.getMonth() + 1, //month
    	"d+": this.getDate(),    //day
    	"h+": this.getHours(),   //hour
    	"m+": this.getMinutes(), //minute
    	"s+": this.getSeconds(), //second
    	"q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
    	"S": this.getMilliseconds() //millisecond
    }
		if (/(y+)/.test(format))
			format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
		for (var k in o)
			if (new RegExp("(" + k + ")").test(format))
				format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
		return format;
	}

});
