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

///引入新的名字空间
using ASPNETAJAXWeb.AjaxEBusiness;

public partial class ViewProductByCategoryTime : System.Web.UI.Page
{
	private int categoryID = -1;

	protected void Page_Load(object sender,EventArgs e)
	{	///获取商品种类的ID
		if(Request.Params["CategoryID"] != null)
		{
			categoryID = Int32.Parse(Request.Params["CategoryID"].ToString());
		}

		///显示商品数据
		if(!Page.IsPostBack && categoryID > 0)
		{   ///绑定数据初始化
			gvProduct.DataSource = null;
			gvProduct.DataBind();
			BindPageData(categoryID);
		}
	}

	private void BindPageData(int categoryID)
	{
		Product product = new Product();
        DataSet ds = product.GetProductByFenlei(categoryID);
		if(ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)return;

		///设置按最后访问时间排序
		DataView dv = ds.Tables[0].DefaultView;
		dv.Sort = "LasterDate DESC";
		gvProduct.DataSource = dv;
		gvProduct.DataBind();
	}
	protected void gvProduct_RowCommand(object sender,GridViewCommandEventArgs e)
	{
		if(e.CommandName == "buy")
		{
			ShoppingCartItem item = new ShoppingCartItem();
			int rowIndex = Int32.Parse(e.CommandArgument.ToString());
			if(rowIndex <= -1 || rowIndex >= gvProduct.Rows.Count) return;
			///获取商品ID和数量
			item.ProductID = Int32.Parse(gvProduct.DataKeys[rowIndex]["ID"].ToString());
			item.Number = 1;
			///获取商品名称
			Label lbName = (Label)gvProduct.Rows[rowIndex].FindControl("lbName");
			if(lbName != null)
			{
				item.Name = lbName.Text;
			}
			///获取商品价格
			Label lbPrice = (Label)gvProduct.Rows[rowIndex].FindControl("lbPrice");
			if(lbPrice != null)
			{
				item.Price = decimal.Parse(lbPrice.Text);
			}

			ShoppingCart shoppingCart = new ShoppingCart(Session);
			if(shoppingCart.AddProductToShoppingCart(item) > -1)
			{
				AjaxEBusinessSystem.ShowAjaxDialog((Button)e.CommandSource,"恭喜您，添加商品到购物车成功。");
			}
		}
	}
	protected void gvProduct_RowDataBound(object sender,GridViewRowEventArgs e)
	{
		Button btnBuy = (Button)e.Row.FindControl("btnBuy");
		if(btnBuy != null)
		{   ///设置CommandArgument属性的值为当前行的索引
			btnBuy.CommandArgument = e.Row.RowIndex.ToString();
		}
	}
}
