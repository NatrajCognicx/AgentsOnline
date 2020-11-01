<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SubUserEmailConfirm.aspx.vb" Inherits="SubUserEmailConfirm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <style>
    .btn {
  font-family: Arial;
  color: #ffffff;
  font-size: 20px;
  background: #4F8A10;
  padding: 10px 10px 10px 10px;
  text-decoration: none;
  border:none;
}

.btn:hover {
  background: #ff7200;
  text-decoration: none;
}
.success
{
    width:50%;margin-left:20%;height:100px;padding:30px;background-color:#DFF2BF;margin-top:50px;text-align:center;
}
.warning
{
    width:50%;margin-left:20%;height:100px;padding:30px;background-color:#FEEFB3;margin-top:50px;text-align:center;
}
.lblSuccess
{
   color: #4F8A10;
}
.lblWarning
{
   color: #8a6d3b;
}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width:100%;height:100%;min-height:100%;">
    <div style="width:100%;height:100%; font-family: Arial;">
    <div id="dvWarning" runat="server" >
    <div>
       <asp:Label ID="lblWaring" CssClass="lblSuccess" runat="server"></asp:Label>
    </div>

    <div style="padding-top:15px;padding-bottom:25px;">
       <asp:Button ID="btnLogin" CssClass="btn" runat="server" Text="Login" />
    </div>
 
 
    </div>
    </div>
    </form>
</body>
</html>
