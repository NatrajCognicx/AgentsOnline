﻿<%--
                    ** =============================================================== **
                    ** =============================================================== **
                    ** =============================================================== **
                    ** ==                                                           == **
                    ** ==    Project Name                : Agents Online            == **
                    ** ==    Client                      : Royal Park Tourism       == **
                    ** ==    Designed and Developed by   : Abin Paul                == **
                    ** ==                                                           == **
                    ** =============================================================== **
                    ** =============================================================== **
                    ** =============================================================== **
--%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <%--   <title>Mohd Al Humaidi Computer  Tourism Services L.L.C.</title>--%>
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta charset="utf-8" />
    <link rel="icon" href="favicon.png" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="css/AgentsOnlineStyles.css" />
    <link rel="stylesheet" href="css/jquery-ui.css" />
    <link rel="stylesheet" href="css/owl.carousel.css" />
    <link rel="stylesheet" href="css/idangerous.swiper.css" />
    <link rel='stylesheet' type='text/css' href='css/style.css' />
    <link id="lnkCSS" rel='stylesheet' type='text/css' href='css/style-common.css' />
    <link href="css/ValidationEngine.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.11.3.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="css/dialog_box.css" />

<%--   <link rel="stylesheet" href="css/Raleway.css" />
<link rel="stylesheet" href="css/Raleway.css" />
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
        });    
    </script>
    <script type="text/javascript">

        function fnPopupShow() {
            var hdpop = document.getElementById('hdPopup')
            if (hdpop.value == 'Y') {
                $('.overlay-popup').fadeIn(function () {
                    $('.autorize-popup-popup').eq('0').css('display', 'block');
                    $('.autorize-popup-popup').animate({ top: '50%' }, 0).find('input:text').eq('0').focus();

                });
                return false;
            }
        }

        function fnTabShow() {
            var hdpop = document.getElementById('hdTab')
            if (hdpop.value == '1') {
                $('.autorize-tabs a').removeClass('current').eq(hdpop.value).addClass('current');
                $('.autorize-tab-content').hide().eq(hdpop.value).fadeIn().find('input:text').eq('0').focus();
            }
            else if (hdpop.value == '0') {
                $('.autorize-tabs a').removeClass('current').eq(hdpop.value).addClass('current');
                $('.autorize-tab-content').hide().eq(hdpop.value).fadeIn().find('input:text').eq('0').focus();
            }
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function isPhoneNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 43 && charCode != 45 && charCode != 32 && charCode != 44) {
                return false;
            }
            return true;
        }
        function CountryAutoCompleteExtenderKeyUp() {
            $("#<%= txtRegCountry.ClientID %>").bind("change", function () {
                document.getElementById('<%=txtCountryCode.ClientID%>').value = '';
            });
        }
        function CityAutoCompleteExtenderKeyUp() {
            $("#<%= txtRegCity.ClientID %>").bind("change", function () {
                document.getElementById('<%=txtCityCode.ClientID%>').value = '';
            });
        }

      
        

        function Countryautocompleteselected(source, eventArgs) {

            var hiddenfieldID = source.get_id().replace("CountryAutoCompleteExtender", "txtCountryCode");
            $get(hiddenfieldID).value = eventArgs.get_value();
            //  alert(eventArgs.get_value());
        }
        function Cityautocompleteselected(source, eventArgs) {

            var hiddenfieldID = source.get_id().replace("CityAutoCompleteExtender", "txtCityCode");
            $get(hiddenfieldID).value = eventArgs.get_value();
            //  alert(eventArgs.get_value());
        }
    </script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestUserControl);
        function EndRequestUserControl(sender, args) {
            CountryAutoCompleteExtenderKeyUp();
            CountryAutoCompleteExtenderKeyUp();

        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#form1").validationEngine('attach', { promptPosition: "topRight", scroll: false });
            $('#ex4').scroll(function () {
                $("#form1").validationEngine("hideAll");
            });
            $('#ibCaptchaRefresh').click(function () {
                // $("#form1").validationEngine("hideAll");
                $("#form1").validationEngine('detach');

            });
            $('#btnRegistration').click(function () {

                $("#form1").validationEngine('attach', { promptPosition: "topRight", scroll: false });
            });
            $('#chkTermsAndConditions').click(function () {
                document.getElementById('<%=chkTermsAndConditions.ClientID%>').checked = false;
                var mpTermsAndConditions = $find("mpTermsAndConditions");
                mpTermsAndConditions.show();
            });
            $('#bntAgree').click(function () {
                document.getElementById('<%=chkTermsAndConditions.ClientID%>').checked = true;
                var mpTermsAndConditions = $find("mpTermsAndConditions");
                mpTermsAndConditions.hide();
            });
        });
    </script>
    <script type="text/javascript">
        function DateFormat(field, rules, i, options) {
            var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            if (!regex.test(field.val())) {
                return "Please enter date in dd/MM/yyyy format."
            }
        }
    </script>
</head>
<body style="background: rgba(20,29,30,0.19);">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <header id="top">
	<div class="header-a">
		<div class="wrapper-padding">			
			<div class="header-phone" style="display:none"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
	
			<div class="header-social" style="display:none;">
				<a href="#" class="social-twitter"></a>
				<a href="#" class="social-facebook"></a>
				<a href="#" class="social-vimeo"></a>
				<a href="#" class="social-pinterest"></a>
				<a href="#" class="social-instagram"></a>
			</div>

	
			<div class="clear"></div>
		</div>
	</div>
	<div class="header-b">
				
		<div class="wrapper-padding">
			<div class="header-logo-new"><a href="#"><img id="imgLogo"  runat="server" alt="" src="" /></a></div>
			<div class="header-right">
					
			
			</div>


           <%-- <div class="header-right" style="min-width:500px">
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
	
				<a href="#" class="menu-btn"></a>
                <div style="min-width:300px"> <a href='#'><img id='lcoloumbuslogo' alt='' src='img/LoginLogoSmall.png' /></a></div>
			</div>--%>

			<div class="clear"></div>
		</div>
	</div>	
</header>
        <!-- main-cont -->
        <div>
            <div class="body-padding">
                <div class="mp-slider">
                    <div>
                        <div style="padding-top: 15px;">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-6">
                                <!-- // authorize // -->
                                <div class="col-md-12">
                                    <div class="overlay">
                                    </div>
                                    <div class="autorize-popup">
                                        <div class="autorize-tabs">
                                            <a id="aAgentLogin" runat="server" href="#" class="autorize-tab-a current" >Agent Login</a>
                                             <a id="aROLogin"  runat="server" href="#" class="autorize-tab-b" >RO Login</a> 
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <section class="autorize-tab-content" style="display: block">
			<div class="autorize-padding">
            <div style="width:100%;float:left;"> <div style="width:48%;float:left;margin-top:0px;">
            <h6 class="autorize-lbl">Welcome! Login to Your Account</h6>
            <asp:TextBox ID="txtUserName" runat="server" TabIndex="1"  placeholder="Name"></asp:TextBox>
             <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" TabIndex="2"  placeholder="Password"></asp:TextBox>
               <asp:TextBox ID="txtShortCode" runat="server" Enabled="false" TabIndex="3" placeholder="Short Name"></asp:TextBox>
                	<div class="clear"></div>
				<footer class="autorize-bottom">
                	<div class="clear"></div>

                          <a href="#" class="authorize-remember-me"><asp:CheckBox ID="chkRememberMe" Text=" Remember me" TabIndex="4" runat="server"></asp:CheckBox></a>
                          <asp:LinkButton ID="lbForgotPassword" class="authorize-forget-pass" TabIndex="5"  runat="server">Forgot your password?</asp:LinkButton>
                          <div class="clear"></div>
                          <div style="margin-top:-25px;float:left">
                            <div style="float:left"><asp:CheckBox ID="chkTermsAndConditions" Text="" 
                                    TabIndex="6" runat="server" ></asp:CheckBox></div>  <div style="float:left"><a id="aTermsAndConditions" runat="server" href="#" class="authorize-terms-and-conditions"> Terms and conditions</a></div>
                         
                         <asp:ModalPopupExtender ID="mpTermsAndConditions" runat="server" BackgroundCssClass="modalBackground-term-and-conditions"
                                                                            CancelControlID="aCloseTermAndConditions" EnableViewState="true" PopupControlID="dvPopupTermAndConditions"
                                                                            TargetControlID="aTermsAndConditions">
                                                                        </asp:ModalPopupExtender>
                        </div>
                          <div class="clear"></div>
                          <div  style="margin-top:-5px;float:left">
                          <div style="float:left;">
                          <asp:Button ID="btnLogin"  class="authorize-btn autorize-lbl" runat="server" TabIndex="7"  Text="Login"></asp:Button>
                          
                          </div>
                         <div  style="float:left;padding-left:145px;margin-top:-15px;"> <%--<a href="#">
                         <img src="img/columbus_log.png" alt="" /> </a> --%>
                         </div>
                          
                          </div> 
			
					<div class="clear"></div>
				</footer>
            </div>
		
                <div style="float:left;margin-left:0px;margin-top:0px; padding-left:25px; width:47%; border-left-color:rgba(20,29,30,0.19);border:2px;border-left-style:outset; ">    <h1 class="autorize-lbl">Partner with us.</h1>
								<p  class="reasons-txt-content">We believe travel for business or leisure has to be a memorable, joyous experience.</p>
								<p  class="reasons-txt-content">As a travel planner, partnering with us opens up a world of opportunities for you. Hotels, transfers, attractions, events, guides, visas, and a wide range of ground-handling services are now available at the click of a button.</p>
                                <p  class="reasons-txt-content">The best rates await you.</p>
                                <p  id="pDontAccount" runat="server" class="reasons-txt-content">For registration please send your company details to email : info@mahce.net</p><%--Don't have an account yet?--%>
<footer class="autorize-bottom">
<div class="header-account" style="display:none;">
<input id="btnRegister" runat="server" name="btnRegister" class="authorize-btn autorize-lbl" type="button" value="Register" />
			
</div>
									</div>
				</footer>
</div>
			</div></section>
                                        <section class="autorize-tab-content">

			<div class="autorize-padding">
            <div style="width:100%;float:left;"> <div style="width:48%;float:left;margin-top:0px;">
            <h6 class="autorize-lbl">Welcome! Login to Your RO Account</h6>
            <asp:TextBox ID="txtROuserName" runat="server" TabIndex="20" placeholder="Name"></asp:TextBox>
             <asp:TextBox ID="txtROPassword" runat="server" TabIndex="21" TextMode="Password"  placeholder="Password"></asp:TextBox>
                	<div class="clear"></div>
				<footer class="autorize-bottom">
                	<div class="clear"></div>
                          <a href="#" class="authorize-remember-me"><asp:CheckBox ID="chkRORemeberMe" Text="Remember Me"  TabIndex="23" runat="server"></asp:CheckBox></a>

                           <div class="clear"></div>
                         <%-- <div style="margin-top:-25px;float:left">
                          <div style="float:left"><asp:CheckBox ID="chkAgentTermsAndConditions" Text="" TabIndex="6" runat="server"></asp:CheckBox></div> <div  style="float:left"> <a id="aAgentTermsAndConditions" runat="server" href="#" class="authorize-terms-and-conditions"> Terms and conditions</a></div>
                        
                         <asp:ModalPopupExtender ID="mpAgentTermsAndConditions" runat="server" BackgroundCssClass="modalBackground-term-and-conditions"
                                                                            CancelControlID="aCloseTermAndConditions" EnableViewState="true" PopupControlID="dvPopupTermAndConditions"
                                                                            TargetControlID="aAgentTermsAndConditions">
                                                                        </asp:ModalPopupExtender>
                        </div>--%>

                         <div class="clear"></div>
			  <div  style="margin-top:-5px;float:left">
                          <div style="float:left;">
                          <asp:Button ID="btnROLogin"  class="authorize-btn autorize-lbl" runat="server"  TabIndex="22" Text="Login"></asp:Button> &nbsp;&nbsp;
                          <asp:Button ID="btnOktaLogin"  class="authorize-btn autorize-lbl" runat="server" TabIndex="7"  Text="Login with Okta"></asp:Button>
</div>
                         <div  style="float:left;padding-left:145px;margin-top:-15px;"> <%--<a href="#">
                         <img src="img/columbus_log.png" alt="" /> </a>--%> </div>
                          
                          </div> 
					<div class="clear"></div>
				</footer>
            </div>
		
                <div style="float:left;margin-left:0px;margin-top:0px; padding-left:25px; width:47%; border-left-color:rgba(20,29,30,0.19);border:2px;border-left-style:outset; ">    <h1 class="autorize-lbl">Partner with us.</h1>
								<p  class="reasons-txt-content">We believe travel for business or leisure has to be a memorable, joyous experience.</p>
								<p  class="reasons-txt-content">As a travel planner, partnering with us opens up a world of opportunities for you. Hotels, transfers, attractions, events, guides, visas, and a wide range of ground-handling services are now available at the click of a button.</p>
                                <p  class="reasons-txt-content">The best rates await you.</p>
    
									</div>
			
</div>
			</div></section>
                                    </div>
                                </div>
                            </div>
                            <!-- \\ authorize \\-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /main-cont -->
        <div style="position: absolute; bottom: 0px; width: 100%;">
            <footer class="footer-b">
	<div class="wrapper-padding">
		<div style="float:right" class="footer-left">© Copyright 2017 by MAHCE. All rights reserved.</div>
		
		<div class="clear"></div>
	</div>
</footer>
        </div>
         <div id="dvPopupTermAndConditions" class="modal-popup-div-login" style="display: none;">
    
             <div id="dvPopupTermAndConditions1"  >
                <div class="autorize-popup-tabs-term-and-conditions">
                    <a id="aCloseTermAndConditions" href="#" class="autorize-close-term-and-conditions"></a>
                </div>
               
                  <div>
                  <div id="dvTerms" style="padding-left:40%;padding-top:8px;border-bottom:1px solid #e3e3e3;height:40px;" >
                  <h6 style="padding-left:20px;" class="autorize-lbl">Terms and Conditions</h6></div>
                    <div class="container" >
                    <div>
                   <div style="width:845px;height:380px;overflow:auto;padding-right:15px;"  class="reasons-txt-content">
                       <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>

                    </div></div>
                    <div style="padding-top:15px;padding-left:40%;">
                        <input id="bntAgree"   class="authorize-btn autorize-lbl"  type="button" value="Agree" />
                                    </div>
                    </div>
                  </div>

                 
             </div>
         </div>

        <div id="dvPopup">
            <div class="overlay-popup">
            </div>
            <div class="autorize-popup-popup" style="display: none;">
                <div class="autorize-popup-tabs">
                    <a href="#" class="autorize-popup-close"></a>
                </div>
                <section class="autorize-tab-content-popup">
			<div class="autorize-padding-popup" style="overflow:auto;" >
          
				<h6 style="padding-left:20px;" class="autorize-lbl">Register for Your Account</h6>
                <div class="container">
              
  <div id="ex4" class="scrollbar">

              <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;" ><asp:Label ID="Label1" CssClass="autorize-lbl-Register" runat="server" Text="Company Name"></asp:Label></div> 
                <div class="col-md-4" style="margin-left:-15px;" > <asp:TextBox ID="txtRegName" placeholder="Company Name"   CssClass="validate[required]"   runat="server"></asp:TextBox></div>
            
          <div class="col-md-2 required"   style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label2" CssClass="autorize-lbl-Register" runat="server" Text="Registration No"></asp:Label></div>
          <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtRegNo" placeholder="Registration no"   CssClass="validate[required]" runat="server"></asp:TextBox></div>
                
               
            <div class="col-md-2"   style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label3" CssClass="autorize-lbl-Register" runat="server" Text="Building No"></asp:Label></div>
                 <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtRegBuildingNo" placeholder="Building No" runat="server"></asp:TextBox>          </div>
                             <div class="col-md-2"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label4" CssClass="autorize-lbl-Register" runat="server" Text="Street Line1"></asp:Label></div>
                 <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtRegStrretLine1" placeholder="Street Line1"   runat="server"></asp:TextBox></div>
                   <div class="col-md-2"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label5" CssClass="autorize-lbl-Register" runat="server" Text="Street Line2"></asp:Label></div>
                <div class="col-md-4" style="margin-left:-15px;" >  <asp:TextBox ID="txtRegStrretLine2" placeholder="Street Line2"     runat="server"></asp:TextBox></div>
                 <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label6" CssClass="autorize-lbl-Register" runat="server" Text="Country"></asp:Label></div>
                <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtRegCountry" placeholder="Country"    CssClass="validate[required]"  runat="server"></asp:TextBox>
                              
                                                               <asp:AutoCompleteExtender ID="CountryAutoCompleteExtender" 
                                                                  runat="server" DelimiterCharacters="" Enabled="True" ServicePath="" 
                                                                   CompletionInterval="10"
CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1" 
EnableCaching="false" FirstRowSelected="true" MinimumPrefixLength="-1"  OnClientItemSelected="Countryautocompleteselected"
ServiceMethod="GetCountry" TargetControlID="txtRegCountry">  </asp:AutoCompleteExtender>
    <asp:TextBox  style="display: none" id="txtCountryCode"  type="text" runat="server" ></asp:TextBox>
                </div>
               <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label7" CssClass="autorize-lbl-Register" runat="server" Text="City"></asp:Label></div>
                 <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtRegCity" placeholder="City"    CssClass="validate[required]"  runat="server"></asp:TextBox>
                           <asp:AutoCompleteExtender ID="CityAutoCompleteExtender" 
                                                                  runat="server" DelimiterCharacters="" Enabled="True" ServicePath="" 
                                                                   CompletionInterval="10"
CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1" 
EnableCaching="false" FirstRowSelected="true" MinimumPrefixLength="-1"  OnClientItemSelected="Cityautocompleteselected"
ServiceMethod="GetCity" TargetControlID="txtRegCity">  </asp:AutoCompleteExtender>
    <asp:TextBox  style="display: none" id="txtCityCode"  type="text" runat="server" ></asp:TextBox>
                 </div>
                  <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label8" CssClass="autorize-lbl-Register" runat="server" Text="Zip Code"></asp:Label></div>
                 <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtRegZipCode" placeholder="Zip Code"  CssClass="validate[required]"   runat="server"></asp:TextBox></div>
                  <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label9" CssClass="autorize-lbl-Register" runat="server" Text="Telephone No"></asp:Label></div>
                 <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtRegTelNo" placeholder="Telephone No"  onkeypress="return isPhoneNumber(event)"   CssClass="validate[required]"  runat="server"></asp:TextBox></div>
     <div class="col-md-2" style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label10" CssClass="autorize-lbl-Register" runat="server" Text="Fax No"></asp:Label></div>
                <div class="col-md-4" style="margin-left:-15px;">
                <asp:TextBox ID="txtRegFax" placeholder="Fax No"  onkeypress="return isPhoneNumber(event)"    runat="server"></asp:TextBox>
                 </div>
                  <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label11" CssClass="autorize-lbl-Register" runat="server" Text="Email Id"></asp:Label></div>
                <div class="col-md-4" style="margin-left:-15px;">
               <asp:TextBox ID="txtRegEmailId" placeholder="Email Id"  CssClass="autorize-tab-content-smallcase validate[required,custom[email]]" runat="server"></asp:TextBox>
                </div>
               
                 <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label12" CssClass="autorize-lbl-Register" runat="server" Text="Founded (Year)"></asp:Label></div>
                 <div class="col-md-4" style="margin-left:-15px;"><asp:TextBox ID="txtRegFounded" placeholder="Founded (Year)"    CssClass="validate[required]"   onkeypress="return isNumber(event)" runat="server"></asp:TextBox> </div>
         
            <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label13" CssClass="autorize-lbl-Register" runat="server" Text="No of Employees"></asp:Label></div>
                <div class="col-md-4" style="margin-left:-15px;"><asp:TextBox ID="txtRegNoOfEmp" placeholder="No of Employees"  CssClass="validate[required]"   onkeypress="return isNumber(event)"  runat="server"></asp:TextBox></div>
                   <div class="col-md-2"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label14" CssClass="autorize-lbl-Register" runat="server" Text="Website"></asp:Label></div>
                  <div class="col-md-4" style="margin-left:-15px;"><asp:TextBox ID="txtRegWebsite" placeholder="Website" CssClass="autorize-tab-content-smallcase"   runat="server"></asp:TextBox></div>
          

            <div class="col-md-2"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label15" CssClass="autorize-lbl-Register" runat="server" Text="About Us"></asp:Label></div>
                <div class="col-md-4" style="margin-left:-15px;"> <asp:TextBox ID="txtRegAboutUs" placeholder="About Us" CssClass="autorize-tab-content-textarea"  TextMode="MultiLine"  runat="server"></asp:TextBox> </div>
                   <div class="col-md-4" style="margin-left:-15px;"> </div>
                   <div class="clear"></div>
              <div class="col-md-12">	<h5 class="autorize-lbl">Contact Person Details</h5></div>
                 <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label16" CssClass="autorize-lbl-Register" runat="server" Text="Name"></asp:Label></div>
                <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtCName" placeholder="Name"   CssClass="validate[required]" runat="server"></asp:TextBox></div>
                   <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label17" CssClass="autorize-lbl-Register" runat="server" Text="Position"></asp:Label></div>
                  <div class="col-md-4" style="margin-left:-15px;"><asp:TextBox ID="txtCPosition" placeholder="Position"   CssClass="validate[required]" runat="server"></asp:TextBox></div>
                    <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label18" CssClass="autorize-lbl-Register" runat="server" Text="Phone No"></asp:Label></div>
                    <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtCPhoneNo" placeholder="Phone No"   CssClass="validate[required]" onkeypress="return isPhoneNumber(event)"   runat="server"></asp:TextBox></div>
                      <div class="col-md-2"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label19" CssClass="autorize-lbl-Register" runat="server" Text="Fax No"></asp:Label></div>
                  <div class="col-md-4" style="margin-left:-15px;"><asp:TextBox ID="txtCFaxNo" placeholder="Fax No"   onkeypress="return isPhoneNumber(event)"  runat="server"></asp:TextBox></div>
                   <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px;"><asp:Label ID="Label20" CssClass="autorize-lbl-Register" runat="server" Text="Mobile No"></asp:Label></div>
                    <div class="col-md-4" style="margin-left:-15px;">  <asp:TextBox ID="txtCMobileNo" placeholder="Mobile No"  CssClass="validate[required]"   onkeypress="return isPhoneNumber(event)"  runat="server"></asp:TextBox></div>
                     <div class="col-md-2 required" style="padding-left:20px;padding-top:10px;">  <asp:Label ID="Label21" CssClass="autorize-lbl-Register" runat="server" Text="Email"></asp:Label></div>
                  <div class="col-md-4" style="margin-left:-15px;"><asp:TextBox ID="txtCEmail" placeholder="Email"  CssClass="autorize-tab-content-smallcase validate[required,custom[email]]"  runat="server"></asp:TextBox>
                         </div> 
                         <div class="col-md-1">
                  </div>
                  <asp:UpdatePanel runat="server"><ContentTemplate>
              <div class="col-md-4"  style="padding-left:70px;">   
                  <cc1:CaptchaControl ID="ccJoin"  runat="server" 
                    CaptchaHeight="31" CaptchaLength="5" CaptchaLineNoise="None" EnableTheming="true"
                    CaptchaMaxTimeout="240" CaptchaMinTimeout="5" BorderStyle="None"  /> 
                    
                  
                    </div>       <div class="col-md-1">  <asp:ImageButton ID="ibCaptchaRefresh" runat="server" Height="30px" 
                      ImageUrl="~/img/refresh-button-icon-1.png"    UseSubmitBehavior="false"  Width="30px"></asp:ImageButton>
                  </div></ContentTemplate></asp:UpdatePanel>
                     <div class="col-md-2 required"  style="padding-left:20px;padding-top:10px; margin-left:-15px;"><asp:Label ID="Label22" CssClass="autorize-lbl-Register" runat="server" Text="captcha code"></asp:Label></div>
                <div class="col-md-4" style="margin-left:-15px;"><asp:TextBox ID="txtCaptchaCode"  CssClass="validate[required]"  placeholder="captcha code" runat="server"></asp:TextBox></div>
                    </div>
				</div>

                
				<footer class="autorize-bottom" style="padding-left:25px; ">
                            <asp:Button ID="btnRegistration" runat="server"  class="authorize-btn" Text="Register"></asp:Button>
					<div class="clear"></div>
				</footer>
            
			</div>
		</section>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdPopup" runat="server" />
    <asp:HiddenField ID="hdTab" runat="server" />


    </form>
    <!-- // scripts // -->
    <script src="js/idangerous.swiper.js"></script>
    <script src="js/slideInit.js"></script>
    <script src="js/jqeury.appear.js"></script>
    <script src="js/owl.carousel.min.js"></script>
    <script src="js/bxSlider.js"></script>
    <script src="js/custom.select.js"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/twitterfeed.js"></script>
    <script src="js/script.js"></script>
    <script type="text/javascript" src="js/jquery.validationEngine-en.js" charset="utf-8"></script>
    <script type="text/javascript" src="js/jquery.validationEngine.js" charset="utf-8"></script>
    <!-- \\ scripts \\ -->
    


</body>
</html>

