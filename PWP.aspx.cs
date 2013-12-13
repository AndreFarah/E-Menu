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
public partial class PWP : System.Web.UI.Page
{
    DCDC dc = new DCDC();



    public static void LogOut()
    {
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Response.Redirect("Login.aspx", true);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (mSession.EmployeeCode == "")
                LogOut();

            this.Currency = ConfigFile.Currency;
            this.FloatPattern = ConfigFile.FloatPattern;

            //if (mSession.MastersIncludedChiled.Count == 0)
            //{
            //    var q = (from i in dc.Tblfoodbeverages
            //             where i.Tblfoodbeveragerequests.Count > 0
            //             select new
            //             {
            //                 i.Foodbeveragecode,
            //             }).ToList();
            //    List<string> ls = new List<string>();
            //    foreach (var s in q)
            //        ls.Add(s.Foodbeveragecode);
            //    mSession.MastersIncludedChiled = ls;
            //}

            if (!IsPostBack)
            {
                RefreshGrid(mSession.WindowParameter_PWPfbCode);
                mSession.BasketTemp = null;
                btnDone.Font.Size = mSession.AppFont;
                //lblClose.Font.Size = mSession.AppFont;
            }
        }
        catch (Exception ex)
        {
            MessageBox.ShowAlert("error : " + ex.Message);
        }
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        if (mSession.BasketTemp.Count > 0)
        {
            int countNonRequest = mSession.BasketTemp.Where(i => i.Requests.Count == 0).Sum(z => z.Quantity);
            int countRequest = mSession.BasketTemp.Where(i => i.Requests.Count > 0).Sum(order => order.Requests.Count);

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
            if (mSession.BasketTemp.Where(z => z.fbCode == fbCode).Count() > 0)
                return true;
            else
                return false;
        }
    }

    private bool VisibleQuantity(string fbCode)
    {
        if (mSession.BasketTemp.Where(z => z.fbCode == fbCode).Count() > 0)
            return true;
        else
            return false;
    }

    private void RefreshGrid(string ifbCode)
    {
        var zz = dc.Tblfoodbeveragepwpitems.Where(i => i.Foodbeveragecode == ifbCode && i.Tblfoodbeverage1.Foodbeverageavailable == "True").ToList();
        if (zz.Count > 0)
        {
            var q = (from i in zz
                     select new
                     {

                         ID = i.Foodbeveragepwpitempk,
                         fbCode = i.Foodbeveragepwpitemcode,
                         Dsc = mSession.Language == Language.Description || string.IsNullOrEmpty(i.Foodbeveragepwpitemdescription) ? i.Foodbeveragepwpitemdescription : i.Foodbeveragepwpitemalternatedescription,
                         Quantity = FindQuantity(i.Foodbeveragepwpitemcode),
                         Expanded = ExpandedRow(i.Foodbeveragepwpitemcode),
                         VisibleMinus = VisibleMinus_InMasterTable(i.Foodbeveragepwpitemcode),
                         VisibleQuantity = VisibleQuantity(i.Foodbeveragepwpitemcode),
                         Price = i.Foodbeveragepwpitemprice,
                         HasRequest = i.Tblfoodbeverage1.Tblfoodbeveragerequests.Count > 0,
                         Batch = i.Foodbeveragepurchasewithpurchasebatch,
                         //fb = i,
                     });
            var query = from i in q.AsEnumerable()
                        select new
                        {
                            i.ID,
                            i.fbCode,
                            Price = FormatPrice(i.Price),
                            i.Dsc,
                            Quantity = i.Quantity.ToString(),
                            i.Expanded,
                            Font = mSession.AppFont,
                            i.VisibleMinus,
                            i.VisibleQuantity,
                            FoatPattern = this.FloatPattern,
                            btnPlusText = "[ + ]",
                            TooltipWidth = System.Web.UI.WebControls.Unit.Parse(ConfigFile.TooltipPictureSize),
                            PicVisible = ConfigFile.PicVisible,
                            i.HasRequest,
                            i.Batch,
                        };
            gMenu1.DataSource = query.Where(i => i.Batch == 1).ToList();
            gMenu2.DataSource = query.Where(i => i.Batch == 2).ToList();
            gMenu3.DataSource = query.Where(i => i.Batch == 3).ToList();

            gMenu1.Visible = query.Where(i => i.Batch == 1).Count() > 0;
            gMenu2.Visible = query.Where(i => i.Batch == 2).Count() > 0;
            gMenu3.Visible = query.Where(i => i.Batch == 3).Count() > 0;
        }
        else
        {
            gMenu1.DataSource = gMenu2.DataSource = gMenu3.DataSource = null;
        }
        gMenu1.DataBind();
        gMenu2.DataBind();
        gMenu3.DataBind();
    }

    private bool ExpandedRow(string fbCode)
    {
        var RowOrder = mSession.BasketTemp.FirstOrDefault(z => z.fbCode == fbCode);
        if (mSession.ExpandedRows.Where(z => z == fbCode).Count() > 0
              || (RowOrder != null && RowOrder.Requests.Count > 0))
            return false;
        else
            return false;
    }

    private int FindQuantity(string ID)
    {

        var q = mSession.BasketTemp.FirstOrDefault(z => z.fbCode == ID);
        if (q != null)
        {
            return q.Quantity;
        }
        else
        {
            return 0;
        }
    }

    private string FormatPrice(double? price)
    {
        return String.Format("{0:0" + this.FloatPattern + "}", price);
    }

    MenuSessionMng mSession = new MenuSessionMng();
    List<Order> basket = new List<Order>();


    private string Currency = "";
    private string FloatPattern = "";


    eMenuTools tools = new eMenuTools();

    public void AddOrderTempToBasket(Order order)
    {
        eMenuTools tools = new eMenuTools();

        List<Order> all;
        all = mSession.Basket;

        if (mSession.Basket.Where(z => z.fbCode == order.fbCode && z.fbUnitPrice == order.fbUnitPrice).Count() > 0)
        {
            all.First(z => z.fbCode == order.fbCode).Quantity += order.Quantity;
            if (order.Requests != null && order.Requests.Count > 0)
            {
                AddRequestList_ToBasket(order.Requests, order.fbCode);
            }
        }
        else
        {
            //Tblfoodbeverage pr = dc.Tblfoodbeverages.First(z => z.Foodbeveragecode == order.fbCode);
            //Order newOrder = new Order();
            //newOrder.fbCode = order.fbCode;
            //newOrder.Description = order.Foodbeveragedescription;
            //newOrder.AltDescription = order.Foodbeveragealternatedescription;
            //newOrder.Quantity = order.Quantity;
            //newOrder.Pic = ConfigFile.PicVisible ? pr.Foodbeveragepicture : null;
            //newOrder.fbUnitPrice = tools.CalculatePrice(pr).Value;
            all.Add(order);
        }
        this.basket = all;


    }

    public void AddRequestList_ToBasket(List<Requests> reqList, string fbCode)
    {
        eMenuTools tools = new eMenuTools();
        Order order = mSession.Basket.FirstOrDefault(i => i.fbCode == fbCode);
        var fb = dc.Tblfoodbeverages.First(i => i.Foodbeveragecode == fbCode);

        //If item includes 'Request' and request didn't choose for order
        if (reqList.Count == 0
            && (dc.Tblfoodbeveragerequestrules.Where(z => z.Foodbeveragecode == fbCode).Count() == 0
            || dc.Tblfoodbeveragerequestrules.Where(z => z.Foodbeveragecode == fbCode && z.Foodbeveragerequestrulescondition == "At Least").Count() > 0))
        {

            Requests requests = new Requests();
            requests.ID = order.Requests.Count > 0 ? order.Requests.Max(z => z.ID) + 1 : 0;

            RequestUnit newR = new RequestUnit();
            newR.fbCode = order.fbCode;
            newR.RCode = -1;
            newR.RDescription = "Normal ";
            newR.Price = 0;
            requests.RequestUnits.Add(newR);


            var q = order.Requests.FirstOrDefault(i => i == requests);

            if (q == null)
            {
                order.Requests.Add(requests);

            }
            else
            {
                order.Requests.FirstOrDefault(i => i == requests).Quantity++;

            }
        }
        if (reqList.Count > 0)
        {

            foreach (var request in reqList)
            {
                var q = order.Requests.FirstOrDefault(i => i == request);

                if (q == null)
                {
                    order.Requests.Add(request);
                }
                else
                {
                    order.Requests.FirstOrDefault(i => i == request).Quantity++;
                }

            }
        }

        //if (mSession.Basket.FirstOrDefault(i => i.fbCode == fbCode) != null)
        //{
        //    mSession.Basket.Add(order);
        //}
        //else
        //{
        mSession.Basket.Remove(mSession.Basket.First(i => i.fbCode == fbCode));
        mSession.Basket.Add(order);
        //}
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        bool result1 = CheckPWPRules(mSession.WindowParameter_PWPfbCode, "Batch1");
        bool result2 = CheckPWPRules(mSession.WindowParameter_PWPfbCode, "Batch2");
        bool result3 = CheckPWPRules(mSession.WindowParameter_PWPfbCode, "Batch3");

        if (result1 && result2 && result3)
        {
            foreach (var tempOrder in mSession.BasketTemp)
            {
                AddOrderTempToBasket(tempOrder);
            }
            mSession.BasketTemp = null;

            if (!mSession.ViewingBasket)
                btnDone.Text = "<script>Close()</" + "script>";
            else
                //Response.Redirect("default.aspx");
                btnDone.Text = "<script>CloseAndRefresh()</" + "script>";
        }

    }

    private bool CheckPWPRules(string fbCode, string batch)
    {
        var rules = dc.Tblfoodbeveragepurchasewithpurchaserules.Where(z => z.Foodbeveragecode == fbCode);
        var ruleBatch1 = rules.FirstOrDefault(i => i.Foodbeveragepurchasewithpurchasebatch == 1);
        var ruleBatch2 = rules.FirstOrDefault(i => i.Foodbeveragepurchasewithpurchasebatch == 2);
        var ruleBatch3 = rules.FirstOrDefault(i => i.Foodbeveragepurchasewithpurchasebatch == 3);


        if (batch == "Batch1")
        {
            if (ruleBatch1 != null)
            {
                //                lblPWPmsg1.Text = "Choose " + (ruleBatch1.Foodbeveragepurchasewithpurchaserulescondition == "Exact" ? "" : ruleBatch1.Foodbeveragepurchasewithpurchaserulescondition) + " " + ruleBatch1.Foodbeveragepurchasewithpurchaserulesnumber + " Item";
                lblPWPmsg2.Text = "";
                lblPWPmsg3.Text = "";
                if (CheckRule(ruleBatch1, mSession.BasketTemp))
                {
                    lblPWPmsg1.Text = "";
                    lblPWPmsg1.Visible = false;
                    return true;
                }
                else
                {
                    lblPWPmsg1.Visible = true;
                    lblPWPmsg1.Text = "Choose " + (ruleBatch1.Foodbeveragepurchasewithpurchaserulescondition == "Exact" ? "" : ruleBatch1.Foodbeveragepurchasewithpurchaserulescondition) + " " + ruleBatch1.Foodbeveragepurchasewithpurchaserulesnumber + " Item";
                    return false;
                }
            }
            else
            {
                lblPWPmsg1.Text = "";
                lblPWPmsg1.Visible = false;
                return true;
            }
        }


        if (batch == "Batch2")
        {
            if (ruleBatch2 != null)
            {
                //lblPWPmsg2.Text = "Choose " + (ruleBatch2.Foodbeveragepurchasewithpurchaserulescondition == "Exact" ? "" : ruleBatch2.Foodbeveragepurchasewithpurchaserulescondition) + " " + ruleBatch2.Foodbeveragepurchasewithpurchaserulesnumber + " Item";
                lblPWPmsg3.Text = "";
                if (CheckRule(ruleBatch2, mSession.BasketTemp))
                {
                    lblPWPmsg2.Text = "";
                    lblPWPmsg2.Visible = false;
                    return true;
                }
                else
                {
                    lblPWPmsg2.Text = "Choose " + (ruleBatch2.Foodbeveragepurchasewithpurchaserulescondition == "Exact" ? "" : ruleBatch2.Foodbeveragepurchasewithpurchaserulescondition) + " " + ruleBatch2.Foodbeveragepurchasewithpurchaserulesnumber + " Item";
                    return false;
                }
            }
            else
            {
                lblPWPmsg2.Text = "";
                lblPWPmsg2.Visible = false;
                return true;
            }
        }

        if (batch == "Batch3")
        {
            if (ruleBatch3 != null)
            {
                //                lblPWPmsg3.Text = "Choose " + (ruleBatch3.Foodbeveragepurchasewithpurchaserulescondition == "Exact" ? "" : ruleBatch3.Foodbeveragepurchasewithpurchaserulescondition) + " " + ruleBatch3.Foodbeveragepurchasewithpurchaserulesnumber + " Item";
                if (CheckRule(ruleBatch3, mSession.BasketTemp))
                {
                    lblPWPmsg3.Text = "";
                    lblPWPmsg3.Visible = false;
                    return true;
                }
                else
                {
                    lblPWPmsg3.Visible = true;
                    lblPWPmsg3.Text = "Choose " + (ruleBatch3.Foodbeveragepurchasewithpurchaserulescondition == "Exact" ? "" : ruleBatch3.Foodbeveragepurchasewithpurchaserulescondition) + " " + ruleBatch3.Foodbeveragepurchasewithpurchaserulesnumber + " Item";
                    return false;
                }
            }
            else
            {
                lblPWPmsg3.Text = "";
                lblPWPmsg3.Visible = false;
                return true;
            }
        }
        return true;
    }


    private bool CheckRule(Tblfoodbeveragepurchasewithpurchaserule rule, List<Order> AlTempBasket)
    {
        string currentFbCode = mSession.WindowParameter_PWPfbCode;
        List<Tblfoodbeveragepwpitem> selectable1 = dc.Tblfoodbeveragepwpitems.Where(i => i.Foodbeveragecode == currentFbCode && i.Foodbeveragepurchasewithpurchasebatch == rule.Foodbeveragepurchasewithpurchasebatch).ToList();
        var ruleItemInBasket = from s in selectable1
                               join l in AlTempBasket on s.Foodbeveragepwpitemcode equals l.fbCode
                               //where s.Foodbeveragepurchasewithpurchasebatch == rule.Foodbeveragepurchasewithpurchasebatch
                               select new
                               {
                                   l.fbCode,
                                   l.Description,
                                   l.Quantity,
                                   s.Foodbeveragepwpitemprice,
                               };

        int ruleItemInBasket_Count = ruleItemInBasket.Count();
        if (rule.Foodbeveragepurchasewithpurchaserulescondition == "At Least")
        {
            if (ruleItemInBasket_Count >= rule.Foodbeveragepurchasewithpurchaserulesnumber)
                return true;
            else return false;
        }
        if (rule.Foodbeveragepurchasewithpurchaserulescondition == "Exact")
        {
            if (ruleItemInBasket_Count == rule.Foodbeveragepurchasewithpurchaserulesnumber)
                return true;
            else return false;
        }

        if (rule.Foodbeveragepurchasewithpurchaserulescondition == "At Most")
        {
            if (ruleItemInBasket_Count <= rule.Foodbeveragepurchasewithpurchaserulesnumber && ruleItemInBasket.Count() > 0)
                return true;
            else return false;
        }
        return false;
    }

}

