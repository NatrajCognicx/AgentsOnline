<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MyAccount.aspx.vb" Inherits="MyAccount" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 
  <meta name="description" content="" />
  <meta name="keywords" content="" />
  <meta charset="utf-8" /><link rel="icon" href="favicon.png" />
  <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no"> 
  <link rel="stylesheet" href="css/jquery-ui.css">
  <link rel="stylesheet" href="css/jquery.formstyler.css">
  <link rel="stylesheet" href="css/style.css" />
  <link id="lnkCSS" rel='stylesheet' type='text/css' href='css/style-common.css' />

<%--    <link rel="stylesheet" href="css/Raleway.css" />
    <link rel="stylesheet" href="css/Raleway1.css" />
    <link rel="stylesheet" href="css/Raleway2.css" />
    <link rel="stylesheet" href="css/Montserrat.css" />
    <link rel="stylesheet" href="css/Lora.css" />
    <link rel="stylesheet" href="css/Lato.css" />
    <link rel="stylesheet" href="css/OpenSans.css" />
    <link rel="stylesheet" href="css/PTSans.css" />--%>

    <%--***Danny 18/08/2018 fa fa-star--%>
    <link href="css/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
 <%-- <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

        <link href='http://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lora:400,400italic' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:100,200,300,400,600,700,800' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,600' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700&amp;subset=latin,cyrillic' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lato:400,700&amp;subset=latin,latin-ext' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700&amp;subset=latin,cyrillic' rel='stylesheet' type='text/css' />
--%>



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
      .pinned
    {
        position: fixed; /* i.e. not scrolled */
        background-color: White; /* prevent the scrolled columns showing through */
        z-index: 10000; /* keep the pinned on top of the scrollables */
    }
     td.lockedHeader, th.lockedHeader
        {
            background-color: #06788B;
            position: absolute;
            display: block;
            border-right-color: #06788B;
            z-index: 999999;
           
        }
      .mygrid
      {
          width: 100%;
          min-width: 100%;
          border: 1px solid #EDE7E1;
          font-family: Raleway !important;
          color: #455051;
      }
      .mygrid-header
      {
          text-transform:uppercase;
          background-color: #EDE7E1;
          font-family: Raleway !important;
          color: #455051;
          height: 25px;
          text-align: left;
          font-size: 16px;
          font-size: small;
          padding: 5px 5px 5px 5px;
      }
      .totalValue-header
      {
          font-family: Raleway !important;
          color: #455051 !important;
          text-align: left !important;
          font-size: 16px !important;
          font-size: small !important;
      }
      .mygrid-rows
      {
          font-family: Raleway !important;
          font-size: 15px;
          color: #455051;
          min-height: 25px;
          text-align: left;
          border: 1px solid #EDE7E1;
          vertical-align: middle;
      }
       .mygrid-rows-alternative
      {
          font-family: Raleway !important;
          font-size: 15px;
          color: #455051;
          min-height: 25px;
          text-align: left;
          border: 1px solid #EDE7E1;
          vertical-align: middle;
          background-color:#F2F4F4;
      }
       .mygrid-rows-alternative:hover
      {
          background-color: #FDF2E9;
          font-family: Raleway;
          color: #fff;
          text-align: left;
      }
      .mygrid-rows:hover
      {
          background-color: #FDF2E9;
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
          color: #455051;
          padding: 5px 5px 5px 5px;
          font-size: 11px;
          vertical-align: middle;
          font-family: Raleway;
      }

      .mygrid td
      {
          padding: 5px;
            border: 1px solid #EDE7E1;
      }
      .mygrid th
      {
          padding: 5px;
            border: 1px solid #EDE7E1;
      }
      .myaccount-tab-active
      {
          float:left;width:100px;height:30px;padding-top:10px;padding-left:1px;background-color:White;color:#ff7200;font-family:'Montserrat';font-size:12px;border-right:1px solid #e3e3e3;border-top:1px solid #f2f2f2;font-weight:600;cursor:pointer;z-index:9;
      }
      .myaccount-tab-active_new
      {
          float:left;width:200px;height:30px;padding-top:10px;padding-left:1px;background-color:White;color:#ff7200;font-family:'Montserrat';font-size:12px;border-right:1px solid #e3e3e3;border-top:1px solid #f2f2f2;font-weight:600;cursor:pointer;z-index:9;
      }
      
       .myaccount-tab-inactive
      {
          float:left;width:100px;height:30px;padding-top:10px;padding-left:1px;background-color:#1d292e;color:White;margin-left:5px;font-family:'Montserrat';border-right:1px solid #e3e3e3;border-top:1px solid #f2f2f2;font-size:12px;font-weight:600;cursor:pointer;z-index:9;
      }
      .myaccount-tab-active:hover
      {
          color:#fff;
       
      }
  </style>
  <%--<script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"   type="text/javascript"></script>--%>
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
 
   


    
    <script language="javascript" type="text/javascript">
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


            var BookingDate = $("#<%= ddlBookingDate.ClientID %>").val()
            if (BookingDate == 'Specific date') {
                if ((document.getElementById('<%=txtBookingFromDate.ClientID%>').value == '') || (document.getElementById('<%=txtBookingToDate.ClientID%>').value == '')) {
                    showDialog('Alert Message', 'Please select booking date.', 'warning');
                    HideProgess();
                    return false;
                }

            }

            var TravelDate = $("#<%= ddlTravelDate.ClientID %>").val()

            //  if (TravelDate == 'Specific date') {
            if ((TravelDate == 'Check In or Check Out') || (TravelDate == 'CheckIn Date') || (TravelDate == 'CheckOut Date')) {//modified by abin on 20181206
                if ((document.getElementById('<%=txtTravelFromDate.ClientID%>').value == '') || (document.getElementById('<%=txtTravelToDate.ClientID%>').value == '')) {
                    showDialog('Alert Message', 'Please select travel date.', 'warning');
                    HideProgess();
                    return false;
                }
            }



            var BookingRef = document.getElementById('<%=txtBookingRef.ClientID%>').value
            var BookingFlag = 0;
            if ((BookingRef == 'RP/') || (BookingRef == 'RG/') || (BookingRef == '')) { //
                BookingFlag = 1;
            }

            var dropDownServiceType = document.getElementById('<%=ddlServiceType.ClientID%>').selectedIndex;
            var AgentRef = document.getElementById('<%=txtAgentRef.ClientID%>').value;
            var DestinationCode = document.getElementById('<%=txtDestinationCode.ClientID%>').value;
            var GuestFirstName = document.getElementById('<%=txtGuestFirstName.ClientID%>').value;
            var txtGuestSecondName = document.getElementById('<%=txtGuestSecondName.ClientID%>').value;
            var BookingStatus = document.getElementById('<%=ddlBookingStatus.ClientID%>').selectedIndex;
            var HotelCode = document.getElementById('<%=txtHotelCode.ClientID%>').value;
            var HotelConfNo = document.getElementById('<%=txtHotelConfNo.ClientID%>').value;
            var TravelDate = document.getElementById('<%=ddlTravelDate.ClientID%>').selectedIndex;
            var CustCode = document.getElementById('<%=txtCustomerCode.ClientID%>').value;
            if (document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') {
                var ROCode = document.getElementById('<%=txtROCode.ClientID%>').value;
                //alert('BookingFlag:' + BookingFlag + ' dropDownServiceType:' + dropDownServiceType + ' AgentRef:' + AgentRef + ' DestinationCode:' + DestinationCode + ' GuestFirstName:' + GuestFirstName)
                BookingFlag = 0;
                if ((BookingFlag != 1) || (dropDownServiceType != 0) || (AgentRef != '') || (DestinationCode != '') || (GuestFirstName != '') || (txtGuestSecondName != '') || (BookingStatus != '0') || (HotelCode != '') || (HotelConfNo != '') || (ROCode != '') || (CustCode != '') || (TravelDate != 0)) {
                    return true;
                }
                else {
                    showDialog('Alert Message', 'Please enter any search criteria.', 'warning');
                    HideProgess();
                    return false;

                }
            }
            else {
                if ((BookingFlag != 1) || (dropDownServiceType != 0) || (AgentRef != '') || (DestinationCode != '') || (GuestFirstName != '') || (txtGuestSecondName != '') || (BookingStatus != '0') || (HotelCode != '') || (HotelConfNo != '') || (TravelDate != 0)) {
                    return true;
                }
                else {
                    showDialog('Alert Message', 'Please enter any search criteria.', 'warning');
                    HideProgess();
                    return false;

                }
            }


            return true;

        }

        $(document).ready(function () {
            AutoCompleteExtenderKeyUp();
            AutoCompleteExtender_Customer_KeyUp();
            AutoCompleteExtender_Country_KeyUp();
            AutoCompleteExtender_HotelNameKeyUp();
            AutoCompleteExtender_RO_KeyUp();
            ShowBookingAndTravelDate();
            $('#dvMenus').hide();
            $("#btnViewMenu").button().click(function () {

                if ($('#btnViewMenu').val() == 'Show Menu') {
                    $('#btnViewMenu').val('Hide Menu');

                    $('#dvMenus').show(1000);
                    var txtFocus = document.getElementById('<%=txtFocus.ClientID%>');
                    txtFocus.focus();
                }
                else {
                    $('#btnViewMenu').val('Show Menu');
                    $('#dvMenus').hide(1000);

                }

            });

            if (document.getElementById('<%=hdTab.ClientID%>').value == '1' || document.getElementById('<%=hdTab.ClientID%>').value == '') {

                $("#dvTabBookings").removeClass("myaccount-tab-active");
                $("#dvTabBookings").addClass("myaccount-tab-inactive");
                $("#dvTabPayments").removeClass("myaccount-tab-active");
                $("#dvTabPayments").addClass("myaccount-tab-inactive");
                $("#dvTabQuotes").removeClass("myaccount-tab-inactive");
                $("#dvTabQuotes").addClass("myaccount-tab-active");
                $('#lblSearchResultHeader').html('Search Results - Quotes');
                $("#dvSearchresultsBookings").css('display', 'none');
                $("#dvSearchresultsPayments").css('display', 'none');
                $("#dvSearchresultsQuotes").css('display', 'block'); ;
                document.getElementById('<%=hdTab.ClientID%>').value = '1';
                var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
                var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
                //if (div == 'RP/') {
                //    txtBookingRef.value = 'QR/';
                //}
                //if (div == 'RG/') {
                //    txtBookingRef.value = 'QG/';
                //}
                //changed by mohamed on 08/07/2018
                var hdQuoteBookingRef = document.getElementById('<%=hdQuoteBookingRef.ClientID%>');
                txtBookingRef.value = hdQuoteBookingRef.value + '/';

                $('#lblBookingRef').html('Quote Reference');
                $('#lblBookingFromDate').html('Quote From date');
                $('#lblBookingToDate').html('Quote To date');
                $('#lblBookingDate').html('Quote Date');
                $("#dvGFName").css('display', 'none');
                $("#dvGSName").css('display', 'none');
                $("#dvBookingStatus").css('display', 'none');
                $("#dvHotelConfNo").css('display', 'none');
            }
            else if (document.getElementById('<%=hdTab.ClientID%>').value == '2')  {

                $("#dvTabBookings").removeClass("myaccount-tab-active");
                $("#dvTabBookings").addClass("myaccount-tab-inactive");
                $("#dvTabQuotes").removeClass("myaccount-tab-active");
                $("#dvTabQuotes").addClass("myaccount-tab-inactive");
                $("#dvTabPayments").removeClass("myaccount-tab-inactive");
                $("#dvTabPayments").addClass("myaccount-tab-active");
                $('#lblSearchResultHeader').html('Search Results - Payments');
                $("#dvSearchresultsBookings").css('display', 'none');
                $("#dvSearchresultsQuotes").css('display', 'none'); 
                $("#dvSearchresultsPayments").css('display', 'block'); 
                document.getElementById('<%=hdTab.ClientID%>').value = '2';
                var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
                var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
                
                //changed by mohamed on 08/07/2018
                var hdPaymentBookingRef = document.getElementById('<%=hdPaymentBookingRef.ClientID%>');
                txtBookingRef.value = hdPaymentBookingRef.value + '/';

                $('#lblBookingRef').html('Quote Reference');
                $('#lblBookingFromDate').html('Quote From date');
                $('#lblBookingToDate').html('Quote To date');
                $('#lblBookingDate').html('Quote Date');
                $("#dvGFName").css('display', 'none');
                $("#dvGSName").css('display', 'none');
                $("#dvBookingStatus").css('display', 'none');
                $("#dvHotelConfNo").css('display', 'none');
            }
            else {

                $("#dvTabQuotes").removeClass("myaccount-tab-active");
                $("#dvTabQuotes").addClass("myaccount-tab-inactive");
                $("#dvTabPayments").removeClass("myaccount-tab-active");
                $("#dvTabPayments").addClass("myaccount-tab-inactive");
                $("#dvTabBookings").removeClass("myaccount-tab-inactive");
                $("#dvTabBookings").addClass("myaccount-tab-active");
                $('#lblSearchResultHeader').html('Search Results - Bookings');
                $("#dvSearchresultsBookings").css('display', 'block');
                $("#dvSearchresultsQuotes").css('display', 'none');
                $("#dvSearchresultsPayments").css('display', 'none');
                //  document.getElementById('<%=hdTab.ClientID%>').value = '0';

                var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
                var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
                //changed by mohamed on 08/07/2018
                var hdBookingRef = document.getElementById('<%=hdBookingRef.ClientID%>');
                txtBookingRef.value = hdBookingRef.value

                //                    if (div == 'RP/') {
                //                        txtBookingRef.value = 'RP/';
                //                    }
                //                    if (div == 'RG/') {
                //                        txtBookingRef.value = 'RG/';
                //                    }
                $('#lblBookingRef').html('Booking Reference');
                $('#lblBookingFromDate').html('Booking From date');
                $('#lblBookingToDate').html('Booking To date');
                $('#lblBookingDate').html('Booking Date');
                $("#dvGFName").css('display', 'block');
                $("#dvGSName").css('display', 'block');
                $("#dvBookingStatus").css('display', 'block');
                $("#dvHotelConfNo").css('display', 'block');
            }



            $("#dvTabBookings").button().click(function () {

                $("#dvTabQuotes").removeClass("myaccount-tab-active");
                $("#dvTabQuotes").addClass("myaccount-tab-inactive");
                $("#dvTabPayments").removeClass("myaccount-tab-active");
                $("#dvTabPayments").addClass("myaccount-tab-inactive");
                $("#dvTabBookings").removeClass("myaccount-tab-inactive");
                $("#dvTabBookings").addClass("myaccount-tab-active");
                $('#lblSearchResultHeader').html('Search Results - Bookings');
                $("#dvSearchresultsBookings").css('display', 'block');
                $("#dvSearchresultsQuotes").css('display', 'none');
                $("#dvSearchresultsPayments").css('display', 'none');
                document.getElementById('<%=hdTab.ClientID%>').value = '0';

                var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
                var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
                //*** Danny 21/07/2018
                var hdBookingRef = document.getElementById('<%=hdBookingRef.ClientID%>');
                txtBookingRef.value = hdBookingRef.value
                //                    if (div == 'RP/') {
                //                        txtBookingRef.value = 'RP/';
                //                    }
                //                    if (div == 'RG/') {
                //                        txtBookingRef.value = 'RG/';
                //                    }
                $('#lblBookingRef').html('Booking Reference');
                $('#lblBookingFromDate').html('Booking From date');
                $('#lblBookingToDate').html('Booking To date');
                $('#lblBookingDate').html('Booking Date');
                $("#dvGFName").css('display', 'block');
                $("#dvGSName").css('display', 'block');
                $("#dvBookingStatus").css('display', 'block');
                $("#dvHotelConfNo").css('display', 'block');

            });

            $("#dvTabQuotes").button().click(function () {

                $("#dvTabBookings").removeClass("myaccount-tab-active");
                $("#dvTabBookings").addClass("myaccount-tab-inactive");
                $("#dvTabPayments").removeClass("myaccount-tab-active");
                $("#dvTabPayments").addClass("myaccount-tab-inactive");
                $("#dvTabQuotes").removeClass("myaccount-tab-inactive");
                $("#dvTabQuotes").addClass("myaccount-tab-active");
                $('#lblSearchResultHeader').html('Search Results - Quotes');
                $("#dvSearchresultsBookings").css('display', 'none');
                $("#dvSearchresultsQuotes").css('display', 'block');
                $("#dvSearchresultsPayments").css('display', 'none');
                document.getElementById('<%=hdTab.ClientID%>').value = '1';
                var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
                var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
                //if (div == 'RP/') {
                //    txtBookingRef.value = 'QR/';
                //}
                //if (div == 'RG/') {
                //    txtBookingRef.value = 'QG/';
                //}
                //changed by mohamed on 08/07/2018
                var hdQuoteBookingRef = document.getElementById('<%=hdQuoteBookingRef.ClientID%>');
                txtBookingRef.value = hdQuoteBookingRef.value + '/';

                $('#lblBookingRef').html('Quote Reference');
                $('#lblBookingFromDate').html('Quote From date');
                $('#lblBookingToDate').html('Quote To date');
                $('#lblBookingDate').html('Quote Date');
                $("#dvGFName").css('display', 'none');
                $("#dvGSName").css('display', 'none');
                $("#dvBookingStatus").css('display', 'none');
                $("#dvHotelConfNo").css('display', 'none');
            });

            $("#dvTabPayments").button().click(function () {

                $("#dvTabBookings").removeClass("myaccount-tab-active");
                $("#dvTabBookings").addClass("myaccount-tab-inactive");
                $("#dvTabQuotes").removeClass("myaccount-tab-active");
                $("#dvTabQuotes").addClass("myaccount-tab-inactive");
                $("#dvTabPayments").removeClass("myaccount-tab-inactive");
                $("#dvTabPayments").addClass("myaccount-tab-active");
                $('#lblSearchResultHeader').html('Search Results - Payments');
                $("#dvSearchresultsBookings").css('display', 'none');
                $("#dvSearchresultsQuotes").css('display', 'none'); ;
                $("#dvSearchresultsPayments").css('display', 'block'); ;
                document.getElementById('<%=hdTab.ClientID%>').value = '2';
                var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
                var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
                
                //changed by mohamed on 08/07/2018
                var hdPaymentBookingRef = document.getElementById('<%=hdPaymentBookingRef.ClientID%>');
                txtBookingRef.value = hdPaymentBookingRef.value + '/';

                $('#lblBookingRef').html('Quote Reference');
                $('#lblBookingFromDate').html('Quote From date');
                $('#lblBookingToDate').html('Quote To date');
                $('#lblBookingDate').html('Quote Date');
                $("#dvGFName").css('display', 'none');
                $("#dvGSName").css('display', 'none');
                $("#dvBookingStatus").css('display', 'none');
                $("#dvHotelConfNo").css('display', 'none');
            });

            $("#btnReset").button().click(function () {

                fnReset();

            });

            $("#<%= ddlBookingDate.ClientID %>").bind("change", function () {
                var BookingDate = $("#<%= ddlBookingDate.ClientID %>").val()
                if (BookingDate == 'Specific date') {

                    $('#dvBookingFromDate').show();
                    $('#dvBookingToDate').show();
                }
                else {
                    $('#dvBookingFromDate').hide();
                    $('#dvBookingToDate').hide();
                }
            });
            $("#<%= ddlTravelDate.ClientID %>").bind("change", function () {
                var TravelDate = $("#<%= ddlTravelDate.ClientID %>").val()
                if ((TravelDate == 'Check In or Check Out') || (TravelDate == 'CheckIn Date') || (TravelDate == 'CheckOut Date')) {//modified by abin on 20181206
                    //  if (TravelDate == 'Specific date') {

                    $('#dvTravelFromDate').show();
                    $('#dvTravelToDate').show();
                }
                else {
                    $('#dvTravelFromDate').hide();
                    $('#dvTravelToDate').hide();
                }
            });

            $("#<%= txtBookingFromDate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=txtBookingFromDate.ClientID%>').value;
                var dp = d.split("/");
                var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-booking-to").datepicker("destroy");
                $(".date-inpt-booking-to").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });

            });

            $("#<%= txtTravelFromDate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=txtTravelFromDate.ClientID%>').value;
                var dp = d.split("/");

                //                    if (dp[0] = '31') {
                //                        dp[0] = '30';
                //                    }
                var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                // var dateOut = new Date(dp[2], dp[1], dp[0]);
                var currentMonth = dateOut.getMonth();
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();

                //                    var currentMonth = dp[1];
                //                    var currentDate = dp[0];
                //                    var currentYear = dp[2];

                //                    alert(dp[0]);
                // alert(dateOut - 1);
                $(".date-inpt-travel-to").datepicker("destroy");
                $(".date-inpt-travel-to").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });



            });
        });


        function GetHotelsDetails(HotelCode) {
            $.ajax({
                type: "POST",
                url: "HotelSearch.aspx/GetHotelsDetails",
                data: '{HotelCode:  "' + HotelCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessHotel,
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
        function OnSuccessHotel(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var customers = xml.find("Customers");

            $.each(customers, function () {
                var customer = $(this);

                document.getElementById('<%=txtDestinationcode.ClientID%>').value = $(this).find("destcode").text();
                document.getElementById('<%=txtDestinationName.ClientID%>').value = $(this).find("destname").text();

            });
            SetHotelContextkey();

        };

        function fnReset() {
           
            document.getElementById('<%=txtBookingRef.ClientID%>').value = '';
            document.getElementById('<%=txtBookingRef.ClientID%>').value = document.getElementById('<%=hdBookingRef.ClientID%>').value;
            var dropDownServiceType = document.getElementById('<%=ddlServiceType.ClientID%>');
            dropDownServiceType.selectedIndex = "0";
            $('.custom-select-ServiceType').next('span').children('.customSelectInner').text(jQuery("#ddlServiceType :selected").text());
            document.getElementById('<%=txtAgentRef.ClientID%>').value = '';
            document.getElementById('<%=txtDestinationName.ClientID%>').value = '';
            document.getElementById('<%=txtDestinationCode.ClientID%>').value = '';
            document.getElementById('<%=txtHotelName.ClientID%>').value = '';
            document.getElementById('<%=txtHotelCode.ClientID%>').value = '';
            document.getElementById('<%=txtGuestFirstName.ClientID%>').value = '';
            document.getElementById('<%=txtGuestSecondName.ClientID%>').value = '';

            var dropDown = document.getElementById('<%=ddlBookingDate.ClientID%>');
            dropDown.selectedIndex = "0";
            $('.custom-select-BookingDate').next('span').children('.customSelectInner').text(jQuery("#ddlBookingDate :selected").text());
            document.getElementById('<%=txtBookingFromDate.ClientID%>').value = '';
            document.getElementById('<%=txtBookingToDate.ClientID%>').value = '';

            var dropDownTravel = document.getElementById('<%=ddlTravelDate.ClientID%>');
            dropDownTravel.selectedIndex = "0";
            $('.custom-select-TravelDate').next('span').children('.customSelectInner').text(jQuery("#ddlTravelDate :selected").text());
            document.getElementById('<%=txtTravelFromDate.ClientID%>').value = '';
            document.getElementById('<%=txtTravelToDate.ClientID%>').value = '';

            var dropDownBookingStatus = document.getElementById('<%=ddlBookingStatus.ClientID%>');
            dropDownBookingStatus.selectedIndex = "0";
            $('.custom-select-BookingStatus').next('span').children('.customSelectInner').text(jQuery("#ddlBookingStatus :selected").text());

            document.getElementById('<%=txtHotelConfNo.ClientID%>').value = '';


            if (document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') {
                document.getElementById('<%=txtRO.ClientID%>').value = '';
                document.getElementById('<%=txtROCode.ClientID%>').value = '';
                document.getElementById('<%=txtCustomer.ClientID%>').value = ''
                document.getElementById('<%=txtCustomerCode.ClientID%>').value = '';
                document.getElementById('<%=txtCountry.ClientID%>').value = ''
                document.getElementById('<%=txtCountryCode.ClientID%>').value = '';
            }



            var BookingDate = $("#<%= ddlBookingDate.ClientID %>").val()
            if (BookingDate == 'Specific date') {

                $('#dvBookingFromDate').show();
                $('#dvBookingToDate').show();
            }
            else {
                $('#dvBookingFromDate').hide();
                $('#dvBookingToDate').hide();
            }
            var TravelDate = $("#<%= ddlTravelDate.ClientID %>").val()
            if ((TravelDate == 'Check In or Check Out') || (TravelDate == 'CheckIn Date') || (TravelDate == 'CheckOut Date')) {//modified by abin on 20181206
                //  if (TravelDate == 'Specific date') {

                $('#dvTravelFromDate').show();
                $('#dvTravelToDate').show();
            }
            else {
                $('#dvTravelFromDate').hide();
                $('#dvTravelToDate').hide();
            }


            var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
            var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
            //if (div == 'RP/') {
            //    txtBookingRef.value = 'QR/';
            //}
            //if (div == 'RG/') {
            //    txtBookingRef.value = 'QG/';
            //}
            //changed by mohamed on 08/07/2018
            var hdQuoteBookingRef = document.getElementById('<%=hdQuoteBookingRef.ClientID%>');
            var hdPaymentBookingRef = document.getElementById('<%=hdPaymentBookingRef.ClientID%>');
            if (document.getElementById('<%=hdTab.ClientID%>').value == 1) {
                txtBookingRef.value = hdQuoteBookingRef.value + '/';
            }
            else if (document.getElementById('<%=hdTab.ClientID%>').value == 2) {
                txtBookingRef.value = hdPaymentBookingRef.value + '/';
            }
            else {
                txtBookingRef.value = div;
            }
          


            $("#dvSearchresultsBookings").css('display', 'none');
            $("#dvSearchresultsQuotes").css('display', 'none');
            $("#dvSearchresultsPayments").css('display', 'none');



        }


        function Countryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtCountryCode.ClientID%>').value = '';
            }
        }
        function ROautocompleteselected(source, eventArgs) {

            if (eventArgs != null) {
                document.getElementById('<%=txtROCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtROCode.ClientID%>').value = '';
            }

        }


        function Customersautocompleteselected(source, eventArgs) {

            if (eventArgs != null) {
                document.getElementById('<%=txtCustomerCode.ClientID%>').value = eventArgs.get_value();

                $find('AutoCompleteExtender_txtCountry').set_contextKey(eventArgs.get_value());
                // alert(eventArgs.get_value());
                GetCountryDetails(eventArgs.get_value());
            }
            else {
                document.getElementById('<%=txtCustomerCode.ClientID%>').value = '';

            }
        }
        function GetCountryDetails(CustCode) {

            $.ajax({
                type: "POST",
                url: "HotelSearch.aspx/GetCountryDetails",
                data: '{CustCode:  "' + CustCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
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

        function OnSuccess(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var Countries = xml.find("Countries");
            var rowCount = Countries.length;

            if (rowCount == 1) {
                $.each(Countries, function () {
                    document.getElementById('<%=txtCountry.ClientID%>').value = ''
                    document.getElementById('<%=txtCountryCode.ClientID%>').value = '';
                    document.getElementById('<%=txtCountry.ClientID%>').value = $(this).find("ctryname").text();
                    document.getElementById('<%=txtCountryCode.ClientID%>').value = $(this).find("ctrycode").text();
                    document.getElementById('<%=txtCountry.ClientID%>').setAttribute("readonly", true);
                    $find('AutoCompleteExtender_txtCountry').setAttribute("Enabled", false);
                });
            }
            else {
                document.getElementById('<%=txtCountry.ClientID%>').value = ''
                document.getElementById('<%=txtCountryCode.ClientID%>').value = '';
                document.getElementById('<%=txtCountry.ClientID%>').removeAttribute("readonly");
                $find('AutoCompleteExtender_txtCountry').setAttribute("Enabled", true);
            }
        };

        function DestinationNameautocompleteselected(source, eventArgs) {

            if (eventArgs != null) {

                document.getElementById('<%=txtDestinationcode.ClientID%>').value = eventArgs.get_value();

            }
            else {
                document.getElementById('<%=txtDestinationcode.ClientID%>').value = '';
            }
            SetHotelContextkey();
        }

        function SetHotelContextkey() {
            var dc = document.getElementById('<%=txtDestinationcode.ClientID%>').value;
            var contxt = '';
            if (dc != '') {
                if (contxt != '') {
                    contxt = contxt + '||' + 'DC:' + dc;
                }
                else {
                    contxt = 'DC:' + dc;
                }

            }
            $find('AutoCompleteExtender_txtHotelName').set_contextKey(contxt);
        }

        function AutoCompleteExtender_HotelNameKeyUp() {
            $("#<%= txtHotelName.ClientID %>").bind("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtHotelName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
                SetHotelContextkey();
            });
            $("#<%= txtHotelName.ClientID %>").keyup("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtHotelName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
                SetHotelContextkey();
            });
        }


        function AutoCompleteExtenderKeyUp() {
            $("#<%= txtDestinationName.ClientID %>").bind("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtDestinationName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtDestinationcode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
                SetHotelContextkey();
            });

            $("#<%= txtDestinationName.ClientID %>").keyup(function (event) {
                var hiddenfieldID1 = document.getElementById('<%=txtDestinationName.ClientID%>');

                var hiddenfieldID = document.getElementById('<%=txtDestinationcode.ClientID%>');

                if (hiddenfieldID1.value == '') {

                    hiddenfieldID.value = '';
                }
                SetHotelContextkey();
            });
        }

        function AutoCompleteExtender_Country_KeyUp() {
            $("#<%= txtCountry.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtCountry.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtCountryCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtCountry.ClientID %>").keyup(function (event) {
                var hiddenfieldID1 = document.getElementById('<%=txtCountry.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtCountryCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }

        function AutoCompleteExtender_Customer_KeyUp() {
            $("#<%= txtCustomer.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtCustomer.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtCustomerCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtCustomer.ClientID %>").keyup(function (event) {
                var hiddenfieldID1 = document.getElementById('<%=txtCustomer.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtCustomerCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }

        function AutoCompleteExtender_RO_KeyUp() {
            $("#<%= txtRO.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtRO.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtROCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtRO.ClientID %>").keyup(function (event) {
                var hiddenfieldID1 = document.getElementById('<%=txtRO.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtROCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }

        function fnConfirm() {

        }

        function fnConfirmRateChange(ReqId, msg) {

            var confirm_value = document.createElement("INPUT");
            var req_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            req_value.type = "hidden";
            req_value.name = "req_value";

            if (confirm(msg)) {
                confirm_value.value = "Yes";
                req_value.value = ReqId;
                document.forms[0].appendChild(confirm_value);
                document.forms[0].appendChild(req_value);
                document.getElementById('<%=btnQuoteEdit.ClientID%>').click();
            } else {
                confirm_value.value = "No";
                document.forms[0].appendChild(confirm_value);
            }

            // lbQEdit_Click();

            //btnQuoteEdit
        }

        function fnConfirmPrivilege(ReqId) {
            var req_value = document.createElement("INPUT");
            req_value.type = "hidden";
            req_value.name = "req_value";
            if (confirm("Invoice already prepared; Do you want to edit?")) {
                req_value.value = ReqId;
                document.forms[0].appendChild(req_value);
                document.getElementById('<%=btnEdit.ClientID%>').click();
            }
        }

        function HotelNameautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {

                document.getElementById('<%=txtHotelCode.ClientID%>').value = eventArgs.get_value();
                GetHotelsDetails(document.getElementById('<%=txtHotelCode.ClientID%>').value);
            }
            else {
                document.getElementById('<%=txtHotelCode.ClientID%>').value = '';

            }

            SetHotelContextkey();
        }

        function redirectItinerary(urlName, strId) {
            window.open(urlName + "?Id=" + strId);
        } 
    
    </script>



    


      <script type="text/javascript">


          Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {


          });

          function RefreshContent() {
              Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandlerForProgressBar);
              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandlerForProgressBar);

          }
          function BeginRequestHandlerForProgressBar() {
              ShowProgess();
              ShowBookingAndTravelDate();
          }
          function EndRequestHandlerForProgressBar() {
              HideProgess();
              ShowBookingAndTravelDate();
              AutoCompleteExtenderKeyUp();
              AutoCompleteExtender_Customer_KeyUp();
              AutoCompleteExtender_Country_KeyUp();
              AutoCompleteExtender_HotelNameKeyUp();
              AutoCompleteExtender_RO_KeyUp();

              $("#<%= ddlBookingDate.ClientID %>").bind("change", function () {
                  var BookingDate = $("#<%= ddlBookingDate.ClientID %>").val()
                  if (BookingDate == 'Specific date') {

                      $('#dvBookingFromDate').show();
                      $('#dvBookingToDate').show();
                  }
                  else {
                      $('#dvBookingFromDate').hide();
                      $('#dvBookingToDate').hide();
                  }
              });
              $("#<%= ddlTravelDate.ClientID %>").bind("change", function () {
                  var TravelDate = $("#<%= ddlTravelDate.ClientID %>").val()
                  //  if (TravelDate == 'Specific date') {
                  if ((TravelDate == 'Check In or Check Out') || (TravelDate == 'CheckIn Date') || (TravelDate == 'CheckOut Date')) {//modified by abin on 20181206
                      $('#dvTravelFromDate').show();
                      $('#dvTravelToDate').show();
                  }
                  else {
                      $('#dvTravelFromDate').hide();
                      $('#dvTravelToDate').hide();
                  }
              });

              $("#<%= txtBookingFromDate.ClientID %>").bind("change", function () {
                  var d = document.getElementById('<%=txtBookingFromDate.ClientID%>').value;
                  var dp = d.split("/");
                  var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                  var currentMonth = dateOut.getMonth() ;
                  var currentDate = dateOut.getDate();
                  var currentYear = dateOut.getFullYear();
                  // alert(currentMonth);
                  $(".date-inpt-booking-to").datepicker("destroy");
                  $(".date-inpt-booking-to").datepicker({
                      minDate: new Date(currentYear, currentMonth, currentDate)
                  });

              });

              //              $("# < %= txtBookingFromDate.ClientID % >").bind("change", function () {

              var d = document.getElementById('<%=txtBookingFromDate.ClientID%>').value;
              if (d != '') {
                  var dp = d.split("/");
                  var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                  var currentMonth = dateOut.getMonth() ;
                  var currentDate = dateOut.getDate();
                  var currentYear = dateOut.getFullYear();
                  // alert(currentMonth);
                  $(".date-inpt-booking-to").datepicker("destroy");
                  $(".date-inpt-booking-to").datepicker({
                      minDate: new Date(currentYear, currentMonth, currentDate)
                  });
              }


              $("#<%= txtTravelFromDate.ClientID %>").bind("change", function () {
                  var d = document.getElementById('<%=txtTravelFromDate.ClientID%>').value;
                  var dp = d.split("/");
                  var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                  var currentMonth = dateOut.getMonth();
                  var currentDate = dateOut.getDate();
                  var currentYear = dateOut.getFullYear();
                  // alert(currentMonth);
                  $(".date-inpt-travel-to").datepicker("destroy");
                  $(".date-inpt-travel-to").datepicker({
                      minDate: new Date(currentYear, currentMonth, currentDate)
                  });

              });


              var d = document.getElementById('<%=txtTravelFromDate.ClientID%>').value;
              if (d != '') {
                  var dp = d.split("/");
                  var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                  var currentMonth = dateOut.getMonth() ;
                  var currentDate = dateOut.getDate();
                  var currentYear = dateOut.getFullYear();
                  // alert(currentMonth);
                  $(".date-inpt-travel-to").datepicker("destroy");
                  $(".date-inpt-travel-to").datepicker({
                      minDate: new Date(currentYear, currentMonth, currentDate)
                  });
              }



              $(".date-inpt-travel-from").datepicker({


          });
          if (document.getElementById('<%=hdTab.ClientID%>').value == '1') {

              $("#dvTabBookings").removeClass("myaccount-tab-active");
              $("#dvTabBookings").addClass("myaccount-tab-inactive");
              $("#dvTabPayments").removeClass("myaccount-tab-active");
              $("#dvTabPayments").addClass("myaccount-tab-inactive");
              $("#dvTabQuotes").removeClass("myaccount-tab-inactive");
              $("#dvTabQuotes").addClass("myaccount-tab-active");
              $('#lblSearchResultHeader').html('Search Results - Quotes');
              $("#dvSearchresultsBookings").css('display', 'none');
              $("#dvSearchresultsPayments").css('display', 'none');
              $("#dvSearchresultsQuotes").css('display', 'block');
              // document.getElementById('<%=hdTab.ClientID%>').value = '1';
              var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
              var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
              //if (div == 'RP/') {
              //    txtBookingRef.value = 'QR/';
              //}
              //if (div == 'RG/') {
              //    txtBookingRef.value = 'QG/';
              //}
              //changed by mohamed on 08/07/2018
              var hdQuoteBookingRef = document.getElementById('<%=hdQuoteBookingRef.ClientID%>');
              txtBookingRef.value = hdQuoteBookingRef.value + '/';

              $('#lblBookingRef').html('Quote Reference');
              $('#lblBookingFromDate').html('Quote From date');
              $('#lblBookingToDate').html('Quote To date');
              $('#lblBookingDate').html('Quote Date');
              $("#dvGFName").css('display', 'none');
              $("#dvGSName").css('display', 'none');
              $("#dvBookingStatus").css('display', 'none');
              $("#dvHotelConfNo").css('display', 'none');
          }
          else if (document.getElementById('<%=hdTab.ClientID%>').value == '2') {

              $("#dvTabBookings").removeClass("myaccount-tab-active");
              $("#dvTabBookings").addClass("myaccount-tab-inactive");
              $("#dvTabQuotes").removeClass("myaccount-tab-active");
              $("#dvTabQuotes").addClass("myaccount-tab-inactive");
              $("#dvTabPayments").removeClass("myaccount-tab-inactive");
              $("#dvTabPayments").addClass("myaccount-tab-active");              
              $('#lblSearchResultHeader').html('Search Results - Payments');
              $("#dvSearchresultsBookings").css('display', 'none');
              $("#dvSearchresultsQuotes").css('display', 'none');
              $("#dvSearchresultsPayments").css('display', 'block');              
              // document.getElementById('<%=hdTab.ClientID%>').value = '2';
              var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
              var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
              //if (div == 'RP/') {
              //    txtBookingRef.value = 'QR/';
              //}
              //if (div == 'RG/') {
              //    txtBookingRef.value = 'QG/';
              //}
              //changed by mohamed on 08/07/2018
              var hdPaymentBookingRef = document.getElementById('<%=hdPaymentBookingRef.ClientID%>');
              txtBookingRef.value = hdPaymentBookingRef.value + '/';

              $('#lblBookingRef').html('Quote Reference');
              $('#lblBookingFromDate').html('Quote From date');
              $('#lblBookingToDate').html('Quote To date');
              $('#lblBookingDate').html('Quote Date');
              $("#dvGFName").css('display', 'none');
              $("#dvGSName").css('display', 'none');
              $("#dvBookingStatus").css('display', 'none');
              $("#dvHotelConfNo").css('display', 'none');
          }
          else {

              $("#dvTabQuotes").removeClass("myaccount-tab-active");
              $("#dvTabQuotes").addClass("myaccount-tab-inactive");
              $("#dvTabPayments").removeClass("myaccount-tab-active");
              $("#dvTabPayments").addClass("myaccount-tab-inactive");
              $("#dvTabBookings").removeClass("myaccount-tab-inactive");
              $("#dvTabBookings").addClass("myaccount-tab-active");
              $('#lblSearchResultHeader').html('Search Results - Bookings');
              $("#dvSearchresultsBookings").css('display', 'block');
              $("#dvSearchresultsQuotes").css('display', 'none');
              $("#dvSearchresultsPayments").css('display', 'none');
              //  document.getElementById('<%=hdTab.ClientID%>').value = '0';

              var div = document.getElementById('<%=hdBookingRef.ClientID%>').value;
              var txtBookingRef = document.getElementById('<%=txtBookingRef.ClientID%>');
              if (div == 'RP/') {
                  txtBookingRef.value = 'RP/';
              }
              if (div == 'RG/') {
                  txtBookingRef.value = 'RG/';
              }
              $('#lblBookingRef').html('Booking Reference');
              $('#lblBookingFromDate').html('Booking From date');
              $('#lblBookingToDate').html('Booking To date');
              $('#lblBookingDate').html('Booking Date');
              $("#dvGFName").css('display', 'block');
              $("#dvGSName").css('display', 'block');
              $("#dvBookingStatus").css('display', 'block');
              $("#dvHotelConfNo").css('display', 'block');
          }

          $("#btnReset").button().click(function () {

              fnReset();

          });

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



      function ShowBookingAndTravelDate() {

          var BookingDate = $("#<%= ddlBookingDate.ClientID %>").val()
          $('#dvTravelFromDate').hide();
          $('#dvTravelToDate').hide();
          $('#dvBookingFromDate').hide();
          $('#dvBookingToDate').hide();
          if (BookingDate == 'Specific date') {

              $('#dvBookingFromDate').show();
              $('#dvBookingToDate').show();
          }
          else {
              $('#dvBookingFromDate').hide();
              $('#dvBookingToDate').hide();
          }
          var TravelDate = $("#<%= ddlTravelDate.ClientID %>").val()

          //if (TravelDate == 'Specific date') {
          if ((TravelDate == 'Check In or Check Out') || (TravelDate == 'CheckIn Date') || (TravelDate == 'CheckOut Date')) {//modified by abin on 20181206
              $('#dvTravelFromDate').show();
              $('#dvTravelToDate').show();
          }
          else {
              $('#dvTravelFromDate').hide();
              $('#dvTravelToDate').hide();
          }
      }

//      function btnLoadReport_onclick() {
//        
//      }

      </script>


</head>  
<body  onload="RefreshContent()">
    <form id="form1" runat="server">
  <!-- // authorize // -->
	<div class="overlay"></div>

<!-- \\ authorize \\-->
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"  EnablePartialRendering="true">
    </asp:ScriptManager>
    <header id="top">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
	<div class="header-a">
		<div class="wrapper-padding">	
         <div class="header-user"  style="margin-top:2px;" ><asp:Label ID="lblHeaderUserName" runat="server" ></asp:Label> </div>				
			<div class="header-phone" id="dvlblHeaderAgentName" runat="server" style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-account">
					<%--<asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>--%>
                <asp:LinkButton ID="btnMyAccount"   UseSubmitBehavior="False"
                    CssClass="header-account-button" runat="server" Text="Account" causesvalidation="true"></asp:LinkButton>
			</div>
              <div class="header-agentname" style="padding-left:105;margin-top:2px;"><asp:Label ID="lblHeaderAgentName" style="    padding: 0px 10px 0px 0px;" runat="server" ></asp:Label> </div>
            <div class="header-lang">
            <%--<asp:UpdatePanel runat="server"><ContentTemplate><asp:Button ID="btnLogOut"  UseSubmitBehavior="false" TabIndex="50"  OnClick="btnLogOut_Click"
                    CssClass="header-account-button" runat="server" Text="Log Out"></asp:Button>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <asp:LinkButton ID="btnLogOut"   UseSubmitBehavior="False" OnClick="btnLogOut_Click"
                    CssClass="header-account-button" runat="server" Text="Log Out" 
                    meta:resourcekey="btnLogOutResource1"></asp:LinkButton>
                <%--	<a href="#">Log Out</a>--%>
			</div>
			<div class="header-social"  style="display:none;">
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
			<div id="dvCurrency"  style="margin-top:2px;" runat="server" class="header-curency">
			</div>
               <div class="header-lang" id="dvMybooking" runat="server" style="padding-right:10px;margin-right:5px;">
          <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:Button ID="btnMyBooking" 
                    CssClass="header-account-button" runat="server" Text="MY BOOKING"></asp:Button>
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
			<div class="header-logo"><a href="Home.aspx?Tab=0"><img id="imgLogo" runat="server" alt="" /></a></div>
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
				<%--<div class="hdr-srch-devider"></div>--%>
				<a href="#" class="menu-btn"></a>
            <asp:Literal ID="ltMenu" runat="server"></asp:Literal>
			</div>
			<div class="clear"></div>
		</div>
	</div>	
</header>


<!-- main-cont -->
<div class="main-cont">
  <div class="body-wrapper" style="padding-top:150px;padding-bottom:50px;">
    <div class="wrapper-padding-full">
    <div class="page-head">
      <div class="page-title">My Account - <span></span></div>
      <div class="breadcrumbs">
        <%--*** Danny 17/10/2018--%>
        <%--<a href="#">Home</a> / <a href="#">My Account</a> --%>
               Home / My Account
      </div>
      <div class="clear"></div>
    </div>
        <div class="page-head">
            <div style="width:400px;height:30px;">
            <div id="dvTabBookings" style="border-right:1px solid #e3e3e3 !important;border-top:1px solid #e3e3e3  !important;" class="myaccount-tab-inactive" > <label style="cursor:pointer;"> BOOKINGS </label> </div>
            <div  id="dvTabQuotes"  style="border-right:1px solid #e3e3e3  !important;border-top:1px solid #e3e3e3  !important;border-left:1px solid #e3e3e3  !important;"  class="myaccount-tab-active" ><label  style="cursor:pointer;">QUOTES</label> </div>
            <div  id="dvTabPayments"  style="border-right:1px solid #e3e3e3  !important;border-top:1px solid #e3e3e3  !important;border-left:1px solid #e3e3e3  !important;"  class="myaccount-tab-active" ><label  style="cursor:pointer;">PAYMENTS</label> </div>  

            </div>
            <div class="page-search-content-search" style="margin-top:10px;box-shadow:1px 1px 5px #fff;">
                <div class="search-tab-content" style="min-height: 370px; max-height: 450px; padding-left: 55px;
                    padding-top: 15px;">
                    <asp:UpdatePanel ID="upnlSearch" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="page-search-p">
                                <!-- // -->
                                <div class="row-col-12">
                                    <div class="row-col-2" style="padding-right: 15px;">
                                        <div class="srch-tab-line no-margin-bottom">
                                            <label id="lblBookingRef">
                                                Booking Reference</label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtBookingRef" TabIndex="1" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-col-2" style="padding-right: 15px;">
                                        <div class="srch-tab-line no-margin-bottom">
                                            <label>
                                                Service type</label>
                                            <div class="select-wrapper">
                                                <asp:DropDownList ID="ddlServiceType" TabIndex="2" class="custom-select custom-select-ServiceType"
                                                    Style="text-transform: uppercase;" runat="server">
                                                    <asp:ListItem>All</asp:ListItem>
                                                    <asp:ListItem>Accommodation</asp:ListItem>
                                                    <asp:ListItem>Transfers</asp:ListItem>
                                                    <asp:ListItem>Airport Services</asp:ListItem>
                                                    <asp:ListItem>Visa</asp:ListItem>
                                                    <asp:ListItem>Tours</asp:ListItem>
                                                    <asp:ListItem>Other Services</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-col-4" style="padding-right: 15px; width: 34.5%;">
                                        <div class="srch-tab-line no-margin-bottom">
                                            <label>
                                                Destination/Location</label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtDestinationName" TabIndex="3" runat="server" placeholder="Example: dubai"></asp:TextBox>
                                                <asp:TextBox ID="txtDestinationCode" runat="server" Style="display: none"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txtDestinationName_AutoCompleteExtender" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetDeastinationList" TargetControlID="txtDestinationName"
                                                    OnClientItemSelected="DestinationNameautocompleteselected">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-col-2" style="padding-right: 15px;">
                                        <div class="srch-tab-line no-margin-bottom">
                                            <label>
                                                Agent Reference</label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtAgentRef" runat="server" TabIndex="4" placeholder="Agent Reference"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                                <%--     <div class="row-col-12" style="">--%>
                                <div id="dvGFName" class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Guest First Name</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtGuestFirstName" TabIndex="5" runat="server" placeholder="First Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div  id="dvGSName" class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Guest Second Name</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtGuestSecondName" TabIndex="6" runat="server" placeholder="Second Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Travel Date</label>
                                        <div class="select-wrapper">
                                            <asp:DropDownList ID="ddlTravelDate" TabIndex="7" class="custom-select custom-select-TravelDate"
                                                Style="text-transform: uppercase;" runat="server">
                                                <asp:ListItem>Any Dates</asp:ListItem>
                                                <asp:ListItem>Future bookings</asp:ListItem>
                                                <asp:ListItem>Past bookings</asp:ListItem>
                                                 <asp:ListItem>Check In or Check Out</asp:ListItem><%--modified by abin on 20181206--%>
                                                 <asp:ListItem>CheckIn Date</asp:ListItem><%--modified by abin on 20181206--%>
                                                  <asp:ListItem>CheckOut Date</asp:ListItem><%--modified by abin on 20181206--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-col-2" style="padding-right: 15px; margin-top: 15px; display: none;"
                                    id="dvTravelFromDate">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Travel From date</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtTravelFromDate" class="date-inpt-travel-from" placeholder="dd/mm/yyyy" AutoComplete="off"
                                                runat="server"></asp:TextBox>
                                            <span class="date-icon-travel"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-col-2" style="padding-right: 15px; margin-top: 15px; display: none;"
                                    id="dvTravelToDate">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Travel To Date</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtTravelToDate" class="date-inpt-travel-to" placeholder="dd/mm/yyyy"  AutoComplete="off"
                                                runat="server"></asp:TextBox>
                                            <span class="date-icon-travel"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label id="lblBookingDate" >
                                            Booking Date</label>
                                        <div class="select-wrapper">
                                            <asp:DropDownList ID="ddlBookingDate" TabIndex="8" class="custom-select custom-select-BookingDate"
                                                Style="text-transform: uppercase;" runat="server">
                                                <asp:ListItem>Any Dates</asp:ListItem>
                                                <asp:ListItem>Specific date</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-col-2" style="padding-right: 15px; margin-top: 15px; display: none;"
                                    id="dvBookingFromDate">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label id="lblBookingFromDate">
                                            Booking From date</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtBookingFromDate" class="date-inpt-travel-from" placeholder="dd/mm/yyyy"  AutoComplete="off"
                                                runat="server"></asp:TextBox>
                                            <span class="date-icon-travel"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-col-2" style="padding-right: 15px; margin-top: 15px; display: none;"
                                    id="dvBookingToDate">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label  id="lblBookingToDate">
                                            Booking To Date</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtBookingToDate" class="date-inpt-booking-to" placeholder="dd/mm/yyyy"  AutoComplete="off"
                                                runat="server"></asp:TextBox>
                                            <span class="date-icon-travel"></span>
                                        </div>
                                    </div>
                                </div>
                                <div  id="dvBookingStatus"  class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Booking Status</label>
                                        <div class="select-wrapper">
                                            <asp:DropDownList ID="ddlBookingStatus" TabIndex="9" class="custom-select custom-select-BookingStatus"
                                                Style="text-transform: uppercase;" runat="server">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Confirmed bookings</asp:ListItem>
                                                <asp:ListItem>On request bookings</asp:ListItem>
                                                <asp:ListItem>Amended bookings</asp:ListItem>
                                                <asp:ListItem>Cancelled bookings</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <%--</div>--%>
                                <%--   <div class="row-col-12">--%>
                                <div class="row-col-4" style="padding-right: 15px; margin-top: 15px; width: 34.5%;">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Hotels</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtHotelName" TabIndex="10" runat="server" placeholder="--"></asp:TextBox>
                                            <asp:TextBox ID="txtHotelCode" runat="server" Style="display: none"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtHotelName" runat="server" CompletionInterval="10"
                                                CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="-1" ServiceMethod="GetHotelName" TargetControlID="txtHotelName"
                                                UseContextKey="true" OnClientItemSelected="HotelNameautocompleteselected">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                </div>
                                <div id="dvHotelConfNo" class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Hotel Conf No</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtHotelConfNo" TabIndex="11" runat="server" placeholder="Hotel Conf No"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-col-4" style="padding-right: 15px; margin-top: 15px; width: 34.5%;"
                                    id="dvForAgent" runat="server">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Agent</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtCustomer" runat="server" TabIndex="12" placeholder="--"></asp:TextBox>
                                            <asp:TextBox ID="txtCustomerCode" runat="server" Style="display: none"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCustomer" runat="server" CompletionInterval="10"
                                                CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="-1" ServiceMethod="GetCustomers" TargetControlID="txtCustomer"
                                                OnClientItemSelected="Customersautocompleteselected">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-col-4" style="padding-right: 15px; margin-top: 15px; width: 34.5%;">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Source Country</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtCountry" runat="server" TabIndex="13" placeholder="--"></asp:TextBox>
                                            <asp:TextBox ID="txtCountryCode" runat="server" Style="display: none"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCountry" runat="server" CompletionInterval="10"
                                                CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtCountry"
                                                UseContextKey="true" OnClientItemSelected="Countryautocompleteselected">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-col-2" style="padding-right: 15px; margin-top: 15px;" id="dvForRO"
                                    runat="server">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            ro</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtRO" TabIndex="14" runat="server" placeholder="--"></asp:TextBox>
                                            <asp:TextBox ID="txtROCode" runat="server" Style="display: none"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="aceRO" runat="server" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="-1" ServiceMethod="GetRODetails" TargetControlID="txtRO"
                                                UseContextKey="true" OnClientItemSelected="ROautocompleteselected">
                                            </asp:AutoCompleteExtender>
                                            <%--
                                          <asp:TextBox ID="txtDummy" TabIndex="14"  runat="server"  Style="display: none" placeholder="--"></asp:TextBox>
                             
                                         <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="10"
                                             CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                             CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                             DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                             MinimumPrefixLength="-1" ServiceMethod="GetRODetails" TargetControlID="txtDummy"
                                             UseContextKey="true" OnClientItemSelected="Dummyautocompleteselected">
                                         </asp:AutoCompleteExtender>--%>
                                        </div>
                                    </div>
                                </div>
                                <%-- </div>--%>
                                <div class="clear">
                                </div>
                                <div class="row-col-12" style="padding-right: 15px; margin-top: 15px;">
                                    <div class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                        <asp:Button ID="btnSearch" class="authorize-btn" TabIndex="15" runat="server" OnClientClick="return ValidateSearch()"
                                            Text="Search"></asp:Button>
                                    </div>
                                    <div class="row-col-2" style="padding-right: 15px; margin-top: 15px; margin-left: -45px;">
                                        <input id="btnReset" type="button" class="authorize-btn" value="Reset" />
                                    </div>
                                     <div class="row-col-2"  style="padding-right: 15px; margin-top: 15px; margin-left: -45px;display:none;">
                                        <input id="btnLoadReport" type="button" class="authorize-btn" value="Load Report To Excel" onclick="return btnLoadReport_onclick()" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="sp-page">

   <div >
          
            <div class="row-col-12">
                <div class="row-col-12" id="dvSearchResults" style="overflow:hidden;">
                   

                    <div class="row-col-12" style="background-color: white; min-height: 150px; float: left;">
                    <div class="row-col-12" style="padding-bottom:30px;">
                        <div style="padding-top: 15px; padding-left: 5px;float:left;">
                        <asp:Label ID="lblSearchResultHeader" class="my-account-header" Text="Search Results - Bookings" runat="server"></asp:Label>
                
                        </div>
                        <div style="float: right;margin-top:10px;">
                            <input id="btnViewMenu" type="button" class="authorize-btn" value="Show Menu" /></div>
                    </div>
                        <div class="comlete-alert">
                            <asp:UpdatePanel ID="upnlsearchResults" runat="server">
                                <ContentTemplate>
                                <div style="width:100%;">
                                 <div style="width:100%;float:left;">
                                    <div style="width: 100%; min-height: 100px; max-height: 550px; padding-bottom: 20px;
                                        overflow: auto; padding-right: 0px;">
                                        <div id="dvWarning" runat="server" style="background-color: #F2F3F4; padding-top: 16px;
                                            padding-bottom: 16px; padding-left: 16px; text-align: center">
                                            <asp:Label ID="lblheader" runat="server" Text="Oops, No results to show. Can you please try a different combination?"
                                                CssClass="oop-s"   Font-Bold="True">
                                            </asp:Label></div>
                                           
                                           <div id="dvSearchresultsBookings" >
                                           
                                          
                                               <asp:GridView ID="gvSearchResults" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                            PageSize="7" GridLines="Horizontal" AllowPaging="True" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Request Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestId" runat="server" Text='<%# Bind("requestid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Room No"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("roomno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Booking Status"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("bookingstatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amend Status"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("amendstatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("requestdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Code" Visible="False">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("agentname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country Code" Visible="False">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country Name"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("sourcectryname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Type"  Visible="False" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("servicetype") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                              
                                                <asp:TemplateField HeaderText="Check In Date"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("checkindate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Check Out Date"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("checkoutdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Date"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("servicedate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Agent Ref">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("agentref") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hotel Conf No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("hotelconfno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="HotelName/Services"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("servicename") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Guest name" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("guestname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Time Limit"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label17" runat="server" Text='<%# Bind("timelimit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Can Edit" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCanEdit" runat="server" Text='<%# Bind("canedit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created By" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("createdby") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created Date"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("createddate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modified By"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label20" runat="server" Text='<%# Bind("modifiedby") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Modified"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label21" runat="server" Text='<%# Bind("lastmodified") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Copy">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbCopy" CssClass="my-account-btn" runat="server" OnClick="lbCopy_Click" >Copy</asp:LinkButton></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderText="Hotel Email">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbHotEmail" CssClass="my-account-btn" runat="server" OnClick="lbHotEmail_Click" ToolTip="Send HOTEL email to RO"  >Send</asp:LinkButton></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Email">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbAgeEmail" CssClass="my-account-btn" runat="server" OnClick="lbAgeEmail_Click" ToolTip="Send AGENT email to RO" >Send</asp:LinkButton></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbEdit" CssClass="my-account-btn" runat="server" OnClick="lbEdit_Click">Edit</asp:LinkButton></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbPrint" CssClass="my-account-btn" runat="server" OnClick="lbPrint_Click">Print</asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <div style="padding: 3px;">
                                                            <asp:LinkButton ID="lbItinerary" CssClass="my-account-btn" runat="server" OnClick="lbItinerary_Click">Itinerary</asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                           <%--     <asp:TemplateField>
                                                    <HeaderTemplate>
                                                   
                                                        <asp:ImageButton ID="imgbReadMore" runat="server" Width="30px" onclick="imgbReadMore_Click" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                            <PagerStyle CssClass="mygrid-pager" HorizontalAlign="Left"   />
                                            <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                            <AlternatingRowStyle  CssClass="mygrid-rows-alternative" HorizontalAlign="Left" />
                                          
                                        </asp:GridView>
                                          </div>
                                               <div id="dvSearchresultsQuotes" >
                                               
                                               <asp:GridView ID="gvSearchResultsQuotes" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                            PageSize="7" GridLines="Horizontal" AllowPaging="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Request Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestId" runat="server" Text='<%# Bind("requestid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Room No"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("roomno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amend Status"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("amendstatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("requestdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Code" Visible="False">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("agentname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country Code" Visible="False">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country Name"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("sourcectryname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Type"  Visible="False" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("servicetype") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Name" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("servicename") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Check In Date" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("checkindate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Check Out Date" Visible="False">                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("checkoutdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Date"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("servicedate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Hotel Conf No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("hotelconfno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Ref">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("agentref") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Columbus Ref">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblColumbusRef" runat="server" Text='<%# Bind("columbusref") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Time Limit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label17" runat="server" Text='<%# Bind("timelimit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Can Edit" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCanEdit" runat="server" Text='<%# Bind("canedit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created By"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("createdby") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created Date"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("createddate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modified By"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label20" runat="server" Text='<%# Bind("modifiedby") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Modified"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label21" runat="server" Text='<%# Bind("lastmodified") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Alternative Hotel" Visible="false">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbQAlternativeHotel" CssClass="my-account-btn" runat="server" 
                                                                OnClick="lbQAlternativeHotel_Click">Alternative Hotel</asp:LinkButton></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit/Book">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbQEdit" CssClass="my-account-btn" runat="server" 
                                                                OnClick="lbQEdit_Click">Edit/Book</asp:LinkButton></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbQPrint" CssClass="my-account-btn" runat="server" 
                                                                OnClick="lbQPrint_Click">Print                                                                                            
                                                             </asp:LinkButton>  
                                                             <asp:LinkButton id="ImgBtnDownload" runat="server" Visible ="false" CssClass="my-account-btn" style="    border-left: groove;" OnClick="imgbtnDownload_Click" ToolTip="Click to download Quote"><i class="fa fa-download"  aria-hidden="true"></i></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                           <%--     <asp:TemplateField>
                                                    <HeaderTemplate>
                                                   
                                                        <asp:ImageButton ID="imgbReadMore" runat="server" Width="30px" onclick="imgbReadMore_Click" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                            <PagerStyle CssClass="mygrid-pager" HorizontalAlign="Left"   />
                                            <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                            <AlternatingRowStyle  CssClass="mygrid-rows-alternative" HorizontalAlign="Left" />
                                          
                                        </asp:GridView>
                                               </div>

                                               <div id="dvSearchresultsPayments">
                                               
                                               <asp:GridView ID="gvSearchResultsPayments" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                            PageSize="7" GridLines="Horizontal" AllowPaging="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Request Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestId" runat="server" Text='<%# Bind("requestid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Room No"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("roomno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amend Status"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("amendstatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("requestdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Code" Visible="False">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("agentname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country Code" Visible="False">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country Name"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("sourcectryname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Type"  Visible="False" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("servicetype") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Name" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("servicename") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Check In Date" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("checkindate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Check Out Date" Visible="False">                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("checkoutdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Date"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("servicedate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Hotel Conf No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("hotelconfno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Ref">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("agentref") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Columbus Ref">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblColumbusRef" runat="server" Text='<%# Bind("columbusref") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Time Limit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label17" runat="server" Text='<%# Bind("timelimit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Can Edit" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCanEdit" runat="server" Text='<%# Bind("canedit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created By"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("createdby") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created Date"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("createddate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modified By"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label20" runat="server" Text='<%# Bind("modifiedby") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Modified"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label21" runat="server" Text='<%# Bind("lastmodified") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Alternative Hotel" Visible="false">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbQAlternativeHotel" CssClass="my-account-btn" runat="server">
                                                            Alternative Hotel</asp:LinkButton></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit/Book">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbQPayEdit" CssClass="my-account-btn" runat="server" 
                                                                OnClick="lbQPayEdit_Click">Edit/Book</asp:LinkButton></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <div style="padding: 5px;">
                                                            <asp:LinkButton ID="lbQPayPrint" CssClass="my-account-btn" runat="server" 
                                                                OnClick="lbQPayPrint_Click">Print                                                                                            
                                                             </asp:LinkButton>  
                                                             <asp:LinkButton id="ImgBtnDownload" runat="server" Visible ="false" CssClass="my-account-btn" style="    border-left: groove;" OnClick="imgbtnDownload_Click" ToolTip="Click to download Quote"><i class="fa fa-download"  aria-hidden="true"></i></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                           <%--     <asp:TemplateField>
                                                    <HeaderTemplate>
                                                   
                                                        <asp:ImageButton ID="imgbReadMore" runat="server" Width="30px" onclick="imgbReadMore_Click" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                            <PagerStyle CssClass="mygrid-pager" HorizontalAlign="Left"   />
                                            <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                            <AlternatingRowStyle  CssClass="mygrid-rows-alternative" HorizontalAlign="Left" />
                                          
                                        </asp:GridView>
                                               </div>

                                     </div>
                                        

                                        <asp:TextBox ID="txtFocus" style="border:none;" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                       <div class="mygrid-header" id="dvReadMore" runat="server" style="width:2%;float:left;height:32px;display:none;">
                                             <asp:ImageButton ID="imgbReadMoreNew" runat="server" Width="30px" Height="30px" ImageUrl="~/img/ReadMore.png" ToolTip="Read More" onclick="imgbReadMoreNew_Click" />
                                            </div></div>        
                                            
                                            
                                        
                                
                                            
                     <div  class="row-col-12"  style="background-color:#ebebeb !important; min-height:50px; float: left;padding:15px;">
                         <div style="width:100%;" >
                                              <div class="row-col-4" style=" margin-top:10px;">
                                                  <asp:Label ID="Label5" runat="server" Text="Retrieve Temporary Booking Reference"
                                                CssClass="oop-s"   Font-Bold="True"></asp:Label>
                                              </div>
                                                     <div class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                        <asp:TextBox ID="txtTempRefNo" class="input-a" TabIndex="15" runat="server" 
                                            Text=""></asp:TextBox>
                                    </div>

                                               <div class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                        <asp:Button ID="btnRetrieve" class="authorize-btn" TabIndex="15" runat="server" 
                                            Text="Retrieve"></asp:Button>
                                    </div>
                            
                                            </div>          </div>
                                                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>


                    <asp:Button ID="btnEdit" runat="server" style="display:none;" Text="Button" />
                    <asp:Button ID="btnCopy" runat="server" style="display:none;" Text="Button" />
                    <asp:Button ID="btnQuoteEdit" runat="server" style="display:none;" Text="Button" />
                </div>
                <div class="row-col-3" id="dvMenus" style="z-index: 999999; position: absolute; top: 649px;float:right;right:180px;
                    bottom: 10px; padding: 0; background-color:#141d1e; height: 420px;">
                    <div class="row-col-12">
                        <asp:DataList ID="dlSubMenuHeader" OnItemDataBound="dlSubMenuHeader_ItemDataBound" runat="server">
                           <ItemTemplate>
                               <div class="reasons-i">
                                   <div class="h-reasons">
                                       <div class="h-liked-lbl">
                                       <asp:Label ID="lblMenuHeader" Text='<%# Bind("parentname") %>'  runat="server"></asp:Label>
                                       </div>
                                       <asp:DataList ID="dlSubMenu" OnItemDataBound="dlSubMenu_ItemDataBound"  runat="server">
                                           <ItemTemplate>
                                               <div class="h-reasons-row">
                                                   <!-- // -->
                                                   <div class="reasons-h">
                                                       <div class="reasons-l" style="margin-top: -4px;">
                                                           <img alt="" width="25px" height="25px" src="img/icon-arrow-right.png">
                                                       </div>
                                                       <div class="reasons-r">
                                                           <div class="reasons-rb">
                                                               <div class="reasons-p">
                                                                   <div class="reasons-i-lbl">
                                                                       <a id="A1" runat="server"  class="reasons-i-lbl" style="text-decoration: none;" href='<%# Bind("pagename") %>'>
                                                                    <asp:Label ID="lblMenuDesc" Text='<%# Bind("menudesc") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblPageName" style="display:none;" Text='<%# Bind("pagename") %>' runat="server"></asp:Label>
                                                                       </a></div>
                                                               </div>
                                                           </div>
                                                           <br class="clear">
                                                       </div>
                                                   </div>
                                                   <div class="clear">
                                                   </div>
                                                   <!-- \\ -->
                                               </div>
                                           </ItemTemplate>
                                       </asp:DataList>
                           
                                     
                                   </div>
                               </div>
                           </ItemTemplate>
                        </asp:DataList>
                        <%--     <asp:TemplateField>
                                                    <HeaderTemplate>
                                                   
                                                        <asp:ImageButton ID="imgbReadMore" runat="server" Width="30px" onclick="imgbReadMore_Click" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>--%>
                    </div>
                </div>
            </div>


                    
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



    <center>
        <div id="Loading1" runat="server" style="height: 150px; width: 500px;">
            <img alt="" id="Image1" runat="server" src="~/img/page-loader.gif" width="200" />
            <h2 style="display: none;" class="page-loader-label">
                Processing please wait...</h2>
        </div>
    </center>
    <asp:ModalPopupExtender ID="ModalPopupDays" runat="server" BehaviorID="ModalPopupDays"
        TargetControlID="btnInvisibleGuest" CancelControlID="btnClose" PopupControlID="Loading1"
        BackgroundCssClass="ModalPopupForPageLoading">
    </asp:ModalPopupExtender>
    <input id="btnInvisibleGuest" runat="server" type="button" value="Cancel" style="display: none" />
    <input id="btnClose" type="button" value="Cancel" style="display: none" />
     <asp:HiddenField ID="hdLoginType" runat="server" />
    <asp:HiddenField ID="hdBookingRef" runat="server" />

    <asp:HiddenField ID="hdQuoteBookingRef" runat="server" />
    <asp:HiddenField ID="hdPaymentBookingRef" runat="server" />

    <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
 <asp:HiddenField ID="hdWhiteLabel" runat="server" />
  <asp:HiddenField ID="hdFindBooking" runat="server" />
   <asp:HiddenField ID="hdPrintVoucher" runat="server" />
    <asp:HiddenField ID="hdTab" runat="server" />
     <asp:HiddenField ID="hdRequestId" runat="server" />
       <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
    </form>
</body>
</html>
