$(function() {
	var data = [
    "Neutral",
    "Warrior",
    "Warlock",
    "Shaman",
    "Rogue",
    "Priest",
    "Paladin",
    "Mage",
    "Hunter",
    "Druid"
  ];
  var hsb = {"hue": 360, "saturation": 100, "brightness": 100, "opacity": 100};
  var filterRegex = /hue\-rotate\((\d+)deg\) saturate\(([0-9\.]+)\) brightness\(([0-9\.]+)\)/;
  
  var parseFilter = function(str) {
	var match = str.match(filterRegex);
	return {
		h: parseInt(match[1]),
		s: Math.round(parseFloat(match[2]) * 100),
		b: Math.round(parseFloat(match[3]) * 100)
	};
  }
  
  var updateFilter = function(name) {
	$("#preview").css("background-image",'url("images/' + name + '-game.png"');
	$("#filter-overlay")
		.removeClass()
		.css("-webkit-filter", "")
		.css("filter", "")
		.addClass(name + "-color");
	var filter = parseFilter($("." + name + "-color").css("-webkit-filter"));
	console.log(filter);
	$("#hueRange").val(filter.h).trigger("input");
	$("#saturationRange").val(filter.s).trigger("input");
	$("#brightnessRange").val(filter.b).trigger("input");
  };  
  
  var updateFilterColor = function(h, s, b, o) {
	$("#filter-overlay")
		.css("filter", "hue-rotate(" + h + "deg) saturate(" + s + "%) brightness(" + b + "%)")
		.css("-webkit-filter", "hue-rotate(" + h + "deg) saturate(" + s + "%) brightness(" + b + "%)")
		.css("opacity", o/100);
  };
  
  // create class selection
  var classSelect = $("#classSelect");
  $.each(data, function(index, value) {
	classSelect		
		.append($("<input>", {type: "radio", name: "classSelect", value: value.toLowerCase()}))
		.append($("<label>", {text: value}))
		.append($("<br>"));
  });
  $("input[type=radio][name=classSelect]").on("change", function() {	
	updateFilter($(this).val());
  });
  
  // create color selection
  var colorSelect = $("#colorSelect");
  $.each(hsb, function(key, value) {
    var sliderId = key + "Range";
    var textBoxId = key + "RangeText";
    var slider = $("<input>", {type: "range", id: sliderId, min: 0, max: value, value: 0});
    slider.on("input", function() {
      $("#" + textBoxId).val($("#" + sliderId).val());
      updateFilterColor($("#hueRange").val(), $("#saturationRange").val(), $("#brightnessRange").val(), $("#opacityRange").val())
    });
    colorSelect.append($("<p>" + key.toUpperCase() + "</p>"));
    colorSelect.append(slider);
	var textBox = $("<input>", {type: "text", id: textBoxId});
    colorSelect.append(textBox);
	textBox.on("change", function() {
      $("#" + sliderId).val($("#" + textBoxId).val());
      updateFilterColor($("#hueRange").val(), $("#saturationRange").val(), $("#brightnessRange").val(), $("#opacityRange").val())
    });
  });
  
  // set inital selection
  $("input[type=radio][value=druid]").attr("checked", true).trigger("change");
});