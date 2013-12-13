using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

using System.Collections.Generic;
using System.Linq;
using RestaurantpoContext;
using System.Text;

using RadMessageBox;

using System.Security.Permissions;
public partial class Default : System.Web.UI.Page
{
    DCDC dc = new DCDC();



    public static void LogOut()
    {
        //FormsAuthentication.SignOut();
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Session.RemoveAll();
        HttpContext.Current.Response.Redirect("Login.aspx", false);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (mSession.EmployeeCode == "")
            {
                if (Request.Cookies["EmployeeCode"] != null && Request.Cookies["CounterCode"] != null)
                {
                    mSession.EmployeeCode = Server.HtmlEncode(Request.Cookies["EmployeeCode"].Value);
                    mSession.CounterCode = Server.HtmlEncode(Request.Cookies["CounterCode"].Value);
                }
                else
                {
                    LogOut();
                }
            }
            //"HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");

            wPrintReciept.VisibleOnPageLoad = false;
            //wPWP.VisibleOnPageLoad = false;
            //Response.Charset = "gb2312";
            //Session.CodePage = 936;
            this.Currency = ConfigFile.Currency;
            this.FloatPattern = ConfigFile.FloatPattern;

            if (mSession.MastersIncludedChiled.Count == 0)
            {
                var q = (from i in dc.Tblfoodbeverages
                         where i.Tblfoodbeveragerequests.Count > 0
                         select new
                         {
                             i.Foodbeveragecode,
                         }).ToList();
                List<string> ls = new List<string>();
                foreach (var s in q)
                    ls.Add(s.Foodbeveragecode);
                mSession.MastersIncludedChiled = ls;
            }

            if (!IsPostBack)
            {
                LoadPabelbar();
                ShowBasket(mSession.ViewingBasket);


                //((RadToolBarButton)tbHeader.FindButtonByCommandName("PrintReciept")).Visible = ConfigFile.VisiblePrintReceipt == "True" ? true : false;
                //((RadToolBarButton)tbHeader.FindButtonByCommandName("RecallOrder")).Visible = ConfigFile.VisibleRecallOrder == "True" ? true : false;
                //((RadToolBarButton)tbHeader.FindButtonByCommandName("Payment")).Visible = ConfigFile.VisiblePayment == "True" ? true : false;
                //((RadToolBarDropDown)tbHeader.FindItemByText("Language")).Visible = ConfigFile.VisibleLanguageSelection == "True" ? true : false;
            }
            ResizeControlsFont();
        }
        catch (Exception ex)
        {
            MessageBox.ShowAlert("error : " + ex.Message);
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        if (mSession.Basket.Count > 0)
        {
            int countNonRequest = mSession.Basket.Where(i => i.Requests.Count == 0).Sum(z => z.Quantity);
            int countRequest = mSession.Basket.Where(i => i.Requests.Count > 0).Sum(order => order.Requests.Count);
            //lblTotalPrice.Text = FormatPrice(countNonRequest + countRequest);
            //(countNonRequest + countRequest).ToString()
            //btnBasket.Text = "View Order [" + mSession.Basket.Sum(z => z.Quantity) + "]";
            RefreshTotalPrice();
        }
        else
        {

        }
        if (mSession.RefreshGrid)
        {

            mSession.RefreshGrid = false;
            Response.Redirect("default.aspx");
            //RadAjaxManager1.RaisePostBackEvent("server");
            //btnRefreshGrid_Click(null, null);
        }

    }


    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        //gMenu.Rebind();
        //RadAjaxManager1.RaisePostBackEvent("server");
    }

    private void LoadPabelbar()
    {
        panelbar.Items.Clear();
        var types = dc.Tblfoodbeveragemenutypes.ToList();

        foreach (var type in types)
        {
            if (type.Tblfoodbeveragegroups.Where(i => i.Groupavailable == "True").Count() > 0)
            {
                RadPanelItem itemMain = new RadPanelItem();
                itemMain.Text = (type.Groupmenutype);
                itemMain.Font.Name = "Arial";
                itemMain.ForeColor = System.Drawing.Color.Blue;
                itemMain.Font.Size = 16;
                itemMain.Font.Size = mSession.AppFont;
                itemMain.PostBack = false;
                itemMain.Expanded = true;
                panelbar.Items.Add(itemMain);

                foreach (var group in type.Tblfoodbeveragegroups.Where(i => i.Groupavailable == "True"))
                {

                    RadPanelItem subItem = new RadPanelItem((group.Groupdescription));
                    subItem.Value = group.Groupcode;
                    subItem.Font.Name = "Arial";
                    subItem.Font.Size = mSession.AppFont;
                    subItem.Height = 42;
                    itemMain.Items.Add(subItem);
                }
            }
        }
        if (panelbar.Items.Count > 0)
        {
            panelbar.Items[0].Expanded = true;
            panelbar.Items[0].Items[0].Selected = true;

            if (mSession.MenuType == "" || mSession.GroupName == "")
            {
                mSession.MenuType = panelbar.Items[0].Text;
                mSession.GroupName = panelbar.Items[0].Items[0].Value;
            }
            gMenu.Rebind();

        }
    }

    private bool VisibleMinus_InMasterTable(string fbCode)
    {
        if (mSession.MastersIncludedChiled.Where(z => z == fbCode).Count() > 0)
        {
            return false;
        }
        else
        {
            if (mSession.Basket.Where(z => z.fbCode == fbCode && !z.IsPWP).Count() > 0)
                return true;
            else
                return false;
        }
    }

    private bool VisibleQuantity(string fbCode)
    {
        if (mSession.Basket.Where(z => z.fbCode == fbCode && !z.IsPWP).Count() > 0)
            return true;
        else
            return false;
    }


    //private void RefreshGrid()
    //{
    //    if (mSession.ViewingBasket)
    //    {
    //        var query = from i in mSession.Basket.AsEnumerable()
    //                    //where i.Groupcode == panelbar.SelectedItem.Value
    //                    select new
    //                    {
    //                        ID = i.fbCode,
    //                        Price = FormatPrice(i.fbUnitPrice),
    //                        Pic = ConfigFile.PicVisible ? i.Pic : null,
    //                        Dsc = mSession.Language == Language.Description || string.IsNullOrEmpty(i.AltDescription) ? i.Description : i.AltDescription,
    //                        Quantity = i.Quantity,// FindQuantity(i.fbCode),
    //                        VisibleMinus = VisibleMinus_InMasterTable(i.fbCode),
    //                        Expanded = this.ExpandedRow(i.fbCode),
    //                        //VisibleQuantity = VisibleQuantity(i.fbCode),
    //                        FoatPattern = this.FloatPattern,
    //                        Font = mSession.AppFont,
    //                        btnPlusText = "[ + ]",// mSession.MastersIncludedChiled.Where(z => z == i.fbCode).Count() > 0 ? "[ < ]" : "[ + ]",
    //                        TooltipWidth = System.Web.UI.WebControls.Unit.Parse(ConfigFile.TooltipPictureSize),
    //                        PicVisible = ConfigFile.PicVisible,
    //                        i.IsPWP,
    //                        HasPWP = i.HasPWP,
    //                    };
    //        //if(ConfigFile.)
    //        gMenu.DataSource = query.ToList().Distinct();
    //    }
    //    else
    //    {
    //        if (mSession.GroupName != "")
    //        {
    //            var menuItem = panelbar.Items.FindItemByText(mSession.MenuType);
    //            if (menuItem != null)
    //            {
    //                var groupItem = menuItem.Items.FindItemByValue(mSession.GroupName);
    //                if (groupItem != null)
    //                    groupItem.Selected = true;
    //            }
    //        }
    //        if (panelbar.SelectedItem != null && panelbar.SelectedItem.Text != "")
    //        {

    //            var q = (from i in dc.Tblfoodbeverages
    //                     where i.Groupcode == panelbar.SelectedItem.Value
    //                     && i.Foodbeverageavailable == "True"
    //                     select new
    //                     {
    //                         ID = i.Foodbeveragecode,

    //                         Pic = i.Foodbeveragepicture,
    //                         Dsc = mSession.Language == Language.Description || string.IsNullOrEmpty(i.Foodbeveragealternatedescription) ? i.Foodbeveragedescription : i.Foodbeveragealternatedescription,
    //                         Quantity = FindQuantity(i.Foodbeveragecode),

    //                         Expanded = ExpandedRow(i.Foodbeveragecode),
    //                         VisibleMinus = VisibleMinus_InMasterTable(i.Foodbeveragecode),
    //                         // VisibleQuantity = FindQuantity(i.Foodbeveragecode) > 0 ? true : false, //VisibleQuantity(i.Foodbeveragecode),
    //                         HasPWP = i.Tblfoodbeveragepwpitems.Count > 0 ? true : false,
    //                         fb = i,
    //                     });
    //            var query = from i in q.AsEnumerable()
    //                        select new
    //                        {
    //                            i.ID,
    //                            Price = FormatPrice(tools.CalculatePrice(i.fb)),
    //                            Pic = ConfigFile.PicVisible ? i.Pic : null,
    //                            i.Dsc,
    //                            Quantity = i.Quantity,
    //                            i.Expanded,
    //                            Font = mSession.AppFont,
    //                            i.VisibleMinus,
    //                            //VisibleQuantity = (i.Quantity > 0 ? true : false),
    //                            FoatPattern = this.FloatPattern,
    //                            btnPlusText = "[ + ]",// mSession.MastersIncludedChiled.Where(z => z == i.ID).Count() > 0 ? "[ < ]" : "[ + ]",
    //                            TooltipWidth = System.Web.UI.WebControls.Unit.Parse(ConfigFile.TooltipPictureSize),
    //                            PicVisible = ConfigFile.PicVisible,
    //                            i.HasPWP,
    //                            IsPWP = false,
    //                        };
    //            gMenu.DataSource = query.ToList();
    //        }

    //    }
    //    //gMenu.Rebind();
    //    gMenu.DataBind();
    //    //mSession.ExpandedRows
    //}


    //eMenuTools tools = new eMenuTools();
    private bool ExpandedRow(string fbCode)
    {
        var RowOrder = mSession.Basket.FirstOrDefault(z => z.fbCode == fbCode);
        if (mSession.ExpandedRows.Where(z => z == fbCode).Count() > 0
              || (RowOrder != null && RowOrder.Requests.Count > 0))
            return false;
        else
            return false;
    }


    private int FindQuantity(string ID)
    {

        var q = mSession.Basket.FirstOrDefault(z => z.fbCode == ID && !z.IsPWP);
        if (q != null)
        {
            return q.Quantity;
        }
        else
        {
            return 0;
        }
    }

    //private string FormatPrice(Tblfoodbeverage fb)
    //{
    //    double? price = tools.CalculatePrice(fb);
    //    return String.Format("{0:0" + this.FloatPattern + "}", price);
    //    //return String.Format("{0:0.00}", price);
    //}


    private string FormatPrice(double? price)
    {
        return String.Format("{0:0" + this.FloatPattern + "}", price);
        //return String.Format("{0:0.00}", price);
    }

    protected void panelbar_ItemClick1(object sender, RadPanelBarEventArgs e)
    {
        if (e.Item.Value != "")
        {
            mSession.GroupName = e.Item.Value;
            mSession.MenuType = ((RadPanelItem)e.Item.Parent).Text;
            ShowBasket(false);
            gMenu.Rebind();
            mSession.ViewingBasket = false;
            //gMenu.Rebind();
        }
    }

    MenuSessionMng mSession = new MenuSessionMng();

    protected void btnBasket_Click(object sender, EventArgs e)
    {
        ShowBasket(true);
        gMenu.Rebind();
        //gMenu.Rebind();

    }

    private void ShowBasket(bool value)
    {
        mSession.ViewingBasket = value;
        pnlbasket.Visible = value;
        if (value)
        {
            RefreshTotalPrice();
        }
    }
    private string Currency = "";
    private string FloatPattern = "";

    private void RefreshTotalPrice()
    {
        if (mSession.ViewingBasket)
        {
            //this.Currency = this.Currency
            lblCurrency.Text = Currency;
            double sumNonRequest = mSession.Basket.Where(i => i.Requests.Count == 0).Sum(z => z.fbUnitPrice * z.Quantity);
            double sumRequest = mSession.Basket.Where(i => i.Requests.Count > 0).Sum(order => order.Requests.Sum(req => (req.RequestUnits.Sum(unit => unit.Price) + order.fbUnitPrice) * req.Quantity)); //i.Quantity * (z.fbUnitPrice + i.Price)));
            lblTotalPrice.Text = FormatPrice(sumNonRequest + sumRequest);

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mSession.Basket = null;
        mSession.ExpandedRows = null;

        //btnBasket.Text = "View Order";
        pnlbasket.Visible = false;
        gMenu.Rebind();
        //gMenu.Rebind();
    }

    protected void toolbar_ButtonClick(object sender, RadToolBarEventArgs e)
    {
        if (e.Item.Value == "A1")
        {
            mSession.AppFont = int.Parse(ConfigFile.Font1);
            RefreshFonts(mSession.AppFont);
        }
        else if (e.Item.Value == "A2")
        {
            mSession.AppFont = int.Parse(ConfigFile.Font2);
            RefreshFonts(mSession.AppFont);
        }
        else if (e.Item.Value == "A3")
        {
            mSession.AppFont = int.Parse(ConfigFile.Font3);
            RefreshFonts(mSession.AppFont);
        }
    }

    private void RefreshFonts(FontUnit fontUnit)
    {
        LoadPabelbar();
        //pnlbasket.Font.Size = fontUnit;
        gMenu.Rebind();
        ResizeControlsFont();
    }

    private void ResizeControlsFont()
    {
        //btnBasket.Font.Size = mSession.AppFont;
        //btnHowToUse.Font.Size = mSession.AppFont;
        tbHeader.Font.Size = mSession.AppFont;
        btnCancel.Font.Size = mSession.AppFont;
        btnConfirm.Font.Size = mSession.AppFont;
        lblTotalPrice.Font.Size = mSession.AppFont;
        Label1.Font.Size = mSession.AppFont;

        panelbar.Font.Size = mSession.AppFont;
        ((RadToolBarButton)tbHeader.FindButtonByCommandName("HowToUse")).Font.Size = mSession.AppFont - 2;



        ((RadToolBarButton)tbHeader.FindButtonByCommandName("ViewOrder")).Font.Size = mSession.AppFont - 2;


        var btn = ((RadToolBarButton)tbHeader.FindButtonByCommandName("PrintReciept"));

        if (ConfigFile.VisiblePrintReceipt == "True")
        {
            btn.Enabled = true;
            btn.Text = "Print Receipt";

            btn.Font.Size = mSession.AppFont - 2;
        }
        else
        {
            btn.Enabled = false;
            btn.Text = "";
        }

        btn = ((RadToolBarButton)tbHeader.FindButtonByCommandName("RecallOrder"));

        if (ConfigFile.VisibleRecallOrder == "True")
        {
            btn.Enabled = true;
            btn.Text = "Recall Order";
            btn.Font.Size = mSession.AppFont - 2;
        }
        else
        {
            btn.Enabled = false;
            btn.Text = "";
        }

        btn = ((RadToolBarButton)tbHeader.FindButtonByCommandName("Payment"));

        if (ConfigFile.VisiblePayment == "True")
        {
            btn.Enabled = true;
            btn.Text = "Payment";
            btn.Font.Size = mSession.AppFont - 2;
        }
        else
        {
            btn.Enabled = false;
            btn.Text = "";
        }

        var dd = ((RadToolBarDropDown)tbHeader.FindItemByText("Language"));
        if (dd != null)
            if (ConfigFile.VisibleLanguageSelection == "True")
            {
                dd.Enabled = true;
                dd.Text = "Language";

                dd.Font.Size = mSession.AppFont - 2;
                ((RadToolBarButton)tbHeader.FindButtonByCommandName("Main")).Font.Size = mSession.AppFont - 2;
                ((RadToolBarButton)tbHeader.FindButtonByCommandName("Alternate")).Font.Size = mSession.AppFont - 2;

            }
            else
            {
                dd.Enabled = false;
                dd.Text = "";
            }

        btn = ((RadToolBarButton)tbHeader.FindButtonByCommandName("LogOut"));
        btn.Font.Size = mSession.AppFont - 2;
    }

    protected void btnHowToUse_Click(object sender, EventArgs e)
    {
        Response.Redirect("Edu.aspx");
    }

    protected void btnHowToUse_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Edu.aspx");
    }

    protected void gMenu_ItemCommand(object source, GridCommandEventArgs e)
    {

    }

    protected void tbHeader_ButtonClick(object sender, RadToolBarEventArgs e)
    {
        if (e.Item.Value == "PrintReciept")
        {
            mSession.WindowParameter_PrintReceiptOrRecallOrder = PrintReceiptOrRecallOrder.PrintReceipt;
            wPrintReciept.VisibleOnPageLoad = true;
        }
        else if (e.Item.Value == "Main")
        {
            mSession.Language = Language.Description;
            gMenu.Rebind();
        }
        else if (e.Item.Value == "Alternate")
        {
            mSession.Language = Language.Alternate;
            gMenu.Rebind();
        }
        else if (e.Item.Value == "ViewOrder")
        {
            mSession.ViewingBasket = true;
            ShowBasket(true);
            hfMenuSelected.Value = "";
            gMenu.Rebind();
        }
        else if (e.Item.Value == "RecallOrder")
        {
            if (mSession.Basket.Count > 0)
                MessageBox.ShowConfirmation("Discard current order?", "RecallOrder", true, false);
            else
            {
                mSession.WindowParameter_PrintReceiptOrRecallOrder = PrintReceiptOrRecallOrder.RecallOrder;
                wPrintReciept.VisibleOnPageLoad = true;
            }
        }
        else if (e.Item.Value == "LogOut")
        {
            Session.RemoveAll();
            Session.Abandon();


            if (Request.Cookies["EmployeeCode"] != null)
            {
                HttpCookie myCookie = new HttpCookie("EmployeeCode");
                myCookie.Expires = DateTime.Now.AddDays(-11);
                Response.Cookies.Add(myCookie);
            }

            if (Request.Cookies["CounterCode"] != null)
            {
                HttpCookie myCookie = new HttpCookie("CounterCode");
                myCookie.Expires = DateTime.Now.AddDays(-11);
                Response.Cookies.Add(myCookie);
            }

            Response.Redirect("login.aspx");

        }
        else if (e.Item.Value == "Payment")
        {
            //if (!string.IsNullOrEmpty(mSession.TableName))
            //{
            mSession.WindowParameter_PrintReceiptOrRecallOrder = PrintReceiptOrRecallOrder.Payment;
            wPrintReciept.VisibleOnPageLoad = true;
            //}
            //else
            //{
            //    tools.PaymentExpress(mSession.TableName);
            //}


        }
    }
    eMenuTools tools = new eMenuTools();

    protected void MessageBox_YesSelected(object sender, string Key)
    {
        if (Key == "RecallOrder")
        {
            // RadAjaxManager1.ResponseScripts.Add(@"Sys.Application.add_load(openWindow)");
            gMenu.Rebind();
            mSession.WindowParameter_PrintReceiptOrRecallOrder = PrintReceiptOrRecallOrder.RecallOrder;
            wPrintReciept.VisibleOnPageLoad = true;
        }
    }

    //public void LoadPWPwindw(string fbCode)
    //{
    //    mSession.WindowParameter_PWPfbCode = fbCode;
    //    wPWP.VisibleOnPageLoad = true;
    //}

    //private void CheckPWPItems(string iFBCode)
    //{
    //    bool iGot = false;

    //    foreach (Data.DataRow dri in ds.tblPWPItems.Select("FBCode = '" + iFBCode + "' "))
    //    {
    //        iGot = true;
    //        break; // TODO: might not be correct. Was : Exit For
    //    }

    //    if (iGot == true)
    //    {
    //        if (ReadMenuView() != "List")
    //        {
    //            dlgPWP pwp = new dlgPWP();
    //            pwp.AlternateDescription = AlternateDescription;
    //            pwp.FBCode = iFBCode;
    //            pwp.ds = ds;
    //            pwp.ShowDialog();

    //            foreach (string st in pwp.iSelectedItems)
    //            {
    //                //st.Split("|")(5))
    //                if (AddSalesItem(st.Split("|")(0), GetSalesItemReceiptDescription(st.Split("|")(1), st.Split("|")(2)), GetSalesItemKitchenOrderDescription(st.Split("|")(1), st.Split("|")(2)), Conversion.Val(st.Split("|")(3).ToString), 0, "False") == true)
    //                {
    //                    RefreshSalesItems();

    //                    if (ReadAutoPopUpFBRequest() == "True" & (ReadRequestView() == "Multiple Column" | ReadRequestView() == "Multiple Column Lite"))
    //                    {
    //                        lvItems.Items(lvItems.Items.Count - 1).Selected = true;
    //                        mniItemRequest_Click(System.DBNull.Value, System.EventArgs.Empty);
    //                    }
    //                    if (ReadAutoPopUpGeneralRequest() == "True" & (ReadRequestView() == "Multiple Column" | ReadRequestView() == "Multiple Column Lite"))
    //                    {
    //                        lvItems.Items(lvItems.Items.Count - 1).Selected = true;
    //                        mniGeneralRequest_Click(System.DBNull.Value, System.EventArgs.Empty);
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            dlgPWPLite pwp = new dlgPWPLite();
    //            pwp.AlternateDescription = AlternateDescription;
    //            pwp.FBCode = iFBCode;
    //            pwp.ds = ds;
    //            pwp.ShowDialog();

    //            foreach (string st in pwp.iSelectedItems)
    //            {
    //                //st.Split("|")(5))
    //                if (AddSalesItem(st.Split("|")(0), GetSalesItemReceiptDescription(st.Split("|")(1), st.Split("|")(2)), GetSalesItemKitchenOrderDescription(st.Split("|")(1), st.Split("|")(2)), Conversion.Val(st.Split("|")(3).ToString), 0, "False") == true)
    //                {
    //                    RefreshSalesItems();

    //                    if (ReadAutoPopUpFBRequest() == "True" & (ReadRequestView() == "Multiple Column" | ReadRequestView() == "Multiple Column Lite"))
    //                    {
    //                        lvItems.Items(lvItems.Items.Count - 1).Selected = true;
    //                        mniItemRequest_Click(System.DBNull.Value, System.EventArgs.Empty);
    //                    }
    //                    if (ReadAutoPopUpGeneralRequest() == "True" & (ReadRequestView() == "Multiple Column" | ReadRequestView() == "Multiple Column Lite"))
    //                    {
    //                        lvItems.Items(lvItems.Items.Count - 1).Selected = true;
    //                        mniGeneralRequest_Click(System.DBNull.Value, System.EventArgs.Empty);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    protected void btnTest_Click(object sender, EventArgs e)
    {
        // LoadPWPwindw("F0718");

    }
    protected void btnRefreshGrid_Click(object sender, EventArgs e)
    {
        ShowBasket(true);
        hfMenuSelected.Value = "";
        gMenu.Rebind();
    }

    protected void gMenu_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        if (mSession.ViewingBasket)
        {
            byte[] b = null;
            var query = from i in mSession.Basket.AsEnumerable()
                        //where i.Groupcode == panelbar.SelectedItem.Value
                        select new
                        {
                            ID = i.fbCode,
                            Price = FormatPrice(i.fbUnitPrice),
                            Pic = b, //ConfigFile.PicVisible ? i.Pic : null,
                            Dsc = mSession.Language == Language.Description || string.IsNullOrEmpty(i.AltDescription) ? i.Description : i.AltDescription,
                            Quantity = i.Quantity,// FindQuantity(i.fbCode),
                            VisibleMinus = VisibleMinus_InMasterTable(i.fbCode),
                            Expanded = this.ExpandedRow(i.fbCode),
                            //VisibleQuantity = VisibleQuantity(i.fbCode),
                            FoatPattern = this.FloatPattern,
                            Font = mSession.AppFont,
                            btnPlusText = "[ + ]",// mSession.MastersIncludedChiled.Where(z => z == i.fbCode).Count() > 0 ? "[ < ]" : "[ + ]",
                            TooltipWidth = System.Web.UI.WebControls.Unit.Parse(ConfigFile.TooltipPictureSize),
                            PicVisible = false,
                            i.IsPWP,
                            HasPWP = i.HasPWP,
                        };
            //if(ConfigFile.)
            gMenu.DataSource = query.ToList().Distinct();
        }
        else
        {
            if (mSession.GroupName != "")
            {
                var menuItem = panelbar.Items.FindItemByText(mSession.MenuType);
                if (menuItem != null)
                {
                    var groupItem = menuItem.Items.FindItemByValue(mSession.GroupName);
                    if (groupItem != null)
                        groupItem.Selected = true;
                }
            }
            if (panelbar.SelectedItem != null && panelbar.SelectedItem.Text != "")
            {

                var q = (from i in dc.Tblfoodbeverages
                         where i.Groupcode == panelbar.SelectedItem.Value
                         && i.Foodbeverageavailable == "True"
                         select new
                         {
                             ID = i.Foodbeveragecode,

                             Pic = i.Foodbeveragepicture,
                             Dsc = mSession.Language == Language.Description || string.IsNullOrEmpty(i.Foodbeveragealternatedescription) ? i.Foodbeveragedescription : i.Foodbeveragealternatedescription,
                             Quantity = FindQuantity(i.Foodbeveragecode),

                             Expanded = ExpandedRow(i.Foodbeveragecode),
                             VisibleMinus = VisibleMinus_InMasterTable(i.Foodbeveragecode),
                             // VisibleQuantity = FindQuantity(i.Foodbeveragecode) > 0 ? true : false, //VisibleQuantity(i.Foodbeveragecode),
                             HasPWP = i.Tblfoodbeveragepwpitems.Count > 0 ? true : false,
                             fb = i,
                         });
                var query = from i in q.AsEnumerable()
                            select new
                            {
                                i.ID,
                                Price = FormatPrice(tools.CalculatePrice(i.fb)),
                                Pic = ConfigFile.PicVisible ? i.Pic : null,
                                i.Dsc,
                                Quantity = i.Quantity,
                                i.Expanded,
                                Font = mSession.AppFont,
                                i.VisibleMinus,
                                //VisibleQuantity = (i.Quantity > 0 ? true : false),
                                FoatPattern = this.FloatPattern,
                                btnPlusText = "[ + ]",// mSession.MastersIncludedChiled.Where(z => z == i.ID).Count() > 0 ? "[ < ]" : "[ + ]",
                                TooltipWidth = System.Web.UI.WebControls.Unit.Parse(ConfigFile.TooltipPictureSize),
                                PicVisible = ConfigFile.PicVisible,
                                i.HasPWP,
                                IsPWP = false,
                            };
                gMenu.DataSource = query.ToList();
            }

        }
        //gMenu.Rebind();
        //gMenu.DataBind();
    }
}

