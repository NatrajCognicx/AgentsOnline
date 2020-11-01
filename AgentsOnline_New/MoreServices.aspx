<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MoreServices.aspx.vb" Inherits="MoreServices" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta charset="utf-8" />
    <link rel="icon" href="favicon.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <link rel="stylesheet" href="css/jquery.formstyler.css">
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
<%--    <link href='http://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lora:400,400italic' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:100,200,300,400,600,700,800'
        rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,600' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700&amp;subset=latin,cyrillic'
        rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Lato:400,700&amp;subset=latin,latin-ext'
        rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700&amp;subset=latin,cyrillic'
        rel='stylesheet' type='text/css' />--%>
    <link id="lnkCSS" rel='stylesheet' type='text/css' href='css/style-common.css' />
    <link href="css/ValidationEngine.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/AgentsOnlineStyles.css" />
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
    <script type="text/javascript" src="js/script.js"></script>
    <link rel="stylesheet" type="text/css" href="css/dialog_box.css" />
    <script type="text/javascript" src="js/dialog_box.js"></script>
    <script type="text/javascript" src="js/jquery.collapsiblepanel.js"></script>
    <script type="text/javascript" src="js/jquery-ui-timepicker-addon.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery-ui.css" />
    <link rel="stylesheet" media="all" type="text/css" href="css/jquery-ui-timepicker-addon.css" />
    <style type="text/css">
        div.blueTable
        {
            border: 1px solid #dddd;
            background-color: #FFFFFF;
            width: 102%;
            text-align: left;
            border-collapse: collapse;
        }
        .divTable.blueTable .divTableCell, .divTable.blueTable .divTableHead
        {
            border: 1px solid #dddd;
            padding: 5px 5px;
            color: #828282;
            text-transform: uppercase;
            font-family: Raleway;
            font-weight: 500;
        }
        .divTable.blueTable .divTableBody .divTableCell
        {
            font-size: 13px;
        }
        .divTable.blueTable .divTableHeading
        {
            background: #F2F2F2;
            background: -moz-linear-gradient(top, #f5f5f5 0%, #f3f3f3 66%, #F2F2F2 100%);
            background: -webkit-linear-gradient(top, #f5f5f5 0%, #f3f3f3 66%, #F2F2F2 100%);
            background: linear-gradient(to bottom, #f5f5f5 0%, #f3f3f3 66%, #F2F2F2 100%);
        }
        .divTable.blueTable .divTableHeading .divTableHead
        {
            font-size: 15px;
            font-weight: bold;
            color: #FF9947;
        }
        .blueTable .tableFootStyle
        {
            font-size: 14px;
        }
        .blueTable .tableFootStyle .links
        {
            text-align: right;
        }
        .blueTable .tableFootStyle .links a
        {
            display: inline-block;
            background: #1C6EA4;
            color: #FFFFFF;
            padding: 2px 8px;
            border-radius: 5px;
        }
        .blueTable.outerTableFooter
        {
            border-top: none;
        }
        .blueTable.outerTableFooter .tableFootStyle
        {
            padding: 3px 5px;
        }
        /* DivTable.com */
        .divTable
        {
            display: table;
        }
        .divTableRow
        {
            display: table-row;
        }
        .divTableHeading
        {
            display: table-header-group;
        }
        .divTableCell, .divTableHead
        {
            display: table-cell;
        }
        .divTableHeading
        {
            display: table-header-group;
        }
        .divTableFoot
        {
            display: table-footer-group;
        }
        .divTableBody
        {
            display: table-row-group;
        }
        
        
        
        
        .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default
        {
            font-family: 'Montserrat' !important;
            border: 0px !important;
            font-size: 11px;
            font-weight: normal;
            border-radius: 1px !important;
            display: block;
            background-color: #999999;
            color: #000 !important;
        }
        
        .mygrid
        {
            width: 100%;
            border: 1px solid #EDE7E1;
            font-family: Raleway !important;
            color: #455051;
        }
        .mygrid-header
        {
            background-color: #EDE7E1;
            font-family: Raleway !important;
            color: #455051;
            height: 25px;
            text-align: left;
            font-size: 11px !important;
            font-size: small;
            font-style: normal !important;
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
            font-size: 12px !important;
            color: #455051;
            min-height: 10px;
            text-align: left;
            border: 1px solid #EDE7E1;
            vertical-align: middle;
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
            color: #455051;
            padding: 5px 5px 5px 5px;
            font-size: 11px;
            vertical-align: middle;
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
    </script>
    <script type="text/javascript">
        settimeout(function () { document.getElementById('<%=btnexcelreport.ClientID%>').click() }, 3000);
</script>
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
    <script>

        $(document).ready(function () {
            //          $('#confirm_datetimepicker').datetimepicker();
            'use strict';
            (function ($) {
                $(function () {

                    $('.checkbox input').styler({
                        selectSearch: true
                    });
                });
            })(jQuery);


        });

    </script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $('.time-picker').timepicker();
            fillDates();

        });
     
    </script>
    <!-- \\ scripts \\ -->
    <script language="javascript" type="text/javascript">

        function fnHidePackage() {

            //    $("#dvROPackage1").css('display', 'none');
            //           $('#dvROPackage1').hide();
            //           $('#dvROPackage2').hide();
            //           $('#dvROPackage3').hide();
            //           $('#dvROPackage4').hide();
            //           $('#dvROPackage5').hide();
            //           $('#dvROPackage6').hide();
            //           $('#dvROPackage7').hide();
        }


        function HideButtons() {
            //           var divsubmitquote = document.getElementById("divsubmitquote");
            //           divsubmitquote.style.display = "none";
            alert('test');
        }

        function hidepopup() {

            var mpConfirm = $find("mpBookingError");

            mpConfirm.hide();
            return true;

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

        function ChkApplyConfirm(chk) {

            document.getElementById("<%= btnApplySameConfChk.ClientID %>").click();
        }

        function ChkApplyCancel(chk) {
            document.getElementById("<%= btnApplySameCancelChk.ClientID %>").click();

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
                var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalAirport");

                $get(hiddenfieldID).value = eventArgs.get_value();
                GetAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport);
            }

        }

        function GetAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport) {
            $.ajax({
                type: "POST",
                url: "GuestPage.aspx/GetAirportAndTimeDetails",
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

                // var row = $(source.get_id()).closest("tr");
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_DepartureFlight", "txtDepartureFlightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_DepartureFlight", "txtDepartureTime");
                var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_DepartureFlight", "txtDepartureAirport");

                $get(hiddenfieldID).value = eventArgs.get_value();
                GetDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport);

                //               //Find the TextBox control.
                //               var txtName = row.find("[id*=txtDepartureflightCode]");
                //               var hiddenfieldIDTime = row.find("[id*=txtDepartureTime]"); //source.get_id().replace("AutoCompleteExtender_DepartureFlight", "txtDepartureTime");  //
                //               var hiddenfieldIDAirport = row.find("[id*=txtDepartureAirport]");
                //               txtName.value = eventArgs.get_value();
                //               alert(hiddenfieldIDTime.value);
                //               GetDepartureAirportAndTimeDetails(txtName.value, hiddenfieldIDTime, hiddenfieldIDAirport);
            }
        }

        function GetDepartureAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport) {
            $.ajax({
                type: "POST",
                url: "GuestPage.aspx/GetDepartureAirportAndTimeDetails",
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
            var ddlVisa = source.get_id().replace("AutoCompleteExtender_txtNationality", "ddlVisa");
            var ddlVisaType = source.get_id().replace("AutoCompleteExtender_txtNationality", "ddlVisatype");
            var txtPrice = source.get_id().replace("AutoCompleteExtender_txtNationality", "txtVisaPrice");
            if (hiddenfieldName != '') {
                $get(hiddenfieldID).value = eventArgs.get_value();
                $get(txtPrice).value = '';
                SelectVisa($get(hiddenfieldID).value, $get(ddlVisa), $get(ddlVisaType))
            }
            else {
                $get(hiddenfieldID).value = '';
            }

        }
        function SelectVisa(nationality, ddlVisa, ddlVisaType) {

            PageMethods.SelectVisa(nationality, CallSuccess(ddlVisa, ddlVisaType), CallFailed)
        }
        function CallSuccess(ddlVisa, ddlVisaType) {
            return function (response) {
                //handle success code here

                if (response == '0') {
                    ddlVisa.value = "Required";
                    ddlVisa.selectedIndex = 1;
                }
                else {
                    ddlVisa.value = "Not Required";
                    ddlVisa.selectedIndex = 2;
                }

                ddlVisa.onchange();
                ddlVisaType.onchange();

            };

        }

        // alert message on some failure
        function CallFailed(res) {
            //  txtNoDay.value = '';
        }

        function AutoCompleteExtender_NationalityKeyUp() {
            $(this).keyup("change", function () {

            });

            //        $("#< %=dlPersonalInfo.ClientID%> tr input[id*='txtNationality']").each(function () {
            //            alert('test');
            //            $(this).bind("change", function () {

            //                var hiddenfieldID1 = $(this).attr("id").replace("txtNationality", "txtNationality");
            //                var hiddenfieldID = $(this).attr("id").replace("txtNationality", "txtNationalityCode");
            //                if (hiddenfieldID1.value == '') {
            //                    hiddenfieldID.value = '';
            //                }
            //            });
            //            $(this).keyup("change", function () {

            //                var hiddenfieldID1 = $(this).attr("id").replace("txtNationality", "txtNationality");
            //                var hiddenfieldID = $(this).attr("id").replace("txtNationality", "txtNationalityCode");
            //                if (hiddenfieldID1.value == '') {
            //                    hiddenfieldID.value = '';
            //                }
            //            });
            //        });
        }

    </script>
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

        function RefreshContent() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }

        function EndRequestHandler() {
//            $('.collapse').collapsiblePanel({
//                toggle: true
//            });

            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();


            $(".date-inpt-check-in").datepicker({

                minDate: new Date(currentYear, currentMonth, currentDate)

            });
            $('.checkbox input').styler({
                selectSearch: true
            });

            $('.time-picker').timepicker();

            fillDates();
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

        function TrfChangeDate(txtDays, txtTimeLimit) {

            var txt = document.getElementById('<%=lblClbltransdate.ClientID%>');

            var ddl = document.getElementById(txtDays);

            var txt1 = document.getElementById(txtTimeLimit);


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

        function showhidedivs() {
          //  alert('test');
        }
        function Calculateprofit(txtmarkup, txtpax, lblprofit) {

            var txtmarkup = document.getElementById(txtmarkup);
         
            var txtpax = document.getElementById(txtpax);

            var lblprofit = document.getElementById(lblprofit);


           
            lblprofit.innerHTML = (txtpax.innerHTML * txtmarkup.value);

           

        
        }

        function ChangeDateForAll(txtDays, txtTimeLimit, lblTransdate) {

            var txt = document.getElementById(lblTransdate);

            var ddl = document.getElementById(txtDays);

            var txt1 = document.getElementById(txtTimeLimit);


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
        function confirmHotelDelete() {

            var opMode = document.getElementById('<%=hdOpMode.ClientID%>').value;
            var msg = '';
            if (opMode == 'Edit') {
                return true;


            }
            else {
                msg = 'Are you sure to remove this line from the booking?';
            }

            if (confirm(msg)) {

                return true;
            }
            else {
                return false;
            }

        }
        function confirmDelete() {
            var opMode = document.getElementById('<%=hdOpMode.ClientID%>').value;
            var msg = '';
            if (opMode == 'Edit') {
                //  return true;
                msg = 'Are you sure want to Cancel?';
            }
            else {
                msg = 'Are you sure want to Remove?';
            }

            if (confirm(msg)) {

                return true;
            }
            else {
                return false;
            }

        }

        function confirmDiscountPackage(msg) {
            alert('ssss');


            if (confirm(msg)) {
                document.getElementById("<%= btnFillPackageWithoutDiscount.ClientID %>").click();

                return true;
            }
            else {
                return false;
            }

        }


        function confirmsaveDiscountPackage(msg) {


            if (confirm(msg)) {
                document.getElementById("<%= btncheck.ClientID %>").click();

                return true;
            }
            else {
                return false;
            }

        }


        function confirmEntireServiceCancel() {
            var hdOnlyHotelbooking = document.getElementById('<%=hdOnlyHotelbooking.ClientID%>').value;
            var type = document.getElementById('<%=hdCanceltype.ClientID%>').value;
            var hdCancelEntireBooking = document.getElementById('<%=hdCancelEntireBooking.ClientID%>');
            if (type == '1' || hdOnlyHotelbooking == '1') {
                hdCancelEntireBooking.value = '0';
                return true;
            }
            else {

                if (confirm(msg = 'Do you want to cancel all services for this booking? Please press OK button else press CANCEL button for this hotel only.')) {
                    hdCancelEntireBooking.value = '1';
                    return true;
                }
                else {
                    hdCancelEntireBooking.value = '0';
                    return true;
                }
            }

        }
                
    </script>
</head>
<body onload="RefreshContent()">
    <form id="form1" runat="server">
    <div>
        <!-- // authorize // -->
        <div class="overlay">
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <header id="top">
	<div class="header-a">
		<div class="wrapper-padding">	
         <div class="header-user"  style="margin-top:2px;"><asp:Label ID="lblHeaderUserName"  runat="server" ></asp:Label> </div>				
			<div class="header-phone"  style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-account">
				<asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
			</div>
              <div class="header-agentname" style="padding-left:105px;margin-top:2px;"><asp:Label ID="lblHeaderAgentName" runat="server" ></asp:Label> </div>
            <div class="header-lang">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><asp:Button ID="btnLogOut"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Log Out"></asp:Button>
</ContentTemplate></asp:UpdatePanel>
                <%--	<a href="#">Log Out</a>--%>
			</div>
			<div class="header-social"  style="display:none;" >
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
			<div class="header-logo"><a href="#"><img id="imgLogo" runat="server" alt="" /></a></div>
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
                        </div>
                        <div class="breadcrumbs">
                            <a href="#">Home</a> / <a href="#">Services</a> / <span>Summary</span>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="sp-page" style="margin-top: 10px;">
                        <div class="sp-page-a">
                            <div class="sp-page-l">
                                <div class="sp-page-lb">
                                    <div class="sp-page-p">
                                        <div style="padding-bottom: 15px; margin-top: 5px; margin-bottom: 10px;">
                                            <asp:Label ID="Label1" Text="CLICK ON ANY OF THE SERVICES BELOW TO ADD THESE TO THE BOOKING"
                                                CssClass="additional-service-heading-label" runat="server"></asp:Label>
                                        </div>
                                        <div class="additional-service-images" style="margin-top: 5px; padding-bottom: 15px;">
                                            <a id="lnkAccom" style="padding-right: 2px;" runat="server" href='#'>
                                                <img width="200px" height="150px" alt='' style="border: 1px solid #e3e3e3" src='img/Accommodation.jpg' /></a><%--Home.aspx?Tab=0 --%>
                                            <a id="lnkTours" style="padding-right: 2px;" runat="server">
                                                <img width="200px" height="150px" alt='' style="border: 1px solid #e3e3e3" src='img/Tours.jpg' /></a>
                                            <a id="lnkAirport" style="padding-right: 2px;" runat="server">
                                                <img width="200px" height="150px" alt='' style="border: 1px solid #e3e3e3" src='img/Airport meet and greet.jpg' /></a>
                                        </div>
                                        <div class="additional-service-images">
                                            <a id="lnkTransfer" style="padding-right: 2px;" runat="server">
                                                <img width="200px" height="150px" style="border: 1px solid #e3e3e3" alt='' src='img/Transfers.jpg' /></a>
                                            <a id="lnkVisa" style="padding-right: 0px;" runat="server">
                                                <img width="200px" height="150px" alt='' style="border: 1px solid #e3e3e3" src='img/Visa.jpg' /></a>
                                            <a id="lnkOtherServices" style="padding-right: 0px;" runat="server">
                                                <img width="200px" height="150px" alt='' style="border: 1px solid #e3e3e3" src='img/OtherServices.jpg' /></a>
                                        </div>
                                        <div class="booking-left">
                                            <div id="dvAgenRef" runat="server">
                                                <div class="input" style="float: left; margin-top: 20px">
                                                    <label>
                                                        Agency Ref.</label></div>
                                                <div class="booking-form-i-a" style="margin-left: 20px; margin-top: 20px">
                                                    <div class="input">
                                                        <asp:TextBox ID="txtAgencyRef" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                            <div class="page-search-content-Additional-Service" id="divcheck" runat="server">
                                                <div class="checkbox" style="float: left; margin-top: 60px">
                                                    <asp:CheckBox ID="chkTermsAndConditions" runat="server" /></div>
                                                <div class="booking-form-i-a" style="margin-left: 20px; margin-top: 60px">
                                                    <label>
                                                        I Accept the Terms and Conditions</label>
                                                </div>
                                            </div>
                                            <div id="divsubmitquote" runat="server" style="width: 200px; float: left">
                                                <asp:Button ID="btnSubmitQuote" class="authorize-btn" Width="200px" runat="server"
                                                    Text="Generate Quote" />
                                            </div>
                                            <div id="divproceed" runat="server" style="margin-left: 220px;">
                                                <asp:Button ID="btnProceedBooking" class="authorize-btn" Width="200px" runat="server"
                                                    Text="Book" />
                                            </div>
                                            <div id="divabondan" runat="server" style="margin-left: 450px;">
                                                <asp:Button ID="btnAbondon" class="authorize-btn" Width="200px" runat="server" OnClientClick="return Validate()"
                                                    Text="Abandon" />
                                            </div>
                                            <div id="divprintquote" runat="server" style="width: 200px; float: left; display: none">
                                                <asp:Button ID="btnprintquote" class="authorize-btn" Width="200px" runat="server"
                                                    Text="Print Quote" />
                                            </div>
                                            <div id="divbackhome" runat="server" style="margin-left: 220px; display: none">
                                                <asp:Button ID="btnbacktohome" class="authorize-btn" Width="200px" runat="server"
                                                    Text="Back to Home" />
                                            </div>
                                            <div id="divcheckbutton" runat="server" style="width: 200px; display: none; float: right">
                                                <asp:Button ID="btncheck" class="authorize-btn" Width="200px" runat="server" Text="Generate Quote" />
                                            </div>
                                             <div id="divexcelreport" runat="server" style="width:200px ;display: none;  float: left;padding-left:20px">
                                              <asp:Button ID="btnexcelreport" class="authorize-btn"  style="width: 180px;  float: right"    runat="server" Text="Excel Report" />
                                               
                                            </div>
                                            <asp:UpdatePanel ID="up1" runat="server">
                                                <ContentTemplate>
                                                    <div class="clear">
                                                    </div>
                                                    <div>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                    <div>
                                                        <div class="booking-form">
                                                            <div class="booking-form-i-a">
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    </div>
                                                    <asp:HiddenField ID="hdCheckIn" runat="server" />
                                                    <asp:HiddenField ID="hdCheckOut" runat="server" />
                                                    <asp:HiddenField ID="hdCheckInPrevDay" runat="server" />
                                                    <asp:HiddenField ID="hdCheckInNextDay" runat="server" />
                                                    <asp:HiddenField ID="hdCheckOutPrevDay" runat="server" />
                                                    <asp:HiddenField ID="hdCheckOutNextDay" runat="server" />
                                                    <asp:HiddenField ID="hdBookingEngineRateType" runat="server" />
                                                    <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
                                                    <asp:HiddenField ID="hdQuoteReqestId" runat="server" />
                                                    <asp:HiddenField ID="hdTabHotelTotalPrice" runat="server" />
                                                    <asp:HiddenField ID="hdTourTabTotalPrice" runat="server" />
                                                    <asp:HiddenField ID="hdTransferTabTotalPrice" runat="server" />
                                                    <asp:HiddenField ID="hdVisaTabTotalPrice" runat="server" />
                                                    <asp:HiddenField ID="hdAirportTabtotalPrice" runat="server" />
                                                    <asp:HiddenField ID="hdOtherServiceTabTotalPrice" runat="server" />
                                                    <asp:HiddenField ID="hdOpMode" runat="server" />
                                                    <asp:HiddenField ID="hdCancelEntireBooking" runat="server" />
                                                    <asp:HiddenField ID="hdCanceltype" runat="server" />
                                                    <asp:HiddenField ID="hdOnlyHotelbooking" runat="server" />
                                                     <asp:HiddenField ID="hdTempRequest" runat="server" />
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
                        <asp:UpdatePanel ID="UpbookError" runat="server">
                            <ContentTemplate>
                                <asp:ModalPopupExtender ID="mpBookingError" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                    CancelControlID="aErrorclose" EnableViewState="true" PopupControlID="pnlBookingError"
                                    TargetControlID="btnviewchild">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="pnlBookingError" Style="display: none;" runat="server">
                                    <div id="divError" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                        max-height: 500px;">
                                        <div align="left" id="Div12">
                                            <div id="divErrorlist" runat="server" style="border: 1px solid #fff; width: 700px;
                                                height: 30px; background-color: #ede7e1; padding-right: 5px; margin-right: 10px;
                                                text-align: center; vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                                <asp:Label ID="lblErrorlist" CssClass="room-type-breakup-headings" Text="Booking Errors"
                                                    runat="server"></asp:Label>
                                                &nbsp; &nbsp;<a id="aErrorclose" href="#" class="roomtype-popupremarks-close"></a>
                                            </div>
                                            <div class="booking-form" id="Div13" runat="server" style="background-color: White;
                                                padding: 5px;">
                                                <div class="row-col-12" style="padding-left: 10px;">
                                                    <asp:GridView ID="gdErrorlist" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                        Width="100%" GridLines="Horizontal">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsno" Text='<%# Eval("autoid") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
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
                                                <div class="booking-form-i-a" style="margin-left: 100px; margin-top: 30px;">
                                                    <div class="clear">
                                                    </div>
                                                    <asp:Button ID="btnbacktobooking" class="authorize-btn" Width="170px" runat="server"
                                                        Text="Back to Booking" />
                                                </div>
                                                <div class="booking-form-i-a" style="margin-top: 30px;">
                                                    <div class="clear">
                                                    </div>
                                                    <asp:Button ID="btnproceed" class="authorize-btn" Width="170px" runat="server" OnClientClick="hidepopup();"
                                                        Text="Proceed with Saving" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <input id="btnviewchild" runat="server" type="button" value="Cancel" style="display: none" />
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                                            <asp:Label ID="lblvisaarrremarks" CssClass="room-type-breakup-headings" Text="Arrival - Information to Operation"
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
                                                <asp:Label ID="lbltrfsArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival - Information to Operation"
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
                                                <asp:Label ID="lblTrfsDeptRemarks" CssClass="room-type-breakup-headings" Text="Departure - Information to Operation"
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
                                                <asp:Label ID="lblAirArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival - Information to Operation"
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
                                                <asp:Label ID="lblAirdeptRemarks" CssClass="room-type-breakup-headings" Text="Departure - Information to Operation"
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
                        <asp:ModalPopupExtender ID="MPOthServRemarks" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aOthRemarksClose" EnableViewState="true" PopupControlID="pnlOtherServRemarks"
                            TargetControlID="hdnOthServ">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="hdnOthServ" runat="server" />
                        <asp:Panel runat="server" ID="pnlOtherServRemarks" Style="display: none;">
                            <div id="Div8" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                max-height: 500px;">
                                <div align="left" id="Div14">
                                    <div id="Div15" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
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
                                                <asp:Label ID="lblOthArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival - Information to Operation"
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
                                                <asp:Label ID="lblOthDeptRemarks" CssClass="room-type-breakup-headings" Text="Departure - Information to Operation"
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
                        <asp:ModalPopupExtender ID="mpRemarks" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aRemarksClose" EnableViewState="true" PopupControlID="pnlRemarks"
                            TargetControlID="imgbRemarks">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="imgbRemarks" runat="server" />
                        <asp:Panel runat="server" ID="pnlRemarks" Style="display: none;">
                            <div id="dvremarks" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                max-height: 500px;">
                                <div align="left" id="Div5">
                                    <div id="Div1" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                                        background-color: #ede7e1; padding-right: 5px; margin-right: 10px; text-align: center;
                                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                        <asp:Label ID="lblheading" CssClass="room-type-breakup-headings" Text="Remarks" runat="server"></asp:Label>
                                        &nbsp; &nbsp;<a id="aRemarksClose" href="#" class="roomtype-popupremarks-close"></a>
                                    </div>
                                    <div id="Div2" class="roomtype-price-breakuppopup" style="max-height: 500px; overflow: auto">
                                        <div id="dvHotelRemarkshead" runat="server">
                                            <div id="dvHotelRemarkslbl" runat="server" style="border: 1px solid #fff; width: 700px;
                                                height: 30px; background-color: #ede7e1; padding-right: 5px; padding-left: 5px;
                                                margin-right: 10px; text-align: left; vertical-align: middle; padding-top: 10px;">
                                                <asp:Label ID="lblHotelRemarks" CssClass="room-type-breakup-headings" Text="Information to Hotel"
                                                    runat="server"></asp:Label>
                                            </div>
                                            <div class="booking-form">
                                                <div class="booking-form">
                                                    <div style="padding-left: 10px; margin-bottom: 10px; margin-top: 10px;" class="checkbox">
                                                        <asp:CheckBoxList ID="chkHotelRemarks" CellSpacing="5" CellPadding="5" RepeatColumns="3"
                                                            runat="server" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
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
                                                <asp:Label ID="lblArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival - Information to Operation"
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
                                                <asp:Label ID="lblDeptRemarks" CssClass="room-type-breakup-headings" Text="Departure - Information to Operation"
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
                        <asp:ModalPopupExtender ID="MPToursRemarks" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aToursRemarksClose" EnableViewState="true" PopupControlID="pnlToursRemarks"
                            TargetControlID="ImghdnToursRemarks">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="ImghdnToursRemarks" runat="server" />
                        <asp:Panel runat="server" ID="pnlToursRemarks" Style="display: none;">
                            <div id="dvToursRemarks" class="roomtype-price-breakuppopup" style="float: left;
                                text-align: left; max-height: 500px;">
                                <div align="left" id="Div10">
                                    <div id="Div11" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                                        background-color: #ede7e1; padding-right: 5px; padding-left: 8px; text-align: left;
                                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                        <div class="chk-l" style="font-size: small;">
                                            <asp:Label ID="lbltourhead" runat="server" Text="Excursion  Name -" CssClass="room-type-breakup-headings"></asp:Label>
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
                                            <div runat="server" style="border: 1px solid #fff; width: 700px; height: 30px; background-color: #ede7e1;
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
                                                <asp:Label ID="lblToursArrRemarks" CssClass="room-type-breakup-headings" Text="Arrival - Information to Operation"
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
                                                <asp:Label ID="lblToursDeptRemarks" CssClass="room-type-breakup-headings" Text="Departure - Information to Operation"
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
                        <asp:ModalPopupExtender ID="mpConfirm" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aConfirmClose" EnableViewState="true" PopupControlID="pnlConfirm"
                            TargetControlID="imgbConfirm">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="imgbConfirm" runat="server" />
                        <asp:Panel ID="pnlConfirm" Style="display: none;" runat="server">
                            <div align="left" id="Div7">
                                <div id="dvheading" runat="server" style="border: 1px solid #fff; width: 900px; height: 30px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div class="chk-l" style="margin-left: 20px; font-size: small;">
                                        Room Type &nbsp;&nbsp;&nbsp;</div>
                                    <%--style="width:25%"--%>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="lblConfirmHeading" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <%--style="width:75%"--%>
                                    <a id="aConfirmClose" href="#" class="roomtype-popupconfirm-close"></a>
                                </div>
                                <div id="dvConfirm" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
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
                                            <asp:Label ID="lblPersons" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lbldlrlineno" Visible="false" runat="server"></asp:Label>
                                        </div>
                                        <div class="chk-l" style="margin-left: 40px; font-size: 12px; color: #4a90a4;">
                                            Check Out &nbsp;&nbsp;
                                        </div>
                                        <div class="chk-r" style="font-size: 12px; padding-left: 10px">
                                            <asp:Label ID="lblCheckOutDate" ReadOnly="true" runat="server"></asp:Label>
                                        </div>
                                        <asp:UpdatePanel ID="upchkapplysame" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div id="dvchkapply" runat="server" style="font-size: 12px; padding-left: 10px; margin-left: 620px;
                                                    margin-top: -20px">
                                                    <asp:CheckBox runat="server" class="checkbox" ID="chkApplySameConf" />
                                                </div>
                                                <div class="chk-r" style="font-size: 12px; padding-left: 10px; margin-left: 650px;
                                                    margin-top: -20px">
                                                    <asp:Label ID="lblapplyconfirm" Style="font-size: 12px;" runat="server" Text="Apply Same Confirmation to all Rooms"></asp:Label>
                                                </div>
                                                <asp:Button ID="btnApplySameConfChk" runat="server" Style="display: none;" Text="Filter" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="upgvconfirmbooking" runat="server">
                                            <ContentTemplate>
                                                <div id="dvCancelheading" runat="server" style="border: 1px solid #fff; width: 383px;
                                                    height: 25px; background-color: #ede7e1; padding-right: 5px; margin-left: 510px;
                                                    margin-top: 15px; text-align: center; vertical-align: middle; padding-top: 5px;
                                                    margin-bottom: 0px;">
                                                    <asp:Label ID="lblcancelheading" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                        Text="Cancel Without Charge" runat="server"></asp:Label>
                                                </div>
                                                <div class="booking-form" id="dvgvConfirmBooking" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvConfirmBooking" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" OnRowDataBound="gvConfirmBooking_RowDataBound" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Room No.">
                                                                    <%--ControlStyle-Width="80px"--%>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNoOfRooms" Text='<%# Eval("RoomNumber") %>' runat="server"></asp:Label>
                                                                        <%-- <asp:TextBox ID="txtNoOfPax" Text='<%# Eval("noofpax") %>' runat="server"></asp:TextBox>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Room Occupancy">
                                                                    <%--ControlStyle-Width="100px"--%>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRoomOcc" Text='<%# Eval("RoomOccupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <%--ControlStyle-Width="110px"--%>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hotel Confirmation No.">
                                                                    <%--ControlStyle-Width="120px"--%>
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvhotelconfno" runat="server">
                                                                                <asp:TextBox ID="txthotelconfno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    runat="server"> </asp:TextBox><%--Text='<%# Eval("hotelconfno").ToString()%> '--%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Confirm Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtConfirmDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("confirmdate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox><%-- Text='<%# Eval("confirmdate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnConfDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Days">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 40px; float: left; height: 10px;">
                                                                            <div id="dvdays" runat="server" class="input input">
                                                                                <asp:TextBox ID="txtCancelDays" Style="text-align: right; font-size: 12px;" onchange="ChangeDate();"
                                                                                    onkeypress="validateDecimalOnly(event,this)" Width="22px" Text='<%# Eval("CancelDays")%>'
                                                                                    runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit" ControlStyle-Width="115px">
                                                                    <ItemTemplate>
                                                                        <div id="dvtimelimitdate" runat="server" class="input-a" style="width: 115px; padding-bottom: 9px">
                                                                            <asp:TextBox ID="txtTimeLimitDate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                                                runat="server"></asp:TextBox>
                                                                            <%--Text='<%# Eval("TimeLimitDate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdtimelimit" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 60px; float: left; height: 10px; padding-right: 3px">
                                                                            <div id="dvtimelimittime" runat="server" class="input input">
                                                                                <input id="txtTimeLimitTime" placeholder="hh:mm" class="time-picker" style="border: none;
                                                                                    width: 38px; font-size: 12px;" runat="server" />
                                                                                <%-- value='<%# Eval("TimeLimitTime") %>'--%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Prev. Confirmation No." ControlStyle-Width="125px">
                                                                    <ItemTemplate>
                                                                        <div id="PrevConfNo" runat="server" class="booking-form-i-a" style="width: 115px;
                                                                            float: left; height: 10px;">
                                                                            <div class="input input">
                                                                                <asp:TextBox ID="txtPrevConfNo" Style="width: 95px; font-size: 12px;" runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnPrevConfNo" runat="server"></asp:HiddenField>
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
                        <asp:ModalPopupExtender ID="mpHotelCancel" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aHotelCancelClose" EnableViewState="true" PopupControlID="pnlHotelCancel"
                            TargetControlID="hdHotelCancel">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="mpConnectHotelCancel" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aConnectHotelCancelClose" EnableViewState="true" PopupControlID="pnlConnectHotelCancel"
                            TargetControlID="hdConnectHotelCancel">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="mpTransferCancel" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aTrfCancelClose" EnableViewState="true" PopupControlID="pnltransfercancel"
                            TargetControlID="hdTransferCancel">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="mpVisaCancel" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aVisaCancelClose" EnableViewState="true" PopupControlID="pnlvisacancel"
                            TargetControlID="hdvisaCancel">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="mptourCancel" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="atourCancelClose" EnableViewState="true" PopupControlID="pnltourcancel"
                            TargetControlID="hdtourCancel">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="mpotherCancel" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aothCancelClose" EnableViewState="true" PopupControlID="pnlothcancel"
                            TargetControlID="hdotherCancel">
                        </asp:ModalPopupExtender>
                        <asp:ModalPopupExtender ID="mpairCancel" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aairCancelClose" EnableViewState="true" PopupControlID="pnlaircancel"
                            TargetControlID="hdairCancel">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="hdHotelCancel" runat="server" />
                        <asp:HiddenField ID="hdConnectHotelCancel" runat="server" />
                        <asp:HiddenField ID="hdTransferCancel" runat="server" />
                        <asp:HiddenField ID="hdvisaCancel" runat="server" />
                        <asp:HiddenField ID="hdtourCancel" runat="server" />
                        <asp:HiddenField ID="hdotherCancel" runat="server" />
                        <asp:HiddenField ID="hdairCancel" runat="server" />
                        <asp:Panel ID="pnlHotelCancel" Style="display: none;" runat="server">
                            <div align="left" id="Div33">
                                <div id="Div34" runat="server" style="border: 1px solid #fff; width: 900px; height: 40px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div style="float: left; padding-left: 15px; padding-bottom: 5px; padding-top: 5px;">
                                        <asp:Label ID="Label24" CssClass="room-type-breakup-headings" Text="Cancel Accomodation"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div style="float: left; margin-top: -21px;">
                                        <a id="aHotelCancelClose" href="#" class="roomtype-popupconfirm-close"></a>
                                    </div>
                                </div>
                                <div id="Div35" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <div class="chk-l" style="margin-left: 20px; font-size: small;">
                                        Room Type &nbsp;&nbsp;&nbsp;</div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="lblHotelCancelHeading" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="clear" style="padding-bottom: 10px;">
                                    </div>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <div class="chk-l" style="font-size: 12px; margin-left: 20px; color: #4a90a4;">
                                            Check In &nbsp;
                                        </div>
                                        <div class="chk-r" style="font-size: 12px; padding-left: 20px">
                                            <asp:Label ID="lblHotelCheckInDate" ReadOnly="true" runat="server"></asp:Label><asp:Label
                                                ID="Label26" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="Label27" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="Label28" Visible="false" runat="server"></asp:Label>
                                        </div>
                                        <div class="chk-l" style="margin-left: 40px; font-size: 12px; color: #4a90a4;">
                                            Check Out &nbsp;&nbsp;
                                        </div>
                                        <div class="chk-r" style="font-size: 12px; padding-left: 10px">
                                            <asp:Label ID="lblHotelCheckOutDate" ReadOnly="true" runat="server"></asp:Label>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div id="Div37" runat="server" style="font-size: 12px; padding-left: 10px; margin-left: 620px;
                                                    margin-top: -20px">
                                                    <asp:CheckBox runat="server" class="checkbox" ID="chkApplySameCancel" />
                                                </div>
                                                <div class="chk-r" style="font-size: 12px; padding-left: 10px; margin-left: 650px;
                                                    margin-top: -20px">
                                                    <asp:Label ID="Label30" Style="font-size: 12px;" runat="server" Text="Apply Same Cancellation to all Rooms"></asp:Label>
                                                </div>
                                                <asp:Button ID="btnApplySameCancelChk" runat="server" Style="display: none;" Text="Filter" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>                                                
                                                <div id="dvCancelWithOutCharge" runat="server" style="border: 1px solid #fff; width: 383px; height: 25px;
                                                    background-color: #ede7e1; padding-right: 5px; margin-left: 510px; margin-top: 15px;
                                                    text-align: center; vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                                    <asp:Label ID="Label31" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                        Text="Cancel Without Charge" runat="server"></asp:Label>
                                                </div>
                                                <div class="booking-form" id="Div39" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvHotelCancel" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                        <asp:Label ID="lbrLineNo" Text='<%# Eval("rlineno") %>' Visible="false" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Room No.">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblNoOfRooms" Text='<%# Eval("RoomNumber") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Room Occupancy">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRoomOcc" Text='<%# Eval("RoomOccupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblTimeLimit" Text='<%# Eval("timelimit") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:CheckBox runat="server" class="checkbox" ID="chkHotelCancel" />
                                                                        <asp:HiddenField ID="hdnHotelCancel" Value='<%# Eval("cancelled") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hotel Cancellation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvhotelcancelno" runat="server">
                                                                                <asp:TextBox ID="txthotelcancelno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    Text='<%# Eval("hotelcancelno") %>' runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtHotelCancelDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("canceldate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnHotelCancelDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel By">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input" style="border-style: none;" runat="server">
                                                                                <asp:Label ID="lblHotelCancelBy" Text='<%# Eval("cancelby") %>' runat="server"></asp:Label>
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
                                        <asp:Button ID="btnHotelCancelSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                    <input id="Button3" runat="server" type="button" value="Cancel" style="visibility: hidden" />
                                    <input id="Text1" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text2" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text3" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlConnectHotelCancel" Style="display: none;" runat="server">
                            <div align="left" id="Div38">
                                <div id="Div119" runat="server" style="border: 1px solid #fff; width: 900px; height: 40px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div style="float: left; padding-left: 15px; padding-bottom: 5px; padding-top: 5px;">
                                        <asp:Label ID="Label105" style="color:#ff7200;font-family: 'Raleway'; font-weight: bold;font-size: 14px !important;text-decoration:none;"
                                           Text="CANCEL   ACCOMMODATION" runat="server"></asp:Label>
                                    </div>
                                    <div style="float: left; margin-top: -21px;">
                                        <a id="aConnectHotelCancelClose" href="#" class="roomtype-popupconfirm-close"></a>
                                    </div>
                                </div>
                                <div id="Div125" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <div class="chk-l" style="margin-left: 20px; font-size: small;">
                                        Hotel Name &nbsp;&nbsp;&nbsp;</div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="lblConnectHotelName" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtConnectHotelRlineNo" runat="server" style="display:none"></asp:TextBox>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="clear" style="padding-bottom: 10px;">
                                    </div>
                                    <div class="chk-l" style="margin-left: 20px; font-size: small;">
                                        Room Type &nbsp;&nbsp;&nbsp;</div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="lblConnectHotelCancelHeading" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="clear" style="padding-bottom: 10px;">
                                    </div>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <div class="chk-l" style="font-size: 12px; margin-left: 20px; color: #4a90a4;">
                                            Check In &nbsp;
                                        </div>
                                        <div class="chk-r" style="font-size: 12px; padding-left: 20px">
                                            <asp:Label ID="Label107" ReadOnly="true" runat="server"></asp:Label>
                                            <asp:Label ID="lblConnectHotelCheckInDate" runat="server"></asp:Label>                                            
                                        </div>
                                        <div class="chk-l" style="margin-left: 40px; font-size: 12px; color: #4a90a4;">
                                            Check Out &nbsp;&nbsp;
                                        </div>
                                        <div class="chk-r" style="font-size: 12px; padding-left: 10px">
                                            <asp:Label ID="lblConnectHotelCheckOutDate" ReadOnly="true" runat="server"></asp:Label>
                                        </div>                                        
                                        <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                            <ContentTemplate>                                                
                                                <div class="booking-form" id="Div129" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvConnectHotelCancel" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                        <asp:Label ID="lbrLineNo" Text='<%# Eval("rlineno") %>' Visible="false" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Room No.">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblNoOfRooms" Text='<%# Eval("RoomNumber") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Room Occupancy">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRoomOcc" Text='<%# Eval("RoomOccupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblTimeLimit" Text='<%# Eval("timelimit") %>' runat="server"></asp:Label><%-- --%>
                                                                        <asp:HiddenField ID="hdnHotelCancelDate" runat="server" Value='<%# Eval("canceldate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                                                                                                                              
                                                                <asp:TemplateField HeaderText="Cancel Date" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtHotelCancelDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("canceldate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                            <span class="date-icon"></span>                                                                            
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
                                    <div class="clear" style="padding-top: 5px">
                                    </div>
                                    <div class="row-col-12" id="dvCancelPrice" runat="server" visible="false" style="border: 1px solid #fff; width: 383px; height: 25px;
                                        padding-right: 5px; margin-left: 10px; margin-top: 5px;
                                        text-align: center; vertical-align: middle; padding-top: 5px; margin-bottom: 5px;">

                                        <asp:Label ID="lblCancelPrice"  Text="Cancellation Charge " runat="server" style="font-size: 12px; font-weight:bold; color: Red;"></asp:Label>
                                        <asp:TextBox ID="txtCancelPrice" Style="border: none; font-size: 12px; width: 100px; font-weight:bold; color: Red;"
                                        runat="server"> </asp:TextBox>
                                    </div>  
                                    <div class="clear" style="padding-top: 5px">
                                    </div>                                             
                                    <div class="booking-form-i-a" style="margin-left: 380px;">
                                        <asp:Button ID="btnConnectHotelCancelSave" class="authorize-btn" Width="180px" runat="server"
                                            Text="Agree To Cancel" />
                                    </div>                                    
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnltransfercancel" Style="display: none;" runat="server">
                            <div align="left" id="Div70">
                                <div id="Div71" runat="server" style="border: 1px solid #fff; width: 900px; height: 40px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div style="float: left; padding-left: 15px; padding-bottom: 5px; padding-top: 5px;">
                                        <asp:Label ID="Label63" CssClass="room-type-breakup-headings" Text="Cancel Transfers"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div style="float: left; margin-top: -21px;">
                                        <a id="aTrfCancelClose" href="#" class="roomtype-popupconfirm-close"></a>
                                    </div>
                                </div>
                                <div id="Div72" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="lblTransdetails" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="clear" style="padding-bottom: 10px;">
                                    </div>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label66" Text="Transfer Type:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCanTransferType" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 55%; padding-bottom: 10px; color: #455051; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label67" Text="Transfer Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCanlbltransdate" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label68" Text="Vehicle Name:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCanVehicleName" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 55%; padding-bottom: 10px; color: #455051; text-transform: uppercase;
                                                    font-size: 12px;">
                                                    <asp:Label ID="Label69" ForeColor="#4a90a4" Text="Transfer Details:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblcanTransferDetails" runat="server"></asp:Label></div>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 5px; padding-left: 10px; text-transform: uppercase">
                                                    <div id="Div73" runat="server" style="font-size: 12px; padding-left: 10px; display: none;">
                                                        <asp:CheckBox runat="server" class="checkbox" ID="chkcanApplySameTrfConf" />
                                                        <asp:Label ID="Label70" Style="font-size: 12px;" runat="server" Text="Apply Same Confirmation to all"></asp:Label>
                                                    </div>
                                                    <asp:Button ID="Button9" runat="server" Style="display: none;" Text="Filter" />
                                                </div>
                                                <asp:Label ID="lblcanTrfLineNo" Visible="false" runat="server"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <div id="Div74" runat="server" style="border: 1px solid #fff; width: 383px; height: 25px;
                                                    background-color: #ede7e1; padding-right: 5px; margin-left: 510px; margin-top: 15px;
                                                    text-align: center; vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                                    <asp:Label ID="Label73" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                        Text="Cancel Without Charge" runat="server"></asp:Label>
                                                </div>
                                                <div class="booking-form" id="Div75" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvTransferCancel" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                        <asp:Label ID="lbtLineNo" Text='<%# Eval("tlineno") %>' Visible="false" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Transfer Type">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lbltransfertype" Text='<%# Eval("transfertype") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Occupancy">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRoomOcc" Text='<%# Eval("Occupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblTimeLimit" Text='<%# Eval("timelimit") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:CheckBox runat="server" class="checkbox" ID="chkTrfCancel" />
                                                                        <asp:HiddenField ID="hdnTrfCancel" Value='<%# Eval("cancelled") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Transfer Cancellation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvTrfcancelno" runat="server">
                                                                                <asp:TextBox ID="txtTrfcancelno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    Text='<%# Eval("transfercancelno") %>' runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txttrfCancelDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("canceldate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdntrfCancelDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel By">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div id="Div72" class="input" style="border-style: none;" runat="server">
                                                                                <asp:Label ID="lbltrfCancelBy" Text='<%# Eval("cancelby") %>' runat="server"></asp:Label>
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
                                        <asp:Button ID="btntransferCancelSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                    <input id="Button11" runat="server" type="button" value="Cancel" style="visibility: hidden" />
                                    <input id="Text19" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text20" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text21" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlvisacancel" Style="display: none;" runat="server">
                            <div align="left" id="Div76">
                                <div id="Div77" runat="server" style="border: 1px solid #fff; width: 900px; height: 40px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div style="float: left; padding-left: 15px; padding-bottom: 5px; padding-top: 5px;">
                                        <asp:Label ID="Label71" CssClass="room-type-breakup-headings" Text="Cancel Visa"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div style="float: left; margin-top: -21px;">
                                        <a id="aVisaCancelClose" href="#" class="roomtype-popupconfirm-close"></a>
                                    </div>
                                </div>
                                <div id="Div78" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="Label72" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="clear" style="padding-bottom: 10px;">
                                    </div>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 50%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label74" Text="Visa Type:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCanVisaType" ForeColor="#455051" runat="server">
                                                    </asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 50%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label76" Text="Nationality:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblcanNation" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-l" style="width: 50%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <asp:Label ID="Label78" Text="Service Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblvisacandate" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 30%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                </div>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                            <ContentTemplate>
                                                <div id="Div80" runat="server" style="border: 1px solid #fff; width: 383px; height: 25px;
                                                    background-color: #ede7e1; padding-right: 5px; margin-left: 510px; margin-top: 15px;
                                                    text-align: center; vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                                    <asp:Label ID="Label84" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                        Text="Cancel Without Charge" runat="server"></asp:Label>
                                                </div>
                                                <div class="booking-form" id="Div81" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvVisaCancel" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                        <asp:Label ID="lbvLineNo" Text='<%# Eval("vlineno") %>' Visible="false" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Transfer Type">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblvisatype" Text='<%# Eval("visatype") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Occupancy">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblVisaOcc" Text='<%# Eval("Occupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblvisaleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblTimeLimit" Text='<%# Eval("timelimit") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:CheckBox runat="server" class="checkbox" ID="chkVisaCancel" />
                                                                        <asp:HiddenField ID="hdnVisaCancel" Value='<%# Eval("cancelled") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancellation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvTrfcancelno" runat="server">
                                                                                <asp:TextBox ID="txtVisacancelno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    Text='<%# Eval("visacancelno") %>' runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtvisacancelDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("canceldate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnvisaCancelDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel By">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div id="Div72" class="input" style="border-style: none;" runat="server">
                                                                                <asp:Label ID="lblvisaCancelBy" Text='<%# Eval("cancelby") %>' runat="server"></asp:Label>
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
                                        <asp:Button ID="btnvisaCancelSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnltourcancel" Style="display: none;" runat="server">
                            <div align="left" id="Div79">
                                <div id="Div82" runat="server" style="border: 1px solid #fff; width: 900px; height: 40px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div style="float: left; padding-left: 15px; padding-bottom: 5px; padding-top: 5px;">
                                        <asp:Label ID="Label75" CssClass="room-type-breakup-headings" Text="Cancel Tours"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div style="float: left; margin-top: -21px;">
                                        <a id="atourCancelClose" href="#" class="roomtype-popupconfirm-close"></a>
                                    </div>
                                </div>
                                <div id="Div83" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="Label77" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="clear" style="padding-bottom: 10px;">
                                    </div>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 50%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label79" Text="Tour Name:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCantour" ForeColor="#455051" runat="server">
                                                    </asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 50%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label80" Text="Tour Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lbltourcandate" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 30%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                </div>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <div id="Div84" runat="server" style="border: 1px solid #fff; width: 383px; height: 25px;
                                                    background-color: #ede7e1; padding-right: 5px; margin-left: 510px; margin-top: 15px;
                                                    text-align: center; vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                                    <asp:Label ID="Label81" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                        Text="Cancel Without Charge" runat="server"></asp:Label>
                                                </div>
                                                <div class="booking-form" id="Div85" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvtourCancel" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                        <asp:Label ID="lbeLineNo" Text='<%# Eval("elineno") %>' Visible="false" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tour Name">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lbltourname" Text='<%# Eval("exctypname") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Occupancy">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lbltourOcc" Text='<%# Eval("Occupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lbltourleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblTimeLimit" Text='<%# Eval("timelimit") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:CheckBox runat="server" class="checkbox" ID="chktourCancel" />
                                                                        <asp:HiddenField ID="hdntourCancel" Value='<%# Eval("cancelled") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancellation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvtourcancelno" runat="server">
                                                                                <asp:TextBox ID="txttourcancelno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    Text='<%# Eval("tourcancelno") %>' runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txttourcancelDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("canceldate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdntourCancelDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel By">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div id="Div72" class="input" style="border-style: none;" runat="server">
                                                                                <asp:Label ID="lbltourCancelBy" Text='<%# Eval("cancelby") %>' runat="server"></asp:Label>
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
                                        <asp:Button ID="btntourCancelSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlothcancel" Style="display: none;" runat="server">
                            <div align="left" id="Div86">
                                <div id="Div87" runat="server" style="border: 1px solid #fff; width: 900px; height: 40px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div style="float: left; padding-left: 15px; padding-bottom: 5px; padding-top: 5px;">
                                        <asp:Label ID="Label82" CssClass="room-type-breakup-headings" Text="Cancel OtherSevices"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div style="float: left; margin-top: -21px;">
                                        <a id="aothCancelClose" href="#" class="roomtype-popupconfirm-close"></a>
                                    </div>
                                </div>
                                <div id="Div88" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="Label83" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="clear" style="padding-bottom: 10px;">
                                    </div>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 50%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label85" Text="Service Name:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblservice" ForeColor="#455051" runat="server">
                                                    </asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 50%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label86" Text="Service Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblservicecandate" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 30%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                </div>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <div id="Div89" runat="server" style="border: 1px solid #fff; width: 383px; height: 25px;
                                                    background-color: #ede7e1; padding-right: 5px; margin-left: 510px; margin-top: 15px;
                                                    text-align: center; vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                                    <asp:Label ID="Label87" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                        Text="Cancel Without Charge" runat="server"></asp:Label>
                                                </div>
                                                <div class="booking-form" id="Div90" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvotherCancel" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                        <asp:Label ID="lboLineNo" Text='<%# Eval("olineno") %>' Visible="false" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tour Name">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblservicename" Text='<%# Eval("servicename") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Occupancy">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblothOcc" Text='<%# Eval("Occupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblothleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblTimeLimit" Text='<%# Eval("timelimit") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:CheckBox runat="server" class="checkbox" ID="chkothCancel" />
                                                                        <asp:HiddenField ID="hdnothCancel" Value='<%# Eval("cancelled") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancellation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvothcancelno" runat="server">
                                                                                <asp:TextBox ID="txtotherscancelno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    Text='<%# Eval("otherscancelno") %>' runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtothcancelDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("canceldate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnothCancelDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel By">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div id="Div72" class="input" style="border-style: none;" runat="server">
                                                                                <asp:Label ID="lblothCancelBy" Text='<%# Eval("cancelby") %>' runat="server"></asp:Label>
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
                                        <asp:Button ID="btnotherCancelSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlaircancel" Style="display: none;" runat="server">
                            <div align="left" id="Div91">
                                <div id="Div92" runat="server" style="border: 1px solid #fff; width: 900px; height: 40px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div style="float: left; padding-left: 15px; padding-bottom: 5px; padding-top: 5px;">
                                        <asp:Label ID="Label88" CssClass="room-type-breakup-headings" Text="Cancel AirportServices"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div style="float: left; margin-top: -21px;">
                                        <a id="aairCancelClose" href="#" class="roomtype-popupconfirm-close"></a>
                                    </div>
                                </div>
                                <div id="Div93" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="Label89" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="clear" style="padding-bottom: 10px;">
                                    </div>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label90" Text="Service Type:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblcancelirportMateType" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 60%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label91" Text="Service Name:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCancelServiceName" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-l" style="width: 60%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <asp:Label ID="Label92" Text="Service Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCancelAirportMateDate" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 30%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                </div>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                                <div id="Div96" runat="server" style="border: 1px solid #fff; width: 383px; height: 25px;
                                                    background-color: #ede7e1; padding-right: 5px; margin-left: 510px; margin-top: 15px;
                                                    text-align: center; vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                                    <asp:Label ID="Label96" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                        Text="Cancel Without Charge" runat="server"></asp:Label>
                                                </div>
                                                <div class="booking-form" id="Div97" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvairCancel" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                        <asp:Label ID="lbaLineNo" Text='<%# Eval("alineno") %>' Visible="false" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Service Type">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblairservicename" Text='<%# Eval("airportmatype") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Occupancy">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblairOcc" Text='<%# Eval("Occupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblairleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:Label ID="lblairTimeLimit" Text='<%# Eval("timelimit") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel">
                                                                    <ItemTemplate>
                                                                        <div style="padding-top: 5px;">
                                                                        </div>
                                                                        <asp:CheckBox runat="server" class="checkbox" ID="chkairCancel" />
                                                                        <asp:HiddenField ID="hdnairCancel" Value='<%# Eval("cancelled") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancellation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvaircancelno" runat="server">
                                                                                <asp:TextBox ID="txtaircancelno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    Text='<%# Eval("airportmatecancelno") %>' runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtaircancelDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("canceldate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnairCancelDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel By">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div id="Div72" class="input" style="border-style: none;" runat="server">
                                                                                <asp:Label ID="lblairCancelBy" Text='<%# Eval("cancelby") %>' runat="server"></asp:Label>
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
                                        <asp:Button ID="btnairCancelSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="mpTransferConfirm" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aTransferConfirmClose" EnableViewState="true" PopupControlID="pnlTransferConfirm"
                            TargetControlID="hdTransferConfirm">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="hdTransferConfirm" runat="server" />
                        <asp:Panel ID="pnlTransferConfirm" Style="display: none;" runat="server">
                            <div align="left" id="Div40">
                                <div id="Div41" runat="server" style="border: 1px solid #fff; width: 900px; height: 30px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div class="chk-l" style="margin-left: 20px; font-size: small;">
                                        <asp:Label ID="Label32" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                            Text="Confirm Transfer" runat="server"></asp:Label></div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="Label29" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <a id="aTransferConfirmClose" href="#" class="roomtype-popupconfirm-close"></a>
                                </div>
                                <div id="Div42" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <%--        <asp:UpdatePanel ID="upgvconfirmbooking" runat ="server" >
                          <ContentTemplate >--%>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label34" Text="Transfer Type:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCTransferType" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 55%; padding-bottom: 10px; color: #455051; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label35" Text="Transfer Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblClbltransdate" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label36" Text="Vehicle Name:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCVehicleName" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 55%; padding-bottom: 10px; color: #455051; text-transform: uppercase;
                                                    font-size: 12px;">
                                                    <asp:Label ID="Label33" ForeColor="#4a90a4" Text="Transfer Details:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblTransferDetails" runat="server"></asp:Label></div>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 5px; padding-left: 10px; text-transform: uppercase">
                                                    <div id="Div43" runat="server" style="font-size: 12px; padding-left: 10px; display: none;">
                                                        <asp:CheckBox runat="server" class="checkbox" ID="chkApplySameTrfConf" />
                                                        <asp:Label ID="Label37" Style="font-size: 12px;" runat="server" Text="Apply Same Confirmation to all"></asp:Label>
                                                    </div>
                                                    <asp:Button ID="btnApplySameTrfConf" runat="server" Style="display: none;" Text="Filter" />
                                                </div>
                                                <asp:Label ID="lblTrfLineNo" Visible="false" runat="server"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <div id="Div44" runat="server" style="border: 1px solid #fff; width: 383px; height: 25px;
                                                    background-color: #ede7e1; padding-right: 5px; margin-left: 510px; margin-top: 0px;
                                                    text-align: center; vertical-align: middle; padding-top: 5px; padding-bottom: 5px;
                                                    margin-bottom: 0px;">
                                                    <asp:Label ID="Label38" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                        Text="Cancel Without Charge" runat="server"></asp:Label>
                                                </div>
                                                <div class="booking-form" id="Div45" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvTransferConfirm" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" OnRowDataBound="gvTransferConfirmBooking_RowDataBound" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Transfer Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransferType" Text='<%# Eval("transfertype") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Occupancy">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOccupancy" Text='<%# Eval("Occupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Transfer Confirmation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvtransferconfno" runat="server">
                                                                                <asp:TextBox ID="txttransferconfno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Confirm Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtConfirmDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("confirmdate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox><%-- Text='<%# Eval("confirmdate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnConfDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Days">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 40px; float: left; height: 10px;">
                                                                            <div id="dvdays" runat="server" class="input input">
                                                                                <asp:TextBox ID="txtCancelDays" Style="text-align: right; font-size: 12px;" onchange="ChangeDate();"
                                                                                    onkeypress="validateDecimalOnly(event,this)" Width="22px" Text='<%# Eval("CancelDays")%>'
                                                                                    runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit" ControlStyle-Width="115px">
                                                                    <ItemTemplate>
                                                                        <div id="dvtimelimitdate" runat="server" class="input-a" style="width: 115px; padding-bottom: 9px">
                                                                            <asp:TextBox ID="txtTimeLimitDate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                                                runat="server"></asp:TextBox>
                                                                            <%--Text='<%# Eval("TimeLimitDate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdtimelimit" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 60px; float: left; height: 10px; padding-right: 3px">
                                                                            <div id="dvtimelimittime" runat="server" class="input input">
                                                                                <input id="txtTimeLimitTime" placeholder="hh:mm" class="time-picker" style="border: none;
                                                                                    width: 38px; font-size: 12px;" runat="server" />
                                                                                <%-- value='<%# Eval("TimeLimitTime") %>'--%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Prev. Confirmation No." Visible="false" ControlStyle-Width="125px">
                                                                    <ItemTemplate>
                                                                        <div id="PrevConfNo" runat="server" class="booking-form-i-a" style="width: 115px;
                                                                            float: left; height: 10px;">
                                                                            <div class="input input">
                                                                                <asp:TextBox ID="txtPrevConfNo" Style="width: 95px; font-size: 12px;" runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnPrevConfNo" runat="server"></asp:HiddenField>
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
                                        <asp:Button ID="btnTransferConfirmSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                    <input id="Button4" runat="server" type="button" value="Cancel" style="visibility: hidden" />
                                    <input id="Text4" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text5" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text6" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="mpToursConfirm" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aToursConfirmClose" EnableViewState="true" PopupControlID="pnlToursConfirm"
                            TargetControlID="hdToursConfirm">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="hdToursConfirm" runat="server" />
                        <asp:Panel ID="pnlToursConfirm" Style="display: none;" runat="server">
                            <div align="left" id="Div46">
                                <div id="Div47" runat="server" style="border: 1px solid #fff; width: 900px; height: 30px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div class="chk-l" style="margin-left: 20px; font-size: small;">
                                        <asp:Label ID="Label39" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                            Text="Confirm Tours" runat="server"></asp:Label></div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="Label40" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <a id="aToursConfirmClose" href="#" class="roomtype-popupconfirm-close"></a>
                                </div>
                                <div id="Div48" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <%--        <asp:UpdatePanel ID="upgvconfirmbooking" runat ="server" >
                          <ContentTemplate >--%>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 60%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label41" Text="Tours Name:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCToursType" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-l" style="width: 60%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <asp:Label ID="Label42" Text="Tours Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblClblTourdate" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 30%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <div id="Div49" runat="server" style="font-size: 12px; padding-left: 10px; display: none;">
                                                        <asp:CheckBox runat="server" class="checkbox" ID="CheckBox1" />
                                                        <asp:Label ID="Label47" Style="font-size: 12px;" runat="server" Text="Apply Same Confirmation to all"></asp:Label>
                                                    </div>
                                                    <div id="Div50" runat="server" style="border: 1px solid #fff; width: 283px; height: 25px;
                                                        background-color: #ede7e1; padding-right: 15px; margin-left: 10px; margin-top: 0px;
                                                        text-align: center; vertical-align: middle; padding-top: 5px; padding-bottom: 5px;
                                                        padding-left: 15px; margin-bottom: 0px;">
                                                        <asp:Label ID="Label49" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                            Text="Cancel Without Charge" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <%--            <div class="chk-l" style="width:40%; padding-bottom: 10px; padding-left:50px; text-transform: uppercase;font-size: 12px">
                                     <asp:Label ID="Label44" Text="Vehicle Name:" runat="server"></asp:Label>
                                        <asp:Label ID="Label45"  ForeColor="#455051" runat="server"></asp:Label>
                                </div>
                                <div class="chk-r" style="width: 55%; padding-bottom: 10px; color: #455051;text-transform:uppercase;font-size:12px;">
                                    <asp:Label ID="Label46" ForeColor="#4a90a4"  Text="Tours Details:" runat="server"></asp:Label>
                                    <asp:Label ID="lblToursDetails" runat="server"></asp:Label></div>--%>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 5px; padding-left: 10px; text-transform: uppercase">
                                                    <asp:Button ID="btnTourApplySame" runat="server" Style="display: none;" Text="Filter" />
                                                </div>
                                                <asp:Label ID="lblToursLineNo" Visible="false" runat="server"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <div class="booking-form" id="Div51" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvToursConfirm" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" OnRowDataBound="gvToursConfirmBooking_RowDataBound" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tours Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblToursType" Text='<%# Eval("exctypname") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Occupancy">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOccupancy" Text='<%# Eval("Occupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tours Confirmation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvToursconfno" runat="server">
                                                                                <asp:TextBox ID="txtToursconfno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Confirm Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtConfirmDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("confirmdate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox><%-- Text='<%# Eval("confirmdate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnConfDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Days">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 40px; float: left; height: 10px;">
                                                                            <div id="dvdays" runat="server" class="input input">
                                                                                <asp:TextBox ID="txtCancelDays" Style="text-align: right; font-size: 12px;" onchange="ChangeDate();"
                                                                                    onkeypress="validateDecimalOnly(event,this)" Width="22px" Text='<%# Eval("CancelDays")%>'
                                                                                    runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit" ControlStyle-Width="115px">
                                                                    <ItemTemplate>
                                                                        <div id="dvtimelimitdate" runat="server" class="input-a" style="width: 115px; padding-bottom: 9px">
                                                                            <asp:TextBox ID="txtTimeLimitDate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                                                runat="server"></asp:TextBox>
                                                                            <%--Text='<%# Eval("TimeLimitDate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdtimelimit" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 60px; float: left; height: 10px; padding-right: 3px">
                                                                            <div id="dvtimelimittime" runat="server" class="input input">
                                                                                <input id="txtTimeLimitTime" placeholder="hh:mm" class="time-picker" style="border: none;
                                                                                    width: 38px; font-size: 12px;" runat="server" />
                                                                                <%-- value='<%# Eval("TimeLimitTime") %>'--%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Prev. Confirmation No." Visible="false" ControlStyle-Width="125px">
                                                                    <ItemTemplate>
                                                                        <div id="PrevConfNo" runat="server" class="booking-form-i-a" style="width: 115px;
                                                                            float: left; height: 10px;">
                                                                            <div class="input input">
                                                                                <asp:TextBox ID="txtPrevConfNo" Style="width: 95px; font-size: 12px;" runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnPrevConfNo" runat="server"></asp:HiddenField>
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
                                        <asp:Button ID="btnToursConfirmSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                    <input id="Button2" runat="server" type="button" value="Cancel" style="visibility: hidden" />
                                    <input id="Text7" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text8" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text9" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="mpAirportMateConfirm" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aAirportMateConfirmClose" EnableViewState="true" PopupControlID="pnlAirportMateConfirm"
                            TargetControlID="hdAirportMateConfirm">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="hdAirportMateConfirm" runat="server" />
                        <asp:Panel ID="pnlAirportMateConfirm" Style="display: none;" runat="server">
                            <div align="left" id="Div52">
                                <div id="Div53" runat="server" style="border: 1px solid #fff; width: 900px; height: 30px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div class="chk-l" style="margin-left: 20px; font-size: small;">
                                        <asp:Label ID="Label43" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                            Text="Confirm Airport Mate" runat="server"></asp:Label></div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="Label44" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <a id="aAirportMateConfirmClose" href="#" class="roomtype-popupconfirm-close"></a>
                                </div>
                                <div id="Div54" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <%--        <asp:UpdatePanel ID="upgvconfirmbooking" runat ="server" >
                          <ContentTemplate >--%>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label45" Text="AirportMate Type:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCAirportMateType" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 60%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label48" Text="Service Name:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCServiceName" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-l" style="width: 60%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <asp:Label ID="Label46" Text="Airport Mate Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblClblAirportMateDate" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 30%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <div id="Div55" runat="server" style="font-size: 12px; padding-left: 10px; display: none;">
                                                        <asp:CheckBox runat="server" class="checkbox" ID="CheckBox2" />
                                                        <asp:Label ID="Label50" Style="font-size: 12px;" runat="server" Text="Apply Same Confirmation to all"></asp:Label>
                                                    </div>
                                                    <div id="Div56" runat="server" style="border: 1px solid #fff; width: 283px; height: 25px;
                                                        background-color: #ede7e1; padding-right: 15px; margin-left: 10px; margin-top: 0px;
                                                        text-align: center; vertical-align: middle; padding-top: 5px; padding-bottom: 5px;
                                                        padding-left: 15px; margin-bottom: 0px;">
                                                        <asp:Label ID="Label51" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                            Text="Cancel Without Charge" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <%--            <div class="chk-l" style="width:40%; padding-bottom: 10px; padding-left:50px; text-transform: uppercase;font-size: 12px">
                                     <asp:Label ID="Label44" Text="Vehicle Name:" runat="server"></asp:Label>
                                        <asp:Label ID="Label45"  ForeColor="#455051" runat="server"></asp:Label>
                                </div>
                                <div class="chk-r" style="width: 55%; padding-bottom: 10px; color: #455051;text-transform:uppercase;font-size:12px;">
                                    <asp:Label ID="Label46" ForeColor="#4a90a4"  Text="AirportMate Details:" runat="server"></asp:Label>
                                    <asp:Label ID="lblAirportMateDetails" runat="server"></asp:Label></div>--%>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 5px; padding-left: 10px; text-transform: uppercase">
                                                    <asp:Button ID="btnAiportMateApplySame" runat="server" Style="display: none;" Text="Filter" />
                                                </div>
                                                <asp:Label ID="lblAirportMateLineNo" Visible="false" runat="server"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <div class="booking-form" id="Div57" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvAirportMateConfirm" runat="server" AutoGenerateColumns="False"
                                                            CssClass="mygrid" Width="100%" OnRowDataBound="gvAirportMateConfirmBooking_RowDataBound"
                                                            GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Airport Mate Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAirportMateType" Text='<%# Eval("airportmatype") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Occupancy">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOccupancy" Text='<%# Eval("Occupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AirportMate Confirmation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvAirportMateconfno" runat="server">
                                                                                <asp:TextBox ID="txtAirportMateconfno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Confirm Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtConfirmDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("confirmdate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox><%-- Text='<%# Eval("confirmdate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnConfDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Days">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 40px; float: left; height: 10px;">
                                                                            <div id="dvdays" runat="server" class="input input">
                                                                                <asp:TextBox ID="txtCancelDays" Style="text-align: right; font-size: 12px;" onchange="ChangeDate();"
                                                                                    onkeypress="validateDecimalOnly(event,this)" Width="22px" Text='<%# Eval("CancelDays")%>'
                                                                                    runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit" ControlStyle-Width="115px">
                                                                    <ItemTemplate>
                                                                        <div id="dvtimelimitdate" runat="server" class="input-a" style="width: 115px; padding-bottom: 9px">
                                                                            <asp:TextBox ID="txtTimeLimitDate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                                                runat="server"></asp:TextBox>
                                                                            <%--Text='<%# Eval("TimeLimitDate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdtimelimit" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 60px; float: left; height: 10px; padding-right: 3px">
                                                                            <div id="dvtimelimittime" runat="server" class="input input">
                                                                                <input id="txtTimeLimitTime" placeholder="hh:mm" class="time-picker" style="border: none;
                                                                                    width: 38px; font-size: 12px;" runat="server" />
                                                                                <%-- value='<%# Eval("TimeLimitTime") %>'--%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Prev. Confirmation No." Visible="false" ControlStyle-Width="125px">
                                                                    <ItemTemplate>
                                                                        <div id="PrevConfNo" runat="server" class="booking-form-i-a" style="width: 115px;
                                                                            float: left; height: 10px;">
                                                                            <div class="input input">
                                                                                <asp:TextBox ID="txtPrevConfNo" Style="width: 95px; font-size: 12px;" runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnPrevConfNo" runat="server"></asp:HiddenField>
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
                                        <asp:Button ID="btnAirportMateConfirmSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                    <input id="Button5" runat="server" type="button" value="Cancel" style="visibility: hidden" />
                                    <input id="Text10" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text11" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text12" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="mpOthersConfirm" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aOthersConfirmClose" EnableViewState="true" PopupControlID="pnlOthersConfirm"
                            TargetControlID="hdOthersConfirm">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="hdOthersConfirm" runat="server" />
                        <asp:Panel ID="pnlOthersConfirm" Style="display: none;" runat="server">
                            <div align="left" id="Div58">
                                <div id="Div59" runat="server" style="border: 1px solid #fff; width: 900px; height: 30px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div class="chk-l" style="margin-left: 20px; font-size: small;">
                                        <asp:Label ID="Label52" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                            Text="Confirm Other Service" runat="server"></asp:Label></div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="Label53" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <a id="aOthersConfirmClose" href="#" class="roomtype-popupconfirm-close"></a>
                                </div>
                                <div id="Div60" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <%--        <asp:UpdatePanel ID="upgvconfirmbooking" runat ="server" >
                          <ContentTemplate >--%>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label54" Text="Others Name:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCOthersType" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 60%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label5" Text="Service Name:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCOtherServicename" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-l" style="width: 60%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <asp:Label ID="Label55" Text="Service Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblClblOtherdate" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 30%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <div id="Div61" runat="server" style="font-size: 12px; padding-left: 10px; display: none;">
                                                        <asp:CheckBox runat="server" class="checkbox" ID="CheckBox3" />
                                                        <asp:Label ID="Label56" Style="font-size: 12px;" runat="server" Text="Apply Same Confirmation to all"></asp:Label>
                                                    </div>
                                                    <div id="Div62" runat="server" style="border: 1px solid #fff; width: 283px; height: 25px;
                                                        background-color: #ede7e1; padding-right: 15px; margin-left: 10px; margin-top: 0px;
                                                        text-align: center; vertical-align: middle; padding-top: 5px; padding-bottom: 5px;
                                                        padding-left: 15px; margin-bottom: 0px;">
                                                        <asp:Label ID="Label57" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                            Text="Cancel Without Charge" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <%--            <div class="chk-l" style="width:40%; padding-bottom: 10px; padding-left:50px; text-transform: uppercase;font-size: 12px">
                                     <asp:Label ID="Label44" Text="Vehicle Name:" runat="server"></asp:Label>
                                        <asp:Label ID="Label45"  ForeColor="#455051" runat="server"></asp:Label>
                                </div>
                                <div class="chk-r" style="width: 55%; padding-bottom: 10px; color: #455051;text-transform:uppercase;font-size:12px;">
                                    <asp:Label ID="Label46" ForeColor="#4a90a4"  Text="Others Details:" runat="server"></asp:Label>
                                    <asp:Label ID="lblOthersDetails" runat="server"></asp:Label></div>--%>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 5px; padding-left: 10px; text-transform: uppercase">
                                                    <asp:Button ID="Button1" runat="server" Style="display: none;" Text="Filter" />
                                                </div>
                                                <asp:Label ID="lblOthersLineNo" Visible="false" runat="server"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                            <ContentTemplate>
                                                <div class="booking-form" id="Div63" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvOthersConfirm" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" OnRowDataBound="gvOthersConfirmBooking_RowDataBound" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Service Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOthersType" Text='<%# Eval("othtypname") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Occupancy">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOccupancy" Text='<%# Eval("Occupancy") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Others Confirmation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvOthersconfno" runat="server">
                                                                                <asp:TextBox ID="txtOthersconfno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Confirm Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtConfirmDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("confirmdate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox><%-- Text='<%# Eval("confirmdate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnConfDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Days">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 40px; float: left; height: 10px;">
                                                                            <div id="dvdays" runat="server" class="input input">
                                                                                <asp:TextBox ID="txtCancelDays" Style="text-align: right; font-size: 12px;" onchange="ChangeDate();"
                                                                                    onkeypress="validateDecimalOnly(event,this)" Width="22px" Text='<%# Eval("CancelDays")%>'
                                                                                    runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit" ControlStyle-Width="115px">
                                                                    <ItemTemplate>
                                                                        <div id="dvtimelimitdate" runat="server" class="input-a" style="width: 115px; padding-bottom: 9px">
                                                                            <asp:TextBox ID="txtTimeLimitDate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                                                runat="server"></asp:TextBox>
                                                                            <%--Text='<%# Eval("TimeLimitDate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdtimelimit" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 60px; float: left; height: 10px; padding-right: 3px">
                                                                            <div id="dvtimelimittime" runat="server" class="input input">
                                                                                <input id="txtTimeLimitTime" placeholder="hh:mm" class="time-picker" style="border: none;
                                                                                    width: 38px; font-size: 12px;" runat="server" />
                                                                                <%-- value='<%# Eval("TimeLimitTime") %>'--%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Prev. Confirmation No." Visible="false" ControlStyle-Width="125px">
                                                                    <ItemTemplate>
                                                                        <div id="PrevConfNo" runat="server" class="booking-form-i-a" style="width: 115px;
                                                                            float: left; height: 10px;">
                                                                            <div class="input input">
                                                                                <asp:TextBox ID="txtPrevConfNo" Style="width: 95px; font-size: 12px;" runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnPrevConfNo" runat="server"></asp:HiddenField>
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
                                        <asp:Button ID="btnOthersConfirmSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                    <input id="Button6" runat="server" type="button" value="Cancel" style="visibility: hidden" />
                                    <input id="Text13" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text14" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text15" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="mpVisaConfirm" runat="server" BackgroundCssClass="roomtype-modalBackground"
                            CancelControlID="aVisaConfirmClose" EnableViewState="true" PopupControlID="pnlVisaConfirm"
                            TargetControlID="hdVisaConfirm">
                        </asp:ModalPopupExtender>
                        <asp:HiddenField ID="hdVisaConfirm" runat="server" />
                        <asp:Panel ID="pnlVisaConfirm" Style="display: none;" runat="server">
                            <div align="left" id="Div64">
                                <div id="Div65" runat="server" style="border: 1px solid #fff; width: 900px; height: 30px;
                                    background-color: #ede7e1; padding-right: 10px; margin-right: 10px; text-align: center;
                                    vertical-align: middle; padding-top: 5px; margin-bottom: 0px;">
                                    <div class="chk-l" style="margin-left: 20px; font-size: small;">
                                        <asp:Label ID="Label58" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                            Text="Confirm Visa" runat="server"></asp:Label></div>
                                    <div class="chk-r" style="font-size: 12px;">
                                        <asp:Label ID="Label59" runat="server"></asp:Label>
                                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <a id="aVisaConfirmClose" href="#" class="roomtype-popupconfirm-close"></a>
                                </div>
                                <div id="Div66" runat="server" class="roomtype-price-breakuppopup" style="float: left;
                                    width: 900px; text-align: left; max-height: 1000px; overflow: auto">
                                    <div class="clear" style="padding-top: 20px">
                                    </div>
                                    <%--        <asp:UpdatePanel ID="upgvconfirmbooking" runat ="server" >
                          <ContentTemplate >--%>
                                    <div class="page-search-content-Additional-Service">
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server" EnableViewState="true">
                                            <ContentTemplate>
                                                <div class="chk-l" style="width: 50%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label60" Text="Visa Type:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCVisaType" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 50%; padding-bottom: 10px; padding-left: 50px; text-transform: uppercase;
                                                    font-size: 12px">
                                                    <asp:Label ID="Label61" Text="Nationality:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCVisaNationality" ForeColor="#455051" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-l" style="width: 50%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <asp:Label ID="Label62" Text="Service Date:" ForeColor="#4a90a4" runat="server"></asp:Label>
                                                    <asp:Label ID="lblClblVisaDate" runat="server"></asp:Label>
                                                </div>
                                                <div class="chk-r" style="width: 30%; padding-bottom: 10px; padding-left: 50px; color: #455051;
                                                    text-transform: uppercase; font-size: 12px">
                                                    <div id="Div67" runat="server" style="font-size: 12px; padding-left: 10px; display: none;">
                                                        <asp:CheckBox runat="server" class="checkbox" ID="CheckBox4" />
                                                        <asp:Label ID="Label64" Style="font-size: 12px;" runat="server" Text="Apply Same Confirmation to all"></asp:Label>
                                                    </div>
                                                    <div id="Div68" runat="server" style="border: 1px solid #fff; width: 283px; height: 25px;
                                                        background-color: #ede7e1; padding-right: 15px; margin-left: 10px; margin-top: 0px;
                                                        text-align: center; vertical-align: middle; padding-top: 5px; padding-bottom: 5px;
                                                        padding-left: 15px; margin-bottom: 0px;">
                                                        <asp:Label ID="Label65" Style="vertical-align: center !important" CssClass="room-type-breakup-headings"
                                                            Text="Cancel Without Charge" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="clear" style="padding: 5px;">
                                                </div>
                                                <div class="chk-l" style="width: 40%; padding-bottom: 5px; padding-left: 10px; text-transform: uppercase">
                                                    <asp:Button ID="Button7" runat="server" Style="display: none;" Text="Filter" />
                                                </div>
                                                <asp:Label ID="lblVisaLineNo" Visible="false" runat="server"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <div class="booking-form" id="Div69" runat="server" style="background-color: White;
                                                    padding: 5px;">
                                                    <div class="row-col-12" style="padding-left: 10px;">
                                                        <asp:GridView ID="gvVisaConfirm" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                            Width="100%" OnRowDataBound="gvVisaConfirmBooking_RowDataBound" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RowNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNo" Text='  <%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Visa Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVisaType" Text='<%# Eval("visatype") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No of Visas">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOccupancy" Text='<%# Eval("noofvisas") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lead Guest">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblleadguest" Text='<%# Eval("LeadGuest") %>' runat="server"></asp:Label><%-- --%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Visa Confirmation No.">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 120px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvVisaconfno" runat="server">
                                                                                <asp:TextBox ID="txtVisaconfno" Style="border: none; font-size: 12px; width: 100px;"
                                                                                    runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Confirm Date">
                                                                    <ItemTemplate>
                                                                        <div class="input-a" style="width: 110px; padding-bottom: 9px;">
                                                                            <asp:TextBox ID="txtConfirmDate" Style="border: none;" class="date-inpt-check-in"
                                                                                Text='<%# Eval("confirmdate") %>' placeholder="dd/mm/yyyy" runat="server"></asp:TextBox><%-- Text='<%# 

Eval("confirmdate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdnConfDate" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel Days">
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 40px; float: left; height: 10px;">
                                                                            <div id="dvdays" runat="server" class="input input">
                                                                                <asp:TextBox ID="txtCancelDays" Style="text-align: right; font-size: 12px;" onchange="ChangeDate();"
                                                                                    onkeypress="validateDecimalOnly(event,this)" Width="22px" Text='<%# Eval("CancelDays")%>'
                                                                                    runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time Limit" ControlStyle-Width="115px">
                                                                    <ItemTemplate>
                                                                        <div id="dvtimelimitdate" runat="server" class="input-a" style="width: 115px; padding-bottom: 9px">
                                                                            <asp:TextBox ID="txtTimeLimitDate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                                                runat="server"></asp:TextBox>
                                                                            <%--Text='<%# Eval("TimeLimitDate") %>'--%>
                                                                            <span class="date-icon"></span>
                                                                            <asp:HiddenField ID="hdtimelimit" runat="server" />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 60px; float: left; height: 10px; padding-right: 3px">
                                                                            <div id="dvtimelimittime" runat="server" class="input input">
                                                                                <input id="txtTimeLimitTime" placeholder="hh:mm" class="time-picker" style="border: none;
                                                                                    width: 38px; font-size: 12px;" runat="server" />
                                                                                <%-- value='<%# Eval("TimeLimitTime") %>'--%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Prev. Confirmation No." Visible="false" ControlStyle-Width="125px">
                                                                    <ItemTemplate>
                                                                        <div id="PrevConfNo" runat="server" class="booking-form-i-a" style="width: 115px;
                                                                            float: left; height: 10px;">
                                                                            <div class="input input">
                                                                                <asp:TextBox ID="txtPrevConfNo" Style="width: 95px; font-size: 12px;" runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnPrevConfNo" runat="server"></asp:HiddenField>
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
                                        <asp:Button ID="btnVisaConfirmSave" class="authorize-btn" Width="100px" runat="server"
                                            Text="Save" />
                                    </div>
                                    <input id="Button8" runat="server" type="button" value="Cancel" style="visibility: hidden" />
                                    <input id="Text16" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text17" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                    <input id="Text18" runat="server" type="text" style="width: 0px; height: 0px; visibility: hidden;" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="sp-page-r">
                            <div style="width: 100%" runat="server" id="dvSummaryPart">
                                <div style="float: left;">
                                    <input id="btnTabShowAll" onclick="btnTabShowAllClick(this)" type="button" class="guest-addrow-generate"
                                        value="Show All" />
                                </div>
                                <div style="float: right;">
                                    <asp:Label ID="lblEditRequestId1" CssClass="additional-service-heading-label" Text="REQUEST ID: "
                                        runat="server"></asp:Label>
                                    <asp:Label ID="lblEditRequestId" CssClass="additional-service-heading-label" runat="server"></asp:Label></div>
                                <div class="clear">
                                </div>
                                <div class="ui-widget collapse" id="dvTabHotelSummary" runat="server">
                                    <div class="ui-widget-header">
                                        <div style="float: left; margin: 5px; width: 40%;">
                                            <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                                                style="padding-top: 5px;">ACCOMMODATION</span>                                              
                                        </div>
                                        <div style="float: left; margin: 5px; padding-left: 40px; padding-top: 5px;">
                                            <span style="font-size: 12px !important;">
                                                <asp:Label ID="lblTabHotelTotalPriceText" Text="TOTAL PRICE:" runat="server"></asp:Label></span>
                                        </div>
                                        <div style="float: right; margin: 5px;">
                                            <span style="float: right; padding-right: 15px; padding-top: 5px; font-size: 12px !important;">
                                                <asp:Label ID="lblTabHotelTotalPrice" runat="server"></asp:Label></span>
                                        </div>
                                        <div class="ui-helper-clearfix">
                                        </div>
                                    </div>
                                    <div class="ui-widget-content" id="dvHotelTab1" style="display: none;">
                                        <asp:DataList ID="dlBookingSummary" Width="100%" runat="server">
                                            <ItemTemplate>
                                                <div class="checkout-coll">
                                                    <div class="checkout-head">
                                                        <div class="checkout-headrb">
                                                            <div class="checkout-headrp">
                                                                <div class="chk-left">
                                                                 <div id="dvBookingMode" runat="server" class="guest-summary-heading"> <%--Modified by abin on 20180728--%>
                                                                    <asp:Label ID="lblBookingMode" Text="Free Form Booking" CssClass="additional-service-heading-label" runat="server"></asp:Label>
                                                                 <asp:HiddenField ID="hdBookingMode" Value='<%# Eval("bookingmode") %>' runat="server" />
                                                                 </div>
                                                                    <div class="guest-summary-heading">
                                                                        <asp:Label ID="lblHotelName" Text='<%# Eval("partyname") %>' runat="server"></asp:Label>
                                                                        <asp:HiddenField runat="server" Value='<%# Eval("cancelled") %>' ID="hdCancelled" />
                                                                        <asp:HiddenField runat="server" Value='<%# Eval("RatePlanSource") %>' ID="hdRatePlanSource" />
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
                                                                    <%-- <asp:Label ID="lblPersons" Text='<%# Eval("Persons") %>'  visible="false"  runat="server"></asp:Label>--%>
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
                                                        <div id="dvRoomValue" runat="server" class="line-bottom" style="padding: 5px 0px 5px 0px !important;">
                                                            <div class="chk-total-l" style="width: 30%">
                                                                Room Value</div>
                                                            <div class="chk-total-r" style="width: 30%">
                                                                <asp:Label ID="lblTotalPrice" Text='<%# Eval("totalPrice") %>' runat="server"></asp:Label></div>
                                                            <div class="clear">
                                                            </div>
                                                        </div>
                                                        <div id="dvSplEvents" runat="server" class="chk-detais-row">
                                                            <div class="clear" style="padding-bottom: 15px;">
                                                            </div>
                                                            <h2>
                                                                Special Events Details</h2>
                                                            <div class="chk-line1">
                                                                <asp:DataList ID="dlSpecialEventsSummary" OnItemDataBound="dlSpecialEventsSummary_ItemDataBound"
                                                                    Width="100%" runat="server">
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
                                                                                <div class="chk-r" style="width: 70%">
                                                                                    <asp:Label ID="lblsplEventName" Text='<%# Eval("spleventname") %>' runat="server"></asp:Label></div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                            <div id="dvSplEventValue" runat="server" class="chk-line">
                                                                                <div class="chk-l" style="width: 30%">
                                                                                    Event Value &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                                <div class="chk-r" style="width: 70%">
                                                                                    <asp:Label ID="lblsplEventValue" Text='<%# Eval("spleventvalue") %>' runat="server"></asp:Label></div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        </div>

                                                        <div id="dvOneTimePay" runat="server" >
                                                            <div style="padding-bottom:35px;">
                                                            <div style="float: left; margin-right: 10px;">
                                                               
                                                                <asp:Label ID="lblFunctionType" class="chk-l" runat="server"></asp:Label>
                                                            </div>
                                                            <div style="float: left;">
                                                                 <asp:Label ID="lblExtraBedPrice" style="color:#ff7200;font-size:small" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        </div>
                                                        <div class="clear"></div>

                                                        <div class="chk-total" id="dvTourTotal" runat="server">
                                                            <div class="chk-total-l">
                                                                Total Price</div>
                                                            <div class="chk-total-r">
                                                                <asp:Label ID="lblTotalPricewithSE" Text='<%# Eval("TotalPriceWithSE") %>' runat="server"></asp:Label></div>
                                                            <div class="clear">
                                                            </div>
                                                        </div>
                                                        <div id="dvHotelCancelled" runat="server" class="chk-line" style="padding-bottom: 15px;
                                                            padding-top: 10px; margin-top: 5px;">
                                                            <div style="background-color: #F2F3F4; text-align: center; padding: 5px;">
                                                                <asp:Label ID="Label25" CssClass="summary-cancelled-label" Text="Booking Status: Cancelled"
                                                                    runat="server"></asp:Label></div>
                                                        </div>
                                                        <div id="dvHotelAvailability" runat="server" class="chk-line" style="padding-bottom:3px;
                                                            padding-top: 3px; margin-top: 3px;" visible="false">
                                                            <div style="background-color: #F2F3F4; text-align: center; padding: 3px;">
                                                                <asp:Label ID="lblHotelAvailability" CssClass="summary-cancelled-label" Text='<%# Eval("errorDescription") %>'
                                                                    runat="server"></asp:Label>
                                                                    <asp:Label ID="lblConfirmStatus" CssClass="summary-cancelled-label" Text='<%# Eval("confirmStatus") %>'
                                                                    runat="server" style="display:none"></asp:Label>
                                                                    </div>
                                                        </div>
                                                        <div id="dvHotelButtons" runat="server" class="chk-line" style="padding-bottom: 45px;
                                                            padding-top: 10px; margin-top: 5px;">
                                                            <div style="float: left;">
                                                                <div style="float: left; padding-right: 10px; padding-top: 5px;">
                                                                    <div id="dvHotelAmend" runat="server" style="float: left; padding-right: 5px; padding-top: 5px;">
                                                                        <asp:ImageButton ID="imgbEdit" ImageUrl="~/img/button_edit.png" ToolTip="Amend" OnClick="imgbAmend_Click"
                                                                            runat="server" /></div>
                                                                    <div id="dvHotelDelete" runat="server" style="float: left; padding-top: 5px; padding-right: 5px;">
                                                                        <asp:ImageButton ID="imgbDelete" ImageUrl="~/img/button_remove.png" OnClick="imgbDelete_Click"
                                                                            OnClientClick="return confirmHotelDelete();" ToolTip="Remove" runat="server" />
                                                                    </div>
                                                                    <div id="dvHotelRemarks" runat="server" style="float: left; padding-top: 5px; padding-right: 5px;">
                                                                        <asp:ImageButton ID="imgbRemarks" ImageUrl="~/img/button_remarks.png" ToolTip="Remarks"
                                                                            runat="server" OnClick="imgbRemarks_Click" /></div>
                                                                    <div id="dvHotelConfirm" runat="server" style="float: left; padding-top: 5px;">
                                                                        <asp:ImageButton ID="imgbConfirm" ImageUrl="~/img/button_confirm.png" ToolTip="Confirm"
                                                                            runat="server" OnClick="imgbConfirm_Click" /></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                                <div class="ui-widget collapse" id="dvTabTourSummary" runat="server">
                                    <div class="ui-widget-header">
                                        <div style="float: left; margin: 5px; width: 40%;">
                                            <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                                                style="padding-top: 15px;">TOURS</span>
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
                                    <div class="ui-widget-content" id="dvTourTab1" style="display: none;">
                                        <div id="dvTourSummarry" runat="server" style="width: 100%">
                                            <div class="checkout-coll">
                                                <%--  <div class="checkout-head">
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
                                </div>--%>
                                                <div>
                                                    <div class="clear">
                                                    </div>
                                                    <div class="chk-details" style="margin-top: -15px;">
                                                        <asp:DataList ID="dlTourSummary" Width="100%" runat="server">
                                                            <ItemTemplate>
                                                              <div id="dvBookingMode" runat="server" class="guest-summary-heading">
                                                                    <asp:Label ID="lblBookingMode" Text="Free Form Booking" CssClass="additional-service-heading-label" runat="server"></asp:Label>
                                                                 <asp:HiddenField ID="hdBookingMode" Value='<%# Eval("bookingmode") %>' runat="server" />
                                                                 </div>
                                                                <div class="chk-detais-row">
                                                                    <div id="dvPicupLocGroup" runat="server" class="line-bottom" style="background-color: #F2F2F2;
                                                                        margin-left: -24px; padding-left: 25px; width: 119%; font-weight: 600;">
                                                                        <div class="chk-l" style="width: 40%; padding-bottom: 5px;">
                                                                            TOUR - PICK UP LOCATION &nbsp;&nbsp;</div>
                                                                        <div class="chk-r" style="width: 50%; padding-bottom: 5px;">
                                                                            <asp:Label ID="Label93" Text='<%# Eval("sectorgroupname") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblsectorgroupcode" Style="display: none;" Text='<%# Eval("sectorgroupcode") %>'
                                                                                runat="server"></asp:Label>
                                                                            <div id="divtouredit" runat="server" style="float: right; padding-right: 3px; padding-top: 0px;">
                                                                                <asp:ImageButton ID="imgbEdit" ImageUrl="~/img/button_amend.png" ToolTip="Amend"
                                                                                    OnClick="imgTourAmend_Click" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="clear">
                                                                        </div>
                                                                    </div>
                                                                    <div class="line-bottom">
                                                                        <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                            Excursion Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                        <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                            <asp:Label ID="lblExcursionCode" Style="display: none;" Text='<%# Eval("exctypcode") %>'
                                                                                runat="server"></asp:Label>
                                                                            <asp:Label ID="lblCombo" Style="display: none;" Text='<%# Eval("combo") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblmultipleDates" Style="display: none;" Text='<%# Eval("multipledatesyesno") %>'
                                                                                runat="server"></asp:Label>
                                                                            <asp:Label ID="lblRequestId" Style="display: none;" Text='<%# Eval("RequestId") %>'
                                                                                runat="server"></asp:Label>
                                                                            <asp:Label ID="lblExcursionName" Text='<%# Eval("exctypname") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblelineno" Visible="false" Text='<%# Eval("elineno") %>' runat="server"></asp:Label>
                                                                            <asp:HiddenField runat="server" Value='<%# Eval("cancelled") %>' ID="hdtourCancelled" />
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
                                                                        <div id="dvtourCancelled" runat="server" class="chk-line" style="padding-bottom: 15px;
                                                                            padding-top: 10px; margin-top: 5px;">
                                                                            <div style="background-color: #F2F3F4; text-align: center; padding: 5px;">
                                                                                <asp:Label ID="Label25" CssClass="summary-cancelled-label" Text="Tour Status: Cancelled"
                                                                                    runat="server"></asp:Label></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div id="dvTourButtons" runat="server" class="chk-line" style="padding-bottom: 45px;
                                                                    padding-top: 10px; margin-top: 5px;">
                                                                    <div style="float: left;">
                                                                        <div style="float: left; padding-right: 5px; padding-top: 5px;">
                                                                            <div id="divtourremove" runat="server" style="float: left; padding-top: 5px; padding-right: 10px;">
                                                                                <asp:ImageButton ID="imgbDelete" ImageUrl="~/img/button_remove.png" OnClick="imgTourRemove_Click"
                                                                                    ToolTip="Remove" runat="server" OnClientClick="return confirmDelete();" />
                                                                            </div>
                                                                            <div id="divtourcancel" runat="server" style="float: left; padding-top: 5px; padding-right: 10px">
                                                                                <asp:ImageButton ID="imgtourcancel" ImageUrl="~/img/button_cancel.png" OnClick="imgtourcancel_Click"
                                                                                    ToolTip="Cancel" runat="server" />
                                                                            </div>
                                                                            <div id="divtourremarks" runat="server" style="float: left; padding-right: 5px; padding-top: 5px;">
                                                                                <asp:ImageButton ID="ImgToursRemarks" ImageUrl="~/img/button_remarks.png" ToolTip="Remarks"
                                                                                    runat="server" OnClick="imgToursRemarks_Click" />
                                                                            </div>
                                                                            <div id="dvTourConfirm" runat="server" style="float: left; padding-top: 5px;">
                                                                                <asp:ImageButton ID="imgbTourConfirm" ImageUrl="~/img/button_confirm.png" ToolTip="Confirm"
                                                                                    runat="server" OnClick="imgbTourConfirm_Click" /></div>
                                                                        </div>
                                                                        <div style="float: left;" id="dvAmend" runat="server">
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
                                        <div style="float: left; margin: 5px; width: 40%;">
                                            <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                                                style="padding-top: 15px;">AIRPORT SERVICE</span>
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
                                    <div class="ui-widget-content" id="dvAirportTab1" style="display: none;">
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
                                                                     <div id="dvBookingMode" runat="server" class="guest-summary-heading">
                                                                    <asp:Label ID="lblBookingMode" Text="Free Form Booking" CssClass="additional-service-heading-label" runat="server"></asp:Label>
                                                                 <asp:HiddenField ID="hdBookingMode" Value='<%# Eval("bookingmode") %>' runat="server" />
                                                                 </div>

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
                                                                            <asp:Label ID="lblalineno" Visible="false" Text='<%# Eval("alineno") %>' runat="server"></asp:Label>
                                                                            <asp:HiddenField runat="server" Value='<%# Eval("cancelled") %>' ID="hdairCancelled" />
                                                                        </div>
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
                                                                        </div>   </div>
                                                                        <div id="dvairportCancelled" runat="server" class="chk-line" style="padding-bottom: 15px;
                                                                            padding-top: 10px; margin-top: 5px;">
                                                                            <div style="background-color: #F2F3F4; text-align: center; padding: 5px;">
                                                                                <asp:Label ID="Label25" CssClass="summary-cancelled-label" Text="AirportService Status: Cancelled"
                                                                                    runat="server"></asp:Label></div>
                                                                        </div>
                                                                        <div class="clear">
                                                                        </div>
                                                                 
                                                                    <div class="chk-line" id="dvAMbutton" runat="server" style="padding-bottom: 45px;
                                                                        padding-top: 10px; margin-top: 5px;">
                                                                        <div style="float: left;">
                                                                            <div id="divairedit" runat="server" style="float: left; padding-right: 10px; padding-top: 5px;">
                                                                                <asp:ImageButton ID="imgAirEdit" ImageUrl="~/img/button_amend.png" ToolTip="Amend"
                                                                                    OnClick="imgAirEdit_Click" runat="server" />
                                                                            </div>
                                                                            <div id="divAirremove" runat="server" style="float: left; padding-top: 5px; padding-right: 10px;">
                                                                                <asp:ImageButton ID="imgAirDelete" ImageUrl="~/img/button_remove.png" OnClick="imgAirDelete_Click"
                                                                                    ToolTip="Remove" runat="server" OnClientClick="return confirmDelete();" />
                                                                            </div>
                                                                            <div id="divAircancel" runat="server" style="float: left; padding-top: 5px; padding-right: 10px">
                                                                                <asp:ImageButton ID="imgAircancel" ImageUrl="~/img/button_cancel.png" ToolTip="Cancel"
                                                                                    OnClick="imgAircancel_Click" runat="server" />
                                                                            </div>
                                                                            <div id="divairremarks" runat="server" style="float: left; padding-right: 10px; padding-top: 5px;">
                                                                                <asp:ImageButton ID="ImgAirportmaRemarks" ImageUrl="~/img/button_remarks.png" ToolTip="Remarks"
                                                                                    runat="server" OnClick="ImgAirportmaRemarks_click" />
                                                                            </div>
                                                                            <div id="dvAirportmateConfirm" runat="server" style="float: left; padding-top: 5px;">
                                                                                <asp:ImageButton ID="imgbAirportmateConfirm" ImageUrl="~/img/button_confirm.png"
                                                                                    ToolTip="Confirm" runat="server" OnClick="imgbAirportmateConfirm_Click" /></div>
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
                                                style="padding-top: 15px;">TRANSFERS</span>
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
                                    <div class="ui-widget-content" id="dvTransferTab1" style="display: none;">
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
                                                                 <div id="dvBookingMode" runat="server" class="guest-summary-heading">
                                                                    <asp:Label ID="lblBookingMode" Text="Free Form Booking" CssClass="additional-service-heading-label" runat="server"></asp:Label>
                                                                 <asp:HiddenField ID="hdBookingMode" Value='<%# Eval("bookingmode") %>' runat="server" />
                                                                 </div>

                                                                    <div class="line-bottom">
                                                                        <div class="chk-l" style="width: 35%; padding-bottom: 5px;">
                                                                            Transfer Type &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                                        <div class="chk-r" style="width: 65%; padding-bottom: 5px;">
                                                                            <asp:Label ID="lblExcursionName" Text='<%# Eval("transfertype") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbltlineno" Visible="false" Text='<%# Eval("tlineno") %>' runat="server"></asp:Label>
                                                                            <asp:HiddenField runat="server" Value='<%# Eval("cancelled") %>' ID="hdtrfCancelled" />
                                                                        </div>
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
                                                                            <asp:Label ID="lblTransferDetails" Text='<%# Eval("transferdetail") %>' runat="server"></asp:Label></div>
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
                                                                        <div id="dvtransferCancelled" runat="server" class="chk-line" style="padding-bottom: 15px;
                                                                            padding-top: 10px; margin-top: 5px;">
                                                                            <div style="background-color: #F2F3F4; text-align: center; padding: 5px;">
                                                                                <asp:Label ID="Label25" CssClass="summary-cancelled-label" Text="Transfer Status: Cancelled"
                                                                                    runat="server"></asp:Label></div>
                                                                        </div>
                                                                        <div id="dvTrfBuuton" runat="server" class="chk-line" style="padding-bottom: 45px;
                                                                            padding-top: 10px; margin-top: 5px;">
                                                                            <div style="float: left;">
                                                                                <div id="divtrfedit" runat="server" style="float: left; padding-right: 10px; padding-top: 5px;">
                                                                                    <asp:ImageButton ID="imgTrfEdit" ImageUrl="~/img/button_amend.png" ToolTip="Amend"
                                                                                        OnClick="imgTrfEdit_Click" runat="server" />
                                                                                </div>
                                                                                <div id="divTrfremove" runat="server" style="float: left; padding-top: 5px; padding-right: 5px;">
                                                                                    <asp:ImageButton ID="imgTrfDelete" ImageUrl="~/img/button_remove.png" OnClick="imgTrfDelete_Click"
                                                                                        ToolTip="Remove" runat="server" OnClientClick="return confirmDelete();" />
                                                                                </div>
                                                                                <div id="divTrfcancel" runat="server" style="float: left; padding-top: 5px; padding-right: 5px">
                                                                                    <asp:ImageButton ID="imgTrfcancel" ImageUrl="~/img/button_cancel.png" OnClick="imgTrfcancel_Click"
                                                                                        ToolTip="Cancel" runat="server" />
                                                                                </div>
                                                                                <div id="divtrfremarks" runat="server" style="float: left; padding-right: 10px; padding-top: 5px;">
                                                                                    <asp:ImageButton ID="ImgtrfsRemarks" ImageUrl="~/img/button_remarks.png" ToolTip="Remarks"
                                                                                        runat="server" OnClick="ImgtrfsRemarks_Click" />
                                                                                </div>
                                                                                <div id="divTrfConfirm" runat="server" style="float: left; padding-top: 5px; padding-right: 5px">
                                                                                    <asp:ImageButton ID="imgTrfConfirm" ImageUrl="~/img/button_confirm.png" ToolTip="Confirm"
                                                                                        OnClick="imgTrfConfirm_Click" runat="server" />
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
                                                style="padding-top: 15px;">VISA</span>
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
                                    <div class="ui-widget-content" id="dvVisaTab1" style="display: none; background-color: White;">
                                        <div id="dvVisaSummary" runat="server" style="margin-left: 20px;">
                                            <%--             <div class="checkout-head">
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
                                                                <asp:Label ID="lblVisaHeading" Text="VISA DETAILS" Visible="false" runat="server"></asp:Label>
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
                                                  <div id="dvBookingMode" runat="server" class="guest-summary-heading" style="margin-top:-15px;"> <%--Modified by abin on 20180728--%>
                                                                    <asp:Label ID="lblBookingMode" Text="Free Form Booking" CssClass="additional-service-heading-label" runat="server"></asp:Label>
                                                                 <asp:HiddenField ID="hdBookingMode" Value='<%# Eval("bookingmode") %>' runat="server" />
                                                                 </div>
                                                    <div class="line-bottom">
                                                    

                                                        <div class="chk-details">
                                                            <div class="chk-l" style="width: 25%">
                                                                Visa type &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                            <div class="chk-r" style="width: 75%">
                                                                <asp:Label ID="lblVisaTypeName" Text='<%# Eval("visatypename") %>' runat="server">
                                                                </asp:Label>
                                                                <asp:Label ID="lblvlineno" Text='<%# Eval("vlineno") %>' Visible="false" runat="server">
                                                                </asp:Label>
                                                                <asp:HiddenField runat="server" Value='<%# Eval("cancelled") %>' ID="hdvisaCancelled" />
                                                            </div>
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
                                                    </div>
                                                    <%-- <div class="chk-detais-row">--%>
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
                                                        <div id="dvwlVisaSalevalue" runat="server">
                                                            <div style="width: 35%; float: left;">
                                                                <div class="chk-l">
                                                                    <span>Visa Price</span></div>
                                                                <div class="chk-r" style="margin-left: 5px;">
                                                                    <asp:Label ID="lblwlVisaPrice" Text='<%# Eval("wlvisaprice") %>' runat="server"></asp:Label></div>
                                                            </div>
                                                            <div style="width: 35%; float: left;">
                                                                <div class="chk-l">
                                                                    <span>Visa Value</span></div>
                                                                <div class="chk-r" style="margin-left: 5px;">
                                                                    <asp:Label ID="lblwlVisaValue" class="chk-r" Text='<%# Eval("wlvisavalue") %>' runat="server"></asp:Label></div>
                                                            </div>
                                                        </div>
                                                        <div id="dvvisaCancelled" runat="server" class="chk-line" style="padding-bottom: 15px;
                                                            padding-top: 10px; margin-top: 5px;">
                                                            <div style="background-color: #F2F3F4; text-align: center; padding: 5px;">
                                                                <asp:Label ID="Label25" CssClass="summary-cancelled-label" Text="Visa Status: Cancelled"
                                                                    runat="server"></asp:Label></div>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        <div id="dvVisaButtons" runat="server" class="chk-line" style="padding-bottom: 45px;
                                                            padding-top: 10px; margin-top: 5px;">
                                                            <div style="float: left;">
                                                                <div id="divvisaedit" runat="server" style="float: left; padding-right: 10px; padding-top: 5px;">
                                                                    <asp:ImageButton ID="imgVisaEdit" ImageUrl="~/img/button_amend.png" ToolTip="Amend"
                                                                        OnClick="imgVisaEdit_Click" runat="server" />
                                                                </div>
                                                                <div id="divVisaremove" runat="server" style="float: left; padding-top: 5px; padding-right: 10px;">
                                                                    <asp:ImageButton ID="imgVisaDelete" ImageUrl="~/img/button_remove.png" OnClick="imgVisaDelete_Click"
                                                                        ToolTip="Remove" runat="server" OnClientClick="return confirmDelete();" />
                                                                </div>
                                                                <div id="divVisacancel" runat="server" style="float: left; padding-top: 5px; padding-right: 10px">
                                                                    <asp:ImageButton ID="imgVisacancel" ImageUrl="~/img/button_cancel.png" ToolTip="Cancel"
                                                                        OnClick="imgVisacancel_Click" runat="server" />
                                                                </div>
                                                                <div id="divvisaremarks" runat="server" style="float: left; padding-right: 10px;
                                                                    padding-top: 5px;">
                                                                    <asp:ImageButton ID="ImgVisaRemarks" ImageUrl="~/img/button_remarks.png" ToolTip="Remarks"
                                                                        runat="server" OnClick="ImgVisaRemarks_Click" />
                                                                </div>
                                                                <div id="dvVisaConfirm" runat="server" style="float: left; padding-top: 5px;">
                                                                    <asp:ImageButton ID="imgbVisaConfirm" ImageUrl="~/img/button_confirm.png" ToolTip="Confirm"
                                                                        runat="server" OnClick="imgbVisaConfirm_Click" /></div>
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
                                        </div>
                                    </div>
                                </div>
                                <div class="ui-widget collapse" id="dvTabOtherServicesSummary" runat="server">
                                    <div class="ui-widget-header">
                                        <div style="float: left; margin: 5px; width: 40%;">
                                            <span class="ui-icon ui-expander" style="float: left; margin: 5px;">+</span> <span
                                                style="padding-top: 15px;">OTHER SERVICES</span>
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
                                    <div class="ui-widget-content" id="dvOtherTab1" style="display: none;">
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
                                                                     <div id="dvBookingMode" runat="server" class="guest-summary-heading" style="margin-top:-15px;"> <%--Modified by abin on 20180728--%>
                                                                    <asp:Label ID="lblBookingMode" Text="Free Form Booking" CssClass="additional-service-heading-label" runat="server"></asp:Label>
                                                                 <asp:HiddenField ID="hdBookingMode" Value='<%# Eval("bookingmode") %>' runat="server" />
                                                                 </div>

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
                                                                            <asp:Label ID="lblOthersServiceName" Text='<%# Eval("servicename") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblolineno" Visible="false" Text='<%# Eval("olineno") %>' runat="server"></asp:Label>
                                                                            <asp:HiddenField runat="server" Value='<%# Eval("cancelled") %>' ID="hdothCancelled" />
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
                                                                            <div id="dvotherCancelled" runat="server" class="chk-line" style="padding-bottom: 15px;
                                                                                padding-top: 10px; margin-top: 5px;">
                                                                                <div style="background-color: #F2F3F4; text-align: center; padding: 5px;">
                                                                                    <asp:Label ID="Label25" CssClass="summary-cancelled-label" Text="OtherService Status: Cancelled"
                                                                                        runat="server"></asp:Label></div>
                                                                            </div>
                                                                        </div>
                                                                        <div id="dvOSButtons" runat="server" class="chk-line" style="padding-bottom: 45px;
                                                                            padding-top: 10px; margin-top: 5px;">
                                                                            <div style="float: left;">
                                                                                <div id="divothedit" runat="server" style="float: left; padding-right: 10px; padding-top: 5px;">
                                                                                    <asp:ImageButton ID="imgothEdit" ImageUrl="~/img/button_amend.png" ToolTip="Amend"
                                                                                        OnClick="imgothEdit_Click" runat="server" />
                                                                                </div>
                                                                                <div id="divotherremove" runat="server" style="float: left; padding-top: 5px; padding-right: 10px;">
                                                                                    <asp:ImageButton ID="imgothDelete" ImageUrl="~/img/button_remove.png" OnClick="imgothDelete_Click"
                                                                                        ToolTip="Remove" runat="server" OnClientClick="return confirmDelete();" />
                                                                                </div>
                                                                                <div id="divothercancel" runat="server" style="float: left; padding-top: 5px; padding-right: 10px">
                                                                                    <asp:ImageButton ID="imgothercancel" ImageUrl="~/img/button_cancel.png" ToolTip="Cancel"
                                                                                        OnClick="imgothercancel_Click" runat="server" />
                                                                                </div>
                                                                                <div id="divothremarks" runat="server" style="float: left; padding-right: 10px; padding-top: 5px;">
                                                                                    <asp:ImageButton ID="ImgOthServRemarks" ImageUrl="~/img/button_remarks.png" ToolTip="Remarks"
                                                                                        runat="server" OnClick="ImgOthServRemarks_Click" />
                                                                                </div>
                                                                                <div id="dvOtherConfirm" runat="server" style="float: left; padding-top: 5px;">
                                                                                    <asp:ImageButton ID="imgbOthersConfirm" ImageUrl="~/img/button_confirm.png" ToolTip="Confirm"
                                                                                        runat="server" OnClick="imgbOthersConfirm_Click" /></div>
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
                                                                <div id="dvPreHotelButtons" runat="server"  style="padding-bottom: 45px;
                                                                    padding-top: 10px; margin-top: 5px;">
                                                                    <div style="float: left;">
                                                                        <div id="divPreHoteledit" runat="server" style="float: left; padding-right: 10px;
                                                                            padding-top: 5px;">
                                                                            <asp:ImageButton ID="imgPreHotelEdit" ImageUrl="~/img/button_amend.png" ToolTip="Amend"
                                                                                OnClick="imgPreHotelEdit_Click" runat="server" />
                                                                        </div>
                                                                        <div id="divPreHotelremove" runat="server" style="float: left; padding-top: 5px;
                                                                            padding-right: 10px;">
                                                                            <asp:ImageButton ID="imgPreHotelDelete" ImageUrl="~/img/button_remove.png" OnClick="imgPreHotelDelete_Click"
                                                                                ToolTip="Remove" runat="server" OnClientClick="return confirmDelete();" />
                                                                        </div>
                                                                        <div id="divPreHotelremarks" runat="server" style="float: left; padding-right: 10px;
                                                                            display: none; padding-top: 5px;">
                                                                            <asp:ImageButton ID="ImgPreHotelRemarks" ImageUrl="~/img/button_remarks.png" ToolTip="Remarks"
                                                                                Visible="false" runat="server" OnClick="ImgPreHotelRemarks_Click" />
                                                                        </div>
                                                                        <div style="float: left; padding-top: 5px; padding-right: 10px;" id="dvAmend" runat="server">
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
                                <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>--%>
                                <div class="clear">
                                </div>
                                <div class="line-bottom" style="margin-left: 5px; background-color: #777777; margin-left: -px;"
                                    id="divtotal" runat="server">
                                    <div class="chk-total" id="Div3" runat="server">
                                        <div class="chk-totalnew-l" style="color: #fff; margin-top: -9px; padding-left: 15px;">
                                            Total Booking Value
                                        </div>
                                        <div class="chk-total-r" style="padding-right: 15px; margin-top: -9px;">
                                            <asp:Label ID="lbltotalbooking" runat="server"></asp:Label>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                                <div class="line-bottom" style="margin-left: 5px; background-color: #777777; margin-left: 0px;"
                                    id="div126" runat="server">
                                    <div class="chk-total" id="Div127" runat="server">
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
                                    <div class="chk-total" id="Div128" runat="server">
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
                                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                    <ContentTemplate>
                                <div id="dvGeneratePackageValue" runat="server" style="padding-top: 20px;">
                                    <asp:Button ID="btnGeneratePackageValue" class="authorize-btn" Width="200px" runat="server"
                                        Text="Generate Package Value" />
                                </div>

                                <div id="dvPackageDetails" runat="server" style="padding-top: 40px; background-color: White;">
                                    <div style="width: 100%; height: 30px; padding-left: 10px;" class="ui-widget-header">
                                        <span>Package Details</span></div>
                                    <div class="ui-widget-content" style="background-color: White; border: none !important;
                                        padding-left: 10px; padding-bottom: 45px;">
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
                                                            <asp:LinkButton ID="lblPFormulaId" runat="server"></asp:LinkButton></div>
                                                    </div>
                                                    <div id="dvROPackage5" class="divTableRow">
                                                        <div class="divTableCell">
                                                            <asp:Label ID="lblPDifferentialMarkupText" runat="server"></asp:Label></div>
                                                        <div class="divTableCell">
                                                            <asp:Label ID="lblPDifferentialMarkup" runat="server"></asp:Label></div>
                                                        <div class="divTableCell">
                                                            <asp:Label ID="lblPDifferentialMarkupbase" runat="server"></asp:Label></div>
                                                    </div>
                                                    <div id="dvROPackage6" style="display: none;" class="divTableRow">
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
                                                    <%--Added shahul 28/06/18--%>
                                                    <div class="divTableRow">
                                                        <div class="divTableCell">
                                                            <asp:LinkButton ID="lblPNetprofitText" runat="server"></asp:LinkButton></div>
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
                                                     <%--Added shahul 30/06/18--%>
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
                                        <div id="dvPackageSummaryAgent" runat="server">
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
                                </ContentTemplate> 
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="upnlPackageROSummary" runat="server">
                                    <ContentTemplate>
                                        <asp:ModalPopupExtender ID="mpPackageROSummary" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                            CancelControlID="a1" EnableViewState="true" PopupControlID="pnlPackageROSummary"
                                            TargetControlID="hdPackageROSummary">
                                        </asp:ModalPopupExtender>
                                        <asp:HiddenField ID="hdPackageROSummary" runat="server" />
                                        <asp:Panel ID="pnlPackageROSummary" Style="display: none;" runat="server">
                                            <div id="div94" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                                max-height: 500px;">
                                                <div align="left" id="Div95">
                                                    <div id="div98" runat="server" style="border: 1px solid #fff; width: 800px; height: 30px;
                                                        background-color: #ede7e1; padding-right: 5px; margin-right: 10px; text-align: center;
                                                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                                        <asp:Label ID="lblPackageROSummaryHeadding" CssClass="room-type-breakup-headings"
                                                            Text="Booking Summary" runat="server"></asp:Label>
                                                        &nbsp; &nbsp;<a id="a1" href="#" class="roomtype-popupremarks-close"></a>
                                                    </div>
                                                    <div class="booking-form" id="Div99" runat="server" style="background-color: White;
                                                        padding: 5px;">
                                                        <div class="row-col-12" style="padding-left: 10px;">
                                                            <asp:GridView ID="gvPackageROSummary" runat="server" AutoGenerateColumns="False"
                                                                CssClass="mygrid" Width="100%" GridLines="Horizontal">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Request Id">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRequestId" Text='<%# Eval("requestid") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Services">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRequestType" Text='<%# Eval("requesttype") %>' runat="server"></asp:Label>
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
                                                                    <asp:TemplateField HeaderText="Sale Value">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSaleValueHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSaleValue" Text='<%# Eval("salevalue") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sale Value Base">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSaleValueBaseHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSaleValueBase" Text='<%# Eval("salevaluebase") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cost Value">
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
                                            <input id="Button13" runat="server" type="button" value="Cancel" style="display: none" />
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="upnlFormulaROSummary" runat="server">
                                    <ContentTemplate>
                                        <asp:ModalPopupExtender ID="mpFormulaROSummary" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                            CancelControlID="a2" EnableViewState="true" PopupControlID="pnlFormulaROSummary"
                                            TargetControlID="hdFormulaROSummary">
                                        </asp:ModalPopupExtender>
                                        <asp:HiddenField ID="hdFormulaROSummary" runat="server" />
                                        <asp:Panel ID="pnlFormulaROSummary" Style="display: none;" runat="server">
                                            <div id="div104" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                                max-height: 500px; max-width: 600px;">
                                                <div align="left" id="Div107">
                                                    <div id="div108" runat="server" style="border: 1px solid #fff; width: 600px; height: 30px;
                                                        background-color: #ede7e1; padding-right: 5px; margin-right: 10px; text-align: center;
                                                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                                        <asp:Label ID="lblFormulaROSummaryHeadding" CssClass="room-type-breakup-headings"
                                                            Text="Formula Details" runat="server"></asp:Label>
                                                        &nbsp; &nbsp;&nbsp;<a id="a2" href="#" class="roomtype-popupremarksnew-close" style="margin-left:550px;"></a>
                                                    </div>
                                                    <div class="booking-form" id="Div109" runat="server" style="background-color: White;
                                                        padding: 5px;">
                                                        <div class="search-large-i tour-change-date-label">
                                                            <div class="row-col-3" style="margin-left: 10px; width: 500px;">
                                                                <label>
                                                                    Adults without Visa</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtAdultwovisa" CssClass="roomtype-popup-textbox" onkeydown="fnReadOnly(event)"
                                                                    runat="server"></asp:TextBox>
                                                                <label>
                                                                    Adults with Visa</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtadultvisa" CssClass="roomtype-popup-textbox" onkeydown="fnReadOnly(event)"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="row-col-3" style="margin-left: 10px; margin-top: 20px; width: 600px;">
                                                                <label>
                                                                    Child without Visa</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtChildwovisa" CssClass="roomtype-popup-textbox" onkeydown="fnReadOnly(event)"
                                                                    runat="server"></asp:TextBox>
                                                                <label>
                                                                    Child with Visa
                                                                </label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtChildwvisa" CssClass="roomtype-popup-textbox" onkeydown="fnReadOnly(event)"
                                                                    runat="server"></asp:TextBox>
                                                                    <label>
                                                                    Child FreeUpto
                                                                      <asp:TextBox ID="txtchildfree" CssClass="roomtype-popup-textbox" onkeydown="fnReadOnly(event)"
                                                                    runat="server"></asp:TextBox>
                                                                </label>
                                                            </div>
                                                            <div class="row-col-3" style="margin-left: 10px; margin-top: 20px; width: 500px;">
                                                                <label>
                                                                    Child Freewithout Visa</label>
                                                                <asp:TextBox ID="txtChildfreewovisa" CssClass="roomtype-popup-textbox" onkeydown="fnReadOnly(event)"
                                                                    runat="server"></asp:TextBox>
                                                                <label>
                                                                    Child Free with Visa
                                                                </label>
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtChildfreewvisa" CssClass="roomtype-popup-textbox" onkeydown="fnReadOnly(event)"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        <asp:Label ID="lbltxt1" Style="margin-left: 10px;" Text="Discount per booking over and above minimum markup"
                                                            runat="server" Font-Names="Raleway"></asp:Label>
                                                        <div style="margin-left: 10px; width: 500px;">
                                                            <asp:GridView ID="gvFormulaROSummary" runat="server" AutoGenerateColumns="False"
                                                                CssClass="mygrid" Width="50%" GridLines="Horizontal">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="From Slab">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRequestId" Text='<%# Eval("fromslab") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="To Slab">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRequestType" Text='<%# Eval("toslab") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Discount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLineNo" Text='<%# Eval("discount") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                            <input id="Button10" runat="server" type="button" value="Cancel" style="display: none" />
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div id="Div110" runat="server" style="padding-top:50px;width:200px;float:left;">
                                    <asp:Button ID="btnCheckVAT" class="authorize-btn" Width="200px" runat="server"
                                        Text="Check VAT Values" />
                                </div>
                                <div id="Div118" runat="server" style="padding-top:50px;margin-left: 230px;">
                                    <asp:Button ID="btnItinerary" class="authorize-btn" Width="150px" runat="server"
                                        Text="Itinerary" />
                                </div>                               
                                <asp:UpdatePanel ID="upnlPackageConfirmError" runat="server">
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:ModalPopupExtender ID="mpPackageConfirmError" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                    CancelControlID="aPackageConfirmError" EnableViewState="true" PopupControlID="pnlPackageConfirmError"
                                    TargetControlID="Button14">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="pnlPackageConfirmError" Style="display: none;" runat="server">
                                    <div id="div100" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                        max-height: 500px;">
                                        <div align="left" id="Div101">
                                            <div id="div102" runat="server" style="border: 1px solid #fff; width: 700px; height: 30px;
                                                background-color: #ede7e1; padding-right: 5px; margin-right: 10px; text-align: center;
                                                vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                                <asp:Label ID="Label21" CssClass="room-type-breakup-headings" Text="Kindly Note"
                                                    runat="server"></asp:Label>
                                                &nbsp; &nbsp;<a id="aPackageConfirmError" href="#" class="roomtype-popupremarks-close"></a>
                                            </div>
                                            <div class="booking-form" id="Div103" runat="server" style="background-color: White;
                                                padding: 5px;">
                                                <div class="row-col-12" style="padding-left: 10px;">
                                                    <asp:GridView ID="gvPackageConfirmError" runat="server" AutoGenerateColumns="False"
                                                        CssClass="mygrid" Width="100%" GridLines="Horizontal">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblerrdesc" Text='<%# Eval("errdesc") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                        <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                    <asp:Label ID="lblPRequestId" Style="display: none;" runat="server"></asp:Label>
                                                    <asp:Label ID="lblDivCode" Style="display: none;" runat="server"></asp:Label>
                                                </div>
                                                <div class="clear">
                                                </div>
                                                <div style="padding-top: 10px;">
                                                    |<asp:Label ID="lblErrorNote" CssClass="room-type-breakup-headings" Text="* You can avail discount if you book additional services."
                                                        runat="server"></asp:Label>
                                                </div>
                                                <div class="clear">
                                                </div>
                                                <div class="booking-form-i-a" style="margin-left: 100px; margin-top: 30px;">
                                                    <div class="clear">
                                                    </div>
                                                    <asp:Button ID="btnBackTo" class="authorize-btn" Width="170px" runat="server" Text="Back to Booking" />
                                                </div>
                                                <div class="booking-form-i-a" style="margin-top: 30px;">
                                                    <div class="clear">
                                                    </div>
                                                    <asp:Button ID="btnBacktoBookingForPackage" class="authorize-btn" Width="170px" runat="server"
                                                        Text="Proceed with Quote" />
                                                    <asp:Button ID="btnProceedWithBook" class="authorize-btn" Width="170px" runat="server"
                                                        Text="Proceed with Book" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <input id="Button14" runat="server" type="button" value="Cancel" style="display: none" />
                                </asp:Panel>

                                  <asp:UpdatePanel ID="upnlCheckVATValues" runat="server">
                                    <ContentTemplate>
                                        <asp:ModalPopupExtender ID="mpCheckVATValues" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                            CancelControlID="aCloseCheckVATValues" EnableViewState="true" PopupControlID="pnlCheckVATValues"
                                            TargetControlID="hdCheckVATValues">
                                        </asp:ModalPopupExtender>
                                        <asp:HiddenField ID="hdCheckVATValues" runat="server" />
                                        <asp:Panel ID="pnlCheckVATValues" Style="display: none;" runat="server">
                                            <div id="dvCheckVATValues" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                                max-height: 500px;">
                                                <div align="left" id="Div112">
                                                    <div id="div113" runat="server" style="border: 1px solid #fff; width: 100%; height: 30px;
                                                        background-color: #ede7e1; padding-right: 5px;  text-align: center;
                                                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                                        <asp:Label ID="Label103" style="padding-left:150px;" CssClass="room-type-breakup-headings"
                                                            Text="VAT Details" runat="server"></asp:Label>
                                                        &nbsp; &nbsp;<a style="float:right;margin-top:-10px;" id="aCloseCheckVATValues" href="#" class="roomtype-popupremarks-close"></a>
                                                    </div>
                                                    <div class="booking-form" id="Div114" runat="server" style="background-color: White;
                                                        padding: 5px;">
                                                        <div class="row-col-12" style="padding-left: 10px;">
                                                            <asp:GridView ID="gvCheckVATValues" runat="server" AutoGenerateColumns="False" 
                                                                CssClass="mygrid" Width="100%" GridLines="Horizontal">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Request Id">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRequestId" Text='<%# Eval("requestid") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Services">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRequestType" Text='<%# Eval("requesttype") %>' runat="server"></asp:Label>
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
                                                                    <asp:TemplateField HeaderText="Sale Value">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSaleValueHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSaleValue" Text='<%# Eval("salevalue") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sale Value Base">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSaleValueBaseHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSaleValueBase" Text='<%# Eval("salevaluebase") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cost Value">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblCostValueHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCostValue" Text='<%# Eval("costvalue") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="Sale Taxable Value">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSaleTaxableValueHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSaleTaxableValue" Text='<%# Eval("PriceTaxableValue") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="Sale NonTaxable Value">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSaleNonTaxableValueHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSaleNonTaxableValue" Text='<%# Eval("PriceNonTaxableValue") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="Sale VAT Value">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSaleVATValueHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSaleVATValue" Text='<%# Eval("PriceVATValue") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Cost Taxable Value">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblCostTaxableValueHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCostTaxableValue" Text='<%# Eval("CostTaxableValue") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="Cost NonTaxable Value">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblCostNonTaxableValueHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCostNonTaxableValue" Text='<%# Eval("CostNonTaxableValue") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="Cost VAT Value">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblCostVATValueHeader" Text="" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCostVATValue" Text='<%# Eval("CostVATValue") %>' runat="server"></asp:Label>
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

                                   <asp:UpdatePanel ID="upnlDiscountROSummary" runat="server">
                                    <ContentTemplate>
                                        <asp:ModalPopupExtender ID="mpDiscountROSummary" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                            CancelControlID="a3" EnableViewState="true" PopupControlID="pnlDiscountROSummary"
                                            TargetControlID="hdDiscountROSummary">
                                        </asp:ModalPopupExtender>
                                        <asp:HiddenField ID="hdDiscountROSummary" runat="server" />
                                        <asp:Panel ID="pnlDiscountROSummary" Style="display: none;" runat="server">
                                            <div id="div111" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                                max-height: 500px; max-width: 500px;">
                                                <div align="left" id="Div115">
                                                    <div id="div116" runat="server" style="border: 1px solid #fff; width: 500px; height: 30px;
                                                        background-color: #ede7e1; padding-right: 5px; margin-right: 10px; text-align: center;
                                                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                                        <asp:Label ID="lblDiscountROSummaryHeadding" CssClass="room-type-breakup-headings"
                                                            Text="Markup Details" runat="server"></asp:Label>
                                                        &nbsp; &nbsp;&nbsp;<a id="a3" href="#" class="roomtype-popupremarksnew-close" style="margin-left:450px;"></a>
                                                    </div>
                                                    <div class="booking-form" id="Div117" runat="server" style="background-color: White;
                                                        padding: 5px;">
                                                       
                                                        <div class="clear">
                                                        </div>
                                                        <asp:Label ID="lbltxtDiscount1" Style="margin-left: 10px;" Text=""
                                                            runat="server" Font-Names="Raleway"></asp:Label>
                                                        <div style="margin-left: 10px; width: 500px;">
                                                            <asp:GridView ID="gvDiscountROSummary" runat="server" AutoGenerateColumns="False"
                                                                CssClass="mygrid" Width="80%" GridLines="Horizontal">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Pax Details">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpaxdetails" Text='<%# Eval("paxdetails") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="No.of Pax">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpax" Text='<%# Eval("noofpax") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  
                                                                     <asp:TemplateField HeaderText="Required Markup.">
                                                                  
                                                                    <ItemTemplate>
                                                                        <div class="booking-form-i-a" style="width: 70px; float: left; height: 10px;">
                                                                            <div class="input input" id="dvpax" runat="server">
                                                                                <asp:TextBox ID="txtsystemmarkup" Style="border: none; font-size: 12px; width: 50px;"
                                                                                 Text='<%# Eval("systemmarkup") %>'   runat="server"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Required Net Profit">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnetprofit" Text='<%# Eval("revisedmarkup") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Paxtype" Visible ="false" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpaxtype" Text='<%# Eval("paxtype") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                        </div>

                                                        <div class="booking-form-i-a" style="margin-left: 270px;margin-top:20px;">
                                                              <asp:Button ID="btndiscountsave" class="authorize-btn" Width="100px" runat="server"
                                                                Text="Save" />
                                                         </div>

                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                            <input id="Button15" runat="server" type="button" value="Cancel" style="display: none" />
                                             <div class="clear" style="padding-top: 20px">
                                             </div>
                                            
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                   <asp:UpdatePanel ID="upnlItineraryOrder" runat="server">
                                    <ContentTemplate>
                                        <asp:ModalPopupExtender ID="mpIdineraryOrder" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                            CancelControlID="aCloseIdineraryOrder" EnableViewState="true" PopupControlID="pnlIdineraryOrder"
                                            TargetControlID="hdIdineraryOrder">
                                        </asp:ModalPopupExtender>
                                        <asp:HiddenField ID="hdIdineraryOrder" runat="server" />
                                        <asp:Panel ID="pnlIdineraryOrder" Style="display: none;" runat="server">
                                            <div id="dvIdineraryOrder" class="roomtype-price-breakuppopup" style="float: left; text-align: left;
                                                max-height: 500px;">
                                                <div align="left" id="Div120">
                                                    <div id="div121" runat="server" style="border: 1px solid #fff; width: 100%; height: 30px;
                                                        background-color: #ede7e1; padding-right: 5px;  text-align: center;
                                                        vertical-align: middle; padding-top: 10px; margin-bottom: 0px;">
                                                        <asp:Label ID="Label104" style="padding-left:150px;" CssClass="room-type-breakup-headings"
                                                            Text="Itinerary Ordering" runat="server"></asp:Label>
                                                        &nbsp; &nbsp;<a style="float:right;margin-top:-10px;" id="aCloseIdineraryOrder" href="#" class="roomtype-popupremarks-close"></a>
                                                    </div>
                                                    <div class="booking-form" id="Div122" runat="server" style="background-color: White;
                                                        padding: 5px;">
                                                        <div class="row-col-12" style="padding-left: 10px;">
                                                            <asp:GridView ID="gvItinerary" runat="server" AutoGenerateColumns="False" 
                                                                CssClass="mygrid" Width="100%" GridLines="Horizontal">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Day /Sequence">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldaynoseq" Text='<%# Eval("daynoseq") %>' runat="server"></asp:Label>
                                                                            <asp:TextBox ID="txtDayNoSeq" runat="server" style="max-width: 50px;" Visible="false"></asp:TextBox>                                                                            
                                                                            <asp:Label ID="lblRlineno" Text='<%# Eval("rlineno") %>' runat="server" style="display:none;"></asp:Label>                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Exclude Itinerary">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkExcludeServ" runat="server" Checked='<%# Eval("ExcludeService") %>' />                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Day">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDay" Text='<%# Eval("checkindayname") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="From Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFromDate" Text='<%# Eval("checkin") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="To Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblToDate" Text='<%# Eval("checkout") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="service Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblServiceType" Text='<%# Eval("serviceType") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Service Details">                                                                        
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblServiceDetails" Text='<%# Eval("servicedetails") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                            <div>
                                                            <div id="Div123" style="padding-top:10px;width:120px;float:left;">
                                                            <input id="btnEditItineraryOrder" runat="server" type="button" class="authorize-btn" style="width:120px;" value="Edit" />
                                                            </div>
                                                            <div id="Div124" style="padding-top:10px;margin-left: 150px;">
                                                                <input id="btnSaveItineraryOrder" runat="server" type="button"  class="authorize-btn" style="width:120px;" value="Save" />
                                                            </div>
                                                            </div>
                                                        </div>

                                                        <div class="clear">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>                                            
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <asp:Button ID="btnFillPackageWithoutDiscount" Style="display: none;" Width="170px"
                                    runat="server" Text="FillPackageWithoutDiscount" />
                                <asp:Button ID="btnConfirmHome" Width="170px" Style="display: none;" runat="server"
                                    Text="ConfirmHome" />
                                <asp:HiddenField ID="hdSectorgroupcode" runat="server" />
                                <%--           </ContentTemplate> 
            </asp:UpdatePanel> --%>
                                <div class="h-help">
                                </div>
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
