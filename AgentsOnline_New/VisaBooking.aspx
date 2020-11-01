<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VisaBooking.aspx.vb" Inherits="VisaBooking" %>

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
<%--    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"
        type="text/javascript"></script>--%>
            <script language="javascript" type="text/javascript">
                $(document).ready(function () {
                    AutoCompleteExtender_NationalityKeyUp();
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



        function VisaCustomerAutocompleteselected(source, eventArgs) {
            if (source) {

                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtVisaCustomer", "txtVisaCustomerCode");

                var hiddenfieldIDCustomer = source.get_id().replace("AutoCompleteExtender_txtVisaCustomer", "txtVisaCustomer");
                var hiddenfieldIDCtry = source.get_id().replace("AutoCompleteExtender_txtVisaCustomer", "txtSourceCountry");
                var hiddenfieldIDCtryCode = source.get_id().replace("AutoCompleteExtender_txtVisaCustomer", "txtSourceCountryCode");
                $get(hiddenfieldID).value = eventArgs.get_value();
                if (hiddenfieldIDCustomer != '') {
                    GetCountryDetails($get(hiddenfieldID).value, hiddenfieldIDCtry, hiddenfieldIDCtryCode);

                }

                else {
                    $get(hiddenfieldID).value = '';
                }

            }

        }

        function fnReadOnly(txt) {
            event.preventDefault();
        }

        function showmessage(ddlvisa, ddlVisatype, txtVisaPrice, txtVisaValue, rowid,txtChildVisaPrice) {
            var ddlvisa1 = document.getElementById(ddlvisa);
            var ddlVisatype1 = document.getElementById(ddlVisatype);
            var txtVisaPrice1 = document.getElementById(txtVisaPrice);
            var txtVisaValue1 = document.getElementById(txtVisaValue);
            var txtChildVisaPrice1 = document.getElementById(txtChildVisaPrice);


            if ((ddlvisa1.value == 'Endorsed in Mothers Visa') || (ddlvisa1.value == 'Endorsed in Fathers Visa')) {
                showDialog('Alert Message', 'Child Photo is mandatory in the Passport in order to Apply.', 'warning');

            }


            if (ddlvisa1.value == 'Not Required') {
                txtVisaPrice1.value = '';
                txtVisaValue1.value = '';
                txtChildVisaPrice1.value = '';
                ddlVisatype1.selectedIndex = "0";
                $('.custom-select-ddlAvailability').next('span').children('.customSelectInner').text(jQuery("#ddlVisatype1 :selected").text());
                ddlVisatype1.setAttribute("disabled", false);

            }
            else {

                ddlVisatype1.removeAttribute('disabled');


            }

        }

        function CalculateVisaValue(NoofVisas, Visaprice, Visavalue, wlVisaprice, wlVisavalue, wlmarkup, noofchild, rowid, VisaCostPrice, VisaCostValue,visachildprice) {



            txtNoofVisas = document.getElementById(NoofVisas);
            txtVisaprice = document.getElementById(Visaprice);
            txtVisavalue = document.getElementById(Visavalue);
            txtwlVisaPrice = document.getElementById(wlVisaprice);
            txtwlVisaValue = document.getElementById(wlVisavalue);
            hdnwlmarkup = document.getElementById(wlmarkup);

    

            var hdvisachildprice = document.getElementById(visachildprice);

            var ddlchild = document.getElementById(noofchild);


   

            var childvisa = '0';


            if ((txtNoofVisas.value == '')) {
                txtNoofVisas.value = '0'
                txtVisavalue.value = '';
            }
            if ((ddlchild.value == '--')) {
                childvisa = '0';
            }
            else {
                childvisa = ddlchild.value;
            }

            if (txtVisaprice.value == '') {
                txtVisavalue.value = '';
            }
            if (txtwlVisaPrice.value == '') {
                txtwlVisaPrice.value = '';
            }

            if (hdvisachildprice.value == '') {
                hdvisachildprice.value = '';
            }

            var totalvisas = (parseFloat(txtNoofVisas.value) + parseFloat(childvisa));
      

            if ((txtVisaprice.value != '') && (totalvisas.value != '')) {

                var totalAdultamt = ((parseFloat(txtNoofVisas.value)) * parseFloat(txtVisaprice.value));

                var totalChildamt = (parseFloat(childvisa) * parseFloat(hdvisachildprice.value));


                var wltotalamt = (parseFloat(totalAdultamt) * (100 + parseFloat(hdnwlmarkup.value)) / 100);

                if (hdvisachildprice.value == '') {
                    totalChildamt = 0;
                }

                txtVisavalue.value = (totalAdultamt + totalChildamt).toFixed(2);

                if (hdnwlmarkup.value != '') {

                    var wlvisapricenew = (parseFloat(txtVisaprice.value) * ((100 + parseFloat(hdnwlmarkup.value)) / 100));
                    txtwlVisaPrice.value = wlvisapricenew.toFixed(2);
                    txtwlVisaValue.value = wltotalamt.toFixed(2);
                }
                else {
                    txtwlVisaPrice.value = txtVisaprice.value
                    txtwlVisaValue.value = txtVisavalue.value
                }



            }


            txtVisaCostPrice = document.getElementById(VisaCostPrice);
            txtVisaCostValue = document.getElementById(VisaCostValue);

            if (txtNoofVisas.value == '') {
                txtNoofVisas.value = 0
            }
            if (txtVisaCostPrice.value == '') {
                txtVisaCostPrice.value = 0
            }

            if (txtNoofVisas.value > 0 && txtVisaCostPrice.value > 0) {
                txtVisaCostValue.value = (parseFloat(txtNoofVisas.value) * parseFloat(txtVisaCostPrice.value));
            }
            else {
                txtVisaCostValue.value = 0
            }



        }
        function CalculatewlVisaValue(NoofVisas, Visaprice, Visavalue, noofchild, rowid) {

            txtNoofVisas = document.getElementById(NoofVisas);
            txtwlVisaprice = document.getElementById(Visaprice);
            txtwlVisavalue = document.getElementById(Visavalue);
            ddlchild = document.getElementById(noofchild);

            var childvisa = '';

            if ((txtNoofVisas.value == '') && (ddlchild.value == '')) {
                txtwlVisavalue.value = '';
            }
            if (txtwlVisaprice.value == '') {
                txtwlVisavalue.value = '';
            }

            if ((ddlchild.value == '--')) {
                childvisa = '0';
            }
            else {
                childvisa = ddlchild.value;
            }

            var totalvisas = (parseFloat(txtNoofVisas.value) + parseFloat(childvisa));

            if ((txtwlVisaprice.value != '') && (totalvisas.value != '')) {
                var totalamt = ((parseFloat(totalvisas)) * parseFloat(txtwlVisaprice.value));
                txtwlVisavalue.value = totalamt.toFixed(2);
            }

        }
        function SourceCountryautocompleteselected(source, eventArgs) {

            if (source) {

                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtSourceCountry", "txtSourceCountryCode");
                var hiddenfieldIDCountry = source.get_id().replace("AutoCompleteExtender_txtSourceCountry", "txtSourceCountry");
                $get(hiddenfieldID).value = eventArgs.get_value();

            }

        }

        function GetCountryDetails(CustCode, hiddenfieldIDCtry, hiddenfieldIDCtryCode) {
            $.ajax({
                type: "POST",
                url: "VisaBooking.aspx/GetCountryDetails",
                data: '{CustCode:  "' + CustCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var countries = xml.find("Countries");

                    $.each(countries, function () {
                        var customer = $(this);
                        // alert($(this).find('ctryname').text());

                        $get(hiddenfieldIDCtry).value = $(this).find('ctryname').text();

                        $get(hiddenfieldIDCtryCode).value = $(this).find('ctrycode').text();
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


        function SourceCountryautocompleteselected(source, eventArgs) {

            if (source) {

                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtSourceCountry", "txtSourceCountryCode");
                var hiddenfieldIDCountry = source.get_id().replace("AutoCompleteExtender_txtSourceCountry", "txtSourceCountry");
                $get(hiddenfieldID).value = eventArgs.get_value();

            }

        }

        //        '*** Danny 22/10/2018 FreeForm SupplierAgent        
        function SupplierAutocompleteSelected(source, eventArgs) {

            if (source) {

                var hiddenSupplierCode = source.get_id().replace("AutoCompleteExtender_Supplier", "Txt_SupplierCode");
                var hiddenSupplierName = source.get_id().replace("AutoCompleteExtender_Supplier", "Txt_SupplierName");

                if (hiddenSupplierName != '') {
                    $get(hiddenSupplierCode).value = eventArgs.get_value();
                }
                else {
                    $get(hiddenSupplierCode).value = '';
                }
            }
        }
        function SupplierAgentAutocompleteSelected(source, eventArgs) {
            if (source) {
                var hiddenSupplierAgentCode = source.get_id().replace("AutoCompleteExtender_SupplierAgent", "Txt_SupplierAgentCode");
                var hiddenSupplierAgentName = source.get_id().replace("AutoCompleteExtender_SupplierAgent", "Txt_SupplierAgentName");

                if (hiddenSupplierAgentName != '') {
                    $get(hiddenSupplierAgentCode).value = eventArgs.get_value();
                }
                else {
                    $get(hiddenSupplierAgentCode).value = '';
                }
            }
        }

        function NationalityAutocompleteSelected(source, eventArgs) {
            if (source) {
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtNationality", "txtNationalityCode");
                var hiddenfieldName = source.get_id().replace("AutoCompleteExtender_txtNationality", "txtNationality");
                var ddlVisa = source.get_id().replace("AutoCompleteExtender_txtNationality", "ddlVisa");
                var ddlVisaType = source.get_id().replace("AutoCompleteExtender_txtNationality", "ddlVisatype");
                var txtPrice = source.get_id().replace("AutoCompleteExtender_txtNationality", "txtVisaPrice");
                var txtValue = source.get_id().replace("AutoCompleteExtender_txtNationality", "txtVisaValue");

                if (hiddenfieldName != '') {
                    $get(hiddenfieldID).value = eventArgs.get_value();
                    $get(txtPrice).value = '';
                    $get(txtValue).value = '';
                    SelectVisa($get(hiddenfieldID).value, $get(ddlVisa), $get(ddlVisaType))
                }
                else {
                    $get(hiddenfieldID).value = '';
                }

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


        //       function Selectedtype(ddlvisatype, rowNumber) {
        //           var hd = document.getElementById("< %= hddlRowNumber.ClientID %>");
        //           hd.value = rowNumber;
        //           document.getElementById("< %= btnSelected.ClientID %>").click();
        //       }


    </script>
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestUserControl);

        function EndRequestUserControl(sender, args) {

            AutoCompleteExtender_NationalityKeyUp();

        }

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


        function AutoCompleteExtender_NationalityKeyUp() {

            $("#<%=dlVisaInfo.ClientID%> tr input[id*='txtNationality']").each(function () {

                $(this).change(function (event) {

                    var hiddenfieldID1 = $(this).attr("id").replace("txtNationality", "txtNationality");
                    var hiddenfieldID = $(this).attr("id").replace("txtNationality", "txtNationalityCode");


                    if ($get(hiddenfieldID1).value == '') {

                        $get(hiddenfieldID).value = '';
                    }

                });

                $(this).keyup(function (event) {
                    var hiddenfieldID1 = $(this).attr("id").replace("txtNationality", "txtNationality");
                    var hiddenfieldID = $(this).attr("id").replace("txtNationality", "txtNationalityCode");


                    if ($get(hiddenfieldID1).value == '') {

                        $get(hiddenfieldID).value = '';
                    }

                });
            });

        }

    </script>
    
    <script language="javascript" type="text/javascript">

        


    </script>
    <script type="text/javascript">
    //<![CDATA[
        function pageLoad() { // this gets fired when the UpdatePanel.Update() completes
            ReBindMyStuff();

        }

      

    //]]>
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
    <asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
	<div class="header-a">
		<div class="wrapper-padding">	
         <div class="header-user" style="margin-top:2px;"><asp:Label ID="lblHeaderUserName"  runat="server" ></asp:Label> </div>				
			<div class="header-phone" id="dvlblHeaderAgentName" runat="server" style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-agentname" style="padding-left:105;margin-top:2px;">
				<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>--%>
                <asp:LinkButton ID="btnMyAccount" style="    padding: 0px 10px 0px 0px;"  UseSubmitBehavior="False" OnClick="btnMyAccount_Click"
                        CssClass="header-account-button" runat="server" Text="Account" causesvalidation="true"></asp:LinkButton>
			</div>
              <div class="header-agentname" style="margin-top:2px;"><asp:Label ID="lblHeaderAgentName" tyle="padding: 0px 10px 0px 0px;" runat="server" ></asp:Label> </div>
            <div class="header-lang">
            <%--<asp:UpdatePanel   runat="server"><ContentTemplate><asp:Button ID="btnLogOut"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Log Out"></asp:Button>
</ContentTemplate></asp:UpdatePanel>--%>
                     <asp:LinkButton ID="btnLogOut"   UseSubmitBehavior="False" OnClick="btnLogOut_Click"
                                    CssClass="header-account-button" runat="server" Text="Log Out" 
                                    ></asp:LinkButton>
			<%--	<a href="#">Log Out</a>--%>
			</div>
	<%--		<div class="header-social"> commented on 7/06/2017
				<a href="#" class="social-twitter"></a>
				<a href="#" class="social-facebook"></a>
				<a href="#" class="social-vimeo"></a>
				<a href="#" class="social-pinterest"></a>
				<a href="#" class="social-instagram"></a>
			</div>--%>
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
                <div class="page-head">
                    <div class="page-title"   >
                    <asp:Label ID="lblheading" runat="server" Text="VISA BOOKING"></asp:Label>
                    <span ></span></div>
                    <div class="breadcrumbs">
                        <a href="Home.aspx?Tab=1">Home</a> <%--/ <a href="#">Tours</a>--%>
                    </div>
                    <div class="clear" style="padding-bottom:10px;">
                    </div>
<div class="page-search-content-search"  style="padding-top:10px;"    >
<div class="search-tab-content" style="background-color:White !important;">
                                    <div id="divvisacustomer" runat="server"  class="booking-form-i-a"  style="float: left; margin-top:20px;margin-left: 30px; width: 200px">
                                                <label style=" font-family: Raleway !important;
                                        font-size: 11px !important; text-transform: uppercase !important;display: inline-block;  font-weight :600;">
                                        Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div id="dvvisacustomercode" runat="server"  class="input" style="width: 220px;  font-family: Raleway;
                                                font-size: 11px !important; text-transform: uppercase !important;display: inline-block;  ">
                                              <asp:TextBox ID="txtVisaCustomer" width="100%"  Style="font-family: Raleway; font-size: 11px !important;
                                                            text-transform: uppercase !important;" runat="server" placeholder="--"></asp:TextBox>
                                            <asp:TextBox ID="txtVisaCustomerCode" runat="server" Style="display: none"></asp:TextBox>
                                            <asp:AutoCompleteExtender   ID="AutoCompleteExtender_txtVisaCustomer" runat="server"
                                                CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="-1" ServiceMethod="GetCustomers" TargetControlID="txtVisaCustomer"
                                                OnClientItemSelected="VisaCustomerAutocompleteselected">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                             <div class="search-large-i">
                                        <!-- // -->
                                        <div id="dvsourcectry" runat="server"  class="srch-tab-line no-margin-bottom" style="margin-left: 85px; margin-top:20px; width: 240px;">
                                 
                                             <label style=" font-family: Raleway !important;
                                font-size: 11px !important; text-transform: uppercase !important;display: inline-block;  font-weight :600;">
                                                   Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                               <div class="input-a" style="height:14px ;">
                                                <div class="select-wrapper" style="padding-top:0px !important;" >
                                                 <asp:TextBox ID="txtSourceCountry" runat="server" Width="280px" placeholder="--" AutoComplete="off"></asp:TextBox>
                                                        <asp:TextBox ID="txtSourceCountryCode" runat="server" Style="display: none"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtSourceCountry" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtSourceCountry"
                                                            UseContextKey="true" OnClientItemSelected="SourceCountryautocompleteselected">
                                                        </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                                                    <div class="clear">
                                            </div>
                                        </div>
                                    </div>
                                    <!-- // -->
                                    <div class="search-large-i">
                                        <div id="dvchkoverride" runat="server" class="srch-tab-line no-margin-bottom" style="margin-left: -250px;margin-top:50px;">
                                            <asp:CheckBox ID="chkOveridePrice" style="padding-left:25px"  autopostback=true  runat="server" />
                                <asp:Label ID="lbloverrideprice"   style="padding-bottom:40px"  runat="server" CssClass="page-search-content-override-price"
                                    Text="Override Price"></asp:Label>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        </div> 
                                    
                         
                                <asp:DataList ID="dlVisaInfo" Width="100%"   runat="server">
        <ItemTemplate > 
          
                            <div class="page-search-p" style="margin-top:0px">
                               
                               
                                <div class="booking-form">
                                    <div class="booking-form-i-a" style="float: left; margin-left: 10px; width: 200px">
                                        <label class="required">
                                            Nationalty<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                        <div class="input" style="width: 220px">
                                            <asp:TextBox ID="txtNationality" Style="font-family: Raleway; font-size: 11px !important;
                                                text-transform: uppercase !important;" OnTextChanged="txtNationality_click" runat="server"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtNationality" runat="server"
                                                CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="-1" ServiceMethod="GetNationality" TargetControlID="txtNationality"
                                                UseContextKey="true" OnClientItemSelected="NationalityAutocompleteSelected">
                                            </asp:AutoCompleteExtender>
                                            <asp:TextBox ID="txtNationalityCode" style="display:none" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!-- // -->
                                    <div class="search-large-i">
                                        <!-- // -->
                                        <div class="srch-tab-line no-margin-bottom" style="margin-left: 85px; width: 240px;">
                                 
                                                <label style ="font-size: 11px !important">
                                                    Visa<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                               <div class="input-a" style="height:18px ;">
                                                <div class="select-wrapper" style="padding-top:0px !important;margin-top:-6px;" >
                                                    <asp:DropDownList ID="ddlVisa" Width="20px" Height="26px" Style="border: 1px solid #fff;font-size:12px"
                                                        AutoPostBack="True"  runat="server"  OnSelectedIndexChanged="ddlVisa_SelectedIndexChanged"
                                                        CssClass="custom-select custom-select-ddlAvailability">
                                                        <asp:ListItem Text="--"></asp:ListItem>
                                                        <asp:ListItem Text="Required"></asp:ListItem>
                                                        <asp:ListItem Text="Not Required"></asp:ListItem>
                                                        <asp:ListItem Text="Endorsed in Mothers Visa"></asp:ListItem>
                                                        <asp:ListItem Text="Endorsed in Fathers Visa"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </div>
                                    <!-- // -->
                                    <div class="search-large-i">
                                        <div class="srch-tab-line no-margin-bottom" style="margin-left: -210px;">
                                            <label style ="font-size: 11px !important">
                                                Visa Type<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a" style="height:18px ;width: 280px;">
                                            <div class="select-wrapper" style="width: 280px;margin-top:-6px;">
                                                <asp:DropDownList ID="ddlVisatype" Height="26px" Style="border: 1px solid #fff;font-size:12px" runat="server"
                                                    AutoPostBack="true" OnSelectedIndexChanged ="ddlVisatype_SelectedIndexChanged"
                                                    CssClass="custom-select custom-select-ddlAvailability">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        </div> 
                                    </div>
                         
                                </div>

                                <%--'*** Danny 22/10/2018 FreeForm SupplierAgent--%>
                                        <div class="clear">
                                        </div>
                                        <div class="booking-form" style="margin-left: 10px; margin-top: 10px">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-18 col-lg-4">
                                                    <div class="booking-form-i-a">
                                                        <label class="required">
                                                            Supplier</label>
                                                        <div class="input">
                                                            <asp:TextBox ID="Txt_SupplierName" class="form-control1" Text='<%# Eval("SupplierName") %>' runat="server" ></asp:TextBox>
                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender_Supplier" runat="server"
                                                                CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                MinimumPrefixLength="-1" ServiceMethod="GetSuppliers" TargetControlID="Txt_SupplierName"
                                                                UseContextKey="true" OnClientItemSelected="SupplierAutocompleteSelected">
                                                            </asp:AutoCompleteExtender>
                                                            <asp:TextBox ID="Txt_SupplierCode" Style="display: none;" Text='<%# Eval("preferredsupplier") %>' runat="server"></asp:TextBox>
                                                           
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-18 col-lg-4">
                                                    <div class="booking-form-i-a">
                                                        <label class="required">
                                                            Supplier Agent</label>
                                                        <div class="input">
                                                            <asp:TextBox ID="Txt_SupplierAgentName" class="form-control1" Text='<%# Eval("SupplierAgentName") %>'  runat="server"></asp:TextBox>
                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender_SupplierAgent" runat="server"
                                                                CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                MinimumPrefixLength="-1" ServiceMethod="GetSuppliersAgents" TargetControlID="Txt_SupplierAgentName"
                                                                UseContextKey="true" OnClientItemSelected="SupplierAgentAutocompleteSelected">
                                                            </asp:AutoCompleteExtender>
                                                            <asp:TextBox ID="Txt_SupplierAgentCode" Style="display: none" Text='<%# Eval("SupplierAgentCode") %>' runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

         <div class="clear">
        </div>
                       
                             <div class="booking-form" style="margin-left: 10px; margin-top: 10px">
                                    <div class="booking-form-i-a" style="width: 100px;">
                                        <label>No. of Adults<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                        <div class="input-a"">
                                            <asp:TextBox ID="txtNoOfVisas" Style="border: none; font-size: 12px;text-align:right " onkeypress="validateDecimalOnly(event,this)"
                                                runat="server"></asp:TextBox></div>
                                    </div>
                                    
                                    <div class="srch-tab-line no-margin-bottom">
                                     <div class="booking-form-i-a" style="margin-left:20px;width: 150px;" >
                                        <label  style="margin-left:20px;">
                                            No. of Childs</label>
                                       <div class="select-wrapper" style="width: 100px; margin-left :20px;margin-top:6px;">
                                                <asp:DropDownList ID="ddlchild"  Height="32px" class="custom-select custom-select-ddlTourChildren"
                                                     AutoPostBack="true" OnSelectedIndexChanged ="ddlchild_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem Text="--"></asp:ListItem>
                                                        <asp:ListItem Text="1"></asp:ListItem>
                                                        <asp:ListItem Text="2"></asp:ListItem>
                                                        <asp:ListItem Text="3"></asp:ListItem>
                                                        <asp:ListItem Text="4"></asp:ListItem>
                                                        <asp:ListItem Text="5"></asp:ListItem>
                                                        <asp:ListItem Text="6"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    

                                  

                                </div>
                                <div class="booking-form" style="margin-left: 10px; margin-top: 10px;">
                                    <div class="search-large-i" style="margin-left: 5px;">
                                        <div class="srch-tab-line no-margin-bottom">
                                            <div id="chdage" runat="server">
                                                <label>
                                                    Child Ages</label>
                                            </div>
                                            <div class="srch-tab-left">
                                                <div class="srch-tab-3c" id="dvChild1" runat="server">
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtchildage1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-3c" id="dvChild2" runat="server">
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtchildage2" placeholder="CH 2" runat="server" onchange="validateAge(this)"
                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-3c" id="dvChild3" runat="server">
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtchildage3" placeholder="CH 3" runat="server" onchange="validateAge(this)"
                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="srch-tab-right">
                                                <div class="srch-tab-3c" id="dvChild4" runat="server">
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtchildage4" placeholder="CH 4" runat="server" onchange="validateAge(this)"
                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-3c" id="dvChild5" runat="server">
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtchildage5" placeholder="CH 5" runat="server" onchange="validateAge(this)"
                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-3c" id="dvChild6" runat="server">
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtchildage6" placeholder="CH 6" runat="server" onchange="validateAge(this)"
                                                            onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                                <div class="booking-form" style="margin-left: -5px; margin-top: 10px">
                                    <div class="booking-form-i-a" style="padding-left: 0px; width: 120px; margin-left: 20px;">
                                        <label>
                                            Visa Date<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                        <div class="input-a" style="font-size: 14px;">
                                            <asp:TextBox ID="txtVisaDate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                AutoComplete="off" runat="server"></asp:TextBox>
                                            <span class="date-icon"></span>
                                            <asp:HiddenField ID="hdnConfDate" runat="server" />
                                        </div>
                                    </div>
                                    <div id="dvVisaPrice" runat="server" class="booking-form-i-b" style="float: left;
                                        width: 110px; margin-left: 45px;">
                                        <label>
                                            Visa Price<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                        <div class="input">
                                            <asp:TextBox ID="txtVisaPrice" onkeypress="validateDecimalOnly(event,this)" AutoComplete="off" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div id="dvVisaChildPrice" runat="server" class="booking-form-i-b" style="float: left;
                                        width: 110px; margin-left: 45px;">
                                        <label>
                                            Visa Child Price<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                        <div class="input">
                                            <asp:TextBox ID="txtVisaChildPrice" onkeypress="validateDecimalOnly(event,this)" AutoComplete="off" runat="server"></asp:TextBox></div>
                                    </div>

                                    <div id="dvwlVisaPrice" runat="server" class="booking-form-i-b" style="float: left;
                                        width: 110px; margin-left: 45px;">
                                        <label>
                                            Visa Price</label>
                                        <div class="input">
                                            <asp:TextBox ID="txtwlVisaPrice" onkeypress="validateDecimalOnly(event,this)" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div id="dvVisaValue" runat="server" class="booking-form-i-a" style="width: 110px;
                                        margin-left: 20px;">
                                        <label>
                                            Visa Value</label>
                                        <div class="input">
                                            <asp:TextBox ID="txtVisaValue" runat="server"></asp:TextBox></div>
                                    </div>
                                    <div id="dvwlVisaValue" runat="server" class="booking-form-i-a" style="width: 110px;
                                        margin-left: 20px;">
                                        <label>
                                            Visa Value</label>
                                        <div class="input">
                                            <asp:TextBox ID="txtwlVisaValue" runat="server"></asp:TextBox></div>
                                    </div>
                                    
                                  
                                    <div class="clear">
                                    </div>
                                  
                                  

                                    <div class="clear" style="padding-bottom: 0px; padding-top: 10px; margin-left: 10px;">
                                        <asp:Label ID="lblIncTax" CssClass="page-search-content-override-price" runat="server"
                                            Text="# Price Incl of Taxes & vat"></asp:Label>
                                    </div>
                                    <asp:Label ID="lblrowno" Text='<%# Eval("Vlineno") %>' Visible="false" runat="server"></asp:Label>
                                    <div class="search-large-i" style="margin-left: 580px; margin-top: -50px;" id="dvComplementCust"
                                        runat="server">
                                        <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                            <asp:CheckBox ID="chkComplementCust" runat="server" />
                                            <asp:Label ID="lblcomplemetarycust" runat="server" CssClass="page-search-content-override-price"
                                                Text="Complementary to Customer"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="booking-form-i-a" style="width: 200px; margin-left: 80px; padding-top: 25px;">
                                        <div style="margin-left: 820px; margin-top: -80px;">
                                            <label>
                                            </label>
                                            <div class="input" style="border: none;">
                                                <asp:LinkButton CssClass="room-type-total-price" ID="RemoveRow" Width="200px" style="padding-top:20px;" Text="DELETE ROW"
                                                    OnClick="RemoveRow_Click" runat="server"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                               <div id="div_VatCost" visible="false"   runat="server" >
                                        <div style="padding-left: 5px">
                                   <div>
                                        <div  class="booking-form-i-a">
                                            <%--'*** Danny 22/10/2018 FreeForm SupplierAgent--%>
                                            <label>
                                                <asp:Label runat="server" ID="lblCostPrice" Text="Visa Cost(---)"></asp:Label>
                                                </label>
                                            <div class="input">
                                                <asp:TextBox ID="Txt_VisaCost"   AutoComplete="off" runat="server" onkeypress="validateDecimalOnly(event,this)"></asp:TextBox></div>
                                        </div>                                                                
                                            </div>
                                            <div >                                                               
                                                <div id="Div3" runat="server" class="booking-form-i-a">
                                                    <%--'*** Danny 22/10/2018 FreeForm SupplierAgent--%>
                                                    <label>
                                                        <asp:Label runat="server" ID="lblCostTotal" Text="Visa Cost Value(---)"></asp:Label></label>
                                                    <div class="input">
                                                        <asp:TextBox ID="Txt_VisaCostValue"   AutoComplete="off" runat="server" onkeypress="validateDecimalOnly(event,this)"></asp:TextBox></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
            <asp:HiddenField ID="hdnplistcode" runat ="server" />    
            <asp:HiddenField ID="hdncplistcode" runat ="server" /> 
            <asp:HiddenField ID="hdncostprice" runat ="server" />  
              <asp:HiddenField ID="hdnChildcostprice" runat ="server" />  
            <asp:HiddenField ID="hdnpartycode" runat ="server" />    
            <asp:HiddenField ID="hdnwlcurrcode" runat ="server" />     
            <asp:HiddenField ID="hdnwlmarkup" runat ="server" />     
            <asp:HiddenField ID="hdnwlconvrate" runat ="server" />                           
            <asp:HiddenField ID="hdVisaCostValue" runat ="server" />  
                  <asp:HiddenField ID="hdnChildprice" runat ="server" />                 
                         
                                                   </div>
                   
                          </ItemTemplate> 
                </asp:DataList> 

                                   <div class="clear">
                    </div>
                                      
 
                    <footer class="search-footer" style="padding-top:60px;">

                                       <div class="search-large-i" style="margin-left :890px;margin-top :-40px;"  >
                                         <div  class="srch-tab-left" style="margin-right:0px;"  >
                             
  <asp:Button   id="btnaddrow" runat="server" type="button"      class="srch-btn-home"  text="Add Row"></asp:button>
  </div> 
						     <div  class="srch-tab-left"  >
                             
                          <asp:Button ID="btnSave" class="authorize-btn"      runat="server" Text="Book Visas"></asp:Button>  
                          </div>
                          <%-- <div  class="srch-tab-left" style="margin-left:300px;margin-top:-45px">
                      <input  id="btnTourReset"  type="button"   class="srch-btn-home"  value="Reset"/>
                        </div>--%></div>
						<div class="clear"></div>
					</footer>
                                </div>
</div>
                 
                
 
                                 <asp:HiddenField ID="hdnlineno" runat="server" />
                                <asp:HiddenField ID="hdCheckInPrevDay" runat="server" />
                                <asp:HiddenField ID="hdCheckOutNextDay" runat="server" />
                                 <asp:HiddenField ID="hdWhiteLabel" runat="server" />
                                  <asp:Button ID="btnSelected" runat="server" Style="display: none;" Text="Filter" />
                                 

                                <!-- \\ -->
                                <div class="clear">
                                </div>
                            </div>
      
                        </div>
          
                <div class="clear">
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

            var vminSelected = hdPriceMin.val()
            var maxSelected = hdPriceMax.val()
            var curcode = $("#hdSliderCurrency").val();
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

                ammount_from.val(slider_range.slider("values", 0) + curcode;
                ammount_to.val(slider_range.slider("values", 1) + curcode);
                // alert(hdPriceMax.val());
            });
            // alert(hdPriceMax.val());
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
     <asp:HiddenField ID="hdChildAgeLimit" runat="server" />
     <asp:HiddenField ID="hddlRowNumber" runat="server" />
        <asp:HiddenField ID="hdTab" runat="server" />
          <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
    <asp:CheckBox runat="server" AutoPostBack ="true"  ID="chkSelectTour1"  />
      <asp:HiddenField ID="hdSliderCurrency" runat="server" />
    <asp:LinkButton   CssClass="room-type-total-price"   ID="RemoveRow1" text="Delete"   runat="server"></asp:LinkButton>
      <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
    
    </form>
</body>
</html>
