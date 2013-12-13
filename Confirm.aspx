<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="Confirm" %>

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
                                                            <cc1:RadMsgBox ID="Messagebox" runat="server" OnNoSelected="Messagebox_NoSelected"
                                                                OnYesSelected="Messagebox_YesSelected" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <%--<asp:Panel ID="pnlLogin" runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <telerik:RadTextBox ID="txtEmpCode" runat="server" AutoCompleteType="Disabled" EmptyMessage="Employee Code"
                                                                                Font-Size="18pt" SelectionOnFocus="SelectAll" Skin="Windows7" Width="195px">
                                                                            </telerik:RadTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Login"
                                                                                ForeColor="Blue" Font-Size="Small" Font-Underline="False" Font-Names="Arial" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>--%>
                                                            <asp:Panel ID="pnlTableNumber" runat="server" Visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <telerik:RadTextBox ID="txtTableNumber" runat="server" AutoCompleteType="Disabled"
                                                                                EmptyMessage="Table Number" Font-Size="18pt" SelectionOnFocus="SelectAll" Skin="Windows7"
                                                                                Width="195px">
                                                                            </telerik:RadTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnConfirmTableNumber" runat="server" Text="Confirm" ForeColor="Blue"
                                                                                Font-Size="Small" Font-Underline="False" Font-Names="Arial" OnClick="btnConfirmTableNumber_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="right" width="140px">
                                                            <div id="Div1" style="cursor: pointer; width: 80px;" onclick="return Close();">
                                                                <asp:Label ID="lblClose" runat="server" Text="Close" ForeColor="Blue" Font-Size="Small"
                                                                    Font-Underline="False" Font-Names="Arial" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <telerik:RadListView ID="lTable" runat="server" ItemPlaceholderID="EmployeesContainer"
                                                OnNeedDataSource="RadListView1_NeedDataSource" OnSelectedIndexChanged="RadListView1_SelectedIndexChanged"
                                                OnItemCommand="RadListView1_ItemCommand" Skin="Windows7" OnItemCreated="lTable_ItemCreated"
                                                OnPreRender="lTable_PreRender">
                                                <LayoutTemplate>
                                                    <fieldset>
                                                        <legend>Tables</legend>
                                                        <div align="left">
                                                            <asp:PlaceHolder ID="EmployeesContainer" runat="server" />
                                                        </div>
                                                    </fieldset>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnTable" runat="server" Text='<%# Eval("TableNumber") %>' CommandName="Selected"
                                                        CommandArgument='<%# Eval("ID") %>' Font-Names="Arial" Font-Size="15px" Font-Bold="false" />
                                                </ItemTemplate>
                                            </telerik:RadListView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
    </form>
</body>
</html>
