/* firefox 设置 html height = 100%;
----------------------------------------------------------*/
var defaultTable = null;
$(function () {
	$("html").css("height", "100%");
	$("body").css("height", "100%");
	defaultTable = new CommonTable("#common-table", { autoSetHeight: true });
	defaultTable.initialize();
});

/* 通用table类
----------------------------------------------------------*/
function CommonTable(selector, options) {
	this.selector = selector;
	this._options = options;
	this._data_order_name_selector = this.selector + " input[data-order-name='']";
	this._data_order_direction_selector = this.selector + " input[data-order-direction='']";
	this._data_order_th_selector = this.selector + " th[data-order-field]";
	this._checkbox_all_selector = this.selector + " .common-table-checkbox-all";
	this._checkbox_radio_selector = this.selector + " tbody tr input[type='checkbox'], tbody tr input[type='radio']";
	this._checkbox_selector = this.selector + " tbody tr input[type='checkbox']";
	this._radio_selector = this.selector + " tbody tr input[type='radio']";
	this._table_container_selector = ".common-table-container";
	this._layout_part = [".toolbar", ".search-container", ".search-clue", ".pagination-container", ".tabs-container", ".page-caption"];
}

CommonTable.prototype = {

	//获得jquery table 对象
	getJTable: function () {
		return $(this.selector);
	},
	//初始化table
	initialize: function () {
		if (this.getJTable().length == 0)
			return;

		this.setTableStyle();
		this.setTableHeight();
		this.setTableWidth();
		this.fieldOrderInitialize();
		this.sumColumnOfTable();
		this.setCheckBoxAllChange();
		this.setCheckBoxChange();
		this.setRadioChange();
	},
	//排序函数
	fieldOrder: function (fieldName) {
		if ($(this._data_order_name_selector).val() == fieldName) {
			var orderDirection = $(this._data_order_direction_selector).val() == "0" ? "1" : "0";
			$(this._data_order_direction_selector).attr("value", orderDirection);
		}
		else {
			$(this._data_order_name_selector).attr("value", fieldName);
			$(this._data_order_direction_selector).attr("value", "0");
		}
		$("form").submit();
	},
	//排序初始化函数
	fieldOrderInitialize: function () {
		var orderName = $(this._data_order_name_selector).val();
		var orderDirection = $(this._data_order_direction_selector).val();
		if (orderName == "" || orderDirection == "")
			return;
		$(this._data_order_th_selector).each(
		function () {
			var orderField = $(this).attr("data-order-field");
			if (orderField != orderName || ($(this).html().endsWith("↑") || $(this).html().endsWith("↓")))
				return;
			orderDirection == "0" ? $(this).append("↑") : $(this).append("↓");
		}
	);
	},
	//获得第一个选中值
	getFirstSelectedValue: function () {
		var result = this.listSelectedValues(this.selector);
		if (result.length > 0)
			return result[0];
		return null;
	},
	//强制选择一行并返回选中的值
	forceSelectOne: function () {
		var result = this.listSelectedValues(this.selector);
		if (result.length < 1) {
			//请至少选择一行记录！
			window.top.sysAlert("请至少选择一行记录！");
			return null;
		}
		else if (result.length > 1) {
			//最多只能选择一行记录！
			window.top.sysAlert("最多只能选择一行记录！");
			return null;
		}
		return result[0];
	},
	//列出所有选中值
	listSelectedValues: function () {
		var result = [];
		$(this._checkbox_radio_selector).each(function () {
			if (this.checked) {
				result[result.length] = $(this).attr("value");
			}
		});
		return result;
	},
	listSelectedTrs: function () {
		var result = [];
		$(this._checkbox_radio_selector).each(function () {
			if (this.checked) {
				result[result.length] = $(this).parent().parent();
			}
		});
		return result;
	},
	listSelectedItems: function () {
		var result = [];
		$(this._checkbox_radio_selector).each(function () {
			if (this.checked) {
				result[result.length] = this;
			}
		});
		return result;
	},
	//判断是否被选中
	isSelect: function () {
		var result = this.listSelectedValues();
		if (result.length < 1) {
			//请至少选择一行记录！
			window.top.sysAlert("请至少选择一行记录！");
			return false;
		}
		return true;
	},
	//全部选中的checkbox的点击事件
	setCheckBoxAllChange: function () {
		var tableInstance = this;
		$(this._checkbox_all_selector).click(function () {
			var status = this.checked;
			$(tableInstance._checkbox_selector).each(function () {
				if ($(this).attr("disabled"))
					return;
				$(this).attr("checked", status);
				$(this).change();
				status ? $(this).parent().parent("tr").addClass("common-table-tr-selected") : $(this).parent().parent("tr").removeClass("common-table-tr-selected");
			})
		});
	},
	//单个checkbox的点击事件
	setCheckBoxChange: function () {
		$(this._checkbox_selector).change(function () {
			if ($(this).is(":checked")) {
				$(this).parent().parent("tr").addClass("common-table-tr-selected");
			}
			else {
				$(this).parent().parent("tr").removeClass("common-table-tr-selected");
			}
		});
	},
	//单个Radio的点击事件
	setRadioChange: function () {
		$(this._radio_selector).change(function () {
			if ($(this).is(":checked")) {
				$(this).parent().parent("tr").addClass("common-table-tr-selected");
				$(this).parent().parent("tr").siblings().removeClass("common-table-tr-selected");
			}
			else {
				$(this).parent().parent("tr").removeClass("common-table-tr-selected");
			}
		});
	}
	,
	checkboxChangeEvent: function (callback) {
		$(this._checkbox_radio_selector).change(callback);
	},
	//设置table的样式
	setTableStyle: function () {
		$(this.selector + " tr:odd").addClass("common-table-odd-backgroundColor");
		$(this.selector + " tr:even").addClass("common-table-even-backgroundColor");

		$(this.selector + " tbody tr").each(function () {
			if (typeof ($(this).attr("ondblclick")) == "undefined") {
				$(this).hover(function () {
					$(this).addClass("common-table-tr-hover-default");
				}, function () {
					$(this).removeClass("common-table-tr-hover-default");
				});
			}
			else {
				$(this).hover(function () {
					$(this).addClass("common-table-tr-hover");
				}, function () {
					$(this).removeClass("common-table-tr-hover");
				});
			}
		})
	},
	//动态设置table 宽度
	setTableWidth: function () {
		$(this.selector).css("width", "");
		var cols = $(this.selector).find("col");
		var totalWidth = 0;
		var defaultWidth = $(this.selector).width();
		for (var i = 0; i < cols.length; i++) {
			var value = parseInt($(cols[i]).attr("width"));
			if (value == NaN)
				return;
			totalWidth += value;
		}
		if (totalWidth >= defaultWidth)
			$(this.selector).css("width", totalWidth + "px");
	},
	//动态设置table的高度
	setTableHeight: function () {
		if (!this._options.autoSetHeight)
			return;
		var size = {};
		size.y = $("html").height();
		for (var i = 0; i < this._layout_part.length; i++) {
			if ($(this._layout_part[i]).is(":visible"))
				size.y = size.y - $(this._layout_part[i]).outerHeight();
		}
		$(this._table_container_selector).css("height", (size.y - 5) + "px");
	},

	//动态添加布局
	addPart: function (partSelector) {
		this._layout_part[this._layout_part.length] = partSelector;
		this.setTableHeight();
	},
	highlight: function (highlightData) {
		if (highlightData == undefined || highlightData == null) {
			$(this._table_container_selector + " tr").effect("highlight", {}, 1000);
			return;
		}
		$(this._checkbox_radio_selector).each(
			function () {
				if (this.value != highlightData)
					return;
				$(this).parent().parent().effect("highlight", {}, 2000);
			}
		);
	},
	//计算table某列的总和,th 元素上需标记 "data_column_sum" 属性。
	sumColumnOfTable: function () {
		var targetTables = $(this.selector);
		var willSumColumns = [];
		for (var i = 0; i < targetTables.length; i++) {
			var item = targetTables[i];
			var totalColumns = 0;
			//var ths = $(item).find("th");
			var ths = $(item).find("tbody tr:eq(0) td");
			var sumResult = [];
			var lastTr = "";
			totalColumns = ths.length;
			ths.each(
			function (index) {
				var sumAttrubte = this.getAttribute("data_column_sum");
				if (sumAttrubte != null) {
					willSumColumns.length = willSumColumns.length + 1;
					willSumColumns[willSumColumns.length - 1] = index;
				}
			}
		);
			sumResult = [willSumColumns.length];
			for (var i = 0; i < willSumColumns.length; i++) {
				sumResult[i] = this.sumOfColumns(willSumColumns[i] + 1, true);
			}
			lastTr += "<tr>";
			for (var i = 0; i < ths.length; i++) {
				if (jQuery.inArray(i, willSumColumns) == -1 && i == 0) {
					lastTr += "<td style='font-weight:bolder;'>合计</td>";
				} else if (jQuery.inArray(i, willSumColumns) == -1 && i != 0) {
					lastTr += "<td></td>";
				}
				else {
					lastTr += "<td>" + sumResult[jQuery.inArray(i, willSumColumns)] + "</td>";
				}
			}
			lastTr += "</tr>";
			if (willSumColumns.length > 0)
				$(item).find("tbody").append(lastTr);
		}
	},
	/* 计算指定列汇总
	----------------------------------------------------------*/
	sumOfColumns: function (columnIndex, hasHeader) {
		var tot = 0;
		$(this.selector + " tr" + (hasHeader ? ":gt(0)" : ""))
			.children("td:nth-child(" + columnIndex + ")")
			.each(function () {
				var value = parseFloat($(this).text());
				if (value == undefined || isNaN(value))
					return;
				tot += value;
			});
		return this.formatNumber(tot, 2);
	},
	// 格式化数字
	formatNumber: function (pnumber, decimals) {
		if (pnumber == 0) { return 0 }
		if (isNaN(pnumber)) { return 0 };
		if (pnumber == '') { return 0 };
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

		result = result.replace(/\.00/, "");
		return result;
	}
};