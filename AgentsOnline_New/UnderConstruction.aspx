<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UnderConstruction.aspx.vb"
    Inherits="UnderConstruction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .background
        {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(20,29,30,0.19);
        }
        .content
        {
            margin-left: 6%;
            margin-top: 6%;
            width: 70%;
            height: 100px;
            background-color: White;
            font-family: Arial;
            font-size: 25px;
            padding-bottom: 150px;
            padding-top: 150px;
            padding-left: 100px;
            padding-right: 100px;
            text-align: center;
            color: #ff7200;
            font-weight: bold;
            text-transform: uppercase;
            line-height: 2;
        }
        .header
        {
            top: 0;
            height: 50px;
            width: 100%;
            background-color: #141d1e;
        }
        .footer
        {
            bottom: 0;
            height: 50px;
            width: 100%;
            background-color: #141d1e;
            position: absolute;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="background">
        <div class="header">
        </div>
        <div class="content">
            <span>our website is undergoing major reconstruction and will be available soon.</span>
        </div>
        <div class="footer">
        </div>
    </div>
    </form>
</body>
</html>
