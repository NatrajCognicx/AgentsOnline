<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HotelSearch.aspx.vb" Inherits="HotelSearch" %>
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

<%--        <link href='http://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css' />
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

   <style>
   
   </style>

   
    <%-- Added multiselect shahul--%>
      <link rel="stylesheet" type="text/css" href="/css/result-light.css">
      <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
      <link rel="stylesheet" type="text/css" href="css/bootstrap-multiselect.css">
       <link rel="stylesheet" type="text/css" href="css/Multiselect.css">
     <%-- <link rel="stylesheet" type="text/css" href="css/bootstrap-3.3.2.min.css">--%>
      <script type="text/javascript" src="js/bootstrap-3.3.2.min.js"></script>
  <style type="text/css">
    .multiselect-container>li>a>label {
  padding: 4px 20px 3px 20px;
}
  </style>
  <!-- TODO: Missing CoffeeScript 2 -->
 <%-- -- end multiselect--%>

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
  
  <%--<script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"   type="text/javascript"></script>--%>
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

            $('#ddlMealPlan').multiselect();
            $('#ddlMealPlan').change(function () {
                $('#hdmealcode').val($('#ddlMealPlan').val());
            });

            Assignmealcode();
        });

        function Assignmealcode() {

            var vv = $('#hdmealcode').val().split(',');
            $('#ddlMealPlan').multiselect('select', vv);
//            $('#ddlMealPlan').multiselect({
//                onChange: function (option, checked, select) {
//                    alert('Changed option ' + $(option).val() + '.');
//                }
//            });

//            $('#ddlMealPlan').multiselect();
//            $('#ddlMealPlan').val($('#hdmealcode').val());

//           
//            alert($('#hdmealcode').val());
        }

        function fnHotelStarsSelectAll() {
            document.getElementById("<%= hdfilterType.ClientID %>").value = 'HotelStar';
            document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '0';
            document.getElementById("<%= btnFilter.ClientID %>").click();

        }
        function fnHotelSectorSelectAll() {
            document.getElementById("<%= hdfilterType.ClientID %>").value = 'HotelSector';
            document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '0';
            document.getElementById("<%= btnFilter.ClientID %>").click();

        }
        function fnPropertyTypeSelectAll() {
            document.getElementById("<%= hdfilterType.ClientID %>").value = 'PropertyType';
            document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '0';
            document.getElementById("<%= btnFilter.ClientID %>").click();

        }
        function fnRoomClassSelectAll() {
            document.getElementById("<%= hdfilterType.ClientID %>").value = 'RoomClass';
            document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '0';
            document.getElementById("<%= btnFilter.ClientID %>").click();

        }
    </script>
    <script language="javascript" type="text/javascript">
        function CheckShiftingCheckbox(chk) {
        }

            function VATConfirm(msg) {
//                var msg = 'Please note above room rates are subject to 5% VAT as levied by the Government from 1st Jan 2018 onwards which will be notified to you in due course. If you agree please proceed to BOOK.';
//                if (confirm(msg) == true) {

//                    return true;

//                }
//                else {
//                    HideProgess(msg);
//                    return false;
//                }
                return true;
            }
            function VATAlert(msg) {
                var msg1 = 'Please note above room rates are subject to 5% VAT as levied by the Government from 1st Jan 2018 onwards which will be notified to you in due course. If you agree please proceed to BOOK.';

              //  showDialog('IMPORTANT NOTICE', msg1 + ' </br></br>' + msg, 'warning');
                //showDialog('Alert Message', msg1, 'warning');
                return true;
            }

            function VATNewAlert(msg) {
                //                showDialog('IMPORTANT NOTICE', msg, 'warning');
                              return true;

//                if (confirm(msg, 'IMPORTANT NOTICE') == true) {

//                    return true;

//                }
//                else {
//                    HideProgess(msg);
//                    return false;
             //   }

            }
            function CalculateSpecialEventSaleValue(lblNoOfPax, txtPaxRate, lblSpecialEventValue, lblpaxcurrcode, wlSpecialEventValue, dwlmarkup, wlcurrcode, lblwlPaxRate) {

                var txtPaxRate_ = document.getElementById(txtPaxRate);
                var lblSpecialEventValue_ = document.getElementById(lblSpecialEventValue);
                var lblwlPaxRate_ = document.getElementById(lblwlPaxRate);
                var wlSpecialEventValue_ = document.getElementById(wlSpecialEventValue);
                lblSpecialEventValue_.innerHTML = ((parseFloat(lblNoOfPax) * parseFloat(txtPaxRate_.value))).toString() + ' ' + lblpaxcurrcode
                wlSpecialEventValue_.innerHTML = Math.round((parseFloat(lblNoOfPax) * parseFloat(txtPaxRate_.value)) * parseFloat(dwlmarkup)).toString() + ' ' + wlcurrcode
                lblwlPaxRate_.innerHTML = Math.round(parseFloat(txtPaxRate_.value) * parseFloat(dwlmarkup)).toString()
            }

            function SpecialEventChanged(ddl, rowNumber) {

                var hd = document.getElementById("<%= hddlSpclEventRowNumber.ClientID %>");
                var hdValue = document.getElementById("<%= hdEventSelectedValue.ClientID %>");
                hd.value = rowNumber;
                document.getElementById("<%= btnSelectedSpclEvent.ClientID %>").click();
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
     



        function myMap(lati, long,hotelname) {

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
        function float_exponent(number) {
            exponent = 1;
            while (number < 1.0) {
                exponent += 1
                number *= 10
            }
            return exponent;
        }
        function format_float(number, extra_precision) {
            precision = float_exponent(number) + (extra_precision || 0)
            return number.toFixed(precision).split(/\.?0+$/)[0]
        }
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
                fSaleTotal = fSaleTotal + parseFloat(valSale);
                var valCostSale = objGridView.rows[j].cells[3].children[0].children[0].value;
                fCostSaleTotal = fCostSaleTotal + parseFloat(valCostSale);

            }
            var fCostSaleTotal_formatted = format_float(fSaleTotal, 0);
            lblSaleTotal_.innerHTML = fCostSaleTotal_formatted.toString();
        }

        function CalculateUSDAndCostPriceTotal(costPrice, convrate, lbl, lblCostTotal, gv,salecurcode) {

            Number.prototype.round = function (decimals) {
                return Number((Math.round(this + "e" + decimals) + "e-" + decimals));
            }

            var vcostPrice = document.getElementById(costPrice).value;
            var cost = parseFloat(vcostPrice) * parseFloat(convrate); //innerHTML
            var usdLabel = document.getElementById(lbl);
            var vCostTotal = document.getElementById(lblCostTotal);
            var v = cost.round(2);
            usdLabel.innerHTML = '(' + v.toString() + ' '+ salecurcode +')';// modifed by abin on 20180711
            var objGridView = document.getElementById(gv);
            var txtrowcnt = document.getElementById('<%=hdgvPricebreakupRowwCount.ClientID%>');
            var lblCostTotal_ = document.getElementById(lblCostTotal);
            var intRows = txtrowcnt.value;
            var fCostSaleTotal = 0;

            for (j = 1; j <= intRows; j++) {
                var valCostSale = objGridView.rows[j].cells[3].children[0].children[0].value;
                fCostSaleTotal = fCostSaleTotal + parseFloat(valCostSale);
            }

            var fCostSaleTotal_formatted = fCostSaleTotal.round(0);
            lblCostTotal_.innerHTML = fCostSaleTotal_formatted.toString();

        }

        function RoundPrice() {
            Number.prototype.round = function (decimals) {
                return Number((Math.round(this + "e" + decimals) + "e-" + decimals));
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
            document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '1';
            document.getElementById("<%= btnFilter.ClientID %>").click();
        }


        function ValidatePreHotelSave() {
            ShowProgess();
            if (document.getElementById('<%=txtPreHotelFromDate.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any check-in date.', 'warning');
                HideProgess();
                return false;
            }
            if (document.getElementById('<%=txtPreHotelToDate.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any check-out date.', 'warning');
                HideProgess();
                return false;
            }

            if ((document.getElementById('<%=txtPreHotelCode.ClientID%>').value == '') && (document.getElementById('<%=txtUAELocationCode.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please select any Hotel or Location.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtShiftHotelCodePreArranged.ClientID%>').value == '') {
                if (document.getElementById("<%= chkShiftingPreArranged.ClientID %>").checked == true) {
                    showDialog('Alert Message', 'Please select shifting hotel.', 'warning');
                    HideProgess(); //check here later
                    return false;
                }
                //commented / changed by mohamed on 29/08/2018
                //else {
                //    if (document.getElementById("<%= hdHotelAvailableForShifting.ClientID %>").value == "1") {
                //        if (document.getElementById("<%= hdOPModePreArranged.ClientID %>").value != 'Edit') {
                //            showDialog('Alert Message', 'Please select hotel in shifting hotel even shifting is unticked', 'warning');
                //            HideProgess(); //check here later
                //            return false;
                //        }
                //    }
                //}
            }

            var adult = $("#<%= ddlPreHotelAdult.ClientID %>").val()
      
            if (adult == '0') {
                showDialog('Alert Message', 'Please select any adult.', 'warning');
                HideProgess();
                return false;
            }

            var child = document.getElementById('<%=ddlPreHotelChild.ClientID%>').value;
      
            if (child != 0) {
              
                var child1 = document.getElementById('<%=txtPreHotelChild1.ClientID%>').value;
                var child2 = document.getElementById('<%=txtPreHotelChild2.ClientID%>').value;
                var child3 = document.getElementById('<%=txtPreHotelChild3.ClientID%>').value;
                var child4 = document.getElementById('<%=txtPreHotelChild4.ClientID%>').value;
                var child5 = document.getElementById('<%=txtPreHotelChild5.ClientID%>').value;
                var child6 = document.getElementById('<%=txtPreHotelChild6.ClientID%>').value;
                var child7 = document.getElementById('<%=txtPreHotelChild7.ClientID%>').value;
                var child8 = document.getElementById('<%=txtPreHotelChild8.ClientID%>').value;
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


        }

        //changed by mohamed on 29/08/2018
        function ShowAdultChild() { 

        }

        //changed by mohamed on 11/04/2018
        function fnLockControlsForShifting() {
            //Hotel
            //if shift is ticked
            if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                document.getElementById("<%= txtDestinationName.ClientID %>").removeAttribute("readonly");
                document.getElementById("<%= txtHotelName.ClientID %>").removeAttribute("readonly");
                document.getElementById("<%= ddlPropertType.ClientID %>").removeAttribute("readonly");
                document.getElementById("<%= txtHotelStars.ClientID %>").removeAttribute("readonly");
                document.getElementById("<%= txtCheckIn.ClientID %>").setAttribute("readonly", true);
                document.getElementById("<%= txtCheckOut.ClientID %>").removeAttribute("readonly");

            }
            else {
                if (document.getElementById("<%= hdHotelAvailableForShifting.ClientID %>").value != "1" || document.getElementById("<%= txtShiftHotelCode.ClientID %>").value=="") {
                    //if shifting is unticked, and there is no hotel booked so no need to shift 
                    //or if shifting is unticked, and no hotel is selected
                    document.getElementById("<%= txtDestinationName.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= txtHotelName.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= ddlPropertType.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= txtHotelStars.ClientID %>").removeAttribute("readonly");
                    //document.getElementById("< %= txtCheckIn.ClientID %>").removeAttribute("readonly");
                    //document.getElementById("< %= txtCheckOut.ClientID %>").removeAttribute("readonly");             
                }
                else {
                    //if shifting is unticked, and there is a hotel / pre hotel booked
                    //changed by mohamed on 29/08/2018
                    document.getElementById("<%= txtDestinationName.ClientID %>").setAttribute("readonly", true);
                    document.getElementById("<%= txtHotelName.ClientID %>").setAttribute("readonly", true);
                    document.getElementById("<%= ddlPropertType.ClientID %>").setAttribute("readonly", true);
                    document.getElementById("<%= txtHotelStars.ClientID %>").setAttribute("readonly", true);
                    document.getElementById("<%= txtCheckIn.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= txtCheckOut.ClientID %>").removeAttribute("readonly");
                }
            }

            //PreArranged Hotel
            if (document.getElementById("<%= chkShiftingPreArranged.ClientID %>").checked == true) {
                document.getElementById("<%= txtUAELocation.ClientID %>").removeAttribute("readonly");
                document.getElementById("<%= txtPreHotel.ClientID %>").removeAttribute("readonly");
                document.getElementById("<%= txtPreHotelFromDate.ClientID %>").setAttribute("readonly", true);
                document.getElementById("<%= txtPreHotelToDate.ClientID %>").removeAttribute("readonly");

            }
            else {
                if (document.getElementById("<%= hdHotelAvailableForShifting.ClientID %>").value != "1" || document.getElementById("<%= txtShiftHotelCodePreArranged.ClientID %>").value == "") {
                    //changed by mohamed on 29/08/2018
                    //if shifting is unticked, and there is no hotel booked so no need to shift
                    //or if shifting is unticked, and no hotel is selected
                    document.getElementById("<%= txtUAELocation.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= txtPreHotel.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= txtPreHotelFromDate.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= txtPreHotelToDate.ClientID %>").removeAttribute("readonly");
                }
                else {
                    //if shifting is unticked, and there is a hotel / pre hotel booked
                    document.getElementById("<%= txtUAELocation.ClientID %>").setAttribute("readonly", true);
                    document.getElementById("<%= txtPreHotel.ClientID %>").setAttribute("readonly", true);
                    document.getElementById("<%= txtPreHotelFromDate.ClientID %>").removeAttribute("readonly");
                    document.getElementById("<%= txtPreHotelToDate.ClientID %>").removeAttribute("readonly");
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

            if ((document.getElementById('<%=txtHotelStarscode.ClientID%>').value == '') && (document.getElementById('<%=txtHotelStars.ClientID%>').value != '')) {
                showDialog('Alert Message', 'Category code not select Please select Category again.', 'warning');
                HideProgess();
                return false;
            }

                if (document.getElementById('<%=txtShiftHotelCode.ClientID%>').value == '') {
                    if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                        showDialog('Alert Message', 'Please select shifting hotel.', 'warning');
                        HideProgess(); //check here later
                        return false;
                    }
                    //commented / changed by mohamed on 29/08/2018
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

            if (document.getElementById('<%=ddlRoom_Dynamic.ClientID%>').value == '0') {
                showDialog('Alert Message', 'Please select any number of rooms.', 'warning');
                HideProgess();
                return false;
            }


            if (document.getElementById('<%=txtCountryCode.ClientID%>').value == '0') {
                showDialog('Alert Message', 'Please select any source country.', 'warning');
                HideProgess();
                return false;
            }


            // *** Hotel child age change based on room on 25/05/2017 -- Start

            // *** Danny 26/08/2018 


            // *** ---------------------------- End

            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtCustomerCode.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please Select agent.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtCountryCode.ClientID%>').value == '0') {
                showDialog('Alert Message', 'Please select any source country.', 'warning');
                HideProgess();
                return false;
            }


            return true;

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
                $('#dvFullAdultChild').show();
            }
            else if (room == 2) {


                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').hide();
                $('#dvRoom4AdultChild').hide();
                $('#dvRoom5AdultChild').hide();
                $('#dvRoom6AdultChild').hide();
                $('#dvFullAdultChild').show();
            }
            else if (room == 3) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').hide();
                $('#dvRoom5AdultChild').hide();
                $('#dvRoom6AdultChild').hide();
                $('#dvFullAdultChild').show();
            }
            else if (room == 4) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').show();
                $('#dvRoom5AdultChild').hide();
                $('#dvRoom6AdultChild').hide();
                $('#dvFullAdultChild').show();
            }
            else if (room == 5) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').show();
                $('#dvRoom5AdultChild').show();
                $('#dvRoom6AdultChild').hide();
                $('#dvFullAdultChild').show();
            }
            else if (room == 6) {

                $('#dvRoom1AdultChild').show();
                $('#dvRoom2AdultChild').show();
                $('#dvRoom3AdultChild').show();
                $('#dvRoom4AdultChild').show();
                $('#dvRoom5AdultChild').show();
                $('#dvRoom6AdultChild').show();
                $('#dvFullAdultChild').show();
            }
        }

//        //        *** 26/082018 Danny
        function ClearRoomAdultChild() {
            $("[id*=dlNofoRooms]").remove();
            return false;

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

        function Countryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtCountryCode.ClientID%>').value = '';
            }
        }


        // added shahul 23/04/18
        function AutoCompleteExtender_txtCountry_OnClientPopulating(sender, args) {

            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtCustomerCode.ClientID%>').value != '')) {

                sender.set_contextKey(document.getElementById('<%=txtCustomerCode.ClientID%>').value);
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

        function HotelNameautocompleteselected(source, eventArgs) {
            var chkshow = document.getElementById('<%=chkshowall.ClientID%>'); //                added shahul 27/06/18
            if (eventArgs != null) {

                document.getElementById('<%=txtHotelCode.ClientID%>').value = eventArgs.get_value();

                GetHotelsDetails(document.getElementById('<%=txtHotelCode.ClientID%>').value);
                chkshow.checked = false;
            }
            else {
                document.getElementById('<%=txtHotelCode.ClientID%>').value = '';
                chkshow.removeAttribute('disabled'); //                added shahul 27/06/18
            }
            SethotelContextkey();
            chkshow.setAttribute("disabled", false); //                added shahul 27/06/18
        }

        function PreHotelNameautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {

                document.getElementById('<%=txtPreHotelCode.ClientID%>').value = eventArgs.get_value();
                GetSectorDetailsFromPartyCode(document.getElementById('<%=txtPreHotelCode.ClientID%>').value);
            }
            else {
                document.getElementById('<%=txtPreHotelCode.ClientID%>').value = '';

            }
        }

        function UAELocationAutocompleteselected(source, eventArgs) {
            if (eventArgs != null) {

                document.getElementById('<%=txtUAELocationCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtUAELocationCode.ClientID%>').value = '';

            }
        }


        function GetSectorDetailsFromPartyCode(HotelCode) {
           
            $.ajax({
                type: "POST",
                url: "HotelSearch.aspx/GetSectorDetailsFromPartyCode",
                data: '{HotelCode:  "' + HotelCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessSector,
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

        function OnSuccessSector(response) {
          
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);

            var customers = xml.find("Customers");
            var rowCount = customers.length;
    
            $.each(customers, function () {
               
                var customer = $(this);

                document.getElementById('<%=txtUAELocationCode.ClientID%>').value = $(this).find("destcode").text();
                document.getElementById('<%=txtUAELocation.ClientID%>').value = $(this).find("destname").text();

            });

        };

        function PreHotelCustomersautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {

                document.getElementById('<%=txtPreHotelCustomercode.ClientID%>').value = eventArgs.get_value();
                GetPreHotelCountryDetails(eventArgs.get_value());
            }
            else {
                document.getElementById('<%=txtPreHotelCustomercode.ClientID%>').value = '';

            }
        }
        function PreHotelCountryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {

                document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>').value = '';

            }
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
            var ddlRoom_Dynamic = document.getElementById('<%=ddlRoom_Dynamic.ClientID%>');
            ddlRoom_Dynamic.value = strroomstring.length
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
            
            ShowPreHotelChildAge();
            CallCheckOutDatePicker();
            
        }

        // alert message on some failure
        function ShiftingCallFailed(res) {

        }

        function PropertTypeChanged(val) {

            SethotelContextkey();

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

        function HotelStarsNameautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtHotelStarscode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtHotelStarscode.ClientID%>').value = '';
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

        function AutoCompleteExtender_PrehotelCountry_KeyUp() {
            $("#<%= txtPreHotelSourceCountry.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtPreHotelSourceCountry.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtPreHotelSourceCountry.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtPreHotelSourceCountry.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }


        function AutoCompleteExtender_PreHotelCustomer_KeyUp() {
            $("#<%= txtPreHotelCustomer.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtPreHotelCustomer.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtPreHotelCustomercode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtPreHotelCustomer.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtPreHotelCustomer.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtPreHotelCustomercode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }

        function AutoCompleteExtender_PreHotelNameKeyUp() {
            $("#<%= txtPreHotel.ClientID %>").bind("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtPreHotel.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtPreHotelCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }

            });
            $("#<%= txtPreHotel.ClientID %>").keyup("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtPreHotel.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtPreHotelCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }

        function AutoCompleteExtender_UAELocationeKeyUp() {
            $("#<%= txtUAELocation.ClientID %>").bind("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtUAELocation.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtUAELocationCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }

            });
            $("#<%= txtUAELocation.ClientID %>").keyup("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtUAELocation.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtUAELocationCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
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

        function AutoCompleteExtender_HotelNameKeyUp() {
            $("#<%= txtHotelName.ClientID %>").bind("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtHotelName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelCode.ClientID%>');
                var chkshow = document.getElementById('<%=chkshowall.ClientID%>'); //                added shahul 27/06/18
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    chkshow.removeAttribute('disabled'); //                added shahul 27/06/18
                }
                SethotelContextkey();
            });
            $("#<%= txtHotelName.ClientID %>").keyup("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtHotelName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelCode.ClientID%>');
                var chkshow = document.getElementById('<%=chkshowall.ClientID%>'); //                added shahul 27/06/18
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    chkshow.removeAttribute('disabled'); //                added shahul 27/06/18
                }
                SethotelContextkey();
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

        function AutoCompleteExtender_HotelStarsKeyUp() {
            $("#<%= txtHotelStars.ClientID %>").bind("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtHotelStars.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelStarscode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';

                }

                SethotelContextkey();
            });

            $("#<%= txtHotelStars.ClientID %>").keyup(function (event) {

                var hiddenfieldID1 = document.getElementById('<%=txtHotelStars.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelStarscode.ClientID%>');

                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
                SethotelContextkey();
            });
        }

        function SethotelContextkey() {
            var dc = document.getElementById('<%=txtDestinationcode.ClientID%>').value;
            var hsc = document.getElementById('<%=txtHotelStarscode.ClientID%>').value;
            var pt = document.getElementById('<%=ddlPropertType.ClientID%>').value;
            var contxt = '';
            if (dc != '') {
                if (contxt != '') {
                    contxt = contxt + '||' + 'DC:' + dc;
                }
                else {
                    contxt = 'DC:' + dc;
                }

            }
            if (hsc != '') {
                if (contxt != '') {
                    contxt = contxt + '||' + 'HSC:' + hsc;
                }
                else {
                    contxt = 'HSC:' + hsc;
                }

            }
            if (pt != '') {
                if (contxt != '') {
                    contxt = contxt + '||' + 'PT:' + pt;
                }
                else {
                    contxt = 'PT:' + pt;
                }

            }

            $find('AutoCompleteExtender_txtHotelName').set_contextKey(contxt);
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
             
                document.getElementById('<%=txtDestinationcode.ClientID%>').value = $(this).find("destcode").text() ;
                document.getElementById('<%=txtDestinationName.ClientID%>').value = $(this).find("destname").text();
                document.getElementById('<%=txtHotelStarscode.ClientID%>').value = $(this).find("catcode").text();
                document.getElementById('<%=txtHotelStars.ClientID%>').value = $(this).find("catname").text();


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




        function GetPreHotelCountryDetails(CustCode) {
            $.ajax({
                type: "POST",
                url: "HotelSearch.aspx/GetCountryDetails",
                data: '{CustCode:  "' + CustCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnPreHotelSuccess,
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

        function OnPreHotelSuccess(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var Countries = xml.find("Countries");
            var rowCount = Countries.length;

            if (rowCount == 1) {
                $.each(Countries, function () {
                    document.getElementById('<%=txtPreHotelSourceCountry.ClientID%>').value = ''
                    document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>').value = '';
                    document.getElementById('<%=txtPreHotelSourceCountry.ClientID%>').value = $(this).find("ctryname").text();
                    document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>').value = $(this).find("ctrycode").text();
                    document.getElementById('<%=txtPreHotelSourceCountry.ClientID%>').setAttribute("readonly", true);
                    $find('AutoCompleteExtender_txtPreHotelSourceCountry').setAttribute("Enabled", false);
                });
            }
            else {
                document.getElementById('<%=txtPreHotelSourceCountry.ClientID%>').value = ''
                document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelSourceCountry.ClientID%>').removeAttribute("readonly");
                $find('AutoCompleteExtender_txtPreHotelSourceCountry').setAttribute("Enabled", true);
            }
        };

    </script>


    
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            CallPriceSlider()
            AutoCompleteExtender_HotelNameKeyUp();
            AutoCompleteExtenderKeyUp();
            AutoCompleteExtender_HotelStarsKeyUp();
            AutoCompleteExtender_Customer_KeyUp();
            AutoCompleteExtender_Country_KeyUp();
            //AutoCompleteExtender_ShiftHotel_KeyUp();
            AutoCompleteExtender_UAELocationeKeyUp();
            AutoCompleteExtender_PreHotelNameKeyUp();
            AutoCompleteExtender_PreHotelCustomer_KeyUp();
            AutoCompleteExtender_PrehotelCountry_KeyUp();
            


            $('.srch-lbl').closest('.search-tab-content').find('.search-asvanced').fadeIn();
            $('.srch-lbl').text('Close Search Options').addClass('open');


            var slider_range = $("#slider-range");
            slider_range.on("click", function () {
                document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '1';
                document.getElementById("<%= btnFilter.ClientID %>").click();
            });

            if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                document.getElementById("<%= lblshiftfrom.ClientID %>").value = "SHIFT FROM HOTEL";
                if (document.getElementById("<%= hdOPMode.ClientID %>").value != 'Edit') {
                    $('#dvShiftingSub').show();
                }
                else {
                    $('#dvShiftingSub').show();
                    //document.getElementById('< %=txtShiftHotel.ClientID%>').setAttribute("readonly", true); //changed by mohamed on 11/04/2018
                  //  document.getElementById('<%=btnSelectShiftHotel.ClientID%>').setAttribute("disabled", true);
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

            //Pre Arranged
            if (document.getElementById("<%= chkShiftingPreArranged.ClientID %>").checked == true) {
                document.getElementById("<%= lblshiftfromPreArranged.ClientID %>").value = "SHIFT FROM HOTEL";
                if (document.getElementById("<%= hdOPModePreArranged.ClientID %>").value != 'Edit') {
                    $('#dvShiftingSubPreArranged').show();
                }
                else {
                    $('#dvShiftingSubPreArranged').show();

                    //document.getElementById('< %=txtShiftHotelPreArranged.ClientID%>').setAttribute("readonly", true); //changed by mohamed on 11/04/2018
                    document.getElementById('<%=btnSelectShiftHotelPreArranged.ClientID%>').setAttribute("disabled", true);
                    //document.getElementById('< %=chkShiftingPreArranged.ClientID%>').setAttribute("readonly", true);
                    //$find('AutoCompleteExtendertxtShiftHotelPreArranged').setAttribute("Enabled", false); //changed by mohamed on 09/04/2018
                }
            }
            else {
                document.getElementById("<%= lblshiftfromPreArranged.ClientID %>").value = "SELECT SIMILAR HOTEL";
                //changed by mohamed on 08/04/2018
                //$('#dvShiftingSubPreArranged').hide();
                if (document.getElementById("<%= hdOPModePreArranged.ClientID %>").value != 'Edit') {
                    $('#dvShiftingSubPreArranged').show();
                }
                else {
                    $('#dvShiftingSubPreArranged').hide();
                    //document.getElementById('< %=txtShiftHotelPreArranged.ClientID%>').setAttribute("readonly", true); //changed by mohamed on 11/04/2018
                    document.getElementById('<%=btnSelectShiftHotelPreArranged.ClientID%>').setAttribute("disabled", true);
                    //document.getElementById('< %=chkShiftingPreArranged.ClientID%>').setAttribute("readonly", true);
                }
            }

            //changed by mohamed on 11/04/2018
            document.getElementById('<%=txtShiftHotel.ClientID%>').setAttribute("readonly", true);
            document.getElementById('<%=txtShiftHotelPreArranged.ClientID%>').setAttribute("readonly", true);

            $('#txtDestinationName').bind("mousedown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
            });
            $('#txtDestinationName').bind("keydown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
            });
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

            // $('#txtCheckIn').bind("change", function () {
            //            $('#txtCheckIn').focus(function () {
            //                if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
            //                    $(".date-inpt-check-in").datepicker("destroy");
            //                    event.preventDefault();
            //              
            //                }
            //            });
            $('#txtDestinationName').bind("keydown", function () {
                if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                    $(".date-inpt-check-in").datepicker("destroy");
                    //event.preventDefault();
                }
            });
            $('#txtCheckOut').focus(function () {
                if (document.getElementById("<%= chkShifting.ClientID %>").checked == true) {
                    $(".date-inpt-check-in").datepicker("destroy");
                    event.preventDefault();

                }
            });
            $('#txtPreHotelToDate').focus(function () {
                if (document.getElementById("<%= chkShiftingPreArranged.ClientID %>").checked == true) {
                    $(".date-inpt-prehotel-check-in").datepicker("destroy");
                    event.preventDefault();

                }
            });


            $('#txtCustomer').bind("mousedown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
            });

            $('#txtCustomer').bind("keydown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
            });

            $('#txtCountry').bind("mousedown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
            });

            $('#txtCountry').bind("keydown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
            });
            $('#txtHotelStars').bind("mousedown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
            });

            $('#txtHotelStars').bind("keydown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
            });
            $('#txtHotelName').bind("mousedown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
            });

            //            $('#ddlAvailability').bind("keydown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //            });

            $('#ddlPropertType').bind("keydown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
                else { //changed by mohamed on 11/04/2018
                    if (document.getElementById("<%= chkShifting.ClientID %>").checked == false &&
                        document.getElementById("<%= hdHotelAvailableForShifting.ClientID %>").value == "1") {
                        event.preventDefault();
                    }
                }
            });
            //            $('#ddlAvailability').bind("mousedown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
            //                }
            //            });

            $('#ddlPropertType').bind("mousedown", function () {
                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
                    event.preventDefault();
                }
                else { //changed by mohamed on 11/04/2018
                    if (document.getElementById("<%= chkShifting.ClientID %>").checked == false &&
                        document.getElementById("<%= hdHotelAvailableForShifting.ClientID %>").value == "1") {
                        event.preventDefault();
                    }
                }
            });
            //            $('#chkOveridePrice').bind("mousedown", function () {
            //                if (document.getElementById("<%= hdOPMode.ClientID %>").value == 'Edit') {
            //                    event.preventDefault();
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

            //changed by mohamed on 11/04/2018
            $("#<%= chkShiftingPreArranged.ClientID %>").bind("change", function () {
                document.getElementById('<%=txtShiftHotelPreArranged.ClientID%>').value = "";
                document.getElementById('<%=txtShiftHotelCodePreArranged.ClientID%>').value = "";
                fnLockControlsForShifting();  //changed by mohamed on 11/04/2018
            });

            //changed by mohamed on 11/04/2018
            $("#<%= txtCheckIn.ClientID %>").bind("mousedown", function () {
                if (document.getElementById('<%=txtCheckIn.ClientID%>').getAttribute("readonly") == 'true') {
                    event.preventDefault();
                }
            });

            //changed by mohamed on 11/04/2018
            $("#<%= txtCheckOut.ClientID %>").bind("mousedown", function () {
                if (document.getElementById('<%=txtCheckOut.ClientID%>').getAttribute("readonly") == 'true') {
                    event.preventDefault();
                }
            });

            $("#<%= txtCheckIn.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=txtCheckIn.ClientID%>').value;
                var dp = d.split("/");
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

            $("#<%= txtPreHotelFromDate.ClientID %>").bind("change", function () {
                var dPreHotel = document.getElementById('<%=txtPreHotelFromDate.ClientID%>').value;
                var dp = dPreHotel.split("/");
                  var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-prehotel-check-out").datepicker("destroy");
                $(".date-inpt-prehotel-check-out").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });

            });

            //changed by mohamed on 11/04/2018
            $("#<%= txtPreHotelFromDate.ClientID %>").bind("mousedown", function () {
                if (document.getElementById('<%=txtPreHotelFromDate.ClientID%>').getAttribute("readonly") == 'true') {
                    event.preventDefault();
                }
            });

            //changed by mohamed on 11/04/2018
            $("#<%= txtPreHotelToDate.ClientID %>").bind("mousedown", function () {
                if (document.getElementById('<%=txtPreHotelToDate.ClientID%>').getAttribute("readonly") == 'true') {
                    event.preventDefault();
                }
            });

            $("#<%= ddlPreHotelchild.ClientID %>").bind("change", function () {
                ShowPreHotelChildAge();
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


            $("#btnPreHotelReset").button().click(function () {

                document.getElementById('<%=txtPreHotelFromDate.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelToDate.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotel.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelCode.ClientID%>').value = '';
                document.getElementById('<%=txtUAELocationCode.ClientID%>').value = '';
                document.getElementById('<%=txtUAELocation.ClientID%>').value = '';


                var ddltourAdult = document.getElementById('<%=ddlPreHotelAdult.ClientID%>');
                ddltourAdult.selectedIndex = "0";
                $('.custom-select-ddlAdult').next('span').children('.customSelectInner').text(jQuery("#ddlAdult :selected").text());

                var ddlPreHotelChild = document.getElementById('<%=ddlPreHotelChild.ClientID%>');
                ddlPreHotelChild.selectedIndex = "0";
                $('.custom-select-ddlChildren').next('span').children('.customSelectInner').text(jQuery("#ddlChildren :selected").text());



                document.getElementById('<%=txtPreHotelChild1.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelChild2.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelChild3.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelChild4.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelChild5.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelChild6.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelChild7.ClientID%>').value = '';
                document.getElementById('<%=txtPreHotelChild8.ClientID%>').value = '';


                //   $('#divPreHotelChild').hide();

            });




            $("#btnReset").button().click(function () {


           
                document.getElementById('<%=txtDestinationName.ClientID%>').value = '';
                document.getElementById('<%=txtDestinationCode.ClientID%>').value = '';
                document.getElementById('<%=txtHotelName.ClientID%>').value = '';
                document.getElementById('<%=txtHotelCode.ClientID%>').value = '';
                if (document.getElementById("<%= chkShifting.ClientID %>").checked != true) {
                    document.getElementById('<%=txtCheckIn.ClientID%>').value = '';
                    var dropDown = document.getElementById('<%=ddlRoom_Dynamic.ClientID%>');
                    dropDown.selectedIndex = "0";
                    $('.custom-select-ddlroom').next('span').children('.customSelectInner').text(jQuery("#ddlRoom_Dynamic :selected").text());
                }
                document.getElementById('<%=txtNoOfNights.ClientID%>').value = '';
                document.getElementById('<%=txtCheckOut.ClientID%>').value = '';
                // alert('test');


                var hdmealcode = document.getElementById('<%=hdmealcode.ClientID%>');
                hdmealcode.value = '';



                var vv = $('#ddlMealPlan').val();

                $('#ddlMealPlan').multiselect('deselect', vv);
                //$('#ddlMealPlan').multiselect('refresh');

                var chkshow = document.getElementById('<%=chkshowall.ClientID%>'); //
                chkshow.removeAttribute('disabled'); // Added shahul 18/07/2018


                document.getElementById('<%=txtHotelStars.ClientID%>').value = ''
                document.getElementById('<%=txtHotelStarsCode.ClientID%>').value = '';


                var ddlAvailability = document.getElementById('<%=ddlAvailability.ClientID%>');
                ddlAvailability.selectedIndex = "1";
                $('.custom-select-ddlAvailability').next('span').children('.customSelectInner').text(jQuery("#ddlAvailability :selected").text());

                var ddlPropertType = document.getElementById('<%=ddlPropertType.ClientID%>');
                ddlPropertType.selectedIndex = "0";
                $('.custom-select-ddlPropertType').next('span').children('.customSelectInner').text(jQuery("#ddlPropertType :selected").text());


                var ddlOrderBy = document.getElementById('<%=ddlOrderBy.ClientID%>');
                ddlOrderBy.selectedIndex = "0";
                $('.custom-select-ddlOrderBy').next('span').children('.customSelectInner').text(jQuery("#ddlOrderBy :selected").text());

                //                if (document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') {
                //                    document.getElementById('<%=txtCustomer.ClientID%>').value = ''
                //                    document.getElementById('<%=txtCustomerCode.ClientID%>').value = '';
                //                    document.getElementById('<%=txtCountry.ClientID%>').value = ''
                //                    document.getElementById('<%=txtCountryCode.ClientID%>').value = '';

                //                }



                // *** Hotel child age change based on room on 23/05/2017
                if (document.getElementById("<%= chkShifting.ClientID %>").checked != true) {
//                    *** Danny 26/08/2018
                    ClearRoomAdultChild();
                    
                }
                //***

                //                $('#dvLeftSide').hide();
                //                $('#dvSearchContent').hide();
                //                $('#dvPager').hide();
                SethotelContextkey();
                document.getElementById('<%=hdPriceMinRange.ClientID%>').value = '0'
                document.getElementById('<%=hdPriceMaxRange.ClientID%>').value = '1'


                document.getElementById("<%= btnResetForClear.ClientID %>").click();
                // 
            });

             
            // *** Hotel child age change based on room on 22/05/2017

           
//            alert('ss')
            
//             alert('ww');
            ShowPreHotelChildAge();
           
            if (document.getElementById('<%=hdHotelTab.ClientID%>').value == '1' || document.getElementById('<%=hdHotelTab.ClientID%>').value == '') {

                $("#dvPreHotels").removeClass("myhotel-tab-active");
                $("#dvPreHotels").addClass("myhotel-tab-inactive");
                $("#dvHotels").removeClass("myhotel-tab-inactive");
                $("#dvHotels").addClass("myhotel-tab-active");

                $("#dvHotelsContent").css('display', 'block');
                $("#dvPreHotelsContent").css('display', 'none');
                $("#dvSearchContent").css('display', 'block');
                document.getElementById('<%=hdHotelTab.ClientID%>').value = '1';
            }
            else {

                $("#dvHotels").removeClass("myhotel-tab-active");
                $("#dvHotels").addClass("myhotel-tab-inactive");
                $("#dvPreHotels").removeClass("myhotel-tab-inactive");
                $("#dvPreHotels").addClass("myhotel-tab-active");

                $("#dvHotelsContent").css('display', 'none');
                $("#dvPreHotelsContent").css('display', 'block');
                $("#dvSearchContent").css('display', 'none');
                document.getElementById('<%=hdHotelTab.ClientID%>').value = '0';
            }


            $("#dvHotels").button().click(function () {


                if (document.getElementById('<%=hdHotelTabFreeze.ClientID%>').value != '1') {


                    $("#dvPreHotels").removeClass("myhotel-tab-active");
                    $("#dvPreHotels").addClass("myhotel-tab-inactive");
                    $("#dvHotels").removeClass("myhotel-tab-inactive");
                    $("#dvHotels").addClass("myhotel-tab-active");

                    $("#dvHotelsContent").css('display', 'block');
                    $("#dvPreHotelsContent").css('display', 'none');
                    $("#dvSearchContent").css('display', 'block');
                    document.getElementById('<%=hdHotelTab.ClientID%>').value = '1';
                }

            });

            $("#dvPreHotels").button().click(function () {
                if (document.getElementById('<%=hdHotelTabFreeze.ClientID%>').value != '1') {
                    $("#dvHotels").removeClass("myhotel-tab-active");
                    $("#dvHotels").addClass("myhotel-tab-inactive");
                    $("#dvPreHotels").removeClass("myhotel-tab-inactive");
                    $("#dvPreHotels").addClass("myhotel-tab-active");

                    $("#dvHotelsContent").css('display', 'none');
                    $("#dvPreHotelsContent").css('display', 'block');
                    $("#dvSearchContent").css('display', 'none');
                    document.getElementById('<%=hdHotelTab.ClientID%>').value = '0';
                }
            });


        });


        //**Document read end ** 

        


        function ShowPreHotelChildAge() {

            var child = $("#<%= ddlPreHotelChild.ClientID %>").val()
            if (child == 0) {
                $('#divPreHotelChild').hide();
            }
            else if (child == 1) {

                $('#divPreHotelChild1').show(); divPreHotelChild4
                $('#divPreHotelChild2').hide();
                $('#divPreHotelChild3').hide();
                $('#divPreHotelChild4').hide();
                $('#divPreHotelChild5').hide();
                $('#divPreHotelChild6').hide();
                $('#divPreHotelChild7').hide();
                $('#divPreHotelChild8').hide();
                $('#divPreHotelChild').show();

            }
            else if (child == 2) {

                $('#divPreHotelChild1').show();
                $('#divPreHotelChild2').show();
                $('#divPreHotelChild3').hide();
                $('#divPreHotelChild4').hide();
                $('#divPreHotelChild5').hide();
                $('#divPreHotelChild6').hide();
                $('#divPreHotelChild7').hide();
                $('#divPreHotelChild8').hide();
                $('#divPreHotelChild').show();

            }
            else if (child == 3) {

                $('#divPreHotelChild1').show();
                $('#divPreHotelChild2').show();
                $('#divPreHotelChild3').show();
                $('#divPreHotelChild4').hide();
                $('#divPreHotelChild5').hide();
                $('#divPreHotelChild6').hide();
                $('#divPreHotelChild7').hide();
                $('#divPreHotelChild8').hide();
                $('#divPreHotelChild').show();

            }
            else if (child == 4) {

                $('#divPreHotelChild1').show();
                $('#divPreHotelChild2').show();
                $('#divPreHotelChild3').show();
                $('#divPreHotelChild4').show();
                $('#divPreHotelChild5').hide();
                $('#divPreHotelChild6').hide();
                $('#divPreHotelChild7').hide();
                $('#divPreHotelChild8').hide();
                $('#divPreHotelChild').show();

            }
            else if (child == 5) {

                $('#divPreHotelChild1').show();
                $('#divPreHotelChild2').show();
                $('#divPreHotelChild3').show();
                $('#divPreHotelChild4').show();
                $('#divPreHotelChild5').show();
                $('#divPreHotelChild6').hide();
                $('#divPreHotelChild7').hide();
                $('#divPreHotelChild8').hide();
                $('#divPreHotelChild').show();

            }
            else if (child == 6) {

                $('#divPreHotelChild1').show();
                $('#divPreHotelChild2').show();
                $('#divPreHotelChild3').show();
                $('#divPreHotelChild4').show();
                $('#divPreHotelChild5').show();
                $('#divPreHotelChild6').show();
                $('#divPreHotelChild7').hide();
                $('#divPreHotelChild8').hide();
                $('#divPreHotelChild').show();

            }
            else if (child == 7) {

                $('#divPreHotelChild1').show();
                $('#divPreHotelChild2').show();
                $('#divPreHotelChild3').show();
                $('#divPreHotelChild4').show();
                $('#divPreHotelChild5').show();
                $('#divPreHotelChild6').show();
                $('#divPreHotelChild7').show();
                $('#divPreHotelChild8').hide();
                $('#divPreHotelChild').show();

            }
            else if (child == 8) {

                $('#divPreHotelChild1').show();
                $('#divPreHotelChild2').show();
                $('#divPreHotelChild3').show();
                $('#divPreHotelChild4').show();
                $('#divPreHotelChild5').show();
                $('#divPreHotelChild6').show();
                $('#divPreHotelChild7').show();
                $('#divPreHotelChild8').show();
                $('#divPreHotelChild').show();

            }
        }




        function SetContextkey() {
            if (document.getElementById('<%=txtDestinationcode.ClientID%>').value != '') {


                var dc = document.getElementById('<%=txtDestinationcode.ClientID%>').value;
                var hsc = document.getElementById('<%=txtHotelStarscode.ClientID%>').value;
                var pt = document.getElementById('<%=ddlPropertType.ClientID%>').value;
                var contxt = '';
                if (dc != '') {
                    if (contxt != '') {
                        contxt = contxt + '||' + 'DC:' + dc;
                    }
                    else {
                        contxt = 'DC:' + dc;
                    }

                }
                if (hsc != '') {
                    if (contxt != '') {
                        contxt = contxt + '||' + 'HSC:' + hsc;
                    }
                    else {
                        contxt = 'HSC:' + hsc;
                    }

                }

                if (pt != '') {
                    if (contxt != '') {
                        contxt = contxt + '||' + 'PT:' + pt;
                    }
                    else {
                        contxt = 'PT:' + pt;
                    }

                }

                $find('AutoCompleteExtender_txtHotelName').set_contextKey(contxt);
            }

        }

        function CallCheckOutDatePicker() {
            var d = document.getElementById('<%=txtCheckIn.ClientID%>').value;
            if (d != '') {
               var dp = d.split("/");
                var dateOut =  new Date(dp[2] + "/" +  dp[1] + "/" +  dp[0]); 
//                var dp = d.split("/");
//                var dateOut = new Date(dp[2], dp[1], dp[0]);
                var currentMonth = dateOut.getMonth() ;
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-check-out").datepicker("destroy");
                $(".date-inpt-check-out").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });
            }

            var d = document.getElementById('<%=txtPreHotelFromDate.ClientID%>').value;
            if (d != '') {
                var dp = d.split("/");
                   var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-prehotel-check-out").datepicker("destroy");
                $(".date-inpt-prehotel-check-out").datepicker({
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


        function fnExtraBedTotal()
        {

            var NoOfExtraBed = document.getElementById('<%=txtNoOfExtraBed.ClientID%>').value;
            var ExtraBedUnitPrice = document.getElementById('<%=txtExtraBedUnitPrice.ClientID%>').value;
            var ExtraBedTotalPrice = document.getElementById('<%=txtExtraBedTotalPrice.ClientID%>');
            
            ExtraBedTotalPrice.value=NoOfExtraBed*ExtraBedUnitPrice;

        }

    </script>

    <script type="text/javascript">
    //<![CDATA[
        function pageLoad() { // this gets fired when the UpdatePanel.Update() completes
            ReBindMyStuff();
            //google.maps.event.clearListeners(map);
            //google.maps.event.clearListeners(window, 'resize');

        }

        function ReBindMyStuff() { // create the rebinding logic in here
            //            $("#slider-range").on("click", function () {
            //                document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '1';
            //                document.getElementById("<%= btnFilter.ClientID %>").click();
            //            });
        }
</script>


     <script type="text/javascript">

         //  alert('testt123');
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(EndRequestUserControl);
         function EndRequestUserControl(sender, args) {
             AutoCompleteExtender_HotelNameKeyUp();
             AutoCompleteExtenderKeyUp();
             AutoCompleteExtender_HotelStarsKeyUp();
             AutoCompleteExtender_Customer_KeyUp();
             AutoCompleteExtender_Country_KeyUp();
             AutoCompleteExtender_ShiftHotel_KeyUp(); 
             AutoCompleteExtender_UAELocationeKeyUp();
             AutoCompleteExtender_PreHotelNameKeyUp();
             AutoCompleteExtender_PreHotelCustomer_KeyUp();
             AutoCompleteExtender_PrehotelCountry_KeyUp();

             $("#slider-range").on("click", function () {
                 //   alert('add_endRequest');
                 document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '1';
                 document.getElementById("<%= btnFilter.ClientID %>").click();

             });

             SetContextkey();

             //    ShowProgess();
             var d = document.getElementById('<%=txtCheckIn.ClientID%>').value;
             if (d != '') {
                 var dp = d.split("/");
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

             //    ShowProgess();
             var d = document.getElementById('<%=txtPreHotelFromDate.ClientID%>').value;
             if (d != '') {
                 var dp = d.split("/");
                 var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                 var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;
                 var currentDate = dateOut.getDate();
                 var currentYear = dateOut.getFullYear();
                 // alert(currentMonth);
                 $(".date-inpt-prehotel-check-out").datepicker("destroy");
                 $(".date-inpt-prehotel-check-out").datepicker({
                     minDate: new Date(currentYear, currentMonth, currentDate)
                 });
             }

             ShowHotelTab();
            



         }
         function ShowHotelTab() {
             if (document.getElementById('<%=hdHotelTab.ClientID%>').value == '1' || document.getElementById('<%=hdHotelTab.ClientID%>').value == '') {

                 $("#dvPreHotels").removeClass("myhotel-tab-active");
                 $("#dvPreHotels").addClass("myhotel-tab-inactive");
                 $("#dvHotels").removeClass("myhotel-tab-inactive");
                 $("#dvHotels").addClass("myhotel-tab-active");

                 $("#dvHotelsContent").css('display', 'block');
                 $("#dvPreHotelsContent").css('display', 'none');
                 $("#dvSearchContent").css('display', 'block');
                 document.getElementById('<%=hdHotelTab.ClientID%>').value = '1';
             }
             else {

                 $("#dvHotels").removeClass("myhotel-tab-active");
                 $("#dvHotels").addClass("myhotel-tab-inactive");
                 $("#dvPreHotels").removeClass("myhotel-tab-inactive");
                 $("#dvPreHotels").addClass("myhotel-tab-active");

                 $("#dvHotelsContent").css('display', 'none');
                 $("#dvPreHotelsContent").css('display', 'block');
                 $("#dvSearchContent").css('display', 'none');
                 document.getElementById('<%=hdHotelTab.ClientID%>').value = '0';
             }
         }
         function IncludeScriptAfterThreadUpdate() {
             ShowHotelTab();
         }

    </script>



      <script type="text/javascript">


          Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {

              $("#slider-range").on("click", function () {
                  document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '1';
                  document.getElementById("<%= btnFilter.ClientID %>").click();
              });

          });

          function RefreshContent() {

              Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandlerForProgressBar);
              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandlerForProgressBar);

          }
          function BeginRequestHandlerForProgressBar(sender, args) {

              var elem = args.get_postBackElement();
              var elmid = elem.id;

            
              if ((elmid.indexOf('NofoRooms') <= 0) && (elmid.indexOf('_Dynamic') <= 0) && (elmid.indexOf('_lbRatePlan') <= 0) && (elmid.indexOf('_lbMinLengthStay') <= 0) && (elmid.indexOf('_lbTotalprice') <= 0) && (elmid.indexOf('_lbOffer') <= 0) && (elmid.indexOf('_lbReadMore') <= 0) && (elmid.indexOf('_lbCancelationPolicy') <= 0) && (elmid.indexOf('_lbSpecialEvents') <= 0) && (elmid.indexOf('_lbHotelContruction') <= 0) && (elmid.indexOf('_lbTariff') <= 0) && (elmid.indexOf('_lbHeaderMealPlan') <= 0) && (elmid.indexOf('_lbHeaderTotalValue') <= 0)) {
                  if (elem.id != 'Timer2') {
                      if ((document.getElementById("<%= hdProgress.ClientID %>").value != '0') || (document.getElementById("<%= hdProgress.ClientID %>").value == '0' && elem.id != 'Timer1')) {
                          
                          ShowProgess();
                      }
                  }
              }




          }
          function EndRequestHandlerForProgressBar(sender, args) {

              if (document.getElementById("<%= hdProgress.ClientID %>").value != '1') {
                  HideProgess();
              }


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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"  EnablePartialRendering="true">
    </asp:ScriptManager>
    <header id="top">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
	<div class="header-a">
		<div class="wrapper-padding">	
         <div class="header-user" style="margin-top:2px;"><asp:Label ID="lblHeaderUserName" runat="server" ></asp:Label> </div>				
			<div class="header-phone" 
            id="dvlblHeaderAgentName" runat="server" 
            style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-agentname" style="padding-left:105;margin-top:2px;">
            
				<%--<asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>--%>
                 
                        <asp:LinkButton ID="btnMyAccount" style="    padding: 0px 10px 0px 0px;"  UseSubmitBehavior="False" OnClick="btnMyAccount_Click"
                        CssClass="header-account-button" runat="server" Text="Account" causesvalidation="true"></asp:LinkButton>
                 
			</div>
              <div class="header-agentname" style="margin-top:2px;">
                <asp:Label ID="lblHeaderAgentName" style="    padding: 0px 10px 0px 0px;" runat="server" ></asp:Label> 
               </div>
            <div class="header-lang">            
                <asp:LinkButton ID="btnLogOut"   UseSubmitBehavior="False" OnClick="btnLogOut_Click"
                                    CssClass="header-account-button" runat="server" Text="Log Out" 
                                    ></asp:LinkButton>		
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
      <div class="page-title">Hotels - <span>list style</span></div>
      <div class="breadcrumbs">
        <a href="Home.aspx?Tab=0">Home</a> / <a href="#">Hotel</a> 
      </div>
      <div class="clear"></div>
    </div>
     <div class="page-head">
      <div class="page-search-content-search">
            <div style="width:400px;height:30px;">
            <div id="dvHotels" runat="server" style="border-right:1px solid #e3e3e3 !important;border-top:1px solid #e3e3e3  !important;width:80px;padding-left:20px;" class="myhotel-tab-active" > <label style="cursor:pointer;"> HOTELS </label> </div>
            <div  id="dvPreHotels"  runat="server"  style="border-right:1px solid #e3e3e3  !important;border-top:1px solid #e3e3e3  !important;border-left:1px solid #e3e3e3  !important;width:180px;padding-left:10px;"  class="myhotel-tab-inactive" ><label  style="cursor:pointer;">PRE-ARRANGED HOTELS</label> </div>
            </div>
      <div id="dvHotelsContent"  runat="server" >
                 <div class="search-tab-content">
                     <%-- <asp:UpdatePanel ID="upSearchMainTab" runat="server"><ContentTemplate>--%>
                                    <div class="page-search-p">
                                        <!-- // -->

                                        <%--<asp:UpdatePanel ID="upPanShift" runat="server">
                                        <ContentTemplate>--%>
                                        
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
                                                        <label ID="lblshiftfrom" runat="server">Shift From Hotel<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                        
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
                                                         <asp:Button Text="Select" ID="btnSelectShiftHotel" runat="server"   style="margin-top:-25px;margin-left:105%" class="btn-search-text"  />   
                                                    </div>
                                                         
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
                                      

                    <%-- Changed by Mohamed 05/04/2018 --%>
                     <asp:ModalPopupExtender ID="mpShiftHotel" runat="server" BackgroundCssClass="roomtype-modalBackground"
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
                            </asp:Panel>   <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                    <%-- Changed by Mohamed 05/04/2018 --%>

                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    Destination/Location<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
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
                                                            Check in<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                        <div class="input-a" style="z-index:0;">
                                                            <asp:TextBox ID="txtCheckIn" class="date-inpt-check-in" placeholder="dd/mm/yyyy" autocomplete="off"
                                                                runat="server"></asp:TextBox>
                                                            <span class="date-icon"></span>
                                                            <asp:HiddenField ID="hdCheckIn" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <div style="width: 100%">
                                                        <label>
                                                            Check out<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtCheckOut" class="date-inpt-check-out" placeholder="dd/mm/yyyy" autocomplete="off"
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
                                                            No of Nights<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtNoOfNights" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox>
                                                        </div>
                                                  
                                             </div>

                                              <div class="srch-tab-right">
                                              <label>
                                                        Rooms<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="select-wrapper">
                                                        <%--//        *** 26/082018 Danny--%>
                                                         <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                         <asp:DropDownList ID="ddlRoom_Dynamic" class="custom-select" runat="server" style="background:none !important;" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        </ContentTemplate>
                                                        </asp:UpdatePanel> 
                                 
                                                    </div>
                                              </div>
                                            </div>
                                        </div>
                                        <!-- \\ -->
                                        <div class="clear">
                                        </div>
                                          <asp:UpdatePanel ID="UpdatePanel5" runat="server"> <%-- *** 26/08/2018 Danny Dynamic Adult and Chhild numbers--%> 
                                            <ContentTemplate>
                                                 <asp:DataList ID="dlNofoRooms" runat="server" Width="100%" >
                                                    <ItemTemplate>
                                                   
                                                    <div id="Div53" runat="server" style="padding-top: 15px;">
                                                         <div class="search-large-i">
                                                            <div class="srch-tab-line no-margin-bottom">
                                                                <div class="srch-tab-left">
                                                                <label>Adult For Room <%# Eval("colAdultLbl") %><i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i> </label>
                                                                     <div class="select-wrapper">
                                                                        <asp:DropDownList ID="ddlDynRoomAdult" OnSelectedIndexChanged="AdultChanges"   AutoPostBack="true" class="custom-select " style="background:none !important;"
                                                                            runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div> 
                                                                 <div class="srch-tab-right">
                                                                  <label>Child For Room <%# Eval("colChildLbl") %></label> 
                                                                     <div class="select-wrapper">
                                                                        <asp:DropDownList ID="ddlDynRoomChild" OnSelectedIndexChanged="ChildAgeTxtCreate"   class="custom-select" AutoPostBack="true"  style="background:none !important;"
                                                                            runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div> 
                                                            </div> 
                                                        </div> 
                                                           <asp:DataList ID="dlChildAges"  runat="server" RepeatDirection="Horizontal" RepeatColumns="6" >
                                                    <ItemTemplate>
                                                        <div class="srch-tab-line no-margin-bottom">        
                                                            <label><%# Eval("colChildAgeLbl")%><i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                            <div class="srch-tab-left">
                                                                    <div class="input-a">
                                                                        <asp:TextBox ID="txtRoom1Child1" placeholder='<%# Eval("colCHNo")%>' runat="server" Text ='<%# Eval("colChildAge")%>' onchange="validateAge(this)"  
                                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2" ></asp:TextBox>
                                                                    </div>
                                                            </div>
                                                    </div>
                                                </ItemTemplate> 
                                                </asp:DataList> 
                                            </div> 
                                                
                                            </ItemTemplate>
                                        </asp:DataList> 
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
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
                                            <div class="search-large-i" id="dvForRO" runat="server">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom"   >
                                                    <label>
                                                        Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtCustomer" runat="server" placeholder="--"></asp:TextBox>
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
                                            <div class="search-large-i">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                       Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtCountry" runat="server" placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtCountryCode"   style="display:none;" runat="server" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCountry" runat="server" CompletionInterval="10"
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtCountry" 
                                                            UseContextKey = "true" OnClientPopulating="AutoCompleteExtender_txtCountry_OnClientPopulating" 
                                                            OnClientItemSelected="Countryautocompleteselected">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                         
                                            <div class="search-large-i">
                                                <div class="srch-tab-line no-margin-bottom">
                                                   <%-- ''** Shahul 26/06/2018--%>
                                                

                                                    <label>
                                                        Star Category</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtHotelStars" runat="server" placeholder="--"></asp:TextBox>
                                                        <asp:TextBox ID="txtHotelStarsCode" runat="server"   style="display:none;" ></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtHotelStars" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetHotelStars" TargetControlID="txtHotelStars"
                                                            OnClientItemSelected="HotelStarsNameautocompleteselected">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                      <%--''** Shahul 26/06/2018--%>
                                                       <div id="divstarshow" runat="server">
                                                     <asp:CheckBox ID="chkshowall"     runat="server"  />
                                                      <asp:Label ID="lblshowall" runat="server" CssClass="page-search-content-override-price"
                                                        Text="Include All Higher Categories" ></asp:Label>
                                                      </div>

                                                 

                                                </div>
                                            </div>
                                               <div class="search-large-i" style="display:none;">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        Order By</label>
                                                    <div class="select-wrapper">

                                                        <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="custom-select custom-select-ddlOrderBy" >
                                                        <asp:ListItem Value="0">By Rate</asp:ListItem>
                                                            <asp:ListItem>By Room</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- // advanced // -->
                                        <div class="search-asvanced">
                                            <!-- // -->
                                            <div class="search-large-i">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                     <%--''** Shahul 26/06/2018--%>
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            Availability</label>
                                                        <div class="select-wrapper">
                                                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                        <asp:DropDownList ID="ddlAvailability" runat="server" CssClass="custom-select custom-select-ddlAvailability" >
                                                        <asp:ListItem Value="1" >Available</asp:ListItem>
                                                            <asp:ListItem  Value="2" >All</asp:ListItem>
                                                        </asp:DropDownList>
                                                         </ContentTemplate></asp:UpdatePanel>   
                                                        </div>
                                               
                                                    <div class="clear">
                                                    </div>
                                                  </div> 

                                                    <%--''** Shahul 26/06/2018--%>
                                                    <div class="srch-tab-right" runat="server" id="divmeal">
                                                      <label>
                                                            MealPlan</label>
                                                        <div class="select-wrapper">

                                                        <asp:DropDownList ID="ddlMealPlan" runat="server" multiple="multiple" style="width:80px"
                                                            >
                                                        </asp:DropDownList>
                                                    </div>

                                                    </div>


                                                </div>
                                                <!-- \\ -->
                                            </div>
                                            <!-- \\ -->
                                            <!-- // -->
                                            <div class="search-large-i">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        Property type</label>
                                                    <div class="select-wrapper">
                                                  
                                                     <asp:DropDownList ID="ddlPropertType" runat="server"  onchange="PropertTypeChanged(this.value);" CssClass="custom-select custom-select-ddlPropertType" >
                                                      </asp:DropDownList>
                                                      
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                            <!-- \\ -->
                                            <!-- // -->
                                            <div class="search-large-i">
                                                <!-- // -->
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
                                                <!-- \\ -->
                                            </div>
                                          
                                            <!-- \\ -->
                                            <div class="clear">
                                            </div>
                                                          <div class="search-large-i" style="padding-top:10px;" >
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom" style="padding-top:10px;" >
                                                    <label>
                                                        <asp:Localize ID="Localize2" runat="server" Text="Price Range" />
                                                    </label>
                                                    <div class="select-wrapper">
                                                          <asp:DropDownList ID="ddlPriceRange" runat="server" 
                                                            CssClass="custom-select custom-select-ddlPropertType">
                                                               <asp:ListItem Value="0">All</asp:ListItem>
                                                             <asp:ListItem Value="0-100"><$100/Day</asp:ListItem>
                                                          <asp:ListItem Value="100-300">$100-$300/Day</asp:ListItem>
                                                          <asp:ListItem Value="300-500">$300-$500/Day</asp:ListItem>
                                                           <asp:ListItem Value="500">>$500/Day</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                            <div class="search-large-i" id="dvOverridePrice"  runat="server">
                                                   <div class="srch-tab-line no-margin-bottom" style="padding-top:10px;" >
                                                        <asp:CheckBox ID="chkOveridePrice" CssClass="side-block jq-checkbox-tour" runat="server" />
                                                   
                                                        <asp:Label ID="Label1" runat="server" CssClass="page-search-content-override-price"
                                                              Text="Override Price" ></asp:Label>
                                             </div>
                                                  </div>
                                        </div>
                                        <!-- \\ advanced \\ -->
                                    </div>
                                    <footer class="search-footer">
                                        <div class="search-large-i">
                                            <%--	<a href="#" class="srch-btn">Search</a>	--%>
                     <div class="srch-tab-left">
                          <asp:Button ID="btnSearch" class="authorize-btn" runat="server"   OnClientClick="return ValidateSearch()"  Text="Search"></asp:Button> 
                          </div>
                           <div class="srch-tab-left">
                               <%-- <asp:Button ID="btnReset" class="authorize-btn" runat="server" Text="Reset"></asp:Button>--%>
                        <input  id="btnReset"  type="button"  class="authorize-btn"  value="Reset"/>
                        </div>
                        </div>
						<span class="srch-lbl">Advanced Search options</span>
						<div class="clear"></div>
                    
					</footer>
                     <%--  </ContentTemplate></asp:UpdatePanel>--%>
                     <div class="clear"></div>
                                </div>
                                  <div class="clear"></div>
       </div>
        <div id="dvPreHotelsContent"  runat="server" >
             <div class="page-search-p">

                  	<div id="dvShiftingPreArranged" runat="server">
						<div class="search-large-i">
							<!-- // -->
							<div class="srch-tab-line no-margin-bottom">
								<div class="srch-tab-left" style="width:25%;">
								   <label>
									Shifting</label>
							 <div id="dvShiftCheckPreArranged" style="padding-left:15px;padding-top:5px;">
					   
							  <asp:CheckBox ID="chkShiftingPreArranged"  CssClass="side-block jq-checkbox-tour"  runat="server" />
					

									
							   </div>
								</div>
								<div class="srch-tab-left"   id="dvShiftingSubPreArranged" runat="server">
									   <div style="width: 280%">
										<label ID="lblshiftfromPreArranged" runat="server">Shift From Hotel</label>

										<div class="input-a">
											<asp:TextBox ID="txtShiftHotelPreArranged" runat="server"></asp:TextBox> <%--placeholder="press space to show hotel"--%>
											  <asp:TextBox ID="txtShiftHotelCodePreArranged"   runat="server" Style="display:none"></asp:TextBox>
										</div>
										 <asp:Button Text="Select" ID="btnSelectShiftHotelPreArranged" runat="server" style="margin-top:-25px;margin-left:105%" class="btn-search-text"  />
									</div>
								</div>
					   
								<!-- \\ -->
							</div>
							 </div>
							
						<div class="clear" style="padding-top:10px;"></div>
						 
					</div>


                    <%-- Changed by Mohamed 05/04/2018 --%>
                    <asp:ModalPopupExtender ID="mpShiftHotelPreArranged" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                CancelControlID="aShiftHotelClosePreArranged" EnableViewState="true" PopupControlID="pnlShiftHotelPreArranged"
                                TargetControlID="hdShiftHotelPopupPreArranged">
                            </asp:ModalPopupExtender>
                            <asp:HiddenField ID="hdShiftHotelPopupPreArranged" runat="server" />
                            <asp:Panel runat="server" ID="pnlShiftHotelPreArranged" Style="display: none;">
                                <div class="roomtype-price-breakuppopup">
                                    <div id="Div7PreArranged">
                                        <div class="roomtype-popup-title">
                                            <div style="float: left; padding-left: 10px; width: 80%;">
                                                <asp:Label ID="lblShiftHotelHeadingPreArranged" Width="100%" runat="server" Text="Shifting Hotel Selection"></asp:Label></div>
                                            <div style="float: right; padding-right: 5px;">
                                                <a id="aShiftHotelClosePreArranged" href="#" class="roomtype-popup-close"></a>
                                            </div>
                                        </div>
                                        <div class="roomtype-popup-description">
                                            <div id="dvShiftHotelSavePreArranged" runat="server">
                                                <div style="padding-top: 10px; padding-left: 0px; margin-bottom: 15px;">
                                                    <%--<div style="float: right; padding-right: 35px;">--%>
                                                    <table border="0px" >
                                                        <tr>
                                                        <td style="width:80%;vertical-align:middle">
                                                            <%-- Changed by mohamed on 29/08/2018 --%>
                                                            <label ID="lblShiftingMessageToUserPreArranged" runat="server" style="font-size:12px;color:red;">Select any row if same dates are being booked for other room types or meal plans </label>
                                                        </td>
                                                        <td style="width:15%;vertical-align:middle;">
                                                            <asp:Button ID="btnShiftHotelSavePreArranged" CssClass="roomtype-popup-buttons-save" runat="server" Text="Select" />
                                                        </td>
                                                        </tr>
                                                    </table>
                                                    <%--</div>--%>
                                                </div>
                                            </div>

                                            <div style="overflow: auto; min-height: 329px; max-height: 420px; min-width: 300px;
                                                padding-bottom: 10px; min-width: 700px;">
                                                <asp:DataList ID="dlShiftHotelBreakPreArranged" RepeatColumns="1" RepeatDirection="Horizontal"
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

                  <div class="search-large-i">
                      <!-- // -->
                      <div class="srch-tab-line no-margin-bottom">
                          <div class="srch-tab-left">
                              <label>
                                 CHECK-IN<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                              <div class="input-a">
                                  <asp:TextBox ID="txtPreHotelFromDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-prehotel-check-in" autocomplete="off"
                                      meta:resourcekey="txtPreHotelFromDateResource1"></asp:TextBox>
                                  <span class="date-icon-oth"></span>
                              </div>
                          </div>
                          <div class="srch-tab-right">
                              <label>
                                  CHECK-OUT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                              <div class="input-a">
                                  <asp:TextBox ID="txtPreHotelToDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-prehotel-check-out" autocomplete="off"
                                      meta:resourcekey="txtPreHotelToDateResource1"></asp:TextBox>
                                  <span class="date-icon-oth"></span>
                              </div>
                          </div>
                          <div class="clear">
                          </div>
                      </div>
                      <!-- \\ -->
                  </div>
                  <div class="search-large-i">
                      <!-- // -->
                      <label>
                          HOTELS<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                      <div class="input-a">
                          <asp:TextBox ID="txtPreHotel" placeholder="--" runat="server" meta:resourcekey="txtPreHotelgroupResource1"></asp:TextBox>
                          <asp:TextBox ID="txtPreHotelCode" Style="display: none;" placeholder="example: dubai"
                              runat="server" meta:resourcekey="txtPreHotelResource1"></asp:TextBox>
                          <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtPreHotel" runat="server" CompletionInterval="10"
                              CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                              CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                              DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                              MinimumPrefixLength="-1" ServiceMethod="GetPreHotelName" TargetControlID="txtPreHotel"
                              OnClientItemSelected="PreHotelNameautocompleteselected" ServicePath=""></asp:AutoCompleteExtender>
                      </div>
                     <%-- Added shahul 10/11/2018--%>
                      <asp:CheckBox ID="chkshowhotel"  runat="server"  />
                          <asp:Label ID="Label12" runat="server" CssClass="page-search-content-override-price"
                          Text="Show HotelName In Invoice" ></asp:Label>
                      <div class="clear">
                      </div>
                  </div>
                  <div class="search-large-i">
                      <label>
                          LOCATION</label> 
                      <div class="input-a">
                          <asp:TextBox ID="txtUAELocation" placeholder="--" runat="server" meta:resourcekey="txtUAELocationpResource1"></asp:TextBox>
                          <asp:TextBox ID="txtUAELocationCode" Style="display: none;" placeholder="example: dubai"
                              runat="server" meta:resourcekey="txtUAELocationCodeResource1"></asp:TextBox>
                          <asp:AutoCompleteExtender ID="AutoCompleteExtendertxtUAELocation" runat="server"
                              CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                              CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                              CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                              DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                              MinimumPrefixLength="-1" ServiceMethod="GetUAELocation" TargetControlID="txtUAELocation"
                              OnClientItemSelected="UAELocationAutocompleteselected" ServicePath="">
                          </asp:AutoCompleteExtender>
                      </div>

                  </div>
                  <div class="clear">
                  </div>
                  <div class="search-large-i" id="dvPreHotelAgent" style="margin-top: 20px;" runat="server">
                      <!-- // -->
                      <label>
                          Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                      <div class="input-a">
                          <asp:TextBox ID="txtPreHotelCustomer" runat="server" placeholder="--" meta:resourcekey="txtPreHotelCustomerResource1"></asp:TextBox>
                          <asp:TextBox ID="txtPreHotelCustomercode" runat="server" Style="display: none" meta:resourcekey="txtPreHotelCustomercodeResource1"></asp:TextBox>
                          <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtPreHotelCustomer" runat="server"
                              CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                              CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                              CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                              DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                              MinimumPrefixLength="-1" ServiceMethod="GetPreHotelCustomers" TargetControlID="txtPreHotelCustomer"
                              OnClientItemSelected="PreHotelCustomersautocompleteselected" ServicePath="">
                          </asp:AutoCompleteExtender>
                      </div>
                  </div>
                  <div class="search-large-i" style="float: left; margin-top: 20px;">
                      <label>
                          Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                      <div class="input-a">
                          <asp:TextBox ID="txtPreHotelSourceCountry" runat="server" placeholder="--" meta:resourcekey="txtPreHotelSourceCountryResource1"></asp:TextBox>
                          <asp:TextBox ID="txtPreHotelSourceCountryCode" runat="server" Style="display: none"
                              meta:resourcekey="txtPreHotelSourceCountryCodeResource1"></asp:TextBox>
                          <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtPreHotelSourceCountry" runat="server"
                              CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                              CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                              CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                              DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                              MinimumPrefixLength="-1" ServiceMethod="GetPreHotelCountry" TargetControlID="txtPreHotelSourceCountry"
                              UseContextKey="True" OnClientItemSelected="PreHotelCountryautocompleteselected" ServicePath="">
                          </asp:AutoCompleteExtender>
                      </div>
                      <!-- \\ -->
                  </div>
                  <div class="search-large-i" style="float: left; margin-top: 20px; margin-left: 35px;
                      width: 25%;">
                       <div class="srch-tab-line no-margin-bottom">
                      <div class="srch-tab-3c">
                          <label>
                              adult<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                          <div class="select-wrapper">
                              <asp:DropDownList ID="ddlPreHotelAdult" class="custom-select custom-select-ddlAdult" runat="server"
                                  meta:resourcekey="ddlPreHotelAdultResource1">
                              </asp:DropDownList>
                          </div>
                      </div>
                      <div class="srch-tab-3c">
                          <label>
                              Child</label>
                          <div class="select-wrapper">
                              <asp:DropDownList ID="ddlPreHotelChild" class="custom-select custom-select-ddlChildren"
                                  runat="server" meta:resourcekey="ddlPreHotelChildResource1">
                              </asp:DropDownList>
                          </div>
                      </div>
                      </div>
                      <div class="clear">
                      </div>
                      <!-- \\ -->
                  </div>
                   
                                      
                                        <div id="divPreHotelChild" runat="server" style="margin-top:20px; margin-left: -1px;">
                                            <div class="search-large-i-child-pre" style="margin-top: 15px;">
                                                <label style="text-align: left; padding-right: 12px;">
                                                    Ages of children</label>
                                                <div class="srch-tab-child-pre" id="divPreHotelChild1">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtPreHotelChild1" placeholder="CH 1" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child-pre" id="divPreHotelChild2">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="input-a">
                                                                    <asp:TextBox ID="txtPreHotelChild2" placeholder="CH 2" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child-pre" id="divPreHotelChild3">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtPreHotelChild3" placeholder="CH 3" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                            
                                                    
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child-pre" id="divPreHotelChild4">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="input-a">
                                                                    <asp:TextBox ID="txtPreHotelChild4" placeholder="CH 4" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                             
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="search-large-i-child-pre" style="margin-top:15px;">
                                                <label style="color: White;">
                                                    Ages of children</label>
                                                      <div class="srch-tab-child-pre" id="divPreHotelChild5">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                         <div class="input-a">
                                                                    <asp:TextBox ID="txtPreHotelChild5" placeholder="CH 5" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                              
                                                    </div>
                                                </div>
                                                    <div class="srch-tab-child-pre" id="divPreHotelChild6">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                           <div class="input-a">
                                                                    <asp:TextBox ID="txtPreHotelChild6" placeholder="CH 6" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                            
                                                    </div>
                                                </div>
                                                        <div class="srch-tab-child-pre" id="divPreHotelChild7">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                            <div class="input-a">
                                                                    <asp:TextBox ID="txtPreHotelChild7" placeholder="CH 7" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                 
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child-pre" id="divPreHotelChild8">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                       <div class="input-a">
                                                                    <asp:TextBox ID="txtPreHotelChild8" placeholder="CH 8" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                             
                                                    </div>
                                                </div>
                                        
                                            
                                              

                                            </div>
                                        </div>
                                      <div class="clear">
                      </div>
             
                                    </div>
                                    <div class="clear">
                      </div>
                          <div class="search-large-i" style="padding-top:15px;">
                     <div class="srch-tab-left">
                         <asp:Button ID="btnPreHotelSave" class="authorize-btn" runat="server" OnClientClick="return ValidatePreHotelSave()"
                             Text="Save"></asp:Button>
                     </div>
                     <div class="srch-tab-left">
                         <input id="btnPreHotelReset" type="button" class="srch-btn-home" value="Reset" />
                     </div>
                 </div>
                                                <div class="clear">
                      </div>

         </div>
      </div>
     </div>
        <asp:UpdatePanel ID="upnlTimer2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:Timer runat="server" ID="Timer2" Interval="1000" Enabled="false" OnTick="Timer2_Tick" />
                     <asp:HiddenField ID="hdProgressTimer2" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>

               <asp:UpdatePanel ID="upnlTimer1" runat="server"  UpdateMode="Conditional">
             <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
              </Triggers>
              <ContentTemplate>
                 <asp:Timer runat="server" ID="Timer1" Interval="1000" Enabled="false" OnTick="Timer1_Tick" />
    <div   id="dvSearchContent" runat="server"  class="two-colls" style="padding-top:30px;">
        <div class="two-colls-left">
            <asp:HiddenField ID="hdProgress" runat="server" />
            <asp:HiddenField ID="hdPriceMin" runat="server" />
            <asp:HiddenField ID="hdPriceMax" runat="server" />
            <asp:HiddenField ID="hdPriceMinRange" runat="server" />
            <asp:HiddenField ID="hdPriceMaxRange" runat="server" />
            <div class="srch-results-lbl fly-in" style="padding-bottom: 30px;">
                <div style="float: left; padding-bottom: 5px;">
                    <asp:Label ID="lblHotelCount" runat="server" Text=""></asp:Label>
                </div>
                <div style="float: right; margin-top: -2px;">
                    <asp:Image ID="imgHotelthreadLoading" ImageUrl="~/img/hotel_loader.gif" Width="125px"
                        Height="25px" runat="server" />
                </div>
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
            <div class="side-block fly-in">
                <div class="side-stars">
                    <div class="side-padding">
                        <div class="side-lbl">
                            Hotel Stars</div>
                            <asp:CheckBox ID="chkHotelStarsSelectAll" Text="Select All" onChange="fnHotelStarsSelectAll();"
                            style="color:#ff7200 !important;" CssClass="checkbox"  runat="server" 
                            />
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
                            Sectors
                        </div>
                         <asp:CheckBox ID="chkSectorsSelectAll" Text="Select All"  onChange="fnHotelSectorSelectAll();" style="color:#ff7200 !important;" CssClass="checkbox"  runat="server" />
                        <asp:CheckBoxList ID="chkSectors" CssClass="checkbox" runat="server" OnChange="CallFilter();"
                            CellPadding="5" CellSpacing="1" AutoPostBack="True">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
            <!-- \\ side \\ -->
            <!-- // side // -->
            <div class="side-block fly-in">
                <div class="side-stars">
                    <div class="side-padding">
                        <div class="side-lbl">
                            Property Type</div>
                              <asp:CheckBox ID="chkPropertyTypeSelectAll" Text="Select All"  onChange="fnPropertyTypeSelectAll();"  style="color:#ff7200 !important;" CssClass="checkbox"  runat="server" />
                        <asp:CheckBoxList ID="chkPropertyType" CssClass="checkbox" runat="server" OnChange="CallFilter();"
                            CellPadding="5" CellSpacing="1" AutoPostBack="True">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
            <!-- \\ side \\ -->
            <!-- // side // -->
            <div class="side-block fly-in">
                <div class="side-stars">
                    <div class="side-padding">
                        <div class="side-lbl">
                            Room Classification</div>
                             <asp:CheckBox ID="chkRoomClassificationSelectAll" Text="Select All"  onChange="fnRoomClassSelectAll();"  style="color:#ff7200 !important;" CssClass="checkbox"  runat="server" />
                        <asp:CheckBoxList ID="chkRoomClassification" CssClass="checkbox" runat="server" AutoPostBack="True"
                            OnChange="CallFilter();" CellPadding="5" CellSpacing="1">
                        </asp:CheckBoxList>
                        <%-- OnChange="CallFilterForRoomClassification();" --%>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnFilter" runat="server" Style="display: none;" Text="Filter" />
            <asp:HiddenField ID="hdFilterType" runat="server" />
            <asp:Button ID="btnFilterForRoom" runat="server" Style="display: none;" Text="Room Filter" />
             <asp:Button ID="btnResetForClear" runat="server" Style="display: none;" Text="ResetForClear" />
            <!-- \\ side \\ -->
        </div>
        <div class="two-colls-right">
           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <div class="two-colls-right-b">
                        <div class="padding">
                            <div class="catalog-head large fly-in">
                                <label>
                                    Sorting results by:</label>
                                <div class="search-select">
                                    <asp:DropDownList ID="ddlSorting" AutoPostBack="true" runat="server">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem>Price</asp:ListItem>
                                        <asp:ListItem>Name</asp:ListItem>
                                        <asp:ListItem>Rating</asp:ListItem>
                                        <asp:ListItem>Preferred</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="search-large-i" style="float:left;width:40%;">
                                    <div class="input-a">
                                        <asp:TextBox runat="server" ID="txtSearchHotel" placeholder="Search Hotel"  TabIndex="101"  AutoPostBack="true" OnTextChanged="txtSearchHotel_TextChanged"
                                            ></asp:TextBox>
                                    </div>
                                </div>
                                <div  style="float:left;padding-left:10px;">
                                        <asp:Button ID="btnHotelTextSearch" TabIndex="102" CssClass="btn-search-text" runat="server"  
                                        Text="SEARCH"></asp:Button> 
                                </div>

                                <div style="display: none;" class="search-select">
                                    <select>
                                        <option>Price</option>
                                        <option>Price</option>
                                        <option>Price</option>
                                        <option>Price</option>
                                        <option>Price</option>
                                    </select>
                                </div>
                                <div style="display: none;" class="search-select">
                                    <select>
                                        <option>Rating</option>
                                        <option>Rating</option>
                                        <option>Rating</option>
                                        <option>Rating</option>
                                        <option>Rating</option>
                                    </select>
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
                                <a href="#" class="show-list" style="display: none;"></a><a href="#" class="show-table"
                                    style="display: none;"></a><a href="#" class="show-thumbs chosen" style="display: none;">
                                    </a>
                                <div class="clear">
                                </div>
                            </div>
                            <div  id="dvhotnoshow" runat="server" style="display:none;background-color:#F2F3F4;padding-top:16px;padding-bottom:16px;padding-left:16px;text-align:center">
                                        <asp:Label ID="lblheader" runat="server" Text="Please re-try with the different search options?" CssClass="oop-s" Font-Bold="True"  >
                                        </asp:Label></div>
                            <div class="catalog-row list-rows">
                                <!-- // -->  
                                <asp:DataList ID="dlHotelsSearchResults" runat="server">
                                    <ItemTemplate>
                                        <div class="cat-list-item fly-in">
                                            <div class="cat-list-item-l" style="padding-top:15px;height:171px; padding-left:10px;">
                                                <a href="#">
                                                    <asp:Image ID="imgHotelImage" runat="server" /></a>
                                                <asp:Label ID="lblHotelImage" Visible="false" runat="server" Text='<%# Eval("HotelImage") %>'></asp:Label>
                                            </div>
                                            <div class="cat-list-item-r">
                                                <div class="cat-list-item-rb">
                                                    <div class="cat-list-item-p">
                                                        <div class="cat-list-content">
                                                            <div class="cat-list-content-a">
                                                                <div class="cat-list-content-l">
                                                                    <div class="cat-list-content-lb">
                                                                        <div class="cat-list-content-lpadding" style="margin-top:10px;">
                                                                            <div class="offer-slider-link">
                                                                            <asp:HiddenField ID="lblInt_HotelCode" Value='<%# Eval("Int_PartyCode") %>' runat="server" />
                                                                                <asp:HiddenField ID="lblHotelCode" Value='<%# Eval("PartyCode") %>' runat="server" />
                                                                                <asp:Label ID="lblHotelName" CssClass="offer-slider-link-label" Text='<%# Eval("PartyName") %>'
                                                                                    runat="server"></asp:Label>  <i class="fa fa-tag" title="Quote Option Available" style="font-size:16px;color:Gray;display:none;"></i>
                                                                                <div style="float: right;">
                                                                                    <asp:Label ID="lblPreferred" Style="display: none;" Text='<%# Eval("Preferred") %>'
                                                                                        runat="server"></asp:Label>
                                                                                  <input id="btnPreferred" runat="server" type="button" class="hotels-buttons-prefferred"
                                                                                        value="Preferred" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="clear">
                                                                            </div>
                                                                            <div class="offer-slider-location">
                                                                               <div style="float:left;padding-right:15px;">
                                                                               <asp:Label ID="lblCityName" runat="server" Text='<%# Eval("CityName") %>'></asp:Label>
                                                                                   </div>
                                                                                <div style="float:left;padding-right:15px;" id="dvPhone" runat="server">
                                                                                <asp:Label ID="lblPhone" style="margin-top:-15px; text-transform: capitalize !important;background-color: #e7e7e7; color: black;padding:3px;" runat="server" Text='<%# Eval("tel1") %>'></asp:Label>
                                                                                   <asp:Label ID="lblEmail" style="margin-top:-15px; text-transform: capitalize !important;background-color: #e7e7e7; color: black;padding:3px;" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                                                                </div>
                                                                                </div>
                                                                                   <div class="clear">
                                                                            </div>
                                                                            <p style="text-align: justify;">
                                                                                <asp:Label ID="lblHotelText" runat="server" ToolTip='<%# Eval("HotelText") %>' Text='<%# Eval("HotelText") %>'></asp:Label>
                                                                            </p>
                                                                            <asp:LinkButton ID="lbReadMore" CssClass="rate-plan-headings" OnClick="lbReadMore_Click"
                                                                                runat="server">Read More.</asp:LinkButton>
                                                                            <asp:HiddenField ID="hddlHotelsSearchResultsItemIndex" runat="server" />
                                                                            <div class="cat-icons" style="display: none;">
                                                                                <span class="cat-icon-01"></span><span class="cat-icon-02"></span><span class="cat-icon-03">
                                                                                </span><span class="cat-icon-04"></span><span class="cat-icon-05"></span><span class="cat-icon-06">
                                                                                </span>
                                                                                <div class="clear">
                                                                                </div>
                                                                               
                                                                            </div>
                                                                            <div id="divallott" runat ="server" style="padding-left: 350px;padding-top:4px;display:none">
                                                                              
                                                                             <asp:Button ID="btnalteravailablity" runat="server" class="cat-list-btn" Text="+/- 7 Days"
                                                                                  OnClick="btnalteravailablity_Click" />
                                                                             
                                                                                
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br class="clear" />
                                                                </div>
                                                            </div>
                                                            <div class="cat-list-content-r" style="">
                                                                <div class="cat-list-content-p">
                                                                    <div class="roomtype-icons" style="padding-left: 35px;">
                                                                       <asp:HiddenField ID="hdRatePlanSource" Value='<%# Eval("rateplansource") %>' runat="server" />
                                                                          
                                                                        <asp:HiddenField ID="hdMapLong" runat="server" Value='<%# Eval("longitude") %>' />
                                                                        <asp:HiddenField ID="hdMapLatt" runat="server" Value='<%# Eval("latitude") %>' />
                                                                        <asp:LinkButton ID="lbLocationMap" runat="server" OnClick="lbLocationMap_Click">
                                                                            <span id="spanLocationMap" runat="server" class="loc-map-icon" title="Location Map">
                                                                            </span>
                                                                            <%--<i  class="	fa fa-map-marker"  style="font-size: xx-large;" aria-hidden="true" id="spanLocationMap" runat="server"></i>--%>
                                                                        </asp:LinkButton>
                                                                        <asp:ModalPopupExtender ID="mpLocationMap" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                            CancelControlID="aMapClose" EnableViewState="true" PopupControlID="pnlLocationMap"
                                                                            TargetControlID="hdMapLong">
                                                                        </asp:ModalPopupExtender>
                                                                    </div>
                                                                    <asp:HiddenField ID="hdNoOfhotelStars" Value='<%# Eval("noofStars") %>' runat="server" />
                                                                    <div id="dvHotelStars" style="padding-left: 20px;" runat="server">
                                                                    </div>
                                                                    <div class="cat-list-review" style="padding-bottom: 5px; padding-left: 10px;">
                                                                    </div>
                                                                    <div class="offer-slider-r" style="padding-bottom: 0px; padding-left: 15px;">
                                                                      <asp:HiddenField ID="hddlSliderCurcode" Value='<%# Eval("Currcode") %>' runat="server" />
                                                                       <asp:Label ID="Label9" CssClass="offer-slider-r-price-by" runat="server" Text="From"></asp:Label>
                                                                        <div class="clear" style="padding-bottom:0px; padding-left:25px;">
                                                                        </div>
                                                                        <asp:Label ID="lblPrice" CssClass="offer-slider-r-label" runat="server" Text='<%# Eval("minprice").ToString() + " " +  Eval("Currcode").ToString() %>'></asp:Label>
                                                                        <div class="clear" style="padding-bottom: 10px; padding-left: 15px;">
                                                                        </div>
                                                                        <asp:Label ID="lblPriceBy" CssClass="offer-slider-r-price-by" runat="server" Text="per night"></asp:Label>
                                                                        <div class="clear" style="padding-bottom: 8px; padding-top: 5px; margin-left: -5px;">
                                                                            <asp:Label ID="lblForRoom" CssClass="offer-slider-r-price-by" runat="server" Text='<%# Eval("forrooms") %>'></asp:Label>
                                                                        </div>
                                                                        <div class="clear" style="padding-bottom: 0px; margin-left:-10px;">
                                                                            <asp:Label ID="lblIncTax" CssClass="offer-slider-r-price-by-tax" runat="server" Text="Incl of Taxes & vat"></asp:Label>
                                                                           
                                                                        </div>
                                                                           <div class="clear" style="padding-top:5px; margin-left:-10px;">
                                                                            <asp:Label ID="lblSpl" CssClass="offer-slider-r-price-by-tax" style="color:#ff7200 !important;padding-top:4px;" runat="server" Text="Excl of Special Events Price"></asp:Label>
                                                                           </div>
                                                                        <%--  <asp:Label ID="Label2" CssClass="offer-slider-r-price-by" runat="server" Text='<%# Eval("PriceBy") %>'></asp:Label>--%>
                                                                    </div>
                                                                    <asp:Button ID="btnShowMore" runat="server" class="cat-list-btn" Text="SHOW MORE"
                                                                        OnClick="btnShowMore_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="clear">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br class="clear" />
                                            
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <div>
                                                <asp:Label id="lblRoomTypeWarning" class="roomtype-warning-hide" runat="server"></asp:Label>
                                                <asp:DataList ID="dlRatePlan" Width="100%" runat="server" OnItemDataBound="dlRatePlan_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div style="display: block; background-color: #F2F3F4; height: 20px; padding-top: 15px;
                                                            padding-left: 5px; margin-top: -10px; padding-bottom: 30px;">
                                                            <div>
                                                                <asp:LinkButton ID="lbRatePlan" Text='<%# Eval("RatePlanName") %>' CssClass="rate-plan-headings"
                                                                    runat="server" OnClick="lbRatePlan_Click"></asp:LinkButton></div>
                                                            <div style="margin-bottom: 25px; padding-top: 5px; padding-bottom: 15px;">
                                                                <div style="float: left; padding-right: 15px;">
                                                                    <img alt="" width="20px" src="img/room-icon.png" />
                                                                </div>
                                                                <div style="padding-top: -10px; float: left;">
                                                                    <asp:Label ID="lblRoomSummary" CssClass="roomtype-summary" runat="server" Text='<%# Eval("RatePlanSummary") %>'></asp:Label>
                                                                    <asp:Label ID="lblSplDetails" CssClass="offer-slider-r-price-by-tax" style="color:#ff7200 !important;padding-top:4px; font-style:italic;font-size:smaller; " runat="server" Text=" * Rates exclude special event price (example:- New Year Gala Dinner, Easter Lunch, Christmas Lunch, etc.)"></asp:Label>
                                                                    </div>
                                                            </div>
                                                            <asp:HiddenField ID="hdShow" Value='<%# Eval("Show") %>' runat="server" />
                                                            <asp:HiddenField ID="hdRatePlan" Value='<%# Eval("RatePlanName") %>' runat="server" />
                                                            <asp:HiddenField ID="hdRatePlanCode" Value='<%# Eval("RatePlanId") %>' runat="server" />
                                                            <asp:HiddenField ID="hdRatePlanHotelCode" Value='<%# Eval("PartyCode") %>' runat="server" />
                                                            <asp:HiddenField ID="hdMealPlanOrder" runat="server" />
                                              

                                                            <asp:HiddenField ID="hdPriceOrder" runat="server" />
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="cat-list-item fly-in">
                                                            <asp:GridView ID="gvHotelRoomType" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                                Width="100%" GridLines="Horizontal" OnRowDataBound="gvHotelRoomType_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Room Type" HeaderStyle-Width="35%" ItemStyle-Width="35%">
                                                                        <ItemTemplate>
                                                                            <div class="roomtype-icons" style="padding-top: 5px;">
                                                                                <div style="float: left;">
                                                                                    <asp:Label ID="lblRoomType" runat="server" Text='<%# Eval("rmTypName") %>'></asp:Label></div>
                                                                                <asp:HiddenField ID="hdCancel" runat="server" />
                                                                                <span id="spanRoomInfo" runat="server" class="roomtype-icon-room-info" style="display: none;"
                                                                                    title="Room Info"></span>
                                                                                <asp:ModalPopupExtender ID="mpRoomInfo" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                                    CancelControlID="aClose" EnableViewState="true" PopupControlID="pnlRoomInfo"
                                                                                    TargetControlID="spanRoomInfo">
                                                                                </asp:ModalPopupExtender>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="35%" />
                                                                        <ItemStyle Width="35%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Meal Plan" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                        <HeaderTemplate>
                                                                            <asp:LinkButton ID="lbHeaderMealPlan" runat="server" OnClick="lbHeaderMealPlan_Click">Meal Plan</asp:LinkButton>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="padding-top: 5px;">
                                                                                <asp:Label ID="lblBoardBasis" runat="server" Text='<%# Eval("MealPlanNames") %>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" />
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-Width="35%" ItemStyle-Width="35%">
                                                                        <ItemTemplate>
                                                                            <div class="roomtype-icons">
                                                                                <asp:HiddenField ID="hdRoomTypePopup" runat="server" />
                                                                                <asp:HiddenField ID="hdRoomTypePopup1" runat="server" />
                                                                                <asp:HiddenField ID="hdRoomTypePopup2" runat="server" />
                                                                                <asp:HiddenField ID="hdRoomTypePopup3" runat="server" />
                                                                                <asp:HiddenField ID="hdRoomTypePopup4" runat="server" />
                                                                                <asp:HiddenField ID="hdRoomTypePopup5" runat="server" />
                                                                                  <asp:HiddenField ID="HiddenField6" runat="server" />
                                                                                       <asp:LinkButton runat="server" ID="lbAdditionalCharges" OnClick="lbAdditionalCharges_Click"  style="display:none">
                                                                                    <span id="spanAdditionalCharges" runat="server" class="roomtype-icon-tariff" title="Additional Charges">
                                                                                 
                                                                                    </span>
                                                                                </asp:LinkButton>
                                                                                <asp:ModalPopupExtender ID="mpAdditionalCharges" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                                    CancelControlID="aAdditionalChargesClose" EnableViewState="true" PopupControlID="pnlAdditionalCharges"
                                                                                    TargetControlID="HiddenField6">
                                                                                </asp:ModalPopupExtender>

                                                                                <asp:LinkButton runat="server" ID="lbOffer" OnClick="lbOffer_Click">
                                                                                    <span id="spanOffer" runat="server" class="roomtype-icon-offer" title="Special Offers">
                                                                                    </span>
                                                                                </asp:LinkButton>
                                                                                <asp:ModalPopupExtender ID="mpOffers" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                                    CancelControlID="aOfferClose" EnableViewState="true" PopupControlID="pnlOffers"
                                                                                    TargetControlID="hdRoomTypePopup3">
                                                                                </asp:ModalPopupExtender>
                                                                                <asp:LinkButton runat="server" ID="lbMinLengthStay" OnClick="lbMinLengthStay_Click">
                                                                                    <span id="spanMinLengthStay" runat="server" class="roomtype-icon-min-stay" title="Minimum Length of Stay">
                                                                                    </span>
                                                                                </asp:LinkButton>
                                                                                <asp:ModalPopupExtender ID="mpMinLengthStay" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                                    CancelControlID="aMinLengthStayClose" EnableViewState="true" PopupControlID="pnlMinLengthStay"
                                                                                    TargetControlID="hdRoomTypePopup">
                                                                                </asp:ModalPopupExtender>
                                                                                <asp:LinkButton runat="server" ID="lbCancelationPolicy" OnClick="lbCancelationPolicy_Click">
                                                                                    <span id="spanCancelationPolicy" runat="server" class="roomtype-icon-cancel-policy"
                                                                                        title="Cancelation Policy"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:ModalPopupExtender ID="mpCancelationPolicy" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                                    CancelControlID="aPolicyClose" EnableViewState="true" PopupControlID="pnlCancelationPolicy"
                                                                                    TargetControlID="hdRoomTypePopup1">
                                                                                </asp:ModalPopupExtender>
                                                                                <asp:LinkButton runat="server" ID="lbTariff" OnClick="lbTariff_Click">
                                                                                    <span id="spanTariff" runat="server" class="roomtype-icon-tariff" title="Tariff Notes">
                                                                                    </span>
                                                                                </asp:LinkButton>
                                                                                <asp:ModalPopupExtender ID="mpTariff" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                                    CancelControlID="aTariffClose" EnableViewState="true" PopupControlID="pnltariff"
                                                                                    TargetControlID="hdRoomTypePopup2">
                                                                                </asp:ModalPopupExtender>
                                                                                <asp:LinkButton runat="server" ID="lbHotelContruction" OnClick="lbHotelContruction_Click">
                                                                                    <asp:ModalPopupExtender ID="mpHotelContruction" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                                        CancelControlID="aHotelContructionClose" EnableViewState="true" PopupControlID="pnlHotelContruction"
                                                                                        TargetControlID="hdRoomTypePopup4">
                                                                                    </asp:ModalPopupExtender>
                                                                                    <span class="roomtype-icon-min-price" title="Hotel Construction"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton runat="server" ID="lbSpecialEvents" OnClick="lbSpecialEvents_Click">
                                                                <span class="roomtype-icon-credit" title="Special Events"></span></asp:LinkButton>
                                                                                <asp:ModalPopupExtender ID="mpSpecialEvents" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                                    CancelControlID="aSpecialEventsClose" EnableViewState="true" PopupControlID="pnlSpecialEvents"
                                                                                    TargetControlID="hdRoomTypePopup5">
                                                                                </asp:ModalPopupExtender>
                                                                                <div class="clear">
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="35%" />
                                                                        <ItemStyle Width="35%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Value" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <HeaderTemplate>
                                                                        <asp:Label ID="lblSplDetails1" style="color:#ff7200 !important;padding-top:4px;font-size:x-large;" runat="server" Text="*"></asp:Label>
                                                                            <asp:LinkButton ID="lbHeaderTotalValue" runat="server" Text='<%# Eval("forrooms") %>'
                                                                                OnClick="lbHeaderTotalValue_Click"></asp:LinkButton>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="padding-top: 5px;">
                                                                                <asp:LinkButton ID="lbTotalprice" Text='<%# Eval("salevalue").ToString() + " " +  Eval("Currcode").ToString() %>'
                                                                                    CssClass="room-type-total-price" runat="server" OnClick="lbTotalprice_Click"></asp:LinkButton>
                                                                                     <asp:LinkButton ID="lbwlTotalprice" Text='<%# Eval("totalvalue").ToString() + " " +  Eval("wlCurrcode").ToString() %>'
                                                                                    CssClass="room-type-total-price" runat="server" OnClick="lbTotalprice_Click"></asp:LinkButton>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="15%" />
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAvailable" runat="server" Visible="false" Text='<%# Eval("Available") %>'></asp:Label>
                                                                            <asp:Button ID="btnBookNow" runat="server" CssClass="roomtype-buttons-booknow" OnClick="btnBookNow_Click"
                                                                                Text="Book Now" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="15%" />
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRMRoomId" runat="server" Visible="false" Text='<%# Eval("RoomId") %>'></asp:Label>
                                                                            <asp:Label ID="lblRMPartyCode" runat="server" Visible="false" Text='<%# Eval("partycode") %>'></asp:Label>
                                                                            <asp:Label ID="lblRMRatePlanId" runat="server" Visible="false" Text='<%# Eval("rateplanid") %>'></asp:Label>
                                                                            <asp:Label ID="lblRMRatePlanName" runat="server" Visible="false" Text='<%# Eval("rateplanname") %>'></asp:Label>
                                                                            <asp:Label ID="lblRMRoomTypeCode" runat="server" Visible="false" Text='<%# Eval("rmtypcode") %>'></asp:Label>
                                                                            <asp:Label ID="lblRMMealPlanCode" runat="server" Visible="false" Text='<%# Eval("mealplans") %>'></asp:Label>
                                                                            <asp:Label ID="lblRMcatCode" runat="server" Visible="false" Text='<%# Eval("rmcatcode") %>'></asp:Label>
                                                                            <asp:Label ID="lblRMSharingOrExtraBed" runat="server" Visible="false" Text='<%# Eval("SharingOrExtraBed") %>'></asp:Label>
                                                                            <asp:Label ID="lblRMCurcode" runat="server" Visible="false" Text='<%# Eval("Currcode") %>'></asp:Label>
                                                                            <asp:Label ID="lblRMWlCurcode" runat="server" Visible="false" Text='<%# Eval("WlCurrcode") %>'></asp:Label>
                                                                            <asp:Label ID="lblSupAgentCode" Visible="false" Text='<%# Eval("supagentcode") %>'
                                                                                runat="server"></asp:Label>
                                                                            <asp:Label ID="lblRoomClassCode" Visible="false" Text='<%# Eval("roomclasscode") %>'
                                                                                runat="server"></asp:Label>
                                                                            <asp:Label ID="lblSaleValue" Visible="false" Text='<%# Eval("salevalue") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblCostValue" Visible="false" Text='<%# Eval("costvalue") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblCostCurrCode" Visible="false" Text='<%# Eval("costcurrcode") %>'
                                                                                runat="server"></asp:Label>
                                                                            <asp:Label ID="lblCompCust" Visible="false" Text='<%# Eval("comp_cust") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblCompSupp" Visible="false" Text='<%# Eval("comp_supp") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblComparrtrf" Visible="false" Text='<%# Eval("comparrtrf") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblCompdeptrf" Visible="false" Text='<%# Eval("compdeptrf") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblAdultEb" Visible="false" Text='<%# Eval("noofadulteb") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblChildEb" Visible="false" Text='<%# Eval("noofchildeb") %>' runat="server"></asp:Label>
                                                                             <asp:Label ID="lblNoOfRoom" Visible="false" Text='<%# Eval("noofrooms") %>' runat="server"></asp:Label>
                                                                              <asp:Label ID="lblCurrentSeletion" Visible="false" Text='<%# Eval("currentselection") %>' runat="server"></asp:Label>
                                                                              <asp:Label ID="lblHotelRoomString" Visible="false" Text='<%# Eval("hotelroomstring") %>' runat="server"></asp:Label>
                                                                                <asp:Label ID="lblVATExclude" Visible="false" Text='<%# Eval("VATExclude") %>' runat="server"></asp:Label>
                                                                                  <asp:Label ID="lblmealupgradefrom" Text='<%# Eval("mealupgradefrom") %>' runat="server"></asp:Label>
                                                                                    <asp:Label ID="hdExtraBedRequired" runat="server" />
                                                                                      <asp:Label ID="hdExtraBedValue" runat="server" />


                                                                                                     
                                                                             <asp:Label ID="hdInt_RoomtypeCodes" Text='<%# Eval("Int_RoomtypeCodes") %>' runat="server" />
                                                                             <asp:Label ID="hdInt_RoomtypeNames" Text='<%# Eval("Int_RoomtypeNames") %>' runat="server" />
                                                                             <asp:Label ID="hdInt_Roomtypes" Text='<%# Eval("Int_Roomtypes") %>' runat="server" />
                                                                              <asp:Label ID="hdRoomRatePlanSource" Text='<%# Eval("rateplansource") %>' runat="server" />
                                                                                <asp:Label ID="hdInt_costprice" Text='<%# Eval("Int_costprice") %>' runat="server" />
                                                                                  <asp:Label ID="hdInt_costcurrcode" Text='<%# Eval("Int_costcurrcode") %>' runat="server" />
                                                                                   <asp:Label ID="hdInt_partycode" Text='<%# Eval("Int_partycode") %>' runat="server" />
                                                                                    <asp:Label ID="hdInt_rmtypecode" Text='<%# Eval("Int_rmtypecode") %>' runat="server" />
                                                                                     <asp:Label ID="hdInt_mealcode" Text='<%# Eval("Int_mealcode") %>' runat="server" />
                                                                                     <asp:Label ID="hdOffercode" Text='<%# Eval("offercode") %>' runat="server" />
                                                                                     <asp:Label ID="hdAccomodationcode" Text='<%# Eval("accomodationcode") %>' runat="server" />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                            <%--         <div class="clear"></div>--%>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                                
                                <!-- \\ -->
                            </div>
                            <asp:Panel runat="server" ID="pnlLocationMap" Style="display: none;">
                                <div class="LocationMap-popup">
                                    <div id="Div4">
                                        <div class="roomtype-popup-title">
                                            <asp:Label ID="lblLocMaphotelName" runat="server"></asp:Label>
                                            <a id="aMapClose" href="#" class="roomtype-popup-close"></a>
                                        </div>
                                        <div class="roomtype-popup-description">
                                            <div id="map" style="width: 630px; height: 485px; background: #FBFBFB; margin-top: -10px;">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlRoomInfo" Style="display: none;">
                                <div class="roomtype-popup">
                                    <div id="roomtype-popup-bg">
                                        <div class="roomtype-popup-title">
                                            Standard Room <a id="aClose" href="#" class="roomtype-popup-close"></a>
                                        </div>
                                        <div class="roomtype-popup-description">
                                            Voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur
                                            magni dolores eos qui. Nemo enim ipsam voluptatem quia voluptas.</div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlCancelationPolicy" Style="display: none;">
                                <div class="roomtype-popup">
                                    <div id="Div1">
                                        <div class="roomtype-popup-title">
                                            Cancellation and Check in Check out Policy<a id="aPolicyClose" href="#" class="roomtype-popup-close"></a></div>
                                        <div class="roomtype-popup-description">
                                            <div style="overflow: auto; height: 250px; min-height: 129px; max-height: 389px;">
                                                <asp:Label ID="lblCancelationPolicy" runat="server"></asp:Label></div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlMinLengthStay" Style="display: none;">
                                <div class="roomtype-popup">
                                    <div id="Div2">
                                        <div class="roomtype-popup-title">
                                            Minimum Length of Stay<a id="aMinLengthStayClose" href="#" class="roomtype-popup-close"></a></div>
                                        <div class="roomtype-popup-description">
                                            <asp:Label ID="lblminlengthStay" runat="server"></asp:Label></div>
                                    </div>
                                </div>
                            </asp:Panel>
                         <asp:Panel runat="server" ID="pnltariff" Style="display: none;">
                                <div class="roomtype-popup-tariff">
                                    <div id="Div3">
                                        <div class="roomtype-popup-title">
                                            Tariff<a id="aTariffClose" href="#" class="roomtype-popup-close"></a></div>
                                        <%--<asp:TextBox ID="txtTariffContent" TextMode="MultiLine" runat="server"></asp:TextBox>--%><%----%>
                                        <div class="roomtype-popup-description-tariff" style="min-height: 150px; max-height: 250px;
                                            overflow: auto;">
                                              <p>
                                                <asp:Label ID="lblConditions" runat="server"></asp:Label>
                                            </p>
                                            <p>
                                                <asp:Label ID="lblTariffDate" runat="server"></asp:Label>
                                            </p>
                                            <p>
                                                <asp:Label ID="lblTariffContent" runat="server"></asp:Label>
                                            </p>
                                            </span></div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlHotelContruction" Style="display: none;">
                                <div class="roomtype-popup-tariff">
                                    <div id="Div15">
                                        <div class="roomtype-popup-title">
                                            Hotel Construction<a id="aHotelContructionClose" href="#" class="roomtype-popup-close"></a></div>
                                        <div class="roomtype-popup-description-tariff" style="min-height: 150px; max-height: 250px;
                                            overflow: auto;">
                                            <%--<p>
                                                <asp:Label ID="lblHotelContructionHeading" runat="server"></asp:Label>
                                            </p>
                                            <p>
                                                <asp:Label ID="lblHotelContructionDate" runat="server"></asp:Label>
                                            </p>--%>
                                            <p>
                                                <asp:Label ID="lblHotelContructionContent" runat="server"></asp:Label>
                                            </p>
                                            </span></div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlOffers" Style="display: none;">
                                <div class="roomtype-popup">
                                    <div>
                                        <div id="dvroomtype-popup-title" class="roomtype-popup-title">
                                            Special Offers and Promotions<a id="aOfferClose" href="#" class="roomtype-popup-close"></a></div>
                                        <div class="roomtype-popup-description" style="text-align: justify; min-height: 150px;min-width: 250px;
                                            max-height: 450px; overflow: auto;">
                                            <p>
                                                <asp:Label ID="lblOfferDate" runat="server"></asp:Label>
                                            </p>
                                            <p>
                                                <asp:Label ID="lblOfferContent" runat="server"></asp:Label>
                                            </p>

                                            <div id="dvTransferCompliment" runat="server">
                                                 <p>
                                                <asp:Label ID="lblTextComplimentaryAirportTransfer" Text="Complimentary Airport Transfer" runat="server"></asp:Label>
                                            </p>
                                                <asp:GridView ID="gvComplimentaryAirportTransfer" AutoGenerateColumns="false" runat="server">
                                                <Columns>
                                                	 <asp:TemplateField HeaderText="Transfer Type">
						<ItemTemplate>
						<asp:Label ID="lblTransferType" Text='<%# Eval("transfertype") %>' runat="server">
						</asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
                                              	 <asp:TemplateField HeaderText="Airport Name">
						<ItemTemplate>
						<asp:Label ID="lblTransferType" Text='<%# Eval("AirportCode") %>' runat="server">
						</asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
                                                </Columns>
                                                       <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                    <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlAdditionalCharges" Style="display: none;">
                                <div class="roomtype-popup">
                                    <div>
                                        <div id="Div9" class="roomtype-popup-title">
                                            Additional Charges<a id="aAdditionalChargesClose" href="#" class="roomtype-popup-close"></a></div>
                                        <div class="roomtype-popup-description" style="text-align: justify; min-height: 150px;
                                            max-height: 250px; overflow: auto;padding-bottom:25px;">

                                           <div style="padding-bottom:50px;"> <div style="width:150px;float:left;"><span> No of ExtraBed/Unit </span> </div> <div style="float:left;"><asp:TextBox ID="txtNoOfExtraBed" onkeyup="fnExtraBedTotal()" CssClass="roomtype-popup-textbox" runat="server"></asp:TextBox> </div></div>
                                          <div style="padding-bottom:50px;">   <div  style="width:150px;float:left;"><span> Unit Price </span> </div> <div  style="float:left;"><asp:TextBox ID="txtExtraBedUnitPrice"  onkeyup="fnExtraBedTotal()" CssClass="roomtype-popup-textbox"  runat="server"></asp:TextBox> </div></div>
                                          <div style="padding-bottom:50px;">  <div  style="width:150px;float:left;"><span> Total Price </span></div> <div  style="float:left;"> <asp:TextBox ID="txtExtraBedTotalPrice"  onkeyup="fnExtraBedTotal()" CssClass="roomtype-popup-textbox"   runat="server"></asp:TextBox> </div></div>

                                            <p>
                                                <asp:Label ID="lblAdditionalCharges" runat="server"></asp:Label>
                                                 <asp:Label ID="lblEBBookingCode" style="display:none;" runat="server"></asp:Label>
                                            </p>
                                            <p style="padding-left:150px;">
                                                <asp:Label ID="Label14" runat="server"></asp:Label>
                                                <asp:Button ID="btnAddChargeYes" runat="server" CssClass="background-color: #e7e7e7;color:black;border:none;padding:15px 32px;text-align:center;text-decoration:none;display:inline-block;font-size:16px;width:20px !important;height:20px !important;" Text="YES" />
                                                <asp:Button ID="btnAddChargeNo" runat="server" CssClass="background-color: #e7e7e7;color:black;border:none;padding:15px 32px;text-align:center;text-decoration:none;display:inline-block;font-size:16px;width:20px !important;height:20px !important;"  Text="NO" />
                                            </p>
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
                                            <div style="float: left; padding-left: 10px; width: 80%;">
                                                <asp:Label ID="lblTotlaPriceHeading" Width="100%" runat="server"></asp:Label></div>
                                            <div style="float: right; padding-right: 5px;">
                                                <a id="atotalPriceClose" href="#" class="roomtype-popup-close"></a>
                                            </div>
                                        </div>
                                        <div class="roomtype-popup-description">
                                            <div id="dvPriceSave" runat="server">
                                                <div style="padding-top: 10px; padding-left: 0px; margin-bottom: 15px;">
                                                    <div style="float: left;">
                                                        <asp:CheckBox ID="chkComplimentaryToCustomer" CssClass="price-breakup-popup-label"
                                                            runat="server" Text="Complementary to Customer" /></div>
                                                    <div style="float: left; margin-left: 54px;">
                                                        <asp:CheckBox ID="chkComplimentaryFromSupplier" CssClass="price-breakup-popup-label"
                                                            runat="server" Text="Complementary from Supplier" /></div>
                                                    <div style="float: right; padding-right: 35px;">
                                                        <asp:Button ID="btnPriceBreakupSave" CssClass="roomtype-popup-buttons-save" runat="server"
                                                            Text="Save" /></div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                                <div style="margin-top: 0px; padding-left: 0px;">
                                                    <asp:CheckBox ID="chkComplimentaryArrivalTransfer" CssClass="price-breakup-popup-label"
                                                        runat="server" Text="Complementary Arrival Transfer" />
                                                    <asp:CheckBox ID="chkComplimentaryDepartureTransfer" CssClass="price-breakup-popup-label"
                                                        Style="padding-left: 25px;" runat="server" Text="Complementary Departure Transfer" />
                                                </div>
                                            </div>
                                            <div class="clear" style="padding-top: 10px;">
                                            </div>
                                            <div id="dvFillPrice" runat="server" class="row-col-12">
                                                <div class="row-col-3">
                                                    <asp:Label ID="Label4" CssClass="price-breakup-popup-label" Text="Room" runat="server"></asp:Label>
                                                    <asp:DropDownList ID="ddlRoomNos" CssClass="roomtype-popup-dropdown" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="row-col-3" style="margin-left: -10px;">
                                                    <asp:Label ID="Label3" CssClass="price-breakup-popup-label" Text="Price" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtsalepriceForAll" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div id="dvCostEdit" runat="server" class="row-col-3" style="margin-left: -10px;">
                                                    <asp:Label ID="Label5" CssClass="price-breakup-popup-label" Text="Cost Price" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtBreakupTotalPriceForAll" CssClass="roomtype-popup-textbox" 
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="clear">
                                                </div>
                                                <div class="row-col-6" style="padding-top: 10px;">
                                                    <asp:CheckBox ID="chkFillBlank" CssClass="price-breakup-popup-label" runat="server"
                                                        Text="Fill Blank Fields Only" />
                                                    <asp:Button ID="btnPriceBreakupFillPrice" CssClass="roomtype-popup-buttons-save"
                                                        Style="margin-left: 50px;" runat="server" Text="Fill Price" OnClick="btnPriceBreakupFillPrice_Click" />
                                                </div>
                                            </div>
                                            <div class="clear" style="padding-top: 20px;">
                                            </div>
                                            <div style="overflow: auto; min-height: 329px; max-height: 420px; min-width: 300px;
                                                padding-bottom: 10px; min-width: 700px;">
                                                <asp:DataList ID="dltotalPriceBreak" RepeatColumns="2" RepeatDirection="Horizontal"
                                                    Width="100%" runat="server">
                                                    <ItemTemplate>
                                                        <div style="border: 0px solid #ede7e1; max-width: 500px; min-width: 150px; padding-left: 5px;
                                                            background-color: #F5F5F5; padding: 10px;">
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
                                                                    OnRowDataBound="gvPricebreakup_RowDataBound" Width="100%" GridLines="Horizontal">
                                                                    <%-- --%>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="DATE">
                                                                            <ItemTemplate>
                                                                                <div style="padding-top: 5px;">
                                                                                    <asp:Label ID="lblBreakupDate" Text='<%# Eval("pricedate") %>' CssClass="roomtype-popup-label"
                                                                                        runat="server"></asp:Label><asp:Label ID="lblBreakupDateName" CssClass="roomtype-popup-label-sub"
                                                                                            runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblBreakupDate1" Text='<%# Eval("pricedate") %>' Visible="false" runat="server"></asp:Label>
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
                                                                                <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>' ></asp:Label></div>

                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="BOOKING CODE">
                                                                            <ItemTemplate>
                                                                                <div style="padding-top: 5px;">
                                                                                    <asp:Label ID="lblBookingCode" Text='<%# Eval("bookingcode") %>' ToolTip='<%# Eval("bookingcode") %>'
                                                                                        CssClass="roomtype-popup-label" runat="server"></asp:Label>
                                                                                     <%--'' Added shahul 02/06/18 --%>
                                                                                         <asp:TextBox ID="txtBookingCode" CssClass="roomtype-popup-textbox" 
                                                                                        style="width:90px;" Text='<%# Eval("bookingcode") %>' runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </ItemTemplate>
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
                                                                                         <asp:Label ID="lblwlbreakupPrice" CssClass="roomtype-popup-label" Text='<%# Eval("wlsaleprice") %>'
                                                                                        runat="server"></asp:Label>
                                                                                    <asp:TextBox ID="txtsaleprice" CssClass="roomtype-popup-textbox" onkeypress="validateDecimalOnly(event,this)"
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
                                                                                    <asp:TextBox ID="txtBreakupTotalPrice" CssClass="roomtype-popup-textbox" 
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
                                                                 <div class="clear"  style="padding-top:10px;"></div>
                                                                 <span id="spAvgPrice" runat="server" class="roomtype-popup-label"> &#8226;  PER DAY RATES ARE AVERAGE PRICE</span>
                                                                 
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
                                                   <asp:HiddenField ID="hdRMRoomId" runat="server" />
 <asp:HiddenField ID="hdRoomRatePlanSourcePopup" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="clear">
                            </div>
                            <div id="dvPager" runat="server">
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
                            <asp:HiddenField ID="hdRoomTypePopup6" runat="server" />
                            <asp:ModalPopupExtender ID="mpHotelAllotment" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                CancelControlID="aHotelAllotmentClose" EnableViewState="true" PopupControlID="pnlHotelAllotment"
                                TargetControlID="hdRoomTypePopup6">
                            </asp:ModalPopupExtender>
                    <asp:Panel runat="server" ID="pnlHotelAllotment" Style="display: none;z-index:9999999;" ScrollBars="Auto">
	<div class="roomtype-popup-special-events">
		<div id="Div8">
			<div class="roomtype-popup-title">
               <asp:Label ID="lblpopuphead" Text="+/- 7 Days" runat="server"> </asp:Label>
				<a id="aHotelAllotmentClose" href="#" class="roomtype-popup-close"></a>
			</div>
            <div class="roomtype-popup-title" >
            <asp:Label ID="lblAllotthotel"  
             runat="server"></asp:Label>
            </div>

			<div class="roomtype-popup-description-special-events" style="min-height: 150px;
                                    max-height: 650px; overflow: auto;">
				<div class="roomtype-popup-description-special-eve" style="text-align: justify; min-height: 150px;
                                        max-height: 650px; overflow: auto;">
						<div class="booking-form" style="background-color: White; padding: 10px;">

                         
								<asp:DataList ID="dlHotelallotment" RepeatColumns="2" RepeatDirection="Horizontal"
                                  Width="100%" runat="server">
	                                <ItemTemplate>
		                            <div style="border: 0px solid #ede7e1; max-width: 650px; min-width: 220px; padding-left: 5px;
                                       background-color: #F5F5F5; padding: 10px;">
			<div class="row-col-12">
				<asp:Label ID="lblRoomNo" Text='<%# Eval("roomno") %>' Visible="false" runat="server">
				</asp:Label>
				<div>
					<asp:Label ID="lblRoomSummary" Text='<%# Eval("roomno") %>' Visible="false"
                   CssClass="room-type-breakup-headings" runat="server">
					</asp:Label>
				</div>
			</div>
		
			<div class="row-col-12">
            <div style="display: block; background-color: #E0DAD5; height: 10px; padding-top: 5px;
                    padding-left: 5px; margin-top: 10px; padding-bottom: 10px;">
                    <asp:Label ID="lblallotlabel" Text='<%# " # Room "+ Eval("roomno").ToString() +  ": " +  Eval("roomheading").ToString()  %>'
                        CssClass="room-type-breakup-headings" runat="server"></asp:Label>
                </div>

			 <asp:GridView ID="gvHotelAllotment" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                  Width="100%" GridLines="Horizontal"><%-- --%>
					<Columns>
						<asp:TemplateField HeaderText="ROOM DETAILS">
							<ItemTemplate>
								<asp:Label ID="lblroomdetails" Text='<%# Eval("roomdetails") %>' runat="server">
       							</asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						  <asp:TemplateField HeaderText="FROM DATE">
						<ItemTemplate>
						<asp:Label ID="lblfromdate" Text='<%# Eval("fromdate", "{0:dd/MM/yyyy}")%>' runat="server">
						</asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
						 <asp:TemplateField HeaderText="TO DATE">
						<ItemTemplate>
						<asp:Label ID="lbltodate" Text='<%# Eval("todate", "{0:dd/MM/yyyy}")%>' runat="server">
						</asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
						 <asp:TemplateField HeaderText="PRICE FROM">
						<ItemTemplate>
						<asp:Label ID="lblsaleprice" Text='<%# Eval("saleprice") %>' runat="server">
						</asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="MIN STAY">
						<ItemTemplate>
						<asp:Label ID="lblminstay"  Text='<%# Eval("minstay") %>' runat="server" >
						</asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
					</Columns>
					<HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
					<RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
				</asp:GridView>
				
							
						
						</div>
						
					</div>
					
			
	                            </ItemTemplate>
                                 </asp:DataList>

							</div>
					        <div class="clear" style="padding-bottom: 20px;"></div>
									
								</div>
							</div>
						</div>
					</div>
				</asp:Panel>
                    <asp:Panel runat="server" ID="pnlSpecialEvents" Style="display: none;z-index:9999999;">
                        <div class="roomtype-popup-special-events">
                            <div id="Div6">
                                <div class="roomtype-popup-title">
                                    Special Events<a id="aSpecialEventsClose" href="#" class="roomtype-popup-close"></a></div>
                                <div class="roomtype-popup-description-special-events" style="min-height: 150px;
                                    max-height: 650px; overflow: auto;">
                                    <div class="roomtype-popup-description-special-eve" style="text-align: justify; min-height: 150px;
                                        max-height: 650px; overflow: auto;">
                                        <div style="float: right; padding-right: 20px; margin-top=-25px;">
                                            <asp:Button ID="btnSpclEventSave" CssClass="roomtype-popup-buttons-save" Style="margin-left: 25px;"
                                                runat="server" Text="Save" /></div>
                                        <asp:DataList ID="dlSpecialEvents" runat="server" Width="100%">
                                            <ItemTemplate>
                                                <div class="booking-form" style="background-color: White; padding: 10px;">
                                                    <div class="row-col-8">
                                                        <div class="row-col-3">
                                                            <asp:Label ID="Label6" CssClass="roomtype-popup-label" runat="server" Text="DATE: "></asp:Label>
                                                        </div>
                                                        <div class="row-col-6" style="margin-left: -10px;">
                                                            <asp:Label ID="lblEventDate" CssClass="roomtype-popup-label" runat="server" Text='<%# Eval("spleventdate") %>'></asp:Label>
                                                            <asp:Label ID="lblEventDatefull" CssClass="roomtype-popup-label" Visible="false"
                                                                runat="server" Text='<%# Eval("spleventdate") %>'></asp:Label>
                                                        </div>
                                                        <div class="row-col-3">
                                                        </div>
                                                        <div class="clear">
                                                        </div>
                                                        <div class="row-col-3" style="margin-top: 20px;">
                                                            <asp:Label ID="Label7" CssClass="roomtype-popup-label" runat="server" Text="EVENTS: "></asp:Label>
                                                            <asp:Label ID="lblEventCode" Visible="false" runat="server" Text='<%# Eval("spleventcode") %>'></asp:Label>
                                                            <asp:Label ID="lblEventName" Visible="false" runat="server" Text='<%# Eval("spleventname") %>'></asp:Label>
                                                        </div>
                                                        <div class="row-col-9" style="margin-top: 10px;">
                                                            <div class="dropdown-special-events" style="width: 85%;">
                                                                <asp:DropDownList ID="ddlEvents" Width="100%" Height="26px" Style="border: 1px solid #fff"
                                                                    CssClass="special-event-drop-font" runat="server">
                                                                    <asp:ListItem Text="--Select --" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="row-col-6" style="margin-top: 10px;">
                                                            <asp:CheckBox ID="chkSpclComplimentaryToCustomer" CssClass="roomtype-popup-label"
                                                                runat="server" Text="Complementary to Customer" />
                                                        </div>
                                                        
                                                        <div class="row-col-6" style="margin-top: 10px;">
                                                            <asp:CheckBox ID="chkSpclComplimentaryFromSupplier" CssClass="roomtype-popup-label"
                                                                runat="server" Text="Complementary from Supplier" />
                                                        </div>
                                                         <div class="clear">
                                                        </div>
                                                        <div class="row-col-3">
                                                            <asp:Label ID="Label11" CssClass="roomtype-popup-label" runat="server" Text="Remarks: "></asp:Label>
                                                        </div>
                                                           <div class="clear">
                                                        </div>
                                                         <div class="row-col-12" style="margin-top: 10px;margin-left:10px;">
                                                            <asp:Label ID="lblsplremarks" style="font-size: 13px;" Text='<%# Eval("remarks") %>' runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row-col-4">
                                                        <div class="row-col-8" style="float: right;">
                                                            <asp:Label ID="lblCompulsory" Text='<%# Eval("compulsorytype") %>' Visible="false"
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
                                                                        <asp:TextBox ID="txtPaxRate" Text='<%# Eval("paxrate")%>' onkeypress="validateDecimalOnly(event,this)"
                                                                            Width="70px" runat="server"></asp:TextBox>
                                                                               
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
                                                                                <asp:Label ID="lblwlmarkupperc" runat="server" Text='<%# Eval("wlmarkupperc") %>' ></asp:Label></div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PAX COST">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpaxCost" Text='<%# Eval("paxcost")%>' runat="server"></asp:Label>
                                                                        <asp:TextBox ID="txtPaxCost" Text='<%# Eval("paxcost")%>' onkeypress="validateDecimalOnly(event,this)"
                                                                            Width="70px" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SPECIAL EVENT COST VALUE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSpecialEventCostValue" Text='<%# Eval("spleventcostvalue").ToString() %>'
                                                                            runat="server"></asp:Label>
                                                                        <asp:Label ID="lblcostCurrcode" Text='<%# Eval("costCurrcode") %>' Visible="false"
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                            <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="clear" style="padding-bottom: 20px;">
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
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
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <br class="clear" />
        </div>
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
            var curcode = $("#hdSliderCurrency").val();
            var hdPriceMinRange = $("#hdPriceMinRange");
            var hdPriceMaxRange = $("#hdPriceMaxRange");
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
                        // alert(vmin);
                    }

                });

                ammount_from.val(slider_range.slider("values", 0) + curcode);
                ammount_to.val(slider_range.slider("values", 1) + curcode);
                // alert(hdPriceMax.val());
            });

        }
    </script>
    <script>
        function CallFilter() {
            document.getElementById("<%= hdfilterType.ClientID %>").value = '';
            document.getElementById("<%= hdPriceSliderActive.ClientID %>").value = '0';
            document.getElementById("<%= btnFilter.ClientID %>").click();
        }

        function CallFilterForRoomClassification() {

            document.getElementById("<%= btnFilterForRoom.ClientID %>").click();
        }
    </script>
<!-- \\ scripts \\ -->
    <center>
        <div id="Loading1" runat="server" style="height: 150px; width: 500px;">
            <img alt="" id="Image1" runat="server" src="~/img/page-loader.gif" />
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
                          <asp:HiddenField ID="hdmealcode" runat="server" />
    <asp:Button ID="btnSelectedSpclEvent" runat="server" Style="display: none;"  />

      <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
    </form>
</body>
</html>
