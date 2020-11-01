<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BookingHotelConfirm.aspx.vb"
    Inherits="BookingHotelConfirm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function validate() {
            if (confirm("Are you sure, Do you want to confirm the Room") == true) {
                for (var i = 0; i < arguments.length; i++) {
                    var entry = document.getElementById(arguments[i]).value;
                    if (entry.trim() == "") {
                        if (i % 2 != 0)
                            alert('Confirm By can not be empty');
                        else
                            alert('Confirmation Number can not be empty');
                        document.getElementById(arguments[i]).focus();
                        return false;
                        break;
                    }
                }
                ShowProgess();
                return true;
            }
            else {
                return false;
            }
        }
        function ShowProgess() {
            var ModalPopupDays = $find("ModalPopupDays");
            ModalPopupDays.show();
            return true;
        }  
    </script>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 12pt;
        }
        .MainDivStyle
        {
            position: relative;
            left: 5%;
            width: 90%;
        }
        .TableStyle
        {
            width: 100%;
            border-collapse: collapse;
            margin-top: 5pt;
        }
        .HeaderStyle
        {
            width: 45%;
            border-collapse: collapse;
            margin-top: 5pt;
        }
        .TableTd
        {
            width: 50%;
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: top;
        }
        .HeaderTd
        {
            width: 50%;
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: top;
            font-weight: bold;
        }
        .ClientStyle
        {
            width: 100%;
            border-collapse: collapse;
        }
        .TitleFont
        {
            font-size: 14pt;
            font-weight: bold;
            text-align: center;
        }
        .TitleFontBig
        {
            font-size: 18pt;
            font-weight: bold;
            text-align: center;
            text-decoration: underline;
        }
        .ServTitleTd
        {
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: middle;
            background-color: #d6d6d6;
            text-align: center;
            padding: 3pt;
        }
        .ServTd
        {
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: middle;
            color: rgb(51, 51, 255);
            font-weight: bold;
            text-align: center;
            padding: 3pt;
        }
        .TariffTd
        {
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: middle;
            color: rgb(0, 0, 0);
            font-weight: bold;
            text-align: right;
            padding-left: 3pt;
            padding-bottom: 3pt;
            padding-top: 3pt;
            padding-right: 5pt;
        }
        .TariffTotalTd
        {
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: middle;
            font-weight: bold;
            padding: 3pt;
        }
        .tblNotecss
        {
            margin-top: 7pt;
            width: 100%;
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: middle;
        }
        .NoteStyle
        {
            border-style: none;
            color: Black;
            font-weight: bold;
            text-decoration: underline;
            text-align: left;
            padding: 3pt;
        }
        .NoteRowStyle
        {
            border-style: none;
            color: Red;
            font-weight: bold;
            text-align: left;
            text-indent: 5pt;
        }
        .BillStyle
        {
            margin-top: 7pt;
            width: 100%;
            border-collapse:collapse;            
        }    
         .BillStyletd
        {
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: middle;
            color: Black;
            text-align: left;
            text-indent: 5pt;
            padding: 3pt;
        }    
        .BillHeader
        {
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: middle;
            color: Black;
            font-weight: bold;
            text-align:center;            
            padding: 3pt;
            background-color: #d8f6ca;
        }        
        .tblConfirmcss
        {
            margin-top: 5pt;
            width: 100%;
            border-style: none;
            border-collapse: collapse;
            vertical-align: middle;
        }
        .line
        {
            width: 100%;
            height: 0.2px;
            border-bottom: 1px solid Red;
        }
        .tblConfirmHotel
        {
            font-size: 14pt;
            font-weight: bold;
            text-align: center;
            padding: 5pt;
        }
        .tblConfirmTitle
        {
            border: 1px solid black;
            font-weight: bold;
            text-align: center;
            background-color: Red;
            color: White;
            padding: 5pt;
        }
        .tblConfirmCol
        {
            border: 1px solid black;
            font-weight: bold;
            text-align: center;
            padding: 2pt;
        }
        .txtCss
        {
            font-family: Arial;
            font-size: 14pt;
            font-weight: bold;
        }
        .btnCss
        {
            font-family: Times New Roman;
            font-size: 14pt;
            font-weight: bold;
            background-color: #034f13;
            color: White;
            cursor: pointer;
        }
        .InventoryStyle
        {
            width: 100%;
            border-collapse: collapse;
            margin-top: 7pt;
        }
        .InventoryStyle td
        {
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            vertical-align: middle;
            color: Black;
            text-align: center;
            padding: 3pt;
        }
        .FooterStyle
        {
            width: 100%;
            border-collapse: collapse;
            margin-top: 7pt;
        }
        .FooterStyle td
        {
            border-style: none;
            vertical-align: middle;
            color: Black;
            text-align: left;
            padding-top: 10pt;
            padding-bottom: 15pt;
        }
        .para
        {
            text-align: center;
            font-weight: bold;
            color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="MainDiv" align="center" runat="server" class="MainDivStyle">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
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
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
