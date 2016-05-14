using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addWork : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("~/login.aspx");
    }


    void submitWork(int typeId, string title, string time, string introduction, string workPicName, string link,string author)
    {
        using (var db =new ITStudioEntities())
        {
            var work = new works(); // 要添加的作品
            work.typeId = typeId;
            work.title = title;
            work.time = time;
            work.picture = workPicName;
            work.link = link;
            work.introduction = introduction;
            work.author = author;
            db.works.Add(work);
            db.SaveChanges();

            
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
                lblUploadMessage.Text = "只能上传图片类型文件！";
                lblUploadMessage.Visible = true;
                return null;
            }

            if (fulPicture.FileContent.Length > maxFileSize) // 限制为3MiB以下
            {
                lblUploadMessage.Text = "图片文件大小不可超过 3 MB";
                lblUploadMessage.Visible = true;
                return null;
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


    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string title = txtTitle.Text;
        string introduction = txtIntroduction.InnerText;
        string workPicName = uploadWorkPic();
        string time = txtTime.Text; 
        int typeId = Convert.ToInt32(ddlType.SelectedValue);
        string link = txtLink.Text;
        string author = txtmaker.Text;
        submitWork(typeId, title, time, introduction, workPicName, link,author); // 添加作品
        Response.Write("<script>alert('上传成功!'),location='addWork.aspx' </script>");
    }
}