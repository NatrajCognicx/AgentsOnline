<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GuestPagenew.aspx.vb" Inherits="GuestPagenew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

      <meta name="description" content="" />
  <meta name="keywords" content="" />
  <meta charset="utf-8" /><link rel="icon" href="favicon.png" />
  <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <link rel="stylesheet" href="css/jquery.formstyler.css">
    <link rel="stylesheet" href="css/jquery-ui.css">
    <link rel="stylesheet" href="css/style.css" />
    <link id="Link1" rel='stylesheet' type='text/css' href='css/style-common.css' />

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

        <%--***Danny 18/08/2018 fa fa-star--%>
        <link href="css/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
<%--  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">--%>
    <link id="lnkCSS" rel='stylesheet' type='text/css' href='css/style-common.css' />

    <link href="css/ValidationEngine.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/AgentsOnlineStyles.css" />
    <link rel="stylesheet" media="all" type="text/css" href="css/jquery-ui-timepicker-addon.css" />
    <!-- // scripts // -->
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="js/jqeury.appear.js"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script src="js/owl.carousel.min.js"></script>
    <script src="js/bxSlider.js"></script>
    <script src="js/jquery.formstyler.js"></script>
    <script src="js/custom.select.js"></script>
    <script type="text/javascript" src="js/twitterfeed.js"></script>
    <script type="text/javascript" src="js/jquery.validationEngine-en.js" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.validationEngine.js" charset="utf-8"></script>
    <script src="js/script.js"></script>
    <link rel="stylesheet" type="text/css" href="css/dialog_box.css" />
    <script type="text/javascript" src="js/dialog_box.js"></script>
    <script type="text/javascript" src="js/jquery-ui-timepicker-addon.js"></script>

       <script  type="text/javascript"  src="js/jquery.collapsiblepanel.js"></script>
     <style type="text/css">
         
         
              
     div.blueTable {
  border: 1px solid #dddd;
  background-color: #FFFFFF;
  width: 102%;
  text-align: left;
  border-collapse: collapse;
}
.divTable.blueTable .divTableCell, .divTable.blueTable .divTableHead {
  border: 1px solid #dddd;
  padding: 5px 5px;
  color:#828282;
  text-transform:uppercase;
  font-family:Raleway;
  font-weight:500;

}
.divTable.blueTable .divTableBody .divTableCell {
  font-size: 13px;
}
.divTable.blueTable .divTableHeading {
  background: #F2F2F2;
  background: -moz-linear-gradient(top, #f5f5f5 0%, #f3f3f3 66%, #F2F2F2 100%);
  background: -webkit-linear-gradient(top, #f5f5f5 0%, #f3f3f3 66%, #F2F2F2 100%);
  background: linear-gradient(to bottom, #f5f5f5 0%, #f3f3f3 66%, #F2F2F2 100%);
}
.divTable.blueTable .divTableHeading .divTableHead {
  font-size: 15px;
  font-weight: bold;
  color: #FF9947;
}
.blueTable .tableFootStyle {
  font-size: 14px;
}
.blueTable .tableFootStyle .links {
	 text-align: right;
}
.blueTable .tableFootStyle .links a{
  display: inline-block;
  background: #1C6EA4;
  color: #FFFFFF;
  padding: 2px 8px;
  border-radius: 5px;
}
.blueTable.outerTableFooter {
  border-top: none;
}
.blueTable.outerTableFooter .tableFootStyle {
  padding: 3px 5px; 
}
/* DivTable.com */
.divTable{ display: table; }
.divTableRow { display: table-row; }
.divTableHeading { display: table-header-group;}
.divTableCell, .divTableHead { display: table-cell;}
.divTableHeading { display: table-header-group;}
.divTableFoot { display: table-footer-group;}
.divTableBody { display: table-row-group;}

  .mygrid
{
	width: 100%;
	 border: 1px solid #EDE7E1;
	font-family: Raleway !important;
	
	color:#455051;

}
.mygrid-header
{
	background-color: #EDE7E1;
	font-family: Raleway !important;
	color: #455051;
	height: 25px;
	text-align: left;
	font-size: 11px !important;
	font-size:small;
	font-style:normal !important;
	padding: 5px 5px 5px 5px;
}
.totalValue-header
{

	font-family: Raleway !important;
	color: #455051  !important;
	text-align: left  !important;
	font-size: 16px  !important;
    font-size:small !important;

}
.mygrid-rows
{
 
	font-family: Raleway !important;
	font-size: 12px !important;
	color: #455051;
	min-height: 10px;
	text-align: left;
	border: 1px solid #EDE7E1;
	vertical-align:middle;
	
}
.mygrid-rows:hover
{
	background-color: #fff;
	font-family: Raleway;
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
	font-family: Raleway;
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
      <script type="text/javascript">
          $(document).ready(function () {

              $('.collapse').collapsiblePanel({
                  toggle: false
              });
          });

          //added saurabh on 18/10/2020
          function CheckAmount() {
              var ddlPaymentType = document.getElementById('<%=ddlPaymentType.ClientID%>').selectedIndex;
              debugger;
              if (ddlPaymentType == 1) {

                  var actualamount = document.getElementById('hdnPayAmount').value;
                  var amount = document.getElementById('PaymentAmount').value;
                  if (amount > actualamount) {
                      alert("amount should be less than or equal to actual amount");
                      return false;
                  }
              }
              return true;
          }
    </script>
      <script language="javascript" type="text/javascript">
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
  <script>
      $(document).ready(function () {
          $(document).ready(function () {
              $('.time-picker').timepicker();
              fillDates();

          });

          'use strict';
          (function ($) {
              $(function () {

                  $('.checkbox input').styler({
                      selectSearch: true
                  });

                  $('.checkboxguest input').styler({
                      selectSearch: true
                  });


              });
          })(jQuery);





      });
  </script>
   

<!-- \\ scripts \\ --> 

   <script language="javascript" type="text/javascript">

       function fndvColRef(chk) {
           var chk1 = document.getElementById("chkColRef");
           if (chk1.checked == true) {
             var dvColRef= document.getElementById("dvColRef");
             dvColRef.style.display = "block";
           }
         else {
             var dvColRef = document.getElementById("dvColRef");
             dvColRef.removeAttribute('display');
             dvColRef.style.display = "none";
           }
       }


       function btnTabShowAllClick(btn) {

           var hdTabHotelTotalPrice = document.getElementById("hdTabHotelTotalPrice");
           var hdTourTabTotalPrice = document.getElementById("hdTourTabTotalPrice");
           var hdTransferTabTotalPrice = document.getElementById("hdTransferTabTotalPrice");
           var hdVisaTabTotalPrice = document.getElementById("hdVisaTabTotalPrice");
           var hdAirportTabtotalPrice = document.getElementById("hdAirportTabtotalPrice");
           var hdOtherServiceTabTotalPrice = document.getElementById("hdOtherServiceTabTotalPrice");

           var btnTabShowAll = document.getElementById("btnTabShowAll");
           if (btn.value == 'Show All') {

               if (hdTabHotelTotalPrice.value == '1') {
                   var dvHotelTab1 = document.getElementById("dvHotelTab1");
                   dvHotelTab1.style.display = "block";
               }
               if (hdTourTabTotalPrice.value == '1') {
                   var dvTourTab1 = document.getElementById("dvTourTab1");
                   dvTourTab1.style.display = "block";
               }
               if (hdTransferTabTotalPrice.value == '1') {
                   var dvTransferTab1 = document.getElementById("dvTransferTab1");
                   dvTransferTab1.style.display = "block";
               }
               if (hdVisaTabTotalPrice.value == '1') {
                   var dvVisaTab1 = document.getElementById("dvVisaTab1");
                   dvVisaTab1.style.display = "block";
               }
               if (hdAirportTabtotalPrice.value == '1') {
                   var dvAirportTab1 = document.getElementById("dvAirportTab1");
                   dvAirportTab1.style.display = "block";
               }
               if (hdOtherServiceTabTotalPrice.value == '1') {
                   var dvOtherTab1 = document.getElementById("dvOtherTab1");
                   dvOtherTab1.style.display = "block";
               }

               btnTabShowAll.value = "Hide All";

           }
           else {



               if (hdTabHotelTotalPrice.value == '1') {
                   var dvHotelTab1 = document.getElementById("dvHotelTab1");
                   dvHotelTab1.removeAttribute('display');
                   dvHotelTab1.style.display = "none";
               }
               if (hdTourTabTotalPrice.value == '1') {
                   var dvTourTab1 = document.getElementById("dvTourTab1");
                   dvTourTab1.removeAttribute('display');
                   dvTourTab1.style.display = "none";
               }
               if (hdTransferTabTotalPrice.value == '1') {
                   var dvTransferTab1 = document.getElementById("dvTransferTab1");
                   dvTransferTab1.style.display = "none";
               }
               if (hdVisaTabTotalPrice.value == '1') {
                   var dvVisaTab1 = document.getElementById("dvVisaTab1");
                   dvVisaTab1.style.display = "none";
               }
               if (hdAirportTabtotalPrice.value == '1') {
                   var dvAirportTab1 = document.getElementById("dvAirportTab1");
                   dvAirportTab1.style.display = "none";
               }
               if (hdOtherServiceTabTotalPrice.value == '1') {
                   var dvOtherTab1 = document.getElementById("dvOtherTab1");
                   dvOtherTab1.style.display = "none";
               }
               btnTabShowAll.value = "Show All";

           }

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
       function showreason(chk, divreason, arrflight, arrtime, arrairportfrom, airportto) {


           var chk1 = document.getElementById(chk);
           var divreason1 = document.getElementById(divreason);
           var arrflight1 = document.getElementById(arrflight);
           var arrtime1 = document.getElementById(arrtime);
           var arrairportfrom1 = document.getElementById(arrairportfrom);
           var airportto1 = document.getElementById(airportto);

           if (chk1.checked == true) {

               divreason1.style.display = "block";
               arrflight1.setAttribute("disabled", false);
               arrtime1.setAttribute("disabled", false);
               arrairportfrom1.setAttribute("disabled", false);
               airportto1.setAttribute("disabled", false);

           }
           else {
               divreason1.style.display = "none";
               arrflight1.removeAttribute('disabled');
               arrtime1.removeAttribute('disabled');
               arrairportfrom1.removeAttribute('disabled');
               airportto1.removeAttribute('disabled');

           }

       }

       function showdivreason(chk, divreason, depflight, deptime, depairportfrom, airportto) {


           var chk1 = document.getElementById(chk);
           var divreason1 = document.getElementById(divreason);
           var depflight1 = document.getElementById(depflight);
           var deptime1 = document.getElementById(deptime);
           var depairportfrom1 = document.getElementById(depairportfrom);
           var airportto1 = document.getElementById(airportto);

           if (chk1.checked == true) {

               divreason1.style.display = "block";
               depflight1.setAttribute("disabled", false);
               deptime1.setAttribute("disabled", false);
               depairportfrom1.setAttribute("disabled", false);
               airportto1.setAttribute("disabled", false);

           }
           else {
               divreason1.style.display = "none";
               depflight1.removeAttribute('disabled');
               deptime1.removeAttribute('disabled');
               depairportfrom1.removeAttribute('disabled');
               airportto1.removeAttribute('disabled');

           }

       }

       function ChkApplyConfirm(chk) {

           document.getElementById("<%= btnApplySameConfChk.ClientID %>").click();

       }

       function mpConfirmhide() {
           var mpConfirm = $find("mpConfirm");

           mpConfirm.hide();
           return true;
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

       function ArrivalflightAutocompleteSelected(source, eventArgs) {
           if (source) {
               // Get the HiddenField ID.
               var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalflightCode");
               var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalTime");
               var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivaltoairport");
               var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrBordecode");
               var hiddenfieldIDFromAirport = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalAirport");


               $get(hiddenfieldID).value = eventArgs.get_value();

               GetAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode, hiddenfieldIDFromAirport);
           }

       }


       function MADeparturepickupAutocompleteSelected(source, eventArgs) {
           if (source) {
               //
               if (eventArgs != null) {
                   var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMADeparturepickup", "txtDepBordecode");
                   $get(hiddenfieldID).value = eventArgs.get_value();

               }
               else {
                   var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMADeparturepickup", "txtDepBordecode");
                   $get(hiddenfieldID).value = '';
               }
           }

       }


       function MAArrivalpickupAutocompleteSelected(source, eventArgs) {
           if (source) {
               //
               if (eventArgs != null) {
                   var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMAArrivalpickup", "txtArrBordecode");
                   $get(hiddenfieldID).value = eventArgs.get_value();

               }
               else {
                   var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMAArrivalpickup", "txtArrBordecode");
                   $get(hiddenfieldID).value = '';
               }
           }

       }


       function GetAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode, hiddenfieldIDFromAirport) {
           $.ajax({
               type: "POST",
               url: "GuestPagenew.aspx/GetAirportAndTimeDetails",
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
                       $get(hiddenfieldIDFromAirport).value = $(this).find("airport").text();
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


       function Validate() {

           if (confirm("Are you sure you want to Abandon?") == true) {

               return true;

           }
           else {
               return false;
           }
       }

       function DepartureAutocompleteSelected(source, eventArgs) {
           if (source) {


               var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_DepartureFlight", "txtDepartureFlightCode");
               var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_DepartureFlight", "txtDepartureTime");
               var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_DepartureFlight", "txtDepartureAirport");
               var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_DepartureFlight", "txtDepBordecode");
               var hiddenfieldIDFromAirport = source.get_id().replace("AutoCompleteExtender_DepartureFlight", "txtDeparturetoAirport");

               $get(hiddenfieldID).value = eventArgs.get_value();
               GetDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode, hiddenfieldIDFromAirport);

           }
       }

       function GetDepartureAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode, hiddenfieldIDFromAirport) {
           $.ajax({
               type: "POST",
               url: "GuestPagenew.aspx/GetDepartureAirportAndTimeDetails",
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
                       $get(hiddenfieldIDFromAirport).value = $(this).find("airport").text();
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


       function NationalityAutocompleteSelected(source, eventArgs) {
           var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtNationality", "txtNationalityCode");
           var hiddenfieldName = source.get_id().replace("AutoCompleteExtender_txtNationality", "txtNationality");
           //           var ddlVisa = source.get_id().replace("AutoCompleteExtender_txtNationality", "ddlVisa");
           //           var ddlVisaType = source.get_id().replace("AutoCompleteExtender_txtNationality", "ddlVisatype");
           //           var txtPrice = source.get_id().replace("AutoCompleteExtender_txtNationality", "txtVisaPrice");
           if (hiddenfieldName != '') {
               $get(hiddenfieldID).value = eventArgs.get_value();

               //    SelectVisa($get(hiddenfieldID).value, $get(ddlVisa), $get(ddlVisaType))
           }
           else {
               $get(hiddenfieldID).value = '';
           }

       }


       function Guestchange(firstname, middlename, lastname, title, nationality, divflight, rowid) {

           var txtfirstname = document.getElementById(firstname);
           var txtmiddlename = document.getElementById(middlename);
           var txtlastname = document.getElementById(lastname);
           var txtnationality = document.getElementById(nationality);
           var ddltitle = document.getElementById(title);

           var divflightservice = document.getElementById(divflight);
           var divflightserviceid = '#' + divflight;
           var hdnguest = document.getElementById('<%=hdnguestchange.ClientID%>');
           var hdnguestsaved = document.getElementById('<%=hdnguestsaved.ClientID%>');

           //   $(document).find(divflightserviceid).fadeOut();

           document.getElementById(divflight).style.display = "none";

           hdnguestsaved.value = '0';
           hdnguest.value = '1';

       }


       function showhideservices(btnshow1, divservice1, rowid1) {
  
           var btnshow = document.getElementById(btnshow1);
           var divservice = document.getElementById(divservice1);
           var divserviceid = '#' + divservice1;


           if (btnshow.text == "Edit-Services") {
               $(document).find(divserviceid).fadeIn();

               btnshow.text = 'Hide Services';
               btnshow.style.color = "#fff";
           }
           else {
               $(document).find(divserviceid).fadeOut();
               btnshow.text = 'Edit-Services';
               btnshow.style.color = "#fff";
           }
           return false;
       }


       function ShowhideGuestname(chkguest1, divguestnames1, rowid1) {

           var chkguest = document.getElementById(chkguest1);
           var divguestnames = document.getElementById(divguestnames1);
           var divguestnames = '#' + divguestnames1;
           if (chkguest.checked) {
               $(document).find(divguestnames).fadeOut();



           }
           else {

               $(document).find(divguestnames).fadeIn();
           }
           return false;
       }


      

      



      </script>


      <script type="text/javascript">
          function RefreshContent() {
              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

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

          function ShowBookingError() {
              var mpBookingError1 = $find("mpBookingError");
              mpBookingError1.show();
              return true;
          }
          function HideBookingError() {
              var mpBookingError1 = $find("mpBookingError");
              if (mpBookingError1 != null) {
                  mpBookingError1.hide(500);
              }
              return true;
          }

          function EndRequestHandler() {


              $('.collapse').collapsiblePanel({
                  toggle: false
              });

              var date = new Date();
              var currentMonth = date.getMonth();
              var currentDate = date.getDate();
              var currentYear = date.getFullYear();
              $(".date-inpt-check-in").datepicker({

                  minDate: new Date(currentYear, currentMonth, currentDate)

              });



              var isSafari = /constructor/i.test(window.HTMLElement) || (function (p) { return p.toString() === "[object SafariRemoteNotification]"; })(!window['safari'] || safari.pushNotification);
              if (isSafari == false) {
                  $('.checkbox input').styler({
                      selectSearch: true
                  });

                  $('.checkboxguest input').styler({
                      selectSearch: true
                  });

              }
              $('.time-picker').timepicker();
              fillDates();
              HideProgess();
          }

          function fillDates() {

              var dCheckInMin = document.getElementById('<%=hdCheckInPrevDay.ClientID%>').value;
              var dp1 = dCheckInMin.split("/");
              var date1 = new Date(dp1[2], dp1[1], dp1[0]);
              var currentMonth1 = date1.getMonth() - 1;
              var currentDate1 = date1.getDate();
              var currentYear1 = date1.getFullYear();

              var dCheckInMax = document.getElementById('<%=hdCheckInNextDay.ClientID%>').value;
              var dp2 = dCheckInMax.split("/");
              var date2 = new Date(dp2[2], dp2[1], dp2[0]);
              var currentMonth2 = date2.getMonth() - 1;
              var currentDate2 = date2.getDate();
              var currentYear2 = date2.getFullYear();

              $(".date-inpt-check-in-guest-arrival").datepicker({
                  minDate: new Date(currentYear1, currentMonth1, currentDate1),
                  maxDate: new Date(currentYear2, currentMonth2, currentDate2)
              });



              var dCheckOutMin = document.getElementById('<%=hdCheckOutPrevDay.ClientID%>').value;
              var dp3 = dCheckOutMin.split("/");
              var date3 = new Date(dp3[2], dp3[1], dp3[0]);
              var currentMonth3 = date3.getMonth() - 1;
              var currentDate3 = date3.getDate();
              var currentYear3 = date3.getFullYear();

              var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
              var dp4 = dCheckOutMax.split("/");
              var date4 = new Date(dp4[2], dp4[1], dp4[0]);
              var currentMonth4 = date4.getMonth() - 1;
              var currentDate4 = date4.getDate();
              var currentYear4 = date4.getFullYear();

              $(".date-inpt-check-in-guest-departure").datepicker({
                  minDate: new Date(currentYear3, currentMonth3, currentDate3),
                  maxDate: new Date(currentYear4, currentMonth4, currentDate4)
              });
          }


          function ChangeArrivalDate(ArrivalDate, ext) {
              $find(ext).set_contextKey($get(ArrivalDate).value);
          }

          function ChangeDepartureDate(DepartDate, ext) {
              $find(ext).set_contextKey($get(DepartDate).value);
          }

          function ChangeDate(txtDays, txtTimeLimit) {

              var txt = document.getElementById('<%=lblCheckInDate.ClientID%>');

              var ddl = document.getElementById(txtDays);

              var txt1 = document.getElementById(txtTimeLimit);
              var txttodate = document.getElementById('<%=txttodaydate.ClientID %>');

              if (txt.innerText != '') {

                  var datearray = txt.innerText.split("/");

                  var mn = datearray[1];
                  mn = mn * 1;
                  mn = mn - 1;
                  var yDate = new Date(datearray[2], mn, datearray[0]);

                  var todt = DateAdd('d', (ddl.value * -1), yDate);

                  var nyr = todt.getFullYear();

                  var nmn = todt.getMonth();
                  nmn = nmn * 1;
                  nmn = nmn + 1;
                  nmn = Right('0' + nmn, 2);
                  var ndy = Right('0' + todt.getDate(), 2);
                  txt1.value = ndy + "/" + nmn + "/" + nyr;



              }
          }
          function DateAdd(timeU, byMany, dateObj) {
              var millisecond = 1;
              var second = millisecond * 1000;
              var minute = second * 60;
              var hour = minute * 60;
              var day = hour * 24;
              var year = day * 365;

              var newDate;
              var dVal = dateObj.valueOf();
              switch (timeU) {
                  case "ms": newDate = new Date(dVal + millisecond * byMany); break;
                  case "s": newDate = new Date(dVal + second * byMany); break;
                  case "mi": newDate = new Date(dVal + minute * byMany); break;
                  case "h": newDate = new Date(dVal + hour * byMany); break;
                  case "d": newDate = new Date(dVal + day * byMany); break;
                  case "y": newDate = new Date(dVal + year * byMany); break;
              }

              return newDate;

          }

          function Right(str, n) {
              if (n <= 0)
                  return "";
              else if (n > String(str).length)
                  return str;
              else {
                  var iLen = String(str).length;
                  return String(str).substring(iLen, iLen - n);
              }
          }


          //changed by mohamed on 15/09/2018
          function txtFirstName_onclientitemselected(source, eventArgs) {

          }

          //changed by mohamed on 15/09/2018
          function txtFirstName_onclientpopulating(sender, args) {
              var lsList = '';
              $("#dlPersonalInfo tr").each(function (e) {

                  var textBox = $(this).find(".txtFirstNameClass");
                  if (textBox.val() != 'undefined' && textBox.val() != null) {
                      lsList = lsList + textBox.val() + ',';
                  }
              });

              sender.set_contextKey(lsList);
          }

          //changed by mohamed on 15/09/2018
          function txtMiddleName_onclientpopulating(sender, args) {
              var lsList = '';
              $("#dlPersonalInfo tr").each(function (e) {

                  var textBox = $(this).find(".txtMiddleNameClass");
                  if (textBox.val() != 'undefined' && textBox.val() != null) {
                      lsList = lsList + textBox.val() + ',';
                  }
              });

              sender.set_contextKey(lsList);
          }

          //changed by mohamed on 15/09/2018
          function txtLastName_onclientpopulating(sender, args) {
              var lsList = '';
              $("#dlPersonalInfo tr").each(function (e) {

                  var textBox = $(this).find(".txtLastNameClass");
                  if (textBox.val() != 'undefined' && textBox.val() != null) {
                      lsList = lsList + textBox.val() + ',';
                  }
              });

              sender.set_contextKey(lsList);
          }

          function redirectItinerary(urlName, strId) {
              window.open(urlName + "?Id=" + strId);
          } 
          
</script>



                   
</head>
<body  onload="RefreshContent()">
    <form id="form1" runat="server">
    <div>
    <!-- // authorize // -->
	<div class="overlay"></div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true" AsyncPostBackTimeout="600">
    </asp:ScriptManager>


    <header id="top">
    <asp:UpdatePanel ID="UpdatePanel7" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
	<div class="header-a">
		<div class="wrapper-padding">			
         <div class="header-user"  style="margin-top:2px;"><asp:Label ID="lblHeaderUserName" runat="server" ></asp:Label> </div>		
						  
			<div class="header-phone" id="dvlblHeaderAgentName" runat="server"  style="margin-top:2px;">
            <asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-agentname" style="padding-left:105;margin-top:2px;">
					<%--<asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 

                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>--%>
                <asp:LinkButton ID="btnMyAccount" style="    padding: 0px 10px 0px 0px;"  UseSubmitBehavior="False" OnClick="btnMyAccount_Click"
                        CssClass="header-account-button" runat="server" Text="Account" causesvalidation="true"></asp:LinkButton>
			</div>
              <div class="header-agentname" style="margin-top:2px;"><asp:Label ID="lblHeaderAgentName" style="    padding: 0px 10px 0px 0px;" runat="server" ></asp:Label> </div>
            <div class="header-lang">
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><asp:Button ID="btnLogOut"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Log Out"></asp:Button>
</ContentTemplate></asp:UpdatePanel>--%>
            <asp:LinkButton ID="btnLogOut"   UseSubmitBehavior="False" OnClick="btnLogOut_Click"
                                    CssClass="header-account-button" runat="server" Text="Log Out" 
                                    ></asp:LinkButton>	
                <%--	<a href="#">Log Out</a>--%>
			</div>
			<div class="header-social" style="display:none;">
				<a href="#" class="social-twitter"></a>
				<a href="#" class="social-facebook"></a>
				<a href="#" class="social-vimeo"></a>
				<a href="#" class="social-pinterest"></a>
				<a href="#" class="social-instagram"></a>
			</div>
			<div class="header-viewed">
				<a href="#" class="header-viewed-btn" style="display:none;">recently viewed</a>
				<!-- // viewed drop // -->
					<div class="viewed-drop">
						<div id="dvViewedItem" runat="server" class="viewed-drop-a">

						</div>
					</div>
				<!-- \\ viewed drop \\ -->
			</div>
			
			<div id="dvFlag" runat="server" class="header-lang" style="padding-top:5px;" >
				<a href="#"><img id="imgLang" runat="server" alt="" src="img/en.gif" /></a>
			</div>
			<div id="dvCurrency" runat="server"  style="margin-top:2px;" class="header-curency">
			</div>
   
               <div class="header-lang" id="dvMybooking" runat="server" style="padding-right:25px;margin-right:5px">
            <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:Button ID="btnMyBooking"   UseSubmitBehavior="false" 
                    CssClass="header-account-button"  runat="server" Text="MY BOOKING"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>--%>
            <asp:LinkButton ID="btnMyBooking"   UseSubmitBehavior="False" OnClick="btnMyBooking_Click"
                    CssClass="header-account-button" runat="server" Text="MY BOOKING" 
                    ></asp:LinkButton>
           
			</div>
			<div class="clear"></div>
		</div>
	</div>
    </ContentTemplate></asp:UpdatePanel>
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
  <div class="body-wrapper-guest-page">
    <div class="wrapper-padding">
    <div class="page-head-guest-page">
      <div class="page-title">

      Guest - <span>Summary</span></div>
      <div class="breadcrumbs">
        <a href="Home.aspx">Home</a> / <a href="#">Guest</a> / <span>Summary</span>
        <div>
	 
		</div>	
      </div>
      <div class="clear"></div>
    </div>
        <div class="sp-page" style="margin-top: 10px;">
            <div class="sp-page-a">
                <div class="sp-page-l">
                    <div class="sp-page-lb">
                        <div class="sp-page-p">
                            <div class="booking-left">
                                <asp:UpdatePanel ID="up1" runat="server">
                                    <ContentTemplate>
                                        <h2>
                                            Personal Information</h2>
                                        <asp:DataList ID="dlPersonalInfo" Width="100%" runat="server">
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="padding-bottom: 0px;">
                                                   <div style="padding-bottom:5px" id="divroomhead" runat ="server" >
                                                    <asp:Label ID="lblroomtext" Text='<%# Eval("partyname") %>'  CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                   </div>
                                                    <asp:Label ID="lblPType" Text='<%# Eval("Type") %>' Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblRowNo" Text='<%# Eval("RowNo") %>' Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblrlineno" Text='<%# Eval("rlineno") %>' Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblshiftfrom" Text='<%# Eval("shiftfrom") %>' Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblshiftto" Text='<%# Eval("shiftto") %>' Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblroomno" Text='<%# Eval("Roomno") %>' Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblRowNoAll" Text='<%# Eval("RowNoAll") %>' Visible="false" runat="server"></asp:Label>
                                                     <asp:Label ID="lblfromhotel" Text='<%# Eval("fromhotel") %>' Visible="false" runat="server"></asp:Label>
                                                      <asp:Label ID="lbltohotel" Text='<%# Eval("tohotel") %>' Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblRowHeading" Text='<%# Eval("Type")  + " " + Eval("RowNo") %>' CssClass="guest-label-head"
                                                        runat="server" ></asp:Label>
                                                             <asp:Label ID="lblGuestLineNo" Text='<%# Eval("GuestLineNo") %>' runat="server"></asp:Label>
                                                </div>
                                                <div>
                                                    <div class="guest-form">
                                                        <div class="guest-form-i-b">
                                                           <label class="required" runat="server" id="lbltitle">
                                                                Title</label>
                                                            <div class="dropdown">
                                                                <asp:DropDownList ID="ddlTittle" Width="100%" Height="26px" Style="border: 1px solid #fff"
                                                                    runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="guest-form-i-a">
                                                            <label class="required" runat="server" id="lblFirstName">
                                                                First Name:</label>
                                                            <div class="input">
                                                                <asp:TextBox ID="txtFirstName" placeholder="First Name" runat="server" AutoComplete="Off" CssClass="txtFirstNameClass"></asp:TextBox>
                                                                <%--//changed by mohamed on 15/09/2018--%>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtFirstName" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="False"
                                                                    MinimumPrefixLength="1" ServiceMethod="SerMeth_GetFirstName" TargetControlID="txtFirstName"
                                                                    UseContextKey="true" OnClientPopulating="txtFirstName_onclientpopulating" 
                                                                    >
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="guest-form-i-a"  id="dvmiddlename" runat="server" >
                                                            <label>
                                                                Middle Name(Optional):</label>
                                                            <div class="input">
                                                                <asp:TextBox ID="txtMiddleName" runat="server" AutoComplete="Off" CssClass="txtMiddleNameClass"></asp:TextBox>
                                                                <%--//changed by mohamed on 15/09/2018--%>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMiddleName" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="False"
                                                                    MinimumPrefixLength="1" ServiceMethod="SerMeth_GetFirstName" TargetControlID="txtMiddleName"
                                                                    UseContextKey="true" OnClientPopulating="txtMiddleName_onclientpopulating" 
                                                                    >
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="guest-form-i-a" id="dvlastname" runat="server" >
                                                           <label class="required" runat="server" id="lblLastname">
                                                                Last Name:</label>
                                                             <div class="input">
                                                                <asp:TextBox ID="txtLastName" placeholder="Last Name" runat="server" AutoComplete="Off" CssClass="txtLastNameClass"></asp:TextBox>
                                                                <%--//changed by mohamed on 15/09/2018--%>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtLastName" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="False"
                                                                    MinimumPrefixLength="1" ServiceMethod="SerMeth_GetFirstName" TargetControlID="txtLastName"
                                                                    UseContextKey="true" OnClientPopulating="txtLastName_onclientpopulating" 
                                                                    >
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="guest-form-i-a" style="float: left; margin-left: 10px">
                                                            <label class="required" runat="server" id="lblnationality">
                                                                Nationalty:</label>
                                                          
                                                            <div class="input">
                                                                <asp:TextBox ID="txtNationality" runat="server"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtNationality" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetNationality" TargetControlID="txtNationality"
                                                                    UseContextKey="true" OnClientItemSelected="NationalityAutocompleteSelected">
                                                                </asp:AutoCompleteExtender>
                                                                <asp:TextBox ID="txtNationalityCode" Style="display: none" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div id="dvChildAge" runat="server" class="guest-form-i-c" style="float: left; margin-left: 9px;">
                                                            <label>
                                                                Age:</label>
                                                            <div class="inputnew">
                                                                <asp:TextBox ID="txtChildAge" Text='<%# Eval("ChildAge") %>' ReadOnly="true" runat="server"></asp:TextBox></div>
                                                        </div>
                                                        <div style="float: right; margin-top: 15px;">
                                                             <asp:ImageButton ID="imgSclose" runat="server" ImageUrl="~/Img/DeleteRed.png"
                                                              Width="18px"  OnClick="imgSclose_Click" ToolTip="Delete Current Row" />
                                                        </div>
                                                    </div>
                                                   <div class="clear"></div>
                                                    <asp:LinkButton ID="lnkshow" CssClass="guest-service-btn" style="float:left;margin-top:-5px;margin-bottom:8px" runat="server" >Edit-Services</asp:LinkButton> 
                                                    <asp:Button ID="btnfillguest" CssClass="guest-flight-details-generate"  style="float:right;margin-top:-5px;margin-bottom:8px" runat="server" OnClick="btnfillguest_Click" Text="Fill Guest to Shift Hotel"></asp:Button>
                                                    <div class="side-block fly-in">
                                                     
                                                        <div id="divservices" runat="server" class="" style="display: none;float:left;Width:100%">
                                                            <asp:CheckBoxList ID="chkservices" CellPadding="5" CellSpacing="1"  RepeatColumns="2" Width="100%"
                                                                CssClass="checkboxguest" runat="server" RepeatDirection="Horizontal">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                   
                                                </div>
                                                
                                            </ItemTemplate>
                                             
                                        </asp:DataList>
                                       
                                        <div class="guest-form-i-a" style="margin-top:10px;display:none" >
                                            <asp:Button ID="btnAdd" CssClass="guest-addrow-generate"  runat="server" Text="Add Adult"></asp:Button>
                                        </div>
                                         <div class="guest-form-i-a" style="margin-top:10px;display:none">
                                            <asp:Button ID="btnAddchd" CssClass="guest-addrow-generate"  runat="server" Text="Add Child"></asp:Button>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="booking-form" >
                                            <div class="booking-form-i-a" style="display:none">
                                                <div class="checkbox" style="float: left;">
                                                    <asp:CheckBox ID="chkSameFlight" runat="server" /></div>
                                            </div>
                                            <div class="booking-form-i-a" style="margin-left: -180px;display:none" >
                                                <label>
                                                    Same flight for all guest</label>
                                            </div>
                                            

                                            <div class="booking-form-i-a" >
                                                <asp:Button ID="btnGenerateFlightDetails" Text="Add Flight Details" CssClass="guest-flight-details-generate"
                                                    runat="server" />
                                            </div>

                                                     <div class="booking-form-i-a" >
                                                <asp:Button ID="btnValidateServiceGuest" Text="Services - Guest Assigned" CssClass="guest-service-btn"
                                                    runat="server" />
                                            </div>
                                            
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div id="divFlightdetail" runat="server" style="display:none">

                                            <div id="dvFlightDetailsHeading" runat="server">
                                                <h2>
                                                    ARRIVAL FLIGHT:</h2>
                                            </div>
                                             
                                            <asp:DataList ID="dlFlightDetails" Width="100%" runat="server">
                                             <HeaderTemplate>
                                            
                                             
                                             </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <div class="guest-form" style="padding-bottom: 5px;">
                                                           
                                                            <div class="guest-form-i-a" >
                                                                <label>
                                                                    Arrival Date</label>
                                                                <div class="input-a" ">
                                                                    <asp:TextBox ID="txtArrivalDate" class="date-inpt-check-in-guest-arrival" placeholder="DD/MM/YYYY"
                                                                        Width="100%" runat="server"></asp:TextBox>
                                                                    <span class="date-icon-guest"></span>
                                                                </div>
                                                            </div>
                                                            <div class="guest-form-i-b" >
                                                            <label>
                                                               N/A</label>
                                                               <div class="checkbox" style="height:30px;">
                                                               <asp:CheckBox ID="chkNA" runat="server" />
                                                              
                                                               </div>
                                                            </div>
                                                            
                                                            <div class="guest-form-i-b">
                                                                <label>
                                                                    Flight No:</label>
                                                                <div class="input">
                                                                    <asp:TextBox ID="txtArrivalflight" runat="server"></asp:TextBox>
                                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtArrivalflight" runat="server"
                                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                        MinimumPrefixLength="-1" ServiceMethod="GetArrivalflight" TargetControlID="txtArrivalflight"
                                                                        UseContextKey="true" OnClientItemSelected="ArrivalflightAutocompleteSelected">
                                                                    </asp:AutoCompleteExtender>
                                                                    <asp:TextBox ID="txtArrivalflightCode" Style="display: none" runat="server"></asp:TextBox>
                                                                    <asp:TextBox ID="txtservicelineno" Style="display: none" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="guest-form-i-b" style="margin-left: 10px;">
                                                                <label>
                                                                    Arrival Time:</label>
                                                                <div class="input">
                                                                    <asp:TextBox ID="txtArrivalTime" runat="server"></asp:TextBox></div>
                                                            </div>
                                                            <div class="guest-form-i-a" style="margin-left: 8px;">
                                                                <label>
                                                                    Airport From:</label>
                                                                <div class="input" style="width: 130%">
                                                                    <asp:TextBox ID="txtArrivalAirport" Style="width: 100%" runat="server"></asp:TextBox>
                                                         
                                                                   
                                                                    </div>
                                                            </div>
                                                               <div class="guest-form-i-a" style="margin-left: 50px;">
                                                                <label>
                                                                    Airport To:</label>
                                                                <div class="input" style="width: 130%">
                                                                    <asp:TextBox ID="txtArrivaltoairport" Style="width: 100%" runat="server"></asp:TextBox>

                                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAArrivalpickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetArrivalpickup" TargetControlID="txtArrivaltoairport"
                                                    UseContextKey="true" OnClientItemSelected="MAArrivalpickupAutocompleteSelected">
                                                </asp:AutoCompleteExtender>

                                                                   <asp:TextBox ID="txtArrBordecode" Style="width: 100%;display:none"   runat="server"></asp:TextBox>
                                                                  </div>
                                                            </div>
                                                           
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        <div id="divreason" runat="server" style="display: none">
                                                         <div class="guest-form-i-a" style="float: left">
                                                         <label>Reason For N/A</label>
                                                        
                                                         </div> 
                                                            <div class="guest-form-i-a" style="float: left">
                                                               <div class="input" style="float: left;width:175%">
                                                                <asp:TextBox ID="txtreason" Style="width: 100%" runat="server"></asp:TextBox>
                                                            </div>
                                                            </div>
                                                            
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        <div class="side-block fly-in">
                                                        
                                                            <div id="divguestnames" runat="server" class="" style="float:left">
                                                            <asp:Label ID="Label1" runat="server" Text="Passenger Name(s)" style ="font-size:12px; display:block; margin-bottom:11px; color:#626262;">
                                                            </asp:Label>
                                                                   
                                                                <asp:CheckBoxList ID="chkguest" CellPadding="5" CellSpacing="1"   RepeatColumns="3"
                                                                    CssClass="checkbox" style="float:left" runat="server" RepeatDirection="Horizontal">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <div style="float:right;margin-top:20px">
                                                              <asp:Button ID="btnAppArrival"   cssclass="hotels-buttons-prefferred" Width ="140px" runat="server" OnClick="btnAppArrival_Click" Text="Apply Selection"><%-- CssClass="hotels-buttons-prefferred "--%>
                                                               </asp:Button>
                                                            </div>
                                                        </div>
                                                             <div class="guest-form-i-a">
                                               <asp:CheckBox ID="ChkFlightNotRequired"  style="color:#ff2235 !important;" Text="Tick here for cancel this row" runat="server" />
                                     
                                                  </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>

                                            <div class="guest-form-i-a">
                                                <asp:Button ID="btnAddflight" CssClass="guest-addrow-generate" runat="server" Text="Add">
                                                </asp:Button>
                                                 
                                            </div>
                                         
                                             <div class="clear">
                                                        </div>
                                             <div id="Div3" runat="server">
                                                <h2>
                                                    DEPARTURE FLIGHT:</h2>
                                            </div>
                                            <asp:DataList ID="dldeparturedetails" Width="100%" runat="server">
                                             
                                                <ItemTemplate>
                                                   
                                                          <div>
                                                        <div class="guest-form" style="padding-bottom: 5px;">
                                                           
                                                            <div class="guest-form-i-a" >
                                                                <label>
                                                                    Departure Date</label>
                                                                <div class="input-a" ">
                                                                    <asp:TextBox ID="txtDepartureDate" class="date-inpt-check-in-guest-departure" placeholder="DD/MM/YYYY"
                                                                        Width="100%" runat="server"></asp:TextBox>
                                                                    <span class="date-icon-guest"></span>
                                                                </div>
                                                            </div>
                                                             <div class="guest-form-i-b" >
                                                            <label>
                                                               N/A</label>
                                                               <div class="checkbox" style="height:30px;">
                                                               <asp:CheckBox ID="chkdepNA" runat="server" />
                                                              
                                                               </div>
                                                            </div>
                                                            <div class="guest-form-i-b">
                                                                <label>
                                                                    Flight No:</label>
                                                                 <div class="input">
                                                                <asp:TextBox ID="txtDepartureFlight" runat="server"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_DepartureFlight" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetDepartureflight" TargetControlID="txtDepartureFlight"
                                                                    UseContextKey="true" OnClientItemSelected="DepartureAutocompleteSelected">
                                                                </asp:AutoCompleteExtender>
                                                                <asp:TextBox ID="txtDepartureFlightCode" Style="display: none" runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="txtservicelineno" Style="display: none" runat="server"></asp:TextBox>
                                                                 </div>
                                                            </div>
                                                            <div class="guest-form-i-b" style="margin-left: 10px;">
                                                                <label Style="width: 120%">
                                                                    Departure Time:</label>
                                                                <div class="input">
                                                                    <asp:TextBox ID="txtDepartureTime" runat="server"></asp:TextBox></div>
                                                            </div>
                                                            <div class="guest-form-i-a" style="margin-left: 8px;">
                                                                <label>
                                                                    Airport From:</label>
                                                                <div class="input" style="width: 130%">
                                                                    <asp:TextBox ID="txtDepartureAirport" Style="width: 100%" runat="server"></asp:TextBox>
                                                                     <asp:TextBox ID="txtDepBordecode" Style="width: 100%;display:none" runat="server"></asp:TextBox>


                                                                     
                                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMADeparturepickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetArrivalpickup" TargetControlID="txtDepartureAirport"
                                                    UseContextKey="true" OnClientItemSelected="MADeparturepickupAutocompleteSelected">
                                                </asp:AutoCompleteExtender>

                                                                    </div>
                                                            </div>
                                                               <div class="guest-form-i-a" style="margin-left: 50px;">
                                                                <label>
                                                                    Airport To:</label>
                                                                <div class="input" style="width: 130%">
                                                                    <asp:TextBox ID="txtDeparturetoAirport" Style="width: 100%" runat="server"></asp:TextBox></div>
                                                            </div>
                                                           
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                         <div id="divdepreason" runat="server" style="display: none">
                                                         <div class="guest-form-i-a" style="float: left">
                                                         <label>Reason For N/A</label>
                                                        
                                                         </div> 
                                                            <div class="guest-form-i-a" style="float: left">
                                                               <div class="input" style="float: left;width:175%">
                                                                <asp:TextBox ID="txtdepreason" Style="width: 100%" runat="server"></asp:TextBox>
                                                            </div>
                                                            </div>
                                                            
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        <div class="side-block fly-in">
                                                        
                                                            <div id="divguestnames" runat="server" class="" style="float:left">
                                                            <asp:Label ID="Label18" runat="server" Text="Passenger Name(s)" style ="font-size:12px; display:block; margin-bottom:11px; color:#626262;">
                                                            </asp:Label>
                                                                   
                                                                <asp:CheckBoxList ID="chkguest" CellPadding="5" CellSpacing="1"   RepeatColumns="3"
                                                                    CssClass="checkbox" runat="server" RepeatDirection="Horizontal">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                             <div style="float:right;margin-top:20px">
                                                              <asp:Button ID="btnAppDeparture" Cssclass="hotels-buttons-prefferred"  Width="140px" runat="server" OnClick="btnAppDeparture_Click"  Text="Apply Selection">  <%--CssClass="guest-apply-button"--%>
                                                             </asp:Button>
                                                             </div> 
                                                        </div>
                                                         <div class="guest-form-i-a">
                                                        <asp:CheckBox ID="chkDepFlightNotrquired" style="color:#ff2235 !important;" Text="Tick here for cancel this row" runat="server" />
                                                 </div>
                                                    </div>
                                                </ItemTemplate>
  
                                            </asp:DataList>
                                            <div class="guest-form-i-a">
                                                <asp:Button ID="btnAddDepflight" CssClass="guest-addrow-generate" runat="server" Text="Add">
                                                </asp:Button>
                                                
                                            </div>
                                               
                                            
                                        </div>
                                      
                                        <div class="clear">
                                        </div>
                                     
                                        <div>
                                            <div class="booking-form">
                                                <div class="booking-form-i-a">
                                                    <label>
                                                        Agent Reference:</label>
                                                    <div class="input">
                                                        <asp:TextBox ID="txtAgentReference" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div runat="server" id="dvColumbusReference" style="display:none">
                                                    <div class="booking-form-i-a" runat="server" id="Div37" style="padding-left: 25px;">
                                                        <label>
                                                            Please select check box for 1.0 Invoice No to be assigned (RPT/RGT/RP/RG):</label>
                                                    </div>
                                                    <div class="booking-form-i-a" runat="server" id="Div34" style="padding-left:5px;
                                                        width:5%;">
                                                        <asp:CheckBox ID="chkColRef" runat="server" onchange="fndvColRef(this)" Text="" Height="30px" />
                                                    </div>
                                                    <div class="booking-form-i-a" runat="server" id="dvColRef" style="width:25%;">
                                                        <div class="input">
                                                            <asp:TextBox ID="txtColumbusReference"  MaxLength="12" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                            <div class="booking-form" id="divsubmit" runat ="server">
                                                <div id="divaccept" runat="server">
                                                    <div id="dvSendEmail" runat="server" >
                                                    <div class="booking-form-i-a">
                                                        <div class="checkbox" style="float: left;">
                                                            <asp:CheckBox ID="chkEmailToAgent" Checked="true" runat="server" /></div>
                                                        <label>
                                                            Send email to agent</label>
                                                    </div>
                                                    <div class="booking-form-i-a">
                                                        <div class="checkbox" style="float: left;">
                                                            <asp:CheckBox ID="chkEmailToHotel" Checked="true" runat="server" /></div>
                                                        <label>
                                                            Send email to hotel</label>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                    </div>
                                                    <div id="divTerms" runat ="server">
                                                        <div class="booking-form-i-a">
                                                            <div class="checkbox" style="float: left;">
                                                                <asp:CheckBox ID="chkTermsAndConditions" runat="server" /></div>
                                                        </div>
                                                        <div class="booking-form-i-a" style="margin-left: -180px;">
                                                            <label>
                                                                I Accept the Terms and Conditions</label>
                                                        </div>
                                                    </div>
                                                </div>  
                                                <div class="booking-form-i-a" id="divPayLater" runat ="server" style="width:23%">
                                                    <asp:Button ID="btnPayLater" class="authorize-btn" Width="150px" runat="server"
                                                       Text="Pay Later" />
                                                </div>
                                                <div class="booking-form-i-a" id="divPayNow" runat ="server" style="width:23%">
                                                    <asp:Button ID="btnPayNow" class="authorize-btn" Width="150px" runat="server"
                                                       Text="Pay Now" />
                                                </div>                                             
                                                <div class="booking-form-i-a" id="divSubmitBooking" runat ="server" style="width:23%">
                                                    <asp:Button ID="btnSubmitBooking" class="authorize-btn" Width="150px" runat="server"
                                                       OnClientClick="return ShowProgess()" Text="Submit Booking" />
                                                </div>                                                
                                               <div id="divPrintItinerary" runat ="server" class="booking-form-i-a" style="display:none">
                                                    <asp:Button ID="btnPrintItinerary" class="authorize-btn" Width="170px"  runat="server"
                                                        Text="Print Itinerary" />
                                               </div>
                                                <div id="divprintbook" runat ="server" class="booking-form-i-a" style="display:none">                                                    
                                                    <asp:Button ID="btnprint" class="authorize-btn" Width="170px"  runat="server"
                                                        Text="Print Booking" />
                                                </div>
                                                <div id="divnewbook" runat ="server" class="booking-form-i-a" style="display:none">
                                                    <asp:Button ID="btnnewbooking" class="authorize-btn" Width="170px"  runat="server"
                                                        Text="New Booking" />
                                                </div>
                                                 <div id="divabondon" runat ="server" class="booking-form-i-a" style="width:23%">
                                                    <asp:Button ID="btnAbondon" class="authorize-btn" Width="150px" runat="server"
                                                        OnClientClick="return Validate()"   Text="Abandon" />
                                                </div>
                                            </div>
                                        </div>
                                        
                                       
                                        <asp:HiddenField ID="hdnguestchange" runat="server" />
                                        <asp:HiddenField ID="hdnguestsaved" runat="server" />
                                        <asp:HiddenField ID="hdCheckIn" runat="server" />
                                        <asp:HiddenField ID="hdCheckOut" runat="server" />
                                        <asp:HiddenField ID="hdCheckInPrevDay" runat="server" />
                                        <asp:HiddenField ID="hdCheckInNextDay" runat="server" />
                                        <asp:HiddenField ID="hdCheckOutPrevDay" runat="server" />
                                        <asp:HiddenField ID="hdCheckOutNextDay" runat="server" />
                                        <asp:HiddenField ID="hdBookingEngineRateType" runat="server" />
                                        <asp:HiddenField ID="hdFinalReqestId" runat="server" />
                                        
                                        <asp:HiddenField ID="hdnBookingIDPrefix01" runat="server" />
                                        <asp:HiddenField ID="hdnBookingIDPrefix02" runat="server" />

                                        <div class="clear">
                                        </div>
                                         
                                        <div align="left" id="dvBookingStatus" runat="server" visible="false" style="float:left;">

                                            <div id="div43" runat="server" style="border: 1px solid #fff; width: 900px;
                                                height: 30px; background-color: #ede7e1; padding-right: 5px; margin-right: 10px;
                                                text-align: center; vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">                    
                                                <div></div>
                                                <asp:Label ID="Label21" CssClass="room-type-breakup-headings" Text="Hotel Booking Status" 
                                                    runat="server" Width="100%" ></asp:Label>  
                                            </div>
                                            <div class="booking-form" id="Div44" runat ="server" style="background-color: White; padding: 1px; width: 900px">
                                                <div class="row-col-12" style="padding-left: 2px;">
                                                    <asp:GridView ID="gvBookingStatus" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                    Width="100%"  GridLines="Horizontal">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" >
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgStatus" runat="server" ImageUrl="~/img/tick.jpg" />
                                                                <asp:Image ID="imgStatusNo" runat="server" ImageUrl="~/img/DeleteRed16.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hotel Name" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHotelName" Text='<%# Eval("HotelName") %>' runat="server"></asp:Label>                                  
                                                                <asp:Label ID="lblRlineNo" Text='<%# Eval("rlineNo") %>' runat="server" style="display:none;"> ></asp:Label>                                  
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Room Type" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRoomName" Text='<%# Eval("roomName") %>' runat="server"></asp:Label>                                  
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Description" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblerrdesc" Text='<%# Eval("errDescription") %>' style="font-weight:bold" runat="server"></asp:Label>
                                                                <asp:Label ID="lblCancelled" Text='<%# Eval("cancelled") %>' Visible="false" runat="server"></asp:Label>                                    
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns> 
                                                    <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                    <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                    </asp:GridView> 
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div> 

                                        </div> 
                                        
                                        <div class="clear">
                                        </div>

                                         <div  id="dvhotnoshow" runat="server" style="display:none;background-color:#F2F3F4;text-align:center;height:50px;padding-top:15px;">
                                         <asp:Label ID="lblheader" runat="server" Text="Booking Generated"   CssClass="room-type-breakup-headings" Font-Bold="True"   >
                                            </asp:Label>
                                        </div> 
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                <div style="display: none;" class="booking-complete">
                                    <h2>
                                        Review and book your trip</h2>
                                    <p>
                                        Voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur
                                        magni dolores eos qui voluptatem sequi nesciunt.
                                    </p>
                                    <button class="booking-complete-btn">
                                        COMPLETE BOOKING</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                
                  
                                        
                </div>
            </div>
             <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
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
                         <input id="btnInvisibleGuest" runat="server" type="button" value="Cancel" style="display: none" />
                        <input id="btnClose" type="button" value="Cancel" style="display: none" />
                        </center>
             </ContentTemplate> 
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpbookError" runat="server">
            <ContentTemplate>
            <asp:ModalPopupExtender ID="mpBookingError" runat="server" BackgroundCssClass="roomtype-modalBackground"
                CancelControlID="aErrorclose" EnableViewState="true" PopupControlID="pnlBookingError"
                TargetControlID="btnviewchild">
            </asp:ModalPopupExtender>
           
            <asp:Panel ID="pnlBookingError" style="display:none;"  runat="server">
          
            <div id ="divError"  class="roomtype-price-breakuppopup" style="float:left;text-align:left;max-height: 500px;">
            <div align="left" id="Div8">

                <div id="divErrorlist" runat="server" style="border: 1px solid #fff; width: 700px;
                    height: 30px; background-color: #ede7e1; padding-right: 5px; margin-right: 10px;
                    text-align: center; vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                    <asp:Label ID="lblErrorlist" CssClass="room-type-breakup-headings" Text="Booking Errors"
                        runat="server"></asp:Label>
                         &nbsp; &nbsp;<a id="aErrorclose" href="#" class="roomtype-popupremarks-close"></a>
                   
                </div>
                 <div class="booking-form" id="Div10" runat ="server" style="background-color: White; padding: 5px;">
                    <div class="row-col-12" style="padding-left: 10px;">
                        <asp:GridView ID="gdErrorlist" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                        Width="100%"  GridLines="Horizontal">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                <ItemTemplate>
                                    <asp:Label ID="lblsno" Text='<%# Eval("autoid") %>' runat="server"></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Description" >
                                <ItemTemplate>
                                    <asp:Label ID="lblerrdesc" Text='<%# Eval("errmsg") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns> 
                        <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                        <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                    <div class="clear">
                    </div>
                    
                     <div class="booking-form-i-a"  style="margin-left:100px;margin-top:30px;">

                     <div class="clear">
                     </div>
                      <asp:Button ID="btnbacktobooking"   class="authorize-btn" Width="170px" runat="server" Text="Back to Booking" />
                    </div>
                      <div class="booking-form-i-a"  style="margin-left:100px;margin-top:30px;">

                      <asp:Button ID="btnTempBookingRef"   class="authorize-btn" Width="270px" runat="server" Text="Temporary Booking Reference" />
                    </div>

                     <div class="booking-form-i-a"  style="margin-top:30px;">
                      <div class="clear">
                     </div>
                      <asp:Button ID="btnproceed"   class="authorize-btn" Width="170px" runat="server" Text="Proceed with Saving" OnClientClick="HideBookingError();ShowProgess()" />
                    </div>

                    

                </div> 

            </div> 
            </div>
            <input id="btnviewchild" runat="server" type="button" value="Cancel" style="display:none" />
          
            </asp:Panel>
                       
                               <asp:ModalPopupExtender ID="MpAirportMaRemarks" runat="server" BackgroundCssClass="roomtype-modalBackground"
            CancelControlID="aairportmaRemarksClose" EnableViewState="true" PopupControlID="PnlAirportmaremarks"
            TargetControlID="hdnairportma">
        </asp:ModalPopupExtender>
        <asp:HiddenField ID="hdnairportma" runat="server" />
        <asp:Panel runat="server" ID="PnlAirportmaremarks" Style="display: none;">
            <div id="Div18" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                max-height: 500px;">
                <div align="left" id="Div19">
                    <div id="Div23" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                        background-color: #ede7e1; padding-right: 5px; margin-right: 10px; text-align: center;
                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                        <%--            <asp:Label ID="Label19" CssClass="room-type-breakup-headings" Text="Airport Meet & Assist Remarks"
                                               runat="server"></asp:Label>
                                                &nbsp; &nbsp;<a id="aairportmaRemarksClose" href="#" class="roomtype-popupremarks-close" ></a>--%>
                        <div class="chk-l" style="font-size: small; padding-left: 8px">
                            <asp:Label ID="lblairservhead" runat="server" Text="Service  Type -" CssClass="room-type-breakup-headings"></asp:Label>
                        </div>
                        <%--style="width:25%"--%>
                        <div class="chk-r" style="font-size: 12px; padding-left: 20px;">
                            <asp:Label ID="lblairservname" runat="server"></asp:Label></div>
                        <div class="chk-l" style="font-size: small; padding-left: 20px">
                            <asp:Label ID="lblairdatehead" CssClass="room-type-breakup-headings" Text="Date -"
                                runat="server"></asp:Label>
                        </div>
                        <div class="chk-r" style="font-size: 12px; padding-left: 10px">
                            <asp:Label ID="lblairservdate" runat="server"></asp:Label>
                        </div>
                        <%--style="width:75%"--%>
                        <a id="aairportmaRemarksClose" href="#" class="roomtype-popupallremarks-close"></a>
                    </div>
                    <div id="Div24" class="roomtype-price-breakuppopup" style="max-height: 500px; overflow: auto">
                        <div id="dvAirPartyRemarks" runat="server">
                            <div id="Div25" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                                background-color: #ede7e1; padding-right: 5px; padding-left: 5px; margin-right: 10px;
                                text-align: left; vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblAirPartyRemarks" CssClass="room-type-breakup-headings" Text="Information to Supplier"
                                    runat="server"></asp:Label>
                            </div>
                            <div id="Div26" runat="server" style="padding-left: 10px; padding-top: 2px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i" style="width: 690px; float: left; height: 34;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtAirPartyRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="Div27" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                            background-color: #ede7e1; padding-right: 5px; padding-left: 5px; margin-right: 10px;
                            text-align: left; vertical-align: middle; padding-top: 10px;">
                            <asp:Label ID="lblAirCustRemarks" CssClass="room-type-breakup-headings" Text="Information to Customer"
                                runat="server"> </asp:Label>
                        </div>
                        <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                            <div class="booking-form">
                                <div class="booking-form-i-b" style="width: 690px; float: left;">
                                    <div class="input input">
                                        <asp:TextBox ID="txtAirCustRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                            TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div id="dvAirArrRemarks" runat="server">
                            <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblAirArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival- Information to Operation"
                                    runat="server"> </asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i-b" style="width: 690px; float: left;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtAirArrRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="dvAirdeptremarks" runat="server">
                            <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblAirdeptRemarks" CssClass="room-type-breakup-headings" Text="Departure- Information to Operation"
                                    runat="server"> </asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i-b" style="width: 690px; float: left;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtAirdeptRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="airhdnalineno" runat="server" />
                        <div class="booking-form-i-a" style="margin-left: 270px;">
                            <asp:Button ID="btnAirSaveRemarks" class="authorize-btn" Width="150px" runat="server"
                                Text="Save" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
                <asp:ModalPopupExtender ID="MPTransfersRemarks" runat="server" BackgroundCssClass="roomtype-modalBackground"
            CancelControlID="aTrfsRemarksClose" EnableViewState="true" PopupControlID="PnlTranfersremarks"
            TargetControlID="hdntransfers">
        </asp:ModalPopupExtender>
        <asp:HiddenField ID="hdntransfers" runat="server" />
        <asp:Panel runat="server" ID="PnlTranfersremarks" Style="display: none;">
            <div id="Div16" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                max-height: 500px;">
                <div align="left" id="Div17">
                    <div id="Div20" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                        background-color: #ede7e1; padding-right: 5px; margin-right: 10px; text-align: center;
                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                        <div class="chk-l" style="font-size: small; padding-left: 10px">
                            <asp:Label ID="lbltransname" runat="server" Text="Service  Name -" CssClass="room-type-breakup-headings"></asp:Label>
                        </div>
                        <div class="chk-r" style="font-size: 12px; padding-left: 10px;">
                            <asp:Label ID="lbltransfername" runat="server"></asp:Label></div>
                        <div class="chk-l" style="font-size: small; padding-left: 20px">
                            <asp:Label ID="lbltransdate" CssClass="room-type-breakup-headings" Text="Date -"
                                runat="server"></asp:Label>
                        </div>
                        <div class="chk-r" style="font-size: 12px; padding-left: 5px">
                            <asp:Label ID="lbltransferdate" runat="server"></asp:Label>
                        </div>
                        <a id="aTrfsRemarksClose" href="#" class="roomtype-popupallremarks-close"></a>
                        <%--
                              <asp:Label ID="lblTrfsHeading" CssClass="room-type-breakup-headings" Text="Transfers Remarks"
                                               runat="server"></asp:Label>
                                                &nbsp; &nbsp;<a id="aTrfsRemarksClose" href="#" class="roomtype-popupremarks-close" ></a>--%>
                    </div>
                    <div id="Div21" class="roomtype-price-breakuppopup" style="max-height: 500px; overflow: auto">
                        <div id="dvTrfsPartyRemarks" runat="server">
                            <div id="Div22" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                                background-color: #ede7e1; padding-right: 5px; padding-left: 5px; margin-right: 10px;
                                text-align: left; vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblTrfsPartyRemarks" CssClass="room-type-breakup-headings" Text="Information to Supplier"
                                    runat="server"></asp:Label>
                            </div>
                            <div id="Div28" runat="server" style="padding-left: 10px; padding-top: 2px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i" style="width: 690px; float: left; height: 34;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtTrfsPartyRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="Div29" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                            background-color: #ede7e1; padding-right: 5px; padding-left: 5px; margin-right: 10px;
                            text-align: left; vertical-align: middle; padding-top: 10px;">
                            <asp:Label ID="lblTrfsCustRemarks" CssClass="room-type-breakup-headings" Text="Information to Customer"
                                runat="server"> </asp:Label>
                        </div>
                        <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                            <div class="booking-form">
                                <div class="booking-form-i-b" style="width: 690px; float: left;">
                                    <div class="input input">
                                        <asp:TextBox ID="txtTrfsCustRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                            TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div id="dvtrfsArrRemarks" runat="server" runat="server">
                            <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lbltrfsArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival- Information to Operation"
                                    runat="server"> </asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i-b" style="width: 690px; float: left;">
                                        <div class="input input">
                                            <asp:TextBox ID="txttrfsArrRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="dvtrfsDeptRemarks" runat="server">
                            <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblTrfsDeptRemarks" CssClass="room-type-breakup-headings" Text="Departure- Information to Operation"
                                    runat="server"> </asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i-b" style="width: 690px; float: left;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtTrfsDeptRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="trfshdntlineno" runat="server" />
                        <div class="booking-form-i-a" style="margin-left: 270px;">
                            <asp:Button ID="btnTrfsSaveRemarks" class="authorize-btn" Width="150px" runat="server"
                                Text="Save" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
                <asp:ModalPopupExtender ID="MPOthServRemarks" runat="server" BackgroundCssClass="roomtype-modalBackground"
            CancelControlID="aOthRemarksClose" EnableViewState="true" PopupControlID="pnlOtherServRemarks"
            TargetControlID="hdnOthServ">
        </asp:ModalPopupExtender>
        <asp:HiddenField ID="hdnOthServ" runat="server" />
        <asp:Panel runat="server" ID="pnlOtherServRemarks" Style="display: none;">
            <div id="Div14" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                max-height: 500px;">
                <div align="left" id="Div15">
                    <div id="Div33" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                        background-color: #ede7e1; padding-right: 5px; margin-right: 10px; text-align: center;
                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                        <div class="chk-l" style="font-size: small; padding-left: 8px">
                            <asp:Label ID="Label18" runat="server" Text="Service  Type -" CssClass="room-type-breakup-headings"></asp:Label>
                        </div>
                        <div class="chk-r" style="font-size: 12px; padding-left: 10px;">
                            <asp:Label ID="lblothservicehead" runat="server"></asp:Label></div>
                        <div class="chk-l" style="font-size: small; padding-left: 20px;">
                            <asp:Label ID="lblothservicedate" CssClass="room-type-breakup-headings" Text="Date -"
                                runat="server"></asp:Label>
                        </div>
                        <div class="chk-r" style="font-size: 12px; padding-left: 10px;">
                            <asp:Label ID="lblotherservicedate" runat="server"></asp:Label>
                        </div>
                        &nbsp; &nbsp;<a id="aOthRemarksClose" href="#" class="roomtype-popupremarks-close"></a>
                    </div>
                    <div class="roomtype-price-breakuppopup" style="max-height: 500px; overflow: auto">
                        <div id="dvOthPartyRemarks" runat="server">
                            <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblOthPartyRemarks" CssClass="room-type-breakup-headings" Text="Information to Supplier"
                                    runat="server"></asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 2px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i" style="width: 690px; float: left; height: 34;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtOthPartyRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                            padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                            vertical-align: middle; padding-top: 10px;">
                            <asp:Label ID="lblOthCustRemarks" CssClass="room-type-breakup-headings" Text="Information to Customer"
                                runat="server"> </asp:Label>
                        </div>
                        <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                            <div class="booking-form">
                                <div class="booking-form-i-b" style="width: 690px; float: left;">
                                    <div class="input input">
                                        <asp:TextBox ID="txtOthCustRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                            TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div id="dvOthArrRemarks" runat="server">
                            <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblOthArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival- Information to Operation"
                                    runat="server"> </asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i-b" style="width: 690px; float: left;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtOthArrRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="dvOthdeptremarks" runat="server">
                            <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblOthDeptRemarks" CssClass="room-type-breakup-headings" Text="Departure- Information to Operation"
                                    runat="server"> </asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i-b" style="width: 690px; float: left;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtOthDeptRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="OthHdnRlineno" runat="server" />
                        <div class="booking-form-i-a" style="margin-left: 270px;">
                            <asp:Button ID="btnOthSaveRemarks" class="authorize-btn" Width="150px" runat="server"
                                Text="Save" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
                <asp:ModalPopupExtender ID="MPVisaRemarks" runat="server" BackgroundCssClass="roomtype-modalBackground"
            CancelControlID="aVisaRemarksClose" EnableViewState="true" PopupControlID="PnlVisaremarks"
            TargetControlID="hdnVisaRemarks">
        </asp:ModalPopupExtender>
        <asp:HiddenField ID="hdnVisaRemarks" runat="server" />
        <asp:Panel runat="server" ID="PnlVisaremarks" Style="display: none;">
            <div id="Div30" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                max-height: 500px;">
                <div align="left" id="Div31">
                    <div id="Div32" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                        background-color: #ede7e1; padding-right: 5px; margin-right: 10px; text-align: center;
                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                        <div class="chk-l" style="font-size: small; padding-left: 5px;">
                            <asp:Label ID="Label20" runat="server" Text="Visa Type -" CssClass="room-type-breakup-headings"></asp:Label>
                        </div>
                        <div class="chk-r" style="font-size: 12px; padding-left: 10px;">
                            <asp:Label ID="lblvisatype" runat="server"></asp:Label></div>
                        <div class="chk-l" style="font-size: small; padding-left: 10px">
                            <asp:Label ID="lblvisadate" CssClass="room-type-breakup-headings" Text="Date -" runat="server"></asp:Label>
                        </div>
                        <div class="chk-r" style="font-size: 12px; padding-left: 10px">
                            <asp:Label ID="lblremvisadate" runat="server"></asp:Label>
                        </div>
                        <%--   <asp:Label ID="" CssClass="room-type-breakup-headings" Text="Visa Remarks"
                                               runat="server"></asp:Label>--%>
                        &nbsp; &nbsp;<a id="aVisaRemarksClose" href="#" class="roomtype-popupremarks-close"></a>
                    </div>
                    <div id="Div36" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                        background-color: #ede7e1; padding-right: 5px; padding-left: 5px; margin-right: 10px;
                        text-align: left; vertical-align: middle; padding-top: 10px;">
                        <asp:Label ID="lblVisaAgentRemarks" CssClass="room-type-breakup-headings" Text="Information to Customer"
                            runat="server"> </asp:Label>
                    </div>
                    <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                        <div class="booking-form">
                            <div class="booking-form-i-b" style="width: 690px; float: left;">
                                <div class="input input">
                                    <asp:TextBox ID="txtVisaAgentRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                        TextMode="MultiLine" runat="server"></asp:TextBox></div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div id="dvvisaarrivalrem" runat="server">
                        <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                            padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                            vertical-align: middle; padding-top: 10px;">
                            <asp:Label ID="lblvisaarrremarks" CssClass="room-type-breakup-headings" Text="Arrival- Information to Operation"
                                runat="server"> </asp:Label>
                        </div>
                        <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                            <div class="booking-form">
                                <div class="booking-form-i-b" style="width: 690px; float: left;">
                                    <div class="input input">
                                        <asp:TextBox ID="txtvisaarrremarks" Style="border: none; width: 100%; font-size: 12px;"
                                            TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="visahdnvlineno" runat="server" />
                    <div class="booking-form-i-a" style="margin-left: 270px;">
                        <asp:Button ID="btnVisaSaveRemarks" class="authorize-btn" Width="150px" runat="server"
                            Text="Save" />
                    </div>
                </div>
            </div>
        </asp:Panel>
                                <asp:ModalPopupExtender ID="MPToursRemarks" runat="server" BackgroundCssClass="roomtype-modalBackground"
            CancelControlID="aToursRemarksClose" EnableViewState="true" PopupControlID="pnlToursRemarks"
            TargetControlID="ImghdnToursRemarks">
        </asp:ModalPopupExtender>
        <asp:HiddenField ID="ImghdnToursRemarks" runat="server" />
        <asp:Panel runat="server" ID="pnlToursRemarks" Style="display: none;">
            <div id="dvToursRemarks" class="roomtype-price-breakuppopup" style="float: left;
                text-align: left; max-height: 500px;">
                <div align="left" id="Div11">
                    <div id="Div12" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                        background-color: #ede7e1; padding-right: 5px; padding-left: 8px; text-align: left;
                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                        <div class="chk-l" style="font-size: small;">
                            <asp:Label ID="lbltourhead" runat="server" Text="Excursion  Name - " CssClass="room-type-breakup-headings"></asp:Label>
                        </div>
                        <%--style="width:25%"--%>
                        <div class="chk-r" style="font-size: 12px; padding-left: 10px;">
                            <asp:Label ID="lblToursRemarksheading" runat="server"></asp:Label></div>
                        <div class="chk-l" style="font-size: small; padding-left: 20px">
                            <asp:Label ID="lbltourdate" CssClass="room-type-breakup-headings" Text="Date -" runat="server"></asp:Label>
                        </div>
                        <div class="chk-r" style="font-size: 12px;">
                            <asp:Label ID="lbltoursdate" runat="server"></asp:Label>
                        </div>
                        <%--style="width:75%"--%>
                        <a id="aToursRemarksClose" href="#" class="roomtype-popupallremarks-close"></a>
                        <%--
                        <div class="chk-l" >        <asp:Label ID="lblToursRemarksheading" CssClass="room-type-breakup-headings" style=" text-transform:none;font-size: 12px !important; font-weight:bold;"
                                               runat="server"></asp:Label></div>
                                            <div class="chk-r" >                  <asp:Label ID="lbltoursdate" CssClass="room-type-breakup-headings" style=" text-transform:none;font-size: 12px !important; font-weight:bold;"
                                               runat="server"></asp:Label></div>
                                                <a id="aToursRemarksClose" href="#" class="roomtype-popupremarks-close" ></a>
                        --%>
                    </div>
                    <div class="roomtype-price-breakuppopup" style="max-height: 500px; overflow: auto">
                        <div id="dvToursPartyRemarks" runat="server">
                            <div id="Div13" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-left: 5px; text-align: left; vertical-align: middle; padding-top: 5px;">
                                <asp:Label ID="lblToursheading" CssClass="room-type-breakup-headings" Text="Information to Supplier"
                                    runat="server"></asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 2px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i" style="width: 690px; float: left; height: 34;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtToursPartyRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="dvToursCustRemarks" runat="server" style="border: 1px solid #fff; width: 700px;
                            height: 30px; background-color: #ede7e1; padding-right: 5px; padding-left: 5px;
                            margin-right: 10px; text-align: left; vertical-align: middle; padding-top: 10px;">
                            <asp:Label ID="lblToursCustRemarks" CssClass="room-type-breakup-headings" Text="Information to Customer"
                                runat="server"> </asp:Label>
                        </div>
                        <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                            <div class="booking-form">
                                <div class="booking-form-i-b" style="width: 690px; float: left;">
                                    <div class="input input">
                                        <asp:TextBox ID="txtToursCustRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                            TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div id="dvToursArrRemarks" runat="server">
                            <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblToursArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival- Information to Operation"
                                    runat="server"> </asp:Label>
                            </div>
                            <div id="dvtxtToursArr" runat="server" style="padding-left: 10px; padding-top: 10px;
                                margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i-b" style="width: 690px; float: left;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtToursArrRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="dvToursDeptRemarks" runat="server">
                            <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblToursDeptRemarks" CssClass="room-type-breakup-headings" Text="Departure- Information to Operation"
                                    runat="server"> </asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i-b" style="width: 690px; float: left;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtToursDeptRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="ToursHdnElineno" runat="server" />
                        <div class="booking-form-i-a" style="margin-left: 270px;">
                            <asp:Button ID="btnToursSaveRemarks" class="authorize-btn" Width="150px" runat="server"
                                Text="Save" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:ModalPopupExtender ID="mpCheckAvailability" runat="server" BackgroundCssClass="roomtype-modalBackground"
                CancelControlID="aAvailabilityErrorclose" EnableViewState="true" PopupControlID="pnlCheckAvailability"
                TargetControlID="btnCheckAvailability">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="hdnAvailabilityState" runat="server"/>
            <asp:Panel ID="pnlCheckAvailability" style="display:none;"  runat="server">
          
            <div id ="div35"  class="roomtype-price-breakuppopup" style="float:left;text-align:left;max-height: 500px;">
            <div align="left" id="Div38">

                <div id="div39" runat="server" style="border: 1px solid #fff; width: 900px;
                    height: 30px; background-color: #ede7e1; padding-right: 5px; margin-right: 10px;
                    text-align: center; vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">                    
                    <div></div>
                    <asp:Label ID="lblAvailabilityTitle" CssClass="room-type-breakup-headings" Text="Hotel Availability and Price Errors" 
                        runat="server" Width="850px" ></asp:Label>  
                        <%--Hotel Availability and Price Errors"--%>                  
                         &nbsp; &nbsp;<a id="aAvailabilityErrorclose" href="#" class="roomtype-popupremarks-close" style="margin-left:870px;"></a>
                   
                </div>
                 <div class="booking-form" id="Div40" runat ="server" style="background-color: White; padding: 1px; width: 900px">
                    <div class="row-col-12" style="padding-left: 2px;">
                        <asp:GridView ID="gvCheckAvailability" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                        Width="100%"  GridLines="Horizontal">
                        <Columns>
                            <asp:TemplateField HeaderText="" >
                                <ItemTemplate>
                                    <asp:Image ID="imgStatus" runat="server" ImageUrl="~/img/tick.jpg" />
                                    <asp:Image ID="imgStatusNo" runat="server" ImageUrl="~/img/DeleteRed16.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hotel Name" >
                                <ItemTemplate>
                                    <asp:Label ID="lblHotelName" Text='<%# Eval("HotelName") %>' runat="server"></asp:Label>                                  
                                    <asp:Label ID="lblRlineNo" Text='<%# Eval("rlineNo") %>' runat="server" style="display:none;"> ></asp:Label>                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Room Type" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRoomName" Text='<%# Eval("roomName") %>' runat="server"></asp:Label>                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Description" >
                                <ItemTemplate>
                                    <asp:Label ID="lblerrdesc" Text='<%# Eval("errmsg") %>' style="font-weight:bold" runat="server"></asp:Label>
                                    <asp:Label ID="lblStatusCode" Text='<%# Eval("statusCode") %>' Visible="false" runat="server"></asp:Label>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button id="btnRemoveBooking" runat="server" class="authorize-btn" Height="20px" Text="Remove Hotel" OnClick="btnRemoveBooking_Click" ></asp:Button>
                                     <asp:Label ID="lblShowRemoveFlag" Text='<%# Eval("ShowRemoveFlag") %>' runat="server" style="display:none;"> ></asp:Label>   
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Book With Revised Price">
                                <ItemTemplate>
                                    <asp:CheckBox id="chkPriceChange" runat="server" class="checkbox" />                                    
                                </ItemTemplate>                                
                            </asp:TemplateField>
                        </Columns> 
                        <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                        <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                        </asp:GridView>                                                
                        <div id="dvEditBooking" runat="server" style="float:left;font-size: 12px; padding-left: 10px; padding-top:3px; padding-bottom:3px;padding-right:7px">                            
                            <asp:Button id="btnEditBooking" runat="server" class="authorize-btn" Width="150px" Height="25px" Text="Edit Booking" ></asp:Button>
                        </div>   
                        <div id="dvContinueBooking" runat="server" style=" margin-left:200px; font-size: 12px; padding-left: 10px; padding-top:3px; padding-bottom:3px;padding-right:7px">                                             
                        <asp:Button id="btnContinueBooking" runat="server" Width="150px" Height="25px" class="authorize-btn" Text="Proceed Booking" Visible="false"></asp:Button>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div> 

            </div> 
            </div>
            <input id="btnCheckAvailability" runat="server" type="button" value="Cancel" style="display:none" />          
            </asp:Panel>

            <asp:ModalPopupExtender ID="mpPayLater" runat="server" BackgroundCssClass="roomtype-modalBackground"
                CancelControlID="aPayLaterClose" EnableViewState="true" PopupControlID="PnlPayLater"
                TargetControlID="btnPaylaterPopup">
            </asp:ModalPopupExtender>            
            <asp:Panel ID="PnlPayLater" style="display:none;"  runat="server">          
            <div id ="div41"  class="roomtype-price-breakuppopup" style="float:left;text-align:left;max-height: 250px;">
            <div align="left" id="Div42">
                <div id="div45" runat="server" style="border: 1px solid #fff; width: 700px;
                    height: 30px; background-color: #ede7e1; padding-right: 5px; margin-right: 10px;
                    text-align: center; vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">                    
                    <div></div>
                    <asp:Label ID="Label24" CssClass="room-type-breakup-headings" Text="Pay Later Confirmation" 
                        runat="server" Width="650px" ></asp:Label>                          
                         &nbsp; &nbsp;<a id="aPayLaterClose" href="#" class="roomtype-popupremarks-close" style="margin-left:665px;"></a>                   
                </div>
                 <div class="booking-form" id="Div46" runat ="server" style="background-color: White; padding: 1px; width: 700px">
                    <div class="row-col-12" style="padding-left: 2px;">   
                        <div style="margin-bottom:10px; margin-top:5px; margin-left:20px;">
                        <asp:Label ID="lblPayLaterValue"  style="font-family: 'Raleway'; font-weight: bold; font-size: 12px !important;text-decoration:none;"  runat="server"></asp:Label>  
                        <asp:HiddenField ID="hdnPayLaterAmount" runat="server" />
                        </div>
                        <div style="margin-bottom:10px; margin-top:3px; margin-left:20px;">
                        <asp:Label ID="lblPayLaterNote" style="font-family: 'Raleway'; font-weight: bold; font-size: 12px !important;text-decoration:none;"  runat="server"></asp:Label>  
                        </div>                
                        <div id="dvPayLaterMsg" runat="server" style="margin-bottom:10px; margin-top:3px; margin-left:20px;">
                        <asp:Label ID="lblPayLaterMsg" style="font-family: 'Raleway'; color:Red; font-weight: bold; font-size: 12px !important;text-decoration:none;"  runat="server"></asp:Label>  
                        </div>                
                        <div id="Div47" runat="server" style="font-size: 12px; padding-left: 10px; text-align:center; padding-top:3px; padding-bottom:3px;padding-right:7px">                            
                            <asp:Button id="btnPayLaterConfirm" runat="server" class="authorize-btn" Width="150px" Height="25px" Text="Confirm" ></asp:Button>
                        </div>                           
                    </div>
                    <div class="clear">
                    </div>
                </div> 

            </div> 
            </div>
            <input id="btnPaylaterPopup" runat="server" type="button" value="Cancel" style="display:none" />          
            </asp:Panel>

            </ContentTemplate> 
            </asp:UpdatePanel>
 


            <asp:ModalPopupExtender ID="mpRemarks" runat="server" BackgroundCssClass="roomtype-modalBackground"
                CancelControlID="aRemarksClose" EnableViewState="true" PopupControlID="pnlRemarks"
                TargetControlID="imgbRemarks">
            </asp:ModalPopupExtender>
            <asp:HiddenField ID="imgbRemarks" runat="server" />
            <asp:Panel runat="server" ID="pnlRemarks" Style="display: none;">
                <div id="dvremarks" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                    max-height: 500px;">
                    <div align="left" id="Div5">
                        <div id="Div1" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                            padding-right: 5px; margin-right: 10px; text-align: center; vertical-align: middle;
                            padding-top: 10px; margin-bottom: 0px;">
                            <asp:Label ID="lblheading" CssClass="room-type-breakup-headings" Text="Remarks" runat="server"></asp:Label>
                            &nbsp; &nbsp;<a id="aRemarksClose" href="#" class="roomtype-popupremarks-close"></a>
                        </div>
                        <div id="Div2" class="roomtype-price-breakuppopup" style="max-height: 500px; overflow: auto">
                            <div id="dvHotelRemarkslbl" runat="server" style="border: 1px solid #fff; width: 700px;
                                height: 30px; background-color: #ede7e1; padding-right: 5px; padding-left: 5px;
                                margin-right: 10px; text-align: left; vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblHotelRemarks" CssClass="room-type-breakup-headings" Text="Hotel Remarks"
                                    runat="server"></asp:Label>
                            </div>
                            <div class="booking-form">
                                <div class="booking-form">
                                    <div style="padding-left: 10px; margin-bottom: 10px; margin-top: 10px;" class="checkbox">
                                        <asp:CheckBoxList ID="chkHotelRemarks" CellSpacing="5" CellPadding="5" RepeatColumns="3"
                                            runat="server" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </div>
                                    <%--   </div>--%>
                                </div>
                            </div>
                            <div id="dvTxtHotRem" runat="server" style="padding-left: 10px; padding-top: 2px;
                                margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i" style="width: 690px; float: left; height: 34;">
                                        <div class="input input">
                                            <asp:TextBox ID="txthotremarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div id="dvCustRemarks" runat="server" style="border: 1px solid #fff; width: 700px;
                                height: 30px; background-color: #ede7e1; padding-right: 5px; padding-left: 5px;
                                margin-right: 10px; text-align: left; vertical-align: middle; padding-top: 10px;">
                                <asp:Label ID="lblCustRemarks" CssClass="room-type-breakup-headings" Text="Information to Customer"
                                    runat="server"> </asp:Label>
                            </div>
                            <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                <div class="booking-form">
                                    <div class="booking-form-i-b" style="width: 690px; float: left;">
                                        <div class="input input">
                                            <asp:TextBox ID="txtcustRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                            <div id="dvArrRemarks" runat="server">
                                <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                    padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                    vertical-align: middle; padding-top: 10px;">
                                    <asp:Label ID="lblArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival- Information to Operation"
                                        runat="server"> </asp:Label>
                                </div>
                                <div id="dvTxtArr" runat="server" style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                    <div class="booking-form">
                                        <div class="booking-form-i-b" style="width: 690px; float: left;">
                                            <div class="input input">
                                                <asp:TextBox ID="txtArrRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                    TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="dvDepRemarks" runat="server">
                                <div style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
                                    padding-right: 5px; padding-left: 5px; margin-right: 10px; text-align: left;
                                    vertical-align: middle; padding-top: 10px;">
                                    <asp:Label ID="lblDeptRemarks" CssClass="room-type-breakup-headings" Text="Departure- Information to Operation"
                                        runat="server"> </asp:Label>
                                </div>
                                <div style="padding-left: 10px; padding-top: 10px; margin-bottom: 0px;">
                                    <div class="booking-form">
                                        <div class="booking-form-i-b" style="width: 690px; float: left;">
                                            <div class="input input">
                                                <asp:TextBox ID="txtDeptRemarks" Style="border: none; width: 100%; font-size: 12px;"
                                                    TextMode="MultiLine" runat="server"></asp:TextBox></div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnrlineno" runat="server" />
                            <div class="booking-form-i-a" style="margin-left: 270px;">
                                <asp:Button ID="btnSaveRemarks" class="authorize-btn" Width="150px" runat="server"
                                    Text="Save" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:ModalPopupExtender ID="mpConfirm" runat="server" BackgroundCssClass="roomtype-modalBackground"
                CancelControlID="aConfirmClose" EnableViewState="true" PopupControlID="pnlConfirm"
                TargetControlID="imgbConfirm">
            </asp:ModalPopupExtender>

  <asp:HiddenField ID="imgbConfirm" runat="server" />
  
           <asp:Panel ID="pnlConfirm" style="display:none;"  runat="server" >
            <div align="left" id="Div7" >
                      <div id="dvheading"   runat="server" style="border: 1px solid #fff; width: 900px;
                                           height: 30px; background-color: #ede7e1; padding-right: 10px;  margin-right: 10px;
                                           text-align: center; vertical-align: middle; padding-top: 5px;margin-bottom:0px;">
                                         
                               <div class="chk-l"  style="margin-left:20px; font-size:small; " >Room Type &nbsp;&nbsp;&nbsp;</div>  <%--style="width:25%"--%>
							<div class="chk-r" style="font-size:12px;" ><asp:Label ID="lblConfirmHeading"    runat="server"></asp:Label>     &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </div> <%--style="width:75%"--%>  
                            <a id="aConfirmClose"   href="#" class="roomtype-popupconfirm-close"" ></a>
                            
             </div>
                <div id="dvConfirm"  runat="server"  class="roomtype-price-breakuppopup" style="float: left; width: 900px;
                    text-align: left; max-height:1000px; overflow: auto">
                    <div class="clear" style="padding-top: 20px">
                    </div>
                             <%--        <asp:UpdatePanel ID="upgvconfirmbooking" runat ="server" >
                          <ContentTemplate >--%>
                    <div class="page-search-content-Additional-Service">
                        &nbsp; &nbsp;&nbsp;
                        <div class="chk-l" style="font-size: 12px; margin-left: 20px; color: #4a90a4;">
                            Check In &nbsp;
                        </div>
                        <div class="chk-r" style="font-size: 12px; padding-left: 20px">
                            <asp:Label ID="lblCheckInDate" ReadOnly="true" runat="server"></asp:Label><asp:Label
                                ID="lblrooms" Visible="false" runat="server"></asp:Label>
                                <asp:Label
                                ID="lblPersons" Visible="false" runat="server"></asp:Label>
                                 <asp:Label
                                ID="lbldlrlineno" Visible="false" runat="server"></asp:Label>
                        </div>
                        <div class="chk-l" style="margin-left: 40px; font-size: 12px; color: #4a90a4;">
                            Check Out &nbsp;&nbsp;
                        </div>
                        <div class="chk-r" style="font-size: 12px; padding-left: 10px">
                            <asp:Label ID="lblCheckOutDate" ReadOnly="true" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="upchkapplysame" runat="server" EnableViewState="true">
                            <ContentTemplate>
                               
                           
                                    <div id="dvchkapply" runat="server" style="font-size: 12px; padding-left: 10px;
                                        margin-left: 620px; margin-top: -20px">
                                   
                               
                                        <asp:CheckBox runat="server" class="checkbox"  ID="chkApplySameConf" /> 
                                    </div>
                               
                                <div class="chk-r" style="font-size: 12px; padding-left: 10px; margin-left: 650px;
                                    margin-top: -20px">
                                    <asp:Label ID="lblapplyconfirm" Style="font-size: 12px;" runat="server" Text="Apply Same Confirmation to all Rooms"></asp:Label>
                                </div>
                                <asp:Button ID="btnApplySameConfChk" runat="server" Style="display: none;" Text="Filter" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                       <asp:UpdatePanel ID="upgvconfirmbooking" runat ="server" >
                          <ContentTemplate >
                        <div id="dvCancelheading" runat="server" style="border: 1px solid #fff; width: 383px;
                            height: 25px; background-color: #ede7e1; padding-right: 5px; margin-left: 510px;
                            margin-top: 15px; text-align: center; vertical-align: middle; padding-top: 5px;
                            margin-bottom: 0px;">
                            <asp:Label ID="lblcancelheading" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                Text="Cancel Without Charge" runat="server"></asp:Label>
                        </div>
                     
                        <div class="booking-form" id="dvgvConfirmBooking" runat ="server" style="background-color: White; padding: 5px;">
                            <div class="row-col-12" style="padding-left: 10px;">
                          
                                <asp:GridView ID="gvConfirmBooking" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                    Width="100%" OnRowDataBound="gvConfirmBooking_RowDataBound" GridLines="Horizontal">
                                    <Columns>
                                      <asp:TemplateField HeaderText="RowNumber"  Visible ="false">   
         <ItemTemplate>
          <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> '  runat="server"></asp:Label>
                 
         </ItemTemplate>
     </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Room No." ><%--ControlStyle-Width="80px"--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoOfRooms" Text='<%# Eval("RoomNumber") %>' runat="server"></asp:Label>
                                                <%-- <asp:TextBox ID="txtNoOfPax" Text='<%# Eval("noofpax") %>' runat="server"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Room Occupancy" ><%--ControlStyle-Width="100px"--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRoomOcc" Text='<%# Eval("RoomOccupancy") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lead Guest" ><%--ControlStyle-Width="110px"--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hotel Confirmation No." ><%--ControlStyle-Width="120px"--%>
                                            <ItemTemplate>
                                                <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                    <div class="input input" id="dvhotelconfno" runat="server">
                                                        <asp:TextBox ID="txthotelconfno" Style="border: none;font-size:12px; width: 100px;" 
                                                             runat="server"> </asp:TextBox><%--Text='<%# Eval("hotelconfno").ToString()%> '--%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Confirm Date">
                                            <ItemTemplate>
                                  
                                       
                                                <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                    <asp:TextBox ID="txtConfirmDate" Style="border: none;"  
                                                        class="date-inpt-check-in"   Text='<%# Eval("confirmdate") %>'  placeholder="dd/mm/yyyy" runat="server"></asp:TextBox><%-- Text='<%# Eval("confirmdate") %>'--%>
                                                    <span class="date-icon" ></span>
                                                     <asp:HiddenField ID="hdnConfDate" runat="server" />
                                                </div>
                                             
                                          
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel Days">
                                            <ItemTemplate>
                                                <div class="booking-form-i-a" style="width: 40px; float: left; height: 10px;">
                                                    <div id="dvdays" runat="server" class="input input" >
                                                        <asp:TextBox ID="txtCancelDays"  style="text-align:right;font-size:12px;" onchange="ChangeDate();" onkeypress="validateDecimalOnly(event,this)"
                                                            Width="22px" Text='<%# Eval("CancelDays")%>'   runat="server"></asp:TextBox> 
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Time Limit" ControlStyle-Width="115px">
                                            <ItemTemplate>
                                                <div id="dvtimelimitdate" runat="server" class="input-a" style="width: 115px; padding-bottom: 9px">
                                                    <asp:TextBox ID="txtTimeLimitDate"  class="date-inpt-check-in"
                                                        placeholder="dd/mm/yyyy"  runat="server"></asp:TextBox> <%--Text='<%# Eval("TimeLimitDate") %>'--%>
                                                    <span class="date-icon"></span>
                                                    <asp:HiddenField ID="hdtimelimit" runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="booking-form-i-a" style="width: 60px; float: left; height: 10px;padding-right:3px">
                                                    <div   id="dvtimelimittime" runat="server" class="input input" >
                                                        <input id="txtTimeLimitTime" placeholder="hh:mm"  class="time-picker" style="border: none; width: 38px;font-size:12px;"  runat="server" /> <%-- value='<%# Eval("TimeLimitTime") %>'--%>
                                                    </div> 
                                                </div>
                                            </ItemTemplate>


                                        </asp:TemplateField>
                 
                   
                                        <asp:TemplateField HeaderText="Prev. Confirmation No." ControlStyle-Width="125px"  >
                                            <ItemTemplate>
                                                <div  id ="PrevConfNo"  runat ="server" class="booking-form-i-a" style="width: 115px; float: left; height: 10px;">
                                                    <div class="input input">
                                                        <asp:TextBox ID="txtPrevConfNo"  Style="width: 95px;font-size:12px;" 
                                                            runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="hdnPrevConfNo"  runat="server"></asp:HiddenField>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                    <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                </asp:GridView>
                             
                            </div>
                        </div>
                  
                        </ContentTemplate>
                         </asp:UpdatePanel>
                            
                    </div>
                    <div class="clear" style="padding-top: 20px">
                    </div>
                    <div class="booking-form-i-a" style="margin-left: 380px;">
                        <asp:Button ID="btnsaveconfirm" class="authorize-btn" Width="100px" runat="server"
                            Text="Save" />
                    </div>
                    <input id="btnInvisible" runat="server" type="button" value="Cancel" style="visibility: hidden" />
                <input id="txttodaydate" runat="server" type="text" style="width: 0px; height: 0px;
                    visibility: hidden;" />
                <input id="txtminyear" runat="server" type="text" style="width: 0px; height: 0px;
                    visibility: hidden;" />
                <input id="txtyear" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />



            </div>
        </div>
       
            </asp:Panel>
            <div class="sp-page-r">
                <div style="float: left;">
                    <input id="btnTabShowAll" onclick="btnTabShowAllClick(this)" type="button" class="guest-addrow-generate"
                        value="Show All" />
                </div>
                  <div style="float:right;"> <asp:Label ID="lblEditRequestId1" CssClass="additional-service-heading-label" Text="REQUEST ID: " runat="server"></asp:Label> <asp:Label ID="lblEditRequestId"  CssClass="additional-service-heading-label"  runat="server"></asp:Label></div>
                <div class="clear" ></div>
                   <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div class="ui-widget collapse"  id="dvTabHotelSummary" runat="server" >
                            <div class="ui-widget-header">
                                <div style="float: left; margin: 5px; width: 40%;">
                                    <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                                        style="padding-top: 15px;">ACCOMMODATION </span>
                                </div>
                                <div style="float: left; margin: 5px; padding-left: 40px; padding-top: 5px;">
                                    <span style="font-size: 12px !important;">
                                        <asp:Label ID="lblTabHotelTotalPrice1" Text="TOTAL PRICE:" runat="server"></asp:Label></span>
                                </div>
                                <div style="float: right; margin: 5px;">
                                    <span style="float: right; padding-right: 15px; padding-top: 5px; font-size: 12px !important;">
                                        <asp:Label ID="lblTabHotelTotalPrice" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="ui-helper-clearfix">
                                </div>
                            </div>
                <div class="ui-widget-content"  id="dvHotelTab1" style="display: none;">
                <asp:DataList ID="dlBookingSummary" Width="100%" runat="server">
                    <ItemTemplate>
                        <div class="checkout-coll">
                            <div class="checkout-head">
                                <div class="checkout-headrb">
                                    <div class="checkout-headrp">
                                        <div class="chk-left">
                                            <div class="guest-summary-heading">
                                                <asp:Label ID="lblHotelName" Text='<%# Eval("partyname") %>' runat="server"></asp:Label>
                                                 <asp:HiddenField runat="server" Value='<%# Eval("cancelled") %>'  ID="hdCancelled" />
                                            </div>
                                            <div class="chk-lbl-a">
                                                <asp:Label ID="lblLocation" Text='<%# Eval("sectorname") %>' runat="server"></asp:Label></div>
                                            <asp:Label ID="lblStars" Text='<%# Eval("noofstar") %>' Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblrlineno" Text='<%# Eval("rlineno") %>' Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblchkindateformat" Text='<%# Eval("checkindate") %>' Visible="false"
                                                runat="server"></asp:Label>
                                            <asp:Label ID="lblchkoutdateformat" Text='<%# Eval("checkoutdate") %>' Visible="false"
                                                runat="server"></asp:Label>
                                            <asp:Label ID="lblnoofrooms" Text='<%# Eval("noofrooms") %>' Visible="false" runat="server"></asp:Label>
                                            <div id="dvHotelStars" runat="server">
                                            </div>
                                            <div class="clear">
                                            </div>
                                            </nav>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="chk-lines">
                                <div class="chk-line">
                                    <div class="chk-nights" style="float: left;">
                                        <asp:Label ID="lblNoOfNights" Text='<%# Eval("nights") %>' runat="server"></asp:Label></div>
                                    <div class="chk-dates">
                                        <asp:Label ID="lblDates" Text='<%# Eval("bookDate") %>' runat="server"></asp:Label></div>
                                </div>
                                <div class="chk-line">
                                    <div style="float: left; margin-right: 10px;">
                                        <span class="chk-room-left">NO OF ROOMS:</span>
                                        <asp:Label ID="Label8" Text='<%# Eval("noofrooms") %>' runat="server"></asp:Label>
                                    </div>
                                    <div style="float: left;">
                                        <span class="chk-room-left">OCCUPANCY: </span><span class="chk-persons"></span>
                                        <asp:Label ID="lblNoOfPersons" Text='<%# Eval("persons") %>' runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="chk-details">
                                <h2>
                                    Details</h2>
                                <div class="chk-detais-row">
                                    <div class="chk-line">
                                        <div class="chk-l" style="width: 25%">
                                            Room type &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                        <div class="chk-r" style="width: 75%">
                                            <asp:Label ID="lblRoomType" Text='<%# Eval("rmtypname") %>' runat="server"></asp:Label></div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                      <div id="dvRoomValue" runat="server" class="line-bottom" style="padding:5px 0px 5px 0px !important;">
                                        <div class="chk-total-l" style="width: 30%">
                                            Room Value</div>
                                        <div class="chk-total-r" style="width: 30%">
                                            <asp:Label ID="Label26" Text='<%# Eval("totalPrice") %>' runat="server"></asp:Label></div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                      <div id="dvSplEvents" runat="server" class="chk-detais-row">
                                       <div class="clear" style="padding-bottom:15px;">
                                        </div>
                                       <h2>  Special Events Details</h2>
                                     
                                        <div class="chk-line1">
                                      
                                            <asp:DataList ID="dlSpecialEventsSummary" OnItemDataBound="dlSpecialEventsSummary_ItemDataBound" Width="100%" runat="server">
                                            <ItemTemplate>
                                                <div>
                                                    <div class="chk-line">
                                                        <div class="chk-l" style="width: 30%">
                                                            Event Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                        <div class="chk-r" style="width: 70%">
                                                            <asp:Label ID="lblsplEventDate" Text='<%# Eval("spleventdate") %>' runat="server"></asp:Label></div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                    <div class="chk-line">
                                                        <div class="chk-l" style="width: 30%">
                                                            Event Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                        <div class="chk-r" style="width:70%">
                                                            <asp:Label ID="lblsplEventName" Text='<%# Eval("spleventname") %>' runat="server"></asp:Label></div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                     <div id="dvSplEventValue" runat="server" class="chk-line">
                                                        <div class="chk-l" style="width:30%">
                                                            Event Value &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                        <div class="chk-r" style="width:70%">
                                                            <asp:Label ID="lblsplEventValue" Text='<%# Eval("spleventvalue") %>' runat="server"></asp:Label></div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            </asp:DataList>

                                        </div>
                                   </div>
                                <div class="chk-total" id="dvTourTotal" runat="server">
                                    <div class="chk-total-l">
                                        Total Price</div>
                                    <div class="chk-total-r">
                                        <asp:Label ID="lblTotalPrice" Text='<%# Eval("TotalPriceWithSE") %>' runat="server"></asp:Label> 
                                          </div>
                                        
                                    <div class="clear">
                                    </div>
                                </div>
                                   <div id="dvHotelCancelled" runat="server" class="chk-line" style="padding-bottom: 15px; padding-top: 10px; margin-top: 5px;">
                                    <div style="background-color:#F2F3F4;text-align:center;padding:5px;">
                                     <asp:Label ID="Label25" CssClass="summary-cancelled-label" Text="Booking Status: Cancelled" runat="server"></asp:Label></div>
                                    </div>
                                <div  id="dvHotelButtons" runat="server"  class="chk-line" style="padding-bottom: 45px; padding-top: 10px; margin-top: 5px;">
                                    <div style="float: left;">
                                        <div style="float: left; padding-right: 10px; padding-top: 5px;display:none" >
                                            <asp:ImageButton ID="imgbEdit"  ImageUrl="~/img/button_amend.png" ToolTip="Amend"
                                                runat="server" /></div>
                                        <div style="float: left; padding-top: 5px; padding-right: 10px;display:none">
                                            <asp:ImageButton ID="imgbDelete"  ImageUrl="~/img/button_remove.png"
                                                ToolTip="Remove" runat="server" /></div>
                                        <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                            <asp:ImageButton ID="imgbRemarks"  ImageUrl="~/img/button_remarks.png"
                                                ToolTip="Remarks" runat="server" OnClick="imgbRemarks_Click" /></div>
                                        <div style="float: left; padding-top: 5px; padding-right: 10px;" id="dvAmend" runat="server">
                                            <asp:ImageButton ID="imgbConfirm" ImageUrl="~/img/button_confirm.png"
                                                ToolTip="Confirm" runat="server" OnClick="imgbConfirm_Click" /></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                  </div>
            </div>
            </ContentTemplate></asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="ui-widget collapse" id="dvTabTourSummary" runat="server">
                        
                                    <div class="ui-widget-header">
                     <div style="float: left; margin: 5px;width:40%;">
                         <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                             style="padding-top: 15px;"> TOURS </span>
                     </div>
                     <div style="float: left; margin: 5px; padding-left: 40px; padding-top: 5px;">
                         <span style="font-size: 12px !important;">
                             <asp:Label ID="lblTourTabTotalPrice1" Text="TOTAL PRICE:" runat="server"></asp:Label></span>
                     </div>
                     <div style="float: right; margin: 5px;">
                         <span style="float: right; padding-right: 15px; padding-top: 5px; font-size: 12px !important;">
                                 <asp:Label ID="lblTourTabTotalPrice" runat="server"></asp:Label>
                         </span>
                     </div>
                     <div class="ui-helper-clearfix">
                     </div>
                </div>

                            <div class="ui-widget-content"  id="dvTourTab1"  style="display: none;">
                                <div id="dvTourSummarry" runat="server" style="width: 100%">
                                    <div class="checkout-coll">
                                        <div class="checkout-head">
                                            <div class="checkout-headrb">
                                                <div class="checkout-headrp">
                                                    <div class="chk-left">
                                                        <div class="guest-summary-heading">
                                                            <asp:Label ID="lblTourHeading" Text="Tours" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        </nav>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div>
                                            <div class="clear">
                                            </div>
                                            <div class="chk-details" style="margin-top: -15px;">
                                                <asp:DataList ID="dlTourSummary" Width="100%" runat="server">
                                                    <ItemTemplate>
                                                        <div class="chk-detais-row">
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Excursion Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                       <asp:Label ID="lblExcursionCode" style="display:none;" Text='<%# Eval("exctypcode") %>' runat="server"></asp:Label>
                                                             <asp:Label ID="lblCombo" style="display:none;" Text='<%# Eval("combo") %>' runat="server"></asp:Label>
                                                              <asp:Label ID="lblmultipleDates" style="display:none;" Text='<%# Eval("multipledatesyesno") %>' runat="server"></asp:Label>
                                                               <asp:Label ID="lblRequestId" style="display:none;" Text='<%# Eval("RequestId") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblExcursionName" Text='<%# Eval("exctypname") %>' runat="server"></asp:Label>
                                                                       <asp:Label ID="lblelineno" Visible="false" Text='<%# Eval("elineno") %>' runat="server"></asp:Label>
                                                                       </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Excursion Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="lblexcursiondate" Text='<%# Eval("excdate") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div id="dvACS" runat="server">
                                                                    <asp:HiddenField runat="server" Value='<%# Eval("ratebasis") %>' ID="hdRateBasisSummary" />
                                                                    <div class="chk-l" style="">
                                                                        Adults</div>
                                                                    <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                        <asp:Label ID="lblAdults" Text='<%# Eval("adults") %>' runat="server"></asp:Label></div>
                                                                    <div class="chk-l" style="padding-left: 10px;">
                                                                        Child</div>
                                                                    <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                        <asp:Label ID="lblChild" Text='<%# Eval("child") %>' runat="server"></asp:Label></div>
                                                                    <div class="chk-l" style="padding-left: 10px;">
                                                                        Senior</div>
                                                                    <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                        <asp:Label ID="lblSenior" Text='<%# Eval("senior") %>' runat="server"></asp:Label></div>
                                                                    <div id="dvTourACSSalevalue" runat="server">
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Sale Value</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label1" Text='<%# Eval("totalsalevalueNew") %>' runat="server"></asp:Label></div>
                                                                    </div>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                                <div id="dvUnit" runat="server">
                                                                    <div class="chk-l" style="">
                                                                        Units</div>
                                                                    <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                        <asp:Label ID="lblUnits" Text='<%# Eval("unitnew") %>' runat="server"></asp:Label></div>
                                                                    <div id="dvTourUnitSalevalue" runat="server">
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Sale Value</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label2" Text='<%# Eval("totalsalevalueNew") %>' runat="server"></asp:Label></div>
                                                                    </div>
                                                                </div>
                                                
                                                         <div class="chk-line" style="padding-bottom: 45px; padding-top: 10px; margin-top: 5px;">
                                        <div style="float: left;">
                                            <div style="float: left; padding-right: 10px; padding-top: 5px;">
                                                <asp:ImageButton ID="ImgToursRemarks"  ImageUrl="~/img/button_remarks.png"
                                                    ToolTip="Remarks" runat="server" OnClick="imgToursRemarks_Click" />
                                            </div>
                                            <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                            </div>
                                            <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                            </div>
                                            <div style="float: left; padding-top: 5px; padding-right: 10px;" id="dvAmend" runat="server">
                                            </div>
                                        </div>
                                    </div>

                                                </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <div class="line-bottom">
                                                    <div class="chk-total" id="dvTourTotal" runat="server">
                                                        <div class="chk-total-l">
                                                            Total Price</div>
                                                        <div class="chk-total-r">
                                                            <asp:Label ID="lblTourTotalPrice" runat="server"></asp:Label></div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ui-widget collapse" id="dvTabAirportSummary" runat="server">
                     
                                    <div class="ui-widget-header">
                     <div style="float: left; margin: 5px;width:40%;">
                         <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                             style="padding-top: 15px;"> AIRPORT SERVICE</span>
                     </div>
                     <div style="float: left; margin: 5px; padding-left: 40px; padding-top: 5px;">
                         <span style="font-size: 12px !important;">
                             <asp:Label ID="lblAirportTabtotalPrice1" Text="TOTAL PRICE:" runat="server"></asp:Label></span>
                     </div>
                     <div style="float: right; margin: 5px;">
                         <span style="float: right; padding-right: 15px; padding-top: 5px; font-size: 12px !important;">
                           <asp:Label ID="lblAirportTabtotalPrice" runat="server"></asp:Label>
                         </span>
                     </div>
                     <div class="ui-helper-clearfix">
                     </div>
                </div>

                            <div class="ui-widget-content"  id="dvAirportTab1"   style="display: none;">
                                <div id="dvAirportSummary" runat="server" style="width: 100%">
                                    <div class="checkout-coll">
                                        <div class="checkout-head">
                                            <div class="checkout-headrb">
                                                <div class="checkout-headrp">
                                                    <div class="chk-left">
                                                        <div class="guest-summary-heading">
                                                            <asp:Label ID="Label4" Text="Airport Services" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        </nav>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div>
                                            <div class="clear">
                                            </div>
                                            <div class="chk-details" style="margin-top: -15px;">
                                                <asp:DataList ID="dlAirportSummary" Width="100%" runat="server">
                                                    <ItemTemplate>
                                                        <div class="chk-detais-row">
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Service Type &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="lblairporttype" Text='<%# Eval("airportmatype") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Service Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="lblservicedate" Text='<%# Eval("servicedate") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Service Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="lblairservicename" Text='<%# Eval("servicename") %>' runat="server"></asp:Label>
                                                                    
                                                                     <asp:Label ID="lblalineno" Visible="false" Text='<%# Eval("alineno") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Service details &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="Label6" Text='<%# Eval("servicedetail") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblratebasis" Text='<%# Eval("ratebasis") %>' runat="server" Style="display: none"></asp:Label>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom" id="divunit" runat="server">
                                                                <div>
                                                                    <div class="chk-l" style="">
                                                                        no of units</div>
                                                                    <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                        <asp:Label ID="lblAdults" Text='<%# Eval("units") %>' runat="server"></asp:Label></div>
                                                                    <div id="dvAirportUnitPrice" runat="server">
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Unit Price</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label7" Text='<%# Eval("unitpriceNew") %>' runat="server"></asp:Label></div>
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Sales Value</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label1" Text='<%# Eval("totalsalevalueNew") %>' runat="server"></asp:Label></div>
                                                                    </div>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom" id="divaddtional" runat="server">
                                                                <div>
                                                                    <div class="chk-l" style="">
                                                                        Additional Pax</div>
                                                                    <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                        <asp:Label ID="lbladdpax" Text='<%# Eval("addlpax") %>' runat="server"></asp:Label></div>
                                                                    <div id="dvaddPaxPrice" runat="server">
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Price</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label10" Text='<%# Eval("addlpaxprice") %>' runat="server"></asp:Label></div>
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Pax Value</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label11" Text='<%# Eval("addlpaxsalevalue") %>' runat="server"></asp:Label></div>
                                                                    </div>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom" runat="server" id="divadults">
                                                                <div id="div6" runat="server">
                                                                    <div class="chk-l" style="">
                                                                        No.Of.Adults</div>
                                                                    <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                        <asp:Label ID="Label12" Text='<%# Eval("adults") %>' runat="server"></asp:Label></div>
                                                                    <div id="dvadultPrice" runat="server">
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Price</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label13" Text='<%# Eval("adultprice") %>' runat="server"></asp:Label></div>
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            AdultValue</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label14" Text='<%# Eval("adultsalevalue") %>' runat="server"></asp:Label></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                            <div class="line-bottom" runat="server" id="divchild">
                                                                <div id="div9" runat="server">
                                                                    <div class="chk-l" style="">
                                                                        No.Of.Childs</div>
                                                                    <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                        <asp:Label ID="lblchild" Text='<%# Eval("child") %>' runat="server"></asp:Label></div>
                                                                    <div id="dvChildprice" runat="server">
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Price</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label16" Text='<%# Eval("childprice") %>' runat="server"></asp:Label></div>
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Child Value</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label17" Text='<%# Eval("childsalevalue") %>' runat="server"></asp:Label></div>
                                                                    </div>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="chk-line" style="padding-bottom: 45px; padding-top: 10px; margin-top: 5px;">
                                            <div style="float: left;">
                                                <div style="float: left; padding-right: 10px; padding-top: 5px;">
                                                    <asp:ImageButton ID="ImgAirportmaRemarks"  ImageUrl="~/img/button_remarks.png"
                                                        ToolTip="Remarks" runat="server" OnClick="ImgAirportmaRemarks_click" />
                                                </div>
                                                <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                                </div>
                                                <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                                </div>
                                                <div style="float: left; padding-top: 5px; padding-right: 10px;" id="dvAmend" runat="server">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <div class="line-bottom">
                                                    <div class="chk-total" id="dvairportTotal" runat="server">
                                                        <div class="chk-total-l">
                                                            Total Price</div>
                                                        <div class="chk-total-r">
                                                            <asp:Label ID="lblairporttotal" runat="server"></asp:Label></div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="ui-widget collapse" id="dvTabTransfersummary" runat="server">
                            <div class="ui-widget-header">
                                <div style="float: left; margin: 5px; width: 40%;">
                                    <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                                        style="padding-top: 15px;">TRANSFERS </span>
                                </div>
                                <div style="float: left; margin: 5px; padding-left: 40px; padding-top: 5px;">
                                    <span style="font-size: 12px !important;">
                                        <asp:Label ID="lblTransferTabTotalPrice1" Text="TOTAL PRICE:" runat="server"></asp:Label></span>
                                </div>
                                <div style="float: right; margin: 5px;">
                                    <span style="float: right; padding-right: 15px; padding-top: 5px; font-size: 12px !important;">
                                        <asp:Label ID="lblTransferTabTotalPrice" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="ui-helper-clearfix">
                                </div>
                            </div>


                            <div class="ui-widget-content"   id="dvTransferTab1"  style="display: none;">
                                <div id="dvTransferSummary" runat="server" style="width: 100%">
                                    <div class="checkout-coll">
                                        <div class="checkout-head">
                                            <div class="checkout-headrb">
                                                <div class="checkout-headrp">
                                                    <div class="chk-left">
                                                        <div class="guest-summary-heading">
                                                            <asp:Label ID="Label3" Text="Transfers" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        </nav>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div>
                                            <div class="clear">
                                            </div>
                                            <div class="chk-details" style="margin-top: -15px;">
                                                <asp:DataList ID="dlTransferSummary" Width="100%" runat="server">
                                                    <ItemTemplate>
                                                        <div class="chk-detais-row">
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Transfer Type &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="lblExcursionName" Text='<%# Eval("transfertype") %>' runat="server"></asp:Label>
                                                                       <asp:Label ID="lbltlineno" Visible="false" Text='<%# Eval("tlineno") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Transfer Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="lbltransdate" Text='<%# Eval("transferdate") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    vehicle Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="lblvehiclename" Text='<%# Eval("vehiclename") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Transfer details &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="Label6" Text='<%# Eval("transferdetail") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div id="Div4" runat="server">
                                                                    <div class="chk-l" style="">
                                                                        no of units</div>
                                                                    <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                        <asp:Label ID="lblAdults" Text='<%# Eval("units") %>' runat="server"></asp:Label></div>
                                                                    <div id="dvTransferUnitPrice" runat="server">
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Unit Price</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label7" Text='<%# Eval("unitpriceNew") %>' runat="server"></asp:Label></div>
                                                                        <div class="chk-l" style="padding-left: 10px;">
                                                                            Sales Value</div>
                                                                        <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="Label1" Text='<%# Eval("totalsalevalueNew") %>' runat="server"></asp:Label></div>
                                                                    </div>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                                              <div class="chk-line" style="padding-bottom: 45px; padding-top: 10px; margin-top: 5px;">
                                                <div style="float: left;">
                                                    <div style="float: left; padding-right: 10px; padding-top: 5px;">
                                                        <asp:ImageButton ID="ImgtrfsRemarks"  ImageUrl="~/img/button_remarks.png"
                                                            ToolTip="Remarks" runat="server" OnClick="ImgtrfsRemarks_Click" />
                                                    </div>
                                                    <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                                    </div>
                                                    <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                                    </div>
                                                    <div style="float: left; padding-top: 5px; padding-right: 10px;" id="dvAmend" runat="server">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <div class="line-bottom">
                                                    <div class="chk-total" id="dvTransferTotal" runat="server">
                                                        <div class="chk-total-l">
                                                            Total Price</div>
                                                        <div class="chk-total-r">
                                                            <asp:Label ID="lblTransfertotal" runat="server"></asp:Label></div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ui-widget collapse" id="dvTabVisaSummary" runat="server">
                            <div class="ui-widget-header">
                                <div style="float: left; margin: 5px; width: 40%;">
                                    <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                                        style="padding-top: 15px;">VISA </span>
                                </div>
                                <div style="float: left; margin: 5px; padding-left: 40px; padding-top: 5px;">
                                    <span style="font-size: 12px !important;">
                                        <asp:Label ID="lblVisaTabTotalPrice1" Text="TOTAL PRICE:" runat="server"></asp:Label></span>
                                </div>
                                <div style="float: right; margin: 5px;">
                                    <span style="float: right; padding-right: 15px; padding-top: 5px; font-size: 12px !important;">
                                        <asp:Label ID="lblVisaTabTotalPrice" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="ui-helper-clearfix">
                                </div>
                            </div>

                            <div class="ui-widget-content"  id="dvVisaTab1"  style="display: none; background-color: White;">
                                <div id="dvVisaSummary" runat="server" style="margin-left: 20px;">
                     <%--               <div class="checkout-head">
                                        <div class="checkout-headrb">
                                            <div class="checkout-headrp">
                                                <div class="chk-left">
                                                    <div class="guest-summary-heading">
                                                        <asp:Label ID="lblVisaHeading" Visible="false" Text="VISA DETAILS" runat="server"></asp:Label></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                      <div class="checkout-head">
                        <div class="checkout-headrb">
                            <div class="checkout-headrp">
                                <div class="chk-left">
                                    <div class="guest-summary-heading">
                                        <asp:Label ID="lblVisaHeading" Text="VISA DETAILS"   Visible="false"  runat="server"></asp:Label>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    </nav>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                                    <asp:DataList ID="dlVisaSummary" Width="100%" runat="server">
                                        <ItemTemplate>
                                            <div class="line-bottom">
                                                <div class="chk-details">
                                                 
                                                    <div class="chk-l" style="width: 25%">
                                                        Visa type &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                    <div class="chk-r" style="width: 75%">
                                                        <asp:Label ID="lblVisaTypeName" Text='<%# Eval("visatypename") %>' runat="server"></asp:Label>
                                                               <asp:Label ID="lblvlineno" Text='<%# Eval("vlineno") %>' Visible="false" runat="server">
                                    </asp:Label></div>
                                                </div>
                                               
                                            <div class="clear">
                            </div>
                        </div>
                        <div class="line-bottom">
                            <div class="chk-l" style="width: 25%; padding-bottom: 5px;">
                                Visa Date &nbsp;&nbsp;</div>
                            <div class="chk-r" style="width: 75%; padding-bottom: 5px;">
                                <asp:Label ID="lblvisadate" Text='<%# Eval("visadate") %>' runat="server"></asp:Label></div>
                            <div class="clear">
                            </div>
                        </div>
                                            <div class="line-bottom" style="float: left; width: 100%;">
                                                <div class="chk-l" style="width: 25%">
                                                    Nationality &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                <div class="chk-r" style="width: 75%">
                                                    <asp:Label ID="lblNationality" Text='<%# Eval("ctryname") %>' runat="server"></asp:Label></div>
                                            </div>
                                          
                                            <div class="line-bottom" style="float: left; width: 100%;">
                                                <div style="width: 30%; float: left;">
                                                    <div class="chk-l">
                                                        <span>NO. OF VISAS </span>
                                                    </div>
                                                    <div class="chk-r" style="margin-left: 5px;">
                                                        <asp:Label ID="lblNoOfPax" Text='<%# Eval("noofpax") %>' runat="server"></asp:Label></div>
                                                </div>
                                                <div id="dvVisaSalevalue" runat="server">
                                                    <div style="width: 35%; float: left;">
                                                        <div class="chk-l">
                                                            <span>Visa Price</span></div>
                                                        <div class="chk-r" style="margin-left: 5px;">
                                                            <asp:Label ID="lblVisaPrice" Text='<%# Eval("visaprice") %>' runat="server"></asp:Label></div>
                                                    </div>
                                                    <div style="width: 35%; float: left;">
                                                        <div class="chk-l">
                                                            <span>Visa Value</span></div>
                                                        <div class="chk-r" style="margin-left: 5px;">
                                                            <asp:Label ID="lblVisaValue" class="chk-r" Text='<%# Eval("visavalue") %>' runat="server"></asp:Label></div>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                                                 <div class="chk-line" style="padding-bottom: 45px; padding-top: 10px; margin-top: 5px;">
                                <div style="float: left;">
                                    <div style="float: left; padding-right: 10px; padding-top: 5px;">
                                        <asp:ImageButton ID="ImgVisaRemarks"  ImageUrl="~/img/button_remarks.png"
                                            ToolTip="Remarks" runat="server" OnClick="ImgVisaRemarks_Click" />
                                    </div>
                                    <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                    </div>
                                    <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                    </div>
                                    <div style="float: left; padding-top: 5px; padding-right: 10px;" id="dvAmend" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                                       
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <div class="line-bottom" style="margin-top: 15px;">
                                        <div class="chk-total" id="dvVisatotal" runat="server">
                                            <div style="width: 75%" class="chk-total-l">
                                                &nbsp;&nbsp;<asp:Label ID="lblVisaTotal" Text="TOTAL PRICE" Visible="false" runat="server"></asp:Label></div>
                                            <div style="width: 25%" class="chk-total-r" style="margin-left: 0px;">
                                                <asp:Label ID="lblVIsaTotalPrice" runat="server"></asp:Label></div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="chk-line-bottom">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ui-widget collapse" id="dvTabOtherServicesSummary" runat="server">
                            <div class="ui-widget-header">
                                <div style="float: left; margin: 5px; width: 40%;">
                                    <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                                        style="padding-top: 15px;">OTHER SERVICES </span>
                                </div>
                                <div style="float: left; margin: 5px; padding-left: 40px; padding-top: 5px;">
                                    <span style="font-size: 12px !important;">
                                        <asp:Label ID="lblOtherServiceTabTotalPrice1" Text="TOTAL PRICE:" runat="server"></asp:Label></span>
                                </div>
                                <div style="float: right; margin: 5px;">
                                    <span style="float: right; padding-right: 15px; padding-top: 5px; font-size: 12px !important;">
                                        <asp:Label ID="lblOtherServiceTabTotalPrice" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="ui-helper-clearfix">
                                </div>
                            </div>

                            <div class="ui-widget-content"   id="dvOtherTab1"  style="display: none;">
                                <div id="dvOtherSummary" runat="server" style="width: 100%">
                                    <div class="checkout-coll">
                                        <div class="checkout-head">
                                            <div class="checkout-headrb">
                                                <div class="checkout-headrp">
                                                    <div class="chk-left">
                                                        <div class="guest-summary-heading">
                                                            <asp:Label ID="Label15" Text="Other Services" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        </nav>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div>
                                            <div class="clear">
                                            </div>
                                            <div class="chk-details" style="margin-top: -15px;">
                                                <asp:DataList ID="dlOtherSummary" Width="100%" runat="server">
                                                    <ItemTemplate>
                                                        <div class="chk-detais-row">
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Service Type &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="lblairporttype" Text='<%# Eval("servicedetail") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Service Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="lblothservicedate" Text='<%# Eval("servicedate") %>' runat="server"></asp:Label></div>
                                                                <div class="clear">
                                                                </div>
                                                            </div>
                                                            <div class="line-bottom">
                                                                <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                    Service Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                    <asp:Label ID="Label5" Text='<%# Eval("servicename") %>' runat="server"></asp:Label>
                                                                         <asp:Label ID="lblolineno" Visible="false" Text='<%# Eval("olineno") %>' runat="server"></asp:Label>
                                                                    </div>
                                                                <div class="clear">
                                                                </div>
                                                                <div class="line-bottom" id="divunit" runat="server">
                                                                    <div>
                                                                        <div class="chk-l" style="">
                                                                            no of units</div>
                                                                        <div class="chk-dates" style="float: left; padding-left: 5px;">
                                                                            <asp:Label ID="lblAdults" Text='<%# Eval("units") %>' runat="server"></asp:Label></div>
                                                                        <div id="dvothUnitPrice" runat="server">
                                                                            <div class="chk-l" style="padding-left: 10px;">
                                                                                Price</div>
                                                                            <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                                <asp:Label ID="Label7" Text='<%# Eval("unitprice") %>' runat="server"></asp:Label></div>
                                                                            <div class="chk-l" style="padding-left: 10px;">
                                                                                Sale Value</div>
                                                                            <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                                <asp:Label ID="Label1" Text='<%# Eval("unitsalevalue") %>' runat="server"></asp:Label></div>
                                                                        </div>
                                                                        <div id="dvothwlUnitPrice" runat="server">
                                                                            <div class="chk-l" style="padding-left: 10px;">
                                                                                Price</div>
                                                                            <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                                <asp:Label ID="Label9" Text='<%# Eval("wlunitprice") %>' runat="server"></asp:Label></div>
                                                                            <div class="chk-l" style="padding-left: 10px;">
                                                                                Sale Value</div>
                                                                            <div class="chk-dates-total" style="float: left; padding-left: 5px;">
                                                                                <asp:Label ID="Label19" Text='<%# Eval("wlunitsalevalue") %>' runat="server"></asp:Label></div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="clear">
                                                                    </div>
                                                                            </div>
                                            <div class="chk-line" style="padding-bottom: 45px; padding-top: 10px; margin-top: 5px;">
                                                <div style="float: left;">
                                                    <div style="float: left; padding-right: 10px; padding-top: 5px;">
                                                        <asp:ImageButton ID="ImgOthServRemarks"  ImageUrl="~/img/button_remarks.png"
                                                            ToolTip="Remarks" runat="server" OnClick="ImgOthServRemarks_Click" />
                                                    </div>
                                                    <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                                    </div>
                                                    <div style="float: left; padding-top: 5px; padding-right: 10px;">
                                                    </div>
                                                    <div style="float: left; padding-top: 5px; padding-right: 10px;" id="dvAmend" runat="server">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <div class="line-bottom">
                                                    <div class="chk-total" id="dvOthtotal" runat="server">
                                                        <div class="chk-total-l">
                                                            Total Price</div>
                                                        <div class="chk-total-r">
                                                            <asp:Label ID="lblothTotalPrice" runat="server"></asp:Label></div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                             <div class="ui-widget collapse" id="dvTabPrehotelSummary" runat="server">
                                    <div class="ui-widget-header">
                                        <div style="float: left; margin: 5px; width:60%;">
                                            <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                                                style="padding-top: 15px;">PRE-ARRANGED HOTELS</span>
                                        </div>
                                        <div style="float: left; margin: 5px; padding-left: 40px; padding-top: 5px;">
                                            <span style="font-size: 12px !important;">
                                                <asp:Label ID="Label94" Text="" runat="server"></asp:Label></span>
                                        </div>
                                        <div style="float: right; margin: 5px;">
                                            <span style="float: right; padding-right: 15px; padding-top: 5px; font-size: 12px !important;">
                                                <asp:Label ID="Label95" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="ui-helper-clearfix">
                                        </div>
                                    </div>
                                    <div class="ui-widget-content" id="dvPreHotelTab1" style="display: none;">
                                        <div id="dvPreHotelSummary" runat="server" style="width: 100%">
                                            <div class="checkout-coll">
                                                <div>
                                                    <div class="clear">
                                                    </div>
                                                    <div class="chk-details" style="margin-top: -15px;">
                                                        <asp:DataList ID="dlPreHotelSummary" Width="100%" runat="server">
                                                            <ItemTemplate>
                                                                <div class="chk-detais-row">
                                                                    <div class="line-bottom">
                                                                        <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                            Hotel Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                        <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                            <asp:Label ID="lblHotelName" Text='<%# Eval("partyname") %>' runat="server"></asp:Label></div>
                                                                        <div class="clear">
                                                                        </div>
                                                                    </div>
                                                                              <div class="line-bottom">
                                                                        <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                            UAE location  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                        <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                            <asp:Label ID="Label102" Text='<%# Eval("othtypname") %>' runat="server"></asp:Label></div>
                                                                        <div class="clear">
                                                                        </div>
                                                                    </div>
                                                                    <div class="line-bottom">
                                                                        <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                            Check In date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                        <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                            <asp:Label ID="Label99" Text='<%# Eval("checkindate") %>' runat="server"></asp:Label></div>
                                                                        <div class="clear">
                                                                        </div>
                                                                    </div>
                                                                    <div class="line-bottom">
                                                                        <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                            Check Out date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                        <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                            <asp:Label ID="Label100" Text='<%# Eval("checkoutdate") %>' runat="server"></asp:Label></div>
                                                                        <div class="clear">
                                                                            <asp:Label ID="Label101" Text='<%# Eval("othtypname") %>' Visible="false" runat="server"></asp:Label>
                                                                            <asp:Label ID="lblrlineno" Text='<%# Eval("rlineno") %>' Visible="false" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                               

                                                                          <div class="line-bottom">
                                                                        <div class="chk-l" style="width: 15%; padding-bottom: 5px;">
                                                                            Adult &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                        <div class="chk-r" style="width: 20%; padding-bottom: 5px;">
                                                                            <asp:Label ID="Label97" Text='<%# Eval("adults") %>' runat="server"></asp:Label></div>
                                                                              <div class="chk-l" style="width: 15%; padding-bottom: 5px;">
                                                                            Child &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                        <div class="chk-r" style="width: 25%; padding-bottom: 5px;">
                                                                            <asp:Label ID="Label98" Text='<%# Eval("child") %>' runat="server"></asp:Label> <asp:Label ID="lblChildAges" Text='<%# "(" + Eval("childages") + ")" %>' runat="server"></asp:Label></div>
                                                                        <div class="clear">
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                  
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        <div class="line-bottom" style="margin-left: 5px; background-color: #777777; margin-left: -px;"
                            id="divtotal" runat="server">
                            <div class="chk-total" id="Div30" runat="server">
                                <div class="chk-totalnew-l" style="color: #fff; margin-top: -9px; padding-left: 15px;">
                                    Total Booking Value
                                </div>
                                <div class="chk-total-t" style="padding-right: 15px; margin-top: -9px;">
                                    <asp:Label ID="lbltotalbooking" runat="server"></asp:Label>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div class="line-bottom" style="margin-left: 5px; background-color: #777777; margin-left: 0px;"
                            id="div48" runat="server">
                            <div class="chk-total" id="Div50" runat="server">
                                <div class="chk-totalnew-l" style="color: #fff; margin-top: -9px; padding-left: 15px;">
                                    <asp:Label ID="lblTotalBaseCurrTitle" runat="server"></asp:Label>
                                </div>
                                <div class="chk-total-t" style="padding-right: 15px; margin-top: -9px;">
                                    <asp:Label ID="lblTotalBaseCurrency" runat="server"></asp:Label>                                    
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div class="line-bottom" style="margin-left: 5px; background-color: #777777; margin-left: -px;"
                            id="divPaid" runat="server">
                            <div class="chk-total" id="Div49" runat="server">
                                <div class="chk-totalnew-l" style="color: #fff; margin-top: -9px; padding-left: 15px;">
                                    Total Paid Value
                                </div>
                                <div class="chk-total-t" style="padding-right: 15px; margin-top: -9px;">
                                    <asp:Label ID="lblPaidAmount" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnPaidAmount" runat="server" />
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                                 
                <div id="dvPaymentStatus" runat="server" style="margin-top:100%;margin-left:50px;">
                <asp:Label class="text-danger" id="PaymentStatus" runat="server"></asp:Label>
                </div>
                   <asp:UpdatePanel ID="PaymentDetails" runat="server">
                    <ContentTemplate>
                    <div  id="dvPaymentDetails" runat="server" style="margin-left:50px;">
                         <asp:DropDownList class="input-a" ID="ddlBookingStatus" runat="server" OnSelectedIndexChanged="ddlBookingStatus_SelectedIndexChanged" AutoPostBack="True" Style="margin-top:15px;">
                                    <asp:ListItem>Booking Confirmed</asp:ListItem>
                                    <asp:ListItem>Booking Failed</asp:ListItem>
                            </asp:DropDownList>
                        <div id="DivRefundReverse" runat="server" style="display:none;">
                            <asp:DropDownList class="input-a" ID="ddlPaymentType" runat="server" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" AutoPostBack="True" Style="margin-top:15px;">
                                    <asp:ListItem>Reverse Payment</asp:ListItem>
                                    <asp:ListItem>Refund</asp:ListItem>
                            </asp:DropDownList>

                            <asp:TextBox  class="input-a" ID="PaymentAmount" Style="margin-top:20px;display:none" Width="90px" runat="server" Value="1000"></asp:TextBox>
                            <asp:Label ID="PayCurrency" Style="display:none" Width="30px" runat="server"></asp:Label>
                            
                            <asp:HiddenField ID="hdnPaymentRequestId" runat="server" />
                            <asp:HiddenField ID="hdnPayAmount" runat="server" />
                            <asp:HiddenField ID="hdnPaymentId" runat="server" />
                            <asp:HiddenField ID="hdnPayCurrency" runat="server" />                            
                            <br />
                            <asp:Button ID="btnReverseRefund" class="authorize-btn" OnClientClick="return CheckAmount()" Style="margin-top:20px;" Width="150px" runat="server"
                                        Text="Submit" />
                         </div>
                      </div>
                     </ContentTemplate>
                </asp:UpdatePanel>
                <div id="dvPackageDetails" runat="server" style="padding-top: 40px; background-color: White;">
                    <div style="width: 100%; height: 30px; padding-left: 10px;" class="ui-widget-header">
                        <span>Package Details</span></div>
                    <div class="ui-widget-content" style="background-color: White; padding-left: 10px;border:none;padding-bottom:45px;">
                         <div id="dvPackageSummaryRO" runat="server">
                        <div class="divTable blueTable" style="margin-left: -9px;">
                            <div class="divTableHeading">
                                <div class="divTableRow">
                                    <div class="divTableHead">
                                        <asp:Label ID="Label23" Text="Details" runat="server"></asp:Label></div>
                                    <div class="divTableHead">
                                        <asp:Label ID="lblsalecurrcode" runat="server"></asp:Label></div>
                                    <div class="divTableHead">
                                        <asp:Label ID="lblBaseCurrency" runat="server"></asp:Label></div>
                                </div>
                            </div>
                            <div class="divTableBody">
                                <div class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPTotalSaleValueText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPTotalSaleValue" runat="server"></asp:Label></div>
                                    <div id="dvROPackage7" class="divTableCell">
                                        <asp:Label ID="lblPTotalSaleValueAED" runat="server"></asp:Label></div>
                                </div>
                                <div id="dvROPackage1" class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPTotalCostValueText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPTotalCostValueCurr" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPTotalCostValue" runat="server"></asp:Label></div>
                                </div>
                                <div id="dvROPackage2" class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPTotalProfitText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lbPTotalProfitCurr" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:LinkButton ID="lbPTotalProfit" runat="server"></asp:LinkButton></div>
                                </div>
                                <div id="dvROPackage3" class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPMinimumMarkupText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPMinimumMarkupCurr" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPMinimumMarkup" runat="server"></asp:Label></div>
                                </div>
                                <div id="dvROPackage4" class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPFormulaIdText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                    </div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPFormulaId" runat="server"></asp:Label></div>
                                </div>
                                <div id="dvROPackage5" class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDifferentialMarkupText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDifferentialMarkup" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDifferentialMarkupbase" runat="server"></asp:Label></div>
                                </div>
                                <div id="dvROPackage6"  style="display:none;"  class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDiscount_DifferentialMarkupText" runat="server"></asp:Label></div>
                                    <div class="divTableCell" style="border-left: 0px solid #ffff !important;">
                                        <asp:Label ID="lblPDiscount_DifferentialMarkup" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDiscount_DifferentialMarkup1" runat="server"></asp:Label></div>
                                </div>
                                <div class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDiscountValueText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDiscountValue" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDiscountValueBase" runat="server"></asp:Label></div>
                                </div>

                                 <%-- Added Shahul 09/10/18--%>
                                <div class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPNetprofitText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPNetprofitValue" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPNetprofitValueBase" runat="server"></asp:Label></div>
                                </div>

                                <div class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPNetSaleValueText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPNetSaleValue" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPNetSaleValueBase" runat="server"></asp:Label></div>
                                </div>

                                   <%-- Added Shahul 09/10/18--%>
                                <div class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblSystemMarkupText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblSystemMarkupValue" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblSystemMarkupValueBase" runat="server"></asp:Label></div>
                                </div>
                                    <div class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblRevisedMarkupText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblRevisedMarkupValue" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblRevisedMarkupValueBase" runat="server"></asp:Label></div>
                                </div>
                                <div class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblSystemDiscountText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblSystemDiscountValue" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblSystemDiscountValueBase" runat="server"></asp:Label></div>
                                </div>
                                    <div class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblRevisedDiscountText" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblRevisedDiscountValue" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblRevisedDiscountValueBase" runat="server"></asp:Label></div>
                                </div>

                            </div>
                        </div>
                        </div>

                        <div  id="dvPackageSummaryAgent" runat="server">
                        <div class="divTable blueTable" style="margin-left: -9px;">
                            <div class="divTableHeading">
                                <div class="divTableRow">
                                    <div class="divTableHead">
                                        <asp:Label ID="Label22" Text="Details" runat="server"></asp:Label></div>
                                    <div class="divTableHead">
                                        <asp:Label ID="lblsalecurrcodeAgent" runat="server"></asp:Label></div>
                                </div>
                            </div>
                            <div class="divTableBody">
                                <div class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPTotalSaleValueTextAgent" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPTotalSaleValueAgent" runat="server"></asp:Label></div>
                                </div>
                                <div id="Div105" class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDiscountValueTextAgent" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPDiscountValueAgent" runat="server"></asp:Label></div>
                                </div>
                                <div id="Div106" class="divTableRow">
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPNetSaleValueTextAgent" runat="server"></asp:Label></div>
                                    <div class="divTableCell">
                                        <asp:Label ID="lblPNetSaleValueAgent" runat="server"></asp:Label></div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>

                       <asp:UpdatePanel ID="upnlPackageROSummary" runat="server">
            <ContentTemplate>
            <asp:ModalPopupExtender ID="mpPackageROSummary" runat="server" BackgroundCssClass="roomtype-modalBackground"
                CancelControlID="a1" EnableViewState="true" PopupControlID="pnlPackageROSummary"
                TargetControlID="hdPackageROSummary">
            </asp:ModalPopupExtender>
           <asp:HiddenField ID="hdPackageROSummary" runat="server" />
            <asp:Panel ID="pnlPackageROSummary" style="display:none;"  runat="server">
          
            <div id ="div94"  class="roomtype-price-breakuppopup" style="float:left;text-align:left;max-height: 500px;">
            <div align="left" id="Div95">

                <div id="div98" runat="server" style="border: 1px solid #fff; width: 700px;
                    height: 30px; background-color: #ede7e1; padding-right: 5px; margin-right: 10px;
                    text-align: center; vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                    <asp:Label ID="lblPackageROSummaryHeadding" CssClass="room-type-breakup-headings" Text="Booking Summary"
                        runat="server"></asp:Label>
                         &nbsp; &nbsp;<a id="a1" href="#" class="roomtype-popupremarks-close"></a>
                   
                </div>
                 <div class="booking-form" id="Div99" runat ="server" style="background-color: White; padding: 5px;">
                    <div class="row-col-12" style="padding-left: 10px;">
                           <asp:GridView ID="gvPackageROSummary" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                        Width="100%"  GridLines="Horizontal">
                        <Columns>
                            <asp:TemplateField HeaderText="Request Id" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestId" Text='<%# Eval("requestid") %>' runat="server"></asp:Label>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="RequestType" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestType" Text='<%# Eval("requesttype") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Line No" >
                                <ItemTemplate>
                                    <asp:Label ID="lblLineNo" Text='<%# Eval("rlineno") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Adults" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAdults" Text='<%# Eval("adults") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Child" >
                                <ItemTemplate>
                                    <asp:Label ID="lblChild" Text='<%# Eval("child") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Sale Value" >
                             <HeaderTemplate>
                              <asp:Label ID="lblSaleValueHeader" Text="" runat="server"></asp:Label>
                             </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSaleValue" Text='<%# Eval("salevalue") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Sale Value Base" >
                                <HeaderTemplate>
                              <asp:Label ID="lblSaleValueBaseHeader" Text="" runat="server"></asp:Label>
                             </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSaleValueBase" Text='<%# Eval("salevaluebase") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Cost Value" >
                                <HeaderTemplate>
                              <asp:Label ID="lblCostValueHeader" Text="" runat="server"></asp:Label>
                             </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCostValue" Text='<%# Eval("costvalue") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns> 
                        <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                        <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                    <div class="clear">
                    </div>
                    
                
                </div> 

            </div> 
            </div>
            <input id="Button13" runat="server" type="button" value="Cancel" style="display:none" />
          
            </asp:Panel>
            </ContentTemplate> 
            </asp:UpdatePanel>

                   <asp:UpdatePanel ID="upnlValidateGuestService" runat="server">
                                    <ContentTemplate>
                                        <asp:ModalPopupExtender ID="mpValidateGuestService" runat="server" BackgroundCssClass="roomtype-modalBackground" 
                                            CancelControlID="aCloseValidateGuestService" EnableViewState="true" PopupControlID="pnlValidateGuestService"
                                           
                                            TargetControlID="hdValidateGuestService">
                                        </asp:ModalPopupExtender>
                                        <asp:HiddenField ID="hdValidateGuestService" runat="server" />
                                        <asp:Panel ID="pnlValidateGuestService" Style="display: none;" runat="server">
                                            <div id="dvValidateGuestService" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                                max-height:700px;overflow:auto; ">
                                                <div align="left" id="Div112">
                                                    <div id="div113" runat="server" style="border: 1px solid #fff; width: 100%; height: 40px;
                                                        background-color: #ede7e1; padding-right: 5px;  text-align: left;
                                                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                                        <asp:Label ID="Label103" style="padding-left:15px;" CssClass="room-type-breakup-headings"
                                                            Text="Services" runat="server"></asp:Label>
                                                        &nbsp; &nbsp;<a style="float:right;margin-top:-10px;" id="aCloseValidateGuestService" href="#" class="roomtype-popupremarks-close"></a>
                                                    </div>
                                                    <div class="booking-form" id="Div114" runat="server" style="background-color: White;
                                                        padding: 5px; max-height:600px;overflow:auto;">
                                                        <div class="row-col-12" style="padding-left: 10px;">
                                                            <asp:GridView ID="gvValidateGuestService" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvValidateGuestService_RowDataBound" 
                                                                CssClass="mygrid" Width="100%" GridLines="Horizontal">
                                                                <Columns>
                                        
                                                                    <asp:TemplateField HeaderText="Service Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblServiceType" Text='<%# Eval("ServiceType") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Room No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRoomNo" Text='<%# Eval("roomno") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Line No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLineNo" Text='<%# Eval("rlineno") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Adults">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAdults" Text='<%# Eval("adults") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Child">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblChild" Text='<%# Eval("child") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                         
                                                           <asp:TemplateField HeaderText="Service Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblServiceName" Text='<%# Eval("partyname") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Service Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblServiceDate" Text='<%# Eval("ServiceDate") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Guest Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGuestName" Text='<%# Eval("Guestname") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <input id="Button12" runat="server" type="button" value="Cancel" style="display: none" />
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>




                <div class="h-help">
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
                                     

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
		<div  id="dvMagnifyingMemories" runat="server"   class="section-middle" style="display:none !important" >
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
  <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
  <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
<asp:HiddenField ID="hdTabHotelTotalPrice" runat="server" />
<asp:HiddenField ID="hdTourTabTotalPrice" runat="server" />
<asp:HiddenField ID="hdTransferTabTotalPrice" runat="server" />
<asp:HiddenField ID="hdVisaTabTotalPrice" runat="server" />
<asp:HiddenField ID="hdAirportTabtotalPrice" runat="server" />
<asp:HiddenField ID="hdOtherServiceTabTotalPrice" runat="server" />
<asp:HiddenField ID="hdWhiteLabel" runat="server" />
    </div>
    </form>
</body>

<script type="text/javascript">
    //   ' alert('test');
    $('.clockpicker').clockpicker({
        placement: 'top',
        align: 'left',
        donetext: 'Done'
    });
    //    alert('test1');
</script>
</html>
