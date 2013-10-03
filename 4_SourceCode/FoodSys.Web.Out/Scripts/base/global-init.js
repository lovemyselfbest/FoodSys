$(function () {

	/* 日期选择，强制readonly
	----------------------------------------------------------*/
	$(".datepicker").datepicker({ dateFormat: 'yy-mm-dd', changeMonth: true, changeYear: true, showAnim: 'blind' })
	.bind("paste", function () { return false; })
	.bind("mouseup", function () { $(this).focus(); $(this).select(); })
	.forceReadOnly();

	/* 如果文本框为Readonly 则改变背景颜色
	----------------------------------------------------------*/
	$("input[type='text'][readonly]").css("background", "#eee");

//	var tryNumber = 0;
//	jQuery('input[type=submit]').click(function (event) {
//		var self = $(this);

//		if (self.closest('form').valid()) {
//			if (tryNumber > 0) {
//				tryNumber++;
//				self.attr('disabled', true);
//			}
//			else {
//				tryNumber++;
//			}
//		};
//	});

});