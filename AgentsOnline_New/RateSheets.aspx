﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RateSheets.aspx.vb" Inherits="RateSheets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mohd Al Humaidi Computer  Tourism Services L.L.C.</title>
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta charset="utf-8" />
    <link rel="icon" href="favicon.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="css/jquery-ui.css" />
    <link rel="stylesheet" href="css/owl.carousel.css" />
    <link rel="stylesheet" href="css/idangerous.swiper.css" />
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
    <!-- // scripts // -->
    <script type="text/javascript" src="js/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="js/idangerous.swiper.js"></script>
    <script type="text/javascript" src="js/slideInit.js"></script>
    <script type="text/javascript" src="js/jqeury.appear.js"></script>
    <script type="text/javascript" src="js/owl.carousel.min.js"></script>
    <script type="text/javascript" src="js/bxSlider.js"></script>
    <script type="text/javascript" src="js/custom.select.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/twitterfeed.js"></script>
    <script type="text/javascript" src="js/script.js"></script>
    <link rel="stylesheet" type="text/css" href="css/dialog_box.css" />
    <script type="text/javascript" src="js/dialog_box.js"></script>
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

            //Check if receive cookie from server by second
            $('#btnSearch').on('click', function () {
                intervalProgress = setInterval("$.checkDownloadFileCompletely()", 1000);
            });

        });
        
    </script>
    <script type="text/javascript">
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
        function ValidateSearch() {
            if (document.getElementById('<%=txtHotelCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select Hotel Name.', 'warning');
                return false;
            }
            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtCustomerCode.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please Select Customer.', 'warning');
                return false;
            }
            if (document.getElementById('<%=txtCountryCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select source country.', 'warning');
                return false;
            }
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select check-in date.', 'warning');
                return false;
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any check-out date.', 'warning');
                return false;
            }
            var fromDate = document.getElementById('<%=txtFromDate.ClientID%>').value
            var toDate = document.getElementById('<%=txtToDate.ClientID%>').value
            fromDate = fromDate.trim();
            toDate = toDate.trim();
            if (fromDate.length != 10 || toDate.length != 10) {
                showDialog('Alert Message', 'Invalid date format.', 'warning');
                return false;
            }
            var dp = fromDate.split("/");
            var dp1 = toDate.split("/");
            if (dp.length != 3 || dp1.length != 3) {
                showDialog('Alert Message', 'Invalid date format.', 'warning');
                return false;
            }

            var newChkInDt = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);
            var newChkOutDt = new Date(dp1[2] + "/" + dp1[1] + "/" + dp1[0]);

            newChkInDt = getFormatedDate(newChkInDt);
            newChkOutDt = getFormatedDate(newChkOutDt);

            newChkInDt = new Date(newChkInDt);
            newChkOutDt = new Date(newChkOutDt);
            if (newChkInDt > newChkOutDt) {
                alert("Todate date should not be greater than From date");
                return false;
            }

             //     ShowProgess();

            return true;
        }

        $.checkDownloadFileCompletely = function () {
            var cookieValue = $.getCookie('AgentDownloaded');
            console.log(cookieValue + "---> Cookie Value;");
            if (cookieValue == 'True') {
                $.removeCookie('AgentDownloaded');
                clearInterval(intervalProgress);
                HideProgess();
            }
        }


        /* get cookie from document.cookie */
        $.getCookie = function (cookieName) {
            var cookieValue = document.cookie;
            var c_start = cookieValue.indexOf(" " + cookieName + "=");
            if (c_start == -1) {
                c_start = cookieValue.indexOf(cookieName + "=");
            }
            if (c_start == -1) {
                cookieValue = null;
            }
            else {
                c_start = cookieValue.indexOf("=", c_start) + 1;
                var c_end = cookieValue.indexOf(";", c_start);
                if (c_end == -1) {
                    c_end = cookieValue.length;
                }
                cookieValue = unescape(cookieValue.substring(c_start, c_end));
            }
            return cookieValue;
        }

        /* Remove cookie in document.cookie */
        $.removeCookie = function (cookieName) {
            var cookies = document.cookie.split(";");

            for (var i = 0; i < cookies.length; i++) {
                var cookie = cookies[i];
                var eqPos = cookie.indexOf("=");
                var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                if (name == cookieName) {
                    document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/";
                }
            }
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

        function HotelNameautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtHotelCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtHotelCode.ClientID%>').value = '';
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
                url: "Home.aspx/GetCountryDetails",
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

        function Countryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtCountryCode.ClientID%>').value = '';
            }
        }
        
    </script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequestUserControl);
        prm.add_endRequest(EndRequestUserControl);

        function InitializeRequestUserControl(sender, args) {

        }
        function EndRequestUserControl(sender, args) {
            AutoCompleteExtender_HotelNameKeyUp();
            AutoCompleteExtender_Customer_KeyUp();
            AutoCompleteExtender_Country_KeyUp();

            $("#<%= txtFromDate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=txtFromDate.ClientID%>').value;
                var dp = d.split("/");
                var dateOut = new Date(dp[2], dp[1], dp[0]);
                var currentMonth = dateOut.getMonth() - 1;
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-check-out").datepicker("destroy");
                $(".date-inpt-check-out").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });
            });


        }
        function RefreshContent() {
            //alert('k');
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandlerForProgressBar);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandlerForProgressBar);
            //alert('k');
        }






        function BeginRequestHandlerForProgressBar() {
            //            alert('test');
            ShowProgess();

        }
        function EndRequestHandlerForProgressBar() {
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

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {

            $("#<%= txtFromDate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=txtFromDate.ClientID%>').value;
                var dp = d.split("/");
                var dateOut = new Date(dp[2], dp[1], dp[0]);
                var currentMonth = dateOut.getMonth() - 1;
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-check-out").datepicker("destroy");
                $(".date-inpt-check-out").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });
            });

        });

    </script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {

            AutoCompleteExtender_HotelNameKeyUp();
            AutoCompleteExtender_Customer_KeyUp();
            AutoCompleteExtender_Country_KeyUp();

            $("#<%= txtFromDate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=txtFromDate.ClientID%>').value;
                var dp = d.split("/");
                var dateOut = new Date(dp[2], dp[1], dp[0]);
                var currentMonth = dateOut.getMonth() - 1;
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-check-out").datepicker("destroy");
                $(".date-inpt-check-out").datepicker({
                    minDate: new Date(currentYear, currentMonth, currentDate)
                });
            });


            $("#btnReset").button().click(function () {

                document.getElementById('<%=txtHotelName.ClientID%>').value = '';
                document.getElementById('<%=txtHotelCode.ClientID%>').value = '';

                if (document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') {
                    document.getElementById('<%=txtCustomer.ClientID%>').value = ''
                    document.getElementById('<%=txtCustomerCode.ClientID%>').value = '';

                    document.getElementById('<%=txtCountry.ClientID%>').value = ''
                    document.getElementById('<%=txtCountryCode.ClientID%>').value = '';
                }

                document.getElementById('<%=txtFromDate.ClientID%>').value = '';
                document.getElementById('<%=txtToDate.ClientID%>').value = '';

            });

        });

        function AutoCompleteExtender_HotelNameKeyUp() {
            $("#<%= txtHotelName.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtHotelName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
            $("#<%= txtHotelName.ClientID %>").keyup("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtHotelName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelCode.ClientID%>');
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
        
    </script>
</head>
<body onload="RefreshContent()" >
    <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server" enablepagemethods="true">
    </asp:scriptmanager>
    <header id="top">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
	<div class="header-a">
		<div class="wrapper-padding">	
        <div class="header-user"><asp:Label ID="lblHeaderUserName"  runat="server" ></asp:Label> </div>		
			<div class="header-phone"  id="dvlblHeaderAgentName" runat="server"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-agentname">
				<%--<asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>--%>
            <asp:LinkButton ID="btnMyAccount"  style="    padding: 0px 10px 0px 0px;" UseSubmitBehavior="False" OnClick="btnMyAccount_Click"
                    CssClass="header-account-button" runat="server" Text="Account" causesvalidation="true"></asp:LinkButton>
			</div>
           <div class="header-agentname"><asp:Label ID="lblHeaderAgentName" style="    padding: 0px 10px 0px 0px;" runat="server" ></asp:Label> </div>
           
               <div class="header-lang">
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><asp:Button ID="btnLogOut"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Log Out"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>--%>
            <asp:LinkButton ID="btnLogOut"   UseSubmitBehavior="False" OnClick="btnLogOut_Click"
                    CssClass="header-account-button" runat="server" Text="Log Out" 
                    meta:resourcekey="btnLogOutResource1"></asp:LinkButton>
			<%--	<a href="#">Log Out</a>--%>
			</div>
	
            

			<div id="dvFlag" runat="server" class="header-lang" style="padding-top:5px;" >
				<a href="#"><img id="imgLang" runat="server" alt="" src="img/en.gif" /></a>
			</div>
			<div id="dvCurrency" runat="server" class="header-curency">
			</div>
                
              <div class="header-lang" id="dvMybooking" runat="server" style="padding-right:25px;margin-right:5px;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate><asp:Button ID="btnMyBooking"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="MY BOOKING"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
           
			</div>          
			<div class="clear"></div>
		</div>
	</div>
    </ContentTemplate></asp:UpdatePanel>
	<div class="header-b">
		<!-- // mobile menu // -->
			<div class="mobile-menu" id="dvMobmenu" runat="server" >
				<asp:Label ID="lblRecentViewed" runat="server" Text=""></asp:Label>
              			</div>
		<!-- \\ mobile menu \\ -->
			
		<div class="wrapper-padding">
            	<div class="clear"></div>
			<div class="header-logo"><a href="Home.aspx"><img id="imgLogo" runat="server" alt="" /></a></div>
			<div class="header-right">
				<div class="hdr-srch"  style="display:none;">
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
    <div class="main-cont">
        <div class="body-wrapper" style="padding-top: 150px; padding-bottom: 50px;">
            <div class="wrapper-padding-full">
                <div class="page-head">
                    <div class="page-title">
                        Rate Sheets<span></span></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="page-head">
                    <div class="page-search-content-search" style="margin-top: -10px;">
                        <div class="search-tab-content" style="min-height: 300px; max-height: 370px; padding-left: 55px;
                            padding-top: 15px;">
                            <asp:updatepanel id="upnlSearch" runat="server" >
                                <contenttemplate>                         
                                   <div class="page-search-p">
                         <!-- // -->                         
                             <div class="row-col-5" style="padding-right: 15px;">
                                 <div class="srch-tab-line no-margin-bottom">
                                    <label>
                                        Hotel Name</label>
                                    <div class="input-a">
                                        <asp:TextBox ID="txtHotelName" runat="server" placeholder="--"></asp:TextBox>
                                        <asp:TextBox ID="txtHotelCode" runat="server"  style="display:none;"  ></asp:TextBox>                                        
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtHotelName" runat="server" CompletionInterval="10"
                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                            DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="false"
                                            MinimumPrefixLength="-1" ServiceMethod="GetHotelName" TargetControlID="txtHotelName"
                                            OnClientItemSelected="HotelNameautocompleteselected">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                             </div>

                             <div class="row-col-1">
                             &nbsp;
                             </div>
                             <div class="row-col-5" style="padding-right: 15px;">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Agent Name</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtCustomer" runat="server" placeholder="--"></asp:TextBox>
                                            <asp:TextBox ID="txtCustomerCode" runat="server" Style="display: none"></asp:TextBox>
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
                            <div class="clear">
                            </div>
                            
                            
                            <div class="row-col-5" style="padding-right: 15px; padding-top:20px">
                                    <div class="srch-tab-line no-margin-bottom">
                                        <label>
                                            Country Name</label>
                                        <div class="input-a">
                                            <asp:TextBox ID="txtCountry" runat="server" placeholder="--"></asp:TextBox>
                                            <asp:TextBox ID="txtCountryCode" runat="server" Style="display: none"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCountry" runat="server" CompletionInterval="10"
                                                CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                DelimiterCharacters="" EnableCaching="false" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtCountry"
                                                UseContextKey="true" OnClientItemSelected="Countryautocompleteselected">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                            </div>
                            <div class="row-col-1" style="padding-top:20px;">
                             &nbsp;
                             </div>
                             <div class="row-col-2" style="padding-right: 15px; padding-top:20px">
                                    <div class="srch-tab-line no-margin-bottom">
                                            <div style="width: 100%">
                                                <label>
                                                    From Date</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtFromDate" class="date-inpt-check-in" placeholder="dd/mm/yyyy" AutoComplete="off"
                                                        runat="server"></asp:TextBox>
                                                    <span class="date-icon"></span>                                                    
                                                </div>
                                            </div>
                                        </div>
                            </div>

                            <div class="row-col-2" style="padding-left: 45px; padding-top:20px">
                                         <div class="srch-tab-line no-margin-bottom">
                                            <div style="width: 100%">
                                                <label>
                                                    To Date</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtToDate" class="date-inpt-check-out" placeholder="dd/mm/yyyy"  AutoComplete="off"
                                                        runat="server"></asp:TextBox>                                                           
                                                    <span class="date-icon"></span>
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div>

                             <div class="clear">
                             </div>     
                                 <div class="row-col-2" >   
                                <div style="width: 100%;display:none;">     
                                <asp:CheckBox runat="server" ID="chkAlWithText" Text="Detailed AI Meal Plans" />                 
                               <%-- <input id="Checkbox1" type="checkbox" runat="server" style="margin-top:10px" checked="checked"  />
                                <label style="padding-left: 25px; margin-top:-15px" id="Label1" runat="server">Group Columns by Dates</label>--%>
                                </div>
                             </div>  
                             <div class="row-col-2" >   
                                <div style="width: 100%">                      
                                <input id="chkcolinExcel" type="checkbox" runat="server" style="margin-top:10px" checked="checked"  />
                                <label style="padding-left: 25px; margin-top:-15px" id="lblgroup" runat="server">Group Columns by Dates</label>
                                </div>
                             </div>    
                             <div class="clear">
                            </div>                    
                             <div class="row-col-1" style="padding-right:40px; padding-top:20px">
                                <asp:Button ID="btnSearch" class="authorize-btn"  OnClientClick="return ValidateSearch()" runat="server" Text="Load Report"></asp:Button>                                       
                             </div>
                             <div class="row-col-1">                             
                             </div>
                             <div class="row-col-2" style="padding-left:40px; padding-top:20px; " >
                             <input  id="btnReset"  type="button"  class="srch-btn-home"  value="Reset"/>
                             </div>                           
                            </div>
                            
                             </contenttemplate>
                             <Triggers>                                                                        
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                </Triggers>
                            </asp:updatepanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>  
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
		<div class="footer-social">
					</div>
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
    <asp:modalpopupextender id="ModalPopupDays" runat="server" behaviorid="ModalPopupDays"
        targetcontrolid="btnInvisibleGuest" cancelcontrolid="btnClose" popupcontrolid="Loading1"
        backgroundcssclass="ModalPopupForPageLoading">
    </asp:modalpopupextender>
    <input id="btnInvisibleGuest" runat="server" type="button" value="Cancel" style="display: none" />
    <input id="btnClose" type="button" value="Cancel" style="display: none" />
    <asp:hiddenfield id="hdLoginType" runat="server" />
    <asp:hiddenfield id="hdAbsoluteUrl" runat="server" />
     <asp:HiddenField ID="hdWhiteLabel" runat="server" />
       <asp:Button ID="btnConfirmHome" Width="170px"  style="display:none;"  runat="server" Text="ConfirmHome" />
   <asp:Button ID="btnHidePopup" Width="170px"  style="display:none;"  runat="server" Text="btnHidePopup" />
    </form>
</body>
</html>
