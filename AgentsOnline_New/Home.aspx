<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Home.aspx.vb" Async="true"
    Inherits="Home" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    
    <%--***Danny 18/08/2018 fa fa-star--%>
    <link href="css/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
   <%-- <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">--%>
          

<%--   <link href='http://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet'
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
    <link href='http://fonts.googleapis.com/cssll?family=Open+Sans:400,600,700&amp;subset=latin,cyrillic'
        rel='stylesheet' type='text/css' />--%>

    <script type="text/javascript" src="js/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="js/idangerous.swiper.js"></script>
    <script type="text/javascript" src="js/slideInit.js"></script>
    <script type="text/javascript" src="js/jqeury.appear.js"></script>
    <script type="text/javascript" src="js/owl.carousel.min.js"></script>
    <script type="text/javascript" src="js/bxSlider.js"></script>
    <script type="text/javascript" src="js/custom.select.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
       <script type="text/javascript" src="js/script.js"></script>

    <script type="text/javascript" type="text/javascript" src="js/twitterfeed.js"></script>
 
    <link rel="stylesheet" type="text/css" href="css/dialog_box.css" />
       <script type="text/javascript" src="js/dialog_box.js"></script>

 


    <%-- Added multiselect shahul--%>
     <link rel="stylesheet" type="text/css" href="/css/result-light.css">
      <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
      <link rel="stylesheet" type="text/css" href="css/bootstrap-multiselect.css">
      <link rel="stylesheet" type="text/css" href="css/Multiselect.css">
    <%--  <link rel="stylesheet" type="text/css" href="css/bootstrap-3.3.2.min.css">--%>
      <script type="text/javascript" src="js/bootstrap-3.3.2.min.js"></script>
  <style type="text/css">
    .multiselect-container>li>a>label {
  padding: 4px 20px 4px 30px;
}
  </style>
  <!-- TODO: Missing CoffeeScript 2 -->
 <%-- -- end multiselect--%>
 <!--*** Danny Image Map 09/09/2018-->
  <script>
      function imagecancel() {
          var loder161 = $('#imagemap');
          loder161.attr('src', 'img/arabiya.png');
      }
      function imgloader(e) {

          var loder161 = $('#imagemap');
          loder161.attr('src', e);
      }

      
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

            $('#ddlMealPlan').multiselect();
            $('#ddlMealPlan').change(function () {
                $('#hdmealcode').val($('#ddlMealPlan').val());
            });


        });   
        
        
         
    </script>
    <script language="javascript" type="text/javascript">

    
    function showRoomloder(){  
     var loder161 =document.getElementById('<%=loder16.ClientID%>') 
    loder161.setAttribute('style', 'display:block');

    }
    
        function IncludeScriptAfterUPUpdate() {

            $(".owl-slider").owlCarousel({
                items: 4,
                autoPlay: 5000,
                itemsDesktop: [1120, 4], //5 items between 1000px and 901px
                itemsDesktopSmall: [900, 2], // betweem 900px and 601px
                itemsTablet: [620, 2], //2 items between 600 and 479
                itemsMobile: [479, 1], //1 item between 479 and 0
                stopOnHover: true
            });

          // loadjscssfile("js/script.js", "js") //dynamically load and add this .js file
        }

        function loadjscssfile(filename, filetype) {
            if (filetype == "js") { //if filename is a external JavaScript file
                var fileref = document.createElement('script')
                fileref.setAttribute("type", "text/javascript")
                fileref.setAttribute("src", filename)
            }
            else if (filetype == "css") { //if filename is an external CSS file
                var fileref = document.createElement("link")
                fileref.setAttribute("rel", "stylesheet")
                fileref.setAttribute("type", "text/css")
                fileref.setAttribute("href", filename)
            }
            if (typeof fileref != "undefined")
                document.getElementsByTagName("head")[0].appendChild(fileref)
        }
        function fnReadOnly(txt) {
            event.preventDefault();
        }

        function ValidateAirportMeetSearch() {
            ShowProgess();

            var chkarrival = document.getElementById('<%=chkMAarrival.ClientID%>').checked;
            var chkDeparture = document.getElementById('<%=chkMADeparture.ClientID%>').checked;
            var chkTransit = document.getElementById('<%=chkTransit.ClientID%>').checked;

            if (document.getElementById('<%=txtMAArrivaldate.ClientID%>').value == '' && chkarrival == true) {
                showDialog('Alert Message', 'Please select  Arrival date.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtMAArrivalflightCode.ClientID%>').value == '' && document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value == '' && chkarrival == true) {
                showDialog('Alert Message', 'Please select Arrival Flight/Airport.', 'warning');
                HideProgess();
                return false;
            }


            //            if (document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value == '' && chkarrival == true) {
            //                showDialog('Alert Message', 'Please select Arrival airport.', 'warning');
            //                HideProgess();
            //                return false;
            //            }


            if (document.getElementById('<%=txtMADeparturedate.ClientID%>').value == '' && chkDeparture == true) {
                showDialog('Alert Message', 'Please select  Departure date.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtMADepartureFlightCode.ClientID%>').value == '' && document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value == '' && chkDeparture == true) {
                showDialog('Alert Message', 'Please select   Departure Flight/Airport.', 'warning');
                HideProgess();
                return false;
            }


            //            if (document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value == '' && chkDeparture == true) {
            //                showDialog('Alert Message', 'Please select   Departure airport.', 'warning');
            //                HideProgess();
            //                return false;
            //            }


            if (document.getElementById('<%=txtTransitarrdate.ClientID%>').value == '' && chkTransit == true) {
                showDialog('Alert Message', 'Please select  transit arrival date.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtMATranArrFlightCode.ClientID%>').value == '' && chkTransit == true) {
                showDialog('Alert Message', 'Please select transit arrival Flight.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtMATransitArrivalpickupcode.ClientID%>').value == '' && chkTransit == true) {
                showDialog('Alert Message', 'Please select transit arrival airport.', 'warning');
                HideProgess();
                return false;
            }


            if (document.getElementById('<%=txtMATrandepdate.ClientID%>').value == '' && chkTransit == true) {
                showDialog('Alert Message', 'Please select  transit departure date.', 'warning');
                HideProgess();
                return false;
            }
            if (document.getElementById('<%=txtMATranDepartureFlightCode.ClientID%>').value == '' && chkTransit == true) {
                showDialog('Alert Message', 'Please select transit departure Flight.', 'warning');
                HideProgess();
                return false;
            }
            if (document.getElementById('<%=txtMATransitDeparturepickupcode.ClientID%>').value == '' && chkTransit == true) {
                showDialog('Alert Message', 'Please select transit departure airport.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=ddlMAAdult.ClientID%>').value == '0') {
                showDialog('Alert Message', 'Please select any number of adult.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtMACustomercode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter Customer.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtMASourcecountrycode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter Transfer source country.', 'warning');
                HideProgess();
                return false;
            }


            var child = document.getElementById('<%=ddlMAChild.ClientID%>').value;
            if (child != '0') {
                var child1 = document.getElementById('<%=txtMAChild1.ClientID%>').value;
                var child2 = document.getElementById('<%=txtMAChild2.ClientID%>').value;
                var child3 = document.getElementById('<%=txtMAChild3.ClientID%>').value;
                var child4 = document.getElementById('<%=txtMAChild4.ClientID%>').value;
                var child5 = document.getElementById('<%=txtMAChild5.ClientID%>').value;
                var child6 = document.getElementById('<%=txtMAChild6.ClientID%>').value;
                var child7 = document.getElementById('<%=txtMAChild7.ClientID%>').value;
                var child8 = document.getElementById('<%=txtMAChild8.ClientID%>').value;
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

            return true;

        }


        function ValidateOthSearch() {

            //   var Tab = GetParameterValues('Tab');
            ShowProgess();
            if ((document.getElementById('<%=txtothFromDate.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please select  From-date.', 'warning');
                HideProgess();
                return false;
            }

            if ((document.getElementById('<%=txtothToDate.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please select  To-date.', 'warning');
                HideProgess();
                return false;
            }



            if ((document.getElementById('<%=txtothgroupcode.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please select Other Service Group.', 'warning');
                HideProgess();
                return false;
            }


            if (document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter Other Service source country.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=ddlOthAdult.ClientID%>').value == '0') {
                showDialog('Alert Message', 'Please select any number of adult.', 'warning');
                HideProgess();
                return false;
            }

            var child = document.getElementById('<%=ddlOthChild.ClientID%>').value;
            if (child != '0') {
                var child1 = document.getElementById('<%=txtOthChild1.ClientID%>').value;
                var child2 = document.getElementById('<%=txtOthChild2.ClientID%>').value;
                var child3 = document.getElementById('<%=txtOthChild3.ClientID%>').value;
                var child4 = document.getElementById('<%=txtOthChild4.ClientID%>').value;
                var child5 = document.getElementById('<%=txtOthChild5.ClientID%>').value;
                var child6 = document.getElementById('<%=txtOthChild6.ClientID%>').value;
                var child7 = document.getElementById('<%=txtOthChild7.ClientID%>').value;
                var child8 = document.getElementById('<%=txtOthChild8.ClientID%>').value;
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



            return true;

        }




        function ValidateTourSearch() {
            ShowProgess();

            if (document.getElementById('<%=txtTourfromDate.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any from-date.', 'warning');
                HideProgess();
                return false;
            }

            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtTourCustomerCode.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please select Customer.', 'warning');
                HideProgess();
                return false;
            }
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
            if (document.getElementById('<%=txtTourStartingFromCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter tour starting from.', 'warning');
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
                HideProgess();
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



        function ValidateTrfSearch() {

            //   var Tab = GetParameterValues('Tab');
            ShowProgess();
            if ((document.getElementById('<%=txtTrfArrivaldate.ClientID%>').value == '') && (document.getElementById('<%=chkarrival.ClientID%>').checked == true)) {
                showDialog('Alert Message', 'Please select any Arrival-date.', 'warning');
                HideProgess();
                return false;
            }

            if ((document.getElementById('<%=txtTrfDeparturedate.ClientID%>').value == '') && (document.getElementById('<%=chkDeparture.ClientID%>').checked == true)) {
                showDialog('Alert Message', 'Please select any Departure-date.', 'warning');
                HideProgess();
                return false;
            }

            if ((document.getElementById('<%=txtArrivalflight.ClientID%>').value == '') && (document.getElementById('<%=txtTrfArrivalpickup.ClientID%>').value == '') && (document.getElementById('<%=chkarrival.ClientID%>').checked == true)) {
                showDialog('Alert Message', 'Please Select Arrival Flight No/Arrival Pickup.', 'warning');
                HideProgess();
                return false;
            }

            //            if ((document.getElementById('<%=txtTrfArrivalpickup.ClientID%>').value == '') && (document.getElementById('<%=chkarrival.ClientID%>').checked == true)) {
            //                showDialog('Alert Message', 'Please select any Arrival Pickup.', 'warning');
            //                HideProgess();

            //                return false;
            //            }

            if ((document.getElementById('<%=txtTrfArrDropoff.ClientID%>').value == '') && (document.getElementById('<%=chkarrival.ClientID%>').checked == true)) {
                showDialog('Alert Message', 'Please select any Arrival Drop Off.', 'warning');
                HideProgess();
                return false;
            }


            if ((document.getElementById('<%=txtDepartureFlight.ClientID%>').value == '') && (document.getElementById('<%=txtTrfDepairportdrop.ClientID%>').value == '') && (document.getElementById('<%=chkDeparture.ClientID%>').checked == true)) {
                showDialog('Alert Message', 'Please Select Departure Flight No/Departure Drop Off.', 'warning');
                HideProgess();
                return false;
            }

            if ((document.getElementById('<%=txtTrfDeppickup.ClientID%>').value == '') && (document.getElementById('<%=chkDeparture.ClientID%>').checked == true)) {
                showDialog('Alert Message', 'Please Select Departure Pickup.', 'warning');
                HideProgess();
                return false;
            }

            //            if ((document.getElementById('<%=txtTrfDepairportdrop.ClientID%>').value == '') && (document.getElementById('<%=chkDeparture.ClientID%>').checked == true)) {
            //                showDialog('Alert Message', 'Please Select Departure Drop Off.', 'warning');
            //                HideProgess();
            //                return false;
            //            }

            if ((document.getElementById('<%=txtTrfCustomercode.ClientID%>').value == '') && (document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO')) {
                showDialog('Alert Message', 'Please select Customer.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter Transfer source country.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=ddlTrfAdult.ClientID%>').value == '0') {
                showDialog('Alert Message', 'Please select any number of adult.', 'warning');
                HideProgess();
                return false;
            }

            var child = document.getElementById('<%=ddlTrfChild.ClientID%>').value;
            if (child != '0') {
                var child1 = document.getElementById('<%=txtTrfChild1.ClientID%>').value;
                var child2 = document.getElementById('<%=txtTrfChild2.ClientID%>').value;
                var child3 = document.getElementById('<%=txtTrfChild3.ClientID%>').value;
                var child4 = document.getElementById('<%=txtTrfChild4.ClientID%>').value;
                var child5 = document.getElementById('<%=txtTrfChild5.ClientID%>').value;
                var child6 = document.getElementById('<%=txtTrfChild6.ClientID%>').value;
                var child7 = document.getElementById('<%=txtTrfChild7.ClientID%>').value;
                var child8 = document.getElementById('<%=txtTrfChild8.ClientID%>').value;
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



            return true;

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

            if ((document.getElementById('<%=txtPreHotelCustomercode.ClientID%>').value == '') && (document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO')) {
                showDialog('Alert Message', 'Please select agent.', 'warning');
                HideProgess();
                return false;
            }

            if (document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please enter source country.', 'warning');
                HideProgess();
                return false;
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


        function ValidateSearch() {
          
            ShowProgess();
            if (document.getElementById('<%=txtDestinationcode.ClientID%>').value == '') {
                showDialog('Alert Message', 'Please select any destination.', 'warning');
                HideProgess();
                return false;
            }

            

             if ((document.getElementById('<%=txtHotelStarscode.ClientID%>').value == '') && (document.getElementById('<%=txtHotelStars.ClientID%>').value != '') ){
                showDialog('Alert Message', 'Category code not select Please select Category again.', 'warning');
                HideProgess();
                return false;
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


            // *** Hotel child age change based on room on 23/05/2017 -- Start

            // *** Danny 26/08/2018 







            // *** ---------------------------- End

            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtCustomerCode.ClientID%>').value == '')) {
                showDialog('Alert Message', 'Please Select Customer.', 'warning');
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


        function AutoCompleteExtender_txtPreHotelSourceCountry_OnClientPopulating(sender, args) {

            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>').value != '')) {

                sender.set_contextKey(document.getElementById('<%=txtPreHotelSourceCountryCode.ClientID%>').value);
            }



        }



        // added shahul 23/04/18

        function AutoCompleteExtender_txtTourSourceCountry_OnClientPopulating(sender, args) {
            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtTourCustomerCode.ClientID%>').value != '')) {

                sender.set_contextKey(document.getElementById('<%=txtTourCustomerCode.ClientID%>').value);
            }
        }

        function TourCountryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtTourSourceCountryCode.ClientID%>').value = '';
            }
        }
        // added shahul 23/04/18
        function AutoCompleteExtender_txtothSourceCountry_OnClientPopulating(sender, args) {
            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtothCustomercode.ClientID%>').value != '')) {

                sender.set_contextKey(document.getElementById('<%=txtothCustomercode.ClientID%>').value);
            }
        }

        function OthCountryautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = '';
            }
        }

        function othgroupautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtothgroupcode.ClientID%>').value = eventArgs.get_value();
            }
            else {
                document.getElementById('<%=txtothgroupcode.ClientID%>').value = '';
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


        function othserviceCustomerAutocompleteselected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtothCustomerCode.ClientID%>').value = eventArgs.get_value();
                    $find('AutoCompleteExtender_txtothSourceCountry').set_contextKey(eventArgs.get_value());
                    GetOthCountryDetails(eventArgs.get_value());


                }
                else {
                    document.getElementById('<%=txtothCustomerCode.ClientID%>').value = '';
                }
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
                //                added shahul 27/06/18
                chkshow.removeAttribute('disabled');

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
                $find('AutoCompleteExtender_txtPreHotelSourceCountry').set_contextKey(eventArgs.get_value());
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



        function PropertTypeChanged(val) {

            SethotelContextkey();

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


        function DestinationNameautocompleteselected(source, eventArgs) {

            if (eventArgs != null) {


                document.getElementById('<%=txtDestinationcode.ClientID%>').value = eventArgs.get_value();

            }
            else {
                document.getElementById('<%=txtDestinationcode.ClientID%>').value = '';
            }
            SethotelContextkey();
        }

        function ArrivalflightSetContextKey() {
            $find('<%=AutoCompleteExtender_txtArrivalflight.ClientID%>').set_contextKey($get("<%=txtTrfArrivaldate.ClientID %>").value);

        }

        function DepartureflightSetContextKey() {
            $find('<%=AutoCompleteExtender_txtDepartureFlight.ClientID%>').set_contextKey($get("<%=txtTrfDeparturedate.ClientID %>").value);

        }

        function MADepartureflightSetContextKey() {
            $find('<%=AutoCompleteExtender_txtMADepartureFlight.ClientID%>').set_contextKey($get("<%=txtMADeparturedate.ClientID %>").value);

        }

        function MATranDepartureflightSetContextKey() {
            $find('<%=AutoCompleteExtender_txtMAtranDepartureFlight.ClientID%>').set_contextKey($get("<%=txtMATrandepdate.ClientID %>").value);

        }

        function MATranArrivalflightSetContextKey() {
            $find('<%=AutoCompleteExtender_txtMAtranArrFlight.ClientID%>').set_contextKey($get("<%=txtTransitarrdate.ClientID %>").value);

        }

        function MAArrivalflightSetContextKey() {
            $find('<%=AutoCompleteExtender_txtMAArrivalflight.ClientID%>').set_contextKey($get("<%=txtMAArrivaldate.ClientID %>").value);

        }

        function MAArrDropoffAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtMAArrDropoffcode.ClientID%>').value = eventArgs.get_value();
                }
                else {
                    document.getElementById('<%=txtMAArrDropoffcode.ClientID%>').value = '';
                }
            }
        }


        function ValidateDepDate() {

            var txtArrdate = document.getElementById("<%=txtTrfArrivaldate.ClientID%>");
            var txtDepdate = document.getElementById("<%=txtTrfDeparturedate.ClientID%>");


            var dp = txtArrdate.value.split("/");
            var newChkInDt = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);

            var dp1 = txtDepdate.value.split("/");
            var newChkOutDt = new Date(dp1[2] + "/" + dp1[1] + "/" + dp1[0]);

            newChkInDt = getFormatedDate(newChkInDt);
            newChkOutDt = getFormatedDate(newChkOutDt);

            newChkInDt = new Date(newChkInDt);
            newChkOutDt = new Date(newChkOutDt);
            if (newChkInDt > newChkOutDt) {
                txtDepdate.value = txtArrdate.value;

                // txtDepdate.focus();
                showDialog('Alert Message', 'Departure date should be Greater than Arrival date', 'warning');
                return false;

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


      function TrfArrDropoffAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                     <%--Added Shahul 19/07/2018--%>
                    var code = eventArgs.get_value();   
                    code = code.split(';');   
                    //alert(code[1]);
                    document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>').value = code[0];
                    document.getElementById('<%=txtTrfArrDroptype.ClientID%>').value = code[1];   
                }
                else {
                    document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>').value = '';
                    document.getElementById('<%=txtTrfArrDroptype.ClientID%>').value = '';  
                }
            }
        }

        function MAArrivalpickupAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value = eventArgs.get_value();
                }
                else {
                    document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value = '';
                }
            }

        }

        function MATranArrivalpickupAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtMATransitArrivalpickupcode.ClientID%>').value = eventArgs.get_value();
                }
                else {
                    document.getElementById('<%=txtMATransitArrivalpickupcode.ClientID%>').value = '';
                }
            }

        }


        function TrfArrivalpickupAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>').value = eventArgs.get_value();
                }
                else {
                    document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>').value = '';
                }
            }

        }

        function TrfDepairportdropAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>').value = eventArgs.get_value();
                }
                else {
                    document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>').value = '';
                }
            }
        }


        function MADepairportdropAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value = eventArgs.get_value();
                }
                else {
                    document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value = '';
                }
            }
        }

        function TrfCustomerAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtTrfCustomercode.ClientID%>').value = eventArgs.get_value();
                    $find('AutoCompleteExtender_txtTrfSourcecountry').set_contextKey(eventArgs.get_value());
                    GetTrfCountryDetails(eventArgs.get_value());

                }
                else {
                    document.getElementById('<%=txtTrfCustomercode.ClientID%>').value = '';
                }
            }
        }

        function MACustomerAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtMACustomercode.ClientID%>').value = eventArgs.get_value();
                    $find('AutoCompleteExtender_txtMASourcecountry').set_contextKey(eventArgs.get_value());

                    GetMACountryDetails(eventArgs.get_value());

                }
                else {
                    document.getElementById('<%=txtMACustomercode.ClientID%>').value = '';
                }
            }
        }


        function GetMACountryDetails(CustCode) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetMACountryDetails",
                data: '{CustCode:  "' + CustCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessMA,
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


        function OnSuccessMA(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var Countries = xml.find("MACountries");
            var rowCount = Countries.length;

            if (rowCount == 1) {
                $.each(Countries, function () {
                    document.getElementById('<%=txtMASourcecountry.ClientID%>').value = ''
                    document.getElementById('<%=txtMASourcecountrycode.ClientID%>').value = '';
                    document.getElementById('<%=txtMASourcecountry.ClientID%>').value = $(this).find("ctryname").text();
                    document.getElementById('<%=txtMASourcecountrycode.ClientID%>').value = $(this).find("ctrycode").text();
                    document.getElementById('<%=txtMASourcecountry.ClientID%>').setAttribute("readonly", true);
                    $find('AutoCompleteExtender_txtMASourcecountry').setAttribute("Enabled", false);
                });
            }
            else {
                document.getElementById('<%=txtMASourcecountry.ClientID%>').value = ''
                document.getElementById('<%=txtMASourcecountrycode.ClientID%>').value = '';
                document.getElementById('<%=txtMASourcecountry.ClientID%>').removeAttribute("readonly");
                $find('AutoCompleteExtender_txtMASourcecountry').setAttribute("Enabled", true);
            }
        };


        function MADeppickupAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtMADeppickupcode.ClientID%>').value = eventArgs.get_value();
                }
                else {
                    document.getElementById('<%=txtMADeppickupcode.ClientID%>').value = '';
                }
            }
        }

        function MATranDeppickupAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtMATransitDeparturepickupcode.ClientID%>').value = eventArgs.get_value();
                }
                else {
                    document.getElementById('<%=txtMATransitDeparturepickupcode.ClientID%>').value = '';
                }
            }
        }

        function TrfDeppickupAutocompleteSelected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                 <%--Added Shahul 19/07/2018--%>
                    var code = eventArgs.get_value();   
                    code = code.split(';');   
                    document.getElementById('<%=txtTrfDeppickupcode.ClientID%>').value =code[0];;
                    document.getElementById('<%=txtTrfDeppickuptype.ClientID%>').value = code[1];   
                }
                else {
                    document.getElementById('<%=txtTrfDeppickupcode.ClientID%>').value = '';
                    document.getElementById('<%=txtTrfDeppickuptype.ClientID%>').value = '';
                }
            }
        }

        function MACountryautocompleteselected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtMASourcecountrycode	.ClientID%>').value = eventArgs.get_value();
                    $find('AutoCompleteExtender_txtMASourcecountry').set_contextKey(eventArgs.get_value());
                }
                else {
                    document.getElementById('<%=txtMASourcecountrycode	.ClientID%>').value = '';
                }
            }
        }
        // added shahul 23/04/18
        function AutoCompleteExtender_txtMASourcecountry_OnClientPopulating(sender, args) {
            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtMACustomercode.ClientID%>').value != '')) {

                sender.set_contextKey(document.getElementById('<%=txtMACustomercode.ClientID%>').value);
            }
        }



        function TrfCountryautocompleteselected(source, eventArgs) {
            if (source) {
                if (eventArgs != null) {
                    document.getElementById('<%=txtTrfSourcecountrycode	.ClientID%>').value = eventArgs.get_value();
                    $find('AutoCompleteExtender_txtTrfSourcecountry').set_contextKey(eventArgs.get_value());
                }
                else {
                    document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>').value = '';
                }
            }
        }

        // added shahul 23/04/18
        function AutoCompleteExtender_txtTrfSourcecountry_OnClientPopulating(sender, args) {
            if ((document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') && (document.getElementById('<%=txtTrfCustomercode.ClientID%>').value != '')) {

                sender.set_contextKey(document.getElementById('<%=txtTrfCustomercode.ClientID%>').value);
            }
        }


        function InterPickupautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                 <%--Added Shahul 19/07/2018--%>
                    var code = eventArgs.get_value();   
                    code = code.split(';');  
                document.getElementById('<%=txtTrfinterPickupcode.ClientID%>').value = code[0]; 
                document.getElementById('<%=txtTrfinterPickuptype.ClientID%>').value = code[1]; 
            }
            else {
                document.getElementById('<%=txtTrfinterPickupcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfinterPickuptype.ClientID%>').value = '';
            }
        }

        function InterDropoffautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                 <%--Added Shahul 19/07/2018--%>
                    var code = eventArgs.get_value();   
                    code = code.split(';'); 
                document.getElementById('<%=txtTrfInterdropffcode.ClientID%>').value = code[0]; 
                document.getElementById('<%=txtTrfInterdropfftype.ClientID%>').value = code[1]; 
            }
            else {
                document.getElementById('<%=txtTrfInterdropffcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfInterdropfftype.ClientID%>').value = '';
            }
        }


        function MAArrivalflightAutocompleteSelected(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMAArrivalflight", "txtMAArrivalflightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtMAArrivalflight", "txtMAArrivalTime");
                var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtMAArrivalflight", "txtMAArrivalpickup");
                var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtMAArrivalflight", "txtMAArrivalpickupcode");



                $get(hiddenfieldID).value = eventArgs.get_value();
                GetMAAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
            }

        }

        <%--Added Shahul 01/11/2018--%>
     function CheckMAFlight(Flightcode) {
   
     if ($("#txtMAArrivalflight").val() != ''){
         $.ajax({
          
            type: "POST",
            url: "Home.aspx/CheckMAFlight",
            data: '{Flightcode:  "' + $("#txtMAArrivalflight").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessMAFlight,
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
    }

     function OnSuccessMAFlight(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Countries = xml.find("MAFlightdetails");
        var rowCount = Countries.length;

        if (rowCount == 0){
             showDialog('Alert Message', 'Arrival Flight Not Exists in the Flight Master Please Select  Proper  Flight.', 'warning');
             document.getElementById('<%=txtMAArrivalflight.ClientID%>').value = '';
            return false;
        }
    };

    <%--Added Shahul 01/11/2018--%>
     function CheckMADepFlight(Flightcode) {

     if ($("#txtMADepartureFlight").val() != ''){
         $.ajax({
          
            type: "POST",
            url: "Home.aspx/CheckMADepFlight",
            data: '{Flightcode:  "' + $("#txtMADepartureFlight").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessMADepFlight,
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
    }

     function OnSuccessMADepFlight(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Countries = xml.find("MADepFlightdetails");
        var rowCount = Countries.length;

        if (rowCount == 0){
             showDialog('Alert Message', 'Departure Flight Not Exists in the Flight Master Please Select  Proper  Flight.', 'warning');
         
            document.getElementById('<%=txtMADepartureFlight.ClientID%>').value = '';
            return false;
        }
    };

   


        function MATranArrivalflightAutocompleteSelected(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMAtranArrFlight", "txtMATranArrFlightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtMAtranArrFlight", "txtMATranArrTime");
                var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtMAtranArrFlight", "txtMAtranArrivalpickup");
                var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtMAtranArrFlight", "txtMATransitArrivalpickupcode");



                $get(hiddenfieldID).value = eventArgs.get_value();
                GetMATransitAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
            }

        }


        function ArrivalflightAutocompleteSelected(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalflightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtArrivalTime");
                var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtTrfArrivalpickup");
                var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtArrivalflight", "txtTrfArrivalpickupcode");

                $get(hiddenfieldID).value = eventArgs.get_value();
                GetAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
            }

        }

                <%--Added Shahul 01/11/2018--%>
     function CheckFlight(Flightcode) {

      if ($("#txtArrivalflight").val() !=''){
         $.ajax({
          
            type: "POST",
            url: "Home.aspx/CheckFlight",
            data: '{Flightcode:  "' + $("#txtArrivalflight").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessFlight,
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
    }

     function OnSuccessFlight(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Countries = xml.find("Flightdetails");
        var rowCount = Countries.length;

        if (rowCount == 0){
             showDialog('Alert Message', 'Kindly choose the correct flight details from the provided Arrival Flight List .', 'warning');
             document.getElementById('<%=txtArrivalflight.ClientID%>').value = '';
            return false;
        }
    };

    <%--Added Shahul 01/11/2018--%>
     function CheckDepFlight(Flightcode) {
     if ($("#txtDepartureFlight").val() !=''){
         $.ajax({
          
            type: "POST",
            url: "Home.aspx/CheckDepFlight",
            data: '{Flightcode:  "' + $("#txtDepartureFlight").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessDepFlight,
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
    }

     function OnSuccessDepFlight(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Countries = xml.find("DepFlightdetails");
        var rowCount = Countries.length;

        if (rowCount == 0){
             showDialog('Alert Message', 'Kindly choose the correct flight details from the provided Departure Flight List .', 'warning');
         
            document.getElementById('<%=txtDepartureFlight.ClientID%>').value = '';
            return false;
        }
    };


        function MADepartureAutocompleteSelected(source, eventArgs) {
            if (source) {

                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMADepartureFlight", "txtMADepartureFlightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtMADepartureFlight", "txtMADepartureTime");
                var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtMADepartureFlight", "txtMADepairportdrop");
                var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtMADepartureFlight", "txtMADepairportdropcode");

                $get(hiddenfieldID).value = eventArgs.get_value();
                GetMADepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
            }

        }

        function MATranDepartureAutocompleteSelected(source, eventArgs) {
            if (source) {

                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtMAtranDepartureFlight", "txtMATranDepartureFlightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtMAtranDepartureFlight", "txtMATranDepartureTime");
                var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtMAtranDepartureFlight", "txtMAtranDeppickup");
                var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtMAtranDepartureFlight", "txtMATransitDeparturepickupcode");

                $get(hiddenfieldID).value = eventArgs.get_value();
                GetMATransitDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
            }

        }


        function GetMATransitDepartureAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetMATransitDepartureAirportAndTimeDetails",
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


        function GetMADepartureAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetMADepartureAirportAndTimeDetails",
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


        function DepartureAutocompleteSelected(source, eventArgs) {
            if (source) {

                var hiddenfieldID = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtDepartureFlightCode");
                var hiddenfieldIDTime = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtDepartureTime");
                var hiddenfieldIDAirport = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtTrfDepairportdrop");
                var hiddenfieldIDAirportcode = source.get_id().replace("AutoCompleteExtender_txtDepartureFlight", "txtTrfDepairportdropcode");
                $get(hiddenfieldID).value = eventArgs.get_value();
                GetDepartureAirportAndTimeDetails($get(hiddenfieldID).value, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode);
            }

        }


        function GetDepartureAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetDepartureAirportAndTimeDetails",
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

        function GetMATransitAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetMATransitAirportAndTimeDetails",
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

        function GetMAAirportAndTimeDetails(flightcode, hiddenfieldIDTime, hiddenfieldIDAirport, hiddenfieldIDAirportcode) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetMAAirportAndTimeDetails",
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


        function HotelStarsNameautocompleteselected(source, eventArgs) {
            if (eventArgs != null) {
                document.getElementById('<%=txtHotelStarscode.ClientID%>').value = eventArgs.get_value();

            }
            else {
                document.getElementById('<%=txtHotelStarscode.ClientID%>').value = '';
            }
            SethotelContextkey();
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


        function AutoCompleteExtender_TrfInterpickup_KeyUp() {
            $("#<%= txtTrfinterPickup.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfinterPickupcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfinterPickup.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfinterPickuptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });

            $("#<%= txtTrfinterPickup.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfinterPickupcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfinterPickup.ClientID%>');
                 var hiddenfieldID2 = document.getElementById('<%=txtTrfinterPickuptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });
        }

        function AutoCompleteExtender_TrfInterDrop_KeyUp() {
            $("#<%= txtTrfInterdropff.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfInterdropffcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfInterdropff.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfInterdropfftype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });

            $("#<%= txtTrfInterdropff.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfInterdropffcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfInterdropff.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfInterdropfftype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });
        }


        function AutoCompleteExtender_OtherCountry_KeyUp() {

            $("#<%= txtothSourceCountry.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtothSourceCountry.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtothSourceCountryCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtothSourceCountry.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtothSourceCountry.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtothSourceCountryCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

        }

        function AutoCompleteExtender_OtherCustomer_KeyUp() {

            $("#<%= txtothCustomer.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtothCustomer.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtothCustomercode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtothCustomer.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtothCustomer.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtothCustomercode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

        }


        function AutoCompleteExtender_Othergroup_KeyUp() {

            $("#<%= txtothgroup.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtothgroup.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtothgroupcode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtothgroup.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtothgroup.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtothgroupcode.ClientID%>');
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
//                added shahul 27/06/18
                var chkshow = document.getElementById('<%=chkshowall.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    //                added shahul 27/06/18
                    
                    chkshow.removeAttribute('disabled');
                }
                SethotelContextkey();
            });
            $("#<%= txtHotelName.ClientID %>").keyup("change", function () {

                var hiddenfieldID1 = document.getElementById('<%=txtHotelName.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelCode.ClientID%>');
                //                added shahul 27/06/18
                var chkshow = document.getElementById('<%=chkshowall.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    //                added shahul 27/06/18
                   
                    chkshow.removeAttribute('disabled');
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

            $("#<%= txtHotelStars.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtHotelStars.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtHotelStarscode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
                SethotelContextkey();
            });
        }

        function AutoCompleteExtender_ArrivalFlight_KeyUp() {
            $("#<%= txtArrivalflight.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtArrivalflight.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtArrivalflightCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtArrivalflight.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtArrivalflight.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtArrivalflightCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }

        function AutoCompleteExtender_DepartureFlight_KeyUp() {
            $("#<%= txtDepartureFlight.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtDepartureFlight.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtDepartureFlightCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtDepartureFlight.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtDepartureFlight.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtDepartureFlightCode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }




        function AutoCompleteExtender_TrfArrivalpickupcode_KeyUp() {
            $("#<%= txtTrfArrivalpickup.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfArrivalpickup.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtTrfArrivalpickup.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfArrivalpickup.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }


        function AutoCompleteExtender_TrfArrDropoff_KeyUp() {
            $("#<%= txtTrfArrDropoff.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfArrDropoff.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfArrDroptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });

            $("#<%= txtTrfArrDropoff.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfArrDropoff.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfArrDroptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });
        }

        function AutoCompleteExtender_TrfCustomer_KeyUp() {
            $("#<%= txtTrfCustomer.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfCustomer.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfCustomercode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtTrfCustomer.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfCustomer.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfCustomercode.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }


        function AutoCompleteExtender_TrfSourcecountry_KeyUp() {
            $("#<%= txtTrfSourcecountry.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfSourcecountry.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtTrfSourcecountry.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfSourcecountry.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }


        function AutoCompleteExtender_TrfDepairportdrop_KeyUp() {
            $("#<%= txtTrfDepairportdrop.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfDepairportdrop.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });

            $("#<%= txtTrfDepairportdrop.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfDepairportdrop.ClientID%>');
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                }
            });
        }

        function AutoCompleteExtender_TrfDeppickup_KeyUp() {
            $("#<%= txtTrfDeppickup.ClientID %>").bind("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfDeppickupcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfDeppickup.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfDeppickuptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                    hiddenfieldID2.value = '';
                }
            });

            $("#<%= txtTrfDeppickup.ClientID %>").keyup("change", function () {
                var hiddenfieldID1 = document.getElementById('<%=txtTrfDeppickupcode.ClientID%>');
                var hiddenfieldID = document.getElementById('<%=txtTrfDeppickup.ClientID%>');
                var hiddenfieldID2 = document.getElementById('<%=txtTrfDeppickuptype.ClientID%>');  <%--Added Shahul 19/07/2018--%>
                if (hiddenfieldID1.value == '') {
                    hiddenfieldID.value = '';
                     hiddenfieldID2.value = '';
                }
            });
        }


        function GetHotelsDetails(HotelCode) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetHotelsDetails",
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

                document.getElementById('<%=txtDestinationcode.ClientID%>').value = $(this).find("sectorcode").text() + '|Area';
                document.getElementById('<%=txtDestinationName.ClientID%>').value = $(this).find("sectorname").text();
                document.getElementById('<%=txtHotelStarscode.ClientID%>').value = $(this).find("catcode").text();
                document.getElementById('<%=txtHotelStars.ClientID%>').value = $(this).find("catname").text();
            });

        };


        function GetTrfCountryDetails(CustCode) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetTrfCountryDetails",
                data: '{CustCode:  "' + CustCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessTrf,
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

        function OnSuccessTrf(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var Countries = xml.find("TrfCountries");
            var rowCount = Countries.length;

            if (rowCount == 1) {
                $.each(Countries, function () {
                    document.getElementById('<%=txtTrfSourcecountry.ClientID%>').value = ''
                    document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>').value = '';
                    document.getElementById('<%=txtTrfSourcecountry.ClientID%>').value = $(this).find("ctryname").text();
                    document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>').value = $(this).find("ctrycode").text();
                    document.getElementById('<%=txtTrfSourcecountry.ClientID%>').setAttribute("readonly", true);
                    $find('AutoCompleteExtender_txtTrfSourcecountry').setAttribute("Enabled", false);
                });
            }
            else {
                document.getElementById('<%=txtTrfSourcecountry.ClientID%>').value = ''
                document.getElementById('<%=txtTrfSourcecountrycode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfSourcecountry.ClientID%>').removeAttribute("readonly");
                $find('AutoCompleteExtender_txtTrfSourcecountry').setAttribute("Enabled", true);
            }
        };



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


        function GetTourCountryDetails(CustCode) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/GetCountryDetails",
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


        function GetOthCountryDetails(CustCode) {

            $.ajax({
                type: "POST",
                url: "Home.aspx/GetOtherCountryDetails",
                data: '{CustCode:  "' + CustCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnOthSuccess,
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

        function OnOthSuccess(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var Countries = xml.find("OthCountries");
            var rowCount = Countries.length;

            if (rowCount == 1) {
                $.each(Countries, function () {
                    document.getElementById('<%=txtothSourceCountry.ClientID%>').value = ''
                    document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = '';
                    document.getElementById('<%=txtothSourceCountry.ClientID%>').value = $(this).find("ctryname").text();
                    document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = $(this).find("ctrycode").text();
                    document.getElementById('<%=txtothSourceCountry.ClientID%>').setAttribute("readonly", true);
                    $find('AutoCompleteExtender_txtothSourceCountry').setAttribute("Enabled", false);
                });
            }
            else {
                document.getElementById('<%=txtothSourceCountry.ClientID%>').value = ''
                document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = '';
                document.getElementById('<%=txtothSourceCountry.ClientID%>').removeAttribute("readonly");
                $find('AutoCompleteExtender_txtothSourceCountry').setAttribute("Enabled", true);
            }
        };


    </script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestUserControl);
        function EndRequestUserControl(sender, args) {

            AutoCompleteExtender_HotelNameKeyUp();
            AutoCompleteExtenderKeyUp();
            AutoCompleteExtender_HotelStarsKeyUp();
            AutoCompleteExtender_Customer_KeyUp();
            AutoCompleteExtender_Country_KeyUp();

            AutoCompleteExtender_UAELocationeKeyUp();
            AutoCompleteExtender_PreHotelNameKeyUp();
            AutoCompleteExtender_PreHotelCustomer_KeyUp();
            AutoCompleteExtender_PrehotelCountry_KeyUp();


            AutoCompleteExtender_TourStartingFrom_KeyUp();
            AutoCompleteExtender_TourClassification_KeyUp();
            AutoCompleteExtender_TourCustomer_KeyUp();
            AutoCompleteExtender_TourCountry_KeyUp();


            AutoCompleteExtender_ArrivalFlight_KeyUp();
            AutoCompleteExtender_DepartureFlight_KeyUp();
            AutoCompleteExtender_TrfDeppickup_KeyUp();
            AutoCompleteExtender_TrfDepairportdrop_KeyUp();
            AutoCompleteExtender_TrfSourcecountry_KeyUp();
            AutoCompleteExtender_TrfCustomer_KeyUp();
            AutoCompleteExtender_TrfArrivalpickupcode_KeyUp();
            AutoCompleteExtender_TrfArrDropoff_KeyUp();


            AutoCompleteExtender_TrfInterpickup_KeyUp();
            AutoCompleteExtender_TrfInterDrop_KeyUp();

            AutoCompleteExtender_Othergroup_KeyUp();
            AutoCompleteExtender_OtherCustomer_KeyUp();
            AutoCompleteExtender_OtherCountry_KeyUp();

            SethotelContextkey();

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

            $("#<%= ddlPreHotelchild.ClientID %>").bind("change", function () {
                ShowPreHotelChildAge();
            });

        }

        function RefreshContent() {
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandlerForProgressBar);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandlerForProgressBar);

        }
        function BeginRequestHandlerForProgressBar() {
            //alert('test');
            //ShowProgess();


        }
        //        function HideRoomLoders() {
        //            alert(1);
        //            var loder161 = document.getElementById('<%=loder16.ClientID%>')  // $find("loder16");
        //            loder161.setAttribute('style', 'display:none');
        //        }

        function EndRequestHandlerForProgressBar() {
            //            //  HideProgess();
            //            var loder161 = document.getElementById('<%=loder16.ClientID%>')  // $find("loder16");
            //            loder161.setAttribute('style', 'display:none');
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

            $("#<%= ddlPreHotelchild.ClientID %>").bind("change", function () {
                ShowPreHotelChildAge();
            });

        });

    </script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {

            $('#dvFullAdultChild').hide();

            function GetParameterValues(param) {

                var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');

                for (var i = 0; i < url.length; i++) {
                    var urlparam = url[i].split('=');
                    if (urlparam[0] == param) {
                        return (urlparam[1]);
                    }
                }
            }

            $('.srch-lbl').closest('.search-tab-content').find('.search-asvanced').fadeIn();
            $('.srch-lbl').text('Close Search Options').addClass('open');

            AutoCompleteExtender_HotelNameKeyUp();

            AutoCompleteExtenderKeyUp();

            AutoCompleteExtender_HotelStarsKeyUp();
            AutoCompleteExtender_Customer_KeyUp();
            AutoCompleteExtender_Country_KeyUp();

            AutoCompleteExtender_UAELocationeKeyUp();
            AutoCompleteExtender_PreHotelNameKeyUp();
            AutoCompleteExtender_PreHotelCustomer_KeyUp();
            AutoCompleteExtender_PrehotelCountry_KeyUp();


            AutoCompleteExtender_TourStartingFrom_KeyUp();
            AutoCompleteExtender_TourClassification_KeyUp();
            AutoCompleteExtender_TourCustomer_KeyUp();
            AutoCompleteExtender_Country_KeyUp();

            AutoCompleteExtender_Othergroup_KeyUp();
            AutoCompleteExtender_OtherCustomer_KeyUp();
            AutoCompleteExtender_OtherCountry_KeyUp();

            fillTourDates();
            fillOtherDates();

            $('#dvTourChild').hide();

            $('#divTrfChild').hide();

            $('#divMAChild').hide();

            $('#divOthChild').hide();

            $('#divPreHotelChild').hide();
            $("#<%= ddlTourChildren.ClientID %>").bind("change", function () {
                ShowTourChild();
            });

            $("#<%= ddlTrfchild.ClientID %>").bind("change", function () {
                ShowTrfChild();
            });

            $("#<%= ddlMAchild.ClientID %>").bind("change", function () {
                ShowMAChild();
            });

            $("#<%= ddlOthchild.ClientID %>").bind("change", function () {
                ShowOthChild();
            });



            $("#<%= txtTrfArrivaldate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chkarrival.ClientID%>');

                d.checked = true;

            });

            $("#<%= txtTrfDeparturedate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chkDeparture.ClientID%>');

                d.checked = true;

            });

            $("#<%= txtTrfinterdate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chkinter.ClientID%>');

                d.checked = true;

            });


            $("#<%= txtMAArrivaldate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chkMAarrival.ClientID%>');

                d.checked = true;

            });

            $("#<%= txtMADeparturedate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chkMADeparture.ClientID%>');

                d.checked = true;

            });

            $("#<%= txtTransitarrdate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chktransit.ClientID%>');

                d.checked = true;

            });

            $("#<%= txtMATrandepdate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=chktransit.ClientID%>');

                d.checked = true;

            });

            var Tab = GetParameterValues('Tab');
            if (Tab == 1 || Tab == 2 || Tab == 3 || Tab == 4) { // if (Tab == 0 || Tab == 1 || Tab == 1 || Tab == 1) {

                $('.search-tab-content').hide().eq(Tab).fadeIn();
                $('.search-tab').removeClass('active').eq(Tab).addClass('active');
                if (Tab == 1) {
                    ShowTourChild();
                }
                if (Tab == 2) {

                    ShowMAChild();
                }

                if (Tab == 3) {
                    ShowTrfChild();
                }

                if (Tab == 4) {
                    ShowOthChild();
                }

            }

            $("#<%= txtCheckIn.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=txtCheckIn.ClientID%>').value;


                var dp = d.split("/");
//                var dateOut = new Date(dp[2], dp[1], dp[0]);
//                var currentMonth = dateOut.getMonth() - 1;

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
//                var dateOut = new Date(dp[2], dp[1], dp[0]);
//                var currentMonth = dateOut.getMonth() - 1;

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


            $("#<%= txtOthFromDate.ClientID %>").bind("change", function () {
                var d = document.getElementById('<%=txtOthFromDate.ClientID%>').value;

                var dp = d.split("/");
                 var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;
                var currentDate = dateOut.getDate();
                var currentYear = dateOut.getFullYear();
                // alert(currentMonth);
                $(".date-inpt-oth-to").datepicker("destroy");

                var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
                if ((dCheckOutMax == '') || (dCheckOutMax == '0')) {
                    $(".date-inpt-oth-to").datepicker({
                        minDate: new Date(currentYear, currentMonth, currentDate)
                    });
                }
                else {
                    var dp4 = dCheckOutMax.split("/");
                    var date4 = new Date(dp4[2], dp4[1], dp4[0]);
                    var currentMonth4 = date4.getMonth() - 1;
                    var currentDate4 = date4.getDate();
                    var currentYear4 = date4.getFullYear();

                    $(".date-inpt-oth-to").datepicker({
                        minDate: new Date(currentYear, currentMonth, currentDate),
                        maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                    });
                }

            });


            $("#<%= txtTourFromDate.ClientID %>").bind("change", function () {

                BindToDateCalendar();


            });

            function BindToDateCalendar() {
                var d = document.getElementById('<%=txtTourFromDate.ClientID%>').value;

                var dp = d.split("/");
                var dateOut = new Date(dp[2] + "/" + dp[1] + "/" + dp[0]);       //modified abin 09/12/2018   
                var currentMonth = dateOut.getMonth();                           //dateOut.getMonth() - 1;
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
                    var date4 = new Date(dp4[2], dp4[1], dp4[0]);
                    var currentMonth4 = date4.getMonth() - 1;
                    var currentDate4 = date4.getDate();
                    var currentYear4 = date4.getFullYear();

                    $(".date-inpt-tour-to").datepicker({
                        minDate: new Date(currentYear, currentMonth, currentDate),
                        maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                    });
                }
            }
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

                document.getElementById('<%=txtTourChild1.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild2.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild3.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild4.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild5.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild6.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild7.ClientID%>').value = '';
                document.getElementById('<%=txtTourChild8.ClientID%>').value = '';

                $('#dvTourChild').hide();

            });


            $("#btnMAreset").button().click(function () {

                document.getElementById('<%=chkMADeparture.ClientID%>').checked = false;
                document.getElementById('<%=chkMAarrival.ClientID%>').checked = false;
                document.getElementById('<%=chktransit.ClientID%>').checked = false;

                document.getElementById('<%=txtMADeparturedate.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrivaldate.ClientID%>').value = '';
                document.getElementById('<%=txtTransitarrdate.ClientID%>').value = '';
                document.getElementById('<%=txtMATrandepdate.ClientID%>').value = '';

                document.getElementById('<%=txtMADepairportdropcode.ClientID%>').value = '';
                document.getElementById('<%=txtMADepairportdrop.ClientID%>').value = '';
                document.getElementById('<%=txtMADepartureFlightCode.ClientID%>').value = '';
                document.getElementById('<%=txtMADepartureFlight.ClientID%>').value = '';
                document.getElementById('<%=txtMADepartureTime.ClientID%>').value = '';
                document.getElementById('<%=txtMADeppickup.ClientID%>').value = '';
                document.getElementById('<%=txtMADeppickupcode.ClientID%>').value = '';

                document.getElementById('<%=txtMAArrDropoffcode.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrDropoff.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrivalflightCode.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrivalflight.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrivalTime.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrivalpickup.ClientID%>').value = '';
                document.getElementById('<%=txtMAArrivalpickupcode.ClientID%>').value = '';

                document.getElementById('<%=txtMAtranArrFlight.ClientID%>').value = '';
                document.getElementById('<%=txtMATranArrFlightCode.ClientID%>').value = '';
                document.getElementById('<%=txtMATranDepartureFlightCode.ClientID%>').value = '';
                document.getElementById('<%=txtMAtranDepartureFlight.ClientID%>').value = '';

                document.getElementById('<%=txtMAtranArrivalpickup.ClientID%>').value = '';
                document.getElementById('<%=txtMATransitArrivalpickupcode.ClientID%>').value = '';

                document.getElementById('<%=txtMATranArrTime.ClientID%>').value = '';
                document.getElementById('<%=txtMATranDepartureTime.ClientID%>').value = '';
                document.getElementById('<%=txtMAtranDeppickup.ClientID%>').value = '';
                document.getElementById('<%=txtMATransitDeparturepickupcode.ClientID%>').value = '';


                var ddlMAAdult = document.getElementById('<%=ddlMAAdult.ClientID%>');
                ddlMAAdult.selectedIndex = "0";
                $('.custom-select-ddlMAAdult').next('span').children('.customSelectInner').text(jQuery("#ddlMAAdult :selected").text());

                var ddlMAChild = document.getElementById('<%=ddlMAChild.ClientID%>');
                ddlMAChild.selectedIndex = "0";
                $('.custom-select-ddlMAChild').next('span').children('.customSelectInner').text(jQuery("#ddlMAChild :selected").text());


                document.getElementById('<%=txtMAChild1.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild2.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild3.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild4.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild5.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild6.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild7.ClientID%>').value = '';
                document.getElementById('<%=txtMAChild8.ClientID%>').value = '';


                $('#divMAchild').hide();


            });


            $("#btnOthreset").button().click(function () {

                document.getElementById('<%=txtothFromDate.ClientID%>').value = '';
                document.getElementById('<%=txtothToDate.ClientID%>').value = '';
                document.getElementById('<%=txtothgroupcode.ClientID%>').value = '';
                document.getElementById('<%=txtothgroup.ClientID%>').value = '';


                if (document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') {
                    document.getElementById('<%=txtothCustomer.ClientID%>').value = ''
                    document.getElementById('<%=txtothCustomercode.ClientID%>').value = '';
                }

                document.getElementById('<%=txtothSourceCountry.ClientID%>').value = '';
                document.getElementById('<%=txtothSourceCountryCode.ClientID%>').value = '';




                var ddlothAdult = document.getElementById('<%=ddlOthAdult.ClientID%>');
                ddlothAdult.selectedIndex = "0";
                $('.custom-select-ddlOthAdult').next('span').children('.customSelectInner').text(jQuery("#ddlOthAdult :selected").text());

                var ddlOthChild = document.getElementById('<%=ddlOthChild.ClientID%>');
                ddlOthChild.selectedIndex = "0";
                $('.custom-select-ddlOthChild').next('span').children('.customSelectInner').text(jQuery("#ddlOthChild :selected").text());

                document.getElementById('<%=txtOthChild1.ClientID%>').value = '';
                document.getElementById('<%=txtOthChild2.ClientID%>').value = '';
                document.getElementById('<%=txtOthChild3.ClientID%>').value = '';
                document.getElementById('<%=txtOthChild4.ClientID%>').value = '';
                document.getElementById('<%=txtOthChild5.ClientID%>').value = '';
                document.getElementById('<%=txtOthChild6.ClientID%>').value = '';
                document.getElementById('<%=txtOthChild7.ClientID%>').value = '';
                document.getElementById('<%=txtOthChild8.ClientID%>').value = '';

                $('#divOthChild').hide();

            });

            $("#btnTrfreset").button().click(function () {

                document.getElementById('<%=chkDeparture.ClientID%>').checked = false;
                document.getElementById('<%=chkarrival.ClientID%>').checked = false;




                document.getElementById('<%=txtTrfDeparturedate.ClientID%>').value = '';
                document.getElementById('<%=txtTrfArrivaldate.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDepairportdropcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDepairportdrop.ClientID%>').value = '';
                document.getElementById('<%=txtDepartureFlightCode.ClientID%>').value = '';
                document.getElementById('<%=txtDepartureFlight.ClientID%>').value = '';
                document.getElementById('<%=txtDepartureTime.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDeppickup.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDeppickupcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfDeppickuptype.ClientID%>').value = ''; <%--Added Shahul 19/07/2018--%>

                document.getElementById('<%=txtTrfArrDropoffcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfArrDroptype.ClientID%>').value = '';  <%--Added Shahul 19/07/2018--%>
                document.getElementById('<%=txtTrfArrDropoff.ClientID%>').value = '';
                document.getElementById('<%=txtArrivalflightCode.ClientID%>').value = '';
                document.getElementById('<%=txtArrivalflight.ClientID%>').value = '';
                document.getElementById('<%=txtArrivalTime.ClientID%>').value = '';
                document.getElementById('<%=txtTrfArrivalpickup.ClientID%>').value = '';
                document.getElementById('<%=txtTrfArrivalpickupcode.ClientID%>').value = '';

                var ddlTrfAdult = document.getElementById('<%=ddlTrfAdult.ClientID%>');
                ddlTrfAdult.selectedIndex = "0";
                $('.custom-select-ddlTrfAdult').next('span').children('.customSelectInner').text(jQuery("#ddlTrfAdult :selected").text());

                var ddlTrfChild = document.getElementById('<%=ddlTrfChild.ClientID%>');
                ddlTrfChild.selectedIndex = "0";
                $('.custom-select-ddlTrfChild').next('span').children('.customSelectInner').text(jQuery("#ddlTrfChild :selected").text());

                document.getElementById('<%=txtTrfChild1.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild2.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild3.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild4.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild5.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild6.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild7.ClientID%>').value = '';
                document.getElementById('<%=txtTrfChild8.ClientID%>').value = '';

                $('#divTrfchild').hide();

                document.getElementById('<%=chkinter.ClientID%>').checked = false;
                document.getElementById('<%=txtTrfinterdate.ClientID%>').value = '';
                document.getElementById('<%=txtTrfinterPickup.ClientID%>').value = '';
                document.getElementById('<%=txtTrfinterPickupcode.ClientID%>').value = '';
                document.getElementById('<%=txtTrfinterPickuptype.ClientID%>').value = ''; <%--Added Shahul 19/07/2018--%>
                document.getElementById('<%=txtTrfInterdropff.ClientID%>').value = '';
                document.getElementById('<%=txtTrfInterdropffcode.ClientID%>').value = '';

            });



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


                $('#divPreHotelChild').hide();

            });



            $("#btnReset").button().click(function () {



                document.getElementById('<%=txtDestinationName.ClientID%>').value = '';
                document.getElementById('<%=txtDestinationCode.ClientID%>').value = '';
                document.getElementById('<%=txtHotelName.ClientID%>').value = '';
                document.getElementById('<%=txtHotelCode.ClientID%>').value = '';
                document.getElementById('<%=txtCheckIn.ClientID%>').value = '';
                document.getElementById('<%=txtCheckOut.ClientID%>').value = '';
                document.getElementById('<%=txtNoOfnights.ClientID%>').value = '';
//                //        *** 26/082018 Danny

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
                ddlAvailability.selectedIndex = "0";
                $('.custom-select-ddlAvailability').next('span').children('.customSelectInner').text(jQuery("#ddlAvailability :selected").text());

                var ddlPropertType = document.getElementById('<%=ddlPropertType.ClientID%>');
                ddlPropertType.selectedIndex = "0";
                $('.custom-select-ddlPropertType').next('span').children('.customSelectInner').text(jQuery("#ddlPropertType :selected").text());


                var ddlOrderBy = document.getElementById('<%=ddlOrderBy.ClientID%>');
                ddlOrderBy.selectedIndex = "0";
                $('.custom-select-ddlOrderBy').next('span').children('.customSelectInner').text(jQuery("#ddlOrderBy :selected").text());

                if (document.getElementById('<%=hdLoginType.ClientID%>').value == 'RO') {
                    document.getElementById('<%=txtCustomer.ClientID%>').value = ''
                    document.getElementById('<%=txtCustomerCode.ClientID%>').value = '';
                }



                document.getElementById('<%=txtCountry.ClientID%>').value = ''
                document.getElementById('<%=txtCountryCode.ClientID%>').value = '';

                // *** Hotel child age change based on room on 23/05/2017

//                //        *** 26/082018 Danny
                 var ddlRoom_Dynamic = document.getElementById('<%=ddlRoom_Dynamic.ClientID%>');
                ddlRoom_Dynamic.selectedIndex = "0";
                $('.custom-select-ddlroom').next('span').children('.customSelectInner').text(jQuery("#ddlRoom_Dynamic :selected").text());
                ClearRoomAdultChild();
               
//                $('#dvFullAdultChild').hide();
                //***
                SethotelContextkey();
            });

            // *** Hotel child age change based on room on 22/05/2017

//           //        *** 26/082018 Danny


           

            
        });
        //***-------------------------------------- End

        
////        *** 26/082018 Danny
////        *** 26/082018 Danny
        function ClearRoomAdultChild() {
          $("[id*=dlNofoRooms]").remove();
            return false;
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


        function ValidateRoomChildAges(ChildNo, Room) {


            if (ChildNo != '0') {
                var vRoom = 'Room' + Room;
                var vChild = 'Child' + ChildNo;

                var vChild1 = ('txtRoom1Child1').replace('Room1', vRoom)
                var vChild2 = ('txtRoom1Child2').replace('Room1', vRoom)
//               //        *** 26/082018 Danny

                var roomchild1 = document.getElementById(vChild1).value;
                var roomchild2 = document.getElementById(vChild2).value;
                var roomchild3 = document.getElementById(vChild3).value;
                var roomchild4 = document.getElementById(vChild4).value;
                var roomchild5 = document.getElementById(vChild5).value;
                var roomchild6 = document.getElementById(vChild6).value;

                if (ChildNo == 1) {

                    if (roomchild1 == 0) {
                        showDialog('Alert Message', 'Please select ' + vRoom + ' Child1  Age.', 'warning');
                        HideProgess();
                        return false;
                    }

                }
                else if (ChildNo == 2) {
                    if (roomchild1 == 0 || roomchild2 == 0) {
                        if (roomchild1 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child1  Age.', 'warning');

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
                        if (roomchild5 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + '  Child5 Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                }
                else if (ChildNo == 6) {
                    if (roomchild1 == 0 || roomchild2 == 0 || roomchild3 == 0 || roomchild4 == 0 || roomchild5 == 0 || roomchild6 == 0) {
                        if (roomchild1 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child1  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild2 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child2  Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild3 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child3 Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild4 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child4 Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild5 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child5 Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                        if (roomchild6 == 0) {
                            showDialog('Alert Message', 'Please select ' + vRoom + ' Child6 Age.', 'warning');
                            HideProgess();
                            return false;
                        }
                    }
                }

            }
        }
        /// **** ----------------------------------End

        //////////////////////
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
                $('#dvTourChild').show();

            }

        }


        function ShowOthChild() {
            var child = $("#<%= ddlOthChild.ClientID %>").val()

            if (child == 0) {
                $('#divOthChild').hide();
            }
            else if (child == 1) {

                $('#divOthChild1').show();
                $('#divOthChild2').hide();
                $('#divOthChild3').hide();
                $('#divOthChild4').hide();
                $('#divOthChild5').hide();
                $('#divOthChild6').hide();
                $('#divOthChild7').hide();
                $('#divOthChild8').hide();
                $('#divOthChild').show();
            }
            else if (child == 2) {
                $('#divOthChild1').show();
                $('#divOthChild2').show();
                $('#divOthChild3').hide();
                $('#divOthChild4').hide();
                $('#divOthChild5').hide();
                $('#divOthChild6').hide();
                $('#divOthChild7').hide();
                $('#divOthChild8').hide();
                $('#divOthChild').show();

            }
            else if (child == 3) {

                $('#divOthChild1').show();
                $('#divOthChild2').show();
                $('#divOthChild3').show();
                $('#divOthChild4').hide();
                $('#divOthChild5').hide();
                $('#divOthChild6').hide();
                $('#divOthChild7').hide();
                $('#divOthChild8').hide();
                $('#divOthChild').show();

            }
            else if (child == 4) {

                $('#divOthChild1').show();
                $('#divOthChild2').show();
                $('#divOthChild3').show();
                $('#divOthChild4').show();
                $('#divOthChild5').hide();
                $('#divOthChild6').hide();
                $('#divOthChild7').hide();
                $('#divOthChild8').hide();
                $('#divOthChild').show();

            }
            else if (child == 5) {

                $('#divOthChild1').show();
                $('#divOthChild2').show();
                $('#divOthChild3').show();
                $('#divOthChild4').show();
                $('#divOthChild5').show();
                $('#divOthChild6').hide();
                $('#divOthChild7').hide();
                $('#divOthChild8').hide();
                $('#divOthChild').show();

            }
            else if (child == 6) {

                $('#divOthChild1').show();
                $('#divOthChild2').show();
                $('#divOthChild3').show();
                $('#divOthChild4').show();
                $('#divOthChild5').show();
                $('#divOthChild6').show();
                $('#divOthChild7').hide();
                $('#divOthChild8').hide();
                $('#divOthChild').show();

            }
            else if (child == 7) {

                $('#divOthChild1').show();
                $('#divOthChild2').show();
                $('#divOthChild3').show();
                $('#divOthChild4').show();
                $('#divOthChild5').show();
                $('#divOthChild6').show();
                $('#divOthChild7').show();
                $('#divOthChild8').hide();
                $('#divOthChild').show();

            }
            else if (child == 8) {

                $('#divOthChild1').show();
                $('#divOthChild2').show();
                $('#divOthChild3').show();
                $('#divOthChild4').show();
                $('#divOthChild5').show();
                $('#divOthChild6').show();
                $('#divOthChild7').show();
                $('#divOthChild8').show();
                $('#divOthChild').show();

            }

        }

        function ShowTrfChild() {
            var child = $("#<%= ddlTrfChild.ClientID %>").val()

            if (child == 0) {
                $('#divTrfchild').hide();
            }
            else if (child == 1) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').hide();
                $('#dvTrfChild3').hide();
                $('#dvTrfChild4').hide();
                $('#dvTrfChild5').hide();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();
            }
            else if (child == 2) {
                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').hide();
                $('#dvTrfChild4').hide();
                $('#dvTrfChild5').hide();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 3) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').hide();
                $('#dvTrfChild5').hide();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 4) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').hide();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 5) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').show();
                $('#dvTrfChild6').hide();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 6) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').show();
                $('#dvTrfChild6').show();
                $('#dvTrfChild7').hide();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 7) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').show();
                $('#dvTrfChild6').show();
                $('#dvTrfChild7').show();
                $('#dvTrfChild8').hide();
                $('#divTrfchild').show();

            }
            else if (child == 8) {

                $('#dvTrfChild1').show();
                $('#dvTrfChild2').show();
                $('#dvTrfChild3').show();
                $('#dvTrfChild4').show();
                $('#dvTrfChild5').show();
                $('#dvTrfChild6').show();
                $('#dvTrfChild7').show();
                $('#dvTrfChild8').show();
                $('#divTrfchild').show();

            }

        }

        function ShowMAChild() {
            var child = $("#<%= ddlMAChild.ClientID %>").val()

            if (child == 0) {
                $('#divMAchild').hide();
            }
            else if (child == 1) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').hide();
                $('#dvMAChild3').hide();
                $('#dvMAChild4').hide();
                $('#dvMAChild5').hide();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();
            }
            else if (child == 2) {
                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').hide();
                $('#dvMAChild4').hide();
                $('#dvMAChild5').hide();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 3) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').hide();
                $('#dvMAChild5').hide();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 4) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').hide();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 5) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').show();
                $('#dvMAChild6').hide();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 6) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').show();
                $('#dvMAChild6').show();
                $('#dvMAChild7').hide();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 7) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').show();
                $('#dvMAChild6').show();
                $('#dvMAChild7').show();
                $('#dvMAChild8').hide();
                $('#divMAchild').show();

            }
            else if (child == 8) {

                $('#dvMAChild1').show();
                $('#dvMAChild2').show();
                $('#dvMAChild3').show();
                $('#dvMAChild4').show();
                $('#dvMAChild5').show();
                $('#dvMAChild6').show();
                $('#dvMAChild7').show();
                $('#dvMAChild8').show();
                $('#divMAchild').show();

            }

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
                var date1 = new Date(dp1[2], dp1[1], dp1[0]);
                var currentMonth1 = date1.getMonth() - 1;
                var currentDate1 = date1.getDate();
                var currentYear1 = date1.getFullYear();

                var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
                var dp4 = dCheckOutMax.split("/");
                var date4 = new Date(dp4[2], dp4[1], dp4[0]);
                var currentMonth4 = date4.getMonth() - 1;
                var currentDate4 = date4.getDate();
                var currentYear4 = date4.getFullYear();


                $(".date-inpt-tour-from").datepicker({
                    minDate: new Date(currentYear1, currentMonth1, currentDate1),
                    maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                });


            }


        }


        function fillOtherDates() {
            var dCheckInMin = document.getElementById('<%=hdCheckInPrevDay.ClientID%>').value;
            var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
            if ((dCheckInMin == '') || (dCheckInMin == '0') || (dCheckOutMax == '') || (dCheckOutMax == '0')) {

                var date = new Date();
                var currentMonth = date.getMonth();
                var currentDate = date.getDate();
                var currentYear = date.getFullYear();

                $(".date-inpt-oth-from").datepicker({

                    minDate: new Date(currentYear, currentMonth, currentDate)

                });
            }
            else {


                var dp1 = dCheckInMin.split("/");
                var date1 = new Date(dp1[2], dp1[1], dp1[0]);
                var currentMonth1 = date1.getMonth() - 1;
                var currentDate1 = date1.getDate();
                var currentYear1 = date1.getFullYear();

                var dCheckOutMax = document.getElementById('<%=hdCheckOutNextDay.ClientID%>').value;
                var dp4 = dCheckOutMax.split("/");
                var date4 = new Date(dp4[2], dp4[1], dp4[0]);
                var currentMonth4 = date4.getMonth() - 1;
                var currentDate4 = date4.getDate();
                var currentYear4 = date4.getFullYear();

                $(".date-inpt-oth-from").datepicker({
                    minDate: new Date(currentYear1, currentMonth1, currentDate1),
                    maxDate: new Date(currentYear4, currentMonth4, currentDate4)
                });
            }


        }


        function fnMyAccountClick()
        {
        alert('sss');
        window.location.href("MyAccount.aspx");
      //   document.getElementById("<%= btnMyAccount.ClientID %>").click();
        // alert(document.getElementById("<%= btnMyAccount.ClientID %>"))
        }

    </script>
</head>
<body  onload="RefreshContent()" class="index-page">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <header id="top">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate> <%--*** Danny to work in firefox--%>
	<div class="header-a">
		<div class="wrapper-padding">	
        <div class="header-user"  style="margin-top:2px;"><asp:Label ID="lblHeaderUserName"  runat="server" 
                meta:resourcekey="lblHeaderUserNameResource1" ></asp:Label> </div>		
			<div class="header-phone" 

            id="dvlblHeaderAgentName" runat="server"  
            
            style="margin-top:2px;"><asp:Label ID="lblPhoneNo" runat="server" 
                    meta:resourcekey="lblPhoneNoResource1"></asp:Label> </div>
			<div class="header-account">
            <%--*** Danny to work in firefox--%>
                  <%--<asp:UpdatePanel ID="UpdatePanela" runat="server"><ContentTemplate>--%>
                      <%--<asp:Button ID="btnMyAccount"   UseSubmitBehavior="False" 
                    CssClass="header-account-button" runat="server" Text="Account" 
                          meta:resourcekey="btnMyAccountResource1"></asp:Button>--%>
                           <div class="header-lang">
                          <asp:Button ID="btnMyAccount" 
                    CssClass="header-account-button" runat="server" Text="Account" ></asp:Button>
                    </div>
            <%--</ContentTemplate></asp:UpdatePanel>--%>
             
			</div>
           <div class="header-agentname"  style="padding-left:105;margin-top:2px;">
               <asp:Label ID="lblHeaderAgentName" runat="server" style="    padding: 0px 10px 0px 0px;"
                   meta:resourcekey="lblHeaderAgentNameResource1" ></asp:Label> </div>
           
               <div class="header-lang">
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                <asp:Button ID="btnLogOut"   UseSubmitBehavior="False" 
                    CssClass="header-account-button" runat="server" Text="Log Out" 
                    meta:resourcekey="btnLogOutResource1"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>--%>
       <%--     <asp:LinkButton ID="btnLogOut"   UseSubmitBehavior="False" OnClick="btnLogOut_Click"
                    CssClass="header-account-button" runat="server" Text="Log Out" 
                    meta:resourcekey="btnLogOutResource1"causesvalidation="true"></asp:LinkButton>
--%>
                    <asp:LinkButton ID="btnLogOut"   UseSubmitBehavior="False" OnClick="btnLogOut_Click"
                    CssClass="header-account-button" runat="server" Text="Log Out" 
                    meta:resourcekey="btnLogOutResource1"></asp:LinkButton>


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
				<a href="#" class="header-viewed-btn">recently viewed</a>
				<!-- // viewed drop // -->
					<div class="viewed-drop">
						<div class="viewed-drop-a">
							<!-- // -->
							<div class="viewed-item">
								<div class="viewed-item-l">
									<a href="#"><img alt="" src="img/v-item-01.jpg" /></a>
								</div>
								<div class="viewed-item-r">
									<div class="viewed-item-lbl"><a href="#">Andrassy Thai Hotel</a></div>
									<div class="viewed-item-cat">location: thailand</div>
									<div class="viewed-price">152$</div>
								</div>
								<div class="clear"></div>
							</div>
							<!-- \\ -->
							<!-- // -->
							<div class="viewed-item">
								<div class="viewed-item-l">
									<a href="#"><img alt="" src="img/v-item-02.jpg" /></a>
								</div>
								<div class="viewed-item-r">
									<div class="viewed-item-lbl"><a href="#">Ermin's Hotel</a></div>
									<div class="viewed-item-cat">location: dubai</div>
									<div class="viewed-price">300$</div>
								</div>
								<div class="clear"></div>
							</div>
							<!-- \\ -->
							<!-- // -->
							<div class="viewed-item">
								<div class="viewed-item-l">
									<a href="#"><img alt="" src="img/v-item-03.jpg" /></a>
								</div>
								<div class="viewed-item-r">
									<div class="viewed-item-lbl"><a href="#">Best Western Hotel Reither</a></div>
									<div class="viewed-item-cat">location: berlin</div>
									<div class="viewed-price">2300$</div>
								</div>
								<div class="clear"></div>
							</div>
							<!-- \\ -->
						</div>
					</div>
				<!-- \\ viewed drop \\ -->
			</div>
      

			<div id="dvFlag" runat="server" class="header-lang" style="padding-top:5px;display:none;" >
				<a href="#"><img id="imgLang" runat="server" alt="" src="img/en.gif" /></a>
			</div>
			<div id="dvCurrency" runat="server" class="header-curency">
			</div>
                
              <div class="header-lang" id="dvMybooking" runat="server" style="padding-right:5px;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate><asp:Button ID="btnMyBooking" 
                    CssClass="header-account-button" runat="server" Text="MY BOOKING" 
                    meta:resourcekey="btnMyBookingResource1"></asp:Button>
            </ContentTemplate></asp:UpdatePanel>
           
			</div>
          <%--  <div class="header-lang" style="padding-left:10px;margin-left:5px;">
               <a href="#"><img  alt=""  width="20px;" src="img/booking-cart.png" /></a>
               </div>--%>
			<div class="clear"></div>
		</div>
	</div>
    </ContentTemplate></asp:UpdatePanel>
	<div class="header-b">
		<!-- // mobile menu // -->
			<div class="mobile-menu" id="dvMobmenu" runat="server" >
				<asp:Label ID="lblRecentViewed" runat="server" 
                    meta:resourcekey="lblRecentViewedResource1"></asp:Label>
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
            <asp:Literal ID="ltMenu" runat="server" meta:resourcekey="ltMenuResource1"></asp:Literal>
			</div>
			<div class="clear"></div>
		</div>
	</div>	
</header>
    <!-- main-cont -->
    <div class="main-cont">
        <div class="body-padding">
            <div class="mp-slider">
                <!-- // slider // -->
                <div class="mp-slider-row">
                    <div class="swiper-container">
                        <div class="swiper-preloader-bg">
                        </div>
                        <div id="preloader">
                            <div id="spinner">
                            </div>
                        </div>
                        <a href="#" class="arrow-left"></a><a href="#" class="arrow-right"></a>
                        <div class="swiper-pagination">
                        </div>
                        <div class="swiper-wrapper">
                            <div class="swiper-slide">
                                <div class="slide-section" style="background: url(BannerImage/1-Slide1.jpg) center top no-repeat;">
                                    <div class="mp-slider-lbl">
                                        Great journey begins with a small step</div>
                                    <div class="mp-slider-lbl-a">
                                        Make Your Life Better and Bright! You must trip with Us!</div>
                                    <div style="display: none;" class="mp-slider-btn">
                                        <a href="#" class="btn-a">Learn more</a></div>
                                </div>
                            </div>
                            <div class="swiper-slide">
                                <div class="slide-section slide-b" style="background: url(BannerImage/2-Slide2.jpg) center top no-repeat;">
                                    <div class="mp-slider-lbl">
                                        Relax with us. we love our clients</div>
                                    <div class="mp-slider-lbl-a">
                                        Make Your Life Better and Bright! You must trip with Us!</div>
                                    <div style="display: none;" class="mp-slider-btn">
                                        <a href="#" class="btn-a">Learn more</a></div>
                                </div>
                            </div>
                            <div class="swiper-slide">
                                <div class="slide-section slide-b" style="background: url(BannerImage/3-Slide3.jpg) center top no-repeat;">
                                    <div class="mp-slider-lbl">
                                        Make your life bright & beautiful</div>
                                    <div class="mp-slider-lbl-a">
                                        Make Your Life Better and Bright! You must trip with Us!</div>
                                    <div style="display: none;" class="mp-slider-btn">
                                        <a href="#" class="btn-a">Learn more</a></div>
                                </div>
                            </div>
                            <div class="swiper-slide">
                                <div class="slide-section slide-b" style="background: url(BannerImage/4-Slide4.jpg) center top no-repeat;">
                                    <div class="mp-slider-lbl">
                                        Make your life bright & beautiful</div>
                                    <div class="mp-slider-lbl-a">
                                        Make Your Life Better and Bright! You must trip with Us!</div>
                                    <div style="display: none;" class="mp-slider-btn">
                                        <a href="#" class="btn-a">Learn more</a></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- \\ slider \\ -->
            </div>
            <div class="wrapper-a-holder">
                <div class="wrapper-a">
                    <div class="page-search full-width-search search-type-b">
                        <div class="search-type-padding">
                            <nav class="page-search-tabs">
				<div class="search-tab active">Hotels</div>
				<div class="search-tab">Tours & Packages</div>
                <div class="search-tab">Airport Services</div>
                <div class="search-tab">Transfers</div>
				<div class="search-tab ">Other Services</div>
        				<div class="search-tab ">Pre-Arranged Hotels</div>
				<div class="clear"></div>	
			</nav>

                            <div class="page-search-content">
                                <!-- // tab content hotels // -->
                                <div class="search-tab-content">
                                    <div class="page-search-p">
                                        <!-- // -->
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    <asp:Localize ID="locHDestination" runat="server" Text=" Destination/Location" meta:resourcekey="locHDestinationResource1" />
                                                    <i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i>
                                                </label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtDestinationName" runat="server" placeholder="Example: dubai"
                                                        meta:resourcekey="txtDestinationNameResource1"></asp:TextBox>
                                                    <asp:TextBox ID="txtDestinationCode" runat="server" Style="display: none;" meta:resourcekey="txtDestinationCodeResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="txtDestinationName_AutoCompleteExtender" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetDeastinationList" TargetControlID="txtDestinationName"
                                                        OnClientItemSelected="DestinationNameautocompleteselected" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <!-- // -->
                                        </div>
                                        <!-- \\ -->
                                        <!-- // -->
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <div style="width: 100%">
                                                        <label>
                                                            <asp:Localize ID="locHCheckIn" runat="server" Text="Check in" meta:resourcekey="locHCheckInResource1" />
                                                            <i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i>
                                                        </label>
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtCheckIn" class="date-inpt-check-in" placeholder="dd/mm/yyyy"  autocomplete="off"
                                                                runat="server" meta:resourcekey="txtCheckInResource1"></asp:TextBox>
                                                            <span class="date-icon"></span>
                                                            <asp:HiddenField ID="hdCheckIn" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <div style="width: 100%">
                                                        <label>
                                                            <asp:Localize ID="locHCheckOut" runat="server" Text="Check out" meta:resourcekey="locHCheckOutResource1" />
                                                            <i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i>
                                                        </label>
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtCheckOut" class="date-inpt-check-out" placeholder="dd/mm/yyyy"  autocomplete="off"
                                                                runat="server" meta:resourcekey="txtCheckOutResource1"></asp:TextBox>
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
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        <asp:Localize ID="locHNoOfNights" runat="server" Text="No of Nights" meta:resourcekey="locHNoOfNightsResource1" />
                                                        <i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i>
                                                    </label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtNoOfNights" onkeypress="validateDecimalOnly(event,this)" runat="server" meta:resourcekey="txtNoOfNightsResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        <asp:Localize ID="locHRooms" runat="server" Text="Rooms" meta:resourcekey="locHRoomsResource1" />
                                                        <i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i>
                                                    </label>
                                                    <div class="select-wrapper">
                                                       <%--//        *** 26/082018 Danny--%>
                                                         <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                         <asp:DropDownList ID="ddlRoom_Dynamic"  class="custom-select"  runat="server" style="background:none !important;" AutoPostBack="true">
                                                        </asp:DropDownList>                                                     
                                              <span runat="server" id="loder16" class="loder-icon"></span>
                                                        <%--<img runat="server" alt="" style="display:none;" id="loder16"   src="~/img/Loder1616.gif"/>--%>
                                                        </ContentTemplate>
                                                        </asp:UpdatePanel> 
                                                        
                                          </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                       <%--//        *** 26/082018 Danny--%>
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
                                                                        <asp:DropDownList ID="ddlDynRoomChild"  OnSelectedIndexChanged="ChildAgeTxtCreate"   class="custom-select" AutoPostBack="true"  style="background:none !important;"
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
                                       
                                         
                                    
                                        <div style="margin-top: 15px;">
                                            <div class="search-large-i" id="dvForRO" runat="server">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        <asp:Localize ID="LocalizeHAgent" runat="server" Text="Agent" meta:resourcekey="LocalizeHAgentResource1" />
                                                        <i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i>
                                                    </label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtCustomer" runat="server" placeholder="--" meta:resourcekey="txtCustomerResource1"></asp:TextBox>
                                                        <asp:TextBox ID="txtCustomerCode" runat="server" Style="display: none" meta:resourcekey="txtCustomerCodeResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCustomer" runat="server"
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register"  CompletionSetCount="1" CompletionInterval="10" 
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetCustomers" TargetControlID="txtCustomer"
                                                            OnClientItemSelected="Customersautocompleteselected" ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                            <div class="search-large-i">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        <asp:Localize ID="LocalizeHSourceCountry" runat="server" Text="Source Country" meta:resourcekey="LocalizeHSourceCountryResource1" />
                                                        <i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i>
                                                    </label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtCountry" runat="server" placeholder="--" meta:resourcekey="txtCountryResource1"></asp:TextBox>
                                                        <asp:TextBox ID="txtCountryCode" runat="server" Style="display: none" meta:resourcekey="txtCountryCodeResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCountry" runat="server" CompletionInterval="10"
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtCountry"
                                                            UseContextKey="True" OnClientPopulating="AutoCompleteExtender_txtCountry_OnClientPopulating"  OnClientItemSelected="Countryautocompleteselected" ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                            <div class="search-large-i">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                               <%-- ''** Shahul 26/06/2018--%>
                                                  
                                                    <label>
                                                        <asp:Localize ID="LocalizeHStarCategory" runat="server" Text="Star Category" meta:resourcekey="LocalizeHStarCategoryResource1" />
                                                    </label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtHotelStars" runat="server" placeholder="--" meta:resourcekey="txtHotelStarsResource1"></asp:TextBox>
                                                        <asp:TextBox ID="txtHotelStarsCode" runat="server" Style="display: none" meta:resourcekey="txtHotelStarsCodeResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtHotelStars" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetHotelStars" TargetControlID="txtHotelStars"
                                                            OnClientItemSelected="HotelStarsNameautocompleteselected" ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                         
                                                    </div>
                                                    <div id="divstarshow" runat="server">
                                                     <asp:CheckBox ID="chkshowall"     runat="server"  />
                                                      <asp:Label ID="lblshowall" runat="server" CssClass="page-search-content-override-price"
                                                        Text="Include All Higher Categories" ></asp:Label>
                                                      </div>
                                                </div>
                                            </div>
                                            <div class="search-large-i" style="display: none;">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        <asp:Localize ID="LocalizeHOrderBy" runat="server" Text="Order By" meta:resourcekey="LocalizeHOrderByResource1" />
                                                    </label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="custom-select custom-select-ddlOrderBy"
                                                            meta:resourcekey="ddlOrderByResource1">
                                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">By Rate</asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="ListItemResource2">By Room</asp:ListItem>
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
                                                        <asp:Localize ID="LocalizeHAvailability" runat="server" Text="Availability" meta:resourcekey="LocalizeHAvailabilityResource1" />
                                                    </label>
                                                    <div class="select-wrapper">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server"> <%-- *** 26/08/2018 Danny Dynamic Adult and Chhild numbers--%> 
                                            <ContentTemplate>
                                                        <asp:DropDownList ID="ddlAvailability" runat="server" CssClass="custom-select custom-select-ddlAvailability"
                                                            meta:resourcekey="ddlAvailabilityResource1">
                                                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource3">Available</asp:ListItem>
                                                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource4">All</asp:ListItem>
                                                        </asp:DropDownList>
                                                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                    
                                                    </div>
                                                    <%--''** Shahul 26/06/2018--%>
                                                    <div class="srch-tab-right" runat="server" id="divmeal">
                                                       <label>
                                                        <asp:Localize ID="Localize1" runat="server" Text="MealPlan"  />
                                                        </label>
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
                                                        <asp:Localize ID="LocalizeHPropertyType" runat="server" Text="Property type" meta:resourcekey="LocalizeHPropertyTypeResource1" />
                                                    </label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlPropertType" runat="server" onchange="PropertTypeChanged(this.value);"
                                                            CssClass="custom-select custom-select-ddlPropertType" meta:resourcekey="ddlPropertTypeResource1">
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
                                                        <asp:Localize ID="LocalizeHHotels" runat="server" Text="Hotels" meta:resourcekey="LocalizeHHotelsResource1" />
                                                    </label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtHotelName" runat="server" placeholder="--" meta:resourcekey="txtHotelNameResource1"></asp:TextBox>
                                                        <asp:TextBox ID="txtHotelCode" runat="server" Style="display: none;" meta:resourcekey="txtHotelCodeResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtHotelName" runat="server" CompletionInterval="10"
                                                            CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="10"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetHotelName" TargetControlID="txtHotelName"
                                                            UseContextKey="True" OnClientItemSelected="HotelNameautocompleteselected" ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                            <!-- \\ -->
                                            <div class="clear">
                                            </div>
                                                  <div class="search-large-i">
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

                                            <div class="search-large-i" id="dvOverridePrice" runat="server">
                                                <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    <asp:CheckBox ID="chkOveridePrice" runat="server" meta:resourcekey="chkOveridePriceResource1" />
                                                    <asp:Label ID="Label1" runat="server" CssClass="page-search-content-override-price"
                                                        Text="Override Price" meta:resourcekey="Label1Resource1"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- \\ advanced \\ -->
                                    </div>
                                    <footer class="search-footer" style="padding-top:70px !important;">
                                        <div class="search-large-i">
					<%--	<a href="#" class="srch-btn">Search</a>	--%>
                     <div class="srch-tab-left">
                          <asp:Button ID="btnSearch" class="authorize-btn"  
                              OnClientClick="return ValidateSearch()" runat="server" Text="Search" 
                              meta:resourcekey="btnSearchResource1"></asp:Button> 
                          </div>

                           <div class="srch-tab-left">
                       <%-- <asp:Button ID="btnReset" class="authorize-btn" runat="server" Text="Reset"></asp:Button>--%>
                        <input  id="btnReset"  type="button"  class="authorize-btn" runat="server" meta:resourcekey="btnResetResource1" value="Reset"/>
                        </div>
                        </div>
						<span class="srch-lbl"><asp:Localize ID="LocalizeHAdvancedSearchoptions" runat="server"  
                                        Text="Advanced Search options" 
                                        meta:resourcekey="LocalizeHAdvancedSearchoptionsResource1"/> </span>
						<div class="clear"></div>
                       
					</footer>
                                </div>
                                <!-- // tab content hotels // -->
                                <!-- // tab content tours // -->
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
                                                        <asp:TextBox ID="txtTourFromDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-tour-from"
                                                            meta:resourcekey="txtTourFromDateResource1"></asp:TextBox>
                                                        <span class="date-icon-tour"></span>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        To Date<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtTourToDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-tour-to"
                                                            meta:resourcekey="txtTourToDateResource1"></asp:TextBox>
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
                                                tour Starting from<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTourStartingFrom" placeholder="Enter Space Bar to Show All" runat="server"
                                                    meta:resourcekey="txtTourStartingFromResource1"></asp:TextBox>
                                                <asp:TextBox ID="txtTourStartingFromCode" Style="display: none;" placeholder="example: dubai"
                                                    runat="server" meta:resourcekey="txtTourStartingFromCodeResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourStartingFrom" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetTourStartingFrom" TargetControlID="txtTourStartingFrom"
                                                    OnClientItemSelected="TourStartingFromautocompleteselected" ServicePath="">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                        <div class="search-large-i">
                                            <label>
                                                Classification</label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTourClassification" placeholder="--" runat="server" meta:resourcekey="txtTourClassificationResource1"></asp:TextBox>
                                                <asp:TextBox ID="txtTourClassificationCode" runat="server" Style="display: none"
                                                    meta:resourcekey="txtTourClassificationCodeResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourClassification" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetClassification" TargetControlID="txtTourClassification"
                                                    OnClientItemSelected="TourClassificationAutocompleteselected" ServicePath="">
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
                                                        runat="server" meta:resourcekey="ddlStarCategoryResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource5">--</asp:ListItem>
                                                        <asp:ListItem meta:resourcekey="ListItemResource6">2</asp:ListItem>
                                                        <asp:ListItem meta:resourcekey="ListItemResource7">3</asp:ListItem>
                                                        <asp:ListItem meta:resourcekey="ListItemResource8">4</asp:ListItem>
                                                        <asp:ListItem meta:resourcekey="ListItemResource9">5</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i" id="dvTourCustomer" style="margin-top: 20px; float: left;"
                                            runat="server">
                                            <label>
                                                Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTourCustomer" runat="server" placeholder="--" meta:resourcekey="txtTourCustomerResource1"></asp:TextBox>
                                                <asp:TextBox ID="txtTourCustomerCode" runat="server" Style="display: none" meta:resourcekey="txtTourCustomerCodeResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourCustomer" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetCustomers" TargetControlID="txtTourCustomer"
                                                    OnClientItemSelected="TourCustomerAutocompleteselected" ServicePath="">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                        <div class="search-large-i" style="margin-top: 20px; float: right; margin-right: -1px;">
                                            <label>
                                                Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTourSourceCountry" runat="server" placeholder="--" meta:resourcekey="txtTourSourceCountryResource1"></asp:TextBox>
                                                <asp:TextBox ID="txtTourSourceCountryCode" runat="server" Style="display: none" meta:resourcekey="txtTourSourceCountryCodeResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTourSourceCountry" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetCountry" TargetControlID="txtTourSourceCountry"
                                                    UseContextKey="True" OnClientPopulating="AutoCompleteExtender_txtTourSourceCountry_OnClientPopulating"  OnClientItemSelected="TourCountryautocompleteselected" ServicePath="">
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
                                                            runat="server" meta:resourcekey="ddlSeniorCitizenResource1">
                                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource10">--</asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="ListItemResource11">1</asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="ListItemResource12">2</asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="ListItemResource13">3</asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="ListItemResource14">4</asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="ListItemResource15">5</asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="ListItemResource16">6</asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="ListItemResource17">7</asp:ListItem>
                                                            <asp:ListItem meta:resourcekey="ListItemResource18">8</asp:ListItem>
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
                                                            runat="server" meta:resourcekey="ddlTourAdultResource1">
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
                                                            runat="server" meta:resourcekey="ddlTourChildrenResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i" style="margin-top: 20px; float: left;">
                                            <label>
                                            </label>
                                            <div class="sic-span">
                                                <%--    <asp:RadioButtonList ID="rblPrivateOrSIC" Width="100%" RepeatDirection="Horizontal"
                                                    runat="server" meta:resourcekey="rblPrivateOrSICResource1">
                                                    <asp:ListItem Selected="True" meta:resourcekey="ListItemResource19" >Private</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource20">SIC (Seat In Coach)</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource21">Without Transfers</asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                                <asp:CheckBoxList ID="chklPrivateOrSIC" Width="100%" RepeatDirection="Horizontal"
                                                    runat="server">
                                                    <asp:ListItem Selected="True">Private</asp:ListItem>
                                                    <asp:ListItem Selected="True">SIC (Seat In Coach)</asp:ListItem>
                                                    <asp:ListItem Selected="True">Without Transfers</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="search-large-i" id="dvTourOveridePrice" style="margin-top: 20px; float: left;"
                                            runat="server">
                                            <div class="sic-span" style="padding-top: 10px;">
                                                <asp:CheckBox ID="chkTourOveridePrice" Text="Override Price" runat="server" meta:resourcekey="chkTourOveridePriceResource1" />
                                                <%-- <asp:Label ID="Label2" runat="server" CssClass="page-search-content-override-price"
                                                    Text="Override Price"></asp:Label>--%>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div id="dvTourChild" runat="server" style="margin-top: 20px;">
                                            <div class="search-large-i-child-tour" style="float: left;">
                                                <label style="text-align: left; padding-right: 2px;">
                                                    Ages of children at check-out</label>

                                                 <div class="srch-tab-child" id="dvTourChild1" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div29">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                                 <div class="srch-tab-child" id="dvTourChild2" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div30">
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
                                                          <div class="srch-tab-child-pre" id="div31">
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
                                                        <div class="srch-tab-child-pre" id="div32">
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
                                                         <div class="srch-tab-child-pre" id="div33">
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
                                                         <div class="srch-tab-child-pre" id="div34">
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
                                                          <div class="srch-tab-child-pre" id="div35">
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
                                                         <div class="srch-tab-child-pre" id="div36">
                                                    <div class="select-wrapper" style="width: 60px;">
                                                      <div class="input-a">
                                                                    <asp:TextBox ID="txtTourChild8" placeholder="CH 8" runat="server"   onchange="validateAge(this)"   onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                
                                                    </div>
                                                </div>
                                                    </div>
                                                </div>
                                              
                                               
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hdCheckInPrevDay" runat="server" />
                                        <asp:HiddenField ID="hdCheckOutNextDay" runat="server" />
                                        <!-- \\ -->
                                        <div class="clear">
                                        </div>
                                    </div>
                                    <footer class="search-footer">
                                       <div class="search-large-i">
						     <div  class="srch-tab-left" >
                          <asp:Button ID="btnTourSearch" class="authorize-btn"   
                                     OnClientClick="return ValidateTourSearch()"   runat="server" Text="Search" 
                                     meta:resourcekey="btnTourSearchResource1"></asp:Button>  
                          </div>
                           <div  class="srch-tab-left">
                      <input  id="btnTourReset"  type="button"  class="srch-btn-home"  value="Reset"/>
                        </div></div>
						<div class="clear"></div>
					</footer>
                                </div>
                                <!-- // tab content tours // -->
                                <!-- // tab content Airport M&A // -->
                                <div class="search-tab-content">
                                    <div class="page-search-p">
                                        <!-- /Arrival/ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    ARRIVAL</label>
                                                <div class="search-large-i" id="div8" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chkMAarrival" CssClass="side-block jq-checkbox-tour" runat="server"
                                                            meta:resourcekey="chkMAarrivalResource1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        ARRIVAL DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMAArrivaldate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-tour-from"
                                                            meta:resourcekey="txtMAArrivaldateResource1"></asp:TextBox>
                                                        <span class="date-icon-tour"></span>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlMAFlightClass" runat="server" class="custom-select custom-select-ddlStarCategory"
                                                            meta:resourcekey="ddlMAFlightClassResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left;">
                                            <!-- /MA Arrival flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMAArrivalflight" placeholder="--" runat="server" onkeydown="MAArrivalflightSetContextKey()"
                                                          Onblur ="CheckMAFlight(this);"  meta:resourcekey="txtMAArrivalflightResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAArrivalflight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetMAArrivalflight" TargetControlID="txtMAArrivalflight"
                                                            UseContextKey="True" OnClientItemSelected="MAArrivalflightAutocompleteSelected"
                                                            ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtMAArrivalflightCode" Style="display: none" runat="server" meta:resourcekey="txtMAArrivalflightCodeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /MA Arrival time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        ARRIVAL TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMAArrivalTime" runat="server" meta:resourcekey="txtMAArrivalTimeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div9" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /MA Arrival Pikcup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                ARRIVAL AIRPORT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtMAArrivalpickup" placeholder="--" runat="server" meta:resourcekey="txtMAArrivalpickupResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAArrivalpickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetMAArrivalpickup" TargetControlID="txtMAArrivalpickup"
                                                    UseContextKey="True" OnClientItemSelected="MAArrivalpickupAutocompleteSelected"
                                                    ServicePath="">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtMAArrivalpickupcode" Style="display: none" runat="server" meta:resourcekey="txtMAArrivalpickupcodeResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <!-- /MA Arrival Drop off/ -->
                                        <div class="search-large-i" style="margin-left: 28px; margin-top: 20px; display: none;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    DROP OFF POINT</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtMAArrDropoff" placeholder="--" runat="server" meta:resourcekey="txtMAArrDropoffResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAArrDropoff" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetMAArrDropoff" TargetControlID="txtMAArrDropoff"
                                                        UseContextKey="True" OnClientItemSelected="MAArrDropoffAutocompleteSelected"
                                                        ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:TextBox ID="txtMAArrDropoffcode" Style="display: none" runat="server" meta:resourcekey="txtMAArrDropoffcodeResource1"></asp:TextBox>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \M&A Arrival  End\ -->
                                        <!-- \M&ADeparture  Start\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    DEPARTURE</label>
                                                <div class="search-large-i" id="div10" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chkMADeparture" CssClass="side-block jq-checkbox-tour" runat="server"
                                                            meta:resourcekey="chkMADepartureResource1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        DEPARTURE DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMADeparturedate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                            runat="server" meta:resourcekey="txtMADeparturedateResource1"></asp:TextBox>
                                                        <span class="date-icon"></span>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlMADepFlightClass" runat="server" class="custom-select custom-select-ddlStarCategory"
                                                            meta:resourcekey="ddlMADepFlightClassResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left;">
                                            <!-- /MA Departure flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMADepartureFlight" placeholder="--" runat="server" onkeydown="MADepartureflightSetContextKey()"
                                                            Onblur ="CheckMADepFlight(this);" meta:resourcekey="txtMADepartureFlightResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMADepartureFlight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetMADepartureflight" TargetControlID="txtMADepartureFlight"
                                                            UseContextKey="True" OnClientItemSelected="MADepartureAutocompleteSelected" ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtMADepartureFlightCode" Style="display: none" runat="server" meta:resourcekey="txtMADepartureFlightCodeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /MA Departure time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        DEPARTURE TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMADepartureTime" runat="server" meta:resourcekey="txtMADepartureTimeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \\ -->
                                        <div class="clear">
                                        </div>
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
                                        <!-- /MA Departure Airport/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                DEPARTURE AIRPORT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtMADepairportdrop" placeholder="--" runat="server" meta:resourcekey="txtMADepairportdropResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMADepairportdrop" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetMADepairportdrop" TargetControlID="txtMADepairportdrop"
                                                    UseContextKey="True" OnClientItemSelected="MADepairportdropAutocompleteSelected"
                                                    ServicePath="">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtMADepairportdropcode" Style="display: none" runat="server" meta:resourcekey="txtMADepairportdropcodeResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <!-- /MA Departure Pickup/ -->
                                        <div class="search-large-i" style="margin-left: 28px; margin-top: 20px; display: none;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    PICK-UP POINT</label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtMADeppickup" placeholder="--" runat="server" meta:resourcekey="txtMADeppickupResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMADeppickup" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetMADeppickup" TargetControlID="txtMADeppickup"
                                                        UseContextKey="True" OnClientItemSelected="MADeppickupAutocompleteSelected" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:TextBox ID="txtMADeppickupcode" Style="display: none" runat="server" meta:resourcekey="txtMADeppickupcodeResource1"></asp:TextBox>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \Departure  End\ -->
                                        <!-- \Transit Start\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    TRANSIT</label>
                                                <div class="search-large-i" id="div15" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chktransit" CssClass="side-block jq-checkbox-tour" runat="server"
                                                            meta:resourcekey="chktransitResource1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        Transit Arrival DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtTransitarrdate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                            runat="server" meta:resourcekey="txtTransitarrdateResource1"></asp:TextBox>
                                                        <span class="date-icon"></span>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddltranarrflightclass" runat="server" class="custom-select custom-select-ddlStarCategory"
                                                            meta:resourcekey="ddltranarrflightclassResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left;">
                                            <!-- /MA transit Arrival flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMAtranArrFlight" placeholder="--" runat="server" onkeydown="MATranArrivalflightSetContextKey()"
                                                            meta:resourcekey="txtMAtranArrFlightResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAtranArrFlight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetMATranArrivalflight" TargetControlID="txtMAtranArrFlight"
                                                            UseContextKey="True" OnClientItemSelected="MATranArrivalflightAutocompleteSelected"
                                                            ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtMATranArrFlightCode" Style="display: none" runat="server" meta:resourcekey="txtMATranArrFlightCodeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /MA Transit Arrival time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        ARRIVAL TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMATranArrTime" runat="server" meta:resourcekey="txtMATranArrTimeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div16" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /MA Transit Arrival Pikcup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                Transit ARRIVAL AIRPORT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtMAtranArrivalpickup" placeholder="--" runat="server" meta:resourcekey="txtMAtranArrivalpickupResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAtranArrivalpickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetMATranArrivalpickup" TargetControlID="txtMAtranArrivalpickup"
                                                    UseContextKey="True" OnClientItemSelected="MATranArrivalpickupAutocompleteSelected"
                                                    ServicePath="">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtMATransitArrivalpickupcode" Style="display: none" runat="server"
                                                    meta:resourcekey="txtMATransitArrivalpickupcodeResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div17" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        Transit Departure DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMATrandepdate" class="date-inpt-check-in" placeholder="dd/mm/yyyy"
                                                            runat="server" meta:resourcekey="txtMATrandepdateResource1"></asp:TextBox>
                                                        <span class="date-icon"></span>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlMAtrandepflightlass" runat="server" class="custom-select custom-select-ddlStarCategory"
                                                            meta:resourcekey="ddlMAtrandepflightlassResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- /MA Tran Departure flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMAtranDepartureFlight" placeholder="--" runat="server" onkeydown="MATranDepartureflightSetContextKey()"
                                                            meta:resourcekey="txtMAtranDepartureFlightResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMAtranDepartureFlight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetMAtranDepartureflight" TargetControlID="txtMAtranDepartureFlight"
                                                            UseContextKey="True" OnClientItemSelected="MATranDepartureAutocompleteSelected"
                                                            ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtMATranDepartureFlightCode" Style="display: none" runat="server"
                                                            meta:resourcekey="txtMATranDepartureFlightCodeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /MA Departure time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        DEPARTURE TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtMATranDepartureTime" runat="server" meta:resourcekey="txtMATranDepartureTimeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div18" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /MA Transit Departure Pikcup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                Transit Departure AIRPORT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtMAtranDeppickup" placeholder="--" runat="server" meta:resourcekey="txtMAtranDeppickupResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="10"
                                                    CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetMATranDeparturepickup" TargetControlID="txtMAtranDeppickup"
                                                    UseContextKey="True" OnClientItemSelected="MATranDeppickupAutocompleteSelected"
                                                    ServicePath="">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtMATransitDeparturepickupcode" Style="display: none" runat="server"
                                                    meta:resourcekey="txtMATransitDeparturepickupcodeResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <!-- \\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \Transit Departure  End\ -->
                                        <!-- \MA Cunstomer\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div12" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;" id="dvMACustomer"
                                            runat="server">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtMACustomer" runat="server" placeholder="--" meta:resourcekey="txtMACustomerResource1"></asp:TextBox>
                                                    <asp:TextBox ID="txtMACustomercode" runat="server" Style="display: none" meta:resourcekey="txtMACustomercodeResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMACustomer" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetMACustomer" TargetControlID="txtMACustomer"
                                                        OnClientItemSelected="MACustomerAutocompleteSelected" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- \MA Source Country\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <div>
                                                <label>
                                                    Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtMASourcecountry" runat="server" placeholder="--" meta:resourcekey="txtMASourcecountryResource1"></asp:TextBox>
                                                    <asp:TextBox ID="txtMASourcecountrycode" runat="server" Style="display: none" meta:resourcekey="txtMASourcecountrycodeResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtMASourcecountry" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetMACountry" TargetControlID="txtMASourcecountry"
                                                        UseContextKey="True" OnClientPopulating="AutoCompleteExtender_txtMASourcecountry_OnClientPopulating" OnClientItemSelected="MACountryautocompleteselected" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div13" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- \MA Adult child\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom" style="width: 80%;">
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        adult<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlMAAdult" class="custom-select custom-select-ddlMAAdult"
                                                            runat="server" meta:resourcekey="ddlMAAdultResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        child</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlMAChild" class="custom-select custom-select-ddlMAChild"
                                                            runat="server" meta:resourcekey="ddlMAChildResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-3c" style="float: left; margin-top: 20px;">
                                                    <div id="divMAOverride" style="width: 120%" runat="server">
                                                        <asp:CheckBox ID="chkMAoverride" runat="server" meta:resourcekey="chkMAoverrideResource1" />
                                                        <asp:Label ID="Label3" runat="server" CssClass="page-search-content-override-price"
                                                            Text="Override Price" meta:resourcekey="Label3Resource1"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- // advanced // -->
                                        <div class="clear">
                                        </div>
                                        <div id="divMAchild" runat="server" style="margin-top: 20px;">
                                            <div class="search-large-i-trf-half-cell">
                                                <div style="padding-left: 50px">
                                                    <label>
                                                    </label>
                                                    <div class="search-large-i" id="div14" runat="server">
                                                        <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="search-large-i-child-tour" style="float: left;">
                                                <label style="text-align: left; padding-right: 2px;">
                                                    Ages of children at Airport Meet</label>
                                                <div class="srch-tab-child" id="dvMAChild1" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div45">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtMAChild1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvMAChild2" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div46">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtMAChild2" placeholder="CH 2" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvMAChild3" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                          <div class="srch-tab-child-pre" id="div47">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtMAChild3" placeholder="CH 3" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvMAChild4" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div48">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtMAChild4" placeholder="CH 4" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="search-large-i-child-tour">
                                                <label style="color: White;">
                                                    Ages of children at Aiport Meet</label>
                                                <div class="srch-tab-child" id="dvMAChild5" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div49">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtMAChild5" placeholder="CH 5" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvMAChild6" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                         <div class="srch-tab-child-pre" id="div50">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtMAChild6" placeholder="CH 6" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvMAChild7" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                          <div class="srch-tab-child-pre" id="div51">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtMAChild7" placeholder="CH 7" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvMAChild8" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                         <div class="srch-tab-child-pre" id="div52">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtMAChild8" placeholder="CH 8" runat="server" onchange="validateAge(this)"
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
                                    </div>
                                    <footer class="search-footer">
                                       <div class="search-large-i">
						     <div  class="srch-tab-left" >
                          <asp:Button ID="btnMAsearch" class="authorize-btn"   
                                     OnClientClick="return ValidateAirportMeetSearch()"    runat="server" 
                                     Text="Search" meta:resourcekey="btnMAsearchResource1"></asp:Button>  
                          </div>
                           <div  class="srch-tab-left">
                      <input  id="btnMAreset"  type="button"  class="srch-btn-home"  value="Reset"/>
                        </div></div>
					              	<div class="clear"></div>
				                 	</footer>
                                </div>
                                <!-- // tab content Airport M&A // -->
                                <!-- // tab content Transfers // -->
                                <div class="search-tab-content">
                                    <div class="page-search-p">
                                        <!-- /Arrival/ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    ARRIVAL</label>
                                                <div class="search-large-i" id="divarrival" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chkarrival" runat="server" meta:resourcekey="chkarrivalResource1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        ARRIVAL DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtTrfArrivaldate" runat="server" autocomplete="off"  placeholder="dd/mm/yyyy" CssClass="date-inpt-tour-from"
                                                            meta:resourcekey="txtTrfArrivaldateResource1"></asp:TextBox>
                                                        <span class="date-icon-tour"></span>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlTrfArrFlightClass" runat="server" class="custom-select custom-select-ddlStarCategory"
                                                            meta:resourcekey="ddlTrfArrFlightClassResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <!-- \\ -->
                                        <div class="search-large-i" style="float: left;">
                                            <!-- /TRf Arrival flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtArrivalflight" placeholder="--" runat="server" onkeydown="ArrivalflightSetContextKey()"
                                                            Onblur ="CheckFlight(this);" meta:resourcekey="txtArrivalflightResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtArrivalflight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetArrivalflight" TargetControlID="txtArrivalflight"
                                                            UseContextKey="True" OnClientItemSelected="ArrivalflightAutocompleteSelected"
                                                            ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtArrivalflightCode" Style="display: none" runat="server" meta:resourcekey="txtArrivalflightCodeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /TRf Arrival time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        ARRIVAL TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtArrivalTime" runat="server" meta:resourcekey="txtArrivalTimeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div2" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /TRf Arrival Pikcup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                ARRIVAL PICKUP<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTrfArrivalpickup" placeholder="--" runat="server" meta:resourcekey="txtTrfArrivalpickupResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfArrivalpickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetArrivalpickup" TargetControlID="txtTrfArrivalpickup"
                                                    UseContextKey="True" OnClientItemSelected="TrfArrivalpickupAutocompleteSelected"
                                                    ServicePath="">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtTrfArrivalpickupcode" Style="display: none" runat="server" meta:resourcekey="txtTrfArrivalpickupcodeResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <!-- /TRf Arrival Drop off/ -->
                                        <div class="search-large-i" style="margin-left: 28px; margin-top: 20px;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    Drop off Hotels / Locations<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtTrfArrDropoff" placeholder="--" runat="server" meta:resourcekey="txtTrfArrDropoffResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfArrDropoff" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetTrfArrDropoff" TargetControlID="txtTrfArrDropoff"
                                                        UseContextKey="True" OnClientItemSelected="TrfArrDropoffAutocompleteSelected"
                                                        ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:TextBox ID="txtTrfArrDropoffcode" Style="display: none" runat="server" meta:resourcekey="txtTrfArrDropoffcodeResource1"></asp:TextBox>
                                                    <%--Added Shahul 19/07/2018--%>
                                                    <asp:TextBox ID="txtTrfArrDroptype" Style="display: none" runat="server" meta:resourcekey="txtTrfArrDroptypeResource1"></asp:TextBox>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <!-- \\ -->
                                        <div class="clear">
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \Arrival  End\ -->
                                        <!-- \Departure  Start\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                    DEPARTURE</label>
                                                <div class="search-large-i" id="div3" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        <asp:CheckBox ID="chkDeparture" runat="server" meta:resourcekey="chkDepartureResource1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        DEPARTURE DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtTrfDeparturedate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-tour-from"
                                                        autocomplete="off"    onchange="ValidateDepDate();" meta:resourcekey="txtTrfDeparturedateResource1"></asp:TextBox>
                                                        <span class="date-icon-tour"></span>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        FLIGHT CLASS</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlTrfDepFlightClass" runat="server" class="custom-select custom-select-ddlStarCategory"
                                                            meta:resourcekey="ddlTrfDepFlightClassResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left;">
                                            <!-- /TRf Departure flight/ -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-left">
                                                    <label>
                                                        FLIGHT NO</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtDepartureFlight" placeholder="--" runat="server" onkeydown="DepartureflightSetContextKey()"
                                                            Onblur="CheckDepFlight(this);" meta:resourcekey="txtDepartureFlightResource1"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtDepartureFlight" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetDepartureflight" TargetControlID="txtDepartureFlight"
                                                            UseContextKey="True" OnClientItemSelected="DepartureAutocompleteSelected" ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtDepartureFlightCode" Style="display: none" runat="server" meta:resourcekey="txtDepartureFlightCodeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <!-- /TRf Departure time/ -->
                                                <div class="srch-tab-right">
                                                    <label>
                                                        DEPARTURE TIME</label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtDepartureTime" runat="server" meta:resourcekey="txtDepartureTimeResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \\ -->
                                        <div class="clear">
                                        </div>
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div1" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /TRf Departure Pickup/ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <label>
                                                Pickup Hotels/ Locations<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtTrfDeppickup" placeholder="--" runat="server" meta:resourcekey="txtTrfDeppickupResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfDeppickup" runat="server"
                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetTrfDeppickup" TargetControlID="txtTrfDeppickup"
                                                    UseContextKey="True" OnClientItemSelected="TrfDeppickupAutocompleteSelected"
                                                    ServicePath="">
                                                </asp:AutoCompleteExtender>
                                                <asp:TextBox ID="txtTrfDeppickupcode" Style="display: none" runat="server" meta:resourcekey="txtTrfDeppickupcodeResource1"></asp:TextBox>
                                                   <%--Added Shahul 19/07/2018--%>
                                                <asp:TextBox ID="txtTrfDeppickuptype" Style="display: none" runat="server" meta:resourcekey="txtTrfDeppickuptypeResource1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <!-- /TRf Departure Drop off/ -->
                                        <div class="search-large-i" style="margin-left: 28px; margin-top: 20px;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    AIRPORT DROP OFF<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtTrfDepairportdrop" placeholder="--" runat="server" meta:resourcekey="txtTrfDepairportdropResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfDepairportdrop" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetTrfDepairportdrop" TargetControlID="txtTrfDepairportdrop"
                                                        UseContextKey="True" OnClientItemSelected="TrfDepairportdropAutocompleteSelected"
                                                        ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:TextBox ID="txtTrfDepairportdropcode" Style="display: none" runat="server" meta:resourcekey="txtTrfDepairportdropcodeResource1"></asp:TextBox>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \Departure  End\ -->
                                        <!-- /Inter Hotel start/ -->
                                        <div id="divinterhotel" runat="server">
                                            <div class="search-large-i-trf-half-cell">
                                                <div style="padding-left: 50px">
                                                    <label>
                                                        INTER HOTEL</label>
                                                    <div class="search-large-i" id="div4" runat="server">
                                                        <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                            <asp:CheckBox ID="chkinter" runat="server" meta:resourcekey="chkinterResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="search-large-i">
                                                <!-- // -->
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <div class="srch-tab-left">
                                                        <label>
                                                            TRANSFER DATE<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                        <div class="input-a">
                                                            <asp:TextBox ID="txtTrfinterdate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-tour-from"
                                                              autocomplete="off"  meta:resourcekey="txtTrfinterdateResource1"></asp:TextBox>
                                                            <span class="date-icon-tour"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                            <div class="clear">
                                            </div>
                                            <div class="search-large-i-trf-half-cell">
                                                <div style="padding-left: 50px">
                                                    <label>
                                                    </label>
                                                    <div class="search-large-i" id="div19" runat="server">
                                                        <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="search-large-i" style="float: left; margin-top: 20px;">
                                                <div class="srch-tab-line no-margin-bottom">
                                                    <label>
                                                        Pickup Hotels/ Locations<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtTrfinterPickup" placeholder="--" runat="server" meta:resourcekey="txtTrfinterPickupResource1"></asp:TextBox>
                                                        <asp:TextBox ID="txtTrfinterPickupcode" Style="display: none" runat="server" meta:resourcekey="txtTrfinterPickupcodeResource1"></asp:TextBox>
                                                        <%--added shahul 21/07/18--%>
                                                        <asp:TextBox ID="txtTrfinterPickuptype" Style="display: none" runat="server" meta:resourcekey="txtTrfinterPickuptypeResource1"></asp:TextBox> 

                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfinterPickup" runat="server"
                                                            CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                            CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                            DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                            MinimumPrefixLength="-1" ServiceMethod="GetInterPickup" TargetControlID="txtTrfinterPickup"
                                                            OnClientItemSelected="InterPickupautocompleteselected" ServicePath="">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </div>
                                                <!-- \\ -->
                                            </div>
                                            <div class="search-large-i" style="float: left; margin-top: 20px;">
                                                <!-- // -->
                                                <label>
                                                    Drop off Hotels / Locations<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtTrfInterdropff" placeholder="--" runat="server" meta:resourcekey="txtTrfInterdropffResource1"></asp:TextBox>
                                                    <asp:TextBox ID="txtTrfInterdropffcode" Style="display: none" runat="server" meta:resourcekey="txtTrfInterdropffcodeResource1"></asp:TextBox>
                                                     <%--added shahul 21/07/18--%>
                                                    <asp:TextBox ID="txtTrfInterdropfftype" Style="display: none" runat="server" meta:resourcekey="txtTrfInterdropfftypeResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfInterdropff" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetInterDropoff" TargetControlID="txtTrfInterdropff"
                                                        OnClientItemSelected="InterDropoffautocompleteselected" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- \Inter Hotel End\ -->
                                        <!-- \Trf Cunstomer\ -->
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div5" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;" id="dvTrfCustomer"
                                            runat="server">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtTrfCustomer" runat="server" placeholder="--" meta:resourcekey="txtTrfCustomerResource1"></asp:TextBox>
                                                    <asp:TextBox ID="txtTrfCustomercode" runat="server" Style="display: none" meta:resourcekey="txtTrfCustomercodeResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfCustomer" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetTrfCustomer" TargetControlID="txtTrfCustomer"
                                                        OnClientItemSelected="TrfCustomerAutocompleteSelected" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- \Trf Source Country\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <div>
                                                <label>
                                                    Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtTrfSourcecountry" runat="server" placeholder="--" meta:resourcekey="txtTrfSourcecountryResource1"></asp:TextBox>
                                                    <asp:TextBox ID="txtTrfSourcecountrycode" runat="server" Style="display: none" meta:resourcekey="txtTrfSourcecountrycodeResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtTrfSourcecountry" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetTrfCountry" TargetControlID="txtTrfSourcecountry"
                                                        UseContextKey="True" OnClientPopulating="AutoCompleteExtender_txtTrfSourcecountry_OnClientPopulating" OnClientItemSelected="TrfCountryautocompleteselected" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="search-large-i-trf-half-cell">
                                            <div style="padding-left: 50px">
                                                <label>
                                                </label>
                                                <div class="search-large-i" id="div6" runat="server">
                                                    <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- \Trf Adult child\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom" style="width: 80%;">
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        adult<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlTrfAdult" class="custom-select custom-select-ddlTrfAdult"
                                                            runat="server" meta:resourcekey="ddlTrfAdultResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        child</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlTrfChild" class="custom-select custom-select-ddlTrfChild"
                                                            runat="server" meta:resourcekey="ddlTrfChildResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-3c" style="float: left; margin-top: 20px;">
                                                    <div id="divTrfOverride" style="width: 120%" runat="server">
                                                        <asp:CheckBox ID="chkTrfoverride" runat="server" meta:resourcekey="chkTrfoverrideResource1" />
                                                        <asp:Label ID="Label2" runat="server" CssClass="page-search-content-override-price"
                                                            Text="Override Price" meta:resourcekey="Label2Resource1"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- // advanced // -->
                                        <div class="clear">
                                        </div>
                                        <div id="divTrfchild" runat="server" style="margin-top: 20px;">
                                            <div class="search-large-i-trf-half-cell">
                                                <div style="padding-left: 50px">
                                                    <label>
                                                    </label>
                                                    <div class="search-large-i" id="div7" runat="server">
                                                        <div class="srch-tab-line no-margin-bottom" style="padding-top: 10px;">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="search-large-i-child-tour" style="float: left;">
                                                <label style="text-align: left; padding-right: 2px;">
                                                    Ages of children at Transfer</label>
                                                <div class="srch-tab-child" id="dvTrfChild1" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div37">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvTrfChild2" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                         <div class="srch-tab-child-pre" id="div38">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild2" placeholder="CH 2" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvTrfChild3" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                         <div class="srch-tab-child-pre" id="div39">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild3" placeholder="CH 3" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvTrfChild4" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div40">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild4" placeholder="CH 4" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="search-large-i-child-tour">
                                                <label style="color: White;">
                                                    Ages of children at Transfers</label>
                                                <div class="srch-tab-child" id="dvTrfChild5" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                         <div class="srch-tab-child-pre" id="div41">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild5" placeholder="CH 5" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvTrfChild6" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                         <div class="srch-tab-child-pre" id="div42">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild6" placeholder="CH 6" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvTrfChild7" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div43">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild7" placeholder="CH 7" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-child" id="dvTrfChild8" style="float: left;">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                       <div class="srch-tab-child-pre" id="div44">
                                                            <div class="select-wrapper" style="width: 60px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtTrfChild8" placeholder="CH 8" runat="server" onchange="validateAge(this)"
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
                                        <!-- \\ advanced \\ -->
                                    </div>
                                    <footer class="search-footer">
                                       <div class="search-large-i">
						     <div  class="srch-tab-left" >
                          <asp:Button ID="btnTrfsearch" class="authorize-btn"  
                                     OnClientClick="return ValidateTrfSearch()"  runat="server" Text="Search" 
                                     meta:resourcekey="btnTrfsearchResource1"></asp:Button>  
                          </div>
                           <div  class="srch-tab-left">
                      <input  id="btnTrfreset"  type="button"  class="srch-btn-home"  value="Reset"/>
                        </div></div>
					              	<div class="clear"></div>
				                 	</footer>
                                </div>
                                <!-- // tab content Transfers // -->
                                <!-- // tab content Other Services // -->
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
                                                        <asp:TextBox ID="txtothFromDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-oth-from"
                                                            meta:resourcekey="txtothFromDateResource1"></asp:TextBox>
                                                        <span class="date-icon-oth"></span>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-right">
                                                    <label>
                                                        To Date<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="input-a">
                                                        <asp:TextBox ID="txtothToDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-oth-to"
                                                            meta:resourcekey="txtothToDateResource1"></asp:TextBox>
                                                        <span class="date-icon-oth"></span>
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
                                            <!-- // -->
                                            <label>
                                                Select Service Group<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                            <div class="input-a">
                                                <asp:TextBox ID="txtothgroup" placeholder="--" runat="server" meta:resourcekey="txtothgroupResource1"></asp:TextBox>
                                                <asp:TextBox ID="txtothgroupcode" Style="display: none;" placeholder="example: dubai"
                                                    runat="server" meta:resourcekey="txtothgroupcodeResource1"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtothgroup" runat="server" CompletionInterval="10"
                                                    CompletionListCssClass="autocomplete_completionListElement_register" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                    CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                    DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                    MinimumPrefixLength="-1" ServiceMethod="GetOtherservicegroup" TargetControlID="txtothgroup"
                                                    OnClientItemSelected="othgroupautocompleteselected" ServicePath="">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div class="search-large-i">
                                            <div class="srch-tab-3c" style="float: left; margin-top: 20px;">
                                                <div id="divothoverride" style="width: 120%" runat="server">
                                                    <asp:CheckBox ID="chkothoverride" runat="server" meta:resourcekey="chkothoverrideResource1" />
                                                    <asp:Label ID="Label4" runat="server" CssClass="page-search-content-override-price"
                                                        Text="Override Price" meta:resourcekey="Label4Resource1"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="search-large-i" id="divothcustomer" style="margin-right: 28px; margin-top: 20px;
                                            float: left;" runat="server">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    Agent<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtothCustomer" runat="server" placeholder="--" meta:resourcekey="txtothCustomerResource1"></asp:TextBox>
                                                    <asp:TextBox ID="txtothCustomercode" runat="server" Style="display: none" meta:resourcekey="txtothCustomercodeResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtothCustomer" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetothCustomers" TargetControlID="txtothCustomer"
                                                        OnClientItemSelected="othserviceCustomerAutocompleteselected" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <div class="srch-tab-line no-margin-bottom">
                                                <label>
                                                    Source Country<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                <div class="input-a">
                                                    <asp:TextBox ID="txtothSourceCountry" runat="server" placeholder="--" meta:resourcekey="txtothSourceCountryResource1"></asp:TextBox>
                                                    <asp:TextBox ID="txtothSourceCountryCode" runat="server" Style="display: none" meta:resourcekey="txtothSourceCountryCodeResource1"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtothSourceCountry" runat="server"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement_register"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem_register"
                                                        CompletionListItemCssClass="autocomplete_listItem_register" CompletionSetCount="1"
                                                        DelimiterCharacters="" EnableCaching="False" Enabled="True" FirstRowSelected="True"
                                                        MinimumPrefixLength="-1" ServiceMethod="GetotherCountry" TargetControlID="txtothSourceCountry"
                                                        UseContextKey="True" OnClientPopulating="AutoCompleteExtender_txtothSourceCountry_OnClientPopulating"  OnClientItemSelected="OthCountryautocompleteselected" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <!-- \Adult\ -->
                                        <div class="search-large-i" style="float: left; margin-top: 20px;">
                                            <!-- // -->
                                            <div class="srch-tab-line no-margin-bottom">
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        adult<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlOthAdult" class="custom-select custom-select-ddlAdult" runat="server"
                                                            meta:resourcekey="ddlOthAdultResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="srch-tab-3c">
                                                    <label>
                                                        Child</label>
                                                    <div class="select-wrapper">
                                                        <asp:DropDownList ID="ddlOthChild" class="custom-select custom-select-ddlChildren"
                                                            runat="server" meta:resourcekey="ddlOthChildResource1">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- \\ -->
                                        </div>
                                        <!-- \Adult\ -->
                                        <div id="divOthChild" runat="server" style="margin-top: 20px; margin-left: -1px">
                                            <div class="search-large-i-child" style="float: left;">
                                                <label style="text-align: left; padding-right: 2px;">
                                                    Ages of children at Other Services</label>


                                                 <div class="srch-tab-child" id="divOthChild4">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div24">
                                                            <div class="select-wrapper" style="width: 75px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtOthChild4" placeholder="CH 4" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="srch-tab-child" id="divOthChild3">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div23">
                                                            <div class="select-wrapper" style="width: 75px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtOthChild3" placeholder="CH 3" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                  <div class="srch-tab-child" id="divOthChild2">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div22">
                                                            <div class="select-wrapper" style="width: 75px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtOthChild2" placeholder="CH 2" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                      
                                                    </div>
                                                </div>

                                                <div class="srch-tab-child" id="divOthChild1">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div21">
                                                            <div class="select-wrapper" style="width: 75px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtOthChild1" placeholder="CH 1" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                               
                                             
                                                
                                            </div>
                                            <div class="search-large-i-child" style="float: left;">
                                                <label style="color: White;">
                                                    Ages of children at Other Services</label>

                                                        <div class="srch-tab-child" id="divOthChild5">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                       <div class="srch-tab-child-pre" id="div28">
                                                            <div class="select-wrapper" style="width: 75px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtOthChild5" placeholder="CH 5" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                                    
                                                 <div class="srch-tab-child" id="divOthChild6">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div27">
                                                            <div class="select-wrapper" style="width: 75px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtOthChild6" placeholder="CH 6" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            
                                                    <div class="srch-tab-child" id="divOthChild7">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                       <div class="srch-tab-child-pre" id="div26">
                                                            <div class="select-wrapper" style="width: 75px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtOthChild7" placeholder="CH 7" runat="server" onchange="validateAge(this)"
                                                                        onkeypress="validateDecimalOnly(event,this)" MaxLength="2"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                  <div class="srch-tab-child" id="divOthChild8">
                                                    <div class="select-wrapper" style="width: 75px;">
                                                        <div class="srch-tab-child-pre" id="div25">
                                                            <div class="select-wrapper" style="width: 75px;">
                                                                <div class="input-a">
                                                                    <asp:TextBox ID="txtOthChild8" placeholder="CH 8" runat="server" onchange="validateAge(this)"
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
                                        <!-- // advanced // -->
                                        <!-- \\ -->
                                    </div>
                                    <!-- \\ -->
                                    <!-- // -->
                                    <footer class="search-footer">
                                       <div class="search-large-i">
						                     <div  class="srch-tab-left" >
                                          <asp:Button ID="btnOthsearch" class="authorize-btn"  
                                                     OnClientClick="return ValidateOthSearch()"  runat="server" Text="Search" 
                                                     meta:resourcekey="btnOthsearchResource1"></asp:Button>  
                                          </div>
                                        <div  class="srch-tab-left">
                                        <input  id="btnOthreset"  type="button"  class="srch-btn-home"  value="Reset"/>
                                        </div>
                                     </div>
					              	<div class="clear"></div>
				                </footer>
                                </div>
                                <!-- // tab content Other Services // -->
                                 <!-- // tab content Pre Arranged Hotels // -->
                                  <div class="search-tab-content">
                                  <div class="page-search-p">

                  <div class="search-large-i">
                      <!-- // -->
                      <div class="srch-tab-line no-margin-bottom">
                          <div class="srch-tab-left">
                              <label>
                                 CHECK-IN<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                              <div class="input-a">
                                  <asp:TextBox ID="txtPreHotelFromDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-check-in"
                                      meta:resourcekey="txtPreHotelFromDateResource1"></asp:TextBox>
                                  <span class="date-icon-oth"></span>
                              </div>
                          </div>
                          <div class="srch-tab-right">
                              <label>
                                  CHECK-OUT<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
                              <div class="input-a">
                                  <asp:TextBox ID="txtPreHotelToDate" runat="server" placeholder="dd/mm/yyyy" CssClass="date-inpt-prehotel-check-out"
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
                      <%--Added shahul 10/11/18--%>
                      <asp:CheckBox ID="chkshowhotel"  runat="server"  />
                          <asp:Label ID="Label5" runat="server" CssClass="page-search-content-override-price"
                          Text="Show HotelName In Invoice" ></asp:Label>
                      <div class="clear">
                      </div>
                  </div>
                  <div class="search-large-i">
                      <label>
                          LOCATION<i class="fa fa-star" style="font-size:6px;color:red; position: absolute;"></i></label>
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
                                  <!-- // tab content Pre Arranged Hotels // -->
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <asp:Timer runat="server" ID="Timer1" Interval="1000" Enabled="false" OnTick="Timer1_Tick" />
                    <div class="mp-offesr">
                        <div class="wrapper-padding-a">
                            <div class="offer-slider">
                                <header class="fly-in page-lbl">
					<div class="offer-slider-lbl">Check out these awesome deals</div>
					<p>Handpicked by our destination specialists, we can guarantee that these are the very best rates that you will see anywhere. Got a better rate? Let us know.</p>
				</header>
                                <div class="fly-in offer-slider-c">
                                    <div id="offers" runat="server" class="owl-slider">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Literal ID="ltrlPopularDeal" runat="server" meta:resourcekey="ltrlPopularDealResource1"></asp:Literal>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="mp-b">
                <div class="wrapper-padding">
                    <%--Image Map Common & Dynamic HTML--%>
                    <div class="fly-in mp-b-left" id="imagemapcommon" runat="server">
                          
                        <div class="mp-b-lbl">
                            choose hotel by region</div>
                        <!-- // regions // -->                       

                        <div class="regions">                            
                            <div class="regions1-holder">
                                <img id="imagemap" name="imagemap" src="img/arabiya.png"   alt="Planets" width="347px" usemap="#planetmap"/>
                            <map name="planetmap" >    
                                <area id="uae" onmouseover="imgloader('img/uae.png')" onmouseout="imagecancel()"   target="_blank" alt="UAE" title="UAE" href="UAE" coords="220,65,205,77,189,80,194,93,209,96,219,87,221,75" shape="poly">
                                <area id="oman" onmouseover="imgloader('img/oman.png')" onmouseout="imagecancel()" target="_blank" alt="Oman" title="Oman" href="Oman" coords="223,61,221,85,212,97,213,111,191,121,196,141,225,131,250,87,226,81" shape="poly">
                            </map>
                            <div class="uae"></div>
                            <div class="oman"></div>
                            </div>
                        </div>
                        <!-- // regions // -->
                        <nav class="regions1-nav">
						<ul>
							<li><a class="uae" onmouseover="imgloader('img/uae.png')" onmouseout="imagecancel()" href="#">UAE</a></li>
							<li><a class="oman" onmouseover="imgloader('img/oman.png')" onmouseout="imagecancel()" href="#">OMAN</a></li>
						</ul>
					</nav>                    
                    </div>                                  


                    <div class="fly-in mp-b-right">
                        <div class="mp-b-lbl">
                            reasons to book with us</div>
                        <div class="reasons-item-a">
                            <div class="reasons-lbl">
                                Hotels, excursions, and more...</div>
                            <div class="reasons-txt">
                                Best hotel deals for Dubai or anywhere else in the UAE, transfers, or tickets. It's
                                all here.
                            </div>
                        </div>
                        <div class="reasons-item-b">
                            <div class="reasons-lbl">
                                Best rates</div>
                            <div class="reasons-txt">
                                Got a better rate? We'll match it or even better it. That's a promise!.
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="reasons-item-c">
                            <div class="reasons-lbl">
                                Choose from 500+ Hotels in the UAE</div>
                            <div class="reasons-txt">
                                3 star, 4 star, 5 star...we got them all!.
                            </div>
                        </div>
                        <div class="reasons-item-d">
                            <div class="reasons-lbl">
                                Booking made easy</div>
                            <div class="reasons-txt">
                                Search, filter, select - your booking is just 3 clicks away.
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="clear">
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
		    <div class="footer-adress"> <asp:Label ID="lblFAdd1" runat="server" 
                    meta:resourcekey="lblFAdd1Resource1"></asp:Label>, <br />
                <asp:Label ID="lblFAdd2" runat="server" meta:resourcekey="lblFAdd2Resource1"></asp:Label>,<br />
                <asp:Label ID="lblFAdd3" runat="server" meta:resourcekey="lblFAdd3Resource1"></asp:Label>, 
                <asp:Label ID="lblFAdd4" runat="server" meta:resourcekey="lblFAdd4Resource1"></asp:Label></div>
			<div class="footer-phones"><asp:Label ID="lblFPhone" runat="server" 
                    meta:resourcekey="lblFPhoneResource1"></asp:Label></div>
			<div class="footer-email"> <asp:Label ID="lblFEmail" runat="server" 
                    meta:resourcekey="lblFEmailResource1"></asp:Label></div>
			<div class="footer-timimg"><asp:Label ID="lblFWorkingTime" runat="server" 
                    meta:resourcekey="lblFWorkingTimeResource1"></asp:Label></div>

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
		<div class="footer-social">
					</div>
		<div class="clear"></div>
	</div>
</footer>
    <center>
        <div id="Loading1" runat="server" style="height: 150px; width: 500px;">
            <img alt="" id="Image1" runat="server" src="~/img/page-loader.gif" />
            <h2 style="display: none;" class="page-loader-label">
                Processing please wait...</h2>
        </div>
    </center>
    <asp:ModalPopupExtender ID="ModalPopupDays" runat="server" TargetControlID="btnInvisibleGuest"
        CancelControlID="btnClose" PopupControlID="Loading1" BackgroundCssClass="ModalPopupForPageLoading"
        DynamicServicePath="" Enabled="True">
    </asp:ModalPopupExtender>
    <input id="btnInvisibleGuest" runat="server" type="button" value="Cancel" style="display: none" />
    <input id="btnClose" type="button" value="Cancel" style="display: none" />
    <asp:HiddenField ID="hdLoginType" runat="server" />
    <asp:HiddenField ID="hdTab" runat="server" />
    <asp:HiddenField ID="hdAbsoluteUrl" runat="server" />
    <asp:HiddenField ID="hdChildAgeLimit" runat="server" />
    <asp:HiddenField ID="hdMaxNoOfNight" runat="server" />
    <asp:HiddenField ID="hdBookingEngineRateType" runat="server" />
    <asp:HiddenField ID="hdWhiteLabel" runat="server" />
    <asp:HiddenField ID="hdThreadvalue" runat="server" />
    <asp:HiddenField ID="hdmealcode" runat="server" />
    </form>



</body>
 


    
</html>
