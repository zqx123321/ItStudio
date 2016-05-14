using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("~/login.aspx");
        if (!IsPostBack)
        {
            DataBindApplication();
            DataBindWork();
            DataBindAdmin();
        }
    }

    protected void DataBindApplication()
    {
        using (var db = new ITStudioEntities())
        {
            var select = from it in db.applications select it;
            application.DataSource = select.ToList();
            application.DataBind();
        }
    }
    protected void DataBindWork()
    {
        using (var db = new ITStudioEntities())
        {
            var select = from it in db.works select it;
            RptWork.DataSource = select.ToList();
            RptWork.DataBind();
        }
    }
    protected void DataBindAdmin()
    {
        using (var db = new ITStudioEntities())
        {
            var select = from it in db.admins select it;
            RptAdmin.DataSource = select.ToList();
            RptAdmin.DataBind();
        }
    }
}