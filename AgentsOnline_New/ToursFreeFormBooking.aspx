<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ToursFreeFormBooking.aspx.vb"
    Inherits="TourSearch" %>

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
<%--    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">--%>

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
  <%--  <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"
        type="text/javascript"></script>--%>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            // Restrict mouse right button..
            $(document).bind("contextmenu", function (e) {
                return false;
            });
            ShowHideTourType();
            // Restrict browser back button..
            history.pushState(null, null, document.URL);
            window.addEventListener('popstate', function () {
                history.pushState(null, null, document.URL);
            });
        });    
    </script>
    <script language="javascript" type="text/javascript">


        function findMultiCostTotal(lblAdult, txtPeradult, lblAdultCost, lblChild, txtPerchild, lblchildcost, lblnoofseniors, txtPersenior, lblSeniorCost, txtUnitcost, lbltotalcost, hdAdultCost, hdChildcost, hdSeniorCost, hdTotalcost) {
      
   
            var ValueAdult = parseFloat(0);
            var ValueChild = parseFloat(0);
            var ValueUnits = parseFloat(0);
            var ValueSenior = parseFloat(0);

            var CostValueAdult = parseFloat(0);
            var CostValueChild = parseFloat(0);
            var CostValueUnits = parseFloat(0);
            var CostValueSenior = parseFloat(0);
            var CostValueChildAsAdult = parseFloat(0);
            var TotalCostValue = parseFloat(0);

            if (document.getElementById(lblAdult).innerHTML != '0' || document.getElementById(lblAdult).innerHTML != '') {
                ValueAdult = parseFloat(document.getElementById(txtPeradult).value);

                CostValueAdult = parseFloat(document.getElementById(lblAdult).innerHTML) * parseFloat(ValueAdult);
                document.getElementById(lblAdultCost).value = CostValueAdult;
                document.getElementById(hdAdultCost).value = CostValueAdult;
            }

            if (document.getElementById(lblChild).innerHTML != '0' || document.getElementById(lblChild).innerHTML != '') {
                ValueChild = parseFloat(document.getElementById(txtPerchild).value);
                CostValueChild = parseFloat(document.getElementById(lblChild).innerHTML) * parseFloat(ValueChild);
                document.getElementById(lblchildcost).value = CostValueChild;
                document.getElementById(hdChildcost).value = CostValueChild;
            }

            if (document.getElementById(lblnoofseniors).innerHTML != '0' || document.getElementById(lblnoofseniors).innerHTML != '') {
                ValueSenior = parseFloat(document.getElementById(txtPersenior).value);
                CostValueSenior = parseFloat(document.getElementById(lblnoofseniors).innerHTML) * parseFloat(ValueSenior);
                document.getElementById(lblSeniorCost).value = CostValueSenior;
                document.getElementById(hdSeniorCost).value = CostValueSenior;
            }
            if (document.getElementById(txtUnitcost).innerHTML != '0' || document.getElementById(txtUnitcost).innerHTML != '') {
                ValueSenior = parseFloat(document.getElementById(txtUnitcost).value);
                CostValueUnits = ValueSenior;

            }



            TotalCostValue = CostValueAdult + CostValueChild + CostValueSenior + CostValueUnits;

            document.getElementById(lbltotalcost).value = TotalCostValue;
            document.getElementById(hdTotalcost).value = TotalCostValue;
        }



        function SetContextKey(ch) {
            //       *** Combining value and Stored Procedure parametername
            //       *** First value in the array is the count of the texbox and arrange the text box exactly in the same position.

            var s = ch.toString() + '@ch|';
            if (ch != 1) {
                s = s + $get("<%=txtTourClassification.ClientID%>").value + '@Classification|';
            }
            else {
                s = s + '@Classification|';
            }
            if (ch != 2) {
                s = s + $get("<%=ddlStarCategory.ClientID%>").value + '@StarCategory|';
            }
            else {
                s = s + '@StarCategory|';
            }
            if (ch != 3) {
                s = s + $get("<%=Txt_TourCategory.ClientID%>").value + '@CategoryName|';
            }
            else {
                s = s + '@CategoryName|';
            }
            if (ch != 4) {
                s = s + $get("<%=Txt_Tours.ClientID%>").value + '@Tours|';
            }
            else {
                s = s + '@Tours|';
            }
            if (ch != 5) {
                s = s + $get("<%=Txt_TourType.ClientID%>").value + '@ToursType|';
            }
            else {

                s = s + '@ToursType|';
            }




            if (ch == '1') {

                $find('<%=AutoCompleteExtender_txtTourClassification.ClientID%>').set_contextKey(s);

            }
            if (ch == '3') {

                $find('<%=AutoCompleteExtender_Txt_TourCategory.ClientID%>').set_contextKey(s);

            }
            if (ch == '4') {
                $find('<%=AutoCompleteExtender_Txt_Tours.ClientID%>').set_contextKey(s);
            }
            if (ch == '5') {

                $find('<%=AutoCompleteExtender_Txt_TourType.ClientID%>').set_contextKey(s);
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

    </script>
    <script language="javascript" type="text/javascript">

        function chkdlDateCheck(item) {

            if (item.checked == true) {
                alert(item);
            }
        }

        function totalcalculate() {

            var SaleValueUnits = parseFloat(0);
            var SaleValueSenior = parseFloat(0);
            var SaleValueAdult = parseFloat(0);
            var SaleValueChild = parseFloat(0);
            var SaleValueChildAsAdult = parseFloat(0);
            var TotalSaleValue = parseFloat(0);

            if (document.getElementById('<%=Txt_SaleValueUnits.ClientID%>').value != '') {
                SaleValueUnits = parseFloat(document.getElementById('<%=Txt_SaleValueUnits.ClientID%>').value);
            }
            if (document.getElementById('<%=Txt_SaleValueSenior.ClientID%>').value != '') {
                SaleValueSenior = parseFloat(document.getElementById('<%=Txt_SaleValueSenior.ClientID%>').value);
            }
            if (document.getElementById('<%=Txt_SaleValueAdult.ClientID%>').value != '') {
                SaleValueAdult = parseFloat(document.getElementById('<%=Txt_SaleValueAdult.ClientID%>').value);
            }
            if (document.getElementById('<%=Txt_SaleValueChild.ClientID%>').value != '') {
                SaleValueChild = parseFloat(document.getElementById('<%=Txt_SaleValueChild.ClientID%>').value);
            }
            if (document.getElementById('<%=Txt_SaleValueChildAsAdult.ClientID%>').value != '') {
                SaleValueChildAsAdult = parseFloat(document.getElementById('<%=Txt_SaleValueChildAsAdult.ClientID%>').value);
            }


            var TotalSaleValue = parseFloat(SaleValueUnits) + parseFloat(SaleValueSenior) + parseFloat(SaleValueAdult) + parseFloat(SaleValueChild) + parseFloat(SaleValueChildAsAdult)

            //            alert(SaleValueUnits);
            //            alert(SaleValueSenior);
            //            alert(SaleValueAdult);
            //            alert(SaleValueChild);
            //            alert(SaleValueChildAsAdult);
            //            alert(TotalSaleValue);

            document.getElementById('<%=Txt_TotalSaleValue.ClientID%>').value = parseFloat(TotalSaleValue);


        }

        function totalCostcalculate() {

            var SaleValueUnits = parseFloat(0);
            var SaleValueSenior = parseFloat(0);
            var SaleValueAdult = parseFloat(0);
            var SaleValueChild = parseFloat(0);
            var SaleValueChildAsAdult = parseFloat(0);
            var TotalSaleValue = parseFloat(0);

            if (document.getElementById('<%=Txt_CostSaleValueUnits.ClientID%>').value != '') {
                SaleValueUnits = parseFloat(document.getElementById('<%=Txt_CostSaleValueUnits.ClientID%>').value);
            }
            if (document.getElementById('<%=Txt_CostSaleValueSenior.ClientID%>').value != '') {
                SaleValueSenior = parseFloat(document.getElementById('<%=Txt_CostSaleValueSenior.ClientID%>').value);
            }
            if (document.getElementById('<%=Txt_CostSaleValueAdult.ClientID%>').value != '') {
                SaleValueAdult = parseFloat(document.getElementById('<%=Txt_CostSaleValueAdult.ClientID%>').value);
            }
            if (document.getElementById('<%=Txt_CostSaleValueChild.ClientID%>').value != '') {
                SaleValueChild = parseFloat(document.getElementById('<%=Txt_CostSaleValueChild.ClientID%>').value);
            }
            if (document.getElementById('<%=Txt_CostSaleValueChildAsAdult.ClientID%>').value != '') {
                SaleValueChildAsAdult = parseFloat(document.getElementById('<%=Txt_CostSaleValueChildAsAdult.ClientID%>').value);
            }


            var TotalSaleValue = parseFloat(SaleValueUnits) + parseFloat(SaleValueSenior) + parseFloat(SaleValueAdult) + parseFloat(SaleValueChild) + parseFloat(SaleValueChildAsAdult)



            document.getElementById('<%=Txt_TotalCostValue.ClientID%>').value = parseFloat(TotalSaleValue);


        }

        function CalculateSaleValue(lblNoOfAdult, txtPrice, lblSaleValue, ch) {


            //            var ch1 = document.getElementById(ch);
            var lblNoOfAdult1 = document.getElementById(lblNoOfAdult);
            var txtPrice1 = document.getElementById(txtPrice);
            var lblSaleValue1 = document.getElementById(lblSaleValue);

            if (parseFloat(lblNoOfAdult1.value) > 0) {
                if (parseFloat(txtPrice1.value) > 0) {
                    var totalamt = (parseFloat(txtPrice1.value) * parseFloat(lblNoOfAdult1.value));
                    lblSaleValue1.value = totalamt;

                    if (ch == 1) {
                        document.getElementById('<%=Txt_PriceChildAsAdult.ClientID%>').value = document.getElementById('<%=Txt_PriceAdult.ClientID%>').value;
                        CalculateSaleValue('<%=Txt_NoOfChildAsAdult.ClientID%>', '<%=Txt_PriceChildAsAdult.ClientID%>', '<%=Txt_SaleValueChildAsAdult.ClientID%>', '0');

                    }
                    if (ch == 2) {
                        document.getElementById('<%=Txt_PriceAdult.ClientID%>').value = document.getElementById('<%=Txt_PriceChildAsAdult.ClientID%>').value;
                        CalculateSaleValue('<%=Txt_NoOfAdults.ClientID%>', '<%=Txt_PriceAdult.ClientID%>', '<%=Txt_SaleValueAdult.ClientID%>', '0');

                    }

                }
                else {
                    lblSaleValue1.value = 0
                }
            }
            else {
                lblSaleValue1.value = 0

            }
            totalcalculate();
        }

        function CalculateCostValue(lblNoOfAdult, txtPrice, lblSaleValue, ch) {


            //            var ch1 = document.getElementById(ch);
            var lblNoOfAdult1 = document.getElementById(lblNoOfAdult);
            var txtPrice1 = document.getElementById(txtPrice);
            var lblSaleValue1 = document.getElementById(lblSaleValue);

            if (parseFloat(lblNoOfAdult1.value) > 0) {
                if (parseFloat(txtPrice1.value) > 0) {
                    var totalamt = (parseFloat(txtPrice1.value) * parseFloat(lblNoOfAdult1.value));
                    lblSaleValue1.value = totalamt;

                    if (ch == 1) {
                        document.getElementById('<%=Txt_CostPriceChildAsAdult.ClientID%>').value = document.getElementById('<%=Txt_CostPriceAdult.ClientID%>').value;
                        ///   CalculateSaleValue('<%=Txt_CostNoOfChildAsAdult.ClientID%>', '<%=Txt_CostPriceChildAsAdult.ClientID%>', '<%=Txt_SaleValueChildAsAdult.ClientID%>', '0');

                    }
                    if (ch == 2) {
                        document.getElementById('<%=Txt_CostPriceAdult.ClientID%>').value = document.getElementById('<%=Txt_CostPriceChildAsAdult.ClientID%>').value;
                        ///  CalculateSaleValue('<%=Txt_CostNoOfAdults.ClientID%>', '<%=Txt_CostPriceAdult.ClientID%>', '<%=Txt_CostSaleValueAdult.ClientID%>', '0');

                    }

                }
                else {
                    lblSaleValue1.value = 0
                }
            }
            else {
                lblSaleValue1.value = 0

            }
            totalCostcalculate();
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



        function CallSuccess() {
            var msg = '';
            msg = 'Original booking date is not in the new date range selected, do you want to continue?';

            if (confirm(msg)) {

                return true;
            }
            else {

                return false;
            }

        }

        function CallFailed() {
            return false;
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

        function ValidateTourSearch() {
            ShowProgess();
            if (document.getElementById('<%=txtTourStartingFromCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter TOUR - PICK UP LOCATION.', 'warning');
                HideProgess();
                return false;
            }
            if ((document.getElementById('<%=ddlTourAdult.ClientID%>').value == '0') && (document.getElementById('<%=ddlSeniorCitizen.ClientID%>').value == '0')) {
                showDialog('Alert Message', 'Please enter Adult Or Senior citizen.', 'warning');
                HideProgess();
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
            if (document.getElementById('<%=txtTourCustomer.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select Agent', 'warning');
                HideProgess();
                return false;
            }
            if (document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter tour source country.', 'warning');
                HideProgess();
                return false;
            }
            if (document.getElementById('<%=Txt_ToursCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select TOUR', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=Txt_TourTypeCode.ClientID%>').value == 'NORMAL') {
                if (document.getElementById('<%=Txt_TourDate.ClientID%>').value == '') {
                    showDialog('Alert Message', 'Please select TOUR DATE.', 'warning');
                    HideProgess();
                    return false;
                }

            }
            else if (document.getElementById('<%=Txt_TourTypeCode.ClientID%>').value == 'MULTIPLE DATE') {
                if (document.getElementById('<%=txtTourfromDate.ClientID%>').value == '') {
                    showDialog('Alert Message', 'Please select any from-date.', 'warning');
                    HideProgess();
                    return false;
                }
                if (document.getElementById('<%=txtTourToDate.ClientID%>').value == '') {
                    showDialog('Alert Message', 'Please select any to-date.', 'warning');
                    HideProgess();
                    return false;
                }
            }






            var fromdate = document.getElementById('<%=txtTourfromDate.ClientID%>').value;
            var todate = document.getElementById('<%=txtTourToDate.ClientID%>').value;


            var orgfromdate = document.getElementById('<%=hdChangeFromdate.ClientID%>').value;
            var orgtodate = document.getElementById('<%=hdChangeTodate.ClientID%>').value;
            var lineno = document.getElementById('<%=hdnlineno.ClientID%>').value;


            if ((fromdate != '') && (todate != '') && (lineno != '0')) {

                var dp = fromdate.split("/");
                var newChkInDt = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);

                var dp1 = todate.split("/");
                var newChkOutDt = new Date(dp1[2] + "/" + dp1[1] + "/" + dp1[0]);

                newChkInDt = getFormatedDate(newChkInDt);
                newChkOutDt = getFormatedDate(newChkOutDt);

                newChkInDt = new Date(newChkInDt);
                newChkOutDt = new Date(newChkOutDt);

                var orgdp = orgfromdate.split("/");
                var orgnewChkInDt = new Date(orgdp[2] + "/" + orgdp[1] + "/" + orgdp[0]);

                var orgdp1 = orgtodate.split("/");
                var orgnewChkOutDt = new Date(orgdp1[2] + "/" + orgdp1[1] + "/" + orgdp1[0]);

                orgnewChkInDt = getFormatedDate(orgnewChkInDt);
                orgnewChkOutDt = getFormatedDate(orgnewChkOutDt);

                orgnewChkInDt = new Date(orgnewChkInDt);
                orgnewChkOutDt = new Date(orgnewChkOutDt);


                if (newChkInDt < orgnewChkInDt || newChkOutDt > orgnewChkOutDt) {

                    return CallSuccess();
                }

                else {

                    return true;
                }
            }



            if (document.getElementById('<%=Txt_TourCategoryCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter TOUR - CATEGORY', 'warning');
                HideProgess();
                return false;
            }






            if (document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value == '0') {
                showDialog('Alert Message', 'Please select any source country.', 'warning');
                return false;
            }


            if (ValidateCheckBoxList() == false) {
                showDialog('Alert Message', 'Please select any item (PRIVATE or SIC or WITHOUT TRANSFERS).', 'warning');
                HideProgess();
                return false;
            }

            return true;

        }

        function ValidateCheckBoxList() {

            var listItems = document.getElementById("chklPrivateOrSIC").getElementsByTagName("input");
            var itemcount = listItems.length;
            var iCount = 0;
            var isItemSelected = false;
            for (iCount = 0; iCount < itemcount; iCount++) {
                if (listItems[iCount].checked) {
                    isItemSelected = true;
                    break;
                }
            }
            if (!isItemSelected) {

                return false;
            }
            else {
                return true;
            }
            return false;
        }

    </script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            ShowTourChild();
            $("#<%= ddlTourChildren.ClientID %>").bind("change", function () {
                ShowTourChild();
            });

            CallPriceSlider()

            

            AutoCompleteExtender_TourStartingFrom_KeyUp();
            AutoCompleteExtender_txtTourClassification_KeyUp();
            AutoCompleteExtender_TourCustomer_KeyUp();
            AutoCompleteExtender_TourCountry_KeyUp();

            fillTourDates();



           if (document.getElementById('<%=txtTourFromDate.ClientID%>').value != '') {

                BindToDateCalendar();
            }

                  function BindToDateCalendar() {
                   var d = document.getElementById('<%=txtTourFromDate.ClientID%>').value;

                var dp = d.split("/");
//                var dateOut = new Date(dp[2], dp[1], dp[0]);
//                var currentMonth = dateOut.getMonth() - 1;
                   var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;

                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-tour-to").datepicker("destroy");

                var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
                if ((dCheckOutMax == '') || (dCheckOutMax == '0')) {
                    $(".date-inpt-tour-to").datepicker({
                        minDate: new Date(currentYear, currentMonth, currentDate)
                    });
                }
                else {
                    var dp4 = dCheckOutMax.split("/");
                    var date4 = new Date(dp4[2], dp4[1], dp4[0]);
                    var currentMonth4 = date4.getMonth() - 1;
                    var currentDate4 = date4.getDate();
                    var currentYear4 = date4.getFullYear();

                    $(".date-inpt-tour-to").datepicker({
                        minDate: new Date(currentYear, currentMonth, currentDate),
                        maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                    });
                }
                  }

            $("#<%= txtTourFromDate.ClientID %>").bind("change", function () {
               
                BindToDateCalendar();
            });


            var dfdate = document.getElementById('<%=hdChangeFromdate.ClientID%>').value;
            var dpFdate = dfdate.split("/");
            var FdateOut = new Date(dpFdate[2], dpFdate[1], dpFdate[0]);
            var fcurrentMonth = FdateOut.getMonth() - 1;
            var fcurrentDate = FdateOut.getDate();
            var fcurrentYear = FdateOut.getFullYear();

            var dTdate = document.getElementById('<%=hdChangeTodate.ClientID%>').value;
            var dpTdate = dTdate.split("/");
            var TdateOut = new Date(dpTdate[2], dpTdate[1], dpTdate[0]);
            var tcurrentMonth = TdateOut.getMonth() - 1;
            var tcurrentDate = TdateOut.getDate();
            var tcurrentYear = TdateOut.getFullYear();



//            Added shahul date issue 31 28/07/18
               if(dpFdate[0] =='31') {
                 fcurrentDate=fcurrentDate-1;
                 }


              $(".date-inpt-tour-change-date").datepicker({
               
                minDate: new Date(fcurrentYear, fcurrentMonth, fcurrentDate),
                maxDate: new Date(tcurrentYear, tcurrentMonth, tcurrentDate),
             
               });


        
                

//                minDate: new Date(fcurrentYear, fcurrentMonth+1, fcurrentDate),
//                maxDate: new Date(tcurrentYear, tcurrentMonth+1, tcurrentDate)
           

            $("#<%= ddlSeniorCitizen.ClientID %>").bind("change", function () {

                var adult = $("#<%= ddlTourAdult.ClientID %>").val()
                var SeniorCitizen = $("#<%= ddlSeniorCitizen.ClientID %>").val()
                var Tab = $("#<%= hdTab.ClientID %>").val();
                if (Tab == 1) {
                    if (SeniorCitizen != '0') {
                        if (adult > 0) {
                            if (adult >= SeniorCitizen) {
                                document.getElementById('<%=ddlTourAdult.ClientID%>').value = '1';
                                $('.custom-select-ddlTourAdult').next('span').children('.customSelectInner').text(adult - SeniorCitizen);
                            }

                        }
                    }
                    else {
                        $('.custom-select-ddlTourAdult').next('span').children('.customSelectInner').text(adult);
                    }
                }
            });

            $("#btnTourReset").button().click(function () {

                document.getElementById('<%=txtTourFromDate.ClientID%>').value = '';
                document.getElementById('<%=txtTourToDate.ClientID%>').value = '';
                document.getElementById('<%=txtTourStartingFrom.ClientID%>').value = '';
                document.getElementById('<%=txtTourStartingFromCode.ClientID%>').value = '';
                document.getElementById('<%=txtTourClassification.ClientID%>').value = '';
                document.getElementById('<%=Txt_TourCategory.ClientID%>').value = '';
                document.getElementById('<%=Txt_TourCategoryCode.ClientID%>').value = '';
                document.getElementById('<%=Txt_Tours.ClientID%>').value = '';
                document.getElementById('<%=Txt_ToursCode.ClientID%>').value = '';
                document.getElementById('<%=Txt_TourType.ClientID%>').value = '';
                document.getElementById('<%=Txt_TourTypeCode.ClientID%>').value = '';
                
                document.getElementById('<%=txtTourClassificationCode.ClientID%>').value = '';
                var ddlStarCategory = document.getElementById('<%=ddlStarCategory.ClientID%>');
                ddlStarCategory.selectedIndex = "0";
                $('.custom-select-ddlStarCategory').next('span').children('.customSelectInner').text(jQuery("#ddlStarCategory :selected").text());

                //                document.getElementById('<%=txtTourCustomer.ClientID%>').value = '';
                //                document.getElementById('<%=txtTourCustomerCode.ClientID%>').value = '';
                //                document.getElementById('<%=txtTourSourceCountry.ClientID%>').value = '';
                //                document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = '';

                var ddlSeniorCitizen = document.getElementById('<%=ddlSeniorCitizen.ClientID%>');
                ddlSeniorCitizen.selectedIndex = "0";
                $('.custom-select-ddlSeniorCitizen').next('span').children('.customSelectInner').text(jQuery("#ddlSeniorCitizen :selected").text());

                var ddltourAdult = document.getElementById('<%=ddltourAdult.ClientID%>');
                ddltourAdult.selectedIndex = "0";
                $('.custom-select-ddlTourAdult').next('span').children('.customSelectInner').text(jQuery("#ddltourAdult :selected").text());

                var ddlTourChildren = document.getElementById('<%=ddlTourChildren.ClientID%>');
                ddlTourChildren.selectedIndex = "0";
                $('.custom-select-ddlTourChildren').next('span').children('.customSelectInner').text(jQuery("#ddlTourChildren :selected").text());

                document.getElementById('<%=txtTourChild1.ClientID%>').value='';
                document.getElementById('<%=txtTourChild2.ClientID%>').value='';
                document.getElementById('<%=txtTourChild3.ClientID%>').value='';
                document.getElementById('<%=txtTourChild4.ClientID%>').value='';
                document.getElementById('<%=txtTourChild5.ClientID%>').value='';
                document.getElementById('<%=txtTourChild6.ClientID%>').value='';
                document.getElementById('<%=txtTourChild7.ClientID%>').value='';
                document.getElementById('<%=txtTourChild8.ClientID%>').value='';

                document.getElementById('<%=txtTourCustomer.ClientID%>').value='';
                document.getElementById('<%=txtTourCustomerCode.ClientID%>').value='';

            
                $('#dvTourChild').hide();
                $('#fromto').hide();
                $('#tourdate').hide();

                 $('#divSelectComboDates').hide();
                 $('#divSelectMultipleDates').hide();
                  $('#divSelectNormal').hide();

                 $('#divAdult').hide();
                 $('#divChild').hide();
                 $('#divSenior').hide();
                 $('#divUnits').hide();
                 $('#divChildAsAdult').hide();
                 $('#divpsenior').hide();
                 $('#divpTotal').hide();


                    $('#divCostAdult').hide();
                 $('#divCostChild').hide();
                 $('#divCostSenior').hide();
                 $('#divCostUnits').hide();
                 $('#divCostChildAsAdult').hide();
                 $('#divCostArea').hide();
                 $('#divCostpTotal').hide();

                  
                 $('#divComplimentary').hide();
                 $('#divTotalSale').hide();
                 
                $('#btnSaveMultipleDates').hide();
                  
                 document.getElementById('<%=txtTourSourceCountry.ClientID%>').value = ''
           
                 $('#Txt_PriceCancel').hide();
                  $('#btnSaveComboExcursion').hide();

                 


               
                 $('#dlSelectComboDates').remove();
                  $('#dlMultipleDate').remove();
            });

            function AutoCompleteExtender_Txt_TourCategory_KeyUp() {

                $("#<%= Txt_TourCategory.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=Txt_TourCategory.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=Txt_TourCategoryCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= Txt_TourCategory.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=Txt_TourCategory.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=Txt_TourCategoryCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

            }
            
            function AutoCompleteExtender_Txt_TourType_KeyUp() {

                $("#<%= Txt_TourType.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=Txt_TourType.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=Txt_TourTypeCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= Txt_TourType.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=Txt_TourType.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=Txt_TourTypeCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

            }
             function AutoCompleteExtender_Txt_Tours_KeyUp() {

                $("#<%= Txt_Tours.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=Txt_Tours.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=Txt_ToursCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= Txt_Tours.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=Txt_Tours.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=Txt_ToursCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

            }
            function AutoCompleteExtender_TourStartingFrom_KeyUp() {

                $("#<%= txtTourStartingFrom.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtTourStartingFrom.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtTourStartingFromCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= txtTourStartingFrom.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtTourStartingFrom.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtTourStartingFromCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

            }

            function AutoCompleteExtender_TourCountry_KeyUp() {

                $("#<%= txtTourSourceCountry.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtTourSourceCountry.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtTourSourceCountryCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= txtTourSourceCountry.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtTourSourceCountry.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtTourSourceCountryCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

            }
            

            function AutoCompleteExtender_txtTourClassification_KeyUp() {

                $("#<%= txtTourClassification.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtTourClassification.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtTourClassificationCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= txtTourClassification.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtTourClassification.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtTourClassificationCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

            }
            function AutoCompleteExtender_TourCustomer_KeyUp() {

                $("#<%= txtTourCustomer.ClientID %>").bind("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtTourCustomer.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtTourCustomerCode.ClientID%>');
                    if (hiddenfieldID1.value == '') {
                        hiddenfieldID.value = '';
                    }
                });

                $("#<%= txtTourCustomer.ClientID %>").keyup("change", function () {
                    var hiddenfieldID1 = document.getElementById('<%=txtTourCustomer.ClientID%>');
                    var hiddenfieldID = document.getElementById('<%=txtTourCustomerCode.ClientID%>');
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



        function fillTourDates() {
            var dCheckInMin = document.getElementById('<%=hdCheckInPrevDay.ClientID%>').value;
            var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
            if ((dCheckInMin == '') || (dCheckInMin == '0') || (dCheckOutMax == '') || (dCheckOutMax == '0')) {

                var date = new Date();
                var currentMonth = date.getMonth();
                var currentDate = date.getDate();
                var currentYear = date.getFullYear();

                $(".date-inpt-tour-from").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });

                //changed by mohamed on 28/08/2018
                var currentMonth = date.getMonth() - 2;
                $(".date-inpt-tour-from-freeform").datepicker({
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


                $(".date-inpt-tour-from").datepicker({
                    minDate: new Date(currentYear1, currentMonth1, currentDate1),
                    maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                });

                //changed by mohamed on 28/08/2018
                $(".date-inpt-tour-from-freeform").datepicker({
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
            ShowHideTourType();

            HideProgess();
        }
      
    </script>
    <script type="text/javascript">


        function GetHotelsDetails(exctypcode) {
            $.ajax({
                type: "POST",
                url: "ToursFreeFormBooking.aspx/GetHotelsDetails",
                data: '{exctypcode:  "' + exctypcode + '"}',
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

                ////                document.getElementById('<%=txtTourClassificationCode.ClientID%>').value = $(this).find("classificationcode").text();
                ////                document.getElementById('<%=txtTourClassification.ClientID%>').value = $(this).find("classificationname").text();


                ////                var ddlStarCategory = document.getElementById('<%=ddlStarCategory.ClientID%>');
                ////                ddlStarCategory.selectedIndex = $(this).find("starcat").text();
                ////                $('.custom-select-ddlStarCategory').next('span').children('.customSelectInner').text($(this).find("starcat").text());


                document.getElementById('<%=Txt_TourCategoryCode.ClientID%>').value = $(this).find("sicpvt").text();
                document.getElementById('<%=Txt_TourCategory.ClientID%>').value = $(this).find("sicpvt").text();
                document.getElementById('<%=Txt_TourTypeCode.ClientID%>').value = $(this).find("exctyp").text();
                document.getElementById('<%=Txt_TourType.ClientID%>').value = $(this).find("exctyp").text();
            });
            ShowHideTourType();

        };


        function TourCountryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = '';
            }
        }
        function AutoCompleteExtender_txtTourSourceCountry_OnClientPopulating(sender, args) {
            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtTourCustomerCode.ClientID%>').value != '')) {

                sender.set_contextKey(document.getElementById('<%=txtTourCustomerCode.ClientID%>').value);
            }
        }

        function ShowHideTourType() {

            var TourType = $("#<%= Txt_TourTypeCode.ClientID %>").val()

            if (TourType == "NORMAL") {
                $('#fromto').hide();
                $('#tourdate').show();
            }
            else if (TourType == "COMBO") {
                $('#fromto').hide();
                $('#tourdate').hide();
            }
            else {
                $('#fromto').show();
                $('#tourdate').hide();
            }

        }

        function TourTourTypeautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=Txt_TourTypeCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=Txt_TourTypeCode.ClientID%>').value = '';
            }

        }
        function ToursCodeautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=Txt_ToursCode.ClientID%>').value = eventArgs.get_value();

                GetHotelsDetails(document.getElementById('<%=Txt_ToursCode.ClientID%>').value);
            }
            else {
                document.getElementById('<%=Txt_ToursCode.ClientID%>').value = '';
            }

        }
        function TourStartingFromautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtTourStartingFromCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtTourStartingFromCode.ClientID%>').value = '';
            }
        }
        function TourCategoryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=Txt_TourCategoryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=Txt_TourCategoryCode.ClientID%>').value = '';
            }
        }
        function TourClassificationAutocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtTourClassificationCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtTourClassificationCode.ClientID%>').value = '';

            }
        }
        function TourCustomerAutocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtTourCustomerCode.ClientID%>').value = eventArgs.get_value();
                $find('AutoCompleteExtender_txtTourSourceCountry').set_contextKey(eventArgs.get_value());

                GetTourCountryDetails(eventArgs.get_value());
            }
            else {
                document.getElementById('<%=txtTourCustomerCode.ClientID%>').value = '';

            }
        }

        function GetTourCountryDetails(CustCode) {
            $.ajax({
                type: "POST",
                url: "ToursFreeFormBooking.aspx/GetCountryDetails",
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
                    document.getElementById('<%=txtTourSourceCountry.ClientID%>').value = ''
                    document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = '';
                    document.getElementById('<%=txtTourSourceCountry.ClientID%>').value = $(this).find("ctryname").text();
                    document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = $(this).find("ctrycode").text();

                    document.getElementById('<%=hdCurrCode.ClientID%>').value = $(this).find("currcode").text();



                    document.getElementById('<%=txtTourSourceCountry.ClientID%>').setAttribute("readonly", true);
                    $find('AutoCompleteExtender_txtTourSourceCountry').setAttribute("Enabled", false);

                });
            }
            else {
                document.getElementById('<%=txtTourSourceCountry.ClientID%>').value = ''
                document.getElementById('<%=txtTourSourceCountry.ClientID%>').value = ''
                document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = '';
                document.getElementById('<%=txtTourSourceCountry.ClientID%>').removeAttribute("readonly");
                $find('AutoCompleteExtender_txtTourSourceCountry').setAttribute("Enabled", true);
            }
        };

        function RefreshContent() {
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandlerForProgressBar);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandlerForProgressBar);

        }
        function BeginRequestHandlerForProgressBar() {

            //            ShowProgess();

        }
        function EndRequestHandlerForProgressBar() {
            HideProgess();


            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();

            $(".date-inpt-tour-from").datepicker({
                minDate: new Date(currentYear, currentMonth, currentDate)
            });

            //changed by mohamed on 28/08/2018
            var currentMonth = date.getMonth() - 2;
            $(".date-inpt-tour-from-freeform").datepicker({
                minDate: new Date(currentYear, currentMonth, currentDate)
            });

            // CallToDatePicker();

            var dfdate = document.getElementById('<%=hdChangeFromdate.ClientID%>').value;
            var dpFdate = dfdate.split("/");
            var FdateOut = new Date(dpFdate[2], dpFdate[1], dpFdate[0]);
            var fcurrentMonth = FdateOut.getMonth() - 1;
            var fcurrentDate = FdateOut.getDate();
            var fcurrentYear = FdateOut.getFullYear();

            var dTdate = document.getElementById('<%=hdChangeTodate.ClientID%>').value;
            var dpTdate = dTdate.split("/");
            var TdateOut = new Date(dpTdate[2], dpTdate[1], dpTdate[0]);
            var tcurrentMonth = TdateOut.getMonth() - 1;
            var tcurrentDate = TdateOut.getDate();
            var tcurrentYear = TdateOut.getFullYear();

            $(".date-inpt-tour-change-date").datepicker({
                minDate: new Date(fcurrentYear, fcurrentMonth, fcurrentDate),
                maxDate: new Date(tcurrentYear, tcurrentMonth, tcurrentDate)
            });


            ShowHideTourType();
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
        function CallToDatePicker() {
            var d = document.getElementById('<%=txtTourToDate.ClientID%>').value;
            if (d != '') {
                var dp = d.split("/");
                //                var dateOut = new Date(dp[2], dp[1], dp[0]);
                //                var currentMonth = dateOut.getMonth() - 1;

                var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;

                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-tour-to").datepicker("destroy");
                $(".date-inpt-tour-to").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });
            }
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
    <asp:UpdatePanel ID="UpdatePanel5" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
	<div class="header-a">
		<div class="wrapper-padding">			
         <div class="header-user"  style="margin-top:2px;"><asp:Label ID="lblHeaderUserName"  runat="server" ></asp:Label> </div>		
			<div class="header-phone" id="dvlblHeaderAgentName" runat="server" style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-agentname" style="padding-left:105;margin-top:2px;">
			<%--<asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>--%>
                <asp:LinkButton ID="btnMyAccount" style="    padding: 0px 10px 0px 0px;"  UseSubmitBehavior="False" OnClick="btnMyAccount_Click"
                        CssClass="header-account-button" runat="server" Text="Account" causesvalidation="true"></asp:LinkButton>
			</div>
              <div class="header-agentname" style="margin-top:2px;"><asp:Label ID="lblHeaderAgentName" style="padding: 0px 10px 0px 0px;" runat="server" ></asp:Label> </div>
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
			<div id="dvCurrency" runat="server"  style="margin-top:2px;" class="header-curency">
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
                <div class="page-head" style="padding-bottom: 10px;">
                    <div class="page-title">
                        TOURS - <span>Free Form Booking</span></div>
                    <div class="breadcrumbs">
                        <a href="Home.aspx?Tab=1">Home</a> / <a href="#">Tours Free Form Booking</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="page-head">
                    <div class="page-search-content-search">
                        <div class="search-tab-content">
                            <div class="page-search-p" style="padding: 10px 10px 10px 25px;">
                                <!-- // -->
                                <div class="search-large-i">
                                    <label>
                                        Tour - Pick Up Location</label>
                                    <div class="input-a">
                                        <asp:TextBox ID="txtTourStartingFrom" placeholder="Enter Space Bar to Show All" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtTourStartingFromCode" Style="display: none;" placeholder="example: dubai"
                                            runat="server"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourStartingFrom" runat="server"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                            MinimumPrefixLength="-1" ServiceMethod="GetTourStartingFrom" TargetControlID="txtTourStartingFrom"
                                            OnClientItemSelected="TourStartingFromautocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <!-- \\ -->
                                <div class="search-large-i">
                                    <label>
                                        Classification</label>
                                    <div class="input-a">
                                        <asp:TextBox ID="txtTourClassification" placeholder="--" runat="server" onkeydown="SetContextKey(1)"></asp:TextBox>
                                        <asp:TextBox ID="txtTourClassificationCode" runat="server" Style="display: none"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourClassification" runat="server"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                            MinimumPrefixLength="-1" ServiceMethod="GetTourCategory" TargetControlID="txtTourClassification"
                                            UseContextKey="true" OnClientItemSelected="TourClassificationAutocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <!-- // -->
                                <div class="search-large-i">
                                    <label>
                                        Star category</label>
                                    <div class="srch-tab-line no-margin-bottom">
                                        <div class="select-wrapper">
                                            <asp:DropDownList ID="ddlStarCategory" class="custom-select custom-select-ddlStarCategory"
                                                runat="server">
                                                <asp:ListItem Value="0">--</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <!-- // -->
                                <div class="search-large-i" style="float: left; margin-top: 20px;">
                                    <!-- // -->
                                    <div class="srch-tab-line no-margin-bottom">
                                        <div class="srch-tab-3c">
                                            <label>
                                                Senior Citizen</label>
                                            <div class="select-wrapper">
                                                <asp:DropDownList ID="ddlSeniorCitizen" class="custom-select custom-select-ddlSeniorCitizen"
                                                    runat="server">
                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <!-- \ \ -->
                                    </div>
                                    <div class="srch-tab-line no-margin-bottom" style="width: 80%;">
                                        <div class="srch-tab-3c">
                                            <label>
                                                adult</label>
                                            <div class="select-wrapper">
                                                <asp:DropDownList ID="ddlTourAdult" class="custom-select custom-select-ddlTourAdult"
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
                                                <asp:DropDownList ID="ddlTourChildren" class="custom-select custom-select-ddlTourChildren"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- // -->
                                <div class="search-large-i" id="dvTourCustomer" style="margin-top: 20px; float: left;"
                                    runat="server">
                                    <label>
                                        Agent</label>
                                    <div class="input-a">
                                        <asp:TextBox ID="txtTourCustomer" runat="server" placeholder="--"></asp:TextBox>
                                        <asp:TextBox ID="txtTourCustomerCode" runat="server" Style="display: none"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourCustomer" runat="server"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                            MinimumPrefixLength="-1" ServiceMethod="GetCustomers" TargetControlID="txtTourCustomer"
                                            OnClientItemSelected="TourCustomerAutocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <!-- // -->
                                <div class="search-large-i" style="margin-top: 20px; float: left;">
                                    <label>
                                        Source Country</label>
                                    <div class="input-a">
                                        <asp:TextBox ID="txtTourSourceCountry" runat="server" placeholder="--"></asp:TextBox>
                                        <asp:TextBox ID="txtTourSourceCountryCode" runat="server" Style="display: none"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourSourceCountry" runat="server"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                            MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtTourSourceCountry"
                                            UseContextKey="true" OnClientPopulating="AutoCompleteExtender_txtTourSourceCountry_OnClientPopulating"
                                            OnClientItemSelected="TourCountryautocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <!-- \\ -->
                                </div>
                                <div class="clear">
                                </div>
                                <div id="dvTourChild" runat="server" style="margin-top: 20px; display: none;">
                                    <div class="search-large-i-child-tour" style="float: left;">
                                        <label style="text-align: left; padding-right: 2px;">
                                            Ages of children at Tours</label>
                                        <div class="srch-tab-child" id="dvTourChild1" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="divPreHotelChild1">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtTourChild1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="srch-tab-child" id="dvTourChild2" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="div7">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtTourChild2" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="srch-tab-child" id="dvTourChild3" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="div8">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtTourChild3" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="srch-tab-child" id="dvTourChild4" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="div9">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtTourChild4" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="search-large-i-child-tour">
                                        <label style="color: White;">
                                            Ages of children at check-out</label>
                                        <div class="srch-tab-child" id="dvTourChild5" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="div10">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtTourChild5" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="srch-tab-child" id="dvTourChild6" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="div11">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtTourChild6" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="srch-tab-child" id="dvTourChild7" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="div12">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtTourChild7" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="srch-tab-child" id="dvTourChild8" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="div13">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtTourChild8" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                                <!-- // -->
                                <div class="search-large-i" style="margin-top: 20px; float: left;">
                                    <!-- // -->
                                    <div class="srch-tab-line no-margin-bottom">
                                        <div class="srch-tab-left">
                                            <label>
                                                Tour Category</label>
                                            <div class="input-a">
                                                <asp:TextBox ID="Txt_TourCategory" placeholder="Enter Space Bar to Show All" runat="server"
                                                    onkeydown="SetContextKey(3)"></asp:TextBox>
                                                <asp:TextBox ID="Txt_TourCategoryCode" Style="display: none;" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_Txt_TourCategory" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetTourCategory" TargetControlID="Txt_TourCategory"
                                                    UseContextKey="true" OnClientItemSelected="TourCategoryautocompleteselected">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                        <div class="srch-tab-right">
                                            <label>
                                                Tour Type</label>
                                            <div class="input-a">
                                                <asp:TextBox ID="Txt_TourType" placeholder="Enter Space Bar to Show All" runat="server"
                                                    onchange="ShowHideTourType()" onkeydown="SetContextKey(5)"></asp:TextBox>
                                                <asp:TextBox ID="Txt_TourTypeCode" Style="display: none;" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_Txt_TourType" runat="server" CompletionInterval="10"
                                                    CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetTourCategory" TargetControlID="Txt_TourType"
                                                    UseContextKey="true" OnClientItemSelected="TourTourTypeautocompleteselected">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                    <!-- \\ -->
                                </div>
                                <!-- // -->
                                <div class="search-large-i" style="margin-top: 20px; float: left;">
                                    <label>
                                        Tours</label>
                                    <div class="input-a">
                                        <asp:TextBox ID="Txt_Tours" placeholder="Enter Space Bar to Show All" runat="server"
                                            onkeydown="SetContextKey(4)"></asp:TextBox>
                                        <asp:TextBox ID="Txt_ToursCode" Style="display: none;" runat="server"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_Txt_Tours" runat="server" CompletionInterval="10"
                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                            MinimumPrefixLength="-1" ServiceMethod="GetTourCategory" TargetControlID="Txt_Tours"
                                            UseContextKey="true" OnClientItemSelected="ToursCodeautocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <!-- // -->
                                <div class="search-large-i" style="margin-top: 20px; float: left;">
                                    <!-- // -->
                                    <div id="fromto" class="srch-tab-line no-margin-bottom" style="display: none;">
                                        <div class="srch-tab-left">
                                            <label>
                                                From Date</label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTourFromDate" runat="server" placeholder="dd/mm/yyyy" autocomplete="off"
                                                    CssClass="date-inpt-tour-from-freeform"></asp:TextBox>
                                                <span class="date-icon-tour"></span>
                                            </div>
                                        </div>
                                        <div class="srch-tab-right">
                                            <label>
                                                To Date</label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTourToDate" runat="server" placeholder="dd/mm/yyyy" autocomplete="off"
                                                    CssClass="date-inpt-tour-to"></asp:TextBox>
                                                <span class="date-icon-tour"></span>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                    <div id="tourdate" class="srch-tab-line no-margin-bottom" style="display: none;">
                                        <label>
                                            Tour Date</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="Txt_TourDate" runat="server" placeholder="dd/mm/yyyy" autocomplete="off"
                                                CssClass="date-inpt-tour-from"></asp:TextBox>
                                            <span class="date-icon-tour"></span>
                                        </div>
                                    </div>
                                    <!-- \\ -->
                                </div>


                                <!-- // -->
                                <div class="clear">
                                </div>

                       
                                <div class="clear">
                                </div>
                                <div class="clear">
                                </div>
                                <asp:HiddenField ID="hdCheckInPrevDay" runat="server" />
                                <asp:HiddenField ID="hdCheckOutNextDay" runat="server" />
                                <asp:HiddenField ID="hdChangeFromdate" runat="server" />
                                <asp:HiddenField ID="hdChangeTodate" runat="server" />
                                <!-- \\ -->
                                <div class="clear">
                                </div>
                            </div>
                            <footer class="search-footer" style="padding: 10px 10px 10px 25px">
                                 <div class="search-large-i">
						            <div  class="srch-tab-left" >
                                            <asp:Button ID="btnTourFill" class="srch-btn-home"   OnClientClick="return ValidateTourSearch()"   runat="server" Text="Fill Details"></asp:Button>  
                                    </div>
                                    <div  class="srch-tab-left">
                                      <input  id="btnTourReset"  type="button"  class="srch-btn-home"  value="Reset"/>
                                    </div>
                                </div>
                                <!-- Complimentary// -->
                                    <div class="side-block fly-in" id="divComplimentary" style="width: 45%; font-weight: 700;  margin-bottom: 0px; 
                                        float: left;" runat="server">
                                        <div class="side-stars">
                                            <div class="side-padding" style="padding: 10px 0px 0px 10px;" >
                                                <div class="side-lbl" style="margin-bottom: 0px;" >
                                                    <asp:CheckBox runat="server" ID="Chk_Complimentary" />
                                                    Complimentary To Customer</div>
                                            </div>
                                        </div>
                                    </div>
						        <div class="clear"></div>


                                    

					        </footer>
                            <asp:HiddenField ID="hdChildAgeString" runat="server" />
                            <asp:Label ID="lblComboExcName" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdExcCodeComboPopup" runat="server" />
                            <asp:HiddenField ID="hdVehicleCode" runat="server" />
                            <asp:UpdatePanel ID="UpdatePanelTourType" runat="server">
                                <ContentTemplate>
                                    <div class="message-box-b" id="divSelectComboDates" style="display: none;" runat="server">
                                        <asp:Label ID="lblSelectComboDates" Text="Combo Excursion" runat="server"></asp:Label>
                                    </div>
                                    <div class="message-box-b" id="divSelectMultipleDates" style="display: none;" runat="server">
                                        <asp:Label ID="lblSelectMultipleDates" Text="Multiple Date Excursion" runat="server"></asp:Label>
                                    </div>
                                    <div class="message-box-b" id="divSelectNormal" style="display: none;" runat="server">
                                        <asp:Label ID="lbl_Normal" Text="Normal Excursion" runat="server"></asp:Label>
                                    </div>
                                    <asp:DataList ID="dlSelectComboDates" runat="server" Width="100%" RepeatColumns="3"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <!-- // -->
                                            <div class="search-large-i" id="divComboDates" runat="server" style="margin-left: 2.7%;">
                                                <div class="tour-change-date-label" style="width: 68%; float: left; padding-right: 5px;
                                                    padding-bottom: 1px;">
                                                    <label>
                                                        Excursion</label>
                                                    <div class="input-a">
                                                        <asp:Label ID="lblExcComboCode" Text='<%# Eval("exctypcombocode") %>' Style="display: none;"
                                                            runat="server"></asp:Label>
                                                        <asp:Label ID="lblExcComboName" Text='<%# Eval("exctypname") %>' runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="tour-change-date-label" style="float: left; width: 30%; padding-bottom: 1px;">
                                                    <label>
                                                        Select Date</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtExcComboDate" runat="server" placeholder="dd/mm/yyyy" AutoComplete="Off"
                                                            CssClass="date-inpt-tour-change-date" Style="z-index: 99999;"></asp:TextBox>
                                                        <span class="date-icon-tour"></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:HiddenField ID="hdMealPlanDatesPopup" runat="server" />
                                    <asp:HiddenField ID="hdMealPlanExcCode" runat="server" />
                                    <asp:DataList ID="dlMultipleDate" runat="server" Width="100%" RepeatColumns="8" RepeatDirection="Horizontal">
                                        <ItemTemplate>
                                            <div class="page-search-p" style="padding: 5px 5px 5px 5px;">
                                                <!-- // -->
                                                <div class="search-large-i" id="divComboDates" runat="server" style="width: 100%;">
                                                    <div class="tour-change-date-label" style="float: left; padding-right: 5px;">
                                                        <asp:CheckBox ID="chkMealPlanDates" runat="server" onchange="chkdlDateCheck(this)" />
                                                        <asp:Label ID="lblMeanPlanDates" Text='<%# Eval("AvailableDate") %>' runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <div class="clear" id="divClearCombo" runat="server">
                                    </div>
                                    <asp:HiddenField ID="hdCurrCode" runat="server" />
                                    <div class="page-search-p" id="divpsenior" style="display: none; padding: 6px 1px 0px 25px;"
                                        runat="server">
                                        <!-- Senior// -->
                                        <div class="search-large-i" id="divSenior" runat="server" style="float: left; text-align: center;
                                            margin-top: 2px; display: none;">
                                            <!-- // -->
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px; text-align: center;
                                                font-weight: 700;">
                                                Seniors
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_NoOfSeniors" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                </div>
                                                <!-- \ \ -->
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_PriceSenior" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Sale Value(<asp:Label runat="server" Style="color: Red;" ID="Lbl_CurCodeSenior"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_SaleValueSenior" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Adult// -->
                                        <div class="search-large-i" id="divAdult" runat="server" style="float: left; text-align: center;
                                            margin-top: 2px; display: none;">
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px !important; text-align: center;
                                                font-weight: 700;">
                                                Adults
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No
                                                </label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_NoOfAdults" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <!-- \ \ -->
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_PriceAdult" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Sale Value(<asp:Label Style="color: Red;" runat="server" ID="Lbl_CurCodeAdult"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_SaleValueAdult" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="clear" id="divClearAdult" runat="server">
                                        </div>--%>
                                        <!-- Child// -->
                                        <div class="search-large-i" id="divChild" runat="server" style="float: left; text-align: center;
                                            margin-top: 2px; display: none;">
                                            <!-- // -->
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px; text-align: center;
                                                font-weight: 700;">
                                                Childs
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No
                                                </label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_NoOfChild" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                </div>
                                                <!-- \ \ -->
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_PriceChild" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Sale Value(<asp:Label runat="server" Style="color: Red;" ID="Lbl_CurCodeChild"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_SaleValueChild" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Child As Adult// -->
                                        <div class="search-large-i" id="divChildAsAdult" runat="server" style="float: left;
                                            text-align: center; margin-top: 2px; display: none;">
                                            <!-- // -->
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px; text-align: center;
                                                font-weight: 700;">
                                                Child As Adults
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No
                                                </label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_NoOfChildAsAdult" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                </div>
                                                <!-- \ \ -->
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_PriceChildAsAdult" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Sale Value(<asp:Label runat="server" Style="color: Red;" ID="Lbl_CurCodeChAdult"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_SaleValueChildAsAdult" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Unit// -->
                                        <div class="search-large-i" id="divUnits" runat="server" style="float: left; text-align: center;
                                            margin-top: 2px; display: none;">
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px; text-align: center;
                                                font-weight: 700;">
                                                Units
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No
                                                </label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_NoOfUnits" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <!-- \ \ -->
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_PriceUnits" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Sale Value(<asp:Label runat="server" Style="color: Red;" ID="Lbl_CurCodeUnit"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_SaleValueUnits" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clear" style="width: 100%">
                                    </div>

                                  <div class="page-search-p" style="width: 95%; display: none; padding: 23px 1px 0px 25px;"
                                        id="divpTotal" runat="server">
                                        <!-- TotalSaleValue// -->
                                        <div class="search-large-i" id="divTotalSale" runat="server" style="float: left;
                                            text-align: center; margin-top: 2px; display: none;">
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; vertical-align: middle;">
                                                <label>
                                                    <strong>Total Sale Value</strong></label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_TotalSaleValue" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="width: 100%">
                                    </div>


                               <div class="search-large-i" style="margin-top: 20px; float: left;">
                                              <asp:Button ID="btnMulticost" class="srch-btn-home"    runat="server" Text="Fill Multicost"></asp:Button>  
                                        </div>
                                    <!-- TotalSaleValue// -->
                                    <div class="clear" style="width: 100%">
                                    </div>


                                       <div  style="width:90%;padding-left:30px;">


                                            <asp:GridView ID="gvMultiCost" runat="server" AutoGenerateColumns="False"
                                                                CssClass="mygrid" Width="100%" GridLines="Horizontal">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Service">
                                                                        <ItemTemplate>
                                                                                     <asp:Label ID="lbleplistcode" Text='<%# Eval("eplistcode") %>' style="display:none;" runat="server"></asp:Label>
                                                                             <asp:Label ID="lblpartycode" Text='<%# Eval("partycode") %>'  style="display:none;"  runat="server"></asp:Label>
                                                                            <asp:Label ID="lblService" Text='<%# Eval("servicedescription") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Adult">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAdult" Text='<%# Eval("noofadults") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Per Adult">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtPeradult"  Width="50px"  Text='<%# Eval("peradult") %>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Adult Cost">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="lblAdultCost"  Width="50px"  Text='<%# Eval("adultcost") %>' ReadOnly="true" runat="server"></asp:TextBox>
                                                                                 <asp:HiddenField ID="hdAdultCost"   Value='<%# Eval("adultcost") %>' runat="server"></asp:HiddenField>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Child">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblChild" Text='<%# Eval("noofchildren") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PerChild">                                                         
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtPerchild"  Width="50px"  Text='<%# Eval("perchild") %>' runat="server"></asp:TextBox>
                                                                          
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ChildCost">
                                                         
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="lblchildcost"  Width="50px" Text='<%# Eval("childcost") %>'   ReadOnly="true"  runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdChildcost"   Value='<%# Eval("childcost") %>' runat="server"></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seniors">
                                                              
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnoofseniors" Text='<%# Eval("noofseniors") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Per Senior">
                                                              
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtPersenior"  Width="50px" Text='<%# Eval("persenior") %>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="Senior Cost">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="lblSeniorCost"  Width="50px" Text='<%# Eval("seniorcost") %>'    ReadOnly="true"  runat="server"></asp:TextBox>
                                                                                     <asp:HiddenField ID="hdSeniorCost"   Value='<%# Eval("seniorcost") %>' runat="server"></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                                 <asp:TemplateField HeaderText="Unit Cost">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtUnitcost" Width="50px" Text='<%# Eval("unitcost") %>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                       <asp:TemplateField HeaderText="Total Cost">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="lbltotalcost"  Width="50px"  Text='<%# Eval("totalcost") %>'  ReadOnly="true"  runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdTotalcost"   Value='<%# Eval("totalcost") %>' runat="server"></asp:HiddenField>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                </Columns>
                                                                <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                    </div>

                                    
                                    <div class="page-search-p" id="divCostArea" style="padding: 6px 1px 0px 25px;" runat="server">
                                        <!-- Senior// -->
                                        <div class="search-large-i" id="divCostSenior" runat="server" style="float: left;
                                            text-align: center; margin-top: 2px; display: none;">
                                            <!-- // -->
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px; text-align: center;
                                                font-weight: 700;">
                                                Seniors
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostNoOfSeniors" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                </div>
                                                <!-- \ \ -->
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostPriceSenior" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Value(<asp:Label runat="server" Style="color: Red;" ID="Lbl_CostCurCodeSenior"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_CostSaleValueSenior" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Adult// -->
                                        <div class="search-large-i" id="divCostAdult" runat="server" style="float: left;
                                            text-align: center; margin-top: 2px; display: none;">
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px !important; text-align: center;
                                                font-weight: 700;">
                                                Adults
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No
                                                </label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostNoOfAdults" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <!-- \ \ -->
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostPriceAdult" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Value(<asp:Label Style="color: Red;" runat="server" ID="Lbl_CostCurCodeAdult"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_CostSaleValueAdult" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="clear" id="divCostClearAdult" runat="server">
                                        </div>--%>
                                        <!-- Child// -->
                                        <div class="search-large-i" id="divCostChild" runat="server" style="float: left;
                                            text-align: center; margin-top: 2px; display: none;">
                                            <!-- // -->
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px; text-align: center;
                                                font-weight: 700;">
                                                Childs
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No
                                                </label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostNoOfChild" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                </div>
                                                <!-- \ \ -->
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostPriceChild" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Value(<asp:Label runat="server" Style="color: Red;" ID="Lbl_CostCurCodeChild"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_CostSaleValueChild" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Child As Adult// -->
                                        <div class="search-large-i" id="divCostChildAsAdult" runat="server" style="float: left;
                                            text-align: center; margin-top: 2px; display: none;">
                                            <!-- // -->
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px; text-align: center;
                                                font-weight: 700;">
                                                Child As Adults
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No
                                                </label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostNoOfChildAsAdult" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                </div>
                                                <!-- \ \ -->
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostPriceChildAsAdult" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Value(<asp:Label runat="server" Style="color: Red;" ID="Lbl_CostCurCodeChAdult"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_CostSaleValueChildAsAdult" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Unit// -->
                                        <div class="search-large-i" id="divCostUnits" runat="server" style="float: left;
                                            text-align: center; margin-top: 2px; display: none;">
                                            <div class="tour-change-date-label" style="width: 90%; float: left; padding-right: 5px;
                                                margin-bottom: 0px; padding-top: 10px; padding-bottom: 0px; text-align: center;
                                                font-weight: 700;">
                                                Units
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-left: 5px; padding-bottom: 0px !important;">
                                                <label>
                                                    No
                                                </label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostNoOfUnits" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <!-- \ \ -->
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Price</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="Txt_CostPriceUnits" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; padding-bottom: 0px !important;">
                                                <label>
                                                    Cost Value(<asp:Label runat="server" Style="color: Red;" ID="Lbl_CostCurCodeUnit"></asp:Label>)</label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_CostSaleValueUnits" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                      <div class="row" style="width: 100%">
                                    </div>
                             

                                   <div class="page-search-p" style="width: 95%; display: none; padding:25px 1px 0px 25px;"
                                        id="divpCostTotal" runat="server">
                                        <!-- TotalSaleValue// -->
                                        <div class="search-large-i" id="divTotalCost" runat="server" style="float: left;
                                            text-align: center; margin-top: 2px; display: none;">
                                            <div class="tour-change-date-label" style="width: 30%; float: left; padding-right: 5px;
                                                text-align: center; vertical-align: middle;">
                                                <label>
                                                    <strong>Total Cost Value</strong></label>
                                                <div class="input-l">
                                                    <asp:TextBox ID="Txt_TotalCostValue" runat="server" onkeydown="fnReadOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                  
                               
                                    <div class="clear" id="divClearCombo2" runat="server">
                                    </div>
                                    <footer class="search-footer" style="text-align: center;">
                                    
						            <asp:HiddenField ID="hdnlineno" runat="server" />
                                            <asp:HiddenField ID="hdTourRateBasis" runat="server" />
                                            <asp:Button ID="btnSaveComboExcursion" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px; width=118px;height=34px;"
                                        Visible="false" runat="server" Text="Save" />
                                  <%--  <asp:Button ID="btnSaveMultipleDates" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px;"
                                        Visible="false" runat="server" Text="Save" />--%>
                                   
                                   <asp:Button ID="Txt_PriceCancel" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px; width=118px;height=34px;"
                                        Visible="false" runat="server" Text="Cancel" />
                                      
                                   
                                
						        <div class="clear"></div>


                                    
                                        </footer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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


            // alert(hdPriceMax.val());
        }
        function SelectedTour(chk, rowNumber, type) {
            var hd = document.getElementById("<%= hddlRowNumber.ClientID %>");
            hd.value = rowNumber;
            if (type == 'd') {
                var chkbox = document.getElementById(chk);
                if (chkbox.checked == true) {
                    showDialog('Alert Message', 'Please uncheck checkbox and change date.', 'warning');
                    return false;
                }
            }

        }

        function SelectedTour1(chk, rowNumber, type) {
            var hd = document.getElementById("<%= hddlRowNumber.ClientID %>");
            hd.value = rowNumber;
            if (type == 'd') {
                var chkbox = document.getElementById(chk);
                if (chkbox.checked == true) {
                    showDialog('Alert Message', 'Please uncheck checkbox and change date.', 'warning');
                    return false;
                }
            }
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
    <asp:CheckBox runat="server" AutoPostBack="true" ID="chkSelectTour1" />
    <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
    <asp:HiddenField ID="hdWhiteLabel" runat="server" />
    <asp:HiddenField ID="hdSliderCurrency" runat="server" />
    <asp:Button ID="btnConfirmHome" Width="170px" Style="display: none;" runat="server"
        Text="ConfirmHome" />
    </form>
</body>
</html>
