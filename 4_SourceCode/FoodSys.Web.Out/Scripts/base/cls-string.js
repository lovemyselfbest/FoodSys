/* pads left
----------------------------------------------------------*/
String.prototype.lpad = function (padString, length) {
	var str = this;
	while (str.length < length)
		str = padString + str;
	return str;
}
/* pads right
----------------------------------------------------------*/
String.prototype.rpad = function (padString, length) {
	var str = this;
	while (str.length < length)
		str = str + padString;
	return str;
}

String.prototype.startsWith = function (prefix) {
	return this.indexOf(prefix) === 0;
}

String.prototype.endsWith = function (suffix) {
	return this.match(suffix + "$") == suffix;
};

String.format = function () {
	var s = arguments[0];
	for (var i = 0; i < arguments.length - 1; i++) {
		var reg = new RegExp("\\{" + i + "\\}", "gm");
		s = s.replace(reg, arguments[i + 1]);
	}
	return s;
}

String.ToUnicode = function (str) {
	return escape(str).toLocaleLowerCase().replace(/%u/gi, '\\u');
}

String.UnicodeToGB2312 = function (str) {
	return unescape(str.replace(/\\u/gi, '%u'));
}
