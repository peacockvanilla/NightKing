/**************************************************************************
 * Project:		FlipTimer - jQuery Countdown Timer
 * Info:		https://codecanyon.net/item/fliptimer-jquery-countdown-timer/21154062
 * Version:		1.0
 * Author: 		AthenaStudio
 * Profile: 	https://themeforest.net/user/athenastudio
**************************************************************************/

;(function($) {
	"use strict";
	
	$.fn.flipTimer = function(options) {		
		var timer;
		
		//API
		$.fn.flipTimer.style = function(bgColor, dividerColor, digitColor) {
			timer.style(bgColor, dividerColor, digitColor);
		};
		
		//Default variables
		var defaults = {
			date:"",
			timeZone:0,
			past:false,
			
			//The number of days to be shown
			dayTextNumber:2,
			
			//Show-Hide Day, Hour, Minute, Second
			showDay:true,
			showHour:true,
			showMinute:true,
			showSecond:true,
			
			//Texts
			dayText:"Days",
			hourText:"Hours",
			minuteText:"Minutes",
			secondText:"Seconds",
			
			//Flip style
			bgColor:"#333333",
			dividerColor:"#000000",
			digitColor:"#ffffff",
			textColor:"#666666",
			borderRadius:6,
			boxShadow:true,
			
			//Multi color
			multiColor:false,
			
			//Timer on finish function
			onFinish:function() {}
		};
		
		//Options
		var options = $.extend({}, defaults, options);
		
		//Window
		var win = $(window);
		
		//Create timer
		return this.each(function() {
			var container = $(this);
			container.addClass("fliptimer");
			timer = new FlipTimer($(this), options);
			timer.init();
		});
		
		//Countdown Timer class
		function FlipTimer($obj, options) {
			
			//Variables
			var $timer, $container;
			var currentTime, time, timeDiff;
			var flip_once = false;
			var intervalId = null;
			
			//Arrays
			var days = [], daysCurrent = [];
			var hours = [], hoursCurrent = [];
			var minutes = [], minutesCurrent = [];
			var seconds = [], secondsCurrent = [];
			
			//Count of numbers
			var dayTextNumber = parseInt(options.dayTextNumber, 10);
			var hourTextNumber = 2;
			var minuteTextNumber = 2;
			var secondTextNumber = 2;
			
			//Flip size
			var $flip;
			var $flipVal;
			var flipCount = 0;
			var size = 0;
			var elementSize = 0;
			var flipWidth = 60;
			
			//Multi color
			var maxR = parseInt(options.bgColor.slice(1, 3), 16);
			var maxG = parseInt(options.bgColor.slice(3, 5), 16);
			var maxB = parseInt(options.bgColor.slice(5, 7), 16);
			var R = maxR;
			var G = maxG;
			var B = maxB;
			var addR = 0;
			var addG = 0;
			var addB = 0;
			var intervalColorId = null;
			
			//Timer on finish function
			var onFinish = typeof(options.onFinish)=="function" ? options.onFinish : function() {};
			
			//Init
			this.init = function() {
				var that = this;
				$timer = $obj;
				
				//Add countdown CSS class
				$timer.addClass("fliptimer");
				
				//Container
				$container = $('<div class="flip-container" />');
				$timer.append($container);
				
				//Convert date string to time
				this.convertToTime();
				
				//On finish event
				$timer.onFinish = onFinish;
				
				//Day
				this.addItem("day", options.showDay, dayTextNumber, options.dayText);
				
				//Hour
				this.addItem("hour", options.showHour, hourTextNumber, options.hourText);
				
				//Minute
				this.addItem("minute", options.showMinute, minuteTextNumber, options.minuteText);
				
				//Second
				this.addItem("second", options.showSecond, secondTextNumber, options.secondText);
				
				//Item size
				this.itemSize();
				
				//Window resize
				$(window).resize(function() {
					that.findSize();
					that.flipStyle();					
				});
				
				//Flip style
				this.findSize();
				this.flipStyle();
				
				//Multi color
				if (options.multiColor) {
					this.intervalColorId = setInterval(function() {
						that.changeColor();
					}, 16);
				}
				
				//Start timer
				this.intervalId = setInterval(function() {
					that.checkTime();
				}, 1000);
				
				this.checkTime();
			};
			
			//Add item
			this.addItem = function(className, show, tickNumber, text) {
				if (!show) return;
				
				var html = '<div class="flip-wrapper '+className+'">\
								<div class="flips">';
				
				for (var i=0; i<tickNumber; i++) {				
						html += '<ul class="flip flip-'+i+'">'+
									this.tickItem(0, "before")+
									this.tickItem(0, "active")+
								'</ul>';
				}
				
				html += '	</div>\
							<span class="text">'+text+'</span>\
						 </div>';
				
				$container.append($(html));
			};
			
			//Tick item
			this.tickItem = function(value, className) {
				return '<li class="'+className+'">\
							<a href="#">\
								<div class="flip-up">\
									<div class="flip-shadow"></div>\
									<div class="flip-val">'+value+'</div>\
								</div>\
								<div class="flip-divider" />\
								<div class="flip-down">\
									<div class="flip-shadow"></div>\
									<div class="flip-val">'+value+'</div>\
								</div>\
							</a>\
						</li>';
			};
			
			//Find size
			this.itemSize = function() {
				if (options.showDay) 	{flipCount += dayTextNumber;}
				if (options.showHour) 	{flipCount += hourTextNumber;}
				if (options.showMinute) {flipCount += minuteTextNumber;}
				if (options.showSecond) {flipCount += secondTextNumber;}
				
				if (flipCount>1) {
					size += 2;
					
					if (flipCount>2) {
						size += 1;
						
						if (flipCount>3) {
							size += 2;
							
							if (flipCount>4) {
								size += 1;
								
								if (flipCount>5) {
									size += 2;
									
									if (flipCount>6) {
										size += 1;
										
										if (flipCount>7) {
											size += 2;
											
											if (flipCount>8) {
												size += 1;
											}
										}
									}
								}
							}
						}
					}
				}
			};
			
			//Find Size
			this.findSize = function() {
				var width = $timer.parent().width();
				elementSize = width/(size*flipWidth);
			};
			
			//Style
			this.style = function(bgColor, dividerColor, digitColor) {
				options.bgColor = bgColor;
				options.dividerColor = dividerColor;
				options.digitColor = digitColor;
				
				this.flipStyle();
			};
			
			//Flip style
			this.flipStyle = function() {
				$flip = $container.find("ul");
				
				var w = parseInt(elementSize*flipWidth, 10),
					h = parseInt(elementSize*flipWidth*1.3, 10),
					fs = (w);
				
				//Add px to border radius
				if (!isNaN(options.borderRadius)) options.borderRadius += "px";
				
				//Flip
				$flip.width(w).height(h);
				
				$flip.css({
					"font-size":fs+"px",
					"line-height":h+"px",
					"margin":parseInt(w/20, 10)+"px",
					"border-radius":options.borderRadius
				});
				
				if (options.boxShadow) {
					$flip.addClass("flip-box-shadow");
				}
				
				//Divider
				$flip.find(".flip-divider").css({
					"background":options.dividerColor,
				});		
				
				//Digit
				$flipVal = $flip.find(".flip-val");
				
				$flipVal.css({
					"background":options.bgColor,
					"color":options.digitColor,
					"border-radius":options.borderRadius
				});		
				
				//Down
				$flip.find(".flip-down").css({
					"border-bottom-left-radius":options.borderRadius,
					"border-bottom-right-radius":options.borderRadius
				});
				
				//Shadow effect
				$flip.find("li.active .flip-up .flip-shadow").css({
					"border-top-left-radius":options.borderRadius,
					"border-top-right-radius":options.borderRadius
				});
				
				//Text color
				$container.find(".text").css({
					"color":options.textColor,
				});
			};
			
			//Flip effect
			this.flip = function(className, i, changeNumber) {
				var $wrapper = $timer.find("."+className),
					$flip = $wrapper.find(".flip-"+i),
					$before = $flip.find(".before"),
					$active = $flip.find(".active");
					
				$before.find(".flip-val").text(changeNumber);
				$before.removeClass("before").addClass("active");
				$active.removeClass("active").addClass("before");
			};
			
			//Multi color
			this.changeColor = function() {
				R += addR;
				G += addG;
				B += addB;
				
				if ((R*addR>=maxR*addR) && (G*addG>=maxG*addG) && (B*addB>=maxB*addB)) {
					var color;
					
					R = maxR;
					G = maxG;
					B = maxB;
					
					color = maxR;
					
					maxR = maxG;
					maxG = maxB;
					maxB = color;
					
					addR = (maxR-R)/1000;
					addG = (maxG-G)/1000;
					addB = (maxB-B)/1000;
				}
				
				var newColor = "rgba("+Math.floor(R)+", "+Math.floor(G)+", "+Math.floor(B)+", 1)";
				
				$flipVal.css({
					"background":newColor
				});
			};
			
			//Check current time
			this.checkTime = function() {
				time = new Date();
				currentTime = time.getTime()+time.getTimezoneOffset()*60*1000;
				timeDiff = !options.past ? options.date-currentTime : currentTime-options.date;
				
				if (timeDiff<0) {						
					clearInterval(this.intervalId);
					timeDiff = 0;
					$timer.onFinish.call(this);
				}
				
				var currentTimeText = this.timeFormat(timeDiff);				
				var currentTimeChars = currentTimeText.split("");
				var i = 0, v = 0;
				
				/**************
				    - Day -
				**************/
				for(i = 0; i<dayTextNumber; i++) {
					days[i] = parseInt(currentTimeChars.shift(), 10);
				}
				
				var n = days.length-1;
				
				if (options.showDay && days[n]!=daysCurrent[n]) {
					this.flip("day", n, days[n]);
					daysCurrent[n] = days[n];
					
					if (n>0) {						
						for (i=0; i<n; i++) {
							if ((!options.past && daysCurrent[n]==9) || (options.past && daysCurrent[1]==0) || !flip_once) {								
								this.flip("day", i, days[i]);
							}
						}
					}
				}
				
				/***************
				    - Hour -
				***************/		
				for(i = 0; i<hourTextNumber; i++) {
					hours[i] = parseInt(currentTimeChars.shift(), 10);	
				}
				
				if (options.showHour && hours[1]!=hoursCurrent[1]) {
					//Right
					this.flip("hour", 1, hours[1]);
					hoursCurrent[1] = hours[1];
					
					//Left
					if ((!options.past && hoursCurrent[1]==9) || (options.past && hoursCurrent[1]==0) || !flip_once) {
						this.flip("hour", 0, hours[0]);
					}
					
					if (hoursCurrent[0]<1 && hoursCurrent[1]<2) {
						this.flip("hour", 0, hours[0]);
					}
					
					hoursCurrent[0] = hours[0];
				}
				
				/*****************
				    - Minute -
				*****************/				
				for(i = 0; i<minuteTextNumber; i++) {
					minutes[i] = parseInt(currentTimeChars.shift(), 10);
				}
				
				if (options.showMinute && minutes[1]!=minutesCurrent[1]) {
					//Right
					this.flip("minute", 1, minutes[1]);
					minutesCurrent[1] = minutes[1];
					
					//Left
					if ((!options.past && minutesCurrent[1]==9) || (options.past && minutesCurrent[1]==0) || !flip_once) {
						this.flip("minute", 0, minutes[0]);
					}
					
					minutesCurrent[0] = minutes[0];
				}
				
				/*****************
				    - Second -
				*****************/
				for(i=0; i<secondTextNumber; i++) {
					seconds[i] = parseInt(currentTimeChars.shift(), 10);
				}
				
				if (options.showSecond && seconds[1]!=parseInt(secondsCurrent[1], 10)) {
					//Right
					this.flip("second", 1, seconds[1]);
					secondsCurrent[1] = seconds[1];
					
					//Left
					if ((!options.past && secondsCurrent[1]==9) || (options.past && secondsCurrent[1]==0) || !flip_once) {
						this.flip("second", 0, seconds[0]);
					}
					
					secondsCurrent[0] = seconds[0];
				}
				
				flip_once = true;
			};
			
			//Text format
			this.textFormat = function(text, length, fillChar) {
				text = text.toString();
				
				while (text.length<length) {
					text = fillChar+text;
				}
				
				if (text.length>length) {
					text = text.substr(text.length-length,length);
				}
				
				return text;
			};

			//Time format
			this.timeFormat = function(msec) {
				var time = Math.floor(msec/1000);
				var s = time%60;
				var i = Math.floor(time%(60*60)/60);
				var h = Math.floor(time%(24*60*60)/(60*60));
				var d = Math.floor(time/(24*60*60));
				
				return this.textFormat(d, dayTextNumber, "0")+this.textFormat(h, hourTextNumber, "0")+this.textFormat(i, minuteTextNumber, "0")+this.textFormat(s, secondTextNumber, "0");
			};
			
			//Convert string to time
			this.convertToTime = function() {
				var time = options.date.split("/").join(" ").split(":").join(" ").split(" ");
				var y = parseInt(time[0], 10);
				var m = parseInt(time[1], 10)-1;
				var d = parseInt(time[2], 10);
				var h = parseInt(time[3], 10);
				var i = parseInt(time[4], 10)-options.timeZone*60;
				var s = parseInt(time[5], 10);
				options.date = new Date(y, m, d, h, i, s, 0).getTime();
			};
			
			//On finish event handler
			this.onFinish = function() {};
			
		}
	};
		
})(jQuery);