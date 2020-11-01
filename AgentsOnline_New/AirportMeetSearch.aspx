<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AirportMeetSearch.aspx.vb" Inherits="AirportMeetSearch" %>
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
<%--    <link href='http://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css' />
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





    </script>

   <script language="javascript" type="text/javascript">

       function UpdatePrice() {
      
//           var lbNew = document.getElementById(lb);
//           lbNew.value = '100';
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

               var txtArrdate = document.getElementById("<%=txtMAArrivaldate.ClientID%>");
               var txtDepdate = document.getElementById("<%=txtMADeparturedate.ClientID%>");


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

       function ValidateSearch() {

      

          var chkarrival = document.getElementById('<%=chkarrival.ClientID%>').checked;
          var chkDeparture = document.getElementById('<%=chkDeparture.ClientID%>').checked;
          var chkTransit = document.getElementById('<%=chkTransit.ClientID%>').checked;


         

           if (document.getElementById('<%=txtMAArrivaldate.ClientID%>').value == '' && chkarrival == true) {
               showDialog('Alert Message', 'Please select  Arrival date.', 'warning');
               HideProgess();
               return false;
           }

           if (document.getElementById('<%=txtArrivalflightCode.ClientID%>').value == '' && document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value == '' && chkarrival == true) {
               showDialog('Alert Message', 'Please Select   Arrival Flight/Airport.', 'warning');
               HideProgess();
               return false;
           }


//           if (document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value == '' && chkarrival == true) {
//               showDialog('Alert Message', 'Please select   Arrival airport.', 'warning');
//               HideProgess();
//               return false;
//           }


           if (document.getElementById('<%=txtMADeparturedate.ClientID%>').value == '' && chkDeparture == true) {
               showDialog('Alert Message', 'Please select  Departure date.', 'warning');
               HideProgess();
               return false;
           }

           if (document.getElementById('<%=txtDepartureFlightCode.ClientID%>').value == '' && document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value=='' && chkDeparture == true) {
               showDialog('Alert Message', 'Please select   Departure Flight/Airport.', 'warning');
               HideProgess();
               return false;
           }


           if (document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value == '' && chkDeparture == true) {
               showDialog('Alert Message', 'Please select   Departure airport.', 'warning');
               HideProgess();
               return false;
           }


           if (document.getElementById('<%=txtTransitarrdate.ClientID%>').value == '' && chkTransit == true) {
               showDialog('Alert Message', 'Please select  transit arrival date.', 'warning');
               HideProgess();
               return false;
           }

           if (document.getElementById('<%=txtMATranArrFlightCode.ClientID%>').value == '' && chkTransit == true) {
               showDialog('Alert Message', 'Please select transit arrival Flight.', 'warning');
               HideProgess();
               return false;
           }

           if (document.getElementById('<%=txtMATransitArrivalpickupcode.ClientID%>').value == '' && chkTransit == true) {
               showDialog('Alert Message', 'Please select transit arrival airport.', 'warning');
               HideProgess();
               return false;
           }


           if (document.getElementById('<%=txtMATrandepdate.ClientID%>').value == '' && chkTransit == true) {
               showDialog('Alert Message', 'Please select  transit departure date.', 'warning');
               HideProgess();
               return false;
           }

           if (document.getElementById('<%=txtMATranDepartureFlightCode.ClientID%>').value == '' && chkTransit == true) {
               showDialog('Alert Message', 'Please select transit departure Flight.', 'warning');
               HideProgess();
               return false;
           }
           if (document.getElementById('<%=txtMATransitDeparturepickupcode.ClientID%>').value == '' && chkTransit == true) {
               showDialog('Alert Message', 'Please select transit departure airport.', 'warning');
               HideProgess();
               return false;
           }

           if (document.getElementById('<%=ddlMAAdult.ClientID%>').value == '0') {
               showDialog('Alert Message', 'Please select any number of adult.', 'warning');
               HideProgess();
               return false;
           }


           if (document.getElementById('<%=txtMASourcecountrycode.ClientID%>').value == '') {
               showDialog('Alert Message', 'Please enter Transfer source country.', 'warning');
               HideProgess();
               return false;
           }


           var child = document.getElementById('<%=ddlMAChild.ClientID%>').value;
           if (child != '0') {
               var child1 = document.getElementById('<%=txtMAChild1.ClientID%>').value;
               var child2 = document.getElementById('<%=txtMAChild2.ClientID%>').value;
               var child3 = document.getElementById('<%=txtMAChild3.ClientID%>').value;
               var child4 = document.getElementById('<%=txtMAChild4.ClientID%>').value;
               var child5 = document.getElementById('<%=txtMAChild5.ClientID%>').value;
               var child6 = document.getElementById('<%=txtMAChild6.ClientID%>').value;
               var child7 = document.getElementById('<%=txtMAChild7.ClientID%>').value;
               var child8 = document.getElementById('<%=txtMAChild8.ClientID%>').value;
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




       function AutoCompleteExtender_MAArrivalpickupcode_KeyUp() {
           $("#<%= txtMAArrivalpickup.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMAArrivalpickup.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });

           $("#<%= txtMAArrivalpickup.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMAArrivalpickup.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });
       }


       function AutoCompleteExtender_MAArrDropoff_KeyUp() {
           $("#<%= txtMAArrDropoff.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMAArrDropoff.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMAArrDropoffcode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });

           $("#<%= txtMAArrDropoff.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMAArrDropoff.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMAArrDropoffcode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });
       }

       function AutoCompleteExtender_MACustomer_KeyUp() {
           $("#<%= txtMACustomer.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMACustomer.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMACustomercode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });

           $("#<%= txtMACustomer.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMACustomer.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMACustomercode.ClientID%>');
               if (hiddenfieldID1.value == '') {
                   hiddenfieldID.value = '';
               }
           });
       }


       function AutoCompleteExtender_MASourcecountry_KeyUp() {
           $("#<%= txtMASourcecountry.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMASourcecountrycode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMASourcecountry.ClientID%>');
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
               }
           });

           $("#<%= txtMASourcecountry.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMASourcecountrycode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMASourcecountry.ClientID%>');
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
               }
           });
       }


       function AutoCompleteExtender_MADepairportdrop_KeyUp() {
           $("#<%= txtMADepairportdrop.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMADepairportdropcode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMADepairportdrop.ClientID%>');
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
               }
           });

           $("#<%= txtMADepairportdrop.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMADepairportdropcode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMADepairportdrop.ClientID%>');
               if (hiddenfieldID.value == '') {
                  hiddenfieldID1.value = '';
               }
           });
       }

       function AutoCompleteExtender_MADeppickup_KeyUp() {
           $("#<%= txtMADeppickup.ClientID %>").bind("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMADeppickupcode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMADeppickup.ClientID%>');
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
               }
           });

           $("#<%= txtMADeppickup.ClientID %>").keyup("change", function () {
               var hiddenfieldID1 = document.getElementById('<%=txtMADeppickupcode.ClientID%>');
               var hiddenfieldID = document.getElementById('<%=txtMADeppickup.ClientID%>');
               if (hiddenfieldID.value == '') {
                   hiddenfieldID1.value = '';
               }
           });
       }
     

       function ArrivalflightSetContextKey() {
           $find('<%=AutoCompleteExtender_txtArrivalflight.ClientID%>').set_contextKey($get("<%=txtMAArrivaldate.ClientID %>").value);

       }

       function DepartureflightSetContextKey() {
           $find('<%=AutoCompleteExtender_txtDepartureFlight.ClientID%>').set_contextKey($get("<%=txtMADeparturedate.ClientID %>").value);

       }

       function MATranArrivalflightSetContextKey() {
           $find('<%=AutoCompleteExtender_txtMAtranArrFlight.ClientID%>').set_contextKey($get("<%=txtTransitarrdate.ClientID %>").value);

       }

       function MATranDepartureflightSetContextKey() {
           $find('<%=AutoCompleteExtender_txtMAtranDepartureFlight.ClientID%>').set_contextKey($get("<%=txtMATrandepdate.ClientID %>").value);

       }


       function MATranDeppickupAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtMATransitDeparturepickupcode.ClientID%>').value = eventArgs.get_value();
               }
               else {
                   document.getElementById('<%=txtMATransitDeparturepickupcode.ClientID%>').value = '';
               }
           }
       }

       function MATranArrivalpickupAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtMATransitArrivalpickupcode.ClientID%>').value = eventArgs.get_value();
               }
               else {
                   document.getElementById('<%=txtMATransitArrivalpickupcode.ClientID%>').value = '';
               }
           }

       }


       function MATranArrivalflightAutocompleteSelected(source, eventArgs) {
           if (source) {
               // Get the HiddenField ID.
               var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMAtranArrFlight", "txtMATranArrFlightCode");
               var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtMAtranArrFlight", "txtMATranArrTime");
               var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtMAtranArrFlight", "txtMAtranArrivalpickup");
               var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtMAtranArrFlight", "txtMATransitArrivalpickupcode");



               $get(hiddenfieldID).value = eventArgs.get_value();
               GetMATransitAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
           }

       }

       function GetMATransitAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
           $.ajax({
               type: "POST",
               url: "AirportmeetSearch.aspx/GetMATransitAirportAndTimeDetails",
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



       function ArrivalflightAutocompleteSelected(source, eventArgs) {
           if (source) {
               // Get the HiddenField ID.
               var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalflightCode");
               var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalTime");
               var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtMAArrivalpickup");
               var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtMAArrivalpickupcode");


               $get(hiddenfieldID).value = eventArgs.get_value();
               GetAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
           }

       }


       <%--Added Shahul 01/11/2018--%>
     function CheckMAFlight(Flightcode) {
         $.ajax({
          
            type: "POST",
            url: "Home.aspx/CheckMAFlight",
            data: '{Flightcode:  "' + $("#txtArrivalflight").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessMAFlight,
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

     function OnSuccessMAFlight(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Countries = xml.find("MAFlightdetails");
        var rowCount = Countries.length;

        if (rowCount == 0){
             showDialog('Alert Message', 'Arrival Flight Not Exists in the Flight Master Please Select  Proper  Flight.', 'warning');
             document.getElementById('<%=txtArrivalflight.ClientID%>').value = '';
            return false;
        }
    };

    <%--Added Shahul 01/11/2018--%>
     function CheckMADepFlight(Flightcode) {
         $.ajax({
          
            type: "POST",
            url: "Home.aspx/CheckMADepFlight",
            data: '{Flightcode:  "' + $("#txtDepartureFlight").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessMADepFlight,
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

     function OnSuccessMADepFlight(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Countries = xml.find("MADepFlightdetails");
        var rowCount = Countries.length;

        if (rowCount == 0){
             showDialog('Alert Message', 'Departure Flight Not Exists in the Flight Master Please Select  Proper  Flight.', 'warning');
         
            document.getElementById('<%=txtDepartureFlight.ClientID%>').value = '';
            return false;
        }
    };


       function GetAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
           $.ajax({
               type: "POST",
               url: "Airportmeetsearch.aspx/GetAirportAndTimeDetails",
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


       function MAArrivalpickupAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value = eventArgs.get_value();
               }
               else {
                   document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value = '';
               }
           }

       }

       function MAArrDropoffAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtMAArrDropoffcode.ClientID%>').value = eventArgs.get_value();
               }
               else {
                   document.getElementById('<%=txtMAArrDropoffcode.ClientID%>').value = '';
               }
           }
       }


       function MATranDepartureAutocompleteSelected(source, eventArgs) {
           if (source) {

               var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMAtranDepartureFlight", "txtMATranDepartureFlightCode");
               var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtMAtranDepartureFlight", "txtMATranDepartureTime");
               var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtMAtranDepartureFlight", "txtMAtranDeppickup");
               var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtMAtranDepartureFlight", "txtMATransitDeparturepickupcode");

               $get(hiddenfieldID).value = eventArgs.get_value();
               GetMATransitDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
           }

       }

       function GetMATransitDepartureAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
           $.ajax({
               type: "POST",
               url: "Airportmeetsearch.aspx/GetMATransitDepartureAirportAndTimeDetails",
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


       function calculatevalue(chk,  rowid) {



           chk1 = document.getElementById(chk);
         

           if (chk1.checked) {
               document.getElementById("<%= btnSelectedArrival.ClientID %>").click();
           }

       }

       function calculatevaluetransit(chk, rowid) {



           chk1 = document.getElementById(chk);


           if (chk1.checked) {
               document.getElementById("<%= btnSelectedTransit.ClientID %>").click();
           }

       }

       function calculatevaluedep(chk, rowid) {



           chk1 = document.getElementById(chk);


           if (chk1.checked) {
               document.getElementById("<%= btnSelectedDeparture.ClientID %>").click();
           }

       }



       function DepartureAutocompleteSelected(source, eventArgs) {
           if (source) {

               var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtDepartureFlightCode");
               var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtDepartureTime");
               var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtMADepairportdrop");
               var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtMADepairportdropcode");

               $get(hiddenfieldID).value = eventArgs.get_value();
               GetDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
           }

       }


       function MADeppickupAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtMADeppickupcode.ClientID%>').value = eventArgs.get_value();
               }
               else {
                   document.getElementById('<%=txtMADeppickupcode.ClientID%>').value = '';
               }
           }
       }

       function MACustomerAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtMACustomercode.ClientID%>').value = eventArgs.get_value();
                   $find('AutoCompleteExtender_txtMASourcecountry').set_contextKey(eventArgs.get_value());
                   GetMACountryDetails(eventArgs.get_value());

               }
               else {
                   document.getElementById('<%=txtMACustomercode.ClientID%>').value = '';
               }
           }
       }


       function GetMACountryDetails(CustCode) {
           $.ajax({
               type: "POST",
               url: "AirportMeetSearch.aspx/GetMACountryDetails",
               data: '{CustCode:  "' + CustCode + '"}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: OnSuccessMA,
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

       function OnSuccessMA(response) {
           var xmlDoc = $.parseXML(response.d);
           var xml = $(xmlDoc);
           var Countries = xml.find("MACountries");
           var rowCount = Countries.length;

           if (rowCount == 1) {
               $.each(Countries, function () {
                   document.getElementById('<%=txtMASourcecountry.ClientID%>').value = ''
                   document.getElementById('<%=txtMASourcecountrycode.ClientID%>').value = '';
                   document.getElementById('<%=txtMASourcecountry.ClientID%>').value = $(this).find("ctryname").text();
                   document.getElementById('<%=txtMASourcecountrycode.ClientID%>').value = $(this).find("ctrycode").text();
                   document.getElementById('<%=txtMASourcecountry.ClientID%>').setAttribute("readonly", true);
                   $find('AutoCompleteExtender_txtMASourcecountry').setAttribute("Enabled", false);
               });
           }
           else {
               document.getElementById('<%=txtMASourcecountry.ClientID%>').value = ''
               document.getElementById('<%=txtMASourcecountrycode.ClientID%>').value = '';
               document.getElementById('<%=txtMASourcecountry.ClientID%>').removeAttribute("readonly");
               $find('AutoCompleteExtender_txtMASourcecountry').setAttribute("Enabled", true);
           }
       };



       function MACountryautocompleteselected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtMASourcecountrycode	.ClientID%>').value = eventArgs.get_value();
                   $find('AutoCompleteExtender_txtMASourcecountry').set_contextKey(eventArgs.get_value());
               }
               else {
                   document.getElementById('<%=txtMASourcecountrycode	.ClientID%>').value = '';
               }
           }
       }

       function MADepairportdropAutocompleteSelected(source, eventArgs) {
           if (source) {
               if (eventArgs != null) {
                   document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value = eventArgs.get_value();
               }
               else {
                   document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value = '';
               }
           }
       }

       function CalculateSaleValue(txtNoOfAdult, txtAdultPrice, txtAdultSaleValue, txtNoOfchild, txtChildprice, txtchildSaleValue, txtTotalSaleVale) {

           var NoOfAdult = document.getElementById(txtNoOfAdult);
           var AdultPrice = document.getElementById(txtAdultPrice);
           var AdultSaleValue = document.getElementById(txtAdultSaleValue);
           var NoOfchild = document.getElementById(txtNoOfchild);
           var Childprice = document.getElementById(txtChildprice);
           var childSaleValue = document.getElementById(txtchildSaleValue);
           var TotalSaleVale = document.getElementById(txtTotalSaleVale);

           var iAdultSaleValue
           iAdultSaleValue = (parseFloat(NoOfAdult.value) * parseFloat(AdultPrice.value));
           AdultSaleValue.value = iAdultSaleValue;

           var ichildSaleValue = (parseFloat(NoOfchild.value) * parseFloat(Childprice.value));
           childSaleValue.value = ichildSaleValue;


           var totalamt = iAdultSaleValue + ichildSaleValue;
           TotalSaleVale.value = totalamt;


       }

       function GetDepartureAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
           $.ajax({
               type: "POST",
               url: "AirportMeetSearch.aspx/GetDepartureAirportAndTimeDetails",
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

            AutoCompleteExtender_ArrivalFlight_KeyUp();
            AutoCompleteExtender_DepartureFlight_KeyUp();
            AutoCompleteExtender_MADeppickup_KeyUp();
            AutoCompleteExtender_MADepairportdrop_KeyUp();
            AutoCompleteExtender_MASourcecountry_KeyUp();
            AutoCompleteExtender_MACustomer_KeyUp();
            AutoCompleteExtender_MAArrivalpickupcode_KeyUp();
            AutoCompleteExtender_MAArrDropoff_KeyUp();


            $("#<%= txtMAArrivaldate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chkarrival.ClientID%>');

                d.checked = true;

            });

            $("#<%= txtMADeparturedate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chkDeparture.ClientID%>');

                d.checked = true;

            });

            $("#<%= txtTransitarrdate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chktransit.ClientID%>');

                d.checked = true;

            });

            $("#<%= txtMATrandepdate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chktransit.ClientID%>');

                d.checked = true;

            });


            CallPriceSlider();

            var slider_range = $("#slider-range");
            slider_range.on("click", function () {
                document.getElementById("<%= btnFilter.ClientID %>").click();
            }
            );

            var child = $("#<%= ddlMAchild.ClientID %>").val()
            if (child == 0) {
                $('#divMAChild').hide();
            }
            ShowMAChild();
            $("#<%= ddlMAchild.ClientID %>").bind("change", function () {
                ShowMAChild();

            });



            $("#btnMAreset").button().click(function () {

                document.getElementById('<%=chkDeparture.ClientID%>').checked = false;
                document.getElementById('<%=chkarrival.ClientID%>').checked = false;

                document.getElementById('<%=txtMADeparturedate.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrivaldate.ClientID%>').value = '';
                document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value = '';
                document.getElementById('<%=txtMADepairportdrop.ClientID%>').value = '';
                document.getElementById('<%=txtDepartureFlightCode.ClientID%>').value = '';
                document.getElementById('<%=txtDepartureFlight.ClientID%>').value = '';
                document.getElementById('<%=txtDepartureTime.ClientID%>').value = '';
                document.getElementById('<%=txtMADeppickup.ClientID%>').value = '';
                document.getElementById('<%=txtMADeppickupcode.ClientID%>').value = '';

                document.getElementById('<%=txtMAArrDropoffcode.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrDropoff.ClientID%>').value = '';
                document.getElementById('<%=txtArrivalflightCode.ClientID%>').value = '';
                document.getElementById('<%=txtArrivalflight.ClientID%>').value = '';
                document.getElementById('<%=txtArrivalTime.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrivalpickup.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value = '';



                var ddlMAAdult = document.getElementById('<%=ddlMAAdult.ClientID%>');
                ddlMAAdult.selectedIndex = "0";
                $('.custom-select-ddlMAAdult').next('span').children('.customSelectInner').text(jQuery("#ddlMAAdult :selected").text());

                var ddlMAChild = document.getElementById('<%=ddlMAChild.ClientID%>');
                ddlMAChild.selectedIndex = "0";
                $('.custom-select-ddlMAChild').next('span').children('.customSelectInner').text(jQuery("#ddlMAChild :selected").text());


                document.getElementById('<%=txtMAChild1.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild2.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild3.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild4.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild5.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild6.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild7.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild8.ClientID%>').value = '';
                                
                $('#divMAchild').hide();

            });


        });


        function ShowMAChild() {
            var child = $("#<%= ddlMAChild.ClientID %>").val()

            if (child == 0) {
                $('#divMAchild').hide();
            }
            else if (child == 1) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').hide();
                $('#dvMAChild3').hide();
                $('#dvMAChild4').hide();
                $('#dvMAChild5').hide();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();
            }
            else if (child == 2) {
                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').hide();
                $('#dvMAChild4').hide();
                $('#dvMAChild5').hide();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 3) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').hide();
                $('#dvMAChild5').hide();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 4) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').hide();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 5) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').show();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 6) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').show();
                $('#dvMAChild6').show();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 7) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').show();
                $('#dvMAChild6').show();
                $('#dvMAChild7').show();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 8) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').show();
                $('#dvMAChild6').show();
                $('#dvMAChild7').show();
                $('#dvMAChild8').show();
                $('#divMAchild').show();

            }

        }
      
    </script>

    <script type="text/javascript">
    //<![CDATA[
        function pageLoad() { // this gets fired when the UpdatePanel.Update() completes
            ReBindMyStuff();
         
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
                 AutoCompleteExtender_MADeppickup_KeyUp();
                 AutoCompleteExtender_MADepairportdrop_KeyUp();
                 AutoCompleteExtender_MASourcecountry_KeyUp();
                 AutoCompleteExtender_MACustomer_KeyUp();
                 AutoCompleteExtender_MAArrivalpickupcode_KeyUp();
                 AutoCompleteExtender_MAArrDropoff_KeyUp();

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
                 ModalPopupDays.hide(500);
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
				<asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
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
			
			<div id="dvFlag" runat="server" class="header-lang" style="padding-top:6px;" >
				<a href="#"><img id="imgLang" runat="server" alt="" src="img/en.gif" /></a>
			</div>
			<div id="dvCurrency" runat="server" class="header-curency" style="margin-top:2px;">
			</div>

               <div class="header-lang" id="dvMybooking" runat="server" style="padding-right:25px;margin-right:5px;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:Button ID="btnMyBooking"   UseSubmitBehavior="false" 
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
			<div class="header-logo"><a href="Home.aspx"><img id="imgLogo" runat="server" alt="" /></a></div>
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
      <div class="page-title">Airport Meet - <span>list style</span></div>
      <div class="breadcrumbs">
        <a href="Home.aspx">Home</a> / <a href="#">Airport Meet</a> 
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
                                                        <asp:CheckBox ID="chkarrival" CssClass="side-block jq-checkbox-tour"  runat="server" />
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
                                                        <asp:TextBox ID="txtMAArrivaldate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                            runat="server"></asp:TextBox>
                                                        <span class="date-icon"></span>

                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlMAArrFlightClass" runat="server" class="custom-select custom-select-ddlStarCategory">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>

                                            <div class="search-large-i" style="float: left;">
                                            <!-- /MA Arrival flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtArrivalflight" placeholder="--" runat="server" Onblur ="CheckMAFlight(this);" onkeydown="ArrivalflightSetContextKey()"></asp:TextBox>
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
                                                <!-- /MA Arrival time/ -->
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
                                         <!-- /MA Arrival Pikcup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                ARRIVAL AIRPORT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtMAArrivalpickup" placeholder="--" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAArrivalpickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetArrivalpickup" TargetControlID="txtMAArrivalpickup"
                                                    UseContextKey="true" OnClientItemSelected="MAArrivalpickupAutocompleteSelected">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtMAArrivalpickupcode" Style="display: none" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                         <!-- /MA Arrival Drop off/ -->
                                        <div class="search-large-i" style="margin-left: 28px; margin-top: 20px;display:none;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    DROP OFF POINT</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtMAArrDropoff" placeholder="--" runat="server"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAArrDropoff" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetMAArrDropoff" TargetControlID="txtMAArrDropoff"
                                                        UseContextKey="true" OnClientItemSelected="MAArrDropoffAutocompleteSelected">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:TextBox ID="txtMAArrDropoffcode" Style="display: none" runat="server"></asp:TextBox>
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
                                           <!-- \Departure  Start\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    DEPARTURE</label>
                                                <div class="search-large-i" id="div7" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chkDeparture" CssClass="side-block jq-checkbox-tour"  runat="server" />
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
                                                          <asp:TextBox ID="txtMADeparturedate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                            runat="server" onchange="ValidateDepDate();" ></asp:TextBox>
                                                        <span class="date-icon"></span>

                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlMADepFlightClass" runat="server" class="custom-select custom-select-ddlStarCategory">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                         <div class="search-large-i" style="float: left;">
                                            <!-- /MA Departure flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtDepartureFlight" placeholder="--" runat="server" Onblur ="CheckMADepFlight(this);" onkeydown="DepartureflightSetContextKey()">
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
                                                <!-- /MA Departure time/ -->
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
                                          <!-- /MA Departure Pickup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                             <label>
                                                   DEPARTURE AIRPORT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                              <div class="input-a">
                                                    <asp:TextBox ID="txtMADepairportdrop" placeholder="--" runat="server"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMADepairportdrop" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetMADepairportdrop" TargetControlID="txtMADepairportdrop"
                                                        UseContextKey="true" OnClientItemSelected="MADepairportdropAutocompleteSelected">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:TextBox ID="txtMADepairportdropcode" Style="display: none" runat="server"></asp:TextBox>
                                                </div>
                                         
                                        </div>
                                          <!-- /MA Departure Drop off/ -->
                                          <div class="search-large-i" style="margin-left: 28px; margin-top: 20px;display:none;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                  <label>
                                                PICK-UP POINT</label>
                                                 <div class="input-a">
                                                <asp:TextBox ID="txtMADeppickup" placeholder="--" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMADeppickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetMADeppickup" TargetControlID="txtMADeppickup"
                                                    UseContextKey="true" OnClientItemSelected="MADeppickupAutocompleteSelected">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtMADeppickupcode" Style="display: none" runat="server"></asp:TextBox>
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
                                        <!-- \Departure  End\ -->
                                          <!-- \Transit Start\ -->
                                           <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    TRANSIT</label>
                                                <div class="search-large-i" id="div15" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chktransit" CssClass="side-block jq-checkbox-tour"  runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        Transit Arrival DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                          <asp:TextBox ID="txtTransitarrdate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                            runat="server"></asp:TextBox>
                                                        <span class="date-icon"></span>

                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddltranarrflightclass" runat="server" class="custom-select custom-select-ddlStarCategory">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                         <div class="search-large-i" style="float: left;">
                                            <!-- /MA transit Arrival flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMAtranArrFlight" placeholder="--" runat="server" onkeydown="MATranArrivalflightSetContextKey()">
                                                        </asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAtranArrFlight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetMATranArrivalflight" TargetControlID="txtMAtranArrFlight"
                                                            UseContextKey="true" OnClientItemSelected="MATranArrivalflightAutocompleteSelected">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtMATranArrFlightCode" Style="display: none" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /MA Transit Arrival time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        ARRIVAL TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMATranArrTime" runat="server"></asp:TextBox>
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
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div16" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                          <!-- /MA Transit Arrival Pikcup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                Transit ARRIVAL AIRPORT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtMAtranArrivalpickup" placeholder="--" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAtranArrivalpickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetMATranArrivalpickup" TargetControlID="txtMAtranArrivalpickup"
                                                    UseContextKey="true" OnClientItemSelected="MATranArrivalpickupAutocompleteSelected">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtMATransitArrivalpickupcode" Style="display: none" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="clear">
                                        </div>

                                          <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div17" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                          <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        Transit Departure DATE</label>
                                                    <div class="input-a">
                                                          <asp:TextBox ID="txtMATrandepdate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                            runat="server"></asp:TextBox>
                                                        <span class="date-icon"></span>

                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlMAtrandepflightlass" runat="server" class="custom-select custom-select-ddlStarCategory">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>

                                          <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- /MA Tran Departure flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMAtranDepartureFlight" placeholder="--" runat="server" onkeydown="MATranDepartureflightSetContextKey()">
                                                        </asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAtranDepartureFlight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetMAtranDepartureflight" TargetControlID="txtMAtranDepartureFlight"
                                                            UseContextKey="true" OnClientItemSelected="MATranDepartureAutocompleteSelected">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtMATranDepartureFlightCode" Style="display: none" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /MA Departure time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        DEPARTURE TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMATranDepartureTime" runat="server"></asp:TextBox>
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
                                           <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div18" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                          <!-- /MA Transit Departure Pikcup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                Transit Departure AIRPORT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtMAtranDeppickup" placeholder="--" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetMATranDeparturepickup" TargetControlID="txtMAtranDeppickup"
                                                    UseContextKey="true" OnClientItemSelected="MATranDeppickupAutocompleteSelected">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtMATransitDeparturepickupcode" Style="display: none" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <!-- \\ -->

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
                                        <!-- \Transit Departure  End\ -->

                                         <!-- \MA Cunstomer\ -->
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

                                           <div class="search-large-i" style="float: left; margin-top: 20px;" id="dvMACustomer" runat ="server">
                                            <!-- // -->

                                             <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMACustomer" runat="server" placeholder="--" ></asp:TextBox>
                                                        <asp:TextBox ID="txtMACustomercode" runat="server" Style="display: none"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMACustomer" runat="server" CompletionInterval="10"
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetMACustomer" TargetControlID="txtMACustomer"
                                                            OnClientItemSelected="MACustomerAutocompleteSelected">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>

                                        </div>
                                          <!-- \MA Source Country\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <div>
                                                <label>
                                                    Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtMASourcecountry" runat="server" placeholder="--"></asp:TextBox>
                                                    <asp:TextBox ID="txtMASourcecountrycode" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMASourcecountry" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetMACountry" TargetControlID="txtMASourcecountry"
                                                        UseContextKey ="true"   OnClientItemSelected="MACountryautocompleteselected">
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
                                         <!-- \MA Adult child\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom" style="width: 80%;">
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        adult<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlMAAdult" class="custom-select custom-select-ddlMAAdult"
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
                                                        <asp:DropDownList ID="ddlMAChild" class="custom-select custom-select-ddlMAChild"
                                                            runat="server">
                                                            
                                                        </asp:DropDownList>
                                                    </div>
                                                   
                                                </div>
                                                <div class="srch-tab-3c" style="float: left; margin-top: 20px;" >
                                                   <div  id="divMAOverride" style="width:120%" runat="server">
                                                    
                                                    <asp:CheckBox ID="chkMAoverride" CssClass="side-block jq-checkbox-tour"  runat="server" />
                                                    <asp:Label ID="Label2" runat="server" CssClass="page-search-content-override-price"
                                                        Text="Override Price"></asp:Label>
                                                    
                                                </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- // advanced // -->
                                        <div class="clear">
                                        </div>

                                         <div id="divMAchild" runat="server" style="margin-top: 20px;display:none;">
                                       
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
                                                <div class="srch-tab-child" id="dvMAChild1" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="select-wrapper" style="width: 75px;">
                                                            <div class="srch-tab-child-pre" id="div45">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="input-a">
                                                                        <asp:TextBox ID="txtMAChild1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="srch-tab-child" id="dvMAChild2" style="float: left;">
                                                     <div class="select-wrapper" style="width: 75px;">
                                                            <div class="srch-tab-child-pre" id="div1">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="input-a">
                                                                        <asp:TextBox ID="txtMAChild2" placeholder="CH 2" runat="server" onchange="validateAge(this)"
                                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                </div>
                                                 <div class="srch-tab-child" id="dvMAChild3" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                            <div class="srch-tab-child-pre" id="div2">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="input-a">
                                                                        <asp:TextBox ID="txtMAChild3" placeholder="CH 3" runat="server" onchange="validateAge(this)"
                                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvMAChild4" style="float: left;">
                                                     <div class="select-wrapper" style="width: 75px;">
                                                            <div class="srch-tab-child-pre" id="div3">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="input-a">
                                                                        <asp:TextBox ID="txtMAChild4" placeholder="CH 4" runat="server" onchange="validateAge(this)"
                                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                </div>
                                               
                                               
                                                
                                            </div>
                                             <div class="search-large-i-child-tour">
                                                <label style="color: White;">
                                                    Ages of children at Airport Meet</label>
                                                     <div class="srch-tab-child" id="dvMAChild5" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                            <div class="srch-tab-child-pre" id="div4">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="input-a">
                                                                        <asp:TextBox ID="txtMAChild5" placeholder="CH 5" runat="server" onchange="validateAge(this)"
                                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                </div>
                                                 <div class="srch-tab-child" id="dvMAChild6" style="float: left;">
                                                   <div class="select-wrapper" style="width: 75px;">
                                                            <div class="srch-tab-child-pre" id="div12">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="input-a">
                                                                        <asp:TextBox ID="txtMAChild6" placeholder="CH 6" runat="server" onchange="validateAge(this)"
                                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                </div>
                                                 <div class="srch-tab-child" id="dvMAChild7" style="float: left;">
                                                     <div class="select-wrapper" style="width: 75px;">
                                                            <div class="srch-tab-child-pre" id="div13">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="input-a">
                                                                        <asp:TextBox ID="txtMAChild7" placeholder="CH 7" runat="server" onchange="validateAge(this)"
                                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvMAChild8" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                            <div class="srch-tab-child-pre" id="div14">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="input-a">
                                                                        <asp:TextBox ID="txtMAChild8" placeholder="CH 8" runat="server" onchange="validateAge(this)"
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
                             
                        <input  id="btnMAreset"  type="button"  class="srch-btn-home"  value="Reset"/>
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
          <div  class="side-price" id="divslideprice" runat ="server">
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
              <div class="side-lbl">AIRPORT MEET TYPE</div>  
           <div><asp:TextBox ID="txtSearchFocus" Style="width:2px;height:2px;border:none;" MaxLength="1" runat="server"></asp:TextBox> </div>
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
          <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
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
                        <asp:Button ID="btnbooknow" CssClass="guest-flight-details-generate" runat="server"    Text="BOOK SELECTED SERVICES"></asp:Button> 
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
              
              <!-- /Arrival/ -->
                <asp:DataList ID="dlMAArrivalSearchResults" runat="server"   Width="100%"  >
                   <HeaderTemplate>
                   <div style="margin-bottom:15px;background-color:#F2F3F4;padding-top:6px;padding-bottom:6px">
                           <asp:Label ID="lblheader" runat="server" Text="ARRIVAL" ForeColor="#009999" Font-Bold="True"></asp:Label></div>
                    </HeaderTemplate>
                    <ItemTemplate>
                     
                      <div class="cat-list-item fly-in" style="margin-top:-10px;" >
                            <div class="cat-list-trfitem-l" style="margin-top:10px;margin-left:10px;">
                                <a href="#">
                                    <asp:Image ID="imgMAImage" runat="server" Width="120px" Height="88px" /></a>
                                <asp:Label ID="lblMAImage" Visible="false" runat="server" Text='<%# Eval("imagename") %>'></asp:Label>
                            </div>
                          <div class="cat-list-trfitem-r" style="padding-top:5px;padding-bottom:15px;">
                              <div style="width: 55%; float: left;border-right: 1px solid #F2F3F4;padding-top:10px;padding-right:15px;min-height:100px; " >
                                  <div>
                                      <div class="offer-slider-link" style="margin-top:5px;color:#4c4c4c !important;" >
                                          <asp:HiddenField ID="hdAirportTypeCode" Value='<%# Eval("airportmatypecode") %>' runat="server" />
                                          <asp:Label ID="lblAirportTypeName" CssClass="offer-slider-link" Text='<%# Eval("airportmatypename") %>'
                                              runat="server"></asp:Label>
                                      </div>
                                      <div class="clear">
                                      </div>
                                     
                                      <div class="offer-slider-location1" id="divminmaxpax" runat="server">
                                       <p style="text-align: justify;">
                                          <asp:Label ID="lblremarks"  class="offer-slider-location1"  runat="server" ToolTip='<%# Eval("remarks") %>' Text='<%# Eval("remarks") %>'></asp:Label>
                                      </p>
                                      <div id="dvArrMin" runat="server"  style="float:left; padding-right:10px;">
                                    <asp:Label ID="lblAdultChildText" runat="server" Text='<%# Eval("AdultChildText").ToString() %>'></asp:Label>
                                          <asp:Label ID="lblmin" Visible="false" runat="server" Text="MINIMUM"> </asp:Label>&nbsp;
                                          <asp:Label ID="lblminpax"  Visible="false" runat="server" Text='<%# Eval("minpax").ToString() + " PAX " %>'></asp:Label>
                                          </div>

                                         <div id="dvArrMax" runat="server" style="float:left;" >    <asp:Label ID="lblmax" runat="server" Text="MAXIMUM"> </asp:Label>&nbsp;
                                          <asp:Label ID="lblmaxpax" runat="server" Text='<%# Eval("maxpax").ToString()  + " PAX " %>'></asp:Label></div>
                                      </div>
                                     
                                      <div class="trf-slider-location" id="divunitprice" runat="server" style="padding-top: 10px; padding-left: 0px;width:150px">
                                         <div id="divlbl" runat ="server" >
                                         <asp:Label ID="lblunitprice" runat="server" CssClass ="offer-slider-r-labeltrf" ></asp:Label>
                                         </div>
                                     
                                      </div> 
                                      <asp:HiddenField ID="hddlTransferSearchResultsItemIndex" runat="server" />
                                      <br class="clear" />
                                  </div>
                              </div>
                              <div style="width: 40%; float: left;padding-top:10px;padding-left:15px;" >
                              <div style="width: 50%; float: left;" >
                                <div class="search-large-i" style="width:100%">
                                      <div class="trflbl-slider-location1" style="margin-left: 15px;display:none;">
                                          <asp:Label ID="lblunitname" runat="server" Text="NO.OF.UNITS "> </asp:Label>
                                          <asp:Label ID="lblunit" runat="server" Text='<%# Eval("units") %>'> </asp:Label>
                                      </div>
                                  </div>
                                  <div class="clear"></div>
                                  <div class="search-large-i"  style="width:100%;">
                                      <div class="trflbl-slider-location" style="margin-left: 15px;">
                                          <div style="margin-top: 5px;" id="divtot" runat="server">
                                              <asp:Label ID="lbltotal" runat="server" Text="TOTAL"> </asp:Label>
                                              <asp:LinkButton ID="lbtotalValue" CssClass="ma-total-price-label" 
                                                  runat="server" Text='<%# Eval("totalsalevalue") %>' 
                                                  onclick="lbtotalValue_Click"> </asp:LinkButton>

                                                     <asp:LinkButton ID="lbwltotalValue" CssClass="ma-total-price-label" 
                                                  runat="server" Text='<%# Eval("wltotalsalevalue") %>' 
                                                  onclick="lbtotalValue_Click"> </asp:LinkButton>
                                                    <asp:Label ID="lblTotalCurrcode" runat="server"  CssClass="ma-total-price-label"  Text='<%# Eval("currcode") %>'> </asp:Label>  
                                                   <asp:LinkButton ID="lnkcumunits" style="color:#ff7200;float: none;  font-family: 'Raleway';margin-bottom:7px;text-decoration:none; font-size: 10px !important;display:none"
                                                  runat="server" Text="Units" 
                                                  onclick="lbtotalValue_Click"> </asp:LinkButton>
                                                    <div class="clear" style="padding-top:10px; margin-left:-10px;font-weight:400;">
                                                                            <asp:Label ID="lblIncTax" CssClass="offer-slider-r-price-by-tax" runat="server" Text="Incl of Taxes & vat"></asp:Label>
                                                                        </div>
                                          </div>
                                      </div>
                                  </div>
                              </div>
                               <div style="width: 50%; float: left;" >
                                  <div class="clear"></div>
                                    <div class="search-large-i"  style="width:100%;padding-top:5px;"> 
                                       <div class="trflbl-slider-location" style="margin-left:20px ">
                                       <asp:Label ID="lblbook" runat="server" Text="BOOK NOW" > </asp:Label>
                                       <div   style="margin-top:5px;margin-left:20px;padding-top:5px " >
                                        <asp:CheckBox ID="chkbooknow" CssClass="side-block jq-checkbox-tour"  runat="server" />
                                        </div> </div>
                                     </div>
                              </div>

                                    </div>
                             
                                  </div>
                        </div>
                            
                        <%--  </div>--%>
                               <div  style ="display:none" runat="server" id="divfields">
                         <%--           <asp:Label ID="lblmaxadult" runat="server" Text='<%# Eval("maxadults") %>' ></asp:Label>
                                        <asp:Label ID="lblmaxchild" runat="server" Text='<%# Eval("maxchild") %>' ></asp:Label>
                                        <asp:Label ID="lblshuttle" runat="server" Text='<%# Eval("shuttle") %>' ></asp:Label>
                                        <asp:Label ID="lblpaxcheck" runat="server" Text='<%# Eval("paxcheckreqd") %>' ></asp:Label>
                                        <asp:Label ID="lblairportborder" runat="server" Text='<%# Eval("airportbordercode") %>' ></asp:Label>
                                        <asp:Label ID="lblfromsector" runat="server" Text='<%# Eval("fromsectorgroupcode") %>' ></asp:Label>
                                        <asp:Label ID="lblsectorgroupcode" runat="server" Text='<%# Eval("sectorgroupcode") %>' ></asp:Label>
                                        <asp:Label ID="lbltransferdate" runat="server" Text='<%# Eval("transferdate") %>' ></asp:Label>--%>
                                        <asp:Label ID="lbladdlpax" runat="server" Text='<%# Eval("addlpax") %>'></asp:Label>
                                        <asp:Label ID="lbladultprice" runat="server" Text='<%# Eval("adultprice") %>'></asp:Label>
                                         <asp:Label ID="lblchildprice" runat="server" Text='<%# Eval("childprice") %>'></asp:Label>
                                          <asp:Label ID="lbladultsalevalue" runat="server" Text='<%# Eval("adultsalevalue") %>'></asp:Label>
                                          <asp:Label ID="lblchildsalevalue" runat="server" Text='<%# Eval("childsalevalue") %>'></asp:Label>
                                           <asp:Label ID="lbladdlpaxsalevalue" runat="server" Text='<%# Eval("addlpaxsalevalue") %>'></asp:Label>
                                           <asp:Label ID="lbladdlpaxprice" runat="server" Text='<%# Eval("addlpaxprice") %>'></asp:Label>
                                       

                                         <asp:Label ID="lblairportmadate" runat="server" Text='<%# Eval("airportmadate") %>'></asp:Label>
                                        <asp:Label ID="lblmaxpax1" runat="server" Text='<%# Eval("maxpax") %>' ></asp:Label>
                                        <asp:Label ID="lblratebasis" runat="server" Text='<%# Eval("ratebasis") %>' ></asp:Label>
                                        <asp:Label ID="lblminpax1" runat="server" Text='<%# Eval("minpax") %>' ></asp:Label>
                                        <asp:Label ID="lbladults" runat="server" Text='<%# Eval("adults") %>' ></asp:Label>
                                        <asp:Label ID="lblchild" runat="server" Text='<%# Eval("child") %>' ></asp:Label>
                                        <asp:Label ID="lblchildagestring" runat="server" Text='<%# Eval("childagestring") %>' ></asp:Label>
                                        <asp:Label ID="lblMAunit" runat="server" Text='<%# Eval("units") %>' ></asp:Label>
                                        <asp:Label ID="lblprice" runat="server" Text='<%# Eval("unitprice") %>' ></asp:Label>
                                        <asp:Label ID="lblvalue" runat="server" Text='<%# Eval("unitsalevalue") %>' ></asp:Label>
                                        <asp:Label ID="lbladultplistcode" runat="server" Text='<%# Eval("adultplistcode") %>' ></asp:Label>
                                        <asp:Label ID="lblchildplistcode" runat="server" Text='<%# Eval("childplistcode") %>' ></asp:Label>
                                        <asp:Label ID="lblunitplistcode" runat="server" Text='<%# Eval("unitplistcode") %>' ></asp:Label>
                                        <asp:Label ID="lbladdlpaxplistcode" runat="server" Text='<%# Eval("addlpaxplistcode") %>' ></asp:Label>
                                         <asp:HiddenField ID="hdnselected" Value='<%# Eval("currentselection") %>' runat="server" />


                                          <asp:Label ID="lblpreferredsupplier" runat="server" Text='<%# Eval("preferredsupplier") %>' ></asp:Label>
                                           <asp:Label ID="lbladultcprice" runat="server" Text='<%# Eval("adultcprice") %>' ></asp:Label>
                                            <asp:Label ID="lblchildcprice" runat="server" Text='<%# Eval("childcprice") %>' ></asp:Label>
                                             <asp:Label ID="lblunitcprice" runat="server" Text='<%# Eval("unitcprice") %>' ></asp:Label>
                                              <asp:Label ID="lbladdlpaxcprice" runat="server" Text='<%# Eval("addlpaxcprice") %>' ></asp:Label>
                                               <asp:Label ID="lbladultcostvalue" runat="server" Text='<%# Eval("adultcostvalue") %>' ></asp:Label> 
                                               <asp:Label ID="lblchildcostvalue" runat="server" Text='<%# Eval("childcostvalue") %>' ></asp:Label>
                                                <asp:Label ID="lblunitcostvalue" runat="server" Text='<%# Eval("unitcostvalue") %>' ></asp:Label>
                                                 <asp:Label ID="lbladdlpaxcostvalue" runat="server" Text='<%# Eval("addlpaxcostvalue") %>' ></asp:Label>
                                                  <asp:Label ID="lbltotalcostvalue" runat="server" Text='<%# Eval("totalcostvalue") %>' ></asp:Label>
                                                   <asp:Label ID="lbladultcplistcode" runat="server" Text='<%# Eval("adultcplistcode") %>' ></asp:Label>
                                                    <asp:Label ID="lblchildcplistcode" runat="server" Text='<%# Eval("childcplistcode") %>' ></asp:Label>
                                                     <asp:Label ID="lblunitcplistcode" runat="server" Text='<%# Eval("unitcplistcode") %>' ></asp:Label>
                                                     <asp:Label ID="lbladdlpaxcplistcode" runat="server" Text='<%# Eval("addlpaxcplistcode") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladultprice" runat="server" Text='<%# Eval("wladultprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlchildprice" runat="server" Text='<%# Eval("wlchildprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlunitprice" runat="server" Text='<%# Eval("wlunitprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladdlpaxprice" runat="server" Text='<%# Eval("wladdlpaxprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladultsalevalue" runat="server" Text='<%# Eval("wladultsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlchildsalevalue" runat="server" Text='<%# Eval("wlchildsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlunitsalevalue" runat="server" Text='<%# Eval("wlunitsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladdlpaxsalevalue" runat="server" Text='<%# Eval("wladdlpaxsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwltotalsalevalue" runat="server" Text='<%# Eval("wltotalsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlcurrcode" runat="server" Text='<%# Eval("wlcurrcode") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlconvrate" runat="server" Text='<%# Eval("wlconvrate") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>' ></asp:Label>
                             

                                    </div> 
                          
                       <%-- </div>--%>

                    </ItemTemplate>
                     <FooterTemplate>
                            <div style="float: right; margin-top: -25px; background-color: White; padding: 5px">
                                <asp:LinkButton ID="lbArrShowMore" CssClass="rate-plan-headings" OnClick="lbArrShowMore_Click"
                                    runat="server">Show More</asp:LinkButton>
                             </div>
                        </FooterTemplate>
                </asp:DataList>
              <!-- \Arrival End\ -->
              <!-- / Departure / -->
               <asp:DataList ID="dlMADepartureSearchResults" runat="server"   Width="100%"  >
                   <HeaderTemplate>
                   <div style="margin-bottom:15px;background-color:#F2F3F4;padding-top:6px;padding-bottom:6px">
                           <asp:Label ID="lblDepheader" runat="server" Text="DEPARTURE" ForeColor="#009999" Font-Bold="True"></asp:Label>
                           </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                     
                      <div class="cat-list-item fly-in"  style="margin-top:-10px;">
                            <div class="cat-list-trfitem-l" style="margin-top:10px;margin-left:10px;">
                                <a href="#">
                                    <asp:Image ID="imgMAImage" runat="server" Width="120px" Height="88px" /></a>
                                <asp:Label ID="lblMAImage" Visible="false" runat="server" Text='<%# Eval("imagename") %>'></asp:Label>
                            </div>
                          <div class="cat-list-trfitem-r">
                              <div style="width: 55%; float: left;border-right: 1px solid #F2F3F4;padding-top:10px;padding-right:15px;min-height:100px; " >
                                  <div>
                                        <div class="offer-slider-link" style="margin-top:5px;color:#4c4c4c !important;" >
                                          <asp:HiddenField ID="hdAirportTypeCode" Value='<%# Eval("airportmatypecode") %>' runat="server" />
                                          <asp:Label ID="lblAirportTypeName"  CssClass="offer-slider-link"  Text='<%# Eval("airportmatypename") %>'
                                              runat="server"></asp:Label>
                                      </div>
                                      <div class="clear">
                                      </div>
                                     
                                      <div class="offer-slider-location1" id="divminmaxpax" runat="server">
                                       <p style="text-align: justify;">
                                          <asp:Label ID="lblremarks"  class="offer-slider-location1" runat="server" ToolTip='<%# Eval("remarks") %>' Text='<%# Eval("remarks") %>'></asp:Label>
                                      </p>
                                         <div id="dvDepMin" runat="server"  style="float:left; padding-right:10px;">
                                           <asp:Label ID="lblAdultChildText" runat="server" Text='<%# Eval("AdultChildText").ToString() %>'></asp:Label>
                                          <asp:Label ID="lblmin" Visible="false" runat="server" Text="MINIMUM"> </asp:Label>&nbsp;
                                          <asp:Label ID="lblminpax" Visible="false" runat="server" Text='<%# Eval("minpax").ToString() + " PAX " %>'></asp:Label></div>
                                          <div id="dvDepMax" runat="server"  style="float:left; padding-right:10px;">
                                          <asp:Label ID="lblmax" runat="server" Text="MAXIMUM"> </asp:Label>&nbsp;
                                          <asp:Label ID="lblmaxpax" runat="server" Text='<%# Eval("maxpax").ToString()  + " PAX " %>'></asp:Label></div>
                                      </div>
                                     
                                      <div class="trf-slider-location" id="divunitprice" runat="server" style="padding-top: 10px; padding-left: 0px;width:150px">
                                     
                                      </div> 
                                      <asp:HiddenField ID="hddlTransferSearchResultsItemIndex" runat="server" />
                                      <br class="clear" />
                                  </div>
                              </div>
                         <div style="width: 40%; float: left;padding-top:10px" >
                              <div style="width: 50%; float: left;" >
                                <div class="search-large-i" style="width:100%">
                                      <div class="trflbl-slider-location1" style="margin-left: 15px;display:none;">
                                          <asp:Label ID="lblunitname" runat="server" Text="NO.OF.UNITS "> </asp:Label>
                                          <asp:Label ID="lblunit" runat="server" Text='<%# Eval("units") %>'> </asp:Label>
                                      </div>
                                  </div>
                                  <div class="clear"></div>
                                  <div class="search-large-i"  style="width:100%;">
                                      <div class="trflbl-slider-location" style="margin-left: 15px;">
                                          <div style="margin-top: 5px;" id="divtot" runat="server">
                                              <asp:Label ID="lbltotal" runat="server" Text="TOTAL"> </asp:Label>
                                              <asp:LinkButton ID="lbtotalValue" runat="server"  CssClass="ma-total-price-label" 
                                                  Text='<%# Eval("totalsalevalue") %>' onclick="lbtotalValue_Click1"> </asp:LinkButton>
                                                         <asp:LinkButton ID="lbwltotalValue" CssClass="ma-total-price-label" 
                                                  runat="server" Text='<%# Eval("wltotalsalevalue") %>' 
                                                  onclick="lbtotalValue_Click1"> </asp:LinkButton>
                                                        
                                                    <asp:LinkButton ID="lnkcumunits" style="color:#ff7200;float: none;  font-family: 'Raleway';margin-bottom:7px;text-decoration:none; font-size: 10px !important;display:none"
                                                  runat="server" Text="Units" 
                                                  onclick="lbtotalValue_Click1"> </asp:LinkButton>
                                                    <asp:Label ID="lblTotalCurrcode" runat="server"  CssClass="ma-total-price-label"  Text='<%# Eval("currcode") %>'> </asp:Label>  
                                                      <div class="clear" style="padding-top:10px; margin-left:-10px;font-weight:500;">
                                                                            <asp:Label ID="lblIncTax" CssClass="offer-slider-r-price-by-tax" runat="server" Text="Incl of Taxes & vat"></asp:Label>
                                                                        </div>
                                          </div>
                                      </div>
                                  </div>
                              </div>
                               <div style="width: 50%; float: left;" >
                                  <div class="clear"></div>
                                    <div class="search-large-i"  style="width:100%;padding-top:5px;"> 
                                       <div class="trflbl-slider-location" style="margin-left:20px ">
                                       <asp:Label ID="lblbook" runat="server" Text="BOOK NOW" > </asp:Label>
                                       <div   style="margin-top:5px;margin-left:20px;padding-top:5px " >
                                        <asp:CheckBox ID="chkbooknow" CssClass="side-block jq-checkbox-tour"  runat="server" />
                                        </div> </div>
                                     </div>
                              </div>

                                    </div>
                             
                                  </div>
                        </div>
                            
                        <%--  </div>--%>
                               <div  style ="display:none" runat="server" id="divfields">
                                 <%--   <asp:Label ID="lblmaxadult" runat="server" Text='<%# Eval("maxadults") %>' ></asp:Label>
                                        <asp:Label ID="lblmaxchild" runat="server" Text='<%# Eval("maxchild") %>' ></asp:Label>
                                        <asp:Label ID="lblshuttle" runat="server" Text='<%# Eval("shuttle") %>' ></asp:Label>
                                        <asp:Label ID="lblpaxcheck" runat="server" Text='<%# Eval("paxcheckreqd") %>' ></asp:Label>
                                        <asp:Label ID="lblairportborder" runat="server" Text='<%# Eval("airportbordercode") %>' ></asp:Label>
                                        <asp:Label ID="lblfromsector" runat="server" Text='<%# Eval("fromsectorgroupcode") %>' ></asp:Label>
                                        <asp:Label ID="lblsectorgroupcode" runat="server" Text='<%# Eval("sectorgroupcode") %>' ></asp:Label>
                                        <asp:Label ID="lbltransferdate" runat="server" Text='<%# Eval("transferdate") %>' ></asp:Label>--%>
                                   <asp:Label ID="lbladdlpax" runat="server" Text='<%# Eval("addlpax") %>'></asp:Label>
                                   <asp:Label ID="lbladultprice" runat="server" Text='<%# Eval("adultprice") %>'></asp:Label>
                                   <asp:Label ID="lblchildprice" runat="server" Text='<%# Eval("childprice") %>'></asp:Label>
                                   <asp:Label ID="lbladultsalevalue" runat="server" Text='<%# Eval("adultsalevalue") %>'></asp:Label>
                                   <asp:Label ID="lblchildsalevalue" runat="server" Text='<%# Eval("childsalevalue") %>'></asp:Label>
                                   <asp:Label ID="lbladdlpaxsalevalue" runat="server" Text='<%# Eval("addlpaxsalevalue") %>'></asp:Label>
                                   <asp:Label ID="lbladdlpaxprice" runat="server" Text='<%# Eval("addlpaxprice") %>'></asp:Label>
                                   <asp:Label ID="lblairportmadate" runat="server" Text='<%# Eval("airportmadate") %>'></asp:Label>
                                   <asp:Label ID="lblmaxpax1" runat="server" Text='<%# Eval("maxpax") %>'></asp:Label>
                                   <asp:Label ID="lblratebasis" runat="server" Text='<%# Eval("ratebasis") %>'></asp:Label>
                                   <asp:Label ID="lblminpax1" runat="server" Text='<%# Eval("minpax") %>'></asp:Label>
                                   <asp:Label ID="lbladults" runat="server" Text='<%# Eval("adults") %>'></asp:Label>
                                   <asp:Label ID="lblchild" runat="server" Text='<%# Eval("child") %>'></asp:Label>
                                   <asp:Label ID="lblchildagestring" runat="server" Text='<%# Eval("childagestring") %>'></asp:Label>
                                   <asp:Label ID="lblMAunit" runat="server" Text='<%# Eval("units") %>'></asp:Label>
                                   <asp:Label ID="lblprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label>
                                   <asp:Label ID="lblvalue" runat="server" Text='<%# Eval("unitsalevalue") %>'></asp:Label>
                                    <asp:Label ID="lbladultplistcode" runat="server" Text='<%# Eval("adultplistcode") %>' ></asp:Label>
                                        <asp:Label ID="lblchildplistcode" runat="server" Text='<%# Eval("childplistcode") %>' ></asp:Label>
                                        <asp:Label ID="lblunitplistcode" runat="server" Text='<%# Eval("unitplistcode") %>' ></asp:Label>
                                        <asp:Label ID="lbladdlpaxplistcode" runat="server" Text='<%# Eval("addlpaxplistcode") %>' ></asp:Label>
                                        <asp:HiddenField ID="hdnselected" Value='<%# Eval("currentselection") %>' runat="server" />

                                         <asp:Label ID="lblpreferredsupplier" runat="server" Text='<%# Eval("preferredsupplier") %>' ></asp:Label>
                                           <asp:Label ID="lbladultcprice" runat="server" Text='<%# Eval("adultcprice") %>' ></asp:Label>
                                            <asp:Label ID="lblchildcprice" runat="server" Text='<%# Eval("childcprice") %>' ></asp:Label>
                                             <asp:Label ID="lblunitcprice" runat="server" Text='<%# Eval("unitcprice") %>' ></asp:Label>
                                              <asp:Label ID="lbladdlpaxcprice" runat="server" Text='<%# Eval("addlpaxcprice") %>' ></asp:Label>
                                               <asp:Label ID="lbladultcostvalue" runat="server" Text='<%# Eval("adultcostvalue") %>' ></asp:Label> 
                                               <asp:Label ID="lblchildcostvalue" runat="server" Text='<%# Eval("childcostvalue") %>' ></asp:Label>
                                                <asp:Label ID="lblunitcostvalue" runat="server" Text='<%# Eval("unitcostvalue") %>' ></asp:Label>
                                                 <asp:Label ID="lbladdlpaxcostvalue" runat="server" Text='<%# Eval("addlpaxcostvalue") %>' ></asp:Label>
                                                  <asp:Label ID="lbltotalcostvalue" runat="server" Text='<%# Eval("totalcostvalue") %>' ></asp:Label>
                                                   <asp:Label ID="lbladultcplistcode" runat="server" Text='<%# Eval("adultcplistcode") %>' ></asp:Label>
                                                    <asp:Label ID="lblchildcplistcode" runat="server" Text='<%# Eval("childcplistcode") %>' ></asp:Label>
                                                     <asp:Label ID="lblunitcplistcode" runat="server" Text='<%# Eval("unitcplistcode") %>' ></asp:Label>
                                                     <asp:Label ID="lbladdlpaxcplistcode" runat="server" Text='<%# Eval("addlpaxcplistcode") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladultprice" runat="server" Text='<%# Eval("wladultprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlchildprice" runat="server" Text='<%# Eval("wlchildprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlunitprice" runat="server" Text='<%# Eval("wlunitprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladdlpaxprice" runat="server" Text='<%# Eval("wladdlpaxprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladultsalevalue" runat="server" Text='<%# Eval("wladultsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlchildsalevalue" runat="server" Text='<%# Eval("wlchildsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlunitsalevalue" runat="server" Text='<%# Eval("wlunitsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladdlpaxsalevalue" runat="server" Text='<%# Eval("wladdlpaxsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwltotalsalevalue" runat="server" Text='<%# Eval("wltotalsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlcurrcode" runat="server" Text='<%# Eval("wlcurrcode") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlconvrate" runat="server" Text='<%# Eval("wlconvrate") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>' ></asp:Label>
                             
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
              <!-- / Transit / -->
               <asp:DataList ID="dlMATransitSearchResults" runat="server"   Width="100%"  >
                   <HeaderTemplate>
                   <div style="padding-bottom:10px;background-color:#F2F3F4;padding-top:6px;padding-bottom:6px">
                           <asp:Label ID="lblDepheader" runat="server" Text="TRANSIT" ForeColor="#009999" Font-Bold="True"></asp:Label></div>
                    </HeaderTemplate>
                    <ItemTemplate>
                     
                      <div class="cat-list-item fly-in"  style="margin-top:-10px;"  >
                            <div class="cat-list-trfitem-l" style="margin-top:10px;padding-left:10px;">
                                <a href="#">
                                    <asp:Image ID="imgMAImage" runat="server" Width="120px" Height="88px" /></a>
                                <asp:Label ID="lblMAImage" Visible="false" runat="server" Text='<%# Eval("imagename") %>'></asp:Label>
                            </div>
                          <div class="cat-list-trfitem-r">
                              <div style="width: 55%; float: left;border-right: 1px solid #F2F3F4;padding-top:10px;padding-right:15px;min-height:100px; " >
                                  <div>
                                      <div  class="offer-slider-link"  style="margin-top:10px" >
                                          <asp:HiddenField ID="hdAirportTypeCode" Value='<%# Eval("airportmatypecode") %>' runat="server" />
                                          <asp:Label ID="lblAirportTypeName"  CssClass="offer-slider-link"  Text='<%# Eval("airportmatypename") %>'
                                              runat="server"></asp:Label>
                                      </div>
                                      <div class="clear">
                                      </div>
                                     
                                      <div  class="offer-slider-location1" id="divminmaxpax" runat="server">
                                       <p style="text-align: justify;">
                                          <asp:Label ID="lblremarks" runat="server"  CssClass="offer-slider-location1" ToolTip='<%# Eval("remarks") %>' Text='<%# Eval("remarks") %>'></asp:Label>
                                      </p>
                                       <div id="dvTranMin" runat="server"  style="float:left; padding-right:10px;">
                                         <asp:Label ID="lblAdultChildText" runat="server" Text='<%# Eval("AdultChildText").ToString() %>'></asp:Label>
                                          <asp:Label ID="lblmin" Visible="false" runat="server" Text="MINIMUM"> </asp:Label>&nbsp;
                                          <asp:Label ID="lblminpax" Visible="false" runat="server" Text='<%# Eval("minpax").ToString() + " PAX " %>'></asp:Label></div>
                                           <div id="dvTranMax" runat="server"  style="float:left; padding-right:10px;">
                                          <asp:Label ID="lblmax" runat="server" Text="MAXIMUM"> </asp:Label>&nbsp;
                                          <asp:Label ID="lblmaxpax" runat="server" Text='<%# Eval("maxpax").ToString()  + " PAX " %>'></asp:Label></div>
                                      </div>
                                     
                                      <div class="trf-slider-location" id="divunitprice" runat="server" style="padding-top: 10px; padding-left: 0px;width:150px">
                             
                                      </div> 
                                      <asp:HiddenField ID="hddlTransferSearchResultsItemIndex" runat="server" />
                                      <br class="clear" />
                                  </div>
                              </div>
                               <div style="width: 40%; float: left;padding-top:10px" >
                              <div style="width: 50%; float: left;" >
                                <div class="search-large-i" style="width:100%">
                                      <div class="trflbl-slider-location1" style="margin-left: 15px;display:none;">
                                          <asp:Label ID="lblunitname" runat="server" Text="NO.OF.UNITS "> </asp:Label>
                                          <asp:Label ID="lblunit" runat="server" Text='<%# Eval("units") %>'> </asp:Label>
                                      </div>
                                  </div>
                                  <div class="clear"></div>
                                  <div class="search-large-i"  style="width:100%;">
                                      <div class="trflbl-slider-location" style="margin-left: 15px;">
                                          <div style="margin-top: 5px;" id="divtot" runat="server">
                                              <asp:Label ID="lbltotal" runat="server" Text="TOTAL"> </asp:Label>
                                              <asp:LinkButton ID="lbtotalValue" runat="server"  CssClass="ma-total-price-label" 
                                                  Text='<%# Eval("totalsalevalue") %>' onclick="lbtotalValue_Click2"> </asp:LinkButton>
                                                           <asp:LinkButton ID="lbwltotalValue" CssClass="ma-total-price-label" 
                                                  runat="server" Text='<%# Eval("wltotalsalevalue") %>' 
                                                  onclick="lbtotalValue_Click2"> </asp:LinkButton> 
                                                     <asp:Label ID="lblTotalCurrcode" runat="server"  CssClass="ma-total-price-label"  Text='<%# Eval("currcode") %>'> </asp:Label>  
                                                       
                                                        <asp:LinkButton ID="lnkcumunits" style="color:#ff7200;float: none;  font-family: 'Raleway';margin-bottom:7px;text-decoration:none; font-size: 10px !important;display:none"
                                                  runat="server" Text="Units" 
                                                  onclick="lbtotalValue_Click2"> </asp:LinkButton>
                                                    <div class="clear" style="padding-top:10px; margin-left:-10px;font-weight:500;">
                                                                            <asp:Label ID="lblIncTax" CssClass="offer-slider-r-price-by-tax" runat="server" Text="Incl of Taxes & vat"></asp:Label>
                                                                        </div>
                                          </div>
                                      </div>
                                  </div>
                              </div>
                               <div style="width: 50%; float: left;" >
                                  <div class="clear"></div>
                                    <div class="search-large-i"  style="width:100%;padding-top:5px;"> 
                                       <div class="trflbl-slider-location" style="margin-left:20px ">
                                       <asp:Label ID="lblbook" runat="server" Text="BOOK NOW" > </asp:Label>
                                       <div   style="margin-top:5px;margin-left:20px;padding-top:5px " >
                                        <asp:CheckBox ID="chkbooknow" CssClass="side-block jq-checkbox-tour"  runat="server" />
                                        </div> </div>
                                     </div>
                              </div>
                                
                                
                                    

                                    </div>
                             
                                  </div>
                        </div>
                            
                        <%--  </div>--%>
                               <div  style ="display:none" runat="server" id="divfields">
                                 <%--   <asp:Label ID="lblmaxadult" runat="server" Text='<%# Eval("maxadults") %>' ></asp:Label>
                                        <asp:Label ID="lblmaxchild" runat="server" Text='<%# Eval("maxchild") %>' ></asp:Label>
                                        <asp:Label ID="lblshuttle" runat="server" Text='<%# Eval("shuttle") %>' ></asp:Label>
                                        <asp:Label ID="lblpaxcheck" runat="server" Text='<%# Eval("paxcheckreqd") %>' ></asp:Label>
                                        <asp:Label ID="lblairportborder" runat="server" Text='<%# Eval("airportbordercode") %>' ></asp:Label>
                                        <asp:Label ID="lblfromsector" runat="server" Text='<%# Eval("fromsectorgroupcode") %>' ></asp:Label>
                                        <asp:Label ID="lblsectorgroupcode" runat="server" Text='<%# Eval("sectorgroupcode") %>' ></asp:Label>
                                        <asp:Label ID="lbltransferdate" runat="server" Text='<%# Eval("transferdate") %>' ></asp:Label>--%>

                                          <asp:Label ID="lbladdlpax" runat="server" Text='<%# Eval("addlpax") %>'></asp:Label>
                                   <asp:Label ID="lbladultprice" runat="server" Text='<%# Eval("adultprice") %>'></asp:Label>
                                   <asp:Label ID="lblchildprice" runat="server" Text='<%# Eval("childprice") %>'></asp:Label>
                                   <asp:Label ID="lbladultsalevalue" runat="server" Text='<%# Eval("adultsalevalue") %>'></asp:Label>
                                   <asp:Label ID="lblchildsalevalue" runat="server" Text='<%# Eval("childsalevalue") %>'></asp:Label>
                                   <asp:Label ID="lbladdlpaxsalevalue" runat="server" Text='<%# Eval("addlpaxsalevalue") %>'></asp:Label>
                                   <asp:Label ID="lbladdlpaxprice" runat="server" Text='<%# Eval("addlpaxprice") %>'></asp:Label>
                                   <asp:Label ID="lblairportmadate" runat="server" Text='<%# Eval("airportmadate") %>'></asp:Label>
                                   <asp:Label ID="lblmaxpax1" runat="server" Text='<%# Eval("maxpax") %>'></asp:Label>
                                   <asp:Label ID="lblratebasis" runat="server" Text='<%# Eval("ratebasis") %>'></asp:Label>
                                   <asp:Label ID="lblminpax1" runat="server" Text='<%# Eval("minpax") %>'></asp:Label>

                                        <asp:Label ID="lbladults" runat="server" Text='<%# Eval("adults") %>' ></asp:Label>
                                        <asp:Label ID="lblchild" runat="server" Text='<%# Eval("child") %>' ></asp:Label>
                                        <asp:Label ID="lblchildagestring" runat="server" Text='<%# Eval("childagestring") %>' ></asp:Label>
                                        <asp:Label ID="lblMAunit" runat="server" Text='<%# Eval("units") %>' ></asp:Label>
                                        <asp:Label ID="lblprice" runat="server" Text='<%# Eval("unitprice") %>' ></asp:Label>
                                        <asp:Label ID="lblvalue" runat="server" Text='<%# Eval("unitsalevalue") %>' ></asp:Label>
                                         <asp:Label ID="lbladultplistcode" runat="server" Text='<%# Eval("adultplistcode") %>' ></asp:Label>
                                        <asp:Label ID="lblchildplistcode" runat="server" Text='<%# Eval("childplistcode") %>' ></asp:Label>
                                        <asp:Label ID="lblunitplistcode" runat="server" Text='<%# Eval("unitplistcode") %>' ></asp:Label>
                                        <asp:Label ID="lbladdlpaxplistcode" runat="server" Text='<%# Eval("addlpaxplistcode") %>' ></asp:Label>
                                         <asp:HiddenField ID="hdnselected" Value='<%# Eval("currentselection") %>' runat="server" />

                                          <asp:Label ID="lblpreferredsupplier" runat="server" Text='<%# Eval("preferredsupplier") %>' ></asp:Label>
                                           <asp:Label ID="lbladultcprice" runat="server" Text='<%# Eval("adultcprice") %>' ></asp:Label>
                                            <asp:Label ID="lblchildcprice" runat="server" Text='<%# Eval("childcprice") %>' ></asp:Label>
                                             <asp:Label ID="lblunitcprice" runat="server" Text='<%# Eval("unitcprice") %>' ></asp:Label>
                                              <asp:Label ID="lbladdlpaxcprice" runat="server" Text='<%# Eval("addlpaxcprice") %>' ></asp:Label>
                                               <asp:Label ID="lbladultcostvalue" runat="server" Text='<%# Eval("adultcostvalue") %>' ></asp:Label> 
                                               <asp:Label ID="lblchildcostvalue" runat="server" Text='<%# Eval("childcostvalue") %>' ></asp:Label>
                                                <asp:Label ID="lblunitcostvalue" runat="server" Text='<%# Eval("unitcostvalue") %>' ></asp:Label>
                                                 <asp:Label ID="lbladdlpaxcostvalue" runat="server" Text='<%# Eval("addlpaxcostvalue") %>' ></asp:Label>
                                                  <asp:Label ID="lbltotalcostvalue" runat="server" Text='<%# Eval("totalcostvalue") %>' ></asp:Label>
                                                   <asp:Label ID="lbladultcplistcode" runat="server" Text='<%# Eval("adultcplistcode") %>' ></asp:Label>
                                                    <asp:Label ID="lblchildcplistcode" runat="server" Text='<%# Eval("childcplistcode") %>' ></asp:Label>
                                                     <asp:Label ID="lblunitcplistcode" runat="server" Text='<%# Eval("unitcplistcode") %>' ></asp:Label>
                                                     <asp:Label ID="lbladdlpaxcplistcode" runat="server" Text='<%# Eval("addlpaxcplistcode") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladultprice" runat="server" Text='<%# Eval("wladultprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlchildprice" runat="server" Text='<%# Eval("wlchildprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlunitprice" runat="server" Text='<%# Eval("wlunitprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladdlpaxprice" runat="server" Text='<%# Eval("wladdlpaxprice") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladultsalevalue" runat="server" Text='<%# Eval("wladultsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlchildsalevalue" runat="server" Text='<%# Eval("wlchildsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlunitsalevalue" runat="server" Text='<%# Eval("wlunitsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwladdlpaxsalevalue" runat="server" Text='<%# Eval("wladdlpaxsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwltotalsalevalue" runat="server" Text='<%# Eval("wltotalsalevalue") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlcurrcode" runat="server" Text='<%# Eval("wlcurrcode") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlconvrate" runat="server" Text='<%# Eval("wlconvrate") %>' ></asp:Label>
                                                     <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>' ></asp:Label>
                             
                                    </div> 
                          
                       <%-- </div>--%>
                        
                      
                       
                          
                      
                    </ItemTemplate>
                     <FooterTemplate>
                     <div style="float: right;margin-top:-25px;background-color:White;padding:5px">
                            <asp:LinkButton ID="lbTransitShowMore" CssClass="rate-plan-headings" OnClick="lbTransitShowMore_Click"
                                runat="server">Show More</asp:LinkButton></div>
                    </FooterTemplate>
                </asp:DataList>
               <!-- / Transit / -->    
              
            </div>
              <asp:ModalPopupExtender ID="mpTotalprice" runat="server" BackgroundCssClass="roomtype-modalBackground"
                  CancelControlID="atotalPriceClose" EnableViewState="true" PopupControlID="pnlTotalPrice"
                  TargetControlID="hdpricePopup">
              </asp:ModalPopupExtender>
                                                          <asp:HiddenField ID="hdpricePopup" runat="server" />
              <asp:Panel runat="server" ID="pnlTotalPrice" style="display:none;">
                  <div class="roomtype-price-breakuppopup">
                      <div id="Div5">
                          <div class="roomtype-popup-title">
                              <asp:Label ID="lblTotlaPriceHeading" runat="server"></asp:Label>
                              <a id="atotalPriceClose" href="#" class="roomtype-popup-close"></a>
                          </div>
                          <div class="roomtype-popup-description">
                              <div id="dvPriceBreakupSave" runat="server" style="padding-left: 10px; margin-bottom: 25px;">
                                  <div id="dvComplimentaryToCustomer" class="side-block jq-checkbox-tour" runat="server"
                                      style="padding-left: 10px; float: left; width: 70%;">
                                      <asp:CheckBox ID="chkComplimentaryToCustomer" CssClass="roomtype-popup-label" runat="server"
                                          Text="Complementary to Customer" />
                                  </div>
                                  <asp:HiddenField ID="hdAirportTypeCodePopup" runat="server" />
                                  <asp:HiddenField ID="hdTypePopup" runat="server" />
                                  <asp:HiddenField ID="hdlineno" runat="server" />
                                  <asp:HiddenField ID="hdRateBasisPopup" runat="server" />
                                  <asp:HiddenField ID="hdCurrCodePopup" runat="server" />
                                  <div style="padding-left: 300px; width: 30%;">
                                      <asp:Button ID="btnPriceBreakupSave" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px;"
                                          runat="server" Text="Save" />
                                  </div>
                              </div>
                              <div style="overflow: auto; min-height: 129px; max-height: 420px; min-width: 350px;
                                  max-width: 450px; padding-bottom: 10px; margin-top: 10px;">
                                  <div style="border: 0px solid #ede7e1; max-width: 450px; min-width: 150px; padding-left: 25px;
                                      padding-top: 5px; padding-bottom: 10px;">
                                      <div style="width: 100%">
                                          <div id="dvACS" runat="server">
                                              <div class="search-large-i tour-change-date-label">
                                                  <label>
                                                      No Of Adults</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtNoOfAdult"  onkeypress="validateDecimalOnly(event,this)"   runat="server"></asp:TextBox></div>
                                              </div>
                                              <div class="search-large-i tour-change-date-label">
                                                  <label>
                                                      Adult Price</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtAdultPrice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                       <asp:TextBox ID="txtwlAdultPrice"  onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: left; width: 27%;">
                                                  <label>
                                                      Adult Sale Value</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtAdultSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                         <asp:TextBox ID="txtwlAdultSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                      </div>
                                              </div>
                                              <div class="clear">
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: left;">
                                                  <label>
                                                      No Of Child</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtNoOfchild"  onkeypress="validateDecimalOnly(event,this)"  runat="server"></asp:TextBox></div>
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: left;">
                                                  <label>
                                                      Child Price</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtChildprice"  onkeypress="validateDecimalOnly(event,this)"  runat="server"></asp:TextBox>
                                                       <asp:TextBox ID="txtwlChildprice"   onkeydown="fnReadOnly(event)"  runat="server"></asp:TextBox>
                                                      </div>
                                                  <div class="clear">
                                                  </div>
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: left; width: 27%;
                                                  margin-left: 15px;">
                                                  <label>
                                                      Child Sale Value</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtchildSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                      <asp:TextBox ID="txtwlchildSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                      </div>
                                              </div>
                                             
                                              <div class="clear">
                                              </div>
                                          </div>
                                          <div id="dvUnits" runat="server">
                                              <div class="search-large-i tour-change-date-label" style="float: left;" >
                                                  <label>
                                                      No Of Units</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtNoOfUnits"  onkeypress="validateDecimalOnly(event,this)"  runat="server"></asp:TextBox></div>
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: left;" id="dvnoUnits" runat="server">
                                                  <label>
                                                      Unit Price</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtUnitPrice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                       <asp:TextBox ID="txtwlUnitPrice"  onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: left;" id="dvsalevalue" runat="server">
                                                  <label>
                                                      Unit Sale Value</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtUnitSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtwlUnitSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                      </div>
                                              </div>
                                              <div class="clear">
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: left;" id="dvaddnopax" runat="server">
                                                  <label>
                                                      Additional Pax</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtAdditionalPax"  onkeypress="validateDecimalOnly(event,this)"  runat="server"></asp:TextBox>
                                                      <asp:TextBox ID="txtwlAdditionalPax"  onkeypress="validateDecimalOnly(event,this)"  runat="server"></asp:TextBox>
                                                      </div>
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: left;width:30%" id="dvaddpax" runat="server" >
                                                  <label>
                                                      Additional Pax Price</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtAdditionalPaxPrice" onkeypress="validateDecimalOnly(event,this)"
                                                          runat="server"></asp:TextBox>
                                                           <asp:TextBox ID="txtwlAdditionalPaxPrice"  onkeydown="fnReadOnly(event)"
                                                          runat="server"></asp:TextBox>
                                                  </div>
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: right;" id="dvaddpaxvalue" runat="server">
                                                  <label>
                                                      Additional Pax Value</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtAdditionalPaxValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                       <asp:TextBox ID="txtwlAdditionalPaxValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                      </div>
                                              </div>
                                          </div>
                                           <div class="clear">
                                              </div>
                                              <div class="search-large-i tour-change-date-label" style="float: left;" id="dvtotalsalevalue" runat="server">
                                                  <label>
                                                      Total Sale Value</label>
                                                  <div class="input-a">
                                                      <asp:TextBox ID="txtTotalSaleVale" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                      <asp:TextBox ID="txtwlTotalSaleVale" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                      </div>
                                              </div>
                                      </div>
                                  </div>
                              </div>
                          </div>
                      </div>
                  </div>
              </asp:Panel>
                <asp:Button ID="btnSelectedArrival" runat="server" Style="display: none;" Text="Filter" />
                <asp:Button ID="btnSelectedDeparture" runat="server" Style="display: none;" Text="Filter" />
                <asp:Button ID="btnSelectedTransit" runat="server" Style="display: none;" Text="Filter" />
               <%--        <asp:HiddenField ID="hdRMPartyCode" runat="server" />
                         <asp:HiddenField ID="hdRMRatePlanId" runat="server" />
                           <asp:HiddenField ID="hdRMRoomTypeCode" runat="server" />
                             <asp:HiddenField ID="hdRMMealPlanCode" runat="server" />
                               <asp:HiddenField ID="hdRMcatCode" runat="server" />
                                 <asp:HiddenField ID="hdRMAccCode" runat="server" />
                                  <asp:HiddenField ID="hdRoomTypegrid" runat="server" />
                                        <asp:HiddenField ID="hdRoomTypegridRowId" runat="server" />
                                        <asp:HiddenField ID="hdRoomTypeCurrCode" runat="server" />

                   </div>
               </div>
            </div>
            </div> </asp:Panel>--%>
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
           var hdcurcode = $("#hdSliderCurrency").val();
          
           var vmin = hdPriceMinRange.val()
           var max = hdPriceMaxRange.val()
          
           var vminSelected = hdPriceMin.val()
           var maxSelected = hdPriceMax.val()

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
                       ammount_from.val(ui.values[0] + hdcurcode);
                       ammount_to.val(ui.values[1] + hdcurcode);
                       hdPriceMin.val(ui.values[0])
                       hdPriceMax.val(ui.values[1])

                   }

               });

               ammount_from.val(slider_range.slider("values", 0) + hdcurcode);
               ammount_to.val(slider_range.slider("values", 1) + hdcurcode);
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
                          <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
  <asp:HiddenField ID="hdPriceMin" runat="server" />
    <asp:HiddenField ID="hdPriceMax" runat="server" />
      <asp:HiddenField ID="hdPriceMinRange" runat="server" />
    <asp:HiddenField ID="hdPriceMaxRange" runat="server" />
  <asp:HiddenField ID="hdLoginType" runat="server" />
   <asp:HiddenField ID="hdBookingEngineRateType" runat="server" />
     <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
     <asp:HiddenField ID="hdWhiteLabel" runat="server" />
     <asp:HiddenField ID="hdSliderCurrency" runat="server" />
    </form>
</body>
</html>
