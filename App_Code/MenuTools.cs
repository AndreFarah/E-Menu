using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.IO;
using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Threading;
using System.Web;
using RestaurantpoContext;
using System.Security.Permissions;

/// <summary>
/// Summary description for MenuTools
/// </summary>
public static class ConfigFile
{

    static DataSet _DataSet = new DataSet();

    public static DataSet ConfigDataset
    {
        get
        {
            if (_DataSet.Tables.Count > 0 && _DataSet.Tables[0].Rows.Count > 0)
            { return _DataSet; }
            else
            {
                try
                {
                    _DataSet.ReadXml(HttpContext.Current.Server.MapPath("ConfigFile.xml"));
                    while (!((_DataSet.Tables.Count > 0 && _DataSet.Tables[0].Rows.Count > 0)))
                    { }
                    return _DataSet;//.Tables["ConfigTable"];
                }
                catch
                {
                    if (_DataSet.Tables.Count > 0 && _DataSet.Tables[0].Rows.Count > 0)
                    {
                        _DataSet.ReadXml(HttpContext.Current.Server.MapPath("ConfigFile.xml"));
                        return _DataSet;//.Tables["ConfigTable"];
                    }
                    else
                        return _DataSet;
                }
            }

        }
        set
        {
            WriteDatasetToFile();
        }
    }

    public static System.Data.EnumerableRowCollection<System.Data.DataRow> ConfigDataTable
    {
        get
        {
            return ConfigDataset.Tables["ConfigTable"].AsEnumerable();
        }
    }


    private static void WriteDatasetToFile()
    {
        _DataSet.WriteXml(HttpContext.Current.Server.MapPath("ConfigFile.xml"));
    }

    private static string GetValue(string name)
    {
        var q = ConfigDataTable.ToList().FirstOrDefault(i => i.ItemArray[0].ToString() == name);
        return q["Value"].ToString();
    }

    private static void SetValue(string name, string value)
    {
        ConfigDataset.Tables[0].AsEnumerable().ToList().FirstOrDefault(i => i.ItemArray[0].ToString() == name)["Value"] = value;
        WriteDatasetToFile();
    }


    public static string EducationImageURL
    {
        get
        {
            return GetValue("EducationImageURL");
        }
        set
        {
            SetValue("EducationImageURL", value);
        }
    }

    public static string Currency
    {
        get
        {
            return GetValue("Currency");
        }
        set
        {
            SetValue("Currency", value);
        }
    }

    public static string FloatPattern
    {
        get
        {
            return GetValue("FloatPattern");
        }
        set
        {
            SetValue("FloatPattern", value);
        }
    }

    public static string Font1
    {
        get
        {
            return GetValue("Font1");
        }
        set
        {
            SetValue("Font1", value);
        }
    }

    public static string Font2
    {
        get
        {
            return GetValue("Font2");
        }
        set
        {
            SetValue("Font2", value);
        }
    }

    public static string Font3
    {
        get
        {
            return GetValue("Font3");
        }
        set
        {
            SetValue("Font3", value);
        }
    }

    public static string Font_Batches
    {
        get
        {
            return GetValue("Font_Batches");
        }
        set
        {
            SetValue("Font_Batches", value);
        }
    }

    //public static string CounterCode
    //{
    //    get
    //    {
    //        return GetValue("CounterCode");
    //    }
    //    set
    //    {
    //        SetValue("CounterCode", value);
    //    }
    //}

    public static string TooltipPictureSize
    {
        get
        {
            return GetValue("TooltipPictureSize");
        }
        set
        {
            SetValue("TooltipPictureSize", value);
        }
    }

    public static bool PicVisible
    {
        get
        {
            return GetValue("PicVisible") == "True" ? true : false;
        }
        set
        {
            SetValue("PicVisible", value ? "True" : "False");
        }
    }

    public static string ConfirmationMessageboxShow
    {
        get
        {
            return GetValue("ConfirmationMessageboxShow");
        }
        set
        {
            SetValue("ConfirmationMessageboxShow", value);
        }
    }

    public static string TableSize_Width
    {
        get
        {
            return GetValue("TableSize_Width");
        }
        set
        {
            SetValue("TableSize_Width", value);
        }
    }

    public static string TableSize_Height
    {
        get
        {
            return GetValue("TableSize_Height");
        }
        set
        {
            SetValue("TableSize_Height", value);
        }
    }

    public static string TableFont
    {
        get
        {
            return GetValue("TableFont");
        }
        set
        {
            SetValue("TableFont", value);
        }
    }

    public static string RequestSortByDescription
    {
        get
        {
            return GetValue("RequestSortByDescription");
        }
        set
        {
            SetValue("RequestSortByDescription", value);
        }
    }
    //------------------------
    public static bool PrintOrderList
    {
        get
        {
            string val = GetValue("PrintOrderList");
            return val == "True" ? true : false;
        }
        set
        {
            string val = value ? "True" : "False";
            SetValue("PrintOrderList", val);
        }
    }
    public static bool PrintBarOrderList
    {
        get
        {
            string val = GetValue("PrintBarOrderList");
            return val == "True" ? true : false;
        }
        set
        {
            string val = value ? "True" : "False";
            SetValue("PrintBarOrderList", val);
        }
    }
    public static bool PrintKitchenOrderList
    {
        get
        {
            string val = GetValue("PrintKitchenOrderList");
            return val == "True" ? true : false;
        }
        set
        {
            string val = value ? "True" : "False";
            SetValue("PrintKitchenOrderList", val);
        }
    }
    //-----------------Function Visibility
    public static string VisiblePrintReceipt
    {
        get
        {
            return GetValue("VisiblePrintReceipt");

        }
        set
        {
            SetValue("VisiblePrintReceipt", value);
        }
    }

    public static string VisibleRecallOrder
    {
        get
        {
            return GetValue("VisibleRecallOrder");
        }
        set
        {
            SetValue("VisibleRecallOrder", value);
        }
    }

    public static string VisiblePayment
    {
        get
        {
            return GetValue("VisiblePayment");
        }
        set
        {
            SetValue("VisiblePayment", value);
        }
    }

    public static string VisibleLanguageSelection
    {
        get
        {
            return GetValue("VisibleLanguageSelection");
        }
        set
        {
            SetValue("VisibleLanguageSelection", value);
        }
    }

    public static string GapBetweenItems
    {
        get
        {
            return GetValue("GapBetweenItems");
        }
        set
        {
            SetValue("GapBetweenItems", value);
        }
    }

}

public class eMenuTools
{
    public eMenuTools() { }

    public double? CalculatePrice(Tblfoodbeverage fb)
    {
        try
        {
            if (!IsHappyHourDisableToday())
            {
                double price = 0;

                TimeSpan HappyHourDurationFrom = RegistryReader.HappyHourDurationFrom_First.TimeOfDay;
                TimeSpan HappyHourDurationTo = RegistryReader.HappyHourDurationTo_First.TimeOfDay;

                TimeSpan SecondHappyHourDurationFrom = RegistryReader.HappyHourDurationFrom_Second.TimeOfDay;
                TimeSpan SecondHappyHourDurationTo = RegistryReader.HappyHourDurationTo_Second.TimeOfDay;

                TimeSpan ThirdHappyHourDurationFrom = RegistryReader.HappyHourDurationFrom_Third.TimeOfDay;
                TimeSpan ThirdHappyHourDurationTo = RegistryReader.HappyHourDurationTo_Third.TimeOfDay;

                TimeSpan now = DateTime.Now.TimeOfDay;

                if ((now >= HappyHourDurationFrom && now <= HappyHourDurationTo) && HappyHourDurationFrom < HappyHourDurationTo)
                {
                    price = fb.Foodbeveragehappyhourprice.Value;
                    if (price > 0)
                    {
                        return price;
                    }
                    else if (fb.Foodbeveragehappyhourpercentage.Value > 0)
                    {
                        return fb.Foodbeverageprice - (fb.Foodbeverageprice * fb.Foodbeveragehappyhourpercentage / 100);
                    }
                }
                if ((now >= SecondHappyHourDurationFrom && now <= SecondHappyHourDurationTo) && SecondHappyHourDurationFrom < SecondHappyHourDurationTo)
                {
                    price = fb.Foodbeveragesecondhappyhourprice.Value;
                    if (price > 0)
                    {
                        return price;
                    }
                    else if (fb.Foodbeveragesecondhappyhourpercentage.Value > 0)
                    {
                        return fb.Foodbeverageprice - (fb.Foodbeverageprice * fb.Foodbeveragesecondhappyhourpercentage / 100);
                    }
                }
                if ((now >= ThirdHappyHourDurationFrom && now <= ThirdHappyHourDurationTo) && ThirdHappyHourDurationFrom < ThirdHappyHourDurationTo)
                {
                    price = fb.Foodbeveragethirdhappyhourprice.Value;
                    if (price > 0)
                    {
                        return price;
                    }
                    else if (fb.Foodbeveragethirdhappyhourpercentage.Value > 0)
                    {
                        return fb.Foodbeverageprice - (fb.Foodbeverageprice * fb.Foodbeveragethirdhappyhourpercentage / 100);
                    }
                }
            }
            return fb.Foodbeverageprice;

            #region George's Codes

            //bool iFoundHappyHourFlag = false;

            //if (IsHappyHourDisableToday(CurrentSalesPK) == false && ReadHappyHourDurationFrom > ReadHappyHourDurationTo)
            //{
            //    DateTime idatetimefrom = DateTime.Parse(string.Format("00:00", ReadHappyHourDurationFrom));
            //    DateTime idatetimeto = DateTime.Parse(string.Format("00:00", ReadHappyHourDurationTo));
            //    idatetimeto = idatetimeto.AddDays(1);

            //    DateTime idatetimenow = DateTime.Parse(string.Format("00:00", DateTime.Now));

            //    if (DateTime.Compare(idatetimefrom.ToString("t"), idatetimenow.ToString("t")) <= 0 | DateTime.Compare(idatetimenow.ToString("t"), idatetimeto.ToString("t")) <= 0)
            //    {
            //        //str(2) is price, change it to happy hour rate
            //        double dbl = 0;
            //        dbl = i.Foodbeveragehappyhourprice.Value;// GetHappyHourPrice(Conversion.str(0));
            //        if (dbl > 0)
            //        {
            //            Conversion.str(2) = dbl;
            //            hh1Price = dbl;
            //            nPrice = 0;
            //            iFoundHappyHourFlag = true;
            //        }

            //        if (dbl == 0)
            //        {
            //            double per = 0;
            //            per = i.Foodbeveragehappyhourpercentage.Value;// GetHappyHourPercentage(Conversion.str(0));
            //            if (per > 0)
            //            {
            //                hhPercentage = per;
            //                iFoundHappyHourFlag = true;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (IsHappyHourDisableToday(CurrentSalesPK) == false && DateTime.Compare(ReadHappyHourDurationFrom.ToString("t"), System.DateTime.Now.ToString("t")) <= 0 && DateTime.Compare(System.DateTime.Now.ToString("t"), ReadHappyHourDurationTo.ToString("t")) <= 0)
            //    {
            //        //str(2) is price, change it to happy hour rate
            //        double dbl = 0;
            //        dbl = i.Foodbeveragehappyhourprice.Value;// GetHappyHourPrice(Conversion.str(0));

            //        if (dbl > 0)
            //        {
            //            Conversion.str(2) = dbl;
            //            hh1Price = dbl;
            //            nPrice = 0;
            //            iFoundHappyHourFlag = true;
            //        }

            //        if (dbl == 0)
            //        {
            //            double per = 0;
            //            per = i.Foodbeveragehappyhourpercentage.Value;// GetHappyHourPercentage(Conversion.str(0));
            //            if (per > 0)
            //            {
            //                hhPercentage = per;
            //                iFoundHappyHourFlag = true;
            //            }
            //        }
            //    }
            //}

            //if (IsHappyHourDisableToday(CurrentSalesPK) == false && ReadSecondHappyHourDurationFrom > ReadSecondHappyHourDurationTo)
            //{
            //    DateTime idatetimefrom = DateTime.Parse(ReadSecondHappyHourDurationFrom);
            //    DateTime idatetimeto = DateTime.Parse(ReadSecondHappyHourDurationTo);
            //    idatetimeto = idatetimeto.AddDays(1);

            //    DateTime idatetimenow = DateTime.Parse(Strings.Format(DateTime.Now, "HH:mm"));

            //    if (DateTime.Compare(idatetimefrom.ToString("t"), idatetimenow.ToString("t")) <= 0 | DateTime.Compare(idatetimenow.ToString("t"), idatetimeto.ToString("t")) <= 0 && iFoundHappyHourFlag == false)
            //    {
            //        double dbl = 0;
            //        dbl = i.Foodbeveragesecondhappyhourprice.Value; // GetSecondHappyHourPrice(Conversion.str(0));

            //        if (dbl > 0)
            //        {
            //            Conversion.str(2) = dbl;
            //            hh2Price = dbl;
            //            nPrice = 0;
            //            iFoundHappyHourFlag = true;
            //        }

            //        if (dbl == 0)
            //        {
            //            double per = 0;
            //            per = i.Foodbeveragesecondhappyhourpercentage.Value;// GetSecondHappyHourPercentage(Conversion.str(0));
            //            if (per > 0)
            //            {
            //                hhPercentage = per;
            //                iFoundHappyHourFlag = true;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (IsHappyHourDisableToday(CurrentSalesPK) == false && DateTime.Compare(ReadSecondHappyHourDurationFrom.ToString("t"), System.DateTime.Now.ToString("t")) <= 0 && DateTime.Compare(System.DateTime.Now.ToString("t"), ReadSecondHappyHourDurationTo.ToString("t")) <= 0 && iFoundHappyHourFlag == false)
            //    {
            //        double dbl = 0;
            //        dbl = i.Foodbeveragesecondhappyhourprice.Value; //  GetSecondHappyHourPrice(Conversion.str(0));

            //        if (dbl > 0)
            //        {
            //            Conversion.str(2) = dbl;
            //            hh2Price = dbl;
            //            nPrice = 0;
            //            iFoundHappyHourFlag = true;
            //        }

            //        if (dbl == 0)
            //        {
            //            double per = 0;
            //            per = i.Foodbeveragesecondhappyhourpercentage.Value;//  GetSecondHappyHourPercentage(Conversion.str(0));
            //            if (per > 0)
            //            {
            //                hhPercentage = per;
            //                iFoundHappyHourFlag = true;
            //            }
            //        }
            //    }
            //}

            //if (IsHappyHourDisableToday(CurrentSalesPK) == false && ReadThirdHappyHourDurationFrom > ReadThirdHappyHourDurationTo)
            //{
            //    DateTime idatetimefrom = ReadThirdHappyHourDurationFrom;
            //    DateTime idatetimeto = ReadThirdHappyHourDurationTo;
            //    idatetimeto = idatetimeto.AddDays(1);

            //    DateTime idatetimenow = DateTime.Parse(Strings.Format(DateTime.Now, "HH:mm"));

            //    if (DateTime.Compare(idatetimefrom.ToString("t"), idatetimenow.ToString("t")) <= 0 | DateTime.Compare(idatetimenow.ToString("t"), idatetimeto.ToString("t")) <= 0 && iFoundHappyHourFlag == false)
            //    {
            //        double dbl = 0;
            //        dbl = i.Foodbeveragethirdhappyhourprice.Value;  //GetThirdHappyHourPrice(Conversion.str(0));

            //        if (dbl > 0)
            //        {
            //            Conversion.str(2) = dbl;
            //            hh3Price = dbl;
            //            nPrice = 0;
            //            iFoundHappyHourFlag = true;
            //        }

            //        if (dbl == 0)
            //        {
            //            double per = 0;
            //            per = i.Foodbeveragethirdhappyhourpercentage.Value;// GetThirdHappyHourPercentage(Conversion.str(0));
            //            if (per > 0)
            //            {
            //                hhPercentage = per;
            //                iFoundHappyHourFlag = true;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (IsHappyHourDisableToday(CurrentSalesPK) == false && DateTime.Compare(ReadThirdHappyHourDurationFrom.ToString("t"), System.DateTime.Now.ToString("t")) <= 0 && DateTime.Compare(System.DateTime.Now.ToString("t"), ReadThirdHappyHourDurationTo.ToString("t")) <= 0 && iFoundHappyHourFlag == false)
            //    {
            //        double dbl = 0;
            //        dbl = i.Foodbeveragethirdhappyhourprice.Value;  //GetThirdHappyHourPrice(Conversion.str(0));

            //        if (dbl > 0)
            //        {
            //            Conversion.str(2) = dbl;
            //            hh3Price = dbl;
            //            nPrice = 0;
            //            iFoundHappyHourFlag = true;
            //        }

            //        if (dbl == 0)
            //        {
            //            double per = 0;
            //            per = i.Foodbeveragethirdhappyhourpercentage.Value;//GetThirdHappyHourPercentage(Conversion.str(0));
            //            if (per > 0)
            //            {
            //                hhPercentage = per;
            //                iFoundHappyHourFlag = true;
            //            }
            //        }
            //    }
            //}
            #endregion
        }
        catch
        {
            return fb.Foodbeverageprice;
            // throw new Exception("Registry can not be read.");
        }
    }

    private bool IsHappyHourDisableToday()
    {
        DayOfWeek now = DateTime.Now.DayOfWeek;
        if ((now == DayOfWeek.Friday && RegistryReader.DisableHappyHourOnFriday)
        || (now == DayOfWeek.Monday && RegistryReader.DisableHappyHourOnMonday)
        || (now == DayOfWeek.Saturday && RegistryReader.DisableHappyHourOnSaturday)
        || (now == DayOfWeek.Sunday && RegistryReader.DisableHappyHourOnSunday)
        || (now == DayOfWeek.Thursday && RegistryReader.DisableHappyHourOnThursday)
        || (now == DayOfWeek.Tuesday && RegistryReader.DisableHappyHourOnTuesday)
        || (now == DayOfWeek.Wednesday && RegistryReader.DisableHappyHourOnWednesday))
            return true;
        return false;
    }

    //MenuSessionMng mSession = new MenuSessionMng();
    public bool PaymentExpress(string table, string counterCode)
    {
        localhost.Ordering or = new localhost.Ordering();
        int salesPK = or.GetAnySalesPKFromTableName(counterCode, table);
        or.AddExpressCashPayment(salesPK, counterCode, false, true);
        return true;
    }
}

public static class RegistryReader
{
    public static bool IsRegistryAvailable
    {
        get
        {
            try
            {
                RegistryPermission f = new RegistryPermission(RegistryPermissionAccess.Read, @"SOFTWARE\FBPOS\HappyHourDurationFrom");
                string hhKey = @"SOFTWARE\FBPOS";
                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey,true);
                string hhFrom = rk.GetValue("HappyHourDurationFrom").ToString();
                DateTime.Parse(hhFrom);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public static DateTime HappyHourDurationFrom_First
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string hhFrom = rk.GetValue("HappyHourDurationFrom").ToString();
            return DateTime.Parse(hhFrom);
        }
    }

    public static DateTime HappyHourDurationTo_First
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string hhFrom = rk.GetValue("HappyHourDurationTo").ToString();
            return DateTime.Parse(hhFrom);
        }
    }

    public static DateTime HappyHourDurationFrom_Second
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string hhFrom = rk.GetValue("SecondHappyHourDurationFrom").ToString();
            return DateTime.Parse(hhFrom);
        }
    }

    public static DateTime HappyHourDurationTo_Second
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string hhFrom = rk.GetValue("SecondHappyHourDurationTo").ToString();
            return DateTime.Parse(hhFrom);
        }
    }

    public static DateTime HappyHourDurationFrom_Third
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string hhFrom = rk.GetValue("ThirdHappyHourDurationFrom").ToString();
            return DateTime.Parse(hhFrom);
        }
    }

    public static DateTime HappyHourDurationTo_Third
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string hhFrom = rk.GetValue("ThirdHappyHourDurationTo").ToString();
            return DateTime.Parse(hhFrom);
        }
    }

    public static bool DescriptionOnKitchenOrder
    {
        get
        {
            try
            {
                string hhKey = @"SOFTWARE\FBPOS";
                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
                string val = rk.GetValue("DescriptionOnKitchenOrder").ToString();
                return val == "True" ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public static bool AlternateDescriptionOnKitchenOrder
    {
        get
        {
            try
            {
                string hhKey = @"SOFTWARE\FBPOS";
                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
                string val = rk.GetValue("AlternateDescriptionOnKitchenOrder").ToString();
                return val == "True" ? true : false;
            }
            
            catch (Exception ex)
            {
                return false;
            }
        }
    }


    //Disable Days on pricing happy hours.
    public static bool DisableHappyHourOnFriday
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string val = rk.GetValue("DisableHappyHourOnFriday").ToString();
            return val == "True" ? true : false;
        }
    }
    public static bool DisableHappyHourOnMonday
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string val = rk.GetValue("DisableHappyHourOnMonday").ToString();
            return val == "True" ? true : false;
        }
    }
    public static bool DisableHappyHourOnSaturday
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string val = rk.GetValue("DisableHappyHourOnSaturday").ToString();
            return val == "True" ? true : false;
        }
    }
    public static bool DisableHappyHourOnSunday
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string val = rk.GetValue("DisableHappyHourOnSunday").ToString();
            return val == "True" ? true : false;
        }
    }
    public static bool DisableHappyHourOnTakeAway
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string val = rk.GetValue("DisableHappyHourOnTakeAway").ToString();
            return val == "True" ? true : false;
        }
    }
    public static bool DisableHappyHourOnThursday
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string val = rk.GetValue("DisableHappyHourOnThursday").ToString();
            return val == "True" ? true : false;
        }
    }
    public static bool DisableHappyHourOnTuesday
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string val = rk.GetValue("DisableHappyHourOnTuesday").ToString();
            return val == "True" ? true : false;
        }
    }

    public static bool DisableHappyHourOnWednesday
    {
        get
        {
            string hhKey = @"SOFTWARE\FBPOS";
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(hhKey);
            string val = rk.GetValue("DisableHappyHourOnWednesday").ToString();
            return val == "True" ? true : false;
        }
    }



}