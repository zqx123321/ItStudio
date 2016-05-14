using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addMember : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("~/login.aspx");

        if (!IsPostBack)
        {
            bindYears();
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
                return null;
            }

            if (fulPhoto.FileContent.Length > maxFileSize) // 限制为3MiB以下
            {
                lblUploadMessage.Text = "图片文件大小不可超过 3 MB";
                lblUploadMessage.Visible = true;
                return null;
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
            lblUploadMessage.Text = "请选择文件";
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
                return null;
            }

            if (fulIco.FileContent.Length > maxFileSize) // 限制为3MiB以下
            {
                lblUploadMessage.Text = "图片文件大小不可超过 3 MB";
                lblUploadMessage.Visible = true;
                return null;
            }
            string f1 = DateTime.Now.ToFileTime().ToString() + ".jpg";
            string path = "./Images/" + f1;
            fulIco.SaveAs(Server.MapPath("./Images/" + f1));
            lblUploadMessage.Text = "上传成功";
            lblUploadMessage.Visible = true;
            return path;
        }
        else
        {
            lblUploadMessage.Text = "请选择文件";
            return null;
        }
    }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;
            string content = txtIntroduction.InnerText;
            content = content.Replace("\n", "<br />"); // 尝试替换\n为<br />
            string photo = uploadWorkPho();
            string ico = uploadWorkIco();
            using (var db = new ITStudioEntities())
            {
                var mem = new members();
                mem.grade = Convert.ToInt32(ddlGrade.SelectedValue);
                mem.photo = photo;
                mem.ico = ico;
                mem.name = name;
                mem.introduction = content;
                mem.direction =" ";
                mem.job = ddlDeparement.SelectedValue;
                db.members.Add(mem);
                db.SaveChanges();
            }
            Response.Write("<script>alert('上传成功!'),location='addMember.aspx' </script>");
        }
}