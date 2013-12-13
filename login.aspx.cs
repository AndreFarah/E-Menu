using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections.Generic;
using System.Linq;
using RestaurantpoContext;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

public partial class login : System.Web.UI.Page
{
    MenuSessionMng mSession = new MenuSessionMng();
    DCDC dc = new DCDC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



            var q = from i in dc.Tblcounters
                    select new
                    {
                        Code = i.Countercode,
                        Description = i.Counterdescription,
                    };
            cmbCounterCode.DataSource = q.ToList();
            cmbCounterCode.DataBind();

            if (mSession.CounterCode != "")
                cmbCounterCode.SelectedValue = mSession.CounterCode;
            else
                cmbCounterCode.SelectedValue = q.ToList()[0].Code;


        }

        if (Request.Cookies["EmployeeCode"] != null && Request.Cookies["CounterCode"] != null)
        {
            if (Request.Cookies["EmployeeCode"].Value != "" && Request.Cookies["CounterCode"].Value != "")
            {
                mSession.EmployeeCode = txtEmployeeCode.Text = Server.HtmlEncode(Request.Cookies["EmployeeCode"].Value);
                mSession.CounterCode = cmbCounterCode.SelectedValue = Server.HtmlEncode(Request.Cookies["CounterCode"].Value);
                Login();
            }
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Login();
    }

    private void Login()
    {
        try
        {
            // string AuthenticationString = "";
            //if (!string.IsNullOrEmpty(hfEmpCode.Value))
            //{
            //AuthenticationString = hfEmpCode.Value; //Request.Form["txtAuthenticationString"];
            if (CheckUserAudit(txtEmployeeCode.Text))
            {
                lblError.Text = "";
                mSession.CounterCode = cmbCounterCode.SelectedValue;


                Response.Cookies["EmployeeCode"].Value = txtEmployeeCode.Text;
                Response.Cookies["EmployeeCode"].Expires = DateTime.Now.AddDays(10);

                Response.Cookies["CounterCode"].Value = mSession.CounterCode;
                Response.Cookies["CounterCode"].Expires = DateTime.Now.AddDays(10);


                Response.Redirect("default.aspx", false);
                //                    FormsAuthentication.DefaultUrl = "~/PublicModule/Default.aspx";
                //FormsAuthentication.RedirectFromLoginPage(, false);
            }
            else
            {
                Response.Cookies["EmployeeCode"].Value = null;


                Response.Cookies["CounterCode"].Value = null;

                lblError.Text = "Invalid 'Employee Code'";

            }
            //}
            //else 
            //{

            //}
        }
        catch (Exception ex)
        {
            RadMessageBox.RadMsgBox msg = new RadMessageBox.RadMsgBox();
            msg.ShowAlert(ex.ToString());
        }
    }


    public bool CheckUserAudit(string empCode)
    {
        try
        {
            DCDC dc = new DCDC();
            //if (dc.Tblemployees.ToList().Where(p => MD5Encoding.ComputeMD5(p.Employeecode.ToLower()) == AuthenticationString).Count() > 0)
            var y = dc.Tblfoodbeveragegroups.ToList();
            var x = dc.Tblemployees.ToList();

            if (dc.Tblemployees.ToList().Where(p => p.Employeecode.ToLower() == empCode.ToLower()).Count() > 0)
            {
                //mSession.AuthenticationString = AuthenticationString;
                mSession.EmployeeCode = empCode;
                return true;
            }
            else
            {
                //mSession.AuthenticationString = null;
                mSession.EmployeeCode = "";
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}