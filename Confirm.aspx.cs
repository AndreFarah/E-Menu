using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantpoContext;


public partial class Confirm : System.Web.UI.Page
{
    DCDC dc = new DCDC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (mSession.EmployeeCode != null)
            {
                lTable.Visible = true;
                pnlTableNumber.Visible = true;
            }
            else
            {
                lTable.Visible = false;
                pnlTableNumber.Visible = false;
                DiscardSessions();
                lblMessage.Text = "Exit this window and login again.";

            }
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

    private string ConvertBasketToString()
    {
        string result = "";
        for (int i = 0; i < mSession.Basket.Distinct().Count(); i++)
        {
            Order order = mSession.Basket[i];

            string fbcode = "";
            string description = "";
            string altDescription = "";
            //string request = "";
            double price = 0;
            int quantity = 0;
            fbcode = order.fbCode;
            description = order.Description;
            altDescription = order.AltDescription;


            if (order.Requests.Count == 0)
            {
                price = order.fbUnitPrice;
                //request = "--";
                quantity = order.Quantity;
                result += fbcode + "||" + description + "||" + price + "||" + quantity + "|||";
            }
            else
            {
                var orders = order.Requests.OrderBy(k => string.Concat((
                    from j in k.RequestUnits.ToList()
                    select new { j.RDescription }).ToList()));
                foreach (Requests requests in orders)
                {
                    price = order.fbUnitPrice + requests.RequestUnits.Sum(z => z.Price);
                    quantity = requests.Quantity;

                    if ((RegistryReader.DescriptionOnKitchenOrder == false && RegistryReader.AlternateDescriptionOnKitchenOrder == false) ||
                        (RegistryReader.DescriptionOnKitchenOrder == true && RegistryReader.AlternateDescriptionOnKitchenOrder == false))
                        description = order.Description;
                    else if (RegistryReader.DescriptionOnKitchenOrder == false && RegistryReader.AlternateDescriptionOnKitchenOrder == true)
                        description = order.AltDescription;
                    else if (RegistryReader.DescriptionOnKitchenOrder == true && RegistryReader.AlternateDescriptionOnKitchenOrder == true)
                        description = order.Description + " - " + order.AltDescription;

                    //request += string.Concat((from z in requests.RequestUnits select new { dsc = "\n" + "*" + z.RDescription }).ToArray<string>());
                    foreach (var unit in requests.RequestUnits)
                    {
                        if (unit.RCode != -1)
                            description += "\n" + "*" + unit.RDescription;
                    }
                    result += fbcode + "||" + description + "||" + +price + "||" + quantity + "|||";

                }
            }

        }

        return result;
    }


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
    protected void RadListView1_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        //txtEmpCode.Font.Size = 18; //mSession.AppFont;
        //btnConfirm.Font.Size = 18;//mSession.AppFont;
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
        localhost.Ordering or = new localhost.Ordering();


        string iConflictMessage = "True";
        try
        {
            iConflictMessage = or.CheckLicenseConflict_EMenu(mSession.CounterCode);
        }
        catch (Exception ex)
        {
            iConflictMessage = ex.ToString();// "Connection failed, Please try again.";
        }

        if (iConflictMessage != "True")
        {
            DiscardSessions();
            lblMessage.Text = iConflictMessage;
        }
        else
        {
            string guid = Guid.NewGuid().ToString();
            string counter = mSession.CounterCode;
            string basketStr = ConvertBasketToString();
            try
            {

                if (or.AddiPadOrder(basketStr, counter, mSession.EmployeeCode, tableID,
                    false, true, ConfigFile.PrintOrderList, ConfigFile.PrintKitchenOrderList, ConfigFile.PrintBarOrderList, false, false, false, guid, 0))
                {
                    lblMessage.Text = "Successfully sent to the kitchen.";
                    DiscardSessions();
                }
                else
                {
                    lblMessage.Text = "Order didn't send to the kitchen";
                }
            }
            catch (Exception ex)
            { lblMessage.Text = ex.Message; }
        }
    }

    private void DiscardSessions()
    {

        mSession.Basket = null;
        mSession.TableName = null;
        mSession.ExpandedRows = null;
        lTable.Visible = false;
        //txtEmpCode.Visible = false;
        //btnConfirm.Visible = false;
        btnConfirmTableNumber.Visible = false;
        txtTableNumber.Visible = false;
        //txtEmpCode.Visible = false;
        hfRefresh.Value = "refresh";
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