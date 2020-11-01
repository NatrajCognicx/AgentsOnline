<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HotelFreeFormBooking.aspx.vb" Inherits="HotelFreeFormBooking" %>

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

<%--<link href='http://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css' />
<link href='http://fonts.googleapis.com/css?family=Lora:400,400italic' rel='stylesheet' type='text/css' />
<link href='http://fonts.googleapis.com/css?family=Raleway:100,200,300,400,600,700,800' rel='stylesheet' type='text/css' />
<link href='http://fonts.googleapis.com/css?family=Raleway:400,600' rel='stylesheet' type='text/css' />
<link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700&amp;subset=latin,cyrillic' rel='stylesheet' type='text/css' />
<link href='http://fonts.googleapis.com/css?family=Lato:400,700&amp;subset=latin,latin-ext' rel='stylesheet' type='text/css' />
<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700&amp;subset=latin,cyrillic' rel='stylesheet' type='text/css' />--%>

<%--***Danny 18/08/2018 fa fa-star--%>
<link href="css/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
<%--  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">--%>
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
        .blink {
            background-color: yellow;
            color:Green;
            -webkit-animation: blink 800ms step-end infinite;
            animation: blink 800ms step-end infinite;
            }
            @-webkit-keyframes blink { 50% { background-color: red; }}
            @keyframes blink { 50% { background-color: red; }
        }

        /*=============*/

        .pulse {
            background-color: yellow;
            color:Green;
            -webkit-animation: blink 1000ms infinite;
            animation: blink 1000ms infinite;
            }
            @-webkit-keyframes blink { 50% { background-color: red; }}
            @keyframes blink { 50% { background-color: red; }
        }

        /*=============*/

        .pulse2 
        {
            color:Green;
            -webkit-animation: pulse 400ms infinite alternate;
            animation: pulse 400ms infinite alternate;
            }
            @-webkit-keyframes pulse {
            0% { background-color: red; }
            100% { background-color: yellow; }
            }
            @keyframes pulse {
            0% { background-color: red; }
            100% { background-color: yellow; }
        }
      
  .mygrid
    {
	width: 100%;
	min-width: 100%;
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
	font-size: 16px;
	font-size:small;
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
	font-size: 14px;
	color: #455051;
	min-height: 25px;
	text-align: left;
	border: 1px solid #EDE7E1;
	vertical-align:middle;
	
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
      .myhotel-tab-active
      {
          float:left;width:100px;height:30px;padding-top:10px;padding-left:1px;background-color:White;color:#ff7200;font-family:'Montserrat';font-size:12px;border-right:1px solid #e3e3e3;border-top:1px solid #f2f2f2;font-weight:600;cursor:pointer;z-index:9;
      }
       .myhotel-tab-inactive
      {
          float:left;width:100px;height:30px;padding-top:10px;padding-left:1px;background-color:#696969;color:White;margin-left:5px;font-family:'Montserrat';border-right:1px solid #e3e3e3;border-top:1px solid #f2f2f2;font-size:12px;font-weight:600;cursor:pointer;z-index:9;
      }
      .myhotel-tab-active:hover
      {
          color:#ff7200;
       background-color:White  !important;
      }
            .myhotel-tab-inactive label
      {
      	color:white !important;
      }
       .myhotel-tab-active label
      {
      	color:#ff7200 !important;
      }
              .myhotel-tab-active label:hover
      {
          color:#ff7200 !important;
       
      }
       .myhotel-tab-inactive:hover
      {
          color:#ff7200;
       background-color:#ff7200  !important;
      }
  
  </style>
  
<%--  <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"   type="text/javascript"></script>--%>
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
        function CheckShiftingCheckbox(chk) {
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


        function popup() {

            //var pnl = document.getElementById("<%= pnlShiftHotel.ClientID %>");
            $('#pnlShiftHotel').attr("style", "display:block");
        }

        function myMap(lati, long, hotelname) {

            var uluru = { lat: parseFloat(lati), lng: parseFloat(long) };
            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 18,
                center: uluru
            });
            var marker = new google.maps.Marker({
                position: uluru,
                map: map,
                title: hotelname
            });

            marker.addListener('click', function () {
                map.setZoom(20);
                map.setCenter(marker.getPosition());
            });

        }
      
    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $("#<%= txtCheckIn.ClientID %>").bind("change", function () {

                var d = document.getElementById('<%=txtCheckIn.ClientID%>').value;
                var dp = d.split("/");
//                var dateOut = new Date(dp[2], dp[1], dp[0]);
//                var currentMonth = dateOut.getMonth() - 1;
                   var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;

                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
               
                // alert(currentMonth);
                $(".date-inpt-check-out").datepicker("destroy");
                $(".date-inpt-check-out").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });

                FindNoOfNights(0); <%--**** Danny 22/07/2018--%>
            });

            $("#<%= txtCheckOut.ClientID %>").bind("change", function () {
                FindNoOfNights(1); <%--**** Danny 22/07/2018--%>
            });
            $("#<%= txtNoOfNights.ClientID %>").bind("change", function () {
                FindNoOfNights(2); <%--**** Danny 22/07/2018--%>

            });	
            <%--**** Danny 22/07/2018--%>
            function FindNoOfNights(ch) {
                var oneDay = 24 * 60 * 60 * 1000;
                var dIn = document.getElementById('<%=txtCheckIn.ClientID%>').value;
                var dOut = document.getElementById('<%=txtCheckOut.ClientID%>').value;
                var txtNoDay = document.getElementById('<%=txtNoOfNights.ClientID%>').value;
                if (ch == 0) {
                    if ((dIn != '') && (dOut != '')) {
                        PageMethods.GetNoOfNights(parseDate(dIn), parseDate(dOut), CallSuccess, CallFailed)
                    }
                    else {
                        txtNoDay.value = '';
                    }
                }
                if (ch == 1) {
                    if ((dIn != '') && (dOut != '')) {
                        PageMethods.GetNoOfNights(parseDate(dIn), parseDate(dOut), CallSuccess, CallFailed)
                    }
                    else {
                        txtNoDay.value = '';
                    }
                }
                if (ch == 2) {
                    if ((dIn != '') && (txtNoDay != '')) {
                        PageMethods.GetEndDate(parseDate(dIn), txtNoDay, CallDSuccess, CallDFailed)
                    }
                    else {
                        dOut.value = '';
                    }					
                }	
            }
			<%--**** Danny 22/07/2018--%>
            function CallDSuccess(res) {
                var txtCheckOut = document.getElementById('<%=txtCheckOut.ClientID%>');
                txtCheckOut.value = res;
            }
			<%--**** Danny 22/07/2018--%>
            function CallDFailed(res) {
                var txtNoDay = document.getElementById('<%=txtCheckOut.ClientID%>');
                txtNoDay.value = '';
            }

            function CallSuccess(res) {
                var txtNoDay = document.getElementById('<%=txtNoOfNights.ClientID%>');
                txtNoDay.value = res;
            }

            // alert message on some failure
            function CallFailed(res) {
                var txtNoDay = document.getElementById('<%=txtNoOfNights.ClientID%>');
                txtNoDay.value = '';
            }

            
            function parseDate(str) {
                var mdy = str.split('/');

                return mdy[1] + '/' + mdy[0] + '/' + mdy[2];
            }


            CommonJavaScriptFunctions();

            // *** Hotel child age change based on room on 22/05/2017





            AutoCompleteExtender_HotelNameKeyUp();
            AutoCompleteExtenderKeyUp();
            AutoCompleteExtender_Customer_KeyUp();
            AutoCompleteExtender_Country_KeyUp();

            AutoCompleteExtender_MealPlan_KeyUp();
            AutoCompleteExtender_Accomodation_KeyUp();
            AutoCompleteExtender_AccomodationType_KeyUp();
            AutoCompleteExtender_RoomType_KeyUp();
            AutoCompleteExtender_SupplierAgent_KeyUp();



            $("#btnReset").button().click(function () {

                document.getElementById('<%=txtDestinationName.ClientID%>').value = '';
                document.getElementById('<%=txtDestinationCode.ClientID%>').value = '';
                document.getElementById('<%=txtHotelName.ClientID%>').value = '';
                document.getElementById('<%=txtHotelCode.ClientID%>').value = '';

                if (document.getElementById("<%= chkShifting.ClientID %>").checked != true) {
                    document.getElementById('<%=txtCheckIn.ClientID%>').value = '';
                    var dropDown = document.getElementById('<%=ddlRoom.ClientID%>');
                    dropDown.selectedIndex = "0";
                    $('.custom-select-ddlroom').next('span').children('.customSelectInner').text(jQuery("#ddlRoom :selected").text());
                }

                //document.getElementById('<%=txtCheckin.ClientID%>').value = '';
                document.getElementById('<%=txtNoOfNights.ClientID%>').value = '';
                document.getElementById('<%=txtCheckOut.ClientID%>').value = '';

                document.getElementById('<%=txtSupplierAgentCode.ClientID%>').value = '';
                document.getElementById('<%=txtSupplierAgent.ClientID%>').value = '';

                document.getElementById('<%=txtContract.ClientID%>').value = '';

                document.getElementById('<%=txtRoomTypeCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoomType.ClientID%>').value = '';

                document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom1AccomodationType.ClientID%>').value = '';

                document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom1Accomodation.ClientID%>').value = '';

                document.getElementById('<%=txtRoom2AccomodationTypeCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom2AccomodationType.ClientID%>').value = '';

                document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom2Accomodation.ClientID%>').value = '';

                document.getElementById('<%=txtRoom3AccomodationTypeCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom3AccomodationType.ClientID%>').value = '';

                document.getElementById('<%=txtRoom3AccomodationCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom3Accomodation.ClientID%>').value = '';

                document.getElementById('<%=txtRoom4AccomodationTypeCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom4AccomodationType.ClientID%>').value = '';

                document.getElementById('<%=txtRoom4AccomodationCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom4Accomodation.ClientID%>').value = '';

                document.getElementById('<%=txtRoom5AccomodationTypeCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom5AccomodationType.ClientID%>').value = '';

                document.getElementById('<%=txtRoom5AccomodationCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom5Accomodation.ClientID%>').value = '';

                document.getElementById('<%=txtRoom6AccomodationTypeCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom6AccomodationType.ClientID%>').value = '';

                document.getElementById('<%=txtRoom6AccomodationCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom6Accomodation.ClientID%>').value = '';

                
                document.getElementById('<%=txtRoom7AccomodationCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom7Accomodation.ClientID%>').value = '';

                
                document.getElementById('<%=txtRoom8AccomodationCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom8Accomodation.ClientID%>').value = '';

                
                document.getElementById('<%=txtRoom9AccomodationCode.ClientID%>').value = '';
                document.getElementById('<%=txtRoom9Accomodation.ClientID%>').value = '';




                document.getElementById('<%=txtMealPlan.ClientID%>').value = '';
                document.getElementById('<%=txtMealPlanCode.ClientID%>').value = '';

                document.getElementById('<%=txtCancelFreeUpTo.ClientID%>').value = '';
                document.getElementById('<%=chkNonrefundable.ClientID%>').checked = false;

                // *** Hotel child age change based on room on 23/05/2017
                if (document.getElementById("<%= chkShifting.ClientID %>").checked != true) {
                    ClearRoomAdultChild();
                    $('#dvRoom1Childs').hide();
                    $('#dvRoom2Childs').hide();
                    $('#dvRoom3Childs').hide();
                    $('#dvRoom4Childs').hide();
                    $('#dvRoom5Childs').hide();
                    $('#dvRoom6Childs').hide();
                      $('#dvRoom7Childs').hide();
                        $('#dvRoom8Childs').hide();
                          $('#dvRoom9Childs').hide();
                    $('#dvFullAdultChild').hide();
                }
                //***

                SethotelContextkey();
                $find('AutoCompleteExtendertxtRoomType').set_contextKey('');
                $find('AutoCompleteExtenderRoom1AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom2AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom3AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom4AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom5AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom6AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom7AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom8AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom9AccomodationType').set_contextKey('');
                $find('AutoCompleteExtendertxtMealPlan').set_contextKey('');

                // 
            });


            //****
        });


        //**Document read end **

        /// //ID:119 modified by abin on 20180925 ========= Satrt
        function CalculateRoomTotalPrice(lblSaleTotal, gv, dWlMarkup, lblwlbreakupPrice, txtsaleprice) {
           
            var objGridView = document.getElementById(gv);
            var txtrowcnt = document.getElementById('<%=hdgvPricebreakupRowwCount.ClientID%>');
            var lblSaleTotal_ = document.getElementById(lblSaleTotal);
            var lblwlbreakupPrice_ = document.getElementById(lblwlbreakupPrice);
            var txtsaleprice_ = document.getElementById(txtsaleprice);
           lblwlbreakupPrice_.innerHTML = (Math.round(parseFloat(txtsaleprice_.value) * parseFloat(dWlMarkup), 0)).toString();
            var intRows = txtrowcnt.value;
            var fSaleTotal = 0;
            var fCostSaleTotal = 0;
         
            for (j = 1; j <= intRows; j++) {
                var valSale = objGridView.rows[j].cells[2].children[0].children[1].value;
                if (valSale == '') {
                    valSale = 0;
                }
                fSaleTotal = fSaleTotal + parseFloat(valSale);
//                var valCostSale = objGridView.rows[j].cells[2].children[0].children[0].value;
//           
//                fCostSaleTotal = fCostSaleTotal + parseFloat(valCostSale);
         
            }
         //   alert(fSaleTotal.toString());
           // var fCostSaleTotal_formatted = format_float(fSaleTotal, 0);

             var fCostSaleTotal_formatted;
                if (fSaleTotal == 0) {
                    fCostSaleTotal_formatted = 0;
                }
                else {
                    fCostSaleTotal_formatted = format_float(fSaleTotal, 0);
                }

            lblSaleTotal_.innerHTML = fCostSaleTotal_formatted.toString();
        }

        function CalculateUSDAndCostPriceTotal(costPrice, convrate, lbl, lblCostTotal, gv, salecurcode) {

            Number.prototype.round = function (decimals) {
                return Number((Math.round(this + "e" + decimals) + "e-" + decimals));
            }
       
            var vcostPrice = document.getElementById(costPrice).value;
             if (vcostPrice == '') {
                vcostPrice = 0;
            }
            var cost = parseFloat(vcostPrice) * parseFloat(convrate); //innerHTML
            var usdLabel = document.getElementById(lbl);
            var vCostTotal = document.getElementById(lblCostTotal);
            var v = cost.round(2);
            usdLabel.innerHTML = '(' + v.toString() + ' '+ salecurcode + ')';

            var objGridView = document.getElementById(gv);
            var txtrowcnt = document.getElementById('<%=hdgvPricebreakupRowwCount.ClientID%>');
            
            var lblCostTotal_ = document.getElementById(lblCostTotal);
            var intRows = txtrowcnt.value;
            var fCostSaleTotal = 0;

            for (j = 1; j <= intRows; j++) {

                var valCostSale = objGridView.rows[j].cells[3].children[0].children[0].value;
                if (valCostSale == '') {
                    valCostSale = 0;
                }
        
                fCostSaleTotal = fCostSaleTotal + parseFloat(valCostSale);
            }

            var fCostSaleTotal_formatted = fCostSaleTotal.round(0);
            lblCostTotal_.innerHTML = fCostSaleTotal_formatted.toString();

        }

         /// //ID:119 modified by abin on 20180925 ========= End
        function format_float(number, extra_precision) {
            precision = float_exponent(number) + (extra_precision || 0)
            return number.toFixed(precision).split(/\.?0+$/)[0]
        }





        function CalculateSpecialEventSaleValue(lblNoOfPax, txtPaxRate, lblSpecialEventValue, lblpaxcurrcode, wlSpecialEventValue, dwlmarkup, wlcurrcode, lblwlPaxRate, lblSpecialEventValueNew) {

            var txtPaxRate_ = document.getElementById(txtPaxRate);
            var lblSpecialEventValue_ = document.getElementById(lblSpecialEventValue);
            var lblSpecialEventValueNew_ = document.getElementById(lblSpecialEventValueNew);
            var lblwlPaxRate_ = document.getElementById(lblwlPaxRate);
            var wlSpecialEventValue_ = document.getElementById(wlSpecialEventValue);
            lblSpecialEventValue_.innerHTML = ((parseFloat(lblNoOfPax) * parseFloat(txtPaxRate_.value))).toString() + ' ' + lblpaxcurrcode;
            lblSpecialEventValueNew_.innerHTML = ((parseFloat(lblNoOfPax) * parseFloat(txtPaxRate_.value))).toString();
            wlSpecialEventValue_.innerHTML = Math.round((parseFloat(lblNoOfPax) * parseFloat(txtPaxRate_.value)) * parseFloat(dwlmarkup)).toString() + ' ' + wlcurrcode;
            lblwlPaxRate_.innerHTML = Math.round(parseFloat(txtPaxRate_.value) * parseFloat(dwlmarkup)).toString();
        }
        function CalculateSpecialEventCostValue(lblNoOfPax, txtPaxRate, lblSpecialEventValue, lblpaxcurrcode, lblSpecialEventValueNew) {

            var txtPaxRate_ = document.getElementById(txtPaxRate);
            var lblSpecialEventValue_ = document.getElementById(lblSpecialEventValue);
            var lblSpecialEventValueNew_ = document.getElementById(lblSpecialEventValueNew);
        
            lblSpecialEventValue_.innerHTML = ((parseFloat(lblNoOfPax) * parseFloat(txtPaxRate_.value))).toString() + ' ' + lblpaxcurrcode;
            lblSpecialEventValueNew_.innerHTML = ((parseFloat(lblNoOfPax) * parseFloat(txtPaxRate_.value))).toString();

        }

        function fnBindCheckOutDate() {
            var d = document.getElementById('<%=txtCheckIn.ClientID%>').value;
            var dp = d.split("/");
//            var dateOut = new Date(dp[2], dp[1], dp[0]);
//            var currentMonth = dateOut.getMonth() - 1;
               var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;

            var currentDate = dateOut.getDate();
            var currentYear = dateOut.getFullYear();
            // alert(currentMonth);
            $(".date-inpt-check-out").datepicker("destroy");
            $(".date-inpt-check-out").datepicker({
                minDate: new Date(currentYear, currentMonth, currentDate)
            });

            FindNoOfNights();
        }
        function fnBindSpecEventDate() {

            var dCheckInMin = document.getElementById('<%=txtCheckIn.ClientID%>').value;
            var dCheckOutMax = document.getElementById('<%=txtCheckOut.ClientID%>').value;
            if ((dCheckInMin == '') || (dCheckInMin == '0') || (dCheckOutMax == '') || (dCheckOutMax == '0')) {

                var date = new Date();
                var currentMonth = date.getMonth();
                var currentDate = date.getDate();
                var currentYear = date.getFullYear();

                $(".spcl-date-inpt-check-in").datepicker({

                    minDate: new Date(currentYear, currentMonth, currentDate)

                });
            }
            else {
                var dp1 = dCheckInMin.split("/");
                var date1 = new Date(dp1[2], dp1[1], dp1[0]);
                var currentMonth1 = date1.getMonth() - 1;
                var currentDate1 = date1.getDate();
                var currentYear1 = date1.getFullYear();

                var dCheckOutMax = document.getElementById('<%=txtCheckOut.ClientID%>').value;
                var dp4 = dCheckOutMax.split("/");
                var date4 = new Date(dp4[2], dp4[1], dp4[0]);
                var currentMonth4 = date4.getMonth() - 1;
                var currentDate4 = date4.getDate();
                var currentYear4 = date4.getFullYear();


                $(".spcl-date-inpt-check-in").datepicker({
                    minDate: new Date(currentYear1, currentMonth1, currentDate1),
                    maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                });
            }
        }


        function CommonJavaScriptFunctions() {

            $('#dvFullAdultChild').hide();
            $("#<%= ddlRoom.ClientID %>").bind("change", function () {
                var room = $("#<%= ddlRoom.ClientID %>").val()
                ShowAndHideHotelAdultAndChild(room);
            });


            $('#dvRoom1Childs').hide();
            $("#<%= ddlRoom1Child.ClientID %>").bind("change", function () {
                var child = $("#<%= ddlRoom1Child.ClientID %>").val()
                ShowAndHideHotelChildAges('1', child)

            });




            $('#dvRoom2Childs').hide();
            $("#<%= ddlRoom2Child.ClientID %>").bind("change", function () {
                var child = $("#<%= ddlRoom2Child.ClientID %>").val()
                ShowAndHideHotelChildAges('2', child)
            });

            $('#dvRoom3Childs').hide();
            $("#<%= ddlRoom3Child.ClientID %>").bind("change", function () {
                var child = $("#<%= ddlRoom3Child.ClientID %>").val()
                ShowAndHideHotelChildAges('3', child)
            });

            $('#dvRoom4Childs').hide();
            $("#<%= ddlRoom4Child.ClientID %>").bind("change", function () {
                var child = $("#<%= ddlRoom4Child.ClientID %>").val()
                ShowAndHideHotelChildAges('4', child)
            });

            $('#dvRoom5Childs').hide();
            $("#<%= ddlRoom5Child.ClientID %>").bind("change", function () {
                var child = $("#<%= ddlRoom5Child.ClientID %>").val()
                ShowAndHideHotelChildAges('5', child)
            });

            $('#dvRoom6Childs').hide();
            $("#<%= ddlRoom6Child.ClientID %>").bind("change", function () {
                var child = $("#<%= ddlRoom6Child.ClientID %>").val()
                ShowAndHideHotelChildAges('6', child)
            });

              $('#dvRoom7Childs').hide();
            $("#<%= ddlRoom7Child.ClientID %>").bind("change", function () {
                var child = $("#<%= ddlRoom7Child.ClientID %>").val()
                ShowAndHideHotelChildAges('7', child)
            });

              $('#dvRoom8Childs').hide();
            $("#<%= ddlRoom8Child.ClientID %>").bind("change", function () {
                var child = $("#<%= ddlRoom8Child.ClientID %>").val()
                ShowAndHideHotelChildAges('8', child)
            });

              $('#dvRoom9Childs').hide();
            $("#<%= ddlRoom9Child.ClientID %>").bind("change", function () {
                var child = $("#<%= ddlRoom9Child.ClientID %>").val()
                ShowAndHideHotelChildAges('9', child)
            });

            ShowAdultChild();

//            if (document.getElementById('<%=chkNonrefundable.ClientID%>').checked == true) {
//                $("#<%= txtCancelFreeUpTo.ClientID %>").val('0')
//                document.getElementById('<%=txtCancelFreeUpTo.ClientID%>').setAttribute("readonly", true);
//            }
//            else {
//                $("#<%= txtCancelFreeUpTo.ClientID %>").val('');
//                document.getElementById('<%=txtCancelFreeUpTo.ClientID%>').removeAttribute("readonly");
//            }

            $("#<%= chkNonrefundable.ClientID %>").bind("change", function () {
                if (document.getElementById('<%=chkNonrefundable.ClientID%>').checked == true) {
                    $("#<%= txtCancelFreeUpTo.ClientID %>").val('0')
                    document.getElementById('<%=txtCancelFreeUpTo.ClientID%>').setAttribute("readonly", true);
                }
                else {
                    $("#<%= txtCancelFreeUpTo.ClientID %>").val('');
                    document.getElementById('<%=txtCancelFreeUpTo.ClientID%>').removeAttribute("readonly");
                }


            });

        }

        function fnShowSpecialEvents() {
  
            if (document.getElementById('<%=chkSpecialEvents.ClientID%>').checked == true) {
                $("#pnlSpecialEvents").css('display', 'block');
            }
            else {


                $("#pnlSpecialEvents").css('display', 'none');
            }
        }

    function ShowAdultChild() {

        $('#dvRoom1Childs').hide();
        $('#dvRoom2Childs').hide();
        $('#dvRoom3Childs').hide();
        $('#dvRoom4Childs').hide();
        $('#dvRoom5Childs').hide();
        $('#dvRoom6Childs').hide();
         $('#dvRoom7Childs').hide();
          $('#dvRoom8Childs').hide();
           $('#dvRoom9Childs').hide();
        $('#dvFullAdultChild').hide();

        var Room = $("#<%= ddlRoom.ClientID %>").val()
        ShowAndHideHotelAdultAndChild(Room);

        if (Room == 0) {
            $('#dvFullAdultChild').hide();
        }
        else if (Room == 1) {


            var child1 = $("#<%= ddlRoom1Child.ClientID %>").val()
            ShowAndHideHotelChildAges('1', child1)
        }

        else if (Room == 2) {

            var child1 = $("#<%= ddlRoom1Child.ClientID %>").val()
            ShowAndHideHotelChildAges('1', child1)
            var child2 = $("#<%= ddlRoom2Child.ClientID %>").val()
            ShowAndHideHotelChildAges('2', child2)



        }

        else if (Room == 3) {

            var child1 = $("#<%= ddlRoom1Child.ClientID %>").val()
            ShowAndHideHotelChildAges('1', child1)
            var child2 = $("#<%= ddlRoom2Child.ClientID %>").val()
            ShowAndHideHotelChildAges('2', child2)

            var child3 = $("#<%= ddlRoom3Child.ClientID %>").val()
            ShowAndHideHotelChildAges('3', child3)


        }

        else if (Room == 4) {
            var child1 = $("#<%= ddlRoom1Child.ClientID %>").val()
            ShowAndHideHotelChildAges('1', child1)
            var child2 = $("#<%= ddlRoom2Child.ClientID %>").val()
            ShowAndHideHotelChildAges('2', child2)

            var child3 = $("#<%= ddlRoom3Child.ClientID %>").val()
            ShowAndHideHotelChildAges('3', child3)
            var child4 = $("#<%= ddlRoom4Child.ClientID %>").val()
            ShowAndHideHotelChildAges('4', child4)
        }

        else if (Room == 5) {

            var child1 = $("#<%= ddlRoom1Child.ClientID %>").val()
            ShowAndHideHotelChildAges('1', child1)
            var child2 = $("#<%= ddlRoom2Child.ClientID %>").val()
            ShowAndHideHotelChildAges('2', child2)

            var child3 = $("#<%= ddlRoom3Child.ClientID %>").val()
            ShowAndHideHotelChildAges('3', child3)
            var child4 = $("#<%= ddlRoom4Child.ClientID %>").val()
            ShowAndHideHotelChildAges('4', child4)
            var child5 = $("#<%= ddlRoom5Child.ClientID %>").val()
            ShowAndHideHotelChildAges('5', child5)


        }

        else if (Room == 6) {

            var child1 = $("#<%= ddlRoom1Child.ClientID %>").val()
            ShowAndHideHotelChildAges('1', child1)
            var child2 = $("#<%= ddlRoom2Child.ClientID %>").val()
            ShowAndHideHotelChildAges('2', child2)

            var child3 = $("#<%= ddlRoom3Child.ClientID %>").val()
            ShowAndHideHotelChildAges('3', child3)
            var child4 = $("#<%= ddlRoom4Child.ClientID %>").val()
            ShowAndHideHotelChildAges('4', child4)
            var child5 = $("#<%= ddlRoom5Child.ClientID %>").val()
            ShowAndHideHotelChildAges('5', child5)
            var child6 = $("#<%= ddlRoom6Child.ClientID %>").val()
            ShowAndHideHotelChildAges('6', child6)


        }

         else if (Room == 7) {

            var child1 = $("#<%= ddlRoom1Child.ClientID %>").val()
            ShowAndHideHotelChildAges('1', child1)
            var child2 = $("#<%= ddlRoom2Child.ClientID %>").val()
            ShowAndHideHotelChildAges('2', child2)

            var child3 = $("#<%= ddlRoom3Child.ClientID %>").val()
            ShowAndHideHotelChildAges('3', child3)
            var child4 = $("#<%= ddlRoom4Child.ClientID %>").val()
            ShowAndHideHotelChildAges('4', child4)
            var child5 = $("#<%= ddlRoom5Child.ClientID %>").val()
            ShowAndHideHotelChildAges('5', child5)
            var child6 = $("#<%= ddlRoom6Child.ClientID %>").val()
            ShowAndHideHotelChildAges('6', child6)

              var child7 = $("#<%= ddlRoom7Child.ClientID %>").val()
            ShowAndHideHotelChildAges('7', child7)
        }
        
         else if (Room == 8) {

            var child1 = $("#<%= ddlRoom1Child.ClientID %>").val()
            ShowAndHideHotelChildAges('1', child1)
            var child2 = $("#<%= ddlRoom2Child.ClientID %>").val()
            ShowAndHideHotelChildAges('2', child2)

            var child3 = $("#<%= ddlRoom3Child.ClientID %>").val()
            ShowAndHideHotelChildAges('3', child3)
            var child4 = $("#<%= ddlRoom4Child.ClientID %>").val()
            ShowAndHideHotelChildAges('4', child4)
            var child5 = $("#<%= ddlRoom5Child.ClientID %>").val()
            ShowAndHideHotelChildAges('5', child5)
            var child6 = $("#<%= ddlRoom6Child.ClientID %>").val()
            ShowAndHideHotelChildAges('6', child6)

              var child7 = $("#<%= ddlRoom7Child.ClientID %>").val()
            ShowAndHideHotelChildAges('7', child7)
              var child8 = $("#<%= ddlRoom8Child.ClientID %>").val()
            ShowAndHideHotelChildAges('8', child8)

        }
         else if (Room == 9) {

            var child1 = $("#<%= ddlRoom1Child.ClientID %>").val()
            ShowAndHideHotelChildAges('1', child1)
            var child2 = $("#<%= ddlRoom2Child.ClientID %>").val()
            ShowAndHideHotelChildAges('2', child2)

            var child3 = $("#<%= ddlRoom3Child.ClientID %>").val()
            ShowAndHideHotelChildAges('3', child3)
            var child4 = $("#<%= ddlRoom4Child.ClientID %>").val()
            ShowAndHideHotelChildAges('4', child4)
            var child5 = $("#<%= ddlRoom5Child.ClientID %>").val()
            ShowAndHideHotelChildAges('5', child5)
            var child6 = $("#<%= ddlRoom6Child.ClientID %>").val()
            ShowAndHideHotelChildAges('6', child6)

              var child7 = $("#<%= ddlRoom7Child.ClientID %>").val()
            ShowAndHideHotelChildAges('7', child7)
              var child8 = $("#<%= ddlRoom8Child.ClientID %>").val()
            ShowAndHideHotelChildAges('8', child8)
             var child9 = $("#<%= ddlRoom9Child.ClientID %>").val()
            ShowAndHideHotelChildAges('9', child9)

        }

    }
    function SethotelContextkey() {
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

    function AutoCompleteExtender_ShiftHotel_KeyUp() {
        $("#<%= txtShiftHotel.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtShiftHotel.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtShiftHotelCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtShiftHotel.ClientID %>").keyup("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtShiftHotel.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtShiftHotelCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });
    }

    function ShiftFromAutoCompleteSelected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('< %=txtShiftHotelCode.ClientID%>').value = eventArgs.get_value();
            PageMethods.GetShiftingRoomAdultChild(eventArgs.get_value(), ShiftingCallSuccess, ShiftingCallFailed)
        }
        else {
            document.getElementById('< %=txtShiftHotelCode.ClientID%>').value = '';
        }
    }

    function ShiftingCallSuccess(res) {
        var strRoomAndDate = [];
        strRoomAndDate = res.split("**");

        //changed by mohamed on 08/04/2018
        //document.getElementById('<%=txtCheckIn.ClientID%>').value = strRoomAndDate[1];
        if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
            document.getElementById('<%=txtCheckIn.ClientID%>').value = strRoomAndDate[1];
        }
        else {
            document.getElementById('<%=txtCheckOut.ClientID%>').value = strRoomAndDate[1];
            document.getElementById('<%=txtCheckIn.ClientID%>').value = strRoomAndDate[2];
            document.getElementById('<%=txtHotelCode.ClientID%>').value = strRoomAndDate[3];
            document.getElementById('<%=txtHotelName.ClientID%>').value = strRoomAndDate[4];
            document.getElementById('<%=txtNoOfNights.ClientID%>').value = strRoomAndDate[5];
            GetHotelsDetails(document.getElementById('<%=txtHotelCode.ClientID%>').value);
        }

        var strroomstring = [];
        strroomstring = strRoomAndDate[0].split(";");
        var strroom = [];
        var strchildage = [];
        var ddlRoom = document.getElementById('<%=ddlRoom.ClientID%>');
        ddlRoom.value = strroomstring.length
        $('.custom-select-ddlroom').next('span').children('.customSelectInner').text(strroomstring.length);
        if ((strroomstring.length > 0)) {

            var strRoomAdultName = "";
            var strRoomchildName = "";
            var strRoomchildage = "";

            for (i = 0; i <= strroomstring.length - 1; i++) {
                strroom = strroomstring[i].split(",");

                if ((strroom[1] != "0")) {
                    strRoomAdultName = ("ddlRoom" + (strroom[0] + "Adult"));
                    strRoomchildName = ("ddlRoom" + (strroom[0] + "Child"));
                    var ddlRoomm = document.getElementById(strRoomAdultName);
                    var ddlRoomc = document.getElementById(strRoomchildName);

                    ddlRoomm.value = strroom[1];

                    var ddlRoomAdult = '.custom-select-' + strRoomAdultName
                    var ddlRoomChild = '.custom-select-' + strRoomchildName


                    $(ddlRoomAdult).next('span').children('.customSelectInner').text(ddlRoomm.value);

                    ddlRoomc.value = strroom[2];
                    $(ddlRoomChild).next('span').children('.customSelectInner').text(ddlRoomc.value);
                }

                if ((strroom[2].ToString != "0")) {
                    strchildage = strroom[3].split("|");

                    for (j = 0; j <= strchildage.length - 1; j++) {
                        strRoomchildage = ("txtRoom" + strroom[0] + "Child" + [j + 1]);
                        var txtRoomchild = document.getElementById(strRoomchildage);
                        txtRoomchild.value = strchildage[j];
                    }

                }

            }

        }
        ShowAdultChild();
        //ShowPreHotelChildAge();
        CallCheckOutDatePicker();

    }

    // alert message on some failure
    function ShiftingCallFailed(res) {

    }

    function Countryautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtCountryCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtCountryCode.ClientID%>').value = '';
        }
    }



    function Customersautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtCustomerCode.ClientID%>').value = eventArgs.get_value();
            $find('AutoCompleteExtender_txtCountry').set_contextKey(eventArgs.get_value());

            GetCountryDetails(eventArgs.get_value());
        }
        else {
            document.getElementById('<%=txtCustomerCode.ClientID%>').value = '';

        }
    }

    function SupplierAgentautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtSupplierAgentCode.ClientID%>').value = eventArgs.get_value();         
        }
        else {
            document.getElementById('<%=txtSupplierAgentCode.ClientID%>').value = '';

        }
    }
    function RoomTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoomTypeCode.ClientID%>').value = eventArgs.get_value();
          
        }
        else {
            document.getElementById('<%=txtRoomTypeCode.ClientID%>').value = '';

        }
    }
    function Room1AccomodationTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>').value = '';

        }
    }
    function Room1Accomodationautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value = eventArgs.get_value();
          
        }
        else {
            document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value = '';

        }
    }
    function Room2AccomodationTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom2AccomodationTypeCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtRoom2AccomodationTypeCode.ClientID%>').value = '';

        }
    }
    function Room2Accomodationautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>').value = eventArgs.get_value();

        }
        else {
            document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>').value = '';

        }
    }
    function Room3AccomodationTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom3AccomodationTypeCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtRoom3AccomodationTypeCode.ClientID%>').value = '';

        }
    }
    function Room3Accomodationautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom3AccomodationCode.ClientID%>').value = eventArgs.get_value();

        }
        else {
            document.getElementById('<%=txtRoom3AccomodationCode.ClientID%>').value = '';

        }
    }
    function Room4AccomodationTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom4AccomodationTypeCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtRoom4AccomodationTypeCode.ClientID%>').value = '';

        }
    }
    function Room4Accomodationautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom4AccomodationCode.ClientID%>').value = eventArgs.get_value();

        }
        else {
            document.getElementById('<%=txtRoom4AccomodationCode.ClientID%>').value = '';

        }
    }
    function Room5AccomodationTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom5AccomodationTypeCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtRoom5AccomodationTypeCode.ClientID%>').value = '';

        }
    }
    function Room5Accomodationautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom5AccomodationCode.ClientID%>').value = eventArgs.get_value();

        }
        else {
            document.getElementById('<%=txtRoom5AccomodationCode.ClientID%>').value = '';

        }
    }

    function Room6AccomodationTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom6AccomodationTypeCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtRoom6AccomodationTypeCode.ClientID%>').value = '';

        }
    }
    function Room6Accomodationautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom6AccomodationCode.ClientID%>').value = eventArgs.get_value();

        }
        else {
            document.getElementById('<%=txtRoom6AccomodationCode.ClientID%>').value = '';

        }
    }

    
    function Room7AccomodationTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom7AccomodationTypeCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtRoom7AccomodationTypeCode.ClientID%>').value = '';

        }
    }
    function Room7Accomodationautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom7AccomodationCode.ClientID%>').value = eventArgs.get_value();

        }
        else {
            document.getElementById('<%=txtRoom7AccomodationCode.ClientID%>').value = '';

        }
    }
    
    function Room8AccomodationTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom8AccomodationTypeCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtRoom8AccomodationTypeCode.ClientID%>').value = '';

        }
    }
    function Room8Accomodationautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom8AccomodationCode.ClientID%>').value = eventArgs.get_value();

        }
        else {
            document.getElementById('<%=txtRoom8AccomodationCode.ClientID%>').value = '';

        }
    }

    
    function Room9AccomodationTypeautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom9AccomodationTypeCode.ClientID%>').value = eventArgs.get_value();
        }
        else {
            document.getElementById('<%=txtRoom9AccomodationTypeCode.ClientID%>').value = '';

        }
    }
    function Room9Accomodationautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtRoom9AccomodationCode.ClientID%>').value = eventArgs.get_value();

        }
        else {
            document.getElementById('<%=txtRoom9AccomodationCode.ClientID%>').value = '';

        }
    }


    function MealPlanautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {
            document.getElementById('<%=txtMealPlanCode.ClientID%>').value = eventArgs.get_value();
          
        }
        else {
            document.getElementById('<%=txtMealPlanCode.ClientID%>').value = '';

        }
    }

    function HotelNameAutoCompleteSelectedInLoadForShifting(htlCode) {
        document.getElementById('<%=txtHotelCode.ClientID%>').value = htlCode;
//        $find('AutoCompleteExtendertxtRoomType').set_contextKey("'"  + htlCode + "'");
//        $find('AutoCompleteExtenderRoom1AccomodationType').set_contextKey(htlCode);
//        $find('AutoCompleteExtenderRoom2AccomodationType').set_contextKey(htlCode);
//        $find('AutoCompleteExtenderRoom3AccomodationType').set_contextKey(htlCode);
//        $find('AutoCompleteExtenderRoom4AccomodationType').set_contextKey(htlCode);
//        $find('AutoCompleteExtenderRoom5AccomodationType').set_contextKey(htlCode);
//        $find('AutoCompleteExtenderRoom6AccomodationType').set_contextKey(htlCode);
//        $find('AutoCompleteExtendertxtMealPlan').set_contextKey(htlCode);
        GetHotelsDetails(htlCode);
    }


    function HotelNameautocompleteselected(source, eventArgs) {
        if (eventArgs != null) {

            document.getElementById('<%=txtHotelCode.ClientID%>').value = eventArgs.get_value();

            GetHotelsDetails(document.getElementById('<%=txtHotelCode.ClientID%>').value);
            $find('AutoCompleteExtendertxtRoomType').set_contextKey(eventArgs.get_value());
            $find('AutoCompleteExtenderRoom1AccomodationType').set_contextKey(eventArgs.get_value());
            $find('AutoCompleteExtenderRoom2AccomodationType').set_contextKey(eventArgs.get_value());
            $find('AutoCompleteExtenderRoom3AccomodationType').set_contextKey(eventArgs.get_value());
            $find('AutoCompleteExtenderRoom4AccomodationType').set_contextKey(eventArgs.get_value());
            $find('AutoCompleteExtenderRoom5AccomodationType').set_contextKey(eventArgs.get_value());
            $find('AutoCompleteExtenderRoom6AccomodationType').set_contextKey(eventArgs.get_value());
            $find('AutoCompleteExtendertxtMealPlan').set_contextKey(eventArgs.get_value());

            
        }
        else {
            document.getElementById('<%=txtHotelCode.ClientID%>').value = '';
            $find('AutoCompleteExtendertxtRoomType').set_contextKey('');
            $find('AutoCompleteExtenderRoom1AccomodationType').set_contextKey('');
            $find('AutoCompleteExtenderRoom2AccomodationType').set_contextKey('');
            $find('AutoCompleteExtenderRoom3AccomodationType').set_contextKey('');
            $find('AutoCompleteExtenderRoom4AccomodationType').set_contextKey('');
            $find('AutoCompleteExtenderRoom5AccomodationType').set_contextKey('');
            $find('AutoCompleteExtenderRoom6AccomodationType').set_contextKey('');
            $find('AutoCompleteExtendertxtMealPlan').set_contextKey('');
        }
    }



 
    function DestinationNameautocompleteselected(source, eventArgs) {

        if (eventArgs != null) {


            document.getElementById('<%=txtDestinationcode.ClientID%>').value = eventArgs.get_value();

        }
        else {
            document.getElementById('<%=txtDestinationcode.ClientID%>').value = '';
        }
        SethotelContextkey();
    }



    function AutoCompleteExtender_Country_KeyUp() {
        $("#<%= txtCountry.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtCountry.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtCountryCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtCountry.ClientID %>").keyup("change", function () {
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

        $("#<%= txtCustomer.ClientID %>").keyup("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtCustomer.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtCustomerCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });
    }


    function AutoCompleteExtender_SupplierAgent_KeyUp() {
        $("#<%= txtSupplierAgent.ClientID %>").bind("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtSupplierAgent.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtSupplierAgentCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtSupplierAgent.ClientID %>").keyup("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtSupplierAgent.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtSupplierAgentCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });
    }

    function AutoCompleteExtender_RoomType_KeyUp() {
        $("#<%= txtRoomType.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoomType.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoomTypeCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoomType.ClientID %>").keyup("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoomType.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoomTypeCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });
    }

    function AutoCompleteExtendertxtRoomType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    } 
    function AutoCompleteExtenderRoom1AccomodationType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }
    function AutoCompleteExtenderRoom2AccomodationType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }
    function AutoCompleteExtenderRoom3AccomodationType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }
    function AutoCompleteExtenderRoom4AccomodationType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }
    function AutoCompleteExtenderRoom5AccomodationType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }
    function AutoCompleteExtenderRoom6AccomodationType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }
        function AutoCompleteExtenderRoom7AccomodationType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }
        function AutoCompleteExtenderRoom8AccomodationType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }
        function AutoCompleteExtenderRoom9AccomodationType_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }

    function AutoCompleteExtendertxtMealPlan_OnClientPopulating(sender, args) { //changed by mohamed on 04/08/2018
        sender.set_contextKey(document.getElementById('<%=txtHotelCode.ClientID%>').value);
    }

    function AutoCompleteExtender_AccomodationType_KeyUp() {
        $("#<%= txtRoom1AccomodationType.ClientID %>").bind("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtRoom1AccomodationType.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom1AccomodationType.ClientID %>").keyup("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom1AccomodationType.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });
    }

    function AutoCompleteExtender_Accomodation_KeyUp() {
        $("#<%= txtRoom1Accomodation.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom1Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom1Accomodation.ClientID %>").keyup("change", function () {
        
            var hiddenfieldID1 = document.getElementById('<%=txtRoom1Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom1Accomodation.ClientID %>").keydown("change", function () {
            SetAccomodationContextKey('1');

        });

        $("#<%= txtRoom2Accomodation.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom2Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom2Accomodation.ClientID %>").keyup("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtRoom2Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom2Accomodation.ClientID %>").keydown("change", function () {
            SetAccomodationContextKey('2');

        });
        $("#<%= txtRoom3Accomodation.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom3Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom3AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom3Accomodation.ClientID %>").keyup("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtRoom3Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom3AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom3Accomodation.ClientID %>").keydown("change", function () {
            SetAccomodationContextKey('3');

        });

        $("#<%= txtRoom4Accomodation.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom4Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom4AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom4Accomodation.ClientID %>").keyup("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtRoom4Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom4AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom4Accomodation.ClientID %>").keydown("change", function () {
            SetAccomodationContextKey('4');

        });
        $("#<%= txtRoom5Accomodation.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom5Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom5AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom5Accomodation.ClientID %>").keyup("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtRoom5Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom5AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom5Accomodation.ClientID %>").keydown("change", function () {
            SetAccomodationContextKey('5');

        });
        $("#<%= txtRoom6Accomodation.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom6Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom6AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom6Accomodation.ClientID %>").keyup("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtRoom6Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom6AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom6Accomodation.ClientID %>").keydown("change", function () {
          
            SetAccomodationContextKey('6');

        });



          $("#<%= txtRoom7Accomodation.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom7Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom7AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom7Accomodation.ClientID %>").keyup("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtRoom7Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom7AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom7Accomodation.ClientID %>").keydown("change", function () {
          
            SetAccomodationContextKey('7');

        });


          $("#<%= txtRoom8Accomodation.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom8Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom8AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom8Accomodation.ClientID %>").keyup("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtRoom8Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom8AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom8Accomodation.ClientID %>").keydown("change", function () {
          
            SetAccomodationContextKey('8');

        });


          $("#<%= txtRoom9Accomodation.ClientID %>").bind("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtRoom9Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom9AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom9Accomodation.ClientID %>").keyup("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtRoom9Accomodation.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtRoom9AccomodationCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtRoom9Accomodation.ClientID %>").keydown("change", function () {
          
            SetAccomodationContextKey('9');

        });


    }
    function AutoCompleteExtender_MealPlan_KeyUp() {
        $("#<%= txtMealPlan.ClientID %>").bind("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtMealPlan.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtMealPlanCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });

        $("#<%= txtMealPlan.ClientID %>").keyup("change", function () {
            var hiddenfieldID1 = document.getElementById('<%=txtMealPlan.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtMealPlanCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
        });
    }
    function AutoCompleteExtenderKeyUp() {
        $("#<%= txtDestinationName.ClientID %>").bind("change", function () {
         
            var hiddenfieldID1 = document.getElementById('<%=txtDestinationName.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtDestinationcode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
           
            SethotelContextkey();
        });

        $("#<%= txtDestinationName.ClientID %>").keyup(function (event) {

            var hiddenfieldID1 = document.getElementById('<%=txtDestinationName.ClientID%>');

            var hiddenfieldID = document.getElementById('<%=txtDestinationcode.ClientID%>');

            if (hiddenfieldID1.value == '') {

                hiddenfieldID.value = '';
            }
            SethotelContextkey();
        });
    }
    function AutoCompleteExtender_HotelNameKeyUp() {
      
        $("#<%= txtHotelName.ClientID %>").bind("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtHotelName.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtHotelCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
            SethotelContextkey();
            $find('AutoCompleteExtendertxtRoomType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom1AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom2AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom3AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom4AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom5AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom6AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtendertxtMealPlan').set_contextKey(hiddenfieldID.value);
        });
        $("#<%= txtHotelName.ClientID %>").keyup("change", function () {

            var hiddenfieldID1 = document.getElementById('<%=txtHotelName.ClientID%>');
            var hiddenfieldID = document.getElementById('<%=txtHotelCode.ClientID%>');
            if (hiddenfieldID1.value == '') {
                hiddenfieldID.value = '';
            }
            SethotelContextkey();
            $find('AutoCompleteExtendertxtRoomType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom1AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom2AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom3AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom4AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom5AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtenderRoom6AccomodationType').set_contextKey(hiddenfieldID.value);
            $find('AutoCompleteExtendertxtMealPlan').set_contextKey(hiddenfieldID.value);
        });
    }


        function float_exponent(number) {
            exponent = 1;
            while (number < 1.0) {
                exponent += 1
                number *= 10
            }
            return exponent;
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

        //changed by mohamed on 11/04/2018
        function fnLockControlsForShifting() {
            //Hotel
            //if shift is ticked
            if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                document.getElementById("<%= txtDestinationName.ClientID %>").removeAttribute("readonly");
                document.getElementById("<%= txtHotelName.ClientID %>").removeAttribute("readonly");
                //document.getElementById("< %= ddlPropertType.ClientID %>").removeAttribute("readonly");
                //document.getElementById("< %= txtHotelStars.ClientID %>").removeAttribute("readonly");
                document.getElementById("<%= txtCheckIn.ClientID %>").setAttribute("readonly", true);
                document.getElementById("<%= txtCheckOut.ClientID %>").removeAttribute("readonly");

            }
            else {
                if (document.getElementById("<%= hdHotelAvailableForShifting.ClientID %>").value != "1" || document.getElementById("<%= txtShiftHotelCode.ClientID %>").value=="") {
                    //if shifting is unticked, and there is no hotel booked so no need to shift 
                    //or if shifting is unticked, and no hotel is selected
                    document.getElementById("<%= txtDestinationName.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= txtHotelName.ClientID %>").removeAttribute("readonly");
                    //document.getElementById("< %= ddlPropertType.ClientID %>").removeAttribute("readonly");
                    //document.getElementById("< %= txtHotelStars.ClientID %>").removeAttribute("readonly");
                    //document.getElementById("< %= txtCheckIn.ClientID %>").removeAttribute("readonly");
                    //document.getElementById("< %= txtCheckOut.ClientID %>").removeAttribute("readonly");             
                }
                else {
                    //if shifting is unticked, and there is a hotel / pre hotel booked
                    //changed by mohamed on 29/08/2018
                    document.getElementById("<%= txtDestinationName.ClientID %>").setAttribute("readonly", true);
                    document.getElementById("<%= txtHotelName.ClientID %>").setAttribute("readonly", true);
                    //document.getElementById("< %= ddlPropertType.ClientID %>").setAttribute("readonly", true);
                    //document.getElementById("< %= txtHotelStars.ClientID %>").setAttribute("readonly", true);
                    document.getElementById("<%= txtCheckIn.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= txtCheckOut.ClientID %>").removeAttribute("readonly");
                }
            }
        }


        function ValidateSearch() {


            ShowProgess();
            if (document.getElementById('<%=txtDestinationcode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any destination.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtShiftHotelCode.ClientID%>').value == '') {
                if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                    showDialog('Alert Message', 'Please select shifting hotel.', 'warning');
                    HideProgess(); //check here later
                    return false;
                }
                //else {
                //    if (document.getElementById("<%= hdHotelAvailableForShifting.ClientID %>").value == "1") {
                //        if (document.getElementById("<%= hdOPMode.ClientID %>").value != 'Edit') {
                //            showDialog('Alert Message', 'Please select hotel in shifting hotel even shifting is unticked', 'warning');
                //            HideProgess(); //check here later
                //            return false;
                //        }
                //    }
                //}


            }

            if (document.getElementById('<%=txtCheckIn.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any check-in date.', 'warning');
                HideProgess();
                return false;
            }
            if (document.getElementById('<%=txtCheckOut.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any check-out date.', 'warning');
                HideProgess();
                return false;
            }

            var vMaxNoOfNight = document.getElementById('<%=hdMaxNoOfNight.ClientID%>').value
            var vNoOfNights = document.getElementById('<%=txtNoOfNights.ClientID%>').value
            if (parseInt(vNoOfNights) == 0) {
                showDialog('Alert Message', 'No of Night should be greater than 0.', 'warning');
                HideProgess();
                return false;
            }

            if (parseInt(vNoOfNights) > parseInt(vMaxNoOfNight)) {
                showDialog('Alert Message', 'Maximum number of night allowed only ' + vMaxNoOfNight + '.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=ddlRoom.ClientID%>').value == '0') {
                showDialog('Alert Message', 'Please select any number of rooms.', 'warning');
                HideProgess();
                return false;
            }


            if (document.getElementById('<%=txtCountryCode.ClientID%>').value == '0') {
                showDialog('Alert Message', 'Please select any source country.', 'warning');
                HideProgess();
                return false;
            }
            if (document.getElementById('<%=txtHotelCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any hotel.', 'warning');
                HideProgess();
                return false;
            }

            // *** Hotel child age change based on room on 25/05/2017 -- Start


            var ddlRoom = document.getElementById('<%=ddlRoom.ClientID%>').value;
            if (ddlRoom != '0') {
                var ddlRoom1Adult = document.getElementById('<%=ddlRoom1Adult.ClientID%>').value;
                var ddlRoom2Adult = document.getElementById('<%=ddlRoom2Adult.ClientID%>').value;
                var ddlRoom3Adult = document.getElementById('<%=ddlRoom3Adult.ClientID%>').value;
                var ddlRoom4Adult = document.getElementById('<%=ddlRoom4Adult.ClientID%>').value;
                var ddlRoom5Adult = document.getElementById('<%=ddlRoom5Adult.ClientID%>').value;
                var ddlRoom6Adult = document.getElementById('<%=ddlRoom6Adult.ClientID%>').value;

                var ddlRoom1Child = document.getElementById('<%=ddlRoom1Child.ClientID%>').value;
                var ddlRoom2Child = document.getElementById('<%=ddlRoom2Child.ClientID%>').value;
                var ddlRoom3Child = document.getElementById('<%=ddlRoom3Child.ClientID%>').value;
                var ddlRoom4Child = document.getElementById('<%=ddlRoom4Child.ClientID%>').value;
                var ddlRoom5Child = document.getElementById('<%=ddlRoom5Child.ClientID%>').value;
                var ddlRoom6Child = document.getElementById('<%=ddlRoom6Child.ClientID%>').value;

                if (ddlRoom == 1) {

                    if (ddlRoom1Adult == 0) {
                        showDialog('Alert Message', 'Please select Room1 Adult.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom1Child, '1') == false) {
                        return false;
                    }
               
                    if (document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                }
                else if (ddlRoom == 2) {
                    if (ddlRoom1Adult == 0 || ddlRoom2Adult == 0) {
                        if (ddlRoom1Adult == 0) {
                            showDialog('Alert Message', 'Please select Room1 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom2Adult == 0) {
                            showDialog('Alert Message', 'Please select Room2 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                    if (ValidateRoomChildAges(ddlRoom1Child, '1') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom2Child, '2') == false) {
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                }
                else if (ddlRoom == 3) {
                    if (ddlRoom1Adult == 0 || ddlRoom2Adult == 0 || ddlRoom3Adult == 0) {

                        if (ddlRoom1Adult == 0) {
                            showDialog('Alert Message', 'Please select Room1 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom2Adult == 0) {
                            showDialog('Alert Message', 'Please select Room2 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom3Adult == 0) {
                            showDialog('Alert Message', 'Please select Room3 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                    if (ValidateRoomChildAges(ddlRoom1Child, '1') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom2Child, '2') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom3Child, '3') == false) {
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom3AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room3 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom3AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room3 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                }
                else if (ddlRoom == 4) {
                    if (ddlRoom1Adult == 0 || ddlRoom2Adult == 0 || ddlRoom3Adult == 0 || ddlRoom4Adult == 0) {
                        if (ddlRoom1Adult == 0) {
                            showDialog('Alert Message', 'Please select Room1 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom2Adult == 0) {
                            showDialog('Alert Message', 'Please select Room2 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom3Adult == 0) {
                            showDialog('Alert Message', 'Please select Room3 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom4Adult == 0) {
                            showDialog('Alert Message', 'Please select Room4 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }

                    }
                    if (ValidateRoomChildAges(ddlRoom1Child, '1') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom2Child, '2') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom3Child, '3') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom4Child, '4') == false) {
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom3AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room3 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom3AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room3 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom4AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room4 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom4AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room4 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                }
                else if (ddlRoom == 5) {
                    if (ddlRoom1Adult == 0 || ddlRoom2Adult == 0 || ddlRoom3Adult == 0 || ddlRoom4Adult == 0 || ddlRoom5Adult == 0) {
                        if (ddlRoom1Adult == 0) {
                            showDialog('Alert Message', 'Please select Room1 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom2Adult == 0) {
                            showDialog('Alert Message', 'Please select Room2 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom3Adult == 0) {
                            showDialog('Alert Message', 'Please select Room3 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom4Adult == 0) {
                            showDialog('Alert Message', 'Please select Room4 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom5Adult == 0) {
                            showDialog('Alert Message', 'Please select Room5 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                    if (ValidateRoomChildAges(ddlRoom1Child, '1') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom2Child, '2') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom3Child, '3') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom4Child, '4') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom5Child, '5') == false) {
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom3AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room3 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom3AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room3 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom4AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room4 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom4AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room4 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom5AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room5 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom5AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room5 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                }
                else if (ddlRoom == 6) {
                    if (ddlRoom1Adult == 0 || ddlRoom2Adult == 0 || ddlRoom3Adult == 0 || ddlRoom4Adult == 0 || ddlRoom5Adult == 0 || ddlRoom6Adult == 0) {
                        if (ddlRoom1Adult == 0) {
                            showDialog('Alert Message', 'Please select Room1 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom2Adult == 0) {
                            showDialog('Alert Message', 'Please select Room2 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom3Adult == 0) {
                            showDialog('Alert Message', 'Please select Room3 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom4Adult == 0) {
                            showDialog('Alert Message', 'Please select Room4 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom5Adult == 0) {
                            showDialog('Alert Message', 'Please select Room5 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (ddlRoom6Adult == 0) {
                            showDialog('Alert Message', 'Please select Room6 Adult.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }

                    if (ValidateRoomChildAges(ddlRoom1Child, '1') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom2Child, '2') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom3Child, '3') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom4Child, '4') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom5Child, '5') == false) {
                        return false;
                    }
                    if (ValidateRoomChildAges(ddlRoom6Child, '6') == false) {
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room1 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom2AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room2 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom3AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room3 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom3AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room3 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom4AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room4 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom4AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room4 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom5AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room5 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom5AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room5 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom6AccomodationTypeCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room6 accomodation type.', 'warning');
                        HideProgess();
                        return false;
                    }
                    if (document.getElementById('<%=txtRoom6AccomodationCode.ClientID%>').value == '') {
                        showDialog('Alert Message', 'Please select any Room6 accomodation.', 'warning');
                        HideProgess();
                        return false;
                    }
                }

            }



            // *** ---------------------------- End

            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtCustomerCode.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please Select agent.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtCountryCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any source country.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtSupplierAgentCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any supplier agent.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtContract.ClientID%>').value.trim() == '') { //ID:119 modified by abin on 20180925
                showDialog('Alert Message', 'Please enter any contract or promotion.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtRoomTypeCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any room type.', 'warning');
                HideProgess();
                return false;
            }
           
//            if (document.getElementById('<%=txtRoom1AccomodationCode.ClientID%>').value == '') {
//                showDialog('Alert Message', 'Please select any accomodation.', 'warning');
//                HideProgess();
//                return false;
//            }

            if (document.getElementById('<%=txtMealPlanCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any meal plan.', 'warning');
                HideProgess();
                return false;
            }
            //if (document.getElementById('<%=chkNonrefundable.ClientID%>').checked == false) {
                //if (document.getElementById('<%=txtCancelFreeUpTo.ClientID%>').value == '0' || document.getElementById('<%=txtCancelFreeUpTo.ClientID%>').value == '') {
                //    showDialog('Alert Message', 'Please enter days for cancel.', 'warning');
                //    HideProgess();
                //    return false;
                //}
            //}

            return true;

        }

        function SetAccomodationContextKey(room) {
    
            if (room == '1') {
                var ddlRoom1Adult = document.getElementById('<%=ddlRoom1Adult.ClientID%>').value;
                var ddlRoom1Child = document.getElementById('<%=ddlRoom1Child.ClientID%>').value;
                      if (ddlRoom1Adult != '0') {
                    if (ddlRoom1Child != '0') {
                        AssignAccomodationContextKey(room, ddlRoom1Adult, ddlRoom1Child);
                    }
                    else {
                        var contx = room + ',' + ddlRoom1Adult + ',0,0';
                        $find('AutoCompleteExtendertxtRoom1Accomodation').set_contextKey(contx);
                    }
                }


            }
            else if (room == '2') {
                var ddlRoomAdult = document.getElementById('<%=ddlRoom2Adult.ClientID%>').value;
                var ddlRoomChild = document.getElementById('<%=ddlRoom2Child.ClientID%>').value;
                if (ddlRoomAdult != '0') {
                    if (ddlRoomChild != '0') {
                        AssignAccomodationContextKey(room, ddlRoomAdult, ddlRoomChild);
                    }
                    else {
                        $find('AutoCompleteExtendertxtRoom2Accomodation').set_contextKey(room + ',' + ddlRoomAdult + ',0,0');
                    }
                }
            }
            else if (room == '3') {
                var ddlRoomAdult = document.getElementById('<%=ddlRoom3Adult.ClientID%>').value;
                var ddlRoomChild = document.getElementById('<%=ddlRoom3Child.ClientID%>').value;
                if (ddlRoomAdult != '0') {
                    if (ddlRoomChild != '0') {
                        AssignAccomodationContextKey(room, ddlRoomAdult, ddlRoomChild);
                    }
                    else {
                        $find('AutoCompleteExtendertxtRoom3Accomodation').set_contextKey(room + ',' + ddlRoomAdult + ',0,0');
                    }
                }
            }
            else if (room == '4') {
                var ddlRoomAdult = document.getElementById('<%=ddlRoom4Adult.ClientID%>').value;
                var ddlRoomChild = document.getElementById('<%=ddlRoom4Child.ClientID%>').value;
                if (ddlRoomAdult != '0') {
                    if (ddlRoomChild != '0') {
                        AssignAccomodationContextKey(room, ddlRoomAdult, ddlRoomChild);
                    }
                    else {
                        $find('AutoCompleteExtendertxtRoom4Accomodation').set_contextKey(room + ',' + ddlRoomAdult + ',0,0');
                    }
                }
            }
            else if (room == '5') {

                var ddlRoomAdult = document.getElementById('<%=ddlRoom5Adult.ClientID%>').value;
                var ddlRoomChild = document.getElementById('<%=ddlRoom5Child.ClientID%>').value;
                if (ddlRoomAdult != '0') {
                    if (ddlRoomChild != '0') {
                        AssignAccomodationContextKey(room, ddlRoomAdult, ddlRoomChild);
                    }
                    else {
                        $find('AutoCompleteExtendertxtRoom5Accomodation').set_contextKey(room + ',' + ddlRoomAdult + ',0,0');
                    }
                }
            }
            else if (room == '6') {
           
                var ddlRoomAdult = document.getElementById('<%=ddlRoom6Adult.ClientID%>').value;
                var ddlRoomChild = document.getElementById('<%=ddlRoom6Child.ClientID%>').value;
                if (ddlRoomAdult != '0') {
                    if (ddlRoomChild != '0') {
                        AssignAccomodationContextKey(room, ddlRoomAdult, ddlRoomChild);
                    }
                    else {
                        $find('AutoCompleteExtendertxtRoom6Accomodation').set_contextKey(room + ',' + ddlRoomAdult + ',0,0');
                    }
                }
            }
             else if (room == '7') {
           
                var ddlRoomAdult = document.getElementById('<%=ddlRoom7Adult.ClientID%>').value;
                var ddlRoomChild = document.getElementById('<%=ddlRoom7Child.ClientID%>').value;
                if (ddlRoomAdult != '0') {
                    if (ddlRoomChild != '0') {
                        AssignAccomodationContextKey(room, ddlRoomAdult, ddlRoomChild);
                    }
                    else {
                        $find('AutoCompleteExtendertxtRoom7Accomodation').set_contextKey(room + ',' + ddlRoomAdult + ',0,0');
                    }
                }
            }
             else if (room == '8') {
           
                var ddlRoomAdult = document.getElementById('<%=ddlRoom8Adult.ClientID%>').value;
                var ddlRoomChild = document.getElementById('<%=ddlRoom8Child.ClientID%>').value;
                if (ddlRoomAdult != '0') {
                    if (ddlRoomChild != '0') {
                        AssignAccomodationContextKey(room, ddlRoomAdult, ddlRoomChild);
                    }
                    else {
                        $find('AutoCompleteExtendertxtRoom8Accomodation').set_contextKey(room + ',' + ddlRoomAdult + ',0,0');
                    }
                }
            } else if (room == '9') {
           
                var ddlRoomAdult = document.getElementById('<%=ddlRoom9Adult.ClientID%>').value;
                var ddlRoomChild = document.getElementById('<%=ddlRoom9Child.ClientID%>').value;
                if (ddlRoomAdult != '0') {
                    if (ddlRoomChild != '0') {
                        AssignAccomodationContextKey(room, ddlRoomAdult, ddlRoomChild);
                    }
                    else {
                        $find('AutoCompleteExtendertxtRoom9Accomodation').set_contextKey(room + ',' + ddlRoomAdult + ',0,0');
                    }
                }
            }
            else {
                $find('AutoCompleteExtendertxtRoom6Accomodation').set_contextKey('');
            }
        }

        function AssignAccomodationContextKey(room,Adult,vChildNo) {

            var vRoom = 'Room' + room;
            // var vChild = 'Child' + ChildNo;

            var vChild1 = ('txtRoom1Child1').replace('Room1', vRoom)
            var vChild2 = ('txtRoom1Child2').replace('Room1', vRoom)
            var vChild3 = ('txtRoom1Child3').replace('Room1', vRoom)
            var vChild4 = ('txtRoom1Child4').replace('Room1', vRoom)
            var vChild5 = ('txtRoom1Child5').replace('Room1', vRoom)
            var vChild6 = ('txtRoom1Child6').replace('Room1', vRoom)

            var roomchild1 = document.getElementById(vChild1).value;
            var roomchild2 = document.getElementById(vChild2).value;
            var roomchild3 = document.getElementById(vChild3).value;
            var roomchild4 = document.getElementById(vChild4).value;
            var roomchild5 = document.getElementById(vChild5).value;
            var roomchild6 = document.getElementById(vChild6).value;
          
            if (vChildNo == '1') {
                if (roomchild1 == '') {
                    var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
                    $find(AutoExt).set_contextKey('');  
                    showDialog('Alert Message', 'Please select Room'+ room +' Adult.', 'warning');
                    return false;
                }
                contxt = room + ',' + Adult + ',' + vChildNo + ',' + roomchild1;
            }
            else if (vChildNo == '2') {
                if (roomchild1 == '' || roomchild2 == '') {
                    var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
                    $find(AutoExt).set_contextKey('');
                    showDialog('Alert Message', 'Please select Room' + room + ' Adult.', 'warning');
                    return false;
                }
                contxt = room + ',' + Adult + ',' + vChildNo + ',' + roomchild1 + '|' + roomchild2;
            }
            else if (vChildNo == '3') {
                if (roomchild1 == '' || roomchild2 == '' || roomchild3 == '') {
                    var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
                    $find(AutoExt).set_contextKey('');
                    showDialog('Alert Message', 'Please select Room' + room + ' Adult.', 'warning');
                    return false;
                }
                contxt = room + ',' + Adult + ',' + vChildNo + ',' + roomchild1 + '|' + roomchild2 + '|' + roomchild3;
            }
            else if (vChildNo == '4') {
                if (roomchild1 == '' || roomchild2 == '' || roomchild3 == '' || roomchild4 == '') {
                    var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
                    $find(AutoExt).set_contextKey('');
                    showDialog('Alert Message', 'Please select Room' + room + ' Adult.', 'warning');
                    return false;
                }
                contxt = room + ',' + Adult + ',' + vChildNo + ',' + roomchild1 + '|' + roomchild2 + '|' + roomchild3 + '|' + roomchild4;
            }
            else if (vChildNo == '5') {
                if (roomchild1 == '' || roomchild2 == '' || roomchild3 == '' || roomchild4 == '' || roomchild5 == '') {
                    var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
                    $find(AutoExt).set_contextKey('');
                    showDialog('Alert Message', 'Please select Room' + room + ' Adult.', 'warning');
                    return false;
                }
                contxt = room + ',' + Adult + ',' + vChildNo + ',' + roomchild1 + '|' + roomchild2 + '|' + roomchild3 + '|' + roomchild4 + '|' + roomchild5;
            }
            else if (vChildNo == '6') {
                if (roomchild1 == '' || roomchild2 == '' || roomchild3 == '' || roomchild4 == '' || roomchild5 == '' || roomchild6 == '') {
                    var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
                    $find(AutoExt).set_contextKey('');
                    showDialog('Alert Message', 'Please select Room' + room + ' Adult.', 'warning');
                    return false;
                }
                contxt = room + ',' + Adult + ',' + vChildNo + ',' + roomchild1 + '|' + roomchild2 + '|' + roomchild3 + '|' + roomchild4 + '|' + roomchild5 + '|' + roomchild6;
            }
             else if (vChildNo == '7') {
                if (roomchild1 == '' || roomchild2 == '' || roomchild3 == '' || roomchild4 == '' || roomchild5 == '' || roomchild6 == '' || roomchild7 == '') {
                    var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
                    $find(AutoExt).set_contextKey('');
                    showDialog('Alert Message', 'Please select Room' + room + ' Adult.', 'warning');
                    return false;
                }
                contxt = room + ',' + Adult + ',' + vChildNo + ',' + roomchild1 + '|' + roomchild2 + '|' + roomchild3 + '|' + roomchild4 + '|' + roomchild5 + '|' + roomchild6 + '|' + roomchild7;
            }
             else if (vChildNo == '8') {
                if (roomchild1 == '' || roomchild2 == '' || roomchild3 == '' || roomchild4 == '' || roomchild5 == '' || roomchild6 == '' || roomchild7 == ''  || roomchild7 == ''   || roomchild8 == '') {
                    var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
                    $find(AutoExt).set_contextKey('');
                    showDialog('Alert Message', 'Please select Room' + room + ' Adult.', 'warning');
                    return false;
                }
                contxt = room + ',' + Adult + ',' + vChildNo + ',' + roomchild1 + '|' + roomchild2 + '|' + roomchild3 + '|' + roomchild4 + '|' + roomchild5 + '|' + roomchild6 + '|' + roomchild7  + '|' + roomchild8;
            }
             else if (vChildNo == '9') {
                if (roomchild1 == '' || roomchild2 == '' || roomchild3 == '' || roomchild4 == '' || roomchild5 == '' || roomchild6 == '' || roomchild7 == ''  || roomchild7 == ''   || roomchild8 == ''  || roomchild9 == '') {
                    var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
                    $find(AutoExt).set_contextKey('');
                    showDialog('Alert Message', 'Please select Room' + room + ' Adult.', 'warning');
                    return false;
                }
                contxt = room + ',' + Adult + ',' + vChildNo + ',' + roomchild1 + '|' + roomchild2 + '|' + roomchild3 + '|' + roomchild4 + '|' + roomchild5 + '|' + roomchild6 + '|' + roomchild7  + '|' + roomchild8   + '|' + roomchild9;
            }

            else {
                contxt = room + ',' + Adult + ',0,0';
            }
               
            var AutoExt = ('AutoCompleteExtendertxtRoom1Accomodation').replace('Room1', vRoom);
            $find(AutoExt).set_contextKey(contxt);
        }
        // *** Hotel child age change based on room on 25/05/2017 -- Start
        function ShowAndHideHotelChildAges(room, child) {
            var dvRoomChildsString = '#dvRoom' + room + 'Childs'
            var dvRoomChild1String = '#dvRoom' + room + 'Child1'
            var dvRoomChild2String = '#dvRoom' + room + 'Child2'
            var dvRoomChild3String = '#dvRoom' + room + 'Child3'
            var dvRoomChild4String = '#dvRoom' + room + 'Child4'
            var dvRoomChild5String = '#dvRoom' + room + 'Child5'
            var dvRoomChild6String = '#dvRoom' + room + 'Child6'
//            var dvRoomChild6String = '#dvRoom' + room + 'Child6'
//            var dvRoomChild6String = '#dvRoom' + room + 'Child6'
//            var dvRoomChild6String = '#dvRoom' + room + 'Child6'


            if (child == 0) {
                $(dvRoomChildsString).hide();
            }
            else if (child == 1) {

                $(dvRoomChildsString).show();
                $(dvRoomChild1String).show();
                $(dvRoomChild2String).hide();
                $(dvRoomChild3String).hide();
                $(dvRoomChild4String).hide();
                $(dvRoomChild5String).hide();
                $(dvRoomChild6String).hide();
            }
            else if (child == 2) {

                $(dvRoomChildsString).show();
                $(dvRoomChild1String).show();
                $(dvRoomChild2String).show();
                $(dvRoomChild3String).hide();
                $(dvRoomChild4String).hide();
                $(dvRoomChild5String).hide();
                $(dvRoomChild6String).hide();
            }
            else if (child == 3) {

                $(dvRoomChildsString).show();
                $(dvRoomChild1String).show();
                $(dvRoomChild2String).show();
                $(dvRoomChild3String).show();
                $(dvRoomChild4String).hide();
                $(dvRoomChild5String).hide();
                $(dvRoomChild6String).hide();
            }
            else if (child == 4) {

                $(dvRoomChildsString).show();
                $(dvRoomChild1String).show();
                $(dvRoomChild2String).show();
                $(dvRoomChild3String).show();
                $(dvRoomChild4String).show();
                $(dvRoomChild5String).hide();
                $(dvRoomChild6String).hide();
            }
            else if (child == 5) {

                $(dvRoomChildsString).show();
                $(dvRoomChild1String).show();
                $(dvRoomChild2String).show();
                $(dvRoomChild3String).show();
                $(dvRoomChild4String).show();
                $(dvRoomChild5String).show();
                $(dvRoomChild6String).hide();
            }
            else if (child == 6) {

                $(dvRoomChildsString).show();
                $(dvRoomChild1String).show();
                $(dvRoomChild2String).show();
                $(dvRoomChild3String).show();
                $(dvRoomChild4String).show();
                $(dvRoomChild5String).show();
                $(dvRoomChild6String).show();
            }
        }


        function ShowAndHideHotelAdultAndChild(room) {
            if (room == 0) {
                $('#dvFullAdultChild').hide();
            }
            else if (room == 1) {


                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').hide();
                $('#dvRoom3AdultChild').hide();
                $('#dvRoom4AdultChild').hide();
                $('#dvRoom5AdultChild').hide();
                $('#dvRoom6AdultChild').hide();

                    $('#dvRoom7AdultChild').hide();
                        $('#dvRoom8AdultChild').hide();
                            $('#dvRoom9AdultChild').hide();
                $('#dvFullAdultChild').show();
            }
            else if (room == 2) {


                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').hide();
                $('#dvRoom4AdultChild').hide();
                $('#dvRoom5AdultChild').hide();
                $('#dvRoom6AdultChild').hide();

                                    $('#dvRoom7AdultChild').hide();
                        $('#dvRoom8AdultChild').hide();
                            $('#dvRoom9AdultChild').hide();
                $('#dvFullAdultChild').show();

            }
            else if (room == 3) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').hide();
                $('#dvRoom5AdultChild').hide();
                $('#dvRoom6AdultChild').hide();

                                                    $('#dvRoom7AdultChild').hide();
                        $('#dvRoom8AdultChild').hide();
                            $('#dvRoom9AdultChild').hide();

                $('#dvFullAdultChild').show();
            }
            else if (room == 4) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').show();
                $('#dvRoom5AdultChild').hide();
                $('#dvRoom6AdultChild').hide();

                                                    $('#dvRoom7AdultChild').hide();
                        $('#dvRoom8AdultChild').hide();
                            $('#dvRoom9AdultChild').hide();

                $('#dvFullAdultChild').show();
            }
            else if (room == 5) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').show();
                $('#dvRoom5AdultChild').show();
                $('#dvRoom6AdultChild').hide();

                        $('#dvRoom7AdultChild').hide();
                        $('#dvRoom8AdultChild').hide();
                        $('#dvRoom9AdultChild').hide();
                $('#dvFullAdultChild').show();
            }
            else if (room == 6) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').show();
                $('#dvRoom5AdultChild').show();
                $('#dvRoom6AdultChild').show();
                        $('#dvRoom7AdultChild').hide();
                        $('#dvRoom8AdultChild').hide();
                        $('#dvRoom9AdultChild').hide();
                $('#dvFullAdultChild').show();
            }
             else if (room == 7) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').show();
                $('#dvRoom5AdultChild').show();
                $('#dvRoom6AdultChild').show();
                        $('#dvRoom7AdultChild').show();
                        $('#dvRoom8AdultChild').hide();
                        $('#dvRoom9AdultChild').hide();
                $('#dvFullAdultChild').show();
            }
              else if (room == 8) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').show();
                $('#dvRoom5AdultChild').show();
                $('#dvRoom6AdultChild').show();
                        $('#dvRoom7AdultChild').show();
                        $('#dvRoom8AdultChild').show();
                        $('#dvRoom9AdultChild').hide();
                $('#dvFullAdultChild').show();
            }

              else if (room == 9) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').show();
                $('#dvRoom5AdultChild').show();
                $('#dvRoom6AdultChild').show();
                        $('#dvRoom7AdultChild').show();
                        $('#dvRoom8AdultChild').show();
                        $('#dvRoom9AdultChild').show();
                $('#dvFullAdultChild').show();
            }
             
        }

      
        function ClearRoomAdultChild() {

            var ddlRoom1Adult = document.getElementById('<%=ddlRoom1Adult.ClientID%>');
            ddlRoom1Adult.selectedIndex = "0";
            $('.custom-select-ddlRoom1Adult').next('span').children('.customSelectInner').text(jQuery("#ddlRoom1Adult :selected").text());
            var ddlRoom1Child = document.getElementById('<%=ddlRoom1Child.ClientID%>');
            ddlRoom1Child.selectedIndex = "0";
            $('.custom-select-ddlRoom1Child').next('span').children('.customSelectInner').text(jQuery("#ddlRoom1Child :selected").text());
            ClearRoom1ChildAges();

            var ddlRoom2Adult = document.getElementById('<%=ddlRoom2Adult.ClientID%>');
            ddlRoom2Adult.selectedIndex = "0";
            $('.custom-select-ddlRoom2Adult').next('span').children('.customSelectInner').text(jQuery("#ddlRoom2Adult :selected").text());
            var ddlRoom2Child = document.getElementById('<%=ddlRoom2Child.ClientID%>');
            ddlRoom2Child.selectedIndex = "0";
            $('.custom-select-ddlRoom2Child').next('span').children('.customSelectInner').text(jQuery("#ddlRoom2Child :selected").text());
            ClearRoom2ChildAges();

            var ddlRoom3Adult = document.getElementById('<%=ddlRoom3Adult.ClientID%>');
            ddlRoom3Adult.selectedIndex = "0";
            $('.custom-select-ddlRoom3Adult').next('span').children('.customSelectInner').text(jQuery("#ddlRoom3Adult :selected").text());
            var ddlRoom3Child = document.getElementById('<%=ddlRoom3Child.ClientID%>');
            ddlRoom3Child.selectedIndex = "0";
            $('.custom-select-ddlRoom3Child').next('span').children('.customSelectInner').text(jQuery("#ddlRoom3Child :selected").text());
            ClearRoom3ChildAges();

            var ddlRoom4Adult = document.getElementById('<%=ddlRoom4Adult.ClientID%>');
            ddlRoom4Adult.selectedIndex = "0";
            $('.custom-select-ddlRoom4Adult').next('span').children('.customSelectInner').text(jQuery("#ddlRoom4Adult :selected").text());
            var ddlRoom4Child = document.getElementById('<%=ddlRoom4Child.ClientID%>');
            ddlRoom4Child.selectedIndex = "0";
            $('.custom-select-ddlRoom4Child').next('span').children('.customSelectInner').text(jQuery("#ddlRoom4Child :selected").text());
            ClearRoom4ChildAges();

            var ddlRoom5Adult = document.getElementById('<%=ddlRoom5Adult.ClientID%>');
            ddlRoom5Adult.selectedIndex = "0";
            $('.custom-select-ddlRoom5Adult').next('span').children('.customSelectInner').text(jQuery("#ddlRoom5Adult :selected").text());
            var ddlRoom5Child = document.getElementById('<%=ddlRoom5Child.ClientID%>');
            ddlRoom5Child.selectedIndex = "0";
            $('.custom-select-ddlRoom5Child').next('span').children('.customSelectInner').text(jQuery("#ddlRoom5Child :selected").text());
            ClearRoom5ChildAges();

            var ddlRoom6Adult = document.getElementById('<%=ddlRoom6Adult.ClientID%>');
            ddlRoom6Adult.selectedIndex = "0";
            $('.custom-select-ddlRoom6Adult').next('span').children('.customSelectInner').text(jQuery("#ddlRoom6Adult :selected").text());
            var ddlRoom6Child = document.getElementById('<%=ddlRoom6Child.ClientID%>');
            ddlRoom6Child.selectedIndex = "0";
            $('.custom-select-ddlRoom6Child').next('span').children('.customSelectInner').text(jQuery("#ddlRoom6Child :selected").text());
            ClearRoom6ChildAges();

              var ddlRoom7Adult = document.getElementById('<%=ddlRoom7Adult.ClientID%>');
            ddlRoom7Adult.selectedIndex = "0";
            $('.custom-select-ddlRoom7Adult').next('span').children('.customSelectInner').text(jQuery("#ddlRoom7Adult :selected").text());
            var ddlRoom7Child = document.getElementById('<%=ddlRoom7Child.ClientID%>');
            ddlRoom7Child.selectedIndex = "0";
            $('.custom-select-ddlRoom7Child').next('span').children('.customSelectInner').text(jQuery("#ddlRoom7Child :selected").text());
            ClearRoom7ChildAges();


              var ddlRoom8Adult = document.getElementById('<%=ddlRoom8Adult.ClientID%>');
            ddlRoom8Adult.selectedIndex = "0";
            $('.custom-select-ddlRoom8Adult').next('span').children('.customSelectInner').text(jQuery("#ddlRoom8Adult :selected").text());
            var ddlRoom8Child = document.getElementById('<%=ddlRoom8Child.ClientID%>');
            ddlRoom8Child.selectedIndex = "0";
            $('.custom-select-ddlRoom8Child').next('span').children('.customSelectInner').text(jQuery("#ddlRoom8Child :selected").text());
            ClearRoom8ChildAges();


              var ddlRoom9Adult = document.getElementById('<%=ddlRoom9Adult.ClientID%>');
            ddlRoom9Adult.selectedIndex = "0";
            $('.custom-select-ddlRoom9Adult').next('span').children('.customSelectInner').text(jQuery("#ddlRoom9Adult :selected").text());
            var ddlRoom9Child = document.getElementById('<%=ddlRoom9Child.ClientID%>');
            ddlRoom9Child.selectedIndex = "0";
            $('.custom-select-ddlRoom9Child').next('span').children('.customSelectInner').text(jQuery("#ddlRoom9Child :selected").text());
            ClearRoom9ChildAges();

        }


        function ClearRoom1ChildAges() {

            document.getElementById('<%=txtRoom1Child1.ClientID %>').value = '';
            document.getElementById('<%=txtRoom1Child2.ClientID %>').value = '';
            document.getElementById('<%=txtRoom1Child3.ClientID %>').value = '';
            document.getElementById('<%=txtRoom1Child4.ClientID %>').value = '';
            document.getElementById('<%=txtRoom1Child5.ClientID %>').value = '';
            document.getElementById('<%=txtRoom1Child6.ClientID %>').value = '';

        }

        function ClearRoom2ChildAges() {
            document.getElementById('<%=txtRoom2Child1.ClientID %>').value = '';
            document.getElementById('<%=txtRoom2Child2.ClientID %>').value = '';
            document.getElementById('<%=txtRoom2Child3.ClientID %>').value = '';
            document.getElementById('<%=txtRoom2Child4.ClientID %>').value = '';
            document.getElementById('<%=txtRoom2Child5.ClientID %>').value = '';
            document.getElementById('<%=txtRoom2Child6.ClientID %>').value = '';
        }

        function ClearRoom3ChildAges() {
            document.getElementById('<%=txtRoom3Child1.ClientID %>').value = '';
            document.getElementById('<%=txtRoom3Child2.ClientID %>').value = '';
            document.getElementById('<%=txtRoom3Child3.ClientID %>').value = '';
            document.getElementById('<%=txtRoom3Child4.ClientID %>').value = '';
            document.getElementById('<%=txtRoom3Child5.ClientID %>').value = '';
            document.getElementById('<%=txtRoom3Child6.ClientID %>').value = '';
        }
        function ClearRoom4ChildAges() {
            document.getElementById('<%=txtRoom4Child1.ClientID %>').value = '';
            document.getElementById('<%=txtRoom4Child2.ClientID %>').value = '';
            document.getElementById('<%=txtRoom4Child3.ClientID %>').value = '';
            document.getElementById('<%=txtRoom4Child4.ClientID %>').value = '';
            document.getElementById('<%=txtRoom4Child5.ClientID %>').value = '';
            document.getElementById('<%=txtRoom4Child6.ClientID %>').value = '';
        }

        function ClearRoom5ChildAges() {
            document.getElementById('<%=txtRoom5Child1.ClientID %>').value = '';
            document.getElementById('<%=txtRoom5Child2.ClientID %>').value = '';
            document.getElementById('<%=txtRoom5Child3.ClientID %>').value = '';
            document.getElementById('<%=txtRoom5Child4.ClientID %>').value = '';
            document.getElementById('<%=txtRoom5Child5.ClientID %>').value = '';
            document.getElementById('<%=txtRoom5Child6.ClientID %>').value = '';
        }

        function ClearRoom6ChildAges() {
            document.getElementById('<%=txtRoom6Child1.ClientID %>').value = '';
            document.getElementById('<%=txtRoom6Child2.ClientID %>').value = '';
            document.getElementById('<%=txtRoom6Child3.ClientID %>').value = '';
            document.getElementById('<%=txtRoom6Child4.ClientID %>').value = '';
            document.getElementById('<%=txtRoom6Child5.ClientID %>').value = '';
            document.getElementById('<%=txtRoom6Child6.ClientID %>').value = '';
        }

         function ClearRoom7ChildAges() {
            document.getElementById('<%=txtRoom7Child1.ClientID %>').value = '';
            document.getElementById('<%=txtRoom7Child2.ClientID %>').value = '';
            document.getElementById('<%=txtRoom7Child3.ClientID %>').value = '';
            document.getElementById('<%=txtRoom7Child4.ClientID %>').value = '';
            document.getElementById('<%=txtRoom7Child5.ClientID %>').value = '';
            document.getElementById('<%=txtRoom7Child6.ClientID %>').value = '';
        }

 function ClearRoom8ChildAges() {
            document.getElementById('<%=txtRoom8Child1.ClientID %>').value = '';
            document.getElementById('<%=txtRoom8Child2.ClientID %>').value = '';
            document.getElementById('<%=txtRoom8Child3.ClientID %>').value = '';
            document.getElementById('<%=txtRoom8Child4.ClientID %>').value = '';
            document.getElementById('<%=txtRoom8Child5.ClientID %>').value = '';
            document.getElementById('<%=txtRoom8Child6.ClientID %>').value = '';
        }

 function ClearRoom69ChildAges() {
            document.getElementById('<%=txtRoom9Child1.ClientID %>').value = '';
            document.getElementById('<%=txtRoom9Child2.ClientID %>').value = '';
            document.getElementById('<%=txtRoom9Child3.ClientID %>').value = '';
            document.getElementById('<%=txtRoom9Child4.ClientID %>').value = '';
            document.getElementById('<%=txtRoom9Child5.ClientID %>').value = '';
            document.getElementById('<%=txtRoom9Child6.ClientID %>').value = '';
        }


        function ValidateRoomChildAges(ChildNo, Room) {


            if (ChildNo != '0') {
                var vRoom = 'Room' + Room;
                var vChild = 'Child' + ChildNo;

                var vChild1 = ('txtRoom1Child1').replace('Room1', vRoom)
                var vChild2 = ('txtRoom1Child2').replace('Room1', vRoom)
                var vChild3 = ('txtRoom1Child3').replace('Room1', vRoom)
                var vChild4 = ('txtRoom1Child4').replace('Room1', vRoom)
                var vChild5 = ('txtRoom1Child5').replace('Room1', vRoom)
                var vChild6 = ('txtRoom1Child6').replace('Room1', vRoom)

                var roomchild1 = document.getElementById(vChild1).value;
                var roomchild2 = document.getElementById(vChild2).value;
                var roomchild3 = document.getElementById(vChild3).value;
                var roomchild4 = document.getElementById(vChild4).value;
                var roomchild5 = document.getElementById(vChild5).value;
                var roomchild6 = document.getElementById(vChild6).value;

                if (ChildNo == 1) {

                    if (roomchild1 == 0) {
                        showDialog('Alert Message', 'Please select ' + vRoom + ' Child1 Age.', 'warning');
                        HideProgess();
                        return false;
                    }

                }
                else if (ChildNo == 2) {
                    if (roomchild1 == 0 || roomchild2 == 0) {
                        if (roomchild1 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child1 Age.', 'warning');

                            HideProgess();
                            return false;
                        }
                        if (roomchild1 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child2  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                }
                else if (ChildNo == 3) {
                    if (roomchild1 == 0 || roomchild2 == 0 || roomchild3 == 0) {

                        if (roomchild1 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child1  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild2 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child2  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild3 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child3  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                }
                else if (ChildNo == 4) {
                    if (roomchild1 == 0 || roomchild2 == 0 || roomchild3 == 0 || roomchild4 == 0) {
                        if (roomchild1 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child1  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild2 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child2  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild3 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child3  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild4 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child4  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                }
                else if (ChildNo == 5) {
                    if (roomchild1 == 0 || roomchild2 == 0 || roomchild3 == 0 || roomchild4 == 0 || roomchild5 == 0) {
                        if (roomchild1 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child1  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild2 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child2 Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild3 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child3  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild4 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child4  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild5 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child5  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                }
                else if (ChildNo == 6) {
                    if (roomchild1 == 0 || roomchild2 == 0 || roomchild3 == 0 || roomchild4 == 0 || roomchild5 == 0 || roomchild6 == 0) {
                        if (roomchild1 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child1  Age. ', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild2 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child2  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild3 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child3  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild4 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child4  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild5 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child5  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild6 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child6  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                }

            }
        }
        /// **** ----------------------------------End


        function validateAge(txt) {
            var inputValueNew = txt.value
            var childAgeLimit = document.getElementById('<%=hdChildAgeLimit.ClientID %>').value
            if (inputValueNew > parseInt(childAgeLimit)) {
                txt.value = ''
                txt.focus();
            }
            if (inputValueNew == '0') {
                txt.value = ''
                txt.focus();
            }
        }


  



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

        };

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

   


    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {


            //            $('#txtDestinationName').bind("mousedown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //            });
            //            $('#txtDestinationName').bind("keydown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //            });
            //            $('#txtCheckIn').bind("mousedown", function () {
            //               
            //                //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                //                    event.preventDefault();
            //                //                }

            //            });
            //            $('#txtCheckIn').bind("keydown", function () {
            //          

            //            });

            //            $('#txtDestinationName').bind("keydown", function () {
            //                
            //            });
            //            $('#txtCheckOut').focus(function () {
            //               
            //            });



            //            $('#txtCustomer').bind("mousedown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //            });

            //            $('#txtCustomer').bind("keydown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //            });

            //            $('#txtCountry').bind("mousedown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //            });

            //            $('#txtCountry').bind("keydown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //            });


            //   
            //            $('#txtHotelName').bind("mousedown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //            });





            //            if (document.getElementById('<%=hdHotelTab.ClientID%>').value == '1' || document.getElementById('<%=hdHotelTab.ClientID%>').value == '') {

            //                $("#dvHotels").removeClass("myhotel-tab-inactive");
            //                $("#dvHotels").addClass("myhotel-tab-active");
            //                $("#dvHotelsContent").css('display', 'block');
            //                $("#dvSearchContent").css('display', 'block');
            //                document.getElementById('<%=hdHotelTab.ClientID%>').value = '1';
            //            }
            //            else {

            //                $("#dvHotels").removeClass("myhotel-tab-active");
            //                $("#dvHotels").addClass("myhotel-tab-inactive");
            //                $("#dvHotelsContent").css('display', 'none');
            //                $("#dvSearchContent").css('display', 'none');
            //                document.getElementById('<%=hdHotelTab.ClientID%>').value = '0';
            //            }

            if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                document.getElementById("<%= lblshiftfrom.ClientID %>").value = "SHIFT FROM HOTEL";
                if (document.getElementById("<%= hdOPMode.ClientID %>").value != 'Edit') {
                    $('#dvShiftingSub').show();
                }
                else {
                    $('#dvShiftingSub').show();
                    //document.getElementById('< %=txtShiftHotel.ClientID%>').setAttribute("readonly", true); //changed by mohamed on 11/04/2018
                    document.getElementById('<%=btnSelectShiftHotel.ClientID%>').setAttribute("disabled", true);
                    //$find('AutoCompleteExtendertxtShiftHotel').setAttribute("Enabled", false); //changed by mohamed on 09/04/2018
                }
            }
            else {
                document.getElementById("<%= lblshiftfrom.ClientID %>").value = "SELECT SIMILAR HOTEL";
                //changed by mohamed on 08/04/2018
                //$('#dvShiftingSub').hide();
                if (document.getElementById("<%= hdOPMode.ClientID %>").value != 'Edit') {
                    $('#dvShiftingSub').show();
                }
                else {
                    $('#dvShiftingSub').hide();
                    //document.getElementById('< %=txtShiftHotel.ClientID%>').setAttribute("readonly", true); //changed by mohamed on 11/04/2018
                    document.getElementById('<%=btnSelectShiftHotel.ClientID%>').setAttribute("disabled", true);
                }
            }

            //changed by mohamed on 11/04/2018
            document.getElementById('<%=txtShiftHotel.ClientID%>').setAttribute("readonly", true);
            //document.getElementById('< %=txtShiftHotelPreArranged.ClientID%>').setAttribute("readonly", true);


            $('#txtCheckIn').bind("mousedown", function () {
                if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                    event.preventDefault();
                }
                //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                //                    event.preventDefault();
                //                }

            });
            $('#txtCheckIn').bind("keydown", function () {
                if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                    event.preventDefault();
                }

            });
            $('#txtDestinationName').bind("keydown", function () {
                if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                    $(".date-inpt-check-in-freeform").datepicker("destroy");
                    //event.preventDefault();
                }
            });
            $('#txtCheckOut').focus(function () {
                if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                    $(".date-inpt-check-in-freeform").datepicker("destroy");
                    event.preventDefault();

                }
            });

            //            $('#ddlPropertType').bind("keydown", function () {
            //                if (document.getElementById("< %= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //                else { //changed by mohamed on 11/04/2018
            //                    if (document.getElementById("< %= chkShifting.ClientID %>").checked == false &&
            //                        document.getElementById("< %= hdHotelAvailableForShifting.ClientID %>").value == "1") {
            //                        event.preventDefault();
            //                    }
            //                }
            //            });

            //            $('#ddlPropertType').bind("mousedown", function () {
            //                if (document.getElementById("< %= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //                else { //changed by mohamed on 11/04/2018
            //                    if (document.getElementById("< %= chkShifting.ClientID %>").checked == false &&
            //                        document.getElementById("< %= hdHotelAvailableForShifting.ClientID %>").value == "1") {
            //                        event.preventDefault();
            //                    }
            //                }
            //            });

            $('#dvShiftCheck').bind("mousedown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    //                    $('#dvShiftingSub').hide();
                    //                    alert(document.getElementById("<%= hdOPMode.ClientID %>").value);
                    event.preventDefault();
                }
            });


            $("#<%= chkShifting.ClientID %>").bind("change", function () {
                //*****
                // alert(document.getElementById("<%= hdOPMode.ClientID %>").value);
                if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                    $('#dvShiftingSub').show();

                }
                else {
                    //changed by mohamed on 08/04/2018
                    //$('#dvShiftingSub').hide(); 
                    if (document.getElementById("<%= hdOPMode.ClientID %>").value != 'Edit') {

                        $('#dvShiftingSub').show();
                    }
                    else {
                        $('#dvShiftingSub').hide();
                    }
                }
                document.getElementById('<%=txtShiftHotel.ClientID%>').value = "";
                document.getElementById('<%=txtShiftHotelCode.ClientID%>').value = "";
                fnLockControlsForShifting(); //changed by mohamed on 11/04/2018
            });

        });

        function SetContextkey() {
            if (document.getElementById('<%=txtDestinationcode.ClientID%>').value != '') {
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

        }

        function CallCheckOutDatePicker() {
            var d = document.getElementById('<%=txtCheckIn.ClientID%>').value;
            if (d != '') {
                var dp = d.split("/");
//                var dateOut = new Date(dp[2], dp[1], dp[0]);
//                var currentMonth = dateOut.getMonth() - 1;

                var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;

                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-check-out").datepicker("destroy");
                $(".date-inpt-check-out").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });
            }
        }



        function ShowChild() {

            var child = $("#<%= ddlChildren.ClientID %>").val()

            if (child == 0) {
                $('#dvChild').hide();
            }
            else if (child == 1) {

                $('#dvChld1').show();
                $('#dvChld2').hide();
                $('#dvChld3').hide();
                $('#dvChld4').hide();
                $('#dvChld5').hide();
                $('#dvChld6').hide();
                $('#dvChld7').hide();
                $('#dvChld8').hide();
                $('#dvChild').show();

            }
            else if (child == 2) {

                $('#dvChld1').show();
                $('#dvChld2').show();
                $('#dvChld3').hide();
                $('#dvChld4').hide();
                $('#dvChld5').hide();
                $('#dvChld6').hide();
                $('#dvChld7').hide();
                $('#dvChld8').hide();
                $('#dvChild').show();

            }
            else if (child == 3) {
                $('#dvChld1').show();
                $('#dvChld2').show();
                $('#dvChld3').show();
                $('#dvChld4').hide();
                $('#dvChld5').hide();
                $('#dvChld6').hide();
                $('#dvChld7').hide();
                $('#dvChld8').hide();
                $('#dvChild').show();

            }
            else if (child == 4) {

                $('#dvChld1').show();
                $('#dvChld2').show();
                $('#dvChld3').show();
                $('#dvChld4').show();
                $('#dvChld5').hide();
                $('#dvChld6').hide();
                $('#dvChld7').hide();
                $('#dvChld8').hide();
                $('#dvChild').show();

            }
            else if (child == 5) {

                $('#dvChld1').show();
                $('#dvChld2').show();
                $('#dvChld3').show();
                $('#dvChld4').show();
                $('#dvChld5').show();
                $('#dvChld6').hide();
                $('#dvChld7').hide();
                $('#dvChld8').hide();
                $('#dvChild').show();

            }
            else if (child == 6) {

                $('#dvChld1').show();
                $('#dvChld2').show();
                $('#dvChld3').show();
                $('#dvChld4').show();
                $('#dvChld5').show();
                $('#dvChld6').show();
                $('#dvChld7').hide();
                $('#dvChld8').hide();
                $('#dvChild').show();

            }
            else if (child == 7) {

                $('#dvChld1').show();
                $('#dvChld2').show();
                $('#dvChld3').show();
                $('#dvChld4').show();
                $('#dvChld5').show();
                $('#dvChld6').show();
                $('#dvChld7').show();
                $('#dvChld8').hide();
                $('#dvChild').show();

            }
            else if (child == 8) {

                $('#dvChld1').show();
                $('#dvChld2').show();
                $('#dvChld3').show();
                $('#dvChld4').show();
                $('#dvChld5').show();
                $('#dvChld6').show();
                $('#dvChld7').show();
                $('#dvChld8').show();
                $('#dvChild').show();

            }

        }

    </script>

    <script type="text/javascript">
    //<![CDATA[
        //        function pageLoad() { // this gets fired when the UpdatePanel.Update() completes
        //            ReBindMyStuff();
        //            google.maps.event.clearListeners(map);
        //            google.maps.event.clearListeners(window, 'resize');

        //        }

        //        function ReBindMyStuff() { // create the rebinding logic in here
        //        }
</script>
    <script type="text/javascript">


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestUserControl);
        function EndRequestUserControl(sender, args) {

            AutoCompleteExtender_HotelNameKeyUp();
            AutoCompleteExtenderKeyUp();
            AutoCompleteExtender_Customer_KeyUp();
            AutoCompleteExtender_Country_KeyUp();

            AutoCompleteExtender_ShiftHotel_KeyUp();

            AutoCompleteExtender_MealPlan_KeyUp();
            AutoCompleteExtender_Accomodation_KeyUp();
            AutoCompleteExtender_AccomodationType_KeyUp();
            AutoCompleteExtender_RoomType_KeyUp();
            AutoCompleteExtender_SupplierAgent_KeyUp();


            SetContextkey();


            //    ShowProgess();
            var d = document.getElementById('<%=txtCheckIn.ClientID%>').value;
            if (d != '') {
                var dp = d.split("/");
//                var dateOut = new Date(dp[2], dp[1], dp[0]);
//                var currentMonth = dateOut.getMonth() - 1;

                var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;

                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-check-out").datepicker("destroy");
                $(".date-inpt-check-out").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });
            }


        }

        function IncludeScriptAfterThreadUpdate() {

        }

     </script>
    <script type="text/javascript">


        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {

        });

        function RefreshContent() {

            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandlerForProgressBar);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandlerForProgressBar);

        }
        function BeginRequestHandlerForProgressBar(sender, args) {

            var elem = args.get_postBackElement();
            var elmid = elem.id;
        }
        function EndRequestHandlerForProgressBar(sender, args) {


            fnShowSpecialEvents();
            CommonJavaScriptFunctions();
            AutoCompleteExtender_Accomodation_KeyUp()
            var hotelcode = document.getElementById('<%=txtHotelCode.ClientID%>').value;

            if (hotelcode != null) {

                $find('AutoCompleteExtendertxtRoomType').set_contextKey(hotelcode);
                $find('AutoCompleteExtenderRoom1AccomodationType').set_contextKey(hotelcode);
                $find('AutoCompleteExtenderRoom2AccomodationType').set_contextKey(hotelcode);
                $find('AutoCompleteExtenderRoom3AccomodationType').set_contextKey(hotelcode);
                $find('AutoCompleteExtenderRoom4AccomodationType').set_contextKey(hotelcode);
                $find('AutoCompleteExtenderRoom5AccomodationType').set_contextKey(hotelcode);
                $find('AutoCompleteExtenderRoom6AccomodationType').set_contextKey(hotelcode);
                $find('AutoCompleteExtendertxtMealPlan').set_contextKey(hotelcode);

            }
            else {
                $find('AutoCompleteExtendertxtRoomType').set_contextKey('');
                $find('AutoCompleteExtenderRoom1AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom2AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom3AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom4AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom5AccomodationType').set_contextKey('');
                $find('AutoCompleteExtenderRoom6AccomodationType').set_contextKey('');
                $find('AutoCompleteExtendertxtMealPlan').set_contextKey('');
            }





            HideProgess();
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"  EnablePartialRendering="true">
    </asp:ScriptManager>
    <header id="top">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
	<div class="header-a">
		<div class="wrapper-padding">	
         <div class="header-user" style="margin-top:2px;"><asp:Label ID="lblHeaderUserName" runat="server" ></asp:Label> </div>				
			<div class="header-phone" id="dvlblHeaderAgentName" runat="server"  style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
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
			<div id="dvCurrency" runat="server" class="header-curency" style="margin-top:2px;">
			</div>
               <div class="header-lang" id="dvMybooking" runat="server" style="margin-right:5px;">
            <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:Button ID="btnMyBooking"   UseSubmitBehavior="false" 
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
			<div style="float:left;" class="header-logo"><a href="Home.aspx?Tab=0"><img id="imgLogo" runat="server" alt="" /></a></div>
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
      <div class="page-title">Hotels - <span>FREE FORM BOOKING</span></div>
      <div class="breadcrumbs">
        <a href="Home.aspx?Tab=0">Home</a> / <a href="#">Hotel Free Form Booking</a> 
      </div>
      <div class="clear"></div>
    </div>
     <div class="page-head">
      <div class="page-search-content-search">
        
      <div id="dvHotelsContent"  runat="server" >

      <div class="search-tab-content">
       
                 
                                    <div class="page-search-p">
                                        <!-- // -->

                                   <div id="dvShifting" runat="server">
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left" style="width:25%;">
                                                   <label>
                                                    Shifting</label>
                                             <div id="dvShiftCheck" style="padding-left:15px;padding-top:5px;">
                                       
                                              <asp:CheckBox ID="chkShifting"  CssClass="side-block jq-checkbox-tour"  runat="server" />
                                    

                                                    
                                               </div>
                                                </div>
                                                <div class="srch-tab-left"   id="dvShiftingSub" runat="server">
                                                       <div style="width: 280%">
                                                        <label ID="lblshiftfrom" runat="server">Shift From Hotel</label>
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtShiftHotel" runat="server"></asp:TextBox> <%--placeholder="press space to show hotel"--%>
                                                              <asp:TextBox ID="txtShiftHotelCode"   runat="server" Style="display:none"></asp:TextBox>
                                                    <%--<asp:AutoCompleteExtender ID="AutoCompleteExtendertxtShiftHotel" runat="server" 
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetShiftHotelList" TargetControlID="txtShiftHotel"
                                                        OnClientItemSelected="ShiftFromAutoCompleteSelected">
                                                    </asp:AutoCompleteExtender> --%><%--Changed by mohamed on 05/04/2018--%>
                                                            
                                                        </div>
                                                         <asp:Button Text="Select" ID="btnSelectShiftHotel" runat="server" style="margin-top:-25px;margin-left:105%" class="btn-search-text"  />
                                                    </div>
                                                </div>
                                       
                                                <!-- \\ -->
                                            </div>
                                             </div>
                                            
                                          <%--  <div  class="srch-tab-left">
                                                <asp:Button ID="btnfillhotel" TabIndex="102" CssClass="btn-search-text" runat="server"  
                                                Text="Fill"></asp:Button> 
                                        </div>--%>
                                         <div class="clear" style="padding-top:10px;"></div>
                                         
                                    </div>

                 <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server"><ContentTemplate>--%>
         
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    Destination/Location</label>
                                                <div class="input-a">
                                                                                             <asp:TextBox ID="txtDestinationName" runat="server" placeholder="Example: dubai"></asp:TextBox>
                                                    <asp:TextBox ID="txtDestinationCode"   runat="server"   style="display:none;" ></asp:TextBox>
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
                                            <!-- // -->
                                        </div>
                                        <!-- \\ -->
                                        <!-- // -->
                                        <div class="search-large-i" style="float:left;">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <div style="width: 100%">
                                                        <label>
                                                            Check in</label>
                                                        <div class="input-a" style="z-index:0;">
                                                            <asp:TextBox ID="txtCheckIn" class="date-inpt-check-in-freeform" placeholder="dd/mm/yyyy" autocomplete="off"
                                                                runat="server"></asp:TextBox>
                                                            <span class="date-icon"></span>
                                                            <asp:HiddenField ID="hdCheckIn" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <div style="width: 100%">
                                                        <label>
                                                            Check out</label>
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtCheckOut" class="date-inpt-check-out" placeholder="dd/mm/yyyy" autocomplete="off"  onmousedown="fnBindCheckOutDate()" 
                                                                runat="server"></asp:TextBox>
                                                            <%-- <input type="text" value="" class="date-inpt-check-out" placeholder="dd/mm/yyyy" />--%>
                                                            <span class="date-icon"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                       
                                                <!-- \\ -->
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <!-- \\ -->
                                        <!-- // -->
                                       <div class="search-large-i" >
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                             <div class="srch-tab-left" style="padding-left:10px;">
                                            
                                                        <label>
                                                            No of Nights</label>
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtNoOfNights" onkeypress="validateDecimalOnly(event,this)" autocomplete="off"  runat="server"></asp:TextBox>
                                                        </div>
                                                  
                                             </div>

                                              <div class="srch-tab-right">
                                              <label>
                                                        Rooms</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlRoom" class="custom-select custom-select-ddlroom" runat="server">
                                                     
                                                        </asp:DropDownList>
                                 
                                                    </div>
                                              </div>
                                            </div>
                                        </div>
                                        <!-- \\ -->
                                        <div class="clear">
                                        </div>
                                            <div style="margin-top: 15px;">
                                              <div class="search-large-i" id="dvForRO"  style="margin-top:15px;" runat="server">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom"   >
                                                    <label>
                                                        Agent</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtCustomer"  autocomplete="off"  runat="server" placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtCustomerCode" runat="server"   style="display:none;" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCustomer" runat="server" CompletionInterval="10"
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetCustomers" TargetControlID="txtCustomer"
                                                            OnClientItemSelected="Customersautocompleteselected">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                              <div class="search-large-i"  style="margin-top:15px;">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                       Source Country</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtCountry"  autocomplete="off"  runat="server" placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtCountryCode"   style="display:none;" runat="server" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCountry" runat="server" CompletionInterval="10"
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtCountry" 
                                                            UseContextKey = "true" 
                                                            OnClientItemSelected="Countryautocompleteselected">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                              <div class="search-large-i" style="margin-top:15px;float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                          <label>
                                                        Hotels</label>
                                                    <div class="input-a">
                                                     
                                                        <asp:TextBox ID="txtHotelName" runat="server" placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtHotelCode" runat="server"   style="display:none;" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtHotelName"   runat="server" CompletionInterval="10" 
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register" 
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1" 
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True" 
                                                            MinimumPrefixLength="-1" ServiceMethod="GetHotelName" TargetControlID="txtHotelName" UseContextKey = "true" 
                                                          OnClientItemSelected="HotelNameautocompleteselected">
                                                       
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        <div class="clear">
                                        </div>
                                            <div id="dvFullAdultChild" style="display:none;"> 
                                        <div id="dvRoom1AdultChild" runat="server" style="padding-top:15px;">
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Adult For Room 1</label>
                                                        <div class="select-wrapper" id="dvRoom1Adult" runat="server">
                                                            <asp:DropDownList ID="ddlRoom1Adult" class="custom-select custom-select-ddlRoom1Adult"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <label>
                                                            Child For Room 1</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom1Child" class="custom-select custom-select-ddlRoom1Child"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvRoom1Childs">
                                                <div class="search-large-i">
                                                    <div class="srch-tab-line no-margin-bottom">
                                                        <label>
                                                            Child Ages for Room 1</label>
                                                        <div class="srch-tab-left">
                                                            <div class="srch-tab-3c" id="dvRoom1Child1">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom1Child1" placeholder="CH 1" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom1Child2">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom1Child2" placeholder="CH 2" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom1Child3">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom1Child3" placeholder="CH 3" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-right">
                                                            <div class="srch-tab-3c" id="dvRoom1Child4">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom1Child4" placeholder="CH 4" runat="server" autocomplete="off"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom1Child5">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom1Child5" placeholder="CH 5" runat="server"  autocomplete="off" onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom1Child6">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom1Child6" placeholder="CH 6" runat="server"  autocomplete="off" onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                              <div class="search-large-i" style="float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                        <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room1 Accomodation Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom1AccomodationType" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom1AccomodationTypeCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderRoom1AccomodationType" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom1AccomodationType" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodationType" OnClientItemSelected="Room1AccomodationTypeautocompleteselected"
                                                                    OnClientPopulating="AutoCompleteExtenderRoom1AccomodationType_OnClientPopulating">
                                                                    
                                                                   
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room1 Accomodation</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom1Accomodation" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom1AccomodationCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoom1Accomodation" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom1Accomodation" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodation"  OnClientItemSelected="Room1Accomodationautocompleteselected">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div id="dvRoom2AdultChild" runat="server" style="padding-top:15px;">
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Adult For Room 2</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom2Adult" class="custom-select custom-select-ddlRoom2Adult"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <label>
                                                            Child For Room 2</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom2Child" class="custom-select custom-select-ddlRoom2Child"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvRoom2Childs">
                                                <div class="search-large-i">
                                                    <div class="srch-tab-line no-margin-bottom">
                                                        <label>
                                                            Child Ages for Room 2</label>
                                                        <div class="srch-tab-left">
                                                            <div class="srch-tab-3c" id="dvRoom2Child1">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom2Child1" placeholder="CH 1" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom2Child2">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom2Child2" placeholder="CH 2" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom2Child3">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom2Child3" placeholder="CH 3" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-right">
                                                            <div class="srch-tab-3c" id="dvRoom2Child4">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom2Child4" placeholder="CH 4" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom2Child5">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom2Child5" placeholder="CH 5" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom2Child6">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom2Child6" placeholder="CH 6" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="search-large-i" style="float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                        <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room2 Accomodation Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom2AccomodationType" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom2AccomodationTypeCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderRoom2AccomodationType" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom2AccomodationType" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodationType" OnClientItemSelected="Room2AccomodationTypeautocompleteselected"
                                                                    OnClientPopulating="AutoCompleteExtenderRoom2AccomodationType_OnClientPopulating">
                                                                   
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room2 Accomodation</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom2Accomodation" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom2AccomodationCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoom2Accomodation" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom2Accomodation" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodation"  OnClientItemSelected="Room2Accomodationautocompleteselected">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div id="dvRoom3AdultChild" runat="server"  style="padding-top:15px;">
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Adult For Room 3</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom3Adult" class="custom-select custom-select-ddlRoom3Adult"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <label>
                                                            Child For Room 3</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom3Child" class="custom-select custom-select-ddlRoom3Child"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvRoom3Childs">
                                                <div class="search-large-i">
                                                    <div class="srch-tab-line no-margin-bottom">
                                                        <label>
                                                            Child Ages for Room 3</label>
                                                        <div class="srch-tab-left">
                                                            <div class="srch-tab-3c" id="dvRoom3Child1">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom3Child1" placeholder="CH 1" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom3Child2">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom3Child2" placeholder="CH 2" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom3Child3">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom3Child3" placeholder="CH 3" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-right">
                                                            <div class="srch-tab-3c" id="dvRoom3Child4">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom3Child4" placeholder="CH 4" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom3Child5">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom3Child5" placeholder="CH 5" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom3Child6">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom3Child6" placeholder="CH 6" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="search-large-i" style="float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                        <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room3 Accomodation Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom3AccomodationType" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom3AccomodationTypeCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderRoom3AccomodationType" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom3AccomodationType" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodationType" OnClientItemSelected="Room3AccomodationTypeautocompleteselected"
                                                                    OnClientPopulating="AutoCompleteExtenderRoom3AccomodationType_OnClientPopulating">
                                                                   
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room3 Accomodation</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom3Accomodation" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom3AccomodationCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoom3Accomodation" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom3Accomodation" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodation"  OnClientItemSelected="Room3Accomodationautocompleteselected">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div id="dvRoom4AdultChild" runat="server"  style="padding-top:15px;">
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Adult For Room 4</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom4Adult" class="custom-select custom-select-ddlRoom4Adult"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <label>
                                                            Child For Room 4</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom4Child" class="custom-select custom-select-ddlRoom4Child"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvRoom4Childs">
                                                <div class="search-large-i">
                                                    <div class="srch-tab-line no-margin-bottom">
                                                        <label>
                                                            Child Ages for Room 4</label>
                                                        <div class="srch-tab-left">
                                                            <div class="srch-tab-3c" id="dvRoom4Child1">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom4Child1" placeholder="CH 1" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom4Child2">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom4Child2" placeholder="CH 2" runat="server" autocomplete="off"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom4Child3">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom4Child3" placeholder="CH 3" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-right">
                                                            <div class="srch-tab-3c" id="dvRoom4Child4">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom4Child4" placeholder="CH 4" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom4Child5">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom4Child5" placeholder="CH 5" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom4Child6">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom4Child6" placeholder="CH 6" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="search-large-i" style="float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                        <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room4 Accomodation Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom4AccomodationType" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom4AccomodationTypeCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderRoom4AccomodationType" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom4AccomodationType" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodationType" OnClientItemSelected="Room4AccomodationTypeautocompleteselected"
                                                                    OnClientPopulating="AutoCompleteExtenderRoom4AccomodationType_OnClientPopulating">
                                                                   
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room4 Accomodation</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom4Accomodation" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom4AccomodationCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoom4Accomodation" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom4Accomodation" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodation"  OnClientItemSelected="Room4Accomodationautocompleteselected">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div id="dvRoom5AdultChild" runat="server"  style="padding-top:15px;">
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Adult For Room 5</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom5Adult" class="custom-select custom-select-ddlRoom5Adult"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <label>
                                                            Child For Room 5</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom5Child" class="custom-select custom-select-ddlRoom5Child"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvRoom5Childs">
                                                <div class="search-large-i">
                                                    <div class="srch-tab-line no-margin-bottom">
                                                        <label>
                                                            Child Ages for Room 5</label>
                                                        <div class="srch-tab-left">
                                                            <div class="srch-tab-3c" id="dvRoom5Child1">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom5Child1" placeholder="CH 1" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom5Child2">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom5Child2" placeholder="CH 2" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom5Child3">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom5Child3" placeholder="CH 3" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-right">
                                                            <div class="srch-tab-3c" id="dvRoom5Child4">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom5Child4" placeholder="CH 4" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom5Child5">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom5Child5" placeholder="CH 5" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom5Child6">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom5Child6" placeholder="CH 6" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="search-large-i" style="float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                        <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room5 Accomodation Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom5AccomodationType" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom5AccomodationTypeCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderRoom5AccomodationType" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom5AccomodationType" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodationType" OnClientItemSelected="Room5AccomodationTypeautocompleteselected"
                                                                    OnClientPopulating="AutoCompleteExtenderRoom5AccomodationType_OnClientPopulating">
                                                                   
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room5 Accomodation</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom5Accomodation" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom5AccomodationCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoom5Accomodation" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom5Accomodation" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodation"  OnClientItemSelected="Room5Accomodationautocompleteselected">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div id="dvRoom6AdultChild" runat="server"  style="padding-top:15px;">
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Adult For Room 6</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom6Adult" class="custom-select custom-select-ddlRoom6Adult"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <label>
                                                            Child For Room 6</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom6Child" class="custom-select custom-select-ddlRoom6Child"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvRoom6Childs">
                                                <div class="search-large-i">
                                                    <div class="srch-tab-line no-margin-bottom">
                                                        <label>
                                                            Child Ages for Room 6</label>
                                                        <div class="srch-tab-left">
                                                            <div class="srch-tab-3c" id="dvRoom6Child1">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom6Child1" placeholder="CH 1" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom6Child2">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom6Child2" placeholder="CH 2" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom6Child3">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom6Child3" placeholder="CH 3" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-right">
                                                            <div class="srch-tab-3c" id="dvRoom6Child4">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom6Child4" placeholder="CH 4" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom6Child5">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom6Child5" placeholder="CH 5" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom6Child6">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom6Child6" placeholder="CH 6" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="search-large-i" style="float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                        <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room6 Accomodation Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom6AccomodationType" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom6AccomodationTypeCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderRoom6AccomodationType" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom6AccomodationType" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodationType" OnClientItemSelected="Room6AccomodationTypeautocompleteselected"
                                                                    OnClientPopulating="AutoCompleteExtenderRoom6AccomodationType_OnClientPopulating">
                                                                   
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                              Room6 Accomodation</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom6Accomodation" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom6AccomodationCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoom6Accomodation" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom6Accomodation" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodation"  OnClientItemSelected="Room6Accomodationautocompleteselected">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class="clear">
                                        </div>


                                           <div id="dvRoom7AdultChild" runat="server"  style="padding-top:15px;">
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Adult For Room 7</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom7Adult" class="custom-select custom-select-ddlRoom7Adult"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <label>
                                                            Child For Room 7</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom7Child" class="custom-select custom-select-ddlRoom7Child"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvRoom7Childs">
                                                <div class="search-large-i">
                                                    <div class="srch-tab-line no-margin-bottom">
                                                        <label>
                                                            Child Ages for Room 7</label>
                                                        <div class="srch-tab-left">
                                                            <div class="srch-tab-3c" id="dvRoom7Child1">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom7Child1" placeholder="CH 1" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom7Child2">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom7Child2" placeholder="CH 2" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom7Child3">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom7Child3" placeholder="CH 3" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-right">
                                                            <div class="srch-tab-3c" id="dvRoom7Child4">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom7Child4" placeholder="CH 4" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom7Child5">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom7Child5" placeholder="CH 5" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom7Child6">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom7Child6" placeholder="CH 6" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="search-large-i" style="float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                        <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room7 Accomodation Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom7AccomodationType" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom7AccomodationTypeCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderRoom7AccomodationType" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom7AccomodationType" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodationType" OnClientItemSelected="Room7AccomodationTypeautocompleteselected"
                                                                    OnClientPopulating="AutoCompleteExtenderRoom7AccomodationType_OnClientPopulating">
                                                                   
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                              Room7 Accomodation</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom7Accomodation" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom7AccomodationCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoom7Accomodation" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom7Accomodation" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodation"  OnClientItemSelected="Room7Accomodationautocompleteselected">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class="clear">
                                        </div>

                                           <div id="dvRoom8AdultChild" runat="server"  style="padding-top:15px;">
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Adult For Room 8</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom8Adult" class="custom-select custom-select-ddlRoom8Adult"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <label>
                                                            Child For Room 8</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom8Child" class="custom-select custom-select-ddlRoom8Child"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvRoom8Childs">
                                                <div class="search-large-i">
                                                    <div class="srch-tab-line no-margin-bottom">
                                                        <label>
                                                            Child Ages for Room 8</label>
                                                        <div class="srch-tab-left">
                                                            <div class="srch-tab-3c" id="dvRoom8Child1">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom8Child1" placeholder="CH 1" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom8Child2">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom8Child2" placeholder="CH 2" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom8Child3">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom8Child3" placeholder="CH 3" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-right">
                                                            <div class="srch-tab-3c" id="dvRoom8Child4">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom8Child4" placeholder="CH 4" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom8Child5">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom8Child5" placeholder="CH 5" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom8Child6">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom8Child6" placeholder="CH 6" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="search-large-i" style="float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                        <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room8 Accomodation Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom8AccomodationType" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom8AccomodationTypeCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderRoom8AccomodationType" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom8AccomodationType" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodationType" OnClientItemSelected="Room8AccomodationTypeautocompleteselected"
                                                                    OnClientPopulating="AutoCompleteExtenderRoom8AccomodationType_OnClientPopulating">
                                                                   
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                              Room8 Accomodation</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom8Accomodation" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom8AccomodationCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoom8Accomodation" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom8Accomodation" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodation"  OnClientItemSelected="Room8Accomodationautocompleteselected">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class="clear">
                                        </div>

                                           <div id="dvRoom9AdultChild" runat="server"  style="padding-top:15px;">
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Adult For Room 9</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom9Adult" class="custom-select custom-select-ddlRoom9Adult"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <label>
                                                            Child For Room 9</label>
                                                        <div class="select-wrapper">
                                                            <asp:DropDownList ID="ddlRoom9Child" class="custom-select custom-select-ddlRoom9Child"
                                                                runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="dvRoom9Childs">
                                                <div class="search-large-i">
                                                    <div class="srch-tab-line no-margin-bottom">
                                                        <label>
                                                            Child Ages for Room 9</label>
                                                        <div class="srch-tab-left">
                                                            <div class="srch-tab-3c" id="dvRoom9Child1">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom9Child1" placeholder="CH 1" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom9Child2">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom9Child2" placeholder="CH 2" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom9Child3">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom9Child3" placeholder="CH 3" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-right">
                                                            <div class="srch-tab-3c" id="dvRoom9Child4">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom9Child4" placeholder="CH 4" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom9Child5">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom9Child5" placeholder="CH 5" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" id="dvRoom9Child6">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtRoom9Child6" placeholder="CH 6" runat="server" autocomplete="off"  onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="search-large-i" style="float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                        <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                               Room9 Accomodation Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom9AccomodationType" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom9AccomodationTypeCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderRoom9AccomodationType" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom9AccomodationType" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodationType" OnClientItemSelected="Room9AccomodationTypeautocompleteselected"
                                                                    OnClientPopulating="AutoCompleteExtenderRoom9AccomodationType_OnClientPopulating">
                                                                   
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                              Room9 Accomodation</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtRoom9Accomodation" runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtRoom9AccomodationCode" runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoom9Accomodation" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" TargetControlID="txtRoom9Accomodation" UseContextKey="true"
                                                                    ServiceMethod="GetAccomodation"  OnClientItemSelected="Room9Accomodationautocompleteselected">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class="clear">
                                        </div>


                                        </div>
                                            <div class="search-large-i" style="display:none;">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-3c">
                                                    
                                                </div>
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        adult</label>
                                                    <div class="select-wrapper">
                                     
                                                        <asp:DropDownList ID="ddlAdult" class="custom-select custom-select-ddlAdult" runat="server">
                                            
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        Child</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlChildren" class="custom-select custom-select-ddlChildren"
                                                            runat="server">
                                
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                         <div class="clear">
                                        </div>
                                            <div id="dvChild" runat="server" style="margin-top: 15px;margin-right:-1px;display:none">
                                             <div class="search-large-i-child" ">
                                                <label style="text-align:right;padding-right:2px;">
                                                    Ages of children at check-out</label>
                                            
                                     <div class="srch-tab-child" id="dvChld1">
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlChild1" class="custom-select custom-select-ddlChild1" runat="server">
                                                            <asp:ListItem Value="0">CHLD1</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                         </asp:DropDownList>
                                                    </div>
                                                </div>
                                             <div class="srch-tab-child" id="dvChld2">
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlChild2" class="custom-select custom-select-ddlChild2" runat="server">
                                                            <asp:ListItem Value="0">CHLD2</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                   <div class="srch-tab-child" id="dvChld3">
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlChild3" class="custom-select custom-select-ddlChild3" runat="server">
                                                            <asp:ListItem Value="0">CHLD3</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                       </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvChld4">
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlChild4" class="custom-select custom-select-ddlChild4" runat="server">
                                                            <asp:ListItem Value="0">CHLD4</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                             
                                               
                                              

                                            </div>                                 
                                             <div class="search-large-i-child"  >
                                                <label style="color: White;">
                                                    Ages of children at check-out</label>
                                                                                         
                                                <div class="srch-tab-child" id="dvChld5" >
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlChild5" class="custom-select custom-select-ddlChild5" runat="server">
                                                            <asp:ListItem Value="0">CHLD5</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                      </asp:DropDownList>
                                                    </div>
                                                </div>
                                                   <div class="srch-tab-child" id="dvChld6" >
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlChild6" class="custom-select custom-select-ddlChild6" runat="server">
                                                            <asp:ListItem Value="0">CHLD6</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                      </asp:DropDownList>
                                                    </div>
                                                </div>
                                                                  <div class="srch-tab-child" id="dvChld7"  >
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlChild7" class="custom-select custom-select-ddlChild7" runat="server">
                                                            <asp:ListItem Value="0">CHLD7</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                       </asp:DropDownList>
                                                    </div>
                                                </div>
                                                   <div class="srch-tab-child" id="dvChld8" >
                                                    <div class="select-wrapper">
                                                         <asp:DropDownList ID="ddlChild8" class="custom-select custom-select-ddlChild8" runat="server">
                                                            <asp:ListItem Value="0">CHLD8</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                     </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                                 
                                                  
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div style="margin-top: 15px;">
                                          

                                             <div class="search-large-i"  style="margin-top: 15px;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                          <label>
                                                        Supplier Agent</label>
                                                    <div class="input-a">
                                                     
                                                        <asp:TextBox ID="txtSupplierAgent" runat="server" autocomplete="off" placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtSupplierAgentCode" runat="server"   style="display:none;" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_SupplierAgent"   runat="server" CompletionInterval="10" 
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register" 
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1" 
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True" 
                                                            MinimumPrefixLength="-1" TargetControlID="txtSupplierAgent" UseContextKey = "true" ServiceMethod="GetSupplierAgent"
                                                    OnClientItemSelected="SupplierAgentautocompleteselected"    > 
                                                       
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="search-large-i"  style="margin-top: 15px;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                          <label>
                                                        Contract/Promotion</label>
                                                    <div class="input-a">
                                                     
                                                        <asp:TextBox ID="txtContract" autocomplete="off" runat="server" placeholder="--"></asp:TextBox>
                                           
                                                       
                                                    </div>
                                                </div>
                                            </div>

                                             <div class="search-large-i"  style="margin-top: 15px;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                          <label>
                                                        Room Type</label>
                                                    <div class="input-a">
                                                     
                                                        <asp:TextBox ID="txtRoomType" runat="server" placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtRoomTypeCode" runat="server"   style="display:none;" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtRoomType"   runat="server" CompletionInterval="10" 
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register" 
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1" 
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True" UseContextKey="True"
                                                            MinimumPrefixLength="-1" TargetControlID="txtRoomType"   ServiceMethod="GetRoomType"  
                                                          OnClientItemSelected="RoomTypeautocompleteselected"  
                                                          OnClientPopulating="AutoCompleteExtendertxtRoomType_OnClientPopulating"> 
                                                       
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                       

                                            <div class="search-large-i"  style="margin-top: 15px;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        Meal Plan</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMealPlan" runat="server" placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtMealPlanCode" runat="server" Style="display: none;"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtMealPlan" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" TargetControlID="txtMealPlan" UseContextKey="true"  ServiceMethod="GetMealPlan"  
                                                            OnClientItemSelected="MealPlanautocompleteselected" 
                                                            OnClientPopulating="AutoCompleteExtendertxtMealPlan_OnClientPopulating">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="search-large-i"  style="margin-top: 15px;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <div style="width: 100%">
                                                            <label>
                                                                Non Refundable</label>
                                                                <div class="srch-tab-line no-margin-bottom" style="padding-top:10px;" >
                                                        <asp:CheckBox ID="chkNonrefundable" CssClass="side-block jq-checkbox-tour" runat="server" />
                                                   
                                                        
                                             </div>
                                                        </div>
                                                    </div>
                                                    <div class="srch-tab-right">
                                                        <div style="width: 100%">
                                                            <label>
                                                                Cancel Free up To</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtCancelFreeUpTo"  autocomplete="off"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2" runat="server" placeholder="DAYS"></asp:TextBox>
                                                              </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="clear">
                                        </div>
                                  
                                    
                                
                        <%--</ContentTemplate></asp:UpdatePanel>--%>
                        </div>
                                  
                                        <div class="search-large-i" style="padding-left:20px;">
                                                <div class="srch-tab-left">
                          <asp:Button ID="btnSearch" class="srch-btn-home"  runat="server"  style="width:140px;"  OnClientClick="return ValidateSearch()"  Text="Generate"></asp:Button> 
                          </div>
                           <div class="srch-tab-left">
                          <input  id="btnReset"  type="button"  class="srch-btn-home"  value="Reset"/>
                        </div>
                        </div>
						
						                <div class="clear"></div>
                     

                     <div class="clear"></div>

                        </div>

                    <%-- Changed by Mohamed 05/04/2018 --%>
                    <asp:ModalPopupExtender ID="mpShiftHotel1" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                CancelControlID="aShiftHotelClose" EnableViewState="true" PopupControlID="pnlShiftHotel"
                                TargetControlID="hdShiftHotelPopup">
                            </asp:ModalPopupExtender>
                            <asp:HiddenField ID="hdShiftHotelPopup" runat="server" />
                            <asp:Panel runat="server" ID="pnlShiftHotel" Style="display: none;">
                                <div class="roomtype-price-breakuppopup">
                                    <div id="Div7">
                                        <div class="roomtype-popup-title">
                                            <div style="float: left; padding-left: 10px; width: 80%;">
                                                <asp:Label ID="lblShiftHotelHeading" Width="100%" runat="server" Text="Shifting Hotel Selection"></asp:Label></div>
                                            <div style="float: right; padding-right: 5px;">
                                                <a id="aShiftHotelClose" href="#" class="roomtype-popup-close"></a>
                                            </div>
                                        </div>
                                        <div class="roomtype-popup-description">
                                            <div id="dvShiftHotelSave" runat="server">
                                                <div style="padding-top: 10px; padding-left: 0px; margin-bottom: 15px;">
                                                    <%--<div style="float: right; padding-right: 35px;">--%>
                                                        <table border="0px" >
                                                        <tr>
                                                        <td style="width:80%;vertical-align:middle">
                                                            <%-- Changed by mohamed on 29/08/2018 --%>
                                                            <label ID="lblShiftingMessageToUser" runat="server" style="font-size:12px;color:red;">Select any row if same dates are being booked for other room types or meal plans </label>
                                                        </td>
                                                        <td style="width:15%;vertical-align:middle;">
                                                            <asp:Button ID="btnShiftHotelSave" CssClass="roomtype-popup-buttons-save" runat="server" Text="Select" />
                                                        </td>
                                                        </tr>
                                                        </table>
                                                    <%--</div>--%>
                                                </div>
                                            </div>

                                            <div style="overflow: auto; min-height: 329px; max-height: 420px; min-width: 300px;
                                                padding-bottom: 10px; min-width: 700px;">
                                                <asp:DataList ID="dlShiftHotelBreak" RepeatColumns="1" RepeatDirection="Horizontal"
                                                    Width="100%" runat="server">
                                                    <ItemTemplate>
                                                        <div style="border: 0px solid #ede7e1; max-width: 700px; min-width: 150px; padding-left: 5px;
                                                            background-color: #F5F5F5; padding: 10px;">
                                                            <div class="row-col-12">
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                <asp:Label ID="lblHotelName" Text='<%# Eval("HotelName") %>' 
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                <asp:Label ID="lblRlineNo" Text='<%# Eval("rlineno") %>' Visible="false"
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                <asp:Label ID="lblPartyCode" Text='<%# Eval("partycode") %>' Visible="false" 
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                <asp:Label ID="lblRoomString" Text='<%# Eval("roomstring") %>' Visible="false" 
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                <asp:Label ID="lblCheckout" Text='<%# Eval("checkout") %>' Visible="false"
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                <asp:Label ID="lblNoOfRooms" Text='<%# Eval("NoOfRooms") %>' Visible="false"
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                <asp:Label ID="lblCheckIn" Text='<%# Eval("checkin") %>' Visible="false"
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                <asp:Label ID="lblnights" Text='<%# Eval("nights") %>' Visible="false"
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                <asp:Label ID="lblpartyname" Text='<%# Eval("partyname") %>' Visible="false"
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                      <asp:Label ID="lblAdults" Text='<%# Eval("adults") %>' Visible="false"
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                      <asp:Label ID="lblchild" Text='<%# Eval("child") %>' Visible="false"
                                                                    CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                    <%-- Changed by Mohamed 05/04/2018 --%>     

                                  <div class="clear" style="padding-top:10px;"></div>
          <footer class="search-footer">
			 <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                    <ContentTemplate>         
                        <div id="dvPriceDetails" runat="server">
                     <div class="clear"></div>
                                       <div id="Div5">
                                    
                                        <div>

  <div class="search-large-i" style="margin-top: 15px;float:left;">
    <asp:Label ID="lblRoomSummaryHeading" Text="PRICE DETAILS" CssClass="room-type-breakup-headings" Font-Underline="true" Font-Size="Larger" runat="server"></asp:Label>
  </div>

                                  
                                            <div class="clear" style="padding-top: 10px;">
                                            </div>
                                        
                                            <div class="search-large-i" style="margin-top: 15px;float:left;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                           <asp:Label ID="Label4" CssClass="price-breakup-popup-label" Text="Room" runat="server"></asp:Label>
                                                              <div class="dropdown" style="padding-top:10px;">
                                                    <asp:DropDownList ID="ddlRoomNos" CssClass="roomtype-popup-dropdown" runat="server">
                                                         </asp:DropDownList>
                                                         </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-left" style="width:30%;">
                                                 <asp:Label ID="Label3" CssClass="price-breakup-popup-label" Text="Price" runat="server"></asp:Label>
                                                    <div class="input-a">
                                                    <asp:TextBox ID="txtsalepriceForAll" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server" AutoComplete="off"></asp:TextBox>
                                                        </div>
                                                </div>
                                            </div>

                                      

                                            <div class="search-large-i" style="margin-top: 15px;float:left;width:60%;">
                                                  <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-3c"  style="width:15%;" >
                                                
                                                        <asp:Label ID="Label5" CssClass="price-breakup-popup-label" Text="Cost Price" runat="server"></asp:Label>
                                                        <div class="input-a">
                                                    <asp:TextBox ID="txtBreakupTotalPriceForAll" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server" AutoComplete="off"></asp:TextBox></div>
                                                    </div>
                                             
                                        
                                                <div class="srch-tab-3c"  >
                                                    <asp:Label ID="LblBookingCodeForAll" CssClass="price-breakup-popup-label" Text="BOOKING CODE" runat="server"></asp:Label>
                                                     <div class="input-a">
                                                    <asp:TextBox ID="txtBookingCodeForAll" CssClass="roomtype-popup-textbox" runat="server"  AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                     
                                                </div>
                                                  <div class="srch-tab-3c" style="float:left;" >
                                                       <asp:Label ID="Label1" CssClass="price-breakup-popup-label" Text="Fill Blank Fields Only" runat="server"></asp:Label>
                                                      <div class="checkbox" style="padding-left:35px;padding-top:10px;">
                                                   
                                                           <asp:CheckBox ID="chkFillBlank" CssClass="price-breakup-popup-label" runat="server" /> 
                                                        </div>
                                                  </div>
                                                     </div>
                                            </div>
                                             <div class="search-large-i" style="margin-top:-30px;float:right;width:15%;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                         <asp:Button ID="btnPriceBreakupFillPrice" CssClass="roomtype-popup-buttons-save" OnClick="btnPriceBreakupFillPrice_Click"
                                                        Style="margin-left: 50px;" runat="server" Text="Fill Price"  />
                                                       
                                                    </div>
                                                    </div>
                                                    </div>

                                
                                            <div class="clear" style="padding-top: 20px;">
                                            </div>
                                            <div style="padding-bottom: 10px;">
                                                <asp:DataList ID="dltotalPriceBreak" RepeatColumns="2" RepeatDirection="Horizontal"
                                                    Width="100%" runat="server">
                                                    <ItemTemplate>
                                                        <div style=" max-width: 500px; min-width: 150px; padding-left: 5px;
                                                            background-color: #FFF; padding: 10px;">
                                                            <div class="row-col-12">
                                                                <asp:Label ID="lblRoomNo" Text='<%# Eval("roomno") %>' Visible="false" runat="server"></asp:Label>
                                                                <div>
                                                                    <asp:Label ID="lblRoomSummary" Text='<%# Eval("roomheading") %>' Visible="false"
                                                                        CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="clear" style="padding-top: 10px;">
                                                            </div>
                                                            <div class="row-col-12">
                                                                <div style="display: block; background-color: #E0DAD5; height: 10px; padding-top: 5px;
                                                                    padding-left: 5px; margin-top: 10px; padding-bottom: 10px;">
                                                                    <asp:Label ID="Label2" Text='<%# " # Room "+ Eval("roomno").ToString()+ ": " +  Eval("roomheading").ToString() %>'
                                                                        CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                                                                </div>
                                                                <asp:GridView ID="gvPricebreakup" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                                  Width="100%" GridLines="Horizontal" OnRowDataBound="gvPricebreakup_RowDataBound">
                                                                    <%--    --%>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="DATE">
                                                                            <ItemTemplate>
                                                                                <div style="padding-top: 5px;">
                                                                                    <asp:Label ID="lblBreakupDate" Text='<%# Eval("pricedate", "{0:dd/MM/yyyy}") %>' CssClass="roomtype-popup-label"
                                                                                        runat="server"></asp:Label><asp:Label ID="lblBreakupDateName" CssClass="roomtype-popup-label-sub"
                                                                                            runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblBreakupDate1" Text='<%# Eval("pricedate1") %>' Visible="false" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblgvRoomNo" Text='<%# Eval("roomno") %>' Visible="false" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblgvRoomCatCode" Text='<%# Eval("rmcatcode") %>' Visible="false"
                                                                                        runat="server"></asp:Label>
                                                                                          <asp:Label ID="lblSalePriceCurrcode" CssClass="roomtype-popup-label" Text='<%# Eval("salecurrcode") %>'  Visible="false"
                                                                                        runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblCostPriceCurrcode" CssClass="roomtype-popup-label" Text='<%# Eval("costcurrcode") %>'  Visible="false"
                                                                                        runat="server"></asp:Label>
                                                                                        <div style="display:none">
                                                                                        <asp:Label ID="lblwlcurrcode" runat="server" Text='<%# Eval("wlcurrcode") %>' ></asp:Label>
                                                                                <asp:Label ID="lblwlconvrate" runat="server" Text='<%# Eval("wlconvrate") %>' ></asp:Label>
                                                                                <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>' ></asp:Label>
                                                                                
                                                                                    <asp:Label ID="lblVATPerc" CssClass="roomtype-popup-label" Text='<%# Eval("VATPerc") %>'  
                                                                                        runat="server"></asp:Label>
                                                                                </div>
                                                                                
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="BOOKING CODE"  >
                                                                            <ItemTemplate>
                                                                                <div style="padding-top: 5px;width:50px">
                                                                                    <asp:textbox ID="lblBookingCode" Text='<%# Eval("bookingcode") %>' ToolTip='<%# Eval("bookingcode") %>'
                                                                                        CssClass="roomtype-popup-textbox" runat="server" style="width:50px"></asp:textbox>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="50px" />
                                                                            <HeaderStyle Width="50px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PRICE">
                                                                        <HeaderTemplate>
                                                                        <asp:Label ID="lblSalePriceCurrcodeHeader" Text='<%# Eval("salecurrcode") %>'  CssClass="roomtype-popup-gv-header"
                                                                                        runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="padding-top: 5px;" id="dvSalePrice">
                                                                                    <asp:Label ID="lblbreakupPrice" CssClass="roomtype-popup-label" Text='<%# Eval("saleprice") %>'
                                                                                        runat="server"></asp:Label>
                                                                                         <asp:Label ID="lblwlbreakupPrice" CssClass="roomtype-popup-label" Text='<%# Eval("wlsaleprice") %>' style="display:none;"
                                                                                        runat="server"></asp:Label>
                                                                                    <asp:TextBox ID="txtsaleprice" CssClass="roomtype-popup-textbox" AutoComplete="off" onkeypress="validateDecimalOnly(event,this)"
                                                                                        Text='<%# Eval("saleprice") %>' runat="server"></asp:TextBox>
                                                                                     <%--    <asp:TextBox ID="txtwlsaleprice" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
                                                                                        Text='<%# Eval("wlsaleprice") %>' runat="server"></asp:TextBox>--%>
                                                                                       
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="COST PRICE">
                                                                        <HeaderTemplate>
                                                                          <asp:Label ID="lblCostPriceCurrcodeHeader" CssClass="roomtype-popup-gv-header" Text='<%# Eval("costcurrcode") %>' 
                                                                                        runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="padding-top: 5px;">
                                                                                    <asp:Label ID="lblBreakupTotalPrice" CssClass="roomtype-popup-label" Text='<%# Eval("totalprice") %>'
                                                                                        runat="server"></asp:Label>
                                                                                    <asp:TextBox ID="txtBreakupTotalPrice" CssClass="roomtype-popup-textbox"  AutoComplete="off" onkeypress="validateDecimalOnly(event,this)"
                                                                                        Text='<%# Eval("totalprice") %>' runat="server"></asp:TextBox> 
                                                                                          <%--<asp:TextBox ID="txtwlBreakupTotalPrice" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
                                                                                        Text='<%# Eval("totalprice") %>' runat="server"></asp:TextBox>--%> 
                                                                                     
                                                                                    <asp:Label ID="lblConversionRate" Text='<%# Eval("saleconvrate") %>' Visible="false"
                                                                                        runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblUSDPrice" CssClass="pricebreakup-usd-price" runat="server"></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                    <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                                </asp:GridView>

                                                                <div style="display: block; background-color: #E0DAD5; height: 10px; padding-top: 5px;
                                                                    padding-left: 5px; margin-top: 10px; padding-bottom: 10px; float: left;width: 100%;"">
                                                                    <div style="float: left; padding-left: 45px; width:40%;">
                                                                        <div style="float: left;">
                                                                         <div style="float: left;">
                                                                             <asp:Label ID="Label8" CssClass="price-breakup-popup-label-bold" Text="Price Total: "
                                                                                    runat="server"></asp:Label></div>
                                                                            <div style="float: left;">
                                                                           
                                                                                <asp:Label ID="lblSaleTotal" CssClass="pricebreakup-usd-price-bold" Text='<%# Eval("saletotal") %>'
                                                                                    runat="server"></asp:Label>
                                                                            </div>
                                                                            <div style="float: left;padding-left:5px;">
                                                                                <asp:Label ID="lblSaleTotalCurCode" CssClass="pricebreakup-usd-price-bold" Text='<%# Eval("saletotal") %>'
                                                                                    runat="server"></asp:Label></div>
                                                                        </div>
                                                                        <div style="float: left;">
                                                                            <div style="float: left;">
                                                                                <asp:Label ID="lblwlSaleTotal" CssClass="pricebreakup-usd-price-bold"
                                                                                    Text='<%# Eval("saletotal") %>' runat="server"></asp:Label>
                                                                            </div>
                                                                            <div style="float: left;">
                                                                                <asp:Label ID="lblwlSaleTotalCurCode"  CssClass="pricebreakup-usd-price-bold"
                                                                                    Text='<%# Eval("saletotal") %>' runat="server"></asp:Label></div>
                                                                        </div>
                                                                    </div>
                                                                    <div style="float: left; padding-left: 15px;width:40%;" runat="server" id="dvcosttotal">
                                                                        <asp:Label ID="Label10" Text=" Cost Total: " CssClass="price-breakup-popup-label-bold"
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblCostTotal" Text='<%# Eval("costtotal") %>' CssClass="pricebreakup-usd-price-bold"
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblCostTotalCurCode" CssClass="pricebreakup-usd-price-bold" runat="server"></asp:Label></div>
                                                                </div>
                                                                <div class="clear"></div>
                                                                <div style="padding-top:10px;">
                                                                <span class="roomtype-popup-label"> &#8226;  RATES ARE INCLUSIVE OF ALL TAXES & VAT</span>
                                                                  <div class="clear"  style="padding-top:10px;"></div>
                                                                 <span class="roomtype-popup-label"> &#8226;  RATES ARE EXCLUDING TOURISM DIRHAM FEE</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:HiddenField ID="hdRMPartyCode" runat="server" />
                                                <asp:HiddenField ID="hdRMRatePlanId" runat="server" />
                                                <asp:HiddenField ID="hdRMRoomTypeCode" runat="server" />
                                                <asp:HiddenField ID="hdRMMealPlanCode" runat="server" />
                                                <asp:HiddenField ID="hdRMcatCode" runat="server" />
                                                <asp:HiddenField ID="hdRMAccCode" runat="server" />
                                                <asp:HiddenField ID="hdRMSharingOrExtraBed" runat="server" />
                                                <asp:HiddenField ID="hdRoomTypegrid" runat="server" />
                                                <asp:HiddenField ID="hdRoomTypegridRowId" runat="server" />
                                                <asp:HiddenField ID="hdRoomTypeCurrCode" runat="server" />
                                                <asp:HiddenField ID="hdgvPricebreakupRowwCount" runat="server" />
                                                <asp:HiddenField ID="hdRoomTypewlCurrCode" runat="server" />
                                            </div>
                                        </div>
                                    </div>  
                                     <div class="clear" style="padding-top:10px;"></div>
                                    <div class="search-large-i" style="margin-top: 30px;width:10%;">
                                        <div class="srch-tab-line no-margin-bottom">
                                             <label>   Complementary</label>
                                        </div>
                                    </div>

                                    <div class="search-large-i" style="margin-top: 15px;float:left; ">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                <label>to Customer</label>
                                                <div class="checkbox" >
                                                        <asp:CheckBox ID="chkComplimentaryToCustomer" CssClass="price-breakup-popup-label"
                                                        runat="server" Text="" /></div>
                                                </div>
                                              
                                            <div class="srch-tab-right">
                                                <label>from Supplier</label>
                                                <div class="checkbox" >
                                                    <asp:CheckBox ID="chkComplimentaryFromSupplier" CssClass="price-breakup-popup-label"
                                                        runat="server" Text="" /></div>
                                            </div>  </div>
                                        </div>

                                            <div class="search-large-i" style="margin-top: 15px;float:left; ">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>Arrival Transfer</label>
                                                <div class="checkbox" >
                                                        <asp:CheckBox ID="chkComplimentaryArrivalTransfer" CssClass="price-breakup-popup-label"
                                                    runat="server" Text="" />
                                                    </div>
                                                </div>
                                              
                                            <div class="srch-tab-right">
                                                
                                                    <label>Departure Transfer</label>
                                                <div class="checkbox" >
                                                    <asp:CheckBox ID="chkComplimentaryDepartureTransfer" CssClass="price-breakup-popup-label"
                                                    Style="padding-left: 25px;" runat="server" Text="" />
                                                    </div>
                                            </div>
                                                </div>
                                        </div>
       
                                     <div class="clear">
                                        </div> 
                                        
                                            <div class="search-large-i" style="margin-top: 15px;float:left; ">
                                         
                                                        <asp:CheckBox ID="chkSpecialEvents"  Font-Bold="true" onChange="fnShowSpecialEvents()"
                                                    runat="server" Text="" />
                                                       <asp:Label ID="Label6" CssClass="price-breakup-popup-label" Text="Special Events" runat="server"></asp:Label>
                                                
                                              </div>
                                                <div class="clear">
                                        </div> 
                                                            <asp:Panel runat="server" ID="pnlSpecialEvents" Style="z-index:9999999;">
                                                            <div style="width:100%;border-top: 1px solid #f8f1eb;padding-bottom:20px;margin-top:20px;"></div>
                       
                            <div id="Div6">
                                <asp:Label ID="Label9" Text="Special Events" CssClass="room-type-breakup-headings" Font-Underline="true" Font-Size="Larger" runat="server"></asp:Label>
  
                                <div>
<%--                                            <div class="search-large-i" style="float:left;padding-top:15px;">
                                        <!-- // -->
                                        <div class="srch-tab-line no-margin-bottom">
                                            <div class="srch-tab-left">
                                                <div style="width: 100%">
                                                    <label>
                                                        Room</label>
                                                    <div class="input-a" style="padding-bottom:1px !important;padding-top:1px !important;">
                                                            <asp:DropDownList ID="ddlSpclRoom" Width="100%" Height="26px" Style="border: 1px solid #fff"
                                                                CssClass="special-event-drop-font" runat="server" AutoPostBack="True">
                                                                <asp:ListItem Text="--Select --" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                          
                                                    </div>
                                                </div>
                                                 
                                            </div>
                                            <div class="srch-tab-right">
                                                    <div style="width: 100%">
                                                    <label>
                                                        Pax Type</label>
                                                    <div class="input-a" style="padding-bottom:1px !important;padding-top:1px !important;">
                                                            <asp:DropDownList ID="ddlPaxType" Width="100%" Height="26px" Style="border: 1px solid #fff"
                                                                CssClass="special-event-drop-font" runat="server">
                                                                <asp:ListItem Text="--Select --" Value="0"></asp:ListItem>
                                                                <asp:ListItem>Adult</asp:ListItem>
                                                                <asp:ListItem>Child</asp:ListItem>
                                                            </asp:DropDownList>
                                                          
                                                    </div>
                                                </div>
                                            </div>
                                       
                                            <!-- \\ -->
                                        </div>
                                        </div>

                                            <div class="search-large-i" style="float:left;padding-top:15px;">
                                        <!-- // -->
                                        <div class="srch-tab-line no-margin-bottom">
                                            <div class="srch-tab-left">
                                                <div style="width: 100%">
                                                    <label>
                                                        Event Date</label>
                                                    <div class="input-a" style="z-index:0;">
                                                        <asp:TextBox ID="txtSpecEventDate" class="spcl-date-inpt-check-in" placeholder="dd/mm/yyyy" autocomplete="off"
                                                            runat="server"></asp:TextBox>
                                                        <span class="date-icon"></span>
                                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="srch-tab-right">
                                                <div style="width: 100%">
                                                    <label>
                                                        Special Events</label>
                                                    <div class="input-a" style="padding-bottom:1px !important;padding-top:1px !important;">
                                                      
                                                            <asp:DropDownList ID="ddlSpclEvents" Width="100%" Height="26px" Style="border: 1px solid #fff"
                                                                CssClass="special-event-drop-font" runat="server">
                                                                <asp:ListItem Text="--Select --" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                         
                                                    </div>
                                                </div>
                                            </div>
                                       
                                            <!-- \\ -->
                                        </div>
                                        </div>

                                            <div class="search-large-i" style="float:left;padding-top:15px;">
                                        <!-- // -->
                                        <div class="srch-tab-line no-margin-bottom">
                                            <div class="srch-tab-left">
                                                <div style="width: 100%">
                                                    <label>
                                                        No Of Adult</label>
                                                    <div class="input-a">
                                                    <asp:TextBox ID="txtSpclNoOfAdult" AutoComplete="off" onkeydown="fnReadOnly(event)" runat="server" ></asp:TextBox>
                                                </div>
                                                 
                                            </div>
                                            </div>
                                            <div class="srch-tab-right">
                                                    <div style="width: 100%">
                                                    <label>
                                                        No of Child</label>
                                                    <div class="input-a">
                                                    <asp:TextBox ID="txtNoOfChild"  AutoComplete="off"  onkeydown="fnReadOnly(event)" runat="server" ></asp:TextBox>
                                                </div>
                                                </div>
                                            </div>
                                       
                                            <!-- \\ -->
                                        </div>
                                        </div>

                                        <div class="clear"></div>

                                            <div class="search-large-i" style="float:left;padding-top:15px;">
                                        <!-- // -->
                                        <div class="srch-tab-line no-margin-bottom">
                                            <div class="srch-tab-left">
                                                <div style="width: 100%">
                                                    <label>
                                                        Pax Rate</label>
                                                    <div class="input-a">
                                                    <asp:TextBox ID="txtPaxRate" runat="server" ></asp:TextBox>
                                                </div>
                                                 
                                            </div>
                                            </div>
                                            <div class="srch-tab-right">
                                                    <div style="width: 100%">
                                                    <label>
                                                        Special Event Value</label>
                                                    <div class="input-a">
                                                    <asp:TextBox ID="txtSpecialEventValue" runat="server" ></asp:TextBox>
                                                </div>
                                                </div>
                                            </div>
                                       
                                            <!-- \\ -->
                                        </div>
                                        </div>

                                                <div class="search-large-i" style="float:left;padding-top:15px;">
                                        <!-- // -->
                                        <div class="srch-tab-line no-margin-bottom">
                                            <div class="srch-tab-left">
                                                <div style="width: 100%">
                                                    <label>
                                                        Pax Cost</label>
                                                    <div class="input-a">
                                                    <asp:TextBox ID="txtPaxCost" runat="server" ></asp:TextBox>
                                                </div>
                                                 
                                            </div>
                                            </div>
                                            <div class="srch-tab-right">
                                                    <div style="width: 100%">
                                                    <label>
                                                        Special Event Cost Value</label>
                                                    <div class="input-a">
                                                    <asp:TextBox ID="txtSpecialEventCostValue" runat="server" ></asp:TextBox>
                                                </div>
                                                </div>
                                            </div>
                                       
                                            <!-- \\ -->
                                        </div>
                                        </div>--%>
                                                  <div class="roomtype-popup-description-special-events" style="min-height: 150px;
                                    max-height: 650px; overflow: auto;">
                                    <div class="roomtype-popup-description-special-eve" style="text-align: justify; min-height: 150px;
                                        max-height: 650px; overflow: auto;">
                                    
                                        <asp:DataList ID="dlSpecialEvents" runat="server" Width="100%">
                                            <ItemTemplate>
                                                <div class="booking-form" style="background-color: White; padding: 10px;">
                                                    <div class="row-col-12">
                                             
                                                        <div class="row-col-3">
                                                        <div  style="padding-bottom:3px;">
                                                        
                                                         <asp:Label ID="lblspecialeventcount" Text='<%# Eval("specialeventcount") %>' style="display:none;" runat="server"></asp:Label>
                                                            <asp:Label ID="Label6" CssClass="roomtype-popup-label" runat="server" Text="DATE: "></asp:Label>
                                                            </div>
                                                              <div class="input-a" style="z-index:0;float:left;">
                                                             
                                                        <asp:TextBox ID="txtSpecEventDate" class="spcl-date-inpt-check-in" onmousedown="fnBindSpecEventDate()" placeholder="dd/mm/yyyy" autocomplete="off"
                                                            runat="server"></asp:TextBox>
                                                        <span onmousedown="fnBindSpecEventDate()"   class="date-icon"></span>
                                                     
                                                    </div>
                                                        </div>
                                                           
                                                       
                                                          
                                                                                                       
                                                                                    <div class="row-col-3" >
                                                                                    <div style="padding-bottom:3px;">
                                                                                      <asp:Label ID="Label7" CssClass="roomtype-popup-label" runat="server" Text="EVENTS: "></asp:Label>
                                                                                    </div>
                                                            <div class="dropdown-special-events" >
                                                                <asp:DropDownList ID="ddlEvents" Width="100%" Height="26px" Style="border: 1px solid #fff"
                                                                    CssClass="special-event-drop-font" runat="server">
                                                                    <asp:ListItem Text="--Select --" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                    

                                                    </div>
                                                    <div class="row-col-4">
                                                        <div class="row-col-8" style="float: right;">
                                                            <asp:Label ID="lblCompulsory"  Visible="false"
                                                                runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom: 20px;" class="clear">
                                                    </div>
                                                    <div class="row-col-12">
                                                        <asp:GridView ID="gvSpecialEvents" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" GridLines="Horizontal" OnRowDataBound="gvSpecialEvents_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="PAX TYPE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPaxtype" Text='<%# Eval("paxtype") %>' runat="server"></asp:Label>
                                                                        <asp:HiddenField ID="hdSplistcode" Value='<%# Eval("splistcode") %>' runat="server" />
                                                                        <asp:HiddenField ID="hdSLineNo" Value='<%# Eval("splineno") %>' runat="server" />
                                                                        <asp:HiddenField ID="hdspleventcode" Value='<%# Eval("spleventcode") %>' runat="server" />
                                                                        <asp:HiddenField ID="hdspleventdate" Value='<%# Eval("spleventdate") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="ROOM NO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRoomNo" Text='<%# Eval("roomno") %>' runat="server"></asp:Label>
                                                                        <%-- <asp:TextBox ID="txtNoOfPax" Text='<%# Eval("noofpax") %>' runat="server"></asp:TextBox>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="NO OF PAX">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNoOfPax" Text='<%# Eval("noofpax") %>' runat="server"></asp:Label>
                                                                        <%-- <asp:TextBox ID="txtNoOfPax" Text='<%# Eval("noofpax") %>' runat="server"></asp:TextBox>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CHILD AGES">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblChildAges" Text='<%# Eval("childage") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PAX RATE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPaxRate" Text='<%# Eval("paxrate") %>' runat="server"></asp:Label>
                                                                         <asp:Label ID="lblwlPaxRate" Text='<%# Eval("wlpaxrate") %>' runat="server"></asp:Label>
                                                                         <div style="padding-top:5px">
                                                                        <asp:TextBox ID="txtPaxRate" Text='<%# Eval("paxrate")%>' CssClass="roomtype-popup-textbox"   onkeypress="validateDecimalOnly(event,this)"
                                                                            Width="70px" runat="server"></asp:TextBox>
                                                                               </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SPECIAL EVENT VALUE">
                                                                    <ItemTemplate>
                                                                      <asp:Label ID="lblwlSpecialEventValue" Text='<%# Eval("wlspleventvalue").ToString()  %>'
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblSpecialEventValue" Text='<%# Eval("spleventvalue").ToString()  %>'
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblpaxcurrcode" Text='<%# Eval("saleCurrcode") %>' Visible="false"
                                                                            runat="server"></asp:Label>
                                                                            <div style="display:none;">
                                                                                    <asp:Label ID="lblwlcurrcode" runat="server" Text='<%# Eval("wlcurrcode") %>' ></asp:Label>
                                                                                <asp:Label ID="lblwlconvrate" runat="server" Text='<%# Eval("wlconvrate") %>' ></asp:Label>
                                                                                <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>' ></asp:Label>
                                                                                   <asp:Label ID="lblSpecialEventValueNew" Text='<%# Eval("spleventvalue").ToString()%>'
                                                                            runat="server"></asp:Label>
                                                                                </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PAX COST">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpaxCost" Text='<%# Eval("paxcost")%>' runat="server"></asp:Label>
                                                                         <div style="padding-top:5px">
                                                                        <asp:TextBox ID="txtPaxCost" Text='<%# Eval("paxcost")%>' CssClass="roomtype-popup-textbox"   onkeypress="validateDecimalOnly(event,this)"
                                                                            Width="70px" runat="server"></asp:TextBox></div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SPECIAL EVENT COST VALUE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSpecialEventCostValue" Text='<%# Eval("spleventcostvalue").ToString() %>'
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblcostCurrcode" Text='<%# Eval("costCurrcode") %>' Visible="false"
                                                                            runat="server"></asp:Label>
                                                                            <asp:Label ID="lblSpecialEventCostValueNew" style="display:none;" Text='<%# Eval("spleventcostvalue").ToString() %>'
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                            <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                    </div>

                                                                                
                                                        <div class="row-col-6" style="margin-top: 10px;">
                                                            <asp:CheckBox ID="chkSpclComplimentaryToCustomer" CssClass="roomtype-popup-label"
                                                                runat="server" Text="Complementary to Customer" />
                                                        </div>
                                                        
                                                        <div class="row-col-6" style="margin-top: 10px;">
                                                            <asp:CheckBox ID="chkSpclComplimentaryFromSupplier" CssClass="roomtype-popup-label"
                                                                runat="server" Text="Complementary from Supplier" />
                                                        </div>
                                                </div>
                                                <div class="clear" style="padding-bottom: 20px;">
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>     

                                       <%--      <div class="row-col-6" style="margin-top: 10px;">
                                                            <asp:CheckBox ID="chkSpclComplimentaryToCustomer" CssClass="roomtype-popup-label"
                                                                runat="server" Text="" />  <asp:Label ID="Label12" CssClass="roomtype-popup-label" runat="server" Text="Complementary to Customer"></asp:Label>
                                                        </div>
                                                        
                                                        <div class="row-col-6" style="margin-top: 10px;">
                                                            <asp:CheckBox ID="chkSpclComplimentaryFromSupplier" CssClass="roomtype-popup-label"
                                                                runat="server" Text="" />  <asp:Label ID="Label13" CssClass="roomtype-popup-label" runat="server" Text="Complementary from Supplier"></asp:Label>
                                                        </div>--%>


                                           
                                                </div>

                                      
                                   
                                </div>
                                </asp:Panel>
                           
                     
                   
                    <asp:HiddenField ID="hdSPEPartyCode" runat="server" />
                    <asp:HiddenField ID="hdSPERoomTypeCode" runat="server" />
                    <asp:HiddenField ID="hdSPEMealPlanCode" runat="server" />
                    <asp:HiddenField ID="hdSPEcatCode" runat="server" />
                    <asp:HiddenField ID="hdSPEAccCode" runat="server" />
                    <asp:HiddenField ID="hdSPERatePlanId" runat="server" />
                    <asp:HiddenField ID="hdSPHotelRoomString" runat="server" />
                                        
                                        <div class="clear">
                                        </div>

                                                     <div class="search-large-i" style="padding-left:20px;padding-top:20px;">
                                                <div class="srch-tab-left">
                          <asp:Button ID="btnBook" class="authorize-btn" runat="server" onClientClick="ShowProgess()"   Text="Save Booking"></asp:Button> 
                          </div>
                           <div class="srch-tab-left">
                      
                        </div>
                        </div>
                        <div class="clear">
                                        </div>
                                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                                      </footer>
       </div>
       
     </div>


               <asp:UpdatePanel ID="upnlTimer1" runat="server"  UpdateMode="Conditional">
               <ContentTemplate>       
                    <div   id="dvSearchContent" runat="server"  class="two-colls" style="margin-top:30px;background-color:White;">
                                   
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>

             
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

    </script>

<!-- \\ scripts \\ -->
    <center>
        <div id="Loading1" runat="server" style="height: 150px; width: 500px;">
            <img alt="" id="Image1" runat="server" src="~/img/page-loader.gif" width="200" />
            <h2 style="display: none;" class="page-loader-label">
                Processing please wait...ssing please wait...</h2>
        </div>
    </center>
    <asp:ModalPopupExtender ID="ModalPopupDays" runat="server" BehaviorID="ModalPopupDays"
        TargetControlID="btnInvisibleGuest" CancelControlID="btnClose" PopupControlID="Loading1"
        BackgroundCssClass="ModalPopupForPageLoading">
    </asp:ModalPopupExtender>
    <input id="btnInvisibleGuest" runat="server" type="button" value="Cancel" style="display: none" />
    <input id="btnClose" type="button" value="Cancel" style="display: none" />
   
    <asp:HiddenField ID="hdLoginType" runat="server" />
    <asp:HiddenField ID="hdBookingEngineRateType" runat="server" />
    <asp:HiddenField ID="hdTab" runat="server" />
        <asp:HiddenField ID="hdHotelTab" runat="server" />
              <asp:HiddenField ID="hdHotelTabFreeze" runat="server" />
    <asp:HiddenField ID="hddlSpclEventRowNumber" runat="server" />
    <asp:HiddenField ID="hdEventSelectedValue" runat="server" />
    <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
    <asp:HiddenField ID="hdChildAgeLimit" runat="server" />
    <asp:HiddenField ID="hdMaxNoOfNight" runat="server" />
        <asp:HiddenField ID="hdEditRLineNo" runat="server" />
            <asp:HiddenField ID="hdEditRatePlanId" runat="server" />
               <asp:HiddenField ID="hdOPMode" runat="server" />
               <asp:HiddenField ID="hdOPModePreArranged" runat="server" />
                <asp:HiddenField ID="hdHotelAvailableForShifting" runat="server" />
                  <asp:HiddenField ID="hdOveride" runat="server" />
                  <asp:HiddenField ID="hdWhiteLabel" runat="server" />
                      <asp:HiddenField ID="hdSliderCurrency" runat="server" />
                          <asp:HiddenField ID="hdPriceSliderActive" runat="server" />
    <asp:Button ID="btnSelectedSpclEvent" runat="server" Style="display: none;"  />
      <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
    </form>
</body>
</html>
