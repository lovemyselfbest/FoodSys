
var itpOverlay = null;
$(function () {
	itpOverlay = new ItpOverlay();
});

/* 通用提示loading 类
----------------------------------------------------------*/

function ItpOverlay(id) {

	this.id = id;
	/**
	* Show the overlay
	*/
	this.show = function (id) {
		if (id) {
			this.id = id;
		}
		// Gets the object of the body tag
		var bgObj = document.getElementById(this.id);
		if (bgObj == null)
			bgObj = document.getElementsByTagName("body")[0];

		// Adds a overlay
		var oDiv = document.createElement('div');
		oDiv.setAttribute('id', 'itp_overlay');
		oDiv.setAttribute("class", "black_overlay");
		oDiv.style.display = 'block';
		bgObj.appendChild(oDiv);

		// Adds loading
		var lDiv = document.createElement('div');
		lDiv.setAttribute('id', 'loading');
		lDiv.setAttribute("class", "loading");
		lDiv.style.display = 'block';
		bgObj.appendChild(lDiv);

	}

	/**
	* Hide the overlay
	*/
	this.hide = function (id) {
		if (id) {
			this.id = id;
		}
		var bgObj = document.getElementById(this.id);
		if (bgObj == null)
			bgObj = document.getElementsByTagName("body")[0];
		// Removes loading 
		var element = document.getElementById('loading');
		bgObj.removeChild(element);

		// Removes a overlay box
		var element = document.getElementById('itp_overlay');
		bgObj.removeChild(element);
	}
}