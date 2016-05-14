using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class top : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("~/login.aspx");

        if (Session["user"] != null)
            adminname.Text =Convert.ToString( Session["user"]);
    }
    protected void logout_Click(object sender, ImageClickEventArgs e)
    {
        Session["user"] = null;
       // ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('退出成功'),location='login.aspx'</script>");
    }
}