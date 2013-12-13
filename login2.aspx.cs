using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestaurantpoContext;


public partial class login2 : System.Web.UI.Page
{
    DCDC dc = new DCDC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            txtEmpCode.Focus();

        }
    }
    MenuSessionMng mSession = new MenuSessionMng();
   
    private bool CheckEmpCode(string p)
    {
        var emp = dc.Tblemployees.FirstOrDefault(z => z.Employeecode.ToLower() == p.ToLower());
        if (emp != null)
        {
            //lblError.Text = emp.Employeecode;
            return true;
        }
        else return false;
    }


    private void LoadFonts()
    {

        //lblError.Font.Size = 18; //mSession.AppFont;
        //txtCounterCode.Font.Size = 18; //mSession.AppFont;
        //btnLogin.Font.Size = 18;//mSession.AppFont;
       
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {

    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {

    }
}