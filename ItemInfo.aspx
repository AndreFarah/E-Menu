<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemInfo.aspx.cs" Inherits="ItemInfo" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/ipad.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableTheming="True">
    </telerik:RadScriptManager>
    <script>

        document.body.style.zoom = 2;
        
    </script>
    <script language="javascript" type="text/javascript">

        function Select(ConfirmOrCancel) {
            GetRadWindow().Close(ConfirmOrCancel);
        }
        function Close() {
            GetRadWindow().Close();
        }


    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gBasket">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gBasket" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblTotalPrice" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="20">
        <img alt="Loading..." src="" style="border: 0;" />
    </telerik:RadAjaxLoadingPanel>
    <div align="center">
        <table>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAlternate" runat="server" Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <telerik:RadBinaryImage runat="server" ID="rPic" AutoAdjustImageControlSize="false"
                        Width="90px" Height="110px" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <%--<asp:TextBox ID="txt" runat="server"></asp:TextBox>
                    <asp:Button ID="btn" runat="server" Text="Select" OnClick="btn_Click" />--%>
                    <asp:Label ID="lblDsc" runat="server" Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:restaurantposConnectionString %>"
                        ProviderName="<%$ ConnectionStrings:restaurantposConnectionString.ProviderName %>"
                        SelectCommand="SELECT FoodBeverageCode, GroupCode, FoodBeverageDescription, FoodBeveragePicture FROM tblfoodbeverage">
                    </asp:SqlDataSource>
                    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                        GridLines="None">
                        <MasterTableView DataKeyNames="FoodBeverageCode" DataSourceID="SqlDataSource1">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="FoodBeverageCode" HeaderText="FoodBeverageCode"
                                    ReadOnly="True" SortExpression="FoodBeverageCode" UniqueName="FoodBeverageCode">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="GroupCode" HeaderText="GroupCode" SortExpression="GroupCode"
                                    UniqueName="GroupCode">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FoodBeverageDescription" HeaderText="FoodBeverageDescription"
                                    SortExpression="FoodBeverageDescription" UniqueName="FoodBeverageDescription">
                                </telerik:GridBoundColumn>
                                <telerik:GridBinaryImageColumn DataField="FoodBeveragePicture" ImageHeight="30px" ImageWidth="30px"
                                    UniqueName="column">
                                </telerik:GridBinaryImageColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>--%>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
