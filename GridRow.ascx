<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridRow.ascx.cs" Inherits="GridRow" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="pnlButtunPanel">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="pnl" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="pnlInnerBasket">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblQty" />
                <telerik:AjaxUpdatedControl ControlID="pnlInnerBasket" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="pnlRequest">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="pnl" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7"
    Transparency="70">
</telerik:RadAjaxLoadingPanel>
<asp:Panel ID="pnl" runat="server" Font-Names="Arial">
    <asp:HiddenField ID="hfID" runat="server" />
    <asp:HiddenField ID="hffoatPattern" runat="server" />
    <asp:HiddenField ID="_HasPWP" runat="server" />
    <%--<asp:HiddenField ID="_HasRequest" runat="server" />--%>
    <telerik:RadWindow ID="wPWP" runat="server" KeepInScreenBounds="True" NavigateUrl="PWP.aspx"
        ReloadOnShow="True" VisibleStatusbar="False" Animation="Fade" Font-Size="18pt"
        VisibleTitlebar="False" Width="550px" VisibleOnPageLoad="false">
    </telerik:RadWindow>
    <table runat="server" id="tbl">
        <tr>
            <td style="width: 470px" align="left">
                <asp:Label ID="lblDsc" runat="server"></asp:Label>
                <%--Font-Size='<%# Eval("Font") + "px" %>'--%>
            </td>
            <td align="left" style="width: 80px">
                <div style="width: 80px">
                    <asp:Label ID="lblRM" runat="server" Font-Size="10px"></asp:Label>
                    <asp:Label ID="lblPrice" runat="server"></asp:Label>
                </div>
            </td>
            <td style="width: 25px">
                <div align="center" style="cursor: pointer">
                    <telerik:RadBinaryImage ID="imgPic" runat="server" AutoAdjustImageControlSize="false"
                        Width="35px" Height="35px" />
                    <telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="imgPic" Animation="FlyIn"
                        Position="MiddleLeft" RelativeTo="Element" ShowEvent="OnClick">
                        <telerik:RadBinaryImage ID="imgPicTT" runat="server" Width='<%# Eval("TooltipWidth") %>'
                            Height='<%# Eval("TooltipWidth") %>' />
                    </telerik:RadToolTip>
                </div>
            </td>
            <td align="right" width="120px">
                <asp:Panel ID="pnlButtunPanel" runat="server" HorizontalAlign="right" Width="100%">
                    <table style="width: 120px">
                        <tr>
                            <td align="right">
                                <asp:LinkButton ID="btnMinuss" runat="server" Font-Bold="True" Font-Size="23px" Font-Underline="False"
                                    OnClick="btnMinus_Click" Text="[ - ]" Width="40px"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label ID="lblQty" runat="server" Font-Bold="True" Font-Size="19px" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="btnPlusss" runat="server" Font-Bold="True" Font-Size="23px" Font-Underline="False"
                                    OnClick="btnPlus_Click" Text='<%# Eval("btnPlusText") %>' Width="45px"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="pnlInnerBasket" Visible="false">
        <div style="width: 95%" align="right">
            <telerik:RadGrid ID="gRequest" runat="server" AutoGenerateColumns="False" GridLines="None"
                HorizontalAlign="Center" OnItemCommand="gRequest_ItemCommand" PageSize="100"
                Width="300px" Skin="Windows7">
                <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="ID" DataKeyNames="ID"
                    ShowHeader="False">
                    <Columns>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" ForeColor="Red" Text='<%# Eval("ReqString") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <div align="left">
                                    <asp:Label ID="lblRCurrency" runat="server" Font-Size="7px" Text='<%# Eval("Currency") %>'></asp:Label>
                                    <asp:Label ID="lblRPrice" runat="server" Font-Size="12px" Text='<%# Eval("Price") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <div style="width: 70px">
                                    <asp:LinkButton ID="btnMinusRequest" runat="server" Font-Underline="False" Text="[ - ]"
                                        Font-Bold="true" Width="30px" CommandName="MinusRequest"></asp:LinkButton>
                                    <asp:Label ID="lblReqQty" runat="server" Font-Bold="True" ForeColor="Red" Text='<%# Eval("Quantity") %>'></asp:Label>
                                    <asp:LinkButton ID="btnPlusRequest" runat="server" Font-Underline="False" Text="[ + ]"
                                        CommandName="PlusRequest" Font-Bold="true"></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="70px" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <ItemStyle BackColor="Transparent" BorderColor="Transparent" />
                    <AlternatingItemStyle BackColor="Transparent" BorderColor="Transparent" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlRequest" Visible="false">
        <%--style="font-family: Arial; padding-right: 25px" align="right" --%>
        <div style="width: 100%" align="left">
            <table>
                <tr>
                    <td align="left" valign="top">
                        <asp:Label ID="lblMsgBatch1" runat="server" Font-Bold="false" ForeColor="Blue" Text="Batch1 Messages"
                            Visible="false"></asp:Label>
                        <telerik:RadGrid ID="gBatch1" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="100" Skin="Windows7" AllowMultiRowSelection="true">
                            <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="RCode" DataKeyNames="RCode"
                                ShowHeader="False">
                                <Columns>
                                    <telerik:GridTemplateColumn>
                                        <ItemTemplate>
                                            <div align="left">
                                                <%-- <table frame="void" style="width:100%">
                                                <tr>
                                                    <td>--%>
                                                <asp:CheckBox ID="Checkbox1" runat="server" Text="" />
                                                <asp:Label ID="lblSesc" runat="server" Text='<%# Eval("RDesc") %>'></asp:Label>
                                                <asp:Label ID="lblRCurrency" runat="server" Font-Size="7px" Text='<%# Eval("Currency") %>'
                                                    Visible='<%# Eval("VisibleFee") %>'></asp:Label>
                                                <asp:Label ID="lblRPrice" runat="server" Text='<%# Eval("Price") %>' Visible='<%# Eval("VisibleFee") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <ItemStyle BackColor="Transparent" BorderColor="Transparent" />
                                <AlternatingItemStyle BackColor="Transparent" BorderColor="Transparent" />
                            </MasterTableView>
                            <ClientSettings>
                                <ClientEvents OnRowClick="RowSelecting" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td align="left" valign="top">
                        <asp:Label ID="lblMsgBatch2" runat="server" Font-Bold="false" ForeColor="Blue" Text="Batch2 Messages"
                            Visible="false"></asp:Label>
                        <telerik:RadGrid ID="gBatch2" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="100" Skin="Windows7" AllowMultiRowSelection="true">
                            <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="RCode" DataKeyNames="RCode"
                                ShowHeader="False">
                                <Columns>
                                    <telerik:GridTemplateColumn>
                                        <ItemTemplate>
                                            <div align="left">
                                                <asp:CheckBox ID="Checkbox1" runat="server" Text="" />
                                                <asp:Label ID="lblSesc" runat="server" Text='<%# Eval("RDesc") %>'></asp:Label>
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblRCurrency" runat="server" Font-Size="7px" Text='<%# Eval("Currency") %>'
                                                    Visible='<%# Eval("VisibleFee") %>'></asp:Label>
                                                <asp:Label ID="lblRPrice" runat="server" Text='<%# Eval("Price") %>' Visible='<%# Eval("VisibleFee") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <ItemStyle BackColor="Transparent" BorderColor="Transparent" />
                                <AlternatingItemStyle BackColor="Transparent" BorderColor="Transparent" />
                            </MasterTableView>
                            <ClientSettings>
                                <ClientEvents OnRowClick="RowSelecting" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td align="left" valign="top">
                        <asp:Label ID="lblMsgBatch3" runat="server" ForeColor="Blue" Text="Batch2 Messages"
                            Visible="false"></asp:Label>
                        <telerik:RadGrid ID="gBatch3" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="100" Skin="Windows7" AllowMultiRowSelection="true">
                            <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="RCode" DataKeyNames="RCode"
                                ShowHeader="False">
                                <Columns>
                                    <telerik:GridTemplateColumn>
                                        <ItemTemplate>
                                            <div align="left">
                                                <asp:CheckBox ID="Checkbox1" runat="server" Text="" />
                                                <asp:Label ID="lblSesc" runat="server" Text='<%# Eval("RDesc") %>'></asp:Label>
                                                &nbsp;&nbsp;
                                                <asp:Label ID="lblRCurrency" runat="server" Font-Size="7px" Text='<%# Eval("Currency") %>'
                                                    Visible='<%# Eval("VisibleFee") %>'></asp:Label>
                                                <asp:Label ID="lblRPrice" runat="server" Text='<%# Eval("Price") %>' Visible='<%# Eval("VisibleFee") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <ItemStyle BackColor="Transparent" BorderColor="Transparent" />
                                <AlternatingItemStyle BackColor="Transparent" BorderColor="Transparent" />
                            </MasterTableView>
                            <ClientSettings>
                                <ClientEvents OnRowClick="RowSelecting" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td align="center" valign="top" style="padding-top: 20px">
                        <%--style="padding-bottom: 23px">--%>
                        <asp:LinkButton ID="btnAdd" runat="server" Text="[Confirm]" ForeColor="Blue" Font-Underline="False"
                            Font-Names="Arial" OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Panel>
