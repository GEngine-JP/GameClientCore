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

public partial class ShoppingCart_ViewShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!Page.IsPostBack)
		{
			BindPageData();
		}
    }

	private void BindPageData()
	{   ///获取购物车的商品
		ShoppingCart shoppingCart = new ShoppingCart(Session);
		///绑定数据并显示商品
		gvProduct.DataSource = shoppingCart.ShoppingCartList;
		gvProduct.DataBind();
	}

	protected void gvProduct_RowCommand(object sender,GridViewCommandEventArgs e)
	{
		if(e.CommandName.ToLower() == "del")
		{	///获取购物车的商品
			ShoppingCart shoppingCart = new ShoppingCart(Session);
			if(shoppingCart == null || shoppingCart.ShoppingCartList == null || shoppingCart.ShoppingCartList.Count <= 0) return;
			///创建被删除的商品
			ShoppingCartItem deleteItem = new ShoppingCartItem();
			deleteItem.ProductID = Int32.Parse(e.CommandArgument.ToString());
			///删除选中的商品
			shoppingCart.DeleteProductFromShoppingCart(deleteItem);
			///重新绑定商品数据
			BindPageData();
		}		
	}

	protected void gvProduct_RowDataBound(object sender,GridViewRowEventArgs e)
	{   ///添加删除确认的对话框
		ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
		if(imgDelete != null)
		{
			imgDelete.Attributes.Add("onclick","return confirm(\"您确认要删除当前行的商品吗？\");");
		}
	}
	protected void btnCommit_Click(object sender,EventArgs e)
	{
		Response.Redirect("~/Order/AddOrder.aspx");
	}
	protected void btnAdd_Click(object sender,EventArgs e)
	{
		Response.Redirect("~/Product.aspx?CategoryID=27");
	}
	
	protected void btnStore_Click(object sender,EventArgs e)
	{   ///获取购物车的商品
		ShoppingCart shoppingCart = new ShoppingCart(Session);
		if(shoppingCart == null || shoppingCart.ShoppingCartList == null || shoppingCart.ShoppingCartList.Count <= 0) return;
		///检查购物车中的商品和显示的商品是否相等，如果不相等，则数据错误。
		if(shoppingCart.ShoppingCartList.Count != gvProduct.Rows.Count) return;

		ArrayList products = new ArrayList();
		foreach(GridViewRow row in gvProduct.Rows)
		{   ///找到输入商品数量的控件
			TextBox tbNumber = (TextBox)row.FindControl("tbNumber");
			if(tbNumber == null) return;
			///获取商品数量
			int number = -1;
			if(Int32.TryParse(tbNumber.Text.Trim(),out number) == false) return;
			///创建一个子项，并添加到临时数组中
			ShoppingCartItem item = new ShoppingCartItem();
			///设置子项的名称、数量、价格和商品ID值
			item.Name = ((ShoppingCartItem)shoppingCart.ShoppingCartList[row.RowIndex]).Name;
			item.Number = number;
			item.Price = ((ShoppingCartItem)shoppingCart.ShoppingCartList[row.RowIndex]).Price;
			item.ProductID = ((ShoppingCartItem)shoppingCart.ShoppingCartList[row.RowIndex]).ProductID;
			products.Add(item);
		}
		///修改购物车中的商品数量
		shoppingCart.UpdateShoppingCart(products);
	}
}
