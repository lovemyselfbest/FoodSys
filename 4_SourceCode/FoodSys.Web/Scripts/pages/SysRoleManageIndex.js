﻿$(function () {
	$("#tabs-content").height($("html").outerHeight() - $(".tabs-container").outerHeight() - 5).width($("html").outerWidth());
	$("#tabs-fixed").height($("html").outerHeight() - 50);


	/* 初始化树
	----------------------------------------------------------*/
	var tree = $("#leftTree,#rightTree").jstree({
		ui: { theme_name: "checkbox" },
		"types": {
			"valid_children": ["root"],
			"types": {
				"root": {
					"icon": {
						"image": "/images/ico0.jpg"
					},
					"valid_children": ["default"],
					"max_depth": 2,
					"hover_node": false,
					"select_node": function () { }
				},
				"default": {
					"valid_children": ["default"],
					"icon": {
						"image": "/images/ico1.jpg"
					}
				}
			}
		},
		"plugins": ["themes", "html_data", "ui", "types", "checkbox"]
	});
});

//全部向右移动
function leftMoveAll(operateType) {
	top.window.$("#sysConfirm").html("确定要移动吗？");
	top.window.$("#sysConfirm").dialog({
		resizable: false,
		height: 200,
		title: "系统提示",
		modal: true,
		buttons: {
			"是": function () {

				// 存放所有菜单id
				var arrayId = new Array();
				//未选中菜单id
				$("#leftTree").jstree("get_unchecked", null, true).each(function (i, n) {
					if (n.id != "") {
						arrayId.push(n.id);
					}
				});
				//已选中菜单id
				$("#leftTree").jstree("get_checked", null, true).each(function (i, n) {
					if (n.id != "") {
						arrayId.push(n.id);
					}
				});

				submitMoveMenu(arrayId, operateType);

				top.window.$("#sysConfirm").dialog("close");
			},
			"否": function () {
				top.window.$("#sysConfirm").dialog("close");
			}
		}
	});
}

//提交
function submitMoveMenu(arrayId, operateType) {
	//如果没有选中否不操作
	if (arrayId.length < 1) {
		return;
	}
	//添加到页面进行提交
	for (var i = 0; i < arrayId.length; i++) {
		$(".moveId").append('<input type="hidden" name="menuGuid" value="' + arrayId[i] + '" />');
	}

	$(".moveId").append('<input type="hidden" name="operateType" value="' + operateType + '" />');
	$("form").attr("action", "/ModuleSys/SysRoleManage/MoveRoleMenu/").submit();
}

//选中项向右移动
function leftMoveSelectItem(operateType) {
	top.window.$("#sysConfirm").html("确定要移动吗？");
	top.window.$("#sysConfirm").dialog({
		resizable: false,
		height: 200,
		title: "系统提示",
		modal: true,
		buttons: {
			"是": function () {

				// 存放所有菜单id
				var arrayId = new Array();
				//已选中菜单id
				$("#leftTree").jstree("get_checked", null, true).each(function (i, n) {
					if (n.id != "") {
						arrayId.push(n.id);
					}
				});

				submitMoveMenu(arrayId, operateType);

				top.window.$("#sysConfirm").dialog("close");
			},
			"否": function () {
				top.window.$("#sysConfirm").dialog("close");
			}
		}
	});
}

//全部向左移动
function rightMoveAll(operateType) {
	top.window.$("#sysConfirm").html("确定要移动吗？");
	top.window.$("#sysConfirm").dialog({
		resizable: false,
		height: 200,
		title: "系统提示",
		modal: true,
		buttons: {
			"是": function () {

				// 存放所有菜单id
				var arrayId = new Array();
				//未选中菜单id
				$("#rightTree").jstree("get_unchecked", null, true).each(function (i, n) {
					if (n.id != "") {
						arrayId.push(n.id);
					}
				});
				//已选中菜单id
				$("#rightTree").jstree("get_checked", null, true).each(function (i, n) {
					if (n.id != "") {
						arrayId.push(n.id);
					}
				});
				submitMoveMenu(arrayId, operateType);

				top.window.$("#sysConfirm").dialog("close");
			},
			"否": function () {
				top.window.$("#sysConfirm").dialog("close");
			}
		}
	});
}

//选中项各大左移动
function rightMoveSelectItem(operateType) {
	top.window.$("#sysConfirm").html("确定要移动吗？");
	top.window.$("#sysConfirm").dialog({
		resizable: false,
		height: 200,
		title: "系统提示",
		modal: true,
		buttons: {
			"是": function () {
				// 存放所有菜单id
				var arrayId = new Array();
				//已选中菜单id
				$("#rightTree").jstree("get_checked", null, true).each(function (i, n) {
					if (n.id != "") {
						arrayId.push(n.id);
					}
				});

				submitMoveMenu(arrayId, operateType);

				top.window.$("#sysConfirm").dialog("close");
			},
			"否": function () {
				top.window.$("#sysConfirm").dialog("close");
			}
		}
	});
}
