<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="RadMessageBox" Namespace="RadMessageBox" TagPrefix="cc1" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PWP.aspx.cs" Inherits="PWP" %>

<%@ Register Src="PWP_GridRow.ascx" TagName="GridRow" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Emenu - Bersian Solution(M) - SDN BHD</title>
    <link href="CSS/ipad.css" rel="stylesheet" type="text/css" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
</head>
<body>
    <form id="form1" runat="server" style="font-family: Arial">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="900">
        <Scripts>
            <%--Needed for JavaScript IntelliSense in VS2010--%>
            <%--For VS2008 replace RadScriptManager with ScriptManager--%>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>
    </telerik:RadScriptManager>
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
        function CloseAndRefresh() {
            GetRadWindow().BrowserWindow.location.reload();
            GetRadWindow().Close();
        }

        function Close() {
            GetRadWindow().Close();
        }
    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager11" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel11"
        LoadingPanelID="LoadingPanel1">
        <AjaxSettings>
            <%--<telerik:AjaxSetting AjaxControlID="btnDone">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="MessageBox" />
                    <telerik:AjaxUpdatedControl ControlID="lblClose" />
                    <telerik:AjaxUpdatedControl ControlID="btnDone" />
                    <telerik:AjaxUpdatedControl ControlID="lblPWPmsg1" />
                    <telerik:AjaxUpdatedControl ControlID="lblPWPmsg2" />
                    <telerik:AjaxUpdatedControl ControlID="lblPWPmsg3" />
                    <telerik:AjaxUpdatedControl ControlID="codeBlock1" />

                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="gMenu1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblPWPmsg1" />
                    <telerik:AjaxUpdatedControl ControlID="gMenu1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gMenu2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblPWPmsg2" />
                    <telerik:AjaxUpdatedControl ControlID="gMenu2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gMenu3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblPWPmsg3" />
                    <telerik:AjaxUpdatedControl ControlID="gMenu3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel11" runat="server" Skin="Windows7"
        Transparency="50">
    </telerik:RadAjaxLoadingPanel>
    <table width="100%">
        <tr>
            <td align="right">
                <telerik:RadCodeBlock ID="codeBlock1" runat="server">
                    <script language="javascript" type="text/javascript">


                        function RowSelecting(sender, args) {

                            var id = args.get_id();
                            var inputCheckBox = $get(id).getElementsByTagName("input")[0];
                            if (!inputCheckBox || inputCheckBox.disabled) {
                                //cancel selection for disabled rows 
                                args.set_cancel(true);
                            }
                            if (inputCheckBox.checked == true) {
                                inputCheckBox.checked = false;
                                HighlightRow(inputCheckBox);
                            }
                            else {
                                inputCheckBox.checked = true;
                                HighlightRow(inputCheckBox);
                            }

                        }
                        function RowDeselecting(sender, args) {
                            alert("Deselect");
                            var id = args.get_id();
                            var inputCheckBox = $get(id).getElementsByTagName("input")[2];
                            if (!inputCheckBox || inputCheckBox.disabled) {
                                //cancel selection for disabled rows 
                                args.set_cancel(true);
                            }
                            inputCheckBox.checked = false;
                        }

                        function HighlightRow(chkB) {
                            var IsChecked = chkB.checked;
                            if (IsChecked) {
                                chkB.parentElement.parentElement.style.backgroundColor = '#EBEFF3';
                                chkB.parentElement.parentElement.style.color = 'blue';
                            } else {
                                chkB.parentElement.parentElement.style.backgroundColor = 'white';
                                chkB.parentElement.parentElement.style.color = 'black';
                            }
                        }
                    </script>
                </telerik:RadCodeBlock>
                <table width="100%">
                    <tr>
                        <td align="center" width="50%">
                            <cc1:RadMsgBox ID="MessageBox" runat="server" />
                            <div id="Div1" align="center" style="cursor: pointer; width: 80px;" onclick="return Close();">
                                <%--<asp:Label ID="lblClose" runat="server" Text="Cancel" ForeColor="Blue" Font-Size="Small"
                                    Font-Underline="False" Font-Names="Arial" />--%>
                            </div>
                        </td>
                        <td align="center">
                            <asp:LinkButton ID="btnDone" runat="server" Text="Done" Font-Underline="False" ForeColor="Blue"
                                Font-Size="Small" Font-Names="Arial" OnClick="btnDone_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lblPWPmsg1" runat="server" Font-Bold="false"  ForeColor="Red"
                    Text="" Visible="true"></asp:Label>
                <telerik:RadGrid ID="gMenu1" runat="server" Skin="Windows7" AutoGenerateColumns="False"
                    GridLines="None" Font-Bold="True" Font-Size="Medium" HorizontalAlign="Center"
                    PageSize="100">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" ShowHeader="False">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="Menu">
                                <ItemTemplate>
                                    <uc1:GridRow ID="GridRow1" runat="server" FbCode='<%# Eval("fbCode") %>' Description='<%# Eval("Dsc") %>'
                                        Currency="RM" Price='<%# Eval("Price") %>' VisibleMinus='<%# Eval("VisibleMinus") %>'
                                        ExpandedRow='<%# Eval("Expanded") %>' Quantity='<%# Eval("Quantity") %>' VisibleQuantity='<%# Eval("VisibleQuantity") %>'
                                        FoatPattern='<%# Eval("FoatPattern") %>' Font_Size='<%# Eval("Font") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="100%" HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="False" />
                    </ClientSettings>
                    <FilterMenu Skin="Blue" EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lblPWPmsg2" runat="server" Font-Bold="false" ForeColor="Red"
                    Text="" Visible="true"></asp:Label>
                <telerik:RadGrid ID="gMenu2" runat="server" Skin="Windows7" AutoGenerateColumns="False"
                    GridLines="None" Font-Bold="True" Font-Size="Medium" HorizontalAlign="Center"
                    PageSize="100">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" ShowHeader="False">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="Menu">
                                <ItemTemplate>
                                    <uc1:GridRow ID="GridRow2" runat="server" FbCode='<%# Eval("fbCode") %>' Description='<%# Eval("Dsc") %>'
                                        Currency="RM" Price='<%# Eval("Price") %>' VisibleMinus='<%# Eval("VisibleMinus") %>'
                                        ExpandedRow='<%# Eval("Expanded") %>' Quantity='<%# Eval("Quantity") %>' VisibleQuantity='<%# Eval("VisibleQuantity") %>'
                                        FoatPattern='<%# Eval("FoatPattern") %>' Font_Size='<%# Eval("Font") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="100%" HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="False" />
                    </ClientSettings>
                    <FilterMenu Skin="Blue" EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lblPWPmsg3" runat="server" Font-Bold="false"  ForeColor="Red"
                    Text="" Visible="true"></asp:Label>
                <telerik:RadGrid ID="gMenu3" runat="server" Skin="Windows7" AutoGenerateColumns="False"
                    GridLines="None" Font-Bold="True" Font-Size="Medium" HorizontalAlign="Center"
                    PageSize="100">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" ShowHeader="False">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="Menu">
                                <ItemTemplate>
                                    <uc1:GridRow ID="GridRow3" runat="server" FbCode='<%# Eval("fbCode") %>' Description='<%# Eval("Dsc") %>'
                                        Currency="RM" Price='<%# Eval("Price") %>' VisibleMinus='<%# Eval("VisibleMinus") %>'
                                        ExpandedRow='<%# Eval("Expanded") %>' Quantity='<%# Eval("Quantity") %>' VisibleQuantity='<%# Eval("VisibleQuantity") %>'
                                        FoatPattern='<%# Eval("FoatPattern") %>' Font_Size='<%# Eval("Font") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="100%" HorizontalAlign="Left"></ItemStyle>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="False" />
                    </ClientSettings>
                    <FilterMenu Skin="Blue" EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
