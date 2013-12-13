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

using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

using System.Web.UI.WebControls;

public partial class config : System.Web.UI.Page
{
    DCDC dc = new DCDC();
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

    }

    public static void LogOut()
    {
        //FormsAuthentication.SignOut();
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Response.Redirect("Login.aspx", true);
    }

    MenuSessionMng mSession = new MenuSessionMng();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (mSession.EmployeeCode == "")
            LogOut();
        var conString = ConfigurationManager.ConnectionStrings["restaurantposConnectionString"];
        string strConnString = conString.ConnectionString;

        if (!IsPostBack)
        {
            if (RegistryReader.IsRegistryAvailable)
            {
                isRegistryAvailable.Text = "Available";
                isRegistryAvailable.ForeColor = System.Drawing.Color.Blue;
                isRegistryAvailable.ToolTip = "Happy price is available.";
            }
            else
            {
                isRegistryAvailable.Text = "Unavailable";
                isRegistryAvailable.ForeColor = System.Drawing.Color.Red;
                isRegistryAvailable.ToolTip = "Happy price is not available due to unavailability of Registry.";
            }
            txtCurrency.Text = ConfigFile.Currency;
            txtFloatPattern.Text = ConfigFile.FloatPattern;
            cmbFont1.SelectedValue = ConfigFile.Font1;
            cmbFont2.SelectedValue = ConfigFile.Font2;
            cmbFont3.SelectedValue = ConfigFile.Font3;
            cmbFont_Batches.SelectedValue = ConfigFile.Font_Batches;


            //var q = from i in dc.Tblcounters
            //        select new
            //        {
            //            Code = i.Countercode,
            //            Description = i.Counterdescription,
            //        };
            //cmbCounterCode.DataSource = q.ToList();
            //cmbCounterCode.DataBind();

            //cmbCounterCode.SelectedValue = ConfigFile.CounterCode;
            txtTooltipPictureSize.Text = ConfigFile.TooltipPictureSize;

            rdoPictureVisibleFalse.Checked = !(rdoPictureVisibleTrue.Checked = ConfigFile.PicVisible);

            if (ConfigFile.ConfirmationMessageboxShow == "True")
            {
                rdoConfirmationMessageboxShowTrue.Checked = true;
                rdoConfirmationMessageboxShowFalse.Checked = false;
            }
            else
            {
                rdoConfirmationMessageboxShowTrue.Checked = false;
                rdoConfirmationMessageboxShowFalse.Checked = true;
            }
            txtTableSize_Width.Text = ConfigFile.TableSize_Width;
            txtTableSize_Height.Text = ConfigFile.TableSize_Height;
            cmbTableFont.SelectedValue = ConfigFile.TableFont;

            if (ConfigFile.RequestSortByDescription == "True")
            {
                rdoRequestSortByDescription.Checked = true;
                rdoRequestSortByFIFO.Checked = false;
            }
            else
            {
                rdoRequestSortByDescription.Checked = false;
                rdoRequestSortByFIFO.Checked = true;
            }

            rdoPrintOrderListFalse.Checked = !(rdoPrintOrderListTrue.Checked = ConfigFile.PrintOrderList);

            rdoPrintBarOrderListFalse.Checked = !(rdoPrintBarOrderListTrue.Checked = ConfigFile.PrintBarOrderList);

            rdoPrintKitchenOrderListFalse.Checked = !(rdoPrintKitchenOrderListTrue.Checked = ConfigFile.PrintKitchenOrderList);

            cmbLanguage.SelectedValue = ConfigFile.VisibleLanguageSelection;
            cmbPayment.SelectedValue = ConfigFile.VisiblePayment;
            cmbPrintReceipt.SelectedValue = ConfigFile.VisiblePrintReceipt;
            cmbRecallOrder.SelectedValue = ConfigFile.VisibleRecallOrder;
            txtGapBetweenItems.Text = ConfigFile.GapBetweenItems;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        ConfigFile.Currency = txtCurrency.Text;
        ConfigFile.FloatPattern = txtFloatPattern.Text;
        ConfigFile.Font1 = cmbFont1.Text;


        ConfigFile.Font2 = cmbFont2.Text;
        ConfigFile.Font3 = cmbFont3.Text;
        ConfigFile.Font_Batches = cmbFont_Batches.Text;
        //ConfigFile.CounterCode = cmbCounterCode.SelectedValue;
        ConfigFile.TooltipPictureSize = txtTooltipPictureSize.Text;

        ConfigFile.PicVisible = rdoPictureVisibleTrue.Checked;

        if (rdoConfirmationMessageboxShowTrue.Checked)
        {
            ConfigFile.ConfirmationMessageboxShow = "True";
        }
        else
        {
            ConfigFile.ConfirmationMessageboxShow = "False";
        }
        ConfigFile.TableSize_Width = txtTableSize_Width.Text;
        ConfigFile.TableSize_Height = txtTableSize_Height.Text;
        ConfigFile.TableFont = cmbTableFont.Text;

        if (rdoRequestSortByDescription.Checked)
        {
            ConfigFile.RequestSortByDescription = "True";
        }
        else
        {
            ConfigFile.RequestSortByDescription = "False";
        }

        ConfigFile.PrintOrderList = rdoPrintOrderListTrue.Checked;

        ConfigFile.PrintBarOrderList = rdoPrintBarOrderListTrue.Checked;

        ConfigFile.PrintKitchenOrderList = rdoPrintKitchenOrderListTrue.Checked;

        ConfigFile.Font2 = cmbFont2.Text;


        ConfigFile.VisibleLanguageSelection = cmbLanguage.SelectedValue;
        ConfigFile.VisiblePayment = cmbPayment.SelectedValue;
        ConfigFile.VisiblePrintReceipt = cmbPrintReceipt.SelectedValue;
        ConfigFile.VisibleRecallOrder = cmbRecallOrder.SelectedValue;

        ConfigFile.GapBetweenItems = txtGapBetweenItems.Text;

        Response.Redirect("config.aspx");
    }
    protected void btnBackToMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
}

