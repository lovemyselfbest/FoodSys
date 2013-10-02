var globalFrameHeight = 0;
$(function () {
	//隐藏左面菜单
	$(".menu-toggle").click(function () {
		if ($(".main-left").css("display") == "none") {
			$(".main-left").show();
			$(".container-menu").show("slide");
		} else {
			$(".main-left").fadeOut();
		}
	});
	loadMenu();
	calculateWindowSize();
	$(window).resize(function () { calculateWindowSize(); });
	$(document).keydown(menuShortKeydown).keyup(menuShortKeyup);
});

/* ctrl & esc 快捷键Keydown事件
----------------------------------------------------------*/
var _bind_ctrl_keyup_event = true;
function menuShortKeydown(e) {
	if (e.ctrlKey && e.keyCode != 17) {
		_bind_ctrl_keyup_event = false;
		return;
	}
	if (!_bind_ctrl_keyup_event) {
		_bind_ctrl_keyup_event = !_bind_ctrl_keyup_event;
	}
}

/* ctrl & esc 快捷键Keyup事件
----------------------------------------------------------*/
function menuShortKeyup(e) {
	if (!_bind_ctrl_keyup_event)
		return;
	if (e.keyCode == 27) {
		var hasExpendSecond = false;
		$(window.top.document).find(".container-menu dl").each(
					function () {
						if (hasExpendSecond)
							return;
						hasExpendSecond = $(this).css("display") == "block";
					}
				);
		if (hasExpendSecond) {
			$(window.top.document).find(".container-menu dl").hide();
		}
		else {
			$(window.top.document).find(".main-left").fadeOut();
		}
	}
	if (e.keyCode == 17) {
		if ($(window.top.document).find(".main-left").css("display") == "none") {
			$(window.top.document).find(".main-left").show();
			$(window.top.document).find(".container-menu").show("fast");
			return;
		}
		else {
			if ($(window.top.document).find(".container-menu .dt-selected").length == 0) {
				$(window.top.document).find(".container-menu .second-level-menu").first().click();
			} else {
				$(window.top.document).find(".container-menu .dt-selected").click();
			}
		}
	}
}



/* 加载菜单脚本
----------------------------------------------------------*/
function loadMenu() {
	/* 所有二级菜单隐藏
	----------------------------------------------------------*/
	var menuId = ".container-menu";
	$(menuId).find("dl").hide();

	/* 点击事件
	----------------------------------------------------------*/
	$(menuId).find(".second-level-menu").filter(function () { return $(this).parent().find("dd").size() != 0 }).click(function () {
		if ($(this).parent().find("dl").css("display") == "block") {
			$(this).parent().find("dl").fadeOut();
		} else {
			$(this).parent().find("dl").height($(".container-menu").height());
			$(".container-menu .second-level-menu").removeClass("on").removeClass("dt-selected");
			$(".container-menu dl").hide();
			$(this).addClass("on").addClass("dt-selected");
			$(this).parent().find("dl").show("fast");
		}
	})

	/* 二级菜单点击样式
	----------------------------------------------------------*/
	$(menuId).find("dd").click(function () { $("dd").removeClass("focus"); $(this).addClass("focus"); });

}

/* 首页主体区域URL重定向
----------------------------------------------------------*/
function setframeUrl(url, menu) {
	clearCache();
	$("#mainFrame").attr("src", url);
	$(".navigation span").html(menu);
	$(".container-menu dl").hide('fast');
}

/* 计算每部分内容尺寸
----------------------------------------------------------*/
function calculateWindowSize() {
	$("body").height($("html").height());
	var menuHeight;
	if ($.browser.msie && ($.browser.version == "7.0"))
		menuHeight = $("body").height() - 95;
	else
		menuHeight = $("body").height() - $(".container-top").height();
	$(".main-content").height(menuHeight);
	$(".container-menu").height(menuHeight);
	$("#mainFrame").height(menuHeight - $(".navigation").height());
	globalFrameHeight = $("#mainFrame").height();
}


/* 打开Dialog
----------------------------------------------------------*/
/*
url
, title
, width
, height
, beforeClose
*/
function openDialog(option) {
	option = option || {};
	var _default = {
		Index: "1",
		Width: 650,
		Height: 550
	};
	$.extend(_default, option);
	var dialogID = "#dialog" + _default.Index;
	var frameID = "#iframeDialog" + _default.Index;
	if ($(frameID)[0].contentWindow.itpOverlay)
		$(frameID)[0].contentWindow.itpOverlay.show();
	$(frameID).attr("src", _default.Url);
	var dialogExtendOptions = {
		"minimize": true
	};
	var windowHeight = $(window).height();
	var windowWidth = $(window).width();
	_default.Height = _default.Height > windowHeight ? windowHeight : _default.Height;
	_default.Width = _default.Width > windowWidth ? windowWidth : _default.Width;

	$(dialogID).dialog({ modal: true, title: _default.Title, width: _default.Width, height: _default.Height, beforeClose: _default.BeforeClose }).dialogExtend(dialogExtendOptions);
}

/* 关闭框架层
----------------------------------------------------------*/
/*
isRefreshParent
, message
, callback
, isShowMessage
, index
, form
, HighlightData
, RefreshDialogIndex
*/
function closeDialog(option) {
	option = option || {};
	var _default = {
		Index: null,
		IsShowMessage: false,

		Callback: "",
		Message: "",
		Form: "form",
		RefreshDialogIndex: null,
		RefreshOpener: false,
		RefreshOpenerAsynchronous: false
	};
	$.extend(_default, option);

	if (_default.RefreshDialogIndex != null)
		refreshDialog(_default.RefreshDialogIndex);

	if (_default.IsShowMessage)
		sysMessage(_default.Message);

	if (_default.Callback != undefined && _default.Callback != "")
		window.eval(_default.Callback + '();');

	/* ie按照上面的代码执行会抛出excption: Can't execute code from a freed script,故改为下面执行方法
	----------------------------------------------------------*/
	//	if ($.browser.msie && _default.Callback != undefined && _default.Callback != "")
	//		window.eval(("(" + window.eval(_default.Callback)).toString() + ")" + "()");

	var dialogInstances = [];
	$(window.top.document).find("div.ui-dialog").each(
			function () {
				if (!$(this).find("div.ui-dialog-content").dialog("isOpen"))
					return;
				dialogInstances[dialogInstances.length] = $(this);
			}
		);
	_sortByZIndex(dialogInstances);
	if (_default.Index == null) {
		$(dialogInstances[dialogInstances.length - 1]).find("div.ui-dialog-content").dialog("close");
	} else {
		var dialogID = "#dialog" + _default.Index;
		$(dialogID).dialog("close");
	}
	if (_default.RefreshOpener) {
		if (dialogInstances.length > 1) {
			var willRefreshDialog = dialogInstances[dialogInstances.length - 2];
			var contentWindow = willRefreshDialog.find("iframe")[0].contentWindow;
			_default.RefreshOpenerAsynchronous ? contentWindow.commonPage.submit({ Form: _default.Form }) : willRefreshDialog.find("iframe").contents()[0].location = willRefreshDialog.find("iframe").contents()[0].location;
		} else {
			var mainFrame = document.getElementById("mainFrame");
			var contentWindow = $("#mainFrame")[0].contentWindow;
			if (option.HighlightData != undefined && option.HighlightData != null)
				setHighlightData(option.HighlightData);
			_default.RefreshOpenerAsynchronous ? contentWindow.commonPage.submit({ Form: _default.Form }) : contentWindow.location = contentWindow.commonPage.getCurrentUrl();
		}
	}

}


function _sortByZIndex(a) {
	var k, i;
	for (i = 0; i < a.length - 1; i++) {
		k = i;
		for (j = i + 1; j < a.length; j++)
			if (a[k].css("z-index") > a[j].css("z-index"))
				k = j;
		if (k != i) {
			t = a[i];
			a[i] = a[k];
			a[k] = t;
		}
	}
}

/* 刷新弹出层
----------------------------------------------------------*/
function refreshDialog(index) {
	$("#iframeDialog" + index).contents()[0].location = $("#iframeDialog" + index).contents()[0].location;
}

function refresh(option) {
	option = option || {};
	var _default = {
		Form: "form"
	};
	var contentWindow = $("#mainFrame")[0].contentWindow;
	contentWindow.commonPage.submit({ Form: _default.Form });
}

function refreshEmbeddedFrame(option) {
	option = option || {};
	var _default = {
		Form: "form"
	};
	var contentWindow = $("#mainFrame").contents().find("#embeddedFrame")[0].contentWindow;
	contentWindow.commonPage.submit({ Form: _default.Form })
}

/* 关闭、刷新二级iframe  
二级iframe ID为：embeddedFrame
isRefreshParent
, message
, callback
, isShowMessage
, index
, HighlightData
----------------------------------------------------------*/
function closeEmbeddedFrameDialog(option) {

	option = option || {};
	var _default = {
		Index: null,
		RefreshOpener: false,
		RefreshOpenerAsynchronous: false,
		IsShowMessage: false,
		Callback: "",
		Message: "操作成功"
	};
	$.extend(_default, option);
	if (!_default.IsShowMessage)
		sysMessage(_default.Message);
	var dialogID = "#dialog" + _default.Index;
	if (_default.Callback != undefined && _default.Callback != "")
		window.eval(_default.Callback + '();');

	var dialogInstances = [];
	$(window.top.document).find("div.ui-dialog").each(
			function () {
				if (!$(this).find("div.ui-dialog-content").dialog("isOpen"))
					return;
				dialogInstances[dialogInstances.length] = $(this);
			}
		);
	_sortByZIndex(dialogInstances);

	if (_default.RefreshOpener) {
		if (dialogInstances.length > 1) {
			var willRefreshDialog = dialogInstances[dialogInstances.length - 2];
			willRefreshDialog.find("iframe").contents()[0].location = willRefreshDialog.find("iframe").contents()[0].location;
		} else {
			var contentWindow = $("#mainFrame").contents().find("#embeddedFrame")[0].contentWindow;
			if (option.HighlightData != undefined && option.HighlightData != null)
				setHighlightData(option.HighlightData);
			_default.RefreshOpenerAsynchronous ? contentWindow.commonPage.submit({ Form: _default.Form }) : contentWindow.location = contentWindow.commonPage.getCurrentUrl();
		}
	}

	if (_default.Index == null) {
		$(dialogInstances[dialogInstances.length - 1]).find("div.ui-dialog-content").dialog("close");
	} else {
		var dialogID = "#dialog" + _default.Index;
		$(dialogID).dialog("close");
	}
}

/* 右下角弹出系统消息
----------------------------------------------------------*/
function sysMessage(message) {
	if (message != undefined && message != "")
		$("#sysMessage").html(message);
	var left = $("html").width() - $("#sysMessage").outerWidth();
	var top = $("html").height() - $("#sysMessage").outerHeight() - 20;
	$("#sysMessage").css("left", left + "px");
	$("#sysMessage").css("top", top + "px");
	$("#sysMessage").css("display", "block");
	$("#sysMessage").slideDown();
	window.setTimeout("$('#sysMessage').fadeOut();", 2000);

}

/* 错误消息提示
----------------------------------------------------------*/
function sysAlert(message, option) {

	option = option || {};
	var _default = {
		Height: 200,
		Width: 310
	};
	$.extend(_default, option);
	if (message == undefined)
		return;
	$("#sysAlert").html(message);
	$("#sysAlert").dialog({ modal: true, title: "系统提示", height: _default.Height, width: _default.Width });
	$("#sysAlert").dialog({
		modal: true,
		buttons: {
			确定: function () {
				$(this).dialog("close");
			}
		}
	});
}


var _temp_cache_key = null;
function setCacheValue(value) {
	var date = new Date();
	var ticks = date.getTime() + "_value";
	window[ticks] = value;
	_temp_cache_key = ticks;
}

function getCacheValue(isKeep) {
	var temp = window[_temp_cache_key];
	if (!isKeep)
		window[_temp_cache_key] = null;
	return temp;
}

var _temp_callback_key = null;
function setCallBackFun(fun) {
	var date = new Date();
	var ticks = date.getTime() + "_callback";
	window[ticks] = fun;
	_temp_callback_key = ticks;
}

function getCallBackFun(isKeep) {
	var temp = window[_temp_callback_key];
	if (!isKeep)
		window[_temp_callback_key] = null;
	return temp;
}

var _temp_highlight_key = null;
function setHighlightData(value) {
	var date = new Date();
	var ticks = date.getTime() + "_highlight";
	window[ticks] = value;
	_temp_highlight_key = ticks;
}

function getHighlightData(isKeep) {
	var temp = window[_temp_highlight_key];
	if (!isKeep)
		window[_temp_highlight_key] = null;
	return temp;
}

function clearCache() {
	window[_temp_highlight_key] = null;
	window[_temp_callback_key] = null;
	window[_temp_cache_key] = null;
}






