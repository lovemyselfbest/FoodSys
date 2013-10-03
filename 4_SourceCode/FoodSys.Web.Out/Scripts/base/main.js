
$(function () {
	$(document).keydown(window.top.menuShortKeydown).keyup(window.top.menuShortKeyup);
});

function loadNotify() {
	$.ajax({
		type: "POST",
		url: "/ModuleLogin/Login/index",
		data: "SysUser.UserAccount=" + $("#SysUser_UserAccount").val() + "&SysUser.Password=" + $("#SysUser_Password").val() + "&rememberLoginStatus=" + hasChecked,
		success: function (msg) {
			msg = eval("(" + msg + ")");
			if (msg.result == "")
				window.location = "/home";
			else {
				sysAlert(msg.result);
			}
		}
	});
}

