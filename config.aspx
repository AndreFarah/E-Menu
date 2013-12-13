<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="RadMessageBox" Namespace="RadMessageBox" TagPrefix="cc1" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="config.aspx.cs" Inherits="config" %>

<%@ Register Src="GridRow.ascx" TagName="GridRow" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMenu Configuration</title>
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
    <telerik:RadAjaxManager ID="RadAjaxManager11" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel11"
        LoadingPanelID="LoadingPanel1">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel11" runat="server" Skin="Windows7"
        Transparency="50">
    </telerik:RadAjaxLoadingPanel>
    <br />
    <br />
    <br />
    <div align="center">
        <table style="background-color: #FFFFFF">
            <tr>
                <td align="right">
                    <asp:Label ID="lblCurrency0" runat="server" Text="Registry is : "></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="isRegistryAvailable" runat="server" Font-Bold="True" 
                        Font-Italic="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                   
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCurrency" runat="server" Text="Currency : "></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadTextBox ID="txtCurrency" runat="server" Width="120px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td align="right" height="35px">
                    <asp:Label ID="lblFloatPattern" runat="server" Text="Float Pattern"></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadTextBox ID="txtFloatPattern" runat="server" Width="120px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" Text="Font 1 (Size)"></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbFont1" runat="server" Width="120px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="10" Value="10" />
                            <telerik:RadComboBoxItem runat="server" Text="11" Value="11" />
                            <telerik:RadComboBoxItem runat="server" Text="12" Value="12" />
                            <telerik:RadComboBoxItem runat="server" Text="13" Value="13" />
                            <telerik:RadComboBoxItem runat="server" Text="14" Value="14" />
                            <telerik:RadComboBoxItem runat="server" Text="15" Value="15" />
                            <telerik:RadComboBoxItem runat="server" Text="16" Value="16" />
                            <telerik:RadComboBoxItem runat="server" Text="17" Value="17" />
                            <telerik:RadComboBoxItem runat="server" Text="18" Value="18" />
                            <telerik:RadComboBoxItem runat="server" Text="19" Value="19" />
                            <telerik:RadComboBoxItem runat="server" Text="20" Value="20" />
                            <telerik:RadComboBoxItem runat="server" Text="21" Value="21" />
                            <telerik:RadComboBoxItem runat="server" Text="22" Value="22" />
                            <telerik:RadComboBoxItem runat="server" Text="23" Value="23" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text="Font 2 (Size)"></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbFont2" runat="server" Width="120px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="10" Value="10" />
                            <telerik:RadComboBoxItem runat="server" Text="11" Value="11" />
                            <telerik:RadComboBoxItem runat="server" Text="12" Value="12" />
                            <telerik:RadComboBoxItem runat="server" Text="13" Value="13" />
                            <telerik:RadComboBoxItem runat="server" Text="14" Value="14" />
                            <telerik:RadComboBoxItem runat="server" Text="15" Value="15" />
                            <telerik:RadComboBoxItem runat="server" Text="16" Value="16" />
                            <telerik:RadComboBoxItem runat="server" Text="17" Value="17" />
                            <telerik:RadComboBoxItem runat="server" Text="18" Value="18" />
                            <telerik:RadComboBoxItem runat="server" Text="19" Value="19" />
                            <telerik:RadComboBoxItem runat="server" Text="20" Value="20" />
                            <telerik:RadComboBoxItem runat="server" Text="21" Value="21" />
                            <telerik:RadComboBoxItem runat="server" Text="22" Value="22" />
                            <telerik:RadComboBoxItem runat="server" Text="23" Value="23" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Text="Font 3 (Size)"></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbFont3" runat="server" Width="120px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="10" Value="10" />
                            <telerik:RadComboBoxItem runat="server" Text="11" Value="11" />
                            <telerik:RadComboBoxItem runat="server" Text="12" Value="12" />
                            <telerik:RadComboBoxItem runat="server" Text="13" Value="13" />
                            <telerik:RadComboBoxItem runat="server" Text="14" Value="14" />
                            <telerik:RadComboBoxItem runat="server" Text="15" Value="15" />
                            <telerik:RadComboBoxItem runat="server" Text="16" Value="16" />
                            <telerik:RadComboBoxItem runat="server" Text="17" Value="17" />
                            <telerik:RadComboBoxItem runat="server" Text="18" Value="18" />
                            <telerik:RadComboBoxItem runat="server" Text="19" Value="19" />
                            <telerik:RadComboBoxItem runat="server" Text="20" Value="20" />
                            <telerik:RadComboBoxItem runat="server" Text="21" Value="21" />
                            <telerik:RadComboBoxItem runat="server" Text="22" Value="22" />
                            <telerik:RadComboBoxItem runat="server" Text="23" Value="23" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label4" runat="server" Text="Font Request (Size)"></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbFont_Batches" runat="server" Width="120px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="10" Value="10" />
                            <telerik:RadComboBoxItem runat="server" Text="11" Value="11" />
                            <telerik:RadComboBoxItem runat="server" Text="12" Value="12" />
                            <telerik:RadComboBoxItem runat="server" Text="13" Value="13" />
                            <telerik:RadComboBoxItem runat="server" Text="14" Value="14" />
                            <telerik:RadComboBoxItem runat="server" Text="15" Value="15" />
                            <telerik:RadComboBoxItem runat="server" Text="16" Value="16" />
                            <telerik:RadComboBoxItem runat="server" Text="17" Value="17" />
                            <telerik:RadComboBoxItem runat="server" Text="18" Value="18" />
                            <telerik:RadComboBoxItem runat="server" Text="19" Value="19" />
                            <telerik:RadComboBoxItem runat="server" Text="20" Value="20" />
                            <telerik:RadComboBoxItem runat="server" Text="21" Value="21" />
                            <telerik:RadComboBoxItem runat="server" Text="22" Value="22" />
                            <telerik:RadComboBoxItem runat="server" Text="23" Value="23" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <%--<tr>
                <td align="right" height="35px">
                    <asp:Label ID="Label5" runat="server" Text="Counter Code"></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbCounterCode" runat="server" Width="120px" DataTextField="Description"
                        DataValueField="Code">
                    </telerik:RadComboBox>
                </td>
            </tr>--%>
            <tr>
                <td align="right" height="35px" valign="bottom">
                    <asp:Label ID="Label6" runat="server" Text="Tooltip Picture Size"></asp:Label>
                </td>
                <td align="left" valign="bottom">
                    <telerik:RadTextBox ID="txtTooltipPictureSize" runat="server" Width="120px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label7" runat="server" Text="Picture Visible : "></asp:Label>
                </td>
                <td align="left">
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rdoPictureVisibleTrue" runat="server" Text="True" GroupName="PictureVisible" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rdoPictureVisibleFalse" runat="server" Text="False" GroupName="PictureVisible" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" height="70px">
                    <asp:Label ID="Label8" runat="server" Text="Confirmation Messagebox Show : "></asp:Label>
                </td>
                <td align="left">
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rdoConfirmationMessageboxShowTrue" runat="server" Text="True"
                                    GroupName="rdoConfirmationMessageboxShow" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rdoConfirmationMessageboxShowFalse" runat="server" Text="False"
                                    GroupName="rdoConfirmationMessageboxShow" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" height="35px" valign="bottom">
                    <asp:Label ID="Label9" runat="server" Text="Table Size - Width : "></asp:Label>
                </td>
                <td align="left" valign="bottom">
                    <telerik:RadTextBox ID="txtTableSize_Width" runat="server" Width="120px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label10" runat="server" Text="Table Size - eight : "></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadTextBox ID="txtTableSize_Height" runat="server" Width="120px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label11" runat="server" Text="Table Font : "></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbTableFont" runat="server" Width="120px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="10" Value="10" />
                            <telerik:RadComboBoxItem runat="server" Text="11" Value="11" />
                            <telerik:RadComboBoxItem runat="server" Text="12" Value="12" />
                            <telerik:RadComboBoxItem runat="server" Text="13" Value="13" />
                            <telerik:RadComboBoxItem runat="server" Text="14" Value="14" />
                            <telerik:RadComboBoxItem runat="server" Text="15" Value="15" />
                            <telerik:RadComboBoxItem runat="server" Text="16" Value="16" />
                            <telerik:RadComboBoxItem runat="server" Text="17" Value="17" />
                            <telerik:RadComboBoxItem runat="server" Text="18" Value="18" />
                            <telerik:RadComboBoxItem runat="server" Text="19" Value="19" />
                            <telerik:RadComboBoxItem runat="server" Text="20" Value="20" />
                            <telerik:RadComboBoxItem runat="server" Text="21" Value="21" />
                            <telerik:RadComboBoxItem runat="server" Text="22" Value="22" />
                            <telerik:RadComboBoxItem runat="server" Text="23" Value="23" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td align="right" height="70px">
                    <asp:Label ID="Label12" runat="server" Text="Sort Request By : "></asp:Label>
                </td>
                <td align="left">
                    <table>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rdoRequestSortByDescription" runat="server" Text="Description"
                                    GroupName="rdoRequestSortByDescription" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rdoRequestSortByFIFO" runat="server" Text="FIFO (First In First Out)"
                                    GroupName="rdoRequestSortByDescription" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label16" runat="server" Text="Print Setting" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label13" runat="server" Text="OrderList : "></asp:Label>
                </td>
                <td align="left">
                    <table>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rdoPrintOrderListTrue" runat="server" Text="True" GroupName="PrintOrderList" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rdoPrintOrderListFalse" runat="server" Text="False" GroupName="PrintOrderList" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label14" runat="server" Text="BarOrderList : "></asp:Label>
                </td>
                <td align="left">
                    <table>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rdoPrintBarOrderListTrue" runat="server" Text="True" GroupName="PrintBarOrderList" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rdoPrintBarOrderListFalse" runat="server" Text="False" GroupName="PrintBarOrderList" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label15" runat="server" Text="KitchenOrderList : "></asp:Label>
                </td>
                <td align="left">
                    <table>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rdoPrintKitchenOrderListTrue" runat="server" Text="True" GroupName="PrintKitchenOrderList" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rdoPrintKitchenOrderListFalse" runat="server" Text="False" GroupName="PrintKitchenOrderList" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td align="right">
                    <asp:Label ID="Label17" runat="server" Text="Function Visibility" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label18" runat="server" Text="Print Receipt : "></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbPrintReceipt" runat="server" Width="120px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Visible" Value="True" />
                            <telerik:RadComboBoxItem runat="server" Text="Invisible" Value="False" />
                            
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label19" runat="server" Text="Recall Order : "></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbRecallOrder" runat="server" Width="120px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Visible" Value="True" />
                            <telerik:RadComboBoxItem runat="server" Text="Invisible" Value="False" />
                            
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label20" runat="server" Text="Payment : "></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbPayment" runat="server" Width="120px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Visible" Value="True" />
                            <telerik:RadComboBoxItem runat="server" Text="Invisible" Value="False" />
                            
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
             <tr>
                <td align="right">
                    <asp:Label ID="Label21" runat="server" Text="Language : "></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadComboBox ID="cmbLanguage" runat="server" Width="120px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Visible" Value="True" />
                            <telerik:RadComboBoxItem runat="server" Text="Invisible" Value="False" />
                            
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
             <tr>
                <td align="right">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
             <tr>
                <td align="right">
                    <asp:Label ID="Label22" runat="server" Text="Gap Between Items" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                    <telerik:RadNumericTextBox ID="txtGapBetweenItems" runat="server" Width="120px" Type="Number" DataType="System.Int">
                    </telerik:RadNumericTextBox>
                 </td>
            </tr>
        </table>
        <table>
            <tr>
                <td valign="top" align="right">
                    <asp:Panel ID="pnlbasket" runat="server">
                        <table style="width: 100%; height: 40px">
                            <tr>
                                <td align="left" width="180px">
                                    <asp:LinkButton ID="btnBackToMenu" runat="server" Text="Back to eMenu" Font-Names="Arial"
                                        Font-Underline="False" ForeColor="Blue" OnClick="btnBackToMenu_Click" />
                                </td>
                                <td align="right" width="180px">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" Font-Names="Arial" Font-Underline="False"
                                        ForeColor="Blue" OnClick="btnSave_Click" Font-Bold="True" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
