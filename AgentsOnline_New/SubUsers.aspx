<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SubUsers.aspx.vb" Inherits="SubUsers" %>

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
            text-transform: uppercase;
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
            background-color: #F2F4F4;
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
        function ValidateSave() {


            var EmailId = $("#<%= txtSubUserEmailId.ClientID %>").val()
            if (EmailId == '') {

                showDialog('Alert Message', 'Please enter email id.', 'warning');
                HideProgess();
                return false;
            }
            if (validate(EmailId) == false) {
                showDialog('Alert Message', 'Invalid email id.', 'warning');
                HideProgess();
                return false;
            }
            var SubUserName = $("#<%= txtSubUserName.ClientID %>").val()
            if (SubUserName == '') {

                showDialog('Alert Message', 'Please enter sub user name.', 'warning');
                HideProgess();
                return false;
            }
            var password = $("#<%= txtpassword.ClientID %>").val()
            if (password == '') {

                showDialog('Alert Message', 'Please enter password.', 'warning');
                HideProgess();
                return false;
            }
            var ConfirmPassword = $("#<%= txtConfirmPassword.ClientID %>").val()
            if (ConfirmPassword == '') {

                showDialog('Alert Message', 'Wrong confirm password.', 'warning');
                HideProgess();
                return false;
            }
            if (ConfirmPassword != password) {

                showDialog('Alert Message', 'Please enter confirm password.', 'warning');
                HideProgess();
                return false;
            }
            return true;

        }

        function validate(email) {

            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            //var address = document.getElementById[email].value;
            if (reg.test(email) == false) {
                return (false);
            }
            else {
                return true;
            }
        }

        function ValidateSearch() {
            ShowProgess();


            //                var EmailId = $("#<%= txtSubUserEmailId.ClientID %>").val()
            //                if (EmailId == '') {
            //                  
            //                        showDialog('Alert Message', 'Please select booking date.', 'warning');
            //                        HideProgess();
            //                        return false;
            //                  

            //                }



            return true;

        }
        function Customersautocompleteselected(source, eventArgs) {

            if (eventArgs != null) {
                document.getElementById('<%=txtSAgentNameCode.ClientID%>').value = eventArgs.get_value();
                $find('AutoCompleteExtender_txtSAgentName').set_contextKey(eventArgs.get_value());
                GetCountryDetails(eventArgs.get_value());
            }
            else {
                document.getElementById('<%=txtSAgentNameCode.ClientID%>').value = '';

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

                    document.getElementById('<%=txtSCountry.ClientID%>').value = ''
                    document.getElementById('<%=txtSCountryCode.ClientID%>').value = '';
                    document.getElementById('<%=txtSCountry.ClientID%>').value = $(this).find("ctryname").text();

                    document.getElementById('<%=txtSCountryCode.ClientID%>').value = $(this).find("ctrycode").text();
                    document.getElementById('<%=txtSCountry.ClientID%>').setAttribute("readonly", true);
                    //   $find('AutoCompleteExtender_txtCountry').setAttribute("Enabled", false);
                });
            }
            else {
                document.getElementById('<%=txtSCountry.ClientID%>').value = ''
                document.getElementById('<%=txtSCountryCode.ClientID%>').value = '';
                document.getElementById('<%=txtSCountry.ClientID%>').removeAttribute("readonly");
                $find('AutoCompleteExtender_txtCountry').setAttribute("Enabled", true);
            }
        };
        function Countryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtSCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtSCountryCode.ClientID%>').value = '';
            }
        }

        function AutoCompleteExtender_Country_KeyUp() {
            $("#<%= txtSCountry.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtSCountry.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtSCountryCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtSCountry.ClientID %>").keyup(function (event) {
                var hiddenfieldID1 = document.getElementById('<%=txtSCountry.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtSCountryCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }
        function AutoCompleteExtender_Customer_KeyUp() {
            $("#<%= txtSAgentName.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtSAgentName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtSAgentNameCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtSAgentName.ClientID %>").keyup(function (event) {
                var hiddenfieldID1 = document.getElementById('<%=txtSAgentName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtSAgentNameCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }


        $(document).ready(function () {

            AutoCompleteExtender_Country_KeyUp();
            AutoCompleteExtender_Customer_KeyUp();

            $("#btnReset").button().click(function () {
                document.getElementById('<%=txtSubUserEmailId.ClientID%>').value = '';
            });

            $("#<%= txtSubUserEmailId.ClientID %>").bind("change", function () {
                var txtEmailId = $("#<%= txtSubUserEmailId.ClientID %>").val()

            });

            $('#dvMenus').hide();
            $("#btnViewMenu").button().click(function () {

                if ($('#btnViewMenu').val() == 'Show Menu') {
                    $('#btnViewMenu').val('Hide Menu');

                    $('#dvMenus').show(1000);
                    var txtFocus = document.getElementById('<%=txtFocus.ClientID%>');
                  //  txtFocus.focus();
                }
                else {
                    $('#btnViewMenu').val('Show Menu');
                    $('#dvMenus').hide(1000);

                }

            });
        });

    
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
        }
        function EndRequestHandlerForProgressBar() {
            HideProgess();

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
         <div class="header-user"  style="margin-top:2px;"><asp:Label ID="lblHeaderUserName" runat="server" ></asp:Label> </div>				
			<div class="header-phone"  style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-account">
					<asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
			</div>
              <div class="header-agentname" style="padding-left:105px;margin-top:2px;"><asp:Label ID="lblHeaderAgentName" runat="server" ></asp:Label> </div>
            <div class="header-lang">
            <asp:UpdatePanel runat="server"><ContentTemplate><asp:Button ID="btnLogOut"  UseSubmitBehavior="false" TabIndex="50"  OnClick="btnLogOut_Click"
                    CssClass="header-account-button" runat="server" Text="Log Out"></asp:Button>
            </ContentTemplate>
        </asp:UpdatePanel>
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
			<div id="dvCurrency" runat="server"  style="margin-top:2px;" class="header-curency">
			</div>
               <div class="header-lang" id="dvMybooking" runat="server" style="margin-right:5px;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:Button ID="btnMyBooking" 
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
			<div class="header-logo" ><a href="Home.aspx?Tab=0"><img id="imgLogo" runat="server" alt="" /></a></div>
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
    <div class="main-cont" >
        <div class="body-wrapper" >
            <div class="wrapper-padding">
                <div class="page-head">
                    <div class="page-title">
                        Sub Users<span></span></div>
                    <div class="breadcrumbs">
                        <a href="Home.aspx?Tab=0">Home</a> / <a href="#">Sub Users</a>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="sp-page" style="min-height:300px;max-height:1500px;">
                    <div style="margin-top: -15px;">
                        <div class="row-col-12">
                            <div class="row-col-12" style="background-color: white; float: left;">
                                <%--<div class="comlete-line-bottom" style="width: 100%;">
                            <div style="padding-left:15px; float: left;margin-top:-25px;">
                                <div class="row-col-12" style="padding-bottom: 30px;">
                                    <h2 class="my-account-header">
                                        Agent Lists</h2>
                                </div>
                            </div>
                        </div>--%>
                                <div>
                                    <asp:UpdatePanel ID="upnlsearchResults" runat="server">
                                        <ContentTemplate>
                                            <div style="width: 100%;">
                                                <div style="width: 97%; float: left;">
                                                    <div style="width: 100%; padding-bottom: 20px; padding-right: 0px;">
                                                        <div id="dvWarning" runat="server" style="background-color: #F2F3F4; padding-top: 16px;
                                                            padding-bottom: 16px; padding-left: 16px; text-align: center">
                                                            <asp:Label ID="lblheader" runat="server" Text="Oops, No results to show. Can you please try a different combination?"
                                                                ForeColor="#009999" Font-Bold="True">
                                                            </asp:Label></div>
                                                        <div class="subusers-label">
                                                            <div class="page-search-p">
                                                                <!-- // -->
                                                                <div style="float: right; margin-top:-20px; margin-right: -58px;">
                                                                    <input id="btnViewMenu" type="button" class="my-account-slide-btn" value="Show Menu" /></div>
                                                                <div id="dvAgentUser" runat="server">
                                                                    <div class="row-col-12" style="padding-top: 20px;">
                                                                        <div class="row-col-2" style="padding-right: 15px;">
                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                <label>
                                                                                    Agent Code</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="txtSAgentCode" TabIndex="1" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row-col-2" style="padding-right: 15px;">
                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                <label>
                                                                                    Agent Name</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="txtSAgentName" TabIndex="1" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtSAgentNameCode" runat="server" Style="display: none"></asp:TextBox>
                                                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtSAgentName" runat="server"
                                                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                                        MinimumPrefixLength="-1" ServiceMethod="GetCustomers" TargetControlID="txtSAgentName"
                                                                                        OnClientItemSelected="Customersautocompleteselected">
                                                                                    </asp:AutoCompleteExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row-col-2" style="padding-right: 15px;">
                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                <label>
                                                                                    Short Name</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="txtSShortName" TabIndex="1" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row-col-2" style="padding-right: 35px;">
                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                <label>
                                                                                    Country</label>
                                                                                <div class="input-a">
                                                                                    <asp:TextBox ID="txtSCountry" TabIndex="1" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtSCountryCode" Style="display: none" TabIndex="1" runat="server"></asp:TextBox>
                                                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCountry" runat="server" CompletionInterval="10"
                                                                                        CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                                                        DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                                                        MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtSCountry"
                                                                                        UseContextKey="true" OnClientItemSelected="Countryautocompleteselected">
                                                                                    </asp:AutoCompleteExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row-col-2" style="margin-top: 15px; padding-left: 10px;">
                                                                            <asp:Button ID="btnSFilter" class="authorize-btn" TabIndex="15" runat="server" OnClientClick="return ValidateSearch()"
                                                                                Text="Filter"></asp:Button>
                                                                        </div>
                                                                        <div class="row-col-1" style="padding-right: 5px; margin-top: 15px; margin-left: -5px;">
                                                                            <input id="btnSReset" type="button" class="srch-btn-home" value="Reset" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="clear" style="padding-bottom: 10px;">
                                                                    </div>
                                                                    <div class="comlete-line-bottom">
                                                                        <div class="row-col-12" style="padding-top: 5px;">
                                                                            <h3 class="sub-user-header">
                                                                                Agent Lists</h3>
                                                                        </div>
                                                                    </div>
                                                                    <div class="comlete-line-bottom">
                                                                        <div class="row-col-12" style="padding-top: 5px;">
                                                                            <asp:GridView ID="gvAgentsList" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                                                PageSize="7" GridLines="Horizontal" AllowPaging="True">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Agent Code">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblAgentCode" runat="server" Text='<%# Bind("agentcode") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Agent Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblAgentName" runat="server" Text='<%# Bind("agentname") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Short Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label17" runat="server" Text='<%# Bind("shortname") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Country Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("ctryname") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="City Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("cityname") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Sector Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label20" runat="server" Text='<%# Bind("sectorname") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Sub Users">
                                                                                        <ItemTemplate>
                                                                                            <div style="padding: 5px;">
                                                                                                <asp:LinkButton ID="lbShowSubUsers" CssClass="my-account-btn" runat="server" OnClick="lbShowSubUsers_Click">Show</asp:LinkButton></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                      <asp:TemplateField Visible="false" HeaderText="Assign Rights">
                                                                                    <ItemTemplate>
                                                                                        <div style="padding: 5px;">
                                                                                            <asp:LinkButton ID="lbROAssignRights" CssClass="my-account-btn" runat="server" OnClick="lbROAssignRights_Click">Assign Rights</asp:LinkButton></div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                                <PagerStyle CssClass="mygrid-pager" HorizontalAlign="Left" />
                                                                                <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                                                <AlternatingRowStyle CssClass="mygrid-rows-alternative" HorizontalAlign="Left" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                       <asp:HiddenField ID="hdSubUserPopup" runat="server" />
                                                                            <asp:ModalPopupExtender ID="mpUserRights" runat="server" PopupControlID="pnlUserRights"  BackgroundCssClass="roomtype-modalBackground"
                                                                           CancelControlID="aUserRightsClose"     TargetControlID="hdUserRights">
                                                                            </asp:ModalPopupExtender>
                                                                            <asp:HiddenField ID="hdUserRights" runat="server" />
                                                                <div id="dvSubUser" runat="server" class="row-col-12" style="padding-top: 15px;">
                                                                    <div class="row-col-12" style="padding-top: 5px;">
                                                                        <div class="comlete-line-bottom">
                                                                            <h3 class="sub-user-header" style="margin-top: -5px;">
                                                                                Sub Users</h3>
                                                                        </div>
                                                                        <div class="clear">
                                                                        </div>
                                                                        <div style="padding-bottom: 20px; padding-top: 10px;">
                                                                            <asp:Button ID="btnAddSubUsers" class="authorize-btn" TabIndex="15" runat="server"
                                                                                Width="150px" Text="Add Sub Users"></asp:Button>
                                                                            <asp:ModalPopupExtender ID="mpSubUsers" runat="server" BackgroundCssClass="roomtype-modalBackground"
                                                                                CancelControlID="aSubUserPopupClose" EnableViewState="true" PopupControlID="pnlSubUserPopup"
                                                                                TargetControlID="hdSubUserPopup">
                                                                            </asp:ModalPopupExtender>
                                                                     
                                                                        </div>
                                                                        <div class="clear" style="padding-top: 10px;">
                                                                        </div>
                                                                        <asp:GridView ID="gvSubUsers" runat="server" AutoGenerateColumns="False" CssClass="mygrid"
                                                                            PageSize="7" GridLines="Horizontal" AllowPaging="True">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Agent Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAgentcode_" runat="server" Text='<%# Bind("agentcode") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Agent Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAgentName" runat="server" Text='<%# Bind("agentname") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sub User Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSubUserCode" runat="server" Text='<%# Bind("AGENTSUBCODE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sub User Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSubUserAgentName" runat="server" Text='<%# Bind("SubUserName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false" HeaderText="Sub User Email">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSubUserEmail" runat="server" Text='<%# Bind("SubUserEmail") %>'></asp:Label>
                                                                                        <asp:Label ID="lblSubUserActive" runat="server" Text='<%# Bind("active") %>'></asp:Label>
                                                                                        <asp:Label ID="lblSubUserPassword" runat="server" Text='<%# Bind("pwd") %>'></asp:Label>
                                                                                        <asp:Label ID="lblSubId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Address">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSubUserAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Telephone">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSubUserTel" runat="server" Text='<%# Bind("Telephone") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false" HeaderText="Fax">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSubUserFax" runat="server" Text='<%# Bind("Fax") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Active">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label83" runat="server" Text='<%# Bind("isActive") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Email Confirm">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEmailConfirm" runat="server" Text='<%# Bind("IsEmailConfirm") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Add Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label84" runat="server" Text='<%# Bind("adddate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false" HeaderText="Add User">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label85" runat="server" Text='<%# Bind("adduser") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false" HeaderText="Modified Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label86" runat="server" Text='<%# Bind("moddate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false" HeaderText="Modified User">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label87" runat="server" Text='<%# Bind("moduser") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="View">
                                                                                    <ItemTemplate>
                                                                                        <div style="padding: 5px;">
                                                                                            <asp:LinkButton ID="lbView" CssClass="my-account-btn" runat="server" OnClick="lbView_Click">View</asp:LinkButton></div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Edit">
                                                                                    <ItemTemplate>
                                                                                        <div style="padding: 5px;">
                                                                                            <asp:LinkButton ID="lbEdit" CssClass="my-account-btn" runat="server" OnClick="lbEdit_Click">Edit</asp:LinkButton></div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false" HeaderText="Delete">
                                                                                    <ItemTemplate>
                                                                                        <div style="padding: 5px;">
                                                                                            <asp:LinkButton ID="lbDelete" CssClass="my-account-btn" runat="server" OnClick="lbDelete_Click">Delete</asp:LinkButton></div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Assign Rights" ControlStyle-Width="80px" HeaderStyle-Width="80px" FooterStyle-VerticalAlign="Top">
                                                                                    <ItemTemplate>
                                                                                        <div style="padding: 5px;vertical-align:top;margin-top:-20px;" >
                                                                                            <asp:LinkButton ID="lbAssignRights" CssClass="my-account-btn" runat="server" OnClick="lbAssignRights_Click">Assign Rights</asp:LinkButton></div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                                            <PagerStyle CssClass="mygrid-pager" HorizontalAlign="Left" />
                                                                            <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                                            <AlternatingRowStyle CssClass="mygrid-rows-alternative" HorizontalAlign="Left" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                    <asp:Panel runat="server" ID="pnlSubUserPopup">
                                                                        <div class="dvSubUserPopup-popup">
                                                                            <div id="Div4">
                                                                                <div class="subuser-popup-title">
                                                                                    <div style="padding-top: 14px; float: left; padding-left: 50px;">
                                                                                        <asp:Label ID="lblPopupTittle" Text="Add Sub Users" runat="server"></asp:Label></div>
                                                                                    <div style="float: right;">
                                                                                        <a id="aSubUserPopupClose" href="#" class="roomtype-popup-close"></a>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="roomtype-popup-description">
                                                                                    <div id="map" style="width: 450px; height: 350px; background: #FBFBFB; margin-top: 0px;
                                                                                        padding-left: 50px;">
                                                                                        <div class="row-col-9" style="padding-right: 15px; width: 87%;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <label>
                                                                                                    Agent Name<span style="color: #ff8f35; font-size: large;">*</span></label>
                                                                                                <div class="input-a">
                                                                                                    <asp:TextBox ID="txtAgentName" ReadOnly="true" TabIndex="1" runat="server"></asp:TextBox>
                                                                                                    <asp:Label ID="lblPopupAgentCode" Style="display: none;" TabIndex="1" runat="server"></asp:Label>
                                                                                                    <asp:Label ID="lblPopupSubId" Style="display: none;" TabIndex="1" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="clear">
                                                                                        </div>
                                                                                        <div class="row-col-5" style="padding-right: 15px; padding-top: 15px;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <label>
                                                                                                    Email Id<span style="color: #ff8f35; font-size: large;">*</span></label>
                                                                                                <div class="input-a">
                                                                                                    <asp:TextBox ID="txtSubUserEmailId" TabIndex="1" runat="server"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row-col-5" style="padding-right: 15px; padding-top: 15px;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <label>
                                                                                                    Name of Sub User<span style="color: #ff8f35; font-size: large;">*</span></label>
                                                                                                <div class="input-a">
                                                                                                    <asp:TextBox ID="txtSubUserName" TabIndex="1" runat="server"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row-col-5" style="padding-right: 15px; padding-top: 15px;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <label>
                                                                                                    Password<span style="color: #ff8f35; font-size: large;">*</span></label>
                                                                                                <div class="input-a">
                                                                                                    <asp:TextBox ID="txtpassword" TextMode="Password" TabIndex="1" AutoComplete="off" runat="server"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row-col-5" style="padding-right: 15px; padding-top: 15px;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <label>
                                                                                                    Confirm Password<span style="color: #ff8f35; font-size: large;">*</span></label>
                                                                                                <div class="input-a">
                                                                                                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" TabIndex="1" AutoComplete="off" runat="server"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row-col-9" style="padding-right: 15px; width: 87%; padding-top: 15px;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <label>
                                                                                                    Address</label>
                                                                                                <div class="input-a">
                                                                                                    <asp:TextBox ID="txtAddress" TabIndex="1" runat="server"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row-col-5" style="padding-right: 15px; padding-top: 15px;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <label>
                                                                                                    Telephone</label>
                                                                                                <div class="input-a">
                                                                                                    <asp:TextBox ID="txtTelephone" TabIndex="1" runat="server"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row-col-5" style="padding-right: 15px; padding-top: 15px;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <label>
                                                                                                    Fax</label>
                                                                                                <div class="input-a">
                                                                                                    <asp:TextBox ID="txtFax" TabIndex="1" runat="server"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row-col-3" style="padding-right: 15px; padding-top: 15px;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <div class="checkbox">
                                                                                                    <asp:CheckBox ID="chkActive" Text=" Active" runat="server" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div id="dvEmailConfirm" runat="server" class="row-col-4" style="padding-right: 15px;
                                                                                            padding-top: 15px;">
                                                                                            <div class="srch-tab-line no-margin-bottom">
                                                                                                <div class="checkbox">
                                                                                                    <asp:CheckBox ID="chkEmailConfirm" Text=" Email Confirm" runat="server" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="clear">
                                                                                        </div>
                                                                                        <div class="row-col-12" style="padding-right: 15px; margin-top: 15px;">
                                                                                            <div class="row-col-2" style="padding-right: 15px; margin-top: 15px;">
                                                                                                <asp:Button ID="btnSubUserSave" class="authorize-btn" TabIndex="15" runat="server"
                                                                                                    OnClientClick="return ValidateSave()" Text="Save"></asp:Button>
                                                                                            </div>
                                                                                            <div class="row-col-2" style="padding-right: 15px; margin-top: 15px; margin-left: 55px;">
                                                                                                <input id="btnReset" type="button" runat="server" class="srch-btn-home" value="Reset" />
                                                                                                <asp:HiddenField ID="hdOPMode" runat="server" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                    </asp:Panel>

                                                               
                                                                </div>
                                                                <div class="row-col-3" id="dvMenus" style="z-index: 999999; position: absolute; top:72px;
                                                                    float: right; right:1px; bottom: 10px; padding: 0; background-color: #141d1e;
                                                                    height: 420px;">
                                                                    <div class="row-col-12">
                                                                        <asp:DataList ID="dlSubMenuHeader" OnItemDataBound="dlSubMenuHeader_ItemDataBound"
                                                                            runat="server">
                                                                            <ItemTemplate>
                                                                                <div class="reasons-i">
                                                                                    <div class="h-reasons">
                                                                                        <div class="h-liked-lbl">
                                                                                            <asp:Label ID="lblMenuHeader" Text='<%# Bind("parentname") %>' runat="server"></asp:Label>
                                                                                        </div>
                                                                                        <asp:DataList ID="dlSubMenu" OnItemDataBound="dlSubMenu_ItemDataBound" runat="server">
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
                                                                                                                        <a id="A1" runat="server" class="reasons-i-lbl" style="text-decoration: none;" href='<%# Bind("pagename") %>'>
                                                                                                                            <asp:Label ID="lblMenuDesc" Text='<%# Bind("menudesc") %>' runat="server"></asp:Label>
                                                                                                                            <asp:Label ID="lblPageName" Style="display: none;" Text='<%# Bind("pagename") %>'
                                                                                                                                runat="server"></asp:Label>
                                                                                                                        </a>
                                                                                                                    </div>
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
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox ID="txtFocus" Style="border: none;" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdPopup" runat="server" />
                                              <asp:HiddenField ID="hdPopupUserType" runat="server" />
                                              <asp:Panel runat="server" ID="pnlUserRights" style="display:none;" >
                                       
                                        <div class="dvSubUserPopup-popup-user-rights" style="overflow:scroll">
                                            <div>
                                                <div class="subuser-popup-title">
                                                    <div style="padding-top: 14px; float: left; padding-left: 50px;">
                                                        <asp:Label ID="lbluserRirgtheading" Text="Assign Sub User Rights" runat="server"></asp:Label></div>
                                                    <div style="float: right;">
                                                        <a id="aUserRightsClose" href="#" class="roomtype-popup-close"></a>
                                                    </div>
                                                </div>
                                                <div class="roomtype-popup-description" >
                                                    <div id="Div2" style="width: 450px; height:450px; background: #FBFBFB; margin-top: 0px;
                                                        padding-left: 50px; overflow:visible">
                                                        <div class="row-col-12" style="padding-right: 15px;">
                                                        <asp:Label id="lblARAgentName"  Font-Bold="true"  runat="server"></asp:Label>
                                                        
                                                               <asp:Label id="lblARAgentSubUser" Font-Bold="true" runat="server"></asp:Label>
                                                        </div>
                                                        <div style="display:none;">
                                                             <asp:Label id="lblARAgentCode" runat="server"></asp:Label>
                                                              <asp:Label id="lblARAgentSubUserCode" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="row-col-12" style="padding-right: 15px;">
                                                            <div class="srch-tab-line no-margin-bottom">
                                                                <label class="highlight">
                                                                    Main Menus</label>
                                                            </div>
                                                        </div>
                                                  
                                                    <div class="clear">
                                                    </div>
                                                    <div class="row-col-12" style="max-height:250px;overflow:auto;padding-bottom:10px;">
                                                        <asp:GridView ID="gvMainMenu" runat="server" AutoGenerateColumns="False" CssClass="mygrid" Width="100%"
                                                           GridLines="Horizontal" >
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Menu Id" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmenuId"  Text='<%# Bind("menuid") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Parent Menu">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblParentName"  Text='<%# Bind("parentname") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Parent Menu">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMenuDesc"  Text='<%# Bind("menudesc") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Active">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkActive" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                            <PagerStyle CssClass="mygrid-pager" HorizontalAlign="Left" />
                                                            <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                            <AlternatingRowStyle CssClass="mygrid-rows-alternative" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="row-col-12" style="padding-right: 15px;">
                                                        <div class="srch-tab-line no-margin-bottom">
                                                            <label class="highlight">
                                                                Sub Menus</label>
                                                        </div>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                    <div class="row-col-12"  style="max-height:250px;overflow:auto;">
                                                        <asp:GridView ID="gvSubMenu" runat="server" AutoGenerateColumns="False" CssClass="mygrid"  Width="100%"
                                                            GridLines="Horizontal" AllowPaging="True">
                                                                   <Columns>
                                                                <asp:TemplateField  Visible="false" HeaderText="Menu Id">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmenuId"  Text='<%# Bind("menuid") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Parent Menu">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblParentName"  Text='<%# Bind("parentname") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Parent Menu">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMenuDesc"  Text='<%# Bind("menudesc") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Active">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkActive" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="mygrid-header" HorizontalAlign="Left" />
                                                            <PagerStyle CssClass="mygrid-pager" HorizontalAlign="Left" />
                                                            <RowStyle CssClass="mygrid-rows" HorizontalAlign="Left" />
                                                            <AlternatingRowStyle CssClass="mygrid-rows-alternative" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="clear">
                                                  
                                                    </div>
                                                    <div class="row-col-12" style="padding-top: 10px; margin-top: 15px;height:50px;padding-left:40%;">
                                                      <asp:Button ID="btnUserRightSave" class="authorize-btn" TabIndex="15" runat="server"
                                                                                                 Text="Save"></asp:Button>
                                                    </div>  </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                  
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
		<div  id="dvMagnifyingMemories" runat="server"  class="section-middle">
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
    <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
    <asp:HiddenField ID="hdWhiteLabel" runat="server" />
      <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
    </form>
</body>
</html>
