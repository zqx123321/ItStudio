using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class editMember : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("~/login.aspx");

        if (Request.QueryString["id"] == null || Request.QueryString["id"].Length > 8||!Filter.IsNumeric(Request.QueryString["id"]))
        {
            Response.Redirect("404.aspx");
        }
        using (var db = new ITStudioEntities())
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            if (db.members.SingleOrDefault(a => a.id == id) == null)
            {
                Response.Redirect("404.aspx");
            }
        }
        if (!IsPostBack)
        {
            bindYears();
            int id = Convert.ToInt16(Request.QueryString["id"]);
            using (var db = new ITStudioEntities())
            {
                members work = db.members.SingleOrDefault(a => a.id == id);
                txtName.Text = work.name;
                txtIntroduction.InnerText = work.introduction;
                ImgCurrentWorkPho.ImageUrl = work.photo;
                ImgCurrentWorkIco.ImageUrl = work.ico;
                ddlGrade.SelectedValue = work.grade.ToString();
                ddlDeparement.SelectedValue = work.job.ToString();
            }

        }
    }


    void bindYears()
    {
        int minYearOffset = 5; //相对今年的最小和最大偏移
        int maxYearOffset = 1;

        List<string> years = new List<string>();
        int thisYear = DateTime.Now.Year;
        for (int index = -minYearOffset; index <= maxYearOffset; index++)
        {
            years.Add((thisYear + index).ToString());
        }
        ddlGrade.DataSource = years;
        ddlGrade.DataBind();
        ddlGrade.SelectedValue = thisYear.ToString(); // 预选中今年年份
    }

    string uploadWorkPho() //上传封面图片，返回文件名。
    {
        int maxFileSize = 3145728; // 限制为3MiB以下
        if (fulPhoto.HasFile)
        {
            //取得文件MIME内容类型 
            string uploadFileType = this.fulPhoto.PostedFile.ContentType.ToLower();
            if (!uploadFileType.Contains("image"))    //图片的MIME类型为"image/xxx"，这里只判断是否图片。 
            {
                lblUploadMessage.Text = "只能上传图片类型文件！";
                lblUploadMessage.Visible = true;
                return "error";
            }

            if (fulPhoto.FileContent.Length > maxFileSize) // 限制为3MiB以下
            {
                lblUploadMessage.Text = "图片文件大小不可超过 3 MB";
                lblUploadMessage.Visible = true;
                return "error";
            }
            string f1 = DateTime.Now.ToFileTime().ToString() + ".jpg";
            string path = "./Images/" + f1;
            fulPhoto.SaveAs(Server.MapPath("./Images/" + f1));
            lblUploadMessage.Text = "上传成功";
            lblUploadMessage.Visible = true;
            return path;
        }
        else
        {
           
            return null;
        }
    }


    string uploadWorkIco() //上传封面图片，返回文件名。
    {
        int maxFileSize = 3145728; // 限制为3MiB以下
        if (fulIco.HasFile)
        {
            //取得文件MIME内容类型 
            string uploadFileType = this.fulIco.PostedFile.ContentType.ToLower();
            if (!uploadFileType.Contains("image"))    //图片的MIME类型为"image/xxx"，这里只判断是否图片。 
            {
                lblUploadMessage.Text = "只能上传图片类型文件！";
                lblUploadMessage.Visible = true;
                return "error";
            }

            if (fulIco.FileContent.Length > maxFileSize) // 限制为3MiB以下
            {
                lblUploadMessage.Text = "图片文件大小不可超过 3 MB";
                lblUploadMessage.Visible = true;
                return "error";
            }
            string f1 = DateTime.Now.ToFileTime().ToString() + ".jpg";
            string path = "./Images/" + f1;
            fulIco.SaveAs(Server.MapPath("./Images/" + f1));
            lblUploadMessage.Text = "上传成功";
            lblUploadMessage.Visible = true;
            return path;
        }
        else return null;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string content = txtIntroduction.InnerText;
        string workPhoName = uploadWorkPho();
        string workIcoName = uploadWorkIco();
        int id = Convert.ToInt32(Request.QueryString["id"]);
        if (workPhoName == "error" || workIcoName=="error") return;
        using (var db = new ITStudioEntities())
        {
            //修改works表
            members work = db. members.SingleOrDefault(a => a.id == id);
            work.grade = Convert.ToInt32(ddlGrade.SelectedValue);
            work.job = ddlDeparement.SelectedValue.ToString();
            if (workPhoName != null)
            {
                work.photo = workPhoName; // 修改了图片的情况
                ImgCurrentWorkPho.ImageUrl = workPhoName;
            }
            if (workIcoName != null)
            {
                work.ico = workIcoName; // 修改了图片的情况
                ImgCurrentWorkIco.ImageUrl = workIcoName;
            }
            work.name = name;
            db.SaveChanges();
        }
        Response.Write("<script>alert('修改成功!'),location='seeMember.aspx' </script>");
    }

}