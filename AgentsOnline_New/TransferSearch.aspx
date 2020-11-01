<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TransferSearch.aspx.vb" Inherits="TransferSearch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

  <meta name="description" content="" />
  <meta name="keywords" content="" />
  <meta charset="utf-8" /><link rel="icon" href="favicon.png" />
  <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no"> 
  <link rel="stylesheet" href="css/jquery-ui.css">
  <link rel="stylesheet" href="css/jquery.formstyler.css">
  <link rel="stylesheet" href="css/style.css" />
  <link id="lnkCSS" rel='stylesheet' type='text/css' href='css/style-common.css' />

     <%--***Danny 18/08/2018 fa fa-star--%>
     <link href="css/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
<%--  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">--%>


<%--    <link rel="stylesheet" href="css/Raleway.css" />
    <link rel="stylesheet" href="css/Raleway1.css" />
    <link rel="stylesheet" href="css/Raleway2.css" />
    <link rel="stylesheet" href="css/Montserrat.css" />
    <link rel="stylesheet" href="css/Lora.css" />
    <link rel="stylesheet" href="css/Lato.css" />
    <link rel="stylesheet" href="css/OpenSans.css" />
    <link rel="stylesheet" href="css/PTSans.css" />--%>

<%--        <link href='http://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lora:400,400italic' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:100,200,300,400,600,700,800' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,600' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700&amp;subset=latin,cyrillic' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lato:400,700&amp;subset=latin,latin-ext' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700&amp;subset=latin,cyrillic' rel='stylesheet' type='text/css' />--%>

    <script src="js/jquery-1.11.3.min.js"></script>
  <script src="js/jqeury.appear.js"></script>  
  <script src="js/jquery-ui.min.js"></script> 
  <script src="js/jquery.formstyler.js"></script>  
  <script src="js/owl.carousel.min.js"></script>
  <script src="js/bxSlider.js"></script>
  <script src="js/custom.select.js"></script>
  <script type="text/javascript" src="js/twitterfeed.js"></script>
  <script src="js/script.js"></script>
   <link rel="stylesheet" type="text/css" href="css/dialog_box.css" />
    <script type="text/javascript" src="js/dialog_box.js"></script>
    <meta name="viewport" content="initial-scale=1.0">
    <meta charset="utf-8">
  <style type="text/css">
  .mygrid
{
	width: 100%;
	min-width: 100%;
	border: 1px solid #EDE7E1;
	font-family:Raleway;
	color:#455051;

}
.mygrid-header
{
	background-color: #EDE7E1;
	font-family: Raleway;
	color: #455051;
	height: 25px;
	text-align: left;
	font-size: 16px;
	font-size:small;
	padding: 5px 5px 5px 5px;
}

.mygrid-rows
{

	font-family: Raleway;
	font-size: 14px;
	color: #455051;
	min-height: 25px;
	text-align: left;
	border: 1px solid #EDE7E1;
	vertical-align:middle;
	
}
.mygrid-rows:hover
{
	background-color: #fff;
	font-family: railway;
	color: #fff;
	text-align: left;
}
.mygrid-selectedrow
{
	background-color: #ff8000;
	font-family: Arial;
	color: #fff;
	font-weight: bold;
	text-align: left;
}
/**
.mygrid a /** FOR THE PAGING ICONS 
{
	background-color: Transparent;
	padding: 5px 5px 5px 5px;
	color: #fff;
	text-decoration: none;
	font-weight: bold;
}

.mygrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES
{
	background-color: #000;
	color: #fff;
}**/
.mygrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
{

	color:#455051;
	padding: 5px 5px 5px 5px;
	font-size:11px;
	vertical-align:middle;
}
.mygrid-pager
{
	background-color: #646464;
	font-family: Arial;
	color: White;
	height: 30px;
	text-align: left;
}

.mygrid td
{
	padding: 5px;
}
.mygrid th
{
	padding: 5px;
}
       
  
  </style>
  <%--<script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"   type="text/javascript"></script>--%>
      <script language="javascript" type="text/javascript">
          $(document).ready(function () {
              // Restrict mouse right button..
              $(document).bind("contextmenu", function (e) {
                  return false;
              });

              // Restrict browser back button..
              history.pushState(null, null, document.URL);
              window.addEventListener('popstate', function () {
                  history.pushState(null, null, document.URL);
              });


           


          });    
    </script>
  <script language="javascript" type="text/javascript">
      function myMap(lati, long) {

 if (!map) {

          var myLatlng = { lat: lati, lng: long };

          var map = new google.maps.Map(document.getElementById('map'), {
              zoom: 12,
              center: myLatlng
          });

          var marker = new google.maps.Marker({
              position: myLatlng,
              map: map,
              title: 'Click to zoom'
          });

          map.addListener('center_changed', function () {
              // 3 seconds after the center of the map has changed, pan back to the
              map.panTo(marker.getPosition());
//              window.setTimeout(function () {
//                  map.panTo(marker.getPosition());
//              }, 3);
          });

          marker.addListener('click', function () {
              map.setZoom(18);
              map.setCenter(marker.getPosition());
          });
      }
      else {
          google.maps.event.clearListeners(map);
      }
          if (map) {
              google.maps.event.addListenerOnce(map, 'idle', function () {
                  google.maps.event.trigger(map, 'resize');
                  CallPriceSlider();
              });
          }
      }



//        function ShowProgress() {
//        setTimeout(function () {
//            var modal = $('<div />');
//            modal.addClass("modal");
//            $('body').append(modal);
//            var loading = $(".loading");
//            loading.show();
//            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
//            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
//            loading.css({ top: top, left: left });
//        }, 200);
//     }
//    $('form').live("submit", function () {
//        ShowProgress();
//    });


  </script>

   <script language="javascript" type="text/javascript">

       function UpdatePrice() {
      
//           var lbNew = document.getElementById(lb);
//           lbNew.value = '100';
       }
       function validateDecimalOnly(evt, txt) {
           var theEvent = evt || window.event;
           var key = theEvent.keyCode || theEvent.which;
           if (key == 13) {
           }
           else {
               key = String.fromCharCode(key);

               var regex = /[0-9]/;
               if (!regex.test(key)) {
                   theEvent.returnValue = false;
                   if (theEvent.preventDefault) theEvent.preventDefault();
               }

               var charCode = (evt.which) ? evt.which : event.keyCode
               if (charCode == 46) {
                   var inputValue = txt.value
                   if (inputValue.indexOf('.') < 1) {
                       txt.value = txt.value + '.';
                       return true;
                   }
                   else {
                       return false;
                   }
               }

           }

       }

       function WriteRecentlyViewedHotel(hotelId, hotelName, location, price, hotelImage) {
              var html = '';
           html +='<div class='+'viewed-item'+'><div class='+'viewed-item-l'+'><a href='+'#'+'>';
           html += '<img src=ImageDisplay.aspx?FileName=' + hotelImage + ' width=79 height=61 /></a>'; //79*61
           html +='</div><div class='+'viewed-item-r'+'><div class='+'viewed-item-lbl'+'>';
           html += '<a href=' + '#' + '>' + hotelName + '</a></div>';
           html += '<div class=' + 'viewed-item-cat' + '>location: ' + location + '</div>';
           html += '<div class=' + 'viewed-price' + '>' + price + '</div></div><div class=' + 'clear' + '></div></div>';

           var fullhtml = document.getElementById("dvViewedItem").innerHTML
           document.getElementById("dvViewedItem").innerHTML = fullhtml + html;
           //alert(document.getElementById("dvViewedItem").innerHTML);
       }

       function WriteRecentlyViewedHotel1() {


//           $.ajax({
//               type: "POST",
//               url: "HotelSearch.aspx/GetMos",
//               data: '{}',
//               contentType: "application/json; charset=utf-8",
//               dataType: "json",
//               success: OnSuccess,
//               failure: function (response) {
//                   alert(response.responseText);
//               },
//               error: function (response) {
//                   alert(response.responseText);
//               }
//           });


           var html = '';
           html += '<div class=' + 'viewed-item' + '><div class=' + 'viewed-item-l' + '><a href=' + '#' + '>';
           html += '<img src=ImageDisplay.aspx?FileName=' + hotelImage + ' width=79 height=61 /></a>'; //79*61
           html += '</div><div class=' + 'viewed-item-r' + '><div class=' + 'viewed-item-lbl' + '>';
           html += '<a href=' + '#' + '>' + hotelName + '</a></div>';
           html += '<div class=' + 'viewed-item-cat' + '>location: ' + location + '</div>';
           html += '<div class=' + 'viewed-price' + '>' + price + '</div></div><div class=' + 'clear' + '></div></div>';

           var fullhtml = document.getElementById("dvViewedItem").innerHTML
           document.getElementById("dvViewedItem").innerHTML = fullhtml + html;
           //alert(document.getElementById("dvViewedItem").innerHTML);
       }

       function mUp(obj) {
               document.getElementById("<%= btnFilter.ClientID %>").click();
           }


           function ValidateDepDate() {

               var txtArrdate = document.getElementById("<%=txtTrfArrivaldate.ClientID%>");
               var txtDepdate = document.getElementById("<%=txtTrfDeparturedate.ClientID%>");

              
               var dp = txtArrdate.value.split("/");
               var newChkInDt = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);

               var dp1 = txtDepdate.value.split("/");
               var newChkOutDt = new Date(dp1[2] + "/" + dp1[1] + "/" + dp1[0]);

               newChkInDt = getFormatedDate(newChkInDt);
               newChkOutDt = getFormatedDate(newChkOutDt);

               newChkInDt = new Date(newChkInDt);
               newChkOutDt = new Date(newChkOutDt);
               if (newChkInDt > newChkOutDt) {
                   txtDepdate.value = txtArrdate.value;

                   showDialog('Alert Message', 'Departure date should be Greater than Arrival date', 'warning');
                   return false;
               }
           }

           function getFormatedDate(chkdate) {
               var dd = chkdate.getDate();
               var mm = chkdate.getMonth() + 1; //January is 0!
               var yyyy = chkdate.getFullYear();
               if (dd < 10) { dd = '0' + dd };
               if (mm < 10) { mm = '0' + mm };
               chkdate = mm + '/' + dd + '/' + yyyy;
               return chkdate;
           }

           function fnReadOnly(txt) {
               event.preventDefault();
           }
           function fnConfirmHome(sRequestId) {
               var msg
               if (sRequestId == 'New') {
                   msg = 'Do you want to Abandon existing new booking ?'
               }
               else {
                   msg = 'Do you want to Abandon edit option ' + sRequestId + ' ?'
               }
               if (confirm(msg) == true) {

                   document.getElementById("<%= btnConfirmHome.ClientID %>").click();

               }
               else {
                   return false;
               }


           }

       function ValidateSearch() {

           ShowProgess();

          var chkarrival = document.getElementById('<%=chkarrival.ClientID%>').checked;
          var chkDeparture = document.getElementById('<%=chkDeparture.ClientID%>').checked;
    

         

           if (document.getElementById('<%=txtTrfArrivaldate.ClientID%>').value == '' && chkarrival == true) {
               showDialog('Alert Message', 'Please select  Arrival date.', 'warning');
               HideProgess();
               return false;
           }

           if (document.getElementById('<%=txtTrfArrivalpickup.ClientID%>').value == '' && chkarrival == true) {
               showDialog('Alert Message', 'Please select   Arrival Pickup.', 'warning');
               HideProgess();
               return false;
           }

           if (document.getElementById('<%=txtArrivalflight.ClientID%>').value == '' && document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>').value == '' && chkarrival == true) {

               showDialog('Alert Message', 'Please select   Arrival Flight/Arrival Pickup.', 'warning');
               HideProgess();
               return false;
           }


//           if (document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>').value == '' && chkarrival == true) {
//               showDialog('Alert Message', 'Please select   Arrival Pickup.', 'warning');
//               HideProgess();
//               return false;
//           }


           if (document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>').value == '' && chkarrival == true) {
               showDialog('Alert Message', 'Please select   Arrival Drop Off.', 'warning');
               HideProgess();
               return false;
           }


           if (document.getElementById('<%=txtTrfDeparturedate.ClientID%>').value == '' && chkDeparture == true) {
               alert(chkDeparture);
               showDialog('Alert Message', 'Please select  Departure date.', 'warning');
               HideProgess();
               return false;
           }

           if (document.getElementById('<%=txtTrfDepairportdrop.ClientID%>').value == '' && chkDeparture == true) {
               showDialog('Alert Message', 'Please select   Departure Airport Drop Off.', 'warning');
               HideProgess();
               return false;
           }


           if (document.getElementById('<%=txtDepartureFlight.ClientID%>').value == '' && document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>').value == '' && chkDeparture == true) {
               showDialog('Alert Message', 'Please select   Departure Flight/Airport Drop Off.', 'warning');
               HideProgess();
               return false;
           }


           if (document.getElementById('<%=txtTrfDeppickupcode.ClientID%>').value == '' && chkDeparture == true) {
               showDialog('Alert Message', 'Please select   Departure Pickup.', 'warning');
               HideProgess();
               return false;
           }

           if ((document.getElementById('<%=ddlTrfAdult.ClientID%>').value == '0') && (document.getElementById('<%=hdOPMode.ClientID%>').value != 'Edit') ) {
               showDialog('Alert Message', 'Please select any number of adult.', 'warning');
               HideProgess();
               return false;
           }


           if (document.getElementById('<%=txtTrfSourcecountry.ClientID%>').value == '') {
               showDialog('Alert Message', 'Please enter Transfer source country.', 'warning');
               HideProgess();
               return false;
           }

//           if (document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>').value == '' && chkDeparture == true) {
//               showDialog('Alert Message', 'Please select   Departure Airport Drop Off.', 'warning');
//               HideProgess();
//               return false;
//           }

           var child = document.getElementById('<%=ddlTrfChild.ClientID%>').value;
           if (child != '0') {
               var child1 = document.getElementById('<%=txtTrfChild1.ClientID%>').value;
               var child2 = document.getElementById('<%=txtTrfChild2.ClientID%>').value;
               var child3 = document.getElementById('<%=txtTrfChild3.ClientID%>').value;
               var child4 = document.getElementById('<%=txtTrfChild4.ClientID%>').value;
               var child5 = document.getElementById('<%=txtTrfChild5.ClientID%>').value;
               var child6 = document.getElementById('<%=txtTrfChild6.ClientID%>').value;
               var child7 = document.getElementById('<%=txtTrfChild7.ClientID%>').value;
               var child8 = document.getElementById('<%=txtTrfChild8.ClientID%>').value;
               if (child == 1) {

                   if (child1 == 0) {
                       showDialog('Alert Message', 'Please select child age.', 'warning');
                       HideProgess();
                       return false;
                   }

               }
               else if (child == 2) {
                   if (child1 == 0 || child2 == 0) {
                       showDialog('Alert Message', 'Please select child age.', 'warning');
                       HideProgess();
                       return false;
                   }
               }
               else if (child == 3) {
                   if (child1 == 0 || child2 == 0 || child3 == 0) {
                       showDialog('Alert Message', 'Please select child age.', 'warning');
                       HideProgess();
                       return false;
                   }
               }
               else if (child == 4) {
                   if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0) {
                       showDialog('Alert Message', 'Please select child age.', 'warning');
                       HideProgess();
                       return false;
                   }
               }
               else if (child == 5) {
                   if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0 || child5 == 0) {
                       showDialog('Alert Message', 'Please select child age.', 'warning');
                       HideProgess();
                       return false;
                   }
               }
               else if (child == 6) {
                   if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0 || child5 == 0 || child6 == 0) {
                       showDialog('Alert Message', 'Please select child age.', 'warning');
                       HideProgess();
                       return false;
                   }
               }
               else if (child == 7) {
                   if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0 || child5 == 0 || child6 == 0 || child7 == 0) {
                       showDialog('Alert Message', 'Please select child age.', 'warning');
                       HideProgess();
                       return false;

                   }
               }
               else if (child == 8) {
                   if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0 || child5 == 0 || child6 == 0 || child7 == 0 || child8 == 0) {
                       showDialog('Alert Message', 'Please select child age.', 'warning');
                       HideProgess();
                       return false;
                   }
               }

           }

           return true;
        
       }

   
         

       function AutoCompleteExtender_ArrivalFlight_KeyUp() {
           $("#<%= txtArrivalflight.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtArrivalflight.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtArrivalflightCode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });

           $("#<%= txtArrivalflight.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtArrivalflight.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtArrivalflightCode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });
       }

       function AutoCompleteExtender_DepartureFlight_KeyUp() {
           $("#<%= txtDepartureFlight.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtDepartureFlight.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtDepartureFlightCode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });

           $("#<%= txtDepartureFlight.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtDepartureFlight.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtDepartureFlightCode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });
       }




       function AutoCompleteExtender_TrfArrivalpickupcode_KeyUp() {
           $("#<%= txtTrfArrivalpickup.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfArrivalpickup.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });

           $("#<%= txtTrfArrivalpickup.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfArrivalpickup.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });
       }


        function AutoCompleteExtender_TrfArrDropoff_KeyUp() {
            $("#<%= txtTrfArrDropoff.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfArrDropoff.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfArrDroptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });

            $("#<%= txtTrfArrDropoff.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfArrDropoff.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfArrDroptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });
        }

       function AutoCompleteExtender_TrfCustomer_KeyUp() {
           $("#<%= txtTrfCustomer.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfCustomer.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfCustomercode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });

           $("#<%= txtTrfCustomer.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfCustomer.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfCustomercode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });
       }


       function AutoCompleteExtender_TrfSourcecountry_KeyUp() {
           $("#<%= txtTrfSourcecountry.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfSourcecountry.ClientID%>');
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
               }
           });

           $("#<%= txtTrfSourcecountry.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfSourcecountry.ClientID%>');
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
               }
           });
       }


       function AutoCompleteExtender_TrfDepairportdrop_KeyUp() {
           $("#<%= txtTrfDepairportdrop.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfDepairportdrop.ClientID%>');
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
               }
           });

           $("#<%= txtTrfDepairportdrop.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfDepairportdrop.ClientID%>');
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
               }
           });
       }

       function AutoCompleteExtender_TrfDeppickup_KeyUp() {
           $("#<%= txtTrfDeppickup.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfDeppickupcode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfDeppickup.ClientID%>');
               var hiddenfieldID2 = document.getElementById('<%=txtTrfDeppickuptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
                   hiddenfieldID2.value = '';
               }
           });

           $("#<%= txtTrfDeppickup.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtTrfDeppickupcode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtTrfDeppickup.ClientID%>');
               var hiddenfieldID2 = document.getElementById('<%=txtTrfDeppickuptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
                   hiddenfieldID2.value = '';
               }
           });
       }


        function AutoCompleteExtender_TrfInterpickup_KeyUp() {
            $("#<%= txtTrfinterPickup.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfinterPickupcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfinterPickup.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfinterPickuptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });

            $("#<%= txtTrfinterPickup.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfinterPickupcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfinterPickup.ClientID%>');
                 var hiddenfieldID2 = document.getElementById('<%=txtTrfinterPickuptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });
        }

     function AutoCompleteExtender_TrfInterDrop_KeyUp() {
            $("#<%= txtTrfInterdropff.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfInterdropffcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfInterdropff.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfInterdropfftype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });

            $("#<%= txtTrfInterdropff.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfInterdropffcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfInterdropff.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfInterdropfftype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });
        }


       function ArrivalflightSetContextKey() {
           $find('<%=AutoCompleteExtender_txtArrivalflight.ClientID%>').set_contextKey($get("<%=txtTrfArrivaldate.ClientID %>").value);

       }

       function DepartureflightSetContextKey() {
           $find('<%=AutoCompleteExtender_txtDepartureFlight.ClientID%>').set_contextKey($get("<%=txtTrfDeparturedate.ClientID %>").value);

       }


       function ArrivalflightAutocompleteSelected(source, eventArgs) {
           if (source) {
               // Get the HiddenField ID.
               var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalflightCode");
               var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalTime");
               var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtTrfArrivalpickup");
               var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtTrfArrivalpickupcode");


               $get(hiddenfieldID).value = eventArgs.get_value();
               GetAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
           }

       }

        function CheckFlight(Flightcode) {
        
         if ($("#txtArrivalflight").val() !=''){
        
             $.ajax({
                type: "POST",
                url: "Home.aspx/CheckFlight",
                data: '{Flightcode:  "' + $("#txtArrivalflight").val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessFlight,
                failure: function (response) {
                    alert('failure');
                    alert(response.d);
                },
                error: function (response) {
                    alert('error');
                    alert(response.d);
                }
            });

            }
    }

     function OnSuccessFlight(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Countries = xml.find("Flightdetails");
        var rowCount = Countries.length;

        if (rowCount == 0){
             showDialog('Alert Message', 'Kindly choose the correct flight details from the provided Arrival Flight List .', 'warning');
             document.getElementById('<%=txtArrivalflight.ClientID%>').value = '';
            return false;
        }
    };

     function CheckDepFlight(Flightcode) {
      
      if ($("#txtDepartureFlight").val() !=''){
         $.ajax({
          
            type: "POST",
            url: "Home.aspx/CheckDepFlight",
            data: '{Flightcode:  "' + $("#txtDepartureFlight").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessDepFlight,
            failure: function (response) {
                alert('failure');
                alert(response.d);
            },
            error: function (response) {
                alert('error');
                alert(response.d);
            }
        });

      }
    }

     function OnSuccessDepFlight(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Countries = xml.find("DepFlightdetails");
        var rowCount = Countries.length;

        if (rowCount == 0){
             showDialog('Alert Message', 'Kindly choose the correct flight details from the provided Departure Flight List ', 'warning');
         
            document.getElementById('<%=txtDepartureFlight.ClientID%>').value = '';
            return false;
        }
    };



       function GetAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
           $.ajax({
               type: "POST",
               url: "Home.aspx/GetAirportAndTimeDetails",
               data: '{flightcode:  "' + flightcode + '"}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   var xmlDoc = $.parseXML(response.d);
                   var xml = $(xmlDoc);
                   var customers = xml.find("Customers");

                   $.each(customers, function () {
                       var customer = $(this);
                       $get(hiddenfieldIDTime).value = $(this).find("destintime").text();
                       $get(hiddenfieldIDAirport).value = $(this).find("airportbordername").text();
                       $get(hiddenfieldIDAirportcode).value = $(this).find("airportbordercode").text();
                   });

               },
               failure: function (response) {
                   alert('failure');
                   alert(response.d);
               },
               error: function (response) {
                   alert('error');
                   alert(response.d);
               }
           });
       }


       function TrfArrivalpickupAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>').value = eventArgs.get_value();
               }
               else {
                   document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>').value = '';
               }
           }

       }

       function TrfArrDropoffAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                     <%--Added Shahul 19/07/2018--%>
                    var code = eventArgs.get_value();   
                    code = code.split(';');   
                    //alert(code[1]);
                    document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>').value = code[0];
                    document.getElementById('<%=txtTrfArrDroptype.ClientID%>').value = code[1];   
                }
                else {
                    document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>').value = '';
                    document.getElementById('<%=txtTrfArrDroptype.ClientID%>').value = '';  
                }
            }
        }

       function GetArrivaldate(DropCode) {
           $.ajax({
               type: "POST",
               url: "TransferSearch.aspx/GetArrivaldate",
               data: '{DropCode:  "' + DropCode + '"}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: OnSuccessArr,
               failure: function (response) {
                   alert('failure');
                   alert(response.d);
               },
               error: function (response) {
                   alert('error');
                   alert(response.d);
               }
           });
       }

       function OnSuccessArr(response) {
           var xmlDoc = $.parseXML(response.d);
           var xml = $(xmlDoc);
           var Countries = xml.find("Arrivaldates");
           var rowCount = Countries.length;

           if (rowCount == 1) {
               $.each(Countries, function () {

                   document.getElementById('<%=txtTrfArrivaldate.ClientID%>').value = $(this).find("checkin").text();
                   var ddlAdult = document.getElementById('<%=ddlTrfAdult.ClientID%>');
                   ddlAdult.value = $(this).find("adults").text();
                   $('.custom-select-ddlTrfAdult').next('span').children('.customSelectInner').text(ddlAdult.value);

                   var ddlChild = document.getElementById('<%=ddlTrfChild.ClientID%>');
                   ddlChild.value = $(this).find("child").text();
                   $('.custom-select-ddlTrfChild').next('span').children('.customSelectInner').text(ddlChild.value);

                  
                   var strchildage = [];
                   strchildage = $(this).find("childages").text().split(";");
                  

                   for (i = 0; i <= strchildage.length - 1; i++) {

                       strRoomchildage = ("ddlTrfChild" + [i + 1]);

                       var ddlchildage = document.getElementById(strRoomchildage);
                       var ddlRoomChild = '.custom-select-' + strRoomchildage
                       ddlchildage.value = strchildage[i];
                       $(ddlRoomChild).next('span').children('.customSelectInner').text(ddlchildage.value);

                   }


               });
           }
         

           ShowTrfChild();
       };

       

    


        function InterPickupautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                 <%--Added Shahul 19/07/2018--%>
                    var code = eventArgs.get_value();   
                    code = code.split(';');  
                document.getElementById('<%=txtTrfinterPickupcode.ClientID%>').value = code[0]; 
                document.getElementById('<%=txtTrfinterPickuptype.ClientID%>').value = code[1]; 
            }
            else {
                document.getElementById('<%=txtTrfinterPickupcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfinterPickuptype.ClientID%>').value = '';
            }
        }

       function GetIntertransferdate(Pickupcode) {
           $.ajax({
               type: "POST",
               url: "TransferSearch.aspx/GetIntertransferdate",
               data: '{Pickupcode:  "' + Pickupcode + '"}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: OnSuccessInter,
               failure: function (response) {
                   alert('failure');
                   alert(response.d);
               },
               error: function (response) {
                   alert('error');
                   alert(response.d);
               }
           });
       }

       function OnSuccessInter(response) {
           var xmlDoc = $.parseXML(response.d);
           var xml = $(xmlDoc);
           var Countries = xml.find("Interdates");
           var rowCount = Countries.length;

           if (rowCount == 1) {
               $.each(Countries, function () {

                   document.getElementById('<%=txtTrfinterdate.ClientID%>').value = $(this).find("checkout").text();
                   document.getElementById('<%=txtTrfInterdropffcode.ClientID%>').value = $(this).find("partycode").text();
                   document.getElementById('<%=txtTrfInterdropff.ClientID%>').value = $(this).find("partyname").text();
                  
               });
           }
          
       };


      function InterDropoffautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                 <%--Added Shahul 19/07/2018--%>
                    var code = eventArgs.get_value();   
                    code = code.split(';'); 
                   // alert(code[1]);
                document.getElementById('<%=txtTrfInterdropffcode.ClientID%>').value = code[0]; 
                document.getElementById('<%=txtTrfInterdropfftype.ClientID%>').value = code[1]; 
            }
            else {
                document.getElementById('<%=txtTrfInterdropffcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfInterdropfftype.ClientID%>').value = '';
            }
        }


       function DepartureAutocompleteSelected(source, eventArgs) {
           if (source) {

               var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtDepartureFlightCode");
               var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtDepartureTime");
               var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtTrfDepairportdrop");
               var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtTrfDepairportdropcode");

               $get(hiddenfieldID).value = eventArgs.get_value();
               GetDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
           }

       }


        function TrfDeppickupAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                 <%--Added Shahul 19/07/2018--%>
                    var code = eventArgs.get_value();   
                    code = code.split(';');   
                    document.getElementById('<%=txtTrfDeppickupcode.ClientID%>').value =code[0];;
                    document.getElementById('<%=txtTrfDeppickuptype.ClientID%>').value = code[1];   
                }
                else {
                    document.getElementById('<%=txtTrfDeppickupcode.ClientID%>').value = '';
                }
            }
        }

       function GetDeparturedate(Pickupcode) {
           $.ajax({
               type: "POST",
               url: "TransferSearch.aspx/GetDeparturedate",
               data: '{Pickupcode:  "' + Pickupcode + '"}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: OnSuccessdepdate,
               failure: function (response) {
                   alert('failure');
                   alert(response.d);
               },
               error: function (response) {
                   alert('error');
                   alert(response.d);
               }
           });
       }

       function OnSuccessdepdate(response) {
           var xmlDoc = $.parseXML(response.d);
           var xml = $(xmlDoc);
           var Countries = xml.find("Departdates");
           var rowCount = Countries.length;

           if (rowCount == 1) {
               $.each(Countries, function () {

                   document.getElementById('<%=txtTrfDeparturedate.ClientID%>').value = $(this).find("checkout").text();
                   var ddlAdult = document.getElementById('<%=ddlTrfAdult.ClientID%>');
                   ddlAdult.value = $(this).find("adults").text();
                   $('.custom-select-ddlTrfAdult').next('span').children('.customSelectInner').text(ddlAdult.value);

                   var ddlChild = document.getElementById('<%=ddlTrfChild.ClientID%>');
                   ddlChild.value = $(this).find("child").text();
                   $('.custom-select-ddlTrfChild').next('span').children('.customSelectInner').text(ddlChild.value);


                   var strchildage = [];
                   strchildage = $(this).find("childages").text().split(";");


                   for (i = 0; i <= strchildage.length - 1; i++) {

                       strRoomchildage = ("ddlTrfChild" + [i + 1]);

                       var ddlchildage = document.getElementById(strRoomchildage);
                       var ddlRoomChild = '.custom-select-' + strRoomchildage
                       ddlchildage.value = strchildage[i];
                       $(ddlRoomChild).next('span').children('.customSelectInner').text(ddlchildage.value);

                   }


               });
           }
        

           ShowTrfChild();

       };


       function TrfCustomerAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtTrfCustomercode.ClientID%>').value = eventArgs.get_value();
                   $find('AutoCompleteExtender_txtTrfSourcecountry').set_contextKey(eventArgs.get_value());
                   GetTrfCountryDetails(eventArgs.get_value());

               }
               else {
                   document.getElementById('<%=txtTrfCustomercode.ClientID%>').value = '';
               }
           }
       }


       function GetTrfCountryDetails(CustCode) {
           $.ajax({
               type: "POST",
               url: "TransferSearch.aspx/GetTrfCountryDetails",
               data: '{CustCode:  "' + CustCode + '"}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: OnSuccessTrf,
               failure: function (response) {
                   alert('failure');
                   alert(response.d);
               },
               error: function (response) {
                   alert('error');
                   alert(response.d);
               }
           });
       }

       function OnSuccessTrf(response) {
           var xmlDoc = $.parseXML(response.d);
           var xml = $(xmlDoc);
           var Countries = xml.find("TrfCountries");
           var rowCount = Countries.length;

           if (rowCount == 1) {
               $.each(Countries, function () {
                   document.getElementById('<%=txtTrfSourcecountry.ClientID%>').value = ''
                   document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>').value = '';
                   document.getElementById('<%=txtTrfSourcecountry.ClientID%>').value = $(this).find("ctryname").text();
                   document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>').value = $(this).find("ctrycode").text();
                   document.getElementById('<%=txtTrfSourcecountry.ClientID%>').setAttribute("readonly", true);
                   $find('AutoCompleteExtender_txtTrfSourcecountry').setAttribute("Enabled", false);
               });
           }
           else {
               document.getElementById('<%=txtTrfSourcecountry.ClientID%>').value = ''
               document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>').value = '';
               document.getElementById('<%=txtTrfSourcecountry.ClientID%>').removeAttribute("readonly");
               $find('AutoCompleteExtender_txtTrfSourcecountry').setAttribute("Enabled", true);
           }
       };



       function TrfCountryautocompleteselected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtTrfSourcecountrycode	.ClientID%>').value = eventArgs.get_value();
                   $find('AutoCompleteExtender_txtTrfSourcecountry').set_contextKey(eventArgs.get_value());
               }
               else {
                   document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>').value = '';
               }
           }
       }

       function TrfDepairportdropAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>').value = eventArgs.get_value();
               }
               else {
                   document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>').value = '';
               }
           }
       }

       function calculatevaluedep(chk, txtunits, txtunitprice, txttotalamt, rowid, chkoverride, txtwlunitprice, txtwltotalamt) {



           chk1 = document.getElementById(chk);
           txtunits = document.getElementById(txtunits);
           txtunitprice = document.getElementById(txtunitprice);

           txttotalamt = document.getElementById(txttotalamt);

           chkoverride = document.getElementById(chkoverride);
           txtwlunitprice = document.getElementById(txtwlunitprice);
           txtwltotalamt = document.getElementById(txtwltotalamt);
         


           if (txtunits.value == 0) {
               txtunits.value = '1';
           }

           var totalamt = (parseFloat(txtunitprice.value) * parseFloat(txtunits.value));
           var totalwlamt = (parseFloat(txtwlunitprice.value) * parseFloat(txtunits.value));
           txttotalamt.value = totalamt;
           txtwltotalamt.value = totalwlamt;
           //Modified param 29/10/2018
            document.getElementById("<%= btnSelectedDeparture.ClientID %>").click();
//           if (chk1.checked) {
//               document.getElementById("<%= btnSelectedDeparture.ClientID %>").click();
//           }

       }


       function calculatevalueinter(chk, txtunits, txtunitprice, txttotalamt, rowid, chkoverride, txtwlunitprice, txtwltotalamt) {



           chk1 = document.getElementById(chk);
           txtunits = document.getElementById(txtunits);
           txtunitprice = document.getElementById(txtunitprice);

           txttotalamt = document.getElementById(txttotalamt);

           chkoverride = document.getElementById(chkoverride);

           txtwlunitprice = document.getElementById(txtwlunitprice);
           txtwltotalamt = document.getElementById(txtwltotalamt);


           if (txtunits.value == 0) {
               txtunits.value = '1';
           }

           var totalamt = (parseFloat(txtunitprice.value) * parseFloat(txtunits.value));
           var totalwlamt = (parseFloat(txtwlunitprice.value) * parseFloat(txtunits.value));
           txttotalamt.value = totalamt;
           txtwltotalamt.value = totalwlamt;

           //Modified param 29/10/2018
           document.getElementById("<%= btnSelectedInter.ClientID %>").click();
//           if (chk1.checked) {
//               document.getElementById("<%= btnSelectedInter.ClientID %>").click();
//           }

           

       }


       function calculatevalue(chk, txtunits, txtunitprice, txttotalamt, rowid,chkoverride) {

          

           chk1 = document.getElementById(chk);
           txtunits = document.getElementById(txtunits);
           txtunitprice = document.getElementById(txtunitprice);

           txttotalamt = document.getElementById(txttotalamt);
          
           chkoverride = document.getElementById(chkoverride);



           if (txtunits.value == 0) {
           txtunits.value='1';
            }

           var totalamt = (parseFloat(txtunitprice.value) * parseFloat(txtunits.value));
           txttotalamt.value = totalamt;

           if (chk1.checked ) {
               document.getElementById("<%= btnSelectedArrival.ClientID %>").click();
           }

       }


       function calculatevalueWithWl(chk, txtunits, txtunitprice, txttotalamt, rowid, chkoverride, txtwlunitprice, txtwltotalamt) {



           chk1 = document.getElementById(chk);
           txtunits = document.getElementById(txtunits);
           txtwlunitprice = document.getElementById(txtwlunitprice);
           txtunitprice = document.getElementById(txtunitprice);

           txttotalamt = document.getElementById(txttotalamt);
           txtwltotalamt = document.getElementById(txtwltotalamt);
           chkoverride = document.getElementById(chkoverride);

  
           if (txtunits.value == 0) {
               txtunits.value = '1';
           }

           var totalamt = (parseFloat(txtunitprice.value) * parseFloat(txtunits.value));
           var totalwlamt = (parseFloat(txtwlunitprice.value) * parseFloat(txtunits.value));
           txttotalamt.value = totalamt;
           txtwltotalamt.value = totalwlamt;

           //Modified param 25/10/2018
           document.getElementById("<%= btnSelectedArrival.ClientID %>").click();
//           if (chk1.checked) {
//               document.getElementById("<%= btnSelectedArrival.ClientID %>").click();
//           }

       }

       function GetDepartureAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
           $.ajax({
               type: "POST",
               url: "TransferSearch.aspx/GetDepartureAirportAndTimeDetails",
               data: '{flightcode:  "' + flightcode + '"}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   var xmlDoc = $.parseXML(response.d);
                   var xml = $(xmlDoc);
                   var customers = xml.find("Customers");

                   $.each(customers, function () {
                       var customer = $(this);
                       $get(hiddenfieldIDTime).value = $(this).find("destintime").text();
                       $get(hiddenfieldIDAirport).value = $(this).find("airportbordername").text();
                       $get(hiddenfieldIDAirportcode).value = $(this).find("airportbordercode").text();
                   });

               },
               failure: function (response) {
                   alert('failure');
                   alert(response.d);
               },
               error: function (response) {
                   alert('error');
                   alert(response.d);
               }
           });
       }

    </script>


    
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            //            AutoCompleteExtender_ArrivalFlight_KeyUp();
            //            AutoCompleteExtender_DepartureFlight_KeyUp();
            //            AutoCompleteExtender_TrfDeppickup_KeyUp();

            //            AutoCompleteExtender_TrfSourcecountry_KeyUp();
            //            AutoCompleteExtender_TrfCustomer_KeyUp();
            AutoCompleteExtender_TrfArrivalpickupcode_KeyUp();
            AutoCompleteExtender_TrfDepairportdrop_KeyUp();
            //            AutoCompleteExtender_TrfArrDropoff_KeyUp();

            //            AutoCompleteExtender_TrfInterpickup_KeyUp();
            //            AutoCompleteExtender_TrfInterDrop_KeyUp();


            $("#<%= txtTrfArrivaldate.ClientID %>").bind("change", function () {

                var d = document.getElementById('<%=chkArrival.ClientID%>');

                d.checked = true;

//                document.getElementById('< %=chkarrival.ClientID%>').checked = true;
//                $("#chkarrival").next("span").css("class", "jq-checkbox checked"); 
                


                //                document.getElementById('< %=chkarrival-styler.ClientID%>').removeAttribute("class");
                //                document.getElementById('< %=chkarrival-styler.ClientID%>').setAttribute("class", "jq-checkbox checked");
                //                



            });

            $("#<%= txtTrfDeparturedate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chkDeparture.ClientID%>');

                d.checked = true;

            });




            CallPriceSlider();

            var slider_range = $("#slider-range");
            slider_range.on("click", function () {
                document.getElementById("<%= btnFilter.ClientID %>").click();
            }
            );





            var child = $("#<%= ddlTrfchild.ClientID %>").val()
            if (child == 0) {
                $('#divTrfChild').hide();
            }
            ShowTrfChild();
            $("#<%= ddlTrfchild.ClientID %>").bind("change", function () {
                ShowTrfChild();

            });



            $("#btnTrfreset").button().click(function () {

                document.getElementById('<%=chkDeparture.ClientID%>').checked = false;
                document.getElementById('<%=chkarrival.ClientID%>').checked = false;

                document.getElementById('<%=txtTrfDeparturedate.ClientID%>').value = '';
                document.getElementById('<%=txtTrfArrivaldate.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDepairportdrop.ClientID%>').value = '';
                document.getElementById('<%=txtDepartureFlightCode.ClientID%>').value = '';
                document.getElementById('<%=txtDepartureFlight.ClientID%>').value = '';
                document.getElementById('<%=txtDepartureTime.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDeppickup.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDeppickupcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDeppickuptype.ClientID%>').value = ''; <%--Added Shahul 19/07/2018--%>

                document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfArrDroptype.ClientID%>').value = '';  <%--Added Shahul 19/07/2018--%>
                document.getElementById('<%=txtTrfArrDropoff.ClientID%>').value = '';
                document.getElementById('<%=txtArrivalflightCode.ClientID%>').value = '';
                document.getElementById('<%=txtArrivalflight.ClientID%>').value = '';
                document.getElementById('<%=txtArrivalTime.ClientID%>').value = '';
                document.getElementById('<%=txtTrfArrivalpickup.ClientID%>').value = '';
                document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>').value = '';



                var ddlTrfAdult = document.getElementById('<%=ddlTrfAdult.ClientID%>');
                ddlTrfAdult.selectedIndex = "0";
                $('.custom-select-ddlTrfAdult').next('span').children('.customSelectInner').text(jQuery("#ddlTrfAdult :selected").text());

                var ddlTrfChild = document.getElementById('<%=ddlTrfChild.ClientID%>');
                ddlTrfChild.selectedIndex = "0";
                $('.custom-select-ddlTrfChild').next('span').children('.customSelectInner').text(jQuery("#ddlTrfChild :selected").text());


                document.getElementById('<%=txtTrfChild1.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild2.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild3.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild4.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild5.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild6.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild7.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild8.ClientID%>').value = '';

                                
                $('#divTrfchild').hide();

            });


        });

        


        function ShowTrfChild() {
            var child = $("#<%= ddlTrfChild.ClientID %>").val()

            if (child == 0) {
                $('#divTrfchild').hide();
            }
            else if (child == 1) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').hide();
                $('#dvTrfChild3').hide();
                $('#dvTrfChild4').hide();
                $('#dvTrfChild5').hide();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();
            }
            else if (child == 2) {
                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').hide();
                $('#dvTrfChild4').hide();
                $('#dvTrfChild5').hide();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 3) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').hide();
                $('#dvTrfChild5').hide();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 4) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').hide();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 5) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').show();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 6) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').show();
                $('#dvTrfChild6').show();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 7) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').show();
                $('#dvTrfChild6').show();
                $('#dvTrfChild7').show();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 8) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').show();
                $('#dvTrfChild6').show();
                $('#dvTrfChild7').show();
                $('#dvTrfChild8').show();
                $('#divTrfchild').show();

            }

        }
      
    </script>

    <script type="text/javascript">
    //<![CDATA[
        function pageLoad() { // this gets fired when the UpdatePanel.Update() completes
            ReBindMyStuff();
            //google.maps.event.clearListeners(map);
            //google.maps.event.clearListeners(window, 'resize');
        }

        function ReBindMyStuff() { // create the rebinding logic in here
           // $("a").click(function (event) {
            $("#slider-range").on("click", function () {

                document.getElementById("<%= btnFilter.ClientID %>").click();
            });
        }
    //]]>
</script>


     <script type="text/javascript">
     
           //  alert('testt123');
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(EndRequestUserControl);

             function EndRequestUserControl(sender, args) {

                 AutoCompleteExtender_ArrivalFlight_KeyUp();
                 AutoCompleteExtender_DepartureFlight_KeyUp();
                 AutoCompleteExtender_TrfDeppickup_KeyUp();
                 AutoCompleteExtender_TrfDepairportdrop_KeyUp();
                 AutoCompleteExtender_TrfSourcecountry_KeyUp();
                 AutoCompleteExtender_TrfCustomer_KeyUp();
                 AutoCompleteExtender_TrfArrivalpickupcode_KeyUp();
                 AutoCompleteExtender_TrfArrDropoff_KeyUp();

                 AutoCompleteExtender_TrfInterpickup_KeyUp();
                 AutoCompleteExtender_TrfInterDrop_KeyUp();
               
                
              //   AutoCompleteExtender_HotelStarsKeyUp();
              //  AutoCompleteExtender_Customer_KeyUp();
             //    AutoCompleteExtender_Country_KeyUp();

             

                 $("#slider-range").on("click", function () {
                     //   alert('add_endRequest');
   
                     document.getElementById("<%= btnFilter.ClientID %>").click();

                 });

                 SetContextkey();

             //    ShowProgess();
                 
              
             }
      
    </script>



      <script type="text/javascript">


          Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {

              $("#slider-range").on("click", function () {
           
                  document.getElementById("<%= btnFilter.ClientID %>").click();
              });

          });

          function RefreshContent() {
              Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandlerForProgressBar);
              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandlerForProgressBar);
              
          }
          function BeginRequestHandlerForProgressBar() {
              ShowProgess();

          }
          function EndRequestHandlerForProgressBar() {
              HideProgess();

              $('.side-block input').styler({
                  selectSearch: true
              });

              CallPriceSlider();
         }

         function ShowProgess() {
             var ModalPopupDays = $find("ModalPopupDays");
             ModalPopupDays.show();
             return true;
         }
         function HideProgess() {
             var ModalPopupDays = $find("ModalPopupDays");
             if (ModalPopupDays != null) {
                 ModalPopupDays.hide();
             }
             return true;
         }

    </script>


</head>  
<body  onload="RefreshContent()">
    <form id="form1" runat="server">
  <!-- // authorize // -->
	<div class="overlay"></div>

<!-- \\ authorize \\-->
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true">
    </asp:ScriptManager>
    <header id="top">
	<div class="header-a">
		<div class="wrapper-padding">			
         <div class="header-user" style="margin-top:2px;"><asp:Label ID="lblHeaderUserName"  runat="server" ></asp:Label> </div>		
			<div class="header-phone" style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-account">
				<asp:UpdatePanel ID="UpdatePanel5" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
			</div>
              <div class="header-agentname" style="padding-left:105px;margin-top:2px;"><asp:Label ID="lblHeaderAgentName" runat="server" ></asp:Label> </div>
            <div class="header-lang" style="margin-top:2px;">
            <asp:UpdatePanel runat="server"><ContentTemplate><asp:Button ID="btnLogOut"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Log Out"></asp:Button>
</ContentTemplate></asp:UpdatePanel>
			<%--	<a href="#">Log Out</a>--%>
			</div>
			<div class="header-social" style="display:none;">
				<a href="#" class="social-twitter"></a>
				<a href="#" class="social-facebook"></a>
				<a href="#" class="social-vimeo"></a>
				<a href="#" class="social-pinterest"></a>
				<a href="#" class="social-instagram"></a>
			</div>
			<div class="header-viewed" style="display:none;">
				<a  href="#" class="header-viewed-btn">recently viewed</a>
				<!-- // viewed drop // -->
					<div class="viewed-drop">
						<div id="dvViewedItem" runat="server" class="viewed-drop-a">

						</div>
					</div>
				<!-- \\ viewed drop  \\ -->
			</div>
		
			<div id="dvFlag" runat="server" class="header-lang" style="padding-top:5px;" >
				<a href="#"><img id="imgLang" runat="server" alt="" src="img/en.gif" /></a>
			</div>
			<div id="dvCurrency" runat="server" class="header-curency" style="margin-top:2px;">
			</div>
               <div class="header-lang" id="dvMybooking" runat="server" style="padding-right:25px;margin-right:5px;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyBooking"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="MY BOOKING"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
           
			</div>
			<div class="clear"></div>
		</div>
	</div>
	<div class="header-b">
		<!-- // mobile menu // -->
			<div class="mobile-menu" id="dvMobmenu" runat="server" >
				
              			</div>
		<!-- \\ mobile menu \\ -->
			
		<div class="wrapper-padding">
            	<div class="clear"></div>
			<div class="header-logo"><a href="Home.aspx?Tab=3"><img id="imgLogo" runat="server" alt="" /></a></div>
			<div class="header-right">
				<div class="hdr-srch" style="display:none;">
					<a href="#" class="hdr-srch-btn"></a>
				</div>
				<div class="hdr-srch-overlay">
					<div class="hdr-srch-overlay-a">
						<input type="text" value="" placeholder="Start typing..."/>
						<a href="#" class="srch-close"></a>
						<div class="clear"></div>
					</div>
				</div>	
				<div class="hdr-srch-devider"></div>
				<a href="#" class="menu-btn"></a>
            <asp:Literal ID="ltMenu" runat="server"></asp:Literal>
			</div>
			<div class="clear"></div>
		</div>
	</div>	
</header>


<!-- main-cont -->
<div class="main-cont">
  <div class="body-wrapper">
    <div class="wrapper-padding">
    <div class="page-head">
      <div class="page-title">Transfers - <span>list style</span></div>
      <div class="breadcrumbs">
        <a href="Home.aspx?Tab=2">Home</a> / <a href="#">Transfers</a> 
      </div>
      <div class="clear"></div>
    </div>
     <div class="page-head">
      <div class="page-search-content-search">
                 <div class="search-tab-content">
                     <%-- <asp:UpdatePanel ID="upSearchMainTab" runat="server"><ContentTemplate>--%>
                                    <div class="page-search-p">
                                        <!-- /Arrival/ -->

                                         <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    ARRIVAL</label>
                                                <div class="search-large-i" id="divarrival" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chkarrival" CssClass="side-block jq-checkbox-tour" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                         <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        ARRIVAL DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtTrfArrivaldate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                         autocomplete="off"   runat="server"></asp:TextBox>
                                                        <span class="date-icon"></span>

                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlTrfArrFlightClass" runat="server" class="custom-select custom-select-ddlStarCategory">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>

                                            <div class="search-large-i" style="float: left;">
                                            <!-- /TRf Arrival flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtArrivalflight" placeholder="--" runat="server" onkeydown="ArrivalflightSetContextKey()" Onblur ="CheckFlight(this);" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtArrivalflight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetArrivalflight" TargetControlID="txtArrivalflight"
                                                            UseContextKey="true" OnClientItemSelected="ArrivalflightAutocompleteSelected">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtArrivalflightCode" Style="display: none" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /TRf Arrival time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        ARRIVAL TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtArrivalTime" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                         <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div6" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <!-- /TRf Arrival Pikcup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                ARRIVAL PICKUP<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTrfArrivalpickup" placeholder="--" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfArrivalpickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetArrivalpickup" TargetControlID="txtTrfArrivalpickup"
                                                    UseContextKey="true" OnClientItemSelected="TrfArrivalpickupAutocompleteSelected">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtTrfArrivalpickupcode" Style="display: none"  runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                         <!-- /TRf Arrival Drop off/ -->
                                        <div class="search-large-i" style="margin-left: 28px; margin-top: 20px;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    DROP OFF HOTELS/UAE LOCATIONS<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtTrfArrDropoff" placeholder="--" runat="server"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfArrDropoff" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetTrfArrDropoff" TargetControlID="txtTrfArrDropoff"
                                                        UseContextKey="true" OnClientItemSelected="TrfArrDropoffAutocompleteSelected">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:TextBox ID="txtTrfArrDropoffcode" Style="display: none" runat="server"></asp:TextBox>
                                                    <%--Added Shahul 19/07/2018--%>
                                                    <asp:TextBox ID="txtTrfArrDroptype" Style="display: none" runat="server" meta:resourcekey="txtTrfArrDroptypeResource1"></asp:TextBox>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>

                                         <div class="clear">
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \Arrival  End\ -->
                                        <div id="divdeparture" runat="server">
                                           <!-- \Departure  Start\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    DEPARTURE</label>
                                                <div class="search-large-i" id="div7" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chkDeparture" CssClass="side-block jq-checkbox-tour" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        DEPARTURE DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                          <asp:TextBox ID="txtTrfDeparturedate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                          autocomplete="off"  runat="server" onchange="ValidateDepDate();" ></asp:TextBox>
                                                        <span class="date-icon"></span>

                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlTrfDepFlightClass" runat="server" class="custom-select custom-select-ddlStarCategory">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                         <div class="search-large-i" style="float: left;">
                                            <!-- /TRf Departure flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtDepartureFlight" placeholder="--" runat="server" Onblur="CheckDepFlight(this);"  onkeydown="DepartureflightSetContextKey()">
                                                        </asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtDepartureFlight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetDepartureflight" TargetControlID="txtDepartureFlight"
                                                            UseContextKey="true" OnClientItemSelected="DepartureAutocompleteSelected">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtDepartureFlightCode" Style="display: none" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /TRf Departure time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        DEPARTURE TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtDepartureTime" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                         <div class="clear">
                                        </div>
                                        <!-- \\ -->
                                        <div class="clear">
                                        </div>
                                         <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div8" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                          <!-- /TRf Departure Pickup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                PICKUP HOTELS/UAE LOCATIONS<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTrfDeppickup" placeholder="--" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfDeppickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetTrfDeppickup" TargetControlID="txtTrfDeppickup"
                                                    UseContextKey="true" OnClientItemSelected="TrfDeppickupAutocompleteSelected">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtTrfDeppickupcode" Style="display: none" runat="server"></asp:TextBox>
                                                <%--Added Shahul 19/07/2018--%>
                                                <asp:TextBox ID="txtTrfDeppickuptype" Style="display: none" runat="server" meta:resourcekey="txtTrfDeppickuptypeResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                          <!-- /TRf Departure Drop off/ -->
                                          <div class="search-large-i" style="margin-left: 28px; margin-top: 20px;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    AIRPORT DROP OFF<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtTrfDepairportdrop" placeholder="--" runat="server"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfDepairportdrop" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetTrfDepairportdrop" TargetControlID="txtTrfDepairportdrop"
                                                        UseContextKey="true" OnClientItemSelected="TrfDepairportdropAutocompleteSelected">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:TextBox ID="txtTrfDepairportdropcode" Style="display: none" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>

                                          <div class="clear">
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        </div>
                                        <!-- \Departure  End\ -->
                                         <!-- /Inter Hotel start/ -->

                                        <div id="divinterhotel" runat="server" >
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    INTER HOTEL</label>
                                                <div class="search-large-i" id="div12" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chkinter" CssClass="side-block jq-checkbox-tour" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        TRANSFER DATE</label>
                                                    <div class="input-a">
                                                       
                                                           <asp:TextBox ID="txtTrfinterdate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                           autocomplete="off" runat="server"></asp:TextBox>
                                                            <span class="date-icon"></span>
                                                    </div>
                                                </div>
                                               
                                           
                                            </div>
                                         </div>
                                          <div class="clear">
                                          </div>
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div19" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                          <div class="search-large-i" style="float: left;margin-top: 20px;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    PICKUP HOTELS/UAE LOCATIONS</label>
                                                <div class="input-a">
                                                     <asp:TextBox ID="txtTrfinterPickup" placeholder="--" runat="server"></asp:TextBox>
                                                     <asp:TextBox ID="txtTrfinterPickupcode" Style="display: none" runat="server"></asp:TextBox>
                                                     <asp:TextBox ID="txtTrfinterPickuptype" Style="display: none" runat="server"></asp:TextBox>
                                                     <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfinterPickup" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetInterPickup" TargetControlID="txtTrfinterPickup"
                                                        OnClientItemSelected="InterPickupautocompleteselected">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>

                                          <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                DROP OFF HOTELS/UAE LOCATIONS</label>
                                            <div class="input-a">
                                                 <asp:TextBox ID="txtTrfInterdropff" placeholder="--" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtTrfInterdropffcode" Style="display: none" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtTrfInterdropfftype" Style="display: none" runat="server"></asp:TextBox>
                                                 <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfInterdropff" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetInterDropoff" TargetControlID="txtTrfInterdropff"
                                                        OnClientItemSelected="InterDropoffautocompleteselected">
                                                    </asp:AutoCompleteExtender>
                                               
                                            </div>
                                        </div>

                                        </div> 

                                         <!-- \Inter Hotel  End\ -->

                                         <!-- \Trf Cunstomer\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div9" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                           <div class="search-large-i" style="float: left; margin-top: 20px;" id="dvTrfCustomer" runat ="server">
                                            <!-- // -->

                                             <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtTrfCustomer" runat="server" placeholder="--" ></asp:TextBox>
                                                        <asp:TextBox ID="txtTrfCustomercode" runat="server" Style="display: none"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfCustomer" runat="server" CompletionInterval="10"
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetTrfCustomer" TargetControlID="txtTrfCustomer"
                                                            OnClientItemSelected="TrfCustomerAutocompleteSelected">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>

                                        </div>
                                          <!-- \Trf Source Country\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <div>
                                                <label>
                                                    Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtTrfSourcecountry" runat="server" placeholder="--"></asp:TextBox>
                                                    <asp:TextBox ID="txtTrfSourcecountrycode" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfSourcecountry" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetTrfCountry" TargetControlID="txtTrfSourcecountry"
                                                        UseContextKey ="true"   OnClientItemSelected="TrfCountryautocompleteselected">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="clear"></div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div10" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <!-- \Trf Adult child\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom" style="width: 80%;">
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        adult<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlTrfAdult" class="custom-select custom-select-ddlTrfAdult"
                                                            runat="server">
                                          
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        child</label>
                                                      <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlTrfChild" class="custom-select custom-select-ddlTrfChild"
                                                            runat="server">
                                              
                                                        </asp:DropDownList>
                                                    </div>
                                                   
                                                </div>
                                                <div class="srch-tab-3c" style="float: left; margin-top: 20px;" >
                                                   <div  id="divTrfOverride" style="width:120%" runat="server">
                                                    
                                                    <asp:CheckBox ID="chkTrfoverride" runat="server" />
                                                    <asp:Label ID="Label2" runat="server" CssClass="page-search-content-override-price"
                                                        Text="Override Price"></asp:Label>
                                                    
                                                </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- // advanced // -->
                                        <div class="clear">
                                        </div>

                                         <div id="divTrfchild" runat="server" style="margin-top: 20px;display:none;">
                                       
                                          <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div11" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="search-large-i-child-tour" style="float: left;">
                                                <label style="text-align: left; padding-right: 2px;">
                                                    Ages of children at Transfer</label>
                                                <div class="srch-tab-child" id="dvTrfChild1" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                       <div class="srch-tab-child-pre" id="div37">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="srch-tab-child" id="dvTrfChild2" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                         <div class="srch-tab-child-pre" id="div13">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild2" placeholder="CH 2" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="srch-tab-child" id="dvTrfChild3" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div14">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild3" placeholder="CH 3" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvTrfChild4" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div15">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild4" placeholder="CH 4" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                               
                                               
                                                
                                            </div>
                                             <div class="search-large-i-child-tour">
                                                <label style="color: White;">
                                                    Ages of children at Transfers</label>
                                                     <div class="srch-tab-child" id="dvTrfChild5" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                       <div class="srch-tab-child-pre" id="div16">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild5" placeholder="CH 5" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="srch-tab-child" id="dvTrfChild6" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div17">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild6" placeholder="CH 6" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="srch-tab-child" id="dvTrfChild7" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div18">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild7" placeholder="CH 7" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvTrfChild8" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                          <div class="srch-tab-child-pre" id="div20">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild8" placeholder="CH 8" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                               
                                               
                                               
                                            </div>
                                          
                                         </div> 
                                          <!-- \\ -->
                                        <!-- // -->
                                      
                                        <!-- \\ -->
                                        <!-- // -->
                                   
                                        <!-- \\ -->
                                        <div class="clear">
                                        </div>
                                    
                                       
                                    </div>
                                    <footer class="search-footer">
                                        <div class="search-large-i">
                                            <%--	<a href="#" class="srch-btn">Search</a>	--%>
                     <div class="srch-tab-left">
                          <asp:Button ID="btnSearch" class="authorize-btn" runat="server"   OnClientClick="return ValidateSearch()"  Text="Search"></asp:Button> 
                          </div>
                           <div class="srch-tab-left">
                             
                        <input  id="btnTrfreset"  type="button"  class="srch-btn-home"  value="Reset"/>
                        </div>
                        </div>
						
						<div class="clear"></div>
					</footer>
                     <%--  </ContentTemplate></asp:UpdatePanel>--%>
                                </div>
      </div>
     </div>

    <div class="two-colls">
      <div class="two-colls-left">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>  
        <div class="srch-results-lbl fly-in">
            <asp:Label ID="lblHotelCount" runat="server" Text=""></asp:Label>
        </div>
        
              
        <!-- // side // -->
        <div class="side-block fly-in">
          <div  class="side-price" id="divslideprice" runat ="server" >
            <div class="side-padding">
              <div class="side-lbl">Price</div>
              <div class="price-ranger">
                <div id="slider-range"></div>              
              </div>
              <div class="price-ammounts">
                <input type="text" id="ammount-from" readonly>
                <input type="text" id="ammount-to" readonly>
                <div class="clear"></div>
              </div>  
                     
            </div>
          </div>  
        </div>
        <!-- \\ side \\ -->
         
        <!-- // side // -->
        <div class="side-block fly-in">
          <div class="side-stars">
            <div class="side-padding">
                
              <div class="side-lbl">Vehicle Type</div>  
                           
                                    <asp:CheckBoxList ID="chkHotelStars" CssClass="checkbox"  OnChange="CallFilter();"  runat="server" 
                    CellPadding="5" CellSpacing="1" AutoPostBack="True">
                                    </asp:CheckBoxList>  
              <div class="clear"></div>            
            </div>
          </div>  
          
        </div>
        <!-- \\ side \\ -->
        
        <!-- // side // -->
        <div class="side-block fly-in" style="display:none">
          <div class="side-stars">
            <div class="side-padding">
              <div class="side-lbl">Sectors</div>  
           
           <asp:CheckBoxList ID="chkSectors" CssClass="checkbox"  runat="server"   OnChange="CallFilter();"  
                    CellPadding="5" CellSpacing="1" AutoPostBack="True">
                                    </asp:CheckBoxList>  
            </div>
          </div>  
          
        </div>
        <!-- \\ side \\ -->

        <!-- // side // -->
        <div class="side-block fly-in" style="display:none">
          <div class="side-stars">
            <div class="side-padding">
              <div class="side-lbl">Property Type</div>  
                <asp:CheckBoxList ID="chkPropertyType" CssClass="checkbox"   runat="server"  OnChange="CallFilter();"  
                    CellPadding="5" CellSpacing="1" AutoPostBack="True">
                                    </asp:CheckBoxList>
            </div>
          </div>  
        </div>
        <!-- \\ side \\ -->
                 <div><asp:TextBox ID="txtSearchFocus" Style="width:2px;height:2px;border:none;" MaxLength="1" runat="server"></asp:TextBox> </div>

            <!-- // side // -->
        <div class="side-block fly-in" style="display:none">
          <div class="side-stars">
            <div class="side-padding">
              <div class="side-lbl">Room Classification</div>  
                <asp:CheckBoxList ID="chkRoomClassification" CssClass="checkbox"  runat="server" OnChange="CallFilterForRoomClassification();"  
                    CellPadding="5" CellSpacing="1" AutoPostBack="True">
                                    </asp:CheckBoxList>
            </div>
          </div>  
        </div>
                    <asp:Button ID="btnFilter" runat="server" style="display:none;" Text="Filter" />
                      <asp:Button ID="btnFilterForRoom" runat="server" style="display:none;" Text="Room Filter" />
                   
        <!-- \\ side \\ -->
      </ContentTemplate></asp:UpdatePanel>
      </div>
      <div class="two-colls-right">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
        <div class="two-colls-right-b">
          <div class="padding">
          
            <div class="catalog-head large fly-in">
              <label>Sorting results by:</label>
              <div class="search-select">
                        <asp:DropDownList ID="ddlSorting" AutoPostBack="true"  runat="server">
                                                            <asp:ListItem Value="0">--</asp:ListItem>
                                                            <asp:ListItem>Price</asp:ListItem>
                                                            <asp:ListItem>Name</asp:ListItem>
                                                           
                                                     </asp:DropDownList></div>
    				
    					
              <div style="display:none;" class="search-select">
    							<select>
    								<option>Price</option>
    								<option>Price</option>
    								<option>Price</option>
    								<option>Price</option>
    								<option>Price</option>
    							</select>
    					</div>
              <div style="display:none;"  class="search-select">
    							<select>
    								<option>Rating</option>
    								<option>Rating</option>
    								<option>Rating</option>
    								<option>Rating</option>
    								<option>Rating</option>
    							</select>
    					</div>
                         <div style="display:none;"  class="search-select">
    							<select>
    								<option>Preferred</option>
    								<option>Preferred</option>
    								<option>Preferred</option>
    								<option>Preferred</option>
    								<option>Preferred</option>
    							</select>
    					</div>
                        <div style="float:right">
                        <asp:Button ID="btnbooknow" CssClass="guest-flight-details-generate" runat="server"  OnClick="btnBookNow_Click"   Text="BOOK SELECTED VEHICLES"></asp:Button> 
                        </div>
                        
              <a href="#" class="show-list" style="display:none;" ></a>              
              <a href="#" class="show-table" style="display:none;" ></a>
              <a href="#" class="show-thumbs chosen" style="display:none;" ></a> 
             
              <div class="clear"></div>
               
            </div>
              <div id="dvhotnoshow" runat="server" style="display: none; background-color: #F2F3F4;
                  padding-top: 16px; padding-bottom: 16px; padding-left: 16px; text-align: center">
                  <asp:Label ID="lblheader" runat="server" Text="Oops, No results to show. Can you please try a different combination?"
                      ForeColor="#009999" Font-Bold="True">
                  </asp:Label></div>  
            <div class="catalog-row list-rows">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                <asp:Button ID="btnPageBottom" runat="server" Text="Button" style="margin-left:-999px;"  />
              <!-- /Arrival/ -->
                    <asp:DataList ID="dlArrTransferSearchResults" runat="server" Width="100%">
                        <HeaderTemplate >
                          <div style="background-color:#F2F3F4;padding-top:6px;padding-bottom:6px"><asp:Label ID="lblheader" runat="server" Text="ARRIVAL" ForeColor="#009999" Font-Bold="True"  ></asp:Label></div>
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="cat-list-item fly-in"  style="padding-bottom:10px;">
                                <div class="cat-list-trfitem-l" style="margin-top: 10px">
                                    <a href="#">
                                        <asp:Image ID="imgvehicleImage" runat="server" Width="120px" /></a>
                                    <asp:Label ID="lblHotelImage" Visible="false" runat="server" Text='<%# Eval("imagename") %>'></asp:Label>
                                </div>
                                <div class="cat-list-trfitem-r">
                                    <div style="width: 50%; float: left; border-right: 1px solid #ede7e1">
                                        <div>
                                            <div class="offer-slider-link" style="margin-top: 10px">
                                                <asp:HiddenField ID="lblcartypecode" Value='<%# Eval("cartypecode") %>' runat="server" />
                                                <asp:Label ID="lblvehiclename" CssClass="Trf-slider-link-label" Text='<%# Eval("vehiclename") %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                            <div class="clear">
                                            </div>
                                            <div class="trf-slider-location" id="divminmaxpax" runat="server">
                                                <p style="text-align: justify;">
                                                    <asp:Label ID="lblremarks" runat="server" ToolTip='<%# Eval("remarks") %>' Text='<%# Eval("remarks") %>'></asp:Label>
                                                </p>
                                                <asp:Label ID="lblmin" runat="server" Text="MINIMUM" Style="display: none"> </asp:Label>&nbsp;
                                                <asp:Label ID="lblminpax" runat="server" Style="display: none" Text='<%# Eval("minpax").ToString() + " PAX " %>'></asp:Label>
                                                <asp:Label ID="lblmax" runat="server" Text="MAXIMUM"> </asp:Label>&nbsp;
                                                <asp:Label ID="lblmaxpax" runat="server" Text='<%# Eval("maxpax").ToString()  + " PAX " %>'></asp:Label>
                                            </div>
                                            <div class="trf-slider-location" id="divunitprice" runat="server" style="padding-top: 10px;
                                                padding-left: 0px; width: 150px">
                                                <div id="divlbl" runat="server">
                                                    <asp:Label ID="lblunitprice" runat="server" CssClass="offer-slider-r-label"></asp:Label>
                                                    <asp:Label ID="lblwlunitprice" runat="server" CssClass="offer-slider-r-label"></asp:Label>
                                                </div>
                                                <div id="divpaxprice" runat="server">
                                                    <asp:TextBox ID="txtunitprice" Style="width: 25px; padding: 6px 7px 7px 7px; box-shadow: 0 1px 1px 0 rgba(50, 50, 50, 0.05);
                                                        border: 1px solid #e3e3e3;" runat="server" onkeypress="validateDecimalOnly(event,this)"></asp:TextBox>
                                                         <asp:TextBox ID="txtwlunitprice" Style="width: 25px; padding: 6px 7px 7px 7px; box-shadow: 0 1px 1px 0 rgba(50, 50, 50, 0.05);
                                                        border: 1px solid #e3e3e3;" runat="server" onkeypress="validateDecimalOnly(event,this)"></asp:TextBox>
                                                    <asp:Label ID="lblvaluetext" runat="server"></asp:Label>
                                                  
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hddlTransferSearchResultsItemIndex" runat="server" />
                                            <br class="clear" />
                                        </div>
                                    </div>
                                    <div style="width: 45%; float: left; padding-top: 25px">
                                        <div class="search-large-i">
                                            <div class="trf-slider-location" style="margin-left: 15px">
                                                <%--<label>NO.OF.UNITS</label>--%>
                                                <asp:Label ID="lblunitname" runat="server" Text="NO.OF.UNITS"> </asp:Label>
                                                <div class="input-a" style="margin-top: 5px;" id="dvunit" runat="server">
                                                    <asp:TextBox ID="txtnoofunits" runat="server" onkeypress="validateDecimalOnly(event,this)">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i">
                                            <div class="trflbl-slider-location" style="margin-left: 15px;">
                                                <asp:Label ID="lbltotal" runat="server" Text="TOTAL"> </asp:Label>
                                                <div class="input-a" style="margin-top: 5px;" id="divtot" runat="server">
                                                    <asp:TextBox ID="txttotal" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                     <asp:TextBox ID="txtwltotal" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i">
                                            <div class="trflbl-slider-location" style="margin-left: 20px">
                                                <asp:Label ID="lblbook" runat="server" Text="SELECT"> </asp:Label>
                                                <div style="margin-top: 5px; margin-left: 20px; padding-top: 5px">
                                                    <asp:CheckBox ID="chkbooknow" CssClass="side-block jq-checkbox-tour" runat="server" />
                                                    <%--  <asp:Label ID="lblbooknow" runat="server" Style="display: none" Text='<%# Bind("Selected") %>'></asp:Label>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clear" style="padding-top:10px; margin-left:15px;font-weight:500;">
                                                                            <asp:Label ID="lblIncTax" CssClass="trf-slider-location" runat="server" Text="Incl of Taxes & vat"></asp:Label>
                                                                        </div>
                                        <div  runat="server" id="divcomplement" style="float: left;
                                            margin-left: 10px; margin-top: 5px">
                                            <div class="trf-slider-location" style="margin-left: 5px">
                                                <div style="margin-top: 5px; padding-top: 5px">
                                                    <asp:CheckBox ID="chkarrcompliment" CssClass="side-block jq-checkbox-tour" Text="COMPLEMENT TO CUSTOMER"
                                                        runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                          
                                        <div style="display: none" runat="server" id="divfields">
                                            <asp:Label ID="lblmaxadult" runat="server" Text='<%# Eval("maxadults") %>'></asp:Label>
                                            <asp:Label ID="lblmaxchild" runat="server" Text='<%# Eval("maxchild") %>'></asp:Label>
                                            <asp:Label ID="lblshuttle" runat="server" Text='<%# Eval("shuttle") %>'></asp:Label>
                                            <asp:Label ID="lblpaxcheck" runat="server" Text='<%# Eval("paxcheckreqd") %>'></asp:Label>
                                            <asp:Label ID="lblairportborder" runat="server" Text='<%# Eval("airportbordercode") %>'></asp:Label>
                                            <asp:Label ID="lblfromsector" runat="server" Text='<%# Eval("fromsectorgroupcode") %>'></asp:Label>
                                            <asp:Label ID="lblsectorgroupcode" runat="server" Text='<%# Eval("sectorgroupcode") %>'></asp:Label>
                                            <asp:Label ID="lbltransferdate" runat="server" Text='<%# Eval("transferdate") %>'></asp:Label>
                                            <asp:Label ID="lbladults" runat="server" Text='<%# Eval("adults") %>'></asp:Label>
                                            <asp:Label ID="lblchild" runat="server" Text='<%# Eval("child") %>'></asp:Label>
                                            <asp:Label ID="lblchildagestring" runat="server" Text='<%# Eval("childagestring") %>'></asp:Label>
                                            <asp:Label ID="lbltrfunit" runat="server" Text='<%# Eval("units") %>'></asp:Label>
                                            <asp:Label ID="lblprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                                            <asp:Label ID="lblvalue" runat="server" Text='<%# Eval("unitsalevalue") %>'></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("unitsalevalue") %>'></asp:Label>
                                            <asp:Label ID="lblcurrcode" runat="server" Text='<%# Eval("currcode") %>'></asp:Label>
                                            <asp:Label ID="lblplistcode" runat="server" Text='<%# Eval("tplistcode") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnselected" Value='<%# Eval("currentselection") %>' runat="server" />
                                             <asp:HiddenField ID="hdncompliment" Value='<%# Eval("complimentarycust") %>' runat="server" />
                                                     <asp:Label ID="lblpreferedsupplier" runat="server" Text='<%# Eval("preferredsupplier") %>'></asp:Label>
                                <asp:Label ID="lblunitcprice" runat="server" Text='<%# Eval("unitcprice") %>'></asp:Label>
                                 <asp:Label ID="lblunitcostvalue" runat="server" Text='<%# Eval("unitcostvalue") %>'></asp:Label>
                                  <asp:Label ID="lbltcplistcode" runat="server" Text='<%# Eval("tcplistcode") %>'></asp:Label>
                                   <asp:Label ID="lblwlconvrate" runat="server" Text='<%# Eval("wlconvrate") %>'></asp:Label>
                                    <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>'></asp:Label>
                                    <asp:Label ID="lblwlCurrCode" runat="server" Text='<%# Eval("wlcurrcode") %>'></asp:Label>
                                      <asp:Label ID="lblwlunitprice_grid" runat="server" Text='<%# Eval("wlunitprice") %>'></asp:Label>
                                        <asp:Label ID="lblwlunitsalevalue" runat="server" Text='<%# Eval("wlunitsalevalue") %>'></asp:Label>

                                        <asp:Label ID="lblCostTaxableValue" runat="server" Text='<%# Eval("CostTaxableValue") %>'></asp:Label>
                                        
                                        <asp:Label ID="lblCostVATValue" runat="server" Text='<%# Eval("CostVATValue") %>'></asp:Label>
                                        <asp:Label ID="lblVATPer" runat="server" Text='<%# Eval("VATPer") %>'></asp:Label>
                                        <asp:Label ID="lblPriceWithTAX" runat="server" Text='<%# Eval("PriceWithTAX") %>'></asp:Label>
                                        <asp:Label ID="lblPriceTaxableValue" runat="server" Text='<%# Eval("PriceTaxableValue") %>'></asp:Label>
                                        
                                        <asp:Label ID="lblPriceVATValue" runat="server" Text='<%# Eval("PriceVATValue") %>'></asp:Label>
                                        <asp:Label ID="lblPriceVATPer" runat="server" Text='<%# Eval("PriceVATPer") %>'></asp:Label>
                                        <asp:Label ID="lblPriceWithTAX1" runat="server" Text='<%# Eval("PriceWithTAX1") %>'></asp:Label>
                                        </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div style="float: right; margin-top: -25px; background-color: White; padding: 5px">
                                <asp:LinkButton ID="lbArrShowMore" CssClass="rate-plan-headings" OnClick="lbArrShowMore_Click"
                                    runat="server">Show More</asp:LinkButton></div>
                        </FooterTemplate>
                    </asp:DataList>
                   
              <!-- \Arrival End\ -->
              <!-- / Departure / -->
            
                <asp:DataList ID="dlDepTransferSearchResults" runat="server" Width="100%">
                    <HeaderTemplate>
                    <div style="background-color:#F2F3F4;padding-top:6px;padding-bottom:6px">
                     <asp:Label ID="lblDepheader" runat="server" Text="DEPARTURE" ForeColor="#009999"
                            Font-Bold="True"></asp:Label>
                    </div> 
                       
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="cat-list-item fly-in"  style="padding-bottom:10px;">
                            <div class="cat-list-trfitem-l" style="margin-top: 10px">
                                <a href="#">
                                    <asp:Image ID="imgvehicleImage" runat="server" Width="120px" /></a>
                                <asp:Label ID="lblHotelImage" Visible="false" runat="server" Text='<%# Eval("imagename") %>'></asp:Label>
                            </div>
                            <div class="cat-list-trfitem-r">
                                <div style="width: 50%; float: left; border-right: 1px solid #ede7e1">
                                    <div>
                                        <div class="offer-slider-link" style="margin-top: 10px">
                                            <asp:HiddenField ID="lblcartypecode" Value='<%# Eval("cartypecode") %>' runat="server" />
                                            <asp:Label ID="lblvehiclename" CssClass="Trf-slider-link-label" Text='<%# Eval("vehiclename") %>'
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="trf-slider-location" id="divminmaxpax" runat="server">
                                            <p style="text-align: justify;">
                                                <asp:Label ID="lblremarks" runat="server" ToolTip='<%# Eval("remarks") %>' Text='<%# Eval("remarks") %>'></asp:Label>
                                            </p>
                                            <asp:Label ID="lblmin" runat="server" Text="MINIMUM" Style="display: none"> </asp:Label>&nbsp;
                                            <asp:Label ID="lblminpax" runat="server" Style="display: none" Text='<%# Eval("minpax").ToString() + " PAX " %>'></asp:Label>
                                            <asp:Label ID="lblmax" runat="server" Text="MAXIMUM"> </asp:Label>&nbsp;
                                            <asp:Label ID="lblmaxpax" runat="server" Text='<%# Eval("maxpax").ToString()  + " PAX " %>'></asp:Label>
                                        </div>
                                        <div class="trf-slider-location" id="divunitprice" runat="server" style="padding-top: 10px;
                                            padding-left: 0px; width: 150px">
                                            <div id="divlbl" runat="server">
                                                <asp:Label ID="lblunitprice" runat="server" CssClass="offer-slider-r-label"></asp:Label>
                                                <asp:Label ID="lblwlunitprice" runat="server" CssClass="offer-slider-r-label"></asp:Label>
                                            </div>
                                            <div id="divpaxprice" runat="server">
                                                <asp:TextBox ID="txtunitprice" Style="width: 25px; padding: 6px 7px 7px 7px; box-shadow: 0 1px 1px 0 rgba(50, 50, 50, 0.05);
                                                    border: 1px solid #e3e3e3;" runat="server" onkeypress="validateDecimalOnly(event,this)"></asp:TextBox>
                                                     <asp:TextBox ID="txtwlunitprice" Style="width: 25px; padding: 6px 7px 7px 7px; box-shadow: 0 1px 1px 0 rgba(50, 50, 50, 0.05);
                                                    border: 1px solid #e3e3e3;" runat="server" onkeypress="validateDecimalOnly(event,this)"></asp:TextBox>
                                                <asp:Label ID="lblvaluetext" runat="server"></asp:Label>
                                                 <asp:Label ID="lblwlvaluetext" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hddlTransferSearchResultsItemIndex" runat="server" />
                                        <br class="clear" />
                                    </div>
                                </div>
                                <div style="width: 45%; float: left">
                                    <div class="search-large-i">
                                        <div class="trf-slider-location" style="margin-left: 15px;">
                                            <%--<label>NO.OF.UNITS</label>--%>
                                            <asp:Label ID="lblunitname" runat="server" Text="NO.OF.UNITS"> </asp:Label>
                                            <div class="input-a" style="margin-top: 5px;" id="dvunit" runat="server">
                                                <asp:TextBox ID="txtnoofunits" runat="server" onkeypress="validateDecimalOnly(event,this)">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="search-large-i">
                                        <div class="trflbl-slider-location" style="margin-left: 15px;">
                                            <asp:Label ID="lbltotal" runat="server" Text="TOTAL"> </asp:Label>
                                            <div class="input-a" style="margin-top: 5px;" id="divtot" runat="server">
                                                <asp:TextBox ID="txttotal" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                 <asp:TextBox ID="txtwltotal" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="search-large-i">
                                        <div class="trflbl-slider-location" style="margin-left: 20px">
                                            <asp:Label ID="lblbook" runat="server" Text="SELECT"> </asp:Label>
                                            <div style="margin-top: 5px; margin-left: 20px; padding-top: 5px">
                                                <asp:CheckBox ID="chkbooknow" CssClass="side-block jq-checkbox-tour" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                         <div class="clear" style="padding-top:10px; margin-left:15px;font-weight:500;">
                                                                            <asp:Label ID="lblIncTax" CssClass="trf-slider-location" runat="server" Text="Incl of Taxes & vat"></asp:Label>
                                                                        </div>
                                </div>
                                 <div class="search-large-i" runat="server" id="divdepcomplement" style="float:left;margin-left: 10px;margin-top: 5px">
                                         <div class="trf-slider-location" style="margin-left: 5px">
                                            <div style="margin-top: 5px;  padding-top: 5px">
                                                <asp:CheckBox ID="chkdepcompliment" CssClass="side-block jq-checkbox-tour" Text="COMPLEMENT TO CUSTOMER" runat="server" />
                                            </div>
                                        </div>
                                  </div>
                            </div>
                        </div>
                        <%--  </div>--%>
                        <div style="display: none" runat="server" id="divfields">
                            <asp:Label ID="lblmaxadult" runat="server" Text='<%# Eval("maxadults") %>'></asp:Label>
                            <asp:Label ID="lblmaxchild" runat="server" Text='<%# Eval("maxchild") %>'></asp:Label>
                            <asp:Label ID="lblshuttle" runat="server" Text='<%# Eval("shuttle") %>'></asp:Label>
                            <asp:Label ID="lblpaxcheck" runat="server" Text='<%# Eval("paxcheckreqd") %>'></asp:Label>
                            <asp:Label ID="lblairportborder" runat="server" Text='<%# Eval("airportbordercode") %>'></asp:Label>
                            <asp:Label ID="lblfromsector" runat="server" Text='<%# Eval("fromsectorgroupcode") %>'></asp:Label>
                            <asp:Label ID="lblsectorgroupcode" runat="server" Text='<%# Eval("sectorgroupcode") %>'></asp:Label>
                            <asp:Label ID="lbltransferdate" runat="server" Text='<%# Eval("transferdate") %>'></asp:Label>
                            <asp:Label ID="lbladults" runat="server" Text='<%# Eval("adults") %>'></asp:Label>
                            <asp:Label ID="lblchild" runat="server" Text='<%# Eval("child") %>'></asp:Label>
                            <asp:Label ID="lblchildagestring" runat="server" Text='<%# Eval("childagestring") %>'></asp:Label>
                            <asp:Label ID="lbltrfunit" runat="server" Text='<%# Eval("units") %>'></asp:Label>
                            <asp:Label ID="lblprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                            <asp:Label ID="lblvalue" runat="server" Text='<%# Eval("unitsalevalue") %>'></asp:Label>
                            <asp:Label ID="lblcurrcode" runat="server" Text='<%# Eval("currcode") %>'></asp:Label>
                            <asp:Label ID="lblplistcode" runat="server" Text='<%# Eval("tplistcode") %>'></asp:Label>
                            <asp:HiddenField ID="hdnselected" Value='<%# Eval("currentselection") %>' runat="server" />
                            <asp:HiddenField ID="hdncompliment" Value='<%# Eval("complimentarycust") %>' runat="server" />
                              <asp:Label ID="lblpreferedsupplier" runat="server" Text='<%# Eval("preferredsupplier") %>'></asp:Label>
                                <asp:Label ID="lblunitcprice" runat="server" Text='<%# Eval("unitcprice") %>'></asp:Label>
                                 <asp:Label ID="lblunitcostvalue" runat="server" Text='<%# Eval("unitcostvalue") %>'></asp:Label>
                                  <asp:Label ID="lbltcplistcode" runat="server" Text='<%# Eval("tcplistcode") %>'></asp:Label>
                                   <asp:Label ID="lblwlconvrate" runat="server" Text='<%# Eval("wlconvrate") %>'></asp:Label>
                                    <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>'></asp:Label>
                                      <asp:Label ID="lblwlunitprice_grid" runat="server" Text='<%# Eval("wlunitprice") %>'></asp:Label>
                                        <asp:Label ID="lblwlunitsalevalue" runat="server" Text='<%# Eval("wlunitsalevalue") %>'></asp:Label>
                                          <asp:Label ID="lblwlCurrCode" runat="server" Text='<%# Eval("wlcurrcode") %>'></asp:Label>

                                          <asp:Label ID="lblCostTaxableValue" runat="server" Text='<%# Eval("CostTaxableValue") %>'></asp:Label>                                        
                                        <asp:Label ID="lblCostVATValue" runat="server" Text='<%# Eval("CostVATValue") %>'></asp:Label>
                                        <asp:Label ID="lblVATPer" runat="server" Text='<%# Eval("VATPer") %>'></asp:Label>
                                        <asp:Label ID="lblPriceWithTAX" runat="server" Text='<%# Eval("PriceWithTAX") %>'></asp:Label>
                                        <asp:Label ID="lblPriceTaxableValue" runat="server" Text='<%# Eval("PriceTaxableValue") %>'></asp:Label>
                                        
                                        <asp:Label ID="lblPriceVATValue" runat="server" Text='<%# Eval("PriceVATValue") %>'></asp:Label>
                                        <asp:Label ID="lblPriceVATPer" runat="server" Text='<%# Eval("PriceVATPer") %>'></asp:Label>
                                        <asp:Label ID="lblPriceWithTAX1" runat="server" Text='<%# Eval("PriceWithTAX1") %>'></asp:Label>
                        </div>
                        <%-- </div>--%>
                    </ItemTemplate>
                     <FooterTemplate>
                     <div style="float: right;margin-top:-25px;background-color:White;padding:5px">
                            <asp:LinkButton ID="lbDepShowMore" CssClass="rate-plan-headings" OnClick="lbDepShowMore_Click"
                                runat="server">Show More</asp:LinkButton></div>
                    </FooterTemplate>
                </asp:DataList>
            
              <!-- / Departure / -->
               <!-- / Shifting / -->
              
                <asp:DataList ID="dlShifting" runat="server" Width="100%">
                    <HeaderTemplate>
                     <div style="background-color:#F2F3F4;padding-top:6px;padding-bottom:6px">
                        <asp:Label ID="lblheader" runat="server" Text="INTER HOTEL" ForeColor="#009999" Font-Bold="True"></asp:Label>
                       </div>  
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="cat-list-item fly-in" style="padding-bottom:10px;">
                            <div class="cat-list-trfitem-l" style="margin-top: 10px">
                                <a href="#">
                                    <asp:Image ID="imgvehicleImage" runat="server" Width="120px" /></a>
                                <asp:Label ID="lblHotelImage" Visible="false" runat="server" Text='<%# Eval("imagename") %>'></asp:Label>
                            </div>
                            <div class="cat-list-trfitem-r">
                                <div style="width: 50%; float: left; border-right: 1px solid #ede7e1">
                                    <div>
                                        <div class="offer-slider-link" style="margin-top: 10px">
                                            <asp:HiddenField ID="lblcartypecode" Value='<%# Eval("cartypecode") %>' runat="server" />
                                            <asp:Label ID="lblvehiclename" CssClass="Trf-slider-link-label" Text='<%# Eval("vehiclename") %>'
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="trf-slider-location" id="divminmaxpax" runat="server">
                                            <p style="text-align: justify;">
                                                <asp:Label ID="lblremarks" runat="server" ToolTip='<%# Eval("remarks") %>' Text='<%# Eval("remarks") %>'></asp:Label>
                                            </p>
                                            <asp:Label ID="lblmin" runat="server" Text="MINIMUM" Style="display: none"> </asp:Label>&nbsp;
                                            <asp:Label ID="lblminpax" runat="server" Style="display: none" Text='<%# Eval("minpax").ToString() + " PAX " %>'></asp:Label>
                                            <asp:Label ID="lblmax" runat="server" Text="MAXIMUM"> </asp:Label>&nbsp;
                                            <asp:Label ID="lblmaxpax" runat="server" Text='<%# Eval("maxpax").ToString()  + " PAX " %>'></asp:Label>
                                        </div>
                                        <div class="trf-slider-location" id="divunitprice" runat="server" style="padding-top: 10px;
                                            padding-left: 0px; width: 150px">
                                            <div id="divlbl" runat="server">
                                                <asp:Label ID="lblunitprice" runat="server" CssClass="offer-slider-r-label"></asp:Label>
                                                <asp:Label ID="lblwlunitprice" runat="server" CssClass="offer-slider-r-label"></asp:Label>
                                            </div>
                                            <div id="divpaxprice" runat="server">
                                                <asp:TextBox ID="txtunitprice" Style="width: 25px; padding: 6px 7px 7px 7px; box-shadow: 0 1px 1px 0 rgba(50, 50, 50, 0.05);
                                                    border: 1px solid #e3e3e3;" runat="server" onkeypress="validateDecimalOnly(event,this)"></asp:TextBox>
                                                    <asp:TextBox ID="txtwlunitprice" Style="width: 25px; padding: 6px 7px 7px 7px; box-shadow: 0 1px 1px 0 rgba(50, 50, 50, 0.05);
                                                    border: 1px solid #e3e3e3;" runat="server" onkeypress="validateDecimalOnly(event,this)"></asp:TextBox>
                                                <asp:Label ID="lblvaluetext" runat="server" ></asp:Label>
                                                 <asp:Label ID="lblwlvaluetext" runat="server" ></asp:Label>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hddlTransferSearchResultsItemIndex" runat="server" />
                                        <br class="clear" />
                                    </div>
                                </div>
                                <div style="width: 45%; float: left; padding-top: 25px">
                                    <div class="search-large-i">
                                        <div class="trf-slider-location" style="margin-left: 15px">
                                            <%--<label>NO.OF.UNITS</label>--%>
                                            <asp:Label ID="lblunitname" runat="server" Text="NO.OF.UNITS"> </asp:Label>
                                            <div class="input-a" style="margin-top: 5px;" id="dvunit" runat="server">
                                                <asp:TextBox ID="txtnoofunits" runat="server" onkeypress="validateDecimalOnly(event,this)">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="search-large-i">
                                        <div class="trflbl-slider-location" style="margin-left: 15px;">
                                            <asp:Label ID="lbltotal" runat="server" Text="TOTAL"> </asp:Label>
                                            <div class="input-a" style="margin-top: 5px;" id="divtot" runat="server">
                                                <asp:TextBox ID="txttotal" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                 <asp:TextBox ID="txtwltotal" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="search-large-i">
                                        <div class="trflbl-slider-location" style="margin-left: 20px">
                                            <asp:Label ID="lblbook" runat="server" Text="SELECT"> </asp:Label>
                                            <div style="margin-top: 5px; margin-left: 20px; padding-top: 5px">
                                                <asp:CheckBox ID="chkbooknow" CssClass="side-block jq-checkbox-tour" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                         <div class="clear" style="padding-top:10px; margin-left:15px;font-weight:500;">
                                                                            <asp:Label ID="lblIncTax" CssClass="trf-slider-location" runat="server" Text="Incl of Taxes & vat"></asp:Label>
                                                                        </div>
                                </div>
                                 <div class="search-large-i" runat="server" id="divintercomplement" style="float:left;margin-left: 10px;margin-top: 5px">
                                         <div class="trf-slider-location" style="margin-left: 5px">
                                            <div style="margin-top: 5px;  padding-top: 5px">
                                                <asp:CheckBox ID="chkintercompliment" CssClass="side-block jq-checkbox-tour" Text="COMPLEMENT TO CUSTOMER" runat="server" />
                                            </div>
                                        </div>
                                 </div>

                            </div>
                        </div>
                        <div style="display: none" runat="server" id="divfields">
                            <asp:Label ID="lblmaxadult" runat="server" Text='<%# Eval("maxadults") %>'></asp:Label>
                            <asp:Label ID="lblmaxchild" runat="server" Text='<%# Eval("maxchild") %>'></asp:Label>
                            <asp:Label ID="lblshuttle" runat="server" Text='<%# Eval("shuttle") %>'></asp:Label>
                            <asp:Label ID="lblpaxcheck" runat="server" Text='<%# Eval("paxcheckreqd") %>'></asp:Label>
                            <asp:Label ID="lblairportborder" runat="server" Text='<%# Eval("airportbordercode") %>'></asp:Label>
                            <asp:Label ID="lblfromsector" runat="server" Text='<%# Eval("fromsectorgroupcode") %>'></asp:Label>
                            <asp:Label ID="lblsectorgroupcode" runat="server" Text='<%# Eval("sectorgroupcode") %>'></asp:Label>
                            <asp:Label ID="lbltransferdate" runat="server" Text='<%# Eval("transferdate") %>'></asp:Label>
                            <asp:Label ID="lbladults" runat="server" Text='<%# Eval("adults") %>'></asp:Label>
                            <asp:Label ID="lblchild" runat="server" Text='<%# Eval("child") %>'></asp:Label>
                            <asp:Label ID="lblchildagestring" runat="server" Text='<%# Eval("childagestring") %>'></asp:Label>
                            <asp:Label ID="lbltrfunit" runat="server" Text='<%# Eval("units") %>'></asp:Label>
                            <asp:Label ID="lblprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                            <asp:Label ID="lblvalue" runat="server" Text='<%# Eval("unitsalevalue") %>'></asp:Label>
                            <asp:Label ID="lblcurrcode" runat="server" Text='<%# Eval("currcode") %>'></asp:Label>
                             <asp:Label ID="lblplistcode" runat="server" Text='<%# Eval("tplistcode") %>'></asp:Label>
                             <asp:HiddenField ID="hdnselected" Value='<%# Eval("currentselection") %>' runat="server" />
                              <asp:HiddenField ID="hdncompliment" Value='<%# Eval("complimentarycust") %>' runat="server" />
                               <asp:Label ID="lblpreferedsupplier" runat="server" Text='<%# Eval("preferredsupplier") %>'></asp:Label>
                                <asp:Label ID="lblunitcprice" runat="server" Text='<%# Eval("unitcprice") %>'></asp:Label>
                                 <asp:Label ID="lblunitcostvalue" runat="server" Text='<%# Eval("unitcostvalue") %>'></asp:Label>
                                  <asp:Label ID="lbltcplistcode" runat="server" Text='<%# Eval("tcplistcode") %>'></asp:Label>
                                   <asp:Label ID="lblwlconvrate" runat="server" Text='<%# Eval("wlconvrate") %>'></asp:Label>
                                    <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>'></asp:Label>
                                      <asp:Label ID="lblwlunitprice_grid" runat="server" Text='<%# Eval("wlunitprice") %>'></asp:Label>
                                        <asp:Label ID="lblwlunitsalevalue" runat="server" Text='<%# Eval("wlunitsalevalue") %>'></asp:Label>
                                   <asp:Label ID="lblwlCurrCode" runat="server" Text='<%# Eval("wlcurrcode") %>'></asp:Label>

                                   <asp:Label ID="lblCostTaxableValue" runat="server" Text='<%# Eval("CostTaxableValue") %>'></asp:Label>                                        
                                        <asp:Label ID="lblCostVATValue" runat="server" Text='<%# Eval("CostVATValue") %>'></asp:Label>
                                        <asp:Label ID="lblVATPer" runat="server" Text='<%# Eval("VATPer") %>'></asp:Label>
                                        <asp:Label ID="lblPriceWithTAX" runat="server" Text='<%# Eval("PriceWithTAX") %>'></asp:Label>
                                        <asp:Label ID="lblPriceTaxableValue" runat="server" Text='<%# Eval("PriceTaxableValue") %>'></asp:Label>
                                        
                                        <asp:Label ID="lblPriceVATValue" runat="server" Text='<%# Eval("PriceVATValue") %>'></asp:Label>
                                        <asp:Label ID="lblPriceVATPer" runat="server" Text='<%# Eval("PriceVATPer") %>'></asp:Label>
                                        <asp:Label ID="lblPriceWithTAX1" runat="server" Text='<%# Eval("PriceWithTAX1") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                     <div style="float: right;margin-top:-25px;background-color:White;padding:5px">
                            <asp:LinkButton ID="lbinterShowMore" CssClass="rate-plan-headings" OnClick="lbinterShowMore_Click" 
                                runat="server">Show More</asp:LinkButton></div>
                    </FooterTemplate>
                </asp:DataList>
               
                   </ContentTemplate>
              </asp:UpdatePanel>
               <!-- / Shifting / -->
                 
            
            </div>
            
              <asp:Panel runat="server" ID="pnlLocationMap" style="display:none;" ><div class="LocationMap-popup">
            <div id="Div4">
            	<div class="roomtype-popup-title"><asp:Label ID="lblLocMaphotelName" runat="server"></asp:Label> <a id="aMapClose" href="#" class="roomtype-popup-close"></a></div>
           <div class="roomtype-popup-description"> <div id="map" style="width:630px;height:485px;background:#FBFBFB;margin-top:-10px;"></div></div>
            </div>
            </div> </asp:Panel>

            <asp:Panel runat="server" ID="pnlRoomInfo" style="display:none;" ><div class="roomtype-popup">
            <div id="roomtype-popup-bg">
            	<div class="roomtype-popup-title">Standard Room <a id="aClose" href="#" class="roomtype-popup-close"></a></div>
           <div class="roomtype-popup-description">Voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui. Nemo enim ipsam voluptatem quia voluptas.</div>
            </div>
            </div> </asp:Panel>
               <asp:Panel runat="server" ID="pnlCancelationPolicy" style="display:none;" ><div class="roomtype-popup">
            <div id="Div1">
            	<div class="roomtype-popup-title">Cancelation and Amendment Policies<a id="aPolicyClose" href="#" class="roomtype-popup-close"></a></div>
           <div class="roomtype-popup-description" ><div style="overflow:auto;height:250px; Min-height: 129px; max-height: 389px;" ><asp:Label ID="lblCancelationPolicy" runat="server"></asp:Label></div></div>
            </div>
            </div> </asp:Panel>
             <asp:Panel runat="server" ID="pnlMinLengthStay" style="display:none;" ><div class="roomtype-popup">
            <div id="Div2">
            	<div class="roomtype-popup-title">Minimum Length of Stay<a id="aMinLengthStayClose" href="#" class="roomtype-popup-close"></a></div>
           <div class="roomtype-popup-description"><asp:Label ID="lblminlengthStay" runat="server"></asp:Label></div>
            </div>
            </div> </asp:Panel>
  <asp:Panel runat="server" ID="pnltariff" style="display:none;" ><div class="roomtype-popup-tariff">
            <div id="Div3">
            	<div class="roomtype-popup-title">Tariff<a id="aTariffClose" href="#" class="roomtype-popup-close"></a></div>
           <div class="roomtype-popup-description-tariff" style="text-align:justify;min-height:150px;max-height:250px;overflow:auto; "><p><asp:Label ID="lblTariffDate" runat="server"></asp:Label> </p><p><asp:Label ID="lblTariffContent" runat="server"></asp:Label></p></span></div>
            </div>
            </div> </asp:Panel>
  <asp:Panel runat="server" ID="pnlOffers" style="display:none;" ><div class="roomtype-popup">
            <div>
            	<div id="dvroomtype-popup-title" class="roomtype-popup-title">Special Offres and Promotions<a id="aOfferClose" href="#" class="roomtype-popup-close"></a></div>
           <div class="roomtype-popup-description" style="text-align:justify;min-height:150px;max-height:250px;overflow:auto; "><p><asp:Label ID="lblOfferDate" runat="server"></asp:Label> </p>
           <p><asp:Label ID="lblOfferContent" runat="server"></asp:Label> </p>
           </div>
            </div>
            </div> </asp:Panel>
                <asp:ModalPopupExtender ID="mpTotalprice" runat="server" 
                                                              BackgroundCssClass="roomtype-modalBackground" CancelControlID="atotalPriceClose" 
                                                              EnableViewState="true" PopupControlID="pnlTotalPrice" 
                                                              TargetControlID="hdpricePopup">
                                                          </asp:ModalPopupExtender>
                                                          <asp:HiddenField ID="hdpricePopup" runat="server" />
             <asp:Panel runat="server" ID="pnlTotalPrice" style="display:none;" ><div class="roomtype-price-breakuppopup">
            <div id="Div5">
            	<div class="roomtype-popup-title"><asp:Label ID="lblTotlaPriceHeading" runat="server"></asp:Label> <a id="atotalPriceClose" href="#" class="roomtype-popup-close"></a></div>
               <div   class="roomtype-popup-description">
                   <div ID="dvPriceBreakupSave" runat="server"  style="padding-left: 10px; margin-bottom:5px;">
                       <asp:Label ID="Label3" CssClass="room-type-breakup-headings" Text="Price Per Night"
                           runat="server"></asp:Label>
                       <asp:TextBox ID="txtsalepriceForAll" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
                           runat="server"></asp:TextBox>
                       <asp:Label ID="Label5" CssClass="room-type-breakup-headings" Text="Cost Price Per Night"
                           runat="server"></asp:Label>
                       <asp:TextBox ID="txtBreakupTotalPriceForAll" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
                           runat="server"></asp:TextBox>
                    
                            <asp:CheckBox ID="chkFillBlank"  CssClass="roomtype-popup-label"  runat="server" Text="Fill Blank Fields Only" />
                              <div style="padding-top:10px;padding-left:400px;">
                              <asp:Button ID="btnPriceBreakupFillPrice" CssClass="roomtype-popup-buttons-save"
                           Style="margin-left: 15px;" runat="server" Text="Fill Price" />
                       <asp:Button ID="btnPriceBreakupSave" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px;"
                           runat="server" Text="Save" />
                          </div>  </div>
   <div style="padding-top:10px;padding-left:0px;margin-bottom: 15px;">
     <asp:CheckBox ID="chkComplimentaryToCustomer"  CssClass="roomtype-popup-label"  runat="server" Text="Complementary to Customer" />
     <asp:CheckBox ID="chkComplimentaryFromSupplier"  CssClass="roomtype-popup-label"  runat="server" Text="Complementary from Supplier" />
     </div><div style="padding-top:10px;padding-left:0px;">
     <asp:CheckBox ID="chkComplimentaryArrivalTransfer"  CssClass="roomtype-popup-label"  runat="server" Text="Complementary Arrival Transfer" />
     <asp:CheckBox ID="chkComplimentaryDepartureTransfer"  CssClass="roomtype-popup-label"  runat="server" Text="Complementary Departure Transfer" />
   </div>

                         
                         
                   <div style="overflow: auto; min-height: 329px; max-height: 420px; min-width: 300px;
                       padding-bottom: 10px; min-width: 700px;">
                       <asp:DataList ID="dltotalPriceBreak" RepeatColumns="3" RepeatDirection="Horizontal"
                           runat="server">
                           <ItemTemplate>
                               <div style="border: 0px solid #ede7e1; max-width: 350px; min-width: 150px; padding-left: 5px;
                                   padding-top: 5px; padding-bottom: 10px;">
                                   <div style="float: left;">
                                       <div id="dvPriceDate" runat="server" style="border: 1px solid #fff; width: 150px;
                                           height: 35px; background-color: #ede7e1; padding-right: 5px; margin-right: 20px;
                                           text-align: center; vertical-align: middle; padding-top: 8px;">
                                           <asp:Label ID="Label2" CssClass="room-type-breakup-headings" Text="Date" runat="server"></asp:Label>
                                       </div>
                                       <div id="dvPricePerNight" runat="server" style="border: 1px solid #fff; width: 150px;
                                           height: 30px; background-color: #ede7e1; padding-right: 5px; margin-right: 20px;
                                           text-align: center; vertical-align: middle; padding-top: 10px;">
                                           <asp:Label ID="Label3" CssClass="room-type-breakup-headings" Text="Price Per Night"
                                               runat="server"></asp:Label>
                                       </div>
                                       <div id="dvCostPricePerNightText" runat="server" style="border: 1px solid #fff; width: 150px;
                                           height: 30px; background-color: #ede7e1; padding-right: 5px; margin-right: 20px;
                                           text-align: center; vertical-align: middle; padding-top: 10px;">
                                           <asp:Label ID="Label4" CssClass="room-type-breakup-headings" Text="Cost Price Per Night"
                                               runat="server"></asp:Label>
                                       </div>
                                   </div>
                                   <div style="float: left;">
                                       <div style="border: 1px solid #ede7e1; width: 150px; height: 35px; background-color: #fff;
                                           margin-left: 5px; text-align: center; vertical-align: middle; padding-top: 8px;">
                                           <div>
                                               <asp:Label ID="lblBreakupDate" Text='<%# Eval("pricedate") %>' CssClass="roomtype-popup-label"
                                                   runat="server"></asp:Label>
                                                    <asp:Label ID="lblBreakupDate1" Text='<%# Eval("pricedate") %>' Visible="false"
                                                   runat="server"></asp:Label>
                                           </div>
                                           <div>
                                               <asp:Label ID="lblBreakupDateName" CssClass="roomtype-popup-label" runat="server"></asp:Label></div>
                                       </div>
                                       <div style="border: 1px solid #ede7e1; width: 150px; height: 30px; margin-left: 5px;
                                           margin-top: -1px; text-align: center; vertical-align: middle; padding-top: 8px;">
                                           <asp:Label ID="lblbreakupPrice" CssClass="roomtype-popup-label" Text='<%# Eval("wlsaleprice") %>'
                                               runat="server"></asp:Label>
                                           <asp:TextBox ID="txtsaleprice" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
                                               Text='<%# Eval("wlsaleprice") %>' runat="server"></asp:TextBox>
                                       </div>
                                       <div id="dvCostPricePerNight" runat="server" style="border: 1px solid #ede7e1; width: 150px;
                                           height: 30px; margin-left: 5px; margin-top: -1px; text-align: center; vertical-align: middle;
                                           padding-top: 8px;">
                                           <asp:Label ID="lblBreakupTotalPrice" CssClass="roomtype-popup-label" Text='<%# Eval("totalprice") %>'
                                               runat="server"></asp:Label>
                                           <asp:TextBox ID="txtBreakupTotalPrice" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
                                               Text='<%# Eval("totalprice") %>' runat="server"></asp:TextBox>
                                       </div>
                                   </div>
                               </div>
                               <%--         
              <div style="border:1px solid #ede7e1;width:150px;height:30px;background-color:#ede7e1;"></div>
               <div style="border:1px solid #ede7e1;width:150px;"> </div>--%>
                           </ItemTemplate>
                       </asp:DataList>
                       <asp:HiddenField ID="hdRMPartyCode" runat="server" />
                         <asp:HiddenField ID="hdRMRatePlanId" runat="server" />
                           <asp:HiddenField ID="hdRMRoomTypeCode" runat="server" />
                             <asp:HiddenField ID="hdRMMealPlanCode" runat="server" />
                               <asp:HiddenField ID="hdRMcatCode" runat="server" />
                                 <asp:HiddenField ID="hdRMAccCode" runat="server" />
                                  <asp:HiddenField ID="hdRoomTypegrid" runat="server" />
                                        <asp:HiddenField ID="hdRoomTypegridRowId" runat="server" />
                                        <asp:HiddenField ID="hdRoomTypeCurrCode" runat="server" />
                                         <asp:Button ID="btnSelectedArrival" runat="server" Style="display: none;" Text="Filter" />
                                         <asp:Button ID="btnSelectedDeparture" runat="server" Style="display: none;" Text="Filter" />
                                         <asp:Button ID="btnSelectedInter" runat="server" Style="display: none;" Text="Filter" />
                                          <asp:HiddenField ID="hdOPMode" runat="server" />

                   </div>
               </div>
            </div>
            </div> </asp:Panel>
            <div class="clear"></div>
            
            <div  ><%--style="display:none;"--%>
         <%--     <a class="active" href="#">1</a>
              <a href="#">2</a>
              <a href="#">3</a>--%>
                       <asp:Repeater ID="rptPager" runat="server" Visible="false" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled="true"
                                        CssClass='<%# If(Convert.ToBoolean(Eval("Enabled")), "page_enabled", "page_disabled")%>'
                                        OnClick="Page_Changed" OnClientClick='<%# If(Not Convert.ToBoolean(Eval("Enabled")), "return false;", "") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
              <div class="clear"></div>
            </div>            
          </div>
        </div>
        </ContentTemplate>
          </asp:UpdatePanel>
        <br class="clear" />
      </div>
    </div>
    <div class="clear"></div>
    
    </div>	
  </div>  
</div>
<!-- /main-cont -->
    <footer class="footer-a">
	<div class="wrapper-padding">
	<div id="dvFooterAddress" runat="server" class="section">
			<div class="footer-lbl">CONTACT</div>
		    <div class="footer-adress"> <asp:Label ID="lblFAdd1" runat="server"></asp:Label>, <br /><asp:Label ID="lblFAdd2" runat="server"></asp:Label>,<br /><asp:Label ID="lblFAdd3" runat="server"></asp:Label>, <asp:Label ID="lblFAdd4" runat="server"></asp:Label></div>
			<div class="footer-phones"><asp:Label ID="lblFPhone" runat="server"></asp:Label></div>
			<div class="footer-email"> <asp:Label ID="lblFEmail" runat="server"></asp:Label></div>
			<div class="footer-timimg"><asp:Label ID="lblFWorkingTime" runat="server"></asp:Label></div>

		</div>
		<div  id="dvMagnifyingMemories" runat="server"   class="section-middle">
			<div class="footer-lbl">MAGNIFYING MEMORIES</div>
			
            <div class="footer-magnifying-memories"> 
			We believe, travel whether for business or leisure, has to be memorable and joyous. We help travelers and planners across the world discover, build, and deliver bespoke experiences in the UAE. As a preferred partner for 500+ hotels and attractions in the region, we offer the best deals across hotel bookings, excursions, transfers, guides, and UAE visas. Start exploring right away.
			</div>
		</div>
		<div class="section">
			<div class="footer-lbl">GLOBAL PRESENCE</div>
			<div>
				<div><img src="img/rg-footer.png"/></div>
			</div>
		</div>

	</div>
	<div class="clear"></div>
</footer>

<footer class="footer-b">
	<div class="wrapper-padding">
	<div class="footer-left">© Copyright 2017 by MAHCE. All rights reserved.</div>
		
		<div class="clear"></div>
	</div>
</footer>


<!-- // scripts // -->


   <script type="text/javascript">
       $(function () {

           var lastUpdate = 0;
           var checkInterval = setInterval(function () {
               if (new Date().getTime() - lastUpdate > 840000) {
                   clearInterval(checkInterval);

               } else {
                   $.get('/ImStillAlive.action');

               }
           }, 840000); // 14 mins * 60 * 1000 =840000

           $(document).keydown(function () {
               lastUpdate = new Date().getTime();
           });
       });
       setInterval(function () {
           $.get('/ImStillAlive.action');
       }, 840000);  // 14 mins * 60 * 1000
       function CallPriceSlider() {
          // alert('test');

           'use strict';
           (function ($) {
               $(function () {
                   $('.side-block input').styler({
                       selectSearch: true
                   });
               });
           })(jQuery);

           var slider_range = $("#slider-range");
           var ammount_from = $("#ammount-from");
           var ammount_to = $("#ammount-to");
           var hdPriceMin = $("#hdPriceMin");
           var hdPriceMax = $("#hdPriceMax");
           var hdPriceMinRange = $("#hdPriceMinRange");
           var hdPriceMaxRange = $("#hdPriceMaxRange");
           var vmin = hdPriceMinRange.val()
           var max = hdPriceMaxRange.val()
          
           var vminSelected = hdPriceMin.val()
           var maxSelected = hdPriceMax.val()
           var curcode = $("#hdSliderCurrency").val();
           if (vminSelected == '') {
               vminSelected = vmin;
           }
           if (maxSelected == '') {
               maxSelected = max;
           }
       
          // alert(vmin);
           $(function () {
               slider_range.slider({
                   range: true,
                   min: parseInt(vmin),
                   max: parseInt(max),
                   values: [vminSelected, maxSelected],
                   slide: function (event, ui) {
                       ammount_from.val(ui.values[0] + curcode);
                       ammount_to.val(ui.values[1] + curcode);
                       hdPriceMin.val(ui.values[0])
                       hdPriceMax.val(ui.values[1])

                   }

               });

               ammount_from.val(slider_range.slider("values", 0) + curcode);
               ammount_to.val(slider_range.slider("values", 1) + curcode);
              // alert(hdPriceMax.val());
           });
          // alert(hdPriceMax.val());
       }
     </script>
     <script>
         function CallFilter() {
           
             document.getElementById("<%= btnFilter.ClientID %>").click();
         }
       
     </script>
<!-- \\ scripts \\ --> 

    <center>
                            <div id="Loading1" runat="server" style="height: 150px; width: 500px;">
                                <img alt="" id="Image1" runat="server" src="~/img/page-loader.gif" width="200" />
                                <h2 style="display:none;" class="page-loader-label" >
                                    Processing please wait...</h2>
                            </div>
                        </center>
                        <asp:ModalPopupExtender ID="ModalPopupDays" runat="server" BehaviorID="ModalPopupDays" 
                            TargetControlID="btnInvisibleGuest" CancelControlID="btnClose" PopupControlID="Loading1"
                            BackgroundCssClass="ModalPopupForPageLoading">
                           
                        </asp:ModalPopupExtender>
                   <%--    <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="btnInvisibleGuest">
        <Animations>
                <OnClick>
                    <FadeOut Duration="10" Fps="4" />                    
                </OnClick>
        </Animations>
    </asp:AnimationExtender>--%>
                         <input id="btnInvisibleGuest" runat="server" type="button" value="Cancel" style="display: none" />
                        <input id="btnClose" type="button" value="Cancel" style="display: none" />

  <asp:HiddenField ID="hdPriceMin" runat="server" />
    <asp:HiddenField ID="hdPriceMax" runat="server" />
      <asp:HiddenField ID="hdPriceMinRange" runat="server" />
    <asp:HiddenField ID="hdPriceMaxRange" runat="server" />
  <asp:HiddenField ID="hdLoginType" runat="server" />
   <asp:HiddenField ID="hdBookingEngineRateType" runat="server" />
    <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
<asp:HiddenField ID="hdWhiteLabel" runat="server" />
  <asp:HiddenField ID="hdSliderCurrency" runat="server" />
    <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
    </form>
</body>
</html>
