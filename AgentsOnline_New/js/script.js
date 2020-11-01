$(document).ready(function(){
    "use strict";
    init_validation();
    	
        
    var $slideHover = function() {
		$('.offer-slider-i').on({
			mouseenter: function(){
				$(this).find('.offer-slider-overlay').fadeIn(170);
				$(this).find('.offer-slider-btn').animate({top: "50%"}, 170);
			},
			mouseleave: function(){
				$(this).find('.offer-slider-overlay').fadeOut(170);
				$(this).find('.offer-slider-btn').css('top','-200px');
			}
		}, $(this));
	}

	
	var header_a = $('.header-a');
	var header_b = $('.header-b');
	var header_logo = $('.header-logo');
	var header_right = $('.header-right');
		
		
	var $headerDown = function() {
		header_a.slideUp(120);
//		header_b.css('height','79px');
//		header_b.addClass('fixed');
//		header_logo.css('margin-top','10px')
//		header_right.css('margin-top','21px');
		//header_logo.find('img').attr('src','img/logo-a.png');

	}
	var $headerUp = function() {
		header_a.slideDown(150);
		header_b.removeClass('fixed');
		header_b.css('height','89px');
		header_logo.css('margin-top','10px');
		header_right.css('margin-top','37px');
		//header_logo.find('img').attr('src','img/logo.png');
	}

	$(window).scroll(function(){
		var $scrollTop = $(window).scrollTop();
		if ( $scrollTop>140 ) {
			$headerDown();
		} else {
			$headerUp();
		}
        
        
	});

	$('.mobile-menu a.has-child').on('click',function(){
	 if ( $(this).is('.open') ) {
		$(this).removeClass('open');
		$(this).closest('li').find('ul').slideUp(); 
	 } else {
		$('.mobile-menu li ul').slideUp();
		$('.mobile-menu li a').removeClass('open');
		$(this).addClass('open');
		$(this).closest('li').find('ul').slideDown(); 
	 }

	 
	 return false;
	});

	$('.menu-btn').on('click',function(){
	 var mobile_menu = $('.mobile-menu');
	 if ( $(this).is('.open') ) {
		$(this).removeClass('open') 
		mobile_menu.slideUp(); 
	 } else {
		$(this).addClass('open') 
		mobile_menu.slideDown();
	 }
	 return false;
	 
	});


	$('.header-nav ul li').on({
		mouseenter: function() {
			$(this).find('ul').show();
		}, 
		mouseleave: function() {
			$(this).find('ul').hide();
		}
	});
	
	$('.header-lang').on({
		mouseenter: function() {
			$('.langs-drop').fadeIn();
		}, 
		mouseleave: function() {
			$('.langs-drop').hide();
		}
	});	
	
	$('.header-viewed').on({
		mouseenter: function() {
			$('.viewed-drop').fadeIn();
		},
		mouseleave: function() {
			$('.viewed-drop').hide();
		}
	});
	
	$('.header-curency').on({
		mouseenter: function() {
			$('.curency-drop').fadeIn();
		},
		mouseleave: function() {
			$('.curency-drop').hide();
		}
	});
	
	$('.flight-line .flight-line-b b').on('click',function(){
	  if ( $(this).is('.open') ) {
		$(this).removeClass('open');
		$(this).closest('.flight-line').find('.flight-details').slideUp();  
	  } else {
		$(this).addClass('open');
		$(this).closest('.flight-line').find('.flight-details').slideDown();  
	  }
	});
	$('.alt-flight .flight-line-b b').on('click',function(){
	  if ( $(this).is('.open') ) {
		$(this).removeClass('open');
		$(this).closest('.alt-flight').find('.alt-details').slideUp();  
	  } else {
		$(this).addClass('open');
		$(this).closest('.alt-flight').find('.alt-details').slideDown();  
	  }
	});
	$('.hdr-srch-btn').on('click',function(){
		$('.hdr-srch-overlay').fadeIn().find('input:text').focus();
		return false;
	});
	$('.srch-close').on('click',function(){
		$('.hdr-srch-overlay').fadeOut();
		return false;
	});

	$('.srch-lbl').on('click',function(){
		if ( $(this).is('.open') ) {
			$(this).closest('.search-tab-content').find('.search-asvanced').hide();
			$(this).text('Advanced Search Options').removeClass('open');
		} else {
			
			$(this).closest('.search-tab-content').find('.search-asvanced').fadeIn();
			$(this).text('Close Search Options').addClass('open');	
			
		}
	});

	$('.search-tab').on('click',function(){
		var $index = $(this).index();

       
      
        if($index==1)
        {
                 PageMethods.IsHotelBookingExist(CallTabSuccess($index), CallTabFailed)

        }
       else if($index==2)
        {
             PageMethods.IsHotelBookingExist(CallTabSuccess($index), CallTabFailed)
         }
        else if($index==3)
        {
             PageMethods.IsHotelBookingExist(CallTabSuccess($index), CallTabFailed)
         }
          else if($index==4)
        {
             PageMethods.IsHotelBookingExist(CallTabSuccess($index), CallTabFailed)
         }
       else
        {
            	$('.search-tab-content').hide().eq($index).fadeIn();
		        $('.search-tab').removeClass('active').eq($index).addClass('active');
        }
    

          if($index==1)
        {

        ShowTourChild();
        }
         if($index==3)
        {
         ShowTrfChild();
         }

        if($index==4)
        {
         ShowOthChild();
         }

        if($index==2)
        {
         ShowMAChild();
         }
		return false;
	});


    
            function CallTabSuccess(indx) {
               return function (response) {
       
                    if (response=='0')
                    {
                   // alert('test');
                           //showDialog('Confirmation', 'Are you sure to book services without hotel booking?.', 'prompt');
                            if (confirm("Are you sure to book services without hotel booking?.") == true) {
                                 $('.search-tab-content').hide().eq(indx).fadeIn();
		                        $('.search-tab').removeClass('active').eq(indx).addClass('active');
                          
                            } else {
                                $('.search-tab-content').hide().eq(0).fadeIn();
		                        $('.search-tab').removeClass('active').eq(0).addClass('active');
                            }
                    }
                    else
                    {
                     $('.search-tab-content').hide().eq(indx).fadeIn();
		                        $('.search-tab').removeClass('active').eq(indx).addClass('active');
                    }
                }
            }

            // alert message on some failure
            function CallTabFailed(res) {
        
            }

//	$('.authorize-terms-and-conditions').on('click',function(){	
//		$('.overlay-term-and-conditions').fadeIn(function(){
//			$('.autorize-popup-term-and-conditions').animate({top: '50%'}, 300);
//               $('.autorize-popup-term-and-condition').eq('0').css('display','block');
//			    $('.autorize-popup-term-and-condition').animate({top: '50%'}, 300);   

////            var $index = $(this).index();
////            alert($index);
////			$('.authorize-terms-and-conditionst').removeClass('current').eq($index).addClass('current');
////			$('.autorize-popup-term-and-condition').hide().eq($index).fadeIn();
//		});
//		return false;
//	});

//     $('.authorize-terms-and-conditions').on('click',function(){
//		if ( $(this).is('.autorize-popup-close-term-and-conditions') ) {
//			$('.autorize-popup-term-and-conditions').animate({top: '-300px'}, 300, function(){
//				$('.overlay-term-and-conditions').fadeOut();	
//			});
//		} else {
//			var $index = $(this).index();
//			$('.authorize-terms-and-conditionst').removeClass('current').eq($index).addClass('current');
//			$('.autorize-popup-term-and-condition').hide().eq($index).fadeIn();
//		}
//		return false;
//	});


	$('.header-account a').on('click',function(){	
		$('.overlay-popup').fadeIn(function(){
			$('.autorize-popup-popup').animate({top: '50%'}, 300).find('input:text').eq('0').focus();
		});
		return false;
	});

    $('.header-account input').on('click',function(){
      
            var hdpop = document.getElementById('hdPopup')
            hdpop.value='Y'

        //  $("#form1").validationEngine('attach', { promptPosition: "topRight", scroll: false });


            $('.overlay-popup').fadeIn(function(){
                $('.autorize-popup-popup').eq('0').css('display','block');
			    $('.autorize-popup-popup').animate({top: '50%'}, 300);             
		    });  
            return false;
	});

    
//	$('.overlay-popup').on('click',function(){
//    var hdpop = document.getElementById('hdPopup')
//    hdpop.value='N'

//		$('.autorize-popup-popup').animate({top: '-300px'}, 300, function(){
//          $('.autorize-popup-popup').eq('0').css('display','none');
//			$('.overlay-popup').fadeOut();	
//		});
//	});

	$('.autorize-tab-content-popup').eq('0').css('display','block');
	$('.autorize-popup-tabs a').on('click',function(){
		if ( $(this).is('.autorize-popup-close') ) {
             var hdpop = document.getElementById('hdPopup')
             hdpop.value='N'
             $('#form1').validationEngine('hideAll');
			 $('.autorize-popup-popup').animate({top: '-300px'}, 300, function(){
             $('.autorize-popup-popup').eq('0').css('display','none');
				$('.overlay-popup').fadeOut();	
			});
		} else {
         var hdpop = document.getElementById('hdPopup')
             hdpop.value='Y'
              
			var $index = $(this).index();
			$('.autorize-popup-tabs a').removeClass('current').eq($index).addClass('current');
			$('.autorize-tab-content-popup').hide().eq($index).fadeIn().find('input:text').eq('0').focus();
		}
		return false;
	});
    $('.autorize-popup-tabs input').on('click',function(){
		if ( $(this).is('.autorize-popup-close') ) {
         var hdpop = document.getElementById('hdPopup')
             hdpop.value='N'
			$('.autorize-popup-popup').animate({top: '-300px'}, 300, function(){
				$('.overlay-popup').fadeOut();	
			});
		} else {
         var hdpop = document.getElementById('hdPopup')
             hdpop.value='Y'
			var $index = $(this).index();
			$('.autorize-popup-tabs input').removeClass('current').eq($index).addClass('current');
			$('.autorize-tab-content-popup').hide().eq($index).fadeIn().find('input:text').eq('0').focus();
		}
		return false;
	});

    $('.autorize-tabs a').on('click',function(){
		if ( $(this).is('.autorize-close') ) {
			 $('.autorize-popup').animate({top: '-300px'}, 300, function(){
             $('.autorize-popup').eq('0').css('display','none');
			});
		} else {
              
			var $index = $(this).index();
			$('.autorize-tabs a').removeClass('current').eq($index).addClass('current');
			$('.autorize-tab-content').hide().eq($index).fadeIn().find('input:text').eq('0').focus();
             var hdpop = document.getElementById('hdTab')
             hdpop.value=$index
		}
		return false;
	});

	$('map area').on({
		mouseenter: function(){
			var $id = $(this).attr('id');
			$('.regions-holder .'+$id).css('background-position','left -177px');
			$('.regions-nav a.'+$id).addClass('chosen');
		},
		mouseleave: function(){
			var $id = $(this).attr('id');
			$('.regions-holder .'+$id).css('background-position','left 0px');
			$('.regions-nav a.'+$id).removeClass('chosen');
		}
	}, $(this));
	
	
	$('.regions-nav a').on({
		mouseenter: function(){
			var $id = $(this).attr('class');
			$('.regions-holder .'+$id).css('background-position','left -177px');
		},
		mouseleave: function(){
			var $id = $(this).attr('class');
			$('.regions-holder .'+$id).css('background-position','left 0px');
		}
	}, $(this));

	$('.gallery-i a').on('click',function(){
		var $href = $(this).attr('href');
		$('.gallery-i').removeClass('active');
		$(this).closest('.gallery-i').addClass('active');
		$('.tab-gallery-big img').attr('src',$href);
		return false;
	});
	$('.content-tabs-head a').on('click',function(){
		var $index = $(this).closest('li').index();
		$('.content-tabs-head a').removeClass('active');
		$('.content-tabs-head li').eq($index).find('a').addClass('active');
		$('.content-tabs-i').hide().eq($index).fadeIn();
		return false;
	});
	$('.faq-item-a').on('click',function(){
	  var $parent = $(this).closest('.faq-item');
	  if ($parent.is('.open')) {
		$parent.find('.faq-item-p').hide(); 
		$('.faq-item').removeClass('open');
	  } else {
		$('.faq-item').removeClass('open');
		$('.faq-item-p').hide();
		$parent.addClass('open').find('.faq-item-p').fadeIn();  
	  }
	});
	
	$('.h-tab-i a').on('click',function(){
	 var $index = $(this).closest('.h-tab-i').index();
	 $('.h-tab-i').removeClass('active');
	 $('.h-tab-i').eq($index).addClass('active');
	 
	 if ( $(this).is('.initMap') ) {
	 $('.tab-map').css('opacity','0');
	 $('#preloader').show();
	 $('.tab-item').hide().eq($index).fadeIn(function(){
		var mylat = '52.569334';
		var mylong ='13.380216';                
		var mapOptions = {
			zoom: 13,
			disableDefaultUI: true,
			zoomControl: true,
			zoomControlOptions: {
				style: google.maps.ZoomControlStyle.LARGE,
				position: google.maps.ControlPosition.LEFT_CENTER
			},
			center: new google.maps.LatLng(mylat, mylong), // New York 
		};
		var mapElement = document.getElementById('map');
		var map = new google.maps.Map(mapElement, mapOptions);
		google.maps.event.addDomListener(window, 'resize', init);
		google.maps.event.addListenerOnce(map, 'idle', function(){
			var place = new google.maps.LatLng(52.569334, 13.380216);   
			var image = new google.maps.MarkerImage('img/map.png',      
			new google.maps.Size(19, 29),      
			new google.maps.Point(0,0),      
			new google.maps.Point(0, 32));  
			var marker = new google.maps.Marker({
				map:map,
				icon: image, 
				draggable:false,
				animation: google.maps.Animation.DROP,
				position: place
			}); 
			
			$('.tab-map').css('opacity','1');
			$('#preloader').hide();	
			$('.map-contacts').each(function(index){	
				$(this).delay(141*index).fadeIn();     
			}); 
			
		});                 
		google.maps.event.trigger(map, 'resize'); 
	 }); 
	 } else {
		$('.tab-item').hide().eq($index).fadeIn();
	 } 
	 return false;
	});

	$('.tabs-nav a').on('click',function(){
	 var $parent = $(this).closest('.tabs-block')
	 var $index = $(this).closest('li').index();
	 $parent.find('.tabs-nav li a').removeClass('active');
	 $parent.find('.tabs-nav li').eq($index).find('a').addClass('active');
	 $parent.find('.tabs-content-i').hide().eq($index).fadeIn();
	 return false;
	});

	$('.accordeon-a').on('click',function(){
		var $parent = $(this).closest('.accordeon-item');
		$('.accordeon-item').removeClass('open');
		$('.accordeon-b').hide();
		$parent.addClass('open').find('.accordeon-b').fadeIn();
	});

	$('.toggle-trigger').on('click',function(){
		var $parent = $(this).closest('.toggle-i');
		if ( $parent.is('.open') ) {
			$parent.removeClass('open').find('.toggle-txt').hide();	
		} else {
			$parent.addClass('open').find('.toggle-txt').fadeIn();
		}
		return false;
	});

	$('.shareholder span').on('click',function(){
		if ( $(this).is('.open') ) {
			$('.share-popup').hide();
			$(this).removeClass('open');
		} else {
			$('.share-popup').fadeIn();
			$(this).addClass('open');	
		}

		return false;
	});

	$('.payment-tabs a').on('click',function(){
		var $index = $(this).index();
		$('.payment-tab').hide().eq($index).fadeIn();
		$('.payment-tabs a').removeClass('active').eq($index).addClass('active');
		return false;
	});

	$('.solutions-i').on({
		mouseenter: function(){
			$(this).find('.solutions-over').css('background','rgba(0,0,0,0.7)');
			$(this).find('.solutions-over-c').hide();
			$(this).find('.solutions-over-d').fadeIn(500);
		},
		mouseleave: function(){
			$(this).find('.solutions-over').css('background','rgba(0,0,0,0.5)');
			$(this).find('.solutions-over-d').hide();
			$(this).find('.solutions-over-c').fadeIn(700);
		}
	}, $(this));
		
		
//    
    $('.date-inpt').datepicker();
    


    var date = new Date();
    var currentMonth = date.getMonth();
    var currentDate = date.getDate();
    var currentYear = date.getFullYear();

//        $( ".date-inpt-check-in").click(function() {
//       
//            minDate: new Date(currentYear, currentMonth, currentDate)
//     });

    $( ".date-inpt-check-in").datepicker({

           minDate: new Date(currentYear, currentMonth, currentDate)

    });

    //changed by mohamed on 28/08/2018
    var date1 = new Date();
    var currentMonth = date1.getMonth()-2;
    var currentDate = date1.getDate();
    var currentYear = date1.getFullYear();
    $( ".date-inpt-check-in-freeform").datepicker({
           minDate: new Date(currentYear, currentMonth, currentDate) 
    });

     $( ".date-inpt-prehotel-check-in").datepicker({

           minDate: new Date(currentYear, currentMonth, currentDate)

    });

    $( ".date-inpt-travel-from").datepicker({

     
    });

//     $( ".date-inpt-oth-from").datepicker({

//           minDate: new Date(currentYear, currentMonth, currentDate)

//    });
//   



//         $( ".date-inpt-check-out").datepicker({
//            minDate: new Date(currentYear, currentMonth, currentDate)
//         });

//     var hdCheckIn = document.getElementById('hdCheckIn')
//     alert(hdCheckIn.value);
//     if(hdCheckIn.value=='')
//     {
//        $( ".date-inpt-check-out").datepicker({
//            minDate: new Date(currentYear, currentMonth, currentDate)
//         });
//     }
//     else
//     {
//        $( ".date-inpt-check-out").datepicker({
//        minDate: new Date(hdCheckIn.value)
//        //maxDate: "+1m +7d"
//        });
//     }
    

$('.custom-select').customSelect(); 
    
    $(".owl-slider").owlCarousel({
        items:4, 
        autoPlay: 5000,
        itemsDesktop : [1120,4], //5 items between 1000px and 901px
        itemsDesktopSmall : [900,2], // betweem 900px and 601px
        itemsTablet: [620,2], //2 items between 600 and 479
        itemsMobile: [479,1], //1 item between 479 and 0
        stopOnHover: true
    });

    $('#testimonials-slider').bxSlider({
        infiniteLoop: true,
        speed: 600,
        minSlides: 1,
        maxSlides: 1,
        moveSlides: 1,
        auto: false,
        slideMargin: 0
    });

    $slideHover();
	
	$(window).on('resize',function(){
		var $width = $(document).width();
		if ($width > 900) {
			$('.mobile-menu').hide();
			$('.menu-btn').removeClass('open');
		}
	});

});

function init_validation(target) {
	"use strict";
	function validate(target) {
		var valid = true;
		$(target).find('.req').each(function() {
			if($(this).val() == '') {
				valid = false;
				$(this).parent().addClass('errored');
			}
			else {
				$(this).parent().removeClass('errored');
			}
		});
		return valid;
	}
	
	$('form.w_validation').on('submit', function(e) {
		var valid = validate(this);
		if(!valid) e.preventDefault();
	});
	
	if(target) {return validate(target);}
}
