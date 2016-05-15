using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Applications : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("~/login.aspx");

        if (!IsPostBack)
        {
            Session["pagenum"] = 1;
            int currentPage = 1;
            int pageSize = getPageSize();
            ArticlesBind(currentPage, pageSize);
        }
    }

    protected void RptArticles_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete") //删除文章
        {
            string IDStr = e.CommandArgument.ToString().Trim();
            int id = Convert.ToInt32(IDStr);
            using (var db = new ITStudioEntities())
            {
                applications del = db.applications.SingleOrDefault(a => a.id == id);
                db.applications.Remove(del);
                db.SaveChanges();
            }
            ArticlesBind(getPageNum(), getPageSize());
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功');</script>");
        }
    }
    void ArticlesBind(int CurrentPage, int PageSize) //文章绑定
    {
        using (var db = new ITStudioEntities())
        {
            var dataSource = from items in db.applications
                             orderby items.id descending
                             select new { items.id, items.name, items.gender, items.major, items.job, items.tel, items.time, items.introduction };
            int totalAmount = dataSource.Count();
            Session["pageCount"] = Math.Ceiling((double)totalAmount / (double)PageSize); //总页数，向上取整
            dataSource = dataSource.Skip(PageSize * (CurrentPage - 1)).Take(PageSize); //分页
            RptArticles.DataSource = dataSource.ToList();
            RptArticles.DataBind();

            LtlArticlesCount.Text = totalAmount.ToString();
        }
    }
    protected void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e) // pageSize下拉列表改变
    {
        ArticlesBind(1, getPageSize()); //从第一页绑定，防止单页项目数量变多，导致页码超出范围。
        TxtPageNum.Text = "1";
        Session["pagenum"] = 1;
    }

    int getPageCount(int pageSize) //获得总页数
    {
        int pageCount = 1;
        if (Session["pageCount"] == null)
        {
            using (var db = new ITStudioEntities())
            {
                var dataSource = from items in db.applications
                                 orderby items.id
                                 select new { items };
                int totalAmount = dataSource.Count();
                pageCount = (int)Math.Ceiling((double)totalAmount / (double)pageSize); //总页数，向上取整
            }
            Session["pageCount"] = pageCount;
        }
        else
        {
            pageCount = Convert.ToInt32(Session["pageCount"]);
        }
        return pageCount;
    }

    int getPageSize() //获得页面项目数目
    {
        int pageSize = 5;
        if (DdlPageSize.SelectedValue != null)
        {
            pageSize = Convert.ToInt32(DdlPageSize.SelectedValue);
        }
        return pageSize;
    }

    int getPageNum() //获得当前文本框中的合法数字页码
    {
        int pageNum = Convert.ToInt16(Session["pagenum"]);
        return pageNum;
    }

    protected void BtnPreviousPage_Click(object sender, EventArgs e)
    {
        int pageNum = Convert.ToInt16(Session["pagenum"]) - 1;
        int pageSize = getPageSize();
        if (pageNum < 1)
        {
            pageNum = 1;
            return;
        }
        Session["pagenum"] = pageNum;
        ArticlesBind(pageNum, pageSize);
        TxtPageNum.Text = pageNum.ToString();
    }

    protected void BtnNextPage_Click(object sender, EventArgs e)
    {
        int pageNum = Convert.ToInt16(Session["pagenum"]) + 1;
        int pageSize = getPageSize();
        if (pageNum >= getPageCount(pageSize))
        {
            pageNum = getPageCount(pageSize);
        }
        Session["pagenum"] = pageNum;
        ArticlesBind(pageNum, pageSize);
        TxtPageNum.Text = pageNum.ToString();
    }

    protected void BtnHomePage_Click(object sender, EventArgs e)
    {
        ArticlesBind(1, getPageSize());
        TxtPageNum.Text = "1";
        Session["pagenum"] = 1;
    }

    protected void BtnTrailerPage_Click(object sender, EventArgs e)
    {
        int pageSize = getPageSize();
        int pageNum = getPageCount(pageSize);
        if (pageNum <= 0) //没有内容的情况
        {
            pageNum = 1;
        }
        Session["pagenum"] = pageNum;
        ArticlesBind(pageNum, pageSize);
        TxtPageNum.Text = pageNum.ToString();
    }

    protected void BtnJumpPage_Click(object sender, EventArgs e)
    {
        int pageNum = getPageNum();
        int pageSize = getPageSize();
        if (pageNum < 1)
        {
            pageNum = 1;
        }
        else if (pageNum > pageSize)
        {
            pageNum = getPageCount(pageSize);
        }
        ArticlesBind(pageNum, pageSize);
        TxtPageNum.Text = pageNum.ToString();
    }
    protected void BtnImport_Click(object sender, EventArgs e)
    {
        if (DdlSelect.SelectedValue == "1")
        {
            ExportDataGrid("美术设计", "application/ms-excel", "美工.xls");
        }
        else if (DdlSelect.SelectedValue == "2")
        {
            ExportDataGrid("程序开发", "application/ms-excel", "程序.xls");
        }
        else if (DdlSelect.SelectedValue == "3")
        {
            ExportDataGrid("系统维护", "application/ms-excel", "维护.xls");
        }
    }

    private void ExportDataGrid(string Job, string FileType, string FileName) //从DataGrid导出  
    {
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();

        using (var db = new ITStudioEntities())
        {
            var dt = from it in db.applications
                     where it.job == Job
                     select it;
            dg.DataSource = dt.ToList();
        }
        dg.DataBind();

        //定义文档类型、字符编码　　   
        Response.Clear();
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
        Response.Charset = "UTF-8";
        Response.ContentEncoding = Encoding.Default;
        Response.ContentType = FileType;
        dg.EnableViewState = false;
        //定义一个输入流　　   
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        //目标数据绑定到输入流输出　  
        dg.RenderControl(hw); 
        Response.Write(tw.ToString());
        Response.End();
    }  
}