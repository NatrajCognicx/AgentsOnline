<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContactUs.aspx.vb" Inherits="ContactUs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mohd Al Humaidi Computer  L.L.C.</title>
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
    <%--   <link rel="stylesheet" href="css/AgentsOnlineStyles.css" />--%>

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

    <!-- // scripts // -->
    <script type="text/javascript" src="js/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="js/idangerous.swiper.js"></script>
    <script type="text/javascript" src="js/slideInit.js"></script>
    <script type="text/javascript" src="js/jqeury.appear.js"></script>
    <script type="text/javascript" src="js/owl.carousel.min.js"></script>
    <script type="text/javascript" src="js/bxSlider.js"></script>
    <script type="text/javascript" src="js/custom.select.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    <script type="text/javascript" type="text/javascript" src="js/twitterfeed.js"></script>
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
            });    
    </script>

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
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestUserControl);
        function EndRequestUserControl(sender, args) {

        }
        function RefreshContent() {
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandlerForProgressBar);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandlerForProgressBar);

        }
        function BeginRequestHandlerForProgressBar() {
            //alert('test');
            //ShowProgess();

        }
        function EndRequestHandlerForProgressBar() {
            //  HideProgess();
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
<body class="index-page">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <header id="top">
	<div class="header-a">
		<div class="wrapper-padding">	
        <div class="header-user"><asp:Label ID="lblHeaderUserName"  runat="server" ></asp:Label> </div>		
			<div class="header-phone"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-account">
				<asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
			</div>
           <div class="header-agentname" style="padding-left:225px;"><asp:Label ID="lblHeaderAgentName" runat="server" ></asp:Label> </div>
           
               <div class="header-lang">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><asp:Button ID="btnLogOut"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Log Out"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
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
          <%--  <div class="header-lang" style="padding-left:10px;margin-left:5px;">
               <a href="#"><img  alt=""  width="20px;" src="img/booking-cart.png" /></a>
               </div>--%>
			<div class="clear"></div>
		</div>
	</div>
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
        <div class="body-padding">
        <div style="margin:150px 100px 400px 100px;">
            <div class="row-col-12">
                <div class="page-title-contact-us">
                    <label>
                        Contact Us</label></div>
            </div>
            <div class="clear" style="padding-top:40px;" ></div>
            <div class="row-col-12" style="background-color:#fff !important;">
                <div class="row-col-5" id="dvDestinationSpecialist" runat="server" style="border-right:1px solid #dedcdc;">
                 <div class="row-col-12">
                 <div style="padding-left:40%">
                   <label class="contact-us-sub-heading">
                        Destination Specialist</label></div>
                 </div>
                 <div class="clear" style="padding-bottom:15px;"></div>
                 <div class="row-col-12">
                 <div style="padding-left:41%;">
                  <div style="padding:5px;border:1px solid #dedcdc;width:150px;height:150px;">
                     <asp:Image ID="imgPhoto" Width="150px" Height="150px" runat="server" /></div></div>
                 </div>
            
                 <div class="row-col-6" style="padding-left:35%;">
                    <div class="footer-adress" style="margin-top:-25px;">
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                          <br />
                           <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                             <br />
                             <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                              <br />
                              </div>

                 </div>
                </div>
                <div class="row-col-5" >
                 <div class="row-col-12">
                 <div  style="padding-left:10%">
                   <label class="contact-us-sub-heading">
                        General Contact Details</label></div>
                 </div>
                    <div class="row-col-12" style="padding-top:10px;padding-left:10%;">
                        <div id="dvRGFooterAddressContactUs" runat="server" class="section">
 
                            <div class="footer-adress">
                                 <asp:Label ID="lblCFAdd1" runat="server"></asp:Label>,
                                <br />
                                 <asp:Label ID="lblCFAdd2" runat="server"></asp:Label>,<br />
                                <asp:Label ID="lblCFAdd3" runat="server"></asp:Label>, <asp:Label ID="lblCFAdd4" runat="server"></asp:Label></div>
                            <div class="footer-phones">
                              Phone:  <asp:Label ID="lblCFPhone" runat="server"></asp:Label></div>
                            <div class="footer-email">
                              Email: <asp:Label ID="lblCFEmail" runat="server"></asp:Label></div>
                            <div class="footer-email">
                               Working Time:   <asp:Label ID="lblCFWorkingTime" runat="server"></asp:Label></div>
                        </div>
                    <%--    <div id="dvRPFooterAddressContactUs" runat="server" class="section">
                           
                            <div class="footer-adress">
                                P.O. Box 26475, M04 Deira House,
                                <br />
                                Abu Baker Al Siddique Road,<br />
                                Deira Dubai, UAE</div>
                            <div class="footer-phones">
                              Phone:  +971 4 2626282</div>
                            <div class="footer-email">
                            Email:    info@royalpark.net</div>
                            <div class="footer-email">
                             Working Time:   9:00 AM~ 7:00 PM (GMT+4)</div>
                        </div>--%>
                    </div>
                </div>
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
		<div id="dvMagnifyingMemories" runat="server"  class="section-middle">
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
    <asp:ModalPopupExtender ID="ModalPopupDays" runat="server" BehaviorID="ModalPopupDays"
        TargetControlID="btnInvisibleGuest" CancelControlID="btnClose" PopupControlID="Loading1"
        BackgroundCssClass="ModalPopupForPageLoading">
    </asp:ModalPopupExtender>
    <input id="btnInvisibleGuest" runat="server" type="button" value="Cancel" style="display: none" />
    <input id="btnClose" type="button" value="Cancel" style="display: none" />
    <asp:HiddenField ID="hdLoginType" runat="server" />
    <asp:HiddenField ID="hdTab" runat="server" />
    <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
    <asp:HiddenField ID="hdChildAgeLimit" runat="server" />
    <asp:HiddenField ID="hdMaxNoOfNight" runat="server" />
     <asp:HiddenField ID="hdWhiteLabel" runat="server" />
    </form>
</body>
</html>
