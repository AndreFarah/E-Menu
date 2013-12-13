using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantpoContext;
using System.Drawing;
using System.Web.UI.WebControls;

public class MenuCookieMng
{
    public MenuCookieMng() { }
}
/// <summary>
/// Summary description for MenuSession
/// </summary>
public class MenuSessionMng
{
    public MenuSessionMng()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    DCDC dc = new DCDC();

    public void DiscardSessions()
    {
        Basket = null;
        TableName = null;
        ExpandedRows = null;
        ViewingBasket = false;

    }

    //public string AuthenticatedEmployee
    //{
    //    get
    //    {
    //        if (!string.IsNullOrEmpty(this.AuthenticationString))
    //        {
    //            DCDC dc = new DCDC();
    //            Tblemployee emp = dc.Tblemployees.ToList().FirstOrDefault(p => MD5Encoding.ComputeMD5(p.Employeecode.ToLower()) == this.AuthenticationString);
    //            if (emp != null)
    //            {
    //                return emp.Employeecode;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}

    //public string AuthenticationString
    //{
    //    get
    //    {
    //        if (HttpContext.Current.Session["AuthenticationString"] != null)
    //        {
    //            return HttpContext.Current.Session["AuthenticationString"].ToString();
    //        }
    //        else
    //        {
    //            return (HttpContext.Current.Session["AuthenticationString"] = "").ToString();
    //        }

    //    }
    //    set
    //    {
    //        if (value != null)
    //        {
    //            HttpContext.Current.Session["AuthenticationString"] = value;
    //        }
    //        else
    //        {
    //            HttpContext.Current.Session["AuthenticationString"] = "";
    //        }
    //    }
    //}



    public string EmployeeCode
    {
        get
        {
            if (HttpContext.Current.Session["EmployeeCode"] != null)
            {
                return HttpContext.Current.Session["EmployeeCode"].ToString();
            }
            else
            {
                return (HttpContext.Current.Session["EmployeeCode"] = "").ToString();
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["EmployeeCode"] = value;
            }
            else
            {
                HttpContext.Current.Session["EmployeeCode"] = "";
            }
        }
    }

    public string CounterCode
    {
        get
        {
            if (HttpContext.Current.Session["CounterCode"] != null)
            {
                return HttpContext.Current.Session["CounterCode"].ToString();
            }
            else
            {
                return (HttpContext.Current.Session["CounterCode"] = "").ToString();
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["CounterCode"] = value;
            }
            else
            {
                HttpContext.Current.Session["CounterCode"] = "";
            }
        }
    }

    public string TableName
    {
        get
        {
            if (HttpContext.Current.Session["TableName"] != null)
            {
                return HttpContext.Current.Session["TableName"].ToString();
            }
            else
            {
                return (HttpContext.Current.Session["TableName"] = "").ToString();
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["TableName"] = value;
            }
            else
            {
                HttpContext.Current.Session["TableName"] = "";
            }
        }
    }

    public string GroupName
    {
        get
        {
            if (HttpContext.Current.Session["GroupName"] != null)
            {
                return HttpContext.Current.Session["GroupName"].ToString();
            }
            else
            {
                return (HttpContext.Current.Session["GroupName"] = "").ToString();
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["GroupName"] = value;
            }
            else
            {
                HttpContext.Current.Session["GroupName"] = "";
            }
        }
    }

    public string MenuType
    {
        get
        {
            if (HttpContext.Current.Session["MenuType"] != null)
            {
                return HttpContext.Current.Session["MenuType"].ToString();
            }
            else
            {
                return (HttpContext.Current.Session["MenuType"] = "").ToString();
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["MenuType"] = value;
            }
            else
            {
                HttpContext.Current.Session["MenuType"] = "";
            }
        }
    }

    public bool CancelOrConfirmOrder
    {
        get
        {
            if (HttpContext.Current.Session["CancelOrConfirmOrder"] != null)
            {
                return (bool)HttpContext.Current.Session["CancelOrConfirmOrder"];
            }
            else
            {
                return false;
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["CancelOrConfirmOrder"] = value;
            }
            else
            {
                HttpContext.Current.Session["CancelOrConfirmOrder"] = false;
            }
        }
    }
    //public List<string> Orders
    //{
    //    get
    //    {
    //        if (HttpContext.Current.Session["Orders"] != null)
    //        {
    //            return (List<string>)HttpContext.Current.Session["Orders"];
    //        }
    //        else
    //        {
    //            List<string> or = new List<string>();
    //            HttpContext.Current.Session["Orders"] = or;
    //            return (List<string>)HttpContext.Current.Session["Orders"];
    //        };
    //    }
    //    set
    //    {
    //        if (value != null)
    //        {
    //            HttpContext.Current.Session["Orders"] = value;
    //        }
    //        else
    //        {
    //            HttpContext.Current.Session["MenuType"] = "";
    //        }
    //    }
    //}

    public bool ViewingBasket
    {
        get
        {
            if (HttpContext.Current.Session["ViewBasket"] != null)
            {
                return (bool)HttpContext.Current.Session["ViewBasket"];
            }
            else
            {
                HttpContext.Current.Session["ViewBasket"] = false;
                return false;
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["ViewBasket"] = value;
            }
            else
            {
                HttpContext.Current.Session["ViewBasket"] = false;
            }
        }
    }

    public bool RefreshGrid
    {
        get
        {
            if (HttpContext.Current.Session["RefreshGrid"] != null)
            {
                return (bool)HttpContext.Current.Session["RefreshGrid"];
            }
            else
            {
                HttpContext.Current.Session["RefreshGrid"] = false;
                return false;
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["RefreshGrid"] = value;
            }
            else
            {
                HttpContext.Current.Session["RefreshGrid"] = false;
            }
        }
    }


    public int AppFont
    {
        get
        {
            if (HttpContext.Current.Session["AppFont"] != null)
            {
                return int.Parse(HttpContext.Current.Session["AppFont"].ToString());
            }
            else
            {
                string defaultFont = ConfigFile.Font2;
                HttpContext.Current.Session["AppFont"] = defaultFont;
                return int.Parse(defaultFont);
            }

        }
        set
        {
            HttpContext.Current.Session["AppFont"] = value;
        }
    }

    //public bool PicVisible
    //{
    //    get
    //    {
    //        if (HttpContext.Current.Session["PicVisible"] != null)
    //        {
    //            return HttpContext.Current.Session["PicVisible"].ToString() == "True" ? true : false;
    //        }
    //        else
    //        {
    //            string PicVisible = ConfigFile.PicVisible;
    //            HttpContext.Current.Session["PicVisible"] = PicVisible;
    //            return PicVisible;
    //        }

    //    }
    //    set
    //    {
    //        HttpContext.Current.Session["PicVisible"] = value ? "True" : "False";
    //    }
    //}


    public List<Order> Basket
    {
        get
        {
            if (HttpContext.Current.Session["Order"] != null)
            {
                return (List<Order>)HttpContext.Current.Session["Order"];
            }
            else
            {
                return new List<Order>();
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["Order"] = value;
            }
            else
            {
                HttpContext.Current.Session["Order"] = null;
            }
        }
    }

    public List<Order> BasketTemp
    {
        get
        {
            if (HttpContext.Current.Session["OrderTemp"] != null)
            {
                return (List<Order>)HttpContext.Current.Session["OrderTemp"];
            }
            else
            {
                return new List<Order>();
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["OrderTemp"] = value;
            }
            else
            {
                HttpContext.Current.Session["OrderTemp"] = null;
            }
        }
    }
    // hfMenuSelected

    public List<string> ExpandedRows
    {
        get
        {
            if (HttpContext.Current.Session["ExpandedRows"] != null)
            {
                return (List<string>)HttpContext.Current.Session["ExpandedRows"];
            }
            else
            {
                return new List<string>();
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["ExpandedRows"] = value;
            }
            else
            {
                HttpContext.Current.Session["ExpandedRows"] = null;
            }
        }
    }

    public List<string> MastersIncludedChiled
    {
        get
        {
            if (HttpContext.Current.Session["MastersIncludedChiled"] != null)
            {
                return (List<string>)HttpContext.Current.Session["MastersIncludedChiled"];
            }
            else
            {
                return new List<string>();
            }

        }
        set
        {
            if (value != null)
            {
                HttpContext.Current.Session["MastersIncludedChiled"] = value;
            }
            else
            {
                HttpContext.Current.Session["MastersIncludedChiled"] = null;
            }
        }
    }

    public Language Language
    {
        get
        {
            if (HttpContext.Current.Session["Language"] != null)
            {
                return HttpContext.Current.Session["Language"].ToString() == Language.Description.ToString() ? Language.Description : Language.Alternate;
            }
            else
            {
                Language lang = Language.Description;
                HttpContext.Current.Session["Language"] = lang.ToString();
                return lang;
            }
        }
        set
        {
            HttpContext.Current.Session["Language"] = value;
        }
    }

    public PrintReceiptOrRecallOrder WindowParameter_PrintReceiptOrRecallOrder
    {
        get
        {
            if (HttpContext.Current.Session["WindowParameter_PrintReceiptOrRecallOrder"] != null)
            {
                if (HttpContext.Current.Session["WindowParameter_PrintReceiptOrRecallOrder"].ToString() == PrintReceiptOrRecallOrder.PrintReceipt.ToString())
                    return PrintReceiptOrRecallOrder.PrintReceipt;
                else if (HttpContext.Current.Session["WindowParameter_PrintReceiptOrRecallOrder"].ToString() == PrintReceiptOrRecallOrder.RecallOrder.ToString())
                    return PrintReceiptOrRecallOrder.RecallOrder;
                if (HttpContext.Current.Session["WindowParameter_PrintReceiptOrRecallOrder"].ToString() == PrintReceiptOrRecallOrder.Payment.ToString())
                    return PrintReceiptOrRecallOrder.Payment;
                else throw new Exception("No selected function");
            }
            else
            {
                PrintReceiptOrRecallOrder arg = PrintReceiptOrRecallOrder.PrintReceipt;
                HttpContext.Current.Session["WindowParameter_PrintReceiptOrRecallOrder"] = arg.ToString();
                return arg;
            }
        }
        set
        {
            HttpContext.Current.Session["WindowParameter_PrintReceiptOrRecallOrder"] = value.ToString();
        }
    }

    public string WindowParameter_PWPfbCode
    {
        get
        {
            if (HttpContext.Current.Session["WindowParameter_PWPfbCode"] != null)
            {
                return HttpContext.Current.Session["WindowParameter_PWPfbCode"].ToString();
            }
            else
            {
                HttpContext.Current.Session["WindowParameter_PWPfbCode"] = "";
                return "";
            }
        }
        set
        {
            HttpContext.Current.Session["WindowParameter_PWPfbCode"] = value.ToString();
        }
    }

}



public enum PrintReceiptOrRecallOrder
{
    PrintReceipt,
    RecallOrder,
    Payment,
}
public enum Language
{
    Description,
    Alternate
}



public class Order
{
    public Order()
    {
        Requests = new List<Requests>();
    }
    public string fbCode;
    public string Description;
    public string AltDescription;
    public int Quantity;
    public double fbUnitPrice;
   // public byte[] Pic;
    public List<Requests> Requests;
    public bool OldOrder = false;
    public bool IsPWP = false;
    public bool HasPWP = false;
    public void Add(RequestUnit reqUnit)
    {

    }
    //public int TotalPrice;
}
public class Requests
{
    public Requests()
    {
        RequestUnits = new List<RequestUnit>();
        ID = 0;
        Quantity = 1;
    }
    public int ID;
    public int Quantity;
    public List<RequestUnit> RequestUnits;

    public static bool operator ==(Requests r1, Requests r2)
    {
        // bool result = false;
        if (System.Object.ReferenceEquals(r1, r2))
        {
            return true;
        }
        // If one is null, but not both, return false.
        if (((object)r1 == null) || ((object)r2 == null))
        {
            return false;
        }
        foreach (var req in r1.RequestUnits)
        {
            if (r2.RequestUnits.Where(i => i.RCode == req.RCode).Count() == 0)
                return false;
        }
        return r1.RequestUnits.Count == r2.RequestUnits.Count;
    }


    public static bool operator !=(Requests r1, Requests r2)
    {
        return !(r1 == r2);
    }

}

public class RequestUnit
{
    public RequestUnit()
    {
    }
    public int RCode;
    public string RDescription;
    public string fbCode;
    public double Price;

    //public Order order;
}

public enum IncDec
{
    Inc,
    Dec,
}


