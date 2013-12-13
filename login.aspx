<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="RadMessageBox" Namespace="RadMessageBox" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>Emenu</title>
	<link href="CSS/ipad.css" rel="stylesheet" type="text/css" />
	<%--<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />--%>
	<%--<style type="text/css">
		body
		{
			margin-left: 0px;
			margin-top: 30px;
			margin-right: 0px;
			margin-bottom: 0px;
			background-color: #f1fdc0;
		}
	</style>--%>
</head>
<body style="background-color: #efefef;">
	<script type="text/javascript" src="Javascript/md5.js"></script>
	<%--<telerik:RadCodeBlock ID="radCodeBlock1">--%>
	<script language="JavaScript" type="text/JavaScript">
		function validateSubmit() {
			var txtEmployeeCode = (document.getElementById('<%= txtEmployeeCode.ClientID %>').value).toLowerCase();
			//            document.getElementById('txtAuthenticationString').value = txtEmployeeCode; // hex_md5(txtEmployeeCode);
			document.getElementById('<%= txtEmployeeCode.ClientID %>').value = "";
			document.getElementById('<%= hfEmpCode.ClientID %>').value = hex_md5(txtEmployeeCode);

		}
	</script>
	<%--</telerik:RadCodeBlock>--%>
	<form id="form1" runat="server" style="font-family: Arial">
	
	<telerik:RadScriptManager ID="RadScriptManager2" runat="server" AsyncPostBackTimeout="900">
		<Scripts>
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
		</Scripts>
	</telerik:RadScriptManager>

    <asp:HiddenField ID="hfEmpCode" runat="server" />
	<div class="login-block">
		<table class="login-table-container">
			<tbody>
				<tr class="row">
					<td class="item-name">
						<asp:Label runat="server" ID="lEmployeeCode" Text="Employee Code : " Font-Size="13pt"></asp:Label>
					</td>
				</tr>
				<tr class="row">
					<td class="item-value">
						<telerik:RadTextBox ID="txtEmployeeCode" runat="server" AutoCompleteType="Disabled"
							Font-Size="13pt" SelectionOnFocus="SelectAll" Skin="Windows7" Width="195px" Font-Names="Arial">
						</telerik:RadTextBox>
					</td>
				</tr>
                <tr class="row">
					<td class="item-name">
						<asp:Label runat="server" ID="Label1" Text="Counter Code : " Font-Size="13pt"></asp:Label>
					</td>
				</tr>
				<tr class="row">
					<td class="item-value">
						
                    <telerik:RadComboBox ID="cmbCounterCode" runat="server"  DataTextField="Description"
                        DataValueField="Code"  Skin="Windows7" Width="195px" Font-Names="Arial" 
                            Font-Size="10pt" >
                    </telerik:RadComboBox>
						
					</td>
				</tr>
				<tr class="row">
					<td class="message-error">
						<asp:Label ID="lblError" runat="server" Font-Names="Tahoma" ForeColor="Red" Font-Size="11pt"></asp:Label>
					</td>
				</tr>
				<tr class="row">
					<td style="height: 40px" valign="bottom">
						<div align="right">
							<asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="adminButtonBlue"
								 OnClick="btnLogin_Click" Font-Size="13pt" />
                                 <%--OnClientClick="return(validateSubmit());"--%>
						</div>
					</td>
				</tr>
			</tbody>
		</table>
	</div>
	</form>
</body>
</html>
