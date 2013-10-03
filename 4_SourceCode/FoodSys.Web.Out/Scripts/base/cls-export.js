var defaultExport = null;
$(function () {
	defaultExport = new CommonExport();
	defaultExport.initialize();
});

/* 通用导出 类
----------------------------------------------------------*/
function CommonExport() {
	this._export_to_excel_flag = false;
	this._export_to_excel_index = 0;
	this._export_to_excel_register_submit = false;
}

CommonExport.prototype = {
	initialize: function () {
		if (this._export_to_excel_register_submit)
			return;
		var exportInstance = this;
		$("form").submit(function () {
			if (exportInstance._export_to_excel_flag) {
				exportInstance._export_to_excel_flag = false;
				if (document.getElementById("idExportFields") == null)
					return;
				$("form")[exportInstance._export_to_excel_index].removeChild(document.getElementById("idExportFields"));
				$("form")[exportInstance._export_to_excel_index].removeChild(document.getElementById("idExportType"));
			}
		});
		this._export_to_excel_register_submit = true;
	},
	showExportDialog: function (type, formIndex) {
		if (formIndex == undefined)
			formIndex = 0;
		var ret = commonPage.showModelDialog("/ExportHelper/?type=" + encodeURI(type), 600, 300);
		if (ret != null && ret != undefined && (typeof (ret) != "object") && ret.toString() == "401") {
			window.top.location = window.top.location;
		}
		if (ret == null || ret == undefined)
			return;
		if (document.getElementById("idExportFields") != null) {
			$("form")[formIndex].removeChild(document.getElementById("idExportFields"));
			$("form")[formIndex].removeChild(document.getElementById("idExportType"));
		}

		var input1 = document.createElement("input");
		input1.setAttribute("type", "hidden");
		input1.setAttribute("id", "idExportFields");
		input1.setAttribute("value", ret.ExportFields);
		input1.setAttribute("name", "ExportFields");
		var input2 = document.createElement("input");
		input2.setAttribute("type", "hidden");
		input2.setAttribute("value", ret.ExportType);
		input2.setAttribute("id", "idExportType");
		input2.setAttribute("name", "ExportType");
		$("form")[formIndex].appendChild(input1);
		$("form")[formIndex].appendChild(input2);

		this._export_to_excel_flag = true;
		this._export_to_excel_index = formIndex;
		$("form")[formIndex].submit();
	}
};