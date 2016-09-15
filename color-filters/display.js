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
  
  $.each(data, function(index, value) {
	var img = $("<div>", {"class": "display"}).css("background-image", 'url("images/spell-base.png")');
	img.append($("<img>", {"src": "images/spell-overlay.png", "class": value.toLowerCase() + "-color"}));
	img.append($("<p>", {text: value}));
	$("#container").append(img);
  });
});