using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Edu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string filePath = ConfigFile.EducationImageURL;
            // ConfigFile.EducationImageURL"];
        btnImg.ImageUrl = "~/Images/Edu.jpg";
    }
    protected void btnImg_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}