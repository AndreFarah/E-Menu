<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login2.aspx.cs" Inherits="login2" %>

<%@ Register Assembly="RadMessageBox" Namespace="RadMessageBox" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/ipad.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultfocus="txtEmpCode">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="9000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="pnl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="50"
        HorizontalAlign="Center">
    </telerik:RadAjaxLoadingPanel>
    <div align="center">
        <asp:Panel ID="pnl" runat="server" Font-Names="Arial">
            <asp:HiddenField runat="server" ID="hfRefresh" Value="close" />
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script language="javascript" type="text/javascript">
                    function GetRadWindow() {
                        var oWindow = null;
                        if (window.radWindow) oWindow = window.radWindow;
                        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                        return oWindow;
                    }

                    function Select(ConfirmOrCancel) {
                        GetRadWindow().Close(ConfirmOrCancel);
                    }
                    function Close() {
                        if (document.getElementById('<%=hfRefresh.ClientID%>').value == "refresh") {
                            GetRadWindow().BrowserWindow.location.reload();
                        }
                        GetRadWindow().Close();
                    }


                </script>
            </telerik:RadCodeBlock>
            <input id="txtAuthenticationString" name="txtAuthenticationString" style="display: none;"
                            type="text" />
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <table>
                            <tr>
                                <td align="center" valign="middle">
                                    <asp:Panel ID="pnlEmployeeCode" runat="server">
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table width="100%">
            <tr>
                <td>
                    <asp:Panel ID="pnlLogin" runat="server">
                        <table>
                            <tr>
                                <td align="center">
                                    <telerik:RadTextBox ID="txtEmpCode" runat="server" AutoCompleteType="Disabled" EmptyMessage="Employee Code"
                                        Font-Size="18pt" SelectionOnFocus="SelectAll" Skin="Windows7" Width="195px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <div class="buttons">
                                        <asp:Button ID="btnLogin" runat="server" CommandName="Login" Text="Login" ValidationGroup="LoginForm"
                                            CssClass="adminButtonBlue" OnClientClick="return(validateSubmit());" OnClick="btnLogin_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                         <div class="login-block">
                            <table class="login-table-container">
                                <tbody>
                                    <tr class="row">
                                        <td class="item-name">
                                            <asp:Literal runat="server" ID="lCounterCode" Text="Counter Code : " />
                                        </td>
                                    </tr>
                                    <tr class="row">
                                        <td class="item-value">
                                            <telerik:RadTextBox ID="txtCounterCode" runat="server" Width="225px" EmptyMessage="Counter code"
                                                MaxLength="30" SelectionOnFocus="SelectAll" Skin="Windows7">
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="UserNameOrEmailRequired" runat="server" ControlToValidate="txtUserName"
                                                ErrorMessage="Username is required." ToolTip="Username is required." ValidationGroup="LoginForm">
											
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="row">
                                        <td class="message-error">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            <br />
                                            <asp:Label ID="lblError" runat="server" Font-Names="Tahoma" Font-Size="11px" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="row">
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
