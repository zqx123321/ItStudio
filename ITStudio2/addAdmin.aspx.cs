using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["user"] == null)
                Response.Redirect("~/login.aspx");

    }

    public class getHash
    {
        /// <summary>
        /// 获取string类字符串的的十六进制大写SHA-1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getSHA1(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                //建立SHA1对象
                SHA1 sha = new SHA1CryptoServiceProvider();

                //将mystr转换成byte[]
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(str);

                //Hash运算
                sha.ComputeHash(dataToHash);

                //转换为 string
                string hash = BitConverter.ToString(sha.Hash).Replace("-", "");
                return hash;
            }
            else
            {
                return string.Empty;
            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string newName = TxtNewAdminName.Text;
        if (newName.Length > 16)
        {
            Response.Write("<script>alert('新管理员登录名长度应小于16位')</script>");
            return;
        }
        if (TxtNewAdminPassword.Text != TxtNewAdminPasswordAgain.Text)
        {
            Response.Write("<script>alert('两次密码不一致！')</script>");
            return;
        }
        else if (TxtNewAdminPassword.Text.Length < 6 || TxtNewAdminPassword.Text.Length > 16)
        {
            Response.Write("<script>alert('密码位数应在6到16位！')</script>");
            return;
        }
        using (var db = new ITStudioEntities()) //检验是否已存在此登录名
        {
            admins admin = db.admins.SingleOrDefault(a => a.name == newName);
            if (admin != null)
            {
                Response.Write("<script>alert('此管理员已存在!')</script>");
                return;
            }
        }

        string sha = getHash.getSHA1(TxtNewAdminPassword.Text);
        using (var db = new ITStudioEntities())
        {
            var a = new admins();
            a.name = TxtNewAdminName.Text;
            a.password = sha;
            db.admins.Add(a);
            db.SaveChanges();
        }
        Response.Write("<script>alert('管理员添加成功!'),location='addAdmin.aspx'</script>");
    }
}