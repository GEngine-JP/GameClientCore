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

public partial class Product_ProductUC_ProductAttribute:System.Web.UI.UserControl
{   
	/// <summary>
	/// 显示属性的父控件
	/// </summary>
	public Panel AttributePanel
	{
		get
		{
			return pAttribute;
		}
	}

	/// <summary>
	/// 商品分类的ID值
	/// </summary>
	public int CategoryID
	{
		get
		{
			return Int32.Parse(ViewState["CategoryID"].ToString());
		}
	}

	private const string AttributeCONTROL = "AttributeCONTROL";

	private int productID = -1;
	protected void Page_Load(object sender,EventArgs e)
	{   ///获取商品ID
		if(Request.Params["ProductID"] != null)
		{
			productID = Int32.Parse(Request.Params["ProductID"].ToString());
		}
		///此时为修改商品
		if(productID > 0)
		{
			CreateAttributeControl(-1,productID,true);
			ddlCategory.Enabled = ddlCategoryS.Enabled = ddlCategoryT.Enabled = false;
			return;
		}
		///获取属性的控件
		GetAttributeControl();
		//此时为添加新的商品
		if(!Page.IsPostBack)
		{
			BindCategoryData(1);
		}		
	}

	/// <summary>
	/// 获取保存在Session中的控件列表
	/// </summary>
	private void GetAttributeControl()
	{
		if(Session[Session.SessionID + AttributeCONTROL] == null) return;
		ArrayList AttributeControls = (ArrayList)Session[Session.SessionID + AttributeCONTROL];
		///创建属性的控件
		pAttribute.Controls.Clear();
		foreach(ArrayList controls in AttributeControls)
		{
			foreach(Control c in controls)
			{
				pAttribute.Controls.Add(c);
			}
			pAttribute.Controls.Add(new LiteralControl("<br />"));
		}
	}	

	/// <summary>
	/// 添加新产品时，显示产品的属性
	/// </summary>
	/// <param name="categoryID"></param>
	private void BindCategoryData(int categoryID)
	{
		Category category = new Category();
		DataSet ds = category.GetSubFenlei(categoryID);
		if(ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;	
		///添加子类
		ddlCategory.Items.Clear();
		foreach(DataRow row in ds.Tables[0].Rows)
		{
			ddlCategory.Items.Add(
				new ListItem(row["Name"].ToString(),row["ID"].ToString()));
		}
		if(ddlCategory.Items.Count > 0)
		{   ///一级种类不为空
			ddlCategory.SelectedIndex = 0;
			ddlCategory.Visible = true;
			BindCategorySData(Int32.Parse(ddlCategory.SelectedValue));			
		}
		else
		{   ///一级种类为空
			ddlCategory.Visible = false;
			ddlCategoryS.Visible = false;
			ddlCategoryT.Visible = false;
		}
	}

	private void BindCategorySData(int categoryID)
	{
		Category category = new Category();
        DataSet ds = category.GetSubFenlei(categoryID);
		if(ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;	
		///添加子类
		ddlCategoryS.Items.Clear();
		foreach(DataRow row in ds.Tables[0].Rows)
		{
			ddlCategoryS.Items.Add(
				new ListItem(row["Name"].ToString(),row["ID"].ToString()));
		}
		if(ddlCategoryS.Items.Count > 0)
		{   ///二级种类不为空
			ddlCategoryS.SelectedIndex = 0;	
			ddlCategoryS.Visible = true;
			BindCategoryTData(Int32.Parse(ddlCategoryS.SelectedValue));
		}
		else
		{   ///二级种类不为空
			ddlCategoryS.Visible = false;
			ddlCategoryT.Visible = false;
			///创建一级种类的属性控件
			CreateAttributeControl(Int32.Parse(ddlCategory.SelectedValue),productID,false);
		}
	}

	/// <summary>
	/// 显示三级种类
	/// </summary>
	/// <param name="categoryID"></param>
	private void BindCategoryTData(int categoryID)
	{
		Category category = new Category();
        DataSet ds = category.GetSubFenlei(categoryID);
		if(ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
		///添加子类
		ddlCategoryT.Items.Clear();
		foreach(DataRow row in ds.Tables[0].Rows)
		{
			ddlCategoryT.Items.Add(
				new ListItem(row["Name"].ToString(),row["ID"].ToString()));
		}
		if(ddlCategoryT.Items.Count > 0)
		{
			ddlCategoryT.SelectedIndex = 0;
			CreateAttributeControl(Int32.Parse(ddlCategoryT.SelectedValue),productID,false);
			ddlCategoryT.Visible = true;
		}
		else
		{
			ddlCategoryT.Visible = false;
			///创建二级种类的属性控件
			CreateAttributeControl(Int32.Parse(ddlCategoryS.SelectedValue),productID,false);			
		}
	}

	protected void ddlCategory_SelectedIndexChanged(object sender,EventArgs e)
	{
		BindCategorySData(Int32.Parse(ddlCategory.SelectedValue));
	}
	protected void ddlCategoryS_SelectedIndexChanged(object sender,EventArgs e)
	{
		BindCategoryTData(Int32.Parse(ddlCategoryS.SelectedValue));
	}
	protected void ddlCategoryT_SelectedIndexChanged(object sender,EventArgs e)
	{
		CreateAttributeControl(Int32.Parse(ddlCategoryT.SelectedValue),productID,false);		
	}	

	public void CreateAttributeControl(int categoryID,int productID,bool isEdit)
	{	///设置当前分类的ID值
		ViewState["CategoryID"] = categoryID;
		///清空控件的子控件
		pAttribute.Controls.Clear();
		//获取与该种类的属性
		Product product = new Product();
		DataSet ds = null;
		if(isEdit == false)
		{   ///添加商品时的操作
            ds = product.GetAttributeByFenlei(categoryID);
		}
		else
		{   ///修改商品时的操作
			ds = product.GetAttributeByProduct(productID);
		}
		if(ds == null) return;
		if(ds.Tables.Count <= 0) return;
		ArrayList AttributeControls = new ArrayList();
		///创建新控件		
		foreach(DataRow row in ds.Tables[0].Rows)
		{  
			ArrayList Controls = new ArrayList();
			///创建标签控件
			Label lbName = new Label();
			lbName.ID = "lb" + row["Name"].ToString();
			lbName.Text = row["Text"].ToString() + ":";
			lbName.Width = 200;
			pAttribute.Controls.Add(lbName);
			Controls.Add(lbName);
			///创建文本框控件
			TextBox tbName = new TextBox();
			tbName.ID = "tb" + row["Name"].ToString();
			tbName.Attributes.Add("IDValue",row["ID"].ToString());
			tbName.Width = 400;
			if(isEdit == true)
			{
				tbName.Text = row["Value"].ToString();
			}
			pAttribute.Controls.Add(tbName);
			Controls.Add(tbName);
			///创建单位控件
			Label lbUnit = new Label();
			lbUnit.ID = "lbUnit" + row["Name"].ToString();
			lbUnit.Text = row["Unit"].ToString();
			pAttribute.Controls.Add(lbUnit);
			Controls.Add(lbUnit);
			///添加换行符号
			pAttribute.Controls.Add(new LiteralControl("<br />"));
			AttributeControls.Add(Controls);
		}
		Session[Session.SessionID + AttributeCONTROL] = AttributeControls;
	}
}
