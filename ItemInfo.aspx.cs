using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections.Generic;
using System.Linq;
using RestaurantpoContext;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
public partial class ItemInfo : System.Web.UI.Page
{
    DCDC dc = new DCDC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != "" || Request.QueryString["ID"] != "")
        {
            string ID = Request.QueryString["ID"];

            var item = dc.Tblfoodbeverages.FirstOrDefault(z => z.Foodbeveragecode == ID);
            if (item != null)
            {
                rPic.DataValue = item.Foodbeveragepicture;
                lblDsc.Text = item.Foodbeveragedescription;
                lblAlternate.Text = item.Foodbeveragealternatedescription;
            }
        }

    }
    protected void btn_Click(object sender, EventArgs e)
    {

    }
}