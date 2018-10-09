using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

///引入新的命名空间
using ASPNETAJAXWeb.AjaxEBusiness;

public partial class Category_AddCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!Page.IsPostBack)
		{
			BindPageData();
		}
    }

	private void BindPageData()
	{   ///获取数据
		Category category = new Category();
		///显示分类的层次结构
		category.InitFenleiList(ddlCategory);
		///设置第一项为选择项，如果存在
		if(ddlCategory.Items.Count > 0)
		{
			ddlCategory.SelectedIndex = 0;
		}
	}

	protected void btnCommit_Click(object sender,EventArgs e)
	{   ///添加新的分类
		Category category = new Category();
		if(category.AddFenlei(tbName.Text,Int32.Parse(ddlCategory.SelectedValue),tbRemark.Text) > 0)
		{   ///重定向到管理页面
			Response.Redirect("~/Category/Classes.aspx");
		}
	}
}
