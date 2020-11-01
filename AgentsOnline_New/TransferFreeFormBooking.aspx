<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TransferFreeFormBooking.aspx.vb"
    Inherits="TransferFreeFormBooking" %>

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
            fnShowOrHideChild();
        });

        function fnShowOrHideChild() {
            var dataListid = '<%= dlTransferResults.ClientID %>';
            var obj = document.getElementById(dataListid);
        
            if (obj != 'undefined' || obj != null) {
                for (var i = 0; i < obj.getElementsByTagName('select').length; i++) {
                    var vId = obj.getElementsByTagName('select')[i].id;
                    
                   if (vId.indexOf("ddlTrfChild") > 0) {
                        var vValue = obj.getElementsByTagName('select')[i].value;

                        var ddlTrfChild = vId
                        var divTrfchild = vId.replace("ddlTrfChild", "divTrfchild");
                        var divTrfchild1 = vId.replace("ddlTrfChild", "dvTrfChild1");
                        var divTrfchild2 = vId.replace("ddlTrfChild", "dvTrfChild2");
                        var divTrfchild3 = vId.replace("ddlTrfChild", "dvTrfChild3");
                        var divTrfchild4 = vId.replace("ddlTrfChild", "dvTrfChild4");
                        var divTrfchild5 = vId.replace("ddlTrfChild", "dvTrfChild5");
                        var divTrfchild6 = vId.replace("ddlTrfChild", "dvTrfChild6");

                        TrfChildChanged(ddlTrfChild, divTrfchild, divTrfchild1, divTrfchild2, divTrfchild3, divTrfchild4, divTrfchild5, divTrfchild6);

                    }
                }

            }
        }


        function CheckSIC(transfercode) {

            var txttransfercode = document.getElementById(transfercode);
            var dvSICChild = document.getElementById(transfercode.replace("txtTransfersCode", "dvSICChild"));
            var txtTransfers = document.getElementById(transfercode.replace("txtTransfersCode", "txtTransfers"));
            var trf = txttransfercode.value.split('|');
            var ddlTrfChild = document.getElementById(transfercode.replace("txtTransfersCode", "ddlTrfChild"));
            if ((trf[1] == 'SIC') && (ddlTrfChild.value > 0)) {
                dvSICChild.setAttribute("style", "display:block");

            }
            else {
                dvSICChild.setAttribute("style", "display:none");
            }
          

        }
        function TrfAdultChanged(adult) {

            var txtNoOfUnit = document.getElementById(adult.replace("ddlTrfAdult", "txtNoOfUnit"));
            var ddladult = document.getElementById(adult);
            txtNoOfUnit.value = ddladult.value;
        }
        function calculatetotalvaluewithcost(txtunits, txtunitprice, txttotalamt,txtcostunitprice,txtcosttotal) {

            txtunits = document.getElementById(txtunits);
            txtunitprice = document.getElementById(txtunitprice);
            txttotalamt = document.getElementById(txttotalamt);
            txtcostunitprice = document.getElementById(txtcostunitprice);
            txtcosttotal = document.getElementById(txtcosttotal);

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


        function TransferListautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtendertxtTransfers", "txtTransfersCode");
                document.getElementById(hiddenfieldID).value = eventArgs.get_value();
                CheckSIC(hiddenfieldID);

             
            }
            else {
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtendertxtTransfers", "txtTransfersCode");
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

        function TrfTransferToAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                 
                    var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtTransferTo", "txtTransferTocode");
                    document.getElementById(hiddenfieldID).value = eventArgs.get_value();
                }
                else {
                    var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtTransferTo", "txtTransferTocode");
                    document.getElementById(hiddenfieldID).value = '';
                }
            }

        }
        function TrfTransferFromAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                   
                

                    var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtTransferFrom", "txtTransferFromcode");
                    document.getElementById(hiddenfieldID).value = eventArgs.get_value();
                 
                }
                else {
                    var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtTransferFrom", "txtTransferFromCode");
                    document.getElementById(hiddenfieldID).value = '';
                }
            }

        }

        function TransferToContextKey(ddlTransferType, extnder) {
            
            var TransferType = document.getElementById(ddlTransferType).value;
           
            $find(extnder).set_contextKey(TransferType);
            
        }


        function TransferFromContextKey(ddlTransferType, extnder) {

            var TransferType = document.getElementById(ddlTransferType).value;
           
            $find(extnder).set_contextKey(TransferType);

        }

        function flightSetContextKey(ddlTransferType, txtTrfDate, extnder) {
        
            var TransferType = document.getElementById(ddlTransferType).value;
            var TrfDate = document.getElementById(txtTrfDate).value;
            if (TrfDate == '') {
                showDialog('Alert Message', 'Please select date.', 'warning');
                HideProgess();
                return false;
            }
            $find(extnder).set_contextKey(TransferType + '|' + TrfDate);

        }

        function flightAutocompleteSelected(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtflightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalTime");
                var ddlTransferType = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "ddlTransferType");
                var txtTransferFrom = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtTransferFrom");
                var txtTransferFromCode = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtTransferFromcode");
                var txtTransferTo = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtTransferTo");
                var txtTransferToCode = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtTransferTocode");

                $get(hiddenfieldID).value = eventArgs.get_value();
                if ($get(ddlTransferType).value == 'ARRIVAL') {
                   
                    GetAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, txtTransferFrom, txtTransferFromCode);

                }
                else if ($get(ddlTransferType).value == 'DEPARTURE') {
                    GetDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, txtTransferTo, txtTransferToCode);
                }


            }

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



        function TrfChildChanged(ddlTrfChild, divTrfchild, divTrfChild1, divTrfChild2, divTrfChild3, divTrfChild4, divTrfChild5, divTrfChild6) {

            var child = document.getElementById(ddlTrfChild).value;
                    if (child == 0) {         
                        document.getElementById(divTrfchild).removeAttribute("style");
                        document.getElementById(divTrfchild).setAttribute("style", "display:none;");
                    }
                    else if (child == 1) {

                        document.getElementById(divTrfChild1).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild2).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild3).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild4).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild5).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild6).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfchild).setAttribute("style", "display:block;");
                    }
                    else if (child == 2) {
                      

                        document.getElementById(divTrfChild1).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild2).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild3).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild4).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild5).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild6).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfchild).setAttribute("style", "display:block;");

                    }
                    else if (child == 3) {
                    

                        document.getElementById(divTrfChild1).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild2).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild3).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild4).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild5).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild6).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfchild).setAttribute("style", "display:block;");

                    }
                    else if (child == 4) {
                      

                        document.getElementById(divTrfChild1).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild2).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild3).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild4).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild5).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfChild6).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfchild).setAttribute("style", "display:block;");

                    }
                    else if (child == 5) {

                    
                        document.getElementById(divTrfChild1).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild2).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild3).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild4).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild5).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild6).setAttribute("style", "float:left;display:none;");
                        document.getElementById(divTrfchild).setAttribute("style", "display:block;");

                    }
                    else if (child == 6) {

                        document.getElementById(divTrfChild1).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild2).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild3).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild4).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild5).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfChild6).setAttribute("style", "float:left;display:block;");
                        document.getElementById(divTrfchild).setAttribute("style", "display:block;");

                    }


                    var txtChildNoOfUnit = document.getElementById(ddlTrfChild.replace("ddlTrfChild", "txtChildNoOfUnit"));
                    txtChildNoOfUnit.value = child;

                        var txtTransfersCode = ddlTrfChild.replace("ddlTrfChild", "txtTransfersCode");
                        CheckSIC(txtTransfersCode);
           

                

        }

        function TransferTypeChanged(ddlTransferType, lblTransferDate, lblTransferFrom, lblTransferTo, dvFlightDetails) {
            var TransferType = document.getElementById(ddlTransferType).value;
            var txtTransferFrom = document.getElementById(ddlTransferType.replace("ddlTransferType", "txtTransferFrom"));
            var txtTransferFromCode = document.getElementById(ddlTransferType.replace("ddlTransferType", "txtTransferFromcode"));
            var txtTransferTo = document.getElementById(ddlTransferType.replace("ddlTransferType", "txtTransferTo"));
            var txtTransferToCode = document.getElementById(ddlTransferType.replace("ddlTransferType", "txtTransferTocode"));
//            var trfFrom = txtTransferFrom.value;
//            var trfFromCode = txtTransferFromCode.value;
//            var trfTo = txtTransferTo.value;
//            var trfToCode = txtTransferToCode.value;


            if (TransferType == 'ARRIVAL') {

                document.getElementById(lblTransferDate).innerHTML = 'ARRIVAL DATE';
                document.getElementById(lblTransferFrom).innerHTML = 'ARRIVAL PICKUP';
                document.getElementById(lblTransferTo).innerHTML = 'DROP OFF HOTELS / UAE LOCATIONS';
                document.getElementById(dvFlightDetails).removeAttribute("style");
                document.getElementById(dvFlightDetails).setAttribute("style", "float:left;display:block;");


                txtTransferFrom.value = '';
                txtTransferFromCode.value = '';
                txtTransferTo.value = '';
                txtTransferToCode.value = '';

            }
            else if (TransferType == 'DEPARTURE') {
                document.getElementById(lblTransferDate).innerHTML = 'DEPARTURE DATE';
                document.getElementById(lblTransferFrom).innerHTML = 'PICKUP HOTELS/UAE LOCATIONS';
                document.getElementById(lblTransferTo).innerHTML = 'AIRPORT DROP OFF';
                document.getElementById(dvFlightDetails).removeAttribute("style");
                document.getElementById(dvFlightDetails).setAttribute("style", "float:left;display:block;");
                txtTransferFrom.value = '';
                txtTransferFromCode.value = '';
                txtTransferTo.value = '';
                txtTransferToCode.value = '';

            }
            else if (TransferType == 'INTERHOTEL') {
                document.getElementById(lblTransferDate).innerHTML = 'TRANSFER DATE';
                document.getElementById(lblTransferFrom).innerHTML = 'PICKUP HOTELS/UAE LOCATIONS';
                document.getElementById(lblTransferTo).innerHTML = 'DROP OFF HOTELS / UAE LOCATIONS';

                document.getElementById(dvFlightDetails).removeAttribute("style");
                document.getElementById(dvFlightDetails).setAttribute("style", "float:left;display:none;");
                txtTransferFrom.value = '';
                txtTransferFromCode.value = '';
                txtTransferTo.value = '';
                txtTransferToCode.value = '';

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


            var date = new Date();
            var currentMonth = date.getMonth()-2;
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();
            $(".date-inpt-check-in-freeform").datepicker({
                minDate: new Date(currentYear, currentMonth, currentDate)
            });
            fnShowOrHideChild();
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
	<div class="header-a">
		<div class="wrapper-padding">	
         <div class="header-user" style="margin-top:2px;"><asp:Label ID="lblHeaderUserName" runat="server" ></asp:Label> </div>				
			<div class="header-phone" style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-account">
				<asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
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
                    <div class="page-title">
                        Transfers - <span>FREE FORM BOOKING</span></div>
                    <div class="breadcrumbs">
                        <a href="Home.aspx?Tab=0">Home</a> / <a href="#">Transfer Free Form Booking</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="page-head">
                    <div class="page-search-content-search">
                        <div id="dvTransfersContent" runat="server">
                        

                          
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
                                                   
                                            <div class="clear">
                                            </div>
                                            <div class="clear" style="padding-top:15px;">
                                            </div>
                               

                                             <asp:DataList ID="dlTransferResults" runat="server" Width="100%">
                                                <ItemTemplate>
                                                       <div class="search-large-i">
                                                <!-- // --> <asp:Label ID="lbltlineno" runat="server" style="display:none;" Text='<%# Eval("tlineno") %>'  ></asp:Label>
                                                 <asp:Label ID="lbltransferType" runat="server" style="display:none;" Text='<%# Eval("transfertype") %>'  ></asp:Label>
                                                           <div class="srch-tab-line no-margin-bottom">
                                                               <div class="srch-tab-left">
                                                                   <label>
                                                                       TRANSFER TYPE</label>
                                                                   <div class="select-wrapper">
                                                                       <asp:DropDownList ID="ddlTransferType" runat="server"  class="custom-select-free-form">
                                                                           <asp:ListItem>ARRIVAL</asp:ListItem>
                                                                           <asp:ListItem>DEPARTURE</asp:ListItem>
                                                                           <asp:ListItem Value="INTERHOTEL">INTER HOTEL</asp:ListItem>
                                                                       </asp:DropDownList>
                                                                   </div>
                                                               </div>
                                                               <div class="srch-tab-left" style="padding-left: 15px;">
                                                                   <asp:Label ID="lblTransferDate" runat="server" CssClass="page-search-content-search-asplabel"
                                                                       Text="ARRIVAL DATE"></asp:Label>
                                                                   <div class="input-a">
                                                                       <asp:TextBox ID="txtTrfDate" class="date-inpt-check-in-freeform" placeholder="dd/mm/yyyy" Text='<%# Eval("vtransferdate") %>'
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
                                                                <asp:DropDownList ID="ddlTrfFlightClass" runat="server" Width="100%" Height="26px"
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
                                                        <!-- /TRf Arrival flight/ -->
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <asp:Label ID="lblTransferFrom" runat="server" CssClass="page-search-content-search-asplabel"
                                                                Text="Arrival pickup"></asp:Label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtTransferFrom"  Text='<%# Eval("pickupname") %>'  placeholder="--" runat="server"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTransferFrom" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetTransferFrom" TargetControlID="txtTransferFrom"
                                                                    UseContextKey="true" OnClientItemSelected="TrfTransferFromAutocompleteSelected">
                                                                </asp:AutoCompleteExtender>
                                                                <asp:TextBox ID="txtTransferFromcode"  Text='<%# Eval("pickup") %>'  Style="display: none" runat="server"></asp:TextBox>
                                                            
                                                            </div>
                                                        </div>
                                                        <!-- \\ -->
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                    <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <!-- /TRf Arrival flight/ -->
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <asp:Label ID="lblTransferTo" runat="server" CssClass="page-search-content-search-asplabel"
                                                                Text="DROP OFF HOTELS/UAE LOCATIONS"></asp:Label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtTransferTo"  Text='<%# Eval("dropoffname") %>'  placeholder="--" runat="server"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTransferTo" runat="server"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetTransferTo" TargetControlID="txtTransferTo"
                                                                    UseContextKey="true" OnClientItemSelected="TrfTransferToAutocompleteSelected">
                                                                </asp:AutoCompleteExtender>
                                                                <asp:TextBox ID="txtTransferTocode"  Text='<%# Eval("dropoff") %>'  Style="display: none" runat="server"></asp:TextBox>
                                                                   <%--Added Shahul 19/07/2018--%>
                                                     
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </div>
                                                        <!-- \\ -->
                                                    </div>
                                                    <div class="search-large-i" style="float: left; margin-top: 20px; width: 22.5%;">
                                                        <!-- // -->
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <div class="srch-tab-left">
                                                                <label>
                                                                    adult</label>
                                                                <div class="select-wrapper" style="width: 85%;">
                                                                    <asp:DropDownList ID="ddlTrfAdult" class="custom-select-free-form"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                     <asp:Label ID="lblAdult" runat="server" style="display:none;" Text='<%# Eval("adults") %>'  ></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-left">
                                                                <label>
                                                                    child</label>
                                                                <div class="select-wrapper" style="width: 85%; padding-left: 10px;">
                                                                    <asp:DropDownList ID="ddlTrfChild" class="custom-select-free-form"
                                                                        runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblChild" runat="server" style="display:none;" Text='<%# Eval("child") %>'  ></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divTrfchild" runat="server" style="display:none;">
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
                                                                Ages of children at Transfer</label>
                                                            <div class="srch-tab-child-trf-free" id="dvTrfChild1" runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div37">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtTrfChild1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvTrfChild2"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div13">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtTrfChild2" placeholder="CH 2" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvTrfChild3"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div14">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtTrfChild3" placeholder="CH 3" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvTrfChild4"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div15">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtTrfChild4" placeholder="CH 4" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvTrfChild5"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div16">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtTrfChild5" placeholder="CH 5" runat="server" onchange="validateAge(this)"
                                                                                    onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="srch-tab-child-trf-free" id="dvTrfChild6"  runat="server" style="float: left;">
                                                                <div class="select-wrapper" style="width: 60px;">
                                                                    <div class="srch-tab-child-pre" id="div17">
                                                                        <div class="select-wrapper" style="width: 50px;">
                                                                            <div class="input-a">
                                                                                <asp:TextBox ID="txtTrfChild6" placeholder="CH 6" runat="server" onchange="validateAge(this)"
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
                                                    <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <label>
                                                                Vehicle Type</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtTransfers"  Text='<%# Eval("cartypecode") %>'  runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtTransfersCode"  Text='<%# Eval("cartypecode") %>'  runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtTransfers" runat="server" CompletionInterval="10"
                                                                    CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetTransferList" TargetControlID="txtTransfers"
                                                                    OnClientItemSelected="TransferListautocompleteselected">
                                                                </asp:AutoCompleteExtender>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <!-- // -->
                                                           <div class="srch-tab-3c">
                                                            <label>
                                                                No of Unit</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtNoOfUnit"  Text='<%# Eval("units") %>' autocomplete="off"      onkeypress="validateDecimalOnly(event,this)"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                 Price/pax</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtAdultPrice"  Text='<%# Eval("unitprice") %>'  autocomplete="off"     onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                     
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Total</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtTotal"   Text='<%# Eval("unitsalevalue") %>'  autocomplete="off"    onkeydown="fnReadOnly(this)" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                        <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <!-- // -->
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Cost Price/pax</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtCostPricePax" Text='<%# Eval("unitcprice") %>' autocomplete="off"    onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Total Cost Price</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtCostPricePaxTotal"  Text='<%# Eval("unitcostvalue") %>' autocomplete="off"   onkeydown="fnReadOnly(this)"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                         
                                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="guest-service-btn"
                                                                onclick="btnDelete_Click" />
                                                       
                                                        </div>
                                                    </div>
                                                                                    <div class="clear" ></div>
                                                    <div id="dvSICChild" runat="server">

                                                        <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <label>
                                                                Vehicle Type for child</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtChildTransfers"  Text='<%# Eval("cartypecode") %>'  runat="server" placeholder="--"></asp:TextBox>
                                                                <asp:TextBox ID="txtChildTransfersCode"  Text='<%# Eval("cartypecode") %>'  runat="server" Style="display: none;"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtChildTransfers" runat="server" CompletionInterval="10"
                                                                    CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                    DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                    MinimumPrefixLength="-1" ServiceMethod="GetChildTransferList" TargetControlID="txtChildTransfers"
                                                                    OnClientItemSelected="TransferListautocompleteselected">
                                                                </asp:AutoCompleteExtender>

                                                            </div>
                                                        </div>
                                                    </div>
                                                        <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <!-- // -->
                                                           <div class="srch-tab-3c">
                                                            <label>
                                                                No of Child</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtChildNoOfUnit"  Text='<%# Eval("units") %>' autocomplete="off"   onkeydown="fnReadOnly(this)"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                 Price/pax</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtChildPrice"  Text='<%# Eval("unitprice") %>'  autocomplete="off"     onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                     
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Total</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtChildTotal"   Text='<%# Eval("unitsalevalue") %>'  autocomplete="off"    onkeydown="fnReadOnly(this)" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                        <div class="search-large-i" style="float: left; padding-top: 20px;">
                                                        <!-- // -->
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                               Cost Price/pax</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtChildCostPricePax" Text='<%# Eval("unitcprice") %>' autocomplete="off"    onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                            </div>
                                                            <!-- \ \ -->
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                            <label>
                                                                Total Cost Price</label>
                                                            <div class="input-a">
                                                                <asp:TextBox ID="txtChildCostPricePaxTotal"  Text='<%# Eval("unitcostvalue") %>' autocomplete="off"   onkeydown="fnReadOnly(this)"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="srch-tab-3c">
                                                         
                                                          
                                                       
                                                        </div>
                                                    </div>
                                                      </div>

                                                                                    <div class="clear" >



                            </div>

                           
                            
                            
                          
                                                    <div style="float: left; padding-top: 20px; padding-bottom: 20px;">
                                                      
                                                            <div style="float: left; ">
                                                                <asp:CheckBox ID="chkComplSup" runat="server" />
                                                            </div>
                                                            <div style="padding-left:20px;float: left;padding-top:2px; ">
                                                                <label>
                                                                    Complimentary Customer</label>
                                                            </div>

                                                             <asp:Label ID="lblComplSup" runat="server" style="display:none;" Text='<%# Eval("complimentarycust") %>'  ></asp:Label>

                                                             <div style="margin-left:30px; float: left; ">
                                                                <asp:CheckBox ID="chkAllowZeroCost" runat="server" />
                                                            </div>
                                                            <div style="padding-left:20px;float: left;padding-top:2px; ">
                                                                <label>
                                                                    Allow zero cost</label>
                                                            </div>
                                                    </div>
                                              
                                                            
                                                            <div class="clear" style="margin-top: 20px;margin-bottom: 20px;border-top:1px solid #f8f1eb;">
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
                            <asp:Button ID="btnReset" class="srch-btn-home"  runat="server"  style="width:140px;"  Text="Reset"></asp:Button> 
                        
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
        <asp:Button ID="btnSelectedSpclEvent" runat="server" Style="display: none;" />
        <asp:Button ID="btnConfirmHome" Width="170px" Style="display: none;" runat="server"
            Text="ConfirmHome" />
    </form>
</body>
</html>
