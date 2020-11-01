<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TourSearch.aspx.vb" Inherits="TourSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta charset="utf-8" />
    <link rel="icon" href="favicon.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <link rel="stylesheet" href="css/jquery-ui.css">
    <link rel="stylesheet" href="css/jquery.formstyler.css">
    <link rel="stylesheet" href="css/style.css" />
    <link id="lnkCSS" rel='stylesheet' type='text/css' href='css/style-common.css' />

     <%--***Danny 18/08/2018 fa fa-star--%>
     <link href="css/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
<%--    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">--%>

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
        .alert
        {
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 4px;
        }
        .alert-danger
        {
            color: #a94442;
            background-color: #f2dede;
            border-color: #ebccd1;
        }
        .available
        {
            color: Green !important;
        }
        .not-available
        {
            color: Red !important;
        }
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
    <%--<script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"
        type="text/javascript"></script>--%>
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

        function fnValidatePaxCount(AvailabilityCount, TimeSlotAdult, TimeSlotChild, TimeSlotSenior, iTotalPax, Availability,cAdult,cChild,cSenior) {
            var lblAvailabilityCount = document.getElementById(AvailabilityCount);
            var ddlTimeSlotAdult = document.getElementById(TimeSlotAdult);
            var ddlTimeSlotChild = document.getElementById(TimeSlotChild);
            var ddlTimeSlotSenior = document.getElementById(TimeSlotSenior);
            var lblAvailability = document.getElementById(Availability);
           
             var senior =0;
            if (ddlTimeSlotSenior.value=='')
            {
            senior=0;
            }
            else
            {
            senior=ddlTimeSlotSenior.value;
            }

             var Child =0;
            if (ddlTimeSlotChild.value=='')
            {
            Child=0;
            }
            else
            {
            Child=ddlTimeSlotChild.value;
            }



        var total = 0;
        var gAdult =0;
        var gChild = 0;
        var gSenior = 0;
            var Grid_Table = document.getElementById('<%=gvTimeSlot.ClientID%>');
            var lblTimeSlotSelection = document.getElementById('<%=lblTimeSlotSelection.ClientID%>');
            var TimeSlots = '';
            for (var row = 1; row < Grid_Table.rows.length; row++) {
               
                var ad = Grid_Table.rows[row].cells[2].getElementsByTagName("select")[0].value;
                var ch = Grid_Table.rows[row].cells[3].getElementsByTagName("select")[0].value;
                var sr = Grid_Table.rows[row].cells[4].getElementsByTagName("select")[0].value;
                var gAdult = gAdult+parseInt(ad);
                var gChild = gChild+parseInt(ch);
                var gSenior =gSenior+ parseInt(sr);
                total = parseInt(total) + parseInt(ad) + parseInt(ch) + parseInt(sr);
          
                var dd = Grid_Table.rows[row].cells[2].getElementsByTagName("select")[0].id;
                var lblTimeSlots = document.getElementById(dd.replace("ddlTimeSlotAdult", "lblTimeSlot"));
                if (total > 0) {

                    var strText = '';
                    if (parseInt(ad) > 0) {
                        strText = lblTimeSlots.innerHTML + ':- ' + ad + ' Adult';
                    }
                    if (parseInt(ch) > 0) {
                        strText = strText + ' + ' + ch + ' Child';
                    }
                    if (parseInt(sr) > 0) {
                        strText = strText + ' + ' + sr + ' Senior';
                    }
         
                    if (strText != '' && strText != '</br>') {
                        if (TimeSlots == '') {
                            TimeSlots = strText;
                        }
                        else {
                            TimeSlots = TimeSlots + ', ' + strText;
                        }
                    }
              
                }
            }
            lblTimeSlotSelection.innerHTML = 'Time: ' + TimeSlots;

            var totalpax = parseInt(ddlTimeSlotAdult.value) + parseInt(Child) + parseInt(senior);
            if (totalpax > parseInt(lblAvailabilityCount.innerHTML)) {
                lblAvailability.innerHTML = 'Not Available';
                lblAvailability.classList.remove("Available");
                lblAvailability.classList.add("not-available");
                var dvWarning = document.getElementById('<%=dvWarning.ClientID%>');
                dvWarning.setAttribute("style", "display:block;margin-left: 15px;");
                var lblTimeSlotWarning = document.getElementById('<%=lblTimeSlotWarning.ClientID%>');
                lblTimeSlotWarning.innerHTML = 'Selected time slot is not available.';

                return false
            }
            else {
                lblAvailability.innerHTML = 'Available';
                lblAvailability.classList.remove("not-available");
                lblAvailability.classList.add("available");

                var dvWarning = document.getElementById('<%=dvWarning.ClientID%>');
                dvWarning.setAttribute("style", "display:none");
                var lblTimeSlotWarning = document.getElementById('<%=lblTimeSlotWarning.ClientID%>');
                lblTimeSlotWarning.innerHTML = '';

            }
//            var gAdult = parseInt(ad);
//            var gChild = parseInt(ch);
//            var gSenior = parseInt(sr);

            if ((gAdult > parseInt(cAdult)) || (gChild > parseInt(cChild)) || (gSenior > parseInt(cSenior))) {
                var warning = 'Booking selected for ';
                if (parseInt(cAdult) > 0) {
                    warning = warning + cAdult + ' Adult';
                }
                if (parseInt(cChild) > 0) {
                    warning = warning + ' + ' + cChild + ' Child';
                }
                if (parseInt(cSenior) > 0) {
                    warning = warning + ' + ' + cSenior + ' Senior';
                }

                warning = warning + '. But time slot selected for '

                if (gAdult > 0) {
                    warning = warning + gAdult.toString() + ' Adult';
                }
                if (gChild > 0) {
                    warning = warning + ' + ' + gChild.toString() + ' Child';
                }
                if (gSenior > 0) {
                    warning = warning + ' + ' + gSenior.toString() + ' Senior';
                }

                var dvWarning = document.getElementById('<%=dvWarning.ClientID%>');
                dvWarning.setAttribute("style", "display:block;margin-left: 15px;");
                var lblTimeSlotWarning = document.getElementById('<%=lblTimeSlotWarning.ClientID%>');
                lblTimeSlotWarning.innerHTML = warning;
            }
            else {
                var dvWarning = document.getElementById('<%=dvWarning.ClientID%>');
                dvWarning.setAttribute("style", "display:none");
                var lblTimeSlotWarning = document.getElementById('<%=lblTimeSlotWarning.ClientID%>');
                lblTimeSlotWarning.innerHTML = '';
            }

            if ((gAdult == parseInt(cAdult)) && (gChild == parseInt(cChild)) && (gSenior == parseInt(cSenior))) {
                var lblTimeSlotWarning = document.getElementById('<%=lblTimeSlotWarning1.ClientID%>');
                lblTimeSlotWarning.innerHTML = 'success';
              
            }
            else {
                var lblTimeSlotWarning = document.getElementById('<%=lblTimeSlotWarning1.ClientID%>');
                lblTimeSlotWarning.innerHTML = 'error';
              
            }
//          if (total > parseInt(iTotalPax)) {
//              alert('More pax selected.');
//          }

        }
        function ValidateTimeSlotSave() {
            var lblWar = document.getElementById('<%=lblTimeSlotWarning.ClientID%>').innerHTML;
            if (lblWar != '') {
                showDialog('Alert Message', lblWar, 'warning');

                return false;
            }
            var lblWar1 = document.getElementById('<%=lblTimeSlotWarning1.ClientID%>').innerHTML;
            if (lblWar1 != 'success') {
                showDialog('Alert Message', 'Booking selected pax and time slot selected pax are not matched. ', 'warning');

                return false;
            }
            return true;
        }
        function CalculateSaleValue(lblNoOfAdult, txtPrice, lblSaleValue) {

            var lblNoOfAdult1 = document.getElementById(lblNoOfAdult);
            var txtPrice1 = document.getElementById(txtPrice);
            var lblSaleValue1 = document.getElementById(lblSaleValue);
            var totalamt = (parseFloat(txtPrice1.value) * parseFloat(lblNoOfAdult1.value));
            lblSaleValue1.value = totalamt;


        }
        //added by abin on 20181208
        function CalculateSaleValueForChild(lblNoOfAdult, txtPrice, lblSaleValue, childasfree) {

            var lblNoOfAdult1 = document.getElementById(lblNoOfAdult);
            var txtPrice1 = document.getElementById(txtPrice);
            var lblSaleValue1 = document.getElementById(lblSaleValue);
            var totalamt;
            if (childasfree > 0) {
                var no = lblNoOfAdult1.value - childasfree;
                if (no < 0) {
                    no = 0;
                }
                totalamt = (parseFloat(txtPrice1.value) * parseFloat(no));
            }
            else {
                totalamt = (parseFloat(txtPrice1.value) * parseFloat(lblNoOfAdult1.value));
            }
          
            lblSaleValue1.value = totalamt;


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
            document.getElementById("<%= btnFilter.ClientID %>").click();
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


            if (document.getElementById('<%=txtTourStartingFromCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter TOUR - PICK UP LOCATION.', 'warning');
                HideProgess();
                return false;
            }
            if (document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter tour source country.', 'warning');
                HideProgess();
                return false;
            }
            if ((document.getElementById('<%=ddlTourAdult.ClientID%>').value == '0') && (document.getElementById('<%=ddlSeniorCitizen.ClientID%>').value == '0')) {
                showDialog('Alert Message', 'Please enter senior citizen or adult.', 'warning');
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

            var slider_range = $("#slider-range");
            slider_range.on("click", function () {
                document.getElementById("<%= btnFilter.ClientID %>").click();
            });

            AutoCompleteExtender_TourStartingFrom_KeyUp();
            AutoCompleteExtender_TourClassification_KeyUp();
            AutoCompleteExtender_TourCustomer_KeyUp();
            AutoCompleteExtender_TourCountry_KeyUp();

            fillTourDates();

//              $("#<%= txtSearchTour.ClientID %>").keyup("change", function () {
//                    document.getElementById("<%= btnFilter.ClientID %>").click();
//                });

           if (document.getElementById('<%=txtTourFromDate.ClientID%>').value != '') {

                BindToDateCalendar();
            }

                  function BindToDateCalendar() {
                   var d = document.getElementById('<%=txtTourFromDate.ClientID%>').value;

                var dp = d.split("/");
                var dateOut =  new Date(dp[2] + "/" +  dp[1] + "/" +  dp[0]);     // modified param 11/10/2018 new Date(dp[2], dp[1], dp[0]);
                var currentMonth = dateOut.getMonth();                             //dateOut.getMonth() - 1;
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
                    var date4 = new Date(dp4[2] + "/" + dp4[1] + "/" + dp4[0]);                  //modified param 11/10/2018 new Date(dp4[2], dp4[1], dp4[0]);
                    var currentMonth4 = date4.getMonth();                                     //date4.getMonth() - 1;
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
            var FdateOut = new Date(dpFdate[2] + "/" + dpFdate[1] + "/" + dpFdate[0]);          // modified param 11/10/2018 new Date(dpFdate[2], dpFdate[1], dpFdate[0]); 
            var fcurrentMonth = FdateOut.getMonth();                                            //FdateOut.getMonth() - 1;
            var fcurrentDate = FdateOut.getDate();
            var fcurrentYear = FdateOut.getFullYear();

            var dTdate = document.getElementById('<%=hdChangeTodate.ClientID%>').value;
            var dpTdate = dTdate.split("/");
            var TdateOut = new Date(dpTdate[2] + "/" + dpTdate[1] + "/" + dpTdate[0]);                        //modified param 11/10/2018 new Date(dpTdate[2], dpTdate[1], dpTdate[0]);
            var tcurrentMonth = TdateOut.getMonth();                                                          //TdateOut.getMonth() - 1;
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

            
                $('#dvTourChild').hide();

            });

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


            function AutoCompleteExtender_TourClassification_KeyUp() {

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

        function ReBindMyStuff() { // create the rebinding logic in here
            $("#slider-range").on("click", function () {
                document.getElementById("<%= btnFilter.ClientID %>").click();
            });
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
            }
            else {
                var dp1 = dCheckInMin.split("/");
                var date1 = new Date(dp1[2] + "/" + dp1[1] + "/" + dp1[0]);   //modified param 11/10/2018    new Date(dp1[2] , dp1[1], dp1[0]); 
                var currentMonth1 = date1.getMonth();                              //date1.getMonth()-1;
                var currentDate1 = date1.getDate();
                var currentYear1 = date1.getFullYear();

                var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
                var dp4 = dCheckOutMax.split("/");
                var date4 = new Date(dp4[2] + "/" + dp4[1] + "/" + dp4[0]);    //modified param 11/10/2018   new Date(dp4[2] , dp4[1] , dp4[0]);
                var currentMonth4 = date4.getMonth();                                                        // date4.getMonth()-1;
                var currentDate4 = date4.getDate();
                var currentYear4 = date4.getFullYear();


                $(".date-inpt-tour-from").datepicker({
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
                $('#dvTourChild9').hide();
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
                $('#dvTourChild9').hide();
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
                $('#dvTourChild9').hide();
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
                $('#dvTourChild9').hide();
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
                $('#dvTourChild9').hide();
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
                $('#dvTourChild9').hide();
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
                $('#dvTourChild9').hide();
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
                $('#dvTourChild9').hide();
                $('#dvTourChild').show();
            }
            else if (child == 9) {

                $('#dvTourChild1').show();
                $('#dvTourChild2').show();
                $('#dvTourChild3').show();
                $('#dvTourChild4').show();
                $('#dvTourChild5').show();
                $('#dvTourChild6').show();
                $('#dvTourChild7').show();
                $('#dvTourChild8').show();
                $('#dvTourChild9').show();
                $('#dvTourChild').show();

            }

        }
    //]]>
    </script>
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestUserControl);
        function EndRequestUserControl(sender, args) {
            $("#slider-range").on("click", function () {
                document.getElementById("<%= btnFilter.ClientID %>").click();

            });
            CallToDatePicker();
//            $("#<%= txtSearchTour.ClientID %>").keyup("change", function () {
//                document.getElementById("<%= btnFilter.ClientID %>").click();
//            });
        }
      
    </script>
    <script type="text/javascript">


        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {

//            $("#<%= txtSearchTour.ClientID %>").keyup("change", function () {
//                document.getElementById("<%= btnFilter.ClientID %>").click();
//            });


            $("#slider-range").on("click", function () {

                document.getElementById("<%= btnFilter.ClientID %>").click();
            });

        });
        function TourCountryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = '';
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
                url: "TourSearch.aspx/GetCountryDetails",
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
                    document.getElementById('<%=txtTourSourceCountry.ClientID%>').setAttribute("readonly", true);
                    $find('AutoCompleteExtender_txtTourSourceCountry').setAttribute("Enabled", false);
                });
            }
            else {
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

            ShowProgess();

        }
        function EndRequestHandlerForProgressBar() {
            HideProgess();
            CallPriceSlider()

            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();

            $(".date-inpt-tour-from").datepicker({

                minDate: new Date(currentYear, currentMonth, currentDate)

            });

            CallToDatePicker();

            var dfdate = document.getElementById('<%=hdChangeFromdate.ClientID%>').value;
            var dpFdate = dfdate.split("/");
            var FdateOut = new Date(dpFdate[2] + "/" + dpFdate[1] + "/" + dpFdate[0]);                //Modified param 11/10/2018   new Date(dpFdate[2], dpFdate[1], dpFdate[0]);
            var fcurrentMonth = FdateOut.getMonth();                                                  //FdateOut.getMonth() - 1;
            var fcurrentDate = FdateOut.getDate();
            var fcurrentYear = FdateOut.getFullYear();

            var dTdate = document.getElementById('<%=hdChangeTodate.ClientID%>').value;
            var dpTdate = dTdate.split("/");
            var TdateOut = new Date(dpTdate[2] + "/" + dpTdate[1] + "/" + dpTdate[0]);                            //Modified param 11/10/2018    new Date(dpTdate[2], dpTdate[1], dpTdate[0]); 
            var tcurrentMonth = TdateOut.getMonth();                                                              //TdateOut.getMonth() - 1;
            var tcurrentDate = TdateOut.getDate();
            var tcurrentYear = TdateOut.getFullYear();

            $(".date-inpt-tour-change-date").datepicker({
                minDate: new Date(fcurrentYear, fcurrentMonth, fcurrentDate),
                maxDate: new Date(tcurrentYear, tcurrentMonth, tcurrentDate)
            });

//            $("#<%= txtSearchTour.ClientID %>").keyup("change", function () {
//                document.getElementById("<%= btnFilter.ClientID %>").click();
//              
//            });
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
                var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified param 11/10/2018   new Date(dp[2], dp[1], dp[0]);
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
	<div class="header-a">
		<div class="wrapper-padding">			
         <div class="header-user"  style="margin-top:2px;"><asp:Label ID="lblHeaderUserName"  runat="server" ></asp:Label> </div>		
			<div class="header-phone" style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-account">
			<asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
			</div>
              <div class="header-agentname" style="padding-left:105px;margin-top:2px;"><asp:Label ID="lblHeaderAgentName" runat="server" ></asp:Label> </div>
            <div class="header-lang">
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
			<div id="dvCurrency" runat="server"  style="margin-top:2px;" class="header-curency">
			</div>
               <div class="header-lang" id="dvMybooking" runat="server" style="margin-right:5px;">
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
                        TOURS - <span>list style</span></div>
                    <div class="breadcrumbs">
                        <a href="Home.aspx?Tab=1">Home</a> / <a href="#">Tours</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="page-head">
                    <div class="page-search-content-search">
                        <div class="search-tab-content">
                            <div class="page-search-p">
                                <!-- // -->
                                <div class="search-large-i">
                                    <!-- // -->
                                    <div class="srch-tab-line no-margin-bottom">
                                        <div class="srch-tab-left">
                                            <label>
                                                From Date<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTourFromDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-tour-from"></asp:TextBox>
                                                <span class="date-icon-tour"></span>
                                            </div>
                                        </div>
                                        <div class="srch-tab-right">
                                            <label>
                                                To Date<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTourToDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-tour-to"></asp:TextBox>
                                                <span class="date-icon-tour"></span>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                    <!-- \\ -->
                                </div>
                                <!-- \\ -->
                                <!-- // -->
                                <div class="search-large-i">
                                    <label>
                                        Tour - Pick Up Location<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
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
                                <div class="search-large-i">
                                    <label>
                                        Classification</label>
                                    <div class="input-a">
                                        <asp:TextBox ID="txtTourClassification" placeholder="--" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtTourClassificationCode" runat="server" Style="display: none"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourClassification" runat="server"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                            MinimumPrefixLength="-1" ServiceMethod="GetClassification" TargetControlID="txtTourClassification"
                                            OnClientItemSelected="TourClassificationAutocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                                <div class="search-large-i" style="margin-top: 20px;">
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
                                <div class="search-large-i" id="dvTourCustomer" style="margin-top: 20px; float: left;"
                                    runat="server">
                                    <label>
                                        Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
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
                                <div class="search-large-i" style="margin-right: 0px; float: right; margin-top: 20px;">
                                    <label>
                                        Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                    <div class="input-a">
                                        <asp:TextBox ID="txtTourSourceCountry" runat="server" placeholder="--"></asp:TextBox>
                                        <asp:TextBox ID="txtTourSourceCountryCode" runat="server" Style="display: none"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourSourceCountry" runat="server"
                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                            MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtTourSourceCountry"
                                            UseContextKey="true" OnClientItemSelected="TourCountryautocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <!-- \\ -->
                                </div>
                                <div class="clear">
                                </div>
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
                                                adult<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
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
                                <div class="search-large-i" style="margin-top: 20px; float: left;">
                                    <label>
                                    </label>
                                    <div class="sic-span">
                                  <%--      <asp:RadioButtonList ID="rblPrivateOrSIC" Width="100%" RepeatDirection="Horizontal"
                                            runat="server">
                                            <asp:ListItem Selected="True">Private</asp:ListItem>
                                            <asp:ListItem>SIC (Seat In Coach)</asp:ListItem>
                                            <asp:ListItem>Without Transfers</asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                        
                                          <asp:CheckBoxList ID="chklPrivateOrSIC" Width="100%" RepeatDirection="Horizontal"
                                            runat="server">
                                            <asp:ListItem  Selected="True">Private</asp:ListItem>
                                            <asp:ListItem  Selected="True">SIC (Seat In Coach)</asp:ListItem>
                                            <asp:ListItem  Selected="True">Without Transfers</asp:ListItem>
                                            </asp:CheckBoxList>
                                    </div>

                                </div>
                                <div class="search-large-i" id="dvTourOveridePrice" style="margin-top: 20px; float: left;"
                                    runat="server">
                                    <div class="sic-span" style="padding-top: 10px;">
                                        <asp:CheckBox ID="chkTourOveridePrice" Text="Override Price" runat="server" />
                                        <%-- <asp:Label ID="Label2" runat="server" CssClass="page-search-content-override-price"
                                                    Text="Override Price"></asp:Label>--%>
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                                <div id="dvTourChild" runat="server" style="margin-top: 20px; display: none;">
                                    <div class="search-large-i-child-tour" style="float: left;">
                                        <label style="text-align: left; padding-right: 2px;">
                                            Ages of children at check-out</label>

                                         <div class="srch-tab-child" id="dvTourChild1" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                 <div class="srch-tab-child-pre" id="divPreHotelChild1">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild1" placeholder="CH 1" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txtTourChild2" placeholder="CH 2" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txtTourChild3" placeholder="CH 3" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txtTourChild4" placeholder="CH 4" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txtTourChild5" placeholder="CH 5" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txtTourChild6" placeholder="CH 6" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txtTourChild7" placeholder="CH 7" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txtTourChild8" placeholder="CH 8" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    
                                       
                                       
                                       
                                    </div>


                                       <div class="search-large-i-child-tour" style="float: left;padding-top:25px;">
                                            <div class="srch-tab-child" id="dvTourChild9" style="float: left;">
                                            <div class="select-wrapper" style="width: 75px;">
                                                <div class="srch-tab-child-pre" id="div14">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild9" placeholder="CH 9" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                       </div>
                                </div>
                                <asp:HiddenField ID="hdCheckInPrevDay" runat="server" />
                                <asp:HiddenField ID="hdCheckOutNextDay" runat="server" />
                                <asp:HiddenField ID="hdChangeFromdate" runat="server" />
                                <asp:HiddenField ID="hdChangeTodate" runat="server" />
                                <!-- \\ -->
                                <div class="clear">
                                </div>
                            </div>
                            <footer class="search-footer">
                                       <div class="search-large-i">
						     <div  class="srch-tab-left" >
                          <asp:Button ID="btnTourSearch" class="authorize-btn"   OnClientClick="return ValidateTourSearch()"   runat="server" Text="Search"></asp:Button>  
                          </div>
                           <div  class="srch-tab-left">
                      <input  id="btnTourReset"  type="button"  class="srch-btn-home"  value="Reset"/>
                        </div></div>
						<div class="clear"></div>
					</footer>
                        </div>
                    </div>
                </div>
                <div class="two-colls">
                    <div class="two-colls-left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="srch-results-lbl fly-in">
                                    <asp:Label ID="lblHotelCount" runat="server" Text=""></asp:Label>
                                </div>
                                <!-- // side // -->
                                <div class="side-block fly-in">
                                    <div class="side-price" id="divslideprice" runat="server">
                                        <div class="side-padding">
                                            <div class="side-lbl">
                                                Price</div>
                                            <div class="price-ranger">
                                                <div id="slider-range">
                                                </div>
                                            </div>
                                            <div class="price-ammounts">
                                                <input type="text" id="ammount-from" readonly>
                                                <input type="text" id="ammount-to" readonly>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- \\ side \\ -->
                                <!-- // side // -->
                                <div class="side-block fly-in">
                                    <div class="side-stars">
                                        <div class="side-padding">
                                            <div class="side-lbl">
                                                Star Category</div>
                                            <asp:CheckBoxList ID="chkHotelStars" CssClass="checkbox" OnChange="CallFilter();"
                                                runat="server" CellPadding="5" CellSpacing="1" AutoPostBack="True">
                                            </asp:CheckBoxList>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- \\ side \\ -->
                                <div>
                                    <asp:TextBox ID="txtSearchFocus" Style="width: 2px; height: 2px; border: none;" MaxLength="1"
                                        runat="server"></asp:TextBox>
                                </div>
                                <!-- // side // -->
                                <div class="side-block fly-in">
                                    <div class="side-stars">
                                        <div class="side-padding">
                                            <div class="side-lbl">
                                                Tour Classification</div>
                                            <asp:CheckBoxList ID="chkRoomClassification" CssClass="checkbox" runat="server" OnChange="CallFilter();"
                                                CellPadding="5" CellSpacing="1" AutoPostBack="True">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <asp:Button ID="btnFilter" runat="server" Style="display: none;" Text="Filter" />
                                <asp:Button ID="btnFilterForRoom" runat="server" Style="display: none;" Text="Room Filter" />
                                <asp:Button ID="btnSelectedTourChecknox" runat="server" Style="display: none;" Text="Filter" />
                                <!-- \\ side \\ -->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="two-colls-right">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="two-colls-right-b">
                                    <div class="padding">
                                        <div class="catalog-head large fly-in">
                                            <label>
                                                Sort by:</label>
                                            <div class="search-select">
                                                <asp:DropDownList ID="ddlSorting" AutoPostBack="true" runat="server">
                                                
                                                    <asp:ListItem>Preferred</asp:ListItem>
                                                     <asp:ListItem>Name</asp:ListItem>
                                                    <asp:ListItem>Price</asp:ListItem>
                                                   
                                                    <asp:ListItem>Rating</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="search-large-i" style="float:left;width:40%;">
                                                <div class="input-a">
                                                    <asp:TextBox runat="server" ID="txtSearchTour" placeholder="Search tour"  TabIndex="1001" AutoPostBack="true" OnTextChanged="txtSearchTour_TextChanged"
                                                        ></asp:TextBox>
                                                </div>
                                            </div>
                                             <div  style="float:left;padding-left:10px;">
                                                 <asp:Button ID="btnTourTextSearch" TabIndex="1002" CssClass="btn-search-text" runat="server"  OnClientClick="CallFilter();"   
                                                    Text="SEARCH"></asp:Button>
                                            </div>
                                            <div style="display: none;" class="search-select">
                                                <select>
                                                    <option>Preferred</option>
                                                    <option>Preferred</option>
                                                    <option>Preferred</option>
                                                    <option>Preferred</option>
                                                    <option>Preferred</option>
                                                </select>
                                            </div>
                                             <div  style="float:right;">
                                            <div style="float: left">
                                                <asp:Button ID="btnbooknow" CssClass="guest-flight-details-generate" runat="server"
                                                    Text="BOOK SELECTED TOURS"></asp:Button>
                                            </div></div>
                                            <a href="#" class="show-list" style="display: none;"></a><a href="#" class="show-table"
                                                style="display: none;"></a><a href="#" class="show-thumbs chosen" style="display: none;">
                                                </a>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div id="dvhotnoshow" runat="server" style="display: none; background-color: #F2F3F4;
                                            padding-top: 16px; padding-bottom: 16px; padding-left: 16px; text-align: center">
                                            <asp:Label ID="lblheader" runat="server" Text="Oops, No results to show. Can you please try a different combination?"
                                                ForeColor="#009999" Font-Bold="True">
                                            </asp:Label></div>
                                        <div class="catalog-row list-rows">
                                            <!-- // -->
                                            <%--       <asp:CheckBox runat="server" AutoPostBack ="true"  ID="CheckBox1"  OnCheckedChanged  ="chkSelectTour_CheckedChanged" />--%>
                                            <asp:DataList ID="dlTourSearchResults" runat="server" Width="100%">
                                                <ItemTemplate>
                                                    <div class="cat-list-item fly-in">
                                                        <div class="cat-list-item-l" style="padding-top: 10px; padding-left: 10px;height:171px;">
                                                            <a href="#">
                                                                <asp:Image ID="imgExcImage" runat="server" /></a>
                                                            <asp:Label ID="lblExcImage" Visible="false" runat="server" Text='<%# Eval("excimage") %>'></asp:Label>
                                                        </div>
                                                        <div class="cat-list-item-r" style="padding-top: 5px;">
                                                            <div class="cat-list-item-rb">
                                                                <div class="cat-list-item-p">
                                                                    <div class="cat-list-content">
                                                                        <div class="cat-list-content-a">
                                                                            <div class="cat-list-content-l">
                                                                                <div class="cat-list-content-lb">
                                                                                    <div class="cat-list-content-lpadding">
                                                                                        <div class="offer-slider-link">
                                                                                          <asp:HiddenField ID="hdChildAsFree" Value='<%# Eval("childasfree") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdExcCode" Value='<%# Eval("exctypcode") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdVehicleCode" Value='<%# Eval("vehiclecode") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdCurrCode" Value='<%# Eval("currcode") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdRateBasis" Value='<%# Eval("ratebasis") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdnselected" Value='<%# Eval("currentselection") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdntourdate" Value='<%# Eval("excdate") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdncumunits" Value='<%# Eval("units") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdCombo" Value='<%# Eval("combo") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdMultipleDates" Value='<%# Eval("multipledatesyesno") %>' runat="server" />
                                                                                            <asp:HiddenField ID="hdtotalsalevalue" Value='<%# Eval("totalsalevalue") %>' runat="server" />
                                                                                            <asp:Label ID="lblExcName" CssClass="offer-slider-link-label" Text='<%# Eval("excname") %>'
                                                                                                runat="server"></asp:Label>
                                                                                                  <asp:HiddenField ID="hdElineNo" Value='<%# Eval("elineno") %>' runat="server" />
                                                                                        </div>
                                                                                        <div class="clear">
                                                                                        </div>
                                                                                        <div class="offer-slider-location">
                                                                                            <asp:Label ID="lblCityName" runat="server"></asp:Label></div>
                                                                                               <div class="clear">
                                                                                            </div>
                                                                                            <div class="search-large-i" style="float:left;width:45%;text-transform:uppercase;" >
                                                                                            <div class="input-a" id="dvTourType" runat="server">
                                                                                            <asp:Label ID="lblSicPvt" Text='<%# Eval("sicpvt") + " Tours" %>' runat="server"></asp:Label></div>
                                                                                            </div>
                                                                                             <div style="float: right;">
                                                                                                <asp:Label ID="lblpreferred" Style="display: none;" Text='<%# Eval("preferred") %>'
                                                                                                    runat="server"></asp:Label>
                                                                                                <input id="btnPreferred" runat="server" type="button" class="tour-buttons-prefferred"
                                                                                                    value="Preferred" />
                                                                                            </div> 

                                                                                           

                                                                                            
                                                                                            <div class="clear">
                                                                                            </div>
                                                                                        <p style="text-align: justify;">
                                                                                            <asp:Label ID="lblExcText" runat="server" ToolTip='<%# Eval("exctext") %>' Text='<%# Eval("exctext") %>'></asp:Label>
                                                                                        </p>
                                                                                        <div style="float: right;">
                                                                                            <asp:LinkButton ID="lbReadMore" CssClass="rate-plan-headings" OnClick="lbReadMore_Click"
                                                                                                runat="server">Read More.</asp:LinkButton></div>
                                                                                        <asp:HiddenField ID="hddlHotelsSearchResultsItemIndex" runat="server" />
                                                                                        <div>
                                                                                            <div class="search-large-i">
                                                                                                <!-- // -->
                                                                                                <div class="srch-tab-line no-margin-bottom">
                                                                                                    <div class="tour-change-date-label">
                                                                                                        <div id="dvSelectDatelbl" runat="server">
                                                                                                            <label>
                                                                                                                Select Date</label>
                                                                                                            <div id="dvTourdates" runat="server" class="input-a" style="margin-top: 5px;">
                                                                                                                <asp:TextBox ID="txtTourChangeDate" runat="server" placeholder="dd/mm/yyyy" AutoComplete="Off"  CssClass="date-inpt-tour-change-date"></asp:TextBox>
                                                                                                                <span class="date-icon-tour"></span>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div id="dvSelectDatelink" runat="server">
                                                                                                            <asp:LinkButton ID="lbSelectDate" runat="server" Text="Select Date" OnClick="lbSelectDate_Click"></asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="clear">
                                                                                                    </div>
                                                                                                </div>
                                                                                                <!-- \\ -->
                                                                                            </div>
                                                                                         
                                                                                             <div class="clear">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <br class="clear" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="cat-list-content-r">
                                                                            <div class="cat-list-content-p">
                                                                                <div class="roomtype-icons" style="padding-left: 25px;">
                                                                                </div>
                                                                                <asp:HiddenField ID="hdNoOfExcStars" Value='<%# Eval("starcategory") %>' runat="server" />
                                                                                <div id="dvExcStars" style="padding-left: 15px;" runat="server">
                                                                                </div>
                                                                                <%--  <nav class="stars" >
            										<ul>
            											<li><a href="#"><img alt="" src="img/star-b.png" /></a></li>
            											<li><a href="#"><img alt="" src="img/star-b.png" /></a></li>
            											<li><a href="#"><img alt="" src="img/star-b.png" /></a></li>
            											<li><a href="#"><img alt="" src="img/star-b.png" /></a></li>
            											<li><a href="#"><img alt="" src="img/star-a.png" /></a></li>
            										</ul>
            									
            									</nav>--%>
                                                                                <div class="cat-list-review" style="padding-bottom: 5px; padding-left: 10px;">
                                                                                </div>
                                                                                <div class="offer-slider-r" style="padding-bottom: 5px; padding-left: 10px;">
                                                                                    <asp:LinkButton ID="lbPrice" Style="text-decoration: none;" CssClass="offer-slider-r-label"
                                                                                        runat="server" Text='<%#  Eval("currcode").ToString()  + " " + Eval("totalsalevalue").ToString()  %>'
                                                                                        OnClick="lbPrice_Click"></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lbwlPrice" Style="text-decoration: none;" CssClass="offer-slider-r-label"
                                                                                        runat="server" Text='<%# Eval("wltotalsalevalue").ToString() %>' OnClick="lbwlPrice_Click"></asp:LinkButton>
                                                                                    <asp:HiddenField ID="hdwlCurrCode" Value='<%# Eval("wlcurrcode").ToString() %>' runat="server" />
                                                                                    <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                                                                                    <div class="clear" style="padding-bottom: 10px; padding-left: 15px;">
                                                                                    </div>
                                                                                    <asp:LinkButton ID="lblunits" Style="text-decoration: none;" CssClass="rate-plan-headings"
                                                                                        runat="server" Text="Units" OnClick="lbPrice_Click"></asp:LinkButton>
                                                                                    <asp:Label ID="lblPriceBy" CssClass="offer-slider-r-price-by-tour" runat="server"
                                                                                        Text="TOUR PRICE"></asp:Label>
                                                                                    <div class="clear" style="padding-top: 5px; margin-left: -10px;">
                                                                                        <asp:Label ID="lblIncTax" CssClass="offer-slider-r-price-by-tax" runat="server" Text="Incl of Taxes & vat"></asp:Label>
                                                                                    </div>
                                                                                    <div class="clear" style="padding-bottom: 10px; margin-left: -15px;">
                                                                                    </div>
                                                                                    <asp:Label ID="lblTourAdultChild" CssClass="offer-slider-r-price-by-tour1" runat="server"
                                                                                        Text=""></asp:Label>
                                                                                    <%--  <asp:Label ID="Label2" CssClass="offer-slider-r-price-by" runat="server" Text='<%# Eval("PriceBy") %>'></asp:Label>--%>
                                                                                </div>
                                                                                <div class="offer-slider-r" style="padding-bottom: 5px; padding-left: 10px;">
                                                                                    <div class="srch-tab-line no-margin-bottom tour-change-date-label">
                                                                                        <label>
                                                                                            select tour</label>
                                                                                        <div class="side-block jq-checkbox-tour" style="margin-top: 15px; margin-left: 25px;">
                                                                                            <asp:CheckBox runat="server" ID="chkSelectTour" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="clear">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br class="clear" />
                                                        </div>
                                                        <div>
                                                        </div>
                                                        <%--         <div class="clear"></div>--%>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                            <asp:ModalPopupExtender ID="mpMealPlanDatesPopup" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                CancelControlID="aMealPlanDatesPopup" EnableViewState="true" PopupControlID="pnlMealPlanDates"
                                                TargetControlID="hdMealPlanDatesPopup">
                                            </asp:ModalPopupExtender>
                                            <asp:HiddenField ID="hdMealPlanDatesPopup" runat="server" />
                                            <asp:Panel runat="server" ID="pnlMealPlanDates" Style="display: none;">
                                                <div class="roomtype-price-breakuppopup">
                                                    <div id="Div3">
                                                        <div class="roomtype-popup-title">
                                                           <asp:Label ID="lblMealPlanHead"  Text="Select Dates" runat="server"></asp:Label>
                                                            <a id="aMealPlanDatesPopup" href="#" class="roomtype-popup-close"></a>
                                                        </div>
                                                        <div class="roomtype-popup-description">
                                                         <asp:Label ID="lblMealPlanExcName" runat="server"></asp:Label>
                                                         
                                                            <div style="overflow: auto; min-height: 129px; max-height: 420px; min-width: 350px;
                                                                max-width: 850px; padding-bottom: 10px; margin-top: 10px;">
                                                                <div style="border: 0px solid #ede7e1; max-width: 850px; min-width: 150px; padding-left: 25px;
                                                                    padding-top: 5px; padding-bottom: 10px;">
                                                                    <asp:HiddenField ID="hdMealPlanExcCode" runat="server" />
                                                                    <asp:HiddenField ID="hdMealPlanVehicleCode" runat="server" />
                                                                    <asp:DataList ID="dlMealPlanMultipleDate" runat="server" Width="100%" RepeatColumns="3" RepeatDirection="Horizontal" >
                                                                        <ItemTemplate>
                                                                            <div style="width: 100%">
                                                                                <div id="Div4" runat="server">
                                                                                    <div class="tour-change-date-label"  style="width: 68%; float: left; padding-right: 5px;">
                                                                                        <asp:CheckBox ID="chkMealPlanDates" runat="server" />
                                                                                        <asp:Label ID="lblMeanPlanDates" Text='<%# Eval("AvailableDate") %>'  runat="server"></asp:Label>
                                                                                        <%--Text='<%# Eval("exctypname") %>' --%>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                </div>

                                                                   <div id="Div6" runat="server" style="padding-left: 10px; margin-bottom: 25px;">
                                                                <div style="padding-left: 300px; width: 30%;">
                                                                    <asp:Button ID="btnSaveMultipleDates" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px;"
                                                                        runat="server" Text="Save" />
                                                                </div>
                                                            </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:ModalPopupExtender ID="mpSelectComboDates" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                CancelControlID="aComboDateClose" EnableViewState="true" PopupControlID="pnlComboDates"
                                                TargetControlID="hdSelectComboDates">
                                            </asp:ModalPopupExtender>
                                            <asp:HiddenField ID="hdSelectComboDates" runat="server" />
                                            <asp:Panel runat="server" ID="pnlComboDates" Style="display: none;">
                                                <div class="roomtype-price-breakuppopup">
                                                    <div id="Div1">
                                                        <div class="roomtype-popup-title">
                                                            <asp:Label ID="lblComboExcName" runat="server"></asp:Label>
                                                            <a id="aComboDateClose" href="#" class="roomtype-popup-close"></a>
                                                        </div>
                                                        <div class="roomtype-popup-description">
                                                            <div id="Div2" runat="server" style="padding-left: 10px; margin-bottom: 25px;">
                                                                <div style="padding-left: 300px; width: 30%;">
                                                                    <asp:Button ID="btnSaveComboExcursion" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px;"
                                                                        runat="server" Text="Save" />
                                                                </div>
                                                            </div>
                                                            <div style="overflow: auto; min-height: 129px; max-height: 420px; min-width: 350px;
                                                                max-width: 850px; padding-bottom: 10px; margin-top: 10px;">
                                                                <div style="border: 0px solid #ede7e1; max-width: 850px; min-width: 150px; padding-left: 25px;
                                                                    padding-top: 5px; padding-bottom: 10px;">
                                                                    <asp:HiddenField ID="hdExcCodeComboPopup" runat="server" />
                                                                    <asp:HiddenField ID="hdVehicleCodeComboPopup" runat="server" />
                                                                    <asp:DataList ID="dlSelectComboDates" runat="server" Width="100%">
                                                                        <ItemTemplate>
                                                                            <div style="width: 100%">
                                                                                <div id="Div4" runat="server">
                                                                                    <div class="tour-change-date-label" style="width: 68%; float: left; padding-right: 5px;">
                                                                                        <label>
                                                                                            Excursion</label>
                                                                                        <div class="input-a">
                                                                                            <asp:Label ID="lblExcComboCode" Text='<%# Eval("exctypcombocode") %>' Style="display: none;"
                                                                                                runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblExcComboName" Text='<%# Eval("exctypname") %>' runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="tour-change-date-label" style="float: left; width: 30%;">
                                                                                        <label>
                                                                                            Select Date</label>
                                                                                        <div class="input-a">
                                                                                            <asp:TextBox ID="txtExcComboDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-tour-change-date" AutoComplete="off"
                                                                                                Style="z-index: 99999;"></asp:TextBox>
                                                                                            <span class="date-icon-tour"></span>
                                                                                        </div>
                                                                                    </div>
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
                                            </asp:Panel>



                                             <asp:ModalPopupExtender ID="mpTimeSlot" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                CancelControlID="aTimeSlotClose" EnableViewState="true" PopupControlID="pnlTimeSlot"
                                                TargetControlID="hdTimeSlot">
                                            </asp:ModalPopupExtender>
                                            <asp:HiddenField ID="hdTimeSlot" runat="server" />
                                            <asp:Panel runat="server" ID="pnlTimeSlot" Style="display: none;">
                                                <div class="roomtype-price-breakuppopup">
                                                    <div id="Div15">
                                                        <div class="roomtype-popup-title">
                                                            <asp:Label ID="Label1" Text="Time Slot" runat="server"></asp:Label>
                                                            <a id="aTimeSlotClose" href="#" class="roomtype-popup-close"></a>
                                                        </div>
                                                        <div class="roomtype-popup-description" style="min-height:250px;max-height:1050px;">
                                                        
                                                            <div style="overflow: auto; min-height: 129px; max-height: 1000px; min-width: 350px;
                                                                max-width: 850px; padding-bottom: 10px;">
                                                                <div style="border: 0px solid #ede7e1; max-width: 850px; min-width: 150px; padding-left: 15px;
                                                                    padding-top:0px; padding-bottom: 10px;">
                                                                    <asp:HiddenField ID="hdExcCodeTimeSlot" runat="server" />
                                                                    <asp:HiddenField ID="hdVehicleCodeTimeSlot" runat="server" />
                                                                     <asp:HiddenField ID="hdTimeSlotDate" runat="server" />
                                                 <asp:HiddenField ID="hdTimeSlotInventoryId" runat="server" />
                                                                    <asp:GridView ID="gvTimeSlot" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                                        Width="100%" GridLines="Horizontal">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Time" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                                <ItemTemplate>
                                                                                    <div class="roomtype-icons" style="padding-top: 5px;">
                                                                                        <div style="float: left;">
                                                                                            <asp:Label ID="lblTimeSlot" Text='<%# Eval("TIMESLOT") %>' runat="server"></asp:Label>
                                                                                      <asp:Label ID="lblTimeSlotFrom" Text='<%# Eval("TIMESLOT_FROM") %>' style="display:none"  runat="server"></asp:Label>
                                                                                       <asp:Label ID="lblTimeSlotTo" Text='<%# Eval("TIMESLOT_TO") %>' style="display:none"  runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="20%" />
                                                                                <ItemStyle Width="20%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Availability" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                                <ItemTemplate>
                                                                                    <div class="roomtype-icons" style="padding-top: 5px;">
                                                                                        <div style="float: left;"> 
                                                                                            <asp:Label ID="lblAvailability" runat="server"></asp:Label>
                                                                                               <asp:Label ID="lblAvailabilityCount" Text='<%# Eval("AVAILABILITY") %>' style="display:none;" runat="server"></asp:Label>
                                                                                           
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="20%" />
                                                                                <ItemStyle Width="20%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Adult" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                                <ItemTemplate>
                                                                                    <div class="roomtype-icons" style="padding-top: 5px;">
                                                                                        <div style="float: left;">
                                                                                            <asp:DropDownList ID="ddlTimeSlotAdult" runat="server">
                                                                                            </asp:DropDownList>
                                                                                               <asp:Label ID="lblTimeSlotAdult" style="display:none;" runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="20%" />
                                                                                <ItemStyle Width="20%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Child" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                                <ItemTemplate>
                                                                                    <div class="roomtype-icons" style="padding-top: 5px;">
                                                                                        <div style="float: left;">
                                                                                            <asp:DropDownList ID="ddlTimeSlotChild" runat="server">
                                                                                            </asp:DropDownList>
                                                                                             <asp:Label ID="lblTimeSlotChild"  style="display:none;" runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="20%" />
                                                                                <ItemStyle Width="20%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Senior" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                                <ItemTemplate>
                                                                                    <div class="roomtype-icons" style="padding-top: 5px;">
                                                                                        <div style="float: left;">
                                                                                            <asp:DropDownList ID="ddlTimeSlotSenior" runat="server">
                                                                                            </asp:DropDownList>
                                                                                             <asp:Label ID="lblTimeSlotSenior"  style="display:none;" runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="20%" />
                                                                                <ItemStyle Width="20%" />
                                                                            </asp:TemplateField>
                                                                        
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                        <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                </div>
                                                                <div id="dvWarning" runat="server" class="alert alert-danger" style="display:none;padding-left:15px;">
                                                                        <asp:Label ID="lblTimeSlotWarning" Text="" runat="server"></asp:Label>
                                                                         <asp:Label ID="lblTimeSlotWarning1" Text="" style="display:none" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="padding-bottom:15px; background-color:White !important;min-height:100px;max-height:500px;font-weight:bold;margin-left:15px;padding-right:15px;width:700px;">
                                                                    <div id="Div17" runat="server" style="padding-left: 10px; margin-bottom: 25px;width:70%;float:left;">
                                                                        <div  style="min-height:100px;max-height:500px;">
                                                                            <asp:Label ID="lblTimeSlotSelectionHeader" Width="100%" Text="You have selected following visit date and time."
                                                                                runat="server"></asp:Label>
                                                                                <div>            <asp:Label ID="lblTimeSlotSelectionDate" Text="Time Slot" runat="server"></asp:Label></div>
                                                                                <div>  <asp:Label ID="lblTimeSlotSelection" Text="" runat="server"></asp:Label></div>
                                                                
                                                                          
                                                                        </div>
                                                                    </div>
                                                                    <div id="Div16" runat="server" style="padding-left: 10px; padding-top:25px; margin-bottom: 25px;width:20%;float:left;">
                                                                        <div style="padding-bottom:15px;">
                                                                            <asp:Button ID="btnTimeSlotSave"  OnClientClick="return ValidateTimeSlotSave()"   CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px;"
                                                                                runat="server" Text="Save" />
                                                                        </div>
                                                                        <div>
                                                                            <asp:Button ID="btnTimeSlotCancel" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px;"
                                                                                runat="server" Text="Cancel" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>



                                            <asp:ModalPopupExtender ID="mpTotalprice" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                CancelControlID="atotalPriceClose" EnableViewState="true" PopupControlID="pnlTotalPrice"
                                                TargetControlID="hdpricePopup">
                                            </asp:ModalPopupExtender>
                                            <asp:HiddenField ID="hdpricePopup" runat="server" />
                                            <asp:Panel runat="server" ID="pnlTotalPrice" Style="display: none;">
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
                                                                <asp:HiddenField ID="hdExcCodePopup" runat="server" />
                                                                <asp:HiddenField ID="hdVehicleCodePopup" runat="server" />
                                                                <asp:HiddenField ID="hdRateBasisPopup" runat="server" />
                                                                <asp:HiddenField ID="hdCurrCodePopup" runat="server" />
                                                                <asp:HiddenField ID="hdnlineno" runat="server" />
                                                                <asp:HiddenField ID="hdSelectedDatePopup" runat="server" />
                                                                  <asp:HiddenField ID="hdMultiDay" runat="server" />
                                                                <div style="padding-left: 300px; width: 30%;">
                                                                    <asp:Button ID="btnPriceBreakupSave" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px;"
                                                                        runat="server" Text="Save" />
                                                                </div>
                                                            </div>
                                                            <div style="overflow: auto; min-height: 129px; max-height: 420px; min-width: 350px;
                                                                max-width: 450px; padding-bottom: 10px; margin-top: 10px;">
                                                                <div style="border: 0px solid #ede7e1; max-width: 450px; min-width: 200px; padding-left: 25px;
                                                                    padding-top: 5px; padding-bottom: 10px;">
                                                                    <div style="width: 100%">
                                                                        <div id="dvACS" runat="server">
                                                                            <div class="search-large-i tour-change-date-label">
                                                                                <label>
                                                                                    No Of Adults</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblNoOfAdult" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox></div>
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label">
                                                                                <label>
                                                                                    Adult Price</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="txtAdultPrice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlAdultPrice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left; width: 27%;">
                                                                                <label>
                                                                                    Adult Sale Value</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblAdultSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlAdultSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox></div>
                                                                            </div>
                                                                            <div class="clear">
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left;">
                                                                                <label>
                                                                                    No Of Child</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblNoOfchild" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox></div>
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left;">
                                                                                <label>
                                                                                    Child Price</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="txtChildprice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlChildprice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left; width: 27%;
                                                                                margin-left: 15px;">
                                                                                <label>
                                                                                    Child Sale Value</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblchildSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlChildSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="clear">
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left;">
                                                                                <label>
                                                                                    No Of</label>
                                                                                     <div class="clear">
                                                                                    </div>
                                                                                     <label>
                                                                                     Child As Adult</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblNoOfchildasadult" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox></div>
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left; margin-left: 2px;">
                                                                                <label>
                                                                                    Child As </label>
                                                                                              <div class="clear">
                                                                                 </div>
                                                                                      <label>
                                                                             
                                                                                    Adult Price</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="txtChildasadultprice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlChildasadultprice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                             <div class="search-large-i tour-change-date-label" style="float: left; width: 27%;
                                                                                margin-left: 15px;">
                                                                               
                                                                                <label>
                                                                                    Child As</label>
                                                                                     <div class="clear">
                                                                                 </div>
                                                                                     <label>
                                                                                    Adult Sale Value</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblchildasadultSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlChildasadultSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>

                                                                            <div class="clear">
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left;">
                                                                                <label>
                                                                                    No Of Seniors</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblNoOfSeniors" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox></div>
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label">
                                                                                <label>
                                                                                    Seniors Price</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="txtSeniorsPrice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlSeniorsPrice" onkeypress="validateDecimalOnly(event,this)"
                                                                                        runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left; width: 27%;
                                                                                margin-left: 15px;">
                                                                                <label>
                                                                                    Seniors Sale Value</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblSeniorSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlSeniorSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="clear">
                                                                            <asp:HiddenField ID="hdChildAsFreePopup" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                        <div id="dvUnits" runat="server">
                                                                            <div class="search-large-i tour-change-date-label" style="float: left;">
                                                                                <label>
                                                                                    No Of Units</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblNoOfUnits" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox></div>
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left;" id="dvUnitprice"
                                                                                runat="server">
                                                                                <label>
                                                                                    Unit Price</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="txtUnitPrice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlUnitPrice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="search-large-i tour-change-date-label" style="float: left;" id="dvunitsalevalue"
                                                                                runat="server">
                                                                                <label>
                                                                                    Unit Sale Value</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="lblUnitSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtwlUnitSaleValue" onkeydown="fnReadOnly(event)" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <!-- \\ -->
                                        </div>
                                        <div>
                                            <asp:Repeater ID="rptPager" runat="server">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                        Enabled="true" CssClass='<%# If(Convert.ToBoolean(Eval("Enabled")), "page_enabled", "page_disabled")%>'
                                                        OnClick="Page_Changed" OnClientClick='<%# If(Not Convert.ToBoolean(Eval("Enabled")), "return false;", "") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br class="clear" />
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
        function SelectedTour(chk, rowNumber, type) {
            var hd = document.getElementById("<%= hddlRowNumber.ClientID %>");
            var hdClickFrom = document.getElementById("<%= hdClickFrom.ClientID %>");
            hdClickFrom.value = type;
            hd.value = rowNumber;
            if (type == 'd') {
                var chkbox = document.getElementById(chk);
                if (chkbox.checked == true) {
                    showDialog('Alert Message', 'Please uncheck checkbox and change date.', 'warning');
                    return false;
                }
            }

            document.getElementById("<%= btnSelectedTourChecknox.ClientID %>").click();
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

        function CallFilter() {
            // alert('testt');
            document.getElementById("<%= btnFilter.ClientID %>").click();
        }
        function CallFilterForRoomClassification() {

            document.getElementById("<%= btnFilterForRoom.ClientID %>").click();
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
      <asp:HiddenField ID="hdClickFrom" runat="server" />
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
