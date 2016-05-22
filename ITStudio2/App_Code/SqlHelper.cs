using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// SqlHelper 的摘要说明
/// </summary>
public class SqlHelper
{
	public SqlHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string ConnStr = @"server=(localdb)\v11.0;Integrated Security=SSPI;database=ITStudio;";
    static public DataSet SelectDS(String select)
    {
        using (SqlConnection conn = new SqlConnection(ConnStr))
        {
            DataSet ds = new DataSet();
            {
                conn.Open();//打开
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);//进行填充
            }
            return ds;//返回这个数据集
        }
    }
    static public DataTable Select(string sql)
    {
        using (SqlConnection conn = new SqlConnection(ConnStr))
        {
            DataTable dt = new DataTable();
            {
                conn.Open();//打开
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);//进行填充
            }
            return dt;
        }
    }
    static public int SqlExecute(string ex)
    {
        using (SqlConnection conn = new SqlConnection(ConnStr))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(ex, conn);
            return Convert.ToInt32(cmd.ExecuteScalar());
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
}