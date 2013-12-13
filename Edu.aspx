<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edu.aspx.cs" Inherits="Edu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ImageButton Width="100%" Height="100%" ID="btnImg" runat="server" 
            onclick="btnImg_Click" ImageUrl="~/Images/Edu.jpg" />
    </div>
    </form>
</body>
</html>
