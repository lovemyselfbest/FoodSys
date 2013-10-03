/* 通用页面静态类初始化
----------------------------------------------------------*/
$(function () {
	commonPage.initialize();
});

/* 通用页面静态类
----------------------------------------------------------*/
var commonPage = new Object();
commonPage = {
	initialize: function () {
		this.initializeDecimal();
		this.initializeCommonSearch();
		this.initializeClear();
		this.initializeDatepicker();
		this.highlightKeyword();
	},
	/* 弹出模态对话框
	----------------------------------------------------------*/
	showModelDialog: function (url, width, height) {
		var ret = window.showModalDialog(url, '', 'status:no; help:no; dialogWidth:' + width + 'px; dialogHeight:' + height + 'px');
		return ret;
	},
	/* 初始化通用的查询条件textbox
	----------------------------------------------------------*/
	initializeCommonSearch: function () {
		var searchValue = "";
		if ($(".common-search-condition").val() == "") {
			$(".common-search-condition").val("请输入查询条件");
		};
		$(".common-search-condition").focus(function () {
			searchValue = $(this).val() == "请输入查询条件" ? "" : $(this).val();
			$(this).attr("value", searchValue);
		}).blur(function () {
			searchValue = $(this).val() == "" ? "请输入查询条件" : $(this).val();
			$(this).attr("value", searchValue);
		});
	},
	/* 多条件查询显示与隐藏
	----------------------------------------------------------*/
	scan: function () {
		$(".search-container").css("display") == "none" ? $(".search-container").css("display", "block") : $(".search-container").css("display", "none");
		$(".search-container").css("display") == "block" ? $(".search-clue").css("display", "block") : $(".search-clue").css("display", "none");
		defaultTable.setTableHeight();
	},
	initializeClear: function () {
		$(".button-clear").click(function () {
			$(".search-container input[type='text']").val("");
			$('.search-container select').each(function () {
				$(this).find('option:eq(0)').attr("selected", "selected");
			});
		});

		$(".button-search").click(function () {
			$(".common-search-condition").val("");
		})

		//需要初始化的数据
		$(".common-search-submit").click(function () {
			$(".button-clear").click();
		})
	},
	initializeDatepicker: function () {
		$(".datepicker").datepicker({ dateFormat: 'yy-mm-dd', changeMonth: true, changeYear: true, showAnim: 'blind' })
				.bind("paste", function () { return false; })
				.bind("mouseup", function () { $(this).focus(); $(this).select(); })
				.forceReadOnly();
	},
	highlightKeyword: function () {
		var keyword = $.trim($(".common-search-condition").val());

		if (keyword == "" || keyword == "请输入查询条件")
			return;
		$(".common-table-container").highlight(keyword);
	},

	/* 格式化Decimal
	----------------------------------------------------------*/
	formatNumber: function (pnumber, decimals) {
		if (pnumber == 0) { return 0 }
		if (isNaN(pnumber)) { return '' };
		if (pnumber == '') { return '' };
		var snum = new String(pnumber);
		var sec = snum.split('.');
		var whole = parseFloat(sec[0]);
		var result = '';
		if (sec.length > 1) {
			var dec = new String(sec[1]);
			dec = String(parseFloat(sec[1]) / Math.pow(10, (dec.length - decimals)));
			dec = String(whole + Math.round(parseFloat(dec)) / Math.pow(10, decimals));
			var dot = dec.indexOf('.');
			if (dot == -1) {
				dec += '.';
				dot = dec.indexOf('.');
			}
			while (dec.length <= dot + decimals) { dec += '0'; }
			result = dec;
		} else {
			var dot;
			var dec = new String(whole);
			dec += '.';
			dot = dec.indexOf('.');
			while (dec.length <= dot + decimals) { dec += '0'; }
			result = dec;
		}
		return result;
	},
	initializeDecimal: function () {
		var commonInstance = this;
		$('.decimal').each(
			function () {
				$(this).val(commonInstance.formatNumber($(this).val(), 2));
			}
		);
	},
	/* 获得分页控件中a 标签的当前页面url
	----------------------------------------------------------*/
	getCurrentUrl: function () {
		var url = "";
		var aUrl = $("a[paginate-current-url]").attr("paginate-current-url");
		if (aUrl == undefined || aUrl == null || aUrl == "")
			url = window.location.href;
		else
			url = aUrl;
		return url;
	},
	ajaxGrid: function (option) {
		if ($(".replace-container").length == 0)
			return;
		option = option || {};
		var _default = {
			target: '.replace-container',   // target element(s) to be updated with server response 
			form: 'form',
			beforeSubmit: function () {
			},  // pre-submit callback 
			success: function () {
				defaultTable.initialize();
				defaultTable.highlight(window.top.getHighlightData());
				commonPage.highlightKeyword();
				initializeDateFormat();
			},  // post-submit callback 
			url: window.location.href
		};
		$.extend(_default, option);
		_default.url = $(_default.form).attr("action") == undefined ? _default.url : $(_default.form).attr("action");
		$(_default.form).ajaxForm(_default);
	},
	ajaxGridUnbind: function (option) {
		option = option || {};
		var _default = {
			form: 'form'
		};
		$.extend(_default, option);
		$(_default.form).ajaxFormUnbind();
	},
	submit: function (option) {
		option = option || {};
		var _default = {
			form: 'form'
		};
		$.extend(_default, option);
		$(_default.form).submit();
	}
};
