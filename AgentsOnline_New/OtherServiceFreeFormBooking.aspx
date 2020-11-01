<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OtherServiceFreeFormBooking.aspx.vb" Inherits="OtherServiceFreeFormBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta charset="utf-8" />
    <link rel="icon" href="favicon.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <link rel="stylesheet" href="css/jquery-ui.css">
    <link rel="stylesheet" href="css/jquery.formstyler.css">
    <link rel="stylesheet" href="css/style.css" />
    <link id="lnkCSS" rel='stylesheet' type='text/css' href='css/style-common.css' />
<%--
    <link rel="stylesheet" href="css/Raleway.css" />
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
            font-family: Raleway;
            color: #455051;
        }
        .mygrid-header
        {
            background-color: #EDE7E1;
            font-family: Raleway;
            color: #455051;
            height: 25px;
            text-align: left;
            font-size: 16px;
            font-size: small;
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
            vertical-align: middle;
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
            color: #455051;
            padding: 5px 5px 5px 5px;
            font-size: 11px;
            vertical-align: middle;
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
<%--    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"
        type="text/javascript"></script>--%>
    <script language="javascript" type="text/javascript">

        function SetServiceNameContextKey() {
            if (document.getElementById('<%=txtothgroupcode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any service group.', 'warning');
                return false;
            }
            $find('AutoCompleteExtenderServiceName').set_contextKey(document.getElementById('<%=txtothgroupcode.ClientID%>').value);

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

    </script>
    <script language="javascript" type="text/javascript">

        function CalculateSaleValue(lblNoOfAdult, txtPrice, lblSaleValue) {

            var lblNoOfAdult1 = document.getElementById(lblNoOfAdult);
            var txtPrice1 = document.getElementById(txtPrice);
            var lblSaleValue1 = document.getElementById(lblSaleValue);
            var totalamt = (parseFloat(txtPrice1.value) * parseFloat(lblNoOfAdult1.value));
            lblSaleValue1.value = totalamt;


        }

        function calculatetotalvaluewithcost() {

            var txtunits = document.getElementById('<%=txtNoOfUnit.ClientID%>');
            var txtunitprice = document.getElementById('<%=txtUnitPrice.ClientID%>');
            var txttotalamt = document.getElementById('<%=txtTotal.ClientID%>');
            var txtcostunitprice = document.getElementById('<%=txtCostPricePax.ClientID%>');
            var txtcosttotal = document.getElementById('<%=txtCostPricePaxTotal.ClientID%>');

            if (txtunits.value == 0) {
                txtunits.value = '1';
            }
            if (txtunitprice.value == '') {
                txtunitprice.value = '0';
            }
            if (txtcostunitprice.value == '') {
                txtcostunitprice.value = '0';
            }

            var totalamt = (parseFloat(txtunitprice.value) * parseFloat(txtunits.value));
            txttotalamt.value = totalamt;
            var costtotalamt = (parseFloat(txtunits.value) * parseFloat(txtcostunitprice.value));
            txtcosttotal.value = costtotalamt;
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




        function mUp(obj) {

        }

        function ValidateSearch() {

            if (document.getElementById('<%=txtothCustomercode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any agent.', 'warning');
                return false;
            }

            if ((document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value == '0') || (document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please select any source country.', 'warning');
                return false;
            }

            if (document.getElementById('<%=txtothFromDate.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any from-date.', 'warning');
                return false;
            }


            if (document.getElementById('<%=txtothgroup.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select Service Group.', 'warning');
                return false;
            }





            var child = document.getElementById('<%=ddlTourChildren.ClientID%>').value;
            if (child != '0') {
                var child1 = document.getElementById('<%=txtTourChild1.ClientID%>').value;
                var child2 = document.getElementById('<%=txtTourChild2.ClientID%>').value;
                var child3 = document.getElementById('<%=txtTourChild3.ClientID%>').value;
                var child4 = document.getElementById('<%=txtTourChild4.ClientID%>').value;
                var child5 = document.getElementById('<%=txtTourChild5.ClientID%>').value;
                var child6 = document.getElementById('<%=txtTourChild6.ClientID%>').value;
                var child7 = document.getElementById('<%=txtTourChild7.ClientID%>').value;
                var child8 = document.getElementById('<%=txtTourChild8.ClientID%>').value;
                if (child == 1) {

                    if (child1 == 0) {
                        showDialog('Alert Message', 'Please select child age.', 'warning');
                        return false;
                    }

                }
                else if (child == 2) {
                    if (child1 == 0 || child2 == 0) {
                        showDialog('Alert Message', 'Please select child age.', 'warning');
                        return false;
                    }
                }
                else if (child == 3) {
                    if (child1 == 0 || child2 == 0 || child3 == 0) {
                        showDialog('Alert Message', 'Please select child age.', 'warning');
                        return false;
                    }
                }
                else if (child == 4) {
                    if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0) {
                        showDialog('Alert Message', 'Please select child age.', 'warning');
                        return false;
                    }
                }
                else if (child == 5) {
                    if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0 || child5 == 0) {
                        showDialog('Alert Message', 'Please select child age.', 'warning');
                        return false;
                    }
                }
                else if (child == 6) {
                    if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0 || child5 == 0 || child6 == 0) {
                        showDialog('Alert Message', 'Please select child age.', 'warning');
                        return false;
                    }
                }
                else if (child == 7) {
                    if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0 || child5 == 0 || child6 == 0 || child7 == 0) {
                        showDialog('Alert Message', 'Please select child age.', 'warning');
                        return false;
                    }
                }
                else if (child == 8) {
                    if (child1 == 0 || child2 == 0 || child3 == 0 || child4 == 0 || child5 == 0 || child6 == 0 || child7 == 0 || child8 == 0) {
                        showDialog('Alert Message', 'Please select child age.', 'warning');
                        return false;
                    }
                }

            }

            if (document.getElementById('<%=txtServiceName.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any service.', 'warning');
                return false;
            }

            if ((document.getElementById('<%=txtNoOfUnit.ClientID%>').value == '') || (document.getElementById('<%=txtNoOfUnit.ClientID%>').value == '0')) {
                showDialog('Alert Message', 'Please enter no of unit.', 'warning');
                return false;
            }
            if ((document.getElementById('<%=txtUnitPrice.ClientID%>').value == '') || (document.getElementById('<%=txtUnitPrice.ClientID%>').value == '0')) {
                showDialog('Alert Message', 'Please enter unit price.', 'warning');
                return false;
            }
            if ((document.getElementById('<%=txtCostPricePax.ClientID%>').value == '') || (document.getElementById('<%=txtCostPricePax.ClientID%>').value == '0')) {
                showDialog('Alert Message', 'Please enter cost price.', 'warning');
                return false;
            }

            return true;

        }

     

    </script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            ShowTourChild();
            $("#<%= ddlTourChildren.ClientID %>").bind("change", function () {
                ShowTourChild();
            });




            AutoCompleteExtender_OthCustomer_KeyUp();
            AutoCompleteExtender_OthCountry_KeyUp();

            AutoCompleteExtender_Othergroup_KeyUp();


            fillTourDates();



            $("#<%= txtothFromDate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=txtothFromDate.ClientID%>').value;

                var dp = d.split("/");
                var dateOut = new Date(dp[2], dp[1], dp[0]);
                var currentMonth = dateOut.getMonth() - 1;
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-oth-to").datepicker("destroy");

                var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
                if ((dCheckOutMax == '') || (dCheckOutMax == '0')) {
                    $(".date-inpt-oth-to").datepicker({
                        minDate: new Date(currentYear, currentMonth, currentDate)
                    });
                }
                else {
                    var dp4 = dCheckOutMax.split("/");
                    var date4 = new Date(dp4[2], dp4[1], dp4[0]);
                    var currentMonth4 = date4.getMonth() - 1;
                    var currentDate4 = date4.getDate();
                    var currentYear4 = date4.getFullYear();

                    $(".date-inpt-oth-to").datepicker({
                        minDate: new Date(currentYear, currentMonth, currentDate),
                        maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                    });
                }

            });


            var dfdate = document.getElementById('<%=txtothFromDate.ClientID%>').value;
            var dpFdate = dfdate.split("/");
            var FdateOut = new Date(dpFdate[2], dpFdate[1], dpFdate[0]);
            var fcurrentMonth = FdateOut.getMonth() - 1;
            var fcurrentDate = FdateOut.getDate();
            var fcurrentYear = FdateOut.getFullYear();


            //            var dpTdate = dTdate.split("/");
            //            var TdateOut = new Date(dpTdate[2], dpTdate[1], dpTdate[0]);
            //            var tcurrentMonth = TdateOut.getMonth() - 1;
            //            var tcurrentDate = TdateOut.getDate();
            //            var tcurrentYear = TdateOut.getFullYear();



            //            //
            //            $(".date-inpt-tour-change-date").datepicker({
            //                minDate: new Date(fcurrentYear, fcurrentMonth, fcurrentDate),
            //                maxDate: new Date(tcurrentYear, tcurrentMonth, tcurrentDate)
            //            });



            $("#btnTourReset").button().click(function () {

                document.getElementById('<%=txtothFromDate.ClientID%>').value = '';

                document.getElementById('<%=txtothgroup.ClientID%>').value = '';
                document.getElementById('<%=txtothgroupcode.ClientID%>').value = '';


                document.getElementById('<%=hdnreset.ClientID%>').value = '1';


                if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=hdnLineno.ClientID%>').value == '')) {
                    //                    document.getElementById('<%=txtothCustomer.ClientID%>').value = ''
                    //                    document.getElementById('<%=txtothCustomercode.ClientID%>').value = '';

                    //                    document.getElementById('<%=txtothSourceCountry.ClientID%>').value = '';
                    //                    document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = '';
                }

                document.getElementById('<%=txtServiceName.ClientID%>').value = '';
                document.getElementById('<%=txtServiceNameCode.ClientID%>').value = '';

                document.getElementById('<%=txtNoOfUnit.ClientID%>').value = '';
                document.getElementById('<%=txtUnitPrice.ClientID%>').value = '';
                document.getElementById('<%=txtTotal.ClientID%>').value = '';
                document.getElementById('<%=txtCostPricePax.ClientID%>').value = '';
                document.getElementById('<%=txtCostPricePaxTotal.ClientID%>').value = '';
                document.getElementById('<%=chkComplSup.ClientID%>').checked = false;

                var ddltourAdult = document.getElementById('<%=ddltourAdult.ClientID%>');
                ddltourAdult.selectedIndex = "0";
                $('.custom-select-ddlTourAdult').next('span').children('.customSelectInner').text(jQuery("#ddltourAdult :selected").text());

                var ddlTourChildren = document.getElementById('<%=ddlTourChildren.ClientID%>');
                ddlTourChildren.selectedIndex = "0";
                $('.custom-select-ddlTourChildren').next('span').children('.customSelectInner').text(jQuery("#ddlTourChildren :selected").text());



                //                var rblPrivateOrSIC = document.getElementById('< %=rblPrivateOrSIC.ClientID%>');
                //                var inputElementArray = rblPrivateOrSIC.getElementsByTagName('input');

                //                for (var i = 0; i < inputElementArray.length; i++) {
                //                    var inputElement = inputElementArray[i];

                //                    inputElement.checked = false;
                //                }


                document.getElementById('<%=txtTourChild1.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild2.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild3.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild4.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild5.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild6.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild7.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild8.ClientID%>').value = '';


                $('#dvTourChild').hide();

            });



            function AutoCompleteExtender_OthCountry_KeyUp() {

                $("#<%= txtothSourceCountry.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtothSourceCountryCode.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtothSourceCountry.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= txtothSourceCountry.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtothSourceCountryCode.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtothSourceCountry.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

            }


            function AutoCompleteExtender_Othergroup_KeyUp() {

                $("#<%= txtothgroup.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtothgroup.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtothgroupcode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= txtothgroup.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtothgroup.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtothgroupcode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

            }


            function AutoCompleteExtender_OthCustomer_KeyUp() {

                $("#<%= txtothCustomer.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtothCustomer.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtothCustomercode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= txtothCustomer.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtothCustomer.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtothCustomercode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

            }




        });

    </script>
    <script type="text/javascript">
    //<![CDATA[
        function pageLoad() { // this gets fired when the UpdatePanel.Update() completes
            ReBindMyStuff();

        }

        function ReBindMyStuff() { // create the rebinding logic in here

        }

        function ServiceNameautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtServiceNameCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtServiceNameCode.ClientID%>').value = '';
            }
        }

        function othgroupautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtothgroupcode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtothgroupcode.ClientID%>').value = '';
            }
        }

        function fillTourDates() {
            var dCheckInMin = document.getElementById('<%=hdCheckInPrevDay.ClientID%>').value;
            var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
            if ((dCheckInMin == '') || (dCheckInMin == '0') || (dCheckOutMax == '') || (dCheckOutMax == '0')) {

                var date = new Date();
                var currentMonth = date.getMonth();
                var currentDate = date.getDate();
                var currentYear = date.getFullYear();

                $(".date-inpt-oth-from").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });

                //changed by mohamed on 28/08/2018
                var currentMonth = date.getMonth() - 2;
                $(".date-inpt-oth-from-freeform").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });
            }
            else {
                var dp1 = dCheckInMin.split("/");
                var date1 = new Date(dp1[2], dp1[1], dp1[0]);
                var currentMonth1 = date1.getMonth() - 1;
                var currentDate1 = date1.getDate();
                var currentYear1 = date1.getFullYear();

                var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
                var dp4 = dCheckOutMax.split("/");
                var date4 = new Date(dp4[2], dp4[1], dp4[0]);
                var currentMonth4 = date4.getMonth() - 1;
                var currentDate4 = date4.getDate();
                var currentYear4 = date4.getFullYear();


                $(".date-inpt-oth-from").datepicker({
                    minDate: new Date(currentYear1, currentMonth1, currentDate1),
                    maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                });

                //changed by mohamed on 28/08/2018
                $(".date-inpt-oth-from-freeform").datepicker({
                    minDate: new Date(currentYear1, currentMonth1, currentDate1),
                    maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                });
            }


        }
        function ShowTourChild() {
            var child = $("#<%= ddlTourChildren.ClientID %>").val()

            if (child == 0) {
                $('#dvTourChild').hide();
            }
            else if (child == 1) {

                $('#dvTourChild1').show();
                $('#dvTourChild2').hide();
                $('#dvTourChild3').hide();
                $('#dvTourChild4').hide();
                $('#dvTourChild5').hide();
                $('#dvTourChild6').hide();
                $('#dvTourChild7').hide();
                $('#dvTourChild8').hide();
                $('#dvTourChild').show();
            }
            else if (child == 2) {
                $('#dvTourChild1').show();
                $('#dvTourChild2').show();
                $('#dvTourChild3').hide();
                $('#dvTourChild4').hide();
                $('#dvTourChild5').hide();
                $('#dvTourChild6').hide();
                $('#dvTourChild7').hide();
                $('#dvTourChild8').hide();
                $('#dvTourChild').show();

            }
            else if (child == 3) {

                $('#dvTourChild1').show();
                $('#dvTourChild2').show();
                $('#dvTourChild3').show();
                $('#dvTourChild4').hide();
                $('#dvTourChild5').hide();
                $('#dvTourChild6').hide();
                $('#dvTourChild7').hide();
                $('#dvTourChild8').hide();
                $('#dvTourChild').show();

            }
            else if (child == 4) {

                $('#dvTourChild1').show();
                $('#dvTourChild2').show();
                $('#dvTourChild3').show();
                $('#dvTourChild4').show();
                $('#dvTourChild5').hide();
                $('#dvTourChild6').hide();
                $('#dvTourChild7').hide();
                $('#dvTourChild8').hide();
                $('#dvTourChild').show();

            }
            else if (child == 5) {

                $('#dvTourChild1').show();
                $('#dvTourChild2').show();
                $('#dvTourChild3').show();
                $('#dvTourChild4').show();
                $('#dvTourChild5').show();
                $('#dvTourChild6').hide();
                $('#dvTourChild7').hide();
                $('#dvTourChild8').hide();
                $('#dvTourChild').show();

            }
            else if (child == 6) {

                $('#dvTourChild1').show();
                $('#dvTourChild2').show();
                $('#dvTourChild3').show();
                $('#dvTourChild4').show();
                $('#dvTourChild5').show();
                $('#dvTourChild6').show();
                $('#dvTourChild7').hide();
                $('#dvTourChild8').hide();
                $('#dvTourChild').show();

            }
            else if (child == 7) {

                $('#dvTourChild1').show();
                $('#dvTourChild2').show();
                $('#dvTourChild3').show();
                $('#dvTourChild4').show();
                $('#dvTourChild5').show();
                $('#dvTourChild6').show();
                $('#dvTourChild7').show();
                $('#dvTourChild8').hide();
                $('#dvTourChild').show();

            }
            else if (child == 8) {

                $('#dvTourChild1').show();
                $('#dvTourChild2').show();
                $('#dvTourChild3').show();
                $('#dvTourChild4').show();
                $('#dvTourChild5').show();
                $('#dvTourChild6').show();
                $('#dvTourChild7').show();
                $('#dvTourChild8').show();
                $('#dvTourChild').show();

            }

        }
    //]]>
    </script>
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestUserControl);
        function EndRequestUserControl(sender, args) {


            CallToDatePicker();

            AutoCompleteExtender_Othergroup_KeyUp();
        }
      
    </script>
    <script type="text/javascript">


        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {


        });


        function OthCountryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = '';
            }
        }



        function othserviceCustomerAutocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtothCustomercode.ClientID%>').value = eventArgs.get_value();
                $find('AutoCompleteExtender_txtothSourceCountry').set_contextKey(eventArgs.get_value());

                GetTourCountryDetails(eventArgs.get_value());
            }
            else {
                document.getElementById('<%=txtothCustomercode.ClientID%>').value = '';

            }
        }

        //        '*** Danny 22/10/2018 FreeForm SupplierAgent
        function othserviceSupplierAutocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=Txt_SupplierCode.ClientID%>').value = eventArgs.get_value();
                $find('AutoCompleteExtender_SupplierAgent').set_contextKey(eventArgs.get_value());
            }
            else {
                document.getElementById('<%=Txt_SupplierCode.ClientID%>').value = '';

            }
        }
        function othserviceSupplierAgentsAutocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=Txt_SupplierAgentCode.ClientID%>').value = eventArgs.get_value();
                $find('AutoCompleteExtender_Supplier').set_contextKey(eventArgs.get_value());
            }
            else {
                document.getElementById('<%=Txt_SupplierAgentCode.ClientID%>').value = '';

            }
        }

        function GetTourCountryDetails(CustCode) {
            $.ajax({
                type: "POST",
                url: "OtherSearch.aspx/GetCountryDetails",
                data: '{CustCode:  "' + CustCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnTourSuccess,
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

        function OnTourSuccess(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var Countries = xml.find("Countries");
            var rowCount = Countries.length;

            if (rowCount == 1) {
                $.each(Countries, function () {
                    document.getElementById('<%=txtothSourceCountry.ClientID%>').value = ''
                    document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = '';
                    document.getElementById('<%=txtothSourceCountry.ClientID%>').value = $(this).find("ctryname").text();
                    document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = $(this).find("ctrycode").text();
                    document.getElementById('<%=txtothSourceCountry.ClientID%>').setAttribute("readonly", true);
                    $find('AutoCompleteExtender_txtothSourceCountry').setAttribute("Enabled", false);
                });
            }
            else {
                document.getElementById('<%=txtothSourceCountry.ClientID%>').value = ''
                document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = '';
                document.getElementById('<%=txtothSourceCountry.ClientID%>').removeAttribute("readonly");
                $find('AutoCompleteExtender_txtothSourceCountry').setAttribute("Enabled", true);
            }
        };

        function RefreshContent() {
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandlerForProgressBar);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandlerForProgressBar);

        }
        function BeginRequestHandlerForProgressBar() {

            ShowProgess();

        }
        function EndRequestHandlerForProgressBar() {
            HideProgess();
            CallPriceSlider()

            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();

            $(".date-inpt-oth-from").datepicker({
                minDate: new Date(currentYear, currentMonth, currentDate)
            });

            //changed by mohamed on 28/08/2018
            var currentMonth = date.getMonth() - 2;
            $(".date-inpt-oth-from-freeform").datepicker({
                minDate: new Date(currentYear, currentMonth, currentDate)
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
                ModalPopupDays.hide();
            }
            return true;
        }


     
    </script>
</head>
<body onload="RefreshContent()">
    <form id="form1" runat="server">
    <!-- // authorize // -->
    <div class="overlay">
    </div>
    <!-- \\ authorize \\-->
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <header id="top">
     <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
	<div class="header-a">
		<div class="wrapper-padding">			
         <div class="header-user" style="margin-top:2px;"><asp:Label ID="lblHeaderUserName"  runat="server" ></asp:Label> </div>		
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
            <%--<asp:UpdatePanel runat="server"><ContentTemplate><asp:Button ID="btnLogOut"   UseSubmitBehavior="false" 
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
			<div id="dvCurrency" runat="server" style="margin-top:2px;" class="header-curency">
			</div>
               <div class="header-lang" id="dvMybooking" runat="server" style="padding-right:25px;margin-right:5px;">
           <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:Button ID="btnMyBooking"   UseSubmitBehavior="false" 
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
			<div class="header-logo"><a href="Home.aspx?Tab=1"><img id="imgLogo" runat="server" alt="" /></a></div>
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
                    <div class="page-title">
                        OTHER SERVICES - <span>Free Form Booking</span></div>
                    <div class="breadcrumbs">
                        <a href="Home.aspx?Tab=1">Home</a> / <a href="#">OTHER SERVICES Free Form Booking</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="page-head">
                    <div class="page-search-content-search" >
                        <div class="search-tab-content">
                            <div class="page-search-p">
                                <!-- // -->
                        
                             
                               
                                <div class="search-large-i" id="divothcustomer" style="margin-top: 10px;float: left;" runat="server">
                                    <label>
                                        Agent</label>
                                    <div class="input-a">
                                        <asp:TextBox ID="txtothCustomer" runat="server" TabIndex="101" placeholder="--"></asp:TextBox>
                                        <asp:TextBox ID="txtothCustomercode" runat="server" Style="display: none"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtothCustomer" runat="server"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                            MinimumPrefixLength="-1" ServiceMethod="GetCustomers" TargetControlID="txtothCustomer"
                                            OnClientItemSelected="othserviceCustomerAutocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="search-large-i" style="float: left; margin-top:10px;">
                                    <label>
                                        Source Country</label>
                                 <div class="input-a">
                                        <asp:TextBox ID="txtothSourceCountry"  TabIndex="102"  runat="server" placeholder="--"></asp:TextBox>
                                        <asp:TextBox ID="txtothSourceCountryCode" runat="server" Style="display: none"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtothSourceCountry" runat="server"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                            MinimumPrefixLength="-1" ServiceMethod="GetotherCountry" TargetControlID="txtothSourceCountry"
                                            UseContextKey="true"   OnClientItemSelected="OthCountryautocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <!-- \\ -->
                                </div>
                                  <div class="search-large-i" style="margin-top:10px;">
                                    <label>
                                         Select Service Group</label>
                                          <div class="input-a">
                                        <asp:TextBox ID="txtothgroup" placeholder="--"  TabIndex="103"  runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtothgroupcode"  style="display:none" placeholder="--"
                                            runat="server"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtothgroup" runat="server"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="true"
                                            MinimumPrefixLength="-1" ServiceMethod="GetOtherservicegroup" TargetControlID="txtothgroup"
                                            OnClientItemSelected="othgroupautocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>

                                  
                                </div>
                                <div class="clear">
                                </div>
                             
                         
                              
               
                                <div class="clear">
                                </div>

                                <div class="search-large-i" style="float: left; margin-top: 20px;">
                                    <!-- // -->
                                <div class="srch-tab-line no-margin-bottom">
                                        <div class="srch-tab-3c" style="width:39%;float:left;">
                                                 <label>
                                                Service Date</label>
                                            <div class="input-a">
                                   
                                                <asp:TextBox ID="txtothFromDate" runat="server"  TabIndex="104"  placeholder="dd/mm/yyyy" CssClass="date-inpt-oth-from-freeform"></asp:TextBox>
                                                <span class="date-icon-oth"></span>
                                            </div>
                                        </div>
                                  
                                 
                                        <div class="srch-tab-3c" style="width:23%;float:left;">
                                            <label>
                                                adult</label>
                                            <div class="select-wrapper">
                                                <asp:DropDownList ID="ddlTourAdult"  TabIndex="105"  class="custom-select custom-select-ddlTourAdult"
                                                    runat="server">
                                           
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                
                                        <div class="srch-tab-3c" style="width:23%;float:left;">
                                            <label>
                                                child</label>
                                            <div class="select-wrapper">
                                                <asp:DropDownList ID="ddlTourChildren"  TabIndex="106"  class="custom-select custom-select-ddlTourChildren"
                                                    runat="server">
                                             
                                                </asp:DropDownList>
                                            </div>
                                     
                                    </div>
                                    </div>
                                </div>
                                
                             
                                <div id="dvTourChild" runat="server" style="margin-top: 20px;display:none;">
                                    <div class="search-large-i-child-tour" style="float: left;">
                                        <label style="text-align: left; padding-right: 2px;">
                                            Ages of children at Other Services</label>
                                                                                                               
                                        <div class="srch-tab-child" id="dvTourChild1" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                             <div class="srch-tab-child-pre" id="divPreHotelChild1">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild1" placeholder="CH 1" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                              <%--  <asp:DropDownList ID="ddlTourChild1" Width="100%" class="custom-select custom-select-ddlTourChild1"
                                                    runat="server">
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
                                                </asp:DropDownList>--%>
                                            </div>
                                        </div>
                                         <div class="srch-tab-child" id="dvTourChild2" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                               <div class="srch-tab-child-pre" id="div1">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild2" placeholder="CH 2" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="srch-tab-child" id="dvTourChild3" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                  <div class="srch-tab-child-pre" id="div2">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild3" placeholder="CH 3" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="srch-tab-child" id="dvTourChild4" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                 <div class="srch-tab-child-pre" id="div3">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild4" placeholder="CH 4" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="search-large-i-child-tour">
                                        <label style="color: White;">
                                            Ages of children at Other Services</label>
                                        <div class="srch-tab-child" id="dvTourChild5" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="div4">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild5" placeholder="CH 5" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="srch-tab-child" id="dvTourChild6" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                               <div class="srch-tab-child-pre" id="div6">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild6" placeholder="CH 6" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="srch-tab-child" id="dvTourChild7" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                   <div class="srch-tab-child-pre" id="div7">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild7" placeholder="CH 7" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                           <div class="srch-tab-child" id="dvTourChild8" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                               <div class="srch-tab-child-pre" id="div8">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild8" placeholder="CH 8" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                 <div class="clear">
                                                    </div>
                                                    <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <label>
                                                                Service Name</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtServiceName" onkeydown="SetServiceNameContextKey()"   TabIndex="107"  runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtServiceNameCode"   runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtenderServiceName" runat="server" CompletionInterval="10"
                                                                    CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"  UseContextKey="true"  
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetOtherServiceName" TargetControlID="txtServiceName"
                                                                    OnClientItemSelected="ServiceNameautocompleteselected">
                                                                </asp:AutoCompleteExtender>

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="clear">
                                                    </div>
                                                    <%--'*** Danny 22/10/2018 FreeForm SupplierAgent--%>
                                                   <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                      <div class="srch-tab-line no-margin-bottom">
                                                         <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4" id="div5" runat="server">
                                                            <label>
                                                            Supplier</label>
                                                            <div class="input-a">
                                                               <asp:TextBox ID="Txt_SupplierName" runat="server" class="form-control" onkeyup="AutoCompleteExtender_OthCustomer_KeyUp()"  TabIndex="101" placeholder="--"></asp:TextBox>
                                                               <asp:TextBox ID="Txt_SupplierCode" runat="server" Style="display: none"></asp:TextBox>
                                                               <asp:AutoCompleteExtender ID="AutoCompleteExtender_Supplier" runat="server"
                                                                  CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                  CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                  DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                  MinimumPrefixLength="-1" ServiceMethod="GetSuppliers" TargetControlID="Txt_SupplierName"
                                                                  OnClientItemSelected="othserviceSupplierAutocompleteselected">
                                                               </asp:AutoCompleteExtender>
                                                            </div>
                                                         </div>
                                                         <div class="srch-tab-line no-margin-bottom">
                                                            <label>
                                                            Supplier Agent</label>
                                                            <div class="input-a">
                                                               <asp:TextBox ID="Txt_SupplierAgentName"  TabIndex="102"  class="form-control" onkeydown="SourceCountrykeyDown()"  runat="server" placeholder="--"></asp:TextBox>
                                                               <asp:TextBox ID="Txt_SupplierAgentCode" runat="server" Style="display: none"></asp:TextBox>
                                                               <asp:AutoCompleteExtender ID="AutoCompleteExtender_SupplierAgent" runat="server"
                                                                  CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                  CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                  DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                  MinimumPrefixLength="-1" ServiceMethod="GetSuppliersAgents" TargetControlID="Txt_SupplierAgentName"
                                                                  UseContextKey="true"   OnClientItemSelected="othserviceSupplierAgentsAutocompleteselected">
                                                               </asp:AutoCompleteExtender>
                                                            </div>
                                                            <!-- \\ -->
                                                         </div>
                                                      </div>
                                                   </div>

                                                    <div class="search-large-i" style="float: left; padding-top: 20px; padding-left:30px;">
                                                        <!-- // -->
                                                           <div class="srch-tab-3c">
                                                            <label>
                                                                No of Unit</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtNoOfUnit"   autocomplete="off" onchange="calculatetotalvaluewithcost()"   TabIndex="108"   onkeypress="validateDecimalOnly(event,this)"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Unit Price</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtUnitPrice"   autocomplete="off"  onchange="calculatetotalvaluewithcost()"    TabIndex="109"   onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                     
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Total</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtTotal"    autocomplete="off"    onkeydown="fnReadOnly(this)" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                        <div class="search-large-i" style="float: left; padding-top: 20px;width:28.5% !important;">
                                                        <!-- // -->
                                                        <div class="srch-tab-3c">
                                                            <%--'*** Danny 22/10/2018 FreeForm SupplierAgent--%>
                                                            <label>
                                                                <asp:Label runat="server" ID="lblCostPrice" Text="Cost Price(---)"></asp:Label>
                                                            </label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtCostPricePax" autocomplete="off"  onchange="calculatetotalvaluewithcost()"    TabIndex="110"  onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                        <div class="srch-tab-3c" style="width: 30.6%;">
                                                            <%--'*** Danny 22/10/2018 FreeForm SupplierAgent--%>
                                                            <label>
                                                                <asp:Label runat="server" ID="lblCostTotal" Text="Total Cost Price(---)"></asp:Label>
                                                            </label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtCostPricePaxTotal"  autocomplete="off"  onchange="calculatetotalvaluewithcost()"   onkeydown="fnReadOnly(this)"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    
                                                    </div>
                                                                                    <div class="clear" ></div>

                                                                                               <div style="float: left; padding-top: 20px; padding-bottom: 20px;">
                                                      
                                                            <div style="float: left; ">
                                                                <asp:CheckBox ID="chkComplSup" runat="server" />
                                                            </div>
                                                            <div style="padding-left:20px;float: left;padding-top:2px; ">
                                                                <label>
                                                                    Complimentary Customer</label>
                                                            </div>
                                                            
                                                    </div>
                                <asp:HiddenField ID="hdCheckInPrevDay" runat="server" />
                                <asp:HiddenField ID="hdCheckOutNextDay" runat="server" />
                                <!-- \\ -->
                                <div class="clear">
                                </div>
                            </div>
                            <footer class="search-footer">
                                       <div class="search-large-i">
						     <div  class="srch-tab-left" >
                          <asp:Button ID="btnBook" class="authorize-btn"   OnClientClick="return ValidateSearch()"   runat="server" Text="Book"></asp:Button>  
                          </div>
                           <div  class="srch-tab-left">
                      <input  id="btnTourReset"  type="button"  class="srch-btn-home"  value="Reset"/>
                        </div></div>
						<div class="clear"></div>
					</footer>
                        </div>
                    </div>
                </div>
          
                <div class="clear">
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
            //  alert('test');
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
            var curcode = $("#hdSliderCurrency").val();
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

        function calculatevalue(lblunits, txtunitprice, lbltotalvalue) {

            txtunits = document.getElementById(lblunits);
            txtunitprice = document.getElementById(txtunitprice);
            txttotalvalue = document.getElementById(lbltotalvalue);

            var totalamt = (parseFloat(txtunitprice.value) * parseFloat(txtunits.value));
            txttotalvalue.value = totalamt;

        }

    </script>
    <!-- \\ scripts \\ -->
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
     <asp:HiddenField ID="hddlRowNumber" runat="server" />
        <asp:HiddenField ID="hdTab" runat="server" />
    <asp:CheckBox runat="server" AutoPostBack ="true"  ID="chkSelectTour1"  />
      <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
      <asp:HiddenField ID="hdWhiteLabel" runat="server" />
        <asp:HiddenField ID="hdSliderCurrency" runat="server" />
     <asp:HiddenField ID="hdnLineno" runat="server" />
  <asp:HiddenField ID="hdnreset" runat="server" />
          <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
    </form>
</body>
</html>
