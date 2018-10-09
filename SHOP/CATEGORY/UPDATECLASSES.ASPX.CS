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
using System.Data.SqlClient;

public partial class Category_UpdateCategory : System.Web.UI.Page
{
	int categoryID = -1;
	protected void Page_Load(object sender,EventArgs e)
	{   ///获取被修改数据的ID
		if(Request.Params["CategoryID"] != null)
		{
			categoryID = Int32.Parse(Request.Params["CategoryID"].ToString());
		}
		///显示被修改的数据
		if(!Page.IsPostBack && categoryID > 0)
		{
			BindPageData(categoryID);
		}
		///设置按钮是否可用
		btnCommit.Enabled = categoryID > 0 ? true : false;
	}

	private void BindPageData(int categoryID)
	{   ///读取数据
		Category category = new Category();
		///显示分类的层次结构
        category.InitFenleiList(ddlCategory);
        SqlDataReader dr = category.GetSingleFenlei(categoryID);
		if(dr == null) return;
		if(dr.Read())
		{   ///显示数据
			tbName.Text = dr["Name"].ToString();
			tbRemark.Text = dr["Remark"].ToString();
			AjaxEBusinessSystem.ListSelectedItemByValue(ddlCategory,dr["ParentID"].ToString());
		}
		dr.Close();
	}

	protected void btnCommit_Click(object sender,EventArgs e)
	{
		Category category = new Category();
		///修改分类的属性
        if (category.UpdateFenlei(categoryID, tbName.Text, tbRemark.Text) > 0)
		{	///重定向到管理页面		
			Response.Redirect("~/Category/Classes.aspx");
		}
	}
}
