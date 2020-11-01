<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AirportMeetFreeFormBooking.aspx.vb"
    Inherits="AirportMeetFreeFormBooking" %>

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

    <%--***Danny 18/08/2018 fa fa-star--%>
    <link href="css/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
 <%-- <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">--%>
    

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
       
  
  </style>
<%--    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"
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

        $(document).ready(function () {
            fnRateBasisHide();
        });
        function fnRateBasisHide() {
            var dataListid = '<%= dlAirportMeetResults.ClientID %>';
            var obj = document.getElementById(dataListid);
            if (obj != 'undefined' || obj != null) {
                for (var i = 0; i < obj.getElementsByTagName('input').length; i++) {
                    var dd = obj.getElementsByTagName('input')[i].id;
                    if (dd.indexOf("txtAMDate") >= 0) {

                        var txtAirportMeetCode = document.getElementById(dd.replace("txtAMDate", "txtAirportMeetCode"));

                        var dvACS = document.getElementById(dd.replace("txtAMDate", "dvACS"));
                        var dvUnit = document.getElementById(dd.replace("txtAMDate", "dvUnit"));
                        var vv = txtAirportMeetCode.value.split('|');

                        if (vv[1] == 'Unit') {
                            dvACS.removeAttribute("style");
                            dvUnit.removeAttribute("style");
                            dvACS.setAttribute("style", "display:none");
                            dvUnit.setAttribute("style", "display:block");
                        }
                        else {
                            dvACS.removeAttribute("style");
                            dvUnit.removeAttribute("style");
                            dvACS.setAttribute("style", "display:block");
                            dvUnit.setAttribute("style", "display:none");

                        }

                        var ddlAMChild = dd.replace("txtAMDate", "ddlAMChild");
                        var divAMChild = dd.replace("txtAMDate", "divAMChild");
                        var divAMChild1 = dd.replace("txtAMDate", "dvAMChild1");
                        var divAMChild2 = dd.replace("txtAMDate", "dvAMChild2");
                        var divAMChild3 = dd.replace("txtAMDate", "dvAMChild3");
                        var divAMChild4 = dd.replace("txtAMDate", "dvAMChild4");
                        var divAMChild5 = dd.replace("txtAMDate", "dvAMChild5");
                        var divAMChild6 = dd.replace("txtAMDate", "dvAMChild6");


                        //                        var ddlAMChild = document.getElementById(dd.replace("txtAMDate", "ddlAMChild"));
                        //                        var divAMChild = document.getElementById(dd.replace("txtAMDate", "divAMChild"));
                        //                        var divAMChild1 = document.getElementById(dd.replace("txtAMDate", "dvAMChild1"));
                        //                        var divAMChild2 = document.getElementById(dd.replace("txtAMDate", "dvAMChild2"));
                        //                        var divAMChild3 = document.getElementById(dd.replace("txtAMDate", "dvAMChild3"));
                        //                        var divAMChild4 = document.getElementById(dd.replace("txtAMDate", "dvAMChild4"));
                        //                        var divAMChild5 = document.getElementById(dd.replace("txtAMDate", "dvAMChild5"));
                        //                        var divAMChild6 = document.getElementById(dd.replace("txtAMDate", "dvAMChild6"));





                        //                        var child = ddlAMChild.value;
                        //                        if (child == 0) {
                        //                            divAMChild.removeAttribute("style");
                        //                            divAMChild.setAttribute("style", "display:none;");
                        //                        }
                        //                        else if (child == 1) {

                        //                            divAMChild1.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild2.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild3.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild4.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild5.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild6.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild.setAttribute("style", "display:block;");
                        //                        }
                        //                        else if (child == 2) {


                        //                            divAMChild1.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild2.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild3.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild4.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild5.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild6.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild.setAttribute("style", "display:block;");

                        //                        }
                        //                        else if (child == 3) {


                        //                            divAMChild1.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild2.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild3.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild4.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild5.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild6.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild.setAttribute("style", "display:block;");

                        //                        }
                        //                        else if (child == 4) {


                        //                            divAMChild1.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild2.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild3.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild4.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild5.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild6.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild.setAttribute("style", "display:block;");

                        //                        }
                        //                        else if (child == 5) {


                        //                            divAMChild1.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild2.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild3.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild4.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild5.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild6.setAttribute("style", "float:left;display:none;");
                        //                            divAMChild.setAttribute("style", "display:block;");

                        //                        }
                        //                        else if (child == 6) {

                        //                            divAMChild1.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild2.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild3.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild4.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild5.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild6.setAttribute("style", "float:left;display:block;");
                        //                            divAMChild.setAttribute("style", "display:block;");

                        //                        }






                        AMChildChanged(ddlAMChild, divAMChild, divAMChild1, divAMChild2, divAMChild3, divAMChild4, divAMChild5, divAMChild6, 'addrow');


                        //ID:119 modified by abin on 20180925 ========= Satrt


                        var ddlAirportMeetType = dd.replace("txtAMDate", "ddlAirportMeetType");
                        var lblAirportMeetDate = dd.replace("txtAMDate", "lblAirportMeetDate");
                        var lblAirportMeetFrom = dd.replace("txtAMDate", "lblAirportMeetFrom");
                        var lblAirportMeetTo = dd.replace("txtAMDate", "lblAirportMeetTo");
                        var dvFlightDetails = dd.replace("txtAMDate", "dvFlightDetails");
                        var dvTransitpart = dd.replace("txtAMDate", "dvTransitpart");
                        var dvUnit = dd.replace("txtAMDate", "dvUnit");
                        var dvACS = dd.replace("txtAMDate", "dvACS");
                        var lblAirportMeetType = dd.replace("txtAMDate", "lblAirportMeetType");

                        AirportMeetTypeChanged(ddlAirportMeetType, lblAirportMeetDate, lblAirportMeetFrom, lblAirportMeetTo, dvFlightDetails, dvTransitpart, dvUnit, dvACS, lblAirportMeetType)


                        var txtServiceCode = document.getElementById(dd.replace("txtAMDate", "txtAirportMeetCode")).value;

                        var dvACS1 = document.getElementById(dvACS);
                        var dvUnit1 = document.getElementById(dvUnit);
                        if (txtServiceCode != '') {
                            var vv = txtServiceCode.split('|');
                            if (vv[1] == 'Unit') {
                                dvACS1.removeAttribute("style");
                                dvUnit1.removeAttribute("style");
                                dvACS1.setAttribute("style", "display:none");
                                dvUnit1.setAttribute("style", "display:block");
                            }
                            else {

                                dvACS1.removeAttribute("style");
                                dvUnit1.removeAttribute("style");
                                dvACS1.setAttribute("style", "display:block");
                                dvUnit1.setAttribute("style", "display:none");

                            }
                        }
                        else {
                            dvACS1.removeAttribute("style");
                            dvUnit1.removeAttribute("style");
                            dvACS1.setAttribute("style", "display:none");
                            dvUnit1.setAttribute("style", "display:none");

                        }

                        //ID:119 modified by abin on 20180925 ========= end

                    }
                }

            }
        }

        function CalculateACSTotalValuewithCost(Adult) {
            var ddlAdult = document.getElementById(Adult);
            var ddlChild = document.getElementById(Adult.replace("ddlAMAdult", "ddlAMChild"));
            var txtAdultPrice = document.getElementById(Adult.replace("ddlAMAdult", "txtAdultPrice"));
            var txtAdultSaleValue = document.getElementById(Adult.replace("ddlAMAdult", "txtAdultSaleValue"));
            var txtChildPrice = document.getElementById(Adult.replace("ddlAMAdult", "txtChildPrice"));
            var txtChildSaleValue = document.getElementById(Adult.replace("ddlAMAdult", "txtChildSaleValue"));
            var txtACSTotalSaleValue = document.getElementById(Adult.replace("ddlAMAdult", "txtACSTotalSaleValue"));
            var txtAdultCostPrice = document.getElementById(Adult.replace("ddlAMAdult", "txtAdultCostPrice"));
            var txtAdultCostValue = document.getElementById(Adult.replace("ddlAMAdult", "txtAdultCostValue"));
            var txtChildCostPrice = document.getElementById(Adult.replace("ddlAMAdult", "txtChildCostPrice"));
            var txtChildCostValue = document.getElementById(Adult.replace("ddlAMAdult", "txtChildCostValue"));
            var txtACSTotalCostValue = document.getElementById(Adult.replace("ddlAMAdult", "txtACSTotalCostValue"));

            if (ddlAdult.value == 0) {
                showDialog('Alert Message', 'Please select adult.', 'warning');
                HideProgess();
                return false;
            }




            txtAdultSaleValue.value = (parseFloat(fnSetDefaultToZero(ddlAdult.value)) * parseFloat(fnSetDefaultToZero(txtAdultPrice.value)));
            txtChildSaleValue.value = (parseFloat(fnSetDefaultToZero(ddlChild.value)) * parseFloat(fnSetDefaultToZero(txtChildPrice.value)));
            txtACSTotalSaleValue.value = (parseFloat(fnSetDefaultToZero(ddlAdult.value)) * parseFloat(fnSetDefaultToZero(txtAdultPrice.value))) + (parseFloat(fnSetDefaultToZero(ddlChild.value)) * parseFloat(fnSetDefaultToZero(txtChildPrice.value)));

            txtAdultCostValue.value = (parseFloat(fnSetDefaultToZero(ddlAdult.value)) * parseFloat(fnSetDefaultToZero(txtAdultCostPrice.value)));
            txtChildCostValue.value = (parseFloat(fnSetDefaultToZero(ddlChild.value)) * parseFloat(fnSetDefaultToZero(txtChildCostPrice.value)));
            txtACSTotalCostValue.value = (parseFloat(fnSetDefaultToZero(ddlAdult.value)) * parseFloat(fnSetDefaultToZero(txtAdultCostPrice.value))) + (parseFloat(fnSetDefaultToZero(ddlChild.value)) * parseFloat(fnSetDefaultToZero(txtChildCostPrice.value)));
        }

        function fnSetDefaultToZero(value) {
            if (value == '') {
                value = '0';
            }
            return value;
        }

        function CalculateTotalValuewithCost(NoOfUnit, UnitPrice, UnitSaleValue, NoOfAddionalPax, AdditionalPaxPrice, AdditionalPaxValue, TotalSaleValue, CostPricePax, CostPricePaxTotal, AddionalPaxCostPrice, AddionalPaxCostValue, TotalCostValue) {

            var txtNoOfUnit = document.getElementById(NoOfUnit);
            var txtUnitPrice = document.getElementById(UnitPrice);
            var txtUnitSaleValue = document.getElementById(UnitSaleValue);
            var txtNoOfAddionalPax = document.getElementById(NoOfAddionalPax);
            var txtAdditionalPaxPrice = document.getElementById(AdditionalPaxPrice);
            var txtAdditionalPaxValue = document.getElementById(AdditionalPaxValue);
            var txtTotalSaleValue = document.getElementById(TotalSaleValue);

            var txtCostPricePax = document.getElementById(CostPricePax);
            var txtCostPricePaxTotal = document.getElementById(CostPricePaxTotal);
            var txtAddionalPaxCostPrice = document.getElementById(AddionalPaxCostPrice);
            var txtAddionalPaxCostValue = document.getElementById(AddionalPaxCostValue);
            var txtTotalCostValue = document.getElementById(TotalCostValue);



            if (txtNoOfUnit.value == 0) {
                txtNoOfUnit.value = '1';
            }
            if (txtUnitPrice.value == '') {
                txtUnitPrice.value = '0';
            }
            if (txtNoOfAddionalPax.value == '') {
                txtNoOfAddionalPax.value = '0';
            }
            if (txtAdditionalPaxPrice.value == '') {
                txtAdditionalPaxPrice.value = '0';
            }
            if (txtCostPricePax.value == '') {
                txtCostPricePax.value = '0';
            }
            if (txtAddionalPaxCostPrice.value == '') {
                txtAddionalPaxCostPrice.value = '0';
            }


            txtUnitSaleValue.value = (parseFloat(txtNoOfUnit.value) * parseFloat(txtUnitPrice.value));
            txtAdditionalPaxValue.value = (parseFloat(txtNoOfAddionalPax.value) * parseFloat(txtAdditionalPaxPrice.value));
            txtTotalSaleValue.value = (parseFloat(txtNoOfUnit.value) * parseFloat(txtUnitPrice.value)) + (parseFloat(txtNoOfAddionalPax.value) * parseFloat(txtAdditionalPaxPrice.value));

            txtCostPricePaxTotal.value = (parseFloat(txtNoOfUnit.value) * parseFloat(txtCostPricePax.value));
            txtAddionalPaxCostValue.value = (parseFloat(txtNoOfAddionalPax.value) * parseFloat(txtAddionalPaxCostPrice.value));
            txtTotalCostValue.value = (parseFloat(txtNoOfUnit.value) * parseFloat(txtCostPricePax.value)) + (parseFloat(txtNoOfAddionalPax.value) * parseFloat(txtAddionalPaxCostPrice.value));

            if (txtNoOfUnit.value == '0') {
                txtNoOfUnit.value = '';
            }
            if (txtUnitPrice.value == '0') {
                txtUnitPrice.value = '';
            }
            if (txtNoOfAddionalPax.value == '0') {
                txtNoOfAddionalPax.value = '';
            }
            if (txtAdditionalPaxPrice.value == '0') {
                txtAdditionalPaxPrice.value = '';
            }
            if (txtCostPricePax.value == '0') {
                txtCostPricePax.value = '';
            }
            if (txtAddionalPaxCostPrice.value == '0') {
                txtAddionalPaxCostPrice.value = '';
            }
            if (txtUnitSaleValue.value == '0') {
                txtUnitSaleValue.value = '';
            }
            if (txtAdditionalPaxValue.value == '0') {
                txtAdditionalPaxValue.value = '';
            }
            if (txtTotalSaleValue.value == '0') {
                txtTotalSaleValue.value = '';
            }
            if (txtCostPricePaxTotal.value == '0') {
                txtCostPricePaxTotal.value = '';
            }
            if (txtAddionalPaxCostValue.value == '0') {
                txtAddionalPaxCostValue.value = '';
            }
            if (txtTotalCostValue.value == '0') {
                txtTotalCostValue.value = '';
            }


        }


        function calculatetotalvalue(txtunits, txtunitprice, txttotalamt) {

            txtunits = document.getElementById(txtunits);

            txtunitprice = document.getElementById(txtunitprice);

            txttotalamt = document.getElementById(txttotalamt);


            if (txtunits.value == 0) {
                txtunits.value = '1';
            }
            if (txtunitprice.value == '') {
                txtunitprice.value = '0';
            }


            var totalamt = (parseFloat(txtunitprice.value) * parseFloat(txtunits.value));

            txttotalamt.value = totalamt;


        }


        function AirportMeetListautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {

                var hiddenfieldID = source.get_id().replace("AutoCompleteExtendertxtAirportMeet", "txtAirportMeetCode");
                document.getElementById(hiddenfieldID).value = eventArgs.get_value();
                var dvACS = document.getElementById(source.get_id().replace("AutoCompleteExtendertxtAirportMeet", "dvACS"));
                var dvUnit = document.getElementById(source.get_id().replace("AutoCompleteExtendertxtAirportMeet", "dvUnit"));
                var vv = eventArgs.get_value().split('|');

                if (vv[1] == 'Unit') {
                    dvACS.removeAttribute("style");
                    dvUnit.removeAttribute("style");
                    dvACS.setAttribute("style", "display:none");
                    dvUnit.setAttribute("style", "display:block");
                }
                else {

                    dvACS.removeAttribute("style");
                    dvUnit.removeAttribute("style");
                    dvACS.setAttribute("style", "display:block");
                    dvUnit.setAttribute("style", "display:none");

                }

            }
            else {
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtendertxtAirportMeet", "txtAirportMeetCode");
                document.getElementById(hiddenfieldID).value = '';
            }
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

        function AMAirportMeetToAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtAirportMeetTo", "txtAirportMeetTocode");
                    document.getElementById(hiddenfieldID).value = eventArgs.get_value();
                }
                else {
                    var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtAirportMeetTo", "txtAirportMeetTocode");
                    document.getElementById(hiddenfieldID).value = '';
                }
            }

        }
        function AMAirportMeetFromAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtAirportMeetFrom", "txtAirportMeetFromcode");
                    document.getElementById(hiddenfieldID).value = eventArgs.get_value();

                }
                else {
                    var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtAirportMeetFrom", "txtAirportMeetFromCode");
                    document.getElementById(hiddenfieldID).value = '';
                }
            }

        }

        function AirportMeetToContextKey(ddlAirportMeetType, extnder) {

            var AirportMeetType = document.getElementById(ddlAirportMeetType).value;

            $find(extnder).set_contextKey(AirportMeetType);

        }
        function AirportMeetSetContextKey(ddlAirportMeetType, extender) {
            var AirportMeetType = document.getElementById(ddlAirportMeetType).value;

            $find(extender).set_contextKey(AirportMeetType);
        }

        function AirportMeetFromContextKey(ddlAirportMeetType, extnder) {

            var AirportMeetType = document.getElementById(ddlAirportMeetType).value;

            $find(extnder).set_contextKey(AirportMeetType);

        }

        function flightSetContextKey(ddlAirportMeetType, txtAMDate, extnder) {

            var AirportMeetType = document.getElementById(ddlAirportMeetType).value;
            var AMDate = document.getElementById(txtAMDate).value;
            if (AMDate == '') {
                showDialog('Alert Message', 'Please select date.', 'warning');
                HideProgess();
                return false;
            }
            $find(extnder).set_contextKey(AirportMeetType + '|' + AMDate);

        }

        function flightAutocompleteSelected(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtflightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalTime");
                var ddlAirportMeetType = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "ddlAirportMeetType");
                var txtAirportMeetFrom = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtAirportMeetFrom");
                var txtAirportMeetFromCode = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtAirportMeetFromcode");
                var txtAirportMeetTo = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtAirportMeetTo");
                var txtAirportMeetToCode = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtAirportMeetTocode");

                $get(hiddenfieldID).value = eventArgs.get_value();
                if ($get(ddlAirportMeetType).value == 'ARRIVAL') {

                    GetAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, txtAirportMeetFrom, txtAirportMeetFromCode);

                }
                else if ($get(ddlAirportMeetType).value == 'DEPARTURE') {
                    GetDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, txtAirportMeetFrom, txtAirportMeetFromCode);
                }
                else if ($get(ddlAirportMeetType).value == 'TRANSIT') {
                    GetMATransitAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, txtAirportMeetFrom, txtAirportMeetFromCode);

                }

            }

        }
        function transitflightAutocompleteSelected(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtendertxtTransitFlight", "txtTransitFlightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtendertxtTransitFlight", "txtTransitFlightTime");
                var ddlAirportMeetType = source.get_id().replace("AutoCompleteExtendertxtTransitFlight", "ddlAirportMeetType");
                var txtAirportMeetFrom = source.get_id().replace("AutoCompleteExtendertxtTransitFlight", "txtAirportMeetFrom");
                var txtAirportMeetFromCode = source.get_id().replace("AutoCompleteExtendertxtTransitFlight", "txtAirportMeetFromcode");
                var txtAirportMeetTo = source.get_id().replace("AutoCompleteExtendertxtTransitFlight", "txtAirportMeetTo");
                var txtAirportMeetToCode = source.get_id().replace("AutoCompleteExtendertxtTransitFlight", "txtAirportMeetTocode");

                $get(hiddenfieldID).value = eventArgs.get_value();
                if ($get(ddlAirportMeetType).value == 'ARRIVAL') {

                    GetAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, txtAirportMeetFrom, txtAirportMeetFromCode);

                }
                else if ($get(ddlAirportMeetType).value == 'DEPARTURE') {
                    GetDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, txtAirportMeetTo, txtAirportMeetToCode);
                }
                else if ($get(ddlAirportMeetType).value == 'TRANSIT') {
                    GetMATransitAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, txtAirportMeetTo, txtAirportMeetToCode);

                }

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



        function AMChildChanged(ddlAMChild, divAMChild, divAMChild1, divAMChild2, divAMChild3, divAMChild4, divAMChild5, divAMChild6, type) {
            var child = document.getElementById(ddlAMChild).value;
            if (child == 0) {
                document.getElementById(divAMChild).removeAttribute("style");
                document.getElementById(divAMChild).setAttribute("style", "display:none;");
            }
            else if (child == 1) {

                document.getElementById(divAMChild1).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild2).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild3).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild4).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild5).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild6).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild).setAttribute("style", "display:block;");
            }
            else if (child == 2) {


                document.getElementById(divAMChild1).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild2).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild3).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild4).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild5).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild6).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild).setAttribute("style", "display:block;");

            }
            else if (child == 3) {


                document.getElementById(divAMChild1).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild2).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild3).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild4).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild5).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild6).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild).setAttribute("style", "display:block;");

            }
            else if (child == 4) {


                document.getElementById(divAMChild1).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild2).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild3).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild4).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild5).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild6).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild).setAttribute("style", "display:block;");

            }
            else if (child == 5) {


                document.getElementById(divAMChild1).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild2).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild3).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild4).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild5).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild6).setAttribute("style", "float:left;display:none;");
                document.getElementById(divAMChild).setAttribute("style", "display:block;");

            }
            else if (child == 6) {

                document.getElementById(divAMChild1).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild2).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild3).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild4).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild5).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild6).setAttribute("style", "float:left;display:block;");
                document.getElementById(divAMChild).setAttribute("style", "display:block;");

            }
            if (type != 'addrow') {
                CalculateACSTotalValuewithCost(ddlAMChild.replace("ddlAMChild", "ddlAMAdult"))
            }

        }

        function AirportMeetTypeChanged(ddlAirportMeetType, lblAirportMeetDate, lblAirportMeetFrom, lblAirportMeetTo, dvFlightDetails, dvTransitpart, dvUnit, dvACS, lblAirportMeetType) {
            var AirportMeetType = document.getElementById(ddlAirportMeetType).value;
            document.getElementById(lblAirportMeetType).value = AirportMeetType;
            if (AirportMeetType == 'ARRIVAL') {

                document.getElementById(lblAirportMeetDate).innerHTML = 'ARRIVAL DATE';
                document.getElementById(lblAirportMeetFrom).innerHTML = 'ARRIVAL AIRPORT';
                document.getElementById(lblAirportMeetTo).innerHTML = 'DROP OFF HOTELS / UAE LOCATIONS';
                document.getElementById(dvFlightDetails).removeAttribute("style");
                document.getElementById(dvFlightDetails).setAttribute("style", "float:left;display:block;");
                document.getElementById(dvTransitpart).setAttribute("style", "display:none;");
                document.getElementById(dvUnit).setAttribute("style", "display:none;");
                document.getElementById(dvACS).setAttribute("style", "display:none;");


            }
            else if (AirportMeetType == 'DEPARTURE') {
                document.getElementById(lblAirportMeetDate).innerHTML = 'DEPARTURE DATE';
                document.getElementById(lblAirportMeetFrom).innerHTML = 'DEPARTURE AIRPORT';
                document.getElementById(lblAirportMeetTo).innerHTML = 'AIRPORT DROP OFF';
                document.getElementById(dvFlightDetails).removeAttribute("style");
                document.getElementById(dvFlightDetails).setAttribute("style", "float:left;display:block;");
                document.getElementById(dvTransitpart).setAttribute("style", "display:none;");
                document.getElementById(dvUnit).setAttribute("style", "display:none;");
                document.getElementById(dvACS).setAttribute("style", "display:none;");

            }
            else if (AirportMeetType == 'TRANSIT') {
                document.getElementById(lblAirportMeetDate).innerHTML = 'TRANSFER DATE';
                document.getElementById(lblAirportMeetFrom).innerHTML = 'TRANSIT ARRIVAL AIRPORT';
                document.getElementById(lblAirportMeetTo).innerHTML = 'TRANSIT DEPARTURE AIRPORT';

                document.getElementById(dvFlightDetails).removeAttribute("style");
                document.getElementById(dvFlightDetails).setAttribute("style", "float:left;display:block;");
                document.getElementById(dvTransitpart).setAttribute("style", "display:block;");
                document.getElementById(dvUnit).setAttribute("style", "display:none;");
                document.getElementById(dvACS).setAttribute("style", "display:none;");

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


        //**Document read end **




        function format_float(number, extra_precision) {
            precision = float_exponent(number) + (extra_precision || 0)
            return number.toFixed(precision).split(/\.?0+$/)[0]
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


  


   


    </script>

    <script type="text/javascript">


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestUserControl);
        function EndRequestUserControl(sender, args) {



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

            fnRateBasisHide();
            var date = new Date();
            var currentMonth = date.getMonth() - 2;
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();
            $(".date-inpt-check-in-freeform").datepicker({

                minDate: new Date(currentYear, currentMonth, currentDate)

            });

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
<body onload="RefreshContent()">
    <form id="form1" runat="server">
    <!-- // authorize // -->
    <div class="overlay">
    </div>
    <!-- \\ authorize \\-->
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>
    <header id="top">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
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
        <div class="body-wrapper">
            <div class="wrapper-padding">
                <div class="page-head">
                    <div class="page-title">
                        Airport Meet - <span>FREE FORM BOOKING</span></div>
                    <div class="breadcrumbs">
                        <a href="Home.aspx?Tab=0">Home</a> / <a href="#">Airport Meet Free Form Booking</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="page-head">
                    <div class="page-search-content-search">
                        <div id="dvAirportMeetsContent" runat="server">
                        

                          
                                    <div class="search-tab-content">
                                        <div class="page-search-p">
                                        
                                               <asp:UpdatePanel ID="UpdatePanel5" runat="server"><ContentTemplate>
                                            <div class="clear">
                                            </div>
                                            <div class="search-large-i"  style="float: left;padding-top:20px;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        Agent</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtCustomer" runat="server"   autocomplete="off"  placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtCustomerCode" runat="server" Style="display: none;"></asp:TextBox>
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
                                            <div class="search-large-i"  style="float: left;padding-top:20px;">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        Source Country</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtCountry" runat="server"  autocomplete="off"  placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtCountryCode" Style="display: none;" runat="server"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCountry" runat="server" CompletionInterval="10"
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtCountry"
                                                            UseContextKey="true" OnClientItemSelected="Countryautocompleteselected">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                                   <div class="search-large-i" style="float: left; padding-top: 20px;padding-left: 20px;width:25%;">
                                                  
                                                   </div>
                                            <div class="clear">
                                            </div>
                                            <div class="clear" style="padding-top:15px;">
                                            </div>
                               

                                             <asp:DataList ID="dlAirportMeetResults" runat="server" Width="100%">
                                                <ItemTemplate>
                                                       <div class="search-large-i">
                                                <!-- // --> <asp:Label ID="lbltlineno" runat="server" style="display:none;" Text='<%# Eval("alineno") %>'  ></asp:Label>
                                               
                                                           <div class="srch-tab-line no-margin-bottom">
                                                               <div class="srch-tab-left">
                                                                   <label>
                                                                       AirportMeet TYPE</label>
                                                                   <div class="select-wrapper">
                                                                       <asp:DropDownList ID="ddlAirportMeetType" runat="server"  class="custom-select-free-form">
                                                                           <asp:ListItem>ARRIVAL</asp:ListItem>
                                                                           <asp:ListItem>DEPARTURE</asp:ListItem>
                                                                           <asp:ListItem>TRANSIT</asp:ListItem>
                                                                       </asp:DropDownList>
                                                                        <asp:Label ID="lblAirportMeetType" runat="server" style="display:none;" Text='<%# Eval("airportmatype") %>'  ></asp:Label>
                                                                         <asp:Label ID="lblComplSup" runat="server" style="display:none;" Text='<%# Eval("complimentarycust") %>'  ></asp:Label>
                                                                   </div>
                                                               </div>
                                                               <div class="srch-tab-left" style="padding-left: 15px;">
                                                                   <asp:Label ID="lblAirportMeetDate" runat="server" CssClass="page-search-content-search-asplabel"
                                                                       Text="ARRIVAL DATE"></asp:Label>
                                                                   <div class="input-a">
                                                                       <asp:TextBox ID="txtAMDate" class="date-inpt-check-in-freeform" placeholder="dd/mm/yyyy" Text='<%# Eval("vairportmadate") %>'
                                                                           AutoComplete="off" runat="server"></asp:TextBox>
                                                                       <span class="date-icon"></span>
                                                                   </div>
                                                               </div>
                                                           </div>
                                                           <!-- \\ -->
                                                       </div>
                                                    <div id="dvFlightDetails" runat="server" class="search-large-i" style="float: left;">
                                                        <!-- // -->
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                FLIGHT CLASS</label>
                                                            <div class="select-wrapper">
                                                                <asp:DropDownList ID="ddlAMFlightClass" runat="server" Width="100%" Height="26px"
                                                                    class="custom-select-free-form">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                FLIGHT NO</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtflight"  Text='<%# Eval("flightcode") %>'  placeholder="--" runat="server"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtArrivalflight" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetFlight" TargetControlID="txtflight" 
                                                                    UseContextKey="true" OnClientItemSelected="flightAutocompleteSelected">
                                                                </asp:AutoCompleteExtender>
                                                                <asp:TextBox ID="txtflightCode"  Text='<%# Eval("flightcode") %>' Style="display: none" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                TIME</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtArrivalTime"  Text='<%# Eval("flighttime") %>' onkeydown="fnReadOnly(this)"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="search-large-i" style="float: left;">
                                                        <!-- /AM Arrival flight/ -->
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <asp:Label ID="lblAirportMeetFrom" runat="server" CssClass="page-search-content-search-asplabel"
                                                                Text="Arrival Airport"></asp:Label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtAirportMeetFrom"  Text='<%# Eval("airportbordername") %>'  placeholder="--" runat="server"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtAirportMeetFrom" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetAirportMeetFrom" TargetControlID="txtAirportMeetFrom"
                                                                    UseContextKey="true" OnClientItemSelected="AMAirportMeetFromAutocompleteSelected">
                                                                </asp:AutoCompleteExtender>
                                                                <asp:TextBox ID="txtAirportMeetFromcode"  Text='<%# Eval("airportbordercode") %>'  Style="display: none" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <!-- \\ -->
                                                    </div>
                                                    <div class="clear">
                                                    </div>

                                                    <div id="dvTransitpart" runat="server">

                                                    <div class="search-large-i" style="padding-top: 20px;display:none;">
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <div class="srch-tab-left">
                                                            </div>
                                                            <div class="srch-tab-right" style="padding-left:0px;">
                                                                <asp:Label ID="Label2" runat="server" CssClass="page-search-content-search-asplabel"
                                                                    Text="TRANSIT DEPARTURE DATE"></asp:Label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTransitDepDate" class="date-inpt-check-in-freeform" placeholder="dd/mm/yyyy"
                                                                        Text='<%# Eval("vairportmadate") %>' AutoComplete="off" runat="server"></asp:TextBox>
                                                                    <span class="date-icon"></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- \\ -->
                                                    </div>
                                                    <div id="Div1" runat="server" class="search-large-i" style="padding-top: 20px;float:left;">
                                                        <!-- // -->
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                FLIGHT CLASS</label>
                                                            <div class="select-wrapper">
                                                                <asp:DropDownList ID="ddlTransitFlightClass" runat="server" Width="100%" Height="26px"
                                                                    class="custom-select-free-form">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                FLIGHT NO</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtTransitFlight"  Text='<%# Eval("trdepflightcode") %>'  placeholder="--" runat="server"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtTransitFlight" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetFlight" TargetControlID="txtTransitFlight" 
                                                                    UseContextKey="true" OnClientItemSelected="transitflightAutocompleteSelected">
                                                                </asp:AutoCompleteExtender>
                                                                <asp:TextBox ID="txtTransitFlightCode"  Text='<%# Eval("trdepflightcode") %>' Style="display: none" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                TIME</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtTransitFlightTime"  Text='<%# Eval("trdepflighttime") %>' onkeydown="fnReadOnly(this)"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                     <div class="search-large-i" style="padding-top: 20px;float:left; ">
                                                        <!-- /AM Arrival flight/ -->
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <asp:Label ID="lblAirportMeetTo" runat="server" CssClass="page-search-content-search-asplabel"
                                                                Text="transit departure airport"></asp:Label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtAirportMeetTo"  Text='<%# Eval("trdepairportbordername") %>'  placeholder="--" runat="server"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtAirportMeetTo" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetAirportMeetFrom" TargetControlID="txtAirportMeetTo"
                                                                    UseContextKey="true" OnClientItemSelected="AMAirportMeetToAutocompleteSelected">
                                                                </asp:AutoCompleteExtender>
                                                                <asp:TextBox ID="txtAirportMeetTocode"  Text='<%# Eval("trdepairportbordercode") %>'  Style="display: none" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </div>
                                                        <!-- \\ -->
                                                    </div>

                                                    </div>

                                                    <div class="clear">
                                                    </div>
                                                                                                <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <label>
                                                                Airport Meet Services</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtAirportMeet"  Text='<%# Eval("othtypname") %>'  runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtAirportMeetCode"  Text='<%# Eval("othtypcode") %>'  runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtAirportMeet" runat="server" CompletionInterval="10"
                                                                    CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetAirportMeetList" TargetControlID="txtAirportMeet"
                                                                    OnClientItemSelected="AirportMeetListautocompleteselected">
                                                                </asp:AutoCompleteExtender>

                                                            </div>
                                                        </div>
                                                    </div>
                                             
                                                    <div class="search-large-i" style="float: left; margin-top: 20px; width: 22.5%;margin-left:0px;">
                                                        <!-- // -->
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <div class="srch-tab-left">
                                                                <label>
                                                                    adult</label>
                                                                <div class="select-wrapper" style="width: 85%;">
                                                                    <asp:DropDownList ID="ddlAMAdult" class="custom-select-free-form"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                     <asp:Label ID="lblAdult" runat="server" style="display:none;" Text='<%# Eval("adults") %>'  ></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-left">
                                                                <label>
                                                                    child</label>
                                                                <div class="select-wrapper" style="width: 85%; padding-left: 10px;">
                                                                    <asp:DropDownList ID="ddlAMChild" class="custom-select-free-form"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblChild" runat="server" style="display:none;" Text='<%# Eval("child") %>'  ></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divAMChild" runat="server" style="display:none;">
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
                                                           <asp:Label ID="lblChildAgeString" runat="server" style="display:none;" Text='<%# Eval("childagestring") %>'  ></asp:Label>
                                                            <label style="text-align: left; padding-right: 2px;">
                                                                Ages of children at AirportMeet</label>
                                                            <div class="srch-tab-child-trf-free" id="dvAMChild1" runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div37">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtAMChild1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvAMChild2"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div13">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtAMChild2" placeholder="CH 2" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvAMChild3"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div14">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtAMChild3" placeholder="CH 3" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvAMChild4"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div15">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtAMChild4" placeholder="CH 4" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvAMChild5"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div16">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtAMChild5" placeholder="CH 5" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvAMChild6"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div17">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtAMChild6" placeholder="CH 6" runat="server" onchange="validateAge(this)"
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
                                                    <div class="clear">
                                                    </div>

                                                    <div id="dvUnit" runat="server">
                                                    <div class="search-large-i" style="float: left; padding-top: 20px; padding-left: 0px;">
                                                        <!-- // -->
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                No of Unit</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtNoOfUnit" Text='<%# Eval("units") %>' autocomplete="off" onkeypress="validateDecimalOnly(event,this)"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Unit Price</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtUnitPrice" Text='<%# Eval("unitprice") %>' autocomplete="off"
                                                                    onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Unit Sale Value</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtUnitSaleValue" Text='<%# Eval("unitsalevalue") %>' autocomplete="off"
                                                                    onkeydown="fnReadOnly(this)" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="search-large-i" style="float: left; padding-top: 20px; margin-left: -10px;">
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                No of Add. Pax</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtNoOfAddionalPax" Text='<%# Eval("addlpax") %>' autocomplete="off"
                                                                    onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Add. Pax Price</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtAdditionalPaxPrice" Text='<%# Eval("addlpaxprice") %>' autocomplete="off"
                                                                    onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Add. Pax Value</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtAdditionalPaxValue" Text='<%# Eval("addlpaxsalevalue") %>' autocomplete="off"
                                                                    onkeydown="fnReadOnly(this)" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="search-large-i" style="float: left; padding-top: 20px; width: 25% !important;
                                                        padding-left: 15px; margin-left: 20px;">
                                                        <!-- // -->
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <div class="srch-tab-left">
                                                                <label>
                                                                    total sale Value</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTotalSaleValue" Text='<%# Eval("totalsalevalue") %>' autocomplete="off"
                                                                        onkeydown="fnReadOnly(this)" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                    <div class="search-large-i" style="float: left; padding-top: 20px; padding-right: 30px;
                                                        margin-left: 0px;">
                                                        <div class="srch-tab-3c">
                                                        </div>
                                                        <div class="srch-tab-3c" style="padding-left: 35%;">
                                                            <label>
                                                                Cost Price</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtCostPricePax" Text='<%# Eval("unitcprice") %>' autocomplete="off"
                                                                    onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Cost Value</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtCostPricePaxTotal" Text='<%# Eval("unitcostvalue") %>' autocomplete="off"
                                                                    onkeydown="fnReadOnly(this)" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <div class="srch-tab-left" style="width: 29%; padding-left:90px;">
                                                                <label>
                                                                    Add. Pax Cost</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtAddionalPaxCostPrice" Text='<%# Eval("addlpaxcprice") %>' autocomplete="off"
                                                                        onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                                </div>
                                                                <!-- \ \ -->
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <div class="srch-tab-right" style="width: 29%; margin-right:38px;">
                                                                <label>
                                                                    Add. Pax Cost Value</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtAddionalPaxCostValue" Text='<%# Eval("addlpaxcostvalue") %>' autocomplete="off"
                                                                        onkeydown="fnReadOnly(this)" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="search-large-i" style="float: left; padding-top: 20px; width: 25% !important;
                                                        margin-left:25px;">
                                                        <!-- // -->
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <div class="srch-tab-left">
                                                                <label>
                                                                    total Cost Value</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTotalCostValue" Text='<%# Eval("totalcostvalue") %>' autocomplete="off"
                                                                        onkeydown="fnReadOnly(this)" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                        </div>
                                                    </div>

                                                    </div>
                                                    <div id="dvACS"  runat="server">
                                                        <div class="search-large-i" style="float: left; padding-top: 20px; padding-left: 10px;">
                                                            <!-- // -->
                                                            <div class="srch-tab-3c">
                                                                <label>
                                                                    Adult Price</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtAdultPrice" Text='<%# Eval("adultprice") %>' autocomplete="off" onkeypress="validateDecimalOnly(event,this)"
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c">
                                                                <label>
                                                                    Adult Sale Value</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtAdultSaleValue" Text='<%# Eval("adultsalevalue") %>' autocomplete="off"   onkeydown="fnReadOnly(this)"
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                                <!-- \ \ -->
                                                            </div>
                                                            <div class="srch-tab-3c">
                                                                <label>
                                                                    child Price</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtChildPrice" Text='<%# Eval("childprice") %>' autocomplete="off" onkeypress="validateDecimalOnly(event,this)"
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="search-large-i" style="float: left; padding-top: 20px; margin-left: -10px;">
                                                            <div class="srch-tab-3c">
                                                                <label>
                                                                  Child Sale Value</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtChildSaleValue" Text='<%# Eval("childsalevalue") %>' autocomplete="off"   onkeydown="fnReadOnly(this)"
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c">
                                                                <label>
                                                                  Total Sale Value</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtACSTotalSaleValue" Text='<%# Eval("totalsalevalue") %>' autocomplete="off"   onkeydown="fnReadOnly(this)"
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c">
                                                             
                                                            </div>
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        <div class="search-large-i" style="float: left; padding-top: 20px; padding-left: 10px;">
                                                            <!-- // -->
                                                            <div class="srch-tab-3c">
                                                                <label>
                                                                    Adult Cost Price</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtAdultCostPrice" Text='<%# Eval("adultcprice") %>' autocomplete="off" onkeypress="validateDecimalOnly(event,this)"
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c" style="width:30%;margin-right:5%;">
                                                                <label>
                                                                    Adult Cost Value</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtAdultCostValue" Text='<%# Eval("adultcostvalue") %>' autocomplete="off"   onkeydown="fnReadOnly(this)"
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                                <!-- \ \ -->
                                                            </div>
                                                            <div class="srch-tab-3c">
                                                                <label>
                                                                    Child Cost Price</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtChildCostPrice" Text='<%# Eval("childcprice") %>' autocomplete="off"  onkeypress="validateDecimalOnly(event,this)"
                                                                       runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="search-large-i" style="float: left; padding-top: 20px; margin-left: -10px;">
                                                            <div class="srch-tab-3c">
                                                                <label>
                                                                   Child Cost Value</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtChildCostValue" Text='<%# Eval("childcostvalue") %>' autocomplete="off"   onkeydown="fnReadOnly(this)"
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c">
                                                                <label>
                                                                    Total Cost Value</label>
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtACSTotalCostValue" Text='<%# Eval("totalcostvalue") %>' autocomplete="off"   onkeydown="fnReadOnly(this)"
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-3c">
                                                          
                                                            </div>
                                                        </div>
                                                    </div>
                                                     <div class="search-large-i" style="width:10%">
                                                      <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="guest-service-btn"
                                                                onclick="btnDelete_Click" />
                                                                     </div>
                                                            <div class="clear" style="margin-top: 20px;margin-bottom: 20px;border-top:1px solid #f8f1eb;">
                                                            </div>

                                                      <div class="clear" style="padding-bottom:10px;">
                                                    </div>
                                                         <div style="float: left; padding-top: 20px; padding-bottom: 20px;">
                                                      
                                                            <div style="float: left; ">
                                                                <asp:CheckBox ID="chkComplSup" runat="server" />
                                                            </div>
                                                            <div style="padding-left:20px;float: left;padding-top:2px; ">
                                                                <label>
                                                                    Complimentary Customer</label>
                                                            </div>
                                                       
                                                    </div>

                                                         
                       
                                                         
                                                </ItemTemplate>
                                            </asp:DataList>
                                                <div class="search-large-i" style="float: left; padding-top: 20px;padding-bottom: 20px;">
                                                 <asp:Button ID="btnAddMore" runat="server" Text="Add More" CssClass="guest-service-btn"
                                                               />
                                                        </div>
                                            

                                            </ContentTemplate></asp:UpdatePanel>

                                        </div>
                              
                            <div class="clear" style="padding-top: 10px;">
                            </div>
                                     <asp:UpdatePanel ID="UpdatePanel6" runat="server"><ContentTemplate>
                      
		                <div class="search-large-i" style="padding-left:20px;">
                                                <div class="srch-tab-left">
                          <asp:Button ID="btnBook" class="srch-btn-home"  runat="server"  style="width:140px;"  OnClientClick="return ValidateSearch()"  Text="Book"></asp:Button> 
                          </div>
                           <div class="srch-tab-left">
                          <asp:Button  id="btnReset"  type="button"  runat="server" class="srch-btn-home" Text="Reset"></asp:Button>
                        </div>
                        </div>

                        <div class="clear" style="padding-top: 10px;">
                            </div>
                                     </ContentTemplate></asp:UpdatePanel>
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



        </script>
        <!-- \\ scripts \\ -->
        <center>
            <div id="Loading1" runat="server" style="height: 150px; width: 500px;display: none;">
                <img alt="" id="Image1" runat="server" src="~/img/page-loader.gif" width="200" />
                <h2 style="display: none;" class="page-loader-label">
                    Processing please wait... please wait...</h2>
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
        <asp:Button ID="btnSelectedSpclEvent" runat="server" Style="display: none;" />
        <asp:Button ID="btnConfirmHome" Width="170px" Style="display: none;" runat="server"
            Text="ConfirmHome" />
    </form>
</body>
</html>
