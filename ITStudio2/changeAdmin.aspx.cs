using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class changeAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("~/login.aspx");

        if (!IsPostBack)
        {
            string user = Convert.ToString( Session["user"]) ;
            using (var db = new ITStudioEntities())
            {
                admins work = db.admins.SingleOrDefault(a => a.name == user);
                TxtNewAdminName.Text= work.name;
            }

        }

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
        string user = Convert.ToString(Session["user"]);
        string oldPassword = TxtOldPassword.Text;
        string newPassword = TxtNewPassword.Text;
        if (newPassword.Length < 6 || newPassword.Length > 16)
        {
            Response.Write("<script>alert('密码位数应在6到16位')</script>");
            return;
        }
        string oldPasswordHash = getHash.getSHA1(oldPassword);
        string newPasswordHash = getHash.getSHA1(newPassword);
        using (var db = new ITStudioEntities())
        {
            admins c2 = db.admins.SingleOrDefault(c => c.name == user);
            if (c2 == null)
            {
                return;
            }
            if (newPassword != "" && oldPassword != "")
            {
                if (c2.password.ToString() == oldPasswordHash)
                {
                    c2.password = newPasswordHash;
                    db.SaveChanges();
                    Response.Write("<script>alert('密码修改成功！'),location='changeAdmin.aspx'</script>");
                }
                else
                    Response.Write("<script>alert('原密码错误！')</script>");
            }
            else
                Response.Write("<script>alert('请完整输入！')</script>");
        }
    }
}