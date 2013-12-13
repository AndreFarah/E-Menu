using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantpoContext;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public partial class TableConfirmation : System.Web.UI.Page
{
    DCDC dc = new DCDC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //lTable.Visible = false;
            //txtEmpCode.Focus();

        }
        LoadFonts();
    }
    MenuSessionMng mSession = new MenuSessionMng();


    //protected void btnConfirm_Click(object sender, EventArgs e)
    //{
    //    if (CheckEmpCode(txtEmpCode.Text))
    //    {
    //        lTable.Visible = true;
    //        pnlTableNumber.Visible = true;
    //        pnlLogin.Visible = false;
    //    }
    //    else
    //    {
    //        lTable.Visible = false;
    //        pnlTableNumber.Visible = false;
    //        pnlLogin.Visible = true;
    //        txtEmpCode.Text = "";
    //    }
    //}

    //private bool CheckEmpCode(string p)
    //{
    //    var emp = dc.Tblemployees.FirstOrDefault(z => z.Employeecode.ToLower() == p.ToLower());
    //    if (emp != null)
    //    {
    //        txtEmpCode.Text = emp.Employeecode;
    //        return true;
    //    }
    //    else return false;
    //}

    protected void RadListView1_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {

        var q = from i in dc.Tbltables.ToList()
                select new
                {
                    ID = i.Tablename,
                    TableNumber = i.Tabledescription,

                };
        lTable.DataSource = q.ToList();

    }


    protected void RadListView1_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Selected")
            {
                string tID = e.CommandArgument.ToString();

                CheckTableIsCorrect(tID);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();// "Order didn't send to the kitchen";
        }
    }

    private void CheckTableIsCorrect(string tableID)
    {
        bool show = ConfigFile.ConfirmationMessageboxShow.ToLower() == "true" ? true : false;
        if (show)
            Messagebox.ShowConfirmation(tableID + "     ?    ", tableID, true, true);
        else
            SendToWS(tableID);
    }

    private void LoadFonts()
    {

        lblMessage.Font.Size = 18; //mSession.AppFont;
        lblClose.Font.Size = 18;// mSession.AppFont;
        Messagebox.Font.Size = 18;
        btnConfirmTableNumber.Font.Size = 18;
        txtTableNumber.Font.Size = 18; //mSession.AppFont;
        // lTable.Font.Size = 18;//mSession.AppFont;
    }
    protected void Messagebox_YesSelected(object sender, string Key)
    {
        SendToWS(Key);
    }

    private void SendToWS(string tableID)
    {
        mSession.TableName = tableID;
        if (mSession.WindowParameter_PrintReceiptOrRecallOrder == PrintReceiptOrRecallOrder.PrintReceipt)
        {
            PrintReceipt(tableID);
        }
        else if (mSession.WindowParameter_PrintReceiptOrRecallOrder == PrintReceiptOrRecallOrder.RecallOrder)
        {
            LoadOrder(tableID);
        }
        else if (mSession.WindowParameter_PrintReceiptOrRecallOrder == PrintReceiptOrRecallOrder.Payment)
        {
            Payment(mSession.TableName);
        }

    }




    //private double GetTotalPayment(int iSalesPK, string iCounterCode)
    //{
    //    string iRevenueCenterCode = "All";
    //    double iTotalPayment = 0;


    //    System.Data.Common.DbConnection conn = dc.Connection;
    //    if (conn.State == ConnectionState.Closed)
    //    {
    //        conn.Open();
    //    }

    //    System.Data.Common.DbCommand cmd;// = new System.Data.Common.DbCommand();
    //    string query = null;

    //    query = "SELECT SUM(CreditCardPaymentAmount) ";
    //    query = query + "FROM tblCreditCardPayment ";
    //    query = query + "WHERE SalesPK = ?SalesPK  AND RevenueCenterCode = '" + iRevenueCenterCode + "' ";
    //    query = query + "GROUP BY SalesPK ";

    //    cmd = new System.Data.Common.DbCommand(query, conn);
    //    cmd.Parameters.Add("?SalesPK", iSalesPK);
    //    iTotalPayment = iTotalPayment + double.Parse( cmd.ExecuteScalar().ToString());

    //    query = "SELECT SUM(CashPaymentAmount) ";
    //    query = query + "FROM tblCashPayment ";
    //    query = query + "WHERE SalesPK = ?SalesPK  AND RevenueCenterCode = '" + iRevenueCenterCode + "' ";
    //    query = query + "GROUP BY SalesPK ";

    //    cmd = new MySqlCommand(query, conn);
    //    cmd.Parameters.Add("?SalesPK", iSalesPK);

    //    iTotalPayment = iTotalPayment + double.Parse(cmd.ExecuteScalar().ToString()); ;

    //    query = "SELECT SUM(HouseChequeAndCreditPaymentAmount) ";
    //    query = query + "FROM tblHouseChequeAndCreditPayment ";
    //    query = query + "WHERE SalesPK = ?SalesPK  AND RevenueCenterCode = '" + iRevenueCenterCode + "' ";
    //    query = query + "GROUP BY SalesPK ";

    //    cmd = new MySqlCommand(query, conn);
    //    cmd.Parameters.Add("?SalesPK", iSalesPK);

    //    iTotalPayment = iTotalPayment + double.Parse(cmd.ExecuteScalar().ToString()); 

    //    return Math.Round(iTotalPayment, 2);
    //}




    private void Payment(string tableID)
    {
        localhost.Ordering or = new localhost.Ordering();
        hfRefresh.Value = "False";
        try
        {
            string amount = or.GetUnsettledAmount(mSession.CounterCode, tableID);
            if (!string.IsNullOrWhiteSpace(amount))
            {
                if (tools.PaymentExpress(tableID, mSession.CounterCode))
                {

                    List<string> allAmount = amount.Split('|').ToList<string>();
                    allAmount = allAmount.Where(z => !string.IsNullOrWhiteSpace(z)).ToList<string>();
                    var q = (from i in allAmount
                             select new
                             {
                                 Amount = i.Split(" : ".ToCharArray()).Where(z => !string.IsNullOrWhiteSpace(z)).ToList()[1],
                             }).ToList();
                    amount = q.Sum(i => double.Parse(i.Amount)).ToString();




                    lblMessage.Text = "Successfully paid RM " + String.Format("{0:00}", amount);;
                    //or.GetAnySalesPKFromTableName(mSession.CounterCode, tableID), mSession.CounterCode).ToString();
                    txtTableNumber.Visible = false;
                    btnConfirmTableNumber.Visible = false;
                    lTable.Visible = false;
                    mSession.DiscardSessions();
                    hfRefresh.Value = "True";
                }
                else
                {
                    mSession.TableName = "";
                    lblMessage.Text = "Can not be paid.";
                }
            }
            else
            {
                lblMessage.Text = "There is no order for table : " + mSession.TableName;
                mSession.TableName = "";
            }
        }
        catch (Exception ex)
        { lblMessage.Text = ex.Message; mSession.TableName = ""; }
    }

    private void LoadOrder(string tableID)
    {
        localhost.Ordering or = new localhost.Ordering();
        hfRefresh.Value = "True";
        try
        {
            int salesPK = or.GetAnySalesPKFromTableName(mSession.CounterCode, tableID);

            string orders = or.GetSalesItem(mSession.CounterCode, salesPK);
            if (!string.IsNullOrEmpty(orders))
            {

                List<string> arrOrder = orders.Split("^".ToCharArray()).ToList();
                arrOrder = arrOrder.Where(z => !string.IsNullOrEmpty(z)).ToList();
                List<Order> all = new List<Order>();

                foreach (string str in arrOrder)
                {
                    try
                    {
                        string[] order = str.Split("|".ToCharArray());

                        string FbCode = order[2];



                        Tblfoodbeverage pr = dc.Tblfoodbeverages.First(z => z.Foodbeveragecode == FbCode);
                        Order o = new Order();
                        o.fbCode = order[2];
                        o.Description = order[4];
                        o.AltDescription = order[4];
                        o.Quantity = 1;// arrOrder.Where(i => i.Split("|".ToCharArray())[4] == o.Description).Count();
//                       o.Pic = ConfigFile.PicVisible ? pr.Foodbeveragepicture : null;
                        o.fbUnitPrice = double.Parse(order[5]);
                        o.OldOrder = true;

                        if (all.Where(i => i.Description == o.Description).Count() > 0)
                        {
                            all.First(i => i.Description == o.Description).Quantity++;
                        }
                        else
                            all.Add(o);


                    }
                    catch (Exception ex) { }

                }
                mSession.Basket = all;
                mSession.ViewingBasket = true;
                //        dtr.Item("SalesPK").ToString & "|" & dtr.Item("SalesItemPK").ToString & "|" & _
                //dtr.Item("FoodBeverageCode").ToString & "|" & dtr.Item("SalesItemDescription").ToString & "|" & _
                //dtr.Item("SalesItemKitchenDescription").ToString & "|" & dtr.Item("Amount").ToString & "|"
                //lblMessage.Text = "<script type='text/javascript'> Close() </script>";
                lblMessage.Text = "Successfully loaded.";
                lTable.Visible = false;
                txtTableNumber.Visible = false;
                btnConfirmTableNumber.Visible = false;
            }
            else
            {
                lblMessage.Text = "Order doesn't exist, Select another table.";
                mSession.TableName = "";
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            mSession.TableName = "";
        }
    }
    eMenuTools tools = new eMenuTools();
    private void PrintReceipt(string tableID)
    {
        localhost.Ordering or = new localhost.Ordering();
        hfRefresh.Value = "False";
        try
        {
            int salesPK = or.GetAnySalesPKFromTableName(mSession.CounterCode, tableID);
            string revenue = or.GetRevenueCenterCode(mSession.CounterCode);


            if (or.AddPrintJobToPool(salesPK.ToString(), revenue, true, false, false, "", "", "", ConfigFile.PrintOrderList, ConfigFile.PrintKitchenOrderList, ConfigFile.PrintBarOrderList))
            {
                lblMessage.Text = "Successfully printed.";
                hfRefresh.Value = "True";
            }
            else
            {
                lblMessage.Text = "Order didn't send to the printer";
            }
        }
        catch (Exception ex)
        { lblMessage.Text = ex.Message; }
    }

    protected void Messagebox_NoSelected(object sender, string Key)
    {
        txtTableNumber.Text = "";
    }
    protected void btnConfirmTableNumber_Click(object sender, EventArgs e)
    {
        var q = from i in dc.Tbltables.ToList()
                select new
                {
                    ID = i.Tablename,
                    TableNumber = i.Tabledescription,
                };
        if (q.Where(z => z.TableNumber == txtTableNumber.Text).Count() > 0)
            CheckTableIsCorrect(txtTableNumber.Text);
        else
            Messagebox.ShowAlert("Table not exist.");
    }
    protected void lTable_PreRender(object sender, EventArgs e)
    {

    }
    protected void lTable_ItemCreated(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
    {
        int Width = int.Parse(ConfigFile.TableSize_Width);
        int Height = int.Parse(ConfigFile.TableSize_Height);
        int Font = int.Parse(ConfigFile.TableFont);

        Button btn = e.Item.FindControl("btnTable") as Button;
        btn.Width = Width;
        btn.Height = Height;
        btn.Font.Size = Font;
    }
}