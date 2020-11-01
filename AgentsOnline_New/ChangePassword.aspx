<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="ChangePassword" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
  <meta name="description" content="" />
  <meta name="keywords" content="" />
  <meta charset="utf-8" /><link rel="icon" href="favicon.png" />
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
          text-transform:uppercase;
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
          background-color:#F2F4F4;
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
<%--  <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDJaAdPRN3HuMp8_CSVuUmFif1rWyauBOs"   type="text/javascript"></script>--%>
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

            document.getElementById('<%=txtUserName.ClientID%>').setAttribute("readonly", true);

            $("#btnReset").button().click(function () {
                
                document.getElementById('<%=txtOldPassword.ClientID%>').value = '';
                document.getElementById('<%=txtNewPassword.ClientID%>').value = '';
                document.getElementById('<%=txtNewConfirmPassword.ClientID%>').value = '';
     
       
            });

            $('#dvMenus').hide();
            $("#btnViewMenu").button().click(function () {

                if ($('#btnViewMenu').val() == 'Show Menu') {
                    $('#btnViewMenu').val('Hide Menu');

                    $('#dvMenus').show(1000);
                    txtFocus.focus();
                }
                else {
                    $('#btnViewMenu').val('Show Menu');
                    $('#dvMenus').hide(1000);

                }

            });

        });

        function ValidateEntry() {
            var UserName = document.getElementById('<%=txtUserName.ClientID%>').value;
            var lblUserName = document.getElementById('<%=lblHeaderUserName.ClientID%>').innerText;

            if ((UserName == '')) { //|| (UserName!=lblUserName)
               
                    showDialog('Alert Message', 'You cannot change password.Please login again.', 'warning');
                    HideProgess();
                    return false;

                }
                var OldPassword = document.getElementById('<%=txtOldPassword.ClientID%>').value;
                var NewPassword = document.getElementById('<%=txtNewPassword.ClientID%>').value;
                var NewConfirmPassword = document.getElementById('<%=txtNewConfirmPassword.ClientID%>').value;
                if (OldPassword == '') {

                    showDialog('Alert Message', 'Please enter old password.', 'warning');
                    HideProgess();
                    return false;

                }
                if (NewPassword == '') {

                    showDialog('Alert Message', 'Please enter new password.', 'warning');
                    HideProgess();
                    return false;

                }
                if (NewConfirmPassword == '') {

                    showDialog('Alert Message', 'Please enter confirm password.', 'warning');
                    HideProgess();
                    return false;
                }
                if (NewConfirmPassword != NewPassword) {

                    showDialog('Alert Message', 'New password and confirm password are not matching. Please enter again..', 'warning');
                    HideProgess();
                    return false;
                }
                
        }

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
              document.getElementById('<%=txtUserName.ClientID%>').setAttribute("readonly", true);
              HideProgess();
              $('#dvMenus').hide();
              $("#btnViewMenu").button().click(function () {

                  if ($('#btnViewMenu').val() == 'Show Menu') {
                      $('#btnViewMenu').val('Hide Menu');

                      $('#dvMenus').show(1000);
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
<body  onload="RefreshContent()">
    <form id="form1" runat="server">
  <!-- // authorize // -->
	<div class="overlay"></div>

<!-- \\ authorize \\-->
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"  EnablePartialRendering="true">
    </asp:ScriptManager>
    <header id="top">
	<div class="header-a">
		<div class="wrapper-padding">	
         <div class="header-user"><asp:Label ID="lblHeaderUserName" runat="server" ></asp:Label> </div>				
			<div class="header-phone"><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label> </div>
			<div class="header-account">
				
                		<asp:UpdatePanel ID="UpdatePanel4" runat="server"><ContentTemplate><asp:Button ID="btnMyAccount"   UseSubmitBehavior="false" 
                    CssClass="header-account-button" runat="server" Text="Account"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
			</div>
              <div class="header-agentname" style="padding-left:225px;"><asp:Label ID="lblHeaderAgentName" runat="server" ></asp:Label> </div>
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
			<div id="dvCurrency" runat="server" class="header-curency">
			</div>
               <div class="header-lang" id="dvMybooking" runat="server" style="padding-right:25px;margin-right:5px;">
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
			<div class="header-logo"><a href="Home.aspx?Tab=0"><img id="imgLogo" runat="server" alt="" /></a></div>
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
  <div class="body-wrapper" style="padding-top:150px;padding-bottom:50px;">
    <div class="wrapper-padding-full">
    <div class="page-head">
      <div class="page-title">Change Password <span></span></div>
      <div class="breadcrumbs">
        <a href="Home.aspx?Tab=0">Home</a> / <a href="#">Change Password </a> 
      </div>
      <div class="clear"></div>
    </div>
     <div class="page-head">
         <div class="page-search-content-search" style="margin-top: -10px;">
             <div class="search-tab-content" style="min-height: 370px; max-height: 450px; padding-left: 55px;
                 padding-top: 15px;">
                 <asp:UpdatePanel ID="upnlSearch" runat="server" UpdateMode="Conditional">
                     <ContentTemplate>
                         <div class="page-search-p">
                             <!-- // -->
                             <div class="row-col-12">
                               <div style="float: right;margin-top:10px;">
                            <input id="btnViewMenu" type="button" class="my-account-slide-btn" value="Show Menu" /></div>
                                 <div class="row-col-3" style="padding-right: 35px;">
                                     <div class="srch-tab-line no-margin-bottom">
                                         <label>
                                             User Name</label>
                                         <div class="input-a">
                                             <asp:TextBox ID="txtUserName" TabIndex="1" runat="server"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="row-col-3" style="padding-right: 35px;">
                                     <div class="srch-tab-line no-margin-bottom">
                                         <label>
                                             Old Password</label>
                                         <div class="input-a">
                                             <asp:TextBox ID="txtOldPassword" Style="border: none;" TextMode="Password" TabIndex="2" autocomplete="off"
                                                 runat="server"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="clear">
                                 </div>
                                 <div class="row-col-3" style="padding-right: 35px; margin-top: 35px;">
                                     <div class="srch-tab-line no-margin-bottom">
                                         <label>
                                             New Password</label>
                                         <div class="input-a">
                                             <asp:TextBox ID="txtNewPassword" Style="border: none;" autocomplete="off" TextMode="Password"
                                                 TabIndex="3" runat="server"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="row-col-3" style="padding-right: 15px; margin-top: 35px;">
                                     <div class="srch-tab-line no-margin-bottom">
                                         <label>
                                             Confirm New Password</label>
                                         <div class="input-a">
                                             <asp:TextBox ID="txtNewConfirmPassword" Style="border: none;" TextMode="Password" autocomplete="off"
                                                 TabIndex="4" runat="server"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="clear">
                                 </div>
                                 <div class="row-col-3" style="padding-right: 35px; margin-top: 35px;">
                                     <asp:Button ID="btnChangePassword" class="authorize-btn" TabIndex="15" Width="200px" 
                                         runat="server" OnClientClick="return ValidateEntry()" Text="Change Password">
                                     </asp:Button>
                                 </div>
                                 <div class="row-col-2" style="padding-right: 35px; margin-top: 35px;">
                                     <input id="btnReset" type="button" class="srch-btn-home" value="Reset" />
                                 </div>
                             </div>
                             <div class="clear">
                             </div>
                             <div class="clear">
                             </div>
                             <div class="row-col-12" style="padding-right: 15px; margin-top: 15px;margin-right:96px;">
                             </div>
                                    <div class="row-col-3" id="dvMenus" style="z-index: 999999; position: absolute; top:275px;float:right;right:72px;
                    bottom: 10px; padding: 0; background-color:#141d1e; height: 420px;">
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
                     </ContentTemplate>
                 </asp:UpdatePanel>
             </div>
         </div>
         
     </div>
        <div class="sp-page">

   <div>
          
     
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
    </form>
</body>
</html>
