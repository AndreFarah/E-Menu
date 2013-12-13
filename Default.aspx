<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="RadMessageBox" Namespace="RadMessageBox" TagPrefix="cc1" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%@ Register Src="GridRow.ascx" TagName="GridRow" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Emenu - Bersian Technology(M) - SDN BHD</title>
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
    <script type="text/javascript">
        //Put your JavaScript code here.
    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel11"
        LoadingPanelID="LoadingPanel1" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="toolbar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbHeader" />
                    <telerik:AjaxUpdatedControl ControlID="panelbar" />
                    <telerik:AjaxUpdatedControl ControlID="gMenu" />
                    <telerik:AjaxUpdatedControl ControlID="pnlbasket" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tbHeader">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="wPrintReciept" />
                    <telerik:AjaxUpdatedControl ControlID="gMenu" />
                    <telerik:AjaxUpdatedControl ControlID="pnlbasket" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="panelbar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gMenu" />
                    <telerik:AjaxUpdatedControl ControlID="pnlbasket" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gMenu">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblCurrency" />
                    <telerik:AjaxUpdatedControl ControlID="lblTotalPrice" />
                    <telerik:AjaxUpdatedControl ControlID="btnRefreshGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gMenu" />
                    <telerik:AjaxUpdatedControl ControlID="pnlbasket" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRefreshGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="hfMenuSelected" />
                    <telerik:AjaxUpdatedControl ControlID="gMenu" />
                    <telerik:AjaxUpdatedControl ControlID="pnlbasket" />
                    <telerik:AjaxUpdatedControl ControlID="btnRefreshGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel11" runat="server" Skin="Windows7"
        Transparency="50">
    </telerik:RadAjaxLoadingPanel>
    <table width="100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <img alt="" src="Images/logo.png" width="110px" />
                        </td>
                        <td>
                            <telerik:RadToolBar runat="server" ID="toolbar" Skin="Windows7" OnButtonClick="toolbar_ButtonClick"
                                Font-Names="Arial" Font-Bold="True">
                                <Items>
                                    <telerik:RadToolBarButton runat="server" Text="A" Value="A1" Height="40px" Width="28px"
                                        Font-Size="14">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" IsSeparator="True" PostBack="False" Text="Button 3">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" Text="A" Value="A2" Font-Size="16" Checked="True"
                                        Height="40px" Width="28px">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" IsSeparator="True" PostBack="False" Text="Button 3">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" Text="A" Value="A3" Font-Size="18" Height="40px"
                                        Width="28px">
                                    </telerik:RadToolBarButton>
                                </Items>
                            </telerik:RadToolBar>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <telerik:RadToolBar runat="server" ID="tbHeader" Skin="Windows7" OnButtonClick="tbHeader_ButtonClick"
                                Font-Names="Arial" Font-Bold="True" Width="100%">
                                <Items>
                                    <telerik:RadToolBarButton runat="server" Text="Log Out" Value="LogOut" Width="80px"
                                        ForeColor="Blue" Height="40px" CommandName="LogOut">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" IsSeparator="True" PostBack="False" Text="Separator"
                                        Height="40px">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" Text="Print Receipt" Value="PrintReciept"
                                        Width="105px" ForeColor="Blue" Height="40px" CommandName="PrintReciept">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" IsSeparator="True" PostBack="False" Text="Separator"
                                        Height="40px">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" Text="Recall Order" Value="RecallOrder"
                                        CommandName="RecallOrder" Width="105px" ForeColor="Blue" Height="40px">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" IsSeparator="True" PostBack="False" Text="Separator">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" Text="Payment" Value="Payment" CommandName="Payment"
                                        Width="105px" ForeColor="Blue" Height="40px">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" IsSeparator="True" PostBack="False" Text="Separator"
                                        Height="40px">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarDropDown runat="server" Text="Language" Width="95px" ForeColor="Blue"
                                        Height="40px">
                                        <Buttons>
                                            <telerik:RadToolBarButton Text="Main Language" Group="Bold" CheckOnClick="true" AllowSelfUnCheck="true"
                                                Value="Main" CommandName="Main" Height="35px">
                                            </telerik:RadToolBarButton>
                                            <telerik:RadToolBarButton Text="Alternate Language" Group="Italic" CheckOnClick="true"
                                                AllowSelfUnCheck="true" Value="Alternate" CommandName="Alternate" Height="35px">
                                            </telerik:RadToolBarButton>
                                        </Buttons>
                                    </telerik:RadToolBarDropDown>
                                    <telerik:RadToolBarButton runat="server" IsSeparator="True" PostBack="False" Text="Separator">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" Text="How To Use" Value="HowToUse" Width="90px"
                                        ForeColor="Blue" NavigateUrl="~/Edu.aspx" Height="40px" CommandName="HowToUse">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" IsSeparator="True" PostBack="False" Text="Separator"
                                        Height="40px">
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" Text="View Order" Value="ViewOrder" CommandName="ViewOrder"
                                        Width="105px" ForeColor="Blue" Height="40px">
                                    </telerik:RadToolBarButton>
                                </Items>
                            </telerik:RadToolBar>
                            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" ViewStateMode="Disabled">
                                <Windows>
                                    <telerik:RadWindow ID="wPrintReciept" runat="server" KeepInScreenBounds="True" NavigateUrl="TableConfirmation.aspx"
                                        ReloadOnShow="True" VisibleStatusbar="False" Animation="Fade" Font-Size="18pt"
                                        VisibleTitlebar="False" Width="550px" VisibleOnPageLoad="false" Modal="True">
                                    </telerik:RadWindow>
                                    <%--<telerik:RadWindow ID="wPWP" runat="server" KeepInScreenBounds="True" NavigateUrl="PWP.aspx"
                                        ReloadOnShow="True" VisibleStatusbar="False" Animation="Fade" Font-Size="18pt"
                                        VisibleTitlebar="False" Width="550px" VisibleOnPageLoad="false">
                                    </telerik:RadWindow>--%>
                                </Windows>
                            </telerik:RadWindowManager>
                            <cc1:RadMsgBox ID="MessageBox" runat="server" OnYesSelected="MessageBox_YesSelected" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" width="25%">
                <asp:HiddenField ID="hfMenuSelected" runat="server" />
                <telerik:RadPanelBar runat="server" ID="panelbar" Font-Names="Arial" Skin="Outlook"
                    OnItemClick="panelbar_ItemClick1" Width="100%">
                    <CollapseAnimation Type="InOutQuint"></CollapseAnimation>
                    <ExpandAnimation Type="InQuad" />
                </telerik:RadPanelBar>
            </td>
            <td valign="top" align="right">
                <telerik:RadCodeBlock ID="radCodeBlock1" runat="server">
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
                <telerik:RadGrid ID="gMenu" runat="server" Skin="Windows7" AutoGenerateColumns="False"
                    GridLines="None" Font-Bold="True" Font-Size="Medium" HorizontalAlign="Center"
                    PageSize="100" OnItemCommand="gMenu_ItemCommand" OnNeedDataSource="gMenu_NeedDataSource">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" ShowHeader="False">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="Menu">
                                <ItemTemplate>
                                    <uc1:GridRow ID="GridRow1" runat="server" FbCode='<%# Eval("ID") %>' Description='<%# Eval("Dsc") %>'
                                        Currency="RM" Price='<%# Eval("Price") %>' Pic='<%# Eval("Pic") %>' ExpandedRow='<%# Eval("Expanded") %>'
                                        Quantity='<%# Eval("Quantity") %>' FoatPattern='<%# Eval("FoatPattern") %>' Font_Size='<%# Eval("Font") %>'
                                        PicVisible='<%# Eval("PicVisible") %>' HasPWP='<%# Eval("HasPWP") %>' IsPWP='<%# Eval("IsPWP") %>' />
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
                <asp:Panel ID="pnlbasket" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <telerik:RadWindow ID="wConfirm" runat="server" KeepInScreenBounds="True" NavigateUrl="Confirm.aspx"
                                    OpenerElementID="btnConfirm" ReloadOnShow="True" Skin="Web20" VisibleStatusbar="False"
                                    Animation="Fade" Font-Size="18pt" VisibleTitlebar="False" Width="550px" Modal="True">
                                </telerik:RadWindow>
                            </td>
                            <td align="right">
                                <asp:Label ID="Label1" runat="server" Text="Total Price:" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="X-Small"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblCurrency" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8px"
                                    Text="RM"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblTotalPrice" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="X-Small" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 40px">
                        <tr>
                            <td align="left">
                                <%-- <asp:LinkButton runat="server" ID="btnBack" Text="Back" OnClick="btnBack_Click"></asp:LinkButton>--%>
                                <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel Order" ForeColor="Blue"
                                    OnClick="btnCancel_Click" Font-Size="X-Small" Font-Underline="False" Font-Names="Arial" />
                            </td>
                            <td align="right">
                                <div id="divBtnConfirm" style="cursor: pointer; width: 80px;">
                                    <asp:Label ID="btnConfirm" runat="server" Font-Names="Arial" Font-Underline="False"
                                        ForeColor="Blue" Text="Confirm" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
