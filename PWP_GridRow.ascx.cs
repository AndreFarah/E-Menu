using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantpoContext;
using Telerik.Web.UI;
using localhost;
public partial class PWP_GridRow : System.Web.UI.UserControl
{
    MenuSessionMng mSession = new MenuSessionMng();
    //    private string Currency = "";
    #region Properties


    public string FoatPattern
    {
        set
        {
            hffoatPattern.Value = value;
        }
        get
        {

            return hffoatPattern.Value;
        }
    }


    public string Description
    {
        get { return lblDsc.Text; }
        set { lblDsc.Text = value; }
    }
    public int Font_Size
    {
        set
        {
            lblDsc.Font.Size = value;
            lblPrice.Font.Size = value;
            //gBatch1.Font.Size = value;
            //gBatch2.Font.Size = value;
            //gBatch3.Font.Size = value;
            ResizeFonts();
        }
        //get { return lblDsc.Font.Size; }
    }

    public Order RowOrder
    {
        set
        {
            if (value != null)
            {
                var all = mSession.BasketTemp;
                all.Add(value);
                mSession.BasketTemp = all;
            }
            else
            {
                var o = mSession.BasketTemp.FirstOrDefault(z => z.fbCode == this.FbCode);
                if (o != null)
                    mSession.BasketTemp.Remove(o);
            }
        }
        get
        {
            return mSession.BasketTemp.FirstOrDefault(z => z.fbCode == this.FbCode);
        }
    }

    DCDC dc = new DCDC();

    //public string Currency
    //{
    //    set
    //    {
    //        lblRM.Text = value;
    //    }
    //    get
    //    {
    //        return lblRM.Text;
    //    }
    //}

    public string Price
    {
        set
        {
            lblPrice.Text = value;

        }
    }

    public bool ExpandedRow
    {
        set
        {
            pnlRequest.Visible = value;

            RefreshGridRequest();
        }
        get
        {
            return pnlRequest.Visible;
        }
        //get
        //{
        //    if (mSession.ExpandedRows.Where(z => z == this.FbCode).Count() > 0
        //        || (this.RowOrder != null && this.RowOrder.Requests.Count > 0))
        //        return true;
        //    else
        //        return false;
        //    //return hfExpanded.Value != "" ? Boolean.Parse(hfExpanded.Value) : false;
        //}
    }

    public bool VisibleMinus
    {
        set
        {
            btnMinuss.Visible = value;
        }
    }
    public bool VisibleQuantity
    {
        set
        {
            lblQty.Visible = value;
        }
        get { return lblQty.Visible; }
    }


    public string Quantity
    {
        set { lblQty.Text = value; }
        get { return lblQty.Text; }
    }

    public string FbCode { set { hfID.Value = value; } get { return hfID.Value; } }

    eMenuTools tools = new eMenuTools();

    private void IncDecQty(IncDec op)
    {
        var req = dc.Tblfoodbeveragerequests.FirstOrDefault(z => z.Foodbeveragecode == FbCode);
        if (req != null)
        {
            this.ExpandedRow = !this.ExpandedRow;
        }
        else
        {
            if (op == IncDec.Inc)
            {
                List<Order> all;
                all = mSession.BasketTemp;

                if (mSession.BasketTemp.Where(z => z.fbCode == FbCode && z.IsPWP && z.fbUnitPrice == double.Parse(lblPrice.Text)).Count() > 0)
                {
                    all.First(z => z.fbCode == FbCode).Quantity++;
                }
                else
                {
                    Tblfoodbeverage pr = dc.Tblfoodbeverages.First(z => z.Foodbeveragecode == FbCode);
                    Order order = new Order();
                    order.fbCode = FbCode;
                    order.Description = pr.Foodbeveragedescription;
                    order.AltDescription = pr.Foodbeveragealternatedescription;
                    order.Quantity = 1;
//                   order.Pic = ConfigFile.PicVisible ? pr.Foodbeveragepicture : null;
                    order.fbUnitPrice = double.Parse(lblPrice.Text);
                    order.IsPWP = true;
                    all.Add(order);
                }
                mSession.BasketTemp = all;
            }
            if (op == IncDec.Dec)
            {
                var item = mSession.BasketTemp.FirstOrDefault(z => z.fbCode == FbCode && z.fbUnitPrice == double.Parse(lblPrice.Text));
                if (item != null)
                {
                    if (item.Quantity > 1)
                        mSession.BasketTemp.First(z => z.fbCode == FbCode).Quantity--;
                    else
                        mSession.BasketTemp.Remove(mSession.BasketTemp.First(z => z.fbCode == FbCode));
                }
            }
        }
        Fill();
    }

    private bool HasRequest()
    {
        var req = dc.Tblfoodbeveragerequests.FirstOrDefault(z => z.Foodbeveragecode == FbCode);
        if (req != null)
            return true;
        else return false;
    }
    private int FindQuantityReq(string fbCode, int RequestID)
    {
        if (RowOrder != null)
        {
            if (RowOrder.Requests.Count > 0)
            {
                return RowOrder.Requests.Count;
            }
            return 0;
        }
        else
            return 0;
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //wPWP.VisibleOnPageLoad = false;
        this.FoatPattern = ConfigFile.FloatPattern;
        lblRM.Text = ConfigFile.Currency;
        //gBatch1.ClientSettings.Selecting.AllowRowSelect = true;

        tbl.Height = ConfigFile.GapBetweenItems + "px";
        if (!IsPostBack)
            Fill();
    }

    protected override void OnPreRender(System.EventArgs e)
    {
        base.OnPreRender(e);
        //imgPicTT.ResizeMode = BinaryImageResizeMode.Fit;
        //imgPicTT.Width = imgPicTT.Height = System.Web.UI.WebControls.Unit.Parse(ConfigFile.TooltipPictureSize"]);
    }

    public void Fill()
    {
        if (RowOrder != null)
        {
            //if (RowOrder.Requests.Count == 0)
            this.Quantity = RowOrder.Quantity.ToString();
            //else
            //    this.Quantity = RowOrder.Requests.Count.ToString();
        }
        else
        {
            this.Quantity = "0";
        }

        this.VisibleMinus = VisibleMinMaster(hfID.Value);
        VisibleQuantity = this.VisibleQty(hfID.Value);

        RefreshGridRequest();
        RefreshGridBatches();
        ResizeFonts();

        CheckRequestRules("Batch1");
        CheckRequestRules("Batch2");
        CheckRequestRules("Batch3");

        if (VisibleQuantity)
        {
            btnPlusss.Visible = false;
            btnMinuss.Visible = true;

            if (this.HasRequest())
            {
                btnMinuss.Visible = false;
                pnlRequest.Visible = false;
            }

        }
        else
        {
            btnPlusss.Visible = true;
        }
    }

    private void RefreshGridRequest()
    {
        string cr = ConfigFile.Currency;
        if (RowOrder != null)
        {
            var q = from i in RowOrder.Requests
                    select new
                    {
                        ID = i.ID,
                        ReqString = Concat(i.RequestUnits),
                        Font = mSession.AppFont,
                        i.Quantity,
                        Currency = cr,
                        Price = FormatPrice(i.RequestUnits.Sum(z => z.Price)), //i.Price,
                    };
            if (RowOrder.Requests.Count > 0)
            {
                pnlInnerBasket.Visible = true;
                gRequest.DataSource = q.ToList().OrderBy(z => z.ReqString);
                gRequest.DataBind();
            }
            else
            {
                pnlInnerBasket.Visible = false;
            }
        }
        else
        {
            pnlInnerBasket.Visible = false;
        }
    }

    private string Concat(List<RequestUnit> RequestUnits)
    {
        string str = "";
        foreach (var r in RequestUnits)
        {
            str += r.RDescription + "; ";
        }
        return str;
    }

    private bool VisibleMinMaster(string fbCode)
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

    private bool VisibleQty(string fbCode)
    {
        if (mSession.BasketTemp.Where(z => z.fbCode == fbCode).Count() > 0)
            return true;
        else
            return false;
    }

    private string FormatPrice(double? price)
    {
        return String.Format("{0:0" + this.FoatPattern + "}", price);
        //return String.Format("{0:0.00}", price);
    }

    protected void btnPlus_Click(object sender, EventArgs e)
    {
        IncDecQty(IncDec.Inc);
    }

    protected void btnMinus_Click(object sender, EventArgs e)
    {
        IncDecQty(IncDec.Dec);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        List<int> requestCodes = new List<int>();
        requestCodes.AddRange(ReadGridRequest(gBatch1));
        requestCodes.AddRange(ReadGridRequest(gBatch2));
        requestCodes.AddRange(ReadGridRequest(gBatch3));

        bool result1 = CheckRequestRules("Batch1");
        bool result2 = CheckRequestRules("Batch2");
        bool result3 = CheckRequestRules("Batch3");
        if (result1)
        {
            lblMsgBatch1.ForeColor = System.Drawing.Color.Blue;

        }
        else
        {
            lblMsgBatch1.ForeColor = System.Drawing.Color.Red;
        }

        if (result2)
        {
            lblMsgBatch2.ForeColor = System.Drawing.Color.Blue;
        }
        else
        {
            lblMsgBatch2.ForeColor = System.Drawing.Color.Red;
        }

        if (result3)
        {
            lblMsgBatch3.ForeColor = System.Drawing.Color.Blue;
        }
        else
        {
            lblMsgBatch3.ForeColor = System.Drawing.Color.Red;
        }
        if (result1 && result2 && result3)
        {
            AddRequestToToBasketTemp(requestCodes, hfID.Value);
            CleanGrids();
            Fill();
            //if (_HasPWP.Value == "True")
            //    LoadPWPwindw(this.FbCode);
        }
    }

    private List<int> ReadGridRequest(RadGrid gBatch)
    {
        List<int> requestCodes = new List<int>();
        foreach (GridDataItem grdItem in gBatch.Items)
        {
            CheckBox checkBox = (CheckBox)grdItem.FindControl("CheckBox1");
            if (checkBox.Checked == true)
            {
                int rCode = Convert.ToInt32(grdItem.GetDataKeyValue("RCode"));
                requestCodes.Add(rCode);
            }
        }
        return requestCodes;
    }

    private void CleanGrids()
    {
        foreach (GridDataItem grdItem in gBatch1.Items)
        {

            ((CheckBox)grdItem.FindControl("CheckBox1")).Checked = false;

        }
    }

    protected void gRequest_ItemCommand(object source, GridCommandEventArgs e)
    {
        int rID = int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString());
        if (e.CommandName == "MinusRequest")
        {


            var rq = RowOrder.Requests.First(z => z.ID == rID);
            RowOrder.Quantity--;
            if (rq.Quantity > 1)
                rq.Quantity--;
            else
            {
                RowOrder.Requests.Remove(rq);
                if (RowOrder.Requests.Count == 0)
                    RowOrder = null;
            }
        }
        else if (e.CommandName == "PlusRequest")
        {
            RowOrder.Requests.First(z => z.ID == rID).Quantity++;
            RowOrder.Quantity++;
        }
        Fill();
        //RefreshGridRequest();
    }

    private void RefreshGridBatches()
    {
        string cr = ConfigFile.Currency;
        bool sort = ConfigFile.RequestSortByDescription.ToLower() == "true" ? true : false; ;
        var q = (from z in dc.Tblfoodbeveragerequests
                 where z.Foodbeveragecode == FbCode
                 select new
                 {
                     RCode = z.Foodbeveragerequestpk,
                     RDesc = z.Foodbeveragerequest,
                     Price = z.Foodbeveragerequestprice,
                     Quantity = FindQuantityReq(z.Foodbeveragecode, z.Foodbeveragerequestpk),
                     Batch = z.Foodbeveragerequestbatch,

                     //Color = i.Quantity == 0 ? System.Drawing.Color.Transparent : System.Drawing.Color.Red,
                 });
        if (q.Count() > 0)
        {
            var request = from i in q.AsEnumerable()
                          select new
                          {
                              i.RCode,
                              i.Quantity,
                              RDesc = " " + i.RDesc + "        ",
                              Color = i.Quantity == 0 ? System.Drawing.Color.Transparent : System.Drawing.Color.Red,
                              Enable = i.Quantity == 0 ? false : true,
                              Font = mSession.AppFont.ToString(),
                              Currency = cr,
                              Price = FormatPrice(i.Price), //i.Price,
                              VisibleFee = i.Price == 0 ? false : true,
                              i.Batch,
                          };

            var batch1 = request.Where(i => i.Batch == 1);
            var batch2 = request.Where(i => i.Batch == 2);
            var batch3 = request.Where(i => i.Batch == 3);
            //Batch1
            if (batch1.Count() > 0)
            {
                //lblMsgBatch1
                lblMsgBatch1.Visible = true;
                gBatch1.Visible = true;
                gBatch1.DataSource = sort ? batch1.OrderBy(i => i.RDesc).ToList() : batch1.ToList();
                gBatch1.DataBind();
            }
            else
            {
                lblMsgBatch2.Visible = false;
                gBatch1.Visible = false;
            }
            //Batch2
            if (batch2.Count() > 0)
            {
                lblMsgBatch2.Visible = true;
                gBatch2.Visible = true;
                gBatch2.DataSource = sort ? batch2.OrderBy(i => i.RDesc).ToList() : batch2.ToList();
                gBatch2.DataBind();
            }
            else
            {
                lblMsgBatch2.Visible = false;
                gBatch2.Visible = false;
            }
            //Batch3
            if (batch3.Count() > 0)
            {
                lblMsgBatch3.Visible = true;
                gBatch3.Visible = true;
                gBatch3.DataSource = sort ? batch3.OrderBy(i => i.RDesc).ToList() : batch3.ToList();
                gBatch3.DataBind();
            }
            else
            {
                lblMsgBatch3.Visible = false;
                gBatch3.Visible = false;
            }
        }
    }

    private bool CheckRequestRules(string batch)
    {
        var rules = dc.Tblfoodbeveragerequestrules.Where(z => z.Foodbeveragecode == FbCode);
        var ruleBatch1 = rules.FirstOrDefault(i => i.Foodbeveragerequestbatch == 1);
        var ruleBatch2 = rules.FirstOrDefault(i => i.Foodbeveragerequestbatch == 2);
        var ruleBatch3 = rules.FirstOrDefault(i => i.Foodbeveragerequestbatch == 3);

        if (batch == "Batch1")
        {
            if (ruleBatch1 != null)
            {
                lblMsgBatch1.Text = "Choose " + (ruleBatch1.Foodbeveragerequestrulescondition == "Exact" ? "" : ruleBatch1.Foodbeveragerequestrulescondition) + " " + ruleBatch1.Foodbeveragerequestrulesnumber + " Item";
                lblMsgBatch2.Text = "";
                lblMsgBatch3.Text = "";
                if (CheckRule(ruleBatch1, ReadGridRequest(gBatch1)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                lblMsgBatch1.Text = "_";
                return true;
            }
        }


        if (batch == "Batch2")
        {
            if (ruleBatch2 != null)
            {
                lblMsgBatch2.Text = "Choose " + (ruleBatch2.Foodbeveragerequestrulescondition == "Exact" ? "" : ruleBatch2.Foodbeveragerequestrulescondition) + " " + ruleBatch2.Foodbeveragerequestrulesnumber + " Item";
                lblMsgBatch3.Text = "";
                if (CheckRule(ruleBatch2, ReadGridRequest(gBatch2)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                lblMsgBatch2.Text = "_";
                return true;
            }
        }

        if (batch == "Batch3")
        {
            if (ruleBatch3 != null)
            {
                lblMsgBatch3.Text = "Choose " + (ruleBatch3.Foodbeveragerequestrulescondition == "Exact" ? "" : ruleBatch3.Foodbeveragerequestrulescondition) + " " + ruleBatch3.Foodbeveragerequestrulesnumber + " Item";
                if (CheckRule(ruleBatch3, ReadGridRequest(gBatch3)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                lblMsgBatch3.Text = "_";
                return true;
            }
        }
        return true;
        //return result1 && result2 && result3;
    }

    private bool CheckRule(Tblfoodbeveragerequestrule rule, List<int> gridSelectedReqs)
    {
        if (rule.Foodbeveragerequestrulescondition == "At Least")
        {
            if (gridSelectedReqs.Count >= rule.Foodbeveragerequestrulesnumber)
                return true;
            else return false;
        }
        if (rule.Foodbeveragerequestrulescondition == "Exact")
        {
            if (gridSelectedReqs.Count == rule.Foodbeveragerequestrulesnumber)
                return true;
            else return false;
        }

        if (rule.Foodbeveragerequestrulescondition == "At Most")
        {
            if (gridSelectedReqs.Count <= rule.Foodbeveragerequestrulesnumber && gridSelectedReqs.Count > 0)
                return true;
            else return false;
        }
        return false;
    }

    private void AddRequestToToBasketTemp(List<int> RCode, string fbCode)
    {

        //if (rule.Foodbeveragerequestrulescondition == "At Least")
        //{
        //    if (gridSelectedReqs.Count >= rule.Foodbeveragerequestrulesnumber)
        //        return true;
        //    else return false;
        //}
        if (RCode.Count == 0 && (dc.Tblfoodbeveragerequestrules.Where(z => z.Foodbeveragecode == FbCode).Count() == 0 || dc.Tblfoodbeveragerequestrules.Where(z => z.Foodbeveragecode == FbCode && z.Foodbeveragerequestrulescondition == "At Least").Count() > 0))
        {
            //var fb = dc.Tblfoodbeveragerequest.First(i => i.Foodbeveragerequestpk == RCode[0]).Tblfoodbeverage;
            var fb = dc.Tblfoodbeverages.First(i => i.Foodbeveragecode == fbCode);
            Order order = RowOrder;
            if (order == null)
            {
                order = new Order();

                order.fbCode = fbCode;
                order.Description = fb.Foodbeveragedescription;
                order.AltDescription = fb.Foodbeveragealternatedescription;
                order.Quantity = 1;
//               order.Pic = fb.Foodbeveragepicture;
                order.fbUnitPrice = tools.CalculatePrice(fb).Value; //.Foodbeverageprice.Value;
                RowOrder = order;
            }
            else
            {
                mSession.BasketTemp.FirstOrDefault(z => z.fbCode == this.FbCode).Quantity++;
            }

            Requests requests = new Requests();
            requests.ID = RowOrder.Requests.Count > 0 ? RowOrder.Requests.Max(z => z.ID) + 1 : 0;

            RequestUnit newR = new RequestUnit();
            newR.fbCode = order.fbCode;
            newR.RCode = -1;
            newR.RDescription = "Normal ";
            newR.Price = 0;
            requests.RequestUnits.Add(newR);


            var q = RowOrder.Requests.FirstOrDefault(i => i == requests);

            if (q == null)
            {
                RowOrder.Requests.Add(requests);

            }
            else
            {
                RowOrder.Requests.FirstOrDefault(i => i == requests).Quantity++;

            }
        }
        if (RCode.Count > 0)
        {
            var fb = dc.Tblfoodbeveragerequests.First(i => i.Foodbeveragerequestpk == RCode[0]).Tblfoodbeverage;

            Order order = RowOrder;
            if (order == null)
            {
                order = new Order();

                order.fbCode = fb.Foodbeveragecode;
                order.Description = fb.Foodbeveragedescription;
                order.AltDescription = fb.Foodbeveragealternatedescription;
                order.Quantity = 1;
//               order.Pic = fb.Foodbeveragepicture;
                order.fbUnitPrice = tools.CalculatePrice(fb).Value; //.Foodbeverageprice.Value;
                RowOrder = order;
            }
            else
            {
                mSession.BasketTemp.FirstOrDefault(z => z.fbCode == this.FbCode).Quantity++;
            }

            Requests requests = new Requests();
            requests.ID = RowOrder.Requests.Count > 0 ? RowOrder.Requests.Max(z => z.ID) + 1 : 0;
            foreach (int rc in RCode)
            {
                var req = dc.Tblfoodbeveragerequests.First(z => z.Foodbeveragerequestpk == rc);

                RequestUnit newR = new RequestUnit();
                newR.fbCode = order.fbCode;
                newR.RCode = req.Foodbeveragerequestpk;
                newR.RDescription = req.Foodbeveragerequest;
                newR.Price = req.Foodbeveragerequestprice.Value;
                requests.RequestUnits.Add(newR);
            }

            var q = RowOrder.Requests.FirstOrDefault(i => i == requests);

            if (q == null)
            {
                RowOrder.Requests.Add(requests);

            }
            else
            {
                RowOrder.Requests.FirstOrDefault(i => i == requests).Quantity++;

            }
        }
    }


    private void ResizeFonts()
    {
        int font = int.Parse(ConfigFile.Font_Batches);
        btnAdd.Font.Size = mSession.AppFont;
        //font = font - 3;
        gBatch1.Columns[0].ItemStyle.Font.Size = font;
        // gBatch1.Columns[1].ItemStyle.Font.Size = font;

        gBatch2.Columns[0].ItemStyle.Font.Size = font;
        //gBatch2.Columns[1].ItemStyle.Font.Size = font;

        gBatch3.Columns[0].ItemStyle.Font.Size = font;
        //gBatch3.Columns[1].ItemStyle.Font.Size = font;

        gRequest.Columns[0].ItemStyle.Font.Size = font;
        gRequest.Columns[1].ItemStyle.Font.Size = font;

        //gRequest.AlternatingItemStyle = null;

        lblMsgBatch1.Font.Size = font;
        lblMsgBatch2.Font.Size = font;
        lblMsgBatch3.Font.Size = font;


    }

    protected void gBatch1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        //if (e.Item is GridDataItem)
        //{
        //    GridDataItem item = (GridDataItem)e.Item;
        //    CheckBox chk = (CheckBox)item.FindControl("CheckBox1");
        //    chk.Attributes.Add("onclick", "setStatus('" + item.ItemIndex + "','" + chk.ClientID + "');");
        //} 
    }

    //private bool _HasPWP = false;
    public bool HasPWP
    {
        set
        {
            _HasPWP.Value = value.ToString();
        }
    }
}