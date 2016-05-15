using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class editWork : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("~/login.aspx");

        if (Request.QueryString["id"] == null || Request.QueryString["id"].Length > 8 || !Filter.IsNumeric(Request.QueryString["id"]))
        {
            Response.Redirect("404.aspx");
        }
        else
        {
            using (var db = new ITStudioEntities())
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                if (db.works.SingleOrDefault(a => a.id == id) == null)
                {
                    Response.Redirect("404.aspx");
                }
            }
        }
        if (!IsPostBack)
        {
            int id = Convert.ToInt16(Request.QueryString["id"]);
            using (var db = new ITStudioEntities())
            {
                works work = db.works.SingleOrDefault(a => a.id == id);
                txtTitle.Text = work.title;
                txtIntroduction.InnerText = work.introduction;
                txtLink.Text = work.link;
                txtTime.Text = work.time;
                ImgCurrentWorkPic.ImageUrl = work.picture;
                ddlType.SelectedValue = work.typeId.ToString();
                //作者               
                txtmaker.Text = work.author;
            }

        }
    }




     string uploadWorkPic() //上传封面图片，返回文件名。
    {
        int maxFileSize = 3145728; // 限制为3MiB以下
        if (fulPicture.HasFile)
        {
            //取得文件MIME内容类型 
            string uploadFileType = this.fulPicture.PostedFile.ContentType.ToLower();
            if (!uploadFileType.Contains("image"))    //图片的MIME类型为"image/xxx"，这里只判断是否图片。 
            {
                lblUploadMessage.Visible = true;
                lblUploadMessage.Text = "只能上传图片类型文件！";
                
                return "error";
            }

            if (fulPicture.FileContent.Length > maxFileSize) // 限制为3MiB以下
            {   
                lblUploadMessage.Visible = true;
                lblUploadMessage.Text = "图片文件大小不可超过 3 MB";

                return "error";
            }

            //string picPath = fulPicture.PostedFile.FileName;
            //string picFileName = fulPicture.FileName;
            //string picFileExtension = picFileName.Substring(picFileName.LastIndexOf('.')); //带.的扩展名
            ////string random = RandomStatic.ProduceIntRandom(0, 999999).ToString("D6"); //6位随机数
            //picSaveName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + picFileExtension; //当前时间

            ////取得文件在服务器上保存的位置C:\Inetpub\wwwroot\WebSite1\images\20022775_m.jpg 
            //string serverpath = Server.MapPath("../upload/workPicture/") + picSaveName;
            string f1 = DateTime.Now.ToFileTime().ToString() + ".jpg";
            string path = "./Images/" + f1;
            fulPicture.SaveAs(Server.MapPath("./Images/"+f1)); 
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
        string title = txtTitle.Text;
        string content = txtIntroduction.InnerText;
        string workPicName = uploadWorkPic();
        string time = txtTime.Text;
        int id = Convert.ToInt32(Request.QueryString["id"]);

        //删除旧封面图片
        if (workPicName == "error") return;
        else//此时：已上传新封面图片，文件名未写入数据库
        {
            string oldCoverPic = "";
            using (var db = new ITStudioEntities())
            {
                works w = db.works.SingleOrDefault(a => a.id == id);
                if (w == null)
                {
                    return;
                }
                oldCoverPic = w.picture;
                string oldCoverPicPath = "/upload/workPicture/" + oldCoverPic; //相对路径
                oldCoverPicPath = Server.MapPath(oldCoverPicPath); //必须经过这一步操作才能变成有效路径
                if (System.IO.File.Exists(oldCoverPicPath))//先判断文件是否存在，再执行操作
                {
                    System.IO.File.Delete(oldCoverPicPath); //删除文件
                }
            }
        }
        using (var db = new ITStudioEntities())
        {
            //修改works表
            works work = db.works.SingleOrDefault(a => a.id == id);
            work.typeId = Convert.ToInt32(ddlType.SelectedValue);
            if (workPicName != null&&workPicName!="error")
            {
                work.picture = workPicName; // 修改了图片的情况
                ImgCurrentWorkPic.ImageUrl = workPicName;
            }
            work.title = title;
            work.introduction = content;
            work.time = txtTime.Text;
            work.link = txtLink.Text;
            //修改workmap表
            work.author=txtmaker.Text;
            db.SaveChanges();
        }
        Response.Write("<script>alert('修改成功!'),location='seeWork.aspx' </script>");
    }
}